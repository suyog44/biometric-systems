Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports System.IO
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO

Partial Public Class MainForm
	Inherits Form
#Region "Internal vars"
	Private _currentANTemplate As ANTemplate = Nothing
	Private _currentNFRecords() As NFRecord = Nothing
	Private _currentNFTemplate As NFTemplate = Nothing
	Private _currentNTemplate As NTemplate = Nothing
	Private _currentFMRecord As FMRecord = Nothing
	Private _currentNLTemplate As NLTemplate = Nothing
	Private _currentNLRecords() As NLRecord = Nothing
#End Region	' Internal vars

#Region "Constructor"
	Public Sub New()
		InitializeComponent()
	End Sub
#End Region	' Constructor

#Region "Events"
	Protected Overrides Sub OnLoad(ByVal e As EventArgs)
		makeAllEnabledDisabled(False)

		MyBase.OnLoad(e)
	End Sub
#End Region	' Events

	Private Sub openImageButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openImageButton.Click
		Try
			Dim fileNames() As String = Nothing

			'				#Region "Open ANTemplate"
			If rbANTemplate.Checked Then
				fileNames = OpenDialogSetup("ANTemplate files (*.data)|*.data|All files|*.*", "Open ANTemplate", False)
				If fileNames IsNot Nothing Then
					If _currentANTemplate IsNot Nothing Then
						_currentANTemplate.Dispose()
					End If

					_currentANTemplate = New ANTemplate(fileNames(0), ANValidationLevel.Standard, 0)
					rtbLeft.Text = String.Format("Filename: {0}" & Constants.vbLf & " Type: ANTemplate" & Constants.vbLf, fileNames(0))
				End If
				Return
			End If
			'				#End Region

			'				#Region "Open NFRecord"
			If rbNFRecords.Checked Then
				fileNames = OpenDialogSetup("NFRecord files (*.data)|*.data|All files|*.*", "Open NFRecord(s)", True)
				Dim openedFileNames As String = String.Empty
				If fileNames IsNot Nothing Then
					If _currentNFRecords IsNot Nothing Then
						For i As Integer = 0 To _currentNFRecords.Length - 1
							_currentNFRecords(i).Dispose()
						Next i
					End If

					_currentNFRecords = New NFRecord(fileNames.Length - 1) {}
					For i As Integer = 0 To _currentNFRecords.Length - 1
						Dim arrayTmp() As Byte = File.ReadAllBytes(fileNames(i))
						_currentNFRecords(i) = New NFRecord(arrayTmp)
						openedFileNames &= String.Format("Filename: {0}" & Constants.vbLf & " Type: NFRecord" & Constants.vbCrLf, fileNames(i))
						openedFileNames &= String.Format("Cores: {0}, Double cores: {1}, Deltas: {2}, Minutia: {3}" & Constants.vbCrLf & "G: {4}" & Constants.vbCrLf & Constants.vbCrLf, _currentNFRecords(i).Cores.Count, _currentNFRecords(i).DoubleCores.Count, _currentNFRecords(i).Deltas.Count, _currentNFRecords(i).Minutiae.Count, _currentNFRecords(i).G)
					Next i
					rtbLeft.Text = openedFileNames
				End If
				Return
			End If
			'				#End Region

			'				#Region "Open NFTemplate"
			If rbNFTemplate.Checked Then
				fileNames = OpenDialogSetup("NFTemplate files (*.data)|*.data|All files|*.*", "Open NFTemplate", False)
				If fileNames IsNot Nothing Then
					If _currentNFTemplate IsNot Nothing Then
						_currentNFTemplate.Dispose()
					End If

					Dim arrayN() As Byte = File.ReadAllBytes(fileNames(0))
					_currentNFTemplate = New NFTemplate(arrayN)

					rtbLeft.Text = String.Format("Filename: {0}" & Constants.vbLf & " Type: NFTemplate" & Constants.vbCrLf, fileNames(0))

					If _currentNFTemplate.Records IsNot Nothing Then
						rtbLeft.Text = String.Format("Fingerprint record count: {1}" & Constants.vbCrLf, _currentNFTemplate.Records.Count)
					End If

					If (_currentNFTemplate.Records Is Nothing) OrElse (_currentNFTemplate.Records.Count = 0) Then
						rbNTemplateRight.Enabled = False
						rbNFRecordsRight.Enabled = False
						rbFMRecordISORight.Enabled = False
						rbFMRecordANSIRight.Enabled = False
						rbANTemplateRight.Enabled = False
					End If
				End If
				Return
			End If
			'				#End Region

			'				#Region "Open NTemplate"
			If rbNTemplate.Checked Then
				fileNames = OpenDialogSetup("NTemplate files (*.data)|*.data|All files|*.*", "Open NTemplate", False)
				If fileNames IsNot Nothing Then
					If _currentNTemplate IsNot Nothing Then
						_currentNTemplate.Dispose()
					End If

					Dim arrayN() As Byte = File.ReadAllBytes(fileNames(0))
					_currentNTemplate = New NTemplate(arrayN)

					rtbLeft.Text = String.Format("Filename: {0}" & Constants.vbLf & " Type: NTemplate" & Constants.vbCrLf, fileNames(0))

					If (_currentNTemplate.Fingers IsNot Nothing) AndAlso (_currentNTemplate.Fingers.Records.Count > 0) Then
						rtbLeft.Text += String.Format("Fingerprint record count: {0}" & Constants.vbCrLf, _currentNTemplate.Fingers.Records.Count)
					Else
						rbNFTemplateRight.Enabled = False
						rbNFRecordsRight.Enabled = False
						rbFMRecordISORight.Enabled = False
						rbFMRecordANSIRight.Enabled = False
					End If

					If (_currentNTemplate.Faces IsNot Nothing) AndAlso (_currentNTemplate.Faces.Records.Count > 0) Then
						rtbLeft.Text += String.Format("Face record count: {0}" & Constants.vbCrLf, _currentNTemplate.Faces.Records.Count)
					Else
						rbANTemplateRight.Enabled = False
						rbNLRecordRight.Enabled = False
						rbNLTemplateRight.Enabled = False
					End If
				End If
				Return
			End If
			'				#End Region

			'				#Region "Open FMRecord ANSI/ISO"
			If rbFMRecordANSI.Checked Then
				fileNames = OpenDialogSetup("FMRecord files (*.data)|*.data|All files|*.*", "Open FMRecord (ANSI)", False)
				If fileNames IsNot Nothing Then
					If _currentFMRecord IsNot Nothing Then
						_currentFMRecord.Dispose()
					End If

					Dim arrayN() As Byte = File.ReadAllBytes(fileNames(0))
					_currentFMRecord = New FMRecord(New NBuffer(arrayN), BdifStandard.Ansi)

					rtbLeft.Text = String.Format("Filename: {0}" & Constants.vbLf & " Type: FMRecord ANSI" & Constants.vbCrLf, fileNames(0))

				End If
				Return
			End If

			If rbFMRecordISO.Checked Then
				fileNames = OpenDialogSetup("FMRecord files (*.data)|*.data|All files|*.*", "Open FMRecord (ISO)", False)
				If fileNames IsNot Nothing Then
					If _currentFMRecord IsNot Nothing Then
						_currentFMRecord.Dispose()
					End If

					Dim arrayN() As Byte = File.ReadAllBytes(fileNames(0))
					_currentFMRecord = New FMRecord(New NBuffer(arrayN), BdifStandard.Iso)

					rtbLeft.Text = String.Format("Filename: {0}" & Constants.vbLf & " Type: FMRecord ISO" & Constants.vbCrLf, fileNames(0))
				End If
				Return
			End If
			'				#End Region

			'				#Region "Open NLTemplate"
			If rbNLTemplate.Checked Then
				fileNames = OpenDialogSetup("NLTemplate files (*.data)|*.data|All files|*.*", "Open NLTemplate", False)
				If fileNames IsNot Nothing Then
					If _currentNLTemplate IsNot Nothing Then
						_currentNLTemplate.Dispose()
					End If

					Dim arrayN() As Byte = File.ReadAllBytes(fileNames(0))
					_currentNLTemplate = New NLTemplate(arrayN)

					rtbLeft.Text = String.Format("Filename: {0}" & Constants.vbLf & " Type: NLTemplate" & Constants.vbCrLf, fileNames(0))

					If _currentNLTemplate.Records IsNot Nothing Then
						rtbLeft.Text = String.Format("Face record count: {1}" & Constants.vbCrLf, _currentNLTemplate.Records.Count)
					End If

					If (_currentNLTemplate.Records Is Nothing) OrElse (_currentNLTemplate.Records.Count = 0) Then
						rbNTemplateRight.Enabled = False
						rbNLRecordRight.Enabled = False
					End If
				End If
				Return
			End If
			'				#End Region

			'				#Region "Open NLRecord"
			If rbNLRecord.Checked Then
				fileNames = OpenDialogSetup("NLRecord files (*.data)|*.data|All files|*.*", "Open NLRecord(s)", True)
				Dim openedFileNames As String = String.Empty
				If fileNames IsNot Nothing Then
					If _currentNLRecords IsNot Nothing Then
						For i As Integer = 0 To _currentNLRecords.Length - 1
							_currentNLRecords(i).Dispose()
						Next i
					End If

					_currentNLRecords = New NLRecord(fileNames.Length - 1) {}
					For i As Integer = 0 To _currentNLRecords.Length - 1
						Dim arrayTmp() As Byte = File.ReadAllBytes(fileNames(i))
						_currentNLRecords(i) = New NLRecord(arrayTmp)
						openedFileNames &= String.Format("Filename: {0}" & Constants.vbLf & " Type: NLRecord" & Constants.vbCrLf, fileNames(i))
						openedFileNames &= String.Format("Quality: {0}" & Constants.vbCrLf & Constants.vbCrLf, _currentNLRecords(i).Quality)
					Next i
					rtbLeft.Text = openedFileNames
				End If
				Return
			End If
			'				#End Region

			MessageBox.Show(Me, "Before loading a template you must select the type of the template." & Constants.vbCrLf & "Please select one of the items above and try again.", "Template Convertion: Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		Catch ex As Exception
			MessageBox.Show(Me, "Error occured while openning file(s)." & Constants.vbCrLf & "Details: " & ex.Message, "Template Convertion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#Region "Misc functions"
	Private Function OpenDialogSetup(ByVal extension As String, ByVal title As String, ByVal multiselect As Boolean) As String()
		openFileDialog.Title = title
		openFileDialog.Multiselect = multiselect
		openFileDialog.CheckFileExists = True
		openFileDialog.CheckPathExists = True
		openFileDialog.Filter = extension

		If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Return openFileDialog.FileNames
		End If

		Return Nothing
	End Function

	Private Function SaveDialogSetup(ByVal extension As String, ByVal title As String) As String
		saveFileDialog.Title = title
		saveFileDialog.Filter = extension

		If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Return saveFileDialog.FileName
		End If

		Return Nothing
	End Function

	Private Sub makeAllEnabledDisabled(ByVal enabled As Boolean)
		rbNTemplateRight.Enabled = enabled
		rbNLTemplateRight.Enabled = enabled
		rbNLRecordRight.Enabled = enabled
		rbNFTemplateRight.Enabled = enabled
		rbNFRecordsRight.Enabled = enabled
		rbFMRecordISORight.Enabled = enabled
		rbFMRecordANSIRight.Enabled = enabled
		rbANTemplateRight.Enabled = enabled
	End Sub
#End Region	' Misc functions

	Private Sub Global_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbNLRecord.CheckedChanged, rbNLTemplate.CheckedChanged, rbNTemplate.CheckedChanged, rbNFTemplate.CheckedChanged, rbNFRecords.CheckedChanged, rbFMRecordISO.CheckedChanged, rbFMRecordANSI.CheckedChanged, rbANTemplate.CheckedChanged
		makeAllEnabledDisabled(False)
		rtbLeft.Text = String.Empty

		_currentANTemplate = Nothing
		_currentNFRecords = Nothing
		_currentNFTemplate = Nothing
		_currentNTemplate = Nothing
		_currentFMRecord = Nothing
		_currentNLRecords = Nothing
		_currentNLTemplate = Nothing

		If rbANTemplate.Checked Then
			rbNTemplateRight.Enabled = True
			rbNFTemplateRight.Enabled = True
			rbNFRecordsRight.Enabled = True
			rbFMRecordISORight.Enabled = True
			rbFMRecordANSIRight.Enabled = True
		End If

		If rbFMRecordANSI.Checked Then
			rbNTemplateRight.Enabled = True
			rbNFTemplateRight.Enabled = True
			rbNFRecordsRight.Enabled = True
			rbFMRecordISORight.Enabled = True
			rbANTemplateRight.Enabled = True
		End If

		If rbFMRecordISO.Checked Then
			rbNTemplateRight.Enabled = True
			rbNFTemplateRight.Enabled = True
			rbNFRecordsRight.Enabled = True
			rbFMRecordANSIRight.Enabled = True
			rbANTemplateRight.Enabled = True
		End If

		If rbNFRecords.Checked Then
			rbFMRecordANSIRight.Enabled = True
			rbFMRecordISORight.Enabled = True
			rbANTemplateRight.Enabled = True
			rbNFTemplateRight.Enabled = True
			rbNTemplateRight.Enabled = True
		End If

		If rbNFTemplate.Checked Then
			rbNFRecordsRight.Enabled = True
			rbFMRecordANSIRight.Enabled = True
			rbFMRecordISORight.Enabled = True
			rbANTemplateRight.Enabled = True
			rbNTemplateRight.Enabled = True
		End If

		If rbNLRecord.Checked Then
			rbNLTemplateRight.Enabled = True
			rbNTemplateRight.Enabled = True
		End If

		If rbNLTemplate.Checked Then
			rbNLRecordRight.Enabled = True
			rbNTemplateRight.Enabled = True
		End If

		If rbNTemplate.Checked Then
			rbFMRecordANSIRight.Enabled = True
			rbFMRecordISORight.Enabled = True
			rbANTemplateRight.Enabled = True

			rbNFRecordsRight.Enabled = True
			rbNFTemplateRight.Enabled = True
			rbNLRecordRight.Enabled = True
			rbNLTemplateRight.Enabled = True
		End If

	End Sub

	Private Function checkTemplateNotNull(ByVal i As Integer) As Boolean
		Return True
	End Function

	Private Sub SaveNLTemplateAsNLRecord(ByVal tmpl As NLTemplate)
		Dim fileName As String = SaveDialogSetup("NLRecord files (*.data)|*.data|All files|*.*", "Save NLRecord(s)")

		If fileName IsNot Nothing Then
			Dim extension As String = Path.GetExtension(fileName)
			Dim name As String = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName)

			For i As Integer = 0 To tmpl.Records.Count - 1
				Dim tmp As String = String.Format("{0}{1}{2}", name, i, extension)
				File.WriteAllBytes(tmp, tmpl.Records(i).Save().ToArray())
				rtbRight.AppendText("Saved record (NLRecord): " & tmp & Constants.vbCrLf)
			Next i
		End If
	End Sub

	Private Sub SaveNFTemplateAsNFRecord(ByVal tmpl As NFTemplate)
		Dim fileName As String = SaveDialogSetup("NFRecord files (*.data)|*.data|All files|*.*", "Save NFRecord(s)")

		If fileName IsNot Nothing Then
			Dim extension As String = Path.GetExtension(fileName)
			Dim name As String = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName)
			For i As Integer = 0 To tmpl.Records.Count - 1
				Dim tmp As String = String.Format("{0}{1}{2}", name, i, extension)
				File.WriteAllBytes(tmp, tmpl.Records(i).Save().ToArray())
				rtbRight.AppendText("Saved record (NFRecord): " & tmp & Constants.vbCrLf)
			Next i
		End If
	End Sub

	Private Sub printInvalidStatement()
		MessageBox.Show(Me, "No templates opened for conversion, or data is invalid.", "Template Convertion: Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Private Sub btnConvertAndSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConvertAndSave.Click
		Try
			'				#Region "Conversion To NFRecord"
			If rbNFRecordsRight.Checked Then
				If rbNTemplate.Checked Then
					If (_currentNTemplate Is Nothing) OrElse (_currentNTemplate.Fingers Is Nothing) OrElse (_currentNTemplate.Fingers.Records Is Nothing) OrElse (_currentNTemplate.Fingers.Records.Count = 0) Then
						printInvalidStatement()
					Else
						SaveNFTemplateAsNFRecord(_currentNTemplate.Fingers)
					End If
					Return
				End If

				If rbNFTemplate.Checked Then
					If (_currentNFTemplate Is Nothing) OrElse (_currentNFTemplate.Records Is Nothing) OrElse (_currentNFTemplate.Records.Count = 0) Then
						printInvalidStatement()
					Else
						SaveNFTemplateAsNFRecord(_currentNFTemplate)
					End If
					Return
				End If

				If rbANTemplate.Checked Then
					If (_currentANTemplate Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As NFTemplate = RecordTransformations.ANTemplateToNFTemplate(_currentANTemplate)
						SaveNFTemplateAsNFRecord(tmpl)
						tmpl.Dispose()
					End If
					Return
				End If

				If (rbFMRecordANSI.Checked) OrElse (rbFMRecordISO.Checked) Then
					If (_currentFMRecord Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As NFTemplate = RecordTransformations.FMRecordToNFTemplate(_currentFMRecord)
						SaveNFTemplateAsNFRecord(tmpl)
						tmpl.Dispose()
					End If
					Return
				End If
			End If
			'				#End Region

			'				#Region "Convertion To NFTemplate"
			If rbNFTemplateRight.Checked Then
				If rbNTemplate.Checked Then
					If (_currentNTemplate Is Nothing) OrElse (_currentNTemplate.Fingers Is Nothing) OrElse (_currentNTemplate.Fingers.Records Is Nothing) OrElse (_currentNTemplate.Fingers.Records.Count = 0) Then
						printInvalidStatement()
					Else
						Dim fileName As String = SaveDialogSetup("NFTemplate files (*.data)|*.data|All files|*.*", "Save NFTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, _currentNTemplate.Fingers.Save().ToArray())
							rtbRight.AppendText("Saved record (NFTemplate): " & fileName & Constants.vbCrLf)
						End If
					End If
					Return
				End If

				If rbNFRecords.Checked Then
					If (_currentNFRecords Is Nothing) OrElse (_currentNFRecords.Length = 0) Then
						printInvalidStatement()
					Else
						Dim tmpl As New NFTemplate()
						For i As Integer = 0 To _currentNFRecords.Length - 1
							tmpl.Records.Add(CType(_currentNFRecords(i).Clone(), NFRecord))
						Next i

						Dim fileName As String = SaveDialogSetup("NFTemplate files (*.data)|*.data|All files|*.*", "Save NFTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl.Save().ToArray())
							rtbRight.AppendText("Saved record (NFTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If rbANTemplate.Checked Then
					If (_currentANTemplate Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As NFTemplate = RecordTransformations.ANTemplateToNFTemplate(_currentANTemplate)
						Dim fileName As String = SaveDialogSetup("NFTemplate files (*.data)|*.data|All files|*.*", "Save NFTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl.Save().ToArray())
							rtbRight.AppendText("Saved record (NFTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If (rbFMRecordANSI.Checked) OrElse (rbFMRecordISO.Checked) Then
					If (_currentFMRecord Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As NFTemplate = RecordTransformations.FMRecordToNFTemplate(_currentFMRecord)
						Dim fileName As String = SaveDialogSetup("NFTemplate files (*.data)|*.data|All files|*.*", "Save NFTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl.Save().ToArray())
							rtbRight.AppendText("Saved record (NFTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If
			End If
			'				#End Region

			'				#Region "Conversion to NLRecord"
			If (rbNLRecordRight.Checked) Then
				If rbNTemplate.Checked Then
					If (_currentNTemplate Is Nothing) OrElse (_currentNTemplate.Faces Is Nothing) OrElse (_currentNTemplate.Faces.Records Is Nothing) OrElse (_currentNTemplate.Faces.Records.Count = 0) Then
						printInvalidStatement()
					Else
						SaveNLTemplateAsNLRecord(_currentNTemplate.Faces)
					End If
					Return
				End If

				If rbNLTemplate.Checked Then
					If (_currentNLTemplate Is Nothing) OrElse (_currentNLTemplate.Records Is Nothing) OrElse (_currentNLTemplate.Records.Count = 0) Then
						printInvalidStatement()
					Else
						SaveNLTemplateAsNLRecord(_currentNLTemplate)
					End If
					Return
				End If
			End If
			'				#End Region

			'				#Region "Conversion to NLTemplate"
			If (rbNLTemplateRight.Checked) Then
				If rbNLRecord.Checked Then
					If (_currentNLRecords Is Nothing) OrElse (_currentNLRecords.Length = 0) Then
						printInvalidStatement()
					Else
						Dim tmpl As New NLTemplate()
						For i As Integer = 0 To _currentNLRecords.Length - 1
							tmpl.Records.Add(CType(_currentNLRecords(i).Clone(), NLRecord))
						Next i

						Dim fileName As String = SaveDialogSetup("NLTemplate files (*.data)|*.data|All files|*.*", "Save NLTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl.Save().ToArray())
							rtbRight.AppendText("Saved record (NLTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If rbNTemplate.Checked Then
					If (_currentNTemplate Is Nothing) OrElse (_currentNTemplate.Faces Is Nothing) Then
						printInvalidStatement()
					Else
						Dim fileName As String = SaveDialogSetup("NLTemplate files (*.data)|*.data|All files|*.*", "Save NLTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, _currentNTemplate.Faces.Save().ToArray())
							rtbRight.AppendText("Saved record (NLTemplate): " & fileName & Constants.vbCrLf)
						End If
					End If
					Return
				End If
			End If
			'				#End Region

			'				#Region "Conversion To NTemplate"
			If rbNTemplateRight.Checked Then
				If rbANTemplate.Checked Then
					If (_currentANTemplate Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As NTemplate = RecordTransformations.ANTemplateToNTemplate(_currentANTemplate)
						Dim fileName As String = SaveDialogSetup("NTemplate files (*.data)|*.data|All files|*.*", "Save NTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl.Save().ToArray())
							rtbRight.AppendText("Saved record (NTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If (rbFMRecordANSI.Checked) OrElse (rbFMRecordISO.Checked) Then
					If (_currentFMRecord Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As NTemplate = RecordTransformations.FMRecordToNTemplate(_currentFMRecord)
						Dim fileName As String = SaveDialogSetup("NTemplate files (*.data)|*.data|All files|*.*", "Save NTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl.Save().ToArray())
							rtbRight.AppendText("Saved record (NTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If rbNFTemplate.Checked Then
					If (_currentNFTemplate Is Nothing) OrElse (_currentNFTemplate.Records Is Nothing) OrElse (_currentNFTemplate.Records.Count = 0) Then
						printInvalidStatement()
					Else
						Dim tmpl As New NTemplate()
						tmpl.Fingers = CType(_currentNFTemplate.Clone(), NFTemplate)
						Dim fileName As String = SaveDialogSetup("NTemplate files (*.data)|*.data|All files|*.*", "Save NTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl.Save().ToArray())
							rtbRight.AppendText("Saved record (NTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If rbNFRecords.Checked Then
					If (_currentNFRecords Is Nothing) OrElse (_currentNFRecords.Length = 0) Then
						printInvalidStatement()
					Else
						Dim tmpl As New NFTemplate()
						For i As Integer = 0 To _currentNFRecords.Length - 1
							tmpl.Records.Add(CType(_currentNFRecords(i).Clone(), NFRecord))
						Next i
						Dim tmpl2 As New NTemplate()
						tmpl2.Fingers = CType(tmpl.Clone(), NFTemplate)

						Dim fileName As String = SaveDialogSetup("NTemplate files (*.data)|*.data|All files|*.*", "Save NTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl2.Save().ToArray())
							rtbRight.AppendText("Saved record (NTemplate): " & fileName & Constants.vbCrLf)
							tmpl2.Dispose()
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If rbNLTemplate.Checked Then
					If (_currentNLTemplate Is Nothing) OrElse (_currentNLTemplate.Records Is Nothing) OrElse (_currentNLTemplate.Records.Count = 0) Then
						printInvalidStatement()
					Else
						Dim tmpl As New NTemplate()
						tmpl.Faces = CType(_currentNLTemplate.Clone(), NLTemplate)
						Dim fileName As String = SaveDialogSetup("NTemplate files (*.data)|*.data|All files|*.*", "Save NTemplate")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, tmpl.Save().ToArray())
							rtbRight.AppendText("Saved record (NTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If
			End If
			'				#End Region

			'				#Region "Conversion To ANTemplate"
			If rbANTemplateRight.Checked Then
				If rbNTemplate.Checked Then
					If (_currentNTemplate Is Nothing) OrElse (_currentNTemplate.Faces Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As ANTemplate = RecordTransformations.NTemplateToANTemplate(_currentNTemplate)
						Dim fileName As String = SaveDialogSetup("ANTemplate files (*.data)|*.data|All files|*.*", "Save ANTemplate")
						If fileName IsNot Nothing Then
							tmpl.Save(fileName)
							rtbRight.AppendText("Saved record (ANTemplate): " & fileName & Constants.vbCrLf)
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If rbNFTemplate.Checked Then
					If (_currentNFTemplate Is Nothing) OrElse (_currentNFTemplate.Records Is Nothing) OrElse (_currentNFTemplate.Records.Count = 0) Then
						printInvalidStatement()
					Else
						Dim tmpl As New NTemplate()
						tmpl.Fingers = CType(_currentNFTemplate, NFTemplate)
						Dim tmpl2 As ANTemplate = RecordTransformations.NTemplateToANTemplate(tmpl)
						Dim fileName As String = SaveDialogSetup("ANTemplate files (*.data)|*.data|All files|*.*", "Save ANTemplate")
						If fileName IsNot Nothing Then
							tmpl2.Save(fileName)
							rtbRight.AppendText("Saved record (ANTemplate): " & fileName & Constants.vbCrLf)
							tmpl2.Dispose()
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If rbNFRecords.Checked Then
					If (_currentNFRecords Is Nothing) OrElse (_currentNFRecords.Length = 0) Then
						printInvalidStatement()
					Else
						Dim tmpl As New NFTemplate()
						For i As Integer = 0 To _currentNFRecords.Length - 1
							tmpl.Records.Add(CType(_currentNFRecords(i).Clone(), NFRecord))
						Next i
						Dim tmpl2 As New NTemplate()
						tmpl2.Fingers = CType(tmpl.Clone(), NFTemplate)

						Dim tmpl3 As ANTemplate = RecordTransformations.NTemplateToANTemplate(tmpl2)
						Dim fileName As String = SaveDialogSetup("ANTemplate files (*.data)|*.data|All files|*.*", "Save ANTemplate")
						If fileName IsNot Nothing Then
							tmpl3.Save(fileName)
							rtbRight.AppendText("Saved record (ANTemplate): " & fileName & Constants.vbCrLf)
							tmpl3.Dispose()
							tmpl2.Dispose()
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If (rbFMRecordANSI.Checked) OrElse (rbFMRecordISO.Checked) Then
					If (_currentFMRecord Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As NTemplate = RecordTransformations.FMRecordToNTemplate(_currentFMRecord)
						Dim tmpl2 As ANTemplate = RecordTransformations.NTemplateToANTemplate(tmpl)
						Dim fileName As String = SaveDialogSetup("ANTemplate files (*.data)|*.data|All files|*.*", "Save ANTemplate")
						If fileName IsNot Nothing Then
							tmpl2.Save(fileName)
							rtbRight.AppendText("Saved record (ANTemplate): " & fileName & Constants.vbCrLf)
							tmpl2.Dispose()
							tmpl.Dispose()
						End If
					End If
					Return
				End If
			End If
			'				#End Region

			'				#Region "Conversion To FMRecord ANSI/ISO"
			If (rbFMRecordISORight.Checked) OrElse (rbFMRecordANSIRight.Checked) Then
				If rbNFTemplate.Checked Then
					If (_currentNFTemplate Is Nothing) OrElse (_currentNFTemplate.Records Is Nothing) OrElse (_currentNFTemplate.Records.Count = 0) Then
						printInvalidStatement()
					Else
						Dim record As FMRecord
						If (rbFMRecordISORight.Checked) Then
							record = RecordTransformations.NFTemplateToFMRecord(_currentNFTemplate, BdifStandard.Iso)
						Else
							record = RecordTransformations.NFTemplateToFMRecord(_currentNFTemplate, BdifStandard.Ansi)
						End If
						Dim fileName As String = SaveDialogSetup("FMRecord files (*.data)|*.data|All files|*.*", "Save FMRecord")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, record.Save().ToArray())
							rtbRight.AppendText("Saved record (FMRecord): " & fileName & Constants.vbCrLf)
							record.Dispose()
						End If
					End If
					Return
				End If

				If rbNTemplate.Checked Then
					If (_currentNTemplate Is Nothing) OrElse (_currentNTemplate.Fingers Is Nothing) Then
						printInvalidStatement()
					Else
						Dim record As FMRecord
						If (rbFMRecordISORight.Checked) Then
							record = RecordTransformations.NFTemplateToFMRecord(_currentNTemplate.Fingers, BdifStandard.Iso)
						Else
							record = RecordTransformations.NFTemplateToFMRecord(_currentNTemplate.Fingers, BdifStandard.Ansi)
						End If
						Dim fileName As String = SaveDialogSetup("FMRecord files (*.data)|*.data|All files|*.*", "Save FMRecord")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, record.Save().ToArray())
							rtbRight.AppendText("Saved record (FMRecord): " & fileName & Constants.vbCrLf)
							record.Dispose()
						End If
					End If
					Return
				End If

				If rbNFRecords.Checked Then
					If (_currentNFRecords Is Nothing) OrElse (_currentNFRecords.Length = 0) Then
						printInvalidStatement()
					Else
						Dim tmpl As New NFTemplate()
						For i As Integer = 0 To _currentNFRecords.Length - 1
							tmpl.Records.Add(CType(_currentNFRecords(i).Clone(), NFRecord))
						Next i

						Dim record As FMRecord
						If (rbFMRecordISORight.Checked) Then
							record = RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Iso)
						Else
							record = RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Ansi)
						End If
						Dim fileName As String = SaveDialogSetup("FMRecord files (*.data)|*.data|All files|*.*", "Save FMRecord")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, record.Save().ToArray())
							rtbRight.AppendText("Saved record (FMRecord): " & fileName & Constants.vbCrLf)
							record.Dispose()
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If rbANTemplate.Checked Then
					If (_currentANTemplate Is Nothing) Then
						printInvalidStatement()
					Else
						Dim tmpl As NFTemplate = RecordTransformations.ANTemplateToNFTemplate(_currentANTemplate)
						Dim record As FMRecord
						If (rbFMRecordISORight.Checked) Then
							record = RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Iso)
						Else
							record = RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Ansi)
						End If
						Dim fileName As String = SaveDialogSetup("FMRecord files (*.data)|*.data|All files|*.*", "Save FMRecord")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, record.Save().ToArray())
							rtbRight.AppendText("Saved record (FMRecord): " & fileName & Constants.vbCrLf)
							record.Dispose()
							tmpl.Dispose()
						End If
					End If
					Return
				End If

				If (rbFMRecordISO.Checked) OrElse (rbFMRecordANSI.Checked) Then
					If _currentFMRecord Is Nothing Then
						printInvalidStatement()
					Else
						Dim tmpl As NFTemplate = RecordTransformations.FMRecordToNFTemplate(_currentFMRecord)
						Dim record As FMRecord
						If (rbFMRecordISORight.Checked) Then
							record = RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Iso)
						Else
							record = RecordTransformations.NFTemplateToFMRecord(tmpl, BdifStandard.Ansi)
						End If
						Dim fileName As String = SaveDialogSetup("FMRecord files (*.data)|*.data|All files|*.*", "Save FMRecord")
						If fileName IsNot Nothing Then
							File.WriteAllBytes(fileName, record.Save().ToArray())
							rtbRight.AppendText("Saved record (FMRecord): " & fileName & Constants.vbCrLf)
							record.Dispose()
							tmpl.Dispose()
						End If
					End If
					Return
				End If

			End If
			'				#End Region

			If _currentANTemplate Is Nothing AndAlso _currentNFRecords Is Nothing AndAlso _currentNFTemplate Is Nothing AndAlso _currentNTemplate Is Nothing AndAlso _currentFMRecord Is Nothing AndAlso _currentNLTemplate Is Nothing AndAlso _currentNLRecords Is Nothing Then
				MessageBox.Show(Me, "Before converting a template you must load a template." & Constants.vbCrLf & "Please select one of the items on the left, then press 'Open Template' button and try again.", "Template Convertion: Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Else
				MessageBox.Show(Me, "Before converting a template you must select a type to convert to." & Constants.vbCrLf & "Please select one of the items above and try again.", "Template Convertion: Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			End If

		Catch ex As Exception
			MessageBox.Show(Me, "Error occured while converting or saving files." & Constants.vbCrLf & "Details: " & ex.Message, "Template Convertion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub
End Class
