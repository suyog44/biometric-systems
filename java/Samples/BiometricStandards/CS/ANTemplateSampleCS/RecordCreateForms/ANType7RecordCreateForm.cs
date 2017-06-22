using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType7RecordCreateForm : ANRecordCreateForm
	{
		#region Public properties

		public ANType7RecordCreateForm()
		{
			InitializeComponent();

			isrResolutionEditBox.PpmValue = ANType1Record.MinScanningResolution;
			irResolutionEditBox.PpmValue = ANType1Record.MinScanningResolution;
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			byte[] data = File.ReadAllBytes(tbImageDatePath.Text);
			NBuffer buffer = new NBuffer(data);

			ANType7Record record = new ANType7Record(ANTemplate.VersionCurrent, Idc)
			{
				Data = buffer
			};

			template.Records.Add(record);
			return record;
		}

		#endregion

		#region Private methods

		private void BtnImageDataClick(object sender, EventArgs e)
		{
			if (imageDataOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbImageDatePath.Text = imageDataOpenFileDialog.FileName;
			}
		}

		#endregion
	}
}
