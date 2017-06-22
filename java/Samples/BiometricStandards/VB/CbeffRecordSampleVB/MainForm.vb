Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Gui
Imports Neurotec.IO
Imports Neurotec.Licensing

Partial Public Class MainForm
	Inherits Form

#Region "Private static fields"
	Private Shared ansiFCRecord As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFaceImage)
	Private Shared isoFCRecord As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFaceImage)
	Private Shared ansiFIRecord As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerImage)
	Private Shared isoFIRecord As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFingerImage)
	Private Shared ansiFMRecord As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerMinutiaeX)
	Private Shared isoFMRecord As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.FederalOfficeForInformationSecurityTRBiometricsXmlFinger10)
	Private Shared ansiIIRecordPolar As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsIrisPolar)
	Private Shared isoIIRecordPolar As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsIrisImagePolar)
	Private Shared ansiIIRecordRectilinear As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IncitsTCM1Biometrics, CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsIrisRectilinear)
	Private Shared isoIIRecordRectilinear As UInteger = BdifTypes.MakeFormat(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics, CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsIrisImageRectilinear)
#End Region

#Region "Private fields"

	Private _filename As String

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		aboutToolStripMenuItem.Text = "&"c + AboutBox.Name
	End Sub

#End Region

#Region "Private methods"

	Private Sub ShowError(ByVal message As String)
		MessageBox.Show(message, Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
	End Sub

	Private Sub ShowWarning(ByVal message As String)
		MessageBox.Show(message, Text & ": Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
	End Sub

	Private Function ShowQuestion(ByVal message As String) As DialogResult
		Return MessageBox.Show(message, Text & ": Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
	End Function

	Private Sub ShowInformation(ByVal message As String)
		MessageBox.Show(message, Text & ": Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

	Private Function LoadRecord(ByVal openFileDialog As OpenFileDialog, <System.Runtime.InteropServices.Out()> ByRef fileName As String, <System.Runtime.InteropServices.Out()> ByRef buffer() As Byte) As Boolean
		openFileDialog.FileName = Nothing
		If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			fileName = openFileDialog.FileName
			buffer = File.ReadAllBytes(openFileDialog.FileName)
			Return True
		End If
		fileName = Nothing
		buffer = Nothing
		Return False
	End Function

	Private Function GetOptions(ByRef format As UInteger) As Boolean
		Using form As New CbeffRecordOptionsForm()
			If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				format = form.PatronFormat
			Else
				Return False
			End If
		End Using
		Return True
	End Function

	Private Function GetOptions(ByRef format As UInteger, ByRef standard As BdifStandard) As Boolean
		Using form As New AddRecordOptionsForm()
			If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				format = form.PatronFormat
				standard = form.Standard
			Else
				Return False
			End If
		End Using
		Return True
	End Function

#Region "Read xxRecord data"

	Private Sub ReadFMRecord(ByVal node As TreeNode, ByVal record As FMRecord)
		For Each finger As FmrFingerView In record.FingerViews
			Dim child As New TreeNode(finger.FingerPosition.ToString())
			child.Tag = finger
			node.Nodes.Add(child)
		Next finger
	End Sub

	Private Sub ReadFCRecord(ByVal node As TreeNode, ByVal record As FCRecord)
		Dim index As Integer
		For Each face As FcrFaceImage In record.FaceImages
			index = node.Nodes.Count + 1
			Dim child As New TreeNode("Face " & index)
			child.Tag = face
			node.Nodes.Add(child)
		Next face
	End Sub

	Private Sub ReadFIRecord(ByVal node As TreeNode, ByVal record As FIRecord)
		For Each finger As FirFingerView In record.FingerViews
			Dim child As New TreeNode(finger.Position.ToString())
			child.Tag = finger
			node.Nodes.Add(child)
		Next finger
	End Sub

	Private Sub ReadIIRecord(ByVal node As TreeNode, ByVal record As IIRecord)
		For Each iris As IirIrisImage In record.IrisImages
			Dim child As New TreeNode(iris.Position.ToString())
			child.Tag = iris
			node.Nodes.Add(child)
		Next iris
	End Sub

#End Region

	Private Sub ExtractData(ByVal node As TreeNode, ByVal record As CbeffRecord)
		If record.BdbFormat = ansiFCRecord Then
			'FCRecordAnsi
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Faces")) Then
				Return
			End If
			Dim fcRecord As New FCRecord(record.BdbBuffer, BdifStandard.Ansi)
			Dim child As New TreeNode("FCRecord")
			child.Tag = fcRecord
			ReadFCRecord(child, fcRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = isoFCRecord Then
			'FCRecordIso
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Faces")) Then
				Return
			End If
			Dim fcRecord As New FCRecord(record.BdbBuffer, BdifStandard.Iso)
			Dim child As New TreeNode("FCRecord")
			child.Tag = fcRecord
			ReadFCRecord(child, fcRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = ansiFIRecord Then
			'FIRecordAnsi
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Fingers")) Then
				Return
			End If
			Dim fiRecord As New FIRecord(record.BdbBuffer, BdifStandard.Ansi)
			Dim child As New TreeNode("FIRecord")
			child.Tag = fiRecord
			ReadFIRecord(child, fiRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = isoFIRecord Then
			'FIRecordIso
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Fingers")) Then
				Return
			End If
			Dim fiRecord As New FIRecord(record.BdbBuffer, BdifStandard.Iso)
			Dim child As New TreeNode("FIRecord")
			child.Tag = fiRecord
			ReadFIRecord(child, fiRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = ansiFMRecord Then
			'FMRecordAnsi
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Fingers")) Then
				Return
			End If
			Dim fmRecord As New FMRecord(record.BdbBuffer, BdifStandard.Ansi)
			Dim child As New TreeNode("FMRecord")
			child.Tag = fmRecord
			ReadFMRecord(child, fmRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = isoFMRecord Then
			'FMRecordIso
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Fingers")) Then
				Return
			End If
			Dim fmRecord As New FMRecord(record.BdbBuffer, BdifStandard.Iso)
			Dim child As New TreeNode("FMRecord")
			child.Tag = fmRecord
			ReadFMRecord(child, fmRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = ansiIIRecordPolar Then
			'IIRecordAnsiPolar
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Irises")) Then
				Return
			End If
			Dim iiRecord As New IIRecord(record.BdbBuffer, BdifStandard.Ansi)
			Dim child As New TreeNode("IIRecord")
			child.Tag = iiRecord
			ReadIIRecord(child, iiRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = isoIIRecordPolar Then
			'IIRecordIsoPolar
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Irises")) Then
				Return
			End If
			Dim iiRecord As New IIRecord(record.BdbBuffer, BdifStandard.Iso)
			Dim child As New TreeNode("IIRecord")
			child.Tag = iiRecord
			ReadIIRecord(child, iiRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = ansiIIRecordRectilinear Then
			'IIRecordAnsiRectilinear
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Irises")) Then
				Return
			End If
			Dim iiRecord As New IIRecord(record.BdbBuffer, BdifStandard.Ansi)
			Dim child As New TreeNode("IIRecord")
			child.Tag = iiRecord
			ReadIIRecord(child, iiRecord)
			node.Nodes.Add(child)
		ElseIf record.BdbFormat = isoIIRecordRectilinear Then
			'IIRecordIsoRectilinear
			If (Not NLicense.IsComponentActivated("Biometrics.Standards.Irises")) Then
				Return
			End If
			Dim iiRecord As New IIRecord(record.BdbBuffer, BdifStandard.Iso)
			Dim child As New TreeNode("IIRecord")
			child.Tag = iiRecord
			ReadIIRecord(child, iiRecord)
			node.Nodes.Add(child)
		End If
	End Sub

	Private Sub ReadDataBlock(ByVal node As TreeNode, ByVal record As CbeffRecord)
		If record.BdbBuffer IsNot Nothing Then
			Try
				ExtractData(node, record)
			Catch ex As Exception
				ShowWarning("One of Biometric Data Blocks could not be opened." & Constants.vbCrLf & ex.Message)
			End Try
		End If
	End Sub

	Private Sub ReadRecord(ByVal parentNode As TreeNode, ByVal parentRecord As CbeffRecord)
		ReadDataBlock(parentNode, parentRecord)

		For Each childRecord As CbeffRecord In parentRecord.Records
			Dim childNode As New TreeNode("CbeffRecord")
			ReadRecord(childNode, childRecord)
			childNode.Tag = childRecord
			parentNode.Nodes.Add(childNode)
		Next childRecord
	End Sub

	Private Sub SetRecord(ByVal record As CbeffRecord)
		treeView.BeginUpdate()
		treeView.Nodes.Clear()

		Dim rootNode As New TreeNode(If(_filename Is Nothing, "Unknown", Path.GetFileName(_filename)))

		ReadRecord(rootNode, record)

		rootNode.Tag = record
		treeView.Nodes.Add(rootNode)
		treeView.SelectedNode = rootNode

		treeView.EndUpdate()
		treeView.ExpandAll()
	End Sub

	Private Sub OpenRecord()
		Dim fileName As String = Nothing
		Dim buffer() As Byte = Nothing
		If (Not LoadRecord(cbeffRecordOpenFileDialog, fileName, buffer)) Then
			Return
		End If

		Dim format As UInteger = 0
		If (Not GetOptions(format)) Then
			Return
		End If

		Dim oldFilename As String = _filename
		_filename = fileName
		Dim record As CbeffRecord = Nothing
		Try
			record = New CbeffRecord(New NBuffer(buffer), format)
			SetRecord(record)
		Catch ex As Exception
			_filename = oldFilename
			ShowError("CbeffRecord could not be opened." & Constants.vbCrLf & ex.ToString())
		End Try
	End Sub

	Private Sub SaveAsRecord()
		If treeView.Nodes.Count = 0 Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim record As CbeffRecord = TryCast(treeView.Nodes(0).Tag, CbeffRecord)
		If record Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		cbeffRecordSaveFileDialog.FileName = _filename
		If cbeffRecordSaveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Dim fileName As String

			fileName = cbeffRecordSaveFileDialog.FileName

			Try
				Dim nBuffer As NBuffer = record.Save()
				Dim buffer() As Byte = nBuffer.ToArray()
				File.WriteAllBytes(fileName, buffer)
			Catch ex As Exception
				ShowError("CbeffRecord could not be saved." & Constants.vbCrLf & ex.ToString())
			End Try

			_filename = fileName

			treeView.Nodes(0).Text = Path.GetFileName(fileName)
		End If
	End Sub

	Private Sub SaveSelectedRecord()
		Dim selected As TreeNode = treeView.SelectedNode
		If selected Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Try
			If TypeOf selected.Tag Is CbeffRecord Then
				cbeffRecordSaveFileDialog.FileName = _filename
				If cbeffRecordSaveFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If

				Dim nBuffer As NBuffer = (CType(selected.Tag, CbeffRecord)).Save()
				Dim buffer() As Byte = nBuffer.ToArray()
				File.WriteAllBytes(cbeffRecordSaveFileDialog.FileName, buffer)
				_filename = cbeffRecordSaveFileDialog.FileName
			ElseIf TypeOf selected.Tag Is FCRecord Then
				fcRecordSaveFileDialog.FileName = _filename
				If fcRecordSaveFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If

				Dim nBuffer As NBuffer = (CType(selected.Tag, FCRecord)).Save()
				Dim buffer() As Byte = nBuffer.ToArray()
				File.WriteAllBytes(fcRecordSaveFileDialog.FileName, buffer)
				_filename = fcRecordSaveFileDialog.FileName
			ElseIf TypeOf selected.Tag Is FIRecord Then
				fiRecordSaveFileDialog.FileName = _filename
				If fiRecordSaveFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If

				Dim nBuffer As NBuffer = (CType(selected.Tag, FIRecord)).Save()
				Dim buffer() As Byte = nBuffer.ToArray()
				File.WriteAllBytes(fiRecordSaveFileDialog.FileName, buffer)
				_filename = fiRecordSaveFileDialog.FileName
			ElseIf TypeOf selected.Tag Is FMRecord Then
				fcRecordSaveFileDialog.FileName = _filename
				If fcRecordSaveFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If

				Dim nBuffer As NBuffer = (CType(selected.Tag, FMRecord)).Save()
				Dim buffer() As Byte = nBuffer.ToArray()
				File.WriteAllBytes(fcRecordSaveFileDialog.FileName, buffer)
				_filename = fcRecordSaveFileDialog.FileName
			ElseIf TypeOf selected.Tag Is IIRecord Then
				iiRecordSaveFileDialog.FileName = _filename
				If iiRecordSaveFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If

				Dim nBuffer As NBuffer = (CType(selected.Tag, IIRecord)).Save()
				Dim buffer() As Byte = nBuffer.ToArray()
				File.WriteAllBytes(iiRecordSaveFileDialog.FileName, buffer)
				_filename = iiRecordSaveFileDialog.FileName
			End If
		Catch ex As Exception
			ShowError("Selected item could not be saved." & Constants.vbLf + Constants.vbCr + ex.ToString())
		End Try
	End Sub

	Private Sub SelectedItemChanged()
		Dim selectedNode As TreeNode = treeView.SelectedNode
		If selectedNode Is Nothing Then
			Return
		End If

		propertyGrid.SelectedObject = selectedNode.Tag

		Dim record As CbeffRecord = TryCast(selectedNode.Tag, CbeffRecord)
		If record IsNot Nothing Then
			If record.BdbBuffer IsNot Nothing Then
				InsertAfterToolStripMenuItem.Enabled = False
			Else
				InsertAfterToolStripMenuItem.Enabled = True
			End If

			addCbeffRecordToolStripMenuItem.Enabled = True
			addFromFileToolStripMenuItem.Enabled = True
			InsertBeforeToolStripMenuItem.Enabled = True
			addFCRecordToolStripMenuItem.Enabled = True
			addFIRecordToolStripMenuItem.Enabled = True
			addFMRecordToolStripMenuItem.Enabled = True
			addIIRecordToolStripMenuItem.Enabled = True
			removeBranchToolStripMenuItem.Enabled = True
		Else
			addCbeffRecordToolStripMenuItem.Enabled = False
			addFromFileToolStripMenuItem.Enabled = False
			InsertBeforeToolStripMenuItem.Enabled = False
			InsertAfterToolStripMenuItem.Enabled = False
			addFCRecordToolStripMenuItem.Enabled = False
			addFIRecordToolStripMenuItem.Enabled = False
			addFMRecordToolStripMenuItem.Enabled = False
			addIIRecordToolStripMenuItem.Enabled = False

			If TypeOf selectedNode.Tag Is FCRecord OrElse TypeOf selectedNode.Tag Is FIRecord OrElse TypeOf selectedNode.Tag Is FMRecord OrElse TypeOf selectedNode.Tag Is IIRecord Then
				removeBranchToolStripMenuItem.Enabled = True
			Else
				removeBranchToolStripMenuItem.Enabled = False
			End If
		End If
	End Sub

	Private Sub RemoveSelectedBranch()
		Dim selected As TreeNode = treeView.SelectedNode
		If selected Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim root As CbeffRecord = TryCast(treeView.Nodes(0).Tag, CbeffRecord)
		If root Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		If selected.Parent Is Nothing Then
			treeView.Nodes.Clear()
		Else
			Try
				If TypeOf selected.Tag Is CbeffRecord Then
					Dim parentRecord As CbeffRecord = TryCast(selected.Parent.Tag, CbeffRecord)
					Dim record As CbeffRecord = TryCast(selected.Tag, CbeffRecord)
					parentRecord.Records.Remove(record)
				Else
					Dim parentRecord As CbeffRecord = TryCast(selected.Parent.Tag, CbeffRecord)
					parentRecord.BdbBuffer = Nothing
					parentRecord.BdbFormat = 0
					parentRecord.BdbIndex = Nothing
				End If
				SetRecord(root)
			Catch ex As Exception
				ShowError("Selected branch could not be removed." & Constants.vbLf + Constants.vbCr + ex.ToString())
			End Try
		End If
	End Sub

	Private Sub AddRecord()
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of CbeffRecord)()
		If recordNode Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim root As CbeffRecord = TryCast(treeView.Nodes(0).Tag, CbeffRecord)
		If root Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim format As UInteger = 0
		If (Not GetOptions(format)) Then
			Return
		End If

		Dim record As CbeffRecord = TryCast(recordNode.Tag, CbeffRecord)

		Try
			Dim newRecord As New CbeffRecord(format)
			record.Records.Add(newRecord)

			SetRecord(root)
		Catch ex As Exception
			ShowError("CbeffRecord could not be added." & Constants.vbLf + Constants.vbCr + ex.ToString())
		End Try
	End Sub

	Private Sub AddRecordFromFile(Of T)(ByVal dialog As OpenFileDialog)
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of CbeffRecord)()
		If recordNode Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim root As CbeffRecord = TryCast(treeView.Nodes(0).Tag, CbeffRecord)
		If root Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim fileName As String = Nothing
		Dim buffer() As Byte = Nothing
		If (Not LoadRecord(dialog, fileName, buffer)) Then
			Return
		End If

		Dim format As UInteger = 0
		Dim standard As BdifStandard = BdifStandard.Ansi
		If GetType(T) Is GetType(CbeffRecord) Then
			If (Not GetOptions(format)) Then
				Return
			End If
		Else
			If (Not GetOptions(format, standard)) Then
				Return
			End If
		End If

		Dim record As CbeffRecord = TryCast(recordNode.Tag, CbeffRecord)

		Try
			If GetType(T) Is GetType(CbeffRecord) Then
				Dim newRecord As New CbeffRecord(New NBuffer(buffer), format)
				record.Records.Add(newRecord)
			ElseIf GetType(T) Is GetType(FCRecord) Then
				Dim fcRecord As New FCRecord(buffer, standard)
				Dim newRecord As New CbeffRecord(fcRecord, format)
				record.Records.Add(newRecord)
			ElseIf GetType(T) Is GetType(FIRecord) Then
				Dim fiRecord As New FIRecord(buffer, standard)
				Dim newRecord As New CbeffRecord(fiRecord, format)
				record.Records.Add(newRecord)
			ElseIf GetType(T) Is GetType(FMRecord) Then
				Dim fmRecord As New FMRecord(buffer, standard)
				Dim newRecord As New CbeffRecord(fmRecord, format)
				record.Records.Add(newRecord)
			ElseIf GetType(T) Is GetType(IIRecord) Then
				Dim iiRecord As New IIRecord(buffer, standard)
				Dim newRecord As New CbeffRecord(iiRecord, format)
				record.Records.Add(newRecord)
			Else
				Throw New ArgumentException("Operation is not supported." & Constants.vbLf + Constants.vbCr)
			End If
			SetRecord(root)
		Catch ex As Exception
			ShowError(GetType(T).ToString() & " could not be added." & Constants.vbLf + Constants.vbCr + ex.ToString())
		End Try
	End Sub

	Private Sub NewRecord()
		Dim format As UInteger = 0
		If (Not GetOptions(format)) Then
			Return
		End If

		Dim oldFilename As String = _filename
		_filename = Nothing
		Try
			Dim record As New CbeffRecord(format)
			SetRecord(record)
		Catch ex As Exception
			_filename = oldFilename
			ShowError("CbeffRecord could not be created." & Constants.vbLf + Constants.vbCr + ex.ToString())
		End Try
	End Sub

	Private Sub InsertBeforeSelectedRecord()
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of CbeffRecord)()
		If recordNode Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim format As UInteger = 0
		If (Not GetOptions(format)) Then
			Return
		End If

		Dim record As CbeffRecord = TryCast(recordNode.Tag, CbeffRecord)

		Dim parentNode As TreeNode = recordNode.Parent

		Try
			Dim newRecord As New CbeffRecord(format)
			If parentNode IsNot Nothing Then
				Dim parentRecord As CbeffRecord = TryCast(parentNode.Tag, CbeffRecord)
				parentRecord.Records.Remove(record)
				Dim copyRecord As New CbeffRecord(record.Save(), record.PatronFormat)
				newRecord.Records.Add(copyRecord)
				parentRecord.Records.Add(newRecord)

				SetRecord(parentRecord)
			Else
				newRecord.Records.Add(record)
				SetRecord(newRecord)
			End If
		Catch ex As Exception
			ShowError("CbeffRecord could not be inserted." & Constants.vbLf + Constants.vbCr + ex.ToString())
		End Try
	End Sub

	Private Sub InsertAfterSelectedRecord()
		Dim recordNode As TreeNode = GetFirstNodeWithTag(Of CbeffRecord)()
		If recordNode Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim root As CbeffRecord = TryCast(treeView.Nodes(0).Tag, CbeffRecord)
		If root Is Nothing Then
			ShowError("Operation is not supported." & Constants.vbLf + Constants.vbCr & "Add CbeffRecord first.")
			Return
		End If

		Dim format As UInteger = 0
		If (Not GetOptions(format)) Then
			Return
		End If

		Dim record As CbeffRecord = TryCast(recordNode.Tag, CbeffRecord)

		Try
			Dim newRecord As New CbeffRecord(format)
			For Each childRecord As CbeffRecord In record.Records
				Dim rec As New CbeffRecord(childRecord.Save(), childRecord.PatronFormat)
				newRecord.Records.Add(rec)
			Next childRecord
			record.Records.Clear()
			record.Records.Add(newRecord)

			SetRecord(root)
		Catch ex As Exception
			ShowError("CbeffRecord could not be inserted." & Constants.vbCrLf & ex.ToString())
		End Try
	End Sub

#End Region

#Region "Private form methods"

	Private Sub openToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openToolStripMenuItem.Click
		OpenRecord()
	End Sub

	Private Sub saveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveAsToolStripMenuItem.Click
		SaveAsRecord()
	End Sub

	Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
		AboutBox.Show()
	End Sub

	Private Sub treeView_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles treeView.AfterSelect
		SelectedItemChanged()
	End Sub

	Private Sub exitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem.Click
		Close()
	End Sub

	Private Sub removeBranchToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeBranchToolStripMenuItem.Click
		RemoveSelectedBranch()
	End Sub

	Private Sub addCbeffRecordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addCbeffRecordToolStripMenuItem.Click
		AddRecord()
	End Sub

	Private Sub newToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles newToolStripMenuItem.Click
		NewRecord()
	End Sub

	Private Sub saveSelectedStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles saveSelectedStripMenuItem.Click
		SaveSelectedRecord()
	End Sub

	Private Sub InsertBeforeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles InsertBeforeToolStripMenuItem.Click
		InsertBeforeSelectedRecord()
	End Sub

	Private Sub InsertAfterToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles InsertAfterToolStripMenuItem.Click
		InsertAfterSelectedRecord()
	End Sub

	Private Sub AddFromFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFromFileToolStripMenuItem.Click
		AddRecordFromFile(Of CbeffRecord)(cbeffRecordOpenFileDialog)
	End Sub

	Private Sub addFCRecordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFCRecordToolStripMenuItem.Click
		AddRecordFromFile(Of FCRecord)(fcRecordOpenFileDialog)
	End Sub

	Private Sub addFIRecordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFIRecordToolStripMenuItem.Click
		AddRecordFromFile(Of FIRecord)(fiRecordOpenFileDialog)
	End Sub

	Private Sub addFMRecordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addFMRecordToolStripMenuItem.Click
		AddRecordFromFile(Of FMRecord)(fmRecordOpenFileDialog)
	End Sub

	Private Sub addIIRecordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addIIRecordToolStripMenuItem.Click
		AddRecordFromFile(Of IIRecord)(iiRecordOpenFileDialog)
	End Sub

#End Region
End Class
