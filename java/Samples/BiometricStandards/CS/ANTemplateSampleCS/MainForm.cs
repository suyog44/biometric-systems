using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Standards;
using Neurotec.Gui;
using Neurotec.Images;
using Neurotec.Samples.Properties;
using Neurotec.Samples.RecordCreateForms;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Private constants

		private const string ApplicationName = "ANSI/NIST File Editor";
		private const string TemplateFilterName = "ANSI/NIST Files";
		private const string TemplateFilter = "*.an;*.an2;*.eft;*.lff;*.lffs;*.int;*.nist;*.fiif";
		private const string TemplateFilterString = TemplateFilterName + " (" + TemplateFilter + ")|" + TemplateFilter;
		private const string TemplateDefaultExt = "an2";
		private const string AllFilesFilterString = "All Files (*.*)|*.*";
		private const string TemplateOpenFileFilterString = TemplateFilterString + "|" + AllFilesFilterString;
		private const string TemplateSaveFileFilterString = TemplateFilterString;

		#endregion

		#region Private fields

		private ANTemplate _template;
		private string _fileName;
		private bool _isModified;
		private int _templateIndex;
		private string _name;

		#endregion

		#region Public constructor

		public MainForm()
		{
			InitializeComponent();

			openFileDialog.Filter = TemplateOpenFileFilterString;
			saveFileDialog.Filter = TemplateSaveFileFilterString;
			saveFileDialog.DefaultExt = TemplateDefaultExt;
			folderBrowserDialog.Description = string.Format("Files satisfying \"{0}\" filter will be validated in selected folder and its subfolders", TemplateFilter);

			imageSaveFileDialog.Filter = NImages.GetSaveFileFilterString();

			aboutToolStripMenuItem.Text = '&' + AboutBox.Name;

			OnRecordsChanged();
			UpdateTitle();
		}

		#endregion

		#region Private form events

		private void EditableRecordChanged(object sender, EventArgs e)
		{
			OnSelectedRecordModified();
		}

		private void RemoveRecordsToolStripMenuItemClick(object sender, EventArgs e)
		{
			int selCount = recordListView.SelectedIndices.Count;
			if (selCount > 0)
			{
				if (MessageBox.Show("Are you sure you want to remove selected records?", "Remove records?",
									 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					recordListView.BeginUpdate();
					int[] selIndices = new int[selCount];
					recordListView.SelectedIndices.CopyTo(selIndices, 0);
					Array.Sort<int>(selIndices);
					for (int i = selCount - 1; i >= 0; i--)
					{
						int index = selIndices[i];
						_template.Records.RemoveAt(index);
						recordListView.Items.RemoveAt(index);
					}
					recordListView.Items[selIndices[0] == _template.Records.Count ? _template.Records.Count - 1 : selIndices[0]].Selected = true;
					recordListView.EndUpdate();
					OnTemplateModified();
				}
			}
		}

		private void ClearRecordsToolStripMenuItemClick(object sender, EventArgs e)
		{
			recordListView.BeginUpdate();
			_template.Records.Clear();
			for (int i = recordListView.Items.Count - 1; i > 0; i--)
			{
				recordListView.Items.RemoveAt(i);
			}
			if (fieldListView.SelectedIndices.Count != 0) OnSelectedRecordChanged(); // The first record is selected
			else recordListView.Items[0].Selected = true;
			recordListView.EndUpdate();
			OnTemplateModified();
		}

		private void AddFieldToolStripMenuItemClick(object sender, EventArgs e)
		{
			NVersion version = _template.Version;
			ANRecord selectedRecord = GetSelectedRecord();
			ANRecordType selectedRecordType = selectedRecord.RecordType;
			using (FieldNumberForm form = new FieldNumberForm())
			{
				form.Text = "Add field";
				form.Version = version;
				form.RecordType = selectedRecordType;
				form.ValidationLevel = GetRecordValidationLevel(selectedRecord.IsValidated);
				if (form.ShowDialog() != DialogResult.OK) return;
				tabControl1.SelectedIndex = 1;

				int fieldNumber = form.FieldNumber;
				if (selectedRecord.Fields.Contains(fieldNumber))
				{
					MessageBox.Show("The record already contains a field with the same number", ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					int fieldIndex;
					ANField field = selectedRecord.Fields.Add(fieldNumber, string.Empty, out fieldIndex);
					StringBuilder value = new StringBuilder();
					ListViewItem item = CreateFieldListViewItem(selectedRecord, field, value);
					fieldListView.Items.Insert(fieldIndex, item);
					fieldListView.SelectedIndices.Clear();
					item.Selected = true;
					OnSelectedRecordModified();
					EditField();
				}
			}
		}

		private void EditFieldToolStripMenuItemClick(object sender, EventArgs e)
		{
			EditField();
		}

		private void RemoveFieldToolStripMenuItemClick(object sender, EventArgs e)
		{
			fieldListView.BeginUpdate();
			try
			{
				ANRecord selectedRecord = GetSelectedRecord();
				int selCount = fieldListView.SelectedIndices.Count;
				int[] selIndices = new int[selCount];
				fieldListView.SelectedIndices.CopyTo(selIndices, 0);
				Array.Sort<int>(selIndices);
				for (int i = selCount - 1; i >= 0; i--)
				{
					int index = selIndices[i];
					selectedRecord.Fields.RemoveAt(index);
					fieldListView.Items.RemoveAt(index);
				}
				fieldListView.Items[selIndices[0] == selectedRecord.Fields.Count ? selectedRecord.Fields.Count - 1 : selIndices[0]].Selected = true;
			}
			finally
			{
				fieldListView.EndUpdate();
			}
			OnSelectedRecordModified();
		}

		private void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			AboutBox.Show();
		}

		private void SaveRecordDataToolStripMenuItemClick(object sender, EventArgs e)
		{
			ANRecord selectedRecord = GetSelectedRecord();
			string ext = null;
			if (selectedRecord.IsValidated)
			{
				if (selectedRecord.RecordType.Number == 8)
				{
					ANType8Record type8Record = (ANType8Record)selectedRecord;
					ext = type8Record.SignatureRepresentationType == ANSignatureRepresentationType.ScannedUncompressed ? "raw" : null;
				}
				else if (selectedRecord.RecordType.Number == 5 || selectedRecord.RecordType.Number == 6)
				{
					ANBinaryImageCompressionAlgorithm ca = selectedRecord.RecordType.Number == 5 ? ((ANType5Record)selectedRecord).CompressionAlgorithm :
						/* selectedRecord.Type.Number == 6 ? */ ((ANType6Record)selectedRecord).CompressionAlgorithm;
					ext = ca == ANBinaryImageCompressionAlgorithm.None ? "raw" : null;
				}
				else if (selectedRecord.RecordType.Number == 3 || selectedRecord.RecordType.Number == 4 || selectedRecord is ANImageAsciiBinaryRecord)
				{
					ANImageCompressionAlgorithm ca = selectedRecord.RecordType.Number == 3 ? ((ANType3Record)selectedRecord).CompressionAlgorithm :
						selectedRecord.RecordType.Number == 4 ? ((ANType4Record)selectedRecord).CompressionAlgorithm :
						((ANImageAsciiBinaryRecord)selectedRecord).CompressionAlgorithm;
					ext = ca == ANImageCompressionAlgorithm.None ? "raw" :
						ca == ANImageCompressionAlgorithm.Wsq20 ? "wsq" :
						ca == ANImageCompressionAlgorithm.JpegB || ca == ANImageCompressionAlgorithm.JpegL ? "jpg" :
						ca == ANImageCompressionAlgorithm.JP2 || ca == ANImageCompressionAlgorithm.JP2L ? "jp2" :
						ca == ANImageCompressionAlgorithm.Png ? "png" :
						null;
				}
			}
			if (ext == null) ext = "dat";
			recordDataSaveFileDialog.FileName = "data." + ext;
			if (recordDataSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (Stream stream = File.Create(recordDataSaveFileDialog.FileName))
					{
						selectedRecord.Data.WriteTo(stream);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.ToString(), "Can not save record data", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void SaveImageToolStripMenuItemClick(object sender, EventArgs e)
		{
			ANRecord selectedRecord = GetSelectedRecord();
			NImageFormat imageFormat = null;
			if (selectedRecord.RecordType.Number == 8)
			{
				ANType8Record type8Record = (ANType8Record)selectedRecord;
				imageFormat = type8Record.SignatureRepresentationType == ANSignatureRepresentationType.ScannedUncompressed ? NImageFormat.Tiff : null;
			}
			else if (selectedRecord.RecordType.Number == 5 || selectedRecord.RecordType.Number == 6)
			{
				ANBinaryImageCompressionAlgorithm ca = selectedRecord.RecordType.Number == 5 ? ((ANType5Record)selectedRecord).CompressionAlgorithm :
					/* selectedRecord.Type.Number == 6 ? */ ((ANType6Record)selectedRecord).CompressionAlgorithm;
				imageFormat = ca == ANBinaryImageCompressionAlgorithm.None ? NImageFormat.Tiff : null;
			}
			else if (selectedRecord.RecordType.Number == 3 || selectedRecord.RecordType.Number == 4 || selectedRecord is ANImageAsciiBinaryRecord)
			{
				ANImageCompressionAlgorithm ca = selectedRecord.RecordType.Number == 3 ? ((ANType3Record)selectedRecord).CompressionAlgorithm :
					selectedRecord.RecordType.Number == 4 ? ((ANType4Record)selectedRecord).CompressionAlgorithm :
					((ANImageAsciiBinaryRecord)selectedRecord).CompressionAlgorithm;
				imageFormat = ca == ANImageCompressionAlgorithm.None ? NImageFormat.Tiff :
					ca == ANImageCompressionAlgorithm.Wsq20 ? NImageFormat.Wsq :
					ca == ANImageCompressionAlgorithm.JpegB || ca == ANImageCompressionAlgorithm.JpegL ? NImageFormat.Jpeg :
					ca == ANImageCompressionAlgorithm.JP2 || ca == ANImageCompressionAlgorithm.JP2L ? NImageFormat.Jpeg2K :
					ca == ANImageCompressionAlgorithm.Png ? NImageFormat.Png :
					null;
			}
			if (imageFormat == null) imageFormat = NImageFormat.Tiff;
			imageSaveFileDialog.FileName = null;
			imageSaveFileDialog.FilterIndex = NImageFormat.Formats.IndexOf(imageFormat) + 1;
			if (imageSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				ANImageBinaryRecord imageBinaryRecord = selectedRecord as ANImageBinaryRecord;
				try
				{
					if (imageBinaryRecord != null)
					{
						using (NImage image = imageBinaryRecord.ToNImage())
						{
							image.Save(imageSaveFileDialog.FileName);
						}
					}
					else
					{
						ANImageAsciiBinaryRecord imageAsciiBinaryRecord = (ANImageAsciiBinaryRecord)selectedRecord;
						using (NImage image = imageAsciiBinaryRecord.ToNImage())
						{
							image.Save(imageSaveFileDialog.FileName);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.ToString(), "Can not save image", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void SaveAsNFRecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			ANType9Record type9Record = (ANType9Record)GetSelectedRecord();
			nfRecordSaveFileDialog.FileName = null;
			if (nfRecordSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (NFRecord nfRecord = type9Record.ToNFRecord())
					{
						File.WriteAllBytes(nfRecordSaveFileDialog.FileName, nfRecord.Save().ToArray());
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.ToString(), "Can not save as NFRecord", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void SaveAsNTemplateToolStripMenuItemClick(object sender, EventArgs e)
		{
			nTemplateSaveFileDialog.FileName = null;
			if (nTemplateSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (NTemplate nTemplate = _template.ToNTemplate())
					{
						File.WriteAllBytes(nTemplateSaveFileDialog.FileName, nTemplate.Save().ToArray());
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.ToString(), "Can not save as NTemplate", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void MainFormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !FileSavePrompt();
		}

		private void MainFormClosed(object sender, FormClosedEventArgs e)
		{
			Settings settings = Settings.Default;
			settings.Save();
		}

		private void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			FileNew();
		}

		private void OpenToolStripMenuItemClick(object sender, EventArgs e)
		{
			FileOpen();
		}

		private void CloseToolStripMenuItemClick(object sender, EventArgs e)
		{
			FileClose();
		}

		private void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			FileSave();
		}

		private void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
		{
			FileSaveAs();
		}

		private void ChangeVersionToolStripMenuItemClick(object sender, EventArgs e)
		{
			VersionForm form = new VersionForm();
			form.Text = "Select Version";
			form.SelectedVersion = _template.Version;
			if (form.ShowDialog() == DialogResult.OK && form.SelectedVersion != _template.Version)
			{
				if (MessageBox.Show("Some information may be lost.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
				{
					try
					{
						_template.Version = form.SelectedVersion;
						OnSelectedRecordChanged();
						if (!_isModified) OnTemplateModified();
						else UpdateTitle();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), "Can not change ANSI/NIST file version", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}

		private void VersionsToolStripMenuItemClick(object sender, EventArgs e)
		{
			VersionForm form = new VersionForm();
			form.UseSelectMode = false;
			form.Text = "Versions";
			form.ShowDialog();
		}

		private void RecordTypesToolStripMenuItemClick(object sender, EventArgs e)
		{
			RecordTypeForm form = new RecordTypeForm();
			form.UseSelectMode = false;
			form.Text = "Record Types";
			form.ShowDialog();
		}

		private void CharsetsToolStripMenuItemClick(object sender, EventArgs e)
		{
			CharsetForm form = new CharsetForm();
			form.UseSelectMode = false;
			form.Text = "Charsets";
			form.ShowDialog();
		}

		private void RecordListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedRecordChanged();
		}

		private void ValidateToolStripMenuItemClick(object sender, EventArgs e)
		{
			Settings settings = Settings.Default;
			folderBrowserDialog.SelectedPath = settings.LastValidateDirectory;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				settings.LastValidateDirectory = folderBrowserDialog.SelectedPath;
				settings.Save();
				OptionsForm options = new OptionsForm();
				if (options.ShowDialog() != DialogResult.OK) return;
				ValidateForm form = new ValidateForm();
				form.Path = folderBrowserDialog.SelectedPath;
				form.Filter = TemplateFilter;
				form.ValidationLevel = options.ValidationLevel;
				form.Flags = options.Flags;
				form.ShowDialog();
			}
		}

		private void FieldListViewDoubleClick(object sender, EventArgs e)
		{
			if (GetSelectedField() != null)
			{
				EditField();
			}
		}

		private void FieldListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedFieldChanged();
		}

		#region Add record toolstrip events
		private void AddType2RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			try
			{
				using (ANRecordCreateForm idcForm = new ANRecordCreateForm())
				{
					if (idcForm.ShowDialog() == DialogResult.OK)
					{
						recordListView.SelectedIndices.Clear();
						ANType2Record record = new ANType2Record(ANTemplate.VersionCurrent, idcForm.Idc);
						_template.Records.Add(record);
						AddRecordListViewItem(record).Selected = true;
						OnTemplateModified();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), "Can't add record", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void AddType3RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType3RecordCreateForm());
		}

		private void AddType4RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType4RecordCreateForm());
		}

		private void AddType5RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType5RecordCreateForm());
		}

		private void AddType6RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType6RecordCreateForm());
		}

		private void AddType7RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType7RecordCreateForm());
		}

		private void AddType8RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType8RecordCreateForm());
		}

		private void AddType9RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType9RecordCreateForm());
		}

		private void AddType10RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType10RecordCreateForm());
		}

		private void AddType13RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType13RecordCreateForm());
		}

		private void AddType14RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType14RecordCreateForm());
		}

		private void AddType15RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType15RecordCreateForm());
		}

		private void AddType16RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType16RecordCreateForm());
		}

		private void AddType17RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType17RecordCreateForm());
		}

		private void AddType99RecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddValidatedRecord(new ANType99RecordCreateForm());
		}

		private void AddRecordToolStripMenuItemClick(object sender, EventArgs e)
		{
			RecordTypeForm form = new RecordTypeForm();
			if (form.ShowDialog() == DialogResult.OK)
			{
				ANRecordCreateForm idcForm = new ANRecordCreateForm();
				if (idcForm.ShowDialog() == DialogResult.OK)
				{
					recordListView.SelectedIndices.Clear();
					ANRecord record = new ANRecord(form.RecordType, ANTemplate.VersionCurrent, idcForm.Idc);
					_template.Records.Add(record);
					AddRecordListViewItem(record).Selected = true;
					OnTemplateModified();
				}
			}
		}
		#endregion

		#endregion

		#region Private methods

		private void OnTemplateModified()
		{
			if (!_isModified)
			{
				_isModified = true;
				UpdateTitle();
			}
		}

		private void OnSelectedFieldChanged()
		{
			ANField selectedField = GetSelectedField();
			editFieldToolStripButton.Enabled = editFieldToolStripMenuItem.Enabled = selectedField != null;
			bool canRemove = fieldListView.SelectedItems.Count != 0;
			NVersion version = _template == null ? ANTemplate.VersionCurrent : _template.Version;
			ANRecord selectedRecord = GetSelectedRecord();
			ANRecordType selectedRecordType = selectedRecord == null ? null : selectedRecord.RecordType;
			ANValidationLevel validationLevel = GetRecordValidationLevel(selectedRecord == null || selectedRecord.IsValidated);
			foreach (ListViewItem listViewItem in fieldListView.SelectedItems)
			{
				ANField field = (ANField)listViewItem.Tag;
				int fieldNumber = field.Number;
				if (FieldNumberForm.IsFieldStandard(selectedRecordType, version, fieldNumber, validationLevel))
				{
					canRemove = false;
					break;
				}
			}
			removeFieldToolStripButton.Enabled = removeFieldToolStripMenuItem.Enabled = canRemove;
		}

		private void OnSelectedRecordModified()
		{
			if (!highLevelPropertyGrid.ContainsFocus)
			{
				object selectedObject = highLevelPropertyGrid.SelectedObject;
				highLevelPropertyGrid.SelectedObject = null;
				highLevelPropertyGrid.SelectedObject = selectedObject;
			}
			UpdateFieldListViewItem(fieldListView.Items[0]);

			anRecordView.Invalidate();
			OnTemplateModified();
		}

		private void OnRecordsChanged()
		{
			recordListView.BeginUpdate();
			recordListView.Items.Clear();
			if (_template != null)
			{
				foreach (ANRecord record in _template.Records)
				{
					AddRecordListViewItem(record);
				}
				recordListView.Items[0].Selected = true;
			}
			else
			{
				OnSelectedRecordChanged();
			}
			recordListView.EndUpdate();
			closeToolStripMenuItem.Enabled = saveToolStripMenuItem.Enabled = saveAsToolStripMenuItem.Enabled = saveAsNTemplateToolStripMenuItem.Enabled = changeVersionToolStripMenuItem.Enabled
				= saveToolStripButton.Enabled = addToolStripMenuItem.Enabled = clearRecordsToolStripMenuItem.Enabled = addRecordToolStripDropDownButton.Enabled = _template != null;
		}

		private void OnSelectedRecordChanged()
		{
			ANRecord selectedRecord = GetSelectedRecord();

			if (selectedRecord == null)
			{
				noHighLevelPropertiesLabel.Text = "No record is selected";
				noHighLevelPropertiesLabel.Visible = true;
				highLevelPropertyGrid.Visible = false;
			}
			else if (selectedRecord.ValidationLevel == ANValidationLevel.Minimal)
			{
				noHighLevelPropertiesLabel.Text = "Selected records validation level is minimal";
				noHighLevelPropertiesLabel.Visible = true;
				highLevelPropertyGrid.Visible = false;
			}
			else
			{
				highLevelPropertyGrid.SelectedObject = selectedRecord;
				noHighLevelPropertiesLabel.Visible = false;
				highLevelPropertyGrid.Visible = true;
			}

			fieldListView.BeginUpdate();
			fieldListView.Items.Clear();
			imageErrorToolTip.RemoveAll();
			anRecordView.Record = selectedRecord;
			if (selectedRecord != null)
			{
				StringBuilder value = new StringBuilder();
				foreach (ANField field in selectedRecord.Fields)
				{
					fieldListView.Items.Add(CreateFieldListViewItem(selectedRecord, field, value));
				}
				fieldListView.Items[0].Selected = true;
			}
			fieldListView.EndUpdate();

			ListView.SelectedIndexCollection selected = recordListView.SelectedIndices;
			removeRecordToolStripButton.Enabled = removeRecordsToolStripMenuItem.Enabled = selected.Count > 0 && !selected.Contains(0);
			saveRecordDataToolStripMenuItem.Enabled = selectedRecord != null &&
				(selectedRecord.RecordType.DataType == ANRecordDataType.Binary ||
				 selectedRecord.RecordType.DataType == ANRecordDataType.AsciiBinary);
			saveImageToolStripMenuItem.Enabled = selectedRecord != null
				&& selectedRecord.IsValidated
				&& ((selectedRecord is ANImageBinaryRecord
					&& (selectedRecord.RecordType.Number != 8 || ((ANType8Record)selectedRecord).SignatureRepresentationType != ANSignatureRepresentationType.VectorData))
				|| selectedRecord is ANImageAsciiBinaryRecord);
			saveAsNFRecordToolStripMenuItem.Enabled = selectedRecord != null &&
				selectedRecord.IsValidated &&
				selectedRecord.RecordType.Number == 9 &&
				((ANType9Record)selectedRecord).HasMinutiae;
			addFieldToolStripButton.Enabled = addFieldToolStripMenuItem.Enabled = selectedRecord != null && selectedRecord.RecordType.DataType != ANRecordDataType.Binary;
			OnSelectedFieldChanged();
		}

		private ListViewItem AddRecordListViewItem(ANRecord record)
		{
			ListViewItem recordItem = new ListViewItem(string.Format("Type-{0}{1}", record.RecordType.Number, record.IsValidated ? null : "*"));
			recordItem.Tag = record;
			recordItem.SubItems.Add(record.RecordType.Name);
			if (record.RecordType != ANRecordType.Type1)
			{
				recordItem.SubItems.Add(record.Idc.ToString());
			}
			recordListView.Items.Add(recordItem);
			return recordItem;
		}

		private ANRecord GetSelectedRecord()
		{
			return recordListView.SelectedItems.Count == 1 ? (ANRecord)recordListView.SelectedItems[0].Tag : null;
		}

		private ANValidationLevel GetRecordValidationLevel(bool recordIsValidated)
		{
			return recordIsValidated && _template != null ? _template.ValidationLevel : ANValidationLevel.Minimal;
		}

		private ListViewItem CreateFieldListViewItem(ANRecord record, ANField field, StringBuilder value)
		{
			NVersion version = _template.Version;
			ANRecordType recordType = record.RecordType;
			int fieldNumber = field.Number;
			ListViewItem item = new ListViewItem(field.Header);
			item.Tag = field;
			bool isFieldStandard = FieldNumberForm.IsFieldStandard(recordType, version, fieldNumber, GetRecordValidationLevel(record.IsValidated));
			if (isFieldStandard) item.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
			bool isKnown = recordType.IsFieldKnown(version, fieldNumber);
			string id = isKnown ? recordType.GetFieldId(version, fieldNumber) : "UNK";
			string name = isKnown ? recordType.GetFieldName(version, fieldNumber) : "Unknown field";
			if (id != string.Empty) name = string.Format("{0} ({1})", name, id);
			item.SubItems.Add(name);
			FieldForm.GetFieldValue(field, value);
			if (isFieldStandard)
			{
				item.SubItems.Add(value.ToString());
			}
			else
			{
				ANType1Record type1Record = (ANType1Record)record.Owner.Records[0];
				if (type1Record.Charsets.Contains(ANType1Record.CharsetUtf8))
				{
					byte[] valueBytes = Encoding.GetEncoding(1252).GetBytes(value.ToString());
					string utf8Value = Encoding.UTF8.GetString(valueBytes);
					item.SubItems.Add(utf8Value);
				}
				else
				{
					item.SubItems.Add(value.ToString());
				}
			}
			return item;
		}

		private void UpdateFieldListViewItem(ListViewItem item)
		{
			StringBuilder value = new StringBuilder();
			FieldForm.GetFieldValue((ANField)item.Tag, value);
			item.SubItems[2].Text = value.ToString();
		}

		private ANField GetSelectedField()
		{
			return fieldListView.SelectedItems.Count == 1 ? (ANField)fieldListView.SelectedItems[0].Tag : null;
		}

		private void EditField()
		{
			FieldForm form = new FieldForm();
			form.Field = GetSelectedField();
			ANRecord selectedRecord = GetSelectedRecord();
			form.IsReadOnly = FieldNumberForm.IsFieldStandard(selectedRecord.RecordType, _template.Version, form.Field.Number, GetRecordValidationLevel(selectedRecord.IsValidated));
			form.ShowDialog();
			if (form.IsModified)
			{
				UpdateFieldListViewItem(fieldListView.SelectedItems[0]);
				OnSelectedRecordModified();
			}
		}

		private void UpdateTitle()
		{
			Text = _name == null ? ApplicationName
				: string.Format("{0}{1} [V{2}, VL: {3}] - {4}", _name, _isModified ? "*" : string.Empty, _template.Version, _template.ValidationLevel, ApplicationName);
		}

		private void SetTemplate(ANTemplate value, string fileName)
		{
			bool newTemplate = _template != value;
			_template = value;
			_fileName = fileName;
			if (newTemplate) OnRecordsChanged();
			_isModified = false;
			_name = _template == null ? null : fileName == null ? string.Format("NewFile{0}", ++_templateIndex) : Path.GetFileNameWithoutExtension(fileName);
			UpdateTitle();
		}

		private bool NewTemplate()
		{
			uint flags = 0;
			string tot = "NEUR";
			string dai = "NeurotecDest";
			string ori = "NeurotecOrig";
			string tcn = "00001";
			ANValidationLevel validation = Settings.Default.NewValidationLevel;
			using (ANType1RecordCreateForm type1CreateForm = new ANType1RecordCreateForm())
			{
				type1CreateForm.TransactionType = tot;
				type1CreateForm.DestinationAgency = dai;
				type1CreateForm.OriginatingAgency = ori;
				type1CreateForm.TransactionControl = tcn;
				type1CreateForm.ValidationLevel = validation;
				type1CreateForm.UseNistMinutiaNeighbors = Settings.Default.NewUseNistMinutiaNeighbors;
				type1CreateForm.UseTwoDigitIdc = Settings.Default.NewUseTwoDigitIdc;
				type1CreateForm.UseTwoDigitFieldNumber = Settings.Default.NewUseTwoDigitFieldNumber;
				type1CreateForm.UseTwoDigitFieldNumberType1 = Settings.Default.NewUseTwoDigitFieldNumberType1;
				if (type1CreateForm.ShowDialog() == DialogResult.OK)
				{
					Settings.Default.NewValidationLevel = validation = type1CreateForm.ValidationLevel;
					Settings.Default.NewUseNistMinutiaNeighbors = type1CreateForm.UseNistMinutiaNeighbors;
					Settings.Default.NewUseTwoDigitIdc = type1CreateForm.UseTwoDigitIdc;
					Settings.Default.UseTwoDigitFieldNumber = type1CreateForm.UseTwoDigitFieldNumber;
					Settings.Default.UseTwoDigitFieldNumberType1 = type1CreateForm.UseTwoDigitFieldNumberType1;
					Settings.Default.Save();
					if (type1CreateForm.UseNistMinutiaNeighbors) flags |= ANTemplate.FlagUseNistMinutiaNeighbors;
					if (type1CreateForm.UseTwoDigitIdc) flags |= ANTemplate.FlagUseTwoDigitIdc;
					if (type1CreateForm.UseTwoDigitFieldNumber) flags |= ANTemplate.FlagUseTwoDigitFieldNumber;
					if (type1CreateForm.UseTwoDigitFieldNumberType1) flags |= ANTemplate.FlagUseTwoDigitFieldNumberType1;
					ANTemplate template;
					switch (validation)
					{
						case ANValidationLevel.Minimal:
							template = new ANTemplate(ANTemplate.VersionCurrent, ANValidationLevel.Minimal, flags);
							break;
						case ANValidationLevel.Standard:
							tot = type1CreateForm.TransactionType;
							dai = type1CreateForm.DestinationAgency;
							ori = type1CreateForm.OriginatingAgency;
							tcn = type1CreateForm.TransactionControl;
							template = new ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, flags);
							break;
						default:
							throw new NotImplementedException();
					}
					SetTemplate(template, null);
					return true;
				}
				return false;
			}
		}

		private bool OpenTemplate(string fileName)
		{
			using (OptionsForm options = new OptionsForm())
			{
				if (options.ShowDialog() == DialogResult.OK)
				{
					ANTemplate template;
					try
					{
						template = new ANTemplate(fileName, options.ValidationLevel, options.Flags);
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, ex.ToString(), "Can not open ANSI/NIST file", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					SetTemplate(template, fileName);
				}
				return false;
			}
			
		}

		private bool SaveTemplate(string fileName)
		{
			try
			{
				_template.Save(fileName);
			}
			catch (Exception e)
			{
				MessageBox.Show(this, e.ToString(), "Can not save ANSI/NIST file", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			SetTemplate(_template, fileName);
			return true;
		}

		private bool FileSavePrompt()
		{
			if (_isModified)
			{
				switch (MessageBox.Show(this, "ANSI/NIST file modified. Save changes?", "Confirm", MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question))
				{
					case DialogResult.Yes: return FileSave();
					case DialogResult.No: return true;
					default: return false;
				}
			}
			return true;
		}

		private bool FileNew()
		{
			if (FileSavePrompt())
			{
				return NewTemplate();
			}
			return false;
		}

		private bool FileOpen()
		{
			if (FileSavePrompt())
			{
				Settings settings = Settings.Default;
				openFileDialog.FileName = string.Empty;
				openFileDialog.InitialDirectory = settings.LastDirectory;
				if (openFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					settings.LastDirectory = Path.GetDirectoryName(openFileDialog.FileName);
					return OpenTemplate(openFileDialog.FileName);
				}
				return false;
			}
			return false;
		}

		private void FileClose()
		{
			if (FileSavePrompt())
			{
				SetTemplate(null, null);
			}
		}

		private bool FileSave()
		{
			return _fileName == null ? FileSaveAs() : SaveTemplate(_fileName);
		}

		private bool FileSaveAs()
		{
			Settings settings = Settings.Default;
			saveFileDialog.FileName = _fileName ?? _name;
			saveFileDialog.InitialDirectory = settings.LastDirectory;
			if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
				return false;
			settings.LastDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
			return SaveTemplate(saveFileDialog.FileName);
		}

		private void AddValidatedRecord(ANRecordCreateForm createForm)
		{
			try
			{
				createForm.Template = _template;
				if (createForm.ShowDialog() == DialogResult.OK)
				{
					recordListView.SelectedIndices.Clear();
					AddRecordListViewItem(createForm.CreatedRecord).Selected = true;
					OnTemplateModified();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), "Can't add record", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
	}
}
