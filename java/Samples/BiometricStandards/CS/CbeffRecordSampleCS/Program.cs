using System;
using System.Windows.Forms;
using Neurotec.Licensing;

namespace Neurotec.Samples
{
	static class Program
	{
		private static readonly string[] Components = 
		{
			"Biometrics.Standards.Base",
			"Biometrics.Standards.Irises",
			"Biometrics.Standards.Fingers",
			"Biometrics.Standards.Faces"
		};

		[STAThread]
		static void Main()
		{
			int obtainedComponentCounter = 0;
			try
			{
				foreach (string component in Components)
				{
					if (NLicense.ObtainComponents("/local", 5000, component))
					{
						obtainedComponentCounter++;
					}
				}
				if (obtainedComponentCounter == 0)
					throw new NotActivatedException(@"Could not obtain any of components.");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());

			try
			{
				foreach (string component in Components)
				{
					NLicense.ReleaseComponents(component);
				}
			}
			catch { }
		}
	}
}
