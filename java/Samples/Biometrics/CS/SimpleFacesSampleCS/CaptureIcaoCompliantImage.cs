using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Devices;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class CaptureIcaoCompliantImage : UserControl
	{
		#region Public constructor

		public CaptureIcaoCompliantImage()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private NFace _face;
		private NFace _segmentedFace;
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
			bool hasTemplate = !capturing && _subject != null && _subject.Status == NBiometricStatus.Ok;
			btnSaveImage.Enabled = hasTemplate;
			btnSaveTemplate.Enabled = hasTemplate;
			btnStart.Enabled = !capturing;
			btnRefreshList.Enabled = !capturing;
			btnStop.Enabled = capturing;
			cbCameras.Enabled = !capturing;
			btnForce.Enabled = capturing;
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
					NBiometricTask task = _biometricClient.EndPerformTask(r);
					NBiometricStatus status = task.Status;

					if (task.Error != null)
						Utils.ShowException(task.Error);
					if (status == NBiometricStatus.Ok)
					{
						_segmentedFace = _subject.Faces[1];
						faceView.Face = _segmentedFace;
						icaoWarningView.Face = _segmentedFace;
					}

					lblStatus.Text = status.ToString();
					lblStatus.ForeColor = status == NBiometricStatus.Ok ? Color.Green : Color.Red;
					EnableControls(false);
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

		private void CaptureIcaoCompliantImageLoad(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				try
				{
					nViewZoomSlider2.View = faceView;
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

		private void CbCamerasSelectedIndexChanged(object sender, EventArgs e)
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
			_face = new NFace { CaptureOptions = NBiometricCaptureOptions.Stream };
			_subject = new NSubject();
			_subject.Faces.Add(_face);
			faceView.Face = _face;
			icaoWarningView.Face = _face;

			_biometricClient.FacesCheckIcaoCompliance = true;

			var task = _biometricClient.CreateTask(NBiometricOperations.Capture | NBiometricOperations.Segment | NBiometricOperations.CreateTemplate, _subject);
			_biometricClient.BeginPerformTask(task, OnCapturingCompleted, null);

			lblStatus.Text = string.Empty;
			EnableControls(true);
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			_biometricClient.Cancel();
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			if (saveTemplateDialog.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllBytes(saveTemplateDialog.FileName, _subject.GetTemplateBuffer().ToArray());
			}
		}

		private void BtnSaveImageClick(object sender, EventArgs e)
		{
			if (saveImageDialog.ShowDialog() == DialogResult.OK)
			{
				_segmentedFace.Image.Save(saveImageDialog.FileName);
			}
		}

		private void BtnForceClick(object sender, EventArgs e)
		{
			_biometricClient.Force();
		}
	}
}
