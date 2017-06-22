Imports System
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing
Imports System.Linq
Imports Microsoft.VisualBasic
Imports Neurotec.Images

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[NImage]     - filename with image file.")
		Console.WriteLine(Constants.vbTab & "[ANTemplate] - filename for ANTemplate.")
		Console.WriteLine(Constants.vbTab & "[Tot] - specifies type of transaction.")
		Console.WriteLine(Constants.vbTab & "[Dai] - specifies destination agency identifier.")
		Console.WriteLine(Constants.vbTab & "[Ori] - specifies originating agency identifier.")
		Console.WriteLine(Constants.vbTab & "[Tcn] - specifies transaction control number.")
		Console.WriteLine("")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FingerExtraction,Biometrics.Standards.FingerTemplates"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 6 Then
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

			If (tot.Length < 3) OrElse (tot.Length > 4) Then
				Console.WriteLine("Tot parameter should be 3 or 4 characters length.")
				Return -1
			End If

			Using biometricClient = New NBiometricClient()
				Using subject = New NSubject()
					Using finger = New NFinger()
						' Create empty ANTemplate object with only type 1 record in it
						Using template = New ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, 0)
							'Read finger image from file and add it to NFinger object
							finger.FileName = args(0)

							'Read finger image from file and add it NSubject
							subject.Fingers.Add(finger)

							'Create template from added finger image
							Dim status = biometricClient.CreateTemplate(subject)
							If status = NBiometricStatus.Ok Then
								Console.WriteLine("Template extracted")

								' Create Type 9 record
								Dim record = New ANType9Record(ANTemplate.VersionCurrent, 0, True, subject.GetTemplate().Fingers.Records.First())
								' Add Type 9 record to ANTemplate object
								template.Records.Add(record)

								' Store ANTemplate object with type 9 record in file
								template.Save(args(1))
							Else
								Console.WriteLine("Extraction failed: {0}", status)
							End If
						End Using
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
