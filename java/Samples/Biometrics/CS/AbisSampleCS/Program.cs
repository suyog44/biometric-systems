using System;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			const string Address = "/local";
			const string Port = "5000";
			bool retry;
			string[] licenses = 
			{
				"Biometrics.FingerExtraction",
				"Biometrics.PalmExtraction",
				"Biometrics.FaceExtraction",
				"Biometrics.IrisExtraction",
				"Biometrics.VoiceExtraction",
				"Biometrics.FingerMatchingFast",
				"Biometrics.FingerMatching",
				"Biometrics.PalmMatchingFast",
				"Biometrics.PalmMatching",
				"Biometrics.VoiceMatching",
				"Biometrics.FaceMatchingFast",
				"Biometrics.FaceMatching",
				"Biometrics.IrisMatchingFast",
				"Biometrics.IrisMatching",
				"Biometrics.FingerQualityAssessment",
				"Biometrics.FingerSegmentation",
				"Biometrics.FingerSegmentsDetection",
				"Biometrics.PalmSegmentation",
				"Biometrics.FaceSegmentation",
				"Biometrics.IrisSegmentation",
				"Biometrics.VoiceSegmentation",
				"Biometrics.Standards.Fingers",
				"Biometrics.Standards.FingerTemplates",
				"Biometrics.Standards.Faces",
				"Biometrics.Standards.Irises",
				"Devices.Cameras",
				"Devices.FingerScanners",
				"Devices.IrisScanners",
				"Devices.PalmScanners",
				"Devices.Microphones",
				"Images.WSQ",
				"Media"
			};

			do
			{
				try
				{
					retry = false;
					foreach (string license in licenses)
					{
						Licensing.NLicense.ObtainComponents(Address, Port, license);
					}
				}
				catch (Exception ex)
				{
					string message = string.Format("Failed to obtain licenses for components.\nError message: {0}", ex.Message);
					if (ex is System.IO.IOException)
					{
						message += "\n(Probably licensing service is not running. Use Activation Wizard to figure it out.)";
					}
					if (MessageBox.Show(message, @"Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
					{
						retry = true;
					}
					else
					{
						retry = false;
						return;
					}
				}
			}
			while (retry);

			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			finally
			{
				foreach (string license in licenses)
				{
					Licensing.NLicense.ReleaseComponents(license);
				}
			}
		}
	}
}
