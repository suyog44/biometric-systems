Imports System
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class IdentifyIris
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

	Private Sub OnEnrollCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnEnrollCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			_enrollStatus = task.Status
			If _enrollStatus = NBiometricStatus.Ok Then
				btnIdentify.Enabled = IsSubjectValid(_subject)
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
					btnIdentify.Enabled = _subjects IsNot Nothing
				Else
					_subject = Nothing
					btnIdentify.Enabled = False
				End If
			End Try
		End If
	End Sub

	Private Sub SetFar()
		Try
			_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(cbMatchingFar.Text)
			cbMatchingFar.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold)
		Catch
			cbMatchingFar.Select()
			MessageBox.Show("FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

	Private Sub IdentifyIrisLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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

	Private Sub BtnOpenTemplatesClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenTemplates.Click
		btnIdentify.Enabled = False
		_enrollStatus = NBiometricStatus.None
		_subjects = Nothing

		lblTemplatesCount.Text = "0"
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
				lblTemplatesCount.Text = templatesCount.ToString(CultureInfo.InvariantCulture)

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

	Private Sub BtnOpenImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenImage.Click
		btnIdentify.Enabled = False

		OpenFileDialog.FileName = Nothing
		OpenFileDialog.Filter = String.Empty
		OpenFileDialog.Title = "Open Image File Or Template"

		irisView.Iris = Nothing
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

			' If file is not a template, try to load it as image
			If _subject Is Nothing Then
				' Create an iris object
				Dim iris = New NIris With {.FileName = OpenFileDialog.FileName}
				irisView.Iris = iris
				' Add the iris to a subject
				_subject = New NSubject()
				_subject.Irises.Add(iris)

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

	Private Sub CbMatchingFarEnter(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatchingFar.Enter
		btnDefault.Enabled = True
	End Sub

	Private Sub BtnDefaultClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDefault.Click
		cbMatchingFar.SelectedIndex = 1
		btnDefault.Enabled = False
	End Sub

	Private Sub CbMatchingFarLeave(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatchingFar.Leave
		SetFar()
	End Sub

	Private Sub IdentifyIrisVisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Visible AndAlso _biometricClient IsNot Nothing Then
			SetDefaultFar()
		End If
	End Sub

#End Region
End Class
