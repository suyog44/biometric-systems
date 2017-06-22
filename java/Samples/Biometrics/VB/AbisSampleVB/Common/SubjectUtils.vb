Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Linq
Imports Neurotec.Biometrics

Public Class SubjectUtils
#Region "Private constructor"

	Private Sub New()
	End Sub

#End Region

#Region "Generalization helper functions"

	Public Shared Function IsBiometricGeneralizationResult(ByVal biometric As NBiometric) As Boolean
		If biometric.SessionId = -1 Then
			Dim parentObject As NBiometricAttributes = biometric.ParentObject
			If parentObject IsNot Nothing Then
				Dim owner As NBiometric = CType(parentObject.Owner, NBiometric)
				Return owner IsNot Nothing AndAlso owner.SessionId <> -1
			End If
		End If
		Return False
	End Function

	Public Shared Function IsBiometricGeneralizationSource(ByVal biometric As NBiometric) As Boolean
		Return biometric.SessionId <> -1
	End Function

	Public Shared Function GetFaceGeneralizationGroups(ByVal subject As NSubject) As IEnumerable(Of NFace())
		Return GetFaceGeneralizationGroups(subject.Faces.ToArray())
	End Function

	Public Shared Function GetFaceGeneralizationGroups(ByVal allFaces() As NFace) As IEnumerable(Of NFace())
		Dim result = New List(Of NFace())()
		For Each item In allFaces.Where(Function(x) x.SessionId = -1 AndAlso x.ParentObject Is Nothing)
			result.Add(New NFace() {item})
		Next item

		Dim ids = allFaces.Select(Function(x) x.SessionId).Distinct().Except(New Integer() {-1}).ToArray()
		For Each id In ids
			Dim localId = id
			Dim faces() As NFace = allFaces.Where(Function(x) x.SessionId = localId).ToArray()
			Dim withParent = faces.Where(Function(x) x.ParentObject IsNot Nothing)
			Dim withoutParent = faces.Where(Function(x) x.ParentObject Is Nothing)
			If withParent.Any() Then
				result.Add(withParent.ToArray())
			End If
			If withoutParent.Any() Then
				result.Add(withoutParent.ToArray())
			End If
		Next id
		Return result
	End Function

	Public Shared Function GetFingersGeneralizationGroups(ByVal subject As NSubject) As IEnumerable(Of NFinger())
		Return GetFingersGeneralizationGroups(subject.Fingers.ToArray())
	End Function

	Public Shared Function GetFingersGeneralizationGroups(ByVal fingers() As NFinger) As IEnumerable(Of NFinger())
		Return GetFrictionRidgeGeneralizationGroups(fingers)
	End Function

	Public Shared Function GetPalmsGeneralizationGroups(ByVal subject As NSubject) As IEnumerable(Of NPalm())
		Return GetPalmsGeneralizationGroups(subject.Palms.ToArray())
	End Function

	Public Shared Function GetPalmsGeneralizationGroups(ByVal palms() As NPalm) As IEnumerable(Of NPalm())
		Return GetFrictionRidgeGeneralizationGroups(palms)
	End Function

	Public Shared Function GetFingersInSameGroup(ByVal fingers() As NFinger, ByVal finger As NFinger) As IEnumerable(Of NFinger)
		Return GetFrictionRidgesInSameGroup(fingers, finger)
	End Function

	Public Shared Function GetPalmsInSameGroup(ByVal palms() As NPalm, ByVal palm As NPalm) As IEnumerable(Of NPalm)
		Return GetFrictionRidgesInSameGroup(palms, palm)
	End Function

	Public Shared Function GetFacesInSameGroup(ByVal faces() As NFace, ByVal face As NFace) As IEnumerable(Of NFace)
		If IsBiometricGeneralizationSource(face) Then
			Dim hasParent As Boolean = face.ParentObject IsNot Nothing
			Dim result As New List(Of NFace)(faces.Where(Function(x) x.SessionId = face.SessionId AndAlso x.ParentObject IsNot Nothing = hasParent))
			Dim attributes As NLAttributes = face.Objects.FirstOrDefault()
			Dim child As NFace = If(attributes IsNot Nothing, CType(attributes.Child, NFace), Nothing)
			If child IsNot Nothing AndAlso child.SessionId = -1 Then
				result.Add(child)
			End If
			If result.Count > 0 Then
				Return result
			End If

		ElseIf IsBiometricGeneralizationResult(face) Then
			Dim parentObject As NLAttributes = CType(face.ParentObject, NLAttributes)
			Dim owner As NFace = parentObject.Owner
			Return GetFacesInSameGroup(faces, owner)
		End If
		Return New NFace() {face}
	End Function

#End Region

#Region "Flatten fingers"

	Public Shared Function FlattenFingers(ByVal fingers() As NFinger) As IEnumerable(Of NFinger)
		Dim result As New List(Of NFinger)()
		For Each item In fingers
			result.Add(item)
			Dim children() As NFinger = item.Objects.ToArray().Select(Function(x) CType(x.Child, NFinger)).Where(Function(x) x IsNot Nothing).ToArray()
			result.AddRange(FlattenFingers(children))
		Next item
		Return result.Distinct()
	End Function

	Public Shared Function FlattenPalms(ByVal palms() As NPalm) As IEnumerable(Of NPalm)
		Dim result As New List(Of NPalm)()
		For Each item In palms
			result.Add(item)
			Dim children() As NPalm = item.Objects.ToArray().Select(Function(x) CType(x.Child, NPalm)).Where(Function(x) x IsNot Nothing).ToArray()
			result.AddRange(FlattenPalms(children))
		Next item
		Return result.Distinct()
	End Function

#End Region

#Region "Get template composites"
	' Get biometrics of which template is made, ignoring biometrics not containing template or containing template meant for generalization

	Public Shared Function GetTemplateCompositeFingers(ByVal subject As NSubject) As IEnumerable(Of NFinger)
		Dim results = New List(Of NFinger)()
		Dim allFingers = subject.Fingers.ToArray()
		For Each finger In allFingers.Where(Function(x) x.SessionId = -1)
			Dim attributes = finger.Objects.ToArray()
			If attributes.Length = 1 AndAlso attributes(0).Template IsNot Nothing Then
				results.Add(finger)
			End If
		Next finger
		Return results
	End Function

	Public Shared Function GetTemplateCompositeFaces(ByVal subject As NSubject) As IEnumerable(Of NFace)
		Dim results = New List(Of NFace)()
		Dim allFaces = subject.Faces.ToArray()
		For Each face In allFaces.Where(Function(x) x.SessionId = -1)
			Dim attributes As NLAttributes = face.Objects.FirstOrDefault()
			If attributes IsNot Nothing AndAlso attributes.Template IsNot Nothing Then
				results.Add(face)
			End If
		Next face
		Return results
	End Function

	Public Shared Function GetTemplateCompositeIrises(ByVal subject As NSubject) As IEnumerable(Of NIris)
		Dim results = New List(Of NIris)()
		Dim allIrises = subject.Irises.ToArray()
		For Each iris In allIrises.Where(Function(x) x.SessionId = -1)
			Dim attributes = iris.Objects.ToArray()
			If attributes.Length = 1 AndAlso attributes(0).Template IsNot Nothing Then
				results.Add(iris)
			End If
		Next iris
		Return results
	End Function

	Public Shared Function GetTemplateCompositePalms(ByVal subject As NSubject) As IEnumerable(Of NPalm)
		Dim results = New List(Of NPalm)()
		Dim allPalms = subject.Palms.ToArray()
		For Each palm In allPalms.Where(Function(x) x.SessionId = -1)
			Dim attributes = palm.Objects.ToArray()
			If attributes.Length = 1 AndAlso attributes(0).Template IsNot Nothing Then
				results.Add(palm)
			End If
		Next palm
		Return results
	End Function

	Public Shared Function GetTemplateCompositeVoices(ByVal subject As NSubject) As IEnumerable(Of NVoice)
		Dim results = New List(Of NVoice)()
		Dim allVoices = subject.Voices.ToArray()
		For Each voice In allVoices.Where(Function(x) x.SessionId = -1)
			Dim attributes = voice.Objects.ToArray()
			If attributes.Length = 1 AndAlso attributes(0).Template IsNot Nothing Then
				results.Add(voice)
			End If
		Next voice
		Return results
	End Function

#End Region

#Region "Private static methods"

	Private Shared Function GetFrictionRidgesInSameGroup(Of T As NFrictionRidge)(ByVal allFingers() As T, ByVal finger As T) As IEnumerable(Of T)
		If IsBiometricGeneralizationSource(finger) Then
			Dim result As New List(Of T)()
			For Each item In allFingers
				If item.Position = finger.Position AndAlso item.ImpressionType = finger.ImpressionType AndAlso item.SessionId = finger.SessionId Then
					result.Add(item)
				End If
			Next item

			Dim attributes As NFAttributes = finger.Objects.FirstOrDefault()
			Dim child As T = If(attributes IsNot Nothing, CType(attributes.Child, T), Nothing)
			If child IsNot Nothing AndAlso child.SessionId = -1 Then
				result.Add(child)
			End If
			Return result
		ElseIf IsBiometricGeneralizationResult(finger) Then
			Dim parentObject As NBiometricAttributes = finger.ParentObject
			Dim owner As T = If(parentObject IsNot Nothing, CType(parentObject.Owner, T), Nothing)
			If owner IsNot Nothing AndAlso owner.SessionId <> -1 Then
				Return GetFrictionRidgesInSameGroup(allFingers, owner)
			End If
		End If
		Return New T() {finger}
	End Function

	Private Shared Function GetFrictionRidgeGeneralizationGroups(Of T As NFrictionRidge)(ByVal fingers() As T) As IEnumerable(Of T())
		Dim results = New List(Of T())()
		For Each item In fingers.Where(Function(x) x.SessionId = -1 AndAlso x.ParentObject Is Nothing)
			results.Add(New T() {item})
		Next item

		Dim ids = fingers.Where(Function(x) x.SessionId <> -1).Select(Function(x) New With {Key .Id = x.SessionId, Key .Pos = x.Position, Key .Impr = x.ImpressionType}).Distinct()
		For Each id In ids
			Dim localId = id
			Dim result = fingers.Where(Function(x) x.SessionId = localId.Id AndAlso x.Position = localId.Pos AndAlso x.ImpressionType = localId.Impr).ToList()
			Dim first = result.FirstOrDefault()
			If first IsNot Nothing Then
				Dim attributes = first.Objects.ToArray().FirstOrDefault()
				Dim child = If(attributes IsNot Nothing, CType(attributes.Child, T), Nothing)
				If child IsNot Nothing AndAlso child.SessionId = -1 AndAlso child.Position = first.Position Then
					result.Add(child)
				End If
				results.Add(result.ToArray())
			End If
		Next id
		Return results
	End Function

#End Region
End Class
