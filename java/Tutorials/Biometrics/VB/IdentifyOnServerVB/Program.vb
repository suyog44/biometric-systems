Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.IO

Friend Class Program
	Private Const DefaultAddress As String = "127.0.0.1"
	Private Const DefaultPort As Integer = 25452

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} -s [server:port] -t [template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "-s server:port   - matching server address (optional parameter, if address specified - port is optional)")
		Console.WriteLine(Constants.vbTab & "-t template      - template to be sent for identification (required)")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Dim templateFile As String = Nothing
		Dim server As String = Nothing
		Dim port As Integer

		Try
			ParseArgs(args, server, port, templateFile)
		Catch ex As Exception
			Console.WriteLine("error: {0}", ex)
			Usage()
			Return -1
		End Try

		Try
			Using biometricClient = New NBiometricClient()
				' Read template
				Using subject As NSubject = CreateSubject(templateFile, templateFile)
					' Create connection to server
					Dim connection = New NClusterBiometricConnection With {.Host = server, .Port = port}

					biometricClient.RemoteConnections.Add(connection)

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
		End Try
	End Function

	Private Shared Function CreateSubject(ByVal fileName As String, ByVal subjectId As String) As NSubject
		Dim subject = New NSubject()
		subject.SetTemplateBuffer(New NBuffer(File.ReadAllBytes(fileName)))
		subject.Id = subjectId

		Return subject
	End Function

	Private Shared Sub ParseArgs(ByVal args() As String, <System.Runtime.InteropServices.Out()> ByRef serverIp As String, <System.Runtime.InteropServices.Out()> ByRef serverAdminPort As Integer, <System.Runtime.InteropServices.Out()> ByRef templateFile As String)
		serverIp = DefaultAddress
		serverAdminPort = DefaultPort

		templateFile = String.Empty

		For i As Integer = 0 To args.Length - 1
			Dim optarg As String = String.Empty

			If args(i).Length <> 2 OrElse args(i).Chars(0) <> "-"c Then
				Throw New ApplicationException("parameter parse error")
			End If

			If args.Length > i + 1 AndAlso args(i + 1).Chars(0) <> "-"c Then
				optarg = args(i + 1) ' we have a parameter for given flag
			End If

			If optarg = String.Empty Then
				Throw New ApplicationException("parameter parse error")
			End If

			Select Case args(i).Chars(1)
				Case "s"c
					i += 1
					If optarg.Contains(":") Then
						Dim splitAddress() As String = optarg.Split(":"c)
						serverIp = splitAddress(0)
						serverAdminPort = Integer.Parse(splitAddress(1))
					Else
						serverIp = optarg
						serverAdminPort = DefaultPort
					End If
				Case "t"c
					i += 1
					templateFile = optarg
				Case Else
					Throw New ApplicationException("wrong parameter found!")
			End Select
		Next i

		If templateFile = String.Empty Then
			Throw New ApplicationException("template - required parameter - not specified")
		End If
	End Sub
End Class
