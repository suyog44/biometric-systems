Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Namespace RecordCreateForms

	Partial Public Class ANType1RecordCreateForm
		Inherits ANRecordCreateForm
#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			nudIdc.Enabled = False
			nudIdc.Minimum = -1
			nudIdc.Value = -1

			Dim values As Array = System.Enum.GetValues(GetType(ANValidationLevel))
			For Each item As Object In values
				cbValidationLevel.Items.Add(item)
			Next item
			cbValidationLevel.SelectedIndex = 0
		End Sub

#End Region

#Region "Public properties"

		Public Property TransactionType() As String
			Get
				Return tbTransactionType.Text
			End Get
			Set(ByVal value As String)
				tbTransactionType.Text = value
			End Set
		End Property

		Public Property DestinationAgency() As String
			Get
				Return tbDestinationAgency.Text
			End Get
			Set(ByVal value As String)
				tbDestinationAgency.Text = value
			End Set
		End Property

		Public Property OriginatingAgency() As String
			Get
				Return tbOriginatingAgency.Text
			End Get
			Set(ByVal value As String)
				tbOriginatingAgency.Text = value
			End Set
		End Property

		Public Property TransactionControl() As String
			Get
				Return tbTransactionControlId.Text
			End Get
			Set(ByVal value As String)
				tbTransactionControlId.Text = value
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

		Public Property UseNISTMinutiaNeighboars() As Boolean
			Get
				Return chbUseNistMinutiaNeighbors.Checked
			End Get
			Set(ByVal value As Boolean)
				chbUseNistMinutiaNeighbors.Checked = value
			End Set
		End Property

#End Region

#Region "Private form events"

		Private Sub TbTransactionTypeValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles tbTransactionType.Validating
			Dim value As String = (CType(sender, TextBox)).Text

			If value.Length < ANType1Record.MinTransactionTypeLengthV4 OrElse value.Length > ANType1Record.MaxTransactionTypeLengthV4 Then
				errorProvider.SetError(CType(sender, TextBox), String.Format("Transaction type value must be {0} to {1} characters long", ANType1Record.MinTransactionTypeLengthV4, ANType1Record.MaxTransactionTypeLengthV4))
				e.Cancel = True
			Else
				errorProvider.SetError(CType(sender, TextBox), Nothing)
			End If
		End Sub

		Private Sub CbValidationLevelSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbValidationLevel.SelectedIndexChanged
			Dim isStandard As Boolean = ValidationLevel = ANValidationLevel.Standard
			tbTransactionType.Enabled = isStandard
			tbTransactionControlId.Enabled = tbTransactionType.Enabled
			tbOriginatingAgency.Enabled = tbTransactionControlId.Enabled
			tbDestinationAgency.Enabled = tbOriginatingAgency.Enabled
		End Sub

#End Region
	End Class
End Namespace
