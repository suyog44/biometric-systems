Imports Microsoft.VisualBasic
Imports System
Partial Public Class CaptureVoicePage
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
		Me.gbCapture = New System.Windows.Forms.GroupBox
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.btnStart = New System.Windows.Forms.Button
		Me.voiceView = New Neurotec.Biometrics.Gui.NVoiceView
		Me.btnStop = New System.Windows.Forms.Button
		Me.btnForce = New System.Windows.Forms.Button
		Me.chnCaptureAutomatically = New System.Windows.Forms.CheckBox
		Me.lblHint = New System.Windows.Forms.Label
		Me.gbPhrase = New System.Windows.Forms.GroupBox
		Me.label8 = New System.Windows.Forms.Label
		Me.btnEdit = New System.Windows.Forms.Button
		Me.lblPhraseId = New System.Windows.Forms.Label
		Me.label5 = New System.Windows.Forms.Label
		Me.cbPhrase = New System.Windows.Forms.ComboBox
		Me.label4 = New System.Windows.Forms.Label
		Me.label2 = New System.Windows.Forms.Label
		Me.lblFilename = New System.Windows.Forms.Label
		Me.btnOpenFile = New System.Windows.Forms.Button
		Me.gbSource = New System.Windows.Forms.GroupBox
		Me.rbFromFile = New System.Windows.Forms.RadioButton
		Me.rbMicrophone = New System.Windows.Forms.RadioButton
		Me.lblPleaseSelect = New System.Windows.Forms.Label
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.btnFinish = New System.Windows.Forms.Button
		Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.busyIndicator = New Neurotec.Samples.BusyIndicator
		Me.gbCapture.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.gbPhrase.SuspendLayout()
		Me.gbSource.SuspendLayout()
		Me.tableLayoutPanel2.SuspendLayout()
		Me.TableLayoutPanel3.SuspendLayout()
		Me.SuspendLayout()
		'
		'gbCapture
		'
		Me.gbCapture.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbCapture.Controls.Add(Me.tableLayoutPanel1)
		Me.gbCapture.Location = New System.Drawing.Point(3, 227)
		Me.gbCapture.Name = "gbCapture"
		Me.gbCapture.Size = New System.Drawing.Size(687, 100)
		Me.gbCapture.TabIndex = 13
		Me.gbCapture.TabStop = False
		Me.gbCapture.Text = "Capture"
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 5
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.Controls.Add(Me.btnStart, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.voiceView, 3, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnStop, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnForce, 2, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.chnCaptureAutomatically, 0, 0)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(6, 19)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 2
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(672, 77)
		Me.tableLayoutPanel1.TabIndex = 15
		'
		'btnStart
		'
		Me.btnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnStart.Image = Global.Neurotec.Samples.My.Resources.Resources.play
		Me.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnStart.Location = New System.Drawing.Point(3, 26)
		Me.btnStart.Name = "btnStart"
		Me.btnStart.Size = New System.Drawing.Size(91, 40)
		Me.btnStart.TabIndex = 0
		Me.btnStart.Text = "&Start"
		Me.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnStart.UseVisualStyleBackColor = True
		'
		'voiceView
		'
		Me.voiceView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.voiceView.BackColor = System.Drawing.Color.Transparent
		Me.tableLayoutPanel1.SetColumnSpan(Me.voiceView, 2)
		Me.voiceView.Location = New System.Drawing.Point(278, 26)
		Me.voiceView.Name = "voiceView"
		Me.voiceView.Size = New System.Drawing.Size(391, 58)
		Me.voiceView.TabIndex = 14
		Me.voiceView.Text = "nVoiceView1"
		Me.voiceView.Voice = Nothing
		'
		'btnStop
		'
		Me.btnStop.Image = Global.Neurotec.Samples.My.Resources.Resources._stop
		Me.btnStop.Location = New System.Drawing.Point(100, 26)
		Me.btnStop.Name = "btnStop"
		Me.btnStop.Size = New System.Drawing.Size(83, 40)
		Me.btnStop.TabIndex = 1
		Me.btnStop.UseVisualStyleBackColor = True
		'
		'btnForce
		'
		Me.btnForce.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
		Me.btnForce.Location = New System.Drawing.Point(189, 26)
		Me.btnForce.Name = "btnForce"
		Me.btnForce.Size = New System.Drawing.Size(83, 40)
		Me.btnForce.TabIndex = 15
		Me.btnForce.Text = "&Force"
		Me.btnForce.UseVisualStyleBackColor = True
		'
		'chnCaptureAutomatically
		'
		Me.chnCaptureAutomatically.AutoSize = True
		Me.chnCaptureAutomatically.Checked = True
		Me.chnCaptureAutomatically.CheckState = System.Windows.Forms.CheckState.Checked
		Me.tableLayoutPanel1.SetColumnSpan(Me.chnCaptureAutomatically, 3)
		Me.chnCaptureAutomatically.Location = New System.Drawing.Point(3, 3)
		Me.chnCaptureAutomatically.Name = "chnCaptureAutomatically"
		Me.chnCaptureAutomatically.Size = New System.Drawing.Size(127, 17)
		Me.chnCaptureAutomatically.TabIndex = 16
		Me.chnCaptureAutomatically.Text = "Capture automatically"
		Me.chnCaptureAutomatically.UseVisualStyleBackColor = True
		'
		'lblHint
		'
		Me.lblHint.AutoSize = True
		Me.lblHint.BackColor = System.Drawing.Color.Orange
		Me.lblHint.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblHint.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblHint.ForeColor = System.Drawing.Color.White
		Me.lblHint.Location = New System.Drawing.Point(23, 0)
		Me.lblHint.Name = "lblHint"
		Me.lblHint.Size = New System.Drawing.Size(661, 20)
		Me.lblHint.TabIndex = 15
		Me.lblHint.Text = "Extracting record. Please say phrase ..."
		Me.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'gbPhrase
		'
		Me.gbPhrase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbPhrase.Controls.Add(Me.label8)
		Me.gbPhrase.Controls.Add(Me.btnEdit)
		Me.gbPhrase.Controls.Add(Me.lblPhraseId)
		Me.gbPhrase.Controls.Add(Me.label5)
		Me.gbPhrase.Controls.Add(Me.cbPhrase)
		Me.gbPhrase.Controls.Add(Me.label4)
		Me.gbPhrase.Location = New System.Drawing.Point(3, 16)
		Me.gbPhrase.Name = "gbPhrase"
		Me.gbPhrase.Size = New System.Drawing.Size(687, 110)
		Me.gbPhrase.TabIndex = 8
		Me.gbPhrase.TabStop = False
		Me.gbPhrase.Text = "Secret phrase (Please answer the question)"
		'
		'label8
		'
		Me.label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.label8.Location = New System.Drawing.Point(6, 70)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(513, 29)
		Me.label8.TabIndex = 5
		Me.label8.Text = "Phrase should be secret answer to the selected question. Phrase duration should b" & _
			"e at least about 6 seconds or 4 words."
		'
		'btnEdit
		'
		Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnEdit.Location = New System.Drawing.Point(606, 16)
		Me.btnEdit.Name = "btnEdit"
		Me.btnEdit.Size = New System.Drawing.Size(75, 23)
		Me.btnEdit.TabIndex = 2
		Me.btnEdit.Text = "&Edit"
		Me.btnEdit.UseVisualStyleBackColor = True
		'
		'lblPhraseId
		'
		Me.lblPhraseId.Location = New System.Drawing.Point(96, 47)
		Me.lblPhraseId.Name = "lblPhraseId"
		Me.lblPhraseId.Size = New System.Drawing.Size(270, 23)
		Me.lblPhraseId.TabIndex = 4
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(6, 47)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(55, 13)
		Me.label5.TabIndex = 3
		Me.label5.Text = "Phrase Id:"
		'
		'cbPhrase
		'
		Me.cbPhrase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbPhrase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbPhrase.FormattingEnabled = True
		Me.cbPhrase.Location = New System.Drawing.Point(113, 18)
		Me.cbPhrase.Name = "cbPhrase"
		Me.cbPhrase.Size = New System.Drawing.Size(487, 21)
		Me.cbPhrase.TabIndex = 1
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(6, 21)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(101, 13)
		Me.label4.TabIndex = 0
		Me.label4.Text = "Selected phrase ID:"
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(3, 129)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(152, 13)
		Me.label2.TabIndex = 9
		Me.label2.Text = "2. Please select sound source:"
		'
		'lblFilename
		'
		Me.lblFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblFilename.Location = New System.Drawing.Point(90, 16)
		Me.lblFilename.Name = "lblFilename"
		Me.lblFilename.Size = New System.Drawing.Size(497, 23)
		Me.lblFilename.TabIndex = 3
		Me.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'btnOpenFile
		'
		Me.btnOpenFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOpenFile.Enabled = False
		Me.btnOpenFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnOpenFile.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.btnOpenFile.Location = New System.Drawing.Point(593, 16)
		Me.btnOpenFile.Name = "btnOpenFile"
		Me.btnOpenFile.Size = New System.Drawing.Size(85, 23)
		Me.btnOpenFile.TabIndex = 4
		Me.btnOpenFile.Text = "&Open file"
		Me.btnOpenFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpenFile.UseVisualStyleBackColor = True
		'
		'gbSource
		'
		Me.gbSource.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbSource.Controls.Add(Me.lblFilename)
		Me.gbSource.Controls.Add(Me.btnOpenFile)
		Me.gbSource.Controls.Add(Me.rbFromFile)
		Me.gbSource.Controls.Add(Me.rbMicrophone)
		Me.gbSource.Location = New System.Drawing.Point(3, 145)
		Me.gbSource.Name = "gbSource"
		Me.gbSource.Size = New System.Drawing.Size(687, 76)
		Me.gbSource.TabIndex = 10
		Me.gbSource.TabStop = False
		Me.gbSource.Text = "Source"
		'
		'rbFromFile
		'
		Me.rbFromFile.AutoSize = True
		Me.rbFromFile.Checked = True
		Me.rbFromFile.Location = New System.Drawing.Point(6, 19)
		Me.rbFromFile.Name = "rbFromFile"
		Me.rbFromFile.Size = New System.Drawing.Size(72, 17)
		Me.rbFromFile.TabIndex = 2
		Me.rbFromFile.TabStop = True
		Me.rbFromFile.Text = "Sound file"
		Me.rbFromFile.UseVisualStyleBackColor = True
		'
		'rbMicrophone
		'
		Me.rbMicrophone.AutoSize = True
		Me.rbMicrophone.Location = New System.Drawing.Point(6, 42)
		Me.rbMicrophone.Name = "rbMicrophone"
		Me.rbMicrophone.Size = New System.Drawing.Size(81, 17)
		Me.rbMicrophone.TabIndex = 0
		Me.rbMicrophone.Text = "Microphone"
		Me.rbMicrophone.UseVisualStyleBackColor = True
		'
		'lblPleaseSelect
		'
		Me.lblPleaseSelect.AutoSize = True
		Me.lblPleaseSelect.Location = New System.Drawing.Point(3, 0)
		Me.lblPleaseSelect.Name = "lblPleaseSelect"
		Me.lblPleaseSelect.Size = New System.Drawing.Size(222, 13)
		Me.lblPleaseSelect.TabIndex = 7
		Me.lblPleaseSelect.Text = "1. Please select secret phrase ID from the list:"
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.ColumnCount = 1
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel2.Controls.Add(Me.lblPleaseSelect, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.gbCapture, 0, 4)
		Me.tableLayoutPanel2.Controls.Add(Me.gbPhrase, 0, 1)
		Me.tableLayoutPanel2.Controls.Add(Me.label2, 0, 2)
		Me.tableLayoutPanel2.Controls.Add(Me.gbSource, 0, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.btnFinish, 0, 6)
		Me.tableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 5)
		Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 7
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(693, 413)
		Me.tableLayoutPanel2.TabIndex = 16
		'
		'btnFinish
		'
		Me.btnFinish.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnFinish.Location = New System.Drawing.Point(615, 387)
		Me.btnFinish.Name = "btnFinish"
		Me.btnFinish.Size = New System.Drawing.Size(75, 23)
		Me.btnFinish.TabIndex = 16
		Me.btnFinish.Text = "&Finish"
		Me.btnFinish.UseVisualStyleBackColor = True
		'
		'TableLayoutPanel3
		'
		Me.TableLayoutPanel3.ColumnCount = 2
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Controls.Add(Me.busyIndicator, 0, 0)
		Me.TableLayoutPanel3.Controls.Add(Me.lblHint, 1, 0)
		Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 333)
		Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
		Me.TableLayoutPanel3.RowCount = 1
		Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Size = New System.Drawing.Size(687, 20)
		Me.TableLayoutPanel3.TabIndex = 17
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
		'CaptureVoicePage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel2)
		Me.Name = "CaptureVoicePage"
		Me.Size = New System.Drawing.Size(693, 413)
		Me.gbCapture.ResumeLayout(False)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.gbPhrase.ResumeLayout(False)
		Me.gbPhrase.PerformLayout()
		Me.gbSource.ResumeLayout(False)
		Me.gbSource.PerformLayout()
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.tableLayoutPanel2.PerformLayout()
		Me.TableLayoutPanel3.ResumeLayout(False)
		Me.TableLayoutPanel3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private gbCapture As System.Windows.Forms.GroupBox
	Private WithEvents btnStop As System.Windows.Forms.Button
	Private WithEvents btnStart As System.Windows.Forms.Button
	Private gbPhrase As System.Windows.Forms.GroupBox
	Private label8 As System.Windows.Forms.Label
	Private WithEvents btnEdit As System.Windows.Forms.Button
	Private lblPhraseId As System.Windows.Forms.Label
	Private label5 As System.Windows.Forms.Label
	Private cbPhrase As System.Windows.Forms.ComboBox
	Private label4 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private lblFilename As System.Windows.Forms.Label
	Private WithEvents btnOpenFile As System.Windows.Forms.Button
	Private gbSource As System.Windows.Forms.GroupBox
	Private WithEvents rbFromFile As System.Windows.Forms.RadioButton
	Private WithEvents rbMicrophone As System.Windows.Forms.RadioButton
	Private lblPleaseSelect As System.Windows.Forms.Label
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private voiceView As Neurotec.Biometrics.Gui.NVoiceView
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private lblHint As System.Windows.Forms.Label
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnFinish As System.Windows.Forms.Button
	Private busyIndicator As BusyIndicator
	Friend WithEvents btnForce As System.Windows.Forms.Button
	Friend WithEvents chnCaptureAutomatically As System.Windows.Forms.CheckBox
	Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
End Class
