Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [NTemplate] [CbeffRecord] [PatronFormat]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[NTemplate] - filename of NTemplate.")
		Console.WriteLine(Constants.vbTab & "[CbeffRecord] - filename of CbeffRecord.")
		Console.WriteLine(Constants.vbTab & "[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation).")
		Console.WriteLine("")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.Base"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 3 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			' Read NTemplate buffer
			Dim packedNTemplate = New NBuffer(File.ReadAllBytes(args(0)))

			' Combine NTemplate BDB format
			Dim bdbFormat As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.Neurotechnologija, CbeffBdbFormatIdentifiers.NeurotechnologijaNTemplate)

			' Get CbeffRecord patron format
			' all supported patron formats can be found in CbeffRecord class documentation
			Dim patronFormat As UInteger = UInteger.Parse(args(2), Globalization.NumberStyles.AllowHexSpecifier)

			' Create CbeffRecord from NTemplate buffer
			Using cbeffRecord = New CbeffRecord(bdbFormat, packedNTemplate, patronFormat)
				' Saving NTemplate
				File.WriteAllBytes(args(1), cbeffRecord.Save().ToArray())
				Console.WriteLine("Template successfully saved")
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class
