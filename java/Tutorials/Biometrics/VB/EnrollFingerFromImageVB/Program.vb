Imports System
Imports Neurotec.Licensing
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Standards
Imports System.IO

Friend NotInheritable Class Program
	Private Sub New()
	End Sub
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine("\t{0} [input image] [output template] [format]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("\t[input image]    - image filename to extract.")
		Console.WriteLine("\t[output template] - filename to store extracted features.")
		Console.WriteLine("\t[format]   - whether proprietary or standard template should be created.")
		Console.WriteLine("\t\tIf not specified, proprietary Neurotechnology template is created (recommended).")
		Console.WriteLine("\t\tANSI for ANSI/INCITS 378-2004")
		Console.WriteLine("\t\tISO for ISO/IEC 19794-2")
		Console.WriteLine()
		Console.WriteLine("\texample: {0} image.jpg template.dat", TutorialUtils.GetAssemblyName())
		Console.WriteLine("\texample: {0} image.jpg isoTemplate.dat ISO", TutorialUtils.GetAssemblyName())

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FingerExtraction"
		Dim standard As BdifStandard = BdifStandard.Unspecified

		TutorialUtils.PrintTutorialHeader(args)
		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If
			If args.Length > 2 Then
				If args(2) = "ANSI" Then
					standard = BdifStandard.Ansi
				ElseIf args(2) = "ISO" Then
					standard = BdifStandard.Iso
				End If
			End If

			Using biometricClient = New NBiometricClient()
				Using subject = New NSubject()
					Using finger = New NFinger()
						'Read finger image from file and add it to NFinger object
						finger.FileName = args(0)

						'Read finger image from file and add it NSubject
						subject.Fingers.Add(finger)

						'Set finger template size (recommended, for enroll to database, is large) (optional)
						biometricClient.FingersTemplateSize = NTemplateSize.Large

						'Create template from added finger image
						Dim status = biometricClient.CreateTemplate(subject)
						If status = NBiometricStatus.Ok Then
							Console.WriteLine("{0} template extracted.",If(standard = BdifStandard.Iso, "ISO", If(standard = BdifStandard.Ansi, "ANSI", "Proprietary")))
						Else
							Console.WriteLine("Extraction failed: {0}", status)
							Return -1
						End If

						' save compressed template to file
						If standard = BdifStandard.Iso Then
							File.WriteAllBytes(args(1), subject.GetTemplateBuffer(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFingerMinutiaeRecordFormat, FMCRecord.VersionIsoCurrent).ToArray())
						ElseIf standard = BdifStandard.Ansi Then
							File.WriteAllBytes(args(1), subject.GetTemplateBuffer(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerMinutiaeU, FMRecord.VersionAnsiCurrent).ToArray())
						Else
							File.WriteAllBytes(args(1), subject.GetTemplateBuffer().ToArray())
						End If
						Console.WriteLine("template saved successfully")
					End Using
				End Using
			End Using

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class
