Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Images
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [filename]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(vbTab & "filename - image filename.")
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const components As String = "Images.WSQ,Images.IHead,Images.JPEG2000"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 1 Then
			Return Usage()
		End If

		Dim doRelease As Boolean = False
		Try
			' Obtain license (optional)
			If (Not NLicense.ObtainComponents("/local", 5000, components)) Then
				Console.WriteLine("Could not obtain licenses for components: {0}", components)
			Else
				doRelease = True
			End If

			' Create NImage with info from file
			Using image = NImage.FromFile(args(0))
				' Get image format
				Dim format As NImageFormat = image.Info.Format

				' Print info common to all formats
				Console.WriteLine("Format: {0}", format.Name)

				' Print format specific info.
				If NImageFormat.Jpeg2K.Equals(format) Then
					Dim info As Jpeg2KInfo = CType(image.Info, Jpeg2KInfo)
					Console.WriteLine("Profile: {0}", info.Profile)
					Console.WriteLine("Compression ratio: {0}", info.Ratio)
				ElseIf NImageFormat.Jpeg.Equals(format) Then
					Dim info As JpegInfo = CType(image.Info, JpegInfo)
					Console.WriteLine("Lossless: {0}", info.IsLossless)
					Console.WriteLine("Quality: {0}", info.Quality)
				ElseIf NImageFormat.Png.Equals(format) Then
					Dim info As PngInfo = CType(image.Info, PngInfo)
					Console.WriteLine("Compression level: {0}", info.CompressionLevel)
				ElseIf NImageFormat.Wsq.Equals(format) Then
					Dim info As WsqInfo = CType(image.Info, WsqInfo)
					Console.WriteLine("Bit rate: {0}", info.BitRate)
					Console.WriteLine("Implementation number: {0}", info.ImplementationNumber)
				End If
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			If doRelease Then
				NLicense.ReleaseComponents(components)
			End If
		End Try
	End Function
End Class
