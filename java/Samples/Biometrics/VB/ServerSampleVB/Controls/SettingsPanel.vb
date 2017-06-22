Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics

Partial Public Class SettingsPanel
	Inherits ControlBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		Dim thresholds() As Object = {Utilities.MatchingThresholdToString(12), Utilities.MatchingThresholdToString(24), Utilities.MatchingThresholdToString(36), Utilities.MatchingThresholdToString(48), Utilities.MatchingThresholdToString(60), Utilities.MatchingThresholdToString(72)}

		cbMatchingThreshold.Items.AddRange(thresholds)

		Dim speeds() As Object = {NMatchingSpeed.High, NMatchingSpeed.Medium, NMatchingSpeed.Low}
		cbFingersMatchingSpeed.Items.AddRange(speeds)
		cbFacesMatchingSpeed.Items.AddRange(speeds)
		cbIrisesMatchingSpeed.Items.AddRange(speeds)
		cbPalmsMatchingSpeed.Items.AddRange(speeds)
	End Sub

#End Region

#Region "Private nested types"

	Private Class Wrapper(Of T)
		Private privateValue As T
		Public Property Value() As T
			Get
				Return privateValue
			End Get
			Private Set(ByVal value As T)
				privateValue = value
			End Set
		End Property
		Private ReadOnly _displayName As String

		Public Sub New(ByVal value As T, ByVal displayName As String)
			value = value
			_displayName = displayName
		End Sub

		Public Overrides Function ToString() As String
			Return _displayName
		End Function

		Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
			If TypeOf obj Is Wrapper(Of T) Then
				Dim objCast = TryCast(obj, Wrapper(Of T))
				Return objCast.Value.Equals(Value)
			End If
			If obj IsNot Nothing Then
				Return obj.Equals(Value)
			End If
			Return Value Is Nothing OrElse Value.Equals(obj)
		End Function

		Public Overrides Function GetHashCode() As Integer
			Return Value.GetHashCode()
		End Function
	End Class

#End Region

#Region "Private form events"

	Private Sub MatchingThresholdValidating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cbMatchingThreshold.Validating
		Try
			Dim target = TryCast(sender, ComboBox)
			If target Is Nothing Then
				Return
			End If
			Dim value As Integer = Utilities.MatchingThresholdFromString(target.Text)
			target.Text = Utilities.MatchingThresholdToString(value)
		Catch
			e.Cancel = True
			MessageBox.Show("Matching threshold is invalid")
		End Try
	End Sub

	Private Sub BtnResetClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
		SettingsAccesor.ResetMatchingSettings()
		LoadSettings()
	End Sub

	Private Sub BtnApplyClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
		SettingsAccesor.MatchingThreshold = Utilities.MatchingThresholdFromString(cbMatchingThreshold.Text)
		SettingsAccesor.FingersMatchingSpeed = CType(cbFingersMatchingSpeed.SelectedItem, NMatchingSpeed)
		SettingsAccesor.FingersMaximalRotation = Utilities.MaximalRotationFromDegrees(Convert.ToInt32(nudFingersMaxRotation.Value))
		SettingsAccesor.FacesMatchingSpeed = CType(cbFacesMatchingSpeed.SelectedItem, NMatchingSpeed)
		SettingsAccesor.IrisesMatchingSpeed = CType(cbIrisesMatchingSpeed.SelectedItem, NMatchingSpeed)
		SettingsAccesor.IrisesMaximalRotation = Utilities.MaximalRotationFromDegrees(Convert.ToInt32(nudIrisesMaxRotation.Value))
		SettingsAccesor.PalmsMatchingSpeed = CType(cbPalmsMatchingSpeed.SelectedItem, NMatchingSpeed)
		SettingsAccesor.PalmsMaximalRotation = Utilities.MaximalRotationFromDegrees(Convert.ToInt32(nudPalmsMaxRotation.Value))
		SettingsAccesor.SaveSettings()
	End Sub

	Private Sub SettingsPanelLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		LoadSettings()
	End Sub

#End Region

#Region "Public methods"

	Public Overrides Function GetTitle() As String
		Return "Matching parameters"
	End Function

#End Region

#Region "Private methods"

	Private Sub LoadSettings()
		SelectThreshold(cbMatchingThreshold, SettingsAccesor.MatchingThreshold)
		cbFingersMatchingSpeed.SelectedItem = SettingsAccesor.FingersMatchingSpeed

		nudFingersMaxRotation.Value = Utilities.MaximalRotationToDegrees(SettingsAccesor.FingersMaximalRotation)
		nudIrisesMaxRotation.Value = Utilities.MaximalRotationToDegrees(SettingsAccesor.IrisesMaximalRotation)
		nudPalmsMaxRotation.Value = Utilities.MaximalRotationToDegrees(SettingsAccesor.PalmsMaximalRotation)
		cbFacesMatchingSpeed.SelectedItem = SettingsAccesor.FacesMatchingSpeed
		cbIrisesMatchingSpeed.SelectedItem = SettingsAccesor.IrisesMatchingSpeed
		cbPalmsMatchingSpeed.SelectedItem = SettingsAccesor.PalmsMatchingSpeed
	End Sub

	Private Sub SelectThreshold(ByVal target As ComboBox, ByVal value As Integer)
		Dim str As String = Utilities.MatchingThresholdToString(value)
		Dim index As Integer = target.Items.IndexOf(str)
		If index <> -1 Then
			target.SelectedIndex = index
		Else
			target.Items.Insert(0, str)
			target.SelectedIndex = 0
		End If
	End Sub

#End Region
End Class
