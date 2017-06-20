Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms
	Partial Public Class ANType99RecordCreateForm
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
			Me.label2 = New System.Windows.Forms.Label()
			Me.tbSrc = New System.Windows.Forms.TextBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.label5 = New System.Windows.Forms.Label()
			Me.label6 = New System.Windows.Forms.Label()
			Me.tbDataPath = New System.Windows.Forms.TextBox()
			Me.btnBrowseData = New System.Windows.Forms.Button()
			Me.label7 = New System.Windows.Forms.Label()
			Me.nudBfo = New System.Windows.Forms.NumericUpDown()
			Me.nudBft = New System.Windows.Forms.NumericUpDown()
			Me.dataOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
			Me.cbVersion = New System.Windows.Forms.ComboBox()
			Me.chlbBiometricType = New System.Windows.Forms.CheckedListBox()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudBfo, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudBft, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' okButton
			' 
			Me.btnOk.Location = New System.Drawing.Point(208, 305)
			Me.btnOk.TabIndex = 15
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(289, 305)
			Me.btnCancel.TabIndex = 16
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(9, 62)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(82, 13)
			Me.label2.TabIndex = 2
			Me.label2.Text = "Source agency:"
			' 
			' tbSrc
			' 
			Me.tbSrc.Location = New System.Drawing.Point(135, 59)
			Me.tbSrc.Name = "tbSrc"
			Me.tbSrc.Size = New System.Drawing.Size(216, 20)
			Me.tbSrc.TabIndex = 3
			'			Me.tbSrc.Validating += New System.ComponentModel.CancelEventHandler(Me.TbSrcValidating);
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(9, 88)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(45, 13)
			Me.label3.TabIndex = 4
			Me.label3.Text = "Version:"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(9, 117)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(76, 13)
			Me.label4.TabIndex = 6
			Me.label4.Text = "Biometric type:"
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(12, 226)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(117, 13)
			Me.label5.TabIndex = 8
			Me.label5.Text = "Biometric format owner:"
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Location = New System.Drawing.Point(12, 252)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(108, 13)
			Me.label6.TabIndex = 10
			Me.label6.Text = "Biometric format type:"
			' 
			' tbDataPath
			' 
			Me.tbDataPath.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbDataPath.Location = New System.Drawing.Point(95, 276)
			Me.tbDataPath.Name = "tbDataPath"
			Me.tbDataPath.Size = New System.Drawing.Size(191, 20)
			Me.tbDataPath.TabIndex = 13
			' 
			' btnBrowseData
			' 
			Me.btnBrowseData.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnBrowseData.Location = New System.Drawing.Point(289, 273)
			Me.btnBrowseData.Name = "btnBrowseData"
			Me.btnBrowseData.Size = New System.Drawing.Size(75, 23)
			Me.btnBrowseData.TabIndex = 14
			Me.btnBrowseData.Text = "Browse..."
			Me.btnBrowseData.UseVisualStyleBackColor = True
			'			Me.btnBrowseData.Click += New System.EventHandler(Me.BtnBrowseDataClick);
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Location = New System.Drawing.Point(12, 279)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(77, 13)
			Me.label7.TabIndex = 12
			Me.label7.Text = "Biometric data:"
			' 
			' nudBfo
			' 
			Me.nudBfo.Location = New System.Drawing.Point(135, 224)
			Me.nudBfo.Name = "nudBfo"
			Me.nudBfo.Size = New System.Drawing.Size(75, 20)
			Me.nudBfo.TabIndex = 9
			' 
			' nudBft
			' 
			Me.nudBft.Location = New System.Drawing.Point(135, 250)
			Me.nudBft.Name = "nudBft"
			Me.nudBft.Size = New System.Drawing.Size(75, 20)
			Me.nudBft.TabIndex = 11
			' 
			' dataOpenFileDialog
			' 
			Me.dataOpenFileDialog.Filter = "All Files (*.*)|*.*"
			' 
			' cbVersion
			' 
			Me.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbVersion.FormattingEnabled = True
			Me.cbVersion.Location = New System.Drawing.Point(135, 85)
			Me.cbVersion.Name = "cbVersion"
			Me.cbVersion.Size = New System.Drawing.Size(216, 21)
			Me.cbVersion.TabIndex = 5
			' 
			' chlbBiometricType
			' 
			Me.chlbBiometricType.CheckOnClick = True
			Me.chlbBiometricType.FormattingEnabled = True
			Me.chlbBiometricType.Location = New System.Drawing.Point(135, 117)
			Me.chlbBiometricType.Name = "chlbBiometricType"
			Me.chlbBiometricType.Size = New System.Drawing.Size(216, 94)
			Me.chlbBiometricType.TabIndex = 18
			'			Me.chlbBiometricType.ItemCheck += New System.Windows.Forms.ItemCheckEventHandler(Me.ChlbBiometricTypeImteCheck);
			' 
			' ANType99RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(374, 338)
			Me.Controls.Add(Me.chlbBiometricType)
			Me.Controls.Add(Me.cbVersion)
			Me.Controls.Add(Me.nudBft)
			Me.Controls.Add(Me.nudBfo)
			Me.Controls.Add(Me.tbDataPath)
			Me.Controls.Add(Me.btnBrowseData)
			Me.Controls.Add(Me.label7)
			Me.Controls.Add(Me.label6)
			Me.Controls.Add(Me.label5)
			Me.Controls.Add(Me.label4)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.tbSrc)
			Me.Name = "ANType99RecordCreateForm"
			Me.Text = "Add Type-99 ANRecord"
			Me.Controls.SetChildIndex(Me.label1, 0)
			Me.Controls.SetChildIndex(Me.tbSrc, 0)
			Me.Controls.SetChildIndex(Me.label2, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.label3, 0)
			Me.Controls.SetChildIndex(Me.label4, 0)
			Me.Controls.SetChildIndex(Me.label5, 0)
			Me.Controls.SetChildIndex(Me.label6, 0)
			Me.Controls.SetChildIndex(Me.label7, 0)
			Me.Controls.SetChildIndex(Me.btnBrowseData, 0)
			Me.Controls.SetChildIndex(Me.tbDataPath, 0)
			Me.Controls.SetChildIndex(Me.nudBfo, 0)
			Me.Controls.SetChildIndex(Me.nudBft, 0)
			Me.Controls.SetChildIndex(Me.cbVersion, 0)
			Me.Controls.SetChildIndex(Me.chlbBiometricType, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudBfo, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudBft, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private label2 As System.Windows.Forms.Label
		Private WithEvents tbSrc As System.Windows.Forms.TextBox
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private label6 As System.Windows.Forms.Label
		Private tbDataPath As System.Windows.Forms.TextBox
		Private WithEvents btnBrowseData As System.Windows.Forms.Button
		Private label7 As System.Windows.Forms.Label
		Private nudBfo As System.Windows.Forms.NumericUpDown
		Private nudBft As System.Windows.Forms.NumericUpDown
		Private dataOpenFileDialog As System.Windows.Forms.OpenFileDialog
		Private cbVersion As System.Windows.Forms.ComboBox
		Private WithEvents chlbBiometricType As System.Windows.Forms.CheckedListBox
	End Class
End Namespace