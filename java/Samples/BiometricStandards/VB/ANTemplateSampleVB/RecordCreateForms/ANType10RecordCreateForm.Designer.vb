Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms

	Partial Public Class ANType10RecordCreateForm
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
			Me.imageTypeComboBox = New System.Windows.Forms.ComboBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.label10 = New System.Windows.Forms.Label()
			Me.smtTextBox = New System.Windows.Forms.TextBox()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' okButton
			' 
			Me.btnOk.Location = New System.Drawing.Point(149, 517)
			Me.btnOk.TabIndex = 7
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(230, 517)
			Me.btnCancel.TabIndex = 8
			' 
			' imageLoader
			' 
			Me.imageLoader.Bpx = (CByte(0))
			Me.imageLoader.Colorspace = Neurotec.Biometrics.Standards.ANImageColorSpace.Unspecified
			Me.imageLoader.CompressionAlgorithm = Neurotec.Biometrics.Standards.ANImageCompressionAlgorithm.None
			Me.imageLoader.HasBpx = False
			Me.imageLoader.HasColorspace = True
			Me.imageLoader.Hll = (CUShort(0))
			Me.imageLoader.Hps = (CUShort(0))
			Me.imageLoader.Location = New System.Drawing.Point(-2, 111)
			Me.imageLoader.Name = "imageLoader"
			Me.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None
			Me.imageLoader.Size = New System.Drawing.Size(311, 389)
			Me.imageLoader.Src = ""
			Me.imageLoader.TabIndex = 6
			Me.imageLoader.Vll = (CUShort(0))
			Me.imageLoader.Vps = (CUShort(0))
			' 
			' imageTypeComboBox
			' 
			Me.imageTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.imageTypeComboBox.FormattingEnabled = True
			Me.imageTypeComboBox.Location = New System.Drawing.Point(127, 58)
			Me.imageTypeComboBox.Name = "imageTypeComboBox"
			Me.imageTypeComboBox.Size = New System.Drawing.Size(172, 21)
			Me.imageTypeComboBox.TabIndex = 3
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(5, 61)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(62, 13)
			Me.label2.TabIndex = 2
			Me.label2.Text = "Image type:"
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Location = New System.Drawing.Point(6, 88)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(28, 13)
			Me.label10.TabIndex = 4
			Me.label10.Text = "Smt:"
			' 
			' smtTextBox
			' 
			Me.smtTextBox.Location = New System.Drawing.Point(127, 85)
			Me.smtTextBox.Name = "smtTextBox"
			Me.smtTextBox.Size = New System.Drawing.Size(172, 20)
			Me.smtTextBox.TabIndex = 5
			' 
			' ANType10RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(315, 550)
			Me.Controls.Add(Me.label10)
			Me.Controls.Add(Me.smtTextBox)
			Me.Controls.Add(Me.imageTypeComboBox)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.imageLoader)
			Me.Name = "ANType10RecordCreateForm"
			Me.Text = "Add Type-10 ANRecord"
			Me.Controls.SetChildIndex(Me.imageLoader, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.label2, 0)
			Me.Controls.SetChildIndex(Me.imageTypeComboBox, 0)
			Me.Controls.SetChildIndex(Me.smtTextBox, 0)
			Me.Controls.SetChildIndex(Me.label10, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private imageLoader As ImageLoaderControl
		Private imageTypeComboBox As System.Windows.Forms.ComboBox
		Private label2 As System.Windows.Forms.Label
		Private label10 As System.Windows.Forms.Label
		Private smtTextBox As System.Windows.Forms.TextBox
	End Class
End Namespace