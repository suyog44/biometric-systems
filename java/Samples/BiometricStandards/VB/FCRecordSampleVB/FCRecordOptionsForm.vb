Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Neurotec.Biometrics.Standards

Partial Public Class FCRecordOptionsForm
	Inherits BdifOptionsForm
	Public Sub New()
		InitializeComponent()
		Dim versions = New List(Of StandardVersion)()

		Dim ansiV1 As StandardVersion = New StandardVersion()
		ansiV1.Standard = BdifStandard.Ansi
		ansiV1.Version = FCRecord.VersionAnsi10
		ansiV1.StandardName = "ANSI/INCITS 385-2004"

		Dim isoV1 As StandardVersion = New StandardVersion()
		isoV1.Standard = BdifStandard.Iso
		isoV1.Version = FCRecord.VersionIso10
		isoV1.StandardName = "ISO/IEC 19794-5:2005"

		Dim isoV3 As StandardVersion = New StandardVersion()
		isoV3.Standard = BdifStandard.Iso
		isoV3.Version = FCRecord.VersionIso30
		isoV3.StandardName = "ISO/IEC 19794-5:2011"

		versions.Add(ansiV1)
		versions.Add(isoV1)
		versions.Add(isoV3)

		StandardVersions = versions.ToArray()
	End Sub

	Public Overrides Property Flags() As UInteger
		Get
			'INSTANT VB NOTE: The local variable flags was renamed since Visual Basic will not allow local variables with the same name as their enclosing function or property:
			Dim flags_Renamed As UInteger = MyBase.Flags
			If cbProcessFirstFaceImageOnly.Checked Then
				flags_Renamed = flags_Renamed Or FCRecord.FlagProcessFirstFaceImageOnly
			End If
			If cbSkipFeaturePoints.Checked Then
				flags_Renamed = flags_Renamed Or FcrFaceImage.FlagSkipFeaturePoints
			End If
			Return flags_Renamed
		End Get
		Set(ByVal value As UInteger)
			If (value And FCRecord.FlagProcessFirstFaceImageOnly) = FCRecord.FlagProcessFirstFaceImageOnly Then
				cbProcessFirstFaceImageOnly.Checked = True
			End If
			If (value And FcrFaceImage.FlagSkipFeaturePoints) = FcrFaceImage.FlagSkipFeaturePoints Then
				cbSkipFeaturePoints.Checked = True
			End If
			MyBase.Flags = value
		End Set
	End Property
End Class
