using System;
using System.Drawing;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class RecordTypeForm : Form
	{
		#region Private fields

		private NVersion _version = (NVersion)0;
		private bool _useSelectMode = true;

		#endregion

		#region Public constructor

		public RecordTypeForm()
		{
			InitializeComponent();

			OnUseSelectModeChanged();
		}

		#endregion

		#region Private methods

		private void UpdateRecords()
		{
			lvRecordType.BeginUpdate();
			lvRecordType.Items.Clear();
			foreach (ANRecordType recordType in ANRecordType.Types)
			{
				NVersion recordVersion = recordType.Version;
				if (_useSelectMode && (recordType.Number == 1 || recordVersion < _version)) continue;

				ListViewItem recordTypeItem = new ListViewItem(recordType.Number.ToString());
				recordTypeItem.Tag = recordType;
				recordTypeItem.SubItems.Add(recordType.Name);
				if (!_useSelectMode)
				{
					recordTypeItem.SubItems.Add(recordType.DataType.ToString());
					recordTypeItem.SubItems.Add(recordVersion.ToString());
				}
				lvRecordType.Items.Add(recordTypeItem);
			}
			lvRecordType.EndUpdate();
		}

		private void OnVersionChanged()
		{
			UpdateRecords();
		}

		private void OnUseSelectModeChanged()
		{
			UpdateRecords();
			if (_useSelectMode)
			{
				int index = lvRecordType.Columns.IndexOf(recordTypeDataTypeColumnHeader);
				if (index != -1) lvRecordType.Columns.RemoveAt(index);
				index = lvRecordType.Columns.IndexOf(recordTypeVersionColumnHeader);
				if (index != -1) lvRecordType.Columns.RemoveAt(index);
			}
			else
			{
				int index = lvRecordType.Columns.IndexOf(recordTypeDataTypeColumnHeader);
				if (index == -1) lvRecordType.Columns.Add(recordTypeDataTypeColumnHeader);
				index = lvRecordType.Columns.IndexOf(recordTypeVersionColumnHeader);
				if (index == -1) lvRecordType.Columns.Add(recordTypeVersionColumnHeader);
			}
			ClientSize = new Size(_useSelectMode ? 380 : 530, ClientSize.Height);
			btnShowFields.Visible = !_useSelectMode;
			btnOk.Visible = _useSelectMode;
			btnCancel.Text = _useSelectMode ? "Cancel" : "Close";
			OnSelectedRecordTypeChanged();
		}

		private void OnSelectedRecordTypeChanged()
		{
			ANRecordType selectedRecordType = RecordType;
			btnShowFields.Enabled = !_useSelectMode && selectedRecordType != null;
			btnOk.Enabled = _useSelectMode && selectedRecordType != null;
		}

		private void ShowFields()
		{
			FieldNumberForm form = new FieldNumberForm();
			form.Text = "Fields";
			form.UseSelectMode = false;
			form.RecordType = RecordType;
			form.ShowDialog();
		}

		#endregion

		#region Public properties

		public NVersion Version
		{
			get
			{
				return _version;
			}
			set
			{
				if (_version != value)
				{
					_version = value;
					OnVersionChanged();
				}
			}
		}

		public bool UseSelectMode
		{
			get
			{
				return _useSelectMode;
			}
			set
			{
				if (_useSelectMode != value)
				{
					_useSelectMode = value;
					OnUseSelectModeChanged();
				}
			}
		}

		public ANRecordType RecordType
		{
			get
			{
				return lvRecordType.SelectedItems.Count == 0 ? null : (ANRecordType)lvRecordType.SelectedItems[0].Tag;
			}
			set
			{
				if (value == null) lvRecordType.SelectedItems.Clear();
				else lvRecordType.Items[ANRecordType.Types.IndexOf(value)].Selected = true;
			}
		}

		#endregion

		#region private form events

		private void LvRecordTypeSelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedRecordTypeChanged();
		}

		private void LvRecordTypeDoubleClick(object sender, EventArgs e)
		{
			if (RecordType != null)
			{
				if (_useSelectMode) DialogResult = DialogResult.OK;
				else ShowFields();
			}
		}

		private void BtnShowFieldsClick(object sender, EventArgs e)
		{
			ShowFields();
		}

		#endregion
	}
}
