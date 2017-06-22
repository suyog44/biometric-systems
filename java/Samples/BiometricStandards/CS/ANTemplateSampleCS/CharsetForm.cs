using System;
using System.Drawing;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class CharsetForm : Form
	{
		#region Private fields

		private NVersion _version = (NVersion)0;
		private bool _useSelectMode = true;
		private bool _useUserDefinedCharsetIndex = false;
		private int[] _standardCharsetIndicies;

		#endregion

		#region Public constructor

		public CharsetForm()
		{
			InitializeComponent();

			lblUserDefinedCharsetIndicies.Text = string.Format("({0:000} - {1:000})", ANType1Record.CharsetUserDefinedFrom, ANType1Record.CharsetUserDefinedTo);
			OnUseSelectModeChanged();
		}

		#endregion

		#region Private methods

		private void UpdateCharsets()
		{
			NVersion version = _useSelectMode ? _version : ANTemplate.VersionCurrent;
			NVersion[] versions = ANTemplate.GetVersions();
			_standardCharsetIndicies = ANType1Record.GetStandardCharsetIndexes(version);
			lbStandardCharsets.BeginUpdate();
			lbStandardCharsets.Items.Clear();
			foreach (int charsetIndex in _standardCharsetIndicies)
			{
				ListViewItem charsetItem = new ListViewItem(string.Format("{0:000}", charsetIndex));
				charsetItem.Tag = charsetIndex;
				charsetItem.SubItems.Add(string.Format("{0} ({1})", ANType1Record.GetStandardCharsetName(version, charsetIndex), ANType1Record.GetStandardCharsetDescription(version, charsetIndex)));
				if (!_useSelectMode)
				{
					NVersion knownVer = version;
					foreach (NVersion v in versions)
					{
						if (ANType1Record.IsCharsetKnown(v, charsetIndex))
						{
							knownVer = v;
							break;
						}
					}
					charsetItem.SubItems.Add(knownVer.ToString());
				}
				lbStandardCharsets.Items.Add(charsetItem);
			}
			lbStandardCharsets.EndUpdate();
		}

		private void OnVersionChanged()
		{
			UpdateCharsets();
		}

		private void OnUseSelectModeChanged()
		{
			UpdateCharsets();
			if (_useSelectMode)
			{
				int index = lbStandardCharsets.Columns.IndexOf(charsetVersionColumnHeader);
				if (index != -1) lbStandardCharsets.Columns.RemoveAt(index);
			}
			else
			{
				SetUseUserDefinedCharsetIndex(false);
				int index = lbStandardCharsets.Columns.IndexOf(charsetVersionColumnHeader); if (index == -1) lbStandardCharsets.Columns.Add(charsetVersionColumnHeader);
			}
			ClientSize = new Size(_useSelectMode ? 370 : 430, ClientSize.Height);
			btnOk.Visible =
				userDefinedCharsetIndexLabel.Visible = tbUserDefinedCharsetIndex.Visible =
				userDefinedCharsetNameLabel.Visible = tbUserDefinedCharsetName.Visible =
				charsetVersionLabel.Visible = tbCharsetVersion.Visible =
				rbUserDefinedCharset.Visible = rbStandardCharset.Visible = _useSelectMode;
			standardCharsetsLabel.Visible = userDefinedCharsetsLabel.Visible = !_useSelectMode;
			btnCancel.Text = _useSelectMode ? "Cancel" : "Close";
			OnUseUserDefinedCharsetIndexChanged();
		}

		private void OnUseUserDefinedCharsetIndexChanged()
		{
			rbStandardCharset.Checked = _useSelectMode && !_useUserDefinedCharsetIndex;
			lbStandardCharsets.Enabled = !_useSelectMode || !_useUserDefinedCharsetIndex;
			rbUserDefinedCharset.Checked = _useSelectMode && _useUserDefinedCharsetIndex;
			userDefinedCharsetIndexLabel.Enabled = tbUserDefinedCharsetIndex.Enabled =
				userDefinedCharsetNameLabel.Enabled = tbUserDefinedCharsetName.Enabled =
				!_useSelectMode || _useUserDefinedCharsetIndex;
			OnSelectedCharsetIndexChanged();
		}

		private void SetUseUserDefinedCharsetIndex(bool value)
		{
			if (_useUserDefinedCharsetIndex != value)
			{
				_useUserDefinedCharsetIndex = value;
				OnUseUserDefinedCharsetIndexChanged();
			}
		}

		private void OnSelectedCharsetIndexChanged()
		{
			btnOk.Enabled = _useSelectMode && (_useUserDefinedCharsetIndex || lbStandardCharsets.SelectedIndices.Count != 0);
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

		public int CharsetIndex
		{
			get
			{
				if (_useUserDefinedCharsetIndex)
				{
					int value;
					return int.TryParse(tbUserDefinedCharsetIndex.Text, out value) ? value : -1;
				}
				return lbStandardCharsets.SelectedItems.Count == 0 ? -1 : (int)lbStandardCharsets.SelectedItems[0].Tag;
			}
			set
			{
				if (_useUserDefinedCharsetIndex)
				{
					tbUserDefinedCharsetIndex.Text = value == -1 ? string.Empty : value.ToString();
				}
				else
				{
					if (value == -1) lbStandardCharsets.SelectedItems.Clear();
					else lbStandardCharsets.Items[Array.IndexOf<int>(_standardCharsetIndicies, value)].Selected = true;
				}
			}
		}

		public string CharsetName
		{
			get
			{
				return _useUserDefinedCharsetIndex ? tbUserDefinedCharsetName.Text
					: lbStandardCharsets.SelectedItems.Count == 0 ? null : ANType1Record.GetStandardCharsetName(_useSelectMode ? _version : ANTemplate.VersionCurrent, (int)lbStandardCharsets.SelectedItems[0].Tag);
			}
			set
			{
				if (_useUserDefinedCharsetIndex)
				{
					tbUserDefinedCharsetName.Text = value;
				}
			}
		}

		public string CharsetVersion
		{
			get
			{
				return tbCharsetVersion.Text;
			}
			set
			{
				tbCharsetVersion.Text = value;
			}
		}

		#endregion

		#region Private form events

		private void LvStandardCharsetSelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedCharsetIndexChanged();
		}

		private void LvStandardCharsetDoubleClick(object sender, EventArgs e)
		{
			if (_useSelectMode && CharsetIndex != -1) DialogResult = DialogResult.OK;
		}

		private void RbStandardCharsetClick(object sender, EventArgs e)
		{
			SetUseUserDefinedCharsetIndex(false);
		}

		private void RbUserDefinedCharsetClick(object sender, EventArgs e)
		{
			SetUseUserDefinedCharsetIndex(true);
		}

		private void CharsetFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				if (_useUserDefinedCharsetIndex)
				{
					int charsetIndex = CharsetIndex;
					string errorMessage = charsetIndex == -1 ? "User defined charset index is invalid"
						: charsetIndex < 0 ? "User defined charset index is less than zero"
						: charsetIndex > ANType1Record.CharsetUserDefinedTo ? "User defined charset index is greater than maximal allowed value"
						: charsetIndex < ANType1Record.CharsetUserDefinedFrom || charsetIndex > ANType1Record.CharsetUserDefinedTo ? "User defined charset index is not in user defined charset range"
						: null;
					if (errorMessage != null)
					{
						e.Cancel = true;
						tbUserDefinedCharsetIndex.Focus();
						MessageBox.Show(errorMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		#endregion
	}
}
