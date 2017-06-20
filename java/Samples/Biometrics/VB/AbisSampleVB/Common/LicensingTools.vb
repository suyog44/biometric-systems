Imports System.Collections.Generic
Imports Neurotec.Biometrics
Imports Neurotec.Licensing

Public NotInheritable Class LicensingTools
#Region "Private static fields"

	Private Shared components As New Dictionary(Of String, Boolean)()

#End Region

#Region "Public static methods"

	Public Shared Function IsComponentActivated(ByVal component As String) As Boolean
		If components.ContainsKey(component) Then
			Return components(component)
		End If

		Dim result As Boolean = NLicense.IsComponentActivated(component)
		components(component) = result
		Return result
	End Function

	Public Shared Function CanCreateFaceTemplate(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.FaceExtraction") OrElse (localOperations And NBiometricOperations.CreateTemplate) = 0
	End Function

	Public Shared Function CanDetectFaceSegments(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.FaceSegmentation") OrElse (localOperations And NBiometricOperations.DetectSegments) = 0
	End Function

	Public Shared Function CanCreatePalmTemplate(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.PalmExtraction") OrElse (localOperations And NBiometricOperations.CreateTemplate) = 0
	End Function

	Public Shared Function CanCreateIrisTemplate(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.IrisExtraction") OrElse (localOperations And NBiometricOperations.CreateTemplate) = 0
	End Function

	Public Shared Function CanSegmentIris(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.IrisSegmentation") OrElse (localOperations And NBiometricOperations.Segment) = 0
	End Function

	Public Shared Function CanCreateVoiceTemplate(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.VoiceExtraction") OrElse (localOperations And NBiometricOperations.CreateTemplate) = 0
	End Function

	Public Shared Function CanSegmentVoice(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.VoiceSegmentation") OrElse (localOperations And NBiometricOperations.Segment) = 0
	End Function

	Public Shared Function CanCreateFingerTemplate(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.FingerExtraction") OrElse (localOperations And NBiometricOperations.CreateTemplate) = 0
	End Function

	Public Shared Function CanSegmentFinger(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.FingerSegmentation") OrElse (localOperations And NBiometricOperations.Segment) = 0
	End Function

	Public Shared Function CanDetectFingerSegments(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.FingerSegmentsDetection") OrElse (localOperations And NBiometricOperations.DetectSegments) = 0
	End Function

	Public Shared Function CanAssessFingerQuality(ByVal localOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.FingerQualityAssessmentBase") OrElse (localOperations And NBiometricOperations.AssessQuality) = 0
	End Function

	Public Shared Function CanFingerBeMatched(ByVal remoteOperations As NBiometricOperations) As Boolean
		Return IsComponentActivated("Biometrics.FingerMatching") OrElse (remoteOperations And NBiometricOperations.VerifyOffline) = NBiometricOperations.VerifyOffline
	End Function

#End Region
End Class
