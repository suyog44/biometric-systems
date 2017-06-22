Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing
Imports Neurotec.IO

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [FMRecord] [NTemplate] [Standard] [FlagUseNeurotecFields]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[FMRecord] - filename of FMRecord.")
		Console.WriteLine(Constants.vbTab & "[NTemplate] - filename of NTemplate to be created.")
		Console.WriteLine(Constants.vbTab & "[Standard] - FMRecord standard (ISO or ANSI)")
		Console.WriteLine(Constants.vbTab & "[FlagUseNeurotecFields] - 1 if FmrFingerView.FlagUseNeurotecFields flag is used; otherwise, 0 flag was not used.")
		Console.WriteLine()
		Console.WriteLine("example:")
		Console.WriteLine(Constants.vbTab & "{0} fmrecord.dat ntemplate.dat ISO 1", TutorialUtils.GetAssemblyName())

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.FingerTemplates"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 4 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Dim fmRecordFileName As String = args(0)
			Dim outputFileName As String = args(1)
			Dim standard As BdifStandard = CType(System.Enum.Parse(GetType(BdifStandard), args(2), True), BdifStandard)

			Dim flagUseNeurotecFields As Integer = Integer.Parse(args(3))

			If fmRecordFileName = "/?" OrElse fmRecordFileName = "help" Then
				Return Usage()
			End If

			Dim storedFmRecord() As Byte = File.ReadAllBytes(fmRecordFileName)

			' Creating FMRecord object from FMRecord stored in memory
			Dim fmRecord As FMRecord = If(flagUseNeurotecFields = 1, New FMRecord(New NBuffer(storedFmRecord), FmrFingerView.FlagUseNeurotecFields, standard), New FMRecord(New NBuffer(storedFmRecord), standard))

			' Converting FMRecord object to NTemplate object
			Dim nTemplate As NTemplate = fmRecord.ToNTemplate()
			' Packing NTemplate object
			Dim packedNTemplate() As Byte = nTemplate.Save().ToArray()

			File.WriteAllBytes(outputFileName, packedNTemplate)

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class
