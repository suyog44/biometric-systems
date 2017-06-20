namespace Neurotec.Samples
{
	partial class SubjectStartPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjectStartPage));
			this.btnEnroll = new System.Windows.Forms.Button();
			this.btnIdentify = new System.Windows.Forms.Button();
			this.btnVerify = new System.Windows.Forms.Button();
			this.btnEnrollWithDuplicates = new System.Windows.Forms.Button();
			this.tbSubjectId = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.pbThumnail = new System.Windows.Forms.PictureBox();
			this.btnOpenImage = new System.Windows.Forms.Button();
			this.lblHint = new System.Windows.Forms.Label();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.gbEnrollData = new System.Windows.Forms.GroupBox();
			this.tlpEnrollData = new System.Windows.Forms.TableLayoutPanel();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.gbThumbnail = new System.Windows.Forms.GroupBox();
			this.btnPrintApplicantCard = new System.Windows.Forms.Button();
			this.btnPrintCriminalCard = new System.Windows.Forms.Button();
			this.lblQuery = new System.Windows.Forms.Label();
			this.tbQuery = new System.Windows.Forms.TextBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.lblSubjectId = new System.Windows.Forms.Label();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbThumnail)).BeginInit();
			this.gbEnrollData.SuspendLayout();
			this.tlpEnrollData.SuspendLayout();
			this.gbThumbnail.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnEnroll
			// 
			this.btnEnroll.Location = new System.Drawing.Point(3, 113);
			this.btnEnroll.Name = "btnEnroll";
			this.btnEnroll.Size = new System.Drawing.Size(112, 36);
			this.btnEnroll.TabIndex = 0;
			this.btnEnroll.Text = "Enroll";
			this.btnEnroll.UseVisualStyleBackColor = true;
			this.btnEnroll.Click += new System.EventHandler(this.BtnEnrollClick);
			// 
			// btnIdentify
			// 
			this.btnIdentify.Location = new System.Drawing.Point(3, 29);
			this.btnIdentify.Name = "btnIdentify";
			this.btnIdentify.Size = new System.Drawing.Size(112, 36);
			this.btnIdentify.TabIndex = 1;
			this.btnIdentify.Text = "Identify";
			this.btnIdentify.UseVisualStyleBackColor = true;
			this.btnIdentify.Click += new System.EventHandler(this.BtnIdentifyClick);
			// 
			// btnVerify
			// 
			this.btnVerify.Location = new System.Drawing.Point(3, 71);
			this.btnVerify.Name = "btnVerify";
			this.btnVerify.Size = new System.Drawing.Size(112, 36);
			this.btnVerify.TabIndex = 2;
			this.btnVerify.Text = "Verify";
			this.btnVerify.UseVisualStyleBackColor = true;
			this.btnVerify.Click += new System.EventHandler(this.BtnVerifyClick);
			// 
			// btnEnrollWithDuplicates
			// 
			this.btnEnrollWithDuplicates.Location = new System.Drawing.Point(3, 155);
			this.btnEnrollWithDuplicates.Name = "btnEnrollWithDuplicates";
			this.btnEnrollWithDuplicates.Size = new System.Drawing.Size(112, 36);
			this.btnEnrollWithDuplicates.TabIndex = 3;
			this.btnEnrollWithDuplicates.Text = "Enroll with duplicate check";
			this.btnEnrollWithDuplicates.UseVisualStyleBackColor = true;
			this.btnEnrollWithDuplicates.Click += new System.EventHandler(this.BtnEnrollWithDuplicatesClick);
			// 
			// tbSubjectId
			// 
			this.tbSubjectId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.tbSubjectId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.tableLayoutPanel3.SetColumnSpan(this.tbSubjectId, 2);
			this.tbSubjectId.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSubjectId.Location = new System.Drawing.Point(121, 3);
			this.tbSubjectId.Name = "tbSubjectId";
			this.tbSubjectId.Size = new System.Drawing.Size(662, 20);
			this.tbSubjectId.TabIndex = 5;
			this.tbSubjectId.TextChanged += new System.EventHandler(this.TbSubjectIdTextChanged);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.pbThumnail, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.btnOpenImage, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(237, 244);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// pbThumnail
			// 
			this.pbThumnail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel2.SetColumnSpan(this.pbThumnail, 2);
			this.pbThumnail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbThumnail.Enabled = false;
			this.pbThumnail.Location = new System.Drawing.Point(3, 32);
			this.pbThumnail.Name = "pbThumnail";
			this.pbThumnail.Size = new System.Drawing.Size(231, 209);
			this.pbThumnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbThumnail.TabIndex = 2;
			this.pbThumnail.TabStop = false;
			// 
			// btnOpenImage
			// 
			this.btnOpenImage.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.btnOpenImage.Location = new System.Drawing.Point(3, 3);
			this.btnOpenImage.Name = "btnOpenImage";
			this.btnOpenImage.Size = new System.Drawing.Size(90, 23);
			this.btnOpenImage.TabIndex = 1;
			this.btnOpenImage.Text = "&Open image";
			this.btnOpenImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpenImage.UseVisualStyleBackColor = true;
			this.btnOpenImage.Click += new System.EventHandler(this.BtnOpenImageClick);
			// 
			// lblHint
			// 
			this.lblHint.AutoSize = true;
			this.lblHint.BackColor = System.Drawing.Color.Orange;
			this.tableLayoutPanel3.SetColumnSpan(this.lblHint, 3);
			this.lblHint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHint.ForeColor = System.Drawing.Color.White;
			this.lblHint.Location = new System.Drawing.Point(3, 443);
			this.lblHint.Name = "lblHint";
			this.lblHint.Padding = new System.Windows.Forms.Padding(5);
			this.lblHint.Size = new System.Drawing.Size(780, 30);
			this.lblHint.TabIndex = 7;
			this.lblHint.Text = "Hint";
			this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Image = global::Neurotec.Samples.Properties.Resources.saveHS;
			this.btnSaveTemplate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSaveTemplate.Location = new System.Drawing.Point(3, 323);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(112, 36);
			this.btnSaveTemplate.TabIndex = 10;
			this.btnSaveTemplate.Text = "Save template";
			this.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// gbEnrollData
			// 
			this.tableLayoutPanel3.SetColumnSpan(this.gbEnrollData, 2);
			this.gbEnrollData.Controls.Add(this.tlpEnrollData);
			this.gbEnrollData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbEnrollData.Location = new System.Drawing.Point(121, 71);
			this.gbEnrollData.Name = "gbEnrollData";
			this.tableLayoutPanel3.SetRowSpan(this.gbEnrollData, 7);
			this.gbEnrollData.Size = new System.Drawing.Size(662, 288);
			this.gbEnrollData.TabIndex = 8;
			this.gbEnrollData.TabStop = false;
			this.gbEnrollData.Text = "Enroll data";
			// 
			// tlpEnrollData
			// 
			this.tlpEnrollData.ColumnCount = 2;
			this.tlpEnrollData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpEnrollData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpEnrollData.Controls.Add(this.propertyGrid, 1, 0);
			this.tlpEnrollData.Controls.Add(this.gbThumbnail, 0, 0);
			this.tlpEnrollData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpEnrollData.Location = new System.Drawing.Point(3, 16);
			this.tlpEnrollData.Name = "tlpEnrollData";
			this.tlpEnrollData.RowCount = 1;
			this.tlpEnrollData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpEnrollData.Size = new System.Drawing.Size(656, 269);
			this.tlpEnrollData.TabIndex = 0;
			// 
			// propertyGrid
			// 
			this.propertyGrid.CommandsVisibleIfAvailable = false;
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.HelpVisible = false;
			this.propertyGrid.LineColor = System.Drawing.Color.White;
			this.propertyGrid.Location = new System.Drawing.Point(252, 3);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
			this.propertyGrid.Size = new System.Drawing.Size(401, 263);
			this.propertyGrid.TabIndex = 14;
			this.propertyGrid.ToolbarVisible = false;
			this.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Control;
			// 
			// gbThumbnail
			// 
			this.gbThumbnail.Controls.Add(this.tableLayoutPanel2);
			this.gbThumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbThumbnail.Location = new System.Drawing.Point(3, 3);
			this.gbThumbnail.Name = "gbThumbnail";
			this.gbThumbnail.Size = new System.Drawing.Size(243, 263);
			this.gbThumbnail.TabIndex = 13;
			this.gbThumbnail.TabStop = false;
			this.gbThumbnail.Text = "Thumbnail";
			// 
			// btnPrintApplicantCard
			// 
			this.btnPrintApplicantCard.Location = new System.Drawing.Point(3, 281);
			this.btnPrintApplicantCard.Name = "btnPrintApplicantCard";
			this.btnPrintApplicantCard.Size = new System.Drawing.Size(112, 36);
			this.btnPrintApplicantCard.TabIndex = 14;
			this.btnPrintApplicantCard.Text = "Print applicant card";
			this.btnPrintApplicantCard.UseVisualStyleBackColor = true;
			this.btnPrintApplicantCard.Click += new System.EventHandler(this.BtnPrintApplicantCardClick);
			// 
			// btnPrintCriminalCard
			// 
			this.btnPrintCriminalCard.Location = new System.Drawing.Point(3, 239);
			this.btnPrintCriminalCard.Name = "btnPrintCriminalCard";
			this.btnPrintCriminalCard.Size = new System.Drawing.Size(112, 36);
			this.btnPrintCriminalCard.TabIndex = 15;
			this.btnPrintCriminalCard.Text = "Print criminal card";
			this.btnPrintCriminalCard.UseVisualStyleBackColor = true;
			this.btnPrintCriminalCard.Click += new System.EventHandler(this.BtnPrintCriminalCardClick);
			// 
			// lblQuery
			// 
			this.lblQuery.AutoSize = true;
			this.lblQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblQuery.Image = global::Neurotec.Samples.Properties.Resources.Help;
			this.lblQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblQuery.Location = new System.Drawing.Point(121, 26);
			this.lblQuery.Name = "lblQuery";
			this.lblQuery.Size = new System.Drawing.Size(53, 42);
			this.lblQuery.TabIndex = 16;
			this.lblQuery.Text = "      Query";
			this.lblQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip.SetToolTip(this.lblQuery, resources.GetString("lblQuery.ToolTip"));
			// 
			// tbQuery
			// 
			this.tbQuery.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.tbQuery.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.tbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbQuery.Location = new System.Drawing.Point(180, 37);
			this.tbQuery.Margin = new System.Windows.Forms.Padding(3, 11, 3, 3);
			this.tbQuery.Name = "tbQuery";
			this.tbQuery.Size = new System.Drawing.Size(603, 20);
			this.tbQuery.TabIndex = 17;
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 30000;
			this.toolTip.InitialDelay = 100;
			this.toolTip.ReshowDelay = 100;
			this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.lblSubjectId, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tbSubjectId, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.btnIdentify, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.lblQuery, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.tbQuery, 2, 1);
			this.tableLayoutPanel3.Controls.Add(this.btnVerify, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.btnEnroll, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.gbEnrollData, 1, 2);
			this.tableLayoutPanel3.Controls.Add(this.btnEnrollWithDuplicates, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.lblHint, 0, 10);
			this.tableLayoutPanel3.Controls.Add(this.btnSaveTemplate, 0, 8);
			this.tableLayoutPanel3.Controls.Add(this.btnPrintApplicantCard, 0, 7);
			this.tableLayoutPanel3.Controls.Add(this.btnPrintCriminalCard, 0, 6);
			this.tableLayoutPanel3.Controls.Add(this.btnUpdate, 0, 5);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 11;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(786, 473);
			this.tableLayoutPanel3.TabIndex = 9;
			// 
			// lblSubjectId
			// 
			this.lblSubjectId.AutoSize = true;
			this.lblSubjectId.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblSubjectId.Location = new System.Drawing.Point(3, 0);
			this.lblSubjectId.Name = "lblSubjectId";
			this.lblSubjectId.Size = new System.Drawing.Size(112, 26);
			this.lblSubjectId.TabIndex = 4;
			this.lblSubjectId.Text = "Subject id:";
			this.lblSubjectId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(3, 197);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(112, 36);
			this.btnUpdate.TabIndex = 18;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.BtnUpdateClick);
			// 
			// SubjectStartPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Name = "SubjectStartPage";
			this.Size = new System.Drawing.Size(786, 473);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbThumnail)).EndInit();
			this.gbEnrollData.ResumeLayout(false);
			this.tlpEnrollData.ResumeLayout(false);
			this.gbThumbnail.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnEnroll;
		private System.Windows.Forms.Button btnIdentify;
		private System.Windows.Forms.Button btnVerify;
		private System.Windows.Forms.Button btnEnrollWithDuplicates;
		private System.Windows.Forms.TextBox tbSubjectId;
		private System.Windows.Forms.Label lblHint;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnOpenImage;
		private System.Windows.Forms.PictureBox pbThumnail;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.GroupBox gbThumbnail;
		private System.Windows.Forms.Button btnPrintApplicantCard;
		private System.Windows.Forms.Button btnPrintCriminalCard;
		private System.Windows.Forms.GroupBox gbEnrollData;
		private System.Windows.Forms.TableLayoutPanel tlpEnrollData;
		private System.Windows.Forms.Label lblQuery;
		private System.Windows.Forms.TextBox tbQuery;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label lblSubjectId;
		private System.Windows.Forms.Button btnUpdate;
	}
}
