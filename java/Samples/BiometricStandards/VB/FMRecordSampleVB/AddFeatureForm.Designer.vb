Imports Microsoft.VisualBasic
Imports System
Partial Public Class AddFeatureForm
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(AddFeatureForm))
		Me.cbFeature = New System.Windows.Forms.ComboBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.labelType = New System.Windows.Forms.Label()
		Me.cbType = New System.Windows.Forms.ComboBox()
		Me.SuspendLayout()
		' 
		' cbFeature
		' 
		Me.cbFeature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbFeature.FormattingEnabled = True
		Me.cbFeature.Items.AddRange(New Object() {"Minutia", "Core", "Delta"})
		Me.cbFeature.Location = New System.Drawing.Point(61, 12)
		Me.cbFeature.Name = "cbFeature"
		Me.cbFeature.Size = New System.Drawing.Size(110, 21)
		Me.cbFeature.TabIndex = 0
		'			Me.cbFeature.SelectedIndexChanged += New System.EventHandler(Me.cbFeature_SelectedIndexChanged);
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(12, 15)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(43, 13)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Feature"
		' 
		' btnOk
		' 
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(15, 64)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 2
		Me.btnOk.Text = "OK"
		Me.btnOk.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(96, 64)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 3
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' labelType
		' 
		Me.labelType.AutoSize = True
		Me.labelType.Location = New System.Drawing.Point(12, 40)
		Me.labelType.Name = "labelType"
		Me.labelType.Size = New System.Drawing.Size(34, 13)
		Me.labelType.TabIndex = 4
		Me.labelType.Text = "Type:"
		' 
		' cbType
		' 
		Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbType.FormattingEnabled = True
		Me.cbType.Location = New System.Drawing.Point(61, 37)
		Me.cbType.Name = "cbType"
		Me.cbType.Size = New System.Drawing.Size(110, 21)
		Me.cbType.TabIndex = 5
		' 
		' AddFeatureForm
		' 
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(179, 91)
		Me.Controls.Add(Me.cbType)
		Me.Controls.Add(Me.labelType)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.cbFeature)
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.Name = "AddFeatureForm"
		Me.Text = "Add Feature"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private label1 As System.Windows.Forms.Label
	Private btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private labelType As System.Windows.Forms.Label
	Public WithEvents cbFeature As System.Windows.Forms.ComboBox
	Friend cbType As System.Windows.Forms.ComboBox
End Class
