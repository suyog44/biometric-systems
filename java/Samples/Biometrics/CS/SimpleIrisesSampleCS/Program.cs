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
			const string Components = "Biometrics.IrisExtraction,Biometrics.IrisSegmentation,Biometrics.IrisMatching,Devices.IrisScanners";
			try
			{
				foreach (string component in Components.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries))
				{
					NLicense.ObtainComponents(LicensePanel.Address, LicensePanel.Port, component);
				}
				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				Utils.ShowException(ex);
			}
			finally
			{
				NLicense.ReleaseComponents(Components);
			}
		}
	}
}
