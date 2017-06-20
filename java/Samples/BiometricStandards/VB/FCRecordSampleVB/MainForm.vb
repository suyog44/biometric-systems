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

	Private Shared ansiBdbFormat As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFaceImage)
	Private Shared isoBdbFormat As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFaceImage)

#End Region

#Region "Private static methods"

	Private Shared Function GetOptions(ByVal mode As BdifOptionsForm.BdifOptionsFormMode, ByRef standard As BdifStandard, ByRef flags As UInteger, ByRef version As NVersion) As Boolean
		Using form As New FCRecordOptionsForm()
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

	Private Shared Sub GetBiometricDataBlock(ByVal node As TreeNode, ByVal record As CbeffRecord)
		If record.BdbBuffer IsNot Nothing AndAlso (record.BdbFormat = ansiBdbFormat OrElse record.BdbFormat = isoBdbFormat) Then
			Dim standard As BdifStandard = If(record.BdbFormat = ansiBdbFormat, BdifStandard.Ansi, BdifStandard.Iso)
			Dim fcRecord As New FCRecord(record.BdbBuffer, standard)
			Dim child As New TreeNode("FCRecord")
			child.Tag = fcRecord
			AddFaceImages(child, fcRecord)
			node.Nodes.Add(child)
		End If
	End Sub

	Private Shared Sub AddFaceImages(ByVal node As TreeNode, ByVal record As CbeffRecord)
		GetBiometricDataBlock(node, record)

		For Each child As CbeffRecord In record.Records
			AddFaceImages(node, child)
		Next child
	End Sub

	Private Shared Function AddFaceImage(ByVal node As TreeNode, ByVal faceImage As FcrFaceImage) As TreeNode
		Dim index As Integer = node.Nodes.Count + 1
		Dim recordNode As New TreeNode("FaceImage " & index)
		recordNode.Tag = faceImage
		node.Nodes.Add(recordNode)
		Return recordNode
	End Function

	Private Shared Sub AddFaceImages(ByVal node As TreeNode, ByVal record As FCRecord)
		For Each faceImage As FcrFaceImage In record.FaceImages
			AddFaceImage(node, faceImage)
		Next faceImage
	End Sub

	Private Shared Function ConvertToStandard(ByVal record As FCRecord, ByVal newStandard As BdifStandard, ByVal flags As UInteger, ByVal version As NVersion) As FCRecord
		If record.Standard = newStandard AndAlso record.Flags = flags AndAlso record.Version = version Then
			Return record
		End If

		Return New FCRecord(record, flags, newStandard, version)
	End Function

#End Region

#Region "Private fields"

	Private _fileName As String

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		rawImageOpenFileDialog.Filter = String.Format("All Supported Files ({0};{1})|{0};{1}|JPEG Files ({0})|{0}|JPEG 2000 Files ({1})|{1}|All Files (*.*)|*.*", NImageFormat.Jpeg.FileFilter, NImageFormat.Jpeg2K.FileFilter)
		saveImageFileDialog.Filter = NImages.GetSaveFileFilterString()
		aboutToolStripMenuItem.Text = "&"c + AboutBox.Name

		OnSelectedItemChanged()
	End Sub

#End Region

#Region "Private methods"

	Private Sub ShowError(ByVal message As String)
		MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

	Private Sub SetTemplate(ByVal record As FCRecord, ByVal fileName As String)
		Dim newRecord As Boolean = True
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FCRecord)()
		If recordNode Is Nothing Then
			newRecord = True
		Else
			newRecord = (CType(recordNode.Tag, FCRecord)) IsNot record
		End If

		If newRecord Then
			_fileName = fileName
			treeView.BeginUpdate()
			treeView.Nodes.Clear()

			Dim templateNode As New TreeNode((If(fileName Is Nothing, "Untitled", Path.GetFileName(fileName))))
			templateNode.Tag = record

			AddFaceImages(templateNode, record)

			treeView.Nodes.Add(templateNode)
			treeView.SelectedNode = templateNode

			treeView.EndUpdate()
		End If
	End Sub

	Private Sub SetCbeff(ByVal record As CbeffRecord, ByVal fileName As String)
		Dim newRecord As Boolean = True
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of CbeffRecord)()
		If recordNode Is Nothing Then
			newRecord = True
		Else
			newRecord = (CType(recordNode.Tag, CbeffRecord)) IsNot record
		End If

		If newRecord Then
			_fileName = fileName
			treeView.BeginUpdate()
			treeView.Nodes.Clear()

			Dim root As New TreeNode((If(fileName Is Nothing, "Untitled", Path.GetFileName(fileName))))
			root.Tag = record

			AddFaceImages(root, record)

			treeView.Nodes.Add(root)
			treeView.SelectedNode = root

			treeView.EndUpdate()
		End If
	End Sub

	Private Sub NewTemplate()
		Dim standard As BdifStandard = BdifStandard.Iso
		Dim flags As UInteger = 0
		Dim version As NVersion = FCRecord.VersionIsoCurrent

		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.New, standard, flags, version)) Then
			Return
		End If

		NewTemplate(standard, flags, version)
	End Sub

	Private Sub NewTemplate(ByVal standard As BdifStandard, ByVal flags As UInteger, ByVal version As NVersion)
		Dim record As New FCRecord(standard, version, flags)
		SetTemplate(record, Nothing)
	End Sub

	Private Sub OpenTemplate()
		Dim fileName As String = Nothing
		Dim templ() As Byte = LoadTemplate(fcRecordOpenFileDialog, fileName)
		If templ Is Nothing Then
			Return
		End If

		Dim standard As BdifStandard = BdifStandard.Iso
		Dim flags As UInteger = 0
		Dim version As NVersion = FCRecord.VersionIsoCurrent
		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.Open, standard, flags, version)) Then
			Return
		End If

		Dim template As FCRecord
		Try
			template = New FCRecord(New NBuffer(templ), flags, standard)
		Catch ex As Exception
			ShowError("Failed to load template! Reason:" & Constants.vbCrLf & ex.ToString())
			Return
		End Try
		SetTemplate(template, fileName)
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

		Dim template As CbeffRecord
		Try
			template = New CbeffRecord(New NBuffer(templ), format)
		Catch ex As Exception
			ShowError("Failed to load template! Reason:" & Constants.vbCrLf & ex.ToString())
			Return
		End Try

		SetCbeff(template, fileName)
	End Sub

	Private Sub SaveTemplate(ByVal record As FCRecord)
		SaveTemplate(fcRecordSaveFileDialog, _fileName, record.Save().ToArray())
		If TypeOf treeView.Nodes(0).Tag Is FCRecord Then
			treeView.Nodes(0).Text = Path.GetFileName(fcRecordSaveFileDialog.FileName)
		End If
	End Sub

	Private Sub OnSelectedItemChanged()
		Dim selected As TreeNode = treeView.SelectedNode

		If selected IsNot Nothing Then
			Dim faceImage As FcrFaceImage = TryCast(selected.Tag, FcrFaceImage)
			fcView.Record = faceImage

			If faceImage IsNot Nothing Then
				propertyGrid.SelectedObject = faceImage
			Else
				propertyGrid.SelectedObject = If(treeView.SelectedNode Is Nothing, Nothing, treeView.SelectedNode.Tag)
			End If

			If TypeOf selected.Tag Is CbeffRecord Then
				addFaceFromImageToolStripMenuItem.Enabled = False
				addFaceFromRawToolStripMenuItem.Enabled = False
				saveFaceAsDataToolStripMenuItem.Enabled = False
				saveFaceToolStripMenuItem.Enabled = False
				convertToolStripMenuItem.Enabled = False
				removeToolStripMenuItem.Enabled = False
				saveToolStripMenuItem.Enabled = False
			Else
				addFaceFromImageToolStripMenuItem.Enabled = True
				addFaceFromRawToolStripMenuItem.Enabled = True
				saveFaceAsDataToolStripMenuItem.Enabled = True
				saveFaceToolStripMenuItem.Enabled = True
				convertToolStripMenuItem.Enabled = True
				removeToolStripMenuItem.Enabled = True
				saveToolStripMenuItem.Enabled = True
			End If
		End If
	End Sub

#End Region

#Region "Private form methods"

	Private Sub saveFaceAsDataToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveFaceAsDataToolStripMenuItem.Click
		Dim selected As TreeNode = treeView.SelectedNode
		If (selected IsNot Nothing) AndAlso (selected.Parent IsNot Nothing) Then
			Dim img As FcrFaceImage = TryCast(selected.Tag, FcrFaceImage)
			If img IsNot Nothing Then
				saveRawFileDialog.Filter = If((img.ImageDataType = FcrImageDataType.Jpeg), String.Format("JPEG Files ({0})|{0}", NImageFormat.Jpeg.FileFilter), String.Format("JPEG 2000 Files ({0})|{0}", NImageFormat.Jpeg2K.FileFilter))

				If saveRawFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					Try
						File.WriteAllBytes(saveRawFileDialog.FileName, img.ImageData.ToArray())
					Catch ex As Exception
						ShowError("Failed to save face to data." & Constants.vbCrLf & "Reason: " & ex.ToString())
						Return
					End Try
				End If
			End If
		End If
	End Sub

	Private Sub saveFaceToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveFaceToolStripMenuItem.Click
		Dim selected As TreeNode = treeView.SelectedNode
		If (selected IsNot Nothing) AndAlso (selected.Parent IsNot Nothing) Then
			If saveImageFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				Try
					Dim img As FcrFaceImage = TryCast(selected.Tag, FcrFaceImage)
					If img IsNot Nothing Then
						Using image As NImage = img.ToNImage()
							image.Save(saveImageFileDialog.FileName)
						End Using
					End If
				Catch ex As Exception
					ShowError("Failed to save image to file!" & Constants.vbCrLf & "Reason: " & ex.ToString())
					Return
				End Try
			End If
		End If
	End Sub

	Private Sub addFaceFromImageToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFaceFromImageToolStripMenuItem.Click
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FCRecord)()
		If recordNode Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbCrLf & "Add FCRecord first")
			Return
		End If

		If imageOpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Dim imageType As FcrFaceImageType
			Dim dataType As FcrImageDataType

			Using form As New AddFaceImageForm()
				Dim extension As String = Path.GetExtension(imageOpenFileDialog.FileName)
				dataType = If((NImageFormat.Jpeg2K.FileFilter).Contains(extension), FcrImageDataType.Jpeg2000, FcrImageDataType.Jpeg)
				form.ImageDataType = dataType
				If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					imageType = form.FaceImageType
					dataType = form.ImageDataType
				Else
					Return
				End If
			End Using

			Try
				Using image As NImage = NImage.FromFile(imageOpenFileDialog.FileName)
					Dim record As FCRecord = TryCast(recordNode.Tag, FCRecord)

					Dim img As New FcrFaceImage(record.Standard, record.Version)
					img.FaceImageType = imageType
					img.ImageDataType = dataType
					img.SetImage(image)
					record.FaceImages.Add(img)
					Dim node As TreeNode = AddFaceImage(recordNode, img)
					treeView.SelectedNode = node
				End Using
			Catch ex As Exception
				ShowError("Failed to add image. Reason:" & Constants.vbCrLf & ex.ToString())
				Return
			End Try
			treeView.ExpandAll()
		End If
	End Sub

	Private Sub addFaceFromRawToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFaceFromRawToolStripMenuItem.Click
		If rawImageOpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Dim width, height As UShort
			Dim imageType As FcrFaceImageType
			Dim dataType As FcrImageDataType
			Dim imageColorSpace As FcrImageColorSpace
			Dim vendorImageColorSpace As Byte

			Using form As New RawFaceImageOptionsForm()
				Dim extension As String = Path.GetExtension(rawImageOpenFileDialog.FileName)
				dataType = If((NImageFormat.Jpeg2K.FileFilter).Contains(extension), FcrImageDataType.Jpeg2000, FcrImageDataType.Jpeg)
				form.ImageDataType = dataType

				If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					imageType = form.FaceImageType
					dataType = form.ImageDataType
					imageColorSpace = form.ImageColorSpace
					width = form.ImageWidth
					height = form.ImageHeight
					vendorImageColorSpace = form.VendorColorSpace
				Else
					Return
				End If
			End Using

			Try
				Dim rawImage() As Byte = File.ReadAllBytes(rawImageOpenFileDialog.FileName)

				Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FCRecord)()
				Dim record As FCRecord = TryCast(recordNode.Tag, FCRecord)

				Dim img As New FcrFaceImage(record.Standard, record.Version)
				img.FaceImageType = imageType
				img.ImageDataType = dataType
				img.Width = width
				img.Height = height
				img.SetImageColorSpace(imageColorSpace, vendorImageColorSpace)
				img.ImageData = New NBuffer(rawImage)
				record.FaceImages.Add(img)
				Dim node As TreeNode = AddFaceImage(treeView.Nodes(0), img)
				treeView.SelectedNode = node
			Catch ex As Exception
				ShowError("Failed to add image. Reason:" & Constants.vbCrLf & ex.ToString())
				Return
			End Try
		End If
	End Sub

	Private Sub removeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeToolStripMenuItem.Click
		Dim selected As TreeNode = treeView.SelectedNode
		If selected Is Nothing Then
			Return
		End If

		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FCRecord)()
		If recordNode Is Nothing Then
			ShowError("Add FMRecord first")
			Return
		End If

		Dim fview As FcrFaceImage = TryCast(selected.Tag, FcrFaceImage)
		If fview IsNot Nothing Then
			Dim record As FCRecord = TryCast(recordNode.Tag, FCRecord)
			record.FaceImages.Remove(fview)
			recordNode.Nodes.Remove(selected)
		End If
	End Sub

	Private Sub convertToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles convertToolStripMenuItem.Click
		Dim rootNode As TreeNode = GetFirstNodeWithTag(Of FCRecord)()
		If rootNode Is Nothing Then
			Return
		End If

		Dim fcRecord As FCRecord = TryCast(rootNode.Tag, FCRecord)
		Dim standard As BdifStandard = If((fcRecord.Standard = BdifStandard.Ansi), BdifStandard.Iso, BdifStandard.Ansi)
		Dim flags As UInteger = 0
		Dim version As NVersion = If(standard = BdifStandard.Iso, fcRecord.VersionIsoCurrent, fcRecord.VersionAnsiCurrent)

		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.Convert, standard, flags, version)) Then
			Return
		End If

		fcRecord = ConvertToStandard(fcRecord, standard, flags, version)
		rootNode.Tag = fcRecord

		For i As Integer = 0 To fcRecord.FaceImages.Count - 1
			rootNode.Nodes(i).Tag = fcRecord.FaceImages(i)
		Next i

		OnSelectedItemChanged()
	End Sub

	Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
		AboutBox.Show()
	End Sub

	Private Sub newToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles newToolStripMenuItem.Click
		NewTemplate()
	End Sub

	Private Sub openToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItem.Click
		OpenTemplate()
		treeView.ExpandAll()
	End Sub

	Private Sub openToolStripMenuItemCbeff_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItemCbeff.Click
		OpenCbeff()
		treeView.ExpandAll()
	End Sub

	Private Sub saveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveToolStripMenuItem.Click
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FCRecord)()
		If recordNode Is Nothing Then
			ShowError("Add FCRecord first")
			Return
		End If

		Try
			SaveTemplate(CType(recordNode.Tag, FCRecord))
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

#End Region
End Class
