Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms

	Partial Public Class ANType15RecordCreateForm
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
			Me.btnOk.Location = New System.Drawing.Point(153, 462)
			Me.btnOk.TabIndex = 3
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(234, 462)
			Me.btnCancel.TabIndex = 4
			' 
			' imageLoader
			' 
			Me.imageLoader.Bpx = (CByte(0))
			Me.imageLoader.Colorspace = Neurotec.Biometrics.Standards.ANImageColorSpace.Unspecified
			Me.imageLoader.CompressionAlgorithm = Neurotec.Biometrics.Standards.ANImageCompressionAlgorithm.None
			Me.imageLoader.HasBpx = True
			Me.imageLoader.HasColorspace = False
			Me.imageLoader.Hll = (CUShort(0))
			Me.imageLoader.Hps = (CUShort(0))
			Me.imageLoader.Location = New System.Drawing.Point(3, 53)
			Me.imageLoader.Name = "imageLoader"
			Me.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None
			Me.imageLoader.Size = New System.Drawing.Size(311, 389)
			Me.imageLoader.Src = ""
			Me.imageLoader.TabIndex = 2
			Me.imageLoader.Vll = (CUShort(0))
			Me.imageLoader.Vps = (CUShort(0))
			' 
			' ANType15RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(319, 495)
			Me.Controls.Add(Me.imageLoader)
			Me.Name = "ANType15RecordCreateForm"
			Me.Text = "Add Type-15 ANRecord"
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