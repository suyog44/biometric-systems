Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms

	Partial Public Class ANType16RecordCreateForm
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
			Me.label10 = New System.Windows.Forms.Label()
			Me.tbUdi = New System.Windows.Forms.TextBox()
			Me.imageLoader = New ImageLoaderControl()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' okButton
			' 
			Me.btnOk.Location = New System.Drawing.Point(149, 480)
			Me.btnOk.TabIndex = 5
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(230, 480)
			Me.btnCancel.TabIndex = 6
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Location = New System.Drawing.Point(10, 57)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(86, 13)
			Me.label10.TabIndex = 2
			Me.label10.Text = "User image type:"
			' 
			' tbUit
			' 
			Me.tbUdi.Location = New System.Drawing.Point(131, 54)
			Me.tbUdi.Name = "tbUit"
			Me.tbUdi.Size = New System.Drawing.Size(172, 20)
			Me.tbUdi.TabIndex = 3
			' 
			' imageLoader
			' 
			Me.imageLoader.Bpx = (CByte(0))
			Me.imageLoader.Colorspace = Neurotec.Biometrics.Standards.ANImageColorSpace.Unspecified
			Me.imageLoader.CompressionAlgorithm = Neurotec.Biometrics.Standards.ANImageCompressionAlgorithm.None
			Me.imageLoader.HasBpx = True
			Me.imageLoader.HasColorspace = True
			Me.imageLoader.Hll = (CUShort(0))
			Me.imageLoader.Hps = (CUShort(0))
			Me.imageLoader.Location = New System.Drawing.Point(2, 80)
			Me.imageLoader.Name = "imageLoader"
			Me.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None
			Me.imageLoader.Size = New System.Drawing.Size(311, 389)
			Me.imageLoader.Src = ""
			Me.imageLoader.TabIndex = 4
			Me.imageLoader.Vll = (CUShort(0))
			Me.imageLoader.Vps = (CUShort(0))
			' 
			' ANType16RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(315, 513)
			Me.Controls.Add(Me.label10)
			Me.Controls.Add(Me.tbUdi)
			Me.Controls.Add(Me.imageLoader)
			Me.Name = "ANType16RecordCreateForm"
			Me.Text = "Add Type-16 ANRecord"
			Me.Controls.SetChildIndex(Me.label1, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.imageLoader, 0)
			Me.Controls.SetChildIndex(Me.tbUdi, 0)
			Me.Controls.SetChildIndex(Me.label10, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private label10 As System.Windows.Forms.Label
		Private tbUdi As System.Windows.Forms.TextBox
		Private imageLoader As ImageLoaderControl
	End Class
End Namespace