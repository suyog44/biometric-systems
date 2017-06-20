using System;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;
using System.ComponentModel;

namespace Neurotec.Biometrics.Common
{
	public partial class BdifOptionsForm : Form
	{
		#region Public types

		protected class StandardVersion
		{
			public BdifStandard Standard { get; private set; }
			public NVersion Version { get; private set; }
			public string StandardName { get; private set; }

			public StandardVersion(BdifStandard standard, NVersion version, string standardName)
			{
				Standard = standard;
				Version = version;
				StandardName = standardName;
			}

			public override string ToString()
			{
				return string.Format("{0}, {1}", Version, StandardName);
			}
		}

		public enum BdifOptionsFormMode
		{
			New = 1,
			Open = 2,
			Save = 3,
			Convert = 4
		}

		#endregion

		#region Public Constructor

		public BdifOptionsForm()
		{
			InitializeComponent();

			cbBiometricStandard.Items.Add(BdifStandard.Ansi);
			cbBiometricStandard.Items.Add(BdifStandard.Iso);

			cbBiometricStandard.SelectedIndex = 0;
		}

		#endregion

		#region Public properties

		BdifOptionsFormMode _mode = BdifOptionsFormMode.New;
		public BdifOptionsFormMode Mode
		{
			get
			{
				return _mode;
			}
			set
			{
				if (_mode != value)
				{
					_mode = value;
					OnModeChanged();
				}
			}
		}

		public BdifStandard Standard
		{
			get
			{
				return (BdifStandard)cbBiometricStandard.SelectedItem;
			}
			set
			{
				cbBiometricStandard.SelectedItem = value;
				OnStandardChanged();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NVersion Version
		{
			get
			{
				var standardVersion = (StandardVersion)cbVersion.SelectedItem;
				return standardVersion.Version;
			}
			set
			{
				var standardVersion = StandardVersions.Where(x => x.Standard == Standard && x.Version == Version).FirstOrDefault();
				if (standardVersion == null) throw new ArgumentException("Version is invalid");
				cbVersion.SelectedItem = standardVersion;
			}
		}

		public virtual uint Flags
		{
			get
			{
				uint flags = 0;
				if (cbDoNotCheckCbeffProductId.Checked)
					flags |= BdifTypes.FlagDoNotCheckCbeffProductId;
				if (cbNoStrictRead.Checked)
					flags |= BdifTypes.FlagNonStrictRead;
				return flags;
			}
			set
			{
				if ((value & BdifTypes.FlagDoNotCheckCbeffProductId) == BdifTypes.FlagDoNotCheckCbeffProductId)
					cbDoNotCheckCbeffProductId.Checked = true;
				if ((value & BdifTypes.FlagNonStrictRead) == BdifTypes.FlagNonStrictRead)
					cbNoStrictRead.Checked = true;
			}
		}

		#endregion

		#region Protected properties

		private StandardVersion[] _versions;
		protected StandardVersion[] StandardVersions
		{
			get
			{
				return _versions;
			}
			set
			{
				_versions = value;
				OnStandardChanged();
			}
		}

		#endregion

		#region Private methods

		private void OnStandardChanged()
		{
			if (cbVersion == null || StandardVersions == null) return;
			cbVersion.BeginUpdate();
			cbVersion.Items.Clear();
			try
			{
				cbVersion.Items.AddRange(StandardVersions.Where(x => x.Standard == Standard).ToArray());
				cbVersion.SelectedIndex = cbVersion.Items.Count - 1;
			}
			finally
			{
				cbVersion.EndUpdate();
			}
		}

		#endregion

		#region Events

		protected virtual void OnModeChanged()
		{
			Text = Enum.GetName(typeof(BdifOptionsFormMode), Mode);

			switch (Mode)
			{
				case BdifOptionsFormMode.Open:
					cbVersion.Enabled = false;
					lbVersion.Enabled = false;
					break;
				case BdifOptionsFormMode.New:
				case BdifOptionsFormMode.Save:
				case BdifOptionsFormMode.Convert:
					cbNoStrictRead.Enabled = false;
					break;
			}
		}

		private void cbBiometricStandard_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnStandardChanged();
		}

		#endregion
	}
}
