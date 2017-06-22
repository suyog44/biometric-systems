Imports Microsoft.VisualBasic
Imports System
Partial Public Class CharsetForm
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
		Me.standardCharsetsLabel = New System.Windows.Forms.Label()
		Me.lbStandardCharsets = New System.Windows.Forms.ListView()
		Me.charsetIndexColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.charsetNameColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.charsetVersionColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.rbStandardCharset = New System.Windows.Forms.RadioButton()
		Me.userDefinedCharsetsLabel = New System.Windows.Forms.Label()
		Me.rbUserDefinedCharset = New System.Windows.Forms.RadioButton()
		Me.tbUserDefinedCharsetIndex = New System.Windows.Forms.TextBox()
		Me.lblUserDefinedCharsetIndicies = New System.Windows.Forms.Label()
		Me.userDefinedCharsetIndexLabel = New System.Windows.Forms.Label()
		Me.userDefinedCharsetNameLabel = New System.Windows.Forms.Label()
		Me.tbUserDefinedCharsetName = New System.Windows.Forms.TextBox()
		Me.charsetVersionLabel = New System.Windows.Forms.Label()
		Me.tbCharsetVersion = New System.Windows.Forms.TextBox()
		Me.SuspendLayout()
		' 
		' standardCharsetsLabel
		' 
		Me.standardCharsetsLabel.AutoSize = True
		Me.standardCharsetsLabel.Location = New System.Drawing.Point(12, 9)
		Me.standardCharsetsLabel.Name = "standardCharsetsLabel"
		Me.standardCharsetsLabel.Size = New System.Drawing.Size(96, 13)
		Me.standardCharsetsLabel.TabIndex = 1
		Me.standardCharsetsLabel.Text = "Standard charsets:"
		' 
		' lbStandardCharsets
		' 
		Me.lbStandardCharsets.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.lbStandardCharsets.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.charsetIndexColumnHeader, Me.charsetNameColumnHeader, Me.charsetVersionColumnHeader})
		Me.lbStandardCharsets.FullRowSelect = True
		Me.lbStandardCharsets.GridLines = True
		Me.lbStandardCharsets.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
		Me.lbStandardCharsets.HideSelection = False
		Me.lbStandardCharsets.Location = New System.Drawing.Point(12, 25)
		Me.lbStandardCharsets.MultiSelect = False
		Me.lbStandardCharsets.Name = "lbStandardCharsets"
		Me.lbStandardCharsets.Size = New System.Drawing.Size(398, 120)
		Me.lbStandardCharsets.TabIndex = 2
		Me.lbStandardCharsets.UseCompatibleStateImageBehavior = False
		Me.lbStandardCharsets.View = System.Windows.Forms.View.Details
'		Me.lbStandardCharsets.SelectedIndexChanged += New System.EventHandler(Me.LvStandardCharsetSelectedIndexChanged);
'		Me.lbStandardCharsets.DoubleClick += New System.EventHandler(Me.LvStandardCharsetDoubleClick);
		' 
		' charsetIndexColumnHeader
		' 
		Me.charsetIndexColumnHeader.Text = "Index"
		' 
		' charsetNameColumnHeader
		' 
		Me.charsetNameColumnHeader.Text = "Name"
		Me.charsetNameColumnHeader.Width = 250
		' 
		' charsetVersionColumnHeader
		' 
		Me.charsetVersionColumnHeader.Text = "Version"
		Me.charsetVersionColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		' 
		' btnOk
		' 
		Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(254, 231)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 12
		Me.btnOk.Text = "OK"
		Me.btnOk.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(335, 231)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 13
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' rbStandardCharset
		' 
		Me.rbStandardCharset.AutoCheck = False
		Me.rbStandardCharset.AutoSize = True
		Me.rbStandardCharset.Location = New System.Drawing.Point(15, 7)
		Me.rbStandardCharset.Name = "rbStandardCharset"
		Me.rbStandardCharset.Size = New System.Drawing.Size(109, 17)
		Me.rbStandardCharset.TabIndex = 0
		Me.rbStandardCharset.TabStop = True
		Me.rbStandardCharset.Text = "Standard charset:"
		Me.rbStandardCharset.UseVisualStyleBackColor = True
'		Me.rbStandardCharset.Click += New System.EventHandler(Me.RbStandardCharsetClick);
		' 
		' userDefinedCharsetsLabel
		' 
		Me.userDefinedCharsetsLabel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.userDefinedCharsetsLabel.AutoSize = True
		Me.userDefinedCharsetsLabel.Location = New System.Drawing.Point(12, 157)
		Me.userDefinedCharsetsLabel.Name = "userDefinedCharsetsLabel"
		Me.userDefinedCharsetsLabel.Size = New System.Drawing.Size(113, 13)
		Me.userDefinedCharsetsLabel.TabIndex = 4
		Me.userDefinedCharsetsLabel.Text = "User defined charsets:"
		' 
		' rbUserDefinedCharset
		' 
		Me.rbUserDefinedCharset.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.rbUserDefinedCharset.AutoCheck = False
		Me.rbUserDefinedCharset.AutoSize = True
		Me.rbUserDefinedCharset.Location = New System.Drawing.Point(15, 155)
		Me.rbUserDefinedCharset.Name = "rbUserDefinedCharset"
		Me.rbUserDefinedCharset.Size = New System.Drawing.Size(126, 17)
		Me.rbUserDefinedCharset.TabIndex = 3
		Me.rbUserDefinedCharset.TabStop = True
		Me.rbUserDefinedCharset.Text = "User defined charset:"
		Me.rbUserDefinedCharset.UseVisualStyleBackColor = True
'		Me.rbUserDefinedCharset.Click += New System.EventHandler(Me.RbUserDefinedCharsetClick);
		' 
		' tbUserDefinedCharsetIndex
		' 
		Me.tbUserDefinedCharsetIndex.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.tbUserDefinedCharsetIndex.Location = New System.Drawing.Point(15, 194)
		Me.tbUserDefinedCharsetIndex.Name = "tbUserDefinedCharsetIndex"
		Me.tbUserDefinedCharsetIndex.Size = New System.Drawing.Size(100, 20)
		Me.tbUserDefinedCharsetIndex.TabIndex = 7
		' 
		' lblUserDefinedCharsetIndicies
		' 
		Me.lblUserDefinedCharsetIndicies.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.lblUserDefinedCharsetIndicies.AutoSize = True
		Me.lblUserDefinedCharsetIndicies.Location = New System.Drawing.Point(143, 157)
		Me.lblUserDefinedCharsetIndicies.Name = "lblUserDefinedCharsetIndicies"
		Me.lblUserDefinedCharsetIndicies.Size = New System.Drawing.Size(34, 13)
		Me.lblUserDefinedCharsetIndicies.TabIndex = 5
		Me.lblUserDefinedCharsetIndicies.Text = "(0 - 0)"
		' 
		' userDefinedCharsetIndexLabel
		' 
		Me.userDefinedCharsetIndexLabel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.userDefinedCharsetIndexLabel.AutoSize = True
		Me.userDefinedCharsetIndexLabel.Location = New System.Drawing.Point(12, 178)
		Me.userDefinedCharsetIndexLabel.Name = "userDefinedCharsetIndexLabel"
		Me.userDefinedCharsetIndexLabel.Size = New System.Drawing.Size(36, 13)
		Me.userDefinedCharsetIndexLabel.TabIndex = 6
		Me.userDefinedCharsetIndexLabel.Text = "Index:"
		' 
		' userDefinedCharsetNameLabel
		' 
		Me.userDefinedCharsetNameLabel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.userDefinedCharsetNameLabel.AutoSize = True
		Me.userDefinedCharsetNameLabel.Location = New System.Drawing.Point(118, 178)
		Me.userDefinedCharsetNameLabel.Name = "userDefinedCharsetNameLabel"
		Me.userDefinedCharsetNameLabel.Size = New System.Drawing.Size(38, 13)
		Me.userDefinedCharsetNameLabel.TabIndex = 8
		Me.userDefinedCharsetNameLabel.Text = "Name:"
		' 
		' tbUserDefinedCharsetName
		' 
		Me.tbUserDefinedCharsetName.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.tbUserDefinedCharsetName.Location = New System.Drawing.Point(121, 194)
		Me.tbUserDefinedCharsetName.Name = "tbUserDefinedCharsetName"
		Me.tbUserDefinedCharsetName.Size = New System.Drawing.Size(100, 20)
		Me.tbUserDefinedCharsetName.TabIndex = 9
		' 
		' charsetVersionLabel
		' 
		Me.charsetVersionLabel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.charsetVersionLabel.AutoSize = True
		Me.charsetVersionLabel.Location = New System.Drawing.Point(307, 178)
		Me.charsetVersionLabel.Name = "charsetVersionLabel"
		Me.charsetVersionLabel.Size = New System.Drawing.Size(45, 13)
		Me.charsetVersionLabel.TabIndex = 10
		Me.charsetVersionLabel.Text = "Version:"
		' 
		' tbCharsetVersion
		' 
		Me.tbCharsetVersion.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.tbCharsetVersion.Location = New System.Drawing.Point(310, 194)
		Me.tbCharsetVersion.Name = "tbCharsetVersion"
		Me.tbCharsetVersion.Size = New System.Drawing.Size(100, 20)
		Me.tbCharsetVersion.TabIndex = 11
		' 
		' CharsetForm
		' 
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(422, 266)
		Me.Controls.Add(Me.tbCharsetVersion)
		Me.Controls.Add(Me.charsetVersionLabel)
		Me.Controls.Add(Me.tbUserDefinedCharsetName)
		Me.Controls.Add(Me.userDefinedCharsetNameLabel)
		Me.Controls.Add(Me.userDefinedCharsetIndexLabel)
		Me.Controls.Add(Me.lblUserDefinedCharsetIndicies)
		Me.Controls.Add(Me.tbUserDefinedCharsetIndex)
		Me.Controls.Add(Me.rbUserDefinedCharset)
		Me.Controls.Add(Me.userDefinedCharsetsLabel)
		Me.Controls.Add(Me.rbStandardCharset)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.lbStandardCharsets)
		Me.Controls.Add(Me.standardCharsetsLabel)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(400, 300)
		Me.Name = "CharsetForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Charset"
'		Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.CharsetFormClosing);
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private standardCharsetsLabel As System.Windows.Forms.Label
	Private WithEvents lbStandardCharsets As System.Windows.Forms.ListView
	Private btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private charsetIndexColumnHeader As System.Windows.Forms.ColumnHeader
	Private charsetNameColumnHeader As System.Windows.Forms.ColumnHeader
	Private WithEvents rbStandardCharset As System.Windows.Forms.RadioButton
	Private userDefinedCharsetsLabel As System.Windows.Forms.Label
	Private WithEvents rbUserDefinedCharset As System.Windows.Forms.RadioButton
	Private tbUserDefinedCharsetIndex As System.Windows.Forms.TextBox
	Private lblUserDefinedCharsetIndicies As System.Windows.Forms.Label
	Private charsetVersionColumnHeader As System.Windows.Forms.ColumnHeader
	Private userDefinedCharsetIndexLabel As System.Windows.Forms.Label
	Private userDefinedCharsetNameLabel As System.Windows.Forms.Label
	Private tbUserDefinedCharsetName As System.Windows.Forms.TextBox
	Private charsetVersionLabel As System.Windows.Forms.Label
	Private tbCharsetVersion As System.Windows.Forms.TextBox
End Class
