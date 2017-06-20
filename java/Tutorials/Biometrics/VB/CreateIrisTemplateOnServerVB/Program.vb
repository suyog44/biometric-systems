Imports System
Imports System.IO
Imports Microsoft.VisualBasic
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.IO

Namespace Neurotec.Biometrics
	Friend NotInheritable Class Program
		Private Const DefaultAddress As String = "127.0.0.1"
		Private Const DefaultAdminPort As Integer = 24932

		Private Sub New()
		End Sub
		Private Shared Function Usage() As Integer
			Console.WriteLine("usage:")
			Console.WriteLine("	{0} -s [server:port] -i [input image] -t [output template]", TutorialUtils.GetAssemblyName())
			Console.WriteLine()
			Console.WriteLine(Constants.vbTab & "-s [server:port]   - matching server address (optional parameter, if address specified - port is optional)")
			Console.WriteLine(Constants.vbTab & "-i [image]   - image filename to store iris image.")
			Console.WriteLine(Constants.vbTab & "-t [output template]   - filename to store iris template.")
			Return 1
		End Function

		Shared Function Main(ByVal args() As String) As Integer

			TutorialUtils.PrintTutorialHeader(args)
			If args.Length < 3 Then
				Return Usage()
			End If

			Dim server As String = DefaultAddress
			Dim adminPort As Integer = DefaultAdminPort
			Dim templateFile As String = "template.dat"
			Dim imageFile As String = "image.jpg"
			Try
				ParseArgs(args, server, adminPort, imageFile, templateFile)
			Catch ex As Exception
				Console.WriteLine("error: {0}", ex)
				Return Usage()
			End Try

			Try
				Using biometricClient = New NBiometricClient()
					Using subject = New NSubject()
						Using Iris = New NIris()
							' perform all biometric operations on remote server only
							biometricClient.LocalOperations = NBiometricOperations.None
							Dim connection = New NClusterBiometricConnection With {.Host = server, .AdminPort = adminPort}
							biometricClient.RemoteConnections.Add(connection)

							Iris.SampleBuffer = New NBuffer(File.ReadAllBytes(imageFile))
							subject.Irises.Add(Iris)
							biometricClient.IrisesTemplateSize = NTemplateSize.Large

							Dim status = biometricClient.CreateTemplate(subject)

							If status = NBiometricStatus.Ok Then
								Console.WriteLine("Template extracted")
							Else
								Console.WriteLine("Extraction failed: {0}", status)
								Return -1
							End If

							File.WriteAllBytes(templateFile, subject.GetTemplateBuffer().ToArray())
							Console.WriteLine("Template saved successfully")
						End Using
					End Using
				End Using

				Return 0
			Catch ex As Exception
				Return TutorialUtils.PrintException(ex)
			End Try
		End Function

		Private Shared Sub ParseArgs(ByVal args() As String, <System.Runtime.InteropServices.Out()> ByRef serverIp As String, <System.Runtime.InteropServices.Out()> ByRef adminPort As Integer, <System.Runtime.InteropServices.Out()> ByRef imageFile As String, <System.Runtime.InteropServices.Out()> ByRef templateFile As String)
			serverIp = DefaultAddress
			adminPort = DefaultAdminPort

			imageFile = String.Empty
			templateFile = String.Empty

			For i As Integer = 0 To args.Length - 1
				Dim optarg As String = String.Empty

				If args(i).Length <> 2 OrElse args(i).Chars(0) <> "-"c Then
					Throw New Exception("parameter parse error")
				End If

				If args.Length > i + 1 AndAlso args(i + 1).Chars(0) <> "-"c Then
					optarg = args(i + 1) ' we have a parameter for given flag
				End If

				If optarg = String.Empty Then
					Throw New Exception("parameter parse error")
				End If

				Select Case args(i).Chars(1)
					Case "s"c
						i += 1
						If optarg.Contains(":") Then
							Dim splitAddress() As String = optarg.Split(":"c)
							serverIp = splitAddress(0)
							adminPort = Integer.Parse(splitAddress(1))
						Else
							serverIp = optarg
							adminPort = DefaultAdminPort
						End If
					Case "i"c
						i += 1
						imageFile = optarg
					Case "t"c
						i += 1
						templateFile = optarg
					Case Else
						Throw New Exception("wrong parameter found!")
				End Select
			Next i

			If templateFile = String.Empty Then
				Throw New Exception("template - required parameter - not specified")
			End If

			If imageFile = String.Empty Then
				Throw New Exception("image - required parameter - not specified")
			End If
		End Sub
	End Class
End Namespace
