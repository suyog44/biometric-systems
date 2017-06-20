Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Neurotec.Biometrics.Standards

Partial Public Class IIRecordOptionsForm
	Inherits BdifOptionsForm
	Public Sub New()
		InitializeComponent()
		Dim versions = New List(Of StandardVersion)()

		Dim ansiV1 As StandardVersion = New StandardVersion()
		ansiV1.Standard = BdifStandard.Ansi
		ansiV1.Version = IIRecord.VersionAnsi10
		ansiV1.StandardName = "ANSI/INCITS 379-2004"

		Dim isoV1 As StandardVersion = New StandardVersion()
		isoV1.Standard = BdifStandard.Iso
		isoV1.Version = IIRecord.VersionIso10
		isoV1.StandardName = "ISO/IEC 19794-6:2005"

		Dim isoV2 As StandardVersion = New StandardVersion()
		isoV2.Standard = BdifStandard.Iso
		isoV2.Version = IIRecord.VersionIso20
		isoV2.StandardName = "ISO/IEC 19794-6:2011"

		versions.Add(ansiV1)
		versions.Add(isoV1)
		versions.Add(isoV2)

		StandardVersions = versions.ToArray()
	End Sub

	Public Overrides Property Flags() As UInteger
		Get
			Dim flags_Renamed As UInteger = MyBase.Flags
			If cbProcessFirstIrisImageOnly.Checked Then
				flags_Renamed = flags_Renamed Or IIRecord.FlagProcessIrisFirstIrisImageOnly
			End If

			Return flags_Renamed
		End Get
		Set(ByVal value As UInteger)
			If (value And IIRecord.FlagProcessIrisFirstIrisImageOnly) = IIRecord.FlagProcessIrisFirstIrisImageOnly Then
				cbProcessFirstIrisImageOnly.Checked = True
			End If
			MyBase.Flags = value
		End Set
	End Property

	Protected Overrides Sub OnModeChanged()
		MyBase.OnModeChanged()

		Select Case Mode
			Case BdifOptionsFormMode.New
				cbProcessFirstIrisImageOnly.Enabled = False
		End Select
	End Sub
End Class
