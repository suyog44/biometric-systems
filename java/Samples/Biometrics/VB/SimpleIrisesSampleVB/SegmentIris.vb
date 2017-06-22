Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images
Imports System.Linq

Partial Public Class SegmentIris
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _iris As NIris
	Private _segmentedIris As NIris

#End Region

#Region "Public properties"

	Public Property BiometricClient() As NBiometricClient
		Get
			Return _biometricClient
		End Get
		Set(ByVal value As NBiometricClient)
			_biometricClient = value
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Sub OnSegmentCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnSegmentCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			Dim status As NBiometricStatus = task.Status
			If status = NBiometricStatus.Ok Then
				Dim attributes As NEAttributes = _iris.Objects.First()
				' Segmented iris
				_segmentedIris = TryCast(attributes.Child, NIris)
				irisView2.Iris = _segmentedIris

				tbQuality.Text = attributes.Quality.ToString()
				tbGrayLevelSpread.Text = attributes.GrayScaleUtilisation.ToString()
				tbPupilToIrisRatio.Text = attributes.PupilToIrisRatio.ToString()
				tbUsableIrisArea.Text = attributes.UsableIrisArea.ToString()
				tbIrisScleraContrast.Text = attributes.IrisScleraContrast.ToString()
				tbIrisPupilContrast.Text = attributes.IrisPupilContrast.ToString()
				tbIrisPupilConcentricity.Text = attributes.IrisPupilConcentricity.ToString()
				tbPupilBoundaryCircularity.Text = attributes.PupilBoundaryCircularity.ToString()
				tbMarginAdequacy.Text = attributes.MarginAdequacy.ToString()
				tbSharpness.Text = attributes.Sharpness.ToString()
				tbInterlace.Text = attributes.Interlace.ToString()

				btnSegmentIris.Enabled = False
				btnSaveImage.Enabled = True
			Else
				MessageBox.Show(String.Format("Segmentation failed: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
			End If
			btnOpenImage.Enabled = True
		End If
	End Sub

	Private Sub ResetControls()
		irisView1.Iris = Nothing
		irisView2.Iris = Nothing

		tbQuality.Text = String.Empty
		tbGrayLevelSpread.Text = String.Empty
		tbPupilToIrisRatio.Text = String.Empty
		tbUsableIrisArea.Text = String.Empty
		tbIrisScleraContrast.Text = String.Empty
		tbIrisPupilContrast.Text = String.Empty
		tbIrisPupilConcentricity.Text = String.Empty
		tbPupilBoundaryCircularity.Text = String.Empty
		tbMarginAdequacy.Text = String.Empty
		tbSharpness.Text = String.Empty
		tbInterlace.Text = String.Empty

		btnSaveImage.Enabled = False
		btnSegmentIris.Enabled = False
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnOpenImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenImage.Click
		ResetControls()

		If openFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				_iris = New NIris With {.Image = NImage.FromFile(openFileDialog.FileName)}
				irisView1.Iris = _iris
				btnSegmentIris.Enabled = True
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub BtnSegmentIrisClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSegmentIris.Click
		If _iris IsNot Nothing Then
			_iris.ImageType = NEImageType.CroppedAndMasked
			Dim subject As New NSubject()
			subject.Irises.Add(_iris)

			Dim segmentTask As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.Segment, subject)
			_biometricClient.BeginPerformTask(segmentTask, AddressOf OnSegmentCompleted, Nothing)
			btnSegmentIris.Enabled = False
			btnOpenImage.Enabled = False
		End If
	End Sub

	Private Sub BtnSaveImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveImage.Click
		If _segmentedIris IsNot Nothing Then
			saveFileDialog.Filter = NImages.GetSaveFileFilterString()
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					_segmentedIris.Image.Save(saveFileDialog.FileName)
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub SegmentIrisLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If (Not DesignMode) Then
			btnSaveImage.Enabled = False
			btnSegmentIris.Enabled = False
			saveFileDialog.Filter = NImages.GetSaveFileFilterString()
			openFileDialog.Filter = NImages.GetOpenFileFilterString(True, False)
		End If
	End Sub

#End Region
End Class
