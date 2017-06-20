using System;
using System.Collections.Generic;
using System.Linq;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class FMRecordOptionsForm : Biometrics.Common.BdifOptionsForm
	{
		public FMRecordOptionsForm()
		{
			InitializeComponent();

			var versions = new List<StandardVersion>();
			versions.Add(new StandardVersion(BdifStandard.Ansi, FMRecord.VersionAnsi20, "ANSI/INCITS 378-2004"));
			versions.Add(new StandardVersion(BdifStandard.Ansi, FMRecord.VersionAnsi35, "ANSI/INCITS 378-2009"));
			versions.Add(new StandardVersion(BdifStandard.Iso, FMRecord.VersionIso20, "ISO/IEC 19794-2:2005"));
			versions.Add(new StandardVersion(BdifStandard.Iso, FMRecord.VersionIso30, "ISO/IEC 19794-2:2011"));
			StandardVersions = versions.ToArray();
		}

		public override uint Flags
		{
			get
			{
				uint flags = base.Flags;
				if (cbProcessFirstFingerOnly.Checked)
					flags |= FMRecord.FlagProcessFirstFingerOnly;
				if (cbProcessFirstFingerViewOnly.Checked)
					flags |= FMRecord.FlagProcessFirstFingerViewOnly;
				return flags;
			}
			set
			{
				if ((value & FMRecord.FlagProcessFirstFingerOnly) == FMRecord.FlagProcessFirstFingerOnly)
					cbProcessFirstFingerOnly.Checked = true;
				if ((value & FMRecord.FlagProcessFirstFingerViewOnly) == FMRecord.FlagProcessFirstFingerViewOnly)
					cbProcessFirstFingerViewOnly.Checked = true;
				base.Flags = value;
			}
		}

		protected override void OnModeChanged()
		{
			base.OnModeChanged();

			switch (Mode)
			{
				case BdifOptionsFormMode.New:
					cbProcessFirstFingerOnly.Enabled = false;
					break;
			}
		}

		#region IntializeComponent

		private System.Windows.Forms.GroupBox gbFMRecord;
		private System.Windows.Forms.CheckBox cbProcessFirstFingerViewOnly;
		private System.Windows.Forms.CheckBox cbProcessFirstFingerOnly;

		private void InitializeComponent()
		{
			this.gbFMRecord = new System.Windows.Forms.GroupBox();
			this.cbProcessFirstFingerViewOnly = new System.Windows.Forms.CheckBox();
			this.cbProcessFirstFingerOnly = new System.Windows.Forms.CheckBox();
			this.gbFMRecord.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(244, 203);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(163, 203);
			// 
			// gbFMRecord
			// 
			this.gbFMRecord.Controls.Add(this.cbProcessFirstFingerViewOnly);
			this.gbFMRecord.Controls.Add(this.cbProcessFirstFingerOnly);
			this.gbFMRecord.Location = new System.Drawing.Point(12, 132);
			this.gbFMRecord.Name = "gbFMRecord";
			this.gbFMRecord.Size = new System.Drawing.Size(308, 66);
			this.gbFMRecord.TabIndex = 3;
			this.gbFMRecord.TabStop = false;
			this.gbFMRecord.Text = "FMRecord";
			// 
			// cbProcessFirstFingerViewOnly
			// 
			this.cbProcessFirstFingerViewOnly.AutoSize = true;
			this.cbProcessFirstFingerViewOnly.Location = new System.Drawing.Point(9, 42);
			this.cbProcessFirstFingerViewOnly.Name = "cbProcessFirstFingerViewOnly";
			this.cbProcessFirstFingerViewOnly.Size = new System.Drawing.Size(159, 17);
			this.cbProcessFirstFingerViewOnly.TabIndex = 1;
			this.cbProcessFirstFingerViewOnly.Text = "Process first finger view only";
			this.cbProcessFirstFingerViewOnly.UseVisualStyleBackColor = true;
			// 
			// cbProcessFirstFingerOnly
			// 
			this.cbProcessFirstFingerOnly.AutoSize = true;
			this.cbProcessFirstFingerOnly.Location = new System.Drawing.Point(9, 19);
			this.cbProcessFirstFingerOnly.Name = "cbProcessFirstFingerOnly";
			this.cbProcessFirstFingerOnly.Size = new System.Drawing.Size(134, 17);
			this.cbProcessFirstFingerOnly.TabIndex = 0;
			this.cbProcessFirstFingerOnly.Text = "Process first finger only";
			this.cbProcessFirstFingerOnly.UseVisualStyleBackColor = true;
			// 
			// FMRecordOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(331, 236);
			this.Controls.Add(this.gbFMRecord);
			this.Name = "FMRecordOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FMRecordOptionsForm";
			this.Controls.SetChildIndex(this.gbFMRecord, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.gbFMRecord.ResumeLayout(false);
			this.gbFMRecord.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
	}
}
