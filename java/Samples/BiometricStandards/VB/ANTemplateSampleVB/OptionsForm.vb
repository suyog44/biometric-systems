Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class OptionsForm
	Inherits Form
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		Dim values() As ANValidationLevel = CType(System.Enum.GetValues(GetType(ANValidationLevel)), ANValidationLevel())
		For Each item As ANValidationLevel In values
			cbValidationLevel.Items.Add(item)
		Next item
		cbValidationLevel.SelectedIndex = 0
	End Sub

	#End Region

	#Region "Public properties"

	Public Property UseNistMinutiaeNeighboars() As Boolean
		Get
			Return chbUseNistMinutiaeNeighboars.Checked
		End Get
		Set(ByVal value As Boolean)
			chbUseNistMinutiaeNeighboars.Checked = value
		End Set
	End Property

	Public Property NonStrictRead() As Boolean
		Get
			Return chbNonStrinctRead.Checked
		End Get
		Set(ByVal value As Boolean)
			chbNonStrinctRead.Checked = value
		End Set
	End Property

	Public Property MergeDuplicateFields() As Boolean
		Get
			Return chbMergeDuplicateFields.Checked
		End Get
		Set(ByVal value As Boolean)
			chbMergeDuplicateFields.Checked = value
		End Set
	End Property

	Public Property LeaveInvalidRecordsUnvalidated() As Boolean
		Get
			Return chbLeaveInvalidUnvalidated.Checked
		End Get
		Set(ByVal value As Boolean)
			chbLeaveInvalidUnvalidated.Checked = value
		End Set
	End Property

	Public Property RecoverFromBinaryData() As Boolean
		Get
			Return chbRecover.Checked
		End Get
		Set(ByVal value As Boolean)
			chbRecover.Checked = value
		End Set
	End Property

	Public Property ValidationLevel() As ANValidationLevel
		Get
			Return CType(cbValidationLevel.SelectedItem, ANValidationLevel)
		End Get
		Set(ByVal value As ANValidationLevel)
			cbValidationLevel.SelectedItem = value
		End Set
	End Property

	#End Region

	#Region "Private methods"

	Private Sub LoadSettings()
		ValidationLevel = My.Settings.Default.ValidationLevel
		UseNistMinutiaeNeighboars = My.Settings.Default.UseNistMinutiaNeighbors
		NonStrictRead = My.Settings.Default.NonStrictRead
		MergeDuplicateFields = My.Settings.Default.MergeDuplicateFields
		RecoverFromBinaryData = My.Settings.Default.RecoverFromBinaryData
		LeaveInvalidRecordsUnvalidated = My.Settings.Default.LeaveInvalidRecordsUnvalidated
	End Sub

	Private Sub SaveSettings()
		My.Settings.Default.ValidationLevel = ValidationLevel
		My.Settings.Default.UseNistMinutiaNeighbors = UseNistMinutiaeNeighboars
		My.Settings.Default.NonStrictRead = NonStrictRead
		My.Settings.Default.MergeDuplicateFields = MergeDuplicateFields
		My.Settings.Default.RecoverFromBinaryData = RecoverFromBinaryData
		My.Settings.Default.LeaveInvalidRecordsUnvalidated = LeaveInvalidRecordsUnvalidated
		My.Settings.Default.Save()
	End Sub

	#End Region

	#Region "Private form events"

	Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
		SaveSettings()
		DialogResult = Windows.Forms.DialogResult.OK
	End Sub

	Private Sub OpenOptionsFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		LoadSettings()
	End Sub

	#End Region
End Class
