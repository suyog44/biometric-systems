Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Images
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn] [Src]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[NImage]     - filename with Image file.")
		Console.WriteLine(Constants.vbTab & "[ANTemplate] - filename for ANTemplate.")
		Console.WriteLine(Constants.vbTab & "[Tot] - specifies type of transaction.")
		Console.WriteLine(Constants.vbTab & "[Dai] - specifies destination agency identifier.")
		Console.WriteLine(Constants.vbTab & "[Ori] - specifies originating agency identifier.")
		Console.WriteLine(Constants.vbTab & "[Tcn] - specifies transaction control number.")
		Console.WriteLine(Constants.vbTab & "[Src] - specifies source agency number.")
		Console.WriteLine("")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.Palms"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 7 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Dim tot As String = args(2)	' type of transaction
			Dim dai As String = args(3)	' destination agency identifier
			Dim ori As String = args(4)	' originating agency identifier
			Dim tcn As String = args(5)	' transaction control number
			Dim src As String = args(6)

			If (tot.Length < 3) OrElse (tot.Length > 4) Then
				Console.WriteLine("Tot parameter should be 3 or 4 characters length.")
				Return -1
			End If

			' Create empty ANTemplate object with only type 1 record in it
			Using template = New ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, 0)
				Using image As NImage = NImage.FromFile(args(0))
					' Convert to grayscale images
					Using hrGrayImage As NImage = NImage.FromImage(NPixelFormat.Grayscale8U, 0, image)
						hrGrayImage.HorzResolution = 500
						hrGrayImage.VertResolution = 500
						hrGrayImage.ResolutionIsAspectRatio = False

						' Create Type 15 record
						Dim record = New ANType15Record(ANTemplate.VersionCurrent, 0, src, BdifScaleUnits.PixelsPerInch, ANImageCompressionAlgorithm.None, hrGrayImage)

						' Add Type 15 record to ANTemplate object
						template.Records.Add(record)

						' Store ANTemplate object with type 15 record in file
						template.Save(args(1))
					End Using
				End Using
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class
