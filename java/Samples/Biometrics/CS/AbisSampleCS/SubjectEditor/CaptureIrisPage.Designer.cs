namespace Neurotec.Samples
{
	partial class CaptureIrisPage
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
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btnForce = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.gbCaptureOptions = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.cbPosition = new System.Windows.Forms.ComboBox();
			this.btnCapture = new System.Windows.Forms.Button();
			this.rbFile = new System.Windows.Forms.RadioButton();
			this.rbIrisScanner = new System.Windows.Forms.RadioButton();
			this.btnOpen = new System.Windows.Forms.Button();
			this.chbCaptureAutomatically = new System.Windows.Forms.CheckBox();
			this.irisView = new Neurotec.Biometrics.Gui.NIrisView();
			this.btnFinish = new System.Windows.Forms.Button();
			this.horizontalZoomSlider = new Neurotec.Gui.NViewZoomSlider();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.busyIndicator = new Neurotec.Samples.BusyIndicator();
			this.tableLayoutPanel1.SuspendLayout();
			this.gbCaptureOptions.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(454, 381);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.BackColor = System.Drawing.Color.Red;
			this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.White;
			this.lblStatus.Location = new System.Drawing.Point(23, 0);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(581, 20);
			this.lblStatus.TabIndex = 7;
			this.lblStatus.Text = "Extraction status: None";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnForce
			// 
			this.btnForce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.btnForce.Location = new System.Drawing.Point(373, 381);
			this.btnForce.Name = "btnForce";
			this.btnForce.Size = new System.Drawing.Size(75, 23);
			this.btnForce.TabIndex = 12;
			this.btnForce.Text = "&Force";
			this.btnForce.UseVisualStyleBackColor = true;
			this.btnForce.Click += new System.EventHandler(this.BtnForceClick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.gbCaptureOptions, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.irisView, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnFinish, 3, 3);
			this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.horizontalZoomSlider, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.btnForce, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(613, 407);
			this.tableLayoutPanel1.TabIndex = 10;
			// 
			// gbCaptureOptions
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.gbCaptureOptions, 4);
			this.gbCaptureOptions.Controls.Add(this.tableLayoutPanel2);
			this.gbCaptureOptions.Location = new System.Drawing.Point(3, 3);
			this.gbCaptureOptions.Name = "gbCaptureOptions";
			this.gbCaptureOptions.Size = new System.Drawing.Size(392, 106);
			this.gbCaptureOptions.TabIndex = 9;
			this.gbCaptureOptions.TabStop = false;
			this.gbCaptureOptions.Text = "Capture options";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 6;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.cbPosition, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.btnCapture, 2, 3);
			this.tableLayoutPanel2.Controls.Add(this.rbFile, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.rbIrisScanner, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnOpen, 5, 3);
			this.tableLayoutPanel2.Controls.Add(this.chbCaptureAutomatically, 1, 1);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 19);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(380, 81);
			this.tableLayoutPanel2.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "Position:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbPosition
			// 
			this.cbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPosition.FormattingEnabled = true;
			this.cbPosition.Location = new System.Drawing.Point(56, 54);
			this.cbPosition.Name = "cbPosition";
			this.cbPosition.Size = new System.Drawing.Size(121, 21);
			this.cbPosition.TabIndex = 2;
			// 
			// btnCapture
			// 
			this.btnCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCapture.Location = new System.Drawing.Point(189, 54);
			this.btnCapture.Name = "btnCapture";
			this.btnCapture.Size = new System.Drawing.Size(75, 23);
			this.btnCapture.TabIndex = 11;
			this.btnCapture.Text = "Ca&pture";
			this.btnCapture.UseVisualStyleBackColor = true;
			this.btnCapture.Click += new System.EventHandler(this.BtnCaptureClick);
			// 
			// rbFile
			// 
			this.rbFile.AutoSize = true;
			this.rbFile.Location = new System.Drawing.Point(3, 26);
			this.rbFile.Name = "rbFile";
			this.rbFile.Size = new System.Drawing.Size(41, 17);
			this.rbFile.TabIndex = 1;
			this.rbFile.Text = "File";
			this.rbFile.UseVisualStyleBackColor = true;
			this.rbFile.CheckedChanged += new System.EventHandler(this.RbFileCheckedChanged);
			// 
			// rbIrisScanner
			// 
			this.rbIrisScanner.AutoSize = true;
			this.rbIrisScanner.Checked = true;
			this.tableLayoutPanel2.SetColumnSpan(this.rbIrisScanner, 4);
			this.rbIrisScanner.Location = new System.Drawing.Point(3, 3);
			this.rbIrisScanner.Name = "rbIrisScanner";
			this.rbIrisScanner.Size = new System.Drawing.Size(65, 17);
			this.rbIrisScanner.TabIndex = 0;
			this.rbIrisScanner.TabStop = true;
			this.rbIrisScanner.Text = "Scanner";
			this.rbIrisScanner.UseVisualStyleBackColor = true;
			this.rbIrisScanner.CheckedChanged += new System.EventHandler(this.RbIrisScannerCheckedChanged);
			// 
			// btnOpen
			// 
			this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOpen.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.btnOpen.Location = new System.Drawing.Point(270, 54);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(103, 23);
			this.btnOpen.TabIndex = 10;
			this.btnOpen.Text = "&Open image";
			this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
			// 
			// chbCaptureAutomatically
			// 
			this.chbCaptureAutomatically.AutoSize = true;
			this.chbCaptureAutomatically.Checked = true;
			this.chbCaptureAutomatically.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbCaptureAutomatically.Location = new System.Drawing.Point(56, 26);
			this.chbCaptureAutomatically.Name = "chbCaptureAutomatically";
			this.chbCaptureAutomatically.Size = new System.Drawing.Size(127, 17);
			this.chbCaptureAutomatically.TabIndex = 13;
			this.chbCaptureAutomatically.Text = "Capture automatically";
			this.chbCaptureAutomatically.UseVisualStyleBackColor = true;
			// 
			// irisView
			// 
			this.irisView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.irisView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.irisView, 4);
			this.irisView.InnerBoundaryColor = System.Drawing.Color.Red;
			this.irisView.InnerBoundaryWidth = 2;
			this.irisView.Iris = null;
			this.irisView.Location = new System.Drawing.Point(3, 115);
			this.irisView.Name = "irisView";
			this.irisView.OuterBoundaryColor = System.Drawing.Color.GreenYellow;
			this.irisView.OuterBoundaryWidth = 2;
			this.irisView.Size = new System.Drawing.Size(607, 234);
			this.irisView.TabIndex = 5;
			// 
			// btnFinish
			// 
			this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFinish.Location = new System.Drawing.Point(535, 381);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(75, 23);
			this.btnFinish.TabIndex = 11;
			this.btnFinish.Text = "&Finish";
			this.btnFinish.UseVisualStyleBackColor = true;
			this.btnFinish.Click += new System.EventHandler(this.BtnFinishClick);
			// 
			// horizontalZoomSlider
			// 
			this.horizontalZoomSlider.Location = new System.Drawing.Point(3, 381);
			this.horizontalZoomSlider.Name = "horizontalZoomSlider";
			this.horizontalZoomSlider.Size = new System.Drawing.Size(270, 23);
			this.horizontalZoomSlider.TabIndex = 12;
			this.horizontalZoomSlider.View = this.irisView;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 4);
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.busyIndicator, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.lblStatus, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 355);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(607, 20);
			this.tableLayoutPanel3.TabIndex = 13;
			// 
			// busyIndicator
			// 
			this.busyIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.busyIndicator.Location = new System.Drawing.Point(3, 3);
			this.busyIndicator.Name = "busyIndicator";
			this.busyIndicator.Size = new System.Drawing.Size(14, 14);
			this.busyIndicator.TabIndex = 0;
			this.busyIndicator.Visible = false;
			// 
			// CaptureIrisPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "CaptureIrisPage";
			this.Size = new System.Drawing.Size(613, 407);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.gbCaptureOptions.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnForce;
		private System.Windows.Forms.GroupBox gbCaptureOptions;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbPosition;
		private System.Windows.Forms.Button btnCapture;
		private System.Windows.Forms.RadioButton rbFile;
		private System.Windows.Forms.RadioButton rbIrisScanner;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.CheckBox chbCaptureAutomatically;
		private Neurotec.Biometrics.Gui.NIrisView irisView;
		private Neurotec.Gui.NViewZoomSlider horizontalZoomSlider;
		private System.Windows.Forms.Button btnFinish;
		private BusyIndicator busyIndicator;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	}
}
