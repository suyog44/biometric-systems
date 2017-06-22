Imports Microsoft.VisualBasic
Imports System
Partial Public Class GeneralizeProgressView
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
		Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
		Me.lblStatus = New System.Windows.Forms.Label()
		Me.tlpBiometrics = New System.Windows.Forms.TableLayoutPanel()
		Me.panelPaint = New DoubleBufferedPanel()
		Me.tableLayoutPanel.SuspendLayout()
		Me.tlpBiometrics.SuspendLayout()
		Me.SuspendLayout()
		' 
		' tableLayoutPanel
		' 
		Me.tableLayoutPanel.ColumnCount = 1
		Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tableLayoutPanel.Controls.Add(Me.lblStatus, 0, 1)
		Me.tableLayoutPanel.Controls.Add(Me.tlpBiometrics, 0, 0)
		Me.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel.Name = "tableLayoutPanel"
		Me.tableLayoutPanel.RowCount = 2
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel.Size = New System.Drawing.Size(362, 47)
		Me.tableLayoutPanel.TabIndex = 0
		' 
		' lblStatus
		' 
		Me.lblStatus.AutoSize = True
		Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblStatus.Location = New System.Drawing.Point(3, 34)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(356, 13)
		Me.lblStatus.TabIndex = 0
		Me.lblStatus.Text = "Generalization status"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		' 
		' tlpBiometrics
		' 
		Me.tlpBiometrics.AutoSize = True
		Me.tlpBiometrics.ColumnCount = 1
		Me.tlpBiometrics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tlpBiometrics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0F))
		Me.tlpBiometrics.Controls.Add(Me.panelPaint, 0, 0)
		Me.tlpBiometrics.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tlpBiometrics.Location = New System.Drawing.Point(3, 3)
		Me.tlpBiometrics.MinimumSize = New System.Drawing.Size(0, 20)
		Me.tlpBiometrics.Name = "tlpBiometrics"
		Me.tlpBiometrics.RowCount = 1
		Me.tlpBiometrics.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tlpBiometrics.Size = New System.Drawing.Size(356, 28)
		Me.tlpBiometrics.TabIndex = 1
		' 
		' panelPaint
		' 
		Me.panelPaint.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelPaint.Location = New System.Drawing.Point(3, 3)
		Me.panelPaint.Name = "panelPaint"
		Me.panelPaint.Size = New System.Drawing.Size(350, 22)
		Me.panelPaint.TabIndex = 0
		'			Me.panelPaint.Paint += New System.Windows.Forms.PaintEventHandler(Me.PanelPaintPaint);
		'			Me.panelPaint.MouseMove += New System.Windows.Forms.MouseEventHandler(Me.PanelPaintMouseMove);
		'			Me.panelPaint.MouseClick += New System.Windows.Forms.MouseEventHandler(Me.PanelPaintMouseClick);
		' 
		' GeneralizationView
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel)
		Me.Name = "GeneralizationView"
		Me.Size = New System.Drawing.Size(362, 47)
		Me.tableLayoutPanel.ResumeLayout(False)
		Me.tableLayoutPanel.PerformLayout()
		Me.tlpBiometrics.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
	Private lblStatus As System.Windows.Forms.Label
	Private tlpBiometrics As System.Windows.Forms.TableLayoutPanel
	Private WithEvents panelPaint As DoubleBufferedPanel
End Class
