namespace Neurotec.Samples
{
	partial class StartTab
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartTab));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnChangeDb = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnSettings = new System.Windows.Forms.Button();
			this.btnAbout = new System.Windows.Forms.Button();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.btnChangeDb, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.btnNew, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnOpen, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnSettings, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnAbout, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.pbLogo, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 1, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(550, 349);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// btnChangeDb
			// 
			this.btnChangeDb.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnChangeDb.Location = new System.Drawing.Point(3, 220);
			this.btnChangeDb.Name = "btnChangeDb";
			this.btnChangeDb.Size = new System.Drawing.Size(108, 34);
			this.btnChangeDb.TabIndex = 9;
			this.btnChangeDb.Text = "&Change Database";
			this.btnChangeDb.UseVisualStyleBackColor = true;
			this.btnChangeDb.Click += new System.EventHandler(this.BtnChangeDbClick);
			// 
			// btnNew
			// 
			this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnNew.Location = new System.Drawing.Point(3, 24);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(108, 34);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "New Subject";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.BtnNewSubjectClick);
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnOpen.Location = new System.Drawing.Point(3, 111);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(108, 34);
			this.btnOpen.TabIndex = 1;
			this.btnOpen.Text = "Open Subject";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
			// 
			// btnSettings
			// 
			this.btnSettings.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnSettings.Location = new System.Drawing.Point(3, 177);
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(108, 34);
			this.btnSettings.TabIndex = 2;
			this.btnSettings.Text = "Settings";
			this.btnSettings.UseVisualStyleBackColor = true;
			this.btnSettings.Click += new System.EventHandler(this.BtnSettingsClick);
			// 
			// btnAbout
			// 
			this.btnAbout.Location = new System.Drawing.Point(3, 312);
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(108, 34);
			this.btnAbout.TabIndex = 4;
			this.btnAbout.Text = "About";
			this.btnAbout.UseVisualStyleBackColor = true;
			this.btnAbout.Click += new System.EventHandler(this.BtnAboutClick);
			// 
			// pbLogo
			// 
			this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
			this.pbLogo.Location = new System.Drawing.Point(293, 266);
			this.pbLogo.Name = "pbLogo";
			this.tableLayoutPanel1.SetRowSpan(this.pbLogo, 2);
			this.pbLogo.Size = new System.Drawing.Size(254, 80);
			this.pbLogo.TabIndex = 5;
			this.pbLogo.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(117, 0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.label1.Size = new System.Drawing.Size(430, 83);
			this.label1.TabIndex = 6;
			this.label1.Text = "Create new subject\r\n    Capture biometrics (fingers, faces, etc) from devices or " +
				"create them from files.\r\n    Enroll, identify or verify subject using local data" +
				"base or remote matching server";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(117, 93);
			this.label2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(430, 80);
			this.label2.TabIndex = 7;
			this.label2.Text = "Open subject template\r\n    Open from Neurotechnology template or other supported " +
				"standard templates\r\n    Enroll, identify or verify subject using local database " +
				"or remote matching server\r\n";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(117, 183);
			this.label3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(430, 32);
			this.label3.TabIndex = 8;
			this.label3.Text = "Change settings\r\n    Change feature detection, extraction, matching (etc) setting" +
				"s";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(117, 225);
			this.label4.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(430, 34);
			this.label4.TabIndex = 10;
			this.label4.Text = "Change db\r\n    Configure to use local database or remote matching server";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// StartTab
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(550, 0);
			this.Name = "StartTab";
			this.Size = new System.Drawing.Size(550, 349);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Button btnSettings;
		private System.Windows.Forms.Button btnAbout;
		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnChangeDb;
		private System.Windows.Forms.Label label4;
	}
}
