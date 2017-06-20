Imports Microsoft.VisualBasic
Imports System
Partial Public Class ExtractionSettingsForm
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
		Me.ExtractionGroupBox = New System.Windows.Forms.GroupBox()
		Me.pnQualityThreshold = New System.Windows.Forms.Panel()
		Me.ThresholdLabel = New System.Windows.Forms.Label()
		Me.btnDefaultThreshold = New System.Windows.Forms.Button()
		Me.nudThreshold = New System.Windows.Forms.NumericUpDown()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.btnOK = New System.Windows.Forms.Button()
		Me.ExtractionGroupBox.SuspendLayout()
		Me.pnQualityThreshold.SuspendLayout()
		CType(Me.nudThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' ExtractionGroupBox
		' 
		Me.ExtractionGroupBox.Controls.Add(Me.pnQualityThreshold)
		Me.ExtractionGroupBox.Location = New System.Drawing.Point(12, 12)
		Me.ExtractionGroupBox.Name = "ExtractionGroupBox"
		Me.ExtractionGroupBox.Size = New System.Drawing.Size(235, 58)
		Me.ExtractionGroupBox.TabIndex = 11
		Me.ExtractionGroupBox.TabStop = False
		Me.ExtractionGroupBox.Text = "Quality threshold"
		' 
		' pnQualityThreshold
		' 
		Me.pnQualityThreshold.Controls.Add(Me.ThresholdLabel)
		Me.pnQualityThreshold.Controls.Add(Me.btnDefaultThreshold)
		Me.pnQualityThreshold.Controls.Add(Me.nudThreshold)
		Me.pnQualityThreshold.Location = New System.Drawing.Point(6, 19)
		Me.pnQualityThreshold.Name = "pnQualityThreshold"
		Me.pnQualityThreshold.Size = New System.Drawing.Size(223, 29)
		Me.pnQualityThreshold.TabIndex = 14
		' 
		' ThresholdLabel
		' 
		Me.ThresholdLabel.AutoSize = True
		Me.ThresholdLabel.Location = New System.Drawing.Point(12, 8)
		Me.ThresholdLabel.Name = "ThresholdLabel"
		Me.ThresholdLabel.Size = New System.Drawing.Size(57, 13)
		Me.ThresholdLabel.TabIndex = 8
		Me.ThresholdLabel.Text = "Threshold:"
		' 
		' btnDefaultThreshold
		' 
		Me.btnDefaultThreshold.Location = New System.Drawing.Point(129, 3)
		Me.btnDefaultThreshold.Name = "btnDefaultThreshold"
		Me.btnDefaultThreshold.Size = New System.Drawing.Size(75, 23)
		Me.btnDefaultThreshold.TabIndex = 10
		Me.btnDefaultThreshold.Text = "Default"
		Me.btnDefaultThreshold.UseVisualStyleBackColor = True
'		Me.btnDefaultThreshold.Click += New System.EventHandler(Me.BtnDefaultThresholdClick);
		' 
		' nudThreshold
		' 
		Me.nudThreshold.Location = New System.Drawing.Point(75, 5)
		Me.nudThreshold.Name = "nudThreshold"
		Me.nudThreshold.Size = New System.Drawing.Size(48, 20)
		Me.nudThreshold.TabIndex = 9
		Me.nudThreshold.Value = New Decimal(New Integer() { 39, 0, 0, 0})
		' 
		' btnCancel
		' 
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(174, 76)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 12
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' btnOK
		' 
		Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOK.Location = New System.Drawing.Point(93, 76)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.Size = New System.Drawing.Size(75, 23)
		Me.btnOK.TabIndex = 13
		Me.btnOK.Text = "OK"
		Me.btnOK.UseVisualStyleBackColor = True
		' 
		' ExtractionSettingsForm
		' 
		Me.AcceptButton = Me.btnOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(257, 104)
		Me.Controls.Add(Me.btnOK)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.ExtractionGroupBox)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "ExtractionSettingsForm"
		Me.ShowInTaskbar = False
		Me.Text = "Extraction Settings"
		Me.ExtractionGroupBox.ResumeLayout(False)
		Me.pnQualityThreshold.ResumeLayout(False)
		Me.pnQualityThreshold.PerformLayout()
		CType(Me.nudThreshold, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private ExtractionGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents btnDefaultThreshold As System.Windows.Forms.Button
	Private nudThreshold As System.Windows.Forms.NumericUpDown
	Private ThresholdLabel As System.Windows.Forms.Label
	Private btnCancel As System.Windows.Forms.Button
	Private btnOK As System.Windows.Forms.Button
	Private pnQualityThreshold As System.Windows.Forms.Panel
End Class