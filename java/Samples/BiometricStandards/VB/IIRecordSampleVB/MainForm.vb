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

	Private Shared ansiBdbFormatPolar As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsIrisPolar)
	Private Shared isoBdbFormatPolar As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsIrisImagePolar)
	Private Shared ansiBdbFormatRectilinear As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsIrisRectilinear)
	Private Shared isoBdbFormatRectilinear As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsIrisImageRectilinear)

#End Region

#Region "Private static methods"

	Private Shared Function GetOptions(ByVal mode As BdifOptionsForm.BdifOptionsFormMode, ByRef standard As BdifStandard, ByRef flags As UInteger, ByRef version As NVersion) As Boolean
		Using form As New IIRecordOptionsForm()
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

	Private Shared Sub AddIrisImage(ByVal node As TreeNode, ByVal image As IirIrisImage)
		Dim prefix As String = "Iris Image {0}"
		Dim index As Integer = node.Nodes.Count + 1
		Dim irisImageNode As New TreeNode(String.Format(prefix, index))
		irisImageNode.Tag = image
		irisImageNode.Expand()
		node.Nodes.Add(irisImageNode)
	End Sub

	Private Shared Sub AddIrises(ByVal node As TreeNode, ByVal record As IIRecord)
		For Each image As IirIrisImage In record.IrisImages
			AddIrisImage(node, image)
		Next image
	End Sub

	Private Shared Sub GetBiometricDataBlock(ByVal node As TreeNode, ByVal record As CbeffRecord)
		If record.BdbBuffer IsNot Nothing AndAlso (record.BdbFormat = ansiBdbFormatPolar OrElse record.BdbFormat = isoBdbFormatPolar OrElse record.BdbFormat = ansiBdbFormatRectilinear OrElse record.BdbFormat = isoBdbFormatRectilinear) Then
			Dim standard As BdifStandard = If(record.BdbFormat = ansiBdbFormatPolar OrElse record.BdbFormat = ansiBdbFormatRectilinear, BdifStandard.Ansi, BdifStandard.Iso)
			Dim iiRecord As New IIRecord(record.BdbBuffer, standard)
			Dim child As New TreeNode("IIRecord")
			child.Tag = iiRecord
			AddIrises(child, iiRecord)
			node.Nodes.Add(child)
		End If
	End Sub

	Private Shared Sub AddIrises(ByVal node As TreeNode, ByVal record As CbeffRecord)
		GetBiometricDataBlock(node, record)

		For Each child As CbeffRecord In record.Records
			AddIrises(node, child)
		Next child
	End Sub

	Private Shared Function ConvertToStandard(ByVal record As IIRecord, ByVal newStandard As BdifStandard, ByVal flags As UInteger, ByVal version As NVersion) As IIRecord
		If record.Standard = newStandard AndAlso record.Flags = flags AndAlso record.Version = version Then
			Return record
		End If

		Return New IIRecord(record, flags, newStandard, version)
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

	Private Sub ShowWarning(ByVal message As String)
		MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
	End Sub

	Private Function GetFirstNodeWithTag(Of T)() As TreeNode
		Dim node As TreeNode = TreeView.SelectedNode
		If node Is Nothing Then
			Return Nothing
		End If

		Do While node.Parent IsNot Nothing AndAlso Not (TypeOf node.Tag Is T)
			node = node.Parent
		Loop

		Return If(TypeOf node.Tag Is T, node, Nothing)
	End Function

	Private Sub SetTemplate(ByVal record As IIRecord, ByVal fileName As String)
		Dim newRecord As Boolean = True
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of IIRecord)()
		If recordNode Is Nothing Then
			newRecord = True
		Else
			newRecord = (CType(recordNode.Tag, IIRecord)) IsNot record
		End If

		If newRecord Then
			_fileName = fileName
			TreeView.BeginUpdate()
			TreeView.Nodes.Clear()

			Dim templateNode As New TreeNode((If(fileName Is Nothing, "Untitled", Path.GetFileName(fileName))))
			templateNode.Tag = record

			AddIrises(templateNode, record)

			TreeView.Nodes.Add(templateNode)
			TreeView.SelectedNode = templateNode

			TreeView.EndUpdate()
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
			TreeView.BeginUpdate()
			TreeView.Nodes.Clear()

			Dim root As New TreeNode((If(fileName Is Nothing, "Untitled", Path.GetFileName(fileName))))
			root.Tag = record

			AddIrises(root, record)

			TreeView.Nodes.Add(root)
			TreeView.SelectedNode = root

			TreeView.EndUpdate()
		End If
	End Sub

	Private Sub OpenTemplate()
		Dim fileName As String = Nothing
		Dim templ() As Byte = LoadTemplate(iiRecordOpenFileDialog, fileName)
		If templ Is Nothing Then
			Return
		End If

		Dim standard As BdifStandard = BdifStandard.Iso
		Dim flags As UInteger = 0
		Dim version = IIRecord.VersionIsoCurrent
		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.Open, standard, flags, version)) Then
			Return
		End If

		Dim template As IIRecord
		Try
			template = New IIRecord(New NBuffer(templ), flags, standard)
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

	Private Sub SaveTemplate(ByVal record As IIRecord)
		SaveTemplate(iiRecordSaveFileDialog, _fileName, record.Save().ToArray())
		If TypeOf TreeView.Nodes(0).Tag Is IIRecord Then
			TreeView.Nodes(0).Text = Path.GetFileName(iiRecordSaveFileDialog.FileName)
		End If
	End Sub

	Private Sub OnSelectedItemChanged()
		Dim selectedNode As TreeNode = TreeView.SelectedNode
		If selectedNode Is Nothing Then
			Return
		End If
		Dim iImage As IirIrisImage = TryCast(TreeView.SelectedNode.Tag, IirIrisImage)
		If iImage IsNot Nothing Then
			iiView.Record = iImage
			PropertyGrid.SelectedObject = iImage
		Else
			iiView.Record = Nothing
			PropertyGrid.SelectedObject = selectedNode.Tag
		End If

		If TypeOf selectedNode.Tag Is CbeffRecord Then
			addIrisImageToolStripMenuItem.Enabled = False
			convertToolStripMenuItem.Enabled = False
			removeToolStripMenuItem.Enabled = False
			saveIrisToolStripMenuItem.Enabled = False
			saveToolStripMenuItem.Enabled = False
		Else
			addIrisImageToolStripMenuItem.Enabled = True
			convertToolStripMenuItem.Enabled = True
			removeToolStripMenuItem.Enabled = True
			saveIrisToolStripMenuItem.Enabled = True
			saveToolStripMenuItem.Enabled = True
		End If
	End Sub

	Private Function IsRecordFirstVersion(ByVal record As IIRecord) As Boolean
		Return record.Standard = BdifStandard.Ansi AndAlso record.Version = IIRecord.VersionAnsi10 OrElse record.Standard = BdifStandard.Iso AndAlso record.Version = IIRecord.VersionIso10
	End Function

#End Region

#Region "Private form methods"

	Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
		AboutBox.Show()
	End Sub

	Private Sub newToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles newToolStripMenuItem.Click
		Dim standard As BdifStandard = BdifStandard.Iso
		Dim flags As UInteger = 0
		Dim version = IIRecord.VersionIsoCurrent

		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.New, standard, flags, version)) Then
			Return
		End If

		Dim record As New IIRecord(standard, version, flags)
		SetTemplate(record, Nothing)
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
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of IIRecord)()
		If recordNode Is Nothing Then
			ShowError("Add IIRecord first")
		End If

		Try
			SaveTemplate(CType(recordNode.Tag, IIRecord))
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

	Private Sub convertToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles convertToolStripMenuItem.Click
		Dim rootNode As TreeNode = GetFirstNodeWithTag(Of IIRecord)()
		If rootNode Is Nothing Then
			Return
		End If

		Dim iiRecord As IIRecord = TryCast(rootNode.Tag, IIRecord)
		Dim standard As BdifStandard = If((iiRecord.Standard = BdifStandard.Ansi), BdifStandard.Iso, BdifStandard.Ansi)
		Dim version = If(standard = BdifStandard.Iso, iiRecord.VersionIsoCurrent, iiRecord.VersionAnsiCurrent)
		Dim flags As UInteger = 0

		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.Convert, standard, flags, version)) Then
			Return
		End If

		iiRecord = ConvertToStandard(iiRecord, standard, flags, version)
		rootNode.Tag = iiRecord

		For i As Integer = 0 To iiRecord.IrisImages.Count - 1
			rootNode.Nodes(i).Tag = iiRecord.IrisImages(i)
		Next i

		OnSelectedItemChanged()
	End Sub

	Private Sub removeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeToolStripMenuItem.Click
		Dim selected As TreeNode = treeView.SelectedNode
		If selected Is Nothing Then
			Return
		End If

		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of IIRecord)()
		If recordNode Is Nothing Then
			ShowError("Add IIRecord first")
			Return
		End If

		Dim fview As IirIrisImage = TryCast(selected.Tag, IirIrisImage)
		If fview IsNot Nothing Then
			Dim record As IIRecord = TryCast(recordNode.Tag, IIRecord)
			record.IrisImages.Remove(fview)
			recordNode.Nodes.Remove(selected)
		End If
	End Sub

	Private Sub saveIrisToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveIrisToolStripMenuItem.Click
		Dim imageNode As TreeNode = GetFirstNodeWithTag(Of IirIrisImage)()
		If imageNode Is Nothing Then
			ShowWarning("Please select an image")
			Return
		End If

		If saveImageFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Dim image As IirIrisImage = TryCast(imageNode.Tag, IirIrisImage)
			If image IsNot Nothing Then
				image.ToNImage().Save(saveImageFileDialog.FileName)
			End If
		End If
	End Sub

	Private Sub addIrisImageToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addIrisImageToolStripMenuItem.Click
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of IIRecord)()
		If recordNode Is Nothing Then
			ShowWarning("IIRecord must be opened! Open or create new IIRecord")
			Return
		End If

		If imageOpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Try
				Using image = NImage.FromFile(imageOpenFileDialog.FileName)
					Dim record As IIRecord = TryCast(recordNode.Tag, IIRecord)

					Using form As New AddIrisForm(record.Version)
						If form.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
							Return
						End If

						Dim irisImage As New IirIrisImage(record.Standard, record.Version)

						irisImage.Position = form.IrisPosition

						If IsRecordFirstVersion(record) AndAlso record.IrisImages.Count = 0 Then
							record.RawImageHeight = CUShort(image.Height)
							record.RawImageWidth = CUShort(image.Width)
							record.IntensityDepth = 8
						End If
						irisImage.SetImage(image)
						record.IrisImages.Add(irisImage)
						AddIrisImage(recordNode, irisImage)
					End Using
				End Using
				treeView.ExpandAll()
			Catch ex As Exception
				ShowError(ex.ToString())
			End Try
		End If
	End Sub

#End Region
End Class
