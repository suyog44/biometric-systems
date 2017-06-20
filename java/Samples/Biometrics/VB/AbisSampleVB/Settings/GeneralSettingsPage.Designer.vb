Imports Microsoft.VisualBasic
Imports System
Partial Public Class GeneralSettingsPage
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
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.chbFirstResult = New System.Windows.Forms.CheckBox()
		Me.chbMatchWithDetails = New System.Windows.Forms.CheckBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.cbMatchingThreshold = New System.Windows.Forms.ComboBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.nudResultsCount = New System.Windows.Forms.NumericUpDown()
		Me.tableLayoutPanel1.SuspendLayout()
		CType(Me.nudResultsCount, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' tableLayoutPanel1
		' 
		Me.tableLayoutPanel1.AutoScroll = True
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F))
		Me.tableLayoutPanel1.Controls.Add(Me.chbFirstResult, 1, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.chbMatchWithDetails, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.cbMatchingThreshold, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.nudResultsCount, 1, 1)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 4
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(266, 151)
		Me.tableLayoutPanel1.TabIndex = 0
		' 
		' chbFirstResult
		' 
		Me.chbFirstResult.AutoSize = True
		Me.chbFirstResult.Location = New System.Drawing.Point(120, 79)
		Me.chbFirstResult.Name = "chbFirstResult"
		Me.chbFirstResult.Size = New System.Drawing.Size(95, 17)
		Me.chbFirstResult.TabIndex = 1
		Me.chbFirstResult.Text = "First result only"
		Me.chbFirstResult.UseVisualStyleBackColor = True
'		Me.chbFirstResult.CheckedChanged += New System.EventHandler(Me.ChbFirstResultCheckedChanged);
		' 
		' chbMatchWithDetails
		' 
		Me.chbMatchWithDetails.AutoSize = True
		Me.chbMatchWithDetails.Location = New System.Drawing.Point(120, 56)
		Me.chbMatchWithDetails.Name = "chbMatchWithDetails"
		Me.chbMatchWithDetails.Size = New System.Drawing.Size(137, 17)
		Me.chbMatchWithDetails.TabIndex = 0
		Me.chbMatchWithDetails.Text = "Return matching details"
		Me.chbMatchWithDetails.UseVisualStyleBackColor = True
'		Me.chbMatchWithDetails.CheckedChanged += New System.EventHandler(Me.ChbMatchWithDetailsCheckedChanged);
		' 
		' label2
		' 
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Location = New System.Drawing.Point(3, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(111, 27)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Matching threshold:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		' 
		' cbMatchingThreshold
		' 
		Me.cbMatchingThreshold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbMatchingThreshold.FormattingEnabled = True
		Me.cbMatchingThreshold.Location = New System.Drawing.Point(120, 3)
		Me.cbMatchingThreshold.Name = "cbMatchingThreshold"
		Me.cbMatchingThreshold.Size = New System.Drawing.Size(134, 21)
		Me.cbMatchingThreshold.TabIndex = 3
'		Me.cbMatchingThreshold.SelectedIndexChanged += New System.EventHandler(Me.CbMatchingThresholdSelectedIndexChanged);
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 27)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(111, 26)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Maximal results count:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		' 
		' nudResultsCount
		' 
		Me.nudResultsCount.Location = New System.Drawing.Point(120, 30)
		Me.nudResultsCount.Maximum = New Decimal(New Integer() { 2147483647, 0, 0, 0})
		Me.nudResultsCount.Name = "nudResultsCount"
		Me.nudResultsCount.Size = New System.Drawing.Size(134, 20)
		Me.nudResultsCount.TabIndex = 4
		Me.nudResultsCount.Value = New Decimal(New Integer() { 1000, 0, 0, 0})
'		Me.nudResultsCount.ValueChanged += New System.EventHandler(Me.NudResultsCountValueChanged);
		' 
		' GeneralSettingsPage
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "GeneralSettingsPage"
		Me.PageName = "General"
		Me.Size = New System.Drawing.Size(266, 151)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		CType(Me.nudResultsCount, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents chbMatchWithDetails As System.Windows.Forms.CheckBox
	Private label2 As System.Windows.Forms.Label
	Private WithEvents cbMatchingThreshold As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
	Private WithEvents chbFirstResult As System.Windows.Forms.CheckBox
	Private WithEvents nudResultsCount As System.Windows.Forms.NumericUpDown


End Class
