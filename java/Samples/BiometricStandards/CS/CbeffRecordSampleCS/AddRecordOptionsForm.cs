using System;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class AddRecordOptionsForm : Neurotec.Samples.CbeffRecordOptionsForm
	{
		#region public properties

		public BdifStandard Standard
		{
			get
			{
				return (BdifStandard)cbStandard.SelectedItem;
			}
		}

		#endregion

		#region public constructor

		public AddRecordOptionsForm()
		{
			InitializeComponent();

			cbStandard.Items.Add(BdifStandard.Ansi);
			cbStandard.Items.Add(BdifStandard.Iso);
			cbStandard.SelectedIndex = 0;
		}

		#endregion
	}
}
