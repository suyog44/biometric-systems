Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Neurotec.Biometrics.Standards

Partial Public Class FIRecordOptionsForm
	Inherits BdifOptionsForm
	Public Sub New()
		InitializeComponent()
		Dim versions = New List(Of StandardVersion)()

		Dim ansiV1 As StandardVersion = New StandardVersion()
		ansiV1.Standard = BdifStandard.Ansi
		ansiV1.Version = FIRecord.VersionAnsi10
		ansiV1.StandardName = "ANSI/INCITS 381-2004"

		Dim ansiV2 As StandardVersion = New StandardVersion()
		ansiV1.Standard = BdifStandard.Ansi
		ansiV1.Version = FIRecord.VersionAnsi25
		ansiV1.StandardName = "ANSI/INCITS 381-2009"

		Dim isoV1 As StandardVersion = New StandardVersion()
		isoV1.Standard = BdifStandard.Iso
		isoV1.Version = FIRecord.VersionIso10
		isoV1.StandardName = "ISO/IEC 19794-4:2005"

		Dim isoV2 As StandardVersion = New StandardVersion()
		isoV2.Standard = BdifStandard.Iso
		isoV2.Version = FIRecord.VersionIso20
		isoV2.StandardName = "ISO/IEC 19794-4:2011"

		versions.Add(ansiV1)
		versions.Add(ansiV2)
		versions.Add(isoV1)
		versions.Add(isoV2)

		StandardVersions = versions.ToArray()
	End Sub

	Public Overrides Property Flags() As UInteger
		Get
			'INSTANT VB NOTE: The local variable flags was renamed since Visual Basic will not allow local variables with the same name as their enclosing function or property:
			Dim flags_Renamed As UInteger = MyBase.Flags
			If cbProcessFirstFingerOnly.Checked Then
				flags_Renamed = flags_Renamed Or FIRecord.FlagProcessFirstFingerOnly
			End If
			If cbProcessFirstFingerViewOnly.Checked Then
				flags_Renamed = flags_Renamed Or FIRecord.FlagProcessFirstFingerViewOnly
			End If
			Return flags_Renamed
		End Get
		Set(ByVal value As UInteger)
			If (value And FIRecord.FlagProcessFirstFingerOnly) = FIRecord.FlagProcessFirstFingerOnly Then
				cbProcessFirstFingerOnly.Checked = True
			End If
			If (value And FIRecord.FlagProcessFirstFingerViewOnly) = FIRecord.FlagProcessFirstFingerViewOnly Then
				cbProcessFirstFingerViewOnly.Checked = True
			End If
			MyBase.Flags = value
		End Set
	End Property

	Protected Overrides Sub OnModeChanged()
		MyBase.OnModeChanged()

		Select Case Mode
			Case BdifOptionsFormMode.New
				cbProcessFirstFingerOnly.Enabled = False
		End Select
	End Sub

#Region "IntializeComponent"

	Private gbFIRecord As System.Windows.Forms.GroupBox
	Private cbProcessFirstFingerViewOnly As System.Windows.Forms.CheckBox
	Private cbProcessFirstFingerOnly As System.Windows.Forms.CheckBox

	Private Sub InitializeComponent()
		Me.gbFIRecord = New System.Windows.Forms.GroupBox()
		Me.cbProcessFirstFingerViewOnly = New System.Windows.Forms.CheckBox()
		Me.cbProcessFirstFingerOnly = New System.Windows.Forms.CheckBox()
		Me.gbFIRecord.SuspendLayout()
		Me.SuspendLayout()
		' 
		' btnCancel
		' 
		Me.btnCancel.Location = New System.Drawing.Point(248, 220)
		' 
		' btnOk
		' 
		Me.btnOk.Location = New System.Drawing.Point(167, 220)
		' 
		' gbFIRecord
		' 
		Me.gbFIRecord.Controls.Add(Me.cbProcessFirstFingerViewOnly)
		Me.gbFIRecord.Controls.Add(Me.cbProcessFirstFingerOnly)
		Me.gbFIRecord.Location = New System.Drawing.Point(15, 139)
		Me.gbFIRecord.Name = "gbFIRecord"
		Me.gbFIRecord.Size = New System.Drawing.Size(308, 66)
		Me.gbFIRecord.TabIndex = 3
		Me.gbFIRecord.TabStop = False
		Me.gbFIRecord.Text = "FIRecord"
		' 
		' cbProcessFirstFingerViewOnly
		' 
		Me.cbProcessFirstFingerViewOnly.AutoSize = True
		Me.cbProcessFirstFingerViewOnly.Location = New System.Drawing.Point(6, 42)
		Me.cbProcessFirstFingerViewOnly.Name = "cbProcessFirstFingerViewOnly"
		Me.cbProcessFirstFingerViewOnly.Size = New System.Drawing.Size(157, 17)
		Me.cbProcessFirstFingerViewOnly.TabIndex = 1
		Me.cbProcessFirstFingerViewOnly.Text = "Process first fingerView only"
		Me.cbProcessFirstFingerViewOnly.UseVisualStyleBackColor = True
		' 
		' cbProcessFirstFingerOnly
		' 
		Me.cbProcessFirstFingerOnly.AutoSize = True
		Me.cbProcessFirstFingerOnly.Location = New System.Drawing.Point(6, 19)
		Me.cbProcessFirstFingerOnly.Name = "cbProcessFirstFingerOnly"
		Me.cbProcessFirstFingerOnly.Size = New System.Drawing.Size(134, 17)
		Me.cbProcessFirstFingerOnly.TabIndex = 0
		Me.cbProcessFirstFingerOnly.Text = "Process first finger only"
		Me.cbProcessFirstFingerOnly.UseVisualStyleBackColor = True
		' 
		' FIRecordOptionsForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.ClientSize = New System.Drawing.Size(335, 253)
		Me.Controls.Add(Me.gbFIRecord)
		Me.Name = "FIRecordOptionsForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "FIRecordOptionsForm"
		Me.Controls.SetChildIndex(Me.btnCancel, 0)
		Me.Controls.SetChildIndex(Me.btnOk, 0)
		Me.Controls.SetChildIndex(Me.gbFIRecord, 0)
		Me.gbFIRecord.ResumeLayout(False)
		Me.gbFIRecord.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

End Class
