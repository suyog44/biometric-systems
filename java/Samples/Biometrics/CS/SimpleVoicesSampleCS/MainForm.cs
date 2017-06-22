using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Public constructor

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;

		#endregion

		#region Private form events

		private void MainFormLoad(object sender, EventArgs e)
		{
			_biometricClient = new NBiometricClient {UseDeviceManager = true, BiometricTypes = NBiometricType.Voice};
			_biometricClient.Initialize();

			enrollFromFilePanel.BiometricClient = _biometricClient;
			enrollFromMicrophonePanel.BiometricClient = _biometricClient;
			verifyVoicePanel.BiometricClient = _biometricClient;
			identifyVoicePanel.BiometricClient = _biometricClient;
		}

		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (_biometricClient != null)
				_biometricClient.Cancel();
		}

		private void TabControlSelectedIndexChanged(object sender, EventArgs e)
		{
			if (_biometricClient != null)
			{
				_biometricClient.Cancel();
				_biometricClient.Reset();
			}
		}

		#endregion
	}
}
