using System;
using System.Collections.Generic;
using System.Linq;
using Neurotec.Biometrics.Common;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class FCRecordOptionsForm : BdifOptionsForm
	{
		public FCRecordOptionsForm()
		{
			InitializeComponent();
			var versions = new List<StandardVersion>();
			versions.Add(new StandardVersion(BdifStandard.Ansi, FCRecord.VersionAnsi10, "ANSI/INCITS 385-2004"));
			versions.Add(new StandardVersion(BdifStandard.Iso, FCRecord.VersionIso10, "ISO/IEC 19794-5:2005"));
			versions.Add(new StandardVersion(BdifStandard.Iso, FCRecord.VersionIso30, "ISO/IEC 19794-5:2011"));
			StandardVersions = versions.ToArray();
		}

		public override uint Flags
		{
			get
			{
				uint flags = base.Flags;
				if (cbProcessFirstFaceImageOnly.Checked)
					flags |= FCRecord.FlagProcessFirstFaceImageOnly;
				if (cbSkipFeaturePoints.Checked)
					flags |= FcrFaceImage.FlagSkipFeaturePoints;
				return flags;
			}
			set
			{
				if ((value & FCRecord.FlagProcessFirstFaceImageOnly) == FCRecord.FlagProcessFirstFaceImageOnly)
					cbProcessFirstFaceImageOnly.Checked = true;
				if ((value & FcrFaceImage.FlagSkipFeaturePoints) == FcrFaceImage.FlagSkipFeaturePoints)
					cbSkipFeaturePoints.Checked = true;
				base.Flags = value;
			}
		}
	}
}
