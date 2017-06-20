using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Standards;
using Neurotec.Biometrics.Standards.Gui;
using Neurotec.Gui;
using Neurotec.IO;
using Neurotec.Licensing;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Private static fields

		private static uint ansiFCRecord = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFaceImage);
		private static uint isoFCRecord = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFaceImage);
		private static uint ansiFIRecord = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerImage);
		private static uint isoFIRecord = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFingerImage);
		private static uint ansiFMRecord = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerMinutiaeX);
		private static uint isoFMRecord = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.FederalOfficeForInformationSecurityTRBiometricsXmlFinger10);
		private static uint ansiIIRecordPolar = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsIrisPolar);
		private static uint isoIIRecordPolar = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsIrisImagePolar);
		private static uint ansiIIRecordRectilinear = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsIrisRectilinear);
		private static uint isoIIRecordRectilinear = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsIrisImageRectilinear);

		#endregion

		#region Private fields

		private string _filename;

		#endregion

		#region Public constructor

		public MainForm()
		{
			InitializeComponent();
			aboutToolStripMenuItem.Text = '&' + AboutBox.Name;
		}

		#endregion

		#region Private methods

		private void ShowError(string message)
		{
			MessageBox.Show(message, Text + ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void ShowWarning(string message)
		{
			MessageBox.Show(message, Text + ": Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private DialogResult ShowQuestion(string message)
		{
			return MessageBox.Show(message, Text + ": Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		}

		private void ShowInformation(string message)
		{
			MessageBox.Show(message, Text + ": Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		private bool LoadRecord(OpenFileDialog openFileDialog, out string fileName, out byte[] buffer)
		{
			openFileDialog.FileName = null;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				fileName = openFileDialog.FileName;
				buffer = File.ReadAllBytes(openFileDialog.FileName);
				return true;
			}
			fileName = null;
			buffer = null;
			return false;
		}

		private bool GetOptions(ref uint format)
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

		private bool GetOptions(ref uint format, ref BdifStandard standard)
		{
			using (AddRecordOptionsForm form = new AddRecordOptionsForm())
			{
				if(form.ShowDialog() == DialogResult.OK)
				{
					format = form.PatronFormat;
					standard = form.Standard;
				}
				else 
				{
					return false;
				}
			}
			return true;
		}

		#region Read xxRecord data

		private void ReadFMRecord(TreeNode node, FMRecord record)
		{
			foreach (FmrFingerView finger in record.FingerViews)
			{
				TreeNode child = new TreeNode(finger.FingerPosition.ToString());
				child.Tag = finger;
				node.Nodes.Add(child);
			}
		}

		private void ReadFCRecord(TreeNode node, FCRecord record)
		{
			int index;
			foreach (FcrFaceImage face in record.FaceImages)
			{
				index = node.Nodes.Count + 1;
				TreeNode child = new TreeNode("Face " + index);
				child.Tag = face;
				node.Nodes.Add(child);
			}
		}

		private void ReadFIRecord(TreeNode node, FIRecord record)
		{
			foreach (FirFingerView finger in record.FingerViews)
			{
				TreeNode child = new TreeNode(finger.Position.ToString());
				child.Tag = finger;
				node.Nodes.Add(child);
			}
		}

		private void ReadIIRecord(TreeNode node, IIRecord record)
		{
			foreach (IirIrisImage iris in record.IrisImages)
			{
				TreeNode child = new TreeNode(iris.Position.ToString());
				child.Tag = iris;
				node.Nodes.Add(child);
			}
		}

		#endregion

		private void ExtractData(TreeNode node, CbeffRecord record)
		{
			if(record.BdbFormat == ansiFCRecord)
			{
				//FCRecordAnsi
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Faces")) return;
				FCRecord fcRecord = new FCRecord(record.BdbBuffer, BdifStandard.Ansi);
				TreeNode child = new TreeNode("FCRecord");
				child.Tag = fcRecord;
				ReadFCRecord(child, fcRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == isoFCRecord)
			{
				//FCRecordIso
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Faces")) return;
				FCRecord fcRecord = new FCRecord(record.BdbBuffer, BdifStandard.Iso);
				TreeNode child = new TreeNode("FCRecord");
				child.Tag = fcRecord;
				ReadFCRecord(child, fcRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == ansiFIRecord)
			{
				//FIRecordAnsi
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Fingers")) return;
				FIRecord fiRecord = new FIRecord(record.BdbBuffer, BdifStandard.Ansi);
				TreeNode child = new TreeNode("FIRecord");
				child.Tag = fiRecord;
				ReadFIRecord(child, fiRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == isoFIRecord)
			{
				//FIRecordIso
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Fingers")) return;
				FIRecord fiRecord = new FIRecord(record.BdbBuffer, BdifStandard.Iso);
				TreeNode child = new TreeNode("FIRecord");
				child.Tag = fiRecord;
				ReadFIRecord(child, fiRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == ansiFMRecord)
			{
				//FMRecordAnsi
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Fingers")) return;
				FMRecord fmRecord = new FMRecord(record.BdbBuffer, BdifStandard.Ansi);
				TreeNode child = new TreeNode("FMRecord");
				child.Tag = fmRecord;
				ReadFMRecord(child, fmRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == isoFMRecord)
			{
				//FMRecordIso
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Fingers")) return;
				FMRecord fmRecord = new FMRecord(record.BdbBuffer, BdifStandard.Iso);
				TreeNode child = new TreeNode("FMRecord");
				child.Tag = fmRecord;
				ReadFMRecord(child, fmRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == ansiIIRecordPolar)
			{
				//IIRecordAnsiPolar
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Irises")) return;
				IIRecord iiRecord = new IIRecord(record.BdbBuffer, BdifStandard.Ansi);
				TreeNode child = new TreeNode("IIRecord");
				child.Tag = iiRecord;
				ReadIIRecord(child, iiRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == isoIIRecordPolar)
			{
				//IIRecordIsoPolar
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Irises")) return;
				IIRecord iiRecord = new IIRecord(record.BdbBuffer, BdifStandard.Iso);
				TreeNode child = new TreeNode("IIRecord");
				child.Tag = iiRecord;
				ReadIIRecord(child, iiRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == ansiIIRecordRectilinear)
			{
				//IIRecordAnsiRectilinear
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Irises")) return;
				IIRecord iiRecord = new IIRecord(record.BdbBuffer, BdifStandard.Ansi);
				TreeNode child = new TreeNode("IIRecord");
				child.Tag = iiRecord;
				ReadIIRecord(child, iiRecord);
				node.Nodes.Add(child);
			}
			else if(record.BdbFormat == isoIIRecordRectilinear)
			{
				//IIRecordIsoRectilinear
				if (!NLicense.IsComponentActivated("Biometrics.Standards.Irises")) return;
				IIRecord iiRecord = new IIRecord(record.BdbBuffer, BdifStandard.Iso);
				TreeNode child = new TreeNode("IIRecord");
				child.Tag = iiRecord;
				ReadIIRecord(child, iiRecord);
				node.Nodes.Add(child);
			}
		}

		private void ReadDataBlock(TreeNode node, CbeffRecord record)
		{
			if (record.BdbBuffer != null)
			{
				try
				{
					ExtractData(node, record);
				}
				catch (Exception ex)
				{
					ShowWarning("One of Biometric Data Blocks could not be opened.\r\n" + ex.Message);
				}
			}
		}

		private void ReadRecord(TreeNode parentNode, CbeffRecord parentRecord)
		{
			ReadDataBlock(parentNode, parentRecord);

			foreach (CbeffRecord childRecord in parentRecord.Records)
			{
				TreeNode childNode = new TreeNode("CbeffRecord");
				ReadRecord(childNode, childRecord);
				childNode.Tag = childRecord;
				parentNode.Nodes.Add(childNode);
			}
		}

		private void SetRecord(CbeffRecord record)
		{
			treeView.BeginUpdate();
			treeView.Nodes.Clear();

			TreeNode rootNode = new TreeNode(_filename == null ? "Unknown" : Path.GetFileName(_filename));

			ReadRecord(rootNode, record);

			rootNode.Tag = record;
			treeView.Nodes.Add(rootNode);
			treeView.SelectedNode = rootNode;

			treeView.EndUpdate();
			treeView.ExpandAll();
		}

		private void OpenRecord()
		{
			string fileName;
			byte[] buffer;
			if (!LoadRecord(cbeffRecordOpenFileDialog, out fileName, out buffer))
				return;

			uint format = 0;
			if (!GetOptions(ref format))
				return;

			string oldFilename = _filename;
			_filename = fileName;
			CbeffRecord record = null;
			try
			{
				record = new CbeffRecord(new NBuffer(buffer), format);
				SetRecord(record);
			}
			catch (Exception ex)
			{
				_filename = oldFilename;
				ShowError("CbeffRecord could not be opened.\r\n" + ex.ToString());
			}
		}

		private void SaveAsRecord()
		{
			if (treeView.Nodes.Count == 0)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			CbeffRecord record = treeView.Nodes[0].Tag as CbeffRecord;
			if (record == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			cbeffRecordSaveFileDialog.FileName = _filename;
			if (cbeffRecordSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName;

				fileName = cbeffRecordSaveFileDialog.FileName;

				try
				{
					NBuffer nBuffer = record.Save();
					byte[] buffer = nBuffer.ToArray();
					File.WriteAllBytes(fileName, buffer);
				}
				catch (Exception ex)
				{
					ShowError("CbeffRecord could not be saved.\r\n" + ex.ToString());
				}

				_filename = fileName;

				treeView.Nodes[0].Text = Path.GetFileName(fileName);
			}
		}

		private void SaveSelectedRecord()
		{
			TreeNode selected = treeView.SelectedNode;
			if (selected == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			try
			{
				if (selected.Tag is CbeffRecord)
				{
					cbeffRecordSaveFileDialog.FileName = _filename;
					if (cbeffRecordSaveFileDialog.ShowDialog() != DialogResult.OK)
						return;

					NBuffer nBuffer = ((CbeffRecord)selected.Tag).Save();
					byte[] buffer = nBuffer.ToArray();
					File.WriteAllBytes(cbeffRecordSaveFileDialog.FileName, buffer);
					_filename = cbeffRecordSaveFileDialog.FileName;
				}
				else if (selected.Tag is FCRecord)
				{
					fcRecordSaveFileDialog.FileName = _filename;
					if (fcRecordSaveFileDialog.ShowDialog() != DialogResult.OK)
						return;

					NBuffer nBuffer = ((FCRecord)selected.Tag).Save();
					byte[] buffer = nBuffer.ToArray();
					File.WriteAllBytes(fcRecordSaveFileDialog.FileName, buffer);
					_filename = fcRecordSaveFileDialog.FileName;
				}
				else if (selected.Tag is FIRecord)
				{
					fiRecordSaveFileDialog.FileName = _filename;
					if (fiRecordSaveFileDialog.ShowDialog() != DialogResult.OK)
						return;

					NBuffer nBuffer = ((FIRecord)selected.Tag).Save();
					byte[] buffer = nBuffer.ToArray();
					File.WriteAllBytes(fiRecordSaveFileDialog.FileName, buffer);
					_filename = fiRecordSaveFileDialog.FileName;
				}
				else if (selected.Tag is FMRecord)
				{
					fmRecordSaveFileDialog.FileName = _filename;
					if (fmRecordSaveFileDialog.ShowDialog() != DialogResult.OK)
						return;

					NBuffer nBuffer = ((FMRecord)selected.Tag).Save();
					byte[] buffer = nBuffer.ToArray();
					File.WriteAllBytes(fcRecordSaveFileDialog.FileName, buffer);
					_filename = fcRecordSaveFileDialog.FileName;
				}
				else if (selected.Tag is IIRecord)
				{
					iiRecordSaveFileDialog.FileName = _filename;
					if (iiRecordSaveFileDialog.ShowDialog() != DialogResult.OK)
						return;

					NBuffer nBuffer = ((IIRecord)selected.Tag).Save();
					byte[] buffer = nBuffer.ToArray();
					File.WriteAllBytes(iiRecordSaveFileDialog.FileName, buffer);
					_filename = iiRecordSaveFileDialog.FileName;
				}
			}
			catch (Exception ex)
			{
				ShowError("Selected item could not be saved.\n\r" + ex.ToString());
			}
		}

		private void SelectedItemChanged()
		{
			TreeNode selectedNode = treeView.SelectedNode;
			if (selectedNode == null)
				return;

			propertyGrid.SelectedObject = selectedNode.Tag;

			CbeffRecord record = selectedNode.Tag as CbeffRecord;
			if (record != null)
			{
				if (record.BdbBuffer != null)
					InsertAfterToolStripMenuItem.Enabled = false;
				else
					InsertAfterToolStripMenuItem.Enabled = true;

				addCbeffRecordToolStripMenuItem.Enabled = true;
				addFromFileToolStripMenuItem.Enabled = true;
				InsertBeforeToolStripMenuItem.Enabled = true;
				addFCRecordToolStripMenuItem.Enabled = true;
				addFIRecordToolStripMenuItem.Enabled = true;
				addFMRecordToolStripMenuItem.Enabled = true;
				addIIRecordToolStripMenuItem.Enabled = true;
				removeBranchToolStripMenuItem.Enabled = true;
			}
			else
			{
				addCbeffRecordToolStripMenuItem.Enabled = false;
				addFromFileToolStripMenuItem.Enabled = false;
				InsertBeforeToolStripMenuItem.Enabled = false;
				InsertAfterToolStripMenuItem.Enabled = false;
				addFCRecordToolStripMenuItem.Enabled = false;
				addFIRecordToolStripMenuItem.Enabled = false;
				addFMRecordToolStripMenuItem.Enabled = false;
				addIIRecordToolStripMenuItem.Enabled = false;

				if (selectedNode.Tag is FCRecord || selectedNode.Tag is FIRecord ||
				selectedNode.Tag is FMRecord || selectedNode.Tag is IIRecord)
				{
					removeBranchToolStripMenuItem.Enabled = true;
				}
				else
				{
					removeBranchToolStripMenuItem.Enabled = false;
				}
			}
		}

		private void RemoveSelectedBranch()
		{
			TreeNode selected = treeView.SelectedNode;
			if (selected == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			CbeffRecord root = treeView.Nodes[0].Tag as CbeffRecord;
			if (root == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			if (selected.Parent == null)
			{
				treeView.Nodes.Clear();
			}
			else
			{
				try
				{
					if (selected.Tag is CbeffRecord)
					{
						CbeffRecord parentRecord = selected.Parent.Tag as CbeffRecord;
						CbeffRecord record = selected.Tag as CbeffRecord;
						parentRecord.Records.Remove(record);
					}
					else
					{
						CbeffRecord parentRecord = selected.Parent.Tag as CbeffRecord;
						parentRecord.BdbBuffer = null;
						parentRecord.BdbFormat = 0;
						parentRecord.BdbIndex = null;
					}
					SetRecord(root);
				}
				catch (Exception ex)
				{
					ShowError("Selected branch could not be removed.\n\r" + ex.ToString());
				}
			}
		}

		private void AddRecord()
		{
			TreeNode recordNode = GetFirstNodeWithTag<CbeffRecord>();
			if (recordNode == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			CbeffRecord root = treeView.Nodes[0].Tag as CbeffRecord;
			if (root == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			uint format = 0;
			if (!GetOptions(ref format))
				return;

			CbeffRecord record = recordNode.Tag as CbeffRecord;

			try
			{
				CbeffRecord newRecord = new CbeffRecord(format);
				record.Records.Add(newRecord);

				SetRecord(root);
			}
			catch (Exception ex)
			{
				ShowError("CbeffRecord could not be added.\n\r" + ex.ToString());
			}
		}

		private void AddRecordFromFile<T>(OpenFileDialog dialog)
		{
			TreeNode recordNode = GetFirstNodeWithTag<CbeffRecord>();
			if (recordNode == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			CbeffRecord root = treeView.Nodes[0].Tag as CbeffRecord;
			if (root == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			string fileName;
			byte[] buffer;
			if (!LoadRecord(dialog, out fileName, out buffer))
				return;

			uint format = 0;
			BdifStandard standard = BdifStandard.Ansi;
			if (typeof(T) == typeof(CbeffRecord))
			{
				if (!GetOptions(ref format))
					return;
			}
			else
			{
				if (!GetOptions(ref format, ref standard))
					return;
			}

			CbeffRecord record = recordNode.Tag as CbeffRecord;

			try
			{
				if (typeof(T) == typeof(CbeffRecord))
				{
					CbeffRecord newRecord = new CbeffRecord(new NBuffer(buffer), format);
					record.Records.Add(newRecord);
				}
				else if (typeof(T) == typeof(FCRecord))
				{
					FCRecord fcRecord = new FCRecord(buffer, standard);
					CbeffRecord newRecord = new CbeffRecord(fcRecord, format);
					record.Records.Add(newRecord);
				}
				else if (typeof(T) == typeof(FIRecord))
				{
					FIRecord fiRecord = new FIRecord(buffer, standard);
					CbeffRecord newRecord = new CbeffRecord(fiRecord, format);
					record.Records.Add(newRecord);
				}
				else if (typeof(T) == typeof(FMRecord))
				{
					FMRecord fmRecord = new FMRecord(buffer, standard);
					CbeffRecord newRecord = new CbeffRecord(fmRecord, format);
					record.Records.Add(newRecord);
				}
				else if (typeof(T) == typeof(IIRecord))
				{
					IIRecord iiRecord = new IIRecord(buffer, standard);
					CbeffRecord newRecord = new CbeffRecord(iiRecord, format);
					record.Records.Add(newRecord);
				}
				else
				{
					throw new ArgumentException("Operation is not supported.\n\r");
				}
				SetRecord(root);
			}
			catch (Exception ex)
			{
				ShowError(typeof(T).ToString() + " could not be added.\n\r" + ex.ToString());
			}
		}

		private void NewRecord()
		{
			uint format = 0;
			if (!GetOptions(ref format))
				return;

			string oldFilename = _filename;
			_filename = null;
			try
			{
				CbeffRecord record = new CbeffRecord(format);
				SetRecord(record);
			}
			catch (Exception ex)
			{
				_filename = oldFilename;
				ShowError("CbeffRecord could not be created.\n\r" + ex.ToString());
			}
		}

		private void InsertBeforeSelectedRecord()
		{
			TreeNode recordNode = GetFirstNodeWithTag<CbeffRecord>();
			if (recordNode == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			uint format = 0;
			if (!GetOptions(ref format))
				return;

			CbeffRecord record = recordNode.Tag as CbeffRecord;

			TreeNode parentNode = recordNode.Parent;

			try
			{
				CbeffRecord newRecord = new CbeffRecord(format);
				if (parentNode != null)
				{
					CbeffRecord parentRecord = parentNode.Tag as CbeffRecord;
					parentRecord.Records.Remove(record);
					CbeffRecord copyRecord = new CbeffRecord(record.Save(), record.PatronFormat);
					newRecord.Records.Add(copyRecord);
					parentRecord.Records.Add(newRecord);

					SetRecord(parentRecord);
				}
				else
				{
					newRecord.Records.Add(record);
					SetRecord(newRecord);
				}
			}
			catch (Exception ex)
			{
				ShowError("CbeffRecord could not be inserted.\n\r" + ex.ToString());
			}
		}

		private void InsertAfterSelectedRecord()
		{
			TreeNode recordNode = GetFirstNodeWithTag<CbeffRecord>();
			if (recordNode == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			CbeffRecord root = treeView.Nodes[0].Tag as CbeffRecord;
			if (root == null)
			{
				ShowError("Operation is not supported.\n\rAdd CbeffRecord first.");
				return;
			}

			uint format = 0;
			if (!GetOptions(ref format))
				return;

			CbeffRecord record = recordNode.Tag as CbeffRecord;

			try
			{
				CbeffRecord newRecord = new CbeffRecord(format);
				foreach (CbeffRecord childRecord in record.Records)
				{
					CbeffRecord rec = new CbeffRecord(childRecord.Save(), childRecord.PatronFormat);
					newRecord.Records.Add(rec);
				}
				record.Records.Clear();
				record.Records.Add(newRecord);

				SetRecord(root);
			}
			catch (Exception ex)
			{
				ShowError("CbeffRecord could not be inserted.\r\n" + ex.ToString());
			}
		}

		#endregion

		#region Private form methods

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenRecord();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAsRecord();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox.Show();
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			SelectedItemChanged();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void removeBranchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RemoveSelectedBranch();
		}

		private void addCbeffRecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRecord();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewRecord();
		}

		private void saveSelectedStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSelectedRecord();
		}

		private void InsertBeforeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InsertBeforeSelectedRecord();
		}

		private void InsertAfterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InsertAfterSelectedRecord();
		}

		private void AddFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRecordFromFile<CbeffRecord>(cbeffRecordOpenFileDialog);
		}

		private void addFCRecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRecordFromFile<FCRecord>(fcRecordOpenFileDialog);
		}

		private void addFIRecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRecordFromFile<FIRecord>(fiRecordOpenFileDialog);
		}

		private void addFMRecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRecordFromFile<FMRecord>(fmRecordOpenFileDialog);
		}

		private void addIIRecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddRecordFromFile<IIRecord>(iiRecordOpenFileDialog);
		}
		
		#endregion
	}
}
