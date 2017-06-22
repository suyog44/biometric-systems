using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Common;
using Neurotec.Biometrics.Standards;
using Neurotec.Biometrics.Standards.Gui;
using Neurotec.Gui;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Private static fields

		private static uint ansiBdbFormat = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerMinutiaeX);
		private static uint isoBdbFormat = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.FederalOfficeForInformationSecurityTRBiometricsXmlFinger10);

		#endregion

		#region Private static methods

		private static bool GetOptions(BdifOptionsForm.BdifOptionsFormMode mode, ref BdifStandard standard, ref uint flags, ref NVersion version)
		{
			using (FMRecordOptionsForm form = new FMRecordOptionsForm())
			{
				form.Standard = standard;
				form.Flags = flags;
				form.Mode = mode;
				form.Version = version;
				if (form.ShowDialog() == DialogResult.OK)
				{
					standard = form.Standard;
					flags = form.Flags;
					version = form.Version;
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		private static bool GetOptions(ref uint format)
		{
			using (CbeffRecordOptionsForm form = new CbeffRecordOptionsForm())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					format = form.PatronFormat;
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		private static byte[] LoadTemplate(OpenFileDialog openFileDialog, out string fileName)
		{
			openFileDialog.FileName = null;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				fileName = openFileDialog.FileName;
				return File.ReadAllBytes(openFileDialog.FileName);
			}
			fileName = null;
			return null;
		}

		private static void SaveTemplate(SaveFileDialog saveFileDialog, string fileName, byte[] template)
		{
			saveFileDialog.FileName = fileName;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllBytes(saveFileDialog.FileName, template);
			}
		}

		private static FMRecord ConvertToStandard(FMRecord record, BdifStandard newStandard, uint flags, NVersion version)
		{
			if (record.Standard == newStandard && record.Flags == flags && record.Version == version)
				return record;

			return new FMRecord(record, flags, newStandard, version);
		}

		private static void AddFingerView(TreeNode node, FmrFingerView fview)
		{
			int index = node.Nodes.Count + 1;
			TreeNode fingerViewNode = new TreeNode("Finger view " + index);
			fingerViewNode.Tag = fview;
			node.Nodes.Add(fingerViewNode);
		}

		private static void AddFingers(TreeNode node, FMRecord record)
		{
			foreach (FmrFingerView finger in record.FingerViews)
			{
				AddFingerView(node, finger);
			}
		}

		private static void GetBiometricDataBlock(TreeNode node, CbeffRecord record)
		{
			if (record.BdbBuffer != null && (record.BdbFormat == ansiBdbFormat || record.BdbFormat == isoBdbFormat))
			{
				BdifStandard standard = record.BdbFormat == ansiBdbFormat ? BdifStandard.Ansi : BdifStandard.Iso;
				FMRecord fmRecord = new FMRecord(record.BdbBuffer, standard);
				TreeNode child = new TreeNode("FMRecord");
				child.Tag = fmRecord;
				AddFingers(child, fmRecord);
				node.Nodes.Add(child);
			}
		}

		private static void AddFingers(TreeNode node, CbeffRecord record)
		{
			GetBiometricDataBlock(node, record);

			foreach (CbeffRecord child in record.Records)
			{
				AddFingers(node, child);
			}
		}

		#endregion

		#region Private fields

		private string _fileName;

		#endregion

		#region Public constructor

		public MainForm()
		{
			InitializeComponent();

			fmView.SelectedCoreIndexChanged += new EventHandler(OnDetailSelected);
			fmView.SelectedDeltaIndexChanged += new EventHandler(OnDetailSelected);
			fmView.SelectedMinutiaIndexChanged += new EventHandler(OnDetailSelected);
			fmView.MouseUp += new MouseEventHandler(OnMouseUp);

			aboutToolStripMenuItem.Text = '&' + AboutBox.Name;
		}

		#endregion

		#region Private methods

		private void ShowError(string message)
		{
			MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void ShowWarning(string message)
		{
			MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private TreeNode GetFirstNodeWithTag<T>()
		{
			TreeNode node = treeView.SelectedNode;
			if (node == null)
				return null;

			while (node.Parent != null && !(node.Tag is T))
				node = node.Parent;

			return node.Tag is T ? node : null;
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			TreeNode recordNode = GetFirstNodeWithTag<FMRecord>();
			if (recordNode == null)
				return;

			FmrFingerView fingerView = treeView.SelectedNode.Tag as FmrFingerView;
			int index = fmView.SelectedMinutiaIndex;
			if (index != -1)
				propertyGrid.SelectedObject = new SelectedFmrMinutia(fingerView, index);
			else
			{
				index = fmView.SelectedDeltaIndex;
				if (index != -1)
					propertyGrid.SelectedObject = new SelectedFmrDelta(fingerView, index);
				else
				{
					index = fmView.SelectedCoreIndex;
					if (index != -1)
					{
						propertyGrid.SelectedObject = new SelectedFmrCore(fingerView, index);
					}
				}
			}
		}

		private void OnDetailSelected(object sender, EventArgs e)
		{
			if (fmView.SelectedCoreIndex == fmView.SelectedDeltaIndex && fmView.SelectedDeltaIndex == fmView.SelectedMinutiaIndex)
			{
				btnDeleteFeature.Enabled = false;
				deleteSelectedToolStripMenuItem.Enabled = false;
			}
			else
			{
				btnDeleteFeature.Enabled = true;
				deleteSelectedToolStripMenuItem.Enabled = true;
			}
			FmrFingerView fingerView = treeView.SelectedNode.Tag as FmrFingerView;
			int index = fmView.SelectedDeltaIndex;
			if (index != -1)
			{
				propertyGrid.SelectedObject = new SelectedFmrDelta(fingerView, index);
				return;
			}

			index = fmView.SelectedCoreIndex;
			if (index != -1)
			{
				propertyGrid.SelectedObject = new SelectedFmrCore(fingerView, index);
				return;
			}

			index = fmView.SelectedMinutiaIndex;
			if (index != -1)
			{
				propertyGrid.SelectedObject = new SelectedFmrMinutia(fingerView, index);
				return;
			}

			propertyGrid.SelectedObject = fingerView;
		}

		private void OnFeatureAdded(object sender, EventArgs e)
		{
			TreeNode recordNode = GetFirstNodeWithTag<FMRecord>();
			if (recordNode == null)
				return;

			FMRecord record = recordNode.Tag as FMRecord;

			TreeNode selected = treeView.SelectedNode;
			FmrFingerView fView = selected.Tag as FmrFingerView;
			if (fView == null) return;
			FMView.AddFeaturesTool.FeatureAddCompletedEventArgs args = (FMView.AddFeaturesTool.FeatureAddCompletedEventArgs)e;
			if (args.Start.X < 0 || args.Start.Y < 0) return;

			ushort x = (ushort)args.Start.X;
			ushort y = (ushort)args.Start.Y;
			AddFeatureForm form = new AddFeatureForm();
			form.StartPosition = FormStartPosition.CenterParent;

			var isVersion2 = IsRecordSecondVersion(record);
			var sizeX = isVersion2 ? record.SizeX : fView.SizeX;
			var sizeY = isVersion2 ? record.SizeY : fView.SizeY;
			if (x >= sizeX || y >= sizeY || x < 0 || y < 0
				|| (args.Start.X == args.End.X && args.Start.Y == args.End.Y))
			{
				return;
			}

			if (form.ShowDialog() == DialogResult.OK)
			{
				int w = args.End.X - args.Start.X;
				int h = args.End.Y - args.Start.Y;
				double angle = -(Math.Atan((float)h / w) + (w < 0 ? Math.PI : 0.0));
				if (Double.IsNaN(angle)) return;

				switch (form.cbFeature.SelectedIndex)
				{
					case 0:
						fView.Minutiae.Add(new FmrMinutia(x, y, form.MinutiaType, angle, record.Standard));
						break;
					case 1:
						fView.Cores.Add(new FmrCore(x, y, angle, record.Standard));
						break;
					default:
						fView.Deltas.Add(new FmrDelta(x, y));
						break;
				}
			}
		}

		private void OnSelectedItemChanged()
		{
			TreeNode selectedNode = treeView.SelectedNode;
			if (selectedNode == null)
				return;

			FmrFingerView fingerView = selectedNode.Tag as FmrFingerView;
			if (fingerView != null)
			{
				fmView.Template = fingerView;
				propertyGrid.SelectedObject = fingerView;
				toolStrip.Enabled = true;
			}
			else
			{
				toolStrip.Enabled = false;
				fmView.Template = null;
				propertyGrid.SelectedObject = selectedNode.Tag;
			}

			if (selectedNode.Tag is CbeffRecord)
			{
				addFingerViewToolStripMenuItem.Enabled = false;
				removeFingerToolStripMenuItem.Enabled = false;
				convertToolStripMenuItem.Enabled = false;
				saveAsToolStripMenuItem.Enabled = false;
			}
			else
			{
				addFingerViewToolStripMenuItem.Enabled = true;
				removeFingerToolStripMenuItem.Enabled = true;
				convertToolStripMenuItem.Enabled = true;
				saveAsToolStripMenuItem.Enabled = true;
			}
		}

		private void SetTemplate(FMRecord record, string fileName)
		{
			bool newRecord = true;
			TreeNode recordNode = GetFirstNodeWithTag<FMRecord>();
			if (recordNode == null)
				newRecord = true;
			else
				newRecord = ((FMRecord)recordNode.Tag) != record;

			if (newRecord)
			{
				_fileName = fileName;
				treeView.BeginUpdate();
				treeView.Nodes.Clear();

				TreeNode templateNode = new TreeNode((fileName == null ? "Untitled" : Path.GetFileName(fileName)));
				templateNode.Tag = record;

				AddFingers(templateNode, record);

				treeView.Nodes.Add(templateNode);
				treeView.SelectedNode = templateNode;

				treeView.EndUpdate();
			}
		}

		private void SetCbeff(CbeffRecord record, string fileName)
		{
			bool newRecord = true;
			TreeNode recordNode = GetFirstNodeWithTag<CbeffRecord>();
			if (recordNode == null)
				newRecord = true;
			else
				newRecord = ((CbeffRecord)recordNode.Tag) != record;

			if (newRecord)
			{
				_fileName = fileName;
				treeView.BeginUpdate();
				treeView.Nodes.Clear();

				TreeNode root = new TreeNode((fileName == null ? "Untitled" : Path.GetFileName(fileName)));
				root.Tag = record;

				AddFingers(root, record);

				treeView.Nodes.Add(root);
				treeView.SelectedNode = root;

				treeView.EndUpdate();
			}
		}

		private void OpenTemplate()
		{
			string fileName;
			byte[] templ = LoadTemplate(fmRecordOpenFileDialog, out fileName);
			if (templ == null)
				return;

			BdifStandard standard = BdifStandard.Iso;
			uint flags = 0;
			var version = FMRecord.VersionIsoCurrent;
			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.Open, ref standard, ref flags, ref version))
				return;

			FMRecord template;
			try
			{
				template = new FMRecord(new NBuffer(templ), flags, standard);
			}
			catch (Exception ex)
			{
				ShowError("Failed to load template! Reason:\r\n" + ex.ToString());
				return;
			}
			SetTemplate(template, fileName);
		}

		private void OpenCbeff()
		{
			string fileName;
			byte[] templ = LoadTemplate(cbeffRecordOpenFileDialog, out fileName);
			if (templ == null)
				return;

			uint format = 0;
			if (!GetOptions(ref format))
				return;

			CbeffRecord template;
			try
			{
				template = new CbeffRecord(new NBuffer(templ), format);
			}
			catch (Exception ex)
			{
				ShowError("Failed to load CbeffRecord! Reason:\r\n" + ex.ToString());
				return;
			}
			SetCbeff(template, fileName);
		}

		private void SaveTemplate(FMRecord record)
		{
			SaveTemplate(fmRecordSaveFileDialog, _fileName, record.Save().ToArray());
			if (treeView.Nodes[0].Tag is FMRecord)
				treeView.Nodes[0].Text = Path.GetFileName(fmRecordSaveFileDialog.FileName);
		}

		private bool IsRecordSecondVersion(FMRecord record)
		{
			return record.Standard == BdifStandard.Ansi && record.Version == FMRecord.VersionAnsi20
				|| record.Standard == BdifStandard.Iso && record.Version == FMRecord.VersionIso20;
		}

		#endregion

		#region Private form methods

		private void InvalidateFMView()
		{
			fmView.Invalidate();
			propertyGrid.Refresh();
		}

		private void btnPointerTool_Click(object sender, EventArgs e)
		{
			pointerToolToolStripMenuItem_Click(sender, e);
		}

		private void btnAddFeatureTool_Click(object sender, EventArgs e)
		{
			addFeatureToolToolStripMenuItem_Click(sender, e);
		}

		private void convertToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode rootNode = GetFirstNodeWithTag<FMRecord>();
			if (rootNode == null)
				return;

			FMRecord fmRecord = rootNode.Tag as FMRecord;
			BdifStandard standard = (fmRecord.Standard == BdifStandard.Ansi) ? BdifStandard.Iso : BdifStandard.Ansi;
			var version = standard == BdifStandard.Iso ? FMRecord.VersionIsoCurrent : FMRecord.VersionAnsiCurrent;
			uint flags = 0;

			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.Convert, ref standard, ref flags, ref version))
				return;

			fmRecord = ConvertToStandard(fmRecord, standard, flags, version);
			rootNode.Tag = fmRecord;

			for (int i = 0; i < fmRecord.FingerViews.Count; i++)
			{
				rootNode.Nodes[i].Tag = fmRecord.FingerViews[i];
			}

			OnSelectedItemChanged();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox.Show();
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selected = treeView.SelectedNode;
			if (selected == null)
				return;

			TreeNode recordNode = GetFirstNodeWithTag<FMRecord>();
			if (recordNode == null)
			{
				ShowError("Add FMRecord first");
				return;
			}

			FmrFingerView fview = selected.Tag as FmrFingerView;
			if (fview != null)
			{
				FMRecord record = recordNode.Tag as FMRecord;
				record.FingerViews.Remove(fview);
				recordNode.Nodes.Remove(selected);
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenTemplate();
			treeView.ExpandAll();
		}

		private void openToolStripMenuItemCbeff_Click(object sender, EventArgs e)
		{
			OpenCbeff();
			treeView.ExpandAll();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode recordNode = GetFirstNodeWithTag<FMRecord>();
			if (recordNode == null)
			{
				ShowError("Add FMRecord first");
				return;
			}

			try
			{
				SaveTemplate((FMRecord)recordNode.Tag);
			}
			catch (Exception ex)
			{
				ShowError(ex.ToString());
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			OnSelectedItemChanged();
		}

		private void pointerToolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (fmView.ActiveTool != null && fmView.ActiveTool is FMView.PointerTool)
			{
				pointerToolToolStripMenuItem.Checked = false;
				btnPointerTool.Checked = false;
				fmView.ActiveTool = null;
				return;
			}

			fmView.ActiveTool = new FMView.PointerTool();
			pointerToolToolStripMenuItem.Checked = true;
			addFeatureToolToolStripMenuItem.Checked = false;
			btnPointerTool.Checked = true;
			btnAddFeatureTool.Checked = false;
		}

		private void addFeatureToolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (fmView.ActiveTool != null && fmView.ActiveTool is FMView.AddFeaturesTool)
			{
				addFeatureToolToolStripMenuItem.Checked = false;
				fmView.ActiveTool = null;
				btnAddFeatureTool.Checked = false;
				return;
			}
			FMView.AddFeaturesTool tool = new FMView.AddFeaturesTool();
			tool.FeatureAddCompleted += OnFeatureAdded;
			fmView.ActiveTool = tool;
			pointerToolToolStripMenuItem.Checked = false;
			addFeatureToolToolStripMenuItem.Checked = true;
			btnAddFeatureTool.Checked = true;
			btnPointerTool.Checked = false;
		}

		private void newFMRecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var standard = BdifStandard.Iso;
			uint flags = 0;
			var version = FMRecord.VersionIsoCurrent;
			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.New, ref standard, ref flags, ref version))
				return;

			try
			{
				FMRecord template = new FMRecord(standard, version, flags);
				_fileName = null;
				SetTemplate(template, null);
			}
			catch (Exception ex)
			{
				ShowError("Failed to create template! Reason:\r\n" + ex);
			}
		}

		private void addFingerViewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode recordNode = GetFirstNodeWithTag<FMRecord>();

			if (recordNode == null)
			{
				ShowWarning("FMRecord has to be selected before adding finger view.");
				return;
			}

			FMRecord record = recordNode.Tag as FMRecord;

			try
			{
				FmrFingerView fingerView = new FmrFingerView(record.Standard, record.Version);

				if (!IsRecordSecondVersion(record))
				{
					using (var form = new NewFingerViewForm())
					{
						if (form.ShowDialog() == DialogResult.OK)
						{
							fingerView.SizeX = form.SizeX;
							fingerView.SizeY = form.SizeY;
							fingerView.VertImageResolution = form.VertResolution;
							fingerView.HorzImageResolution = form.HorzResolution;
						}
						else
						{
							return;
						}
					}
				}
				else
				{
					if (record.FingerViews.Count == 0)
					{
						using (var form = new NewFingerViewForm())
						{
							if (form.ShowDialog() == DialogResult.OK)
							{
								record.SizeX = form.SizeX;
								record.SizeY = form.SizeY;
								record.ResolutionX = form.HorzResolution;
								record.ResolutionY = form.VertResolution;
							}
							else
							{
								return;
							}
						}
					}
				}

				record.FingerViews.Add(fingerView);
				AddFingerView(recordNode, fingerView);
				treeView.ExpandAll();
			}
			catch (Exception ex)
			{
				ShowError(ex.ToString());
			}
		}

		private void btnDeleteSelected_Click(object sender, EventArgs e)
		{
			FmrFingerView fingerView = treeView.SelectedNode.Tag as FmrFingerView;
			if (fingerView == null) return;

			int index = fmView.SelectedMinutiaIndex;
			if (index != -1)
			{
				fmView.SelectedMinutiaIndex = -1;
				fingerView.Minutiae.RemoveAt(index);
				return;
			}

			index = fmView.SelectedCoreIndex;
			if (index != -1)
			{
				fmView.SelectedCoreIndex = -1;
				fingerView.Cores.RemoveAt(index);
				return;
			}

			index = fmView.SelectedDeltaIndex;
			if (index != -1)
			{
				fmView.SelectedDeltaIndex = -1;
				fingerView.Deltas.RemoveAt(index);
				return;
			}
		}

		private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			btnDeleteSelected_Click(sender, e);
		}

		#endregion
	}
}
