using System;
using System.Collections.Generic;
using System.Linq;
using Neurotec.Biometrics.Common;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class IIRecordOptionsForm : BdifOptionsForm
	{
		public IIRecordOptionsForm()
		{
			InitializeComponent();
			var versions = new List<StandardVersion>();
			versions.Add(new StandardVersion(BdifStandard.Ansi, IIRecord.VersionAnsi10, "ANSI/INCITS 379-2004"));
			versions.Add(new StandardVersion(BdifStandard.Iso, IIRecord.VersionIso10, "ISO/IEC 19794-6:2005"));
			versions.Add(new StandardVersion(BdifStandard.Iso, IIRecord.VersionIso20, "ISO/IEC 19794-6:2011"));
			StandardVersions = versions.ToArray();
		}

		public override uint Flags
		{
			get
			{
				uint flags = base.Flags;
				if (cbProcessFirstIrisImageOnly.Checked)
					flags |= IIRecord.FlagProcessIrisFirstIrisImageOnly;

				return flags;
			}
			set
			{
				if ((value & IIRecord.FlagProcessIrisFirstIrisImageOnly) == IIRecord.FlagProcessIrisFirstIrisImageOnly)
					cbProcessFirstIrisImageOnly.Checked = true;
				base.Flags = value;
			}
		}

		protected override void OnModeChanged()
		{
			base.OnModeChanged();

			switch (Mode)
			{
				case BdifOptionsFormMode.New:
					cbProcessFirstIrisImageOnly.Enabled = false;
					break;
			}
		}
	}
}
