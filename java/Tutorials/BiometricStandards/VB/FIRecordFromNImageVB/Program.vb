Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Images
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage: {0} [FIRecord] [Standard] [Version] {{[image]}}", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "[FIRecord] - output FIRecord")
		Console.WriteLine(Constants.vbTab & "[Standard] - standard for the record (ANSI or ISO)")
		Console.WriteLine(Constants.vbTab & "[Version] - version for the record")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & " 1 - ANSI/INCITS 381-2004")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & " 2.5 - ANSI/INCITS 381-2009")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & " 1 - ISO/IEC 19794-4:2005")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & " 2 - ISO/IEC 19794-4:2011")
		Console.WriteLine(Constants.vbTab & "[image]    - one or more images")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.Fingers"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 4 Then
			Return Usage()
		End If

		Dim fi As FIRecord = Nothing
		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Dim standard = CType(System.Enum.Parse(GetType(BdifStandard), args(1), True), BdifStandard)
			Dim version As NVersion
			Select Case args(2)
				Case "1"
					version = If(standard = BdifStandard.Ansi, FIRecord.VersionAnsi10, FIRecord.VersionIso10)
				Case "2"
					If standard <> BdifStandard.Iso Then
						Throw New ArgumentException("Standard and version is incompatible")
					End If
					version = FIRecord.VersionIso20
				Case "2.5"
					If standard <> BdifStandard.Ansi Then
						Throw New ArgumentException("Standard and version is incompatible")
					End If
					version = FIRecord.VersionAnsi25
				Case Else
					Throw New ArgumentException("Version was not recognised")
			End Select

			For i As Integer = 3 To args.Length - 1
				Using imageFromFile As NImage = NImage.FromFile(args(i))
					Using grayscaleImage As NImage = NImage.FromImage(NPixelFormat.Grayscale8U, 0, imageFromFile)
						If grayscaleImage.ResolutionIsAspectRatio OrElse grayscaleImage.HorzResolution < 250 OrElse grayscaleImage.VertResolution < 250 Then
							grayscaleImage.HorzResolution = 500
							grayscaleImage.VertResolution = 500
							grayscaleImage.ResolutionIsAspectRatio = False
						End If

						If fi Is Nothing Then
							fi = New FIRecord(standard, version)
							If IsRecordFirstVersion(fi) Then
								fi.PixelDepth = 8
								fi.HorzImageResolution = CUShort(grayscaleImage.HorzResolution)
								fi.HorzScanResolution = CUShort(grayscaleImage.HorzResolution)
								fi.VertImageResolution = CUShort(grayscaleImage.VertResolution)
								fi.VertScanResolution = CUShort(grayscaleImage.VertResolution)
							End If
						End If
						Dim fingerView As New FirFingerView(fi.Standard, fi.Version)
						If (Not IsRecordFirstVersion(fi)) Then
							fingerView.PixelDepth = 8
							fingerView.HorzImageResolution = CUShort(grayscaleImage.HorzResolution)
							fingerView.HorzScanResolution = CUShort(grayscaleImage.HorzResolution)
							fingerView.VertImageResolution = CUShort(grayscaleImage.VertResolution)
							fingerView.VertScanResolution = CUShort(grayscaleImage.VertResolution)
						End If
						fi.FingerViews.Add(fingerView)
						fingerView.SetImage(grayscaleImage)
					End Using
				End Using
			Next i

			If fi IsNot Nothing Then
				File.WriteAllBytes(args(0), fi.Save().ToArray())
				Console.WriteLine("FIRecord saved to {0}", args(0))
			Else
				Console.WriteLine("no images were added to FIRecord")
			End If

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			If fi IsNot Nothing Then
				fi.Dispose()
			End If

			NLicense.ReleaseComponents(Components)
		End Try
	End Function

	Private Shared Function IsRecordFirstVersion(ByVal record As FIRecord) As Boolean
		Return record.Standard = BdifStandard.Ansi AndAlso record.Version = FIRecord.VersionAnsi10 OrElse record.Standard = BdifStandard.Iso AndAlso record.Version = FIRecord.VersionIso10
	End Function
End Class
