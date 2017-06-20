namespace Neurotec.Samples
{
	partial class BandpassFilteringForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BandpassFilteringForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.penSize = new System.Windows.Forms.TrackBar();
			this.label5 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.radioRect = new System.Windows.Forms.RadioButton();
			this.radioCircle = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.button6 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.bRealtime = new System.Windows.Forms.CheckBox();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel3 = new System.Windows.Forms.Panel();
			this.viewFourierMask = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.viewResult = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.penSize)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel6.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewFourierMask)).BeginInit();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewResult)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.panel5);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(86, 419);
			this.panel1.TabIndex = 0;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.button1);
			this.panel5.Controls.Add(this.button2);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(0, 347);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(82, 68);
			this.panel5.TabIndex = 7;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(3, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(76, 26);
			this.button1.TabIndex = 5;
			this.button1.Text = "Accept";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(3, 38);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(76, 26);
			this.button2.TabIndex = 6;
			this.button2.Text = "Dismiss";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.penSize);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.tableLayoutPanel1);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.panel6);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 13);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(82, 334);
			this.panel2.TabIndex = 8;
			// 
			// penSize
			// 
			this.penSize.Location = new System.Drawing.Point(3, 236);
			this.penSize.Maximum = 150;
			this.penSize.Minimum = 1;
			this.penSize.Name = "penSize";
			this.penSize.Size = new System.Drawing.Size(76, 45);
			this.penSize.TabIndex = 14;
			this.penSize.TickFrequency = 10;
			this.penSize.Value = 20;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 220);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Size:";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.radioRect, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.radioCircle, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 173);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(82, 44);
			this.tableLayoutPanel1.TabIndex = 11;
			// 
			// radioRect
			// 
			this.radioRect.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioRect.AutoSize = true;
			this.radioRect.Image = global::Neurotec.Samples.Properties.Resources.ToolRect;
			this.radioRect.Location = new System.Drawing.Point(44, 3);
			this.radioRect.Name = "radioRect";
			this.radioRect.Size = new System.Drawing.Size(35, 38);
			this.radioRect.TabIndex = 1;
			this.radioRect.TabStop = true;
			this.radioRect.UseVisualStyleBackColor = true;
			this.radioRect.CheckedChanged += new System.EventHandler(this.radioRect_CheckedChanged);
			// 
			// radioCircle
			// 
			this.radioCircle.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioCircle.AutoSize = true;
			this.radioCircle.Image = global::Neurotec.Samples.Properties.Resources.ToolCircle;
			this.radioCircle.Location = new System.Drawing.Point(3, 3);
			this.radioCircle.Name = "radioCircle";
			this.radioCircle.Size = new System.Drawing.Size(35, 38);
			this.radioCircle.TabIndex = 0;
			this.radioCircle.TabStop = true;
			this.radioCircle.UseVisualStyleBackColor = true;
			this.radioCircle.CheckedChanged += new System.EventHandler(this.radioCircle_CheckedChanged);
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.SystemColors.ControlDark;
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.label4.Location = new System.Drawing.Point(0, 160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(82, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Tools";
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.button6);
			this.panel6.Controls.Add(this.button4);
			this.panel6.Controls.Add(this.bRealtime);
			this.panel6.Controls.Add(this.button3);
			this.panel6.Controls.Add(this.button5);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel6.Location = new System.Drawing.Point(0, 0);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(82, 160);
			this.panel6.TabIndex = 10;
			// 
			// button6
			// 
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button6.Location = new System.Drawing.Point(3, 98);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(76, 26);
			this.button6.TabIndex = 9;
			this.button6.Text = "Refresh";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button4
			// 
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.Location = new System.Drawing.Point(3, 2);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(76, 26);
			this.button4.TabIndex = 5;
			this.button4.Text = "Set all";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// bRealtime
			// 
			this.bRealtime.Checked = true;
			this.bRealtime.CheckState = System.Windows.Forms.CheckState.Checked;
			this.bRealtime.Location = new System.Drawing.Point(3, 121);
			this.bRealtime.Name = "bRealtime";
			this.bRealtime.Size = new System.Drawing.Size(75, 36);
			this.bRealtime.TabIndex = 8;
			this.bRealtime.Text = "Auto refresh";
			this.bRealtime.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Location = new System.Drawing.Point(3, 34);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(76, 26);
			this.button3.TabIndex = 6;
			this.button3.Text = "Reset all";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button5
			// 
			this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button5.Location = new System.Drawing.Point(3, 66);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(76, 26);
			this.button5.TabIndex = 7;
			this.button5.Text = "Invert";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.SystemColors.ControlDark;
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Operations";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(86, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panel3);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panel4);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Size = new System.Drawing.Size(740, 419);
			this.splitContainer1.SplitterDistance = 365;
			this.splitContainer1.TabIndex = 1;
			// 
			// panel3
			// 
			this.panel3.AutoScroll = true;
			this.panel3.Controls.Add(this.viewFourierMask);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 13);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(361, 402);
			this.panel3.TabIndex = 1;
			// 
			// viewFourierMask
			// 
			this.viewFourierMask.Location = new System.Drawing.Point(3, 3);
			this.viewFourierMask.Name = "viewFourierMask";
			this.viewFourierMask.Size = new System.Drawing.Size(54, 49);
			this.viewFourierMask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.viewFourierMask.TabIndex = 0;
			this.viewFourierMask.TabStop = false;
			this.viewFourierMask.MouseMove += new System.Windows.Forms.MouseEventHandler(this.viewFourier_MouseMove);
			this.viewFourierMask.MouseDown += new System.Windows.Forms.MouseEventHandler(this.viewFourier_MouseDown);
			this.viewFourierMask.MouseUp += new System.Windows.Forms.MouseEventHandler(this.viewFourier_MouseUp);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(361, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Fourier image and mask";
			// 
			// panel4
			// 
			this.panel4.AutoScroll = true;
			this.panel4.Controls.Add(this.viewResult);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 13);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(367, 402);
			this.panel4.TabIndex = 2;
			// 
			// viewResult
			// 
			this.viewResult.Location = new System.Drawing.Point(3, 3);
			this.viewResult.Name = "viewResult";
			this.viewResult.Size = new System.Drawing.Size(54, 49);
			this.viewResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.viewResult.TabIndex = 1;
			this.viewResult.TabStop = false;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(367, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Filtered image";
			// 
			// BandpassFiltering
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(826, 419);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(8, 286);
			this.Name = "BandpassFiltering";
			this.ShowInTaskbar = false;
			this.Text = "Bandpass Filtering";
			this.panel1.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.penSize)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewFourierMask)).EndInit();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewResult)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.CheckBox bRealtime;
		private System.Windows.Forms.PictureBox viewFourierMask;
		private System.Windows.Forms.PictureBox viewResult;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.RadioButton radioRect;
		private System.Windows.Forms.RadioButton radioCircle;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TrackBar penSize;

	}
}
