using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Devices;

namespace Neurotec.Samples
{
	public partial class EnrollFromMicrophone : UserControl
	{
		#region Public constructor

		public EnrollFromMicrophone()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NDeviceManager _deviceManager;
		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private NVoice _voice;

		private bool _defaultExtractDependentFeatures;
		private bool _defaultExtractIndependentFeatures;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set
			{
				_biometricClient = value;
				_defaultExtractDependentFeatures = _biometricClient.VoicesExtractTextDependentFeatures;
				_defaultExtractIndependentFeatures = _biometricClient.VoicesExtractTextIndependentFeatures;
				chbExtractTextDependent.Checked = _defaultExtractDependentFeatures;
				chbExtractTextIndependent.Checked = _defaultExtractIndependentFeatures;
			}
		}

		#endregion

		#region Public methods

		public void StopCapturing()
		{
			_biometricClient.Cancel();
		}

		#endregion

		#region Private methods

		private void UpdateDeviceList()
		{
			lbMicrophones.BeginUpdate();
			try
			{
				lbMicrophones.Items.Clear();
				foreach (NDevice item in _deviceManager.Devices)
				{
					lbMicrophones.Items.Add(item);
				}
			}
			finally
			{
				lbMicrophones.EndUpdate();
			}
		}

		private void EnableControls(bool capturing)
		{
			var hasTemplate = !capturing && _subject != null && _subject.Status == NBiometricStatus.Ok;
			btnSaveTemplate.Enabled = hasTemplate;
			btnSaveVoice.Enabled = hasTemplate;
			btnStart.Enabled = !capturing;
			btnStop.Enabled = capturing;
			btnRefresh.Enabled = !capturing;
			gbOptions.Enabled = !capturing;
			lbMicrophones.Enabled = !capturing;
			chbCaptureAutomatically.Enabled = !capturing;
			btnForce.Enabled = !chbCaptureAutomatically.Checked && capturing;
		}

		private void OnCapturingCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnCapturingCompleted), r);
			}
			else
			{
				NBiometricTask task = _biometricClient.EndPerformTask(r);
				NBiometricStatus status = task.Status;
				// If Stop button was pushed
				if (status == NBiometricStatus.Canceled) return;

				if (status != NBiometricStatus.Ok && status != NBiometricStatus.SourceError && status != NBiometricStatus.TooFewSamples)
				{
					// Since capture failed start capturing again
					_voice.SoundBuffer = null;
					_biometricClient.BeginPerformTask(task, OnCapturingCompleted, null);
				}
				else
				{
					EnableControls(false);
				}
			}
		}

		#endregion

		#region Private form events

		private void EnrollFromMicrophoneLoad(object sender, EventArgs e)
		{
			if (DesignMode) return;
			voiceView.Voice = null;
			_deviceManager = _biometricClient.DeviceManager;
			UpdateDeviceList();
		}

		private void BtnStartClick(object sender, EventArgs e)
		{
			if (_biometricClient.VoiceCaptureDevice == null)
			{
				MessageBox.Show(@"Please select a microphone");
				return;
			}

			if (!chbExtractTextDependent.Checked && !chbExtractTextIndependent.Checked)
			{
				MessageBox.Show(@"No features configured to extract", @"Invalid options", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Set voice capture from stream
			_voice = new NVoice { CaptureOptions = NBiometricCaptureOptions.Stream };
			if (!chbCaptureAutomatically.Checked) _voice.CaptureOptions |= NBiometricCaptureOptions.Manual;
			_subject = new NSubject();
			_subject.Voices.Add(_voice);
			voiceView.Voice = _voice;

			NBiometricTask task = _biometricClient.CreateTask(NBiometricOperations.Capture | NBiometricOperations.Segment, _subject);
			_biometricClient.BeginPerformTask(task, OnCapturingCompleted, null);
			EnableControls(true);
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			StopCapturing();
			EnableControls(false);
		}

		private void BtnRefreshClick(object sender, EventArgs e)
		{
			UpdateDeviceList();
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
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

		private void BtnSaveVoiceClick(object sender, EventArgs e)
		{
			if (saveVoiceFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					// Voice buffer is saved in Child attribute after segmentation
					var voice = _voice.Objects[0].Child as NVoice;
					if (voice == null) return;
					File.WriteAllBytes(saveVoiceFileDialog.FileName, voice.SoundBuffer.Save().ToArray());
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void ChbExtractTextDependentCheckedChanged(object sender, EventArgs e)
		{
			_biometricClient.VoicesExtractTextDependentFeatures = chbExtractTextDependent.Checked;
		}

		private void ChbExtractTextIndependentCheckedChanged(object sender, EventArgs e)
		{
			_biometricClient.VoicesExtractTextIndependentFeatures = chbExtractTextIndependent.Checked;
		}

		private void LbMicrophonesSelectedIndexChanged(object sender, EventArgs e)
		{
			_biometricClient.VoiceCaptureDevice = lbMicrophones.SelectedItem as NMicrophone;
		}

		private void EnrollFromMicrophoneVisibleChanged(object sender, EventArgs e)
		{
			if (Visible && _biometricClient != null)
			{
				EnableControls(false);
				nudPhraseId.Value = 0;
			}
		}

		private void BtnForceClick(object sender, EventArgs e)
		{
			_biometricClient.ForceStart();
			btnForce.Enabled = false;
		}

		#endregion

	}
}
