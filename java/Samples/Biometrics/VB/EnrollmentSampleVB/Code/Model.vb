Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports Neurotec.Biometrics
Imports Neurotec.Images
Imports Neurotec.IO

Public Class InfoField
	#Region "Public constructors"

	Public Sub New()
	End Sub

	Public Sub New(ByVal value As String)
		If value Is Nothing Then
			Throw New ArgumentNullException("value")
		End If

		Dim items() As String = value.Trim().Split(New Char() { ","c }, StringSplitOptions.RemoveEmptyEntries)
		For Each item As String In items
			Dim str As String = item.Trim()
			Dim lower As String = str.ToLower()
			Dim first As Integer = str.IndexOf("'")
			Dim last As Integer = str.LastIndexOf("'")
			Dim v As String = str.Substring(first + 1, last - first - 1)
			If lower.StartsWith("key") Then
				Key = v
			ElseIf lower.StartsWith("isthumbnail") Then
				ShowAsThumbnail = Convert.ToBoolean(v, CultureInfo.InvariantCulture)
				If ShowAsThumbnail Then
					Me.Value = Nothing
				End If
			ElseIf lower.StartsWith("enrolltoserver") Then
				EnrollToServer = Convert.ToBoolean(v, CultureInfo.InvariantCulture)
			End If
		Next item
	End Sub

	#End Region

	#Region "Public fields"

	Public Key As String
	Public Value As Object = String.Empty
	Public ShowAsThumbnail As Boolean
	Public EnrollToServer As Boolean
	Public IsEditable As Boolean = True

	#End Region

	#Region "Public methods"

	Public Overrides Function ToString() As String
		Dim result As String = String.Format("Key = '{0}'", Key)
		If ShowAsThumbnail Then
			result &= ", IsThumbnail = 'True'"
		End If
		If EnrollToServer Then
			result &= ", EnrollToServer = 'True'"
		End If
		Return result
	End Function

	#End Region
End Class

Public Class DataModel
	Implements IDisposable
	#Region "Private types"

	Private Class InfoDescriptor
		Inherits PropertyDescriptor
		#Region "Private fields"

		Private target As InfoField

		#End Region

		#Region "Public constructor"

		Public Sub New(ByVal inf As InfoField)
			MyBase.New(inf.Key, Nothing)
			target = inf
		End Sub

		#End Region

		#Region "Public methods"

		Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
			Return False
		End Function

		Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
			target.Value = value
		End Sub

		Public Overrides Sub ResetValue(ByVal component As Object)
			Throw New NotSupportedException()
		End Sub

		Public Overrides Function GetValue(ByVal component As Object) As Object
			Return target.Value
		End Function

		Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
			Return False
		End Function

		#End Region

		#Region "Public properties"

		Public Overrides ReadOnly Property PropertyType() As Type
			Get
				If target.Value IsNot Nothing Then
					Return target.Value.GetType()
				Else
					Return GetType(String)
				End If
			End Get
		End Property

		Public Overrides ReadOnly Property IsReadOnly() As Boolean
			Get
				Return False
			End Get
		End Property

		Public Overrides ReadOnly Property ComponentType() As Type
			Get
				Return Nothing
			End Get
		End Property

		Public Overrides ReadOnly Property Category() As String
			Get
				Return If(target.EnrollToServer, "Enroll to Server", "Information")
			End Get
		End Property

		#End Region
	End Class

	Private Class Information
		Inherits List(Of InfoField)
		Implements ICustomTypeDescriptor
		#Region "ICustomTypeDescriptor Members"

		Public Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
			Return TypeDescriptor.GetAttributes(Me, True)
		End Function

		Public Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
			Return TypeDescriptor.GetClassName(Me, True)
		End Function

		Public Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
			Return TypeDescriptor.GetComponentName(Me, True)
		End Function

		Public Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
			Return TypeDescriptor.GetConverter(Me, True)
		End Function

		Public Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
			Return TypeDescriptor.GetDefaultEvent(Me, True)
		End Function

		Public Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
			Return TypeDescriptor.GetDefaultProperty(Me, True)
		End Function

		Public Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
			Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
		End Function

		Public Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
			Return TypeDescriptor.GetEvents(Me, True)
		End Function

		Public Function GetEvents(ByVal attributes() As Attribute) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
			Return TypeDescriptor.GetEvents(Me, attributes, True)
		End Function

		Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
			Return Me.GetProperties(New Attribute(){})
		End Function

		Public Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
			Return Me
		End Function

		Public Overridable Function GetProperties(ByVal attributes() As Attribute) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
			Dim descriptors As New List(Of InfoDescriptor)()
			For Each inf As InfoField In Me
				If inf.IsEditable Then
					descriptors.Add(New InfoDescriptor(inf))
				End If
			Next inf
			Return New PropertyDescriptorCollection(descriptors.ToArray())
		End Function

		#End Region
	End Class

	#End Region

	#Region "Private fields"

	Private _info As New Information()
	Private _subject As NSubject

	#End Region

	#Region "Private readonly fields"

	Private Shared ReadOnly InformationElement As String = "Information"
	Private Shared ReadOnly InfoFieldElement As String = "Info"
	Private Shared ReadOnly DataElement As String = "Data"
	Private Shared ReadOnly TemplateAttribute As String = "Template"
	Private Shared ReadOnly DataFieldElement As String = "DataField"
	Private Shared ReadOnly PositionAttribute As String = "Position"
	Private Shared ReadOnly ImpressionAttribute As String = "Impression"
	Private Shared ReadOnly CreateStringAttribute As String = "CreateString"
	Private Shared ReadOnly FileAttribute As String = "File"

	#End Region

	#Region "Public properties"

	Public ReadOnly Property Info() As List(Of InfoField)
		Get
			Return _info
		End Get
	End Property
	Public Property Subject() As NSubject
		Get
			Return _subject
		End Get
		Set(ByVal value As NSubject)
			_subject = value
		End Set
	End Property

	#End Region

	#Region "Public methods"

	Public Sub Save(ByVal dir As String)
		Dim dirName As String = Path.GetFileName(dir)

		Dim settings As New XmlWriterSettings()
		settings.Indent = True
		settings.NewLineOnAttributes = True

		Dim xml As XmlWriter = XmlWriter.Create(Path.ChangeExtension(Path.Combine(dir, dirName), "xml"), settings)
		Try
			xml.WriteStartDocument()
			xml.WriteStartElement("EnrollmentResult")

			xml.WriteStartElement(InformationElement)
			For Each inf As InfoField In Info
				xml.WriteStartElement(InfoFieldElement)
				xml.WriteAttributeString(CreateStringAttribute, inf.ToString())
				If inf.Value IsNot Nothing Then
					If TypeOf inf.Value Is NImage Then
						Dim name As String = Path.ChangeExtension(inf.Key, ".png")
						xml.WriteAttributeString(FileAttribute, name)
						CType(inf.Value, NImage).Save(Path.Combine(dir, name))
					Else
						xml.WriteValue(inf.Value)
					End If
				End If
				xml.WriteEndElement()
			Next inf
			xml.WriteEndElement()

			If Subject IsNot Nothing Then
				xml.WriteStartElement(DataElement)
				Using template As NBuffer = Subject.GetTemplateBuffer()
					Dim name As String = "template"
					File.WriteAllBytes(Path.Combine(dir, name), template.ToArray())
					xml.WriteAttributeString(TemplateAttribute, name)
				End Using

				For Each finger As NFinger In Subject.Fingers.Where(Function(x) x.Status = NBiometricStatus.Ok AndAlso x.ParentObject Is Nothing)
					WriteFingerData(xml, dir, finger)
				Next finger
				xml.WriteEndElement()
			End If

			xml.WriteEndElement()
			xml.WriteEndDocument()
		Finally
			xml.Close()
		End Try
	End Sub

	Public Function GetClusterEnrollParams(ByVal dbidField As String, ByVal templateField As String, ByVal hashNameField As String, <System.Runtime.InteropServices.Out()> ByRef keyIndex As Integer) As Object()
		keyIndex = -1

		Dim dbidFound As Boolean = False, templateFound As Boolean = False, hashNameFound As Boolean = False
		Dim values As New List(Of Object)()
		For Each item As InfoField In _info.Where(Function(x) x.EnrollToServer)
			If item.Key = dbidField Then
				If String.IsNullOrEmpty(TryCast(item.Value, String)) Then
					Throw New ArgumentNullException(String.Format("{0} value is null", item.Key))
				End If
				values.Add(item.Value)
				keyIndex = values.Count
				dbidFound = True
			ElseIf item.Key = templateField Then
				If Subject IsNot Nothing Then
					Using template As NTemplate = Subject.GetTemplate()
						If template.Fingers IsNot Nothing Then
							Using buffer = template.Save()
								values.Add(buffer.ToArray())
								templateFound = True
							End Using
						End If
					End Using
				End If
				If templateFound = False Then
					Throw New InvalidOperationException("Template is empty. Please extract at least one record to proceed")
				End If
			ElseIf item.Key = hashNameField Then
				values.Add(New Random().Next(UShort.MaxValue))
				hashNameFound = True
			Else
				If TypeOf item.Value Is NObject Then
					Using buffer As NBuffer = (CType(item.Value, NObject)).Save()
						values.Add(buffer.ToArray())
					End Using
				Else
					values.Add(item.Value)
				End If
			End If
		Next item

		If (Not hashNameFound) Then
			Throw New ArgumentException("Hash name not found")
		End If
		If (Not dbidFound) Then
			Throw New ArgumentException("dbid not found")
		End If
		If (Not templateFound) Then
			Throw New ArgumentException("template not found")
		End If

		Return values.ToArray()
	End Function

	#End Region

	#Region "Private methods"

	Private Sub WriteFingerData(ByVal xml As XmlWriter, ByVal dir As String, ByVal finger As NFinger)
		xml.WriteStartElement(DataFieldElement)
		xml.WriteAttributeString(PositionAttribute, finger.Position.ToString())
		xml.WriteAttributeString(ImpressionAttribute, finger.ImpressionType.ToString())

		Dim name As String = String.Format("{0}{1}", finger.Position,If(NBiometricTypes.IsImpressionTypeRolled(finger.ImpressionType), "_Rolled", String.Empty))
		name = Path.ChangeExtension(name, "png")
		Dim imagePath As String = Path.Combine(dir, name)
		finger.Image.Save(imagePath)
		xml.WriteAttributeString(FileAttribute, name)

		Dim children = finger.Objects.Select(Function(x) TryCast(x.Child, NFinger)).Where(Function(x) x IsNot Nothing)
		For Each child In children
			WriteFingerData(xml, dir, child)
		Next child

		xml.WriteEndElement()
	End Sub

	#End Region

	#Region "IDisposable Members"

	Public Sub Dispose() Implements IDisposable.Dispose
		Subject.Dispose()
		Subject = Nothing
	End Sub

	#End Region
End Class
