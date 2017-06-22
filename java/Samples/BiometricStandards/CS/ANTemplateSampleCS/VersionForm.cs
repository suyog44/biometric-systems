using System;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class VersionForm : Form
	{
		#region Private fields

		private bool _useSelectMode = true;
		private readonly NVersion[] _versions;

		#endregion

		#region Public constructor

		public VersionForm()
		{
			InitializeComponent();

			_versions = ANTemplate.GetVersions();
			lbVersions.BeginUpdate();
			foreach (NVersion version in _versions)
			{
				ListViewItem versionItem = new ListViewItem(version.ToString());
				versionItem.Tag = version;
				versionItem.SubItems.Add(ANTemplate.GetVersionName(version));
				lbVersions.Items.Add(versionItem);
			}
			lbVersions.EndUpdate();
			OnUseSelectModeChanged();
		}

		#endregion

		#region Private methods

		private void OnUseSelectModeChanged()
		{
			btnOk.Visible = _useSelectMode;
			btnCancel.Text = _useSelectMode ? "Cancel" : "Close";
			OnSelectedVersionChanged();
		}

		private void OnSelectedVersionChanged()
		{
			btnOk.Enabled = _useSelectMode && lbVersions.SelectedIndices.Count != 0;
		}

		#endregion

		#region Private form events

		private void LvVersionsSelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedVersionChanged();
		}

		private void LvVersionsDoubleClick(object sender, EventArgs e)
		{
			if (_useSelectMode && SelectedVersion != (NVersion)0) DialogResult = DialogResult.OK;
		}

		#endregion

		#region Public properties

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

		public NVersion SelectedVersion
		{
			get
			{
				return lbVersions.SelectedItems.Count == 0 ? (NVersion)0 : (NVersion)lbVersions.SelectedItems[0].Tag;
			}
			set
			{
				if (value == (NVersion)0) lbVersions.SelectedItems.Clear();
				else lbVersions.Items[Array.IndexOf<NVersion>(_versions, value)].Selected = true;
			}
		}

		#endregion
	}
}
