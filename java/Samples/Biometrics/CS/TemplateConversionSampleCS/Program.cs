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
			const string Components = "Biometrics.Standards.Fingers,Biometrics.Standards.Faces,Biometrics.Standards.Irises,Biometrics.Standards.Palms";

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					MessageBox.Show(string.Format("Could not obtain licenses for any of components: {0}", Components));
					return;
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), @"Template Conversion", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				NLicense.ReleaseComponents(Components);
			}
		}
	}
}
