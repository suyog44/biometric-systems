using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics.Common;
using Neurotec.Biometrics.Standards;
using Neurotec.Gui;
using Neurotec.Images;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region private static fields

		private static uint ansiBdbFormat = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerImage);
		private static uint isoBdbFormat = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFingerImage);

		#endregion

		#region Private static methods

		private static bool GetOptions(BdifOptionsForm.BdifOptionsFormMode mode, ref BdifStandard standard, ref uint flags, ref NVersion version)
		{
			using (var form = new FIRecordOptionsForm())
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

		private static FIRecord ConvertToStandard(FIRecord record, BdifStandard newStandard, uint flags, NVersion version)
		{
			if (record.Standard == newStandard && record.Flags == flags && record.Version == version)
				return record;

			return new FIRecord(record, flags, newStandard, version);
		}

		private static void AddFingerView(TreeNode fingerNode, FirFingerView fingerView)
		{
			int index = fingerNode.Nodes.Count + 1;
			var fingerViewNode = new TreeNode("FingerView " + index);
			fingerViewNode.Tag = fingerView;
			fingerNode.Nodes.Add(fingerViewNode);
		}

		private static void AddFingers(TreeNode templateNode, FIRecord record)
		{
			foreach (FirFingerView finger in record.FingerViews)
			{
				AddFingerView(templateNode, finger);
			}
		}

		private static void GetBiometricDataBlock(TreeNode node, CbeffRecord record)
		{
			if (record.BdbBuffer != null && (record.BdbFormat == ansiBdbFormat || record.BdbFormat == isoBdbFormat))
			{
				BdifStandard standard = record.BdbFormat == ansiBdbFormat ? BdifStandard.Ansi : BdifStandard.Iso;
				FIRecord fiRecord = new FIRecord(record.BdbBuffer, standard);
				TreeNode child = new TreeNode("FIRecord");
				child.Tag = fiRecord;
				AddFingers(child, fiRecord);
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

			imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			imageSaveFileDialog.Filter = NImages.GetSaveFileFilterString();
			aboutToolStripMenuItem.Text = '&' + AboutBox.Name;
			OnSelectedItemChanged();
		}

		#endregion

		#region Private methods

		private void OnSelectedItemChanged()
		{
			TreeNode selectedNode = treeView.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			FirFingerView fingerView = treeView.SelectedNode.Tag as FirFingerView;
			if (fingerView != null)
			{
				fiView.Record = fingerView;
				propertyGrid.SelectedObject = fingerView;
			}
			else
			{
				fiView.Record = null;
				propertyGrid.SelectedObject = selectedNode.Tag;
			}

			if (selectedNode.Tag is CbeffRecord)
			{
				addFingerViewFromImageToolStripMenuItem.Enabled = false;
				removeFingerToolStripMenuItem.Enabled = false;
				saveIngerAsImageToolStripMenuItem.Enabled = false;
				convertToolStripMenuItem.Enabled = false;
				saveFingerAsToolStripMenuItem.Enabled = false;
			}
			else
			{
				addFingerViewFromImageToolStripMenuItem.Enabled = true;
				removeFingerToolStripMenuItem.Enabled = true;
				saveIngerAsImageToolStripMenuItem.Enabled = true;
				convertToolStripMenuItem.Enabled = true;
				saveFingerAsToolStripMenuItem.Enabled = true;
			}
		}

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

		private void SetTemplate(FIRecord record)
		{
			bool newRecord = true;
			TreeNode recordNode = GetFirstNodeWithTag<FIRecord>();
			if (recordNode == null)
				newRecord = true;
			else
				newRecord = ((FIRecord)recordNode.Tag) != record;

			if (newRecord)
			{
				treeView.BeginUpdate();
				treeView.Nodes.Clear();

				TreeNode templateNode = new TreeNode((_fileName == null ? "Untitled" : Path.GetFileName(_fileName)));
				templateNode.Tag = record;

				AddFingers(templateNode, record);

				treeView.Nodes.Add(templateNode);
				treeView.SelectedNode = templateNode;

				treeView.EndUpdate();
			}
		}

		private void SetCbeff(CbeffRecord record)
		{
			bool newRecord = true;
			TreeNode recordNode = GetFirstNodeWithTag<CbeffRecord>();
			if (recordNode == null)
				newRecord = true;
			else
				newRecord = ((CbeffRecord)recordNode.Tag) != record;

			if (newRecord)
			{
				treeView.BeginUpdate();
				treeView.Nodes.Clear();

				TreeNode root = new TreeNode((_fileName == null ? "Untitled" : Path.GetFileName(_fileName)));
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
			byte[] templ = LoadTemplate(fiRecordOpenFileDialog, out fileName);
			if (templ == null)
				return;

			var standard = BdifStandard.Iso;
			uint flags = 0;
			var version = FIRecord.VersionIsoCurrent;
			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.Open, ref standard, ref flags, ref version))
				return;

			string oldFilename = _fileName;
			_fileName = fileName;
			try
			{
				var template = new FIRecord(new NBuffer(templ), flags, standard);
				SetTemplate(template);
			}
			catch (Exception ex)
			{
				ShowError("Failed to load template! Reason:\r\n" + ex);
				_fileName = oldFilename;
			}
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

			string oldFilename = _fileName;
			_fileName = fileName;
			try
			{
				CbeffRecord template = new CbeffRecord(new NBuffer(templ), format);
				SetCbeff(template);
			}
			catch (Exception ex)
			{
				ShowError("Failed to load template! Reason:\r\n" + ex.ToString());
				_fileName = oldFilename;
			}
		}

		private void SaveTemplate(FIRecord record)
		{
			SaveTemplate(fiRecordSaveFileDialog, _fileName, record.Save().ToArray());
			if (treeView.Nodes[0].Tag is FIRecord)
				treeView.Nodes[0].Text = Path.GetFileName(fiRecordSaveFileDialog.FileName);
		}

		private bool IsRecordFirstVersion(FIRecord record)
		{
			return record.Standard == BdifStandard.Ansi && record.Version == FIRecord.VersionAnsi10
				|| record.Standard == BdifStandard.Iso && record.Version == FIRecord.VersionIso10;
		}

		#endregion

		#region Private form methods

		private void convertToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode rootNode = GetFirstNodeWithTag<FIRecord>();
			if (rootNode == null)
				return;

			FIRecord fiRecord = rootNode.Tag as FIRecord;
			BdifStandard standard = (fiRecord.Standard == BdifStandard.Ansi) ? BdifStandard.Iso : BdifStandard.Ansi;
			NVersion version = standard == BdifStandard.Iso ? FIRecord.VersionIsoCurrent : FIRecord.VersionAnsiCurrent;
			uint flags = 0;

			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.Convert, ref standard, ref flags, ref version))
				return;

			fiRecord = ConvertToStandard(fiRecord, standard, flags, version);
			rootNode.Tag = fiRecord;

			for (int i = 0; i < fiRecord.FingerViews.Count; i++)
			{
				rootNode.Nodes[i].Tag = fiRecord.FingerViews[i];
			}

			OnSelectedItemChanged();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox.Show();
		}

		private void saveFingerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selected = treeView.SelectedNode;
			if ((selected != null) && (selected.Parent != null) && (selected.Parent.Parent != null))
			{
				imageSaveFileDialog.FileName = null;
				if (imageSaveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						var finger = selected.Tag as FirFingerView;
						if (finger != null)
							using (NImage image = finger.ToNImage())
							{
								image.Save(imageSaveFileDialog.FileName);
							}
					}
					catch (Exception ex)
					{
						ShowError("Failed to save image to file!\r\nReason: " + ex);
					}
				}
			}
			else
			{
				ShowWarning("Please select finger image");
			}
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selected = treeView.SelectedNode;
			if (selected == null)
				return;

			TreeNode recordNode = GetFirstNodeWithTag<FIRecord>();
			if (recordNode == null)
			{
				ShowError("Add FIRecord first");
				return;
			}

			FirFingerView fview = selected.Tag as FirFingerView;
			if (fview != null)
			{
				FIRecord record = recordNode.Tag as FIRecord;
				record.FingerViews.Remove(fview);
				recordNode.Nodes.Remove(selected);
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var standard = BdifStandard.Iso;
			uint flags = 0;
			var version = FIRecord.VersionIsoCurrent;
			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.New, ref standard, ref flags, ref version))
				return;

			try
			{
				FIRecord template = new FIRecord(standard, version, flags);
				_fileName = null;
				SetTemplate(template);
			}
			catch (Exception ex)
			{
				ShowError("Failed to create template! Reason:\r\n" + ex);
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
			TreeNode recordNode = GetFirstNodeWithTag<FIRecord>();
			if (recordNode == null)
			{
				ShowError("Add FIRecord first");
				return;
			}

			try
			{
				SaveTemplate((FIRecord)recordNode.Tag);
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

		private void addFingerViewFromImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode recordNode = GetFirstNodeWithTag<FIRecord>();

			if (recordNode != null)
			{
				try
				{
					imageOpenFileDialog.FileName = null;
					NImage imgFromFile;
					if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
						imgFromFile = NImage.FromFile(imageOpenFileDialog.FileName);
					else
						return;

					NImage image = NImage.FromImage(NPixelFormat.Grayscale8U, 0, imgFromFile);
					if (image.ResolutionIsAspectRatio
						|| image.HorzResolution < 250
						|| image.VertResolution < 250)
					{
						image.HorzResolution = 500;
						image.VertResolution = 500;
						image.ResolutionIsAspectRatio = false;
					}

					FIRecord record = recordNode.Tag as FIRecord;

					using (AddFingerForm form = new AddFingerForm())
					{
						if (form.ShowDialog() != DialogResult.OK)
							return;

						FirFingerView fingerView = new FirFingerView(record.Standard, record.Version);

						fingerView.Position = form.FingerPosition;

						if (!IsRecordFirstVersion(record))
						{
							fingerView.PixelDepth = 8;
							fingerView.HorzImageResolution = (ushort)image.HorzResolution;
							fingerView.HorzScanResolution = (ushort)image.HorzResolution;
							fingerView.VertImageResolution = (ushort)image.VertResolution;
							fingerView.VertScanResolution = (ushort)image.VertResolution;
						}
						else
						{
							if (record.FingerViews.Count == 0)
							{
								record.PixelDepth = 8;
								record.HorzImageResolution = (ushort)image.HorzResolution;
								record.HorzScanResolution = (ushort)image.HorzResolution;
								record.VertImageResolution = (ushort)image.VertResolution;
								record.VertScanResolution = (ushort)image.VertResolution;
							}
						}

						record.FingerViews.Add(fingerView);
						fingerView.SetImage(image);
						AddFingerView(recordNode, fingerView);
					}
					treeView.ExpandAll();
				}
				catch (Exception ex)
				{
					ShowError(ex.ToString());
				}
			}
			else
				ShowWarning("Finger must be selected before adding fingerView");
		}

		#endregion
	}
}
