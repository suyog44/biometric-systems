Imports Microsoft.VisualBasic
Imports System
Partial Public Class VoiceView
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

	#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.label1 = New System.Windows.Forms.Label()
		Me.lblPhraseId = New System.Windows.Forms.Label()
		Me.label2 = New System.Windows.Forms.Label()
		Me.lblPhrase = New System.Windows.Forms.Label()
		Me.label3 = New System.Windows.Forms.Label()
		Me.label4 = New System.Windows.Forms.Label()
		Me.label5 = New System.Windows.Forms.Label()
		Me.lblQuality = New System.Windows.Forms.Label()
		Me.lblStart = New System.Windows.Forms.Label()
		Me.lblDuration = New System.Windows.Forms.Label()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' tableLayoutPanel1
		' 
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel1.Controls.Add(Me.label5, 0, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.label3, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.lblPhraseId, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.lblPhrase, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.label4, 0, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.lblQuality, 1, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.lblStart, 1, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.lblDuration, 1, 5)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 6
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(431, 154)
		Me.tableLayoutPanel1.TabIndex = 0
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label1.Location = New System.Drawing.Point(3, 30)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(115, 20)
		Me.label1.TabIndex = 4
		Me.label1.Text = "Phrase id:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.TopRight
		' 
		' lblPhraseId
		' 
		Me.lblPhraseId.AutoSize = True
		Me.lblPhraseId.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.lblPhraseId.Location = New System.Drawing.Point(124, 30)
		Me.lblPhraseId.Name = "lblPhraseId"
		Me.lblPhraseId.Size = New System.Drawing.Size(23, 20)
		Me.lblPhraseId.TabIndex = 1
		Me.lblPhraseId.Text = "id"
		' 
		' label2
		' 
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label2.Location = New System.Drawing.Point(3, 50)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(115, 20)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Phrase:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.TopRight
		' 
		' lblPhrase
		' 
		Me.lblPhrase.AutoSize = True
		Me.lblPhrase.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.lblPhrase.Location = New System.Drawing.Point(124, 50)
		Me.lblPhrase.Name = "lblPhrase"
		Me.lblPhrase.Size = New System.Drawing.Size(38, 20)
		Me.lblPhrase.TabIndex = 3
		Me.lblPhrase.Text = "N/A"
		' 
		' label3
		' 
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label3.Location = New System.Drawing.Point(3, 70)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(115, 20)
		Me.label3.TabIndex = 5
		Me.label3.Text = "Quality:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		' 
		' label4
		' 
		Me.label4.AutoSize = True
		Me.label4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label4.Location = New System.Drawing.Point(3, 90)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(115, 20)
		Me.label4.TabIndex = 6
		Me.label4.Text = "Voice start:"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.TopRight
		' 
		' label5
		' 
		Me.label5.AutoSize = True
		Me.label5.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label5.Location = New System.Drawing.Point(3, 110)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(115, 44)
		Me.label5.TabIndex = 7
		Me.label5.Text = "Voice duration:"
		Me.label5.TextAlign = System.Drawing.ContentAlignment.TopRight
		' 
		' lblQuality
		' 
		Me.lblQuality.AutoSize = True
		Me.lblQuality.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.lblQuality.Location = New System.Drawing.Point(124, 70)
		Me.lblQuality.Name = "lblQuality"
		Me.lblQuality.Size = New System.Drawing.Size(38, 20)
		Me.lblQuality.TabIndex = 8
		Me.lblQuality.Text = "N/A"
		' 
		' lblStart
		' 
		Me.lblStart.AutoSize = True
		Me.lblStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.lblStart.Location = New System.Drawing.Point(124, 90)
		Me.lblStart.Name = "lblStart"
		Me.lblStart.Size = New System.Drawing.Size(38, 20)
		Me.lblStart.TabIndex = 9
		Me.lblStart.Text = "N/A"
		' 
		' lblDuration
		' 
		Me.lblDuration.AutoSize = True
		Me.lblDuration.Font = New System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.lblDuration.Location = New System.Drawing.Point(124, 110)
		Me.lblDuration.Name = "lblDuration"
		Me.lblDuration.Size = New System.Drawing.Size(38, 20)
		Me.lblDuration.TabIndex = 10
		Me.lblDuration.Text = "N/A"
		' 
		' VoiceView
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "VoiceView"
		Me.Size = New System.Drawing.Size(431, 154)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private label1 As System.Windows.Forms.Label
	Private lblPhraseId As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private lblPhrase As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private label5 As System.Windows.Forms.Label
	Private label4 As System.Windows.Forms.Label
	Private lblQuality As System.Windows.Forms.Label
	Private lblStart As System.Windows.Forms.Label
	Private lblDuration As System.Windows.Forms.Label
End Class
