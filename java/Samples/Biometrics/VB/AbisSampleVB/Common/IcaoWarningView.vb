Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics

Partial Public Class IcaoWarningView
	Inherits UserControl
#Region "Private fields"

	Private _face As NFace
	Private _attributes As NLAttributes
	Private _noWarning As Color = Color.Green
	Private _warningColor As Color = Color.Red
	Private _indeterminateColor As Color = Color.Orange

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Public properties"

	Public Property Face() As NFace
		Get
			Return _face
		End Get
		Set(ByVal value As NFace)
			If _face IsNot value Then
				UnsubscribeFromFaceEvents()
				_face = value
				SubscribeToFaceEvents()
				UpdateUI()
				Invalidate()
			End If
		End Set
	End Property

	Public Property NoWarningColor() As Color
		Get
			Return _noWarning
		End Get
		Set(ByVal value As Color)
			_noWarning = value
		End Set
	End Property

	Public Property WarningColor() As Color
		Get
			Return _warningColor
		End Get
		Set(ByVal value As Color)
			_warningColor = value
		End Set
	End Property

	Public Property IndeterminateColor() As Color
		Get
			Return _indeterminateColor
		End Get
		Set(ByVal value As Color)
			_indeterminateColor = value
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Function GetLabels() As IEnumerable(Of Label)
		Return New Label() {lblFaceDetected, lblExpression, lblDarkGlasses, lblBlink, lblMouthOpen, lblRoll, lblYaw, lblPitch, lblTooClose, lblTooFar, lblTooNorth, lblTooSouth, lblTooEast, lblTooWest, lblSharpness, lblGrayscaleDensity, lblSaturation, lblBackgroundUniformity}
	End Function

	Private Function GetColorForConfidence(ByVal warnings As NIcaoWarnings, ByVal flag As NIcaoWarnings, ByVal confidence As Byte) As Color
		If (warnings And flag) = flag Then
			Return If(confidence <= 100, WarningColor, IndeterminateColor)
		End If
		Return NoWarningColor
	End Function

	Private Function GetColorForFlags(ByVal warnings As NIcaoWarnings, ByVal ParamArray flags() As NIcaoWarnings) As Color
		Return If(flags.Any(Function(f) (f And warnings) = f), WarningColor, NoWarningColor)
	End Function

	Private Function GetConfidenceString(ByVal name As String, ByVal value As Byte) As String
		Return String.Format("{0}: {1}", name, If(value <= 100, value.ToString(), "N/A"))
	End Function

	Private Sub UpdateUI()
		If _attributes IsNot Nothing Then
			Dim warnings = _attributes.IcaoWarnings
			If (warnings And NIcaoWarnings.FaceNotDetected) = NIcaoWarnings.FaceNotDetected Then
				For Each lbl In GetLabels()
					lbl.ForeColor = IndeterminateColor
				Next lbl
				lblFaceDetected.ForeColor = WarningColor
			Else
				lblFaceDetected.ForeColor = NoWarningColor
				lblExpression.ForeColor = GetColorForConfidence(warnings, NIcaoWarnings.Expression, _attributes.ExpressionConfidence)
				lblDarkGlasses.ForeColor = GetColorForConfidence(warnings, NIcaoWarnings.DarkGlasses, _attributes.DarkGlassesConfidence)
				lblBlink.ForeColor = GetColorForConfidence(warnings, NIcaoWarnings.Blink, _attributes.BlinkConfidence)
				lblMouthOpen.ForeColor = GetColorForConfidence(warnings, NIcaoWarnings.MouthOpen, _attributes.MouthOpenConfidence)
				lblRoll.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.RollLeft, NIcaoWarnings.RollRight)
				lblYaw.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.YawLeft, NIcaoWarnings.YawRight)
				lblPitch.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.PitchDown, NIcaoWarnings.PitchUp)
				lblTooClose.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooNear)
				lblTooFar.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooFar)
				lblTooNorth.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooNorth)
				lblTooSouth.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooSouth)
				lblTooWest.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooWest)
				lblTooEast.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooEast)

				lblSharpness.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.Sharpness)
				lblSharpness.Text = GetConfidenceString("Sharpness", _attributes.Sharpness)
				lblSaturation.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.Saturation)
				lblSaturation.Text = GetConfidenceString("Saturation", _attributes.Saturation)
				lblGrayscaleDensity.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.GrayscaleDensity)
				lblGrayscaleDensity.Text = GetConfidenceString("Grayscale Density", _attributes.GrayscaleDensity)
				lblBackgroundUniformity.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.BackgroundUniformity)
				lblBackgroundUniformity.Text = GetConfidenceString("Background Uniformity", _attributes.BackgroundUniformity)
			End If
		Else
			For Each lbl In GetLabels()
				lbl.ForeColor = IndeterminateColor
			Next lbl
		End If
	End Sub

	Private Sub UnsubscribeFromFaceEvents()
		If _face IsNot Nothing Then
			RemoveHandler _face.Objects.CollectionChanged, AddressOf OnObjectsCollectionChanged
		End If
		If _attributes IsNot Nothing Then
			RemoveHandler _attributes.PropertyChanged, AddressOf OnAttributesPropertyChanged
		End If
	End Sub

	Private Sub SubscribeToFaceEvents()
		If _face IsNot Nothing Then
			AddHandler _face.Objects.CollectionChanged, AddressOf OnObjectsCollectionChanged
			_attributes = _face.Objects.ToArray().FirstOrDefault()
			If _attributes IsNot Nothing Then
				AddHandler _attributes.PropertyChanged, AddressOf OnAttributesPropertyChanged
			End If
		End If
	End Sub

	Private Sub OnAttributesPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If (Not IsDisposed) AndAlso IsHandleCreated Then
			If e.PropertyName = "IcaoWarnings" Then
				BeginInvoke(New MethodInvoker(AddressOf UpdateUI))
			End If
		End If
	End Sub

	Private Sub OnAttributesAdded(ByVal sender As Object, ByVal attributes As NLAttributes)
		If Object.Equals((CType(sender, NFace.ObjectCollection)).Owner, _face) Then
			If _attributes IsNot Nothing Then
				RemoveHandler _attributes.PropertyChanged, AddressOf OnAttributesPropertyChanged
			End If
			_attributes = attributes
			AddHandler _attributes.PropertyChanged, AddressOf OnAttributesPropertyChanged
		End If
	End Sub

	Private Sub OnAttributesRemoved(ByVal sender As Object)
		If Object.Equals(sender, _face) Then
			If _attributes IsNot Nothing Then
				RemoveHandler _attributes.PropertyChanged, AddressOf OnAttributesPropertyChanged
			End If
			_attributes = Nothing
		End If
	End Sub

	Private Sub OnObjectsCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
		If (Not IsDisposed) AndAlso IsHandleCreated Then
			If e.Action = NotifyCollectionChangedAction.Add Then
				BeginInvoke(New Action(Of Object, NLAttributes)(AddressOf OnAttributesAdded), sender, CType(e.NewItems(0), NLAttributes))
			ElseIf e.Action = NotifyCollectionChangedAction.Remove OrElse e.Action = NotifyCollectionChangedAction.Reset Then
				BeginInvoke(New Action(Of Object)(AddressOf OnAttributesRemoved), sender)
			End If
		End If
	End Sub

#End Region

#Region "Protected methods"

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		UnsubscribeFromFaceEvents()
		_face = Nothing
		_attributes = Nothing

		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#End Region
End Class
