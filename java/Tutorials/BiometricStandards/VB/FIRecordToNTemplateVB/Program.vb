Imports System
Imports System.IO

Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing
Imports Microsoft.VisualBasic

Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage: {0} [FIRecord] [NTemplate]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(vbTab & "[FIRecord]  - input FIRecord")
		Console.WriteLine(vbTab & "[NTemplate] - output NTemplate")

		Return 1
	End Function

	Shared Function Main(ByVal args As String()) As Integer
		Const components As String = "Biometrics.FingerExtraction,Biometrics.Standards.Fingers"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			If Not NLicense.ObtainComponents("/local", 5000, components) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", components))
			End If

			Using biometricClient = New NBiometricClient()
				Using subject = New NSubject()
					Dim fiRec As FIRecord

					' Read FIRecord from file
					Dim fiRecordData As Byte() = File.ReadAllBytes(args(0))

					' Create FIRecord
					fiRec = New FIRecord(fiRecordData, BdifStandard.Iso)

					' Read all images from FIRecord
					For Each fv As FirFingerView In fiRec.FingerViews
						Dim finger As NFinger = New NFinger()
						finger.Image = fv.ToNImage()
						subject.Fingers.Add(finger)
					Next

					' Set finger template size (recommended, for enroll to database, is large) (optional)
					biometricClient.FingersTemplateSize = NTemplateSize.Large

					' Create template from added finger image(s)
					Dim status = biometricClient.CreateTemplate(subject)
					Console.WriteLine(If(status = NBiometricStatus.Ok, "Template extracted", [String].Format("Extraction failed: {0}", status)))

					' Save template to file
					If status = NBiometricStatus.Ok Then
						File.WriteAllBytes(args(1), subject.GetTemplateBuffer().ToArray())
						Console.WriteLine("template saved successfully")
					End If
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
