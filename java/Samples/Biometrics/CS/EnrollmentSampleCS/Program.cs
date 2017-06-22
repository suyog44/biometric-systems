using System;
using System.Windows.Forms;
using Neurotec.Samples.Forms;

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
			const string Components = "Biometrics.FingerExtraction,Biometrics.FingerSegmentation";
			const string OptionalComponents = "Biometrics.FingerQualityAssessmentBase,Devices.Cameras";

			try
			{
				if (!Licensing.NLicense.ObtainComponents(Address, Port, Components))
				{
					Utilities.ShowWarning("Could not obtain licenses for components: {0}", Components);
					return;
				}

				Licensing.NLicense.ObtainComponents(Address, Port, OptionalComponents);
			}
			catch (Exception ex)
			{
				string message = string.Format("Failed to obtain licenses for components.\nError message: {0}", ex.Message);
				if (ex is System.IO.IOException)
				{
					message += "\n(Probably licensing service is not running. Use Activation Wizard to figure it out.)";
				}
				Utilities.ShowWarning(message);
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
