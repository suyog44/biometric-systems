using System;
using System.ComponentModel;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType1RecordCreateForm : ANRecordCreateForm
	{
		#region Public constructor

		public ANType1RecordCreateForm()
		{
			InitializeComponent();

			nudIdc.Enabled = false;
			nudIdc.Minimum = -1;
			nudIdc.Value = -1;

			Array values = Enum.GetValues(typeof(ANValidationLevel));
			foreach (object item in values)
			{
				cbValidationLevel.Items.Add(item);
			}
			cbValidationLevel.SelectedIndex = 0;
		}

		#endregion

		#region Public properties

		public string TransactionType
		{
			get { return tbTransactionType.Text; }
			set { tbTransactionType.Text = value; }
		}

		public string DestinationAgency
		{
			get { return tbDestinationAgency.Text; }
			set { tbDestinationAgency.Text = value; }
		}

		public string OriginatingAgency
		{
			get { return tbOriginatingAgency.Text; }
			set { tbOriginatingAgency.Text = value; }
		}

		public string TransactionControl
		{
			get { return tbTransactionControlId.Text; }
			set { tbTransactionControlId.Text = value; }
		}

		public ANValidationLevel ValidationLevel
		{
			get { return (ANValidationLevel)cbValidationLevel.SelectedItem; }
			set { cbValidationLevel.SelectedItem = value; }
		}

		public bool UseNistMinutiaNeighbors
		{
			get { return chbUseNistMinutiaNeighbors.Checked; }
			set { chbUseNistMinutiaNeighbors.Checked = value; }
		}

		public bool UseTwoDigitIdc
		{
			get { return chbUseTwoDigitIdc.Checked; }
			set { chbUseTwoDigitIdc.Checked = value; }
		}

		public bool UseTwoDigitFieldNumber
		{
			get { return chbUseTwoDigitFieldNumber.Checked; }
			set { chbUseTwoDigitFieldNumber.Checked = value; }
		}

		public bool UseTwoDigitFieldNumberType1
		{
			get { return chbUseTwoDigitFieldNumberType1.Checked; }
			set { chbUseTwoDigitFieldNumberType1.Checked = value; }
		}

		#endregion

		#region Private form events

		private void TbTransactionTypeValidating(object sender, CancelEventArgs e)
		{
			string value = ((TextBox)sender).Text;

			if (value.Length < ANType1Record.MinTransactionTypeLengthV4 || value.Length > ANType1Record.MaxTransactionTypeLengthV4)
			{
				errorProvider.SetError((TextBox)sender,
					string.Format("Transaction type value must be {0} to {1} characters long",
					ANType1Record.MinTransactionTypeLengthV4, ANType1Record.MaxTransactionTypeLengthV4));
				e.Cancel = true;
			}
			else
			{
				errorProvider.SetError((TextBox)sender, null);
			}
		}

		private void CbValidationLevelSelectedIndexChanged(object sender, EventArgs e)
		{
			bool isStandard = ValidationLevel == ANValidationLevel.Standard;
			tbDestinationAgency.Enabled = tbOriginatingAgency.Enabled = tbTransactionControlId.Enabled = tbTransactionType.Enabled = isStandard;
		}

		#endregion
	}
}
