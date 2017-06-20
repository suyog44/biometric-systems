Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Images
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage: {0} [IIRecord] [Standard] [Version] [image]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "[IIRecord] - output IIRecord")
		Console.WriteLine(Constants.vbTab & "[Standard] - standard for the record (ANSI or ISO)")
		Console.WriteLine(Constants.vbTab & "[Version] - version for the record")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & " 1 - ANSI/INCITS 379-2004")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & " 1 - ISO/IEC 19794-6:2005")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & " 2 - ISO/IEC 19794-6:2011")
		Console.WriteLine(Constants.vbTab & "[image]    - one or more images")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.Irises"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 4 Then
			Return Usage()
		End If

		Dim iiRec As IIRecord = Nothing
		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Dim standard = CType(System.Enum.Parse(GetType(BdifStandard), args(1), True), BdifStandard)
			Dim version As NVersion
			Select Case args(2)
				Case "1"
					version = If(standard = BdifStandard.Ansi, IIRecord.VersionAnsi10, IIRecord.VersionIso10)
				Case "2"
					If standard <> BdifStandard.Iso Then
						Throw New ArgumentException("Standard and version is incompatible")
					End If
					version = IIRecord.VersionIso20
				Case Else
					Throw New ArgumentException("Version was not recognised")
			End Select

			For i As Integer = 3 To args.Length - 1
				Using imageFromFile As NImage = NImage.FromFile(args(i))
					Using image As NImage = NImage.FromImage(NPixelFormat.Grayscale8U, 0, imageFromFile)
						If iiRec Is Nothing Then
							iiRec = New IIRecord(standard, version)
							If IsRecordFirstVersion(iiRec) Then
								iiRec.RawImageHeight = CUShort(image.Height)
								iiRec.RawImageWidth = CUShort(image.Width)
								iiRec.IntensityDepth = 8
							End If
						End If
						Dim iirIrisImage = New IirIrisImage(iiRec.Standard, iiRec.Version)
						If (Not IsRecordFirstVersion(iiRec)) Then
							iirIrisImage.ImageWidth = CUShort(image.Width)
							iirIrisImage.ImageHeight = CUShort(image.Height)
							iirIrisImage.IntensityDepth = 8
						End If

						iirIrisImage.SetImage(image)
						iiRec.IrisImages.Add(iirIrisImage)
					End Using
				End Using
			Next i

			If iiRec IsNot Nothing Then
				File.WriteAllBytes(args(0), iiRec.Save().ToArray())

				Console.WriteLine("IIRecord saved to {0}", args(0))
			Else
				Console.WriteLine("no images were added to IIRecord")
			End If

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			If iiRec IsNot Nothing Then
				iiRec.Dispose()
			End If

			NLicense.ReleaseComponents(Components)
		End Try
	End Function

	Private Shared Function IsRecordFirstVersion(ByVal record As IIRecord) As Boolean
		Return record.Standard = BdifStandard.Ansi AndAlso record.Version = IIRecord.VersionAnsi10 OrElse record.Standard = BdifStandard.Iso AndAlso record.Version = IIRecord.VersionIso10
	End Function
End Class
