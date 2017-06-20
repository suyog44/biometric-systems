Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing
Imports Neurotec.IO

Friend Class Program
	Private Shared ReadOnly MatchingComponents() As String = {"Biometrics.FingerMatching", "Biometrics.FaceMatching", "Biometrics.IrisMatching", "Biometrics.PalmMatching", "Biometrics.VoiceMatching"}

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [reference template] [candidate template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Dim obtainedLicenses = New List(Of String)()
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
				' Read templates
				Using probeSubject As NSubject = CreateSubject(args(0), args(0))
					Using candidateSubject As NSubject = CreateSubject(args(1), args(1))
						' Set matching threshold
						biometricClient.MatchingThreshold = 48

						' Set paramater to return matching details
						biometricClient.MatchingWithDetails = True

						' Identify probe subject
						Dim status As NBiometricStatus = biometricClient.Verify(probeSubject, candidateSubject)
						If status = NBiometricStatus.Ok Then
							For Each matchingResult In probeSubject.MatchingResults
								Console.WriteLine("Matched with ID: '{0}' with score {1}", matchingResult.Id, matchingResult.Score)
								If matchingResult.MatchingDetails IsNot Nothing Then
									Console.WriteLine(MatchingDetailsToString(matchingResult.MatchingDetails))
								End If
							Next matchingResult
						ElseIf status = NBiometricStatus.MatchNotFound Then
							Console.WriteLine("Match not found!")
						Else
							Console.WriteLine("Verification failed! Status: {0}", status)
							Return -1
						End If
					End Using
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

	Private Shared Function MatchingDetailsToString(ByVal details As NMatchingDetails) As String
		Dim detailsStr = New StringBuilder()
		If (details.BiometricType And NBiometricType.Finger) = NBiometricType.Finger Then
			detailsStr.Append("    Fingerprint match details:")
			detailsStr.AppendLine(String.Format(" score = {0}", details.FingersScore))
			For Each fngrDetails As NFMatchingDetails In details.Fingers
				detailsStr.AppendLine(String.Format("    fingerprint index: {0}; score: {1};", fngrDetails.MatchedIndex, fngrDetails.Score))
			Next fngrDetails
		End If
		If (details.BiometricType And NBiometricType.Face) = NBiometricType.Face Then
			detailsStr.Append("    Face match details:")
			detailsStr.AppendLine(String.Format(" face index: {0}; score: {1}", details.FacesMatchedIndex, details.FacesScore))
			For Each faceDetails As NLMatchingDetails In details.Faces
				detailsStr.AppendLine(String.Format("    face index: {0}; score: {1};", faceDetails.MatchedIndex, faceDetails.Score))
			Next faceDetails
		End If
		If (details.BiometricType And NBiometricType.Iris) = NBiometricType.Iris Then
			detailsStr.Append("    Irises match details:")
			detailsStr.AppendLine(String.Format(" score = {0}", details.IrisesScore))
			For Each irisesDetails As NEMatchingDetails In details.Irises
				detailsStr.AppendLine(String.Format("    irises index: {0}; score: {1};", irisesDetails.MatchedIndex, irisesDetails.Score))
			Next irisesDetails
		End If
		If (details.BiometricType And NBiometricType.Palm) = NBiometricType.Palm Then
			detailsStr.Append("    Palmprint match details:")
			detailsStr.AppendLine(String.Format(" score = {0}", details.PalmsScore))
			For Each fngrDetails As NFMatchingDetails In details.Palms
				detailsStr.AppendLine(String.Format("    palmprint index: {0}; score: {1};", fngrDetails.MatchedIndex, fngrDetails.Score))
			Next fngrDetails
		End If
		If (details.BiometricType And NBiometricType.Voice) = NBiometricType.Voice Then
			detailsStr.Append("    Voice match details:")
			detailsStr.AppendLine(String.Format(" score = {0}", details.VoicesScore))
			For Each voicesDetails As NSMatchingDetails In details.Voices
				detailsStr.AppendLine(String.Format("    voices index: {0}; score: {1};", voicesDetails.MatchedIndex, voicesDetails.Score))
			Next voicesDetails
		End If
		Return detailsStr.ToString()
	End Function
End Class
