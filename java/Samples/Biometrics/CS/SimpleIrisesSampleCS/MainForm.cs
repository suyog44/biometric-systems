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
			_biometricClient = new NBiometricClient { UseDeviceManager = true, BiometricTypes = NBiometricType.Iris };
			_biometricClient.Initialize();

			var page = new TabPage("Enroll from image");
			var enrollFromImage = new EnrollFromImage { Dock = DockStyle.Fill, BiometricClient = _biometricClient };
			page.Controls.Add(enrollFromImage);
			tabControl.TabPages.Add(page);

			page = new TabPage("Enroll from scanner");
			var enrollFromScanner = new EnrollFromScanner { Dock = DockStyle.Fill, BiometricClient = _biometricClient };
			page.Controls.Add(enrollFromScanner);
			tabControl.TabPages.Add(page);

			page = new TabPage("Verify iris");
			var verifyIris = new VerifyIris { Dock = DockStyle.Fill, BiometricClient = _biometricClient };
			page.Controls.Add(verifyIris);
			tabControl.TabPages.Add(page);

			page = new TabPage("Identify iris");
			var identifyIris = new IdentifyIris { Dock = DockStyle.Fill, BiometricClient = _biometricClient };
			page.Controls.Add(identifyIris);
			tabControl.TabPages.Add(page);

			page = new TabPage("Segment iris");
			var segmentIris = new SegmentIris { Dock = DockStyle.Fill, BiometricClient = _biometricClient };
			page.Controls.Add(segmentIris);
			tabControl.TabPages.Add(page);
		}

		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (_biometricClient != null)
				_biometricClient.Cancel();
		}

		private void TabControlSelecting(object sender, TabControlCancelEventArgs e)
		{
			if (_biometricClient != null)
			{
				_biometricClient.Reset();
				_biometricClient.Cancel();
			}
		}

		#endregion

	}
}
