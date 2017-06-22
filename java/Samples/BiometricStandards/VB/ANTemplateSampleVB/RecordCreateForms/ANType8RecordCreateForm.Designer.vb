Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms

	Partial Public Class ANType8RecordCreateForm
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
			Me.rbFromVectors = New System.Windows.Forms.RadioButton
			Me.fromVectorsPanel = New System.Windows.Forms.Panel
			Me.btnEditVectors = New System.Windows.Forms.Button
			Me.label9 = New System.Windows.Forms.Label
			Me.resolutioEditBox = New Neurotec.Samples.RecordCreateForms.ResolutionEditBox
			Me.label10 = New System.Windows.Forms.Label
			Me.Label7 = New System.Windows.Forms.Label
			Me.cbSignature = New System.Windows.Forms.ComboBox
			Me.panelFromData.SuspendLayout()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.fromVectorsPanel.SuspendLayout()
			Me.SuspendLayout()
			'
			'tbVendorCa
			'
			Me.tbVendorCa.Enabled = False
			'
			'vendorCaLabel
			'
			Me.vendorCaLabel.Enabled = False
			'
			'label4
			'
			Me.label4.Size = New System.Drawing.Size(125, 13)
			Me.label4.Text = "Signature representation:"
			'
			'rbFromImage
			'
			Me.rbFromImage.Location = New System.Drawing.Point(14, 188)
			'
			'rbFromData
			'
			Me.rbFromData.Location = New System.Drawing.Point(14, 254)
			'
			'panelFromImage
			'
			Me.panelFromImage.Location = New System.Drawing.Point(14, 211)
			'
			'panelFromData
			'
			Me.panelFromData.Location = New System.Drawing.Point(14, 277)
			'
			'btnOk
			'
			Me.btnOk.Location = New System.Drawing.Point(159, 543)
			Me.btnOk.TabIndex = 15
			'
			'btnCancel
			'
			Me.btnCancel.Location = New System.Drawing.Point(240, 543)
			Me.btnCancel.TabIndex = 16
			'
			'rbFromVectors
			'
			Me.rbFromVectors.AutoSize = True
			Me.rbFromVectors.Location = New System.Drawing.Point(14, 450)
			Me.rbFromVectors.Name = "rbFromVectors"
			Me.rbFromVectors.Size = New System.Drawing.Size(89, 17)
			Me.rbFromVectors.TabIndex = 13
			Me.rbFromVectors.TabStop = True
			Me.rbFromVectors.Text = "From vectors:"
			Me.rbFromVectors.UseVisualStyleBackColor = True
			'
			'fromVectorsPanel
			'
			Me.fromVectorsPanel.Controls.Add(Me.btnEditVectors)
			Me.fromVectorsPanel.Controls.Add(Me.label9)
			Me.fromVectorsPanel.Controls.Add(Me.resolutioEditBox)
			Me.fromVectorsPanel.Controls.Add(Me.label10)
			Me.fromVectorsPanel.Location = New System.Drawing.Point(14, 473)
			Me.fromVectorsPanel.Name = "fromVectorsPanel"
			Me.fromVectorsPanel.Size = New System.Drawing.Size(301, 61)
			Me.fromVectorsPanel.TabIndex = 14
			'
			'btnEditVectors
			'
			Me.btnEditVectors.Location = New System.Drawing.Point(126, 29)
			Me.btnEditVectors.Name = "btnEditVectors"
			Me.btnEditVectors.Size = New System.Drawing.Size(75, 23)
			Me.btnEditVectors.TabIndex = 3
			Me.btnEditVectors.Text = "Edit..."
			Me.btnEditVectors.UseVisualStyleBackColor = True
			'
			'label9
			'
			Me.label9.AutoSize = True
			Me.label9.Location = New System.Drawing.Point(5, 7)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(87, 13)
			Me.label9.TabIndex = 0
			Me.label9.Text = "Image resolution:"
			'
			'resolutioEditBox
			'
			Me.resolutioEditBox.Location = New System.Drawing.Point(127, 1)
			Me.resolutioEditBox.Name = "resolutioEditBox"
			Me.resolutioEditBox.PpcmValue = 0
			Me.resolutioEditBox.PpiValue = 0
			Me.resolutioEditBox.PpmmValue = 0
			Me.resolutioEditBox.PpmValue = 0
			Me.resolutioEditBox.Size = New System.Drawing.Size(148, 30)
			Me.resolutioEditBox.TabIndex = 1
			'
			'label10
			'
			Me.label10.AutoSize = True
			Me.label10.Location = New System.Drawing.Point(5, 34)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(46, 13)
			Me.label10.TabIndex = 2
			Me.label10.Text = "Vectors:"
			'
			'Label7
			'
			Me.Label7.AutoSize = True
			Me.Label7.Location = New System.Drawing.Point(12, 164)
			Me.Label7.Name = "Label7"
			Me.Label7.Size = New System.Drawing.Size(55, 13)
			Me.Label7.TabIndex = 17
			Me.Label7.Text = "Signature:"
			'
			'cbSignature
			'
			Me.cbSignature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbSignature.FormattingEnabled = True
			Me.cbSignature.Location = New System.Drawing.Point(138, 161)
			Me.cbSignature.Name = "cbSignature"
			Me.cbSignature.Size = New System.Drawing.Size(172, 21)
			Me.cbSignature.TabIndex = 18
			'
			'ANType8RecordCreateForm
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(325, 576)
			Me.Controls.Add(Me.Label7)
			Me.Controls.Add(Me.cbSignature)
			Me.Controls.Add(Me.rbFromVectors)
			Me.Controls.Add(Me.fromVectorsPanel)
			Me.Name = "ANType8RecordCreateForm"
			Me.Text = "Add Type-8 ANRecord"
			Me.Controls.SetChildIndex(Me.rbFromData, 0)
			Me.Controls.SetChildIndex(Me.panelFromData, 0)
			Me.Controls.SetChildIndex(Me.rbFromImage, 0)
			Me.Controls.SetChildIndex(Me.panelFromImage, 0)
			Me.Controls.SetChildIndex(Me.fromVectorsPanel, 0)
			Me.Controls.SetChildIndex(Me.rbFromVectors, 0)
			Me.Controls.SetChildIndex(Me.label1, 0)
			Me.Controls.SetChildIndex(Me.label4, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.cbSignature, 0)
			Me.Controls.SetChildIndex(Me.Label7, 0)
			Me.panelFromData.ResumeLayout(False)
			Me.panelFromData.PerformLayout()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.fromVectorsPanel.ResumeLayout(False)
			Me.fromVectorsPanel.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private WithEvents rbFromVectors As System.Windows.Forms.RadioButton
		Private fromVectorsPanel As System.Windows.Forms.Panel
		Private WithEvents btnEditVectors As System.Windows.Forms.Button
		Private label9 As System.Windows.Forms.Label
		Private resolutioEditBox As ResolutionEditBox
		Protected label10 As System.Windows.Forms.Label
		Protected WithEvents Label7 As System.Windows.Forms.Label
		Protected WithEvents cbSignature As System.Windows.Forms.ComboBox
	End Class
End Namespace