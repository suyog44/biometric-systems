Partial Public Class SubjectStartPage
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(SubjectStartPage))
		Me.btnEnroll = New System.Windows.Forms.Button()
		Me.btnIdentify = New System.Windows.Forms.Button()
		Me.btnVerify = New System.Windows.Forms.Button()
		Me.btnEnrollWithDuplicates = New System.Windows.Forms.Button()
		Me.tbSubjectId = New System.Windows.Forms.TextBox()
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
		Me.pbThumbnail = New System.Windows.Forms.PictureBox()
		Me.btnOpenImage = New System.Windows.Forms.Button()
		Me.lblHint = New System.Windows.Forms.Label()
		Me.btnSaveTemplate = New System.Windows.Forms.Button()
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog()
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
		Me.gbEnrollData = New System.Windows.Forms.GroupBox()
		Me.tlpEnrollData = New System.Windows.Forms.TableLayoutPanel()
		Me.propertyGrid = New System.Windows.Forms.PropertyGrid()
		Me.gbThumbnail = New System.Windows.Forms.GroupBox()
		Me.btnPrintApplicantCard = New System.Windows.Forms.Button()
		Me.btnPrintCriminalCard = New System.Windows.Forms.Button()
		Me.lblQuery = New System.Windows.Forms.Label()
		Me.tbQuery = New System.Windows.Forms.TextBox()
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
		Me.lblSubjectId = New System.Windows.Forms.Label()
		Me.btnUpdate = New System.Windows.Forms.Button()
		Me.tableLayoutPanel2.SuspendLayout()
		CType(Me.pbThumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gbEnrollData.SuspendLayout()
		Me.tlpEnrollData.SuspendLayout()
		Me.gbThumbnail.SuspendLayout()
		Me.tableLayoutPanel3.SuspendLayout()
		Me.SuspendLayout()
		' 
		' btnEnroll
		' 
		Me.btnEnroll.Location = New System.Drawing.Point(3, 113)
		Me.btnEnroll.Name = "btnEnroll"
		Me.btnEnroll.Size = New System.Drawing.Size(112, 36)
		Me.btnEnroll.TabIndex = 0
		Me.btnEnroll.Text = "Enroll"
		Me.btnEnroll.UseVisualStyleBackColor = True
		' 
		' btnIdentify
		' 
		Me.btnIdentify.Location = New System.Drawing.Point(3, 29)
		Me.btnIdentify.Name = "btnIdentify"
		Me.btnIdentify.Size = New System.Drawing.Size(112, 36)
		Me.btnIdentify.TabIndex = 1
		Me.btnIdentify.Text = "Identify"
		Me.btnIdentify.UseVisualStyleBackColor = True
		' 
		' btnVerify
		' 
		Me.btnVerify.Location = New System.Drawing.Point(3, 71)
		Me.btnVerify.Name = "btnVerify"
		Me.btnVerify.Size = New System.Drawing.Size(112, 36)
		Me.btnVerify.TabIndex = 2
		Me.btnVerify.Text = "Verify"
		Me.btnVerify.UseVisualStyleBackColor = True
		' 
		' btnEnrollWithDuplicates
		' 
		Me.btnEnrollWithDuplicates.Location = New System.Drawing.Point(3, 155)
		Me.btnEnrollWithDuplicates.Name = "btnEnrollWithDuplicates"
		Me.btnEnrollWithDuplicates.Size = New System.Drawing.Size(112, 36)
		Me.btnEnrollWithDuplicates.TabIndex = 3
		Me.btnEnrollWithDuplicates.Text = "Enroll with duplicate check"
		Me.btnEnrollWithDuplicates.UseVisualStyleBackColor = True
		' 
		' tbSubjectId
		' 
		Me.tbSubjectId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
		Me.tbSubjectId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
		Me.tableLayoutPanel3.SetColumnSpan(Me.tbSubjectId, 2)
		Me.tbSubjectId.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tbSubjectId.Location = New System.Drawing.Point(121, 3)
		Me.tbSubjectId.Name = "tbSubjectId"
		Me.tbSubjectId.Size = New System.Drawing.Size(662, 20)
		Me.tbSubjectId.TabIndex = 5
		' 
		' tableLayoutPanel2
		' 
		Me.tableLayoutPanel2.ColumnCount = 2
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tableLayoutPanel2.Controls.Add(Me.pbThumbnail, 0, 1)
		Me.tableLayoutPanel2.Controls.Add(Me.btnOpenImage, 0, 0)
		Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 2
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(237, 244)
		Me.tableLayoutPanel2.TabIndex = 0
		' 
		' pbThumnail
		' 
		Me.pbThumbnail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.tableLayoutPanel2.SetColumnSpan(Me.pbThumbnail, 2)
		Me.pbThumbnail.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pbThumbnail.Enabled = False
		Me.pbThumbnail.Location = New System.Drawing.Point(3, 32)
		Me.pbThumbnail.Name = "pbThumnail"
		Me.pbThumbnail.Size = New System.Drawing.Size(231, 209)
		Me.pbThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.pbThumbnail.TabIndex = 2
		Me.pbThumbnail.TabStop = False
		' 
		' btnOpenImage
		' 
		Me.btnOpenImage.Image = Global.Neurotec.Samples.My.Resources.openfolderHS
		Me.btnOpenImage.Location = New System.Drawing.Point(3, 3)
		Me.btnOpenImage.Name = "btnOpenImage"
		Me.btnOpenImage.Size = New System.Drawing.Size(90, 23)
		Me.btnOpenImage.TabIndex = 1
		Me.btnOpenImage.Text = "&Open image"
		Me.btnOpenImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpenImage.UseVisualStyleBackColor = True
		' 
		' lblHint
		' 
		Me.lblHint.AutoSize = True
		Me.lblHint.BackColor = System.Drawing.Color.Orange
		Me.tableLayoutPanel3.SetColumnSpan(Me.lblHint, 3)
		Me.lblHint.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblHint.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.lblHint.ForeColor = System.Drawing.Color.White
		Me.lblHint.Location = New System.Drawing.Point(3, 443)
		Me.lblHint.Name = "lblHint"
		Me.lblHint.Padding = New System.Windows.Forms.Padding(5)
		Me.lblHint.Size = New System.Drawing.Size(780, 30)
		Me.lblHint.TabIndex = 7
		Me.lblHint.Text = "Hint"
		Me.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		' 
		' btnSaveTemplate
		' 
		Me.btnSaveTemplate.Image = Global.Neurotec.Samples.My.Resources.saveHS
		Me.btnSaveTemplate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnSaveTemplate.Location = New System.Drawing.Point(3, 323)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(112, 36)
		Me.btnSaveTemplate.TabIndex = 10
		Me.btnSaveTemplate.Text = "Save template"
		Me.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		' 
		' gbEnrollData
		' 
		Me.tableLayoutPanel3.SetColumnSpan(Me.gbEnrollData, 2)
		Me.gbEnrollData.Controls.Add(Me.tlpEnrollData)
		Me.gbEnrollData.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbEnrollData.Location = New System.Drawing.Point(121, 71)
		Me.gbEnrollData.Name = "gbEnrollData"
		Me.tableLayoutPanel3.SetRowSpan(Me.gbEnrollData, 7)
		Me.gbEnrollData.Size = New System.Drawing.Size(662, 288)
		Me.gbEnrollData.TabIndex = 8
		Me.gbEnrollData.TabStop = False
		Me.gbEnrollData.Text = "Enroll data"
		' 
		' tlpEnrollData
		' 
		Me.tlpEnrollData.ColumnCount = 2
		Me.tlpEnrollData.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tlpEnrollData.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tlpEnrollData.Controls.Add(Me.propertyGrid, 1, 0)
		Me.tlpEnrollData.Controls.Add(Me.gbThumbnail, 0, 0)
		Me.tlpEnrollData.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tlpEnrollData.Location = New System.Drawing.Point(3, 16)
		Me.tlpEnrollData.Name = "tlpEnrollData"
		Me.tlpEnrollData.RowCount = 1
		Me.tlpEnrollData.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tlpEnrollData.Size = New System.Drawing.Size(656, 269)
		Me.tlpEnrollData.TabIndex = 0
		' 
		' propertyGrid
		' 
		Me.propertyGrid.CommandsVisibleIfAvailable = False
		Me.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.propertyGrid.HelpVisible = False
		Me.propertyGrid.LineColor = System.Drawing.Color.White
		Me.propertyGrid.Location = New System.Drawing.Point(252, 3)
		Me.propertyGrid.Name = "propertyGrid"
		Me.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort
		Me.propertyGrid.Size = New System.Drawing.Size(401, 263)
		Me.propertyGrid.TabIndex = 14
		Me.propertyGrid.ToolbarVisible = False
		Me.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Control
		' 
		' gbThumbnail
		' 
		Me.gbThumbnail.Controls.Add(Me.tableLayoutPanel2)
		Me.gbThumbnail.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbThumbnail.Location = New System.Drawing.Point(3, 3)
		Me.gbThumbnail.Name = "gbThumbnail"
		Me.gbThumbnail.Size = New System.Drawing.Size(243, 263)
		Me.gbThumbnail.TabIndex = 13
		Me.gbThumbnail.TabStop = False
		Me.gbThumbnail.Text = "Thumbnail"
		' 
		' btnPrintApplicantCard
		' 
		Me.btnPrintApplicantCard.Location = New System.Drawing.Point(3, 281)
		Me.btnPrintApplicantCard.Name = "btnPrintApplicantCard"
		Me.btnPrintApplicantCard.Size = New System.Drawing.Size(112, 36)
		Me.btnPrintApplicantCard.TabIndex = 14
		Me.btnPrintApplicantCard.Text = "Print applicant card"
		Me.btnPrintApplicantCard.UseVisualStyleBackColor = True
		' 
		' btnPrintCriminalCard
		' 
		Me.btnPrintCriminalCard.Location = New System.Drawing.Point(3, 239)
		Me.btnPrintCriminalCard.Name = "btnPrintCriminalCard"
		Me.btnPrintCriminalCard.Size = New System.Drawing.Size(112, 36)
		Me.btnPrintCriminalCard.TabIndex = 15
		Me.btnPrintCriminalCard.Text = "Print criminal card"
		Me.btnPrintCriminalCard.UseVisualStyleBackColor = True
		' 
		' lblQuery
		' 
		Me.lblQuery.AutoSize = True
		Me.lblQuery.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblQuery.Image = Global.Neurotec.Samples.My.Resources.Help
		Me.lblQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.lblQuery.Location = New System.Drawing.Point(121, 26)
		Me.lblQuery.Name = "lblQuery"
		Me.lblQuery.Size = New System.Drawing.Size(53, 42)
		Me.lblQuery.TabIndex = 16
		Me.lblQuery.Text = "      Query"
		Me.lblQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.toolTip.SetToolTip(Me.lblQuery, resources.GetString("lblQuery.ToolTip"))
		' 
		' tbQuery
		' 
		Me.tbQuery.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
		Me.tbQuery.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
		Me.tbQuery.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tbQuery.Location = New System.Drawing.Point(180, 37)
		Me.tbQuery.Margin = New System.Windows.Forms.Padding(3, 11, 3, 3)
		Me.tbQuery.Name = "tbQuery"
		Me.tbQuery.Size = New System.Drawing.Size(603, 20)
		Me.tbQuery.TabIndex = 17
		' 
		' toolTip
		' 
		Me.toolTip.AutoPopDelay = 30000
		Me.toolTip.InitialDelay = 100
		Me.toolTip.ReshowDelay = 100
		Me.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
		' 
		' tableLayoutPanel3
		' 
		Me.tableLayoutPanel3.ColumnCount = 3
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tableLayoutPanel3.Controls.Add(Me.lblSubjectId, 0, 0)
		Me.tableLayoutPanel3.Controls.Add(Me.tbSubjectId, 1, 0)
		Me.tableLayoutPanel3.Controls.Add(Me.btnIdentify, 0, 1)
		Me.tableLayoutPanel3.Controls.Add(Me.lblQuery, 1, 1)
		Me.tableLayoutPanel3.Controls.Add(Me.tbQuery, 2, 1)
		Me.tableLayoutPanel3.Controls.Add(Me.btnVerify, 0, 2)
		Me.tableLayoutPanel3.Controls.Add(Me.btnEnroll, 0, 3)
		Me.tableLayoutPanel3.Controls.Add(Me.gbEnrollData, 1, 2)
		Me.tableLayoutPanel3.Controls.Add(Me.btnEnrollWithDuplicates, 0, 4)
		Me.tableLayoutPanel3.Controls.Add(Me.lblHint, 0, 10)
		Me.tableLayoutPanel3.Controls.Add(Me.btnSaveTemplate, 0, 8)
		Me.tableLayoutPanel3.Controls.Add(Me.btnPrintApplicantCard, 0, 7)
		Me.tableLayoutPanel3.Controls.Add(Me.btnPrintCriminalCard, 0, 6)
		Me.tableLayoutPanel3.Controls.Add(Me.btnUpdate, 0, 5)
		Me.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
		Me.tableLayoutPanel3.RowCount = 11
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel3.Size = New System.Drawing.Size(786, 473)
		Me.tableLayoutPanel3.TabIndex = 9
		' 
		' lblSubjectId
		' 
		Me.lblSubjectId.AutoSize = True
		Me.lblSubjectId.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblSubjectId.Location = New System.Drawing.Point(3, 0)
		Me.lblSubjectId.Name = "lblSubjectId"
		Me.lblSubjectId.Size = New System.Drawing.Size(112, 26)
		Me.lblSubjectId.TabIndex = 4
		Me.lblSubjectId.Text = "Subject id:"
		Me.lblSubjectId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		' 
		' btnUpdate
		' 
		Me.btnUpdate.Location = New System.Drawing.Point(3, 197)
		Me.btnUpdate.Name = "btnUpdate"
		Me.btnUpdate.Size = New System.Drawing.Size(112, 36)
		Me.btnUpdate.TabIndex = 18
		Me.btnUpdate.Text = "Update"
		Me.btnUpdate.UseVisualStyleBackColor = True
		' 
		' SubjectStartPage
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.Controls.Add(Me.tableLayoutPanel3)
		Me.Name = "SubjectStartPage"
		Me.Size = New System.Drawing.Size(786, 473)
		Me.tableLayoutPanel2.ResumeLayout(False)
		CType(Me.pbThumbnail, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gbEnrollData.ResumeLayout(False)
		Me.tlpEnrollData.ResumeLayout(False)
		Me.gbThumbnail.ResumeLayout(False)
		Me.tableLayoutPanel3.ResumeLayout(False)
		Me.tableLayoutPanel3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents btnEnroll As System.Windows.Forms.Button
	Private WithEvents btnIdentify As System.Windows.Forms.Button
	Private WithEvents btnVerify As System.Windows.Forms.Button
	Private WithEvents btnEnrollWithDuplicates As System.Windows.Forms.Button
	Private WithEvents tbSubjectId As System.Windows.Forms.TextBox
	Private lblHint As System.Windows.Forms.Label
	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnOpenImage As System.Windows.Forms.Button
	Private pbThumbnail As System.Windows.Forms.PictureBox
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private gbThumbnail As System.Windows.Forms.GroupBox
	Private WithEvents btnPrintApplicantCard As System.Windows.Forms.Button
	Private WithEvents btnPrintCriminalCard As System.Windows.Forms.Button
	Private gbEnrollData As System.Windows.Forms.GroupBox
	Private tlpEnrollData As System.Windows.Forms.TableLayoutPanel
	Private lblQuery As System.Windows.Forms.Label
	Private tbQuery As System.Windows.Forms.TextBox
	Private propertyGrid As System.Windows.Forms.PropertyGrid
	Private toolTip As System.Windows.Forms.ToolTip
	Private tableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
	Private lblSubjectId As System.Windows.Forms.Label
	Private WithEvents btnUpdate As System.Windows.Forms.Button
End Class