Imports Microsoft.VisualBasic
Imports System
Partial Public Class CaptureIrisPage
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
		Me.cbPosition = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.irisView = New Neurotec.Biometrics.Gui.NIrisView
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.btnCancel = New System.Windows.Forms.Button
		Me.lblStatus = New System.Windows.Forms.Label
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.btnOpen = New System.Windows.Forms.Button
		Me.rbIrisScanner = New System.Windows.Forms.RadioButton
		Me.btnCapture = New System.Windows.Forms.Button
		Me.rbFile = New System.Windows.Forms.RadioButton
		Me.chbCaptureAutomatically = New System.Windows.Forms.CheckBox
		Me.gbCaptureOptions = New System.Windows.Forms.GroupBox
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.btnFinish = New System.Windows.Forms.Button
		Me.horizontalZoomSlider = New Neurotec.Gui.NViewZoomSlider
		Me.btnForce = New System.Windows.Forms.Button
		Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.busyIndicator = New Neurotec.Samples.BusyIndicator
		Me.tableLayoutPanel2.SuspendLayout()
		Me.gbCaptureOptions.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.TableLayoutPanel3.SuspendLayout()
		Me.SuspendLayout()
		'
		'cbPosition
		'
		Me.cbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbPosition.FormattingEnabled = True
		Me.cbPosition.Location = New System.Drawing.Point(56, 54)
		Me.cbPosition.Name = "cbPosition"
		Me.cbPosition.Size = New System.Drawing.Size(121, 21)
		Me.cbPosition.TabIndex = 2
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 51)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(47, 29)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Position:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'irisView
		'
		Me.irisView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.irisView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.tableLayoutPanel1.SetColumnSpan(Me.irisView, 4)
		Me.irisView.InnerBoundaryColor = System.Drawing.Color.Red
		Me.irisView.InnerBoundaryWidth = 2
		Me.irisView.Iris = Nothing
		Me.irisView.Location = New System.Drawing.Point(3, 111)
		Me.irisView.Name = "irisView"
		Me.irisView.OuterBoundaryColor = System.Drawing.Color.GreenYellow
		Me.irisView.OuterBoundaryWidth = 2
		Me.irisView.Size = New System.Drawing.Size(607, 238)
		Me.irisView.TabIndex = 5
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.Location = New System.Drawing.Point(454, 381)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 6
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'lblStatus
		'
		Me.lblStatus.AutoSize = True
		Me.lblStatus.BackColor = System.Drawing.Color.Red
		Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStatus.ForeColor = System.Drawing.Color.White
		Me.lblStatus.Location = New System.Drawing.Point(23, 0)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(581, 20)
		Me.lblStatus.TabIndex = 7
		Me.lblStatus.Text = "Extraction status: None"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.ColumnCount = 5
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.Controls.Add(Me.btnOpen, 3, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.rbIrisScanner, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.label1, 0, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.cbPosition, 1, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.btnCapture, 2, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.rbFile, 0, 1)
		Me.tableLayoutPanel2.Controls.Add(Me.chbCaptureAutomatically, 1, 1)
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(6, 19)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 4
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(399, 78)
		Me.tableLayoutPanel2.TabIndex = 8
		'
		'btnOpen
		'
		Me.btnOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnOpen.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.btnOpen.Location = New System.Drawing.Point(280, 54)
		Me.btnOpen.Name = "btnOpen"
		Me.btnOpen.Size = New System.Drawing.Size(104, 23)
		Me.btnOpen.TabIndex = 10
		Me.btnOpen.Text = "&Open image"
		Me.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpen.UseVisualStyleBackColor = True
		'
		'rbIrisScanner
		'
		Me.rbIrisScanner.AutoSize = True
		Me.rbIrisScanner.Checked = True
		Me.tableLayoutPanel2.SetColumnSpan(Me.rbIrisScanner, 4)
		Me.rbIrisScanner.Location = New System.Drawing.Point(3, 3)
		Me.rbIrisScanner.Name = "rbIrisScanner"
		Me.rbIrisScanner.Size = New System.Drawing.Size(65, 17)
		Me.rbIrisScanner.TabIndex = 0
		Me.rbIrisScanner.TabStop = True
		Me.rbIrisScanner.Text = "Scanner"
		Me.rbIrisScanner.UseVisualStyleBackColor = True
		'
		'btnCapture
		'
		Me.btnCapture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnCapture.Location = New System.Drawing.Point(189, 54)
		Me.btnCapture.Name = "btnCapture"
		Me.btnCapture.Size = New System.Drawing.Size(85, 23)
		Me.btnCapture.TabIndex = 11
		Me.btnCapture.Text = "Ca&pture"
		Me.btnCapture.UseVisualStyleBackColor = True
		'
		'rbFile
		'
		Me.rbFile.AutoSize = True
		Me.rbFile.Location = New System.Drawing.Point(3, 26)
		Me.rbFile.Name = "rbFile"
		Me.rbFile.Size = New System.Drawing.Size(41, 17)
		Me.rbFile.TabIndex = 1
		Me.rbFile.Text = "File"
		Me.rbFile.UseVisualStyleBackColor = True
		'
		'chbCaptureAutomatically
		'
		Me.chbCaptureAutomatically.AutoSize = True
		Me.chbCaptureAutomatically.Checked = True
		Me.chbCaptureAutomatically.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbCaptureAutomatically.Location = New System.Drawing.Point(56, 26)
		Me.chbCaptureAutomatically.Name = "chbCaptureAutomatically"
		Me.chbCaptureAutomatically.Size = New System.Drawing.Size(127, 17)
		Me.chbCaptureAutomatically.TabIndex = 12
		Me.chbCaptureAutomatically.Text = "Capture automatically"
		Me.chbCaptureAutomatically.UseVisualStyleBackColor = True
		'
		'gbCaptureOptions
		'
		Me.tableLayoutPanel1.SetColumnSpan(Me.gbCaptureOptions, 4)
		Me.gbCaptureOptions.Controls.Add(Me.tableLayoutPanel2)
		Me.gbCaptureOptions.Location = New System.Drawing.Point(3, 3)
		Me.gbCaptureOptions.Name = "gbCaptureOptions"
		Me.gbCaptureOptions.Size = New System.Drawing.Size(411, 102)
		Me.gbCaptureOptions.TabIndex = 9
		Me.gbCaptureOptions.TabStop = False
		Me.gbCaptureOptions.Text = "Capture options"
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 4
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.irisView, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnFinish, 3, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.btnCancel, 2, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.horizontalZoomSlider, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.btnForce, 1, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.gbCaptureOptions, 0, 0)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 4
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(613, 407)
		Me.tableLayoutPanel1.TabIndex = 10
		'
		'btnFinish
		'
		Me.btnFinish.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnFinish.Location = New System.Drawing.Point(535, 381)
		Me.btnFinish.Name = "btnFinish"
		Me.btnFinish.Size = New System.Drawing.Size(75, 23)
		Me.btnFinish.TabIndex = 11
		Me.btnFinish.Text = "&Finish"
		Me.btnFinish.UseVisualStyleBackColor = True
		'
		'horizontalZoomSlider
		'
		Me.horizontalZoomSlider.Location = New System.Drawing.Point(3, 381)
		Me.horizontalZoomSlider.Name = "horizontalZoomSlider"
		Me.horizontalZoomSlider.Size = New System.Drawing.Size(252, 23)
		Me.horizontalZoomSlider.TabIndex = 12
		Me.horizontalZoomSlider.View = Me.irisView
		'
		'btnForce
		'
		Me.btnForce.Location = New System.Drawing.Point(373, 381)
		Me.btnForce.Name = "btnForce"
		Me.btnForce.Size = New System.Drawing.Size(75, 23)
		Me.btnForce.TabIndex = 13
		Me.btnForce.Text = "&Force"
		Me.btnForce.UseVisualStyleBackColor = True
		'
		'TableLayoutPanel3
		'
		Me.TableLayoutPanel3.ColumnCount = 2
		Me.tableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel3, 4)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Controls.Add(Me.busyIndicator, 0, 0)
		Me.TableLayoutPanel3.Controls.Add(Me.lblStatus, 1, 0)
		Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 355)
		Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
		Me.TableLayoutPanel3.RowCount = 1
		Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Size = New System.Drawing.Size(607, 20)
		Me.TableLayoutPanel3.TabIndex = 14
		'
		'busyIndicator
		'
		Me.busyIndicator.Dock = System.Windows.Forms.DockStyle.Fill
		Me.busyIndicator.Location = New System.Drawing.Point(3, 3)
		Me.busyIndicator.Name = "busyIndicator"
		Me.busyIndicator.Size = New System.Drawing.Size(14, 14)
		Me.busyIndicator.TabIndex = 0
		Me.busyIndicator.Visible = False
		'
		'CaptureIrisPage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "CaptureIrisPage"
		Me.Size = New System.Drawing.Size(613, 407)
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.tableLayoutPanel2.PerformLayout()
		Me.gbCaptureOptions.ResumeLayout(False)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.TableLayoutPanel3.ResumeLayout(False)
		Me.TableLayoutPanel3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private cbPosition As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
	Private irisView As Neurotec.Biometrics.Gui.NIrisView
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private lblStatus As System.Windows.Forms.Label
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private rbIrisScanner As System.Windows.Forms.RadioButton
	Private rbFile As System.Windows.Forms.RadioButton
	Private gbCaptureOptions As System.Windows.Forms.GroupBox
	Private WithEvents btnOpen As System.Windows.Forms.Button
	Private WithEvents btnCapture As System.Windows.Forms.Button
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnFinish As System.Windows.Forms.Button
	Private horizontalZoomSlider As Neurotec.Gui.NViewZoomSlider
	Private busyIndicator As BusyIndicator
	Friend WithEvents btnForce As System.Windows.Forms.Button
	Friend WithEvents chbCaptureAutomatically As System.Windows.Forms.CheckBox
	Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
End Class
