Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class AddFingerForm
	Inherits Form
	Public Sub New()
		InitializeComponent()

		cbFingerPosition.Items.AddRange(System.Enum.GetNames(GetType(BdifFPPosition)))
		cbFingerPosition.SelectedIndex = 0
	End Sub

	Public Property FingerPosition() As BdifFPPosition
		Get
			Return CType(System.Enum.Parse(GetType(BdifFPPosition), cbFingerPosition.SelectedItem.ToString()), BdifFPPosition)
		End Get
		Set(ByVal value As BdifFPPosition)
			cbFingerPosition.SelectedText = value.ToString()
		End Set
	End Property
End Class
