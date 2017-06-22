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
			const string Components = "Biometrics.Standards.Fingers";

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					MessageBox.Show(string.Format("Could not obtain licenses for components: {0}", Components), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
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
				NLicense.ReleaseComponents(Components);
			}
			catch {}
		}
	}
}
