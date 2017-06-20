Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.IO

Partial Public Class IdentifyVoice
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
				btnIdentify.Enabled = IsSubjectValid(_subject)
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
						listView.Items.Add(New ListViewItem(New String() {result.Id, result.Score.ToString()}))
					Next result
					Dim isMatchingResult As Boolean
					' Non-matching subjects
					For Each subject In _subjects
						isMatchingResult = False
						For Each result In _subject.MatchingResults
							If subject.Id = result.Id Then
								isMatchingResult = True
								Exit For
							End If
						Next result
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
					btnIdentify.Enabled = _subjects IsNot Nothing
				Else
					_subject = Nothing
					btnIdentify.Enabled = False
				End If
			End Try
		End If
	End Sub

	Private Sub SetFar()
		_biometricClient.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked

		Try
			_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(cbMatchingFar.Text)
			cbMatchingFar.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold)
		Catch
			MessageBox.Show("FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
			cbMatchingFar.Select()
		End Try
	End Sub

	Private Sub SetDefaultFar()
		cbMatchingFar.SelectedIndex = 1
		btnDefault.Enabled = False
		SetFar()
	End Sub

	Private Function IsSubjectValid(ByVal subject As NSubject) As Boolean
		Return subject IsNot Nothing AndAlso (subject.Status = NBiometricStatus.Ok OrElse subject.Status = NBiometricStatus.None AndAlso subject.GetTemplateBuffer() IsNot Nothing)
	End Function

#End Region

#Region "Private form events"

	Private Sub BtnOpenTemplatesClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenTemplates.Click
		btnIdentify.Enabled = False

		lblTemplatesCount.Text = "0"
		_subjects = Nothing
		OpenFileDialog.Multiselect = True
		OpenFileDialog.FileName = Nothing
		openFileDialog.Filter = Nothing
		OpenFileDialog.Title = "Open Templates Files"
		If OpenFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				Dim templatesCount As Integer = OpenFileDialog.FileNames.Length
				_biometricClient.Clear() ' Clear previously enrolled subjects
				_subjects = New NSubject(templatesCount - 1) {}
				For i As Integer = 0 To templatesCount - 1
					_subjects(i) = NSubject.FromFile(OpenFileDialog.FileNames(i))
					_subjects(i).Id = Path.GetFileName(OpenFileDialog.FileNames(i))
				Next i
				lblTemplatesCount.Text = templatesCount.ToString()

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

	Private Sub BtnOpenAudioFileClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenAudio.Click
		btnIdentify.Enabled = False

		OpenFileDialog.FileName = Nothing
		OpenFileDialog.Filter = String.Empty
		OpenFileDialog.Title = "Open Audio File Or Template"

		lblFileForIdentification.Text = String.Empty
		_subject = Nothing
		If OpenFileDialog.ShowDialog() = DialogResult.OK Then
			lblFileForIdentification.Text = OpenFileDialog.FileName

			' Check if given file is a template
			Try
				_subject = NSubject.FromFile(OpenFileDialog.FileName)
				btnIdentify.Enabled = _subjects IsNot Nothing
			Catch
			End Try

			' If file is not a template, try to load it as audio file
			If _subject Is Nothing Then
				' Create an iris object
				Dim voice As New NVoice() With {.FileName = OpenFileDialog.FileName}
				' Add the voice to a subject
				_subject = New NSubject()
				_subject.Voices.Add(voice)

				' Extract the template from the subject
				_biometricClient.BeginCreateTemplate(_subject, AddressOf OnExtractionCompleted, Nothing)
			End If
		End If
	End Sub

	Private Sub BtnIdentifyClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnIdentify.Click
		ListView.Items.Clear()
		If _subject IsNot Nothing AndAlso _subjects IsNot Nothing AndAlso _subjects.Length > 0 Then
			_biometricClient.BeginIdentify(_subject, AddressOf OnIdentifyDone, Nothing)
		End If
	End Sub

	Private Sub BtnDefaultClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDefault.Click
		cbMatchingFar.SelectedIndex = 1
		btnDefault.Enabled = False
	End Sub

	Private Sub CbMatchingFarEnter(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatchingFar.Enter
		btnDefault.Enabled = True
	End Sub

	Private Sub IdentifyVoiceLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		lblFileForIdentification.Text = String.Empty
		lblTemplatesCount.Text = "0"

		Try
			cbMatchingFar.BeginUpdate()
			cbMatchingFar.Items.Add(0.001.ToString("P1"))
			cbMatchingFar.Items.Add(0.0001.ToString("P2"))
			cbMatchingFar.Items.Add(0.00001.ToString("P3"))
		Finally
			cbMatchingFar.EndUpdate()
		End Try
		cbMatchingFar.SelectedIndex = 1
	End Sub

	Private Sub CbMatchingFarLeave(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatchingFar.Leave
		SetFar()
	End Sub

	Private Sub IdentifyVoiceVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If Visible AndAlso _biometricClient IsNot Nothing Then
			chbUniquePhrases.Checked = True
			SetDefaultFar()
		End If
	End Sub

	Private Sub ChbUniquePhrasesCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbUniquePhrases.CheckedChanged
		If _biometricClient IsNot Nothing Then
			_biometricClient.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked
		End If
	End Sub

#End Region
End Class
