Partial Public Class FCRecordOptionsForm
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
		Me.gbFCRecord = New System.Windows.Forms.GroupBox
		Me.cbSkipFeaturePoints = New System.Windows.Forms.CheckBox
		Me.cbProcessFirstFaceImageOnly = New System.Windows.Forms.CheckBox
		Me.gbFCRecord.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnCancel
		'
		Me.btnCancel.Location = New System.Drawing.Point(245, 208)
		'
		'btnOk
		'
		Me.btnOk.Location = New System.Drawing.Point(164, 208)
		'
		'gbFCRecord
		'
		Me.gbFCRecord.Controls.Add(Me.cbSkipFeaturePoints)
		Me.gbFCRecord.Controls.Add(Me.cbProcessFirstFaceImageOnly)
		Me.gbFCRecord.Location = New System.Drawing.Point(12, 136)
		Me.gbFCRecord.Name = "gbFCRecord"
		Me.gbFCRecord.Size = New System.Drawing.Size(311, 65)
		Me.gbFCRecord.TabIndex = 5
		Me.gbFCRecord.TabStop = False
		Me.gbFCRecord.Text = "FCRecord"
		'
		'cbSkipFeaturePoints
		'
		Me.cbSkipFeaturePoints.AutoSize = True
		Me.cbSkipFeaturePoints.Location = New System.Drawing.Point(21, 42)
		Me.cbSkipFeaturePoints.Name = "cbSkipFeaturePoints"
		Me.cbSkipFeaturePoints.Size = New System.Drawing.Size(114, 17)
		Me.cbSkipFeaturePoints.TabIndex = 1
		Me.cbSkipFeaturePoints.Text = "Skip feature points"
		Me.cbSkipFeaturePoints.UseVisualStyleBackColor = True
		'
		'cbProcessFirstFaceImageOnly
		'
		Me.cbProcessFirstFaceImageOnly.AutoSize = True
		Me.cbProcessFirstFaceImageOnly.Location = New System.Drawing.Point(21, 19)
		Me.cbProcessFirstFaceImageOnly.Name = "cbProcessFirstFaceImageOnly"
		Me.cbProcessFirstFaceImageOnly.Size = New System.Drawing.Size(160, 17)
		Me.cbProcessFirstFaceImageOnly.TabIndex = 0
		Me.cbProcessFirstFaceImageOnly.Text = "Process first face image only"
		Me.cbProcessFirstFaceImageOnly.UseVisualStyleBackColor = True
		'
		'FCRecordOptionsForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(332, 243)
		Me.Controls.Add(Me.gbFCRecord)
		Me.Name = "FCRecordOptionsForm"
		Me.Text = "FCRecordOptionsForm"
		Me.Controls.SetChildIndex(Me.btnOk, 0)
		Me.Controls.SetChildIndex(Me.btnCancel, 0)
		Me.Controls.SetChildIndex(Me.gbFCRecord, 0)
		Me.gbFCRecord.ResumeLayout(False)
		Me.gbFCRecord.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private gbFCRecord As System.Windows.Forms.GroupBox
	Private cbSkipFeaturePoints As System.Windows.Forms.CheckBox
	Private cbProcessFirstFaceImageOnly As System.Windows.Forms.CheckBox
	Private cbVersion As System.Windows.Forms.ComboBox



End Class