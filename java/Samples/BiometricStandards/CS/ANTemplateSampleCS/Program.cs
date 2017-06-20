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
			const string Address = "/local";
			const string Port = "5000";
			string[] components = { "Biometrics.Standards.Base",
									"Biometrics.Standards.Fingers",
									"Biometrics.Standards.FingerTemplates",
									"Biometrics.Standards.Palms",
									"Biometrics.Standards.PalmTemplates",
									"Biometrics.Standards.Irises",
									"Biometrics.Standards.Faces",
									"Biometrics.Standards.FingerCardTemplates",
									"Biometrics.Standards.Other",
									"Images.LosslessJPEG",
									"Images.JPEG2000"};

			try
			{
				int obtainedComponents = 0;
				foreach (var component in components)
				{
					if (NLicense.ObtainComponents(Address, Port, component))
					{
						obtainedComponents++;
					}
				}
				if (obtainedComponents == 0)
					throw new NotActivatedException("No licenses obtained!");
			}
			catch (Exception ex)
			{
				string message = string.Format("Failed to obtain licenses for components.\nError message: {0}", ex.Message);
				if (ex is System.IO.IOException)
				{
					message += "\n(Probably licensing service is not running. Use Activation Wizard to figure it out.)";
				}
				MessageBox.Show(message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			finally
			{
				foreach (var component in components)
				{
					NLicense.ReleaseComponents(component);
				}
			}
		}
	}
}
