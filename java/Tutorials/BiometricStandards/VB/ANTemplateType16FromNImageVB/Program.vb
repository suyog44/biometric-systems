Imports Microsoft.VisualBasic
Imports System

Imports Neurotec.Biometrics.Standards
Imports Neurotec.Images
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn] [Src] [Udi]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[NImage]     - filename with image file.")
		Console.WriteLine(Constants.vbTab & "[ANTemplate] - filename for ANTemplate.")
		Console.WriteLine(Constants.vbTab & "[Tot] - specifies type of transaction.")
		Console.WriteLine(Constants.vbTab & "[Dai] - specifies destination agency identifier.")
		Console.WriteLine(Constants.vbTab & "[Ori] - specifies originating agency identifier.")
		Console.WriteLine(Constants.vbTab & "[Tcn] - specifies transaction control number.")
		Console.WriteLine(Constants.vbTab & "[Src] - specifies source agency number.")
		Console.WriteLine(Constants.vbTab & "[Udi] - specifies type of user-defined image.")
		Console.WriteLine("")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.Base"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 8 Then
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
			Dim udi As String = args(7)

			If (tot.Length < 3) OrElse (tot.Length > 4) Then
				Console.WriteLine("Tot parameter should be 3 or 4 characters length.")
				Return -1
			End If

			' Create empty ANTemplate object with only type 1 record in it
			Using template = New ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, 0)
				Using image As NImage = NImage.FromFile(args(0))
					' Convert to rgb image
					Using rgbImage As NImage = NImage.FromImage(NPixelFormat.Rgb8U, 0, image)
						rgbImage.ResolutionIsAspectRatio = True

						' Create Type 16 record
						Dim record = New ANType16Record(ANTemplate.VersionCurrent, 0, udi, src, BdifScaleUnits.None, ANImageCompressionAlgorithm.None, rgbImage)

						' Add Type 16 record to ANTemplate object
						template.Records.Add(record)

						' Store ANTemplate object with type 16 record in file
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
