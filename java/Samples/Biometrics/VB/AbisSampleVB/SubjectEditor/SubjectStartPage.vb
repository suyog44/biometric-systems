Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images
Imports Neurotec.IO

Partial Public Class SubjectStartPage
	Inherits Neurotec.Samples.PageBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		DoubleBuffered = True

		If (Not DesignMode) Then
			openFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		End If
	End Sub

#End Region

#Region "Private fields"

	Private _subject As NSubject
	Private _biometricClient As NBiometricClient
	Private _thumbnail As NImage
	Private _subjectList() As String
	Private _propertyAdapter As SchemaPropertyGridAdapter

#End Region

#Region "Public methods"

	Public Overrides Sub OnNavigatedTo(ByVal ParamArray args() As Object)
		If args Is Nothing OrElse args.Length <> 1 Then
			Throw New ArgumentException("args")
		End If
		_subject = CType(args(0), NSubject)
		_biometricClient = PageController.TabController.Client

		If _subject IsNot Nothing Then
			tbSubjectId.DataBindings.Add("Text", _subject, "Id")
		End If
		Dim isEmpty As Boolean = IsSubjectEmpty()
		If isEmpty Then
			If (Not LicensingTools.CanCreateFingerTemplate(_biometricClient.LocalOperations) AndAlso _
			 Not LicensingTools.CanCreateFaceTemplate(_biometricClient.LocalOperations) AndAlso _
			 Not LicensingTools.CanCreateIrisTemplate(_biometricClient.LocalOperations) AndAlso _
			 Not LicensingTools.CanCreatePalmTemplate(_biometricClient.LocalOperations) AndAlso _
			 Not LicensingTools.CanCreateVoiceTemplate(_biometricClient.LocalOperations)) Then
				SetStatus(Color.Red, "None of required licenses were obtained. For more information open ActivationWizard")
			Else
				SetStatus(Color.Orange, "Subject is empty. Click on wanted modality in tree view to create new template")
			End If
		Else
			SetStatus(Color.Green, "Subject is ready for action. Click on buttons above to perform action")
		End If

		EnableControls()
		GetThumbnail()
		UpdateSchemaControls()

		If _subjectList Is Nothing Then
			Try
				Dim operations As NBiometricOperations = _biometricClient.LocalOperations
				If _biometricClient.RemoteConnections.Count > 0 Then
					operations = operations Or _biometricClient.RemoteConnections(0).Operations
				End If
				If (operations And NBiometricOperations.ListIds) = NBiometricOperations.ListIds Then
					_subjectList = _biometricClient.ListIds()
					tbSubjectId.AutoCompleteCustomSource.AddRange(_subjectList)
				End If
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If

		tbQuery.AutoCompleteCustomSource.Clear()
		tbQuery.AutoCompleteCustomSource.AddRange(SettingsManager.QuerySuggestions)

		TryFillGenderField()

		MyBase.OnNavigatedTo(args)
	End Sub

	Public Overrides Sub OnNavigatingFrom()
		tbSubjectId.DataBindings.Clear()
		_biometricClient = Nothing
		_subject = Nothing
		MyBase.OnNavigatingFrom()
	End Sub

#End Region

#Region "Private methods"

	Private Sub EnableControls()
		Dim isEmpty As Boolean = IsSubjectEmpty()
		If isEmpty Then
			btnVerify.Enabled = False
			btnIdentify.Enabled = btnVerify.Enabled
			btnEnrollWithDuplicates.Enabled = btnIdentify.Enabled
			btnEnroll.Enabled = btnEnrollWithDuplicates.Enabled
			btnSaveTemplate.Enabled = btnEnroll.Enabled
			btnUpdate.Enabled = btnSaveTemplate.Enabled
			btnPrintApplicantCard.Enabled = False
			btnPrintCriminalCard.Enabled = btnPrintApplicantCard.Enabled
		Else
			Dim operations As NBiometricOperations = _biometricClient.LocalOperations
			If _biometricClient.RemoteConnections.Count > 0 Then
				operations = operations Or _biometricClient.RemoteConnections(0).Operations
			End If

			btnSaveTemplate.Enabled = True
			btnEnroll.Enabled = True
			btnIdentify.Enabled = True
			btnEnrollWithDuplicates.Enabled = (operations And NBiometricOperations.EnrollWithDuplicateCheck) = NBiometricOperations.EnrollWithDuplicateCheck
			btnUpdate.Enabled = (operations And NBiometricOperations.Update) = NBiometricOperations.Update
			btnVerify.Enabled = (operations And NBiometricOperations.Verify) = NBiometricOperations.Verify
			btnPrintApplicantCard.Enabled = _subject.Fingers.Count > 0
			btnPrintCriminalCard.Enabled = btnPrintApplicantCard.Enabled
		End If
	End Sub

	Private Sub TryFillGenderField()
		Dim current As SampleDbSchema = SettingsManager.CurrentSchema
		If (Not current.IsEmpty) AndAlso (Not String.IsNullOrEmpty(current.GenderDataName)) Then
			Dim gender As NGender = _propertyAdapter.GetValue(Of NGender)(current.GenderDataName)
			If gender = NGender.Unspecified Then
				For Each item In _subject.Faces
					Dim g As NGender = item.Objects.First().Gender
					If g = NGender.Male OrElse g = NGender.Female Then
						_propertyAdapter.SetValue(current.GenderDataName, g)
						propertyGrid.Refresh()
						Exit For
					End If
				Next item
			End If
		End If
	End Sub

	Private Sub UpdateSchemaControls()
		Dim schema As SampleDbSchema = SettingsManager.CurrentSchema
		If schema.IsEmpty Then
			gbEnrollData.Visible = False
		Else
			gbThumbnail.Visible = Not String.IsNullOrEmpty(schema.ThumbnailDataName)
			If _propertyAdapter Is Nothing Then
				Dim properties As NPropertyBag = CType(_subject.Properties.Clone(), NPropertyBag)
				properties.Remove(schema.ThumbnailDataName)
				properties.Remove(schema.EnrollDataName)
				_propertyAdapter = New SchemaPropertyGridAdapter(schema, properties) With {.IsReadOnly = False, .ShowBlobs = True}
				propertyGrid.SelectedObject = _propertyAdapter
				_subject.Properties.Clear()
			End If
		End If
	End Sub

	Private Sub SetStatus(ByVal backColor As Color, ByVal format As String, ByVal ParamArray args() As Object)
		lblHint.BackColor = backColor
		lblHint.Text = String.Format(format, args)
	End Sub

	Private Function IsSubjectEmpty() As Boolean
		Return _subject.Fingers.Count + _subject.Faces.Count + _subject.Irises.Count + _subject.Voices.Count + _subject.Palms.Count = 0
	End Function

	Private Sub SetSubjectProperties()
		Dim schema As SampleDbSchema = SettingsManager.CurrentSchema
		If (Not schema.IsEmpty) Then
			_subject.Properties.Clear()
			If _thumbnail IsNot Nothing Then
				Dim format As NImageFormat = _thumbnail.Info.Format
				If format Is Nothing OrElse Not format.CanWrite Then
					format = NImageFormat.Png
				End If
				_subject.SetProperty(schema.ThumbnailDataName, _thumbnail.Save(format))
			End If
			If (Not String.IsNullOrEmpty(schema.EnrollDataName)) Then
				Dim serialize As Func(Of NSubject, Boolean, NBuffer) = AddressOf EnrollDataSerializer.Serialize
				Dim title As String = "Serializing subject data"
				Using buffer = CType(LongActionDialog.ShowDialog(Me, title, serialize, _subject, LicensingTools.IsComponentActivated("Images.WSQ")), NBuffer)
					_subject.SetProperty(schema.EnrollDataName, buffer)
				End Using
				_subject.SetProperty(schema.EnrollDataName, EnrollDataSerializer.Serialize(_subject, LicensingTools.IsComponentActivated("Images.WSQ")))
			End If

			_propertyAdapter.ApplyTo(_subject)
		End If
	End Sub

	Private Sub GetThumbnail()
		Dim schema As SampleDbSchema = SettingsManager.CurrentSchema
		If (Not schema.IsEmpty) Then
			Dim hasThumbnail As Boolean = Not String.IsNullOrEmpty(schema.ThumbnailDataName)
			gbThumbnail.Visible = hasThumbnail
			If hasThumbnail AndAlso _thumbnail Is Nothing Then
				If _subject.Properties.ContainsKey(schema.ThumbnailDataName) Then
					_thumbnail = NImage.FromMemory(_subject.GetProperty(Of NBuffer)(schema.ThumbnailDataName))
				Else
					For Each face As NFace In _subject.Faces
						Dim attributes As NLAttributes = face.Objects.FirstOrDefault()
						_thumbnail = attributes.Thumbnail
						If _thumbnail IsNot Nothing Then
							Exit For
						End If
					Next face
				End If
				pbThumbnail.Image = If(_thumbnail IsNot Nothing, _thumbnail.ToBitmap(), Nothing)
			End If
		End If
	End Sub

#End Region

#Region "Private events"

	Private Sub BtnEnrollClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnroll.Click
		If String.IsNullOrEmpty(tbSubjectId.Text) Then
			tbSubjectId.BackColor = Color.DarkRed
			tbSubjectId.Focus()
		Else
			Try
				_subject.QueryString = Nothing
				SetSubjectProperties()
				PageController.TabController.ShowTab(GetType(DababaseOperationTab), True, True, _subject, NBiometricOperations.Enroll)
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If
	End Sub

	Private Sub BtnIdentifyClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnIdentify.Click
		Dim query As String = tbQuery.Text
		_subject.QueryString = query
		If query IsNot String.Empty AndAlso Not tbQuery.AutoCompleteCustomSource.Contains(query) Then
			tbQuery.AutoCompleteCustomSource.Add(query)
			SettingsManager.QuerySuggestions = tbQuery.AutoCompleteCustomSource.OfType(Of String).ToArray()
		End If
		PageController.TabController.ShowTab(GetType(DababaseOperationTab), True, True, _subject, NBiometricOperations.Identify)
	End Sub

	Private Sub BtnVerifyClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerify.Click
		If String.IsNullOrEmpty(tbSubjectId.Text) Then
			tbSubjectId.BackColor = Color.DarkRed
			tbSubjectId.Focus()
		Else
			_subject.QueryString = Nothing
			PageController.TabController.ShowTab(GetType(DababaseOperationTab), True, True, _subject, NBiometricOperations.Verify)
		End If
	End Sub

	Private Sub BtnUpdateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
		If String.IsNullOrEmpty(tbSubjectId.Text) Then
			tbSubjectId.BackColor = Color.DarkRed
			tbSubjectId.Focus()
		Else
			Try
				_subject.QueryString = Nothing
				SetSubjectProperties()
				PageController.TabController.ShowTab(GetType(DababaseOperationTab), True, True, _subject, NBiometricOperations.Update)
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If
	End Sub

	Private Sub BtnEnrollWithDuplicatesClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnrollWithDuplicates.Click
		If String.IsNullOrEmpty(tbSubjectId.Text) Then
			tbSubjectId.BackColor = Color.DarkRed
			tbSubjectId.Focus()
		Else
			Try
				_subject.QueryString = Nothing
				SetSubjectProperties()
				PageController.TabController.ShowTab(GetType(DababaseOperationTab), True, True, _subject, NBiometricOperations.EnrollWithDuplicateCheck)
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If
	End Sub

	Private Sub TbSubjectIdTextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tbSubjectId.TextChanged
		If tbSubjectId.BackColor = Color.DarkRed AndAlso (Not String.IsNullOrEmpty(tbSubjectId.Text)) Then
			tbSubjectId.BackColor = SystemColors.Window
		End If
	End Sub

	Private Sub BtnSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTemplate.Click
		If (Not IsSubjectEmpty()) Then
			If saveFileDialog.ShowDialog(Me) = DialogResult.OK Then
				Try
					Using nbuffer = _subject.GetTemplateBuffer()
						File.WriteAllBytes(saveFileDialog.FileName, nbuffer.ToArray())
					End Using
				Catch ex As Exception
					Utilities.ShowError(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub BtnOpenImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenImage.Click
		If openFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				_thumbnail = NImage.FromFile(openFileDialog.FileName)
				pbThumbnail.Image = _thumbnail.ToBitmap()
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If
	End Sub

	Private Sub BtnPrintCriminalCardClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintCriminalCard.Click
		Using dialog = New TenPrintCardPrintForm(My.Resources.CriminalCard) With {.Subject = _subject, .BiometricClient = _biometricClient}
			dialog.ShowDialog()
		End Using
	End Sub

	Private Sub BtnPrintApplicantCardClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintApplicantCard.Click
		Using dialog = New TenPrintCardPrintForm(My.Resources.ApplicantCard) With {.Subject = _subject, .BiometricClient = _biometricClient}
			dialog.ShowDialog()
		End Using
	End Sub

#End Region

End Class
