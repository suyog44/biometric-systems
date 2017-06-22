Imports Microsoft.VisualBasic
Imports System
Partial Public Class EditPhrasesForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditPhrasesForm))
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.lvPhrases = New System.Windows.Forms.ListView
		Me.chPhraseId = New System.Windows.Forms.ColumnHeader
		Me.chPhrase = New System.Windows.Forms.ColumnHeader
		Me.btnRemove = New System.Windows.Forms.Button
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.btnAdd = New System.Windows.Forms.Button
		Me.tbPhrase = New System.Windows.Forms.TextBox
		Me.tbPhraseId = New System.Windows.Forms.TextBox
		Me.label2 = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.btnClose = New System.Windows.Forms.Button
		Me.groupBox1.SuspendLayout()
		Me.groupBox2.SuspendLayout()
		Me.SuspendLayout()
		'
		'groupBox1
		'
		Me.groupBox1.Controls.Add(Me.lvPhrases)
		Me.groupBox1.Controls.Add(Me.btnRemove)
		Me.groupBox1.Location = New System.Drawing.Point(8, 3)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(268, 316)
		Me.groupBox1.TabIndex = 0
		Me.groupBox1.TabStop = False
		'
		'lvPhrases
		'
		Me.lvPhrases.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chPhraseId, Me.chPhrase})
		Me.lvPhrases.FullRowSelect = True
		Me.lvPhrases.Location = New System.Drawing.Point(6, 19)
		Me.lvPhrases.MultiSelect = False
		Me.lvPhrases.Name = "lvPhrases"
		Me.lvPhrases.Size = New System.Drawing.Size(256, 247)
		Me.lvPhrases.TabIndex = 0
		Me.lvPhrases.UseCompatibleStateImageBehavior = False
		Me.lvPhrases.View = System.Windows.Forms.View.Details
		'
		'chPhraseId
		'
		Me.chPhraseId.Text = "Phrase Id"
		'
		'chPhrase
		'
		Me.chPhrase.Text = "Phrase"
		Me.chPhrase.Width = 189
		'
		'btnRemove
		'
		Me.btnRemove.Image = My.Resources.remove
		Me.btnRemove.Location = New System.Drawing.Point(221, 272)
		Me.btnRemove.Name = "btnRemove"
		Me.btnRemove.Size = New System.Drawing.Size(41, 33)
		Me.btnRemove.TabIndex = 1
		Me.btnRemove.UseVisualStyleBackColor = True
		'
		'groupBox2
		'
		Me.groupBox2.Controls.Add(Me.btnAdd)
		Me.groupBox2.Controls.Add(Me.tbPhrase)
		Me.groupBox2.Controls.Add(Me.tbPhraseId)
		Me.groupBox2.Controls.Add(Me.label2)
		Me.groupBox2.Controls.Add(Me.label1)
		Me.groupBox2.Location = New System.Drawing.Point(8, 325)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(268, 117)
		Me.groupBox2.TabIndex = 1
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Add new"
		'
		'btnAdd
		'
		Me.btnAdd.Image = My.Resources.add
		Me.btnAdd.Location = New System.Drawing.Point(221, 75)
		Me.btnAdd.Name = "btnAdd"
		Me.btnAdd.Size = New System.Drawing.Size(41, 34)
		Me.btnAdd.TabIndex = 4
		Me.btnAdd.UseVisualStyleBackColor = True
		'
		'tbPhrase
		'
		Me.tbPhrase.Location = New System.Drawing.Point(79, 49)
		Me.tbPhrase.Name = "tbPhrase"
		Me.tbPhrase.Size = New System.Drawing.Size(183, 20)
		Me.tbPhrase.TabIndex = 3
		'
		'tbPhraseId
		'
		Me.tbPhraseId.Location = New System.Drawing.Point(79, 23)
		Me.tbPhraseId.Name = "tbPhraseId"
		Me.tbPhraseId.Size = New System.Drawing.Size(183, 20)
		Me.tbPhraseId.TabIndex = 1
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(6, 49)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(43, 13)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Phrase:"
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(6, 26)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(54, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Phrase id:"
		'
		'btnClose
		'
		Me.btnClose.Location = New System.Drawing.Point(201, 448)
		Me.btnClose.Name = "btnClose"
		Me.btnClose.Size = New System.Drawing.Size(75, 23)
		Me.btnClose.TabIndex = 2
		Me.btnClose.Text = "&Close"
		Me.btnClose.UseVisualStyleBackColor = True
		'
		'EditPhrasesForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(288, 484)
		Me.Controls.Add(Me.btnClose)
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.groupBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "EditPhrasesForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Add phrase"
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox2.ResumeLayout(False)
		Me.groupBox2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private groupBox1 As System.Windows.Forms.GroupBox
	Private lvPhrases As System.Windows.Forms.ListView
	Private chPhraseId As System.Windows.Forms.ColumnHeader
	Private chPhrase As System.Windows.Forms.ColumnHeader
	Private WithEvents btnRemove As System.Windows.Forms.Button
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private WithEvents btnAdd As System.Windows.Forms.Button
	Private tbPhrase As System.Windows.Forms.TextBox
	Private tbPhraseId As System.Windows.Forms.TextBox
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private WithEvents btnClose As System.Windows.Forms.Button


End Class