Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms
	Partial Public Class ANType1RecordCreateForm
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
			Me.tbTransactionType = New System.Windows.Forms.TextBox()
			Me.label14 = New System.Windows.Forms.Label()
			Me.tbDestinationAgency = New System.Windows.Forms.TextBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.tbOriginatingAgency = New System.Windows.Forms.TextBox()
			Me.label10 = New System.Windows.Forms.Label()
			Me.tbTransactionControlId = New System.Windows.Forms.TextBox()
			Me.label12 = New System.Windows.Forms.Label()
			Me.cbValidationLevel = New System.Windows.Forms.ComboBox()
			Me.newValidationLevelLabel = New System.Windows.Forms.Label()
			Me.chbUseNistMinutiaNeighbors = New System.Windows.Forms.CheckBox()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' okButton
			' 
			Me.btnOk.Location = New System.Drawing.Point(81, 288)
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(162, 288)
			' 
			' idcNumericUpDown
			' 
			Me.nudIdc.Location = New System.Drawing.Point(12, 25)
			' 
			' label1
			' 
			Me.label1.Location = New System.Drawing.Point(9, 9)
			' 
			' tbTransactionType
			' 
			Me.tbTransactionType.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbTransactionType.Location = New System.Drawing.Point(9, 133)
			Me.tbTransactionType.Name = "tbTransactionType"
			Me.tbTransactionType.Size = New System.Drawing.Size(226, 20)
			Me.tbTransactionType.TabIndex = 4
			'			Me.tbTransactionType.Validating += New System.ComponentModel.CancelEventHandler(Me.TbTransactionTypeValidating);
			' 
			' label14
			' 
			Me.label14.AutoSize = True
			Me.label14.Location = New System.Drawing.Point(9, 117)
			Me.label14.Name = "label14"
			Me.label14.Size = New System.Drawing.Size(89, 13)
			Me.label14.TabIndex = 35
			Me.label14.Text = "Transaction type:"
			' 
			' tbDestinationAgency
			' 
			Me.tbDestinationAgency.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbDestinationAgency.Location = New System.Drawing.Point(9, 174)
			Me.tbDestinationAgency.Name = "tbDestinationAgency"
			Me.tbDestinationAgency.Size = New System.Drawing.Size(226, 20)
			Me.tbDestinationAgency.TabIndex = 5
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(9, 158)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(101, 13)
			Me.label4.TabIndex = 37
			Me.label4.Text = "Destination agency:"
			' 
			' tbOriginatingAgency
			' 
			Me.tbOriginatingAgency.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbOriginatingAgency.Location = New System.Drawing.Point(9, 216)
			Me.tbOriginatingAgency.Name = "tbOriginatingAgency"
			Me.tbOriginatingAgency.Size = New System.Drawing.Size(226, 20)
			Me.tbOriginatingAgency.TabIndex = 6
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Location = New System.Drawing.Point(9, 200)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(98, 13)
			Me.label10.TabIndex = 39
			Me.label10.Text = "Originating agency:"
			' 
			' tbTransactionControlId
			' 
			Me.tbTransactionControlId.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbTransactionControlId.Location = New System.Drawing.Point(9, 259)
			Me.tbTransactionControlId.Name = "tbTransactionControlId"
			Me.tbTransactionControlId.Size = New System.Drawing.Size(226, 20)
			Me.tbTransactionControlId.TabIndex = 7
			' 
			' label12
			' 
			Me.label12.AutoSize = True
			Me.label12.Location = New System.Drawing.Point(9, 243)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(143, 13)
			Me.label12.TabIndex = 41
			Me.label12.Text = "Transaction control identifier:"
			' 
			' cbValidationLevel
			' 
			Me.cbValidationLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbValidationLevel.FormattingEnabled = True
			Me.cbValidationLevel.Location = New System.Drawing.Point(96, 57)
			Me.cbValidationLevel.Name = "cbValidationLevel"
			Me.cbValidationLevel.Size = New System.Drawing.Size(144, 21)
			Me.cbValidationLevel.TabIndex = 2
			'			Me.cbValidationLevel.SelectedIndexChanged += New System.EventHandler(Me.CbValidationLevelSelectedIndexChanged);
			' 
			' newValidationLevelLabel
			' 
			Me.newValidationLevelLabel.AutoSize = True
			Me.newValidationLevelLabel.Location = New System.Drawing.Point(9, 60)
			Me.newValidationLevelLabel.Name = "newValidationLevelLabel"
			Me.newValidationLevelLabel.Size = New System.Drawing.Size(81, 13)
			Me.newValidationLevelLabel.TabIndex = 43
			Me.newValidationLevelLabel.Text = "Validation level:"
			' 
			' chbUseNistMinutiaNeighbors
			' 
			Me.chbUseNistMinutiaNeighbors.AutoSize = True
			Me.chbUseNistMinutiaNeighbors.Checked = My.Settings.Default.NewUseNistMinutiaNeighbors
			Me.chbUseNistMinutiaNeighbors.Location = New System.Drawing.Point(12, 84)
			Me.chbUseNistMinutiaNeighbors.Name = "chbUseNistMinutiaNeighbors"
			Me.chbUseNistMinutiaNeighbors.Size = New System.Drawing.Size(158, 17)
			Me.chbUseNistMinutiaNeighbors.TabIndex = 3
			Me.chbUseNistMinutiaNeighbors.Text = "Use NIST minutia neighbors"
			Me.chbUseNistMinutiaNeighbors.UseVisualStyleBackColor = True
			' 
			' ANType1RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(247, 321)
			Me.Controls.Add(Me.cbValidationLevel)
			Me.Controls.Add(Me.newValidationLevelLabel)
			Me.Controls.Add(Me.chbUseNistMinutiaNeighbors)
			Me.Controls.Add(Me.tbTransactionControlId)
			Me.Controls.Add(Me.label12)
			Me.Controls.Add(Me.tbOriginatingAgency)
			Me.Controls.Add(Me.label10)
			Me.Controls.Add(Me.tbDestinationAgency)
			Me.Controls.Add(Me.label4)
			Me.Controls.Add(Me.tbTransactionType)
			Me.Controls.Add(Me.label14)
			Me.Name = "ANType1RecordCreateForm"
			Me.Text = "New Template"
			Me.Controls.SetChildIndex(Me.label1, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.label14, 0)
			Me.Controls.SetChildIndex(Me.tbTransactionType, 0)
			Me.Controls.SetChildIndex(Me.label4, 0)
			Me.Controls.SetChildIndex(Me.tbDestinationAgency, 0)
			Me.Controls.SetChildIndex(Me.label10, 0)
			Me.Controls.SetChildIndex(Me.tbOriginatingAgency, 0)
			Me.Controls.SetChildIndex(Me.label12, 0)
			Me.Controls.SetChildIndex(Me.tbTransactionControlId, 0)
			Me.Controls.SetChildIndex(Me.chbUseNistMinutiaNeighbors, 0)
			Me.Controls.SetChildIndex(Me.newValidationLevelLabel, 0)
			Me.Controls.SetChildIndex(Me.cbValidationLevel, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private WithEvents tbTransactionType As System.Windows.Forms.TextBox
		Private label14 As System.Windows.Forms.Label
		Private tbDestinationAgency As System.Windows.Forms.TextBox
		Private label4 As System.Windows.Forms.Label
		Private tbOriginatingAgency As System.Windows.Forms.TextBox
		Private label10 As System.Windows.Forms.Label
		Private tbTransactionControlId As System.Windows.Forms.TextBox
		Private label12 As System.Windows.Forms.Label
		Private WithEvents cbValidationLevel As System.Windows.Forms.ComboBox
		Private newValidationLevelLabel As System.Windows.Forms.Label
		Private chbUseNistMinutiaNeighbors As System.Windows.Forms.CheckBox
	End Class
End Namespace