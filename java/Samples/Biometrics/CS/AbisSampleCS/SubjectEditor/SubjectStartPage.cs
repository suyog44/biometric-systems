using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;
using Neurotec.IO;
using Neurotec.Samples.Properties;
using Neurotec.Samples.SubjectEditor.TenPrintCard;

namespace Neurotec.Samples
{
	public partial class SubjectStartPage : Neurotec.Samples.PageBase
	{
		#region Public constructor

		public SubjectStartPage()
		{
			InitializeComponent();
			DoubleBuffered = true;

			if (!DesignMode)
			{
				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			}
		}

		#endregion

		#region Private fields

		private NSubject _subject;
		private NBiometricClient _biometricClient;
		private NImage _thumbnail;
		private string[] _subjectList;
		private SchemaPropertyGridAdapter _propertyAdapter;

		#endregion

		#region Public methods

		public override void OnNavigatedTo(params object[] args)
		{
			if (args == null || args.Length != 1) throw new ArgumentException("args");
			_subject = (NSubject)args[0];
			_biometricClient = PageController.TabController.Client;

			if (_subject != null)
			{
				tbSubjectId.DataBindings.Add("Text", _subject, "Id");
			}
			bool isEmpty = IsSubjectEmpty();

			if (isEmpty)
			{
				if (!LicensingTools.CanCreateFingerTemplate(_biometricClient.LocalOperations) &&
					!LicensingTools.CanCreateFaceTemplate(_biometricClient.LocalOperations) &&
					!LicensingTools.CanCreateIrisTemplate(_biometricClient.LocalOperations) &&
					!LicensingTools.CanCreatePalmTemplate(_biometricClient.LocalOperations) &&
					!LicensingTools.CanCreateVoiceTemplate(_biometricClient.LocalOperations))
				{
					SetStatus(Color.Red, "None of required licenses were obtained. For more information open ActivationWizard");
				}
				else
				{
					SetStatus(Color.Orange, "Subject is empty. Click on wanted modality in tree view to create new template");
				}
			}
			else
			{
				SetStatus(Color.Green, "Subject is ready for action. Click on buttons above to perform action");
			}

			EnableControls();
			GetThumbnail();
			UpdateSchemaControls();

			if (_subjectList == null)
			{
				try
				{
					NBiometricOperations operations = _biometricClient.LocalOperations;
					if (_biometricClient.RemoteConnections.Count > 0)
						operations |= _biometricClient.RemoteConnections[0].Operations;
					if ((operations & NBiometricOperations.ListIds) == NBiometricOperations.ListIds)
					{
						_subjectList = _biometricClient.ListIds();
						tbSubjectId.AutoCompleteCustomSource.AddRange(_subjectList);
					}
				}
				catch (Exception ex)
				{
					Utilities.ShowError(ex);
				}
			}

			tbQuery.AutoCompleteCustomSource.Clear();
			tbQuery.AutoCompleteCustomSource.AddRange(SettingsManager.QuerySuggestions);

			TryFillGenderField();

			base.OnNavigatedTo(args);
		}

		public override void OnNavigatingFrom()
		{
			tbSubjectId.DataBindings.Clear();
			_biometricClient = null;
			_subject = null;
			base.OnNavigatingFrom();
		}

		#endregion

		#region Private methods

		private void EnableControls()
		{
			bool isEmpty = IsSubjectEmpty();
			if (isEmpty)
			{
				btnUpdate.Enabled = btnSaveTemplate.Enabled = btnEnroll.Enabled = btnEnrollWithDuplicates.Enabled = btnIdentify.Enabled = btnVerify.Enabled = false;
				btnPrintCriminalCard.Enabled = btnPrintApplicantCard.Enabled = false;
			}
			else
			{
				NBiometricOperations operations = _biometricClient.LocalOperations;
				if (_biometricClient.RemoteConnections.Count > 0)
					operations |= _biometricClient.RemoteConnections[0].Operations;

				btnSaveTemplate.Enabled = true;
				btnEnroll.Enabled = true;
				btnIdentify.Enabled = true;
				btnEnrollWithDuplicates.Enabled = (operations & NBiometricOperations.EnrollWithDuplicateCheck) == NBiometricOperations.EnrollWithDuplicateCheck;
				btnUpdate.Enabled = (operations & NBiometricOperations.Update) == NBiometricOperations.Update;
				btnVerify.Enabled = (operations & NBiometricOperations.Verify) == NBiometricOperations.Verify;
				btnPrintCriminalCard.Enabled = btnPrintApplicantCard.Enabled = _subject.Fingers.Count > 0;
			}
		}

		private void TryFillGenderField()
		{
			SampleDbSchema current = SettingsManager.CurrentSchema;
			if (!current.IsEmpty && !string.IsNullOrEmpty(current.GenderDataName))
			{
				NGender gender = _propertyAdapter.GetValue<NGender>(current.GenderDataName);
				if (gender == NGender.Unspecified)
				{
					foreach (var item in _subject.Faces)
					{
						NGender g = item.Objects.First().Gender;
						if (g == NGender.Male || g == NGender.Female)
						{
							_propertyAdapter.SetValue(current.GenderDataName, g);
							propertyGrid.Refresh();
							break;
						}
					}
				}
			}
		}

		private void UpdateSchemaControls()
		{
			SampleDbSchema schema = SettingsManager.CurrentSchema;
			if (schema.IsEmpty)
			{
				gbEnrollData.Visible = false;
			}
			else
			{
				gbThumbnail.Visible = !string.IsNullOrEmpty(schema.ThumbnailDataName);
				if (_propertyAdapter == null)
				{
					NPropertyBag properties = (NPropertyBag)_subject.Properties.Clone();
					properties.Remove(schema.ThumbnailDataName);
					properties.Remove(schema.EnrollDataName);
					_propertyAdapter = new SchemaPropertyGridAdapter(schema, properties) { IsReadOnly = false, ShowBlobs = true };
					propertyGrid.SelectedObject = _propertyAdapter;
					_subject.Properties.Clear();
				}
			}
		}

		private void SetStatus(Color backColor, string format, params object[] args)
		{
			lblHint.BackColor = backColor;
			lblHint.Text = string.Format(format, args);
		}

		private bool IsSubjectEmpty()
		{
			return _subject.Fingers.Count +
					_subject.Faces.Count +
					_subject.Irises.Count +
					_subject.Voices.Count +
					_subject.Palms.Count == 0;
		}

		private void SetSubjectProperties()
		{
			SampleDbSchema schema = SettingsManager.CurrentSchema;
			if (!schema.IsEmpty)
			{
				_subject.Properties.Clear();
				if (_thumbnail != null)
				{
					NImageFormat format = _thumbnail.Info.Format;
					if (format == null || !format.CanWrite)
						format = NImageFormat.Png;
					_subject.SetProperty(schema.ThumbnailDataName, _thumbnail.Save(format));
				}
				if (!string.IsNullOrEmpty(schema.EnrollDataName))
				{
					Func<NSubject, bool, NBuffer> serialize = EnrollDataSerializer.Serialize;
					string title = "Serializing subject data";
					using (var buffer = (NBuffer)LongActionDialog.ShowDialog(this, title, serialize, _subject, LicensingTools.IsComponentActivated("Images.WSQ")))
					{
						_subject.SetProperty(schema.EnrollDataName, buffer);
					}
				}

				_propertyAdapter.ApplyTo(_subject);
			}
		}

		private void GetThumbnail()
		{
			SampleDbSchema schema = SettingsManager.CurrentSchema;
			if (!schema.IsEmpty)
			{
				bool hasThumbnail = !string.IsNullOrEmpty(schema.ThumbnailDataName);
				gbThumbnail.Visible = hasThumbnail;
				if (hasThumbnail && _thumbnail == null)
				{
					if (_subject.Properties.ContainsKey(schema.ThumbnailDataName))
					{
						_thumbnail = NImage.FromMemory(_subject.GetProperty<NBuffer>(schema.ThumbnailDataName));
					}
					else
					{
						foreach (NFace face in _subject.Faces)
						{
							NLAttributes attributes = face.Objects.FirstOrDefault();
							_thumbnail = attributes.Thumbnail;
							if (_thumbnail != null) break;
						}
					}
					pbThumnail.Image = _thumbnail != null ? _thumbnail.ToBitmap() : null;
				}
			}
		}

		#endregion

		#region Private events

		private void BtnEnrollClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbSubjectId.Text))
			{
				tbSubjectId.BackColor = Color.DarkRed;
				tbSubjectId.Focus();
			}
			else
			{
				try
				{
					_subject.QueryString = null;
					SetSubjectProperties();
					PageController.TabController.ShowTab(typeof(DababaseOperationTab), true, true, _subject, NBiometricOperations.Enroll);
				}
				catch (Exception ex)
				{
					Utilities.ShowError(ex);
				}
			}
		}

		private void BtnIdentifyClick(object sender, EventArgs e)
		{
			string query = tbQuery.Text;
			_subject.QueryString = query == string.Empty ? null : query;
			if (query != string.Empty && !tbQuery.AutoCompleteCustomSource.Contains(query))
			{
				tbQuery.AutoCompleteCustomSource.Add(query);
				SettingsManager.QuerySuggestions = tbQuery.AutoCompleteCustomSource.OfType<string>().ToArray();
			}
			PageController.TabController.ShowTab(typeof(DababaseOperationTab), true, true, _subject, NBiometricOperations.Identify);
		}

		private void BtnVerifyClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbSubjectId.Text))
			{
				tbSubjectId.BackColor = Color.DarkRed;
				tbSubjectId.Focus();
			}
			else
			{
				_subject.QueryString = null;
				PageController.TabController.ShowTab(typeof(DababaseOperationTab), true, true, _subject, NBiometricOperations.Verify);
			}
		}

		private void BtnEnrollWithDuplicatesClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbSubjectId.Text))
			{
				tbSubjectId.BackColor = Color.DarkRed;
				tbSubjectId.Focus();
			}
			else
			{
				try
				{
					_subject.QueryString = null;
					SetSubjectProperties();
					PageController.TabController.ShowTab(typeof(DababaseOperationTab), true, true, _subject, NBiometricOperations.EnrollWithDuplicateCheck);
				}
				catch (Exception ex)
				{
					Utilities.ShowError(ex);
				}
			}
		}

		private void BtnUpdateClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbSubjectId.Text))
			{
				tbSubjectId.BackColor = Color.DarkRed;
				tbSubjectId.Focus();
			}
			else
			{
				try
				{
					_subject.QueryString = null;
					SetSubjectProperties();
					PageController.TabController.ShowTab(typeof(DababaseOperationTab), true, true, _subject, NBiometricOperations.Update);
				}
				catch (Exception ex)
				{
					Utilities.ShowError(ex);
				}
			}
		}

		private void TbSubjectIdTextChanged(object sender, EventArgs e)
		{
			if (tbSubjectId.BackColor == Color.DarkRed && !string.IsNullOrEmpty(tbSubjectId.Text))
			{
				tbSubjectId.BackColor = SystemColors.Window;
			}
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			if (!IsSubjectEmpty())
			{
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					try
					{
						using (var nbuffer = _subject.GetTemplateBuffer())
						{
							File.WriteAllBytes(saveFileDialog.FileName, nbuffer.ToArray());
						}
					}
					catch (Exception ex)
					{
						Utilities.ShowError(ex);
					}
				}
			}
		}

		private void BtnOpenImageClick(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					_thumbnail = NImage.FromFile(openFileDialog.FileName);
					pbThumnail.Image = _thumbnail.ToBitmap();
				}
				catch (Exception ex)
				{
					Utilities.ShowError(ex);
				}
			}
		}

		private void BtnPrintApplicantCardClick(object sender, EventArgs e)
		{
			using (var dialog = new TenPrintCardPrintForm(Resources.ApplicantCard) { Subject = _subject, BiometricClient = _biometricClient })
			{
				dialog.ShowDialog();
			}
		}

		private void BtnPrintCriminalCardClick(object sender, EventArgs e)
		{
			using (var dialog = new TenPrintCardPrintForm(Resources.CriminalCard) { Subject = _subject, BiometricClient = _biometricClient })
			{
				dialog.ShowDialog();
			}
		}

		#endregion
	}
}
