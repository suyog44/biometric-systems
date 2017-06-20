Imports Microsoft.VisualBasic
Imports System
Namespace Forms
	Partial Public Class ExtractionOptionsForm
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExtractionOptionsForm))
			Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
			Me.nudQualityThreshold = New System.Windows.Forms.NumericUpDown
			Me.cbTemplateSize = New System.Windows.Forms.ComboBox
			Me.vfeTemplateSizeLabel = New System.Windows.Forms.Label
			Me.label4 = New System.Windows.Forms.Label
			Me.label3 = New System.Windows.Forms.Label
			Me.nudMaximalRotation = New System.Windows.Forms.NumericUpDown
			Me.chbFastExtraction = New System.Windows.Forms.CheckBox
			Me.btnOk = New System.Windows.Forms.Button
			Me.btnCancel = New System.Windows.Forms.Button
			Me.btnDefault = New System.Windows.Forms.Button
			Me.tableLayoutPanel.SuspendLayout()
			CType(Me.nudQualityThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudMaximalRotation, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			'
			'tableLayoutPanel
			'
			Me.tableLayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.tableLayoutPanel.ColumnCount = 3
			Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
			Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
			Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
			Me.tableLayoutPanel.Controls.Add(Me.nudQualityThreshold, 2, 1)
			Me.tableLayoutPanel.Controls.Add(Me.cbTemplateSize, 2, 0)
			Me.tableLayoutPanel.Controls.Add(Me.vfeTemplateSizeLabel, 0, 0)
			Me.tableLayoutPanel.Controls.Add(Me.label4, 0, 1)
			Me.tableLayoutPanel.Controls.Add(Me.label3, 0, 2)
			Me.tableLayoutPanel.Controls.Add(Me.nudMaximalRotation, 2, 2)
			Me.tableLayoutPanel.Controls.Add(Me.chbFastExtraction, 2, 3)
			Me.tableLayoutPanel.Location = New System.Drawing.Point(1, 2)
			Me.tableLayoutPanel.Name = "tableLayoutPanel"
			Me.tableLayoutPanel.RowCount = 4
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanel.Size = New System.Drawing.Size(273, 112)
			Me.tableLayoutPanel.TabIndex = 0
			'
			'nudQualityThreshold
			'
			Me.nudQualityThreshold.Location = New System.Drawing.Point(97, 30)
			Me.nudQualityThreshold.Name = "nudQualityThreshold"
			Me.nudQualityThreshold.Size = New System.Drawing.Size(173, 20)
			Me.nudQualityThreshold.TabIndex = 28
			'
			'cbTemplateSize
			'
			Me.cbTemplateSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbTemplateSize.FormattingEnabled = True
			Me.cbTemplateSize.Location = New System.Drawing.Point(97, 3)
			Me.cbTemplateSize.Name = "cbTemplateSize"
			Me.cbTemplateSize.Size = New System.Drawing.Size(173, 21)
			Me.cbTemplateSize.TabIndex = 34
			'
			'vfeTemplateSizeLabel
			'
			Me.vfeTemplateSizeLabel.AutoSize = True
			Me.vfeTemplateSizeLabel.Dock = System.Windows.Forms.DockStyle.Fill
			Me.vfeTemplateSizeLabel.Location = New System.Drawing.Point(3, 0)
			Me.vfeTemplateSizeLabel.Name = "vfeTemplateSizeLabel"
			Me.vfeTemplateSizeLabel.Size = New System.Drawing.Size(88, 27)
			Me.vfeTemplateSizeLabel.TabIndex = 22
			Me.vfeTemplateSizeLabel.Text = "&Template size:"
			Me.vfeTemplateSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			'
			'label4
			'
			Me.label4.AutoSize = True
			Me.label4.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label4.Location = New System.Drawing.Point(3, 27)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(88, 26)
			Me.label4.TabIndex = 40
			Me.label4.Text = "Quality threshold:"
			Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			'
			'label3
			'
			Me.label3.AutoSize = True
			Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label3.Location = New System.Drawing.Point(3, 53)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(88, 26)
			Me.label3.TabIndex = 39
			Me.label3.Text = "Maximal rotation:"
			Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			'
			'nudMaximalRotation
			'
			Me.nudMaximalRotation.Location = New System.Drawing.Point(97, 56)
			Me.nudMaximalRotation.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
			Me.nudMaximalRotation.Name = "nudMaximalRotation"
			Me.nudMaximalRotation.Size = New System.Drawing.Size(173, 20)
			Me.nudMaximalRotation.TabIndex = 41
			'
			'chbFastExtraction
			'
			Me.chbFastExtraction.AutoSize = True
			Me.chbFastExtraction.Location = New System.Drawing.Point(97, 82)
			Me.chbFastExtraction.Name = "chbFastExtraction"
			Me.chbFastExtraction.Size = New System.Drawing.Size(95, 17)
			Me.chbFastExtraction.TabIndex = 37
			Me.chbFastExtraction.Text = "Fast extraction"
			Me.chbFastExtraction.UseVisualStyleBackColor = True
			'
			'btnOk
			'
			Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
			Me.btnOk.Location = New System.Drawing.Point(115, 120)
			Me.btnOk.Name = "btnOk"
			Me.btnOk.Size = New System.Drawing.Size(75, 23)
			Me.btnOk.TabIndex = 1
			Me.btnOk.Text = "&OK"
			Me.btnOk.UseVisualStyleBackColor = True
			'
			'btnCancel
			'
			Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnCancel.Location = New System.Drawing.Point(196, 120)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(75, 23)
			Me.btnCancel.TabIndex = 2
			Me.btnCancel.Text = "&Cancel"
			Me.btnCancel.UseVisualStyleBackColor = True
			'
			'btnDefault
			'
			Me.btnDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
			Me.btnDefault.Location = New System.Drawing.Point(4, 120)
			Me.btnDefault.Name = "btnDefault"
			Me.btnDefault.Size = New System.Drawing.Size(75, 23)
			Me.btnDefault.TabIndex = 3
			Me.btnDefault.Text = "&Default"
			Me.btnDefault.UseVisualStyleBackColor = True
			'
			'ExtractionOptionsForm
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(274, 147)
			Me.Controls.Add(Me.btnDefault)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnOk)
			Me.Controls.Add(Me.tableLayoutPanel)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
			Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
			Me.Name = "ExtractionOptionsForm"
			Me.Text = "Extraction Options"
			Me.tableLayoutPanel.ResumeLayout(False)
			Me.tableLayoutPanel.PerformLayout()
			CType(Me.nudQualityThreshold, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudMaximalRotation, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
		Private WithEvents btnOk As System.Windows.Forms.Button
		Private btnCancel As System.Windows.Forms.Button
		Private nudQualityThreshold As System.Windows.Forms.NumericUpDown
		Private WithEvents btnDefault As System.Windows.Forms.Button
		Private chbFastExtraction As System.Windows.Forms.CheckBox
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private cbTemplateSize As System.Windows.Forms.ComboBox
		Private vfeTemplateSizeLabel As System.Windows.Forms.Label
		Private nudMaximalRotation As System.Windows.Forms.NumericUpDown
	End Class
End Namespace
