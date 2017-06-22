using System;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class StartTab : Neurotec.Samples.TabPageContentBase
	{
		#region Public constructor

		public StartTab()
		{
			InitializeComponent();
			TabName = "Start page";
		}

		#endregion

		#region Private form events

		private void BtnNewSubjectClick(object sender, EventArgs e)
		{
			TabController.CreateNewSubjectTab(new NSubject());
		}

		private void BtnAboutClick(object sender, EventArgs e)
		{
			TabController.ShowAbout();
		}

		private void BtnSettingsClick(object sender, EventArgs e)
		{
			TabController.ShowSettings();
		}

		private void BtnOpenClick(object sender, EventArgs e)
		{
			TabController.OpenSubject();
		}

		private void BtnChangeDbClick(object sender, EventArgs e)
		{
			TabController.ShowChangeDatabase();
		}

		#endregion
	}
}
