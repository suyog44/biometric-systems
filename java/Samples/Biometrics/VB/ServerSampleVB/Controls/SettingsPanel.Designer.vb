Imports Microsoft.VisualBasic
Imports System
Partial Public Class SettingsPanel
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
		Me.tabControl = New System.Windows.Forms.TabControl
		Me.tabGeneral = New System.Windows.Forms.TabPage
		Me.cbMatchingThreshold = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.tabFingers = New System.Windows.Forms.TabPage
		Me.label6 = New System.Windows.Forms.Label
		Me.label8 = New System.Windows.Forms.Label
		Me.nudFingersMaxRotation = New System.Windows.Forms.NumericUpDown
		Me.cbFingersMatchingSpeed = New System.Windows.Forms.ComboBox
		Me.tabFaces = New System.Windows.Forms.TabPage
		Me.label10 = New System.Windows.Forms.Label
		Me.cbFacesMatchingSpeed = New System.Windows.Forms.ComboBox
		Me.tabPalms = New System.Windows.Forms.TabPage
		Me.label18 = New System.Windows.Forms.Label
		Me.label19 = New System.Windows.Forms.Label
		Me.nudPalmsMaxRotation = New System.Windows.Forms.NumericUpDown
		Me.cbPalmsMatchingSpeed = New System.Windows.Forms.ComboBox
		Me.btnReset = New System.Windows.Forms.Button
		Me.btnApply = New System.Windows.Forms.Button
		Me.cbIrisesMatchingSpeed = New System.Windows.Forms.ComboBox
		Me.nudIrisesMaxRotation = New System.Windows.Forms.NumericUpDown
		Me.label15 = New System.Windows.Forms.Label
		Me.label13 = New System.Windows.Forms.Label
		Me.tabIrises = New System.Windows.Forms.TabPage
		Me.tabControl.SuspendLayout()
		Me.tabGeneral.SuspendLayout()
		Me.tabFingers.SuspendLayout()
		CType(Me.nudFingersMaxRotation, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tabFaces.SuspendLayout()
		Me.tabPalms.SuspendLayout()
		CType(Me.nudPalmsMaxRotation, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudIrisesMaxRotation, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tabIrises.SuspendLayout()
		Me.SuspendLayout()
		'
		'tabControl
		'
		Me.tabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tabControl.Controls.Add(Me.tabGeneral)
		Me.tabControl.Controls.Add(Me.tabFingers)
		Me.tabControl.Controls.Add(Me.tabFaces)
		Me.tabControl.Controls.Add(Me.tabIrises)
		Me.tabControl.Controls.Add(Me.tabPalms)
		Me.tabControl.Location = New System.Drawing.Point(4, 8)
		Me.tabControl.Name = "tabControl"
		Me.tabControl.SelectedIndex = 0
		Me.tabControl.Size = New System.Drawing.Size(522, 265)
		Me.tabControl.TabIndex = 3
		'
		'tabGeneral
		'
		Me.tabGeneral.Controls.Add(Me.cbMatchingThreshold)
		Me.tabGeneral.Controls.Add(Me.label1)
		Me.tabGeneral.Location = New System.Drawing.Point(4, 22)
		Me.tabGeneral.Name = "tabGeneral"
		Me.tabGeneral.Padding = New System.Windows.Forms.Padding(3)
		Me.tabGeneral.Size = New System.Drawing.Size(514, 239)
		Me.tabGeneral.TabIndex = 0
		Me.tabGeneral.Text = "General"
		Me.tabGeneral.UseVisualStyleBackColor = True
		'
		'cbMatchingThreshold
		'
		Me.cbMatchingThreshold.FormattingEnabled = True
		Me.cbMatchingThreshold.Location = New System.Drawing.Point(109, 9)
		Me.cbMatchingThreshold.Name = "cbMatchingThreshold"
		Me.cbMatchingThreshold.Size = New System.Drawing.Size(146, 21)
		Me.cbMatchingThreshold.TabIndex = 4
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(3, 12)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(100, 13)
		Me.label1.TabIndex = 6
		Me.label1.Text = "Matching threshold:"
		'
		'tabFingers
		'
		Me.tabFingers.Controls.Add(Me.label6)
		Me.tabFingers.Controls.Add(Me.label8)
		Me.tabFingers.Controls.Add(Me.nudFingersMaxRotation)
		Me.tabFingers.Controls.Add(Me.cbFingersMatchingSpeed)
		Me.tabFingers.Location = New System.Drawing.Point(4, 22)
		Me.tabFingers.Name = "tabFingers"
		Me.tabFingers.Padding = New System.Windows.Forms.Padding(3)
		Me.tabFingers.Size = New System.Drawing.Size(514, 239)
		Me.tabFingers.TabIndex = 1
		Me.tabFingers.Text = "Fingers"
		Me.tabFingers.UseVisualStyleBackColor = True
		'
		'label6
		'
		Me.label6.AutoSize = True
		Me.label6.Location = New System.Drawing.Point(6, 30)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(83, 13)
		Me.label6.TabIndex = 22
		Me.label6.Text = "Maximal rotation"
		'
		'label8
		'
		Me.label8.AutoSize = True
		Me.label8.Location = New System.Drawing.Point(6, 3)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(38, 13)
		Me.label8.TabIndex = 20
		Me.label8.Text = "Speed"
		'
		'nudFingersMaxRotation
		'
		Me.nudFingersMaxRotation.Location = New System.Drawing.Point(188, 28)
		Me.nudFingersMaxRotation.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
		Me.nudFingersMaxRotation.Name = "nudFingersMaxRotation"
		Me.nudFingersMaxRotation.Size = New System.Drawing.Size(89, 20)
		Me.nudFingersMaxRotation.TabIndex = 6
		'
		'cbFingersMatchingSpeed
		'
		Me.cbFingersMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbFingersMatchingSpeed.FormattingEnabled = True
		Me.cbFingersMatchingSpeed.Location = New System.Drawing.Point(188, 1)
		Me.cbFingersMatchingSpeed.Name = "cbFingersMatchingSpeed"
		Me.cbFingersMatchingSpeed.Size = New System.Drawing.Size(154, 21)
		Me.cbFingersMatchingSpeed.TabIndex = 4
		'
		'tabFaces
		'
		Me.tabFaces.Controls.Add(Me.label10)
		Me.tabFaces.Controls.Add(Me.cbFacesMatchingSpeed)
		Me.tabFaces.Location = New System.Drawing.Point(4, 22)
		Me.tabFaces.Name = "tabFaces"
		Me.tabFaces.Size = New System.Drawing.Size(514, 239)
		Me.tabFaces.TabIndex = 2
		Me.tabFaces.Text = "Faces"
		Me.tabFaces.UseVisualStyleBackColor = True
		'
		'label10
		'
		Me.label10.AutoSize = True
		Me.label10.Location = New System.Drawing.Point(9, 15)
		Me.label10.Name = "label10"
		Me.label10.Size = New System.Drawing.Size(38, 13)
		Me.label10.TabIndex = 8
		Me.label10.Text = "Speed"
		'
		'cbFacesMatchingSpeed
		'
		Me.cbFacesMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbFacesMatchingSpeed.FormattingEnabled = True
		Me.cbFacesMatchingSpeed.Location = New System.Drawing.Point(70, 12)
		Me.cbFacesMatchingSpeed.Name = "cbFacesMatchingSpeed"
		Me.cbFacesMatchingSpeed.Size = New System.Drawing.Size(173, 21)
		Me.cbFacesMatchingSpeed.TabIndex = 4
		'
		'tabPalms
		'
		Me.tabPalms.Controls.Add(Me.label18)
		Me.tabPalms.Controls.Add(Me.label19)
		Me.tabPalms.Controls.Add(Me.nudPalmsMaxRotation)
		Me.tabPalms.Controls.Add(Me.cbPalmsMatchingSpeed)
		Me.tabPalms.Location = New System.Drawing.Point(4, 22)
		Me.tabPalms.Name = "tabPalms"
		Me.tabPalms.Size = New System.Drawing.Size(514, 239)
		Me.tabPalms.TabIndex = 4
		Me.tabPalms.Text = "Palms"
		Me.tabPalms.UseVisualStyleBackColor = True
		'
		'label18
		'
		Me.label18.AutoSize = True
		Me.label18.Location = New System.Drawing.Point(3, 37)
		Me.label18.Name = "label18"
		Me.label18.Size = New System.Drawing.Size(83, 13)
		Me.label18.TabIndex = 28
		Me.label18.Text = "Maximal rotation"
		'
		'label19
		'
		Me.label19.AutoSize = True
		Me.label19.Location = New System.Drawing.Point(3, 10)
		Me.label19.Name = "label19"
		Me.label19.Size = New System.Drawing.Size(38, 13)
		Me.label19.TabIndex = 27
		Me.label19.Text = "Speed"
		'
		'nudPalmsMaxRotation
		'
		Me.nudPalmsMaxRotation.Location = New System.Drawing.Point(185, 35)
		Me.nudPalmsMaxRotation.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
		Me.nudPalmsMaxRotation.Name = "nudPalmsMaxRotation"
		Me.nudPalmsMaxRotation.Size = New System.Drawing.Size(89, 20)
		Me.nudPalmsMaxRotation.TabIndex = 5
		'
		'cbPalmsMatchingSpeed
		'
		Me.cbPalmsMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbPalmsMatchingSpeed.FormattingEnabled = True
		Me.cbPalmsMatchingSpeed.Location = New System.Drawing.Point(185, 8)
		Me.cbPalmsMatchingSpeed.Name = "cbPalmsMatchingSpeed"
		Me.cbPalmsMatchingSpeed.Size = New System.Drawing.Size(188, 21)
		Me.cbPalmsMatchingSpeed.TabIndex = 4
		'
		'btnReset
		'
		Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnReset.Location = New System.Drawing.Point(4, 278)
		Me.btnReset.Name = "btnReset"
		Me.btnReset.Size = New System.Drawing.Size(75, 23)
		Me.btnReset.TabIndex = 1
		Me.btnReset.Text = "Reset"
		Me.btnReset.UseVisualStyleBackColor = True
		'
		'btnApply
		'
		Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnApply.Location = New System.Drawing.Point(85, 278)
		Me.btnApply.Name = "btnApply"
		Me.btnApply.Size = New System.Drawing.Size(75, 23)
		Me.btnApply.TabIndex = 2
		Me.btnApply.Text = "Apply"
		Me.btnApply.UseVisualStyleBackColor = True
		'
		'cbIrisesMatchingSpeed
		'
		Me.cbIrisesMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbIrisesMatchingSpeed.FormattingEnabled = True
		Me.cbIrisesMatchingSpeed.Location = New System.Drawing.Point(182, 7)
		Me.cbIrisesMatchingSpeed.Name = "cbIrisesMatchingSpeed"
		Me.cbIrisesMatchingSpeed.Size = New System.Drawing.Size(170, 21)
		Me.cbIrisesMatchingSpeed.TabIndex = 4
		'
		'nudIrisesMaxRotation
		'
		Me.nudIrisesMaxRotation.Location = New System.Drawing.Point(182, 34)
		Me.nudIrisesMaxRotation.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
		Me.nudIrisesMaxRotation.Name = "nudIrisesMaxRotation"
		Me.nudIrisesMaxRotation.Size = New System.Drawing.Size(100, 20)
		Me.nudIrisesMaxRotation.TabIndex = 6
		'
		'label15
		'
		Me.label15.AutoSize = True
		Me.label15.Location = New System.Drawing.Point(3, 10)
		Me.label15.Name = "label15"
		Me.label15.Size = New System.Drawing.Size(38, 13)
		Me.label15.TabIndex = 20
		Me.label15.Text = "Speed"
		'
		'label13
		'
		Me.label13.AutoSize = True
		Me.label13.Location = New System.Drawing.Point(3, 36)
		Me.label13.Name = "label13"
		Me.label13.Size = New System.Drawing.Size(83, 13)
		Me.label13.TabIndex = 22
		Me.label13.Text = "Maximal rotation"
		'
		'tabIrises
		'
		Me.tabIrises.Controls.Add(Me.label13)
		Me.tabIrises.Controls.Add(Me.label15)
		Me.tabIrises.Controls.Add(Me.nudIrisesMaxRotation)
		Me.tabIrises.Controls.Add(Me.cbIrisesMatchingSpeed)
		Me.tabIrises.Location = New System.Drawing.Point(4, 22)
		Me.tabIrises.Name = "tabIrises"
		Me.tabIrises.Size = New System.Drawing.Size(514, 239)
		Me.tabIrises.TabIndex = 3
		Me.tabIrises.Text = "Irises"
		Me.tabIrises.UseVisualStyleBackColor = True
		'
		'SettingsPanel
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.btnApply)
		Me.Controls.Add(Me.btnReset)
		Me.Controls.Add(Me.tabControl)
		Me.Name = "SettingsPanel"
		Me.Size = New System.Drawing.Size(526, 305)
		Me.tabControl.ResumeLayout(False)
		Me.tabGeneral.ResumeLayout(False)
		Me.tabGeneral.PerformLayout()
		Me.tabFingers.ResumeLayout(False)
		Me.tabFingers.PerformLayout()
		CType(Me.nudFingersMaxRotation, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tabFaces.ResumeLayout(False)
		Me.tabFaces.PerformLayout()
		Me.tabPalms.ResumeLayout(False)
		Me.tabPalms.PerformLayout()
		CType(Me.nudPalmsMaxRotation, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudIrisesMaxRotation, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tabIrises.ResumeLayout(False)
		Me.tabIrises.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tabControl As System.Windows.Forms.TabControl
	Private tabGeneral As System.Windows.Forms.TabPage
	Private tabFingers As System.Windows.Forms.TabPage
	Private tabFaces As System.Windows.Forms.TabPage
	Private tabPalms As System.Windows.Forms.TabPage
	Private WithEvents cbMatchingThreshold As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
	Private label6 As System.Windows.Forms.Label
	Private label8 As System.Windows.Forms.Label
	Private nudFingersMaxRotation As System.Windows.Forms.NumericUpDown
	Private cbFingersMatchingSpeed As System.Windows.Forms.ComboBox
	Private label10 As System.Windows.Forms.Label
	Private cbFacesMatchingSpeed As System.Windows.Forms.ComboBox
	Private label18 As System.Windows.Forms.Label
	Private label19 As System.Windows.Forms.Label
	Private nudPalmsMaxRotation As System.Windows.Forms.NumericUpDown
	Private cbPalmsMatchingSpeed As System.Windows.Forms.ComboBox
	Private WithEvents btnReset As System.Windows.Forms.Button
	Private WithEvents btnApply As System.Windows.Forms.Button
	Private WithEvents tabIrises As System.Windows.Forms.TabPage
	Private WithEvents label13 As System.Windows.Forms.Label
	Private WithEvents label15 As System.Windows.Forms.Label
	Private WithEvents nudIrisesMaxRotation As System.Windows.Forms.NumericUpDown
	Private WithEvents cbIrisesMatchingSpeed As System.Windows.Forms.ComboBox
End Class
