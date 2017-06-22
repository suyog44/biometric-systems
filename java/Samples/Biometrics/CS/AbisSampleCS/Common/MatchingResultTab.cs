using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Gui;
using Neurotec.Images;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public partial class MatchingResultTab : Neurotec.Samples.TabPageContentBase
	{
		#region Private types

		private class MatchedFinger : MatchedPair<NFrictionRidge, NFMatchingDetails>
		{
			public MatchedFinger(NFrictionRidge f1, NFrictionRidge f2, NFMatchingDetails dt)
				: base(f1, f2, dt)
			{
			}

			public override string ToString()
			{
				return string.Format("Probe finger({0}) matched with gallery finger({1}). Score = {2}", First.Position, Second.Position, Details.Score);
			}
		}

		private class MatchedIris : MatchedPair<NIris, NEMatchingDetails>
		{
			public MatchedIris(NIris i1, NIris i2, NEMatchingDetails dt)
				: base(i1, i2, dt)
			{
			}

			public override string ToString()
			{
				return string.Format("Probe iris({0}) matched with gallery iris({1}). Score = {2}", First.Position, Second.Position, Details.Score);
			}
		}

		private class MatchedFace : MatchedPair<NFace, NLMatchingDetails>
		{
			public MatchedFace(NFace f1, NFace f2, NLMatchingDetails dt)
				: base(f1, f2, dt)
			{
			}

			public override string ToString()
			{
				return string.Format("Matched faces. Score = {0}", Details.Score);
			}
		}

		private class MatchedVoice : MatchedPair<NVoice, NSMatchingDetails>
		{
			public MatchedVoice(NVoice v1, NVoice v2, NSMatchingDetails dt)
				: base(v1, v2, dt)
			{
			}

			public override string ToString()
			{
				return string.Format("Matched probe voice(PhraseId={0}) with gallery voice(PhraseId={1}). Score={2}", First.PhraseId, Second.PhraseId, Details.Score);
			}
		}

		private class MatchedPair<T1, T2>
		{
			public MatchedPair(T1 first, T1 second, T2 details)
			{
				this.First = first;
				this.Second = second;
				this.Details = details;
			}

			public T1 First { get; set; }
			public T1 Second { get; set; }
			public T2 Details { get; set; }
		}

		#endregion

		#region Public constructor

		public MatchingResultTab()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _probeSubject;
		private NSubject _galerySubject;
		private NMatchingResult _matchingResult;
		private MediaPlayerControl _mediaPlayer1;
		private MediaPlayerControl _mediaPlayer2;

		#endregion

		#region Public methods

		public override void SetParams(params object[] parameters)
		{
			if (parameters == null || parameters.Length != 2) throw new ArgumentException("parameters");

			_probeSubject = (NSubject)parameters[0];
			_galerySubject = (NSubject)parameters[1];
			_matchingResult = _probeSubject.MatchingResults.First(x => x.Id == _galerySubject.Id);
			_biometricClient = TabController.Client;

			TabName = string.Format("Matching result: {0}", _galerySubject.Id);
			if (TabName.Length > 30)
				TabName = TabName.Substring(0, 30) + "...";

			base.SetParams(parameters);
		}

		public override void OnTabAdded()
		{
			lblInfo.Text = string.Format("Subject: '{0}'{1}Score: {2}", _galerySubject.Id, Environment.NewLine, _matchingResult.Score);
			_biometricClient.BeginGet(_galerySubject, OnGetCompleted, null);

			base.OnTabAdded();
		}

		public override void OnTabLeave()
		{
			StopMediaPlayers();

			base.OnTabLeave();
		}

		#endregion

		#region Private methods

		private int RecordIndexToFaceIndex(int index, int[] recordCouns)
		{
			if (recordCouns != null)
			{
				int sum = 0;
				int faceIndex = 0;
				foreach (var item in recordCouns)
				{
					if (index >= sum && index < sum + item) return faceIndex;
					sum += item;
					faceIndex++;
				}
			}
			return 0;
		}

		private void OnGetCompleted(IAsyncResult result)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnGetCompleted), result);
			}
			else
			{
				try
				{
					if (_biometricClient.EndGet(result) == NBiometricStatus.Ok)
					{
						SampleDbSchema schema = SettingsManager.CurrentSchema;
						bool hasSchema = !schema.IsEmpty;
						string thumbnailName = schema.ThumbnailDataName;
						bool hasThumbnail = hasSchema && !string.IsNullOrEmpty(thumbnailName) && _galerySubject.Properties.ContainsKey(thumbnailName);
						if (hasThumbnail)
						{
							using (NBuffer buffer = _galerySubject.GetProperty<NBuffer>(thumbnailName))
							using (NImage image = NImage.FromMemory(buffer))
							{
								pbThumbnail.Image = image.ToBitmap();
							}
						}
						else
						{
							pbThumbnail.Visible = false;
						}

						int[] galeryRecordCounts = null;
						if (hasSchema)
						{
							NPropertyBag bag = new NPropertyBag();
							_galerySubject.CaptureProperties(bag);

							if (!string.IsNullOrEmpty(schema.EnrollDataName) && bag.ContainsKey(schema.EnrollDataName))
							{
								NBuffer templateBuffer = _galerySubject.GetTemplateBuffer();
								NBuffer enrollData = (NBuffer)bag[schema.EnrollDataName];
								_galerySubject = EnrollDataSerializer.Deserialize(templateBuffer, enrollData, out galeryRecordCounts);
							}
							if (!string.IsNullOrEmpty(schema.GenderDataName) && bag.ContainsKey(schema.GenderDataName))
							{
								string genderString = (string)bag[schema.GenderDataName];
								bag[schema.GenderDataName] = Enum.Parse(typeof(NGender), genderString);
							}

							propertyGrid.SelectedObject = new SchemaPropertyGridAdapter(schema, bag) { IsReadOnly = true };
						}
						else
						{
							propertyGrid.Visible = false;
						}

						bool hasDetails = _matchingResult.MatchingDetails != null;
						if (hasDetails)
						{
							lblStatus.Visible = false;
							int threshold = _biometricClient.MatchingThreshold;

							var details = _matchingResult.MatchingDetails;

							var templateFingers = SubjectUtils.GetTemplateCompositeFingers(_probeSubject).ToArray();
							var matchedFingers = details.Fingers.Select((x, i) =>
								{
									if (x.MatchedIndex == -1 || x.Score < threshold) return null;
									else
									{
										return new MatchedFinger(templateFingers[i], _galerySubject.Fingers[x.MatchedIndex], x);
									}
								}).Where(x => x != null);
							cbMatched.Items.AddRange(matchedFingers.ToArray());

							var faces = SubjectUtils.GetTemplateCompositeFaces(_probeSubject).ToArray();
							var recordCounts = faces.Select(x => x.Objects.First().Template.Records.Count).ToArray();
							var matchedFaces = details.Faces.Select((x, i) =>
								{
									if (x.MatchedIndex == -1 || x.Score < threshold) return null;
									else
									{
										return new MatchedFace(faces[RecordIndexToFaceIndex(i, recordCounts)], _galerySubject.Faces[RecordIndexToFaceIndex(x.MatchedIndex, galeryRecordCounts)], x);
									}
								}).Where(x => x != null);
							cbMatched.Items.AddRange(matchedFaces.ToArray());

							var templateIrises = SubjectUtils.GetTemplateCompositeIrises(_probeSubject).ToArray();
							var matchedIrises = details.Irises.Select((x, i) =>
								{
									if (x.MatchedIndex == -1 || x.Score < threshold) return null;
									else
									{
										return new MatchedIris(templateIrises[i], _galerySubject.Irises[x.MatchedIndex], x);
									}
								}).Where(x => x != null);
							cbMatched.Items.AddRange(matchedIrises.ToArray());

							var templatePalms = SubjectUtils.GetTemplateCompositePalms(_probeSubject).ToArray();
							var matchedPalms = details.Palms.Select((x, i) =>
								{
									if (x.MatchedIndex == -1 || x.Score < threshold) return null;
									else
									{
										return new MatchedFinger(templatePalms[i], _galerySubject.Palms[x.MatchedIndex], x);
									}
								}).Where(x => x != null);
							cbMatched.Items.AddRange(matchedPalms.ToArray());

							var templateVoices = SubjectUtils.GetTemplateCompositeVoices(_probeSubject).ToArray();
							var matchedVoices = details.Voices.Select((x, i) =>
								{
									if (x.MatchedIndex == -1 || x.Score < threshold) return null;
									else
									{
										return new MatchedVoice(templateVoices[i], _galerySubject.Voices[x.MatchedIndex], x);
									}
								}).Where(x => x != null);
							cbMatched.Items.AddRange(matchedVoices.ToArray());
							cbMatched.SelectedIndex = 0;
						}
						else
						{
							lblStatus.Text = "Enable 'Return matching details' in settings to see more details in this tab";
							lblProbeInfo.Visible = false;
							lblGaleryInfo.Visible = false;
							tlpSelection.Visible = false;
							panelGaleryView.Visible = false;
							panelProbeView.Visible = false;
							lblProbeSubject.Visible = false;
							lblGalerySubject.Visible = false;
						}
					}
				}
				catch (Exception ex)
				{
					lblStatus.Text = string.Format("Failed to get subject: {0}", ex.Message);
					lblStatus.BackColor = Color.Red;
					lblStatus.Visible = true;
				}
			}
		}

		private NFingerView ShowFinger(NFrictionRidge target, Panel panelView, Label lblInfo)
		{
			NFingerView view = new NFingerView()
			{
				Dock = DockStyle.Fill,
				AutoScroll = true,
				Finger = target,
				ShowTree = true
			};

			panelView.Controls.Clear();
			panelView.Controls.Add(view);

			NFAttributes attributes = target.Objects.ToArray().FirstOrDefault();
			if (attributes != null)
				lblInfo.Text = string.Format("Position={0}, Quality={1}", target.Position, attributes.Quality);
			else
				lblInfo.Text = "Position=" + target.Position;

			return view;
		}

		private NIrisView ShowIris(NIris target, Panel panelView, Label lblInfo)
		{
			NIrisView view = new NIrisView()
			{
				Dock = DockStyle.Fill,
				AutoScroll = true,
				Iris = target
			};

			panelView.Controls.Clear();
			panelView.Controls.Add(view);

			NEAttributes attributes = target.Objects.ToArray().FirstOrDefault();
			if (attributes != null)
				lblInfo.Text = string.Format("Position={0}, Quality={1}", target.Position, attributes.Quality);
			else
				lblInfo.Text = "Position=" + target.Position;

			return view;
		}

		private NFaceView ShowFace(NFace target, Panel panelView, Label lblInfo)
		{
			NFaceView view = new NFaceView()
			{
				Dock = DockStyle.Fill,
				AutoScroll = true,
				Face = target
			};

			panelView.Controls.Clear();
			panelView.Controls.Add(view);

			NLAttributes attributes = target.Objects.ToArray().FirstOrDefault();
			if (attributes != null)
				lblInfo.Text = string.Format("Quality={0}", attributes.Quality);
			else
				lblInfo.Text = string.Empty;

			return view;
		}

		private MediaPlayerControl ShowVoice(NVoice target, Panel panelView, Label lblInfo)
		{
			TableLayoutPanel tlp = new TableLayoutPanel() { Dock = DockStyle.Fill };
			tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
			tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

			panelView.Controls.Clear();
			panelView.Controls.Add(tlp);

			NVoiceView view = new NVoiceView()
			{
				Dock = DockStyle.Fill,
				Voice = target,
				Size = new Size(100, 55)
			};

			MediaPlayerControl player = new MediaPlayerControl() { Dock = DockStyle.Fill };
			if (target.SoundBuffer != null)
				player.SoundBuffer = target.SoundBuffer.Save();

			tlp.Controls.Add(view, 0, 0);
			tlp.Controls.Add(player, 0, 1);

			NSAttributes attributes = target.Objects.ToArray().FirstOrDefault();
			if (attributes != null)
				lblInfo.Text = string.Format("Quality={0}", attributes.Quality);
			else
				lblInfo.Text = string.Empty;

			return player;
		}

		private void CbMatchedSelectedIndexChanged(object sender, EventArgs e)
		{
			var selected = cbMatched.SelectedItem;
			panelGaleryView.Controls.Clear();
			panelProbeView.Controls.Clear();
			lblGaleryInfo.Text = string.Empty;
			lblProbeInfo.Text = string.Empty;
			if (selected != null)
			{
				StopMediaPlayers();
				if (selected is MatchedFinger)
				{
					MatchedFinger mf = (MatchedFinger)selected;
					NFingerView view1 = ShowFinger(mf.First, panelProbeView, lblProbeInfo);
					NFingerView view2 = ShowFinger(mf.Second, panelGaleryView, lblGaleryInfo);

					NIndexPair[] matchedPairs = mf.Details.GetMatedMinutiae();
					view1.MatedMinutiaIndex = 0;
					view1.MatedMinutiae = matchedPairs;
					view2.MatedMinutiaIndex = 1;
					view2.MatedMinutiae = matchedPairs;
					view1.PrepareTree();
					view2.Tree = view1.Tree;

					view1.SelectedTreeMinutiaIndexChanged += (s, a) =>
						{
							var args = a as TreeMinutiaEventArgs;
							if (args != null)
							{
								view2.SelectedMinutiaIndex = args.Index;
							}
						};
					view2.SelectedTreeMinutiaIndexChanged += (s, a) =>
					{
						var args = a as TreeMinutiaEventArgs;
						if (args != null)
						{
							view1.SelectedMinutiaIndex = args.Index;
						}
					};
				}
				else if (selected is MatchedFace)
				{
					MatchedFace mf = (MatchedFace)selected;
					ShowFace(mf.First, panelProbeView, lblProbeInfo);
					ShowFace(mf.Second, panelGaleryView, lblGaleryInfo);
				}
				else if (selected is MatchedIris)
				{
					MatchedIris mi = (MatchedIris)selected;
					ShowIris(mi.First, panelProbeView, lblProbeInfo);
					ShowIris(mi.Second, panelGaleryView, lblGaleryInfo);
				}
				else if (selected is MatchedVoice)
				{
					MatchedVoice mv = (MatchedVoice)selected;
					_mediaPlayer1 = ShowVoice(mv.First, panelProbeView, lblProbeInfo);
					_mediaPlayer2 = ShowVoice(mv.Second, panelGaleryView, lblGaleryInfo);
				}
			}
		}

		private void StopMediaPlayers()
		{
			if (_mediaPlayer1 != null) _mediaPlayer1.Stop();
			if (_mediaPlayer2 != null) _mediaPlayer2.Stop();
		}

		#endregion
	}
}
