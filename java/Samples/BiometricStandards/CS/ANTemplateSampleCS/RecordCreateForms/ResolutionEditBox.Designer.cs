namespace Neurotec.Samples.RecordCreateForms
{
	partial class ResolutionEditBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbValue = new System.Windows.Forms.TextBox();
			this.cbScaleUnits = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// tbValue
			// 
			this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbValue.Location = new System.Drawing.Point(0, 3);
			this.tbValue.Name = "tbValue";
			this.tbValue.Size = new System.Drawing.Size(124, 20);
			this.tbValue.TabIndex = 0;
			// 
			// cbUnit
			// 
			this.cbScaleUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbScaleUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScaleUnits.FormattingEnabled = true;
			this.cbScaleUnits.Location = new System.Drawing.Point(130, 3);
			this.cbScaleUnits.Name = "cbUnit";
			this.cbScaleUnits.Size = new System.Drawing.Size(80, 21);
			this.cbScaleUnits.TabIndex = 1;
			this.cbScaleUnits.SelectedIndexChanged += new System.EventHandler(this.CbScaleUnitsSelectedIndexChanged);
			// 
			// ResolutionEditBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cbScaleUnits);
			this.Controls.Add(this.tbValue);
			this.Name = "ResolutionEditBox";
			this.Size = new System.Drawing.Size(213, 30);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbValue;
		private System.Windows.Forms.ComboBox cbScaleUnits;
	}
}
