Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Images

Partial Public Class BiometricPreviewPage
	Inherits PageBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		DoubleBuffered = True
	End Sub

#End Region

#Region "Private fields"

	Private _node As SubjectTreeControl.Node
	Private _biometric As NBiometric
	Private _view As Object

#End Region

#Region "Public methods"

	Public Overrides Sub OnNavigatedTo(ByVal ParamArray args() As Object)
		If args Is Nothing OrElse args.Length <> 1 Then
			Throw New ArgumentException("args")
		End If

		Dim showZoomControls As Boolean = True
		Dim showBinarizedCheckbox As Boolean = False
		Dim canSave As Boolean = False
		_view = Nothing
		_node = CType(args(0), SubjectTreeControl.Node)
		If _node Is Nothing OrElse (Not _node.IsBiometricNode) Then
			Throw New ArgumentException("args")
		End If
		panelView.Controls.Clear()
		GeneralizeProgressView.Clear()
		generalizeProgressView.Visible = _node.IsGeneralizedNode
		icaoWarningView.Visible = False
		_biometric = _node.Items.First()
		Select Case _node.BiometricType
			Case NBiometricType.Finger, NBiometricType.Palm
				Dim first As NFrictionRidge = TryCast(_biometric, NFrictionRidge)
				Dim view As New NFingerView() With {.Dock = DockStyle.Fill, .ForeColor = Color.Red, .Finger = first}
				_view = view
				panelView.Controls.Add(view)
				horizontalZoomSlider.View = view
				showBinarizedCheckbox = first.BinarizedImage IsNot Nothing
				canSave = first.Image IsNot Nothing
				If GeneralizeProgressView.Visible Then
					Dim generalized = _node.GetAllGeneralized()
					GeneralizeProgressView.View = view
					GeneralizeProgressView.Biometrics = _node.Items
					GeneralizeProgressView.Generalized = generalized
					GeneralizeProgressView.Selected = If(generalized.FirstOrDefault(), first)
				End If

				If showBinarizedCheckbox AndAlso chbShowBinarized.Checked Then
					view.ShownImage = ShownImage.Result
				End If
				Exit Select
			Case NBiometricType.Iris
				Dim view As New NIrisView() With {.Dock = DockStyle.Fill, .Iris = TryCast(_biometric, NIris)}
				panelView.Controls.Add(view)
				horizontalZoomSlider.View = view
				_view = view
				canSave = view.Iris.Image IsNot Nothing
				Exit Select
			Case NBiometricType.Face
				Dim first As NFace = TryCast(_biometric, NFace)
				Dim view As New NFaceView() With {.Dock = DockStyle.Fill, .Face = first, .ShowIcaoArrows = False}
				_view = view
				panelView.Controls.Add(view)
				horizontalZoomSlider.View = view
				canSave = view.Face.Image IsNot Nothing
				If _node.AllItems.Cast(Of NFace).Any(AddressOf WasIcaoCheckPerformed) Then
					icaoWarningView.Visible = True
				End If
				If GeneralizeProgressView.Visible Then
					Dim generalized = _node.GetAllGeneralized()
					generalizeProgressView.View = view
					generalizeProgressView.IcaoView = icaoWarningView
					GeneralizeProgressView.Biometrics = _node.Items
					GeneralizeProgressView.Generalized = generalized
					generalizeProgressView.Selected = If(generalized.FirstOrDefault(), first)
				Else
					icaoWarningView.Face = first
				End If
				Exit Select
			Case NBiometricType.Voice
				showZoomControls = False
				Dim view As New VoiceView() With {.Voice = TryCast(_biometric, NVoice)}
				panelView.Controls.Add(view)
				_view = view
				canSave = view.Voice.SoundBuffer IsNot Nothing
				Exit Select
		End Select

		horizontalZoomSlider.Visible = showZoomControls
		chbShowBinarized.Visible = showBinarizedCheckbox
		btnSave.Visible = canSave
		btnSave.Text = If(_node.BiometricType = NBiometricType.Voice, "Save audio file", "Save image")

		AddHandler GeneralizeProgressView.PropertyChanged, AddressOf GeneralizeProgressViewPropertyChanged

		MyBase.OnNavigatedTo(args)
	End Sub

	Public Overrides Sub OnNavigatingFrom()
		RemoveHandler generalizeProgressView.PropertyChanged, AddressOf GeneralizeProgressViewPropertyChanged

		panelView.Controls.Clear()
		Dim viewControl As IDisposable = TryCast(_view, IDisposable)
		If viewControl IsNot Nothing Then
			viewControl.Dispose()
		End If
		_view = Nothing
		icaoWarningView.Face = Nothing
		generalizeProgressView.IcaoView = Nothing
		generalizeProgressView.View = Nothing

		MyBase.OnNavigatingFrom()
	End Sub

#End Region

#Region "Private static methods"

	Private Shared Function WasIcaoCheckPerformed(ByVal face As NFace) As Boolean
		Dim attributes = face.Objects.FirstOrDefault()
		If attributes IsNot Nothing AndAlso (Not attributes.TokenImageRect.IsEmpty) Then
			Return True
		Else
			Dim parentObject = CType(face.ParentObject, NLAttributes)
			Return parentObject IsNot Nothing AndAlso Not parentObject.TokenImageRect.IsEmpty
		End If
	End Function

#End Region

#Region "Private events"

	Private Sub GeneralizeProgressViewPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "Selected" Then
			If _node.BiometricType = NBiometricType.Finger OrElse _node.BiometricType = NBiometricType.Palm Then
				Dim view As NFingerView = CType(_view, NFingerView)
				Dim finger As NFrictionRidge = view.Finger
				_biometric = finger
				chbShowBinarized.Visible = finger.BinarizedImage IsNot Nothing
				If (Not chbShowBinarized.Visible) AndAlso chbShowBinarized.Checked Then
					chbShowBinarized.Checked = False
				End If
			ElseIf _node.BiometricType = NBiometricType.Face Then
				Dim view As NFaceView = CType(_view, NFaceView)
				_biometric = view.Face
			End If
		End If
	End Sub

	Private Sub BtnFinishClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnFinish.Click
		PageController.NavigateToStartPage()
		panelView.Controls.Clear()
	End Sub

	Private Sub ChbShowBinarizedCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowBinarized.CheckedChanged
		Dim view As NFingerView = TryCast(_view, NFingerView)
		If view IsNot Nothing Then
			view.ShownImage = If(chbShowBinarized.Checked, ShownImage.Result, ShownImage.Original)
		End If
	End Sub

	Private Sub BtnSaveClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
		saveFileDialog.Filter = If(TypeOf _biometric Is NVoice, String.Empty, NImages.GetSaveFileFilterString())
		saveFileDialog.FileName = String.Empty
		If saveFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				If _biometric.BiometricType = NBiometricType.Voice Then
					Dim voice As NVoice = TryCast(_biometric, NVoice)
					voice.SoundBuffer.Save(saveFileDialog.FileName)
				Else
					Dim image As NImage = Nothing
					If _biometric.BiometricType = NBiometricType.Finger OrElse _biometric.BiometricType = NBiometricType.Palm Then
						Dim frictionRidge As NFrictionRidge = CType(_biometric, NFrictionRidge)
						image = If(chbShowBinarized.Checked, frictionRidge.BinarizedImage, frictionRidge.Image)
					ElseIf _biometric.BiometricType = NBiometricType.Iris Then
						image = (CType(_biometric, NIris)).Image
					Else
						image = (CType(_biometric, NFace)).Image
					End If
					image.Save(saveFileDialog.FileName)
				End If
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If
	End Sub

#End Region
End Class
