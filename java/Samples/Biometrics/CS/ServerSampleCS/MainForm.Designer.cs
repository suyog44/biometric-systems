namespace Neurotec.Samples
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnConnection = new System.Windows.Forms.Button();
			this.btnDeduplication = new System.Windows.Forms.Button();
			this.btnEnroll = new System.Windows.Forms.Button();
			this.btnTestSpeed = new System.Windows.Forms.Button();
			this.btnSettings = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer.IsSplitterFixed = true;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.flowLayoutPanel1);
			this.splitContainer.Size = new System.Drawing.Size(843, 414);
			this.splitContainer.SplitterDistance = 194;
			this.splitContainer.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.flowLayoutPanel1.Controls.Add(this.btnConnection);
			this.flowLayoutPanel1.Controls.Add(this.btnDeduplication);
			this.flowLayoutPanel1.Controls.Add(this.btnEnroll);
			this.flowLayoutPanel1.Controls.Add(this.btnTestSpeed);
			this.flowLayoutPanel1.Controls.Add(this.btnSettings);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(194, 414);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btnConnection
			// 
			this.btnConnection.Image = global::Neurotec.Samples.Properties.Resources.settings;
			this.btnConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnConnection.Location = new System.Drawing.Point(3, 3);
			this.btnConnection.Name = "btnConnection";
			this.btnConnection.Size = new System.Drawing.Size(185, 41);
			this.btnConnection.TabIndex = 0;
			this.btnConnection.Text = "Change connection settings";
			this.btnConnection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnConnection.UseVisualStyleBackColor = true;
			this.btnConnection.Click += new System.EventHandler(this.BtnConnectionClick);
			// 
			// btnDeduplication
			// 
			this.btnDeduplication.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.btnDeduplication.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Goldenrod;
			this.btnDeduplication.Location = new System.Drawing.Point(3, 50);
			this.btnDeduplication.Name = "btnDeduplication";
			this.btnDeduplication.Size = new System.Drawing.Size(185, 41);
			this.btnDeduplication.TabIndex = 3;
			this.btnDeduplication.Text = "Deduplication";
			this.btnDeduplication.UseVisualStyleBackColor = false;
			this.btnDeduplication.Click += new System.EventHandler(this.BtnDeduplicationClick);
			// 
			// btnEnroll
			// 
			this.btnEnroll.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.btnEnroll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Goldenrod;
			this.btnEnroll.Location = new System.Drawing.Point(3, 97);
			this.btnEnroll.Name = "btnEnroll";
			this.btnEnroll.Size = new System.Drawing.Size(185, 41);
			this.btnEnroll.TabIndex = 2;
			this.btnEnroll.Text = "Enroll templates";
			this.btnEnroll.UseVisualStyleBackColor = false;
			this.btnEnroll.Click += new System.EventHandler(this.BtnEnrollClick);
			// 
			// btnTestSpeed
			// 
			this.btnTestSpeed.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.btnTestSpeed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Goldenrod;
			this.btnTestSpeed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnTestSpeed.Location = new System.Drawing.Point(3, 144);
			this.btnTestSpeed.Name = "btnTestSpeed";
			this.btnTestSpeed.Size = new System.Drawing.Size(185, 41);
			this.btnTestSpeed.TabIndex = 3;
			this.btnTestSpeed.Text = "Calculate/Test Accelerator matching speed";
			this.btnTestSpeed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnTestSpeed.UseVisualStyleBackColor = false;
			this.btnTestSpeed.Click += new System.EventHandler(this.BtnTestSpeedClick);
			// 
			// btnSettings
			// 
			this.btnSettings.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Goldenrod;
			this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSettings.Location = new System.Drawing.Point(3, 191);
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(185, 41);
			this.btnSettings.TabIndex = 4;
			this.btnSettings.Text = "Change matching settings";
			this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSettings.UseVisualStyleBackColor = false;
			this.btnSettings.Click += new System.EventHandler(this.BtnSettingsClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(843, 414);
			this.Controls.Add(this.splitContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Server Sample";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.VisibleChanged += new System.EventHandler(this.MainFormVisibleChanged);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnConnection;
		private System.Windows.Forms.Button btnTestSpeed;
		private System.Windows.Forms.Button btnEnroll;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button btnDeduplication;
		private System.Windows.Forms.Button btnSettings;
	}
}
