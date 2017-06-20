using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType9RecordCreateForm : ANRecordCreateForm
	{
		#region Public constructor

		public ANType9RecordCreateForm()
		{
			InitializeComponent();

			fromNFRecordPanel.Enabled = rbFromNFRecord.Checked;
			createEmptyPanel.Enabled = rbCreateEmpty.Checked;
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType9Record record;
			if (rbFromNFRecord.Checked)
			{
				byte[] nfRecordData;
				try
				{
					nfRecordData = File.ReadAllBytes(tbNFRecordPath.Text);
				}
				catch
				{
					throw new Exception(string.Format("Could not load NFRecord from {0}", tbNFRecordPath.Text));
				}

				NFRecord nfrecord = new NFRecord(nfRecordData);

				record = new ANType9Record(ANTemplate.VersionCurrent, Idc, chbFmtFlag.Checked, nfrecord);
				Template.Records.Add(record);
				return record;
			}
			else
			{
				record = new ANType9Record(ANTemplate.VersionCurrent, Idc)
				{
					ImpressionType = (BdifFPImpressionType)cbImpressionType.SelectedItem,
					MinutiaeFormat = chbFmtFlag.Checked,
					HasMinutiae = chbHasMinutiae.Checked
				};
				record.SetHasMinutiaeRidgeCounts(chbContainsRidgeCounts.Checked, chbHasRidgeCountsIndicator.Checked);
			}

			template.Records.Add(record);
			return record;
		}

		#endregion

		#region Private methods

		private void ANType9RecordCreateFormLoad(object sender, EventArgs e)
		{
			foreach (object value in Enum.GetValues(typeof(BdifFPImpressionType)))
			{
				cbImpressionType.Items.Add(value);
			}
			cbImpressionType.SelectedIndex = 0;
		}

		private void RbFromNFRecordCheckedChanged(object sender, EventArgs e)
		{
			fromNFRecordPanel.Enabled = rbFromNFRecord.Checked;
		}

		private void RbCreateEmptyCheckedChanged(object sender, EventArgs e)
		{
			createEmptyPanel.Enabled = rbCreateEmpty.Checked;
		}

		private void BtnBrowseNFRecordClick(object sender, EventArgs e)
		{
			if (nfRecordOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbNFRecordPath.Text = nfRecordOpenFileDialog.FileName;
			}
		}

		#endregion
	}
}
