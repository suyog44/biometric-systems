Imports System
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.IO
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports Neurotec.IO
Imports System.Linq
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Public Class SchemaPropertyGridAdapter
#Region "Private types"

	Private Class AdapterdDesrciptorProvider
		Inherits TypeDescriptionProvider
#Region "Public constructor"

		Public Sub New()
			MyBase.New(TypeDescriptor.GetProvider(GetType(SchemaPropertyGridAdapter)))
		End Sub

#End Region

#Region "Public methods"

		Public Overloads Overrides Function GetTypeDescriptor(ByVal objectType As Type, ByVal instance As Object) As ICustomTypeDescriptor
			Dim adapter = CType(instance, SchemaPropertyGridAdapter)
			Return New AdapterTypeDescriptor(adapter)
		End Function

#End Region
	End Class

	Private Class AdapterTypeDescriptor
		Inherits CustomTypeDescriptor
#Region "Private fields"

		Private _adapter As SchemaPropertyGridAdapter

#End Region

#Region "Public constructor"

		Public Sub New(ByVal adapter As SchemaPropertyGridAdapter)
			_adapter = adapter
		End Sub

#End Region

#Region "Public methods"

		Public Overloads Overrides Function GetProperties(ByVal attributes() As Attribute) As PropertyDescriptorCollection
			Dim properties As New PropertyDescriptorCollection(Nothing)
			Dim schema = _adapter.Schema
			If schema IsNot Nothing AndAlso (Not schema.IsEmpty) Then
				Dim items = Enumerable.Union(schema.BiographicData.Elements, schema.CustomData.Elements)
				For Each item In items
					If (Not _adapter.ShowBlobs) AndAlso item.DbType = NDBType.Blob Then
						Continue For
					End If
					If item.Name = schema.EnrollDataName OrElse item.Name = schema.ThumbnailDataName Then
						Continue For
					End If
					If item.Name = schema.GenderDataName Then
						properties.Add(New PropertyDataDescriptor(item.Name, GetType(NGender?), _adapter._propertyBag, _adapter.IsReadOnly))
					Else
						Dim type As Type
						Select Case item.DbType
							Case NDBType.Blob
								type = GetType(NBuffer)
							Case NDBType.Integer
								type = GetType(Integer?)
							Case NDBType.String
								type = GetType(String)
							Case Else
								Throw New ArgumentException()
						End Select

						properties.Add(New PropertyDataDescriptor(item.Name, type, _adapter._propertyBag, _adapter.IsReadOnly))
					End If
				Next item
			End If

			Return properties
		End Function

#End Region
	End Class

	Private Class PropertyDataDescriptor
		Inherits PropertyDescriptor
#Region "Private fields"

		Private _propertyBag As NPropertyBag
		Private _propertyType As Type
		Private _readOnly As Boolean

#End Region

#Region "Public constructor"

		Public Sub New(ByVal name As String, ByVal type As Type, ByVal propertyBag As NPropertyBag, ByVal [readOnly] As Boolean)
			MyBase.New(name, Nothing)
			_propertyBag = propertyBag
			_propertyType = type
			_readOnly = [readOnly]
		End Sub

#End Region

#Region "Private methods"

		Private Function GetValueInternal() As Object
			Dim value As Object = Nothing
			_propertyBag.TryGetValue(Name, value)
			Return value
		End Function

#End Region

#Region "Public methods"

		Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
			Return False
		End Function

		Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
			_propertyBag(Name) = value
		End Sub

		Public Overrides Sub ResetValue(ByVal component As Object)
			_propertyBag.Remove(Name)
		End Sub

		Public Overrides Function GetValue(ByVal component As Object) As Object
			Return GetValueInternal()
		End Function

		Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
			Return Not _readOnly
		End Function

		Public Overrides Function GetEditor(ByVal editorBaseType As Type) As Object
			If (Not _readOnly) AndAlso PropertyType Is GetType(NBuffer) Then
				Return New ReadFileEditor()
			End If
			Return MyBase.GetEditor(editorBaseType)
		End Function

#End Region

#Region "Public properties"

		Public Overrides ReadOnly Property PropertyType() As Type
			Get
				Return _propertyType
			End Get
		End Property

		Public Overrides ReadOnly Property IsReadOnly() As Boolean
			Get
				Return _readOnly
			End Get
		End Property

		Public Overrides ReadOnly Property ComponentType() As Type
			Get
				Return Nothing
			End Get
		End Property

#End Region
	End Class

	Private Class ReadFileEditor
		Inherits UITypeEditor
#Region "Public methods"

		Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
			Return UITypeEditorEditStyle.Modal
		End Function

		Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
			Dim editorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
			If editorService IsNot Nothing Then
				Dim type As Type = context.PropertyDescriptor.PropertyType
				If type IsNot GetType(NBuffer) Then
					Throw New ArgumentException()
				End If
				Using dialog As New OpenFileDialog()
					If dialog.ShowDialog() = DialogResult.OK Then
						Return New NBuffer(File.ReadAllBytes(dialog.FileName))
					End If
				End Using
			End If
			Return MyBase.EditValue(context, provider, value)
		End Function

#End Region
	End Class

#End Region

#Region "Static constructor"

	Shared Sub New()
		TypeDescriptor.AddProvider(New AdapterdDesrciptorProvider(), GetType(SchemaPropertyGridAdapter))
	End Sub

#End Region

#Region "Public constructor"

	Public Sub New(ByVal schema As SampleDbSchema)
		_propertyBag = New NPropertyBag()
		_schema = schema
	End Sub

	Public Sub New(ByVal schema As SampleDbSchema, ByVal values As NPropertyBag)
		If values Is Nothing Then
			Throw New ArgumentNullException()
		End If
		_schema = schema
		_propertyBag = values
	End Sub

#End Region

#Region "Private fields"

	Private _propertyBag As NPropertyBag
	Private _schema As SampleDbSchema

#End Region

#Region "Public properties"

	Private privateIsReadOnly As Boolean
	Public Property IsReadOnly() As Boolean
		Get
			Return privateIsReadOnly
		End Get
		Set(ByVal value As Boolean)
			privateIsReadOnly = value
		End Set
	End Property
	Private privateShowBlobs As Boolean
	Public Property ShowBlobs() As Boolean
		Get
			Return privateShowBlobs
		End Get
		Set(ByVal value As Boolean)
			privateShowBlobs = value
		End Set
	End Property
	Public Property Schema() As SampleDbSchema
		Get
			Return _schema
		End Get
		Set(ByVal value As SampleDbSchema)
			_schema = value
			_propertyBag = New NPropertyBag()
		End Set
	End Property

#End Region

#Region "Public methods"

	Public Sub ApplyTo(ByVal subject As NSubject)
		Dim bag As NPropertyBag = CType(_propertyBag.Clone(), NPropertyBag)
		For Each key In bag.Keys.ToArray()
			Dim value As Object = bag(key)
			If value Is Nothing OrElse value Is NBuffer.Empty OrElse TryCast(value, String) = String.Empty Then
				bag.Remove(key)
			End If
		Next key
		bag.ApplyTo(subject)
	End Sub

	Public Sub SetValue(ByVal key As String, ByVal value As Object)
		_propertyBag(key) = value
	End Sub

	Public Function GetValue(Of T)(ByVal key As String) As T
		Dim value As Object = Nothing
		If _propertyBag.TryGetValue(key, value) Then
			Return CType(value, T)
		Else
			Return Nothing
		End If
	End Function

#End Region
End Class
