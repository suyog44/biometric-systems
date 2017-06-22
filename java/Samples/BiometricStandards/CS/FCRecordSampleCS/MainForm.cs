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

		private static uint ansiBdbFormat = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFaceImage);
		private static uint isoBdbFormat = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFaceImage);

		#endregion

		#region Private static methods

		private static bool GetOptions(BdifOptionsForm.BdifOptionsFormMode mode, ref BdifStandard standard, ref uint flags, ref NVersion version)
		{
			using (FCRecordOptionsForm form = new FCRecordOptionsForm())
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

		private static void GetBiometricDataBlock(TreeNode node, CbeffRecord record)
		{
			if (record.BdbBuffer != null && (record.BdbFormat == ansiBdbFormat || record.BdbFormat == isoBdbFormat))
			{
				BdifStandard standard = record.BdbFormat == ansiBdbFormat ? BdifStandard.Ansi : BdifStandard.Iso;
				FCRecord fcRecord = new FCRecord(record.BdbBuffer, standard);
				TreeNode child = new TreeNode("FCRecord");
				child.Tag = fcRecord;
				AddFaceImages(child, fcRecord);
				node.Nodes.Add(child);
			}
		}

		private static void AddFaceImages(TreeNode node, CbeffRecord record)
		{
			GetBiometricDataBlock(node, record);

			foreach (CbeffRecord child in record.Records)
			{
				AddFaceImages(node, child);
			}
		}

		private static TreeNode AddFaceImage(TreeNode node, FcrFaceImage faceImage)
		{
			int index = node.Nodes.Count + 1;
			TreeNode recordNode = new TreeNode("FaceImage " + index);
			recordNode.Tag = faceImage;
			node.Nodes.Add(recordNode);
			return recordNode;
		}

		private static void AddFaceImages(TreeNode node, FCRecord record)
		{
			foreach (FcrFaceImage faceImage in record.FaceImages)
			{
				AddFaceImage(node, faceImage);
			}
		}

		private static FCRecord ConvertToStandard(FCRecord record, BdifStandard newStandard, uint flags, NVersion version)
		{
			if (record.Standard == newStandard && record.Flags == flags && record.Version == version)
				return record;

			return new FCRecord(record, flags, newStandard, version);
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

		private TreeNode GetFirstNodeWithTag<T>()
		{
			TreeNode node = treeView.SelectedNode;
			if (node == null)
				return null;

			while (node.Parent != null && !(node.Tag is T))
				node = node.Parent;

			return node.Tag is T ? node : null;
		}

		private void SetTemplate(FCRecord record, string fileName)
		{
			bool newRecord = true;
			TreeNode recordNode = GetFirstNodeWithTag<FCRecord>();
			if (recordNode == null)
				newRecord = true;
			else
				newRecord = ((FCRecord)recordNode.Tag) != record;

			if (newRecord)
			{
				_fileName = fileName;
				treeView.BeginUpdate();
				treeView.Nodes.Clear();

				TreeNode templateNode = new TreeNode((fileName == null ? "Untitled" : Path.GetFileName(fileName)));
				templateNode.Tag = record;

				AddFaceImages(templateNode, record);

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

				AddFaceImages(root, record);

				treeView.Nodes.Add(root);
				treeView.SelectedNode = root;

				treeView.EndUpdate();
			}
		}

		private void NewTemplate()
		{
			BdifStandard standard = BdifStandard.Iso;
			uint flags = 0;
			NVersion version = FCRecord.VersionIsoCurrent;

			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.New, ref standard, ref flags, ref version))
				return;

			NewTemplate(standard, flags, version);
		}

		private void NewTemplate(BdifStandard standard, uint flags, NVersion version)
		{
			FCRecord record = new FCRecord(standard, version, flags);
			SetTemplate(record, null);
		}

		private void OpenTemplate()
		{
			string fileName;
			byte[] templ = LoadTemplate(fcRecordOpenFileDialog, out fileName);
			if (templ == null)
				return;

			BdifStandard standard = BdifStandard.Iso;
			uint flags = 0;
			NVersion version = FCRecord.VersionIsoCurrent;
			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.Open, ref standard, ref flags, ref version))
				return;

			FCRecord template;
			try
			{
				template = new FCRecord(new NBuffer(templ), flags, standard);
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

		private void SaveTemplate(FCRecord record)
		{
			SaveTemplate(fcRecordSaveFileDialog, _fileName, record.Save().ToArray());
			if (treeView.Nodes[0].Tag is FCRecord)
				treeView.Nodes[0].Text = Path.GetFileName(fcRecordSaveFileDialog.FileName);
		}

		private void OnSelectedItemChanged()
		{
			TreeNode selected = treeView.SelectedNode;

			if (selected != null)
			{
				FcrFaceImage faceImage = selected.Tag as FcrFaceImage;
				fcView.Record = faceImage;

				if (faceImage != null)
				{
					propertyGrid.SelectedObject = faceImage;
				}
				else
				{
					propertyGrid.SelectedObject = treeView.SelectedNode == null ? null : treeView.SelectedNode.Tag;
				}

				if (selected.Tag is CbeffRecord)
				{
					addFaceFromImageToolStripMenuItem.Enabled = false;
					addFaceFromRawToolStripMenuItem.Enabled = false;
					saveFaceAsDataToolStripMenuItem.Enabled = false;
					saveFaceToolStripMenuItem.Enabled = false;
					convertToolStripMenuItem.Enabled = false;
					removeToolStripMenuItem.Enabled = false;
					saveToolStripMenuItem.Enabled = false;
				}
				else
				{
					addFaceFromImageToolStripMenuItem.Enabled = true;
					addFaceFromRawToolStripMenuItem.Enabled = true;
					saveFaceAsDataToolStripMenuItem.Enabled = true;
					saveFaceToolStripMenuItem.Enabled = true;
					convertToolStripMenuItem.Enabled = true;
					removeToolStripMenuItem.Enabled = true;
					saveToolStripMenuItem.Enabled = true;
				}
			}
		}

		#endregion

		#region Private form methods

		private void saveFaceAsDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selected = treeView.SelectedNode;
			if ((selected != null) && (selected.Parent != null))
			{
				FcrFaceImage img = selected.Tag as FcrFaceImage;
				if (img != null)
				{
					saveRawFileDialog.Filter = (img.ImageDataType == FcrImageDataType.Jpeg) ? string.Format("JPEG Files ({0})|{0}", NImageFormat.Jpeg.FileFilter) : string.Format("JPEG 2000 Files ({0})|{0}", NImageFormat.Jpeg2K.FileFilter);

					if (saveRawFileDialog.ShowDialog() == DialogResult.OK)
					{
						try
						{
							File.WriteAllBytes(saveRawFileDialog.FileName, img.ImageData.ToArray());
						}
						catch (Exception ex)
						{
							ShowError("Failed to save face to data.\r\nReason: " + ex);
							return;
						}
					}
				}
			}
		}

		private void saveFaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selected = treeView.SelectedNode;
			if ((selected != null) && (selected.Parent != null))
			{
				if (saveImageFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						FcrFaceImage img = selected.Tag as FcrFaceImage;
						if (img != null)
							using (NImage image = img.ToNImage())
							{
								image.Save(saveImageFileDialog.FileName);
							}
					}
					catch (Exception ex)
					{
						ShowError("Failed to save image to file!\r\nReason: " + ex);
						return;
					}
				}
			}
		}

		private void addFaceFromImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode recordNode = GetFirstNodeWithTag<FCRecord>();
			if (recordNode == null)
			{
				ShowError("Operation is not supported.\r\nAdd FCRecord first");
				return;
			}

			if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				FcrFaceImageType imageType;
				FcrImageDataType dataType;

				using (AddFaceImageForm form = new AddFaceImageForm())
				{
					string extension = Path.GetExtension(imageOpenFileDialog.FileName);
					dataType = (NImageFormat.Jpeg2K.FileFilter).Contains(extension) ? FcrImageDataType.Jpeg2000 : FcrImageDataType.Jpeg;
					form.ImageDataType = dataType;
					if (form.ShowDialog() == DialogResult.OK)
					{
						imageType = form.FaceImageType;
						dataType = form.ImageDataType;
					}
					else
					{
						return;
					}
				}

				try
				{
					using (NImage image = NImage.FromFile(imageOpenFileDialog.FileName))
					{
						FCRecord record = recordNode.Tag as FCRecord;

						FcrFaceImage img = new FcrFaceImage(record.Standard, record.Version);
						img.FaceImageType = imageType;
						img.ImageDataType = dataType;
						img.SetImage(image);
						record.FaceImages.Add(img);
						TreeNode node = AddFaceImage(recordNode, img);
						treeView.SelectedNode = node;
					}
				}
				catch (Exception ex)
				{
					ShowError("Failed to add image. Reason:\r\n" + ex);
					return;
				}
				treeView.ExpandAll();
			}
		}

		private void addFaceFromRawToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (rawImageOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				ushort width, height;
				FcrFaceImageType imageType;
				FcrImageDataType dataType;
				FcrImageColorSpace imageColorSpace;
				byte vendorImageColorSpace;

				using (RawFaceImageOptionsForm form = new RawFaceImageOptionsForm())
				{
					string extension = Path.GetExtension(rawImageOpenFileDialog.FileName);
					dataType = (NImageFormat.Jpeg2K.FileFilter).Contains(extension) ? FcrImageDataType.Jpeg2000 : FcrImageDataType.Jpeg;
					form.ImageDataType = dataType;

					if (form.ShowDialog() == DialogResult.OK)
					{
						imageType = form.FaceImageType;
						dataType = form.ImageDataType;
						imageColorSpace = form.ImageColorSpace;
						width = form.ImageWidth;
						height = form.ImageHeight;
						vendorImageColorSpace = form.VendorColorSpace;
					}
					else
					{
						return;
					}
				}

				try
				{
					byte[] rawImage = File.ReadAllBytes(rawImageOpenFileDialog.FileName);

					TreeNode recordNode = GetFirstNodeWithTag<FCRecord>();
					FCRecord record = recordNode.Tag as FCRecord;

					FcrFaceImage img = new FcrFaceImage(record.Standard, record.Version);
					img.FaceImageType = imageType;
					img.ImageDataType = dataType;
					img.Width = width;
					img.Height = height;
					img.SetImageColorSpace(imageColorSpace, vendorImageColorSpace);
					img.ImageData = new NBuffer(rawImage);
					record.FaceImages.Add(img);
					TreeNode node = AddFaceImage(treeView.Nodes[0], img);
					treeView.SelectedNode = node;
				}
				catch (Exception ex)
				{
					ShowError("Failed to add image. Reason:\r\n" + ex);
					return;
				}
			}
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selected = treeView.SelectedNode;
			if (selected == null)
				return;

			TreeNode recordNode = GetFirstNodeWithTag<FCRecord>();
			if (recordNode == null)
			{
				ShowError("Add FMRecord first");
				return;
			}

			FcrFaceImage fview = selected.Tag as FcrFaceImage;
			if (fview != null)
			{
				FCRecord record = recordNode.Tag as FCRecord;
				record.FaceImages.Remove(fview);
				recordNode.Nodes.Remove(selected);
			}
		}

		private void convertToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode rootNode = GetFirstNodeWithTag<FCRecord>();
			if (rootNode == null)
				return;

			FCRecord fcRecord = rootNode.Tag as FCRecord;
			BdifStandard standard = (fcRecord.Standard == BdifStandard.Ansi) ? BdifStandard.Iso : BdifStandard.Ansi;
			uint flags = 0;
			NVersion version = standard == BdifStandard.Iso ? FCRecord.VersionIsoCurrent : FCRecord.VersionAnsiCurrent;

			if (!GetOptions(BdifOptionsForm.BdifOptionsFormMode.Convert, ref standard, ref flags, ref version))
				return;

			fcRecord = ConvertToStandard(fcRecord, standard, flags, version);
			rootNode.Tag = fcRecord;

			for (int i = 0; i < fcRecord.FaceImages.Count; i++)
			{
				rootNode.Nodes[i].Tag = fcRecord.FaceImages[i];
			}

			OnSelectedItemChanged();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox.Show();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewTemplate();
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
			TreeNode recordNode = GetFirstNodeWithTag<FCRecord>();
			if (recordNode == null)
			{
				ShowError("Add FCRecord first");
				return;
			}

			try
			{
				SaveTemplate((FCRecord)recordNode.Tag);
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

		#endregion
	}
}
