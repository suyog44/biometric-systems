Imports Microsoft.VisualBasic
Imports System
Partial Public Class CreateANPenVectorArrayForm
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
		Me.components = New System.ComponentModel.Container
		Me.dataGridView = New System.Windows.Forms.DataGridView
		Me.btnOk = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.errorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.columnX = New System.Windows.Forms.DataGridViewTextBoxColumn
		Me.columnY = New System.Windows.Forms.DataGridViewTextBoxColumn
		Me.columnPressure = New System.Windows.Forms.DataGridViewTextBoxColumn
		CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'dataGridView
		'
		Me.dataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.dataGridView.BackgroundColor = System.Drawing.Color.White
		Me.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.dataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.columnX, Me.columnY, Me.columnPressure})
		Me.dataGridView.Location = New System.Drawing.Point(2, 1)
		Me.dataGridView.Name = "dataGridView"
		Me.dataGridView.Size = New System.Drawing.Size(271, 234)
		Me.dataGridView.TabIndex = 0
		'
		'btnOk
		'
		Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOk.Location = New System.Drawing.Point(107, 241)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 1
		Me.btnOk.Text = "Ok"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(188, 241)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 2
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'errorProvider
		'
		Me.errorProvider.ContainerControl = Me
		'
		'columnX
		'
		Me.columnX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.columnX.FillWeight = 33.0!
		Me.columnX.HeaderText = "X"
		Me.columnX.Name = "columnX"
		Me.columnX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'columnY
		'
		Me.columnY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.columnY.FillWeight = 33.0!
		Me.columnY.HeaderText = "Y"
		Me.columnY.Name = "columnY"
		Me.columnY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'columnPressure
		'
		Me.columnPressure.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.columnPressure.FillWeight = 33.0!
		Me.columnPressure.HeaderText = "Pressure"
		Me.columnPressure.Name = "columnPressure"
		Me.columnPressure.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'CreateANPenVectorArrayForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(275, 276)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.dataGridView)
		Me.MinimizeBox = False
		Me.Name = "CreateANPenVectorArrayForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.Text = "Edit pen vectors"
		CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private WithEvents dataGridView As System.Windows.Forms.DataGridView
	Private WithEvents btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private errorProvider As System.Windows.Forms.ErrorProvider
	Private toolTip As System.Windows.Forms.ToolTip
	Friend WithEvents columnX As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents columnY As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents columnPressure As System.Windows.Forms.DataGridViewTextBoxColumn
End Class