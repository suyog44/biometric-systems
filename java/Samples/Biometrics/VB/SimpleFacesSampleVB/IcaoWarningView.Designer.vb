Imports Microsoft.VisualBasic
Imports System
Partial Public Class IcaoWarningView
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
		Me.lblFaceDetected = New System.Windows.Forms.Label()
		Me.lblExpression = New System.Windows.Forms.Label()
		Me.lblDarkGlasses = New System.Windows.Forms.Label()
		Me.lblBlink = New System.Windows.Forms.Label()
		Me.lblMouthOpen = New System.Windows.Forms.Label()
		Me.lblRoll = New System.Windows.Forms.Label()
		Me.lblYaw = New System.Windows.Forms.Label()
		Me.lblPitch = New System.Windows.Forms.Label()
		Me.lblTooClose = New System.Windows.Forms.Label()
		Me.lblTooFar = New System.Windows.Forms.Label()
		Me.lblTooNorth = New System.Windows.Forms.Label()
		Me.lblTooSouth = New System.Windows.Forms.Label()
		Me.lblTooWest = New System.Windows.Forms.Label()
		Me.lblTooEast = New System.Windows.Forms.Label()
		Me.lblSharpness = New System.Windows.Forms.Label()
		Me.lblGrayscaleDensity = New System.Windows.Forms.Label()
		Me.lblSaturation = New System.Windows.Forms.Label()
		Me.lblBackgroundUniformity = New System.Windows.Forms.Label()
		Me.flowLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' flowLayoutPanel1
		' 
		Me.flowLayoutPanel1.AutoSize = True
		Me.flowLayoutPanel1.Controls.Add(Me.lblFaceDetected)
		Me.flowLayoutPanel1.Controls.Add(Me.lblExpression)
		Me.flowLayoutPanel1.Controls.Add(Me.lblDarkGlasses)
		Me.flowLayoutPanel1.Controls.Add(Me.lblBlink)
		Me.flowLayoutPanel1.Controls.Add(Me.lblMouthOpen)
		Me.flowLayoutPanel1.Controls.Add(Me.lblRoll)
		Me.flowLayoutPanel1.Controls.Add(Me.lblYaw)
		Me.flowLayoutPanel1.Controls.Add(Me.lblPitch)
		Me.flowLayoutPanel1.Controls.Add(Me.lblTooClose)
		Me.flowLayoutPanel1.Controls.Add(Me.lblTooFar)
		Me.flowLayoutPanel1.Controls.Add(Me.lblTooNorth)
		Me.flowLayoutPanel1.Controls.Add(Me.lblTooSouth)
		Me.flowLayoutPanel1.Controls.Add(Me.lblTooWest)
		Me.flowLayoutPanel1.Controls.Add(Me.lblTooEast)
		Me.flowLayoutPanel1.Controls.Add(Me.lblSharpness)
		Me.flowLayoutPanel1.Controls.Add(Me.lblGrayscaleDensity)
		Me.flowLayoutPanel1.Controls.Add(Me.lblSaturation)
		Me.flowLayoutPanel1.Controls.Add(Me.lblBackgroundUniformity)
		Me.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
		Me.flowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
		Me.flowLayoutPanel1.Size = New System.Drawing.Size(135, 246)
		Me.flowLayoutPanel1.TabIndex = 15
		' 
		' lblFaceDetected
		' 
		Me.lblFaceDetected.AutoSize = True
		Me.lblFaceDetected.Location = New System.Drawing.Point(3, 0)
		Me.lblFaceDetected.Name = "lblFaceDetected"
		Me.lblFaceDetected.Size = New System.Drawing.Size(76, 13)
		Me.lblFaceDetected.TabIndex = 0
		Me.lblFaceDetected.Text = "Face detected"
		' 
		' lblExpression
		' 
		Me.lblExpression.AutoSize = True
		Me.lblExpression.Location = New System.Drawing.Point(3, 13)
		Me.lblExpression.Name = "lblExpression"
		Me.lblExpression.Size = New System.Drawing.Size(58, 13)
		Me.lblExpression.TabIndex = 1
		Me.lblExpression.Text = "Expression"
		' 
		' lblDarkGlasses
		' 
		Me.lblDarkGlasses.AutoSize = True
		Me.lblDarkGlasses.Location = New System.Drawing.Point(3, 26)
		Me.lblDarkGlasses.Name = "lblDarkGlasses"
		Me.lblDarkGlasses.Size = New System.Drawing.Size(70, 13)
		Me.lblDarkGlasses.TabIndex = 2
		Me.lblDarkGlasses.Text = "Dark Glasses"
		' 
		' lblBlink
		' 
		Me.lblBlink.AutoSize = True
		Me.lblBlink.Location = New System.Drawing.Point(3, 39)
		Me.lblBlink.Name = "lblBlink"
		Me.lblBlink.Size = New System.Drawing.Size(30, 13)
		Me.lblBlink.TabIndex = 3
		Me.lblBlink.Text = "Blink"
		' 
		' lblMouthOpen
		' 
		Me.lblMouthOpen.AutoSize = True
		Me.lblMouthOpen.Location = New System.Drawing.Point(3, 52)
		Me.lblMouthOpen.Name = "lblMouthOpen"
		Me.lblMouthOpen.Size = New System.Drawing.Size(66, 13)
		Me.lblMouthOpen.TabIndex = 4
		Me.lblMouthOpen.Text = "Mouth Open"
		' 
		' lblRoll
		' 
		Me.lblRoll.AutoSize = True
		Me.lblRoll.Location = New System.Drawing.Point(3, 65)
		Me.lblRoll.Name = "lblRoll"
		Me.lblRoll.Size = New System.Drawing.Size(25, 13)
		Me.lblRoll.TabIndex = 5
		Me.lblRoll.Text = "Roll"
		' 
		' lblYaw
		' 
		Me.lblYaw.AutoSize = True
		Me.lblYaw.Location = New System.Drawing.Point(3, 78)
		Me.lblYaw.Name = "lblYaw"
		Me.lblYaw.Size = New System.Drawing.Size(28, 13)
		Me.lblYaw.TabIndex = 6
		Me.lblYaw.Text = "Yaw"
		' 
		' lblPitch
		' 
		Me.lblPitch.AutoSize = True
		Me.lblPitch.Location = New System.Drawing.Point(3, 91)
		Me.lblPitch.Name = "lblPitch"
		Me.lblPitch.Size = New System.Drawing.Size(31, 13)
		Me.lblPitch.TabIndex = 7
		Me.lblPitch.Text = "Pitch"
		' 
		' lblTooClose
		' 
		Me.lblTooClose.AutoSize = True
		Me.lblTooClose.Location = New System.Drawing.Point(3, 104)
		Me.lblTooClose.Name = "lblTooClose"
		Me.lblTooClose.Size = New System.Drawing.Size(55, 13)
		Me.lblTooClose.TabIndex = 8
		Me.lblTooClose.Text = "Too Close"
		' 
		' lblTooFar
		' 
		Me.lblTooFar.AutoSize = True
		Me.lblTooFar.Location = New System.Drawing.Point(3, 117)
		Me.lblTooFar.Name = "lblTooFar"
		Me.lblTooFar.Size = New System.Drawing.Size(44, 13)
		Me.lblTooFar.TabIndex = 9
		Me.lblTooFar.Text = "Too Far"
		' 
		' lblTooNorth
		' 
		Me.lblTooNorth.AutoSize = True
		Me.lblTooNorth.Location = New System.Drawing.Point(3, 130)
		Me.lblTooNorth.Name = "lblTooNorth"
		Me.lblTooNorth.Size = New System.Drawing.Size(55, 13)
		Me.lblTooNorth.TabIndex = 10
		Me.lblTooNorth.Text = "Too North"
		' 
		' lblTooSouth
		' 
		Me.lblTooSouth.AutoSize = True
		Me.lblTooSouth.Location = New System.Drawing.Point(3, 143)
		Me.lblTooSouth.Name = "lblTooSouth"
		Me.lblTooSouth.Size = New System.Drawing.Size(57, 13)
		Me.lblTooSouth.TabIndex = 11
		Me.lblTooSouth.Text = "Too South"
		' 
		' lblTooWest
		' 
		Me.lblTooWest.AutoSize = True
		Me.lblTooWest.Location = New System.Drawing.Point(3, 156)
		Me.lblTooWest.Name = "lblTooWest"
		Me.lblTooWest.Size = New System.Drawing.Size(54, 13)
		Me.lblTooWest.TabIndex = 12
		Me.lblTooWest.Text = "Too West"
		' 
		' lblTooEast
		' 
		Me.lblTooEast.AutoSize = True
		Me.lblTooEast.Location = New System.Drawing.Point(3, 169)
		Me.lblTooEast.Name = "lblTooEast"
		Me.lblTooEast.Size = New System.Drawing.Size(50, 13)
		Me.lblTooEast.TabIndex = 13
		Me.lblTooEast.Text = "Too East"
		' 
		' lblSharpness
		' 
		Me.lblSharpness.AutoSize = True
		Me.lblSharpness.Location = New System.Drawing.Point(3, 182)
		Me.lblSharpness.Name = "lblSharpness"
		Me.lblSharpness.Size = New System.Drawing.Size(57, 13)
		Me.lblSharpness.TabIndex = 14
		Me.lblSharpness.Text = "Sharpness"
		' 
		' lblGrayscaleDensity
		' 
		Me.lblGrayscaleDensity.AutoSize = True
		Me.lblGrayscaleDensity.Location = New System.Drawing.Point(3, 195)
		Me.lblGrayscaleDensity.Name = "lblGrayscaleDensity"
		Me.lblGrayscaleDensity.Size = New System.Drawing.Size(92, 13)
		Me.lblGrayscaleDensity.TabIndex = 15
		Me.lblGrayscaleDensity.Text = "Grayscale Density"
		' 
		' lblSaturation
		' 
		Me.lblSaturation.AutoSize = True
		Me.lblSaturation.Location = New System.Drawing.Point(3, 208)
		Me.lblSaturation.Name = "lblSaturation"
		Me.lblSaturation.Size = New System.Drawing.Size(55, 13)
		Me.lblSaturation.TabIndex = 16
		Me.lblSaturation.Text = "Saturation"
		' 
		' lblBackgroundUniformity
		' 
		Me.lblBackgroundUniformity.AutoSize = True
		Me.lblBackgroundUniformity.Location = New System.Drawing.Point(3, 221)
		Me.lblBackgroundUniformity.Name = "lblBackgroundUniformity"
		Me.lblBackgroundUniformity.Size = New System.Drawing.Size(112, 13)
		Me.lblBackgroundUniformity.TabIndex = 17
		Me.lblBackgroundUniformity.Text = "Background uniformity"
		' 
		' IcaoWarningView
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoSize = True
		Me.Controls.Add(Me.flowLayoutPanel1)
		Me.Name = "IcaoWarningView"
		Me.Size = New System.Drawing.Size(135, 246)
		Me.flowLayoutPanel1.ResumeLayout(False)
		Me.flowLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private flowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
	Private lblFaceDetected As System.Windows.Forms.Label
	Private lblExpression As System.Windows.Forms.Label
	Private lblDarkGlasses As System.Windows.Forms.Label
	Private lblBlink As System.Windows.Forms.Label
	Private lblMouthOpen As System.Windows.Forms.Label
	Private lblRoll As System.Windows.Forms.Label
	Private lblYaw As System.Windows.Forms.Label
	Private lblPitch As System.Windows.Forms.Label
	Private lblTooClose As System.Windows.Forms.Label
	Private lblTooFar As System.Windows.Forms.Label
	Private lblTooNorth As System.Windows.Forms.Label
	Private lblTooSouth As System.Windows.Forms.Label
	Private lblTooWest As System.Windows.Forms.Label
	Private lblTooEast As System.Windows.Forms.Label
	Private lblSharpness As System.Windows.Forms.Label
	Private lblGrayscaleDensity As System.Windows.Forms.Label
	Private lblSaturation As System.Windows.Forms.Label
	Private lblBackgroundUniformity As System.Windows.Forms.Label
End Class
