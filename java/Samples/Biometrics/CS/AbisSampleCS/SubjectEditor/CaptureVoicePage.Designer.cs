namespace Neurotec.Samples
{
	partial class CaptureVoicePage
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
			this.gbCapture = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnStop = new System.Windows.Forms.Button();
			this.voiceView = new Neurotec.Biometrics.Gui.NVoiceView();
			this.chbCaptureAutomatically = new System.Windows.Forms.CheckBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnForce = new System.Windows.Forms.Button();
			this.lblHint = new System.Windows.Forms.Label();
			this.gbPhrase = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.btnEdit = new System.Windows.Forms.Button();
			this.lblPhraseId = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cbPhrase = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblFilename = new System.Windows.Forms.Label();
			this.btnOpenFile = new System.Windows.Forms.Button();
			this.gbSource = new System.Windows.Forms.GroupBox();
			this.rbFromFile = new System.Windows.Forms.RadioButton();
			this.rbMicrophone = new System.Windows.Forms.RadioButton();
			this.lblPleaseSelect = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnFinish = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.busyIndicator = new Neurotec.Samples.BusyIndicator();
			this.gbCapture.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.gbPhrase.SuspendLayout();
			this.gbSource.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbCapture
			// 
			this.gbCapture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbCapture.Controls.Add(this.tableLayoutPanel1);
			this.gbCapture.Location = new System.Drawing.Point(3, 227);
			this.gbCapture.Name = "gbCapture";
			this.gbCapture.Size = new System.Drawing.Size(687, 109);
			this.gbCapture.TabIndex = 13;
			this.gbCapture.TabStop = false;
			this.gbCapture.Text = "Capture";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.btnStop, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.voiceView, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.chbCaptureAutomatically, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnStart, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnForce, 2, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 85);
			this.tableLayoutPanel1.TabIndex = 15;
			// 
			// btnStop
			// 
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnStop.Image = global::Neurotec.Samples.Properties.Resources.stop;
			this.btnStop.Location = new System.Drawing.Point(100, 26);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(83, 40);
			this.btnStop.TabIndex = 1;
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// voiceView
			// 
			this.voiceView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.voiceView.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.SetColumnSpan(this.voiceView, 2);
			this.voiceView.Location = new System.Drawing.Point(278, 26);
			this.voiceView.Name = "voiceView";
			this.voiceView.Size = new System.Drawing.Size(391, 58);
			this.voiceView.TabIndex = 14;
			this.voiceView.Text = "nVoiceView1";
			this.voiceView.Voice = null;
			// 
			// chbCaptureAutomatically
			// 
			this.chbCaptureAutomatically.AutoSize = true;
			this.chbCaptureAutomatically.Checked = true;
			this.chbCaptureAutomatically.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel1.SetColumnSpan(this.chbCaptureAutomatically, 3);
			this.chbCaptureAutomatically.Location = new System.Drawing.Point(3, 3);
			this.chbCaptureAutomatically.Name = "chbCaptureAutomatically";
			this.chbCaptureAutomatically.Size = new System.Drawing.Size(127, 17);
			this.chbCaptureAutomatically.TabIndex = 16;
			this.chbCaptureAutomatically.Text = "Capture automatically";
			this.chbCaptureAutomatically.UseVisualStyleBackColor = true;
			// 
			// btnStart
			// 
			this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStart.Image = global::Neurotec.Samples.Properties.Resources.play;
			this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnStart.Location = new System.Drawing.Point(3, 26);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(91, 40);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "&Start";
			this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// btnForce
			// 
			this.btnForce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.btnForce.Location = new System.Drawing.Point(189, 26);
			this.btnForce.Name = "btnForce";
			this.btnForce.Size = new System.Drawing.Size(83, 40);
			this.btnForce.TabIndex = 15;
			this.btnForce.Text = "&Force";
			this.btnForce.UseVisualStyleBackColor = true;
			this.btnForce.Click += new System.EventHandler(this.BtnForceClick);
			// 
			// lblHint
			// 
			this.lblHint.AutoSize = true;
			this.lblHint.BackColor = System.Drawing.Color.Orange;
			this.lblHint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHint.ForeColor = System.Drawing.Color.White;
			this.lblHint.Location = new System.Drawing.Point(23, 0);
			this.lblHint.Name = "lblHint";
			this.lblHint.Size = new System.Drawing.Size(661, 20);
			this.lblHint.TabIndex = 15;
			this.lblHint.Text = "Extracting record. Please say phrase ...";
			this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// gbPhrase
			// 
			this.gbPhrase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbPhrase.Controls.Add(this.label8);
			this.gbPhrase.Controls.Add(this.btnEdit);
			this.gbPhrase.Controls.Add(this.lblPhraseId);
			this.gbPhrase.Controls.Add(this.label5);
			this.gbPhrase.Controls.Add(this.cbPhrase);
			this.gbPhrase.Controls.Add(this.label4);
			this.gbPhrase.Location = new System.Drawing.Point(3, 16);
			this.gbPhrase.Name = "gbPhrase";
			this.gbPhrase.Size = new System.Drawing.Size(687, 110);
			this.gbPhrase.TabIndex = 8;
			this.gbPhrase.TabStop = false;
			this.gbPhrase.Text = "Secret phrase (Please answer the question)";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(6, 70);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(513, 29);
			this.label8.TabIndex = 5;
			this.label8.Text = "Phrase should be secret answer to the selected question. Phrase duration should b" +
				"e at least about 6 seconds or 4 words.";
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.Location = new System.Drawing.Point(606, 16);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 2;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.BtnEditClick);
			// 
			// lblPhraseId
			// 
			this.lblPhraseId.Location = new System.Drawing.Point(96, 47);
			this.lblPhraseId.Name = "lblPhraseId";
			this.lblPhraseId.Size = new System.Drawing.Size(270, 23);
			this.lblPhraseId.TabIndex = 4;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 13);
			this.label5.TabIndex = 3;
			this.label5.Text = "Phrase Id:";
			// 
			// cbPhrase
			// 
			this.cbPhrase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbPhrase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPhrase.FormattingEnabled = true;
			this.cbPhrase.Location = new System.Drawing.Point(113, 18);
			this.cbPhrase.Name = "cbPhrase";
			this.cbPhrase.Size = new System.Drawing.Size(487, 21);
			this.cbPhrase.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 21);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(101, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Selected phrase ID:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 129);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "2. Please select sound source:";
			// 
			// lblFilename
			// 
			this.lblFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblFilename.Location = new System.Drawing.Point(90, 16);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(507, 23);
			this.lblFilename.TabIndex = 3;
			this.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOpenFile
			// 
			this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenFile.Enabled = false;
			this.btnOpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOpenFile.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.btnOpenFile.Location = new System.Drawing.Point(596, 16);
			this.btnOpenFile.Name = "btnOpenFile";
			this.btnOpenFile.Size = new System.Drawing.Size(82, 23);
			this.btnOpenFile.TabIndex = 4;
			this.btnOpenFile.Text = "&Open file";
			this.btnOpenFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpenFile.UseVisualStyleBackColor = true;
			this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFileClick);
			// 
			// gbSource
			// 
			this.gbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbSource.Controls.Add(this.lblFilename);
			this.gbSource.Controls.Add(this.btnOpenFile);
			this.gbSource.Controls.Add(this.rbFromFile);
			this.gbSource.Controls.Add(this.rbMicrophone);
			this.gbSource.Location = new System.Drawing.Point(3, 145);
			this.gbSource.Name = "gbSource";
			this.gbSource.Size = new System.Drawing.Size(687, 76);
			this.gbSource.TabIndex = 10;
			this.gbSource.TabStop = false;
			this.gbSource.Text = "Source";
			// 
			// rbFromFile
			// 
			this.rbFromFile.AutoSize = true;
			this.rbFromFile.Checked = true;
			this.rbFromFile.Location = new System.Drawing.Point(6, 19);
			this.rbFromFile.Name = "rbFromFile";
			this.rbFromFile.Size = new System.Drawing.Size(72, 17);
			this.rbFromFile.TabIndex = 2;
			this.rbFromFile.TabStop = true;
			this.rbFromFile.Text = "Sound file";
			this.rbFromFile.UseVisualStyleBackColor = true;
			this.rbFromFile.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// rbMicrophone
			// 
			this.rbMicrophone.AutoSize = true;
			this.rbMicrophone.Location = new System.Drawing.Point(6, 42);
			this.rbMicrophone.Name = "rbMicrophone";
			this.rbMicrophone.Size = new System.Drawing.Size(81, 17);
			this.rbMicrophone.TabIndex = 0;
			this.rbMicrophone.Text = "Microphone";
			this.rbMicrophone.UseVisualStyleBackColor = true;
			this.rbMicrophone.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// lblPleaseSelect
			// 
			this.lblPleaseSelect.AutoSize = true;
			this.lblPleaseSelect.Location = new System.Drawing.Point(3, 0);
			this.lblPleaseSelect.Name = "lblPleaseSelect";
			this.lblPleaseSelect.Size = new System.Drawing.Size(222, 13);
			this.lblPleaseSelect.TabIndex = 7;
			this.lblPleaseSelect.Text = "1. Please select secret phrase ID from the list:";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Controls.Add(this.lblPleaseSelect, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.gbCapture, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.gbPhrase, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.gbSource, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.btnFinish, 0, 6);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 5);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 7;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(693, 398);
			this.tableLayoutPanel2.TabIndex = 16;
			// 
			// btnFinish
			// 
			this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFinish.Location = new System.Drawing.Point(615, 372);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(75, 23);
			this.btnFinish.TabIndex = 16;
			this.btnFinish.Text = "&Finish";
			this.btnFinish.UseVisualStyleBackColor = true;
			this.btnFinish.Click += new System.EventHandler(this.BtnFinishClick);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.busyIndicator, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.lblHint, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 342);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(687, 20);
			this.tableLayoutPanel3.TabIndex = 17;
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
			// CaptureVoicePage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "CaptureVoicePage";
			this.Size = new System.Drawing.Size(693, 398);
			this.gbCapture.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.gbPhrase.ResumeLayout(false);
			this.gbPhrase.PerformLayout();
			this.gbSource.ResumeLayout(false);
			this.gbSource.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbCapture;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.GroupBox gbPhrase;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Label lblPhraseId;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbPhrase;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblFilename;
		private System.Windows.Forms.Button btnOpenFile;
		private System.Windows.Forms.GroupBox gbSource;
		private System.Windows.Forms.RadioButton rbFromFile;
		private System.Windows.Forms.RadioButton rbMicrophone;
		private System.Windows.Forms.Label lblPleaseSelect;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private Neurotec.Biometrics.Gui.NVoiceView voiceView;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblHint;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnFinish;
		private System.Windows.Forms.Button btnForce;
		private System.Windows.Forms.CheckBox chbCaptureAutomatically;
		private BusyIndicator busyIndicator;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	}
}
