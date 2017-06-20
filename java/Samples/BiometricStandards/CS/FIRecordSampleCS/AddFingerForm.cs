using System;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class AddFingerForm:Form
	{
		public AddFingerForm()
		{
			InitializeComponent();

			cbFingerPosition.Items.AddRange(Enum.GetNames(typeof(BdifFPPosition)));
			cbFingerPosition.SelectedIndex = 0;
		}

		public BdifFPPosition FingerPosition
		{
			get
			{
				return (BdifFPPosition)Enum.Parse(typeof(BdifFPPosition), cbFingerPosition.SelectedItem.ToString());
			}
			set { cbFingerPosition.SelectedText = value.ToString(); }
		}
	}
}
