Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Gui
Imports Neurotec.Images
Imports Neurotec.IO

Partial Public Class MainForm
	Inherits Form
#Region "Private static fields"

	Private Shared ansiBdbFormat As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerImage)
	Private Shared isoBdbFormat As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFingerImage)

#End Region

#Region "Private static methods"

	Private Shared Function GetOptions(ByVal mode As BdifOptionsForm.BdifOptionsFormMode, ByRef standard As BdifStandard, ByRef flags As UInteger, ByRef version As NVersion) As Boolean
		Using form = New FIRecordOptionsForm()
			form.Standard = standard
			form.Flags = flags
			form.Mode = mode
			form.Version = version
			If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				standard = form.Standard
				flags = form.Flags
				version = form.Version
			Else
				Return False
			End If
		End Using
		Return True
	End Function

	Private Shared Function GetOptions(ByRef format As UInteger) As Boolean
		Using form As New CbeffRecordOptionsForm()
			If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				format = form.PatronFormat
			Else
				Return False
			End If
		End Using
		Return True
	End Function

	Private Shared Function LoadTemplate(ByVal openFileDialog As OpenFileDialog, <System.Runtime.InteropServices.Out()> ByRef fileName As String) As Byte()
		openFileDialog.FileName = Nothing
		If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			fileName = openFileDialog.FileName
			Return File.ReadAllBytes(openFileDialog.FileName)
		End If
		fileName = Nothing
		Return Nothing
	End Function

	Private Shared Sub SaveTemplate(ByVal saveFileDialog As SaveFileDialog, ByVal fileName As String, ByVal template() As Byte)
		saveFileDialog.FileName = fileName
		If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			File.WriteAllBytes(saveFileDialog.FileName, template)
		End If
	End Sub

	Private Shared Function ConvertToStandard(ByVal record As FIRecord, ByVal newStandard As BdifStandard, ByVal flags As UInteger, ByVal version As NVersion) As FIRecord
		If record.Standard = newStandard AndAlso record.Flags = flags AndAlso record.Version = version Then
			Return record
		End If

		Return New FIRecord(record, flags, newStandard, version)
	End Function

	Private Shared Sub AddFingerView(ByVal fingerNode As TreeNode, ByVal fingerView As FirFingerView)
		Dim index As Integer = fingerNode.Nodes.Count + 1
		Dim fingerViewNode = New TreeNode("FingerView " & index)
		fingerViewNode.Tag = fingerView
		fingerNode.Nodes.Add(fingerViewNode)
	End Sub

	Private Shared Sub AddFingers(ByVal templateNode As TreeNode, ByVal record As FIRecord)
		For Each finger As FirFingerView In record.FingerViews
			AddFingerView(templateNode, finger)
		Next finger
	End Sub

	Private Shared Sub GetBiometricDataBlock(ByVal node As TreeNode, ByVal record As CbeffRecord)
		If record.BdbBuffer IsNot Nothing AndAlso (record.BdbFormat = ansiBdbFormat OrElse record.BdbFormat = isoBdbFormat) Then
			Dim standard As BdifStandard = If(record.BdbFormat = ansiBdbFormat, BdifStandard.Ansi, BdifStandard.Iso)
			Dim fiRecord As New FIRecord(record.BdbBuffer, standard)
			Dim child As New TreeNode("FIRecord")
			child.Tag = fiRecord
			AddFingers(child, fiRecord)
			node.Nodes.Add(child)
		End If
	End Sub

	Private Shared Sub AddFingers(ByVal node As TreeNode, ByVal record As CbeffRecord)
		GetBiometricDataBlock(node, record)

		For Each child As CbeffRecord In record.Records
			AddFingers(node, child)
		Next child
	End Sub

#End Region

#Region "Private fields"

	Private _fileName As String

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		imageSaveFileDialog.Filter = NImages.GetSaveFileFilterString()
		aboutToolStripMenuItem.Text = "&"c + AboutBox.Name
		OnSelectedItemChanged()
	End Sub

#End Region

#Region "Private methods"

	Private Sub OnSelectedItemChanged()
		Dim selectedNode As TreeNode = treeView.SelectedNode
		If selectedNode Is Nothing Then
			Return
		End If
		Dim fingerView As FirFingerView = TryCast(treeView.SelectedNode.Tag, FirFingerView)
		If fingerView IsNot Nothing Then
			fiView.Record = fingerView
			propertyGrid.SelectedObject = fingerView
		Else
			fiView.Record = Nothing
			propertyGrid.SelectedObject = selectedNode.Tag
		End If

		If TypeOf selectedNode.Tag Is CbeffRecord Then
			addFingerViewFromImageToolStripMenuItem.Enabled = False
			removeFingerToolStripMenuItem.Enabled = False
			saveIngerAsImageToolStripMenuItem.Enabled = False
			convertToolStripMenuItem.Enabled = False
			saveFingerAsToolStripMenuItem.Enabled = False
		Else
			addFingerViewFromImageToolStripMenuItem.Enabled = True
			removeFingerToolStripMenuItem.Enabled = True
			saveIngerAsImageToolStripMenuItem.Enabled = True
			convertToolStripMenuItem.Enabled = True
			saveFingerAsToolStripMenuItem.Enabled = True
		End If
	End Sub

	Private Sub ShowError(ByVal message As String)
		MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
	End Sub

	Private Sub ShowWarning(ByVal message As String)
		MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
	End Sub

	Private Function GetFirstNodeWithTag(Of T)() As TreeNode
		Dim node As TreeNode = treeView.SelectedNode
		If node Is Nothing Then
			Return Nothing
		End If

		Do While node.Parent IsNot Nothing AndAlso Not (TypeOf node.Tag Is T)
			node = node.Parent
		Loop

		Return If(TypeOf node.Tag Is T, node, Nothing)
	End Function

	Private Sub SetTemplate(ByVal record As FIRecord)
		Dim newRecord As Boolean = True
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FIRecord)()
		If recordNode Is Nothing Then
			newRecord = True
		Else
			newRecord = (CType(recordNode.Tag, FIRecord)) IsNot record
		End If

		If newRecord Then
			treeView.BeginUpdate()
			treeView.Nodes.Clear()

			Dim templateNode As New TreeNode((If(_fileName Is Nothing, "Untitled", Path.GetFileName(_fileName))))
			templateNode.Tag = record

			AddFingers(templateNode, record)

			treeView.Nodes.Add(templateNode)
			treeView.SelectedNode = templateNode

			treeView.EndUpdate()
		End If
	End Sub

	Private Sub SetCbeff(ByVal record As CbeffRecord)
		Dim newRecord As Boolean = True
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of CbeffRecord)()
		If recordNode Is Nothing Then
			newRecord = True
		Else
			newRecord = (CType(recordNode.Tag, CbeffRecord)) IsNot record
		End If

		If newRecord Then
			treeView.BeginUpdate()
			treeView.Nodes.Clear()

			Dim root As New TreeNode((If(_fileName Is Nothing, "Untitled", Path.GetFileName(_fileName))))
			root.Tag = record

			AddFingers(root, record)

			treeView.Nodes.Add(root)
			treeView.SelectedNode = root

			treeView.EndUpdate()
		End If
	End Sub

	Private Sub OpenTemplate()
		Dim fileName As String = Nothing
		Dim templ() As Byte = LoadTemplate(fiRecordOpenFileDialog, fileName)
		If templ Is Nothing Then
			Return
		End If

		Dim standard = BdifStandard.Iso
		Dim flags As UInteger = 0
		Dim version = FIRecord.VersionIsoCurrent
		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.Open, standard, flags, version)) Then
			Return
		End If

		Dim oldFilename As String = _fileName
		_fileName = fileName
		Try
			Dim template = New FIRecord(New NBuffer(templ), flags, standard)
			SetTemplate(template)
		Catch ex As Exception
			ShowError("Failed to load template! Reason:" & Constants.vbCrLf & ex.ToString())
			_fileName = oldFilename
		End Try
	End Sub

	Private Sub OpenCbeff()
		Dim fileName As String = Nothing
		Dim templ() As Byte = LoadTemplate(cbeffRecordOpenFileDialog, fileName)
		If templ Is Nothing Then
			Return
		End If

		Dim format As UInteger = 0
		If (Not GetOptions(format)) Then
			Return
		End If

		Dim oldFilename As String = _fileName
		_fileName = fileName
		Try
			Dim template As New CbeffRecord(New NBuffer(templ), format)
			SetCbeff(template)
		Catch ex As Exception
			ShowError("Failed to load template! Reason:" & Constants.vbCrLf & ex.ToString())
			_fileName = oldFilename
		End Try
	End Sub

	Private Sub SaveTemplate(ByVal record As FIRecord)
		SaveTemplate(fiRecordSaveFileDialog, _fileName, record.Save().ToArray())
		If TypeOf treeView.Nodes(0).Tag Is FIRecord Then
			treeView.Nodes(0).Text = Path.GetFileName(fiRecordSaveFileDialog.FileName)
		End If
	End Sub

	Private Function IsRecordFirstVersion(ByVal record As FIRecord) As Boolean
		Return record.Standard = BdifStandard.Ansi AndAlso record.Version = FIRecord.VersionAnsi10 OrElse record.Standard = BdifStandard.Iso AndAlso record.Version = FIRecord.VersionIso10
	End Function

#End Region

#Region "Private form methods"

	Private Sub convertToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles convertToolStripMenuItem.Click
		Dim rootNode As TreeNode = GetFirstNodeWithTag(Of FIRecord)()
		If rootNode Is Nothing Then
			Return
		End If

		Dim fiRecord As FIRecord = TryCast(rootNode.Tag, FIRecord)
		Dim standard As BdifStandard = If((fiRecord.Standard = BdifStandard.Ansi), BdifStandard.Iso, BdifStandard.Ansi)
		Dim version As NVersion = If(standard = BdifStandard.Iso, fiRecord.VersionIsoCurrent, fiRecord.VersionAnsiCurrent)
		Dim flags As UInteger = 0

		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.Convert, standard, flags, version)) Then
			Return
		End If

		fiRecord = ConvertToStandard(fiRecord, standard, flags, version)
		rootNode.Tag = fiRecord

		For i As Integer = 0 To fiRecord.FingerViews.Count - 1
			rootNode.Nodes(i).Tag = fiRecord.FingerViews(i)
		Next i

		OnSelectedItemChanged()
	End Sub

	Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
		AboutBox.Show()
	End Sub

	Private Sub saveFingerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveIngerAsImageToolStripMenuItem.Click
		Dim selected As TreeNode = treeView.SelectedNode
		If (selected IsNot Nothing) AndAlso (selected.Parent IsNot Nothing) AndAlso (selected.Parent.Parent IsNot Nothing) Then
			imageSaveFileDialog.FileName = Nothing
			If imageSaveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				Try
					Dim finger = TryCast(selected.Tag, FirFingerView)
					If finger IsNot Nothing Then
						Using image As NImage = finger.ToNImage()
							image.Save(imageSaveFileDialog.FileName)
						End Using
					End If
				Catch ex As Exception
					ShowError("Failed to save image to file!" & Constants.vbCrLf & "Reason: " & ex.ToString())
				End Try
			End If
		Else
			ShowWarning("Please select finger image")
		End If
	End Sub

	Private Sub removeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeFingerToolStripMenuItem.Click
		Dim selected As TreeNode = treeView.SelectedNode
		If selected Is Nothing Then
			Return
		End If

		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FIRecord)()
		If recordNode Is Nothing Then
			ShowError("Add FIRecord first")
			Return
		End If

		Dim fview As FirFingerView = TryCast(selected.Tag, FirFingerView)
		If fview IsNot Nothing Then
			Dim record As FIRecord = TryCast(recordNode.Tag, FIRecord)
			record.FingerViews.Remove(fview)
			recordNode.Nodes.Remove(selected)
		End If
	End Sub

	Private Sub newToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles newToolStripMenuItem.Click
		Dim standard = BdifStandard.Iso
		Dim flags As UInteger = 0
		Dim version = FIRecord.VersionIsoCurrent
		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.New, standard, flags, version)) Then
			Return
		End If

		Try
			Dim template As New FIRecord(standard, version, flags)
			_fileName = Nothing
			SetTemplate(template)
		Catch ex As Exception
			ShowError("Failed to create template! Reason:" & Constants.vbCrLf & ex.ToString())
		End Try
	End Sub

	Private Sub openToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItem.Click
		OpenTemplate()
		treeView.ExpandAll()
	End Sub

	Private Sub openToolStripMenuItemCbeff_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItemCbeff.Click
		OpenCbeff()
		treeView.ExpandAll()
	End Sub

	Private Sub saveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveFingerAsToolStripMenuItem.Click
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FIRecord)()
		If recordNode Is Nothing Then
			ShowError("Add FIRecord first")
			Return
		End If

		Try
			SaveTemplate(CType(recordNode.Tag, FIRecord))
		Catch ex As Exception
			ShowError(ex.ToString())
		End Try
	End Sub

	Private Sub exitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem.Click
		Close()
	End Sub

	Private Sub treeView_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles treeView.AfterSelect
		OnSelectedItemChanged()
	End Sub

	Private Sub addFingerViewFromImageToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFingerViewFromImageToolStripMenuItem.Click
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FIRecord)()

		If recordNode IsNot Nothing Then
			Try
				imageOpenFileDialog.FileName = Nothing
				Dim imgFromFile As NImage
				If imageOpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					imgFromFile = NImage.FromFile(imageOpenFileDialog.FileName)
				Else
					Return
				End If

				Dim image As NImage = NImage.FromImage(NPixelFormat.Grayscale8U, 0, imgFromFile)
				If image.ResolutionIsAspectRatio OrElse image.HorzResolution < 250 OrElse image.VertResolution < 250 Then
					image.HorzResolution = 500
					image.VertResolution = 500
					image.ResolutionIsAspectRatio = False
				End If

				Dim record As FIRecord = TryCast(recordNode.Tag, FIRecord)

				Using form As New AddFingerForm()
					If form.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
						Return
					End If

					Dim fingerView As New FirFingerView(record.Standard, record.Version)

					fingerView.Position = form.FingerPosition

					If Not IsRecordFirstVersion(record) Then
						fingerView.PixelDepth = 8
						fingerView.HorzImageResolution = CUShort(image.HorzResolution)
						fingerView.HorzScanResolution = CUShort(image.HorzResolution)
						fingerView.VertImageResolution = CUShort(image.VertResolution)
						fingerView.VertScanResolution = CUShort(image.VertResolution)
					Else
						If record.FingerViews.Count = 0 Then
							record.PixelDepth = 8
							record.HorzImageResolution = CUShort(image.HorzResolution)
							record.HorzScanResolution = CUShort(image.HorzResolution)
							record.VertImageResolution = CUShort(image.VertResolution)
							record.VertScanResolution = CUShort(image.VertResolution)
						End If
					End If

					record.FingerViews.Add(fingerView)
					fingerView.SetImage(image)
					AddFingerView(recordNode, fingerView)
				End Using
				treeView.ExpandAll()
			Catch ex As Exception
				ShowError(ex.ToString())
			End Try
		Else
			ShowWarning("Finger must be selected before adding fingerView")
		End If
	End Sub

#End Region
End Class
