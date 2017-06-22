using System;
using System.Collections.Generic;
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
			_biometricClient = new NBiometricClient {BiometricTypes = NBiometricType.Face, UseDeviceManager = true, FacesCheckIcaoCompliance = true };
			_biometricClient.Initialize();

			var page = new TabPage("Detect faces");
			var detectFaces = new DetectFaces {Dock = DockStyle.Fill, BiometricClient = _biometricClient};
			page.Controls.Add(detectFaces);
			tabControl.TabPages.Add(page);

			page = new TabPage("Enroll from image");
			var enrollFromImage = new EnrollFromImage {Dock = DockStyle.Fill, BiometricClient = _biometricClient};
			page.Controls.Add(enrollFromImage);
			tabControl.TabPages.Add(page);

			page = new TabPage("Enroll from camera");
			var enrollFromCamera = new EnrollFromCamera {Dock = DockStyle.Fill, BiometricClient = _biometricClient};
			page.Controls.Add(enrollFromCamera);
			tabControl.TabPages.Add(page);

			page = new TabPage("Identify face");
			var identifyFace = new IdentifyFace {Dock = DockStyle.Fill, BiometricClient = _biometricClient};
			page.Controls.Add(identifyFace);
			tabControl.TabPages.Add(page);

			page = new TabPage("Verify face");
			var verifyFace = new VerifyFace {Dock = DockStyle.Fill, BiometricClient = _biometricClient};
			page.Controls.Add(verifyFace);
			tabControl.TabPages.Add(page);

			page = new TabPage("Match multiple faces");
			var matchMultipleFaces = new MatchMultipleFaces {Dock = DockStyle.Fill, BiometricClient = _biometricClient};
			page.Controls.Add(matchMultipleFaces);
			tabControl.TabPages.Add(page);

			page = new TabPage("Create token face image");
			var createTokenImage = new CreateTokenFaceImage { Dock = DockStyle.Fill, BiometricClient = _biometricClient };
			page.Controls.Add(createTokenImage);
			tabControl.TabPages.Add(page);

			page = new TabPage("Generalize faces");
			var generalizeFaces = new GeneralizeFaces { Dock = DockStyle.Fill, BiometricClient = _biometricClient };
			page.Controls.Add(generalizeFaces);
			tabControl.TabPages.Add(page);

			page = new TabPage("Capture ICAO image");
			var icaoPage = new CaptureIcaoCompliantImage { Dock = DockStyle.Fill, BiometricClient = _biometricClient };
			page.Controls.Add(icaoPage);
			tabControl.TabPages.Add(page);
		}

		private void TabControlSelecting(object sender, TabControlCancelEventArgs e)
		{
			if (_biometricClient != null)
			{
				_biometricClient.Reset();
				_biometricClient.Cancel();
			}
		}

		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (_biometricClient != null)
				_biometricClient.Cancel();
		}

		#endregion
	}
}
