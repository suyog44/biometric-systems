using System;
using System.Windows.Forms;

using Neurotec.Licensing;

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
			const string Components = "Biometrics.FingerExtraction,Images.Processing.FFT";

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					Utilities.ShowWarning("Could not obtain licenses for any of components: {0}", Components);
					return;
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Unhandled exception. Details: {0}", ex), @"LatentFingerprintSample", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				NLicense.ReleaseComponents(Components);
			}
		}
	}
}
