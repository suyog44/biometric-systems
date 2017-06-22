Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Images
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage: {0} [FCRecord] {{[image]}}", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "[FCRecord] - output FCRecord")
		Console.WriteLine(Constants.vbTab & "[image]    - one or more images")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.Faces"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Dim fc As FCRecord = Nothing
		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			For i As Integer = 1 To args.Length - 1
				Using imageFromFile As NImage = NImage.FromFile(args(i))
					Using image As NImage = NImage.FromImage(NPixelFormat.Grayscale8U, 0, imageFromFile)
						If fc Is Nothing Then
							' Specify standard and version to be used
							fc = New FCRecord(BdifStandard.Iso, FCRecord.VersionIso30)
						End If
						Dim img As New FcrFaceImage(fc.Standard, fc.Version)
						img.SetImage(image)
						fc.FaceImages.Add(img)
					End Using
				End Using
			Next i
			If fc IsNot Nothing Then
				File.WriteAllBytes(args(0), fc.Save().ToArray())

				Console.WriteLine("FCRecord saved to {0}", args(0))
			Else
				Console.WriteLine("no images were added to FCRecord")
			End If

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			If fc IsNot Nothing Then
				fc.Dispose()
			End If

			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class
