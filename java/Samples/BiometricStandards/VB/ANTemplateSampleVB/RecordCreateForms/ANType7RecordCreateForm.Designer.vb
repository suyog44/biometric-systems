Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms

	Partial Public Class ANType7RecordCreateForm
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
			Me.isrResolutionEditBox = New ResolutionEditBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.irResolutionEditBox = New ResolutionEditBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.tbImageDatePath = New System.Windows.Forms.TextBox()
			Me.btnBrowseImageData = New System.Windows.Forms.Button()
			Me.imageDataOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' okButton
			' 
			Me.btnOk.Location = New System.Drawing.Point(125, 215)
			Me.btnOk.TabIndex = 9
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(206, 215)
			Me.btnCancel.TabIndex = 10
			' 
			' isrResolutionEditBox
			' 
			Me.isrResolutionEditBox.Location = New System.Drawing.Point(12, 70)
			Me.isrResolutionEditBox.Name = "isrResolutionEditBox"
			Me.isrResolutionEditBox.PpcmValue = 0
			Me.isrResolutionEditBox.PpiValue = 0
			Me.isrResolutionEditBox.PpmmValue = 0
			Me.isrResolutionEditBox.PpmValue = 0
			Me.isrResolutionEditBox.Size = New System.Drawing.Size(213, 30)
			Me.isrResolutionEditBox.TabIndex = 3
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(12, 54)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(133, 13)
			Me.label2.TabIndex = 2
			Me.label2.Text = "Image scanning resolution:"
			' 
			' irResolutionEditBox
			' 
			Me.irResolutionEditBox.Location = New System.Drawing.Point(12, 117)
			Me.irResolutionEditBox.Name = "irResolutionEditBox"
			Me.irResolutionEditBox.PpcmValue = 0
			Me.irResolutionEditBox.PpiValue = 0
			Me.irResolutionEditBox.PpmmValue = 0
			Me.irResolutionEditBox.PpmValue = 0
			Me.irResolutionEditBox.Size = New System.Drawing.Size(213, 30)
			Me.irResolutionEditBox.TabIndex = 5
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(12, 101)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(89, 13)
			Me.label3.TabIndex = 4
			Me.label3.Text = "Native resolution:"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(12, 150)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(63, 13)
			Me.label4.TabIndex = 6
			Me.label4.Text = "Image data:"
			' 
			' tbImageDatePath
			' 
			Me.tbImageDatePath.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbImageDatePath.Location = New System.Drawing.Point(12, 166)
			Me.tbImageDatePath.Name = "tbImageDatePath"
			Me.tbImageDatePath.Size = New System.Drawing.Size(186, 20)
			Me.tbImageDatePath.TabIndex = 7
			' 
			' btnBrowseImageData
			' 
			Me.btnBrowseImageData.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnBrowseImageData.Location = New System.Drawing.Point(204, 164)
			Me.btnBrowseImageData.Name = "btnBrowseImageData"
			Me.btnBrowseImageData.Size = New System.Drawing.Size(75, 23)
			Me.btnBrowseImageData.TabIndex = 8
			Me.btnBrowseImageData.Text = "Browse..."
			Me.btnBrowseImageData.UseVisualStyleBackColor = True
			'			Me.btnBrowseImageData.Click += New System.EventHandler(Me.BtnImageDataClick);
			' 
			' imageDataOpenFileDialog
			' 
			Me.imageDataOpenFileDialog.Filter = "All Files (*.*)|*.*"
			' 
			' ANType7RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(291, 248)
			Me.Controls.Add(Me.tbImageDatePath)
			Me.Controls.Add(Me.btnBrowseImageData)
			Me.Controls.Add(Me.label4)
			Me.Controls.Add(Me.irResolutionEditBox)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.isrResolutionEditBox)
			Me.Controls.Add(Me.label2)
			Me.Name = "ANType7RecordCreateForm"
			Me.Text = "Add Type-7 ANRecord"
			Me.Controls.SetChildIndex(Me.label1, 0)
			Me.Controls.SetChildIndex(Me.label2, 0)
			Me.Controls.SetChildIndex(Me.isrResolutionEditBox, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.label3, 0)
			Me.Controls.SetChildIndex(Me.irResolutionEditBox, 0)
			Me.Controls.SetChildIndex(Me.label4, 0)
			Me.Controls.SetChildIndex(Me.btnBrowseImageData, 0)
			Me.Controls.SetChildIndex(Me.tbImageDatePath, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private isrResolutionEditBox As ResolutionEditBox
		Private label2 As System.Windows.Forms.Label
		Private irResolutionEditBox As ResolutionEditBox
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private tbImageDatePath As System.Windows.Forms.TextBox
		Private WithEvents btnBrowseImageData As System.Windows.Forms.Button
		Private imageDataOpenFileDialog As System.Windows.Forms.OpenFileDialog
	End Class
End Namespace