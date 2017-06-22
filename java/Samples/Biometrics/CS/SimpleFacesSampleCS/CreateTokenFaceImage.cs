using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class CreateTokenFaceImage : UserControl
	{
		#region Public constructor

		public CreateTokenFaceImage()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private NFace _tokenFace;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set { _biometricClient = value; }
		}

		#endregion

		#region Private methods

		private void CreateImage(String imagePath)
		{
			var face = new NFace { FileName = imagePath };
			_subject = new NSubject();
			_subject.Faces.Add(face);
			faceViewOriginal.Face = face;
			var task = _biometricClient.CreateTask(NBiometricOperations.Segment | NBiometricOperations.AssessQuality, _subject);
			_biometricClient.BeginPerformTask(task, OnImageCreated, null);
		}

		private void OnImageCreated(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnImageCreated), r);
			}
			else
			{
				var task = _biometricClient.EndPerformTask(r);
				if (task.Error == null)
				{
					NBiometricStatus status = task.Status;
					if (status != NBiometricStatus.Ok)
					{
						MessageBox.Show(string.Format(@"Could not create token face image! Status: {0}", status));
						_subject = null;
					}
					else
					{
						_tokenFace = _subject.Faces[1];
						faceViewToken.Face = _tokenFace;
						btnSaveImage.Enabled = true;
						ShowTokenAttributes();
					}
				}
				else
				{
					Utils.ShowException(task.Error);
					_subject = null;
				}
			}
		}

		private void ShowTokenAttributes()
		{
			var attributes = _tokenFace.Objects[0];
			lbQuality.Text = string.Format("Quality: {0}", attributes.Quality);
			lbSharpness.Text = string.Format("Sharpness score: {0}", attributes.Sharpness);
			lbUniformity.Text = string.Format("Background uniformity score: {0}", attributes.BackgroundUniformity);
			lbDensity.Text = string.Format("Grayscale density score: {0}", attributes.GrayscaleDensity);
			ShowAttributeLabels(true);
		}

		private void ShowAttributeLabels(bool show)
		{
			lbQuality.Visible = show;
			lbSharpness.Visible = show;
			lbUniformity.Visible = show;
			lbDensity.Visible = show;
		}

		#endregion

		#region Private form events

		private void TsbOpenOriginalClick(object sender, EventArgs e)
		{
			faceViewToken.Face = null;
			faceViewOriginal.Face = null;
			_tokenFace = null;
			btnSaveImage.Enabled = false;
			ShowAttributeLabels(false);

			openImageFileDlg.Filter = NImages.GetOpenFileFilterString(true, true);
			openImageFileDlg.Title = @"Open Face Image";
			if (openImageFileDlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					CreateImage(openImageFileDlg.FileName);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void BtnSaveImageClick(object sender, EventArgs e)
		{
			if (_tokenFace == null) return;

			saveFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					_tokenFace.Image.Save(saveFileDialog.FileName);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		#endregion

	}
}
