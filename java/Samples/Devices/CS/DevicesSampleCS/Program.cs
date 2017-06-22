using System;
using System.Linq;
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
			string[] components =
			{
				"Biometrics.FingerDetection",
				"Biometrics.PalmDetection",
				"Devices.FingerScanners",
				"Devices.PalmScanners",
				"Devices.Cameras",
				"Biometrics.IrisDetection",
				"Devices.IrisScanners",
				"Devices.Microphones"
			};

			try
			{
				bool anyObtained = components.Aggregate(false, (current, component) => current | NLicense.ObtainComponents("/local", 5000, component));
				if (!anyObtained)
				{
					MessageBox.Show(string.Format("Could not obtain licenses for any of components: {0}", components));
					return;
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				foreach (string component in components)
				{
					NLicense.ReleaseComponents(component);
				}
			}
		}
	}
}
