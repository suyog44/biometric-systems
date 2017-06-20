using System;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;
using Neurotec.Samples.Properties;

namespace Neurotec.Samples
{
	public partial class OptionsForm : Form
	{
		#region Public constructor

		public OptionsForm()
		{
			InitializeComponent();

			ANValidationLevel[] values = (ANValidationLevel[])Enum.GetValues(typeof(ANValidationLevel));
			foreach (ANValidationLevel item in values)
			{
				cbValidationLevel.Items.Add(item);
			}
			cbValidationLevel.SelectedIndex = 0;
		}

		#endregion

		#region Public properties

		public bool UseNistMinutiaeNeighbors
		{
			get { return chbUseNistMinutiaeNeighbors.Checked; }
			set { chbUseNistMinutiaeNeighbors.Checked = value; }
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

		public bool NonStrictRead
		{
			get { return chbNonStrinctRead.Checked; }
			set { chbNonStrinctRead.Checked = value; }
		}

		public bool MergeDuplicateFields
		{
			get { return chbMergeDuplicateFields.Checked; }
			set { chbMergeDuplicateFields.Checked = value; }
		}

		public bool LeaveInvalidRecordsUnvalidated
		{
			get { return chbLeaveInvalidUnvalidated.Checked; }
			set { chbLeaveInvalidUnvalidated.Checked = value; }
		}

		public bool RecoverFromBinaryData
		{
			get { return chbRecover.Checked; }
			set { chbRecover.Checked = value; }
		}

		public ANValidationLevel ValidationLevel
		{
			get { return (ANValidationLevel)cbValidationLevel.SelectedItem; }
			set { cbValidationLevel.SelectedItem = value; }
		}

		public uint Flags
		{
			get
			{
				uint value = 0;
				if (UseNistMinutiaeNeighbors) value |= ANTemplate.FlagUseNistMinutiaNeighbors;
				if (UseTwoDigitIdc) value |= ANTemplate.FlagUseTwoDigitIdc;
				if (UseTwoDigitFieldNumber) value |= ANTemplate.FlagUseTwoDigitFieldNumber;
				if (UseTwoDigitFieldNumberType1) value |= ANTemplate.FlagUseTwoDigitFieldNumberType1;
				if (NonStrictRead) value |= BdifTypes.FlagNonStrictRead;
				if (MergeDuplicateFields) value |= ANRecord.FlagMergeDuplicateFields;
				if (LeaveInvalidRecordsUnvalidated) value |= ANTemplate.FlagLeaveInvalidRecordsUnvalidated;
				if (RecoverFromBinaryData) value |= ANRecord.FlagRecoverFromBinaryData;
				return value;
			}
			set
			{
				UseNistMinutiaeNeighbors = (value & ANTemplate.FlagUseNistMinutiaNeighbors) != 0;
				UseTwoDigitIdc = (value & ANTemplate.FlagUseTwoDigitIdc) != 0;
				UseTwoDigitFieldNumber = (value & ANTemplate.FlagUseTwoDigitFieldNumber) != 0;
				UseTwoDigitFieldNumberType1 = (value & ANTemplate.FlagUseTwoDigitFieldNumberType1) != 0;
				NonStrictRead = (value & BdifTypes.FlagNonStrictRead) != 0;
				MergeDuplicateFields = (value & ANRecord.FlagMergeDuplicateFields) != 0;
				LeaveInvalidRecordsUnvalidated = (value & ANTemplate.FlagLeaveInvalidRecordsUnvalidated) != 0;
				RecoverFromBinaryData = (value & ANRecord.FlagRecoverFromBinaryData) != 0;
			}
		}

		#endregion

		#region Private methods

		private void LoadSettings()
		{
			ValidationLevel = Settings.Default.ValidationLevel;
			UseNistMinutiaeNeighbors = Settings.Default.UseNistMinutiaNeighbors;
			UseTwoDigitIdc = Settings.Default.UseTwoDigitIdc;
			UseTwoDigitFieldNumber = Settings.Default.UseTwoDigitFieldNumber;
			UseTwoDigitFieldNumberType1 = Settings.Default.UseTwoDigitFieldNumberType1;
			NonStrictRead = Settings.Default.NonStrictRead;
			MergeDuplicateFields = Settings.Default.MergeDuplicateFields;
			RecoverFromBinaryData = Settings.Default.RecoverFromBinaryData;
			LeaveInvalidRecordsUnvalidated = Settings.Default.LeaveInvalidRecordsUnvalidated;
		}

		private void SaveSettings()
		{
			Settings.Default.ValidationLevel = ValidationLevel;
			Settings.Default.UseNistMinutiaNeighbors = UseNistMinutiaeNeighbors;
			Settings.Default.UseTwoDigitIdc = UseTwoDigitIdc;
			Settings.Default.UseTwoDigitFieldNumber = UseTwoDigitFieldNumber;
			Settings.Default.UseTwoDigitFieldNumberType1 = UseTwoDigitFieldNumberType1;
			Settings.Default.NonStrictRead = NonStrictRead;
			Settings.Default.MergeDuplicateFields = MergeDuplicateFields;
			Settings.Default.RecoverFromBinaryData = RecoverFromBinaryData;
			Settings.Default.LeaveInvalidRecordsUnvalidated = LeaveInvalidRecordsUnvalidated;
			Settings.Default.Save();
		}

		#endregion

		#region Private form events

		private void BtnOkClick(object sender, EventArgs e)
		{
			SaveSettings();
			DialogResult = DialogResult.OK;
		}

		private void OpenOptionsFormLoad(object sender, EventArgs e)
		{
			LoadSettings();
		}

		#endregion
	}
}
