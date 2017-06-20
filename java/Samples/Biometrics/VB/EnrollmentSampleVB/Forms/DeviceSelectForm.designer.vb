Imports Microsoft.VisualBasic
Imports System
Namespace Forms
	Partial Public Class DeviceSelectForm
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
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(DeviceSelectForm))
			Me.btnOk = New System.Windows.Forms.Button()
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.label1 = New System.Windows.Forms.Label()
			Me.cbScanner = New System.Windows.Forms.ComboBox()
			Me.lblCanCaptureSlaps = New System.Windows.Forms.Label()
			Me.lblCanCaptureRolled = New System.Windows.Forms.Label()
			Me.SuspendLayout()
			' 
			' btnOk
			' 
			Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnOk.Location = New System.Drawing.Point(191, 109)
			Me.btnOk.Name = "btnOk"
			Me.btnOk.Size = New System.Drawing.Size(75, 23)
			Me.btnOk.TabIndex = 0
			Me.btnOk.Text = "&OK"
			Me.btnOk.UseVisualStyleBackColor = True
'			Me.btnOk.Click += New System.EventHandler(Me.BtnOkClick);
			' 
			' btnCancel
			' 
			Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnCancel.Location = New System.Drawing.Point(272, 109)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(75, 23)
			Me.btnCancel.TabIndex = 1
			Me.btnCancel.Text = "&Cancel"
			Me.btnCancel.UseVisualStyleBackColor = True
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(9, 9)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(90, 13)
			Me.label1.TabIndex = 2
			Me.label1.Text = "Selected scanner"
			' 
			' cbScanner
			' 
			Me.cbScanner.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.cbScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbScanner.FormattingEnabled = True
			Me.cbScanner.Location = New System.Drawing.Point(12, 26)
			Me.cbScanner.Name = "cbScanner"
			Me.cbScanner.Size = New System.Drawing.Size(335, 21)
			Me.cbScanner.TabIndex = 3
'			Me.cbScanner.SelectedIndexChanged += New System.EventHandler(Me.CbScannerSelectedIndexChanged);
			' 
			' lblCanCaptureSlaps
			' 
			Me.lblCanCaptureSlaps.Image = My.Resources.Bad
			Me.lblCanCaptureSlaps.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.lblCanCaptureSlaps.Location = New System.Drawing.Point(37, 62)
			Me.lblCanCaptureSlaps.Name = "lblCanCaptureSlaps"
			Me.lblCanCaptureSlaps.Size = New System.Drawing.Size(127, 13)
			Me.lblCanCaptureSlaps.TabIndex = 4
			Me.lblCanCaptureSlaps.Text = "Can capture slaps"
			Me.lblCanCaptureSlaps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' lblCanCaptureRolled
			' 
			Me.lblCanCaptureRolled.Image = My.Resources.Bad
			Me.lblCanCaptureRolled.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.lblCanCaptureRolled.Location = New System.Drawing.Point(37, 90)
			Me.lblCanCaptureRolled.Name = "lblCanCaptureRolled"
			Me.lblCanCaptureRolled.Size = New System.Drawing.Size(160, 13)
			Me.lblCanCaptureRolled.TabIndex = 5
			Me.lblCanCaptureRolled.Text = "Can capture rolled fingers"
			Me.lblCanCaptureRolled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' DeviceSelectForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(359, 144)
			Me.Controls.Add(Me.lblCanCaptureRolled)
			Me.Controls.Add(Me.lblCanCaptureSlaps)
			Me.Controls.Add(Me.cbScanner)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnOk)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "DeviceSelectForm"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Select Scanner"
'			Me.Load += New System.EventHandler(Me.ScannerFormLoad);
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents btnOk As System.Windows.Forms.Button
		Private btnCancel As System.Windows.Forms.Button
		Private label1 As System.Windows.Forms.Label
		Private WithEvents cbScanner As System.Windows.Forms.ComboBox
		Private lblCanCaptureSlaps As System.Windows.Forms.Label
		Private lblCanCaptureRolled As System.Windows.Forms.Label
	End Class
End Namespace
