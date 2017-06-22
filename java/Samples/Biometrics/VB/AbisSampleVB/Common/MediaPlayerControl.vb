Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Media
Imports System.Windows.Forms
Imports Neurotec.IO

Partial Public Class MediaPlayerControl
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _soundBuffer As NBuffer
	Private _soundPlayer As New SoundPlayer()

#End Region

#Region "Public properties"

	Public Property SoundBuffer() As NBuffer
		Get
			Return _soundBuffer
		End Get
		Set(ByVal value As NBuffer)
			Me.Stop()
			_soundBuffer = value
			btnPlay.Enabled = value IsNot Nothing AndAlso value IsNot NBuffer.Empty
			btnStop.Enabled = btnPlay.Enabled
		End Set
	End Property

#End Region

#Region "Private methods"

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		_soundPlayer.Stop()

		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	Public Sub [Stop]()
		_soundPlayer.Stop()
	End Sub

	Public Sub Start()
		Me.Stop()
		_soundPlayer.Stream = New MemoryStream(_soundBuffer.ToArray())
		_soundPlayer.Play()
	End Sub

	Private Sub BtnPlayClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnPlay.Click
		Start()
	End Sub

	Private Sub BtnStopClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
		Me.Stop()
	End Sub

	Private Sub MediaPlayerControlVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If (Not Visible) Then
			Me.Stop()
		End If
	End Sub

#End Region
End Class
