Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports Neurotec.Gui
Imports System.IO
Imports Neurotec.Images
Imports Neurotec.Biometrics

Partial Public Class MainForm
	Inherits Form
#Region "Private static methods"

	Private Shared Function LoadTemplate(ByVal openFileDialog As OpenFileDialog, <System.Runtime.InteropServices.Out()> ByRef fileName As String) As Byte()
		openFileDialog.FileName = Nothing
		If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			fileName = openFileDialog.FileName
			Return File.ReadAllBytes(openFileDialog.FileName)
		End If
		fileName = Nothing
		Return Nothing
	End Function

	Private Shared Function LoadTemplate(ByVal openFileDialog As OpenFileDialog) As Byte()
		Dim fileName As String = String.Empty
		Return LoadTemplate(openFileDialog, fileName)
	End Function

	Private Shared Sub SaveTemplate(ByVal saveFileDialog As SaveFileDialog, ByVal fileName As String, ByVal template() As Byte)
		saveFileDialog.FileName = fileName
		If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			File.WriteAllBytes(saveFileDialog.FileName, template)
		End If
	End Sub

	Private Shared Sub SaveTemplate(ByVal saveFileDialog As SaveFileDialog, ByVal template() As Byte)
		SaveTemplate(saveFileDialog, Nothing, template)
	End Sub

#End Region

#Region "Private fields"

	Private ReadOnly _infoLookup As New Dictionary(Of Object, RecordInfo)()

	Private _template As NTemplate
	Private _selectedRecord As Object
	Private _fileName As String

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)

		aboutToolStripMenuItem.Text = "&"c + AboutBox.Name

		OnSelectedItemChanged()
		NewTemplate()
	End Sub

#End Region

#Region "Private methods"

	Private Function GetRecordInfo(ByVal record As Object) As RecordInfo
		Dim info As RecordInfo = Nothing

		If record Is Nothing Then
			' dummy record info in order to avoid special cases everywhere
			Return New RecordInfo()
		End If

		If _infoLookup.TryGetValue(record, info) Then
			Return info
		End If

		info = New RecordInfo()
		_infoLookup.Add(record, info)
		Return info
	End Function

	Private Sub RefreshTemplateView()
		treeView.BeginUpdate()
		treeView.Nodes.Clear()
		Dim selectedNode As TreeNode = Nothing
		If _template IsNot Nothing Then
			Dim rootText As String = "NTemplate"
			If (Not String.IsNullOrEmpty(_fileName)) Then
				rootText = Path.GetFileNameWithoutExtension(_fileName)
			End If
			Dim templateNode As New TreeNode(rootText & GetRecordInfo(_template.Fingers).Text)
			templateNode.Tag = _template
			If _template.Fingers IsNot Nothing Then
				Dim fingersNode As TreeNode = templateNode.Nodes.Add("Fingers" & GetRecordInfo(_template.Fingers).Text)
				fingersNode.Tag = _template.Fingers
				If _selectedRecord Is _template.Fingers Then
					selectedNode = fingersNode
				End If
				For i As Integer = 0 To _template.Fingers.Records.Count - 1
					Dim rec As NFRecord = _template.Fingers.Records(i)
					Dim fingerNode As TreeNode = fingersNode.Nodes.Add(String.Format("Finger{0}{1}", i, GetRecordInfo(rec).Text))
					fingerNode.Tag = rec
					If _selectedRecord Is rec Then
						selectedNode = fingerNode
					End If
				Next i
			End If
			If _template.Faces IsNot Nothing Then
				Dim facesNode As TreeNode = templateNode.Nodes.Add("Faces" & GetRecordInfo(_template.Faces).Text)
				facesNode.Tag = _template.Faces
				If _selectedRecord Is _template.Faces Then
					selectedNode = facesNode
				End If
				For i As Integer = 0 To _template.Faces.Records.Count - 1
					Dim rec As NLRecord = _template.Faces.Records(i)
					Dim faceNode As TreeNode = facesNode.Nodes.Add(String.Format("Face{0}{1}", i, GetRecordInfo(rec).Text))
					faceNode.Tag = rec
					If _selectedRecord Is rec Then
						selectedNode = faceNode
					End If
				Next i
			End If
			If _template.Irises IsNot Nothing Then
				Dim irisesNode As TreeNode = templateNode.Nodes.Add("Irises" & GetRecordInfo(_template.Irises).Text)
				irisesNode.Tag = _template.Irises
				If _selectedRecord Is _template.Irises Then
					selectedNode = irisesNode
				End If
				For i As Integer = 0 To _template.Irises.Records.Count - 1
					Dim rec As NERecord = _template.Irises.Records(i)
					Dim irisNode As TreeNode = irisesNode.Nodes.Add(String.Format("Iris{0}{1}", i, GetRecordInfo(rec).Text))
					irisNode.Tag = rec
					If _selectedRecord Is rec Then
						selectedNode = irisNode
					End If
				Next i
			End If
			If _template.Palms IsNot Nothing Then
				Dim palmsNode As TreeNode = templateNode.Nodes.Add("Palms" & GetRecordInfo(_template.Palms).Text)
				palmsNode.Tag = _template.Palms
				If _selectedRecord Is _template.Palms Then
					selectedNode = palmsNode
				End If
				For i As Integer = 0 To _template.Palms.Records.Count - 1
					Dim rec As NFRecord = _template.Palms.Records(i)
					Dim palmNode As TreeNode = palmsNode.Nodes.Add(String.Format("Palm{0}{1}", i, GetRecordInfo(rec).Text))
					palmNode.Tag = rec
					If _selectedRecord Is rec Then
						selectedNode = palmNode
					End If
				Next i
			End If
			If _template.Voices IsNot Nothing Then
				Dim voicesNode As TreeNode = templateNode.Nodes.Add("Voices" & GetRecordInfo(_template.Voices).Text)
				voicesNode.Tag = _template.Palms
				If _selectedRecord Is _template.Voices Then
					selectedNode = voicesNode
				End If
				For i As Integer = 0 To _template.Voices.Records.Count - 1
					Dim rec As NSRecord = _template.Voices.Records(i)
					Dim voiceNode As TreeNode = voicesNode.Nodes.Add(String.Format("Voice{0}{1}", i, GetRecordInfo(rec).Text))
					voiceNode.Tag = rec
					If _selectedRecord Is rec Then
						selectedNode = voiceNode
					End If
				Next i
			End If
			treeView.Nodes.Add(templateNode)
			templateNode.ExpandAll()
			If selectedNode Is Nothing Then
				selectedNode = templateNode
			End If
			treeView.SelectedNode = selectedNode
		End If
		treeView.EndUpdate()
	End Sub

	Private Sub ClearData()
		_template = New NTemplate()
		_selectedRecord = Nothing
		_infoLookup.Clear()
		_fileName = Nothing
	End Sub

	Private Sub NewTemplate()
		ClearData()
		RefreshTemplateView()
	End Sub

	Private Sub OpenTemplate()
		Try
			Dim templ() As Byte = LoadTemplate(nTemplateOpenFileDialog, _fileName)
			_selectedRecord = Nothing
			_template = Nothing
			If templ Is Nothing Then
				Return
			End If
			Try
				_template = New NTemplate(templ)
			Catch e1 As FormatException
				Dim record As New NFRecord(templ)
				_template = New NTemplate(record.Save())
			End Try
			RefreshTemplateView()
		Catch ex As Exception
			MessageBox.Show("Failed to open specified template. Reason: " + ex.Message, "NTemplate Sample", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub SaveTemplate()
		Try
			SaveTemplate(nTemplateSaveFileDialog, _fileName, _template.Save().ToArray())
		Catch ex As Exception
			MessageBox.Show("Failed to save specified template. Reason: " + ex.Message, "NTemplate Sample", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub OnSelectedItemChanged()
		Dim subject As New NSubject()
		Dim template As New NTemplate()
		Dim record As Object = Nothing
		If treeView.SelectedNode IsNot Nothing Then
			record = treeView.SelectedNode.Tag
		End If

		Dim recordInfo As RecordInfo = GetRecordInfo(record)
		Dim isRoot As Boolean = treeView.SelectedNode IsNot Nothing AndAlso treeView.SelectedNode Is treeView.Nodes(0)
		If TypeOf record Is NFRecord Then
			Dim fingerOrPalmRecord = CType(record, NFRecord)
			If NBiometricTypes.IsPositionFinger(fingerOrPalmRecord.Position) Then
				Dim nfTemplate As New NFTemplate()
				template.Fingers = nfTemplate
				template.Fingers.Records.Add(fingerOrPalmRecord)
				subject.SetTemplate(template)
				fingerView.Finger = subject.Fingers(0)
				fingerView.Visible = True
			Else
				Dim nfTemplate As New NFTemplate(True)
				template.Palms = nfTemplate
				template.Palms.Records.Add(fingerOrPalmRecord)
				subject.SetTemplate(template)
				fingerView.Finger = subject.Palms(0)
				fingerView.Visible = True
			End If
		Else
			fingerView.Visible = False
			fingerView.Finger = Nothing
		End If
		nViewZoomSlider.Visible = fingerView.Visible

		propertyGrid.SelectedObject = record

		saveItemToolStripMenuItem.Visible = Not isRoot
		editToolStripSeparator5.Visible = saveItemToolStripMenuItem.Visible
		removeToolStripMenuItem.Visible = editToolStripSeparator5.Visible
		editToolStripSeparator4.Visible = removeToolStripMenuItem.Visible

	End Sub

#End Region

#Region "Private form events"

	Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
		AboutBox.Show()
	End Sub

	Private Sub newToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles newToolStripMenuItem.Click
		NewTemplate()
	End Sub

	Private Sub openToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItem.Click
		OpenTemplate()
	End Sub

	Private Sub saveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveToolStripMenuItem.Click
		SaveTemplate()
	End Sub

	Private Sub exitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem.Click
		Close()
	End Sub

	Private Sub treeView_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles treeView.AfterSelect
		OnSelectedItemChanged()
	End Sub

	Private Sub addFingersToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFingersToolStripMenuItem.Click
		_template.Fingers = New NFTemplate()
		RefreshTemplateView()
	End Sub

	Private Sub addFingersFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFingersFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(nfTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Fingers = New NFTemplate(packedData)
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addFacesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFacesToolStripMenuItem.Click
		_template.Faces = New NLTemplate()
		RefreshTemplateView()
	End Sub

	Private Sub addFacesFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFacesFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(nlTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Faces = New NLTemplate(packedData)
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addIrisesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addIrisesToolStripMenuItem.Click
		_template.Irises = New NETemplate()
		RefreshTemplateView()
	End Sub

	Private Sub addIrisesFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addIrisesFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(neTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Irises = New NETemplate(packedData)
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addPalmsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addPalmsToolStripMenuItem.Click
		_template.Palms = New NFTemplate(True)
		RefreshTemplateView()
	End Sub

	Private Sub addVoicesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addVoicesToolStripMenuItem.Click
		_template.Voices = New NSTemplate()
		RefreshTemplateView()
	End Sub

	Private Sub addVoicesFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addVoicesFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(nsTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Voices = New NSTemplate(packedData)
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addPalmsFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addPalmsFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(nfTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Palms = New NFTemplate(packedData)
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addFingerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFingerToolStripMenuItem.Click
		Dim newNFRec As New NewNFRecordForm()
		If newNFRec.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			_template.Fingers.Records.Add(New NFRecord(newNFRec.RecordWidth, newNFRec.RecordHeight, newNFRec.HorzResolution, newNFRec.VertResolution))
			RefreshTemplateView()
		End If
	End Sub

	Private Sub addFaceToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFaceToolStripMenuItem.Click
		_template.Faces.Records.Add(New NLRecord())
		RefreshTemplateView()
	End Sub

	Private Sub addIrisToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addIrisToolStripMenuItem.Click
		Dim newNERec As New NewNERecordForm()
		If newNERec.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			_template.Irises.Records.Add(New NERecord(newNERec.RecordWidth, newNERec.RecordHeight))
			RefreshTemplateView()
		End If
	End Sub

	Private Sub addVoiceToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addVoiceToolStripMenuItem.Click
		_template.Voices.Records.Add(New NSRecord())
		RefreshTemplateView()
	End Sub

	Private Sub addPalmToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addPalmToolStripMenuItem.Click
		Dim newNFRec As New NewNFRecordForm()
		newNFRec.RecordWidth = 1000
		newNFRec.RecordHeight = 1000
		If newNFRec.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			_template.Palms.Records.Add(New NFRecord(True, newNFRec.RecordWidth, newNFRec.RecordHeight, newNFRec.HorzResolution, newNFRec.VertResolution))
			RefreshTemplateView()
		End If
	End Sub

	Private Sub addFingerFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFingerFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(nfTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Fingers.Records.Add(New NFRecord(packedData))
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addFaceFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFaceFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(nfTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Faces.Records.Add(New NLRecord(packedData))
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addIrisFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addIrisFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(neTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Irises.Records.Add(New NERecord(packedData))
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addVoiceFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addVoiceFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(nsTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Voices.Records.Add(New NSRecord(packedData))
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub addPalmFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addPalmFromFileToolStripMenuItem.Click
		Dim fileName As String = String.Empty
		Dim packedData() As Byte = LoadTemplate(nfTemplateOpenFileDialog, fileName)
		If packedData IsNot Nothing Then
			Try
				_template.Palms.Records.Add(New NFRecord(packedData))
				RefreshTemplateView()
			Catch ex As FormatException
				MessageBox.Show(ex.Message)
			End Try
		End If
	End Sub

	Private Sub removeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeToolStripMenuItem.Click
		If treeView.SelectedNode Is Nothing OrElse treeView.SelectedNode.Tag Is Nothing Then
			Return
		End If

		Dim record As Object = treeView.SelectedNode.Tag
		Dim recordType As Type = record.GetType()

		If recordType Is GetType(NFTemplate) Then
			If (CType(record, NFTemplate)).IsPalm Then
				_template.Palms = Nothing
			Else
				_template.Fingers = Nothing
			End If
		ElseIf recordType Is GetType(NLTemplate) Then
			_template.Faces = Nothing
		ElseIf recordType Is GetType(NETemplate) Then
			_template.Irises = Nothing
		ElseIf recordType Is GetType(NFRecord) Then
			If (CType(record, NFRecord)).ImpressionType >= NFImpressionType.LiveScanPalm Then
				_template.Palms.Records.Remove(CType(record, NFRecord))
			Else
				_template.Fingers.Records.Remove(CType(record, NFRecord))
			End If
		ElseIf recordType Is GetType(NLRecord) Then
			_template.Faces.Records.Remove(CType(record, NLRecord))
		ElseIf recordType Is GetType(NERecord) Then
			_template.Irises.Records.Remove(CType(record, NERecord))
		Else
			Throw New NotSupportedException()
		End If

		Dim recordInfo As RecordInfo = GetRecordInfo(record)
		Dim disposable As IDisposable = TryCast(recordInfo.SourceData, IDisposable)
		If disposable IsNot Nothing Then
			disposable.Dispose()
			disposable = Nothing
		End If
		_infoLookup.Remove(record)

		_selectedRecord = Nothing

		RefreshTemplateView()
	End Sub

	Private Sub saveItemToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveItemToolStripMenuItem.Click
		Dim record As Object = treeView.SelectedNode.Tag
		If record Is Nothing Then
			record = _template
		End If
		Dim recordType As Type = record.GetType()

		If recordType Is GetType(NTemplate) Then
			Dim packedTemplate() As Byte = (CType(record, NTemplate)).Save().ToArray()
			SaveTemplate(nTemplateSaveFileDialog, packedTemplate)
		ElseIf recordType Is GetType(NFTemplate) Then
			Dim packedTemplate() As Byte = (CType(record, NFTemplate)).Save().ToArray()
			SaveTemplate(nfTemplateSaveFileDialog, packedTemplate)
		ElseIf recordType Is GetType(NLTemplate) Then
			Dim packedTemplate() As Byte = (CType(record, NLTemplate)).Save().ToArray()
			SaveTemplate(nlTemplateSaveFileDialog, packedTemplate)
		ElseIf recordType Is GetType(NETemplate) Then
			Dim packedTemplate() As Byte = (CType(record, NETemplate)).Save().ToArray()
			SaveTemplate(neTemplateSaveFileDialog, packedTemplate)
		ElseIf recordType Is GetType(NSTemplate) Then
			Dim packedTemplate() As Byte = (CType(record, NSTemplate)).Save().ToArray()
			SaveTemplate(nsTemplateSaveFileDialog, packedTemplate)
		ElseIf recordType Is GetType(NFRecord) Then
			Dim packedTemplate() As Byte = (CType(record, NFRecord)).Save().ToArray()
			SaveTemplate(nfRecordSaveFileDialog, packedTemplate)
		ElseIf recordType Is GetType(NLRecord) Then
			Dim packedTemplate() As Byte = (CType(record, NLRecord)).Save().ToArray()
			SaveTemplate(nlRecordSaveFileDialog, packedTemplate)
		ElseIf recordType Is GetType(NERecord) Then
			Dim packedTemplate() As Byte = (CType(record, NERecord)).Save().ToArray()
			SaveTemplate(neRecordSaveFileDialog, packedTemplate)
		ElseIf recordType Is GetType(NSRecord) Then
			Dim packedTemplate() As Byte = (CType(record, NSRecord)).Save().ToArray()
			SaveTemplate(nsRecordSaveFileDialog, packedTemplate)
		Else
			Throw New NotSupportedException()
		End If
	End Sub

	Private Sub editToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As EventArgs) Handles editToolStripMenuItem.DropDownOpening
		addFingersFromFileToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Fingers Is Nothing)
		addFingersToolStripMenuItem.Enabled = addFingersFromFileToolStripMenuItem.Enabled
		addFacesFromFileToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Faces Is Nothing)
		addFacesToolStripMenuItem.Enabled = addFacesFromFileToolStripMenuItem.Enabled
		addIrisesFromFileToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Irises Is Nothing)
		addIrisesToolStripMenuItem.Enabled = addIrisesFromFileToolStripMenuItem.Enabled
		addPalmsFromFileToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Palms Is Nothing)
		addPalmsToolStripMenuItem.Enabled = addPalmsFromFileToolStripMenuItem.Enabled
		addVoicesFromFileToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Voices Is Nothing)
		addVoicesToolStripMenuItem.Enabled = addVoicesFromFileToolStripMenuItem.Enabled

		addFingerToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Fingers IsNot Nothing)
		addFingerFromFileToolStripMenuItem.Enabled = addFingerToolStripMenuItem.Enabled
		addFaceToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Faces IsNot Nothing)
		addFaceFromFileToolStripMenuItem.Enabled = addFaceToolStripMenuItem.Enabled
		addIrisToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Irises IsNot Nothing)
		addIrisFromFileToolStripMenuItem.Enabled = addIrisToolStripMenuItem.Enabled
		addPalmToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Palms IsNot Nothing)
		addPalmFromFileToolStripMenuItem.Enabled = addPalmToolStripMenuItem.Enabled
		addVoiceToolStripMenuItem.Enabled = (_template IsNot Nothing AndAlso _template.Voices IsNot Nothing)
		addVoiceFromFileToolStripMenuItem.Enabled = addVoiceToolStripMenuItem.Enabled
	End Sub

#End Region
End Class

Friend Class RecordInfo
	Implements IDisposable
#Region "Private fields"

	Private _isDisposed As Boolean = False

#End Region

#Region "Public fields"

	Public Text As String = String.Empty
	Public SourceData As Object

#End Region

#Region "Public constructors"

	Public Sub New()
	End Sub

#End Region

#Region "Destructor"

	Protected Overrides Sub Finalize()
		Dispose(False)
	End Sub

#End Region

#Region "Private methods"

	Private Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			Dim sd As IDisposable = TryCast(SourceData, IDisposable)
			If sd IsNot Nothing Then
				sd.Dispose()
			End If
		End If
		SourceData = Nothing
	End Sub

#End Region

#Region "IDisposable Members"

	Public Sub Dispose() Implements IDisposable.Dispose
		If (Not _isDisposed) Then
			Dispose(True)
			_isDisposed = True
			GC.SuppressFinalize(Me)
		End If
	End Sub

#End Region
End Class
