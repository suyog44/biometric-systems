namespace Neurotec.Samples
{
	partial class GeneralSettingsPage
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.chbFirstResult = new System.Windows.Forms.CheckBox();
			this.chbMatchWithDetails = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbMatchingThreshold = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nudResultsCount = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudResultsCount)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoScroll = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.chbFirstResult, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.chbMatchWithDetails, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbMatchingThreshold, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.nudResultsCount, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(266, 151);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// chbFirstResult
			// 
			this.chbFirstResult.AutoSize = true;
			this.chbFirstResult.Location = new System.Drawing.Point(120, 79);
			this.chbFirstResult.Name = "chbFirstResult";
			this.chbFirstResult.Size = new System.Drawing.Size(95, 17);
			this.chbFirstResult.TabIndex = 1;
			this.chbFirstResult.Text = "First result only";
			this.chbFirstResult.UseVisualStyleBackColor = true;
			this.chbFirstResult.CheckedChanged += new System.EventHandler(this.ChbFirstResultCheckedChanged);
			// 
			// chbMatchWithDetails
			// 
			this.chbMatchWithDetails.AutoSize = true;
			this.chbMatchWithDetails.Location = new System.Drawing.Point(120, 56);
			this.chbMatchWithDetails.Name = "chbMatchWithDetails";
			this.chbMatchWithDetails.Size = new System.Drawing.Size(137, 17);
			this.chbMatchWithDetails.TabIndex = 0;
			this.chbMatchWithDetails.Text = "Return matching details";
			this.chbMatchWithDetails.UseVisualStyleBackColor = true;
			this.chbMatchWithDetails.CheckedChanged += new System.EventHandler(this.ChbMatchWithDetailsCheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111, 27);
			this.label2.TabIndex = 2;
			this.label2.Text = "Matching threshold:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbMatchingThreshold
			// 
			this.cbMatchingThreshold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMatchingThreshold.FormattingEnabled = true;
			this.cbMatchingThreshold.Location = new System.Drawing.Point(120, 3);
			this.cbMatchingThreshold.Name = "cbMatchingThreshold";
			this.cbMatchingThreshold.Size = new System.Drawing.Size(134, 21);
			this.cbMatchingThreshold.TabIndex = 3;
			this.cbMatchingThreshold.SelectedIndexChanged += new System.EventHandler(this.CbMatchingThresholdSelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 26);
			this.label1.TabIndex = 1;
			this.label1.Text = "Maximal results count:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nudResultsCount
			// 
			this.nudResultsCount.Location = new System.Drawing.Point(120, 30);
			this.nudResultsCount.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.nudResultsCount.Name = "nudResultsCount";
			this.nudResultsCount.Size = new System.Drawing.Size(134, 20);
			this.nudResultsCount.TabIndex = 4;
			this.nudResultsCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudResultsCount.ValueChanged += new System.EventHandler(this.NudResultsCountValueChanged);
			// 
			// GeneralSettingsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "GeneralSettingsPage";
			this.PageName = "General";
			this.Size = new System.Drawing.Size(266, 151);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudResultsCount)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox chbMatchWithDetails;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbMatchingThreshold;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chbFirstResult;
		private System.Windows.Forms.NumericUpDown nudResultsCount;

	}
}
