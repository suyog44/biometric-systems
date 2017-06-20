Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Gui
Imports Neurotec.Images
Imports Neurotec.Samples.RecordCreateForms

Partial Public Class MainForm
	Inherits Form
#Region "Private constants"

	Private Const ApplicationName As String = "ANSI/NIST File Editor"
	Private Const TemplateFilterName As String = "ANSI/NIST Files"
	Private Const TemplateFilter As String = "*.an;*.an2;*.eft;*.lff;*.lffs;*.int;*.nist;*.fiif"
	Private Const TemplateFilterString As String = TemplateFilterName & " (" & TemplateFilter & ")|" & TemplateFilter
	Private Const TemplateDefaultExt As String = "an2"
	Private Const AllFilesFilterString As String = "All Files (*.*)|*.*"
	Private Const TemplateOpenFileFilterString As String = TemplateFilterString & "|" & AllFilesFilterString
	Private Const TemplateSaveFileFilterString As String = TemplateFilterString

#End Region

#Region "Private fields"

	Private _template As ANTemplate = Nothing
	Private _fileName As String = Nothing
	Private _isModified As Boolean = False
	Private _templateIndex As Integer = 0
	Private _name As String = Nothing

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		openFileDialog.Filter = TemplateOpenFileFilterString
		saveFileDialog.Filter = TemplateSaveFileFilterString
		saveFileDialog.DefaultExt = TemplateDefaultExt
		folderBrowserDialog.Description = String.Format("Files satisfying ""{0}"" filter will be validated in selected folder and its subfolders", TemplateFilter)

		imageSaveFileDialog.Filter = NImages.GetSaveFileFilterString()

		aboutToolStripMenuItem.Text = "&"c + AboutBox.Name

		OnRecordsChanged()
		UpdateTitle()
	End Sub

#End Region

#Region "Private form events"

	Private Sub EditableRecordChanged(ByVal sender As Object, ByVal e As EventArgs)
		OnSelectedRecordModified()
	End Sub

	Private Sub RemoveRecordsToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles removeRecordsToolStripMenuItem.Click, removeRecordToolStripButton.Click
		Dim selCount As Integer = recordListView.SelectedIndices.Count
		If selCount > 0 Then
			If MessageBox.Show("Are you sure you want to remove selected records?", "Remove records?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
				recordListView.BeginUpdate()
				Dim selIndices(selCount - 1) As Integer
				recordListView.SelectedIndices.CopyTo(selIndices, 0)
				Array.Sort(Of Integer)(selIndices)
				For i As Integer = selCount - 1 To 0 Step -1
					Dim index As Integer = selIndices(i)
					_template.Records.RemoveAt(index)
					recordListView.Items.RemoveAt(index)
				Next i
				recordListView.Items(CInt(IIf(selIndices(0) = _template.Records.Count, _template.Records.Count - 1, selIndices(0)))).Selected = True
				recordListView.EndUpdate()
				OnTemplateModified()
			End If
		End If
	End Sub

	Private Sub ClearRecordsToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles clearRecordsToolStripMenuItem.Click
		recordListView.BeginUpdate()
		_template.Records.Clear()
		For i As Integer = recordListView.Items.Count - 1 To 1 Step -1
			recordListView.Items.RemoveAt(i)
		Next i
		If fieldListView.SelectedIndices.Count <> 0 Then ' The first record is selected
			OnSelectedRecordChanged()
		Else
			recordListView.Items(0).Selected = True
		End If
		recordListView.EndUpdate()
		OnTemplateModified()
	End Sub

	Private Sub AddFieldToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addFieldToolStripMenuItem.Click, addFieldToolStripButton.Click
		Dim version As NVersion = _template.Version
		Dim selectedRecord As ANRecord = GetSelectedRecord()
		Dim selectedRecordType As ANRecordType = selectedRecord.RecordType
		Using form As New FieldNumberForm()
			form.Text = "Add field"
			form.Version = version
			form.RecordType = selectedRecordType
			form.ValidationLevel = GetRecordValidationLevel(selectedRecord.IsValidated)
			If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
				tabControl1.SelectedIndex = 1
				Dim fieldNumber As Integer = form.FieldNumber
				If selectedRecord.Fields.Contains(fieldNumber) Then
					MessageBox.Show("The record already contains a field with the same number", ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Else
					Dim fieldIndex As Integer
					Dim field As ANField = selectedRecord.Fields.Add(fieldNumber, String.Empty, fieldIndex)
					Dim value As New StringBuilder()
					Dim item As ListViewItem = CreateFieldListViewItem(selectedRecord, field, value)
					fieldListView.Items.Insert(fieldIndex, item)
					fieldListView.SelectedIndices.Clear()
					item.Selected = True
					OnSelectedRecordModified()
					EditField()
				End If
			End If
		End Using
	End Sub

	Private Sub EditFieldToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles editFieldToolStripMenuItem.Click, editFieldToolStripButton.Click
		EditField()
	End Sub

	Private Sub RemoveFieldToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles removeFieldToolStripMenuItem.Click, removeFieldToolStripButton.Click
		fieldListView.BeginUpdate()
		Try
			Dim selectedRecord As ANRecord = GetSelectedRecord()
			Dim selCount As Integer = fieldListView.SelectedIndices.Count
			Dim selIndices(selCount - 1) As Integer
			fieldListView.SelectedIndices.CopyTo(selIndices, 0)
			Array.Sort(Of Integer)(selIndices)
			For i As Integer = selCount - 1 To 0 Step -1
				Dim index As Integer = selIndices(i)
				selectedRecord.Fields.RemoveAt(index)
				fieldListView.Items.RemoveAt(index)
			Next i
			fieldListView.Items(CInt(IIf(selIndices(0) = selectedRecord.Fields.Count, selectedRecord.Fields.Count - 1, selIndices(0)))).Selected = True
		Finally
			fieldListView.EndUpdate()
		End Try
		OnSelectedRecordModified()
	End Sub

	Private Sub AboutToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
		AboutBox.Show()
	End Sub

	Private Sub SaveRecordDataToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveRecordDataToolStripMenuItem.Click
		Dim selectedRecord As ANRecord = GetSelectedRecord()
		Dim ext As String = Nothing
		If selectedRecord.IsValidated Then
			If selectedRecord.RecordType.Number = 8 Then
				Dim type8Record As ANType8Record = CType(selectedRecord, ANType8Record)
				ext = CStr(IIf(type8Record.SignatureRepresentationType = ANSignatureRepresentationType.ScannedUncompressed, "raw", Nothing))
			ElseIf selectedRecord.RecordType.Number = 5 OrElse selectedRecord.RecordType.Number = 6 Then
				Dim ca As ANBinaryImageCompressionAlgorithm
				If selectedRecord.RecordType.Number = 5 Then
					ca = (CType(selectedRecord, ANType5Record)).CompressionAlgorithm
				Else
					ca = (CType(selectedRecord, ANType6Record)).CompressionAlgorithm
				End If
				ext = CStr(IIf(ca = ANBinaryImageCompressionAlgorithm.None, "raw", Nothing))
			ElseIf selectedRecord.RecordType.Number = 3 OrElse selectedRecord.RecordType.Number = 4 OrElse TypeOf selectedRecord Is ANImageAsciiBinaryRecord Then
				Dim ca As ANImageCompressionAlgorithm
				If selectedRecord.RecordType.Number = 3 Then
					ca = (CType(selectedRecord, ANType3Record)).CompressionAlgorithm
				ElseIf selectedRecord.RecordType.Number = 4 Then
					ca = (CType(selectedRecord, ANType4Record)).CompressionAlgorithm
				Else
					ca = (CType(selectedRecord, ANImageAsciiBinaryRecord)).CompressionAlgorithm
				End If
				If ca = ANImageCompressionAlgorithm.None Then
					ext = "raw"
				ElseIf ca = ANImageCompressionAlgorithm.Wsq20 Then
					ext = "wsq"
				ElseIf ca = ANImageCompressionAlgorithm.JpegB OrElse ca = ANImageCompressionAlgorithm.JpegL Then
					ext = "jpg"
				ElseIf ca = ANImageCompressionAlgorithm.JP2 OrElse ca = ANImageCompressionAlgorithm.JP2L Then
					ext = "jp2"
				ElseIf ca = ANImageCompressionAlgorithm.Png Then
					ext = "png"
				Else
					ext = Nothing
				End If
			End If
			If ext Is Nothing Then
				ext = "dat"
			End If
			recordDataSaveFileDialog.FileName = "data." & ext
			If recordDataSaveFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
				Try
					Using stream As Stream = File.Create(recordDataSaveFileDialog.FileName)
						selectedRecord.Data.WriteTo(stream)
					End Using
				Catch ex As Exception
					MessageBox.Show(Me, ex.ToString(), "Can not save record data", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End If
		End If
	End Sub

	Private Sub SaveImageToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveImageToolStripMenuItem.Click
		Dim selectedRecord As ANRecord = GetSelectedRecord()
		Dim imageFormat As NImageFormat = Nothing
		If selectedRecord.RecordType.Number = 8 Then
			Dim type8Record As ANType8Record = CType(selectedRecord, ANType8Record)
			imageFormat = CType(IIf(type8Record.SignatureRepresentationType = ANSignatureRepresentationType.ScannedUncompressed, NImageFormat.Tiff, Nothing), NImageFormat)
		ElseIf selectedRecord.RecordType.Number = 5 OrElse selectedRecord.RecordType.Number = 6 Then
			Dim ca As ANBinaryImageCompressionAlgorithm
			If selectedRecord.RecordType.Number = 5 Then
				ca = (CType(selectedRecord, ANType5Record)).CompressionAlgorithm
			Else
				ca = (CType(selectedRecord, ANType6Record)).CompressionAlgorithm
			End If
			imageFormat = CType(IIf(ca = ANBinaryImageCompressionAlgorithm.None, NImageFormat.Tiff, Nothing), NImageFormat)
		ElseIf selectedRecord.RecordType.Number = 3 OrElse selectedRecord.RecordType.Number = 4 OrElse TypeOf selectedRecord Is ANImageAsciiBinaryRecord Then
			Dim ca As ANImageCompressionAlgorithm
			If selectedRecord.RecordType.Number = 3 Then
				ca = (CType(selectedRecord, ANType3Record)).CompressionAlgorithm
			ElseIf selectedRecord.RecordType.Number = 4 Then
				ca = (CType(selectedRecord, ANType4Record)).CompressionAlgorithm
			Else
				ca = (CType(selectedRecord, ANImageAsciiBinaryRecord)).CompressionAlgorithm
			End If
			If ca = ANImageCompressionAlgorithm.None Then
				imageFormat = NImageFormat.Tiff
			ElseIf ca = ANImageCompressionAlgorithm.Wsq20 Then
				imageFormat = NImageFormat.Wsq
			ElseIf ca = ANImageCompressionAlgorithm.JpegB OrElse ca = ANImageCompressionAlgorithm.JpegL Then
				imageFormat = NImageFormat.Jpeg
			ElseIf ca = ANImageCompressionAlgorithm.JP2 OrElse ca = ANImageCompressionAlgorithm.JP2L Then
				imageFormat = NImageFormat.Jpeg2K
			ElseIf ca = ANImageCompressionAlgorithm.Png Then
				imageFormat = NImageFormat.Png
			Else
				imageFormat = Nothing
			End If
		End If
		If imageFormat Is Nothing Then
			imageFormat = NImageFormat.Tiff
		End If
		imageSaveFileDialog.FileName = Nothing
		imageSaveFileDialog.FilterIndex = NImageFormat.Formats.IndexOf(imageFormat) + 1
		If imageSaveFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
			Dim imageBinaryRecord As ANImageBinaryRecord = TryCast(selectedRecord, ANImageBinaryRecord)
			Try
				If imageBinaryRecord IsNot Nothing Then
					Using image As NImage = imageBinaryRecord.ToNImage()
						image.Save(imageSaveFileDialog.FileName)
					End Using
				Else
					Dim imageAsciiBinaryRecord As ANImageAsciiBinaryRecord = CType(selectedRecord, ANImageAsciiBinaryRecord)
					Using image As NImage = imageAsciiBinaryRecord.ToNImage()
						image.Save(imageSaveFileDialog.FileName)
					End Using
				End If
			Catch ex As Exception
				MessageBox.Show(Me, ex.ToString(), "Can not save image", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try
		End If
	End Sub

	Private Sub SaveAsNFRecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveAsNFRecordToolStripMenuItem.Click
		Dim type9Record As ANType9Record = CType(GetSelectedRecord(), ANType9Record)
		nfRecordSaveFileDialog.FileName = Nothing
		If nfRecordSaveFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
			Try
				Using nfRecord As NFRecord = type9Record.ToNFRecord()
					File.WriteAllBytes(nfRecordSaveFileDialog.FileName, nfRecord.Save().ToArray())
				End Using
			Catch ex As Exception
				MessageBox.Show(Me, ex.ToString(), "Can not save as NFRecord", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try
		End If
	End Sub

	Private Sub SaveAsNTemplateToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveAsNTemplateToolStripMenuItem.Click
		nTemplateSaveFileDialog.FileName = Nothing
		If nTemplateSaveFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
			Try
				Using nTemplate As NTemplate = _template.ToNTemplate()
					File.WriteAllBytes(nTemplateSaveFileDialog.FileName, nTemplate.Save().ToArray())
				End Using
			Catch ex As Exception
				MessageBox.Show(Me, ex.ToString(), "Can not save as NTemplate", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try
		End If
	End Sub

	Private Sub MainFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		e.Cancel = Not FileSavePrompt()
	End Sub

	Private Sub MainFormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
		My.Settings.Default.Save()
	End Sub

	Private Sub NewToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles newToolStripMenuItem.Click, newToolStripButton.Click
		FileNew()
	End Sub

	Private Sub OpenToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItem.Click, openToolStripButton.Click
		FileOpen()
	End Sub

	Private Sub CloseToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles closeToolStripMenuItem.Click
		FileClose()
	End Sub

	Private Sub SaveToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveToolStripMenuItem.Click, saveToolStripButton.Click
		FileSave()
	End Sub

	Private Sub SaveAsToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveAsToolStripMenuItem.Click
		FileSaveAs()
	End Sub

	Private Sub ChangeVersionToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles changeVersionToolStripMenuItem.Click
		Dim form As New VersionForm()
		form.Text = "Select Version"
		form.SelectedVersion = _template.Version
		If form.ShowDialog() = Windows.Forms.DialogResult.OK AndAlso form.SelectedVersion <> _template.Version Then
			If MessageBox.Show("Some information may be lost.", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.OK Then
				Try
					_template.Version = form.SelectedVersion
					OnSelectedRecordChanged()
					If (Not _isModified) Then
						OnTemplateModified()
					Else
						UpdateTitle()
					End If
				Catch ex As Exception
					MessageBox.Show(ex.ToString(), "Can not change ANSI/NIST file version", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End If
		End If
	End Sub

	Private Sub ExitToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem.Click
		Close()
	End Sub

	Private Sub VersionsToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles versionsToolStripMenuItem.Click
		Dim form As New VersionForm()
		form.UseSelectMode = False
		form.Text = "Versions"
		form.ShowDialog()
	End Sub

	Private Sub RecordTypesToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles recordTypesToolStripMenuItem.Click
		Dim form As New RecordTypeForm()
		form.UseSelectMode = False
		form.Text = "Record Types"
		form.ShowDialog()
	End Sub

	Private Sub CharsetsToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles charsetsToolStripMenuItem.Click
		Dim form As New CharsetForm()
		form.UseSelectMode = False
		form.Text = "Charsets"
		form.ShowDialog()
	End Sub

	Private Sub RecordListViewSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles recordListView.SelectedIndexChanged
		OnSelectedRecordChanged()
	End Sub

	Private Sub ValidateToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles validateToolStripMenuItem.Click
		Dim settings As My.Settings = My.Settings.Default
		folderBrowserDialog.SelectedPath = settings.LastValidateDirectory
		If folderBrowserDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
			settings.LastValidateDirectory = folderBrowserDialog.SelectedPath
			settings.Save()
			Dim options As New OptionsForm()
			If options.ShowDialog() <> Windows.Forms.DialogResult.OK Then
				Return
			End If
			Dim form As New ValidateForm()
			form.Path = folderBrowserDialog.SelectedPath
			form.Filter = TemplateFilter
			form.ValidationLevel = options.ValidationLevel
			Dim validateFlags As UInteger = 0
			If options.UseNistMinutiaeNeighboars Then
				validateFlags = validateFlags Or ANTemplate.FlagUseNistMinutiaNeighbors
			End If
			If options.NonStrictRead Then
				validateFlags = validateFlags Or BdifTypes.FlagNonStrictRead
			End If
			If options.LeaveInvalidRecordsUnvalidated Then
				validateFlags = validateFlags Or ANTemplate.FlagLeaveInvalidRecordsUnvalidated
			End If
			If options.MergeDuplicateFields Then
				validateFlags = validateFlags Or ANRecord.FlagMergeDuplicateFields
			End If
			If options.RecoverFromBinaryData Then
				validateFlags = validateFlags Or ANRecord.FlagRecoverFromBinaryData
			End If
			form.Flags = validateFlags
			form.ShowDialog()
		End If
	End Sub

	Private Sub FieldListViewDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles fieldListView.DoubleClick
		If GetSelectedField() IsNot Nothing Then
			EditField()
		End If
	End Sub

	Private Sub FieldListViewSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles fieldListView.SelectedIndexChanged
		OnSelectedFieldChanged()
	End Sub

#Region "Add record toolstrip events"
	Private Sub AddType2RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType2RecordToolStripMenuItem.Click
		Try
			Using idcForm As New ANRecordCreateForm()
				If idcForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
					recordListView.SelectedIndices.Clear()
					Dim record As New ANType2Record(ANTemplate.VersionCurrent, idcForm.Idc)
					_template.Records.Add(record)
					AddRecordListViewItem(record).Selected = True
					OnTemplateModified()
				End If
			End Using
		Catch ex As Exception
			MessageBox.Show(ex.ToString(), "Can't add record", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub AddType3RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType3RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType3RecordCreateForm())
	End Sub

	Private Sub AddType4RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType4RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType4RecordCreateForm())
	End Sub

	Private Sub AddType5RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType5RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType5RecordCreateForm())
	End Sub

	Private Sub AddType6RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType6RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType6RecordCreateForm())
	End Sub

	Private Sub AddType7RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType7RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType7RecordCreateForm())
	End Sub

	Private Sub AddType8RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType8RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType8RecordCreateForm())
	End Sub

	Private Sub AddType9RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType9RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType9RecordCreateForm())
	End Sub

	Private Sub AddType10RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType10RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType10RecordCreateForm())
	End Sub

	Private Sub AddType13RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType13RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType13RecordCreateForm())
	End Sub

	Private Sub AddType14RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType14RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType14RecordCreateForm())
	End Sub

	Private Sub AddType15RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType15RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType15RecordCreateForm())
	End Sub

	Private Sub AddType16RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType16RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType16RecordCreateForm())
	End Sub

	Private Sub AddType17RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType17RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType17RecordCreateForm())
	End Sub

	Private Sub AddType99RecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addType99RecordToolStripMenuItem.Click
		AddValidatedRecord(New ANType99RecordCreateForm())
	End Sub

	Private Sub AddRecordToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles addRecordToolStripMenuItem.Click
		Dim form As New RecordTypeForm()
		If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
			Dim idcForm As New ANRecordCreateForm()
			If idcForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
				recordListView.SelectedIndices.Clear()
				Dim record As New ANRecord(form.RecordType, ANTemplate.VersionCurrent, idcForm.Idc)
				_template.Records.Add(record)
				AddRecordListViewItem(record).Selected = True
				OnTemplateModified()
			End If
		End If
	End Sub
#End Region

#End Region

#Region "Private methods"

	Private Sub OnTemplateModified()
		If (Not _isModified) Then
			_isModified = True
			UpdateTitle()
		End If
	End Sub

	Private Sub OnSelectedFieldChanged()
		Dim selectedField As ANField = GetSelectedField()
		editFieldToolStripMenuItem.Enabled = selectedField IsNot Nothing
		editFieldToolStripButton.Enabled = editFieldToolStripMenuItem.Enabled
		Dim canRemove As Boolean = fieldListView.SelectedItems.Count <> 0
		Dim version As NVersion
		If _template Is Nothing Then
			version = ANTemplate.VersionCurrent
		Else
			version = _template.Version
		End If
		Dim selectedRecord As ANRecord = GetSelectedRecord()
		Dim selectedRecordType As ANRecordType
		If selectedRecord Is Nothing Then
			selectedRecordType = Nothing
		Else
			selectedRecordType = selectedRecord.RecordType
		End If
		Dim validationLevel As ANValidationLevel
		If selectedRecord IsNot Nothing Then
			validationLevel = GetRecordValidationLevel(selectedRecord.IsValidated)
		Else
			validationLevel = GetRecordValidationLevel(True)
		End If

		For Each listViewItem As ListViewItem In fieldListView.SelectedItems
			Dim field As ANField = CType(listViewItem.Tag, ANField)
			Dim fieldNumber As Integer = field.Number
			If FieldNumberForm.IsFieldStandard(selectedRecordType, version, fieldNumber, validationLevel) Then
				canRemove = False
				Exit For
			End If
		Next listViewItem
		removeFieldToolStripMenuItem.Enabled = canRemove
		removeFieldToolStripButton.Enabled = removeFieldToolStripMenuItem.Enabled
	End Sub

	Private Sub OnSelectedRecordModified()
		If (Not highLevelPropertyGrid.ContainsFocus) Then
			Dim selectedObject As Object = highLevelPropertyGrid.SelectedObject
			highLevelPropertyGrid.SelectedObject = Nothing
			highLevelPropertyGrid.SelectedObject = selectedObject
		End If
		UpdateFieldListViewItem(fieldListView.Items(0))

		anRecordView.Invalidate()
		OnTemplateModified()
	End Sub

	Private Sub OnRecordsChanged()
		recordListView.BeginUpdate()
		recordListView.Items.Clear()
		If _template IsNot Nothing Then
			For Each record As ANRecord In _template.Records
				AddRecordListViewItem(record)
			Next record
			recordListView.Items(0).Selected = True
		Else
			OnSelectedRecordChanged()
		End If
		recordListView.EndUpdate()
		addRecordToolStripDropDownButton.Enabled = _template IsNot Nothing
		clearRecordsToolStripMenuItem.Enabled = addRecordToolStripDropDownButton.Enabled
		addToolStripMenuItem.Enabled = clearRecordsToolStripMenuItem.Enabled
		saveToolStripButton.Enabled = addToolStripMenuItem.Enabled
		changeVersionToolStripMenuItem.Enabled = saveToolStripButton.Enabled
		saveAsNTemplateToolStripMenuItem.Enabled = changeVersionToolStripMenuItem.Enabled
		saveAsToolStripMenuItem.Enabled = saveAsNTemplateToolStripMenuItem.Enabled
		saveToolStripMenuItem.Enabled = saveAsToolStripMenuItem.Enabled
		closeToolStripMenuItem.Enabled = saveToolStripMenuItem.Enabled
	End Sub

	Private Sub OnSelectedRecordChanged()
		Dim selectedRecord As ANRecord = GetSelectedRecord()

		If selectedRecord Is Nothing Then
			noHighLevelPropertiesLabel.Text = "No record is selected"
			noHighLevelPropertiesLabel.Visible = True
			highLevelPropertyGrid.Visible = False
		ElseIf (selectedRecord.ValidationLevel = ANValidationLevel.Minimal) Then
			noHighLevelPropertiesLabel.Text = "Selected records validation level is minimal"
			noHighLevelPropertiesLabel.Visible = True
			highLevelPropertyGrid.Visible = False
		Else
			highLevelPropertyGrid.SelectedObject = selectedRecord
			noHighLevelPropertiesLabel.Visible = False
			highLevelPropertyGrid.Visible = True
		End If

		fieldListView.BeginUpdate()
		fieldListView.Items.Clear()
		imageErrorToolTip.RemoveAll()
		anRecordView.Record = selectedRecord
		If selectedRecord IsNot Nothing Then
			Dim value As New StringBuilder()
			For Each field As ANField In selectedRecord.Fields
				fieldListView.Items.Add(CreateFieldListViewItem(selectedRecord, field, value))
			Next field
			fieldListView.Items(0).Selected = True
		End If
		fieldListView.EndUpdate()

		Dim selected As ListView.SelectedIndexCollection = recordListView.SelectedIndices
		removeRecordsToolStripMenuItem.Enabled = selected.Count > 0 AndAlso Not selected.Contains(0)
		removeRecordToolStripButton.Enabled = removeRecordsToolStripMenuItem.Enabled
		saveRecordDataToolStripMenuItem.Enabled = selectedRecord IsNot Nothing AndAlso (selectedRecord.RecordType.DataType = ANRecordDataType.Binary OrElse selectedRecord.RecordType.DataType = ANRecordDataType.AsciiBinary)
		saveImageToolStripMenuItem.Enabled = selectedRecord IsNot Nothing AndAlso selectedRecord.IsValidated AndAlso ((TypeOf selectedRecord Is ANImageBinaryRecord AndAlso (selectedRecord.RecordType.Number <> 8 OrElse (CType(selectedRecord, ANType8Record)).SignatureRepresentationType <> ANSignatureRepresentationType.VectorData)) OrElse TypeOf selectedRecord Is ANImageAsciiBinaryRecord)
		saveAsNFRecordToolStripMenuItem.Enabled = selectedRecord IsNot Nothing AndAlso selectedRecord.IsValidated AndAlso selectedRecord.RecordType.Number = 9 AndAlso (CType(selectedRecord, ANType9Record)).HasMinutiae
		addFieldToolStripMenuItem.Enabled = selectedRecord IsNot Nothing AndAlso selectedRecord.RecordType.DataType <> ANRecordDataType.Binary
		addFieldToolStripButton.Enabled = addFieldToolStripMenuItem.Enabled
		OnSelectedFieldChanged()
	End Sub

	Private Function AddRecordListViewItem(ByVal record As ANRecord) As ListViewItem
		Dim recordItem As New ListViewItem(String.Format("Type-{0}{1}", record.RecordType.Number, CStr(IIf(record.IsValidated, Nothing, "*"))))
		recordItem.Tag = record
		recordItem.SubItems.Add(record.RecordType.Name)
		If record.RecordType IsNot ANRecordType.Type1 Then
			recordItem.SubItems.Add(record.Idc.ToString())
		End If
		recordListView.Items.Add(recordItem)
		Return recordItem
	End Function

	Private Function GetSelectedRecord() As ANRecord
		If recordListView.SelectedItems.Count = 1 Then
			Return CType(recordListView.SelectedItems(0).Tag, ANRecord)
		Else
			Return Nothing
		End If
	End Function

	Private Function GetRecordValidationLevel(ByVal recordIsValidated As Boolean) As ANValidationLevel
		If recordIsValidated AndAlso _template IsNot Nothing Then
			Return _template.ValidationLevel
		Else
			Return ANValidationLevel.Minimal
		End If
	End Function

	Private Function CreateFieldListViewItem(ByVal record As ANRecord, ByVal field As ANField, ByVal value As StringBuilder) As ListViewItem
		Dim version As NVersion = _template.Version
		Dim recordType As ANRecordType = record.RecordType
		Dim fieldNumber As Integer = field.Number
		Dim item As New ListViewItem(String.Format("{0}.{1:000}", recordType.Number, fieldNumber))
		item.Tag = field
		If FieldNumberForm.IsFieldStandard(recordType, version, fieldNumber, GetRecordValidationLevel(record.IsValidated)) Then
			item.ForeColor = Color.FromKnownColor(KnownColor.GrayText)
		End If
		Dim isKnown As Boolean = recordType.IsFieldKnown(version, fieldNumber)
		Dim id As String
		Dim nameStr As String
		If isKnown Then
			id = recordType.GetFieldId(version, fieldNumber)
			nameStr = recordType.GetFieldName(version, fieldNumber)
		Else
			id = "UNK"
			nameStr = "Unknown field"
		End If
		If id <> String.Empty Then
			nameStr = String.Format("{0} ({1})", Name, id)
		End If
		item.SubItems.Add(nameStr)
		FieldForm.GetFieldValue(field, value)
		item.SubItems.Add(value.ToString())
		Return item
	End Function

	Private Sub UpdateFieldListViewItem(ByVal item As ListViewItem)
		Dim value As New StringBuilder()
		FieldForm.GetFieldValue(CType(item.Tag, ANField), value)
		item.SubItems(2).Text = value.ToString()
	End Sub

	Private Function GetSelectedField() As ANField
		If fieldListView.SelectedItems.Count = 1 Then
			Return CType(fieldListView.SelectedItems(0).Tag, ANField)
		Else
			Return Nothing
		End If
	End Function

	Private Sub EditField()
		Dim form As New FieldForm()
		form.Field = GetSelectedField()
		Dim selectedRecord As ANRecord = GetSelectedRecord()
		form.IsReadOnly = FieldNumberForm.IsFieldStandard(selectedRecord.RecordType, _template.Version, form.Field.Number, GetRecordValidationLevel(selectedRecord.IsValidated))
		form.ShowDialog()
		If form.IsModified Then
			UpdateFieldListViewItem(fieldListView.SelectedItems(0))
			OnSelectedRecordModified()
		End If
	End Sub

	Private Sub UpdateTitle()
		If _name Is Nothing Then
			Text = ApplicationName
		Else
			Text = String.Format("{0}{1} [V{2}, VL: {3}] - {4}", _name, CStr(IIf(_isModified, "*", String.Empty)), _template.Version, _template.ValidationLevel, ApplicationName)
		End If
	End Sub

	Private Sub SetTemplate(ByVal value As ANTemplate, ByVal fileName As String)
		Dim newTemplate As Boolean = _template IsNot value
		_template = value
		_fileName = fileName
		If newTemplate Then
			OnRecordsChanged()
		End If
		_isModified = False
		_templateIndex += 1
		If _template Is Nothing Then
			_name = Nothing
		ElseIf fileName Is Nothing Then
			_name = String.Format("NewFile{0}", _templateIndex)
		Else
			_name = Path.GetFileNameWithoutExtension(fileName)
		End If
		UpdateTitle()
	End Sub

	Private Function NewTemplate() As Boolean
		Dim flags As UInteger = 0
		Dim template As ANTemplate
		Dim tot As String = "NEUR"
		Dim dai As String = "NeurotecDest"
		Dim ori As String = "NeurotecOrig"
		Dim tcn As String = "00001"
		Dim validation As ANValidationLevel = My.Settings.Default.NewValidationLevel
		Using type1CreateForm As New ANType1RecordCreateForm()
			type1CreateForm.TransactionType = tot
			type1CreateForm.DestinationAgency = dai
			type1CreateForm.OriginatingAgency = ori
			type1CreateForm.TransactionControl = tcn
			type1CreateForm.ValidationLevel = validation
			type1CreateForm.UseNISTMinutiaNeighboars = My.Settings.Default.NewUseNistMinutiaNeighbors
			If type1CreateForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
				validation = type1CreateForm.ValidationLevel
				My.Settings.Default.NewValidationLevel = validation
				My.Settings.Default.NewUseNistMinutiaNeighbors = type1CreateForm.UseNISTMinutiaNeighboars
				If type1CreateForm.UseNISTMinutiaNeighboars Then
					flags = ANTemplate.FlagUseNistMinutiaNeighbors
				End If
				Select Case validation
					Case ANValidationLevel.Minimal
						template = New ANTemplate(ANTemplate.VersionCurrent, ANValidationLevel.Minimal, flags)
					Case ANValidationLevel.Standard
						tot = type1CreateForm.TransactionType
						dai = type1CreateForm.DestinationAgency
						ori = type1CreateForm.OriginatingAgency
						tcn = type1CreateForm.TransactionControl
						template = New ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, flags)
					Case Else
						Throw New NotImplementedException()
				End Select
				SetTemplate(template, Nothing)
				Return True
			End If
			Return False
		End Using
	End Function

	Private Function OpenTemplate(ByVal fileName As String) As Boolean
		Using options As New OptionsForm()
			If options.ShowDialog() = Windows.Forms.DialogResult.OK Then
				Dim template As ANTemplate
				Dim readFlags As UInteger = 0
				If options.UseNistMinutiaeNeighboars Then
					readFlags = readFlags Or ANTemplate.FlagUseNistMinutiaNeighbors
				End If
				If options.NonStrictRead Then
					readFlags = readFlags Or BdifTypes.FlagNonStrictRead
				End If
				If options.LeaveInvalidRecordsUnvalidated Then
					readFlags = readFlags Or ANTemplate.FlagLeaveInvalidRecordsUnvalidated
				End If
				If options.MergeDuplicateFields Then
					readFlags = readFlags Or ANRecord.FlagMergeDuplicateFields
				End If
				If options.RecoverFromBinaryData Then
					readFlags = readFlags Or ANRecord.FlagRecoverFromBinaryData
				End If

				Try
					template = New ANTemplate(fileName, options.ValidationLevel, readFlags)
				Catch ex As Exception
					MessageBox.Show(Me, ex.ToString(), "Can not open ANSI/NIST file", MessageBoxButtons.OK, MessageBoxIcon.Error)
					Return False
				End Try
				SetTemplate(template, fileName)
			End If
			Return False
		End Using

	End Function

	Private Function SaveTemplate(ByVal fileName As String) As Boolean
		Try
			_template.Save(fileName)
		Catch e As Exception
			MessageBox.Show(Me, e.ToString(), "Can not save ANSI/NIST file", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
		End Try
		SetTemplate(_template, fileName)
		Return True
	End Function

	Private Function FileSavePrompt() As Boolean
		If _isModified Then
			Select Case MessageBox.Show(Me, "ANSI/NIST file modified. Save changes?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
				Case Windows.Forms.DialogResult.Yes
					Return FileSave()
				Case Windows.Forms.DialogResult.No
					Return True
				Case Else
					Return False
			End Select
		Else
			Return True
		End If
	End Function

	Private Function FileNew() As Boolean
		If FileSavePrompt() Then
			Return NewTemplate()
		Else
			Return False
		End If
	End Function

	Private Function FileOpen() As Boolean
		If FileSavePrompt() Then
			Dim settings As My.Settings = My.Settings.Default
			openFileDialog.FileName = String.Empty
			openFileDialog.InitialDirectory = settings.LastDirectory
			If openFileDialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
				settings.LastDirectory = Path.GetDirectoryName(openFileDialog.FileName)
				Return OpenTemplate(openFileDialog.FileName)
			Else
				Return False
			End If
		Else
			Return False
		End If
	End Function

	Private Sub FileClose()
		If FileSavePrompt() Then
			SetTemplate(Nothing, Nothing)
		End If
	End Sub

	Private Function FileSave() As Boolean
		If _fileName Is Nothing Then
			Return FileSaveAs()
		Else
			Return SaveTemplate(_fileName)
		End If
	End Function

	Private Function FileSaveAs() As Boolean
		Dim settings As My.Settings = My.Settings.Default
		If _fileName IsNot Nothing Then
			saveFileDialog.FileName = _fileName
		Else
			saveFileDialog.FileName = _name
		End If
		saveFileDialog.InitialDirectory = settings.LastDirectory
		If saveFileDialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
			settings.LastDirectory = Path.GetDirectoryName(saveFileDialog.FileName)
			Return SaveTemplate(saveFileDialog.FileName)
		Else
			Return False
		End If
	End Function

	Private Sub AddValidatedRecord(ByVal createForm As ANRecordCreateForm)
		Try
			createForm.Template = _template
			If createForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
				recordListView.SelectedIndices.Clear()
				AddRecordListViewItem(createForm.CreatedRecord).Selected = True
				OnTemplateModified()
			End If
		Catch ex As Exception
			MessageBox.Show(ex.ToString(), "Can't add record", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#End Region
End Class
