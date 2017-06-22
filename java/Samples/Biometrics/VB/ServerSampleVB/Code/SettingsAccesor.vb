Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Friend NotInheritable Class SettingsAccesor
#Region "Connection Settings"

	Private Sub New()
	End Sub
	Public Shared Property UseDb() As Boolean
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.UseDB
			Catch
				Return False
			End Try
		End Get
		Set(ByVal value As Boolean)
			Global.Neurotec.Samples.Settings.Default.UseDB = value
		End Set
	End Property

	Public Shared Property TemplateDir() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.TemplateDir
			Catch
				Return String.Empty
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.TemplateDir = value
		End Set
	End Property

	Public Shared Property DbServer() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.Server
			Catch
				Return Nothing
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.Server = value
		End Set
	End Property

	Public Shared Property DbTable() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.Table
			Catch
				Return Nothing
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.Table = value
		End Set
	End Property

	Public Shared Property DbUser() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.User
			Catch
				Return Nothing
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.User = value
		End Set
	End Property

	Public Shared Property DbPassword() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.Password
			Catch
				Return Nothing
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.Password = value
		End Set
	End Property

	Public Shared Property IdColumn() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.IDCollumn
			Catch
				Return "dbid"
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.IDCollumn = value
		End Set
	End Property

	Public Shared Property TemplateColumn() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.TemplateCollumn
			Catch
				Return "template"
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.TemplateCollumn = value
		End Set
	End Property

	Public Shared Property Server() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.MMAServer
			Catch
				Return Nothing
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.MMAServer = value
		End Set
	End Property

	Public Shared Property ClientPort() As Integer
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.MMAPort
			Catch
				Return 25452
			End Try
		End Get
		Set(ByVal value As Integer)
			Global.Neurotec.Samples.Settings.Default.MMAPort = value
		End Set
	End Property

	Public Shared Property AdminPort() As Integer
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.MMAAdminPort
			Catch
				Return 24932
			End Try
		End Get
		Set(ByVal value As Integer)
			Global.Neurotec.Samples.Settings.Default.MMAAdminPort = value
		End Set
	End Property

	Public Shared Property UserName() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.MMAUser
			Catch
				Return "Admin"
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.MMAUser = value
		End Set
	End Property

	Public Shared Property Password() As String
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.MMAPassword
			Catch
				Return "Admin"
			End Try
		End Get
		Set(ByVal value As String)
			Global.Neurotec.Samples.Settings.Default.MMAPassword = value
		End Set
	End Property

	Public Shared Property IsAccelerator() As Boolean
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.IsAccelerator
			Catch
				Return False
			End Try
		End Get
		Set(ByVal value As Boolean)
			Global.Neurotec.Samples.Settings.Default.IsAccelerator = value
		End Set
	End Property

#End Region

#Region "Matching parameters settings"

#Region "General"

	Public Shared ReadOnly DefaultMatchingThreshold As Integer = 48
	Public Shared Property MatchingThreshold() As Integer
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.MatchingThreshold
			Catch
				Return DefaultMatchingThreshold
			End Try
		End Get
		Set(ByVal value As Integer)
			Global.Neurotec.Samples.Settings.Default.MatchingThreshold = value
		End Set
	End Property

#End Region

#Region "Fingers"

	Public Shared ReadOnly DefaultFingersMatchingSpeed As NMatchingSpeed = NMatchingSpeed.Low
	Public Shared Property FingersMatchingSpeed() As NMatchingSpeed
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.FingersMatchingSpeed
			Catch
				Return DefaultFingersMatchingSpeed
			End Try
		End Get
		Set(ByVal value As NMatchingSpeed)
			Global.Neurotec.Samples.Settings.Default.FingersMatchingSpeed = value
		End Set
	End Property

	Public Shared ReadOnly DefaultFingersMaximalRotation As Byte = 128
	Public Shared Property FingersMaximalRotation() As Byte
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.FingersMaximalRotation
			Catch
				Return DefaultFingersMaximalRotation
			End Try
		End Get
		Set(ByVal value As Byte)
			Global.Neurotec.Samples.Settings.Default.FingersMaximalRotation = value
		End Set
	End Property

#End Region

#Region "Faces"

	Public Shared ReadOnly DefaultFacesMatchingSpeed As NMatchingSpeed = NMatchingSpeed.Low
	Public Shared Property FacesMatchingSpeed() As NMatchingSpeed
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.FacesMatchingSpeed
			Catch
				Return DefaultFacesMatchingSpeed
			End Try
		End Get
		Set(ByVal value As NMatchingSpeed)
			Global.Neurotec.Samples.Settings.Default.FacesMatchingSpeed = value
		End Set
	End Property

#End Region

#Region "Irises"

	Public Shared ReadOnly DefaultIrisesMatchingSpeed As NMatchingSpeed = NMatchingSpeed.Low
	Public Shared Property IrisesMatchingSpeed() As NMatchingSpeed
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.IrisesMatchingSpeed
			Catch
				Return NMatchingSpeed.Low
			End Try
		End Get
		Set(ByVal value As NMatchingSpeed)
			Global.Neurotec.Samples.Settings.Default.IrisesMatchingSpeed = value
		End Set
	End Property

	Public Shared ReadOnly DefaultIrisesMaximalRotation As Byte = 11
	Public Shared Property IrisesMaximalRotation() As Byte
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.IrisesMaxRotation
			Catch
				Return DefaultIrisesMaximalRotation
			End Try
		End Get
		Set(ByVal value As Byte)
			Global.Neurotec.Samples.Settings.Default.IrisesMaxRotation = value
		End Set
	End Property

#End Region

#Region "Palms"

	Public Shared ReadOnly DefaultPalmsMatchingSpeed As NMatchingSpeed = NMatchingSpeed.Low
	Public Shared Property PalmsMatchingSpeed() As NMatchingSpeed
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.PalmsMatchingSpeed
			Catch
				Return DefaultPalmsMatchingSpeed
			End Try
		End Get
		Set(ByVal value As NMatchingSpeed)
			Global.Neurotec.Samples.Settings.Default.PalmsMatchingSpeed = value
		End Set
	End Property

	Public Shared ReadOnly DefaultPalmsMaximalRotation As Byte = 128
	Public Shared Property PalmsMaximalRotation() As Byte
		Get
			Try
				Return Global.Neurotec.Samples.Settings.Default.PalmsMaximalRotation
			Catch
				Return DefaultPalmsMaximalRotation
			End Try
		End Get
		Set(ByVal value As Byte)
			Global.Neurotec.Samples.Settings.Default.PalmsMaximalRotation = value
		End Set
	End Property

#End Region

#End Region

#Region "Public static methods"

	Public Shared Sub ResetMatchingSettings()
		MatchingThreshold = DefaultMatchingThreshold
		FingersMatchingSpeed = DefaultFingersMatchingSpeed
		FingersMaximalRotation = DefaultFingersMaximalRotation
		FacesMatchingSpeed = DefaultFacesMatchingSpeed
		IrisesMatchingSpeed = DefaultIrisesMatchingSpeed
		IrisesMaximalRotation = DefaultIrisesMaximalRotation
		PalmsMatchingSpeed = DefaultPalmsMatchingSpeed
		PalmsMaximalRotation = DefaultPalmsMaximalRotation
	End Sub

	Public Shared Sub SetMatchingParameters(ByVal biometricClient As NBiometricClient)
		'General params
		biometricClient.MatchingThreshold = MatchingThreshold

		'Finger params
		biometricClient.FingersMatchingSpeed = FingersMatchingSpeed
		biometricClient.FingersMaximalRotation = FingersMaximalRotation

		'Faces params
		biometricClient.FacesMatchingSpeed = FacesMatchingSpeed

		'irises
		biometricClient.IrisesMatchingSpeed = IrisesMatchingSpeed
		biometricClient.IrisesMaximalRotation = IrisesMaximalRotation

		'palms
		biometricClient.PalmsMatchingSpeed = PalmsMatchingSpeed
		biometricClient.PalmsMaximalRotation = PalmsMaximalRotation
	End Sub

	Public Shared Sub SaveSettings()
		Global.Neurotec.Samples.Settings.Default.Save()
	End Sub

#End Region
End Class
