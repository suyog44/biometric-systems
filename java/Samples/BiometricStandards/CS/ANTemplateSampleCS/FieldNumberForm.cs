using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class FieldNumberForm : Form
	{
		#region Public static methods

		public static bool IsFieldStandard(ANRecordType recordType, NVersion version, int fieldNumber, ANValidationLevel validationLevel)
		{
			if (fieldNumber == ANRecord.FieldLen) return true;
			if (recordType == ANRecordType.Type1 && fieldNumber == ANType1Record.FieldVer) return true;
			if (recordType != ANRecordType.Type1 && fieldNumber == ANRecord.FieldIdc) return true;
			if (recordType.DataType != ANRecordDataType.Ascii && fieldNumber == ANRecord.FieldData) return true;
			if (validationLevel != ANValidationLevel.Minimal) return recordType.IsFieldStandard(version, fieldNumber);
			if (recordType == ANRecordType.Type1)
			{
				if (fieldNumber == ANType1Record.FieldCnt) return true;
			}
			return false;
		}

		#endregion

		#region Private fields

		private NVersion _version = (NVersion)0;
		private ANRecordType _recordType = null;
		private bool _useSelectMode = true;
		private ANValidationLevel _validationLevel = ANValidationLevel.Standard;
		private bool _useUserDefinedFieldNumber = true;
		private int _maxFieldNumber;
		private int[] _standardFieldNumbers;
		private NRange[] _userDefinedFieldNumbers;

		#endregion

		#region Public constructor

		public FieldNumberForm()
		{
			InitializeComponent();

			OnUseSelectModeChanged();
		}

		#endregion

		#region Private methods

		private void AddNumbers(StringBuilder sb, NRange[] numbers)
		{
			int i = 0;
			foreach (NRange range in numbers)
			{
				if (i != 0) sb.Append(", ");
				sb.AppendFormat("({0}.{1:000} - {0}.{2:000})", _recordType.Number, range.From, range.To);
				i++;
			}
		}

		private void UpdateFields()
		{
			NVersion version = _useSelectMode ? _version : ANTemplate.VersionCurrent;
			List<NVersion> versions = null;
			if (!_useSelectMode && _recordType != null)
			{
				NVersion[] vers = ANTemplate.GetVersions();
				versions = new List<NVersion>(vers.Length);
				foreach (NVersion v in vers)
				{
					if (v >= _recordType.Version)
					{
						versions.Add(v);
					}
				}
			}
			if (_recordType != null)
			{
				_maxFieldNumber = _recordType.GetMaxFieldNumber(version);
				_standardFieldNumbers = _recordType.GetStandardFieldNumbers(version);
				StringBuilder sb = new StringBuilder();
				if (_useSelectMode)
				{
					if (_validationLevel == ANValidationLevel.Minimal)
					{
						sb.AppendFormat("({0}.001 - {0}.{1:000})", _recordType.Number, _maxFieldNumber);
						sb.AppendLine();
						sb.Append("UDF: ");
					}
					_userDefinedFieldNumbers = _recordType.GetUserDefinedFieldNumbers(version);
					if (_userDefinedFieldNumbers.Length == 0)
					{
						sb.Append("None");
					}
					else
					{
						AddNumbers(sb, _userDefinedFieldNumbers);
					}
				}
				else
				{
					_userDefinedFieldNumbers = null;
					foreach (NVersion v in versions)
					{
						sb.AppendFormat("{0}: ", v);
						NRange[] udfNumbers = _recordType.GetUserDefinedFieldNumbers(v);
						if (udfNumbers.Length == 0)
						{
							sb.Append("None");
						}
						else
						{
							AddNumbers(sb, udfNumbers);
						}
						sb.AppendLine();
					}
				}
				userDefinedFieldNumbersLabel.Text = sb.ToString();
				rbUserDefinedField.Enabled = userDefinedFieldNumbersLabel.Enabled = true;
			}
			else
			{
				_maxFieldNumber = 0;
				_standardFieldNumbers = null;
				_userDefinedFieldNumbers = null;
				userDefinedFieldNumbersLabel.Text = string.Empty;
				rbUserDefinedField.Enabled = userDefinedFieldNumbersLabel.Enabled = false;
			}
			lvStandardField.BeginUpdate();
			lvStandardField.Items.Clear();
			if (_standardFieldNumbers != null)
			{
				foreach (int fieldNumber in _standardFieldNumbers)
				{
					bool isReadOnly = IsFieldStandard(_recordType, version, fieldNumber, _validationLevel);
					if (!_useSelectMode || !isReadOnly)
					{
						ListViewItem fieldItem = new ListViewItem(string.Format("{0}.{1:000}", _recordType.Number, fieldNumber));
						fieldItem.Tag = fieldNumber;
						string id = _recordType.GetFieldId(version, fieldNumber);
						string name = _recordType.GetFieldName(version, fieldNumber);
						bool isMandatory = _recordType.IsFieldMandatory(version, fieldNumber);
						if (id != string.Empty) name = string.Format("{0} ({1})", name, id);
						fieldItem.SubItems.Add(name);
						if (!_useSelectMode)
						{
							NVersion knownVer = version;
							foreach (NVersion v in versions)
							{
								if (_recordType.IsFieldKnown(v, fieldNumber) && _recordType.IsFieldStandard(v, fieldNumber))
								{
									knownVer = v;
									break;
								}
							}
							fieldItem.SubItems.Add(knownVer.ToString());
						}
						if (!_useSelectMode || _validationLevel == ANValidationLevel.Minimal)
						{
							fieldItem.SubItems.Add(isMandatory ? "Yes" : "No");
						}
						lvStandardField.Items.Add(fieldItem);
					}
				}
			}
			lvStandardField.EndUpdate();
		}

		private void OnVersionChanged()
		{
			UpdateFields();
		}

		private void OnRecordTypeChanged()
		{
			UpdateFields();
		}

		private void UpdateGui()
		{
			rbStandardField.Enabled = _validationLevel == ANValidationLevel.Minimal;
			rbUserDefinedField.Text = _validationLevel == ANValidationLevel.Minimal ? "Other field:" : "User-defined field:";
			int index = lvStandardField.Columns.IndexOf(fieldVersionColumnHeader); if (index != -1) lvStandardField.Columns.RemoveAt(index);
			index = lvStandardField.Columns.IndexOf(isFieldMantadoryColumnHeader); if (index != -1) lvStandardField.Columns.RemoveAt(index);
			if (!_useSelectMode)
			{
				index = lvStandardField.Columns.IndexOf(fieldVersionColumnHeader); if (index == -1) lvStandardField.Columns.Add(fieldVersionColumnHeader);
			}
			index = lvStandardField.Columns.IndexOf(isFieldMantadoryColumnHeader); if (index == -1) lvStandardField.Columns.Add(isFieldMantadoryColumnHeader);
			ClientSize = new Size(_useSelectMode ? 440 : 500, ClientSize.Height);
		}

		private void OnUseSelectModeChanged()
		{
			UpdateFields();
			UpdateGui();
			if (!_useSelectMode)
			{
				SetUseUserDefinedFieldNumber(false);
			}
			btnOk.Visible = tbUserDefinedField.Visible = rbUserDefinedField.Visible =
				rbStandardField.Visible = _useSelectMode;
			standardFieldsLabel.Visible = userDefinedFieldsLabel.Visible = !_useSelectMode;
			btnCancel.Text = _useSelectMode ? "Cancel" : "Close";
			OnUseUserDefinedFieldNumberChanged();
		}

		private void OnValidationLevelChanged()
		{
			UpdateFields();
			UpdateGui();
			if (_useSelectMode && _validationLevel != ANValidationLevel.Minimal)
			{
				SetUseUserDefinedFieldNumber(true);
			}
		}

		private void OnUseUserDefinedFieldNumberChanged()
		{
			rbStandardField.Checked = _useSelectMode && !_useUserDefinedFieldNumber;
			lvStandardField.Enabled = !_useSelectMode || !_useUserDefinedFieldNumber;
			rbUserDefinedField.Checked = _useSelectMode && _useUserDefinedFieldNumber;
			tbUserDefinedField.Enabled = !_useSelectMode || _useUserDefinedFieldNumber;
			OnSelectedFieldNumberChanged();
		}

		private void SetUseUserDefinedFieldNumber(bool value)
		{
			if (_useUserDefinedFieldNumber != value)
			{
				_useUserDefinedFieldNumber = value;
				OnUseUserDefinedFieldNumberChanged();
			}
		}

		private void OnSelectedFieldNumberChanged()
		{
			btnOk.Enabled = _useSelectMode && (_useUserDefinedFieldNumber || lvStandardField.SelectedIndices.Count != 0);
		}

		private bool IsUdfNumber(int fieldNumber)
		{
			foreach (NRange range in _userDefinedFieldNumbers)
			{
				if (fieldNumber >= range.From && fieldNumber <= range.To) return true;
			}
			return false;
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

		public ANRecordType RecordType
		{
			get
			{
				return _recordType;
			}
			set
			{
				if (_recordType != value)
				{
					_recordType = value;
					OnRecordTypeChanged();
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

		public ANValidationLevel ValidationLevel
		{
			get
			{
				return _validationLevel;
			}
			set
			{
				if (_validationLevel != value)
				{
					_validationLevel = value;
					OnValidationLevelChanged();
				}
			}
		}

		public int FieldNumber
		{
			get
			{
				if (_useUserDefinedFieldNumber)
				{
					int value;
					string text = tbUserDefinedField.Text;
					if (!string.IsNullOrEmpty(text) && text.Contains("."))
					{
						text = text.Trim();
						string prefix = string.Format("{0}.", _recordType.Number);
						if (text.StartsWith(prefix))
						{
							text = text.Substring(prefix.Length);
						}
					}
					return int.TryParse(text, out value) ? value : -1;
				}
				return lvStandardField.SelectedItems.Count == 0 ? -1 : (int)lvStandardField.SelectedItems[0].Tag;
			}
			set
			{
				if (_useUserDefinedFieldNumber)
				{
					tbUserDefinedField.Text = value == -1 ? string.Empty : value.ToString();
				}
				else
				{
					if (value == -1) lvStandardField.SelectedItems.Clear();
					else lvStandardField.Items[Array.IndexOf<int>(_standardFieldNumbers, value)].Selected = true;
				}
			}
		}

		#endregion

		#region Private form events

		private void LvStandardFieldSelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedFieldNumberChanged();
		}

		private void LvStandardFieldDoubleClick(object sender, EventArgs e)
		{
			if (_useSelectMode && FieldNumber != -1) DialogResult = DialogResult.OK;
		}

		private void RbStandardFieldClick(object sender, EventArgs e)
		{
			SetUseUserDefinedFieldNumber(false);
		}

		private void RbUserDefinedFieldClick(object sender, EventArgs e)
		{
			SetUseUserDefinedFieldNumber(true);
		}

		private void FieldNumberFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				if (_useUserDefinedFieldNumber)
				{
					int fieldNumber = FieldNumber;
					string errorMessage = fieldNumber == -1 ? "User defined field number is invalid"
						: fieldNumber < 1 ? "User defined field number is less than one"
						: fieldNumber > _maxFieldNumber ? "User defined field number is greater than maximal allowed value"
						: _validationLevel != ANValidationLevel.Minimal && !IsUdfNumber(fieldNumber) ? "User defined field number is not in user defined field range"
						: null;
					if (errorMessage != null)
					{
						e.Cancel = true;
						tbUserDefinedField.Focus();
						MessageBox.Show(errorMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		#endregion
	}
}
