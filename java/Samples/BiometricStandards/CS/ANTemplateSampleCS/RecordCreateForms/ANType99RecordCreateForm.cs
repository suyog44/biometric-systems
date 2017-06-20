using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType99RecordCreateForm : ANRecordCreateForm
	{
		#region Public constructor

		public ANType99RecordCreateForm()
		{
			InitializeComponent();

			cbVersion.Items.Add(ANType99Record.HeaderVersion10);
			cbVersion.Items.Add(ANType99Record.HeaderVersion11);
			cbVersion.SelectedIndex = cbVersion.Items.Count - 1;

			nudBfo.Maximum = nudBft.Maximum = ushort.MaxValue;

			foreach (object value in Enum.GetValues(typeof(ANBiometricType)))
			{
				chlbBiometricType.Items.Add(value);
			}
			chlbBiometricType.SetItemChecked(0, true);
		}

		#endregion

		#region Private fields

		private bool _isUpdating;

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			byte[] data = File.ReadAllBytes(tbDataPath.Text);
			NBuffer buffer = new NBuffer(data);

			ANType99Record record = new ANType99Record(ANTemplate.VersionCurrent, Idc)
			{
				BiometricType = GetBiometricType(),
				BdbFormatOwner = (ushort)nudBfo.Value,
				BdbFormatType = (ushort)nudBft.Value,
				Data = buffer
			};

			template.Records.Add(record);
			return record;
		}

		protected ANBiometricType GetBiometricType()
		{
			ANBiometricType value = ANBiometricType.NoInformationGiven;
			CheckedListBox.CheckedIndexCollection indices = chlbBiometricType.CheckedIndices;
			for (int i = 0; i < indices.Count; i++)
			{
				int index = indices[i];
				value |= (ANBiometricType)chlbBiometricType.Items[index];
			}
			return value;
		}

		#endregion

		#region Private form events

		private void BtnBrowseDataClick(object sender, EventArgs e)
		{
			if (dataOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbDataPath.Text = dataOpenFileDialog.FileName;
			}
		}

		private void TbSrcValidating(object sender, CancelEventArgs e)
		{
			if (tbSrc.Text.Length < ANAsciiBinaryRecord.MinSourceAgencyLength
				|| tbSrc.Text.Length > ANAsciiBinaryRecord.MaxSourceAgencyLengthV4)
			{
				errorProvider.SetError(tbSrc, string.Format("Source agency field length must be between {0} and {1} characters.",
					ANAsciiBinaryRecord.MinSourceAgencyLength, ANAsciiBinaryRecord.MaxSourceAgencyLengthV4));
				e.Cancel = true;
			}
			else
			{
				errorProvider.SetError(tbSrc, null);
			}
		}

		private void ChlbBiometricTypeImteCheck(object sender, ItemCheckEventArgs e)
		{
			if (_isUpdating) return;

			_isUpdating = true;
			if (e.Index == 0)
			{
				if (e.NewValue == CheckState.Checked)
				{
					for (int i = 1; i < chlbBiometricType.Items.Count; i++)
					{
						chlbBiometricType.SetItemChecked(i, false);
					}
				}
			}
			else
			{
				chlbBiometricType.SetItemChecked(0, false);
			}
			_isUpdating = false;

		}

		#endregion
	}
}
