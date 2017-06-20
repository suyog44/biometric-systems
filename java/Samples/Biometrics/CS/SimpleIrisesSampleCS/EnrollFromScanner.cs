using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Devices;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class EnrollFromScanner : UserControl
	{
		#region Public constructor

		public EnrollFromScanner()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NDeviceManager _deviceManager;
		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private NIris _iris;

		#endregion Private fields

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set { _biometricClient = value; }
		}

		#endregion

		#region Public methods

		public void CancelScaning()
		{
			irisView.Iris = null;
			_biometricClient.Cancel();
		}

		#endregion

		#region Private methods

		private void UpdateScannerList()
		{
			lbScanners.BeginUpdate();
			try
			{
				lbScanners.Items.Clear();
				if (_deviceManager != null)
				{
					foreach (NDevice item in _deviceManager.Devices)
					{
						lbScanners.Items.Add(item);
					}
				}
			}
			finally
			{
				lbScanners.EndUpdate();
			}
		}

		private void EnableControls(bool capturing)
		{
			btnCancel.Enabled = capturing;
			btnScan.Enabled = !capturing;
			btnRefresh.Enabled = !capturing;
			rbLeft.Enabled = !capturing;
			rbRight.Enabled = !capturing;
			btnSaveImage.Enabled = !capturing && _iris != null && _iris.Status == NBiometricStatus.Ok;
			btnSaveTemplate.Enabled = !capturing && _subject != null && _subject.Status == NBiometricStatus.Ok;
			chbScanAutomatically.Enabled = !capturing;
			btnForce.Enabled = capturing && !chbScanAutomatically.Checked;
		}

		private void OnCaptureCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnCaptureCompleted), r);
			}
			else
			{
				NBiometricTask task = _biometricClient.EndPerformTask(r);
				EnableControls(false);
				NBiometricStatus status = task.Status;
				lblStatus.Text = status.ToString();

				// Check if extraction was canceled
				if (status == NBiometricStatus.Canceled) return;
				if (status != NBiometricStatus.Ok)
				{
					MessageBox.Show(string.Format("The template was not extracted: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					_subject = null;
					_iris = null;
					EnableControls(false);
				}
			}
		}

		#endregion

		#region Private form events

		private void BtnScanClick(object sender, EventArgs e)
		{
			if (_biometricClient.IrisScanner == null)
			{
				MessageBox.Show(@"Please select a scanner");
			}
			else
			{
				EnableControls(true);
				lblStatus.Text = String.Empty;

				// Create iris
				_iris = new NIris { Position = rbRight.Checked ? NEPosition.Right : NEPosition.Left };

				// Set Manual capturing mode if not automatic selected
				if (!chbScanAutomatically.Checked)
				{
					_iris.CaptureOptions = NBiometricCaptureOptions.Manual;
				}

				// Add iris to the subject and irisView
				_subject = new NSubject();
				_subject.Irises.Add(_iris);
				irisView.Iris = _iris;

				// Begin capturing
				NBiometricTask task = _biometricClient.CreateTask(NBiometricOperations.Capture | NBiometricOperations.CreateTemplate, _subject);
				_biometricClient.BeginPerformTask(task, OnCaptureCompleted, null);
			}
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			CancelScaning();
		}

		private void BtnRefreshClick(object sender, EventArgs e)
		{
			UpdateScannerList();
		}

		private void BtnSaveImageClick(object sender, EventArgs e)
		{
			if (_iris.Status == NBiometricStatus.Ok)
			{
				saveFileDialog.FileName = string.Empty;
				saveFileDialog.Filter = NImages.GetSaveFileFilterString();
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						_iris.Image.Save(saveFileDialog.FileName);
					}
					catch (Exception ex)
					{
						Utils.ShowException(ex);
					}
				}
			}
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			if (_subject.Status == NBiometricStatus.Ok)
			{
				saveFileDialog.FileName = string.Empty;
				saveFileDialog.Filter = string.Empty;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						File.WriteAllBytes(saveFileDialog.FileName, _subject.GetTemplateBuffer().ToArray());
					}
					catch (Exception ex)
					{
						Utils.ShowException(ex);
					}
				}
			}
		}

		private void EnrollFromScannerLoad(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				_deviceManager = _biometricClient.DeviceManager;
				UpdateScannerList();
			}
		}

		private void LbScannersSelectedIndexChanged(object sender, EventArgs e)
		{
			_biometricClient.IrisScanner = lbScanners.SelectedItem as NIrisScanner;
		}

		private void EnrollFromScannerVisibleChanged(object sender, EventArgs e)
		{
			if (_biometricClient != null) _biometricClient.Cancel();
			if (Visible && _biometricClient != null)
			{
				rbLeft.Checked = true;
			}
		}

		private void BtnForceClick(object sender, EventArgs e)
		{
			_biometricClient.Force();
		}

		#endregion

	}
}
