Imports Microsoft.VisualBasic
Imports System

Partial Public Class GeneralSettingsPage
	Inherits Neurotec.Samples.SettingsPageBase
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(12))
		cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(24))
		cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(36))
		cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(48))
		cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(60))
		cbMatchingThreshold.Items.Add(Utilities.MatchingThresholdToString(72))

		AddHandler cbMatchingThreshold.SelectedIndexChanged, AddressOf CbMatchingThresholdSelectedIndexChanged
		AddHandler nudResultsCount.ValueChanged, AddressOf NudResultsCountValueChanged
		AddHandler chbMatchWithDetails.CheckedChanged, AddressOf ChbMatchWithDetailsCheckedChanged
		AddHandler chbFirstResult.CheckedChanged, AddressOf ChbFirstResultCheckedChanged
	End Sub

	#End Region

	#Region "Public methods"

	Public Overrides Sub LoadSettings()
		cbMatchingThreshold.SelectedItem = Utilities.MatchingThresholdToString(Client.MatchingThreshold)
		nudResultsCount.Value = Client.MatchingMaximalResultCount
		chbFirstResult.Checked = Client.MatchingFirstResultOnly
		chbMatchWithDetails.Checked = Client.MatchingWithDetails

		MyBase.LoadSettings()
	End Sub

	Public Overrides Sub DefaultSettings()
		Client.ResetProperty("Matching.Threshold")
		Client.ResetProperty("Matching.MaximalResultCount")
		Client.ResetProperty("Matching.FirstResultOnly")
		Client.MatchingWithDetails = True
		MyBase.DefaultSettings()
	End Sub

	#End Region

	#Region "Public events"

	Private Sub CbMatchingThresholdSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.MatchingThreshold = Utilities.MatchingThresholdFromString(CStr(cbMatchingThreshold.SelectedItem))
	End Sub

	Private Sub NudResultsCountValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.MatchingMaximalResultCount = Convert.ToInt32(nudResultsCount.Value)
	End Sub

	Private Sub ChbMatchWithDetailsCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.MatchingWithDetails = chbMatchWithDetails.Checked
	End Sub

	Private Sub ChbFirstResultCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.MatchingFirstResultOnly = chbFirstResult.Checked
	End Sub

	#End Region
End Class
