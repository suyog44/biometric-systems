Imports Neurotec.Biometrics.Standards

Public Class SelectedFmrItem
	Protected ReadOnly FingerView As FmrFingerView
	Protected ReadOnly Index As Integer

	Public Sub New(ByVal fingerView As FmrFingerView, ByVal index As Integer)
		Me.FingerView = fingerView
		Me.Index = index
	End Sub
End Class

Public Class SelectedFmrDelta
	Inherits SelectedFmrItem
	Public Sub New(ByVal fingerView As FmrFingerView, ByVal index As Integer)
		MyBase.New(fingerView, index)
	End Sub

	Public Property Delta() As FmrDelta
		Get
			Return FingerView.Deltas(Index)
		End Get
		Set(ByVal value As FmrDelta)
			FingerView.Deltas(Index) = value
		End Set
	End Property
End Class

Public Class SelectedFmrCore
	Inherits SelectedFmrItem
	Public Sub New(ByVal fingerView As FmrFingerView, ByVal index As Integer)
		MyBase.New(fingerView, index)
	End Sub

	Public Property Core() As FmrCore
		Get
			Return FingerView.Cores(Index)
		End Get
		Set(ByVal value As FmrCore)
			FingerView.Cores(Index) = value
		End Set
	End Property
End Class

Public Class SelectedFmrMinutia
	Inherits SelectedFmrItem
	Public Sub New(ByVal fingerView As FmrFingerView, ByVal index As Integer)
		MyBase.New(fingerView, index)
	End Sub

	Public Property Minutia() As FmrMinutia
		Get
			Return FingerView.Minutiae(Index)
		End Get
		Set(ByVal value As FmrMinutia)
			FingerView.Minutiae(Index) = value
		End Set
	End Property
End Class
