Imports System
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui

Partial Public Class IdentifyFinger
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _subjects() As NSubject
	Private _enrollStatus As NBiometricStatus = NBiometricStatus.None

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

	Private Sub SetMatchingThreshold()
		Try
			_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(matchingFarComboBox.Text)
			matchingFarComboBox.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold)
		Catch
			matchingFarComboBox.Select()
			MessageBox.Show("FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub OnEnrollCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnEnrollCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			_enrollStatus = task.Status
			If task.Status = NBiometricStatus.Ok Then
				identifyButton.Enabled = IsSubjectValid(_subject)
			Else
				MessageBox.Show(String.Format("Enrollment failed: {0}", _enrollStatus))
			End If
		End If
	End Sub

	Private Sub OnIdentifyDone(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnIdentifyDone), r)
		Else
			Try
				Dim status As NBiometricStatus = _biometricClient.EndIdentify(r)
				If status = NBiometricStatus.Ok OrElse status = NBiometricStatus.MatchNotFound Then
					' Matching subjects
					For Each result In _subject.MatchingResults
						listView.Items.Add(New ListViewItem(New String() {result.Id, result.Score.ToString(CultureInfo.InvariantCulture)}))
					Next result
					' Non-matching subjects
					For Each subject In _subjects
						Dim id = subject.Id
						Dim isMatchingResult As Boolean = _subject.MatchingResults.Any(Function(result) id = result.Id)
						If (Not isMatchingResult) Then
							listView.Items.Add(New ListViewItem(New String() {subject.Id, "0"}))
						End If
					Next subject
				Else
					MessageBox.Show(String.Format("Identification failed: {0}", status))
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub OnExtractionCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnExtractionCompleted), r)
		Else
			Dim status As NBiometricStatus = NBiometricStatus.None
			Try
				status = _biometricClient.EndCreateTemplate(r)
				If status <> NBiometricStatus.Ok Then
					MessageBox.Show(String.Format("Template was not extracted: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
			Finally
				If status = NBiometricStatus.Ok Then
					identifyButton.Enabled = _subjects IsNot Nothing
					chbShowBinarizedImage.Enabled = True
					SetShownImage()
				Else
					_subject = Nothing
					identifyButton.Enabled = False
				End If
			End Try
		End If
	End Sub

	Private Sub SetDefaultFar()
		matchingFarComboBox.SelectedIndex = 1
		defaultMatchingFARButton.Enabled = False
		SetMatchingThreshold()
	End Sub

	Private Function IsSubjectValid(ByVal subject As NSubject) As Boolean
		Return subject IsNot Nothing AndAlso (subject.Status = NBiometricStatus.Ok OrElse subject.Status = NBiometricStatus.None AndAlso subject.GetTemplateBuffer() IsNot Nothing)
	End Function

	Private Sub SetShownImage()
		fingerView.ShownImage = If(chbShowBinarizedImage.Checked, ShownImage.Result, ShownImage.Original)
	End Sub

#End Region

#Region "Private form events"

	Private Sub IdentifyFingerLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		fileForIdentificationLabel.Text = String.Empty
		templatesCountLabel.Text = "0"

		matchingFarComboBox.BeginUpdate()
		matchingFarComboBox.Items.Add(0.001.ToString("P1"))
		matchingFarComboBox.Items.Add(0.0001.ToString("P2"))
		matchingFarComboBox.Items.Add(0.00001.ToString("P3"))
		matchingFarComboBox.EndUpdate()
		matchingFarComboBox.SelectedIndex = 1
	End Sub

	Private Sub OpenTemplatesButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles openTemplatesButton.Click
		identifyButton.Enabled = False
		_enrollStatus = NBiometricStatus.None
		_subjects = Nothing

		templatesCountLabel.Text = "0"
		openFileDialog.Multiselect = True
		openFileDialog.FileName = Nothing
		openFileDialog.Filter = String.Empty
		openFileDialog.Title = "Open Templates Files"
		If openFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				Dim templatesCount As Integer = openFileDialog.FileNames.Length
				_biometricClient.Clear()
				_subjects = New NSubject(templatesCount - 1) {}
				For i As Integer = 0 To templatesCount - 1
					_subjects(i) = NSubject.FromFile(openFileDialog.FileNames(i))
					_subjects(i).Id = Path.GetFileName(openFileDialog.FileNames(i))
				Next i
				templatesCountLabel.Text = templatesCount.ToString(CultureInfo.InvariantCulture)

				Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.Enroll, Nothing)
				For Each item As NSubject In _subjects
					task.Subjects.Add(item)
				Next item
				_biometricClient.BeginPerformTask(task, AddressOf OnEnrollCompleted, Nothing)
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub OpenImageButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles openImageButton.Click
		identifyButton.Enabled = False
		chbShowBinarizedImage.Enabled = False
		_subject = Nothing

		openFileDialog.FileName = Nothing
		openFileDialog.Filter = String.Empty
		openFileDialog.Title = "Open Image File Or Template"

		fingerView.Finger = Nothing
		fileForIdentificationLabel.Text = String.Empty
		If openFileDialog.ShowDialog() = DialogResult.OK Then
			fileForIdentificationLabel.Text = openFileDialog.FileName

			' Check if given file is a template
			Try
				_subject = NSubject.FromFile(openFileDialog.FileName)
			Catch
			End Try

			' If file is not a template, try to load it as image
			If _subject IsNot Nothing AndAlso _subject.Fingers.Count > 0 Then
				fingerView.Finger = _subject.Fingers(0)
				identifyButton.Enabled = _subjects IsNot Nothing
			Else
				' Create a finger object
				Dim finger = New NFinger With {.FileName = openFileDialog.FileName}
				fingerView.Finger = finger
				' Add the finger to a subject
				_subject = New NSubject()
				_subject.Fingers.Add(finger)

				' Extract the template from the subject
				_biometricClient.FingersReturnBinarizedImage = True
				_biometricClient.BeginCreateTemplate(_subject, AddressOf OnExtractionCompleted, Nothing)
			End If
		End If
	End Sub

	Private Sub IdentifyButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles identifyButton.Click
		listView.Items.Clear()
		If _subject IsNot Nothing AndAlso _subjects IsNot Nothing AndAlso _subjects.Length > 0 Then
			_biometricClient.BeginIdentify(_subject, AddressOf OnIdentifyDone, Nothing)
		End If
	End Sub

	Private Sub DefaultButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles defaultButton.Click
		thresholdNumericUpDown.Value = 39
		defaultButton.Enabled = False
	End Sub

	Private Sub ThresholdNumericUpDownEnter(ByVal sender As Object, ByVal e As EventArgs)
		defaultButton.Enabled = True
	End Sub

	Private Sub MatchingFarComboBoxEnter(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Enter
		defaultMatchingFARButton.Enabled = True
	End Sub

	Private Sub DefaultMatchingFarButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles defaultMatchingFARButton.Click
		SetDefaultFar()
	End Sub

	Private Sub IdentifyFingerVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If Visible AndAlso _biometricClient IsNot Nothing Then
			thresholdNumericUpDown.Value = 39
			thresholdNumericUpDown.Enabled = True
			defaultButton.Enabled = True
			SetDefaultFar()
		End If
	End Sub

	Private Sub MatchingFarComboBoxLeave(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Leave
		SetMatchingThreshold()
	End Sub

	Private Sub ThresholdNumericUpDownValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles thresholdNumericUpDown.ValueChanged
		If _biometricClient IsNot Nothing Then
			_biometricClient.FingersQualityThreshold = CByte(thresholdNumericUpDown.Value)
			defaultButton.Enabled = True
		End If
	End Sub

	Private Sub ChbShowBinarizedImageCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowBinarizedImage.CheckedChanged
		SetShownImage()
	End Sub

	Private Sub FingerViewMouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fingerView.MouseClick
		If e.Button = MouseButtons.Right AndAlso chbShowBinarizedImage.Enabled Then
			chbShowBinarizedImage.Checked = Not chbShowBinarizedImage.Checked
		End If
	End Sub

#End Region

End Class
