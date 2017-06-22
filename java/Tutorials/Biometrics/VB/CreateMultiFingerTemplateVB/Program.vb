Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Biometrics

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [templates ...] [NTemplate]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[templates] - one or more files containing fingerprint templates.")
		Console.WriteLine(Constants.vbTab & "[NTemplate] - filename of output file where NTemplate is saved.")
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			Dim nfTemplate = New NFTemplate()
			For i As Integer = 0 To args.Length - 2
				Dim template = New NTemplate(File.ReadAllBytes(args(i)))
				If template.Fingers IsNot Nothing Then
					For Each record As NFRecord In template.Fingers.Records
						nfTemplate.Records.Add(CType(record.Clone(), NFRecord))
					Next record
				End If
			Next i

			File.WriteAllBytes(args(args.Length - 1), nfTemplate.Save().ToArray())
			Console.WriteLine("Template successfully writen to {0}", args(args.Length - 1))

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		End Try
	End Function
End Class
