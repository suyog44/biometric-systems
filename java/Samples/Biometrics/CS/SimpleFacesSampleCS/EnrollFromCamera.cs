using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Devices;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class EnrollFromCamera : UserControl
	{
		#region Public constructor

		public EnrollFromCamera()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private NDeviceManager _deviceManager;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set { _biometricClient = value; }
		}

		#endregion

		#region Private methods

		private void UpdateCameraList()
		{
			cbCameras.BeginUpdate();
			try
			{
				cbCameras.Items.Clear();
				foreach (NDevice device in _deviceManager.Devices)
				{
					cbCameras.Items.Add(device);
				}

				if (_biometricClient.FaceCaptureDevice == null && cbCameras.Items.Count > 0)
				{
					cbCameras.SelectedIndex = 0;
					return;
				}

				if (_biometricClient.FaceCaptureDevice != null)
				{
					cbCameras.SelectedIndex = cbCameras.Items.IndexOf(_biometricClient.FaceCaptureDevice);
				}
			}
			finally
			{
				cbCameras.EndUpdate();
			}
		}

		private void EnableControls(bool capturing)
		{
			var hasTemplate = !capturing && _subject != null && _subject.Status == NBiometricStatus.Ok;
			btnSaveImage.Enabled = hasTemplate;
			btnSaveTemplate.Enabled = hasTemplate;
			btnStart.Enabled = !capturing;
			btnRefreshList.Enabled = !capturing;
			btnStop.Enabled = capturing;
			cbCameras.Enabled = !capturing;
			btnStartExtraction.Enabled = capturing && !chbCaptureAutomatically.Checked;
			chbCaptureAutomatically.Enabled = !capturing;
			chbCheckLiveness.Enabled = !capturing;
		}

		private void OnCapturingCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnCapturingCompleted), r);
			}
			else
			{
				try
				{
					NBiometricStatus status = _biometricClient.EndCapture(r);
					// If Stop button was pushed
					if (status == NBiometricStatus.Canceled) return;

					lblStatus.Text = status.ToString();
					if (status != NBiometricStatus.Ok)
					{
						// Since capture failed start capturing again
						_subject.Faces[0].Image = null;
						_biometricClient.BeginCapture(_subject, OnCapturingCompleted, null);
					}
					else
					{
						EnableControls(false);
					}
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
					lblStatus.Text = string.Empty;
					EnableControls(false);
				}
			}
		}

		#endregion

		#region Private form events

		private void EnrollFromCameraLoad(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				try
				{
					lblStatus.Text = string.Empty;
					_deviceManager = _biometricClient.DeviceManager;
					saveImageDialog.Filter = NImages.GetSaveFileFilterString();
					UpdateCameraList();
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void BtnRefreshListClick(object sender, EventArgs e)
		{
			UpdateCameraList();
		}

		private void CbCameraSelectedIndexChanged(object sender, EventArgs e)
		{
			_biometricClient.FaceCaptureDevice = cbCameras.SelectedItem as NCamera;
		}

		private void BtnStartClick(object sender, EventArgs e)
		{
			if (_biometricClient.FaceCaptureDevice == null)
			{
				MessageBox.Show(@"Please select camera from the list");
				return;
			}
			// Set face capture from stream
			var face = new NFace { CaptureOptions = NBiometricCaptureOptions.Stream };
			if (!chbCaptureAutomatically.Checked) face.CaptureOptions |= NBiometricCaptureOptions.Manual;
			_subject = new NSubject();
			_subject.Faces.Add(face);
			facesView.Face = face;

			// Begin capturing faces
			_biometricClient.BeginCapture(_subject, OnCapturingCompleted, null);

			lblStatus.Text = string.Empty;
			EnableControls(true);
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			_biometricClient.Cancel();
			EnableControls(false);
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			if (saveTemplateDialog.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllBytes(saveTemplateDialog.FileName, _subject.GetTemplateBuffer().ToArray());
			}
		}

		private void BtnStartExtractionClick(object sender, EventArgs e)
		{
			lblStatus.Text = @"Extracting ...";
			// Begin extraction
			_biometricClient.ForceStart();
		}

		private void BtnSaveImageClick(object sender, EventArgs e)
		{
			if (saveImageDialog.ShowDialog() == DialogResult.OK)
			{
				_subject.Faces[0].Image.Save(saveImageDialog.FileName);
			}
		}

		private void EnrollFromCameraVisibleChanged(object sender, EventArgs e)
		{
			if (Visible && _biometricClient != null)
			{
				EnableControls(false);
			}
		}

		private void ChbCheckLivenessCheckedChanged(object sender, EventArgs e)
		{
			// set liveness check mode
			_biometricClient.FacesLivenessMode = chbCheckLiveness.Checked ? NLivenessMode.PassiveAndActive : NLivenessMode.None;
		}

		#endregion

	}
}
