Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms
	Partial Public Class ANRecordCreateForm
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
			Me.nudIdc = New System.Windows.Forms.NumericUpDown()
			Me.label1 = New System.Windows.Forms.Label()
			Me.btnOk = New System.Windows.Forms.Button()
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.errorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' nudIdc
			' 
			Me.nudIdc.Location = New System.Drawing.Point(12, 27)
			Me.nudIdc.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
			Me.nudIdc.Name = "nudIdc"
			Me.nudIdc.Size = New System.Drawing.Size(120, 20)
			Me.nudIdc.TabIndex = 1
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(9, 11)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(28, 13)
			Me.label1.TabIndex = 0
			Me.label1.Text = "IDC:"
			' 
			' btnOk
			' 
			Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
			Me.btnOk.Location = New System.Drawing.Point(27, 70)
			Me.btnOk.Name = "btnOk"
			Me.btnOk.Size = New System.Drawing.Size(75, 23)
			Me.btnOk.TabIndex = 2
			Me.btnOk.Text = "OK"
			Me.btnOk.UseVisualStyleBackColor = True
			' 
			' btnCancel
			' 
			Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnCancel.CausesValidation = False
			Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnCancel.Location = New System.Drawing.Point(108, 70)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(75, 23)
			Me.btnCancel.TabIndex = 3
			Me.btnCancel.Text = "Cancel"
			Me.btnCancel.UseVisualStyleBackColor = True
			' 
			' errorProvider
			' 
			Me.errorProvider.ContainerControl = Me
			' 
			' ANRecordCreateForm
			' 
			Me.AcceptButton = Me.btnOk
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.CancelButton = Me.btnCancel
			Me.ClientSize = New System.Drawing.Size(193, 103)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnOk)
			Me.Controls.Add(Me.nudIdc)
			Me.Controls.Add(Me.label1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "ANRecordCreateForm"
			Me.ShowIcon = False
			Me.ShowInTaskbar = False
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Add ANRecord"
			'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.ANRecordCreateFormFormClosing);
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Protected btnOk As System.Windows.Forms.Button
		Protected btnCancel As System.Windows.Forms.Button
		Protected errorProvider As System.Windows.Forms.ErrorProvider
		Protected nudIdc As System.Windows.Forms.NumericUpDown
		Protected label1 As System.Windows.Forms.Label
	End Class
End Namespace