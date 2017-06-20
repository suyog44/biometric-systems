using System;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class AddFeatureForm : Form
	{
		public AddFeatureForm()
		{
			InitializeComponent();

			cbType.Items.Add("End");
			cbType.Items.Add("Bifurcation");
			cbType.Items.Add("Other");

			cbFeature.SelectedIndex = 0;
			cbType.SelectedIndex = 0;
		}

		public BdifFPMinutiaType MinutiaType
		{
			get
			{
				switch (cbType.SelectedIndex)
				{
					case 0:
						return BdifFPMinutiaType.End;
					case 1:
						return BdifFPMinutiaType.Bifurcation;
					case 2:
						return BdifFPMinutiaType.Other;
					default:
						return BdifFPMinutiaType.Unknown;
				}
			}
		}

		private void cbFeature_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbFeature.SelectedIndex == 0)
			{
				cbType.SelectedItem = 0;
				cbType.Enabled = true;
				labelType.Enabled = true;
			}
			else
			{
				cbType.SelectedText = "";
				cbType.Enabled = false;
				labelType.Enabled = false;
			}
		}
	}
}
