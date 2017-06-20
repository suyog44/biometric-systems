Imports System
Imports System.IO

Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing
Imports Neurotec.Biometrics
Imports Microsoft.VisualBasic

Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage: {0} [IIRecord] [NTemplate]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(vbTab & "[IIRecord]  - input IIRecord")
		Console.WriteLine(vbTab & "[NTemplate] - output NTemplate")

		Return 1
	End Function

	Shared Function Main(ByVal args As String()) As Integer
		Const components As String = "Biometrics.IrisExtraction,Biometrics.Standards.Irises"

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
					Dim iiRec As IIRecord

					' Read IIRecord from file
					Dim iiRecordData As Byte() = File.ReadAllBytes(args(0))

					' Create IIRecord
					iiRec = New IIRecord(iiRecordData, BdifStandard.Iso)

					' Read all images from IIRecord
					For Each irisImage As IirIrisImage In iiRec.IrisImages
						Dim iris As NIris = New NIris()
						iris.Image = irisImage.ToNImage()
						subject.Irises.Add(iris)
					Next

					' Set iris template size (recommended, for enroll to database, is large) (optional)
					biometricClient.IrisesTemplateSize = NTemplateSize.Large

					' Create template from added iris image(s)
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
