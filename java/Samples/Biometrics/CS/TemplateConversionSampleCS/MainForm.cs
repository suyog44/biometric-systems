using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using System.IO;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Internal vars

		private ANTemplate _currentANTemplate;
		private NFRecord[] _currentNFRecords;
		private NFTemplate _currentNFTemplate;
		private NTemplate _currentNTemplate;
		private FMRecord _currentFMRecord;
		private NLTemplate _currentNLTemplate;
		private NLRecord[] _currentNLRecords;

		#endregion Internal vars

		#region Constructor
		public MainForm()
		{
			InitializeComponent();
		}
		#endregion Constructor

		#region Events
		protected override void OnLoad(EventArgs e)
		{
			MakeAllEnabledDisabled(false);

			base.OnLoad(e);
		}
		#endregion Events

		private void openImageButton1_Click(object sender, EventArgs e)
		{
			try
			{
				string[] fileNames;
				
				#region Open ANTemplate
				if (rbANTemplate.Checked)
				{
					fileNames = OpenDialogSetup("ANTemplate files (*.dat)|*.dat|All files|*.*", "Open ANTemplate", false);
					if (fileNames != null)
					{
						if (_currentANTemplate != null)
						{
							_currentANTemplate.Dispose();
						}

						_currentANTemplate = new ANTemplate(fileNames[0], ANValidationLevel.Standard);
						rtbLeft.Text = string.Format(
							"Filename: {0}\n Type: ANTemplate\n", fileNames[0]);
					}
					return;
				}
				#endregion

				#region Open NFRecord
				if (rbNFRecords.Checked)
				{
					fileNames = OpenDialogSetup("NFRecord files (*.dat)|*.dat|All files|*.*", "Open NFRecord(s)", true);
					string openedFileNames = string.Empty;
					if (fileNames != null)
					{
						if (_currentNFRecords != null)
						{
							foreach (NFRecord t in _currentNFRecords)
							{
								t.Dispose();
							}
						}

						_currentNFRecords = new NFRecord[fileNames.Length];
						for (int i = 0; i < _currentNFRecords.Length; i++)
						{
							byte[] arrayTmp = File.ReadAllBytes(fileNames[i]);
							_currentNFRecords[i] = new NFRecord(arrayTmp);
							openedFileNames += string.Format("Filename: {0}\n Type: NFRecord\r\n", fileNames[i]);
							openedFileNames += string.Format(
								"Cores: {0}, Double cores: {1}, Deltas: {2}, Minutia: {3}\r\nG: {4}\r\n\r\n",
								_currentNFRecords[i].Cores.Count, _currentNFRecords[i].DoubleCores.Count,
								_currentNFRecords[i].Deltas.Count, _currentNFRecords[i].Minutiae.Count, _currentNFRecords[i].G);
						}
						rtbLeft.Text = openedFileNames;
					}
					return;
				}

				#endregion

				#region Open NFTemplate
				if (rbNFTemplate.Checked)
				{
					fileNames = OpenDialogSetup("NFTemplate files (*.dat)|*.dat|All files|*.*", "Open NFTemplate", false);
					if (fileNames != null)
					{
						if (_currentNFTemplate != null)
						{
							_currentNFTemplate.Dispose();
						}

						byte[] arrayN = File.ReadAllBytes(fileNames[0]);
						_currentNFTemplate = new NFTemplate(arrayN);

						rtbLeft.Text = string.Format("Filename: {0}\n Type: NFTemplate\r\n", fileNames[0]);

						if (_currentNFTemplate.Records != null)
						{
							rtbLeft.Text = string.Format("Fingerprint record count: {0}\r\n", _currentNFTemplate.Records.Count);
						}

						if ((_currentNFTemplate.Records != null) && (_currentNFTemplate.Records.Count != 0)) return;

						rbNTemplateRight.Enabled = false;
						rbNFRecordsRight.Enabled = false;
						rbFMRecordISORight.Enabled = false;
						rbFMRecordANSIRight.Enabled = false;
						rbANTemplateRight.Enabled = false;
					}
					return;
				}
				#endregion

				#region Open NTemplate
				if (rbNTemplate.Checked)
				{
					fileNames = OpenDialogSetup("NTemplate files (*.dat)|*.dat|All files|*.*", "Open NTemplate", false);
					if (fileNames != null)
					{
						if (_currentNTemplate != null)
						{
							_currentNTemplate.Dispose();
						}

						byte[] arrayN = File.ReadAllBytes(fileNames[0]);
						_currentNTemplate = new NTemplate(arrayN);

						rtbLeft.Text = string.Format("Filename: {0}\n Type: NTemplate\r\n", fileNames[0]);

						if ((_currentNTemplate.Fingers != null) && (_currentNTemplate.Fingers.Records.Count > 0))
						{
							rtbLeft.Text += string.Format("Fingerprint record count: {0}\r\n", _currentNTemplate.Fingers.Records.Count);
						}
						else
						{
							rbNFTemplateRight.Enabled = false;
							rbNFRecordsRight.Enabled = false;
							rbFMRecordISORight.Enabled = false;
							rbFMRecordANSIRight.Enabled = false;
						}

						if ((_currentNTemplate.Faces != null) && (_currentNTemplate.Faces.Records.Count > 0))
						{
							rtbLeft.Text += string.Format("Face record count: {0}\r\n", _currentNTemplate.Faces.Records.Count);
						}
						else
						{
							rbANTemplateRight.Enabled = false;
							rbNLRecordRight.Enabled = false;
							rbNLTemplateRight.Enabled = false;
						}
					}
					return;
				}
				#endregion

				#region Open FMRecord ANSI/ISO
				if (rbFMRecordANSI.Checked)
				{
					fileNames = OpenDialogSetup("FMRecord files (*.dat)|*.dat|All files|*.*", "Open FMRecord (ANSI)", false);
					if (fileNames != null)
					{
						if (_currentFMRecord != null)
						{
							_currentFMRecord.Dispose();
						}

						byte[] arrayN = File.ReadAllBytes(fileNames[0]);
						_currentFMRecord = new FMRecord(new NBuffer(arrayN), BdifStandard.Ansi);

						rtbLeft.Text = string.Format("Filename: {0}\n Type: FMRecord ANSI\r\n", fileNames[0]);

					}
					return;
				}

				if (rbFMRecordISO.Checked)
				{
					fileNames = OpenDialogSetup("FMRecord files (*.dat)|*.dat|All files|*.*", "Open FMRecord (ISO)", false);
					if (fileNames != null)
					{
						if (_currentFMRecord != null)
						{
							_currentFMRecord.Dispose();
						}

						byte[] arrayN = File.ReadAllBytes(fileNames[0]);
						_currentFMRecord = new FMRecord(new NBuffer(arrayN), BdifStandard.Iso);

						rtbLeft.Text = string.Format("Filename: {0}\n Type: FMRecord ISO\r\n", fileNames[0]);
					}
					return;
				}
				#endregion

				#region Open NLTemplate
				if (rbNLTemplate.Checked)
				{
					fileNames = OpenDialogSetup("NLTemplate files (*.dat)|*.dat|All files|*.*", "Open NLTemplate", false);
					if (fileNames != null)
					{
						if (_currentNLTemplate != null)
						{
							_currentNLTemplate.Dispose();
						}

						byte[] arrayN = File.ReadAllBytes(fileNames[0]);
						_currentNLTemplate = new NLTemplate(arrayN);

						rtbLeft.Text = string.Format("Filename: {0}\n Type: NLTemplate\r\n", fileNames[0]);

						if (_currentNLTemplate.Records != null)
						{
							rtbLeft.Text = string.Format("Face record count: {0}\r\n", _currentNLTemplate.Records.Count);
						}

						if ((_currentNLTemplate.Records != null) && (_currentNLTemplate.Records.Count != 0)) return;

						rbNTemplateRight.Enabled = false;
						rbNLRecordRight.Enabled = false;
					}
					return;
				}
				#endregion

				#region Open NLRecord
				if (rbNLRecord.Checked)
				{
					fileNames = OpenDialogSetup("NLRecord files (*.dat)|*.dat|All files|*.*", "Open NLRecord(s)", true);
					string openedFileNames = string.Empty;
					if (fileNames != null)
					{
						if (_currentNLRecords != null)
						{
							foreach (NLRecord t in _currentNLRecords)
							{
								t.Dispose();
							}
						}

						_currentNLRecords = new NLRecord[fileNames.Length];
						for (int i = 0; i < _currentNLRecords.Length; i++)
						{
							byte[] arrayTmp = File.ReadAllBytes(fileNames[i]);
							_currentNLRecords[i] = new NLRecord(arrayTmp);
							openedFileNames += string.Format("Filename: {0}\n Type: NLRecord\r\n", fileNames[i]);
							openedFileNames += string.Format("Quality: {0}\r\n\r\n", _currentNLRecords[i].Quality);
						}
						rtbLeft.Text = openedFileNames;
					}
					return;
				}
				#endregion

				MessageBox.Show(this, "Before loading a template you must select the type of the template.\r\n"
					+ "Please select one of the items above and try again.",
					"Template Convertion: Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Error occured while openning file(s).\r\nDetails: " + ex.Message,
					"Template Convertion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#region Misc functions
		private String[] OpenDialogSetup(string extension, string title, bool multiselect)
		{
			openFileDialog.Title = title;
			openFileDialog.Multiselect = multiselect;
			openFileDialog.CheckFileExists = true;
			openFileDialog.CheckPathExists = true;
			openFileDialog.Filter = extension;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				return openFileDialog.FileNames;
			}

			return null;
		}

		private String SaveDialogSetup(string extension, string title)
		{
			saveFileDialog.Title = title;
			saveFileDialog.Filter = extension;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				return saveFileDialog.FileName;
			}

			return null;
		}

		private void MakeAllEnabledDisabled(bool enabled)
		{
			rbNTemplateRight.Enabled = enabled;
			rbNLTemplateRight.Enabled = enabled;
			rbNLRecordRight.Enabled = enabled;
			rbNFTemplateRight.Enabled = enabled;
			rbNFRecordsRight.Enabled = enabled;
			rbFMRecordISORight.Enabled = enabled;
			rbFMRecordANSIRight.Enabled = enabled;
			rbANTemplateRight.Enabled = enabled;
		}

		#endregion Misc functions

		private void Global_CheckedChanged(object sender, EventArgs e)
		{
			MakeAllEnabledDisabled(false);
			rtbLeft.Text = string.Empty;

			_currentANTemplate = null;
			_currentNFRecords = null;
			_currentNFTemplate = null;
			_currentNTemplate = null;
			_currentFMRecord = null;
			_currentNLRecords = null;
			_currentNLTemplate = null;

			if (rbANTemplate.Checked)
			{
				rbNTemplateRight.Enabled = true;
				rbNFTemplateRight.Enabled = true;
				rbNFRecordsRight.Enabled = true;
				rbFMRecordISORight.Enabled = true;
				rbFMRecordANSIRight.Enabled = true;
			}

			if (rbFMRecordANSI.Checked)
			{
				rbNTemplateRight.Enabled = true;
				rbNFTemplateRight.Enabled = true;
				rbNFRecordsRight.Enabled = true;
				rbFMRecordISORight.Enabled = true;
				rbANTemplateRight.Enabled = true;
			}

			if (rbFMRecordISO.Checked)
			{
				rbNTemplateRight.Enabled = true;
				rbNFTemplateRight.Enabled = true;
				rbNFRecordsRight.Enabled = true;
				rbFMRecordANSIRight.Enabled = true;
				rbANTemplateRight.Enabled = true;
			}

			if (rbNFRecords.Checked)
			{
				rbFMRecordANSIRight.Enabled = true;
				rbFMRecordISORight.Enabled = true;
				rbANTemplateRight.Enabled = true;
				rbNFTemplateRight.Enabled = true;
				rbNTemplateRight.Enabled = true;
			}

			if (rbNFTemplate.Checked)
			{
				rbNFRecordsRight.Enabled = true;
				rbFMRecordANSIRight.Enabled = true;
				rbFMRecordISORight.Enabled = true;
				rbANTemplateRight.Enabled = true;
				rbNTemplateRight.Enabled = true;
			}

			if (rbNLRecord.Checked)
			{
				rbNLTemplateRight.Enabled = true;
				rbNTemplateRight.Enabled = true;
			}

			if (rbNLTemplate.Checked)
			{
				rbNLRecordRight.Enabled = true;
				rbNTemplateRight.Enabled = true;
			}

			if (rbNTemplate.Checked)
			{
				rbFMRecordANSIRight.Enabled = true;
				rbFMRecordISORight.Enabled = true;
				rbANTemplateRight.Enabled = true;

				rbNFRecordsRight.Enabled = true;
				rbNFTemplateRight.Enabled = true;
				rbNLRecordRight.Enabled = true;
				rbNLTemplateRight.Enabled = true;
			}

		}

		private void SaveNLTemplateAsNLRecord(NLTemplate tmpl)
		{
			string fileName = SaveDialogSetup("NLRecord files (*.dat)|*.dat|All files|*.*", "Save NLRecord(s)");

			if (fileName != null)
			{
				string extension = Path.GetExtension(fileName);
				string name = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName);

				for (int i = 0; i < tmpl.Records.Count; i++)
				{
					string tmp = string.Format("{0}{1}{2}", name, i, extension);
					File.WriteAllBytes(tmp, tmpl.Records[i].Save().ToArray());
					rtbRight.AppendText("Saved record (NLRecord): " + tmp + "\r\n");
				}
			}
		}

		private void SaveNFTemplateAsNFRecord(NFTemplate tmpl)
		{
			string fileName = SaveDialogSetup("NFRecord files (*.dat)|*.dat|All files|*.*", "Save NFRecord(s)");

			if (fileName != null)
			{
				string extension = Path.GetExtension(fileName);
				string name = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName);
				for (int i = 0; i < tmpl.Records.Count; i++)
				{
					string tmp = string.Format("{0}{1}{2}", name, i, extension);
					File.WriteAllBytes(tmp, tmpl.Records[i].Save().ToArray());
					rtbRight.AppendText("Saved record (NFRecord): " + tmp + "\r\n");
				}
			}
		}

		private void PrintInvalidStatement()
		{
			MessageBox.Show(this, "No templates opened for conversion, or data is invalid.",
								"Template Convertion: Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnConvertAndSave_Click(object sender, EventArgs e)
		{
			try
			{
				#region Conversion To NFRecord
				if (rbNFRecordsRight.Checked)
				{
					if (rbNTemplate.Checked)
					{
						if ((_currentNTemplate == null) || (_currentNTemplate.Fingers == null)
							|| (_currentNTemplate.Fingers.Records == null) || (_currentNTemplate.Fingers.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							SaveNFTemplateAsNFRecord(_currentNTemplate.Fingers);
						}
						return;
					}

					if (rbNFTemplate.Checked)
					{
						if ((_currentNFTemplate == null) || (_currentNFTemplate.Records == null) || (_currentNFTemplate.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							SaveNFTemplateAsNFRecord(_currentNFTemplate);
						}
						return;
					}

					if (rbANTemplate.Checked)
					{
						if ((_currentANTemplate == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							NFTemplate tmpl = RecordTransformations.ANTemplateToNFTemplate(_currentANTemplate);
							SaveNFTemplateAsNFRecord(tmpl);
							tmpl.Dispose();
						}
						return;
					}

					if ((rbFMRecordANSI.Checked) || (rbFMRecordISO.Checked))
					{
						if ((_currentFMRecord == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							NFTemplate tmpl = RecordTransformations.FMRecordToNFTemplate(_currentFMRecord);
							SaveNFTemplateAsNFRecord(tmpl);
							tmpl.Dispose();
						}
						return;
					}
				}
				#endregion

				#region Convertion To NFTemplate
				if (rbNFTemplateRight.Checked)
				{
					if (rbNTemplate.Checked)
					{
						if ((_currentNTemplate == null) || (_currentNTemplate.Fingers == null) || (_currentNTemplate.Fingers.Records == null) || (_currentNTemplate.Fingers.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							string fileName = SaveDialogSetup("NFTemplate files (*.dat)|*.dat|All files|*.*", "Save NFTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, _currentNTemplate.Fingers.Save().ToArray());
								rtbRight.AppendText("Saved record (NFTemplate): " + fileName + "\r\n");
							}
						}
						return;
					}

					if (rbNFRecords.Checked)
					{
						if ((_currentNFRecords == null) || (_currentNFRecords.Length == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							var tmpl = new NFTemplate();
							foreach (NFRecord t in _currentNFRecords)
							{
								tmpl.Records.Add((NFRecord)t.Clone());
							}

							string fileName = SaveDialogSetup("NFTemplate files (*.dat)|*.dat|All files|*.*", "Save NFTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, tmpl.Save().ToArray());
								rtbRight.AppendText("Saved record (NFTemplate): " + fileName + "\r\n");
								tmpl.Dispose();
							}
						}
						return;
					}

					if (rbANTemplate.Checked)
					{
						if ((_currentANTemplate == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							NFTemplate tmpl = RecordTransformations.ANTemplateToNFTemplate(_currentANTemplate);
							string fileName = SaveDialogSetup("NFTemplate files (*.dat)|*.dat|All files|*.*", "Save NFTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, tmpl.Save().ToArray());
								rtbRight.AppendText("Saved record (NFTemplate): " + fileName + "\r\n");
								tmpl.Dispose();
							}
						}
						return;
					}

					if ((rbFMRecordANSI.Checked) || (rbFMRecordISO.Checked))
					{
						if ((_currentFMRecord == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							NFTemplate tmpl = RecordTransformations.FMRecordToNFTemplate(_currentFMRecord);
							string fileName = SaveDialogSetup("NFTemplate files (*.dat)|*.dat|All files|*.*", "Save NFTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName,tmpl.Save().ToArray());
								rtbRight.AppendText("Saved record (NFTemplate): " + fileName + "\r\n");
								tmpl.Dispose();
							}
						}
						return;
					}
				}
				#endregion

				#region Conversion to NLRecord
				if ((rbNLRecordRight.Checked))
				{
					if (rbNTemplate.Checked)
					{
						if ((_currentNTemplate == null) || (_currentNTemplate.Faces == null) || (_currentNTemplate.Faces.Records == null) || (_currentNTemplate.Faces.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							SaveNLTemplateAsNLRecord(_currentNTemplate.Faces);
						}
						return;
					}

					if (rbNLTemplate.Checked)
					{
						if ((_currentNLTemplate == null) || (_currentNLTemplate.Records == null) || (_currentNLTemplate.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							SaveNLTemplateAsNLRecord(_currentNLTemplate);
						}
						return;
					}
				}
				#endregion

				#region Conversion to NLTemplate
				if ((rbNLTemplateRight.Checked))
				{
					if (rbNLRecord.Checked)
					{
						if ((_currentNLRecords == null) || (_currentNLRecords.Length == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							var tmpl = new NLTemplate();
							foreach (NLRecord t in _currentNLRecords)
							{
								tmpl.Records.Add((NLRecord)t.Clone());
							}

							string fileName = SaveDialogSetup("NLTemplate files (*.dat)|*.dat|All files|*.*", "Save NLTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, tmpl.Save().ToArray());
								rtbRight.AppendText("Saved record (NLTemplate): " + fileName + "\r\n");
								tmpl.Dispose();
							}
						}
						return;
					}

					if (!rbNTemplate.Checked) return;
					if ((_currentNTemplate == null) || (_currentNTemplate.Faces == null))
					{
						PrintInvalidStatement();
					}
					else
					{
						string fileName = SaveDialogSetup("NLTemplate files (*.dat)|*.dat|All files|*.*", "Save NLTemplate");
						if (fileName != null)
						{
							File.WriteAllBytes(fileName, _currentNTemplate.Faces.Save().ToArray());
							rtbRight.AppendText("Saved record (NLTemplate): " + fileName + "\r\n");
						}
					}
					return;
				}
				#endregion

				#region Conversion To NTemplate
				if (rbNTemplateRight.Checked)
				{
					if (rbANTemplate.Checked)
					{
						if ((_currentANTemplate == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							NTemplate tmpl = RecordTransformations.ANTemplateToNTemplate(_currentANTemplate);
							string fileName = SaveDialogSetup("NTemplate files (*.dat)|*.dat|All files|*.*", "Save NTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, tmpl.Save().ToArray());
								rtbRight.AppendText("Saved record (NTemplate): " + fileName + "\r\n");
								tmpl.Dispose();
							}
						}
						return;
					}

					if ((rbFMRecordANSI.Checked) || (rbFMRecordISO.Checked))
					{
						if ((_currentFMRecord == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							NTemplate tmpl = RecordTransformations.FMRecordToNTemplate(_currentFMRecord);
							string fileName = SaveDialogSetup("NTemplate files (*.dat)|*.dat|All files|*.*", "Save NTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, tmpl.Save().ToArray());
								rtbRight.AppendText("Saved record (NTemplate): " + fileName + "\r\n");
								tmpl.Dispose();
							}
						}
						return;
					}

					if (rbNFTemplate.Checked)
					{
						if ((_currentNFTemplate == null) || (_currentNFTemplate.Records == null) || (_currentNFTemplate.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							var tmpl = new NTemplate {Fingers = (NFTemplate) _currentNFTemplate.Clone()};
							string fileName = SaveDialogSetup("NTemplate files (*.dat)|*.dat|All files|*.*", "Save NTemplate");
							if (fileName == null) return;
							File.WriteAllBytes(fileName, tmpl.Save().ToArray());
							rtbRight.AppendText("Saved record (NTemplate): " + fileName + "\r\n");
							tmpl.Dispose();
						}
						return;
					}

					if (rbNFRecords.Checked)
					{
						if ((_currentNFRecords == null) || (_currentNFRecords.Length == 0))
						{
							PrintInvalidStatement();
						}
						else
							{
							var tmpl = new NFTemplate();
							foreach (NFRecord t in _currentNFRecords)
							{
								tmpl.Records.Add((NFRecord)t.Clone());
							}
								var tmpl2 = new NTemplate {Fingers = (NFTemplate) tmpl.Clone()};

								string fileName = SaveDialogSetup("NTemplate files (*.dat)|*.dat|All files|*.*", "Save NTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, tmpl2.Save().ToArray());
								rtbRight.AppendText("Saved record (NTemplate): " + fileName + "\r\n");
								tmpl2.Dispose();
								tmpl.Dispose();
							}
						}
						return;
					}

					if (rbNLTemplate.Checked)
					{
						if ((_currentNLTemplate == null) || (_currentNLTemplate.Records == null) || (_currentNLTemplate.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							var tmpl = new NTemplate {Faces = (NLTemplate) _currentNLTemplate.Clone()};
							string fileName = SaveDialogSetup("NTemplate files (*.dat)|*.dat|All files|*.*", "Save NTemplate");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, tmpl.Save().ToArray());
								rtbRight.AppendText("Saved record (NTemplate): " + fileName + "\r\n");
								tmpl.Dispose();
							}
						}
						return;
					}
				}
				#endregion

				#region Conversion To ANTemplate
				if (rbANTemplateRight.Checked)
				{
					if (rbNTemplate.Checked)
					{
						if ((_currentNTemplate == null) || (_currentNTemplate.Faces == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							ANTemplate tmpl = RecordTransformations.NTemplateToANTemplate(_currentNTemplate);
							string fileName = SaveDialogSetup("ANTemplate files (*.dat)|*.dat|All files|*.*", "Save ANTemplate");
							if (fileName != null)
							{
								tmpl.Save(fileName);
								rtbRight.AppendText("Saved record (ANTemplate): " + fileName + "\r\n");
								tmpl.Dispose();
							}
						}
						return;
					}

					if (rbNFTemplate.Checked)
					{
						if ((_currentNFTemplate == null) || (_currentNFTemplate.Records == null) || (_currentNFTemplate.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							var tmpl = new NTemplate {Fingers = (NFTemplate) _currentNFTemplate.Clone()};
							ANTemplate tmpl2 = RecordTransformations.NTemplateToANTemplate(tmpl);
							string fileName = SaveDialogSetup("ANTemplate files (*.dat)|*.dat|All files|*.*", "Save ANTemplate");
							if (fileName != null)
							{
								tmpl2.Save(fileName);
								rtbRight.AppendText("Saved record (ANTemplate): " + fileName + "\r\n");
								tmpl2.Dispose();
								tmpl.Dispose();
							}
						}
						return;
					}

					if (rbNFRecords.Checked)
					{
						if ((_currentNFRecords == null) || (_currentNFRecords.Length == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							var tmpl = new NFTemplate();
							foreach (NFRecord t in _currentNFRecords)
							{
								tmpl.Records.Add((NFRecord)t.Clone());
							}
							var tmpl2 = new NTemplate {Fingers = (NFTemplate) tmpl.Clone()};

							ANTemplate tmpl3 = RecordTransformations.NTemplateToANTemplate(tmpl2);
							string fileName = SaveDialogSetup("ANTemplate files (*.dat)|*.dat|All files|*.*", "Save ANTemplate");
							if (fileName != null)
							{
								tmpl3.Save(fileName);
								rtbRight.AppendText("Saved record (ANTemplate): " + fileName + "\r\n");
								tmpl3.Dispose();
								tmpl2.Dispose();
								tmpl.Dispose();
							}
						}
						return;
					}

					if ((!rbFMRecordANSI.Checked) && (!rbFMRecordISO.Checked)) return;
					if ((_currentFMRecord == null))
					{
						PrintInvalidStatement();
					}
					else
					{
						NTemplate tmpl = RecordTransformations.FMRecordToNTemplate(_currentFMRecord);
						ANTemplate tmpl2 = RecordTransformations.NTemplateToANTemplate(tmpl);
						string fileName = SaveDialogSetup("ANTemplate files (*.dat)|*.dat|All files|*.*", "Save ANTemplate");
						if (fileName != null)
						{
							tmpl2.Save(fileName);
							rtbRight.AppendText("Saved record (ANTemplate): " + fileName + "\r\n");
							tmpl2.Dispose();
							tmpl.Dispose();
						}
					}
					return;
				}
				#endregion

				#region Conversion To FMRecord ANSI/ISO
				if ((rbFMRecordISORight.Checked) || (rbFMRecordANSIRight.Checked))
				{
					if (rbNFTemplate.Checked)
					{
						if ((_currentNFTemplate == null) || (_currentNFTemplate.Records == null) || (_currentNFTemplate.Records.Count == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							FMRecord record = (rbFMRecordISORight.Checked) ?
								RecordTransformations.NFTemplateToFMRecord(_currentNFTemplate, BdifStandard.Iso) :
								RecordTransformations.NFTemplateToFMRecord(_currentNFTemplate, BdifStandard.Ansi);
							string fileName = SaveDialogSetup("FMRecord files (*.dat)|*.dat|All files|*.*", "Save FMRecord");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, record.Save().ToArray());
								rtbRight.AppendText("Saved record (FMRecord): " + fileName + "\r\n");
								record.Dispose();
							}
						}
						return;
					}

					if (rbNTemplate.Checked)
					{
						if ((_currentNTemplate == null) || (_currentNTemplate.Fingers == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							FMRecord record = (rbFMRecordISORight.Checked) ?
								RecordTransformations.NFTemplateToFMRecord(_currentNTemplate.Fingers, BdifStandard.Iso) :
								RecordTransformations.NFTemplateToFMRecord(_currentNTemplate.Fingers, BdifStandard.Ansi);
							string fileName = SaveDialogSetup("FMRecord files (*.dat)|*.dat|All files|*.*", "Save FMRecord");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, record.Save().ToArray());
								rtbRight.AppendText("Saved record (FMRecord): " + fileName + "\r\n");
								record.Dispose();
							}
						}
						return;
					}

					if (rbNFRecords.Checked)
					{
						if ((_currentNFRecords == null) || (_currentNFRecords.Length == 0))
						{
							PrintInvalidStatement();
						}
						else
						{
							var tmpl = new NFTemplate();
							foreach (NFRecord t in _currentNFRecords)
							{
								tmpl.Records.Add((NFRecord)t.Clone());
							}

							FMRecord record = (rbFMRecordISORight.Checked) ?
															RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Iso) :
															RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Ansi);
							string fileName = SaveDialogSetup("FMRecord files (*.dat)|*.dat|All files|*.*", "Save FMRecord");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, record.Save().ToArray());
								rtbRight.AppendText("Saved record (FMRecord): " + fileName + "\r\n");
								record.Dispose();
								tmpl.Dispose();
							}
						}
						return;
					}

					if (rbANTemplate.Checked)
					{
						if ((_currentANTemplate == null))
						{
							PrintInvalidStatement();
						}
						else
						{
							NFTemplate tmpl = RecordTransformations.ANTemplateToNFTemplate(_currentANTemplate);
							FMRecord record = (rbFMRecordISORight.Checked) ?
															RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Iso) :
															RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Ansi);
							string fileName = SaveDialogSetup("FMRecord files (*.dat)|*.dat|All files|*.*", "Save FMRecord");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, record.Save().ToArray());
								rtbRight.AppendText("Saved record (FMRecord): " + fileName + "\r\n");
								record.Dispose();
								tmpl.Dispose();
							}
						}
						return;
					}

					if ((rbFMRecordISO.Checked) || (rbFMRecordANSI.Checked))
					{
						if (_currentFMRecord == null)
						{
							PrintInvalidStatement();
						}
						else
						{
							NFTemplate tmpl = RecordTransformations.FMRecordToNFTemplate(_currentFMRecord);
							FMRecord record = (rbFMRecordISORight.Checked) ?
															RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Iso) :
															RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Ansi);
							string fileName = SaveDialogSetup("FMRecord files (*.dat)|*.dat|All files|*.*", "Save FMRecord");
							if (fileName != null)
							{
								File.WriteAllBytes(fileName, record.Save().ToArray());
								rtbRight.AppendText("Saved record (FMRecord): " + fileName + "\r\n");
								record.Dispose();
								tmpl.Dispose();
							}
						}
						return;
					}

				}
				#endregion

				if (_currentANTemplate == null && _currentNFRecords == null
					&& _currentNFTemplate == null && _currentNTemplate == null
					&& _currentFMRecord == null && _currentNLTemplate == null
					&& _currentNLRecords == null)
				{
					MessageBox.Show(this, "Before converting a template you must load a template.\r\n"
						+ "Please select one of the items on the left, then press 'Open Template' button and try again.",
						"Template Convertion: Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					MessageBox.Show(this, "Before converting a template you must select a type to convert to.\r\n"
						+ "Please select one of the items above and try again.",
						"Template Convertion: Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Error occured while converting or saving files.\r\nDetails: " + ex.Message,
					"Template Convertion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
