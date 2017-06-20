using System;

namespace Neurotec.Samples
{
	public partial class GeneralSettingsPage : Neurotec.Samples.SettingsPageBase
	{
		#region Public constructor

		public GeneralSettingsPage()
		{
			InitializeComponent();

			cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(12));
			cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(24));
			cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(36));
			cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(48));
			cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(60));
			cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(72));
		}

		#endregion

		#region Public methods

		public override void LoadSettings()
		{
			cbMatchingThreshold.SelectedItem = Utilities.MatchingThresholdToString(Client.MatchingThreshold);
			nudResultsCount.Value = Client.MatchingMaximalResultCount;
			chbFirstResult.Checked = Client.MatchingFirstResultOnly;
			chbMatchWithDetails.Checked = Client.MatchingWithDetails;

			base.LoadSettings();
		}

		public override void DefaultSettings()
		{
			Client.ResetProperty("Matching.Threshold");
			Client.ResetProperty("Matching.MaximalResultCount");
			Client.ResetProperty("Matching.FirstResultOnly");
			Client.MatchingWithDetails = true;
			base.DefaultSettings();
		}

		#endregion

		#region Public events

		private void CbMatchingThresholdSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.MatchingThreshold = Utilities.MatchingThresholdFromString((string)cbMatchingThreshold.SelectedItem);
		}

		private void NudResultsCountValueChanged(object sender, EventArgs e)
		{
			Client.MatchingMaximalResultCount = Convert.ToInt32(nudResultsCount.Value);
		}

		private void ChbMatchWithDetailsCheckedChanged(object sender, EventArgs e)
		{
			Client.MatchingWithDetails = chbMatchWithDetails.Checked;
		}

		private void ChbFirstResultCheckedChanged(object sender, EventArgs e)
		{
			Client.MatchingFirstResultOnly = chbFirstResult.Checked;
		}

		#endregion
	}
}
