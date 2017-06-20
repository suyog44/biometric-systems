Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class SegmentFingerprints
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		OpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		OpenFileDialog.Filter = OpenFileDialog.Filter

		lbPosition.Items.Add(NFPosition.PlainLeftFourFingers)
		lbPosition.Items.Add(NFPosition.PlainRightFourFingers)
		lbPosition.Items.Add(NFPosition.PlainThumbs)
		lbPosition.Items.Add(NFPosition.LeftLittle)
		lbPosition.Items.Add(NFPosition.LeftRing)
		lbPosition.Items.Add(NFPosition.LeftMiddle)
		lbPosition.Items.Add(NFPosition.LeftIndex)
		lbPosition.Items.Add(NFPosition.LeftThumb)
		lbPosition.Items.Add(NFPosition.RightThumb)
		lbPosition.Items.Add(NFPosition.RightIndex)
		lbPosition.Items.Add(NFPosition.RightMiddle)
		lbPosition.Items.Add(NFPosition.RightRing)
		lbPosition.Items.Add(NFPosition.RightLittle)
		lbPosition.SelectedIndex = 0
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _image As NImage

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

#Region "Private form events"

	Private Sub ClearSegmentInfo()
		pictureBox4.Image = Nothing
		pictureBox3.Image = pictureBox4.Image
		pictureBox2.Image = pictureBox3.Image
		pictureBox1.Image = pictureBox2.Image
		lbPosition4.Text = "Position:"
		lbPosition3.Text = lbPosition4.Text
		lbPosition2.Text = lbPosition3.Text
		lbPosition1.Text = lbPosition2.Text
		lbQuality4.Text = "Quality:"
		lbQuality3.Text = lbQuality4.Text
		lbQuality2.Text = lbQuality3.Text
		lbQuality1.Text = lbQuality2.Text
		lbClass4.Text = "Class:"
		lbClass3.Text = lbClass4.Text
		lbClass2.Text = lbClass3.Text
		lbClass1.Text = lbClass2.Text
	End Sub

	Private Sub Segment()
		If _image Is Nothing Then
			Return
		End If
		_subject = New NSubject()
		Dim finger = New NFinger With {.Image = _image}
		_subject.Fingers.Add(finger)
		originalFingerView.Finger = finger
		finger.Position = CType(lbPosition.SelectedItem, NFPosition)
		_subject.MissingFingers.Clear()
		For Each o As Object In chlbMissing.CheckedItems
			_subject.MissingFingers.Add(CType(o, NFPosition))
		Next o
		_biometricClient.FingersDeterminePatternClass = True
		_biometricClient.FingersCalculateNfiq = True
		Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.Segment Or NBiometricOperations.CreateTemplate Or NBiometricOperations.AssessQuality, _subject)
		_biometricClient.BeginPerformTask(task, AddressOf OnSegmentCompleted, Nothing)
	End Sub

	Private Sub OnSegmentCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnSegmentCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			Dim status As NBiometricStatus = task.Status
			If task.Error IsNot Nothing Then
				Utils.ShowException(task.Error)
			End If
			lblStatus.Text = String.Format("Segmentation status: {0}", status)
			lblStatus.ForeColor = If(status = NBiometricStatus.Ok, Color.Green, Color.Red)
			ShowSegments()
			btnSaveImages.Enabled = status = NBiometricStatus.Ok
		End If
	End Sub

	Private Sub ShowSegments()
		Dim segmentsCount As Integer = _subject.Fingers.Count - 1
		If segmentsCount > 0 AndAlso _subject.Fingers(1).Status = NBiometricStatus.Ok Then
			SetSegmentInfo(_subject.Fingers(1), lbPosition1, lbQuality1, lbClass1, pictureBox1)
		End If
		If segmentsCount > 1 AndAlso _subject.Fingers(2).Status = NBiometricStatus.Ok Then
			SetSegmentInfo(_subject.Fingers(2), lbPosition2, lbQuality2, lbClass2, pictureBox2)
		End If
		If segmentsCount > 2 AndAlso _subject.Fingers(3).Status = NBiometricStatus.Ok Then
			SetSegmentInfo(_subject.Fingers(3), lbPosition3, lbQuality3, lbClass3, pictureBox3)
		End If
		If segmentsCount > 3 AndAlso _subject.Fingers(4).Status = NBiometricStatus.Ok Then
			SetSegmentInfo(_subject.Fingers(4), lbPosition4, lbQuality4, lbClass4, pictureBox4)
		End If
	End Sub

	Private Sub SetSegmentInfo(ByVal finger As NFinger, ByVal position As Label, ByVal quality As Label, ByVal patternClass As Label, ByVal pictureBox As PictureBox)
		Dim attributes As NFAttributes = finger.Objects.FirstOrDefault()
		position.Text = "Position: " & finger.Position.ToString()
		If attributes IsNot Nothing Then
			quality.Text = "Quality: " & attributes.NfiqQuality.ToString()
			patternClass.Text = "Class: " & attributes.PatternClass.ToString()
		End If
		pictureBox.Image = finger.Image.ToBitmap()
	End Sub

	Private Sub PrepareForSegment()
		btnSaveImages.Enabled = False
		lblStatus.Text = String.Empty
		ClearSegmentInfo()
	End Sub

	Private Sub OpenButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles openButton.Click
		PrepareForSegment()
		If openFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				_image = NImage.FromFile(openFileDialog.FileName)
				Segment()
			Catch ex As Exception
				Utils.ShowException(ex)
				lblStatus.Text = "Segmentation status: Error"
			End Try
		End If
	End Sub

	Private Sub LbPositionSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbPosition.SelectedIndexChanged
		Dim position = CType(lbPosition.SelectedItem, NFPosition)
		chlbMissing.Items.Clear()
		If (Not NBiometricTypes.IsPositionSingleFinger(position)) Then
			For Each item As NFPosition In NBiometricTypes.GetPositionAvailableParts(position, Nothing)
				chlbMissing.Items.Add(item)
			Next item
		End If
	End Sub

	Private Sub BtnSaveImagesClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveImages.Click
		If folderBrowserDialog.ShowDialog() = DialogResult.OK Then
			Try
				For i As Integer = 1 To _subject.Fingers.Count - 1
					If _subject.Fingers(i).Status = NBiometricStatus.Ok Then
						Dim name As String = String.Format("finger{0} {1}{2}", i, _subject.Fingers(i).Position.ToString(), ".png")
						Dim path As String = System.IO.Path.Combine(folderBrowserDialog.SelectedPath, name)
						_subject.Fingers(i).Image.Save(path)
					End If
				Next i
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub SegmentButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles segmentButton.Click
		If _image IsNot Nothing Then
			PrepareForSegment()
			Segment()
		Else
			MessageBox.Show(String.Format("No image selected!"), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End If
	End Sub

#End Region

End Class
