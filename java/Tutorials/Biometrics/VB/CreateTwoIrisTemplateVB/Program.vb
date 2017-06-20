Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Licensing
Imports Neurotec.IO

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [left eye record] [right eye record] [template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[left eye record]  - filename of the left eye file with template.")
		Console.WriteLine(Constants.vbTab & "[right eye record] - filename of the right eye file with template.")
		Console.WriteLine(Constants.vbTab & "[template]        - filename for template.")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.IrisExtraction"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 3 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Dim nTemplate = New NTemplate()
			'create NTemplate
			Dim outputTemplate = New NTemplate()
			'create NETemplate
			Dim outputIrisesTemplate = New NETemplate()
			'set NETemplate to NTemplate
			outputTemplate.Irises = outputIrisesTemplate
			nTemplate.Irises = New NETemplate()

			For index As Integer = 0 To args.Length - 2
				'read NTemplate/NETemplate/NERecord from input file
				Dim hBuffer = New NBuffer(File.ReadAllBytes(args(index)))
				Dim newTemplate = New NTemplate(hBuffer)
				Dim irisTemplate = newTemplate.Irises

				'retrieve NETemplate from NTemplate
				Dim inputIrisesTemplate = nTemplate.Irises

				'retrieve NERecords count
				Dim inputRecordCount = irisTemplate.Records.Count
				Console.WriteLine("found {0} records in file {1}\n", inputRecordCount, args(index))

				For index2 As Integer = 0 To inputRecordCount - 1
					'add NERecord to output NETemplate
					outputIrisesTemplate.Records.Add(irisTemplate.Records(index2))
				Next
			Next

			' save compressed template to file
			File.WriteAllBytes(args(2), nTemplate.Save().ToArray())
			Console.WriteLine("Template successfully saved to file: {0}", args(2))

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class
