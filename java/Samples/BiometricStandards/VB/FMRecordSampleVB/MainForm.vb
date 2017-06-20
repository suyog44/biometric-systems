Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Biometrics.Standards.Gui
Imports Neurotec.Gui
Imports Neurotec.IO

Partial Public Class MainForm
	Inherits Form
#Region "Private static fields"

	Private Shared ansiBdbFormat As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerMinutiaeX)
	Private Shared isoBdbFormat As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.FederalOfficeForInformationSecurityTRBiometricsXmlFinger10)

#End Region

#Region "Private static methods"

	Private Shared Function GetOptions(ByVal mode As BdifOptionsForm.BdifOptionsFormMode, ByRef standard As BdifStandard, ByRef flags As UInteger, ByRef version As NVersion) As Boolean
		Using form As New FMRecordOptionsForm()
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

	Private Shared Function ConvertToStandard(ByVal record As FMRecord, ByVal newStandard As BdifStandard, ByVal flags As UInteger, ByVal version As NVersion) As FMRecord
		If record.Standard = newStandard AndAlso record.Flags = flags AndAlso record.Version = version Then
			Return record
		End If

		Return New FMRecord(record, flags, newStandard, version)
	End Function

	Private Shared Sub AddFingerView(ByVal node As TreeNode, ByVal fview As FmrFingerView)
		Dim index As Integer = node.Nodes.Count + 1
		Dim fingerViewNode As New TreeNode("Finger view " & index)
		fingerViewNode.Tag = fview
		node.Nodes.Add(fingerViewNode)
	End Sub

	Private Shared Sub AddFingers(ByVal node As TreeNode, ByVal record As FMRecord)
		For Each finger As FmrFingerView In record.FingerViews
			AddFingerView(node, finger)
		Next finger
	End Sub

	Private Shared Sub GetBiometricDataBlock(ByVal node As TreeNode, ByVal record As CbeffRecord)
		If record.BdbBuffer IsNot Nothing AndAlso (record.BdbFormat = ansiBdbFormat OrElse record.BdbFormat = isoBdbFormat) Then
			Dim standard As BdifStandard = If(record.BdbFormat = ansiBdbFormat, BdifStandard.Ansi, BdifStandard.Iso)
			Dim fmRecord As New FMRecord(record.BdbBuffer, standard)
			Dim child As New TreeNode("FMRecord")
			child.Tag = fmRecord
			AddFingers(child, fmRecord)
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

		AddHandler FMView.SelectedCoreIndexChanged, AddressOf OnDetailSelected
		AddHandler FMView.SelectedDeltaIndexChanged, AddressOf OnDetailSelected
		AddHandler FMView.SelectedMinutiaIndexChanged, AddressOf OnDetailSelected
		AddHandler FMView.MouseUp, AddressOf OnMouseUp

		aboutToolStripMenuItem.Text = "&"c + AboutBox.Name
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

	Private Overloads Sub OnMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FMRecord)()
		If recordNode Is Nothing Then
			Return
		End If

		Dim fingerView As FmrFingerView = TryCast(TreeView.SelectedNode.Tag, FmrFingerView)
		Dim index As Integer = FMView.SelectedMinutiaIndex
		If index <> -1 Then
			PropertyGrid.SelectedObject = New SelectedFmrMinutia(fingerView, index)
		Else
			index = FMView.SelectedDeltaIndex
			If index <> -1 Then
				PropertyGrid.SelectedObject = New SelectedFmrDelta(fingerView, index)
			Else
				index = FMView.SelectedCoreIndex
				If index <> -1 Then
					PropertyGrid.SelectedObject = New SelectedFmrCore(fingerView, index)
				End If
			End If
		End If
	End Sub

	Private Sub OnDetailSelected(ByVal sender As Object, ByVal e As EventArgs)
		If FMView.SelectedCoreIndex = FMView.SelectedDeltaIndex AndAlso FMView.SelectedDeltaIndex = FMView.SelectedMinutiaIndex Then
			btnDeleteFeature.Enabled = False
			deleteSelectedToolStripMenuItem.Enabled = False
		Else
			btnDeleteFeature.Enabled = True
			deleteSelectedToolStripMenuItem.Enabled = True
		End If
		Dim fingerView As FmrFingerView = TryCast(TreeView.SelectedNode.Tag, FmrFingerView)
		Dim index As Integer = FMView.SelectedDeltaIndex
		If index <> -1 Then
			PropertyGrid.SelectedObject = New SelectedFmrDelta(fingerView, index)
			Return
		End If

		index = FMView.SelectedCoreIndex
		If index <> -1 Then
			PropertyGrid.SelectedObject = New SelectedFmrCore(fingerView, index)
			Return
		End If

		index = FMView.SelectedMinutiaIndex
		If index <> -1 Then
			PropertyGrid.SelectedObject = New SelectedFmrMinutia(fingerView, index)
			Return
		End If

		PropertyGrid.SelectedObject = fingerView
	End Sub

	Private Sub OnFeatureAdded(ByVal sender As Object, ByVal e As EventArgs)
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FMRecord)()
		If recordNode Is Nothing Then
			Return
		End If

		Dim record As FMRecord = TryCast(recordNode.Tag, FMRecord)

		Dim selected As TreeNode = TreeView.SelectedNode
		Dim fView As FmrFingerView = TryCast(selected.Tag, FmrFingerView)
		If fView Is Nothing Then
			Return
		End If
		Dim args As FMView.AddFeaturesTool.FeatureAddCompletedEventArgs = CType(e, FMView.AddFeaturesTool.FeatureAddCompletedEventArgs)
		If args.Start.X < 0 OrElse args.Start.Y < 0 Then
			Return
		End If

		Dim x As UShort = CUShort(args.Start.X)
		Dim y As UShort = CUShort(args.Start.Y)
		Dim form As New AddFeatureForm()
		form.StartPosition = FormStartPosition.CenterParent

		Dim isVersion2 = isRecordSecondVersion(record)
		Dim sizeX = If(isVersion2, record.SizeX, fView.SizeX)
		Dim sizeY = If(isVersion2, record.SizeY, fView.SizeY)
		If x >= sizeX OrElse y >= sizeY OrElse x < 0 OrElse y < 0 OrElse (args.Start.X = args.End.X AndAlso args.Start.Y = args.End.Y) Then
			Return
		End If

		If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Dim w As Integer = args.End.X - args.Start.X
			Dim h As Integer = args.End.Y - args.Start.Y
			Dim angle As Double = -(Math.Atan(CSng(h) / w) + (If(w < 0, Math.PI, 0.0)))
			If Double.IsNaN(angle) Then
				Return
			End If

			Select Case form.cbFeature.SelectedIndex
				Case 0
					fView.Minutiae.Add(New FmrMinutia(x, y, form.MinutiaType, angle, record.Standard))
				Case 1
					fView.Cores.Add(New FmrCore(x, y, angle, record.Standard))
				Case Else
					fView.Deltas.Add(New FmrDelta(x, y))
			End Select
		End If
	End Sub

	Private Sub OnSelectedItemChanged()
		Dim selectedNode As TreeNode = TreeView.SelectedNode
		If selectedNode Is Nothing Then
			Return
		End If

		Dim fingerView As FmrFingerView = TryCast(selectedNode.Tag, FmrFingerView)
		If fingerView IsNot Nothing Then
			FMView.Template = fingerView
			PropertyGrid.SelectedObject = fingerView
			ToolStrip.Enabled = True
		Else
			ToolStrip.Enabled = False
			FMView.Template = Nothing
			PropertyGrid.SelectedObject = selectedNode.Tag
		End If

		If TypeOf selectedNode.Tag Is CbeffRecord Then
			addFingerViewToolStripMenuItem.Enabled = False
			removeFingerToolStripMenuItem.Enabled = False
			convertToolStripMenuItem.Enabled = False
			saveAsToolStripMenuItem.Enabled = False
		Else
			addFingerViewToolStripMenuItem.Enabled = True
			removeFingerToolStripMenuItem.Enabled = True
			convertToolStripMenuItem.Enabled = True
			saveAsToolStripMenuItem.Enabled = True
		End If
	End Sub

	Private Sub SetTemplate(ByVal record As FMRecord, ByVal fileName As String)
		Dim newRecord As Boolean = True
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FMRecord)()
		If recordNode Is Nothing Then
			newRecord = True
		Else
			newRecord = (CType(recordNode.Tag, FMRecord)) IsNot record
		End If

		If newRecord Then
			_fileName = fileName
			TreeView.BeginUpdate()
			TreeView.Nodes.Clear()

			Dim templateNode As New TreeNode((If(fileName Is Nothing, "Untitled", Path.GetFileName(fileName))))
			templateNode.Tag = record

			AddFingers(templateNode, record)

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

			AddFingers(root, record)

			TreeView.Nodes.Add(root)
			TreeView.SelectedNode = root

			TreeView.EndUpdate()
		End If
	End Sub

	Private Sub OpenTemplate()
		Dim fileName As String = Nothing
		Dim templ() As Byte = LoadTemplate(fmRecordOpenFileDialog, fileName)
		If templ Is Nothing Then
			Return
		End If

		Dim standard As BdifStandard = BdifStandard.Iso
		Dim flags As UInteger = 0
		Dim version = FMRecord.VersionIsoCurrent
		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.Open, standard, flags, version)) Then
			Return
		End If

		Dim template As FMRecord
		Try
			template = New FMRecord(New NBuffer(templ), flags, standard)
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
			ShowError("Failed to load CbeffRecord! Reason:" & Constants.vbCrLf & ex.ToString())
			Return
		End Try
		SetCbeff(template, fileName)
	End Sub

	Private Sub SaveTemplate(ByVal record As FMRecord)
		SaveTemplate(fmRecordSaveFileDialog, _fileName, record.Save().ToArray())
		If TypeOf treeView.Nodes(0).Tag Is FMRecord Then
			treeView.Nodes(0).Text = Path.GetFileName(fmRecordSaveFileDialog.FileName)
		End If
	End Sub

	Private Function IsRecordSecondVersion(ByVal record As FMRecord) As Boolean
		Return record.Standard = BdifStandard.Ansi AndAlso record.Version = FMRecord.VersionAnsi20 OrElse record.Standard = BdifStandard.Iso AndAlso record.Version = FMRecord.VersionIso20
	End Function

#End Region

#Region "Private form methods"

	Private Sub InvalidateFMView()
		fmView.Invalidate()
		propertyGrid.Refresh()
	End Sub

	Private Sub btnPointerTool_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPointerTool.Click
		pointerToolToolStripMenuItem_Click(sender, e)
	End Sub

	Private Sub btnAddFeatureTool_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddFeatureTool.Click
		addFeatureToolToolStripMenuItem_Click(sender, e)
	End Sub

	Private Sub convertToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles convertToolStripMenuItem.Click
		Dim rootNode As TreeNode = GetFirstNodeWithTag(Of FMRecord)()
		If rootNode Is Nothing Then
			Return
		End If

		Dim fmRecord As FMRecord = TryCast(rootNode.Tag, FMRecord)
		Dim standard As BdifStandard = If((fmRecord.Standard = BdifStandard.Ansi), BdifStandard.Iso, BdifStandard.Ansi)
		Dim version = If(standard = BdifStandard.Iso, fmRecord.VersionIsoCurrent, fmRecord.VersionAnsiCurrent)
		Dim flags As UInteger = 0

		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.Convert, standard, flags, version)) Then
			Return
		End If

		fmRecord = ConvertToStandard(fmRecord, standard, flags, version)
		rootNode.Tag = fmRecord

		For i As Integer = 0 To fmRecord.FingerViews.Count - 1
			rootNode.Nodes(i).Tag = fmRecord.FingerViews(i)
		Next i

		OnSelectedItemChanged()
	End Sub

	Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
		AboutBox.Show()
	End Sub

	Private Sub removeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeFingerToolStripMenuItem.Click
		Dim selected As TreeNode = treeView.SelectedNode
		If selected Is Nothing Then
			Return
		End If

		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FMRecord)()
		If recordNode Is Nothing Then
			ShowError("Add FMRecord first")
			Return
		End If

		Dim fview As FmrFingerView = TryCast(selected.Tag, FmrFingerView)
		If fview IsNot Nothing Then
			Dim record As FMRecord = TryCast(recordNode.Tag, FMRecord)
			record.FingerViews.Remove(fview)
			recordNode.Nodes.Remove(selected)
		End If
	End Sub

	Private Sub openToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItem.Click
		OpenTemplate()
		treeView.ExpandAll()
	End Sub

	Private Sub openToolStripMenuItemCbeff_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItemCbeff.Click
		OpenCbeff()
		treeView.ExpandAll()
	End Sub

	Private Sub saveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveAsToolStripMenuItem.Click
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FMRecord)()
		If recordNode Is Nothing Then
			ShowError("Add FMRecord first")
			Return
		End If

		Try
			SaveTemplate(CType(recordNode.Tag, FMRecord))
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

	Private Sub pointerToolToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pointerToolToolStripMenuItem.Click
		If fmView.ActiveTool IsNot Nothing AndAlso TypeOf fmView.ActiveTool Is FMView.PointerTool Then
			pointerToolToolStripMenuItem.Checked = False
			btnPointerTool.Checked = False
			fmView.ActiveTool = Nothing
			Return
		End If

		fmView.ActiveTool = New FMView.PointerTool()
		pointerToolToolStripMenuItem.Checked = True
		addFeatureToolToolStripMenuItem.Checked = False
		btnPointerTool.Checked = True
		btnAddFeatureTool.Checked = False
	End Sub

	Private Sub addFeatureToolToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFeatureToolToolStripMenuItem.Click
		If fmView.ActiveTool IsNot Nothing AndAlso TypeOf fmView.ActiveTool Is FMView.AddFeaturesTool Then
			addFeatureToolToolStripMenuItem.Checked = False
			fmView.ActiveTool = Nothing
			btnAddFeatureTool.Checked = False
			Return
		End If
		Dim tool As New FMView.AddFeaturesTool()
		AddHandler tool.FeatureAddCompleted, AddressOf OnFeatureAdded
		fmView.ActiveTool = tool
		pointerToolToolStripMenuItem.Checked = False
		addFeatureToolToolStripMenuItem.Checked = True
		btnAddFeatureTool.Checked = True
		btnPointerTool.Checked = False
	End Sub

	Private Sub newFMRecordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles newFMRecordToolStripMenuItem.Click
		Dim standard = BdifStandard.Iso
		Dim flags As UInteger = 0
		Dim version = FMRecord.VersionIsoCurrent
		If (Not GetOptions(BdifOptionsForm.BdifOptionsFormMode.New, standard, flags, version)) Then
			Return
		End If

		Try
			Dim template As New FMRecord(standard, version, flags)
			_fileName = Nothing
			SetTemplate(template, Nothing)
		Catch ex As Exception
			ShowError("Failed to create template! Reason:" & Constants.vbCrLf & ex.ToString())
		End Try
	End Sub

	Private Sub addFingerViewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFingerViewToolStripMenuItem.Click
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of FMRecord)()

		If recordNode Is Nothing Then
			ShowWarning("FMRecord has to be selected before adding finger view.")
			Return
		End If

		Dim record As FMRecord = TryCast(recordNode.Tag, FMRecord)

		Try
			Dim fingerView As New FmrFingerView(record.Standard, record.Version)

			If Not IsRecordSecondVersion(record) Then
				Using form = New NewFingerViewForm()
					If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
						fingerView.SizeX = form.SizeX
						fingerView.SizeY = form.SizeY
						fingerView.VertImageResolution = form.VertResolution
						fingerView.HorzImageResolution = form.HorzResolution
					Else
						Return
					End If
				End Using
			Else
				If record.FingerViews.Count = 0 Then
					Using form = New NewFingerViewForm()
						If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
							record.SizeX = form.SizeX
							record.SizeY = form.SizeY
							record.ResolutionX = form.HorzResolution
							record.ResolutionY = form.VertResolution
						Else
							Return
						End If
					End Using
				End If
			End If

			record.FingerViews.Add(fingerView)
			AddFingerView(recordNode, fingerView)
			treeView.ExpandAll()
		Catch ex As Exception
			ShowError(ex.ToString())
		End Try
	End Sub

	Private Sub btnDeleteSelected_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteFeature.Click
		Dim fingerView As FmrFingerView = TryCast(treeView.SelectedNode.Tag, FmrFingerView)
		If fingerView Is Nothing Then
			Return
		End If

		Dim index As Integer = fmView.SelectedMinutiaIndex
		If index <> -1 Then
			fmView.SelectedMinutiaIndex = -1
			fingerView.Minutiae.RemoveAt(index)
			Return
		End If

		index = fmView.SelectedCoreIndex
		If index <> -1 Then
			fmView.SelectedCoreIndex = -1
			fingerView.Cores.RemoveAt(index)
			Return
		End If

		index = fmView.SelectedDeltaIndex
		If index <> -1 Then
			fmView.SelectedDeltaIndex = -1
			fingerView.Deltas.RemoveAt(index)
			Return
		End If
	End Sub

	Private Sub deleteSelectedToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles deleteSelectedToolStripMenuItem.Click
		btnDeleteSelected_Click(sender, e)
	End Sub

#End Region
End Class
