Imports Microsoft.VisualBasic
Imports System
Partial Public Class ResolutionForm
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(ResolutionForm))
		Me.panel1 = New System.Windows.Forms.Panel()
		Me.panel2 = New System.Windows.Forms.Panel()
		Me.pbFingerprint = New System.Windows.Forms.PictureBox()
		Me.nudVertResolution = New System.Windows.Forms.NumericUpDown()
		Me.nudHorzResolution = New System.Windows.Forms.NumericUpDown()
		Me.label1 = New System.Windows.Forms.Label()
		Me.label2 = New System.Windows.Forms.Label()
		Me.groupBox1 = New System.Windows.Forms.GroupBox()
		Me.groupBox2 = New System.Windows.Forms.GroupBox()
		Me.rbCentimeters = New System.Windows.Forms.RadioButton()
		Me.rbInches = New System.Windows.Forms.RadioButton()
		Me.pictureBox2 = New System.Windows.Forms.PictureBox()
		Me.nudUnitScale = New System.Windows.Forms.NumericUpDown()
		Me.label3 = New System.Windows.Forms.Label()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.btnOK = New System.Windows.Forms.Button()
		Me.errorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.panel1.SuspendLayout()
		Me.panel2.SuspendLayout()
		CType(Me.pbFingerprint, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudVertResolution, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudHorzResolution, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.groupBox1.SuspendLayout()
		Me.groupBox2.SuspendLayout()
		CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudUnitScale, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' panel1
		' 
		Me.panel1.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.panel1.AutoSize = True
		Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.panel1.Controls.Add(Me.panel2)
		Me.panel1.Location = New System.Drawing.Point(12, 12)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(541, 224)
		Me.panel1.TabIndex = 1
		' 
		' panel2
		' 
		Me.panel2.AutoScroll = True
		Me.panel2.Controls.Add(Me.pbFingerprint)
		Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel2.Location = New System.Drawing.Point(0, 0)
		Me.panel2.Name = "panel2"
		Me.panel2.Size = New System.Drawing.Size(537, 220)
		Me.panel2.TabIndex = 1
		' 
		' pbFingerprint
		' 
		Me.pbFingerprint.Cursor = System.Windows.Forms.Cursors.Cross
		Me.pbFingerprint.Location = New System.Drawing.Point(0, 0)
		Me.pbFingerprint.Name = "pbFingerprint"
		Me.pbFingerprint.Size = New System.Drawing.Size(196, 184)
		Me.pbFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.pbFingerprint.TabIndex = 0
		Me.pbFingerprint.TabStop = False
'		Me.pbFingerprint.MouseMove += New System.Windows.Forms.MouseEventHandler(Me.pbFingerprint_MouseMove);
'		Me.pbFingerprint.MouseDown += New System.Windows.Forms.MouseEventHandler(Me.pbFingerprint_MouseDown);
'		Me.pbFingerprint.Paint += New System.Windows.Forms.PaintEventHandler(Me.pbFingerprint_Paint);
'		Me.pbFingerprint.MouseUp += New System.Windows.Forms.MouseEventHandler(Me.pbFingerprint_MouseUp);
		' 
		' nudVertResolution
		' 
		Me.nudVertResolution.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.nudVertResolution.DecimalPlaces = 2
		Me.nudVertResolution.Location = New System.Drawing.Point(133, 45)
		Me.nudVertResolution.Maximum = New Decimal(New Integer() { 3000, 0, 0, 0})
		Me.nudVertResolution.Minimum = New Decimal(New Integer() { 50, 0, 0, 0})
		Me.nudVertResolution.Name = "nudVertResolution"
		Me.nudVertResolution.Size = New System.Drawing.Size(98, 20)
		Me.nudVertResolution.TabIndex = 3
		Me.nudVertResolution.Value = New Decimal(New Integer() { 500, 0, 0, 0})
'		Me.nudVertResolution.ValueChanged += New System.EventHandler(Me.nudVertResolution_ValueChanged);
		' 
		' nudHorzResolution
		' 
		Me.nudHorzResolution.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.nudHorzResolution.DecimalPlaces = 2
		Me.nudHorzResolution.Location = New System.Drawing.Point(133, 19)
		Me.nudHorzResolution.Maximum = New Decimal(New Integer() { 3000, 0, 0, 0})
		Me.nudHorzResolution.Minimum = New Decimal(New Integer() { 50, 0, 0, 0})
		Me.nudHorzResolution.Name = "nudHorzResolution"
		Me.nudHorzResolution.Size = New System.Drawing.Size(98, 20)
		Me.nudHorzResolution.TabIndex = 1
		Me.nudHorzResolution.Value = New Decimal(New Integer() { 500, 0, 0, 0})
'		Me.nudHorzResolution.ValueChanged += New System.EventHandler(Me.nudHorzResolution_ValueChanged);
		' 
		' label1
		' 
		Me.label1.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(18, 21)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(105, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Horizontal resolution:"
		' 
		' label2
		' 
		Me.label2.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(30, 47)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(93, 13)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Vertical resolution:"
		' 
		' groupBox1
		' 
		Me.groupBox1.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Controls.Add(Me.label2)
		Me.groupBox1.Controls.Add(Me.nudVertResolution)
		Me.groupBox1.Controls.Add(Me.nudHorzResolution)
		Me.groupBox1.Location = New System.Drawing.Point(294, 242)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(259, 79)
		Me.groupBox1.TabIndex = 3
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Resolution"
		' 
		' groupBox2
		' 
		Me.groupBox2.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.groupBox2.Controls.Add(Me.rbCentimeters)
		Me.groupBox2.Controls.Add(Me.rbInches)
		Me.groupBox2.Controls.Add(Me.pictureBox2)
		Me.groupBox2.Controls.Add(Me.nudUnitScale)
		Me.groupBox2.Controls.Add(Me.label3)
		Me.groupBox2.Location = New System.Drawing.Point(12, 242)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(276, 79)
		Me.groupBox2.TabIndex = 2
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Measure resolution tool"
		' 
		' rbCentimeters
		' 
		Me.rbCentimeters.AutoSize = True
		Me.rbCentimeters.Location = New System.Drawing.Point(143, 48)
		Me.rbCentimeters.Name = "rbCentimeters"
		Me.rbCentimeters.Size = New System.Drawing.Size(86, 17)
		Me.rbCentimeters.TabIndex = 5
		Me.rbCentimeters.Text = "Centimeter(s)"
		Me.rbCentimeters.UseVisualStyleBackColor = True
		' 
		' rbInches
		' 
		Me.rbInches.AutoSize = True
		Me.rbInches.Checked = True
		Me.rbInches.Location = New System.Drawing.Point(143, 32)
		Me.rbInches.Name = "rbInches"
		Me.rbInches.Size = New System.Drawing.Size(63, 17)
		Me.rbInches.TabIndex = 4
		Me.rbInches.TabStop = True
		Me.rbInches.Text = "Inch(es)"
		Me.rbInches.UseVisualStyleBackColor = True
		' 
		' pictureBox2
		' 
		Me.pictureBox2.Image = My.Resources.measureDpiTool
		Me.pictureBox2.Location = New System.Drawing.Point(9, 32)
		Me.pictureBox2.Name = "pictureBox2"
		Me.pictureBox2.Size = New System.Drawing.Size(32, 33)
		Me.pictureBox2.TabIndex = 3
		Me.pictureBox2.TabStop = False
		' 
		' nudUnitScale
		' 
		Me.nudUnitScale.Location = New System.Drawing.Point(47, 40)
		Me.nudUnitScale.Minimum = New Decimal(New Integer() { 1, 0, 0, 0})
		Me.nudUnitScale.Name = "nudUnitScale"
		Me.nudUnitScale.Size = New System.Drawing.Size(80, 20)
		Me.nudUnitScale.TabIndex = 1
		Me.nudUnitScale.Value = New Decimal(New Integer() { 1, 0, 0, 0})
		' 
		' label3
		' 
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(6, 16)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(210, 13)
		Me.label3.TabIndex = 0
		Me.label3.Text = "Draw a line on the image which represents:"
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(478, 335)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 4
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' btnOK
		' 
		Me.btnOK.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOK.Location = New System.Drawing.Point(397, 335)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.Size = New System.Drawing.Size(75, 23)
		Me.btnOK.TabIndex = 4
		Me.btnOK.Text = "OK"
		Me.btnOK.UseVisualStyleBackColor = True
		' 
		' errorProvider1
		' 
		Me.errorProvider1.ContainerControl = Me
		' 
		' ResolutionForm
		' 
		Me.AcceptButton = Me.btnOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(565, 370)
		Me.Controls.Add(Me.btnOK)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.panel1)
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.MinimumSize = New System.Drawing.Size(573, 404)
		Me.Name = "ResolutionForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Resolution"
		Me.panel1.ResumeLayout(False)
		Me.panel2.ResumeLayout(False)
		Me.panel2.PerformLayout()
		CType(Me.pbFingerprint, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudVertResolution, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudHorzResolution, System.ComponentModel.ISupportInitialize).EndInit()
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.groupBox2.ResumeLayout(False)
		Me.groupBox2.PerformLayout()
		CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudUnitScale, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private WithEvents pbFingerprint As System.Windows.Forms.PictureBox
	Private panel1 As System.Windows.Forms.Panel
	Private WithEvents nudVertResolution As System.Windows.Forms.NumericUpDown
	Private WithEvents nudHorzResolution As System.Windows.Forms.NumericUpDown
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private nudUnitScale As System.Windows.Forms.NumericUpDown
	Private label3 As System.Windows.Forms.Label
	Private btnCancel As System.Windows.Forms.Button
	Private btnOK As System.Windows.Forms.Button
	Private pictureBox2 As System.Windows.Forms.PictureBox
	Private panel2 As System.Windows.Forms.Panel
	Private errorProvider1 As System.Windows.Forms.ErrorProvider
	Private rbCentimeters As System.Windows.Forms.RadioButton
	Private rbInches As System.Windows.Forms.RadioButton
End Class