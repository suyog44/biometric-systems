Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.IO
Imports System.Collections.Generic
Imports Neurotec.Licensing

Friend Class Program
	Private Shared ReadOnly MatchingComponents() As String = {"Biometrics.FingerMatching", "Biometrics.FaceMatching", "Biometrics.IrisMatching", "Biometrics.PalmMatching", "Biometrics.VoiceMatching"}

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [template] [path to database file]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(vbTab & "template                   - template for identification")
		Console.WriteLine(vbTab & "path to database file      - path to SQLite database file")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Dim obtainedLicenses As New List(Of String)()
		Try
			' Obtain licenses.
			For Each matchingComponent As String In MatchingComponents
				If NLicense.ObtainComponents("/local", 5000, matchingComponent) Then
					Console.WriteLine("Obtained license for component: {0}", matchingComponent)
					obtainedLicenses.Add(matchingComponent)
				End If
			Next matchingComponent
			If obtainedLicenses.Count = 0 Then
				Throw New NotActivatedException("Could not obtain any matching license")
			End If

			Using biometricClient = New NBiometricClient()
				' Read template
				Using subject As NSubject = CreateSubject(args(0), args(0))

					' Set the SQLite database
					biometricClient.SetDatabaseConnectionToSQLite(args(1))

					' Create identification task
					Dim identifyTask As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.Identify, subject)
					biometricClient.PerformTask(identifyTask)
					Dim status As NBiometricStatus = identifyTask.Status
					If status <> NBiometricStatus.Ok Then
						Console.WriteLine("Identification was unsuccessful. Status: {0}.", status)
						If identifyTask.Error IsNot Nothing Then
							Throw identifyTask.Error
						End If
						Return -1
					End If
					For Each matchingResult In subject.MatchingResults
						Console.WriteLine("Matched with ID: '{0}' with score {1}", matchingResult.Id, matchingResult.Score)
					Next matchingResult
				End Using
			End Using

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			For Each matchingComponent As String In obtainedLicenses
				NLicense.ReleaseComponents(matchingComponent)
			Next matchingComponent
		End Try
	End Function

	Private Shared Function CreateSubject(ByVal fileName As String, ByVal subjectId As String) As NSubject
		Dim subject = New NSubject()
		subject.SetTemplateBuffer(New NBuffer(File.ReadAllBytes(fileName)))
		subject.Id = subjectId

		Return subject
	End Function
End Class
