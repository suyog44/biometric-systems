Imports Neurotec.Biometrics.Standards

Partial Public Class AddRecordOptionsForm
	Inherits CbeffRecordOptionsForm
#Region "public properties"

	Public ReadOnly Property Standard() As BdifStandard
		Get
			Return CType(cbStandard.SelectedItem, BdifStandard)
		End Get
	End Property

#End Region

#Region "public constructor"

	Public Sub New()
		InitializeComponent()

		cbStandard.Items.Add(BdifStandard.Ansi)
		cbStandard.Items.Add(BdifStandard.Iso)
		cbStandard.SelectedIndex = 0
	End Sub

#End Region
End Class
