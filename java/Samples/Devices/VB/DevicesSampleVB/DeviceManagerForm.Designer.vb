Imports Microsoft.VisualBasic
Imports System

Partial Public Class DeviceManagerForm
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(DeviceManagerForm))
		Me.cbAutoplug = New System.Windows.Forms.CheckBox()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.btnAccept = New System.Windows.Forms.Button()
		Me.deviceTypesGroupBox = New System.Windows.Forms.GroupBox()
		Me.captureDeviceCheckBox = New System.Windows.Forms.CheckBox()
		Me.microphoneCheckBox = New System.Windows.Forms.CheckBox()
		Me.anyCheckBox = New System.Windows.Forms.CheckBox()
		Me.fScannerCheckBox = New System.Windows.Forms.CheckBox()
		Me.cameraCheckBox = New System.Windows.Forms.CheckBox()
		Me.irisScannerCheckBox = New System.Windows.Forms.CheckBox()
		Me.palmScannerCheckBox = New System.Windows.Forms.CheckBox()
		Me.fingerScannerCheckBox = New System.Windows.Forms.CheckBox()
		Me.biometricDeviceCheckBox = New System.Windows.Forms.CheckBox()
		Me.deviceTypesGroupBox.SuspendLayout()
		Me.SuspendLayout()
		' 
		' cbAutoplug
		' 
		Me.cbAutoplug.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.cbAutoplug.AutoSize = True
		Me.cbAutoplug.Checked = True
		Me.cbAutoplug.CheckState = System.Windows.Forms.CheckState.Checked
		Me.cbAutoplug.Location = New System.Drawing.Point(9, 246)
		Me.cbAutoplug.Name = "cbAutoplug"
		Me.cbAutoplug.Size = New System.Drawing.Size(71, 17)
		Me.cbAutoplug.TabIndex = 1
		Me.cbAutoplug.Text = "&Auto plug"
		Me.cbAutoplug.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(184, 271)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 4
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' btnAccept
		' 
		Me.btnAccept.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnAccept.Location = New System.Drawing.Point(103, 271)
		Me.btnAccept.Name = "btnAccept"
		Me.btnAccept.Size = New System.Drawing.Size(75, 23)
		Me.btnAccept.TabIndex = 3
		Me.btnAccept.Text = "&OK"
		Me.btnAccept.UseVisualStyleBackColor = True
		'			Me.btnAccept.Click += New System.EventHandler(Me.btnAccept_Click);
		' 
		' deviceTypesGroupBox
		' 
		Me.deviceTypesGroupBox.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.deviceTypesGroupBox.Controls.Add(Me.captureDeviceCheckBox)
		Me.deviceTypesGroupBox.Controls.Add(Me.microphoneCheckBox)
		Me.deviceTypesGroupBox.Controls.Add(Me.anyCheckBox)
		Me.deviceTypesGroupBox.Controls.Add(Me.fScannerCheckBox)
		Me.deviceTypesGroupBox.Controls.Add(Me.cameraCheckBox)
		Me.deviceTypesGroupBox.Controls.Add(Me.irisScannerCheckBox)
		Me.deviceTypesGroupBox.Controls.Add(Me.palmScannerCheckBox)
		Me.deviceTypesGroupBox.Controls.Add(Me.fingerScannerCheckBox)
		Me.deviceTypesGroupBox.Controls.Add(Me.biometricDeviceCheckBox)
		Me.deviceTypesGroupBox.Location = New System.Drawing.Point(12, 12)
		Me.deviceTypesGroupBox.Name = "deviceTypesGroupBox"
		Me.deviceTypesGroupBox.Size = New System.Drawing.Size(250, 228)
		Me.deviceTypesGroupBox.TabIndex = 0
		Me.deviceTypesGroupBox.TabStop = False
		Me.deviceTypesGroupBox.Text = "Device types"
		' 
		' captureDeviceCheckBox
		' 
		Me.captureDeviceCheckBox.AutoCheck = False
		Me.captureDeviceCheckBox.AutoSize = True
		Me.captureDeviceCheckBox.Location = New System.Drawing.Point(22, 42)
		Me.captureDeviceCheckBox.Name = "captureDeviceCheckBox"
		Me.captureDeviceCheckBox.Size = New System.Drawing.Size(100, 17)
		Me.captureDeviceCheckBox.TabIndex = 8
		Me.captureDeviceCheckBox.Text = "Capture Device"
		Me.captureDeviceCheckBox.UseVisualStyleBackColor = True
		'			Me.captureDeviceCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' microphoneCheckBox
		' 
		Me.microphoneCheckBox.AutoCheck = False
		Me.microphoneCheckBox.AutoSize = True
		Me.microphoneCheckBox.Location = New System.Drawing.Point(40, 89)
		Me.microphoneCheckBox.Name = "microphoneCheckBox"
		Me.microphoneCheckBox.Size = New System.Drawing.Size(82, 17)
		Me.microphoneCheckBox.TabIndex = 7
		Me.microphoneCheckBox.Text = "Microphone"
		Me.microphoneCheckBox.UseVisualStyleBackColor = True
		'			Me.microphoneCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' anyCheckBox
		' 
		Me.anyCheckBox.AutoCheck = False
		Me.anyCheckBox.AutoSize = True
		Me.anyCheckBox.Location = New System.Drawing.Point(6, 19)
		Me.anyCheckBox.Name = "anyCheckBox"
		Me.anyCheckBox.Size = New System.Drawing.Size(44, 17)
		Me.anyCheckBox.TabIndex = 0
		Me.anyCheckBox.Text = "Any"
		Me.anyCheckBox.ThreeState = True
		Me.anyCheckBox.UseVisualStyleBackColor = True
		'			Me.anyCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' fScannerCheckBox
		' 
		Me.fScannerCheckBox.AutoCheck = False
		Me.fScannerCheckBox.AutoSize = True
		Me.fScannerCheckBox.Location = New System.Drawing.Point(40, 135)
		Me.fScannerCheckBox.Name = "fScannerCheckBox"
		Me.fScannerCheckBox.Size = New System.Drawing.Size(73, 17)
		Me.fScannerCheckBox.TabIndex = 3
		Me.fScannerCheckBox.Text = "F scanner"
		Me.fScannerCheckBox.UseVisualStyleBackColor = True
		'			Me.fScannerCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' cameraCheckBox
		' 
		Me.cameraCheckBox.AutoCheck = False
		Me.cameraCheckBox.AutoSize = True
		Me.cameraCheckBox.Location = New System.Drawing.Point(40, 65)
		Me.cameraCheckBox.Name = "cameraCheckBox"
		Me.cameraCheckBox.Size = New System.Drawing.Size(62, 17)
		Me.cameraCheckBox.TabIndex = 1
		Me.cameraCheckBox.Text = "Camera"
		Me.cameraCheckBox.UseVisualStyleBackColor = True
		'			Me.cameraCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' irisScannerCheckBox
		' 
		Me.irisScannerCheckBox.AutoCheck = False
		Me.irisScannerCheckBox.AutoSize = True
		Me.irisScannerCheckBox.Location = New System.Drawing.Point(40, 204)
		Me.irisScannerCheckBox.Name = "irisScannerCheckBox"
		Me.irisScannerCheckBox.Size = New System.Drawing.Size(80, 17)
		Me.irisScannerCheckBox.TabIndex = 6
		Me.irisScannerCheckBox.Text = "Iris scanner"
		Me.irisScannerCheckBox.UseVisualStyleBackColor = True
		'			Me.irisScannerCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' palmScannerCheckBox
		' 
		Me.palmScannerCheckBox.AutoCheck = False
		Me.palmScannerCheckBox.AutoSize = True
		Me.palmScannerCheckBox.Location = New System.Drawing.Point(56, 181)
		Me.palmScannerCheckBox.Name = "palmScannerCheckBox"
		Me.palmScannerCheckBox.Size = New System.Drawing.Size(90, 17)
		Me.palmScannerCheckBox.TabIndex = 5
		Me.palmScannerCheckBox.Text = "Palm scanner"
		Me.palmScannerCheckBox.UseVisualStyleBackColor = True
		'			Me.palmScannerCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' fingerScannerCheckBox
		' 
		Me.fingerScannerCheckBox.AutoCheck = False
		Me.fingerScannerCheckBox.AutoSize = True
		Me.fingerScannerCheckBox.Location = New System.Drawing.Point(56, 158)
		Me.fingerScannerCheckBox.Name = "fingerScannerCheckBox"
		Me.fingerScannerCheckBox.Size = New System.Drawing.Size(96, 17)
		Me.fingerScannerCheckBox.TabIndex = 4
		Me.fingerScannerCheckBox.Text = "Finger scanner"
		Me.fingerScannerCheckBox.UseVisualStyleBackColor = True
		'			Me.fingerScannerCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' biometricDeviceCheckBox
		' 
		Me.biometricDeviceCheckBox.AutoCheck = False
		Me.biometricDeviceCheckBox.AutoSize = True
		Me.biometricDeviceCheckBox.Location = New System.Drawing.Point(22, 112)
		Me.biometricDeviceCheckBox.Name = "biometricDeviceCheckBox"
		Me.biometricDeviceCheckBox.Size = New System.Drawing.Size(104, 17)
		Me.biometricDeviceCheckBox.TabIndex = 2
		Me.biometricDeviceCheckBox.Text = "Biometric device"
		Me.biometricDeviceCheckBox.UseVisualStyleBackColor = True
		'			Me.biometricDeviceCheckBox.Click += New System.EventHandler(Me.deviceTypeCheckBox_Click);
		' 
		' DeviceManagerForm
		' 
		Me.AcceptButton = Me.btnAccept
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(274, 306)
		Me.Controls.Add(Me.deviceTypesGroupBox)
		Me.Controls.Add(Me.btnAccept)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.cbAutoplug)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "DeviceManagerForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "DeviceManager"
		Me.deviceTypesGroupBox.ResumeLayout(False)
		Me.deviceTypesGroupBox.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private btnCancel As System.Windows.Forms.Button
	Private WithEvents btnAccept As System.Windows.Forms.Button
	Private cbAutoplug As System.Windows.Forms.CheckBox
	Private deviceTypesGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents fScannerCheckBox As System.Windows.Forms.CheckBox
	Private WithEvents cameraCheckBox As System.Windows.Forms.CheckBox
	Private WithEvents irisScannerCheckBox As System.Windows.Forms.CheckBox
	Private WithEvents palmScannerCheckBox As System.Windows.Forms.CheckBox
	Private WithEvents fingerScannerCheckBox As System.Windows.Forms.CheckBox
	Private WithEvents biometricDeviceCheckBox As System.Windows.Forms.CheckBox
	Private WithEvents anyCheckBox As System.Windows.Forms.CheckBox
	Private WithEvents microphoneCheckBox As System.Windows.Forms.CheckBox
	Private WithEvents captureDeviceCheckBox As System.Windows.Forms.CheckBox
End Class
