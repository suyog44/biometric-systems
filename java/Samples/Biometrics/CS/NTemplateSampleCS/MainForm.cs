using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Gui;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Private static methods

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

		private static byte[] LoadTemplate(OpenFileDialog openFileDialog)
		{
			string fileName;
			return LoadTemplate(openFileDialog, out fileName);
		}

		private static void SaveTemplate(SaveFileDialog saveFileDialog, string fileName, byte[] template)
		{
			saveFileDialog.FileName = fileName;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllBytes(saveFileDialog.FileName, template);
			}
		}

		private static void SaveTemplate(SaveFileDialog saveFileDialog, byte[] template)
		{
			SaveTemplate(saveFileDialog, null, template);
		}

		#endregion

		#region Private fields

		private readonly Dictionary<object, RecordInfo> _infoLookup = new Dictionary<object, RecordInfo>();

		private NTemplate _template;
		private object _selectedRecord;
		private string _fileName;

		#endregion

		#region Public constructor

		public MainForm()
		{
			InitializeComponent();

			imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);

			aboutToolStripMenuItem.Text = '&' + AboutBox.Name;

			OnSelectedItemChanged();
			NewTemplate();
		}

		#endregion

		#region Private methods

		private RecordInfo GetRecordInfo(object record)
		{
			RecordInfo info;

			if (record == null)
			{
				// dummy record info in order to avoid special cases everywhere
				return new RecordInfo();
			}

			if (_infoLookup.TryGetValue(record, out info))
			{
				return info;
			}

			info = new RecordInfo();
			_infoLookup.Add(record, info);
			return info;
		}

		private void RefreshTemplateView()
		{
			treeView.BeginUpdate();
			treeView.Nodes.Clear();
			TreeNode selectedNode = null;
			if (_template != null)
			{
				string rootText = !string.IsNullOrEmpty(_fileName) ? Path.GetFileNameWithoutExtension(_fileName) : "NTemplate";
				TreeNode templateNode = new TreeNode(rootText + GetRecordInfo(_template.Fingers).Text);
				templateNode.Tag = _template;
				if (_template.Fingers != null)
				{
					TreeNode fingersNode = templateNode.Nodes.Add("Fingers" + GetRecordInfo(_template.Fingers).Text);
					fingersNode.Tag = _template.Fingers;
					if (_selectedRecord == _template.Fingers) selectedNode = fingersNode;
					for (int i = 0; i < _template.Fingers.Records.Count; i++)
					{
						NFRecord rec = _template.Fingers.Records[i];
						TreeNode fingerNode = fingersNode.Nodes.Add("Finger" + i + GetRecordInfo(rec).Text);
						fingerNode.Tag = rec;
						if (_selectedRecord == rec) selectedNode = fingerNode;
					}
				}
				if (_template.Faces != null)
				{
					TreeNode facesNode = templateNode.Nodes.Add("Faces" + GetRecordInfo(_template.Faces).Text);
					facesNode.Tag = _template.Faces;
					if (_selectedRecord == _template.Faces) selectedNode = facesNode;
					for (int i = 0; i < _template.Faces.Records.Count; i++)
					{
						NLRecord rec = _template.Faces.Records[i];
						TreeNode faceNode = facesNode.Nodes.Add("Face" + i + GetRecordInfo(rec).Text);
						faceNode.Tag = rec;
						if (_selectedRecord == rec) selectedNode = faceNode;
					}
				}
				if (_template.Irises != null)
				{
					TreeNode irisesNode = templateNode.Nodes.Add("Irises" + GetRecordInfo(_template.Irises).Text);
					irisesNode.Tag = _template.Irises;
					if (_selectedRecord == _template.Irises) selectedNode = irisesNode;
					for (int i = 0; i < _template.Irises.Records.Count; i++)
					{
						NERecord rec = _template.Irises.Records[i];
						TreeNode irisNode = irisesNode.Nodes.Add("Iris" + i + GetRecordInfo(rec).Text);
						irisNode.Tag = rec;
						if (_selectedRecord == rec) selectedNode = irisNode;
					}
				}
				if (_template.Palms != null)
				{
					TreeNode palmsNode = templateNode.Nodes.Add("Palms" + GetRecordInfo(_template.Palms).Text);
					palmsNode.Tag = _template.Palms;
					if (_selectedRecord == _template.Palms) selectedNode = palmsNode;
					for (int i = 0; i < _template.Palms.Records.Count; i++)
					{
						NFRecord rec = _template.Palms.Records[i];
						TreeNode palmNode = palmsNode.Nodes.Add("Palm" + i + GetRecordInfo(rec).Text);
						palmNode.Tag = rec;
						if (_selectedRecord == rec) selectedNode = palmNode;
					}
				}
				if (_template.Voices != null)
				{
					TreeNode voicesNode = templateNode.Nodes.Add("Voices" + GetRecordInfo(_template.Voices).Text);
					voicesNode.Tag = _template.Palms;
					if (_selectedRecord == _template.Voices) selectedNode = voicesNode;
					for (int i = 0; i < _template.Voices.Records.Count; i++)
					{
						NSRecord rec = _template.Voices.Records[i];
						TreeNode voiceNode = voicesNode.Nodes.Add("Voice" + i + GetRecordInfo(rec).Text);
						voiceNode.Tag = rec;
						if (_selectedRecord == rec) selectedNode = voiceNode;
					}
				}
				treeView.Nodes.Add(templateNode);
				templateNode.ExpandAll();
				if (selectedNode == null)
				{
					selectedNode = templateNode;
				}
				treeView.SelectedNode = selectedNode;
			}
			treeView.EndUpdate();
		}

		private void ClearData()
		{
			_template = new NTemplate();
			_selectedRecord = null;
			_infoLookup.Clear();
			_fileName = null;
		}

		private void NewTemplate()
		{
			ClearData();
			RefreshTemplateView();
		}

		private void OpenTemplate()
		{
			try
			{
				byte[] templ = LoadTemplate(nTemplateOpenFileDialog, out _fileName);
				_selectedRecord = null;
				_template = null;
				if (templ == null)
				{
					return;
				}
				try
				{
					_template = new NTemplate(templ);
				}
				catch (FormatException)
				{
					NFRecord record = new NFRecord(templ);
					_template = new NTemplate(record.Save());
				}
				RefreshTemplateView();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to open specified template. Reason: " + ex.Message, "NTemplate Sample", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SaveTemplate()
		{
			try
			{
				SaveTemplate(nTemplateSaveFileDialog, _fileName, _template.Save().ToArray());
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to save specified template. Reason: " + ex.Message, "NTemplate Sample", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void OnSelectedItemChanged()
		{
			NSubject subject = new NSubject();
			NTemplate template = new NTemplate();
			object record = null;
			if (treeView.SelectedNode != null)
			{
				record = treeView.SelectedNode.Tag;
			}

			RecordInfo recordInfo = GetRecordInfo(record);
			bool isRoot = treeView.SelectedNode != null && treeView.SelectedNode == treeView.Nodes[0];
			if (record is NFRecord)
			{
				var fingerOrPalmRecord = (NFRecord)record;
				if (NBiometricTypes.IsPositionFinger(fingerOrPalmRecord.Position))
				{
					NFTemplate nfTemplate = new NFTemplate();
					template.Fingers = nfTemplate;
					template.Fingers.Records.Add(fingerOrPalmRecord);
					subject.SetTemplate(template);
					fingerView.Finger = subject.Fingers[0];
					fingerView.Visible = true;
				}
				else
				{
					NFTemplate nfTemplate = new NFTemplate(true);
					template.Palms = nfTemplate;
					template.Palms.Records.Add(fingerOrPalmRecord);
					subject.SetTemplate(template);
					fingerView.Finger = subject.Palms[0];
					fingerView.Visible = true;
				}
			}
			else
			{
				fingerView.Visible = false;
				fingerView.Finger = null;
			}
			nViewZoomSlider.Visible = fingerView.Visible;

			propertyGrid.SelectedObject = record;

			editToolStripSeparator4.Visible = removeToolStripMenuItem.Visible = editToolStripSeparator5.Visible = saveItemToolStripMenuItem.Visible = !isRoot;
		}

		#endregion

		#region Private form events

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
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveTemplate();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			OnSelectedItemChanged();
		}

		private void addFingersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_template.Fingers = new NFTemplate();
			RefreshTemplateView();
		}

		private void addFingersFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(nfTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Fingers = new NFTemplate(packedData);
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addFacesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_template.Faces = new NLTemplate();
			RefreshTemplateView();
		}

		private void addFacesFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(nlTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Faces = new NLTemplate(packedData);
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addIrisesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_template.Irises = new NETemplate();
			RefreshTemplateView();
		}

		private void addIrisesFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(neTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Irises = new NETemplate(packedData);
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addPalmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_template.Palms = new NFTemplate(true);
			RefreshTemplateView();
		}

		private void addVoicesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_template.Voices = new NSTemplate();
			RefreshTemplateView();
		}

		private void addVoicesFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(nsTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Voices = new NSTemplate(packedData);
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addPalmsFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(nfTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Palms = new NFTemplate(packedData);
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addFingerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewNFRecordForm newNFRec = new NewNFRecordForm();
			if (newNFRec.ShowDialog() == DialogResult.OK)
			{
				_template.Fingers.Records.Add(new NFRecord(newNFRec.RecordWidth, newNFRec.RecordHeight, newNFRec.HorzResolution, newNFRec.VertResolution));
				RefreshTemplateView();
			}
		}

		private void addFaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_template.Faces.Records.Add(new NLRecord());
			RefreshTemplateView();
		}

		private void addIrisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewNERecordForm newNERec = new NewNERecordForm();
			if (newNERec.ShowDialog() == DialogResult.OK)
			{
				_template.Irises.Records.Add(new NERecord(newNERec.RecordWidth, newNERec.RecordHeight));
				RefreshTemplateView();
			}
		}

		private void addVoiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_template.Voices.Records.Add(new NSRecord());
			RefreshTemplateView();
		}

		private void addPalmToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewNFRecordForm newNFRec = new NewNFRecordForm();
			newNFRec.RecordWidth = 1000;
			newNFRec.RecordHeight = 1000;
			if (newNFRec.ShowDialog() == DialogResult.OK)
			{
				_template.Palms.Records.Add(new NFRecord(true, newNFRec.RecordWidth, newNFRec.RecordHeight, newNFRec.HorzResolution, newNFRec.VertResolution));
				RefreshTemplateView();
			}
		}

		private void addFingerFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(nfTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Fingers.Records.Add(new NFRecord(packedData));
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addFaceFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(nfTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Faces.Records.Add(new NLRecord(packedData));
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addIrisFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(neTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Irises.Records.Add(new NERecord(packedData));
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addVoiceFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(nsTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Voices.Records.Add(new NSRecord(packedData));
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void addPalmFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName;
			byte[] packedData = LoadTemplate(nfTemplateOpenFileDialog, out fileName);
			if (packedData != null)
			{
				try
				{
					_template.Palms.Records.Add(new NFRecord(packedData));
					RefreshTemplateView();
				}
				catch (FormatException ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeView.SelectedNode == null
				|| treeView.SelectedNode.Tag == null)
			{
				return;
			}

			object record = treeView.SelectedNode.Tag;
			Type recordType = record.GetType();

			if (recordType == typeof(NFTemplate))
			{
				if (((NFTemplate)record).IsPalm)
				{
					_template.Palms = null;
				}
				else
				{
					_template.Fingers = null;
				}
			}
			else if (recordType == typeof(NLTemplate))
			{
				_template.Faces = null;
			}
			else if (recordType == typeof(NETemplate))
			{
				_template.Irises = null;
			}
			else if (recordType == typeof(NFRecord))
			{
				if (((NFRecord)record).ImpressionType >= NFImpressionType.LiveScanPalm)
				{
					_template.Palms.Records.Remove((NFRecord)record);
				}
				else
				{
					_template.Fingers.Records.Remove((NFRecord)record);
				}
			}
			else if (recordType == typeof(NLRecord))
			{
				_template.Faces.Records.Remove((NLRecord)record);
			}
			else if (recordType == typeof(NERecord))
			{
				_template.Irises.Records.Remove((NERecord)record);
			}
			else
			{
				throw new NotSupportedException();
			}

			RecordInfo recordInfo = GetRecordInfo(record);
			IDisposable disposable = recordInfo.SourceData as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
				disposable = null;
			}
			_infoLookup.Remove(record);

			_selectedRecord = null;
			
			RefreshTemplateView();
		}

		private void saveItemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			object record = treeView.SelectedNode.Tag;
			if (record == null)
			{
				record = _template;
			}
			Type recordType = record.GetType();

			if (recordType == typeof(NTemplate))
			{
				byte[] packedTemplate = ((NTemplate)record).Save().ToArray();
				SaveTemplate(nTemplateSaveFileDialog, packedTemplate);
			}
			else if (recordType == typeof(NFTemplate))
			{
				byte[] packedTemplate = ((NFTemplate)record).Save().ToArray();
				SaveTemplate(nfTemplateSaveFileDialog, packedTemplate);
			}
			else if (recordType == typeof(NLTemplate))
			{
				byte[] packedTemplate = ((NLTemplate)record).Save().ToArray();
				SaveTemplate(nlTemplateSaveFileDialog, packedTemplate);
			}
			else if (recordType == typeof(NETemplate))
			{
				byte[] packedTemplate = ((NETemplate)record).Save().ToArray();
				SaveTemplate(neTemplateSaveFileDialog, packedTemplate);
			}
			else if (recordType == typeof(NSTemplate))
			{
				byte[] packedTemplate = ((NSTemplate)record).Save().ToArray();
				SaveTemplate(nsTemplateSaveFileDialog, packedTemplate);
			}
			else if (recordType == typeof(NFRecord))
			{
				byte[] packedTemplate = ((NFRecord)record).Save().ToArray();
				SaveTemplate(nfRecordSaveFileDialog, packedTemplate);
			}
			else if (recordType == typeof(NLRecord))
			{
				byte[] packedTemplate = ((NLRecord)record).Save().ToArray();
				SaveTemplate(nlRecordSaveFileDialog, packedTemplate);
			}
			else if (recordType == typeof(NERecord))
			{
				byte[] packedTemplate = ((NERecord)record).Save().ToArray();
				SaveTemplate(neRecordSaveFileDialog, packedTemplate);
			}
			else if (recordType == typeof(NSRecord))
			{
				byte[] packedTemplate = ((NSRecord)record).Save().ToArray();
				SaveTemplate(nsRecordSaveFileDialog, packedTemplate);
			}
			else
			{
				throw new NotSupportedException();
			}
		}
		
		private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			addFingersToolStripMenuItem.Enabled = addFingersFromFileToolStripMenuItem.Enabled = (_template != null && _template.Fingers == null);
			addFacesToolStripMenuItem.Enabled = addFacesFromFileToolStripMenuItem.Enabled = (_template != null && _template.Faces == null);
			addIrisesToolStripMenuItem.Enabled = addIrisesFromFileToolStripMenuItem.Enabled = (_template != null && _template.Irises == null);
			addPalmsToolStripMenuItem.Enabled = addPalmsFromFileToolStripMenuItem.Enabled = (_template != null && _template.Palms == null);
			addVoicesToolStripMenuItem.Enabled = addVoicesFromFileToolStripMenuItem.Enabled = (_template != null && _template.Voices == null);

			addFingerFromFileToolStripMenuItem.Enabled = addFingerToolStripMenuItem.Enabled = (_template != null && _template.Fingers != null);
			addFaceFromFileToolStripMenuItem.Enabled = addFaceToolStripMenuItem.Enabled = (_template != null && _template.Faces != null);
			addIrisFromFileToolStripMenuItem.Enabled = addIrisToolStripMenuItem.Enabled = (_template != null && _template.Irises != null);
			addPalmFromFileToolStripMenuItem.Enabled = addPalmToolStripMenuItem.Enabled = (_template != null && _template.Palms != null);
			addVoiceFromFileToolStripMenuItem.Enabled = addVoiceToolStripMenuItem.Enabled = (_template != null && _template.Voices != null);
		}

		#endregion
	}

	internal class RecordInfo : IDisposable
	{
		#region Private fields

		private bool _isDisposed = false;

		#endregion

		#region Public fields

		public string Text = string.Empty;
		public object SourceData;

		#endregion

		#region Public constructors

		public RecordInfo()
		{
		}

		#endregion

		#region Destructor

		~RecordInfo()
		{
			Dispose(false);
		}

		#endregion

		#region Private methods

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				IDisposable sd = SourceData as IDisposable;
				if (sd != null)
				{
					sd.Dispose();
				}
			}
			SourceData = null;
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (!_isDisposed)
			{
				Dispose(true);
				_isDisposed = true;
				GC.SuppressFinalize(this);
			}
		}

		#endregion
	}
}
