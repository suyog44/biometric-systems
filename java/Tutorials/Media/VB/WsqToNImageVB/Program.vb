Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Images
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [srcImage] [dstImage]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(vbTab & "srcImage - filename of source WSQ image.")
		Console.WriteLine(vbTab & "dstImage - name of a file to save converted image to.")
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

			' Create an NImage from a WSQ image file
			Using image As NImage = NImage.FromFile(args(0), NImageFormat.Wsq)
				Console.WriteLine("loaded wsq bitrate: {0}", (CType(image.Info, WsqInfo)).BitRate)
				' Pick a format to save in, e.g. JPEG
				Dim dstFormat As NImageFormat = NImageFormat.Jpeg
				' Save image to specified file
				image.Save(args(1), dstFormat)
				Console.WriteLine("{0} image was saved to {1}", dstFormat.Name, args(1))
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(components)
		End Try
	End Function
End Class
