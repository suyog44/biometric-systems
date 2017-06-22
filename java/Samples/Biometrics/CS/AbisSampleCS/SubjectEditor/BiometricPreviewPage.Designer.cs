namespace Neurotec.Samples
{
	partial class BiometricPreviewPage
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
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnSave = new System.Windows.Forms.Button();
			this.horizontalZoomSlider = new Neurotec.Gui.NViewZoomSlider();
			this.chbShowBinarized = new System.Windows.Forms.CheckBox();
			this.btnFinish = new System.Windows.Forms.Button();
			this.generalizeProgressView = new Neurotec.Samples.Common.GeneralizeProgressView();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panelView = new System.Windows.Forms.Panel();
			this.icaoWarningView = new Neurotec.Samples.IcaoWarningView();
			this.tableLayoutPanel.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.generalizeProgressView, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 3;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(846, 480);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel.SetColumnSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.horizontalZoomSlider, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.chbShowBinarized, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnFinish, 3, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 448);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(840, 29);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// btnSave
			// 
			this.btnSave.AutoSize = true;
			this.btnSave.Image = global::Neurotec.Samples.Properties.Resources.saveHS;
			this.btnSave.Location = new System.Drawing.Point(3, 3);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(72, 23);
			this.btnSave.TabIndex = 8;
			this.btnSave.Text = "Save";
			this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// horizontalZoomSlider
			// 
			this.horizontalZoomSlider.BackColor = System.Drawing.SystemColors.Control;
			this.horizontalZoomSlider.Dock = System.Windows.Forms.DockStyle.Fill;
			this.horizontalZoomSlider.Location = new System.Drawing.Point(81, 3);
			this.horizontalZoomSlider.Name = "horizontalZoomSlider";
			this.horizontalZoomSlider.Size = new System.Drawing.Size(264, 23);
			this.horizontalZoomSlider.TabIndex = 7;
			this.horizontalZoomSlider.View = null;
			// 
			// chbShowBinarized
			// 
			this.chbShowBinarized.AutoSize = true;
			this.chbShowBinarized.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chbShowBinarized.Location = new System.Drawing.Point(351, 3);
			this.chbShowBinarized.Name = "chbShowBinarized";
			this.chbShowBinarized.Size = new System.Drawing.Size(404, 23);
			this.chbShowBinarized.TabIndex = 6;
			this.chbShowBinarized.Text = "Show binarized image";
			this.chbShowBinarized.UseVisualStyleBackColor = true;
			this.chbShowBinarized.CheckedChanged += new System.EventHandler(this.ChbShowBinarizedCheckedChanged);
			// 
			// btnFinish
			// 
			this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFinish.Location = new System.Drawing.Point(761, 3);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(76, 23);
			this.btnFinish.TabIndex = 4;
			this.btnFinish.Text = "&Finish";
			this.btnFinish.UseVisualStyleBackColor = true;
			this.btnFinish.Click += new System.EventHandler(this.BtnFinishClick);
			// 
			// generalizeProgressView
			// 
			this.generalizeProgressView.AutoSize = true;
			this.generalizeProgressView.Biometrics = null;
			this.tableLayoutPanel.SetColumnSpan(this.generalizeProgressView, 2);
			this.generalizeProgressView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.generalizeProgressView.EnableMouseSelection = true;
			this.generalizeProgressView.Generalized = null;
			this.generalizeProgressView.IcaoView = null;
			this.generalizeProgressView.Location = new System.Drawing.Point(3, 407);
			this.generalizeProgressView.Name = "generalizeProgressView";
			this.generalizeProgressView.Selected = null;
			this.generalizeProgressView.Size = new System.Drawing.Size(840, 35);
			this.generalizeProgressView.StatusText = "";
			this.generalizeProgressView.TabIndex = 7;
			this.generalizeProgressView.View = null;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel.SetColumnSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.icaoWarningView, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.panelView, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(840, 398);
			this.tableLayoutPanel2.TabIndex = 9;
			// 
			// panelView
			// 
			this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelView.Location = new System.Drawing.Point(153, 3);
			this.panelView.Name = "panelView";
			this.panelView.Size = new System.Drawing.Size(684, 392);
			this.panelView.TabIndex = 1;
			// 
			// icaoWarningView
			// 
			this.icaoWarningView.AutoScroll = true;
			this.icaoWarningView.AutoSize = true;
			this.icaoWarningView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.icaoWarningView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.icaoWarningView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.icaoWarningView.IndeterminateColor = System.Drawing.Color.Orange;
			this.icaoWarningView.Location = new System.Drawing.Point(3, 3);
			this.icaoWarningView.Name = "icaoWarningView";
			this.icaoWarningView.NoWarningColor = System.Drawing.Color.Green;
			this.icaoWarningView.Size = new System.Drawing.Size(144, 392);
			this.icaoWarningView.TabIndex = 9;
			this.icaoWarningView.WarningColor = System.Drawing.Color.Red;
			// 
			// BiometricPreviewPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "BiometricPreviewPage";
			this.Size = new System.Drawing.Size(846, 480);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Button btnFinish;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private Neurotec.Samples.Common.GeneralizeProgressView generalizeProgressView;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnSave;
		private Neurotec.Gui.NViewZoomSlider horizontalZoomSlider;
		private System.Windows.Forms.CheckBox chbShowBinarized;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private IcaoWarningView icaoWarningView;
		private System.Windows.Forms.Panel panelView;
	}
}
