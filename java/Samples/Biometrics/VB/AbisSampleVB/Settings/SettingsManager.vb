Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Devices
Imports Neurotec.Samples.My

Public Enum ConnectionType
	SQLiteDatabase
	OdbcDatabase
	RemoteMatchingServer
End Enum

Public Class SampleDbSchema
#Region "Public constructor"

	Public Sub New()
		BiographicData = New NBiographicDataSchema()
		CustomData = New NBiographicDataSchema()
	End Sub

#End Region

#Region "Public properties"

	Private privateBiographicData As NBiographicDataSchema
	Public Property BiographicData() As NBiographicDataSchema
		Get
			Return privateBiographicData
		End Get
		Set(ByVal value As NBiographicDataSchema)
			privateBiographicData = value
		End Set
	End Property
	Private privateCustomData As NBiographicDataSchema
	Public Property CustomData() As NBiographicDataSchema
		Get
			Return privateCustomData
		End Get
		Set(ByVal value As NBiographicDataSchema)
			privateCustomData = value
		End Set
	End Property
	Private privateGenderDataName As String
	Public Property GenderDataName() As String
		Get
			Return privateGenderDataName
		End Get
		Set(ByVal value As String)
			privateGenderDataName = value
		End Set
	End Property
	Private privateEnrollDataName As String
	Public Property EnrollDataName() As String
		Get
			Return privateEnrollDataName
		End Get
		Set(ByVal value As String)
			privateEnrollDataName = value
		End Set
	End Property
	Private privateThumbnailDataName As String
	Public Property ThumbnailDataName() As String
		Get
			Return privateThumbnailDataName
		End Get
		Set(ByVal value As String)
			privateThumbnailDataName = value
		End Set
	End Property
	Private privateSchemaName As String
	Public Property SchemaName() As String
		Get
			Return privateSchemaName
		End Get
		Set(ByVal value As String)
			privateSchemaName = value
		End Set
	End Property
	Public ReadOnly Property HasCustomData() As Boolean
		Get
			Return CustomData IsNot Nothing AndAlso CustomData.Elements.Count > 0
		End Get
	End Property
	Public ReadOnly Property IsEmpty() As Boolean
		Get
			Return Me Is Empty
		End Get
	End Property

#End Region

#Region "Public fields"

	Public Shared ReadOnly Empty As New SampleDbSchema() With {.SchemaName = "None"}

#End Region

#Region "Public methods"

	Public Overrides Function ToString() As String
		Return SchemaName
	End Function

	Public Shared Function Parse(ByVal value As String) As SampleDbSchema
		Dim sc As New SampleDbSchema()
		Dim values = value.Split(New String() {"#"}, StringSplitOptions.None)
		If values.Length <> 6 Then
			Throw New ArgumentException()
		End If

		sc.SchemaName = values(0)
		sc.BiographicData = NBiographicDataSchema.Parse(values(1))
		If (Not String.IsNullOrEmpty(values(2))) Then
			sc.CustomData = NBiographicDataSchema.Parse(values(2))
		End If

		sc.GenderDataName = values(3).Split(New Char() {"="c})(1)
		sc.ThumbnailDataName = values(4).Split(New Char() {"="c})(1)
		sc.EnrollDataName = values(5).Split(New Char() {"="c})(1)

		Return sc
	End Function

	Public Function Save() As String
		If IsEmpty Then
			Throw New InvalidOperationException()
		End If

		Dim format As String = "{0}#{1}#{2}#Gender={3}#Thumbnail={4}#EnrollData={5}"
		Return String.Format(format, SchemaName, If(CObj(BiographicData), String.Empty), If(CObj(CustomData), String.Empty), GenderDataName, ThumbnailDataName, EnrollDataName)
	End Function

#End Region
End Class

Public NotInheritable Class SettingsManager
#Region "Public methods"

	Private Sub New()
	End Sub
	Public Shared Sub LoadSettings(ByVal client As NBiometricClient)
		Dim s As Settings = Settings.Default
		Dim propertyBag As NPropertyBag = Nothing
		Dim propertiesString As String = String.Empty

		If client Is Nothing Then
			Throw New ArgumentNullException("client")
		End If

		client.Reset()
		client.UseDeviceManager = True
		Try
			propertiesString = My.Settings.ClientProperties
		Catch
		End Try
		propertyBag = NPropertyBag.Parse(propertiesString)
		propertyBag.ApplyTo(client)

		client.FingersDeterminePatternClass = client.FingersDeterminePatternClass AndAlso LicensingTools.CanDetectFingerSegments(client.LocalOperations)
		client.FingersCalculateNfiq = client.FingersCalculateNfiq AndAlso LicensingTools.CanAssessFingerQuality(client.LocalOperations)
		Dim remoteConnection = client.RemoteConnections.FirstOrDefault()
		Dim remoteOperations As NBiometricOperations = If(remoteConnection IsNot Nothing, remoteConnection.Operations, NBiometricOperations.None)
		client.FingersCheckForDuplicatesWhenCapturing = client.FingersCheckForDuplicatesWhenCapturing AndAlso LicensingTools.CanFingerBeMatched(remoteOperations)
		If Not LicensingTools.CanDetectFaceSegments(client.LocalOperations) Then
			client.FacesDetectAllFeaturePoints = False
			client.FacesDetectBaseFeaturePoints = False
			client.FacesDetermineGender = False
			client.FacesRecognizeEmotion = False
			client.FacesDetectProperties = False
			client.FacesRecognizeEmotion = False
			client.FacesDetermineAge = False
		End If
	End Sub

	Public Shared Sub LoadPreferedDevices(ByVal client As NBiometricClient)
		Dim s As Settings = Settings.Default
		Try
			If (Not String.IsNullOrEmpty(s.FingerScanner)) Then
				Dim device As NDevice = client.DeviceManager.Devices.FirstOrDefault(Function(x) x.Id = s.FingerScanner)
				If device IsNot Nothing Then
					client.FingerScanner = CType(device, NFScanner)
				End If
			End If
		Catch
		End Try
		Try
			If (Not String.IsNullOrEmpty(s.PalmScanner)) Then
				Dim device As NDevice = client.DeviceManager.Devices.FirstOrDefault(Function(x) x.Id = s.PalmScanner)
				If device IsNot Nothing Then
					client.PalmScanner = CType(device, NFScanner)
				End If
			End If
		Catch
		End Try
		Try
			If (Not String.IsNullOrEmpty(s.IrisScanner)) Then
				Dim device As NDevice = client.DeviceManager.Devices.FirstOrDefault(Function(x) x.Id = s.IrisScanner)
				If device IsNot Nothing Then
					client.IrisScanner = CType(device, NIrisScanner)
				End If
			End If
		Catch
		End Try
		Try
			If (Not String.IsNullOrEmpty(s.FaceCaptureDevice)) Then
				Dim device As NDevice = client.DeviceManager.Devices.FirstOrDefault(Function(x) x.Id = s.FaceCaptureDevice)
				If device IsNot Nothing Then
					client.FaceCaptureDevice = CType(device, NCamera)
				End If
			End If
		Catch
		End Try
		Try
			If (Not String.IsNullOrEmpty(s.VoiceCaptureDevice)) Then
				Dim device As NDevice = client.DeviceManager.Devices.FirstOrDefault(Function(x) x.Id = s.VoiceCaptureDevice)
				If device IsNot Nothing Then
					client.VoiceCaptureDevice = CType(device, NMicrophone)
				End If
			End If
		Catch
		End Try
	End Sub

	Public Shared Sub SaveSettings(ByVal client As NBiometricClient)
		Dim s As Settings = Settings.Default
		Dim properties As New NPropertyBag()

		If client Is Nothing Then
			Throw New ArgumentNullException("client")
		End If

		client.CaptureProperties(properties)
		s.ClientProperties = properties.ToString()

		' prefered devices
		s.FaceCaptureDevice = If(client.FaceCaptureDevice IsNot Nothing, client.FaceCaptureDevice.Id, Nothing)
		s.FingerScanner = If(client.FingerScanner IsNot Nothing, client.FingerScanner.Id, Nothing)
		s.PalmScanner = If(client.PalmScanner IsNot Nothing, client.PalmScanner.Id, Nothing)
		s.IrisScanner = If(client.IrisScanner IsNot Nothing, client.IrisScanner.Id, Nothing)
		s.VoiceCaptureDevice = If(client.VoiceCaptureDevice IsNot Nothing, client.VoiceCaptureDevice.Id, Nothing)

		s.Save()
	End Sub

#End Region

#Region "Public properties"

	Public Shared Property Phrases() As IEnumerable(Of Phrase)
		Get
			Dim values As String = Nothing
			Try
				values = My.Settings.Phrases
			Catch
			End Try

			Dim result As List(Of Phrase) = New List(Of Phrase)
			If (Not String.IsNullOrEmpty(values)) Then
				Dim split() As String = values.Split(New Char() {";"c}, StringSplitOptions.RemoveEmptyEntries)
				For Each item In split
					Dim phrase As Phrase = Nothing
					Try
						Dim splitPhrase = item.Split("="c)
						phrase = New Phrase(Integer.Parse(splitPhrase(0)), splitPhrase(1))
					Catch
						'ignore invalid entries
						Continue For
					End Try
					result.Add(phrase)
				Next item
			End If
			Return result
		End Get
		Set(ByVal value As IEnumerable(Of Phrase))
			If value IsNot Nothing Then
				Dim sb As New StringBuilder()
				For Each phrase As Phrase In value
					sb.AppendFormat("{0}={1};", phrase.Id, phrase.String)
				Next phrase
				My.Settings.Phrases = sb.ToString()
			Else
				My.Settings.Phrases = String.Empty
			End If
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property ConnectionType() As ConnectionType
		Get
			Try
				Return My.Settings.ConnectionType
			Catch
				Return True
			End Try
		End Get
		Set(ByVal value As ConnectionType)
			My.Settings.ConnectionType = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property OdbcConnectionString() As String
		Get
			Try
				Return My.Settings.OdbcConnectionString
			Catch
				Return String.Empty
			End Try
		End Get
		Set(ByVal value As String)
			My.Settings.OdbcConnectionString = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property TableName() As String
		Get
			Try
				Return My.Settings.TableName
			Catch ex As Exception
				Return String.Empty
			End Try
		End Get
		Set(ByVal value As String)

		End Set
	End Property

	Public Shared Property RemoteServerHostName() As String
		Get
			Try
				Return My.Settings.HostName
			Catch
				Return "localhost"
			End Try
		End Get
		Set(ByVal value As String)
			My.Settings.HostName = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property RemoteServerPort() As Integer
		Get
			Try
				Return My.Settings.ClientPort
			Catch
				Return 25452
			End Try
		End Get
		Set(ByVal value As Integer)
			My.Settings.ClientPort = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property RemoteServerAdminPort() As Integer
		Get
			Try
				Return My.Settings.AdminPort
			Catch
				Return 24932
			End Try
		End Get
		Set(ByVal value As Integer)
			My.Settings.AdminPort = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property FingersGeneralizationRecordCount() As Integer
		Get
			Try
				Return My.Settings.FingersGeneralizationRecordCount
			Catch
				Return 3
			End Try
		End Get
		Set(ByVal value As Integer)
			My.Settings.FingersGeneralizationRecordCount = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property PalmsGeneralizationRecordCount() As Integer
		Get
			Try
				Return My.Settings.PalmsGeneralizationRecordCount
			Catch
				Return 3
			End Try
		End Get
		Set(ByVal value As Integer)
			My.Settings.PalmsGeneralizationRecordCount = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property FacesGeneralizationRecordCount() As Integer
		Get
			Try
				Return My.Settings.FacesGeneralizationRecordCount
			Catch
				Return 3
			End Try
		End Get
		Set(ByVal value As Integer)
			My.Settings.FacesGeneralizationRecordCount = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property QuerySuggestions() As String()
		Get
			Try
				Return My.Settings.QueryAutoComplete.OfType(Of String)().ToArray()
			Catch
				Return New String() {}
			End Try
		End Get
		Set(ByVal value As String())
			My.Settings.QueryAutoComplete = New System.Collections.Specialized.StringCollection()
			If value IsNot Nothing Then
				My.Settings.QueryAutoComplete.AddRange(value)
			End If
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property WarnHasSchema() As Boolean
		Get
			Try
				Return My.Settings.WarnHasSchema
			Catch
				Return True
			End Try
		End Get
		Set(ByVal value As Boolean)
			My.Settings.WarnHasSchema = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property CurrentSchemaIndex() As Integer
		Get
			Try
				Return My.Settings.CurrentScema
			Catch
				Return 0
			End Try
		End Get
		Set(ByVal value As Integer)
			My.Settings.CurrentScema = value
			My.Settings.Save()
		End Set
	End Property

	Public Shared Property Schemas() As IEnumerable(Of SampleDbSchema)
		Get
			Dim schemasLocal() As String = Nothing
			Try
				schemasLocal = My.Settings.Schemas.OfType(Of String)().ToArray()
			Catch
				schemasLocal = New String() {}
			End Try

			Return schemasLocal.Select(Function(x) SampleDbSchema.Parse(x)).ToArray()
		End Get
		Set(ByVal value As IEnumerable(Of SampleDbSchema))
			My.Settings.Schemas.Clear()
			If value IsNot Nothing Then
				For Each item In value
					My.Settings.Schemas.Add(item.Save())
				Next item
			End If
			My.Settings.Save()
		End Set
	End Property

	Public Shared ReadOnly Property CurrentSchema() As SampleDbSchema
		Get
			If CurrentSchemaIndex = -1 Then
				Return SampleDbSchema.Empty
			Else
				Return Schemas.ToArray()(CurrentSchemaIndex)
			End If
		End Get
	End Property

	Public Shared Property LocalOperationsIndex() As Integer
		Get
			Try
				Return Settings.Default.LocalOperations
			Catch
				Return 5
			End Try
		End Get
		Set(ByVal value As Integer)
			Settings.Default.LocalOperations = value
			Settings.Default.Save()
		End Set
	End Property

	Public Shared Property FacesMirrorHorizontally() As Boolean
		Get
			Try
				Return Settings.Default.FacesMirrorHorizontally
			Catch
				Return True
			End Try
		End Get
		Set(ByVal value As Boolean)
			Settings.Default.FacesMirrorHorizontally = value
			Settings.Default.Save()
		End Set
	End Property

#End Region
End Class
