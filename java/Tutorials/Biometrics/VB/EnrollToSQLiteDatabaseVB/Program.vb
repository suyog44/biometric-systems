Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.IO

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [template] [path to database file]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(vbTab & "template                   - template for enrollment")
		Console.WriteLine(vbTab & "path to database file      - path to SQLite database file")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			Using biometricClient = New NBiometricClient()
				' Read template
				Using subject As NSubject = CreateSubject(args(0), args(0))

					' Set the SQLite database
					biometricClient.SetDatabaseConnectionToSQLite(args(1))

					' Create enrollment task
					Dim enrollTask As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.Enroll, subject)

					' Perform task
					biometricClient.PerformTask(enrollTask)

					Dim status As NBiometricStatus = enrollTask.Status
					If status <> NBiometricStatus.Ok Then
						Console.WriteLine("Enrollment was unsuccessful. Status: {0}.", status)
						If enrollTask.Error IsNot Nothing Then
							Throw enrollTask.Error
						End If
						Return -1
					End If
					Console.WriteLine(String.Format("Enrollment was successful. The SQLite database conatins these IDs:"))

					' List of enrolled templates
					Dim subjects() As NSubject = biometricClient.List()
					For Each subj As NSubject In subjects
						Console.WriteLine(vbTab & "{0}", subj.Id)
					Next subj
				End Using
			End Using

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		End Try
	End Function

	Private Shared Function CreateSubject(ByVal fileName As String, ByVal subjectId As String) As NSubject
		Dim subject = New NSubject()
		subject.SetTemplateBuffer(New NBuffer(File.ReadAllBytes(fileName)))
		subject.Id = subjectId

		Return subject
	End Function
End Class
