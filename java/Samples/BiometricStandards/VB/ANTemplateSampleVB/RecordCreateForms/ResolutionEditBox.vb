Imports System
Imports System.Windows.Forms

Namespace RecordCreateForms

	Partial Public Class ResolutionEditBox
		Inherits UserControl
#Region "Private fields"

		Private _currentUnit As ResolutionUnits.Unit

#End Region

#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			cbScaleUnits.Items.AddRange(ResolutionUnits.Units.ToArray())

			RawValue = 0
			_currentUnit = ResolutionUnits.PpmUnit
			cbScaleUnits.SelectedItem = _currentUnit
		End Sub

#End Region

#Region "Public properties"

		Public Property RawValue() As Double
			Get
				Return Double.Parse(tbValue.Text)
			End Get
			Friend Set(ByVal value As Double)
				tbValue.Text = value.ToString()
			End Set
		End Property

		Public Property PpmValue() As Double
			Get
				Return ResolutionUnits.Unit.Convert(_currentUnit, ResolutionUnits.PpmUnit, RawValue)
			End Get
			Set(ByVal value As Double)
				cbScaleUnits.SelectedItem = ResolutionUnits.PpmUnit
				RawValue = value
			End Set
		End Property

		Public Property PpcmValue() As Double
			Get
				Return ResolutionUnits.Unit.Convert(_currentUnit, ResolutionUnits.PpcmUnit, RawValue)
			End Get
			Set(ByVal value As Double)
				cbScaleUnits.SelectedItem = ResolutionUnits.PpcmUnit
				RawValue = value
			End Set
		End Property

		Public Property PpmmValue() As Double
			Get
				Return ResolutionUnits.Unit.Convert(_currentUnit, ResolutionUnits.PpmmUnit, RawValue)
			End Get
			Set(ByVal value As Double)
				cbScaleUnits.SelectedItem = ResolutionUnits.PpmmUnit
				RawValue = value
			End Set
		End Property

		Public Property PpiValue() As Double
			Get
				Return ResolutionUnits.Unit.Convert(_currentUnit, ResolutionUnits.PpiUnit, RawValue)
			End Get
			Set(ByVal value As Double)
				cbScaleUnits.SelectedItem = ResolutionUnits.PpiUnit
				RawValue = value
			End Set
		End Property

#End Region

#Region "Private form events"

		Private Sub CbScaleUnitsSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbScaleUnits.SelectedIndexChanged
			If cbScaleUnits.SelectedItem IsNot _currentUnit Then
				Dim newValue As Double = ResolutionUnits.Unit.Convert(_currentUnit, CType(cbScaleUnits.SelectedItem, ResolutionUnits.Unit), RawValue)
				RawValue = newValue
				_currentUnit = CType(cbScaleUnits.SelectedItem, ResolutionUnits.Unit)
			End If
		End Sub

#End Region
	End Class
End Namespace
