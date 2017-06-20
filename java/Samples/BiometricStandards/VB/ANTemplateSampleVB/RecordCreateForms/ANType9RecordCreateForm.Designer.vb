Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms
	Partial Public Class ANType9RecordCreateForm
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
			Me.chbFmtFlag = New System.Windows.Forms.CheckBox()
			Me.rbFromNFRecord = New System.Windows.Forms.RadioButton()
			Me.fromNFRecordPanel = New System.Windows.Forms.Panel()
			Me.tbNFRecordPath = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.btnBrowseNFRecord = New System.Windows.Forms.Button()
			Me.rbCreateEmpty = New System.Windows.Forms.RadioButton()
			Me.createEmptyPanel = New System.Windows.Forms.Panel()
			Me.chbHasRidgeCountsIndicator = New System.Windows.Forms.CheckBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.cbImpressionType = New System.Windows.Forms.ComboBox()
			Me.chbContainsRidgeCounts = New System.Windows.Forms.CheckBox()
			Me.chbHasMinutiae = New System.Windows.Forms.CheckBox()
			Me.nfRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.fromNFRecordPanel.SuspendLayout()
			Me.createEmptyPanel.SuspendLayout()
			Me.SuspendLayout()
			' 
			' okButton
			' 
			Me.btnOk.Location = New System.Drawing.Point(168, 276)
			Me.btnOk.TabIndex = 7
			' 
			' cancelButton
			' 
			Me.btnCancel.Location = New System.Drawing.Point(249, 276)
			Me.btnCancel.TabIndex = 8
			' 
			' chbFmtFlag
			' 
			Me.chbFmtFlag.AutoSize = True
			Me.chbFmtFlag.Checked = True
			Me.chbFmtFlag.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chbFmtFlag.Location = New System.Drawing.Point(12, 53)
			Me.chbFmtFlag.Name = "chbFmtFlag"
			Me.chbFmtFlag.Size = New System.Drawing.Size(146, 17)
			Me.chbFmtFlag.TabIndex = 2
			Me.chbFmtFlag.Text = "Minutia format is standard"
			Me.chbFmtFlag.UseVisualStyleBackColor = True
			' 
			' rbFromNFRecord
			' 
			Me.rbFromNFRecord.AutoSize = True
			Me.rbFromNFRecord.Checked = True
			Me.rbFromNFRecord.Location = New System.Drawing.Point(12, 76)
			Me.rbFromNFRecord.Name = "rbFromNFRecord"
			Me.rbFromNFRecord.Size = New System.Drawing.Size(103, 17)
			Me.rbFromNFRecord.TabIndex = 3
			Me.rbFromNFRecord.TabStop = True
			Me.rbFromNFRecord.Text = "From NFRecord:"
			Me.rbFromNFRecord.UseVisualStyleBackColor = True
			'			Me.rbFromNFRecord.CheckedChanged += New System.EventHandler(Me.RbFromNFRecordCheckedChanged);
			' 
			' fromNFRecordPanel
			' 
			Me.fromNFRecordPanel.Controls.Add(Me.tbNFRecordPath)
			Me.fromNFRecordPanel.Controls.Add(Me.label2)
			Me.fromNFRecordPanel.Controls.Add(Me.btnBrowseNFRecord)
			Me.fromNFRecordPanel.Location = New System.Drawing.Point(12, 99)
			Me.fromNFRecordPanel.Name = "fromNFRecordPanel"
			Me.fromNFRecordPanel.Size = New System.Drawing.Size(312, 33)
			Me.fromNFRecordPanel.TabIndex = 4
			' 
			' tbNFRecordPath
			' 
			Me.tbNFRecordPath.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbNFRecordPath.Location = New System.Drawing.Point(84, 3)
			Me.tbNFRecordPath.Name = "tbNFRecordPath"
			Me.tbNFRecordPath.Size = New System.Drawing.Size(144, 20)
			Me.tbNFRecordPath.TabIndex = 1
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(3, 6)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(75, 13)
			Me.label2.TabIndex = 0
			Me.label2.Text = "NFRecord file:"
			' 
			' btnBrowseNFRecord
			' 
			Me.btnBrowseNFRecord.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnBrowseNFRecord.Location = New System.Drawing.Point(234, 1)
			Me.btnBrowseNFRecord.Name = "btnBrowseNFRecord"
			Me.btnBrowseNFRecord.Size = New System.Drawing.Size(75, 23)
			Me.btnBrowseNFRecord.TabIndex = 2
			Me.btnBrowseNFRecord.Text = "Browse..."
			Me.btnBrowseNFRecord.UseVisualStyleBackColor = True
			'			Me.btnBrowseNFRecord.Click += New System.EventHandler(Me.BtnBrowseNFRecordClick);
			' 
			' rbCreateEmpty
			' 
			Me.rbCreateEmpty.AutoSize = True
			Me.rbCreateEmpty.Location = New System.Drawing.Point(12, 138)
			Me.rbCreateEmpty.Name = "rbCreateEmpty"
			Me.rbCreateEmpty.Size = New System.Drawing.Size(90, 17)
			Me.rbCreateEmpty.TabIndex = 5
			Me.rbCreateEmpty.Text = "Create empty:"
			Me.rbCreateEmpty.UseVisualStyleBackColor = True
			'			Me.rbCreateEmpty.CheckedChanged += New System.EventHandler(Me.RbCreateEmptyCheckedChanged);
			' 
			' createEmptyPanel
			' 
			Me.createEmptyPanel.Controls.Add(Me.chbHasRidgeCountsIndicator)
			Me.createEmptyPanel.Controls.Add(Me.label3)
			Me.createEmptyPanel.Controls.Add(Me.cbImpressionType)
			Me.createEmptyPanel.Controls.Add(Me.chbContainsRidgeCounts)
			Me.createEmptyPanel.Controls.Add(Me.chbHasMinutiae)
			Me.createEmptyPanel.Location = New System.Drawing.Point(12, 161)
			Me.createEmptyPanel.Name = "createEmptyPanel"
			Me.createEmptyPanel.Size = New System.Drawing.Size(312, 109)
			Me.createEmptyPanel.TabIndex = 6
			' 
			' chbHasRidgeCountsIndicator
			' 
			Me.chbHasRidgeCountsIndicator.AutoSize = True
			Me.chbHasRidgeCountsIndicator.Checked = True
			Me.chbHasRidgeCountsIndicator.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chbHasRidgeCountsIndicator.Location = New System.Drawing.Point(0, 53)
			Me.chbHasRidgeCountsIndicator.Name = "chbHasRidgeCountsIndicator"
			Me.chbHasRidgeCountsIndicator.Size = New System.Drawing.Size(149, 17)
			Me.chbHasRidgeCountsIndicator.TabIndex = 4
			Me.chbHasRidgeCountsIndicator.Text = "Has ridge counts indicator"
			Me.chbHasRidgeCountsIndicator.UseVisualStyleBackColor = True
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(0, 7)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(83, 13)
			Me.label3.TabIndex = 0
			Me.label3.Text = "Impression type:"
			' 
			' cbImpressionType
			' 
			Me.cbImpressionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbImpressionType.FormattingEnabled = True
			Me.cbImpressionType.Location = New System.Drawing.Point(89, 4)
			Me.cbImpressionType.Name = "cbImpressionType"
			Me.cbImpressionType.Size = New System.Drawing.Size(220, 21)
			Me.cbImpressionType.TabIndex = 1
			' 
			' chbContainsRidgeCounts
			' 
			Me.chbContainsRidgeCounts.AutoSize = True
			Me.chbContainsRidgeCounts.Location = New System.Drawing.Point(18, 76)
			Me.chbContainsRidgeCounts.Name = "chbContainsRidgeCounts"
			Me.chbContainsRidgeCounts.Size = New System.Drawing.Size(128, 17)
			Me.chbContainsRidgeCounts.TabIndex = 3
			Me.chbContainsRidgeCounts.Text = "Contains ridge counts"
			Me.chbContainsRidgeCounts.UseVisualStyleBackColor = True
			' 
			' chbHasMinutiae
			' 
			Me.chbHasMinutiae.AutoSize = True
			Me.chbHasMinutiae.Checked = True
			Me.chbHasMinutiae.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chbHasMinutiae.Location = New System.Drawing.Point(0, 31)
			Me.chbHasMinutiae.Name = "chbHasMinutiae"
			Me.chbHasMinutiae.Size = New System.Drawing.Size(153, 17)
			Me.chbHasMinutiae.TabIndex = 2
			Me.chbHasMinutiae.Text = "Contains standard minutiae"
			Me.chbHasMinutiae.UseVisualStyleBackColor = True
			' 
			' nfRecordOpenFileDialog
			' 
			Me.nfRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NFRecord Files (*.dat)|*.dat|All Files (*.*)|*." & "*"
			' 
			' ANType9RecordCreateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(334, 309)
			Me.Controls.Add(Me.createEmptyPanel)
			Me.Controls.Add(Me.rbCreateEmpty)
			Me.Controls.Add(Me.chbFmtFlag)
			Me.Controls.Add(Me.rbFromNFRecord)
			Me.Controls.Add(Me.fromNFRecordPanel)
			Me.Name = "ANType9RecordCreateForm"
			Me.Text = "Add Type-9 ANRecord"
			'			Me.Load += New System.EventHandler(Me.ANType9RecordCreateFormLoad);
			Me.Controls.SetChildIndex(Me.label1, 0)
			Me.Controls.SetChildIndex(Me.fromNFRecordPanel, 0)
			Me.Controls.SetChildIndex(Me.rbFromNFRecord, 0)
			Me.Controls.SetChildIndex(Me.chbFmtFlag, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			Me.Controls.SetChildIndex(Me.rbCreateEmpty, 0)
			Me.Controls.SetChildIndex(Me.createEmptyPanel, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.fromNFRecordPanel.ResumeLayout(False)
			Me.fromNFRecordPanel.PerformLayout()
			Me.createEmptyPanel.ResumeLayout(False)
			Me.createEmptyPanel.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private chbFmtFlag As System.Windows.Forms.CheckBox
		Private WithEvents rbFromNFRecord As System.Windows.Forms.RadioButton
		Private fromNFRecordPanel As System.Windows.Forms.Panel
		Private tbNFRecordPath As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private WithEvents btnBrowseNFRecord As System.Windows.Forms.Button
		Private WithEvents rbCreateEmpty As System.Windows.Forms.RadioButton
		Private createEmptyPanel As System.Windows.Forms.Panel
		Private chbHasMinutiae As System.Windows.Forms.CheckBox
		Private chbContainsRidgeCounts As System.Windows.Forms.CheckBox
		Private label3 As System.Windows.Forms.Label
		Private cbImpressionType As System.Windows.Forms.ComboBox
		Private nfRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
		Private chbHasRidgeCountsIndicator As System.Windows.Forms.CheckBox
	End Class
End Namespace