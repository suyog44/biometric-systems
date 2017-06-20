Imports Microsoft.VisualBasic
Imports System
Partial Public Class BiometricPreviewPage
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
		Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.btnFinish = New System.Windows.Forms.Button
		Me.chbShowBinarized = New System.Windows.Forms.CheckBox
		Me.horizontalZoomSlider = New Neurotec.Gui.NViewZoomSlider
		Me.btnSave = New System.Windows.Forms.Button
		Me.generalizeProgressView = New Neurotec.Samples.GeneralizeProgressView
		Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.panelView = New System.Windows.Forms.Panel
		Me.icaoWarningView = New Neurotec.Samples.IcaoWarningView
		Me.tableLayoutPanel.SuspendLayout()
		Me.TableLayoutPanel1.SuspendLayout()
		Me.TableLayoutPanel2.SuspendLayout()
		Me.TableLayoutPanel3.SuspendLayout()
		Me.SuspendLayout()
		'
		'tableLayoutPanel
		'
		Me.tableLayoutPanel.ColumnCount = 1
		Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel.Controls.Add(Me.TableLayoutPanel1, 0, 2)
		Me.tableLayoutPanel.Controls.Add(Me.generalizeProgressView, 0, 1)
		Me.tableLayoutPanel.Controls.Add(Me.TableLayoutPanel2, 0, 0)
		Me.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel.Name = "tableLayoutPanel"
		Me.tableLayoutPanel.RowCount = 3
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel.Size = New System.Drawing.Size(772, 376)
		Me.tableLayoutPanel.TabIndex = 0
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.ColumnCount = 4
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel1.Controls.Add(Me.btnFinish, 3, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.chbShowBinarized, 2, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.horizontalZoomSlider, 1, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.btnSave, 0, 0)
		Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 343)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 1
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(766, 30)
		Me.TableLayoutPanel1.TabIndex = 1
		'
		'btnFinish
		'
		Me.btnFinish.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnFinish.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnFinish.Location = New System.Drawing.Point(687, 4)
		Me.btnFinish.Name = "btnFinish"
		Me.btnFinish.Size = New System.Drawing.Size(76, 23)
		Me.btnFinish.TabIndex = 10
		Me.btnFinish.Text = "&Finish"
		Me.btnFinish.UseVisualStyleBackColor = True
		'
		'chbShowBinarized
		'
		Me.chbShowBinarized.AutoSize = True
		Me.chbShowBinarized.Dock = System.Windows.Forms.DockStyle.Fill
		Me.chbShowBinarized.Location = New System.Drawing.Point(333, 3)
		Me.chbShowBinarized.Name = "chbShowBinarized"
		Me.chbShowBinarized.Size = New System.Drawing.Size(348, 24)
		Me.chbShowBinarized.TabIndex = 9
		Me.chbShowBinarized.Text = "Show binarized image"
		Me.chbShowBinarized.UseVisualStyleBackColor = True
		'
		'horizontalZoomSlider
		'
		Me.horizontalZoomSlider.Location = New System.Drawing.Point(81, 3)
		Me.horizontalZoomSlider.Name = "horizontalZoomSlider"
		Me.horizontalZoomSlider.Size = New System.Drawing.Size(246, 24)
		Me.horizontalZoomSlider.TabIndex = 8
		Me.horizontalZoomSlider.View = Nothing
		'
		'btnSave
		'
		Me.btnSave.AutoSize = True
		Me.btnSave.Image = Global.Neurotec.Samples.My.Resources.Resources.saveHS
		Me.btnSave.Location = New System.Drawing.Point(3, 3)
		Me.btnSave.Name = "btnSave"
		Me.btnSave.Size = New System.Drawing.Size(72, 23)
		Me.btnSave.TabIndex = 7
		Me.btnSave.Text = "Save"
		Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSave.UseVisualStyleBackColor = True
		'
		'generalizeProgressView
		'
		Me.generalizeProgressView.AutoSize = True
		Me.generalizeProgressView.Biometrics = Nothing
		Me.generalizeProgressView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.generalizeProgressView.EnableMouseSelection = True
		Me.generalizeProgressView.Generalized = Nothing
		Me.generalizeProgressView.IcaoView = Nothing
		Me.generalizeProgressView.Location = New System.Drawing.Point(3, 302)
		Me.generalizeProgressView.Name = "generalizeProgressView"
		Me.generalizeProgressView.Selected = Nothing
		Me.generalizeProgressView.Size = New System.Drawing.Size(766, 35)
		Me.generalizeProgressView.StatusText = ""
		Me.generalizeProgressView.TabIndex = 7
		Me.generalizeProgressView.View = Nothing
		'
		'TableLayoutPanel2
		'
		Me.TableLayoutPanel2.ColumnCount = 2
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
		Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
		Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
		Me.TableLayoutPanel2.RowCount = 1
		Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.TableLayoutPanel2.Size = New System.Drawing.Size(766, 293)
		Me.TableLayoutPanel2.TabIndex = 8
		'
		'TableLayoutPanel3
		'
		Me.TableLayoutPanel3.ColumnCount = 2
		Me.TableLayoutPanel2.SetColumnSpan(Me.TableLayoutPanel3, 2)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Controls.Add(Me.icaoWarningView, 0, 0)
		Me.TableLayoutPanel3.Controls.Add(Me.panelView, 1, 0)
		Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
		Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
		Me.TableLayoutPanel3.RowCount = 1
		Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.TableLayoutPanel3.Size = New System.Drawing.Size(760, 287)
		Me.TableLayoutPanel3.TabIndex = 3
		'
		'panelView
		'
		Me.panelView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelView.Location = New System.Drawing.Point(153, 3)
		Me.panelView.Name = "panelView"
		Me.panelView.Size = New System.Drawing.Size(604, 281)
		Me.panelView.TabIndex = 2
		'
		'icaoWarningView
		'
		Me.icaoWarningView.AutoScroll = True
		Me.icaoWarningView.AutoSize = True
		Me.icaoWarningView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.icaoWarningView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.icaoWarningView.Face = Nothing
		Me.icaoWarningView.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.icaoWarningView.IndeterminateColor = System.Drawing.Color.Orange
		Me.icaoWarningView.Location = New System.Drawing.Point(3, 3)
		Me.icaoWarningView.Name = "icaoWarningView"
		Me.icaoWarningView.NoWarningColor = System.Drawing.Color.Green
		Me.icaoWarningView.Size = New System.Drawing.Size(144, 281)
		Me.icaoWarningView.TabIndex = 3
		Me.icaoWarningView.WarningColor = System.Drawing.Color.Red
		'
		'BiometricPreviewPage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel)
		Me.Name = "BiometricPreviewPage"
		Me.Size = New System.Drawing.Size(772, 376)
		Me.tableLayoutPanel.ResumeLayout(False)
		Me.tableLayoutPanel.PerformLayout()
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.TableLayoutPanel1.PerformLayout()
		Me.TableLayoutPanel2.ResumeLayout(False)
		Me.TableLayoutPanel3.ResumeLayout(False)
		Me.TableLayoutPanel3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private generalizeProgressView As GeneralizeProgressView
	Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnFinish As System.Windows.Forms.Button
	Private WithEvents chbShowBinarized As System.Windows.Forms.CheckBox
	Private WithEvents horizontalZoomSlider As Neurotec.Gui.NViewZoomSlider
	Private WithEvents btnSave As System.Windows.Forms.Button
	Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents icaoWarningView As Neurotec.Samples.IcaoWarningView
	Private WithEvents panelView As System.Windows.Forms.Panel
End Class
