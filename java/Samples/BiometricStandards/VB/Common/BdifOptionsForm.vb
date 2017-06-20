Imports System
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards
Imports System.ComponentModel

Partial Public Class BdifOptionsForm
	Inherits Form
#Region "Public types"

	Protected Class StandardVersion
		Private privateStandard As BdifStandard
		Public Property Standard() As BdifStandard
			Get
				Return privateStandard
			End Get
			Set(ByVal value As BdifStandard)
				privateStandard = value
			End Set
		End Property
		Private privateVersion As NVersion
		Public Property Version() As NVersion
			Get
				Return privateVersion
			End Get
			Set(ByVal value As NVersion)
				privateVersion = value
			End Set
		End Property
		Private privateStandardName As String
		Public Property StandardName() As String
			Get
				Return privateStandardName
			End Get
			Set(ByVal value As String)
				privateStandardName = value
			End Set
		End Property

		Public Sub New()

		End Sub

		Public Overrides Function ToString() As String
			Return String.Format("{0}, {1}", Version, StandardName)
		End Function
	End Class

	Public Enum BdifOptionsFormMode
		[New] = 1
		Open = 2
		Save = 3
		Convert = 4
	End Enum

#End Region

#Region "Public Constructor"

	Public Sub New()
		InitializeComponent()

		cbBiometricStandard.Items.Add(BdifStandard.Ansi)
		cbBiometricStandard.Items.Add(BdifStandard.Iso)

		cbBiometricStandard.SelectedIndex = 0
	End Sub

#End Region

#Region "Public properties"

	Private _mode As BdifOptionsFormMode = BdifOptionsFormMode.New
	Public Property Mode() As BdifOptionsFormMode
		Get
			Return _mode
		End Get
		Set(ByVal value As BdifOptionsFormMode)
			If _mode <> value Then
				_mode = value
				OnModeChanged()
			End If
		End Set
	End Property

	Public Property Standard() As BdifStandard
		Get
			Return CType(cbBiometricStandard.SelectedItem, BdifStandard)
		End Get
		Set(ByVal value As BdifStandard)
			cbBiometricStandard.SelectedItem = value
			OnStandardChanged()
		End Set
	End Property

	<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
	Public Property Version() As NVersion
		Get
			Dim standardVersion = CType(cbVersion.SelectedItem, StandardVersion)
			Return standardVersion.Version
		End Get
		Set(ByVal value As NVersion)
			Dim standardVersion As StandardVersion = StandardVersions.Where(Function(x) x.Standard = Standard AndAlso x.Version = Version).FirstOrDefault()
			If standardVersion Is Nothing Then
				Throw New ArgumentException("Version is invalid")
			End If
			cbVersion.SelectedItem = standardVersion
		End Set
	End Property

	Public Overridable Property Flags() As UInteger
		Get
			'INSTANT VB NOTE: The local variable flags was renamed since Visual Basic will not allow local variables with the same name as their enclosing function or property:
			Dim flags_Renamed As UInteger = 0
			If cbDoNotCheckCbeffProductId.Checked Then
				flags_Renamed = flags_Renamed Or BdifTypes.FlagDoNotCheckCbeffProductId
			End If
			If cbNoStrictRead.Checked Then
				flags_Renamed = flags_Renamed Or BdifTypes.FlagNonStrictRead
			End If
			Return flags_Renamed
		End Get
		Set(ByVal value As UInteger)
			If (value And BdifTypes.FlagDoNotCheckCbeffProductId) = BdifTypes.FlagDoNotCheckCbeffProductId Then
				cbDoNotCheckCbeffProductId.Checked = True
			End If
			If (value And BdifTypes.FlagNonStrictRead) = BdifTypes.FlagNonStrictRead Then
				cbNoStrictRead.Checked = True
			End If
		End Set
	End Property

#End Region

#Region "Protected properties"

	Private _versions() As StandardVersion
	Protected Property StandardVersions() As StandardVersion()
		Get
			Return _versions
		End Get
		Set(ByVal value As StandardVersion())
			_versions = value
			OnStandardChanged()
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Sub OnStandardChanged()
		If cbVersion Is Nothing OrElse StandardVersions Is Nothing Then
			Return
		End If
		cbVersion.BeginUpdate()
		cbVersion.Items.Clear()
		Try
			cbVersion.Items.AddRange(StandardVersions.Where(Function(x) x.Standard = Standard).ToArray())
			cbVersion.SelectedIndex = cbVersion.Items.Count - 1
		Finally
			cbVersion.EndUpdate()
		End Try
	End Sub

#End Region

#Region "Events"

	Protected Overridable Sub OnModeChanged()
		Text = System.Enum.GetName(GetType(BdifOptionsFormMode), Mode)

		Select Case Mode
			Case BdifOptionsFormMode.Open
				cbVersion.Enabled = False
				lbVersion.Enabled = False
			Case BdifOptionsFormMode.New, BdifOptionsFormMode.Save, BdifOptionsFormMode.Convert
				cbNoStrictRead.Enabled = False
		End Select
	End Sub

	Private Sub cbBiometricStandard_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbBiometricStandard.SelectedIndexChanged
		OnStandardChanged()
	End Sub

#End Region
End Class
