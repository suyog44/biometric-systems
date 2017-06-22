using System;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class AddIrisForm : Form
	{
		public AddIrisForm(NVersion version)
		{
			InitializeComponent();

			if (version == IIRecord.VersionAnsiCurrent || version == IIRecord.VersionIsoCurrent)
			{
				cbIrisPosition.Items.Add(Enum.GetName(typeof(BdifEyePosition), BdifEyePosition.Left));
				cbIrisPosition.Items.Add(Enum.GetName(typeof(BdifEyePosition), BdifEyePosition.Right));
			}
			else
				cbIrisPosition.Items.AddRange(Enum.GetNames(typeof(BdifEyePosition)));
			cbIrisPosition.SelectedIndex = 0;
		}

		public BdifEyePosition IrisPosition
		{
			get
			{
				return (BdifEyePosition)Enum.Parse(typeof(BdifEyePosition), cbIrisPosition.SelectedItem.ToString());
			}
			set
			{
				cbIrisPosition.SelectedItem = Enum.GetName(typeof(BdifEyePosition), value);
			}
		}
	}
}
