Imports System
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class IdentifyFace
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

	Private Sub OnEnrollCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnEnrollCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			If task.Status = NBiometricStatus.Ok Then
				' Identify current subject with enrolled ones
				_biometricClient.BeginIdentify(_subject, AddressOf OnIdentifyDone, Nothing)
			Else
				MessageBox.Show(String.Format("Enrollment failed: {0}", task.Status))
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
					For Each subject In _subjects
						Dim tmpSubject As NSubject = subject
						Dim isMatchingResult As Boolean = _subject.MatchingResults.Any(Function(result) tmpSubject.Id = result.Id)
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
				Else
					_subject = Nothing
					identifyButton.Enabled = False
				End If
			End Try
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub MatchingFarComboBoxEnter(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Enter
		defaultMatchingFARButton.Enabled = True
	End Sub

	Private Sub OpenTemplatesButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles openTemplatesButton.Click
		identifyButton.Enabled = False

		templatesCountLabel.Text = "0"
		OpenFileDialog.Multiselect = True
		OpenFileDialog.FileName = Nothing
		OpenFileDialog.Filter = Nothing
		OpenFileDialog.Title = "Open Templates Files"
		If OpenFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				Dim templatesCount As Integer = OpenFileDialog.FileNames.Length
				_subjects = New NSubject(templatesCount - 1) {}
				' Create subjects from selected templates
				For i As Integer = 0 To templatesCount - 1
					_subjects(i) = NSubject.FromFile(OpenFileDialog.FileNames(i))
					_subjects(i).Id = Path.GetFileName(OpenFileDialog.FileNames(i))
				Next i
				templatesCountLabel.Text = templatesCount.ToString(CultureInfo.InvariantCulture)
				identifyButton.Enabled = _subject IsNot Nothing
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub OpenImageButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles openImageButton.Click
		identifyButton.Enabled = False

		OpenFileDialog.FileName = Nothing
		OpenFileDialog.Filter = String.Empty

		_subject = Nothing
		faceView.Face = Nothing
		fileForIdentificationLabel.Text = String.Empty

		OpenFileDialog.Title = "Open Image File Or Template"
		If OpenFileDialog.ShowDialog() = DialogResult.OK Then
			fileForIdentificationLabel.Text = OpenFileDialog.FileName

			' Check if given file is a template
			Try
				_subject = NSubject.FromFile(OpenFileDialog.FileName)
				identifyButton.Enabled = _subjects IsNot Nothing
			Catch
			End Try

			' If file is not a template, try to load it as image
			If _subject Is Nothing Then
				' Create a face object
				Dim face = New NFace() With {.FileName = OpenFileDialog.FileName}
				faceView.Face = face
				_subject = New NSubject()
				_subject.Faces.Add(face)

				' Extract a template from the subject
				_biometricClient.BeginCreateTemplate(_subject, AddressOf OnExtractionCompleted, Nothing)
			End If
		End If
	End Sub

	Private Sub IdentifyButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles identifyButton.Click
		ListView.Items.Clear()
		If _subject IsNot Nothing AndAlso _subjects IsNot Nothing AndAlso _subjects.Length > 0 Then
			Try
				' Clean earlier data before proceeding, enroll new data
				_biometricClient.Clear()
				' Create enrollment task
				Dim enrollmentTask = New NBiometricTask(NBiometricOperations.Enroll)
				' Create subjects from templates and set them for enrollment
				For Each t As NSubject In _subjects
					enrollmentTask.Subjects.Add(t)
				Next t
				' Enroll subjects
				_biometricClient.BeginPerformTask(enrollmentTask, AddressOf OnEnrollCompleted, Nothing)
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub DefaultMatchingFARButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles defaultMatchingFARButton.Click
		matchingFarComboBox.SelectedIndex = 1
		defaultMatchingFARButton.Enabled = False
	End Sub

	Private Sub IdentifyFaceLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Try
			matchingFarComboBox.BeginUpdate()
			matchingFarComboBox.Items.Add(0.001.ToString("P1"))
			matchingFarComboBox.Items.Add(0.0001.ToString("P2"))
			matchingFarComboBox.Items.Add(0.00001.ToString("P3"))
		Finally
			matchingFarComboBox.EndUpdate()
		End Try
		matchingFarComboBox.SelectedIndex = 1
	End Sub

	Private Sub MatchingFarComboBoxLeave(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Leave
		Try
			_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(matchingFarComboBox.Text)
			matchingFarComboBox.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold)
		Catch
			matchingFarComboBox.Select()
			MessageBox.Show("FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#End Region
End Class
