using System;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANRecordCreateForm : Form
	{
		#region Private fields

		private ANTemplate _template;
		private ANRecord _createdRecord;

		#endregion

		#region Public constructor

		public ANRecordCreateForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		public ANTemplate Template
		{
			get
			{
				return _template;
			}
			set
			{
				_template = value;
			}
		}

		public ANRecord CreatedRecord
		{
			get
			{
				return _createdRecord;
			}
			protected set
			{
				_createdRecord = value;
			}
		}

		public int Idc
		{
			get
			{
				return (int)nudIdc.Value;
			}
			set
			{
				nudIdc.Value = value;
			}
		}

		#endregion

		#region Protected methods

		protected virtual ANRecord OnCreateRecord(ANTemplate template)
		{
			return null;
		}

		#endregion

		#region Private methods

		private void ANRecordCreateFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK)
			{
				return;
			}

			if (!ValidateChildren())
			{
				e.Cancel = true;
				return;
			}

			try
			{
				CreatedRecord = OnCreateRecord(Template);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				e.Cancel = true;
			}
		}

		#endregion
	}
}
