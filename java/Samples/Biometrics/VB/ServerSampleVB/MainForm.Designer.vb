Imports Microsoft.VisualBasic
Imports System

Partial Public Class MainForm
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
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.splitContainer = New System.Windows.Forms.SplitContainer
		Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
		Me.btnConnection = New System.Windows.Forms.Button
		Me.btnDeduplication = New System.Windows.Forms.Button
		Me.btnEnroll = New System.Windows.Forms.Button
		Me.btnTestSpeed = New System.Windows.Forms.Button
		Me.btnSettings = New System.Windows.Forms.Button
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.splitContainer.Panel1.SuspendLayout()
		Me.splitContainer.SuspendLayout()
		Me.flowLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'splitContainer
		'
		Me.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainer.IsSplitterFixed = True
		Me.splitContainer.Location = New System.Drawing.Point(0, 0)
		Me.splitContainer.Name = "splitContainer"
		'
		'splitContainer.Panel1
		'
		Me.splitContainer.Panel1.Controls.Add(Me.flowLayoutPanel1)
		Me.splitContainer.Size = New System.Drawing.Size(843, 414)
		Me.splitContainer.SplitterDistance = 194
		Me.splitContainer.TabIndex = 5
		'
		'flowLayoutPanel1
		'
		Me.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight
		Me.flowLayoutPanel1.Controls.Add(Me.btnConnection)
		Me.flowLayoutPanel1.Controls.Add(Me.btnDeduplication)
		Me.flowLayoutPanel1.Controls.Add(Me.btnEnroll)
		Me.flowLayoutPanel1.Controls.Add(Me.btnTestSpeed)
		Me.flowLayoutPanel1.Controls.Add(Me.btnSettings)
		Me.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
		Me.flowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
		Me.flowLayoutPanel1.Size = New System.Drawing.Size(194, 414)
		Me.flowLayoutPanel1.TabIndex = 0
		'
		'btnConnection
		'
		Me.btnConnection.Image = Global.Neurotec.Samples.My.Resources.Resources.settings
		Me.btnConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnConnection.Location = New System.Drawing.Point(3, 3)
		Me.btnConnection.Name = "btnConnection"
		Me.btnConnection.Size = New System.Drawing.Size(185, 41)
		Me.btnConnection.TabIndex = 2
		Me.btnConnection.Text = "Change connection settings"
		Me.btnConnection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnConnection.UseVisualStyleBackColor = True
		'
		'btnDeduplication
		'
		Me.btnDeduplication.BackColor = System.Drawing.SystemColors.InactiveCaption
		Me.btnDeduplication.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Goldenrod
		Me.btnDeduplication.Location = New System.Drawing.Point(3, 50)
		Me.btnDeduplication.Name = "btnDeduplication"
		Me.btnDeduplication.Size = New System.Drawing.Size(185, 41)
		Me.btnDeduplication.TabIndex = 3
		Me.btnDeduplication.Text = "Deduplication"
		Me.btnDeduplication.UseVisualStyleBackColor = False
		'
		'btnEnroll
		'
		Me.btnEnroll.BackColor = System.Drawing.SystemColors.InactiveCaption
		Me.btnEnroll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Goldenrod
		Me.btnEnroll.Location = New System.Drawing.Point(3, 97)
		Me.btnEnroll.Name = "btnEnroll"
		Me.btnEnroll.Size = New System.Drawing.Size(185, 41)
		Me.btnEnroll.TabIndex = 1
		Me.btnEnroll.Text = "Enroll templates"
		Me.btnEnroll.UseVisualStyleBackColor = False
		'
		'btnTestSpeed
		'
		Me.btnTestSpeed.BackColor = System.Drawing.SystemColors.InactiveCaption
		Me.btnTestSpeed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Goldenrod
		Me.btnTestSpeed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnTestSpeed.Location = New System.Drawing.Point(3, 144)
		Me.btnTestSpeed.Name = "btnTestSpeed"
		Me.btnTestSpeed.Size = New System.Drawing.Size(185, 41)
		Me.btnTestSpeed.TabIndex = 0
		Me.btnTestSpeed.Text = "Calculate/Test Accelerator matching speed"
		Me.btnTestSpeed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnTestSpeed.UseVisualStyleBackColor = False
		'
		'btnSettings
		'
		Me.btnSettings.BackColor = System.Drawing.SystemColors.InactiveCaption
		Me.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Goldenrod
		Me.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnSettings.Location = New System.Drawing.Point(3, 191)
		Me.btnSettings.Name = "btnSettings"
		Me.btnSettings.Size = New System.Drawing.Size(185, 41)
		Me.btnSettings.TabIndex = 4
		Me.btnSettings.Text = "Change matching settings"
		Me.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSettings.UseVisualStyleBackColor = False
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(843, 414)
		Me.Controls.Add(Me.splitContainer)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "Server sample"
		Me.splitContainer.Panel1.ResumeLayout(False)
		Me.splitContainer.ResumeLayout(False)
		Me.flowLayoutPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private splitContainer As System.Windows.Forms.SplitContainer
	Private flowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
	Private WithEvents btnConnection As System.Windows.Forms.Button
	Private WithEvents btnTestSpeed As System.Windows.Forms.Button
	Private WithEvents btnEnroll As System.Windows.Forms.Button
	Private toolTip As System.Windows.Forms.ToolTip
	Private WithEvents btnDeduplication As System.Windows.Forms.Button
	Private WithEvents btnSettings As System.Windows.Forms.Button
End Class
