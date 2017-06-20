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
		#region Private static fields

		private static uint ansiBdbFormatPolar = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsIrisPolar);
		private static uint isoBdbFormatPolar = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsIrisImagePolar);
		private static uint ansiBdbFormatRectilinear = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsIrisRectilinear);
		private static uint isoBdbFormatRectilinear = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsIrisImageRectilinear);

		#endregion

		#region Private static methods

		private static bool GetOptions(BdifOptionsForm.BdifOptionsFormMode mode, ref BdifStandard standard, ref uint flags, ref NVersion version)
		{
			using (IIRecordOptionsForm form = new IIRecordOptionsForm())
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

		private static void AddIrisImage(TreeNode node, IirIrisImage image)
		{
			string prefix = "Iris Image {0}";
			int index = node.Nodes.Count + 1;
			TreeNode irisImageNode = new TreeNode(string.Format(prefix, index));
			irisImageNode.Tag = image;
			irisImageNode.Expand();
			node.Nodes.Add(irisImageNode);
		}

		private static void AddIrises(TreeNode node, IIRecord record)
		{
			foreach (IirIrisImage image in record.IrisImages)
			{
				AddIrisImage(node, image);
			}
		}

		private static void GetBiometricDataBlock(TreeNode node, CbeffRecord record)
		{
			if (record.BdbBuffer != null && (record.BdbFormat == ansiBdbFormatPolar || record.BdbFormat == isoBdbFormatPolar ||
											 record.BdbFormat == ansiBdbFormatRectilinear || record.BdbFormat == isoBdbFormatRectilinear))
			{
				BdifStandard standard = record.BdbFormat == ansiBdbFormatPolar || record.BdbFormat == ansiBdbFormatRectilinear ? BdifStandard.Ansi : BdifStandard.Iso;
				IIRecord iiRecord = new IIRecord(record.BdbBuffer, standard);
				TreeNode child = new TreeNode("IIRecord");
				child.Tag = iiRecord;
				AddIrises(child, iiRecord);
				node.Nodes.Add(child);
			}
		}

		private static void AddIrises(TreeNode node, CbeffRecord record)
		{
			GetBiometricDataBlock(node, record);

			foreach (CbeffRecord child in record.Records)
			{
				AddIrises(node, child);
			}
		}

		private static IIRecord ConvertToStandard(IIRecord record, BdifStandard newStandard, uint flags, NVersion version)
		{
			if (record.Standard == newStandard && record.Flags == flags && record.Version == version)
				return record;

			return new IIRecord(record, flags, newStandard, version);
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
			rawImageOpenFileDialog.Filter = string.Format("All Supported Files ({0};{1})|{0};{1}|JPEG Files ({0})|{0}|JPEG 2000 Files ({1})|{1}|All Files (*.*)|*.*", NImageFormat.Jpeg.FileFilter, NImageFormat.Jpeg2K.FileFilter);
			saveImageFileDialog.Filter = NImages.GetSaveFileFilterString();
			aboutToolStripMenuItem.Text = '&' + AboutBox.Name;

			OnSelectedItemChanged();
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

		private void SetTemplate(IIRecord record, string fileName)
		{
			bool newRecord = true;
			TreeNode recordNode = GetFirstNodeWithTag<IIRecord>();
			if (recordNode == null)
				newRecord = true;
			else
				newRecord = ((IIRecord)recordNode.Tag) != record;

			if (newRecord)
			{
				_fileName = fileName;
				treeView.BeginUpdate();
				treeView.Nodes.Clear();

				TreeNode templateNode = new TreeNode((fileName == null ? "Untitled" : Path.GetFileName(fileName)));
				templateNode.Tag = record;

				AddIrises(templateNode, record);

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

				AddIrises(root, record);

				treeView.Nodes.Add(root);
				treeView.SelectedNode = root;

				treeView.EndUpdate();
			}
		}

		private void OpenTemplate()
		{
			string fileName;
			byte[] templ = LoadTemplate(iiRecordOpenFileDialog, out fileName);
			if (templ == null)
				return;

			BdifStandard standard = BdifStandard.Iso;
			uint flags = 0;
			var version = IIRecord.VersionIsoCurrent;
			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.Open, ref standard, ref flags, ref version))
				return;

			IIRecord template;
			try
			{
				template = new IIRecord(new NBuffer(templ), flags, standard);
			}
			catch (Exception ex)
			{
				ShowError("Failed to load template! Reason:\r\n" + ex);
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
				ShowError("Failed to load template! Reason:\r\n" + ex.ToString());
				return;
			}
			SetCbeff(template, fileName);
		}

		private void SaveTemplate(IIRecord record)
		{
			SaveTemplate(iiRecordSaveFileDialog, _fileName, record.Save().ToArray());
			if (treeView.Nodes[0].Tag is IIRecord)
				treeView.Nodes[0].Text = Path.GetFileName(iiRecordSaveFileDialog.FileName);
		}

		private void OnSelectedItemChanged()
		{
			TreeNode selectedNode = treeView.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			IirIrisImage iImage = treeView.SelectedNode.Tag as IirIrisImage;
			if (iImage != null)
			{
				iiView.Record = iImage;
				propertyGrid.SelectedObject = iImage;
			}
			else
			{
				iiView.Record = null;
				propertyGrid.SelectedObject = selectedNode.Tag;
			}

			if (selectedNode.Tag is CbeffRecord)
			{
				addIrisImageToolStripMenuItem.Enabled = false;
				convertToolStripMenuItem.Enabled = false;
				removeToolStripMenuItem.Enabled = false;
				saveIrisToolStripMenuItem.Enabled = false;
				saveToolStripMenuItem.Enabled = false;
			}
			else
			{
				addIrisImageToolStripMenuItem.Enabled = true;
				convertToolStripMenuItem.Enabled = true;
				removeToolStripMenuItem.Enabled = true;
				saveIrisToolStripMenuItem.Enabled = true;
				saveToolStripMenuItem.Enabled = true;
			}
		}

		private bool IsRecordFirstVersion(IIRecord record)
		{
			return (record.Standard == BdifStandard.Iso && record.Version == IIRecord.VersionIso10
					|| record.Standard == BdifStandard.Ansi && record.Version == IIRecord.VersionAnsi10);
		}

		#endregion

		#region Private form methods

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox.Show();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BdifStandard standard = BdifStandard.Iso;
			uint flags = 0;
			var version = IIRecord.VersionIsoCurrent;

			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.New, ref standard, ref flags, ref version))
				return;

			IIRecord record = new IIRecord(standard, version, flags);
			SetTemplate(record, null);
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
			TreeNode recordNode = GetFirstNodeWithTag<IIRecord>();
			if (recordNode == null)
			{
				ShowError("Add IIRecord first");
			}

			try
			{
				SaveTemplate((IIRecord)recordNode.Tag);
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

		private void convertToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode rootNode = GetFirstNodeWithTag<IIRecord>();
			if (rootNode == null)
				return;

			IIRecord iiRecord = rootNode.Tag as IIRecord;
			BdifStandard standard = (iiRecord.Standard == BdifStandard.Ansi) ? BdifStandard.Iso : BdifStandard.Ansi;
			var version = standard == BdifStandard.Iso ? IIRecord.VersionIsoCurrent : IIRecord.VersionAnsiCurrent;
			uint flags = 0;

			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.Convert, ref standard, ref flags, ref version))
				return;

			iiRecord = ConvertToStandard(iiRecord, standard, flags, version);
			rootNode.Tag = iiRecord;

			for (int i = 0; i < iiRecord.IrisImages.Count; i++)
			{
				rootNode.Nodes[i].Tag = iiRecord.IrisImages[i];
			}

			OnSelectedItemChanged();
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selected = treeView.SelectedNode;
			if (selected == null)
				return;

			TreeNode recordNode = GetFirstNodeWithTag<IIRecord>();
			if (recordNode == null)
			{
				ShowError("Add IIRecord first");
				return;
			}

			IirIrisImage fview = selected.Tag as IirIrisImage;
			if (fview != null)
			{
				IIRecord record = recordNode.Tag as IIRecord;
				record.IrisImages.Remove(fview);
				recordNode.Nodes.Remove(selected);
			}
		}

		private void saveIrisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode imageNode = GetFirstNodeWithTag<IirIrisImage>();
			if (imageNode == null)
			{
				ShowWarning("Please select an image");
				return;
			}

			if (saveImageFileDialog.ShowDialog() == DialogResult.OK)
			{
				IirIrisImage image = imageNode.Tag as IirIrisImage;
				if (image != null) image.ToNImage().Save(saveImageFileDialog.FileName);
			}
		}

		private void addIrisImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode recordNode = GetFirstNodeWithTag<IIRecord>();
			if (recordNode == null)
			{
				ShowWarning("IIRecord must be opened! Open or create new IIRecord");
				return;
			}

			if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (var image = NImage.FromFile(imageOpenFileDialog.FileName))
					{
						IIRecord record = recordNode.Tag as IIRecord;

						using (AddIrisForm form = new AddIrisForm(record.Version))
						{
							if (form.ShowDialog() != DialogResult.OK)
								return;

							IirIrisImage irisImage = new IirIrisImage(record.Standard, record.Version);

							irisImage.Position = form.IrisPosition;

							if (IsRecordFirstVersion(record) && record.IrisImages.Count == 0)
							{
								record.RawImageHeight = (ushort)image.Height;
								record.RawImageWidth = (ushort)image.Width;
								record.IntensityDepth = 8;
							}
							irisImage.SetImage(image);
							record.IrisImages.Add(irisImage);
							AddIrisImage(recordNode, irisImage);
						}
					}
					treeView.ExpandAll();
				}
				catch (Exception ex)
				{
					ShowError(ex.ToString());
				}
			}
		}

		#endregion
	}
}
