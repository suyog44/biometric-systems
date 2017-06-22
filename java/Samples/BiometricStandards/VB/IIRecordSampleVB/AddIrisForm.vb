Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class AddIrisForm
	Inherits Form
	Public Sub New(ByVal version As NVersion)
		InitializeComponent()

		If version = IIRecord.VersionAnsi10 OrElse version = IIRecord.VersionIso10 Then
			cbIrisPosition.Items.Add(System.Enum.GetName(GetType(BdifEyePosition), BdifEyePosition.Left))
			cbIrisPosition.Items.Add(System.Enum.GetName(GetType(BdifEyePosition), BdifEyePosition.Right))
		Else
			cbIrisPosition.Items.AddRange(System.Enum.GetNames(GetType(BdifEyePosition)))
		End If
		cbIrisPosition.SelectedIndex = 0
	End Sub

	Public Property IrisPosition() As BdifEyePosition
		Get
			Return CType(System.Enum.Parse(GetType(BdifEyePosition), cbIrisPosition.SelectedItem.ToString()), BdifEyePosition)
		End Get
		Set(ByVal value As BdifEyePosition)
			cbIrisPosition.SelectedItem = System.Enum.GetName(GetType(BdifEyePosition), value)
		End Set
	End Property
End Class
