using System;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class ExtractionSettingsForm : Form
	{
		#region Public constructor

		public ExtractionSettingsForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		public byte QualityThreshold
		{
			get { return Convert.ToByte(nudThreshold.Value); }
			set { nudThreshold.Value = value; }
		}

		#endregion

		#region Private events

		private void BtnDefaultThresholdClick(object sender, EventArgs e)
		{
			nudThreshold.Value = 39;
		}

		#endregion
	}
}
