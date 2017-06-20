Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Images
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [ANTemplate]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[ATemplate] - filename of ANTemplate.")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.Base,Biometrics.Standards.PalmTemplates,Biometrics.Standards.Irises,Biometrics.Standards.Faces"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 1 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using template = New ANTemplate(args(0), ANValidationLevel.Standard)
				For i As Integer = 0 To template.Records.Count - 1
					Dim record As ANRecord = template.Records(i)
					Dim image As NImage = Nothing
					Dim number As Integer = record.RecordType.Number
					If number >= 3 AndAlso number <= 8 AndAlso number <> 7 Then
						image = (CType(record, ANImageBinaryRecord)).ToNImage()
					ElseIf number >= 10 AndAlso number <= 17 Then
						image = (CType(record, ANImageAsciiBinaryRecord)).ToNImage()
					End If

					If image IsNot Nothing Then
						Dim fileName As String = String.Format("record{0}_type{1}.jpg", i + 1, number)
						image.Save(fileName)
						image.Dispose()
						Console.WriteLine("Image saved to {0}", fileName)
					End If
				Next i
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class
