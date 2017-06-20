Imports Microsoft.VisualBasic
Imports System
Partial Public Class ConnectToDeviceForm
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
		Me.pluginLabel = New System.Windows.Forms.Label()
		Me.pluginComboBox = New System.Windows.Forms.ComboBox()
		Me.groupBox = New System.Windows.Forms.GroupBox()
		Me.propertyGrid = New System.Windows.Forms.PropertyGrid()
		Me.btnOK = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.groupBox.SuspendLayout()
		Me.SuspendLayout()
		' 
		' pluginLabel
		' 
		Me.pluginLabel.AutoSize = True
		Me.pluginLabel.Location = New System.Drawing.Point(12, 15)
		Me.pluginLabel.Name = "pluginLabel"
		Me.pluginLabel.Size = New System.Drawing.Size(39, 13)
		Me.pluginLabel.TabIndex = 1
		Me.pluginLabel.Text = "&Plugin:"
		' 
		' pluginComboBox
		' 
		Me.pluginComboBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.pluginComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.pluginComboBox.FormattingEnabled = True
		Me.pluginComboBox.Location = New System.Drawing.Point(57, 12)
		Me.pluginComboBox.Name = "pluginComboBox"
		Me.pluginComboBox.Size = New System.Drawing.Size(311, 21)
		Me.pluginComboBox.TabIndex = 2
		'			Me.pluginComboBox.SelectedIndexChanged += New System.EventHandler(Me.pluginComboBox_SelectedIndexChanged);
		' 
		' groupBox
		' 
		Me.groupBox.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.groupBox.Controls.Add(Me.propertyGrid)
		Me.groupBox.Location = New System.Drawing.Point(12, 51)
		Me.groupBox.Name = "groupBox"
		Me.groupBox.Size = New System.Drawing.Size(359, 274)
		Me.groupBox.TabIndex = 0
		Me.groupBox.TabStop = False
		Me.groupBox.Text = "P&arameters"
		' 
		' propertyGrid
		' 
		Me.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.propertyGrid.Location = New System.Drawing.Point(3, 16)
		Me.propertyGrid.Name = "propertyGrid"
		Me.propertyGrid.Size = New System.Drawing.Size(353, 255)
		Me.propertyGrid.TabIndex = 0
		' 
		' btnOK
		' 
		Me.btnOK.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOK.Location = New System.Drawing.Point(215, 331)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.Size = New System.Drawing.Size(75, 23)
		Me.btnOK.TabIndex = 3
		Me.btnOK.Text = "OK"
		Me.btnOK.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(296, 331)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 4
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' ConnectToDeviceForm
		' 
		Me.AcceptButton = Me.btnOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(383, 366)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOK)
		Me.Controls.Add(Me.groupBox)
		Me.Controls.Add(Me.pluginComboBox)
		Me.Controls.Add(Me.pluginLabel)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(200, 200)
		Me.Name = "ConnectToDeviceForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Connect to Device"
		'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.ConnectToDeviceForm_FormClosing);
		Me.groupBox.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private pluginLabel As System.Windows.Forms.Label
	Private WithEvents pluginComboBox As System.Windows.Forms.ComboBox
	Private groupBox As System.Windows.Forms.GroupBox
	Private propertyGrid As System.Windows.Forms.PropertyGrid
	Private btnOK As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
End Class
