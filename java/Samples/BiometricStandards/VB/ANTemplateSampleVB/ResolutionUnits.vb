
Imports System.Collections.Generic

Public NotInheritable Class ResolutionUnits
	#Region "Nested types"

	Public Class Unit
		#Region "Private fields"

		Private ReadOnly _name As String
		Private ReadOnly _relationWithMeter As Double

		#End Region

		#Region "Public constructor"

		Public Sub New(ByVal name As String, ByVal relationWithMeter As Double)
			_name = name
			_relationWithMeter = relationWithMeter
		End Sub

		#End Region

		#Region "Public methods"

		Public Shared Function Convert(ByVal sourceUnit As Unit, ByVal destinationUnit As Unit, ByVal value As Double) As Double
			Return value * destinationUnit._relationWithMeter / sourceUnit._relationWithMeter
		End Function

		Public Overrides Function ToString() As String
			Return _name
		End Function

		#End Region
	End Class

	#End Region

	#Region "Private static fields"

	Private Shared _ppmUnit As New Unit("ppm", 1.0)
	Private Shared _ppcmUnit As New Unit("ppcm", 0.01)
	Private Shared _ppmmUnit As New Unit("ppmm", 0.001)
	Private Shared _ppiUnit As New Unit("ppi", 0.0254)
	Private Shared _resolutionUnits As New List(Of Unit)()

	#End Region

	#Region "Static constructor"

	Private Sub New()
	End Sub
	Shared Sub New()
		_resolutionUnits.Add(_ppmUnit)
		_resolutionUnits.Add(_ppcmUnit)
		_resolutionUnits.Add(_ppmmUnit)
		_resolutionUnits.Add(_ppiUnit)
	End Sub

	#End Region

	#Region "Public static properties"

	Public Shared ReadOnly Property Units() As List(Of Unit)
		Get
			Return _resolutionUnits
		End Get
	End Property

	Public Shared ReadOnly Property PpmUnit() As Unit
		Get
			Return _ppmUnit
		End Get
	End Property

	Public Shared ReadOnly Property PpcmUnit() As Unit
		Get
			Return _ppcmUnit
		End Get
	End Property

	Public Shared ReadOnly Property PpmmUnit() As Unit
		Get
			Return _ppmmUnit
		End Get
	End Property

	Public Shared ReadOnly Property PpiUnit() As Unit
		Get
			Return _ppiUnit
		End Get
	End Property

	#End Region
End Class
