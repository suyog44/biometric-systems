Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO
Imports Neurotec.Licensing

Friend Class Program
	Private Shared ReadOnly Components() As String = {"Biometrics.Standards.Base", "Biometrics.Standards.Irises", "Biometrics.Standards.Fingers", "Biometrics.Standards.Faces", "Biometrics.Standards.Palms", "Biometrics.IrisExtraction", "Biometrics.FingerExtraction", "Biometrics.FaceExtraction", "Biometrics.PalmExtraction"}

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [CbeffRecord] [PatronFormat] [NTemplate]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[CbeffRecord] - filename of CbeffRecord.")
		Console.WriteLine(Constants.vbTab & "[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation).")
		Console.WriteLine(Constants.vbTab & "[NTemplate] - filename of NTemplate.")
		Console.WriteLine("")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 3 Then
			Return Usage()
		End If

		Dim obtainedLicenses = New List(Of String)()
		Try
			' Obtain licenses.
			For Each component As String In Components
				If NLicense.ObtainComponents("/local", 5000, component) Then
					Console.WriteLine("Obtained license for component: {0}", component)
					obtainedLicenses.Add(component)
				End If
			Next component
			If obtainedLicenses.Count = 0 Then
				Throw New NotActivatedException("Could not obtain any matching license")
			End If

			' Read CbeffRecord buffer
			Dim packedCbeffRecord = New NBuffer(File.ReadAllBytes(args(0)))

			' Get CbeffRecord patron format
			' all supported patron formats can be found in CbeffRecord class documentation
			Dim patronFormat As UInteger = UInteger.Parse(args(1), Globalization.NumberStyles.AllowHexSpecifier)

			' Creating CbeffRecord object from NBuffer object
			Using cbeffRecord = New CbeffRecord(packedCbeffRecord, patronFormat)
				Using subject = New NSubject()
					Using engine = New NBiometricEngine()
						' Setting CbeffRecord
						subject.SetTemplate(cbeffRecord)

						' Extracting template details from specified CbeffRecord data
						engine.CreateTemplate(subject)

						If subject.Status = NBiometricStatus.Ok Then
							File.WriteAllBytes(args(2), subject.GetTemplateBuffer().ToArray())
							Console.WriteLine("Template successfully saved")
						Else
							Console.WriteLine("Template creation failed! Status: {0}", subject.Status)
							Return -1
						End If
					End Using
				End Using
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			For Each component As String In obtainedLicenses
				NLicense.ReleaseComponents(component)
			Next component
		End Try
	End Function
End Class
