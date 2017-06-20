Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Images
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [srcImage] [dstImage] <optional: bitRate>", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(vbTab & "srcImage - filename of source finger image.")
		Console.WriteLine(vbTab & "dstImage - name of a file to save the created WSQ image to.")
		Console.WriteLine(vbTab & "bitRate  - specifies WSQ image compression level. Typical bit rates: 0.75, 2.25.")
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const components As String = "Images.WSQ"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			' Obtain license
			If (Not NLicense.ObtainComponents("/local", 5000, components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", components))
			End If

			' Create an NImage from file
			Using image As NImage = NImage.FromFile(args(0))
				' Create WSQInfo to store bit rate
				Using info As WsqInfo = CType(NImageFormat.Wsq.CreateInfo(image), WsqInfo)
					' Set specified bit rate (or default if bit rate was not specified).
					Dim bitrate As Single = WsqInfo.DefaultBitRate
					If args.Length > 2 Then
						bitrate = Single.Parse(args(2))
					End If
					info.BitRate = bitrate

					' Save image in WSQ format and bitrate to file.
					image.Save(args(1), info)
					Console.WriteLine("WSQ image with bit rate {0} was saved to {1}", bitrate, args(1))
				End Using
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(components)
		End Try
	End Function
End Class
