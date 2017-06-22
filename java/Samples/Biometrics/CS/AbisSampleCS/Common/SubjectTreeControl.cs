using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class SubjectTreeControl : UserControl, INotifyPropertyChanged
	{
		#region Private types

		private class NodeTag
		{
			#region Public constructors

			public NodeTag(object tag)
			{
				Items = new List<NBiometric>();
				ObjectTag = tag;
				SessionId = -1;
				if (tag is NSubject.FingerCollection)
					Type = NBiometricType.Finger;
				else if (tag is NSubject.FaceCollection)
					Type = NBiometricType.Face;
				else if (tag is NSubject.IrisCollection)
					Type = NBiometricType.Iris;
				else if (tag is NSubject.PalmCollection)
					Type = NBiometricType.Palm;
				else if (tag is NSubject.VoiceCollection)
					Type = NBiometricType.Voice;
			}

			public NodeTag(params NBiometric[] biometrics)
			{
				Items = new List<NBiometric>(biometrics);
				var first = biometrics.First();
				Type = first.BiometricType;
				SessionId = first.SessionId;
				if (Type == NBiometricType.Finger || Type == NBiometricType.Palm)
				{
					NFrictionRidge ridge = (NFrictionRidge)first;
					Position = ridge.Position;
					ImpressionType = ridge.ImpressionType;
				}
			}

			#endregion

			#region Public properties

			public object ObjectTag { get; set; }
			public List<NBiometric> Items { get; set; }
			public NBiometricType Type { get; set; }
			public int SessionId { get; set; }
			public NFPosition Position { get; set; }
			public NFImpressionType ImpressionType { get; set; }

			#endregion

			#region Public methods

			public bool HasTag(object tag)
			{
				return object.Equals(ObjectTag, tag)
					|| (Items != null && tag is NBiometric && Items.Contains((NBiometric)tag));
			}

			public bool BelongsToNode(object tag)
			{
				NBiometric item = tag as NBiometric;
				if (item != null && item.BiometricType == Type)
				{
					if (Type == NBiometricType.Finger || Type == NBiometricType.Palm)
					{
						NFrictionRidge frictionRidge = (NFrictionRidge)item;
						if (Position != frictionRidge.Position || ImpressionType != frictionRidge.ImpressionType) return false;
					}
					if (Type == NBiometricType.Face && Items.Any())
					{
						if (SessionId == item.SessionId)
						{
							if ((item.ParentObject == null) ^ (Items.First().ParentObject == null)) return false;
						}
					}
					if (SessionId == -1 && item.SessionId == -1) return Items.Contains(item);
					if (SessionId == item.SessionId) return true;
					if (SessionId != -1 && item.SessionId == -1)
					{
						NBiometricAttributes parentObject = item.ParentObject;
						if (parentObject != null)
						{
							NBiometric parent = (NBiometric)parentObject.Owner;
							return Items.Contains(parent);
						}
					}
				}
				return false;
			}

			#endregion
		};

		#endregion

		#region Public types

		public class Node
		{
			#region Public properties

			public bool IsNewNode { get; internal set; }
			public bool IsSubjectNode { get; internal set; }
			public bool IsBiometricNode { get; internal set; }
			public bool IsGeneralizedNode { get; set; }

			public NBiometricType BiometricType { get; private set; }
			public NBiometric[] AllItems { get; private set; }
			public NBiometric[] Items { get; private set; }
			public NBiometric[] GeneralizedItems { get; private set; }

			#endregion

			#region Internal fields
			internal TreeNode _node;
			#endregion

			#region Internal constructor
			internal Node(TreeNode node)
			{
				NodeTag tag = node.Tag as NodeTag;

				_node = node;

				BiometricType = tag.Type;
				IsSubjectNode = tag.ObjectTag is NSubject;
				IsBiometricNode = tag.Items.Count > 0;
				IsGeneralizedNode = tag.Items.Count > 1;
				AllItems = tag.Items.ToArray();
				Items = tag.Items.Where(x => !SubjectUtils.IsBiometricGeneralizationResult(x)).ToArray();
				GeneralizedItems = tag.Items.Where(x => SubjectUtils.IsBiometricGeneralizationResult(x)).ToArray();
			}
			#endregion

			#region Public methods

			public Node GetParent()
			{
				TreeNode parent = _node.Parent;
				return parent != null ? new Node(parent) : null;
			}

			public Node[] GetChildren()
			{
				return (from TreeNode n in _node.Nodes select new Node(n)).ToArray();
			}

			public NBiometric[] GetAllGeneralized()
			{
				List<NBiometric> result = new List<NBiometric>();
				result.AddRange(GeneralizedItems);
				foreach (var ch in GetChildren())
				{
					result.AddRange(ch.GetAllGeneralized());
				}
				return result.Distinct().ToArray();
			}

			public override bool Equals(object obj)
			{
				Node target = (Node)obj;
				return target != null && _node == target._node;
			}

			public override int GetHashCode()
			{
				return _node.GetHashCode();
			}

			#endregion
		}

		#endregion

		#region Public constructor
		public SubjectTreeControl()
		{
			InitializeComponent();
			DoubleBuffered = true;
			EnabledChanged += new EventHandler(SubjectTreeControlEnabledChanged);
		}
		#endregion

		#region Private fields

		private bool _showBiometricsOnly = false;
		private bool _allowRemove = true;
		private NSubject _subject;
		private NBiometricType _shownTypes = NBiometricType.Finger | NBiometricType.Face | NBiometricType.Iris | NBiometricType.Voice | NBiometricType.Palm;
		private NBiometricType _allowNew = NBiometricType.Finger | NBiometricType.Face | NBiometricType.Iris | NBiometricType.Voice | NBiometricType.Palm;

		#endregion

		#region Public properties

		public NSubject Subject
		{
			get
			{
				return _subject;
			}
			set
			{
				if (_subject != value)
				{
					if (_subject != null)
					{
						_subject.Fingers.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
						_subject.Faces.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
						_subject.Palms.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
						_subject.Irises.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
						_subject.Voices.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
					}
					_subject = value;
					if (_subject != null)
					{
						_subject.Fingers.CollectionChanged += new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
						_subject.Faces.CollectionChanged += new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
						_subject.Palms.CollectionChanged += new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
						_subject.Irises.CollectionChanged += new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
						_subject.Voices.CollectionChanged += new NotifyCollectionChangedEventHandler(OnSubjectCollectionChanged);
					}
					OnSubjectChanged();
				}
			}
		}

		public NBiometricType ShownTypes
		{
			get { return _shownTypes; }
			set
			{
				if (_shownTypes != value)
				{
					_shownTypes = value;
					SelectedItem = null;
					OnSubjectChanged();
				}
			}
		}

		public NBiometricType AllowNew
		{
			get { return _allowNew; }
			set
			{
				if (_allowNew != value)
				{
					_allowNew = value;
					if (_allowNew != NBiometricType.None) _showBiometricsOnly = false;
					SelectedItem = null;
					OnSubjectChanged();
				}
			}
		}

		public bool ShowBiometricsOnly
		{
			get { return _showBiometricsOnly; }
			set
			{
				if (_showBiometricsOnly != value)
				{
					_showBiometricsOnly = value;
					SelectedItem = null;
					if (_showBiometricsOnly) _allowNew = NBiometricType.None;
					OnSubjectChanged();
				}
			}
		}

		public Node SelectedItem
		{
			get
			{
				TreeNode node = treeView.SelectedNode;
				return node != null ? new Node(node) : null;
			}
			set
			{
				TreeNode node = treeView.SelectedNode;
				Node current = node != null ? new Node(node) : null;
				if (current != value)
				{
					if (value == null)
					{
						bool changed = treeView.SelectedNode != null;
						treeView.SelectedNode = null;
						if (Enabled && changed && PropertyChanged != null)
							PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
					}
					else
					{
						treeView.SelectedNode = value._node;
					}
					tsbRemove.Enabled = current != null && current.IsBiometricNode;
				}
			}
		}

		public bool AllowRemove
		{
			get { return _allowRemove; }
			set
			{
				if (value != _allowRemove)
				{
					_allowRemove = value;
					if (value)
					{
						tableLayoutPanel.RowStyles[0].SizeType = SizeType.AutoSize;
					}
					else
					{
						tableLayoutPanel.RowStyles[0].SizeType = SizeType.Absolute;
						tableLayoutPanel.RowStyles[0].Height = 0;
					}
				}
			}
		}

		public Node[] Nodes
		{
			get
			{
				return (from TreeNode n in treeView.Nodes select new Node(n)).ToArray();
			}
		}

		#endregion

		#region Private methods

		private TreeNode GetNodeWithTag(object tag)
		{
			foreach (TreeNode node in treeView.Nodes)
			{
				NodeTag t = node.Tag as NodeTag;
				if (t.HasTag(tag) || t.BelongsToNode(tag)) return node;

				TreeNode n = GetNodeWithTag(node, tag);
				if (n != null) return n;
			}
			return null;
		}

		private TreeNode GetNodeWithTag(TreeNode rootNode, object tag)
		{
			foreach (TreeNode node in rootNode.Nodes)
			{
				NodeTag t = node.Tag as NodeTag;
				if (t.HasTag(tag) || t.BelongsToNode(tag)) return node;

				TreeNode n = GetNodeWithTag(node, tag);
				if (n != null) return n;
			}
			return null;
		}

		private TreeNode CreateFingerNode(NFinger[] allFingers, params NFinger[] fingers)
		{
			NodeTag tag = new NodeTag(fingers);
			TreeNode node = new TreeNode(string.Empty) { Tag = tag };
			NFinger first = fingers.First();
			foreach (NFAttributes attribute in first.Objects.ToArray())
			{
				NFinger child = (NFinger)attribute.Child;
				if (child != null && !fingers.Contains(child) && !tag.BelongsToNode(child))
				{
					var grouped = SubjectUtils.GetFingersInSameGroup(allFingers, child).ToArray();
					node.Nodes.Add(CreateFingerNode(allFingers, grouped));
				}
			}
			UpdateFingerNodeText(node);
			return node;
		}

		private void UpdateFingerNodeText(TreeNode node)
		{
			NodeTag tag = node.Tag as NodeTag;
			if (tag != null)
			{
				NFrictionRidge first = tag.Items.FirstOrDefault() as NFrictionRidge;
				if (first != null)
				{
					string format = "{0}{1}";
					string rolled = string.Empty;
					if (NBiometricTypes.IsImpressionTypeRolled(first.ImpressionType)) rolled = ", Rolled";
					string text = string.Format(format, first.Position, rolled);
					if (tag.Items.Count > 1)
						text += ", Generalized";
					node.Text = text;
				}
			}
		}

		private TreeNode CreateFaceNode(NFace [] allFaces, params NFace [] faces)
		{
			NodeTag tag = new NodeTag(faces);
			TreeNode node = new TreeNode(string.Empty) { Tag = tag };
			NFace first = faces.First();
			foreach (NLAttributes attributes in first.Objects.ToArray())
			{
				NFace child = (NFace)attributes.Child;
				if (child != null && !faces.Contains(child) && !tag.BelongsToNode(child))
				{
					var grouped = SubjectUtils.GetFacesInSameGroup(allFaces, child).ToArray();
					node.Nodes.Add(CreateFaceNode(allFaces, grouped));
				}
			}
			UpdateFaceNodeText(node);
			return node;
		}

		private void UpdateFaceNodeText(TreeNode node)
		{
			NodeTag tag = node.Tag as NodeTag;
			if (tag != null)
			{
				string text = "Face";
				NFace face = tag.Items.Last() as NFace;
				if (face != null)
				{
					NLAttributes attributes = face.Objects.FirstOrDefault();
					bool generalized = tag.Items.Any(f => f.SessionId != -1);
					bool segmented = tag.Items.All(f => f.ParentObject != null);
					if (!segmented && attributes != null && attributes.Quality != NBiometricTypes.QualityUnknown)
						text += string.Format(" (Quality={0})", attributes.Quality);
					if (segmented)
						text += ", Segmented";
					if (generalized)
						text += ", Generalized";
					node.Text = text;
				}
			}
		}

		private TreeNode CreateIrisNode(NIris iris)
		{
			TreeNode node = new TreeNode(string.Format("Iris ({0})", iris.Position)) { Tag = new NodeTag(iris) };
			foreach (NEAttributes attribute in iris.Objects.ToArray())
			{
				NIris child = (NIris)attribute.Child;
				if (child != null)
				{
					node.Nodes.Add(CreateIrisNode(child));
				}
			}
			return node;
		}

		private TreeNode CreateVoiceNode(NVoice voice)
		{
			string text = string.Format("Voice ({0})", voice.ParentObject != null ? "Segmented" : "Phrase id: " + voice.PhraseId);
			TreeNode node = new TreeNode(text) { Tag = new NodeTag(voice) };
			foreach (NSAttributes attribute in voice.Objects.ToArray())
			{
				NVoice child = (NVoice)attribute.Child;
				if (child != null)
				{
					node.Nodes.Add(CreateVoiceNode(child));
				}
			}
			return node;
		}

		private TreeNode CreatePalmNode(params NPalm[] palms)
		{
			TreeNode node = new TreeNode(string.Empty) { Tag = new NodeTag(palms) };
			UpdateFingerNodeText(node);
			return node;
		}

		private void OnSubjectChanged()
		{
			Node selected = SelectedItem;
			treeView.BeginUpdate();
			try
			{
				treeView.Nodes.Clear();
				if (_subject != null)
				{
					List<TreeNode> fingerNodes = new List<TreeNode>();
					List<TreeNode> faceNodes = new List<TreeNode>();
					List<TreeNode> irisNodes = new List<TreeNode>();
					List<TreeNode> palmNodes = new List<TreeNode>();
					List<TreeNode> voiceNodes = new List<TreeNode>();

					bool allowNewFingers = (_allowNew & NBiometricType.Finger) == NBiometricType.Finger;
					bool allowNewFaces = (_allowNew & NBiometricType.Face) == NBiometricType.Face;
					bool allowNewIrises = (_allowNew & NBiometricType.Iris) == NBiometricType.Iris;
					bool allowNewVoices = (_allowNew & NBiometricType.Voice) == NBiometricType.Voice;
					bool allowNewPalms = (_allowNew & NBiometricType.Palm) == NBiometricType.Palm;

					if ((_shownTypes & NBiometricType.Finger) == NBiometricType.Finger)
					{
						var allFingers = _subject.Fingers.ToArray();
						var groups = SubjectUtils.GetFingersGeneralizationGroups(allFingers);
						var topLevelGroups = groups.Where(g => g.All(x => x.SessionId == -1 || x.ParentObject == null)).ToArray();
						foreach (var g in topLevelGroups)
						{
							fingerNodes.Add(CreateFingerNode(allFingers, g));
						}
						if (!_showBiometricsOnly) fingerNodes.Add(new TreeNode("New...") { Tag = new NodeTag(_subject.Fingers), ForeColor = allowNewFingers ? Color.Black : SystemColors.GrayText });
					}
					if ((_shownTypes & NBiometricType.Face) == NBiometricType.Face)
					{
						var allFaces = _subject.Faces.ToArray();
						var groups = SubjectUtils.GetFaceGeneralizationGroups(allFaces);
						var topLevelGroups = groups.Where(g => g.All(x => x.SessionId == -1 || x.ParentObject == null)).ToArray();
						foreach (var g in topLevelGroups)
						{
							faceNodes.Add(CreateFaceNode(allFaces, g));
						}
						if (!_showBiometricsOnly) faceNodes.Add(new TreeNode("New...") { Tag = new NodeTag(_subject.Faces), ForeColor = allowNewFaces ? Color.Black : SystemColors.GrayText });
					}
					if ((_shownTypes & NBiometricType.Iris) == NBiometricType.Iris)
					{
						foreach (var item in _subject.Irises.ToArray().Where(x => x.ParentObject == null))
						{
							irisNodes.Add(CreateIrisNode(item));
						}
						if (!_showBiometricsOnly) irisNodes.Add(new TreeNode("New...") { Tag = new NodeTag(_subject.Irises), ForeColor = allowNewIrises ? Color.Black : SystemColors.GrayText });
					}
					if ((_shownTypes & NBiometricType.Palm) == NBiometricType.Palm)
					{
						foreach (var group in SubjectUtils.GetPalmsGeneralizationGroups(_subject))
						{
							palmNodes.Add(CreatePalmNode(group));
						}
						if (!_showBiometricsOnly) palmNodes.Add(new TreeNode("New...") { Tag = new NodeTag(_subject.Palms), ForeColor = allowNewPalms ? Color.Black : SystemColors.GrayText });
					}
					if ((_shownTypes & NBiometricType.Voice) == NBiometricType.Voice)
					{
						foreach (var item in _subject.Voices.ToArray().Where(x => x.ParentObject == null))
						{
							voiceNodes.Add(CreateVoiceNode(item));
						}
						if (!_showBiometricsOnly) voiceNodes.Add(new TreeNode("New...") { Tag = new NodeTag(_subject.Voices), ForeColor = allowNewVoices ? Color.Black : SystemColors.GrayText });
					}

					if (ShowBiometricsOnly)
					{
						treeView.Nodes.AddRange(fingerNodes.ToArray());
						treeView.Nodes.AddRange(faceNodes.ToArray());
						treeView.Nodes.AddRange(irisNodes.ToArray());
						treeView.Nodes.AddRange(palmNodes.ToArray());
						treeView.Nodes.AddRange(voiceNodes.ToArray());
					}
					else
					{
						TreeNode subjectNode = new TreeNode("Subject") { Tag = new NodeTag(_subject) };
						if ((_shownTypes & NBiometricType.Finger) == NBiometricType.Finger)
						{
							if (allowNewFingers || fingerNodes.Count > 1)
								subjectNode.Nodes.Add(new TreeNode("Fingers", fingerNodes.ToArray()) { Tag = new NodeTag(_subject.Fingers), ForeColor = allowNewFingers ? Color.Black : SystemColors.GrayText });
						}
						if ((_shownTypes & NBiometricType.Face) == NBiometricType.Face)
						{
							if (allowNewFaces || faceNodes.Count > 1)
								subjectNode.Nodes.Add(new TreeNode("Faces", faceNodes.ToArray()) { Tag = new NodeTag(_subject.Faces), ForeColor = allowNewFaces ? Color.Black : SystemColors.GrayText });
						}
						if ((_shownTypes & NBiometricType.Iris) == NBiometricType.Iris && irisNodes.Count != 0)
						{
							if (allowNewIrises || irisNodes.Count > 1)
								subjectNode.Nodes.Add(new TreeNode("Irises", irisNodes.ToArray()) { Tag = new NodeTag(_subject.Irises), ForeColor = allowNewIrises ? Color.Black : SystemColors.GrayText });
						}
						if ((_shownTypes & NBiometricType.Palm) == NBiometricType.Palm)
						{
							if (allowNewPalms || palmNodes.Count > 1)
								subjectNode.Nodes.Add(new TreeNode("Palms", palmNodes.ToArray()) { Tag = new NodeTag(_subject.Palms), ForeColor = allowNewPalms ? Color.Black : SystemColors.GrayText });
						}
						if ((_shownTypes & NBiometricType.Voice) == NBiometricType.Voice)
						{
							if (allowNewVoices || voiceNodes.Count > 1)
								subjectNode.Nodes.Add(new TreeNode("Voices", voiceNodes.ToArray()) { Tag = new NodeTag(_subject.Voices), ForeColor = allowNewVoices ? Color.Black : SystemColors.GrayText });
						}
						treeView.Nodes.Add(subjectNode);
					}
					treeView.ExpandAll();

					if (selected != null)
					{
						SelectedItem = GetNodeFor(selected.Items.First());
					}
				}
			}
			finally
			{
				treeView.EndUpdate();
			}
		}

		private void OnBiometricRemoved(NBiometric biometric)
		{
			TreeNode node = GetNodeWithTag(biometric);
			if (node != null)
			{
				int index = node.Index;
				TreeNode parent = node.Parent;
				NodeTag tag = (NodeTag)node.Tag;
				tag.Items.Remove(biometric);
				if (tag.Items.Count == 0)
				{
					var subNodes = (from TreeNode item in node.Nodes select item).ToArray();
					node.Nodes.Clear();
					node.Remove();
					foreach (TreeNode item in subNodes)
					{
						if (parent != null)
							parent.Nodes.Insert(index++, item);
						else
							treeView.Nodes.Insert(index++, item);
					}
				}
				if (!_showBiometricsOnly)
				{
					if (tag.Items.Count == 0 && parent != null && parent.Nodes.Count == 1)
					{
						tag = (NodeTag)parent.Tag;
						if (tag.Items.Count == 0 && (tag.Type & _allowNew) == NBiometricType.None)
						{
							parent.Remove();
						}
					}
				}
			}
		}

		private void OnBiometricAdded(NBiometric biometric)
		{
			TreeNode node = null;
			TreeNode parent = null;
			NBiometricAttributes parentObject = null;

			node = GetNodeWithTag(biometric);
			if (node != null)
			{
				NodeTag tag = (NodeTag)node.Tag;
				if (!tag.Items.Contains(biometric))
				{
					tag.Items.Add(biometric);
					if (tag.Type == NBiometricType.Face)
						UpdateFaceNodeText(node);
					else if (tag.Type == NBiometricType.Finger || tag.Type == NBiometricType.Palm)
						UpdateFingerNodeText(node);
				}
			}
			else
			{
				switch (biometric.BiometricType)
				{
					case NBiometricType.Face:
						{
							var allFaces = _subject.Faces.ToArray();
							var group = SubjectUtils.GetFacesInSameGroup(allFaces, (NFace)biometric).ToArray();
							node = CreateFaceNode(allFaces, group);
							parentObject = biometric.ParentObject;
							if (parentObject != null) parent = GetNodeWithTag(parentObject.Owner);
							if (parentObject == null && !ShowBiometricsOnly) parent = GetNodeWithTag(_subject.Faces);
							break;
						}
					case NBiometricType.Finger:
						{
							var fingers = _subject.Fingers.ToArray();
							var group = SubjectUtils.GetFingersInSameGroup(fingers, (NFinger)biometric).ToArray();
							node = CreateFingerNode(fingers, group);
							parentObject = biometric.ParentObject;
							if (parentObject != null) parent = GetNodeWithTag(parentObject.Owner);
							if (parentObject == null && !ShowBiometricsOnly) parent = GetNodeWithTag(_subject.Fingers);
							break;
						}
					case NBiometricType.Iris:
						{
							node = CreateIrisNode((NIris)biometric);
							parentObject = biometric.ParentObject;
							if (parentObject != null) parent = GetNodeWithTag(parentObject.Owner);
							if (parentObject == null && !ShowBiometricsOnly) parent = GetNodeWithTag(_subject.Irises);
							break;
						}
					case NBiometricType.Palm:
						{
							var palms = _subject.Palms.ToArray();
							var group = SubjectUtils.GetPalmsInSameGroup(palms, (NPalm)biometric).ToArray();
							node = CreatePalmNode(group);
							if (!ShowBiometricsOnly) parent = GetNodeWithTag(_subject.Palms);
							break;
						}
					case NBiometricType.Voice:
						{
							node = CreateVoiceNode((NVoice)biometric);
							parentObject = biometric.ParentObject;
							if (parentObject != null) parent = GetNodeWithTag(parentObject.Owner);
							if (parentObject == null && !ShowBiometricsOnly) parent = GetNodeWithTag(_subject.Voices);
							break;
						}
				}

				if (parent == null)
				{
					treeView.Nodes.Add(node);
					treeView.ExpandAll();
				}
				else
				{
					int index = parent.Nodes.Count;
					if ((_allowNew & biometric.BiometricType) == biometric.BiometricType && index != 0) index--;
					parent.Nodes.Insert(index, node);
					parent.ExpandAll();
				}
			}
		}

		private void OnSubjectCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (IsHandleCreated)
			{
				if (e.Action == NotifyCollectionChangedAction.Reset)
					BeginInvoke(new MethodInvoker(OnSubjectChanged));
				else if (e.Action == NotifyCollectionChangedAction.Remove)
				{
					BeginInvoke(new Action<IList>(oldItems =>
						{
							treeView.BeginUpdate();
							try
							{
								foreach (NBiometric item in oldItems)
								{
									OnBiometricRemoved(item);
								}
							}
							finally
							{
								treeView.EndUpdate();
							}
						}), e.OldItems);
				}
				else if (e.Action == NotifyCollectionChangedAction.Add)
				{
					BeginInvoke(new Action<IList>(newItems =>
						{
							treeView.BeginUpdate();
							try
							{
								foreach (NBiometric item in newItems)
								{
									OnBiometricAdded(item);
								}
							}
							finally
							{
								treeView.EndUpdate();
							}
						}), e.NewItems);
				}
			}
		}

		private void TreeViewAfterSelect(object sender, TreeViewEventArgs e)
		{
			var selected = SelectedItem;
			tsbRemove.Enabled = selected != null && selected.IsBiometricNode;
			if (Enabled && PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
			}
		}

		private void TsbRemoveClick(object sender, EventArgs e)
		{
			var selection = SelectedItem;
			SelectedItem = null;
			tsbRemove.Enabled = false;
			if (selection.IsBiometricNode)
			{
				switch (selection.BiometricType)
				{
					case NBiometricType.Face:
						{
							var parent = selection.GetParent();
							NBiometric[] allItems = selection.AllItems;
							if (parent != null && parent.IsBiometricNode)
							{
								allItems = Enumerable.Union(parent.Items, allItems).ToArray();
							}

							foreach (var face in allItems)
							{
								_subject.Faces.Remove((NFace)face);
							}
						}
						break;
					case NBiometricType.Palm:
						foreach (var item in selection.AllItems)
						{
							_subject.Palms.Remove(item as NPalm);
						}
						break;
					case NBiometricType.Voice:
						{
							NVoice voice = selection.AllItems.First() as NVoice;
							NVoice relatedVoice = null;
							if (voice.ParentObject != null)
								relatedVoice = (NVoice)voice.ParentObject.Owner;
							else
							{
								NSAttributes attributes = voice.Objects.FirstOrDefault();
								if (attributes != null)
									relatedVoice = attributes.Child as NVoice;
							}
							_subject.Voices.Remove(voice);
							_subject.Voices.Remove(relatedVoice);
							break;
						}
					case NBiometricType.Finger:
						{
							foreach (var item in selection.Items)
							{
								NFinger finger = item as NFinger;
								NFAttributes parentObject = (NFAttributes)finger.ParentObject;
								if (parentObject != null)
								{
									NFinger ownerFinger = (NFinger)parentObject.Owner;
									_subject.Fingers.Remove(ownerFinger);
								}
								_subject.Fingers.Remove(finger);
							}
							foreach (var item in selection.GeneralizedItems)
							{
								_subject.Fingers.Remove((NFinger)item);
							}
							break;
						}
					case NBiometricType.Iris:
						{
							NIris iris = selection.Items.First() as NIris;
							NEAttributes parentObject = (NEAttributes)iris.ParentObject;
							if (parentObject != null)
							{
								NIris ownerIris = (NIris)parentObject.Owner;
								_subject.Irises.Remove(ownerIris);
							}
							_subject.Irises.Remove(iris);
							break;
						}
				}
			}
		}

		private void TreeViewBeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = e.Node.ForeColor == SystemColors.GrayText;
		}

		private void SubjectTreeControlEnabledChanged(object sender, EventArgs e)
		{
			var selected = SelectedItem;
			tsbRemove.Enabled = selected != null && selected.IsBiometricNode;
		}

		#endregion

		#region Protected methods

		protected override void Dispose(bool disposing)
		{
			Subject = null;
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Public methods

		public void UpdateTree()
		{
			OnSubjectChanged();
		}

		public Node GetBiometricNode(NBiometric biometric)
		{
			return GetNodeFor(biometric);
		}

		public Node GetNewNode(NBiometricType biometricType)
		{
			if (_subject != null)
			{
				switch (biometricType)
				{
					case NBiometricType.Face: return GetNodeFor(_subject.Faces);
					case NBiometricType.Finger: return GetNodeFor(_subject.Fingers);
					case NBiometricType.Iris: return  GetNodeFor(_subject.Irises);
					case NBiometricType.Palm: return GetNodeFor(_subject.Palms);
					case NBiometricType.Voice: return GetNodeFor(_subject.Voices);
				}
			}
			return null;
		}

		public Node GetSubjectNode()
		{
			return GetNodeFor(_subject);
		}

		public Node GetNodeFor(object param)
		{
			if (param is Node) return (Node)param;
			if (param != null)
			{
				TreeNode nodeToSelect = GetNodeWithTag(param);
				if (nodeToSelect != null)
					return new Node(nodeToSelect);
			}
			return null;
		}

		#endregion

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
