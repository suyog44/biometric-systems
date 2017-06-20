Imports Microsoft.VisualBasic
Imports System
Partial Public Class OptionsForm
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
		Me.chbLeaveInvalidUnvalidated = New System.Windows.Forms.CheckBox()
		Me.chbRecover = New System.Windows.Forms.CheckBox()
		Me.chbMergeDuplicateFields = New System.Windows.Forms.CheckBox()
		Me.chbUseNistMinutiaeNeighboars = New System.Windows.Forms.CheckBox()
		Me.chbNonStrinctRead = New System.Windows.Forms.CheckBox()
		Me.cbValidationLevel = New System.Windows.Forms.ComboBox()
		Me.openValidationLevelLabel = New System.Windows.Forms.Label()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		' 
		' chbLeaveInvalidUnvalidated
		' 
		Me.chbLeaveInvalidUnvalidated.AutoSize = True
		Me.chbLeaveInvalidUnvalidated.Checked = My.Settings.Default.LeaveInvalidRecordsUnvalidated
		Me.chbLeaveInvalidUnvalidated.Location = New System.Drawing.Point(139, 53)
		Me.chbLeaveInvalidUnvalidated.Name = "chbLeaveInvalidUnvalidated"
		Me.chbLeaveInvalidUnvalidated.Size = New System.Drawing.Size(185, 17)
		Me.chbLeaveInvalidUnvalidated.TabIndex = 18
		Me.chbLeaveInvalidUnvalidated.Text = "Leave invalid records unvalidated"
		Me.chbLeaveInvalidUnvalidated.UseVisualStyleBackColor = True
		' 
		' chbRecover
		' 
		Me.chbRecover.AutoSize = True
		Me.chbRecover.Checked = My.Settings.Default.RecoverFromBinaryData
		Me.chbRecover.Location = New System.Drawing.Point(139, 76)
		Me.chbRecover.Name = "chbRecover"
		Me.chbRecover.Size = New System.Drawing.Size(145, 17)
		Me.chbRecover.TabIndex = 20
		Me.chbRecover.Text = "Recover from binary data"
		Me.chbRecover.UseVisualStyleBackColor = True
		' 
		' chbMergeDuplicateFields
		' 
		Me.chbMergeDuplicateFields.AutoSize = True
		Me.chbMergeDuplicateFields.Checked = My.Settings.Default.MergeDuplicateFields
		Me.chbMergeDuplicateFields.Location = New System.Drawing.Point(6, 76)
		Me.chbMergeDuplicateFields.Name = "chbMergeDuplicateFields"
		Me.chbMergeDuplicateFields.Size = New System.Drawing.Size(129, 17)
		Me.chbMergeDuplicateFields.TabIndex = 19
		Me.chbMergeDuplicateFields.Text = "Merge duplicate fields"
		Me.chbMergeDuplicateFields.UseVisualStyleBackColor = True
		' 
		' chbUseNistMinutiaeNeighboars
		' 
		Me.chbUseNistMinutiaeNeighboars.AutoSize = True
		Me.chbUseNistMinutiaeNeighboars.Checked = My.Settings.Default.UseNistMinutiaNeighbors
		Me.chbUseNistMinutiaeNeighboars.Location = New System.Drawing.Point(6, 33)
		Me.chbUseNistMinutiaeNeighboars.Name = "chbUseNistMinutiaeNeighboars"
		Me.chbUseNistMinutiaeNeighboars.Size = New System.Drawing.Size(158, 17)
		Me.chbUseNistMinutiaeNeighboars.TabIndex = 16
		Me.chbUseNistMinutiaeNeighboars.Text = "Use NIST minutia neighbors"
		Me.chbUseNistMinutiaeNeighboars.UseVisualStyleBackColor = True
		' 
		' chbNonStrinctRead
		' 
		Me.chbNonStrinctRead.AutoSize = True
		Me.chbNonStrinctRead.Checked = My.Settings.Default.NonStrictRead
		Me.chbNonStrinctRead.Location = New System.Drawing.Point(6, 56)
		Me.chbNonStrinctRead.Name = "chbNonStrinctRead"
		Me.chbNonStrinctRead.Size = New System.Drawing.Size(95, 17)
		Me.chbNonStrinctRead.TabIndex = 17
		Me.chbNonStrinctRead.Text = "Non-strict read"
		Me.chbNonStrinctRead.UseVisualStyleBackColor = True
		' 
		' cbValidationLevel
		' 
		Me.cbValidationLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbValidationLevel.FormattingEnabled = True
		Me.cbValidationLevel.Location = New System.Drawing.Point(90, 6)
		Me.cbValidationLevel.Name = "cbValidationLevel"
		Me.cbValidationLevel.Size = New System.Drawing.Size(121, 21)
		Me.cbValidationLevel.TabIndex = 15
		' 
		' openValidationLevelLabel
		' 
		Me.openValidationLevelLabel.AutoSize = True
		Me.openValidationLevelLabel.Location = New System.Drawing.Point(3, 9)
		Me.openValidationLevelLabel.Name = "openValidationLevelLabel"
		Me.openValidationLevelLabel.Size = New System.Drawing.Size(81, 13)
		Me.openValidationLevelLabel.TabIndex = 14
		Me.openValidationLevelLabel.Text = "Validation level:"
		' 
		' btnOk
		' 
		Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnOk.Location = New System.Drawing.Point(167, 98)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 21
		Me.btnOk.Text = "Ok"
		Me.btnOk.UseVisualStyleBackColor = True
'		Me.btnOk.Click += New System.EventHandler(Me.BtnOkClick);
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(248, 98)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 22
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' OpenOptionsForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(335, 133)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.chbLeaveInvalidUnvalidated)
		Me.Controls.Add(Me.chbRecover)
		Me.Controls.Add(Me.chbMergeDuplicateFields)
		Me.Controls.Add(Me.chbUseNistMinutiaeNeighboars)
		Me.Controls.Add(Me.chbNonStrinctRead)
		Me.Controls.Add(Me.cbValidationLevel)
		Me.Controls.Add(Me.openValidationLevelLabel)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "OpenOptionsForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Options"
'		Me.Load += New System.EventHandler(Me.OpenOptionsFormLoad);
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private chbLeaveInvalidUnvalidated As System.Windows.Forms.CheckBox
	Private chbRecover As System.Windows.Forms.CheckBox
	Private chbMergeDuplicateFields As System.Windows.Forms.CheckBox
	Private chbUseNistMinutiaeNeighboars As System.Windows.Forms.CheckBox
	Private chbNonStrinctRead As System.Windows.Forms.CheckBox
	Private cbValidationLevel As System.Windows.Forms.ComboBox
	Private openValidationLevelLabel As System.Windows.Forms.Label
	Private WithEvents btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button

End Class