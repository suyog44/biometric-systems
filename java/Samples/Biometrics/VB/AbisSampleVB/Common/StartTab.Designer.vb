Imports Microsoft.VisualBasic
Imports System
Partial Public Class StartTab
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(StartTab))
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.btnChangeDb = New System.Windows.Forms.Button()
		Me.btnNew = New System.Windows.Forms.Button()
		Me.btnOpen = New System.Windows.Forms.Button()
		Me.btnSettings = New System.Windows.Forms.Button()
		Me.btnAbout = New System.Windows.Forms.Button()
		Me.pbLogo = New System.Windows.Forms.PictureBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.label2 = New System.Windows.Forms.Label()
		Me.label3 = New System.Windows.Forms.Label()
		Me.label4 = New System.Windows.Forms.Label()
		Me.tableLayoutPanel1.SuspendLayout()
		CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' tableLayoutPanel1
		' 
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F))
		Me.tableLayoutPanel1.Controls.Add(Me.btnChangeDb, 0, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.btnNew, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.btnOpen, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnSettings, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.btnAbout, 0, 6)
		Me.tableLayoutPanel1.Controls.Add(Me.pbLogo, 1, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.label3, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.label4, 1, 4)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 7
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(550, 349)
		Me.tableLayoutPanel1.TabIndex = 0
		' 
		' btnChangeDb
		' 
		Me.btnChangeDb.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.btnChangeDb.Location = New System.Drawing.Point(3, 220)
		Me.btnChangeDb.Name = "btnChangeDb"
		Me.btnChangeDb.Size = New System.Drawing.Size(108, 34)
		Me.btnChangeDb.TabIndex = 9
		Me.btnChangeDb.Text = "&Change Database"
		Me.btnChangeDb.UseVisualStyleBackColor = True
'		Me.btnChangeDb.Click += New System.EventHandler(Me.BtnChangeDbClick);
		' 
		' btnNew
		' 
		Me.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.btnNew.Location = New System.Drawing.Point(3, 24)
		Me.btnNew.Name = "btnNew"
		Me.btnNew.Size = New System.Drawing.Size(108, 34)
		Me.btnNew.TabIndex = 0
		Me.btnNew.Text = "New Subject"
		Me.btnNew.UseVisualStyleBackColor = True
'		Me.btnNew.Click += New System.EventHandler(Me.BtnNewSubjectClick);
		' 
		' btnOpen
		' 
		Me.btnOpen.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.btnOpen.Location = New System.Drawing.Point(3, 111)
		Me.btnOpen.Name = "btnOpen"
		Me.btnOpen.Size = New System.Drawing.Size(108, 34)
		Me.btnOpen.TabIndex = 1
		Me.btnOpen.Text = "Open Subject"
		Me.btnOpen.UseVisualStyleBackColor = True
'		Me.btnOpen.Click += New System.EventHandler(Me.BtnOpenClick);
		' 
		' btnSettings
		' 
		Me.btnSettings.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.btnSettings.Location = New System.Drawing.Point(3, 177)
		Me.btnSettings.Name = "btnSettings"
		Me.btnSettings.Size = New System.Drawing.Size(108, 34)
		Me.btnSettings.TabIndex = 2
		Me.btnSettings.Text = "Settings"
		Me.btnSettings.UseVisualStyleBackColor = True
'		Me.btnSettings.Click += New System.EventHandler(Me.BtnSettingsClick);
		' 
		' btnAbout
		' 
		Me.btnAbout.Location = New System.Drawing.Point(3, 312)
		Me.btnAbout.Name = "btnAbout"
		Me.btnAbout.Size = New System.Drawing.Size(108, 34)
		Me.btnAbout.TabIndex = 4
		Me.btnAbout.Text = "About"
		Me.btnAbout.UseVisualStyleBackColor = True
'		Me.btnAbout.Click += New System.EventHandler(Me.BtnAboutClick);
		' 
		' pbLogo
		' 
		Me.pbLogo.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.pbLogo.Image = (CType(resources.GetObject("pbLogo.Image"), System.Drawing.Image))
		Me.pbLogo.Location = New System.Drawing.Point(293, 266)
		Me.pbLogo.Name = "pbLogo"
		Me.tableLayoutPanel1.SetRowSpan(Me.pbLogo, 2)
		Me.pbLogo.Size = New System.Drawing.Size(254, 80)
		Me.pbLogo.TabIndex = 5
		Me.pbLogo.TabStop = False
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label1.Location = New System.Drawing.Point(117, 0)
		Me.label1.Name = "label1"
		Me.label1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.label1.Size = New System.Drawing.Size(430, 83)
		Me.label1.TabIndex = 6
		Me.label1.Text = "Create new subject" & Constants.vbCrLf & "    Capture biometrics (fingers, faces, etc) from devices or " & "create them from files." & Constants.vbCrLf & "    Enroll, identify or verify subject using local data" & "base or remote matching server"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' label2
		' 
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label2.Location = New System.Drawing.Point(117, 93)
		Me.label2.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(430, 80)
		Me.label2.TabIndex = 7
		Me.label2.Text = "Open subject template" & Constants.vbCrLf & "    Open from Neurotechnology template or other supported " & "standard templates" & Constants.vbCrLf & "    Enroll, identify or verify subject using local database " & "or remote matching server" & Constants.vbCrLf
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' label3
		' 
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label3.Location = New System.Drawing.Point(117, 183)
		Me.label3.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(430, 32)
		Me.label3.TabIndex = 8
		Me.label3.Text = "Change settings" & Constants.vbCrLf & "    Change feature detection, extraction, matching (etc) setting" & "s"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' label4
		' 
		Me.label4.AutoSize = True
		Me.label4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.label4.Location = New System.Drawing.Point(117, 225)
		Me.label4.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(430, 34)
		Me.label4.TabIndex = 10
		Me.label4.Text = "Change db" & Constants.vbCrLf & "    Configure to use local database or remote matching server"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' StartTab
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.MinimumSize = New System.Drawing.Size(550, 0)
		Me.Name = "StartTab"
		Me.Size = New System.Drawing.Size(550, 349)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnNew As System.Windows.Forms.Button
	Private WithEvents btnOpen As System.Windows.Forms.Button
	Private WithEvents btnSettings As System.Windows.Forms.Button
	Private WithEvents btnAbout As System.Windows.Forms.Button
	Private pbLogo As System.Windows.Forms.PictureBox
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private WithEvents btnChangeDb As System.Windows.Forms.Button
	Private label4 As System.Windows.Forms.Label
End Class
