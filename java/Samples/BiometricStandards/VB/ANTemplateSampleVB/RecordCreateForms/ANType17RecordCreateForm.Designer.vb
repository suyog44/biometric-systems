Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms

	Partial Public Class ANType17RecordCreateForm
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
			Me.imageLoader = New ImageLoaderControl()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' okButton
			' 
			Me.btnOk.Location = New System.Drawing.Point(154, 457)
			Me.btnOk.TabIndex = 3
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(235, 457)
			Me.btnCancel.TabIndex = 4
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
			Me.imageLoader.Location = New System.Drawing.Point(5, 51)
			Me.imageLoader.Name = "imageLoader"
			Me.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None
			Me.imageLoader.Size = New System.Drawing.Size(311, 389)
			Me.imageLoader.Src = ""
			Me.imageLoader.TabIndex = 2
			Me.imageLoader.Vll = (CUShort(0))
			Me.imageLoader.Vps = (CUShort(0))
			' 
			' ANType17RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(320, 490)
			Me.Controls.Add(Me.imageLoader)
			Me.Name = "ANType17RecordCreateForm"
			Me.Text = "Add Type-17 ANRecord"
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.imageLoader, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private imageLoader As ImageLoaderControl
	End Class
End Namespace