namespace Neurotec.Samples
{
	partial class VoicesSettingsPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoicesSettingsPage));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.cbFormats = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chbUniquePhrases = new System.Windows.Forms.CheckBox();
			this.chbTextIndependant = new System.Windows.Forms.CheckBox();
			this.chbTextDependent = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbMicrophones = new System.Windows.Forms.ComboBox();
			this.nudMaxFileSize = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxFileSize)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoScroll = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.cbFormats, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.chbUniquePhrases, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.chbTextIndependant, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.chbTextDependent, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbMicrophones, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.nudMaxFileSize, 1, 6);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 202);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 175);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 27);
			this.label4.TabIndex = 7;
			this.label4.Text = "Maximal loaded\r\nfile size (MB):";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbFormats
			// 
			this.cbFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFormats.FormattingEnabled = true;
			this.cbFormats.Location = new System.Drawing.Point(89, 30);
			this.cbFormats.Name = "cbFormats";
			this.cbFormats.Size = new System.Drawing.Size(269, 21);
			this.cbFormats.TabIndex = 6;
			this.cbFormats.SelectedIndexChanged += new System.EventHandler(this.CbFormatsSelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(16, 77);
			this.label1.Margin = new System.Windows.Forms.Padding(16, 0, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(466, 52);
			this.label1.TabIndex = 2;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// chbUniquePhrases
			// 
			this.chbUniquePhrases.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.chbUniquePhrases, 2);
			this.chbUniquePhrases.Location = new System.Drawing.Point(3, 57);
			this.chbUniquePhrases.Name = "chbUniquePhrases";
			this.chbUniquePhrases.Size = new System.Drawing.Size(122, 17);
			this.chbUniquePhrases.TabIndex = 0;
			this.chbUniquePhrases.Text = "Unique phrases only";
			this.chbUniquePhrases.UseVisualStyleBackColor = true;
			this.chbUniquePhrases.CheckedChanged += new System.EventHandler(this.ChbUniquePhrasesCheckedChanged);
			// 
			// chbTextIndependant
			// 
			this.chbTextIndependant.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.chbTextIndependant, 2);
			this.chbTextIndependant.Location = new System.Drawing.Point(3, 155);
			this.chbTextIndependant.Name = "chbTextIndependant";
			this.chbTextIndependant.Size = new System.Drawing.Size(182, 17);
			this.chbTextIndependant.TabIndex = 2;
			this.chbTextIndependant.Text = "Extract text independent features";
			this.chbTextIndependant.UseVisualStyleBackColor = true;
			this.chbTextIndependant.CheckedChanged += new System.EventHandler(this.ChbTextIndependantCheckedChanged);
			// 
			// chbTextDependent
			// 
			this.chbTextDependent.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.chbTextDependent, 2);
			this.chbTextDependent.Location = new System.Drawing.Point(3, 132);
			this.chbTextDependent.Name = "chbTextDependent";
			this.chbTextDependent.Size = new System.Drawing.Size(174, 17);
			this.chbTextDependent.TabIndex = 1;
			this.chbTextDependent.Text = "Extract text dependent features";
			this.chbTextDependent.UseVisualStyleBackColor = true;
			this.chbTextDependent.CheckedChanged += new System.EventHandler(this.ChbTextDependentCheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 27);
			this.label2.TabIndex = 3;
			this.label2.Text = "Microphone:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 27);
			this.label3.TabIndex = 4;
			this.label3.Text = "Format:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbMicrophones
			// 
			this.cbMicrophones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMicrophones.FormattingEnabled = true;
			this.cbMicrophones.Location = new System.Drawing.Point(89, 3);
			this.cbMicrophones.Name = "cbMicrophones";
			this.cbMicrophones.Size = new System.Drawing.Size(269, 21);
			this.cbMicrophones.TabIndex = 5;
			this.cbMicrophones.SelectedIndexChanged += new System.EventHandler(this.CbMicrophonesSelectedIndexChanged);
			// 
			// nudMaxFileSize
			// 
			this.nudMaxFileSize.DecimalPlaces = 3;
			this.nudMaxFileSize.Location = new System.Drawing.Point(89, 178);
			this.nudMaxFileSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.nudMaxFileSize.Name = "nudMaxFileSize";
			this.nudMaxFileSize.Size = new System.Drawing.Size(128, 20);
			this.nudMaxFileSize.TabIndex = 8;
			this.nudMaxFileSize.ValueChanged += new System.EventHandler(this.NudMaxFileSizeValueChanged);
			// 
			// VoicesSettingsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "VoicesSettingsPage";
			this.Size = new System.Drawing.Size(485, 202);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxFileSize)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox chbTextIndependant;
		private System.Windows.Forms.CheckBox chbTextDependent;
		private System.Windows.Forms.CheckBox chbUniquePhrases;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbFormats;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbMicrophones;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown nudMaxFileSize;
	}
}
