Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms

	Partial Public Class ANType13RecordCreateForm
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
			Me.cbFpImpressionType = New System.Windows.Forms.ComboBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.imageLoader = New ImageLoaderControl()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' okButton
			' 
			Me.btnOk.Location = New System.Drawing.Point(155, 479)
			Me.btnOk.TabIndex = 5
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(236, 479)
			Me.btnCancel.TabIndex = 6
			' 
			' cbFpImpressionType
			' 
			Me.cbFpImpressionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbFpImpressionType.FormattingEnabled = True
			Me.cbFpImpressionType.Location = New System.Drawing.Point(132, 52)
			Me.cbFpImpressionType.Name = "cbFpImpressionType"
			Me.cbFpImpressionType.Size = New System.Drawing.Size(172, 21)
			Me.cbFpImpressionType.TabIndex = 3
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(10, 55)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(99, 13)
			Me.label2.TabIndex = 2
			Me.label2.Text = "FP Impression type:"
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
			Me.imageLoader.Location = New System.Drawing.Point(3, 79)
			Me.imageLoader.Name = "imageLoader"
			Me.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None
			Me.imageLoader.Size = New System.Drawing.Size(311, 389)
			Me.imageLoader.Src = ""
			Me.imageLoader.TabIndex = 4
			Me.imageLoader.Vll = (CUShort(0))
			Me.imageLoader.Vps = (CUShort(0))
			' 
			' ANType13RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(321, 512)
			Me.Controls.Add(Me.cbFpImpressionType)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.imageLoader)
			Me.Name = "ANType13RecordCreateForm"
			Me.Text = "Add Type-13 ANRecord"
			Me.Controls.SetChildIndex(Me.label1, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.imageLoader, 0)
			Me.Controls.SetChildIndex(Me.label2, 0)
			Me.Controls.SetChildIndex(Me.cbFpImpressionType, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private cbFpImpressionType As System.Windows.Forms.ComboBox
		Private label2 As System.Windows.Forms.Label
		Private imageLoader As ImageLoaderControl
	End Class
End Namespace