using System;
using System.Collections.Generic;
using System.Linq;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class FIRecordOptionsForm : Biometrics.Common.BdifOptionsForm
	{
		public FIRecordOptionsForm()
		{
			InitializeComponent();

			var versions = new List<StandardVersion>();
			versions.Add(new StandardVersion(BdifStandard.Ansi, FIRecord.VersionAnsi10, "ANSI/INCITS 381-2004"));
			versions.Add(new StandardVersion(BdifStandard.Ansi, FIRecord.VersionAnsi25, "ANSI/INCITS 381-2009"));
			versions.Add(new StandardVersion(BdifStandard.Iso, FIRecord.VersionIso10, "ISO/IEC 19794-4:2005"));
			versions.Add(new StandardVersion(BdifStandard.Iso, FIRecord.VersionIso20, "ISO/IEC 19794-4:2011"));
			StandardVersions = versions.ToArray();
		}

		public override uint Flags
		{
			get
			{
				uint flags = base.Flags;
				if (cbProcessFirstFingerOnly.Checked)
					flags |= FIRecord.FlagProcessFirstFingerOnly;
				if (cbProcessFirstFingerViewOnly.Checked)
				{
					flags |= FIRecord.FlagProcessFirstFingerViewOnly;
				}
				return flags;
			}
			set
			{
				if ((value & FIRecord.FlagProcessFirstFingerOnly) == FIRecord.FlagProcessFirstFingerOnly)
					cbProcessFirstFingerOnly.Checked = true;
				if ((value & FIRecord.FlagProcessFirstFingerViewOnly) == FIRecord.FlagProcessFirstFingerViewOnly)
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

		private System.Windows.Forms.GroupBox gbFIRecord;
		private System.Windows.Forms.CheckBox cbProcessFirstFingerViewOnly;
		private System.Windows.Forms.CheckBox cbProcessFirstFingerOnly;

		private void InitializeComponent()
		{
			this.gbFIRecord = new System.Windows.Forms.GroupBox();
			this.cbProcessFirstFingerViewOnly = new System.Windows.Forms.CheckBox();
			this.cbProcessFirstFingerOnly = new System.Windows.Forms.CheckBox();
			this.gbFIRecord.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(248, 220);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(167, 220);
			// 
			// gbFIRecord
			// 
			this.gbFIRecord.Controls.Add(this.cbProcessFirstFingerViewOnly);
			this.gbFIRecord.Controls.Add(this.cbProcessFirstFingerOnly);
			this.gbFIRecord.Location = new System.Drawing.Point(15, 139);
			this.gbFIRecord.Name = "gbFIRecord";
			this.gbFIRecord.Size = new System.Drawing.Size(308, 66);
			this.gbFIRecord.TabIndex = 3;
			this.gbFIRecord.TabStop = false;
			this.gbFIRecord.Text = "FIRecord";
			// 
			// cbProcessFirstFingerViewOnly
			// 
			this.cbProcessFirstFingerViewOnly.AutoSize = true;
			this.cbProcessFirstFingerViewOnly.Location = new System.Drawing.Point(6, 42);
			this.cbProcessFirstFingerViewOnly.Name = "cbProcessFirstFingerViewOnly";
			this.cbProcessFirstFingerViewOnly.Size = new System.Drawing.Size(157, 17);
			this.cbProcessFirstFingerViewOnly.TabIndex = 1;
			this.cbProcessFirstFingerViewOnly.Text = "Process first fingerView only";
			this.cbProcessFirstFingerViewOnly.UseVisualStyleBackColor = true;
			// 
			// cbProcessFirstFingerOnly
			// 
			this.cbProcessFirstFingerOnly.AutoSize = true;
			this.cbProcessFirstFingerOnly.Location = new System.Drawing.Point(6, 19);
			this.cbProcessFirstFingerOnly.Name = "cbProcessFirstFingerOnly";
			this.cbProcessFirstFingerOnly.Size = new System.Drawing.Size(134, 17);
			this.cbProcessFirstFingerOnly.TabIndex = 0;
			this.cbProcessFirstFingerOnly.Text = "Process first finger only";
			this.cbProcessFirstFingerOnly.UseVisualStyleBackColor = true;
			// 
			// FIRecordOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(335, 253);
			this.Controls.Add(this.gbFIRecord);
			this.Name = "FIRecordOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FIRecordOptionsForm";
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.gbFIRecord, 0);
			this.gbFIRecord.ResumeLayout(false);
			this.gbFIRecord.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

	}
}
