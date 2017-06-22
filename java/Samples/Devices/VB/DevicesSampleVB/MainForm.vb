Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports Neurotec.Samples.My
Imports Neurotec.Biometrics
Imports Neurotec.Devices
Imports Neurotec.Devices.Virtual
Imports Neurotec.Gui
Imports Neurotec.Media
Imports System.Collections.Specialized

Partial Public Class MainForm
	Inherits Form
#Region "Private constants"

	Private Const AppName As String = "Device Manager"

#End Region

#Region "Private fields"

	Private _deviceManager As NDeviceManager
	Private ReadOnly _captureForms As New List(Of CaptureForm)()
	Private biometricDevicePositionList As New List(Of NFPosition)()

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		aboutToolStripMenuItem.Text = "&"c + AboutBox.Name

		OnDeviceManagerChanged()
	End Sub

#End Region

#Region "Private methods"

	Private Delegate Sub LogDelegate(ByVal str As String)
	Private Sub Log(ByVal str As String)
		logRichTextBox.AppendText(str)
		logRichTextBox.AppendText(Environment.NewLine)
		logRichTextBox.ScrollToCaret()
	End Sub

	Private Sub Log(ByVal format As String, ByVal ParamArray args() As Object)
		Log(String.Format(format, args))
	End Sub

	Private Sub UpdateDeviceList()
		deviceTreeView.BeginUpdate()
		Try
			deviceTreeView.Nodes.Clear()
			If _deviceManager IsNot Nothing Then
				For Each device As NDevice In _deviceManager.Devices.ToArray()
					If device.Parent Is Nothing Then
						FoundDevice(deviceTreeView.Nodes, device)
					End If
				Next device
			End If
			If deviceTreeView.Nodes.Count <> 0 Then
				deviceTreeView.SelectedNode = deviceTreeView.Nodes(0)
			Else
				Try
					OnSelectedDeviceChanged()
				Catch ex As Exception
					MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			End If
		Finally
			deviceTreeView.EndUpdate()
		End Try
	End Sub

	Private Sub OnDeviceManagerChanged()
		UpdateDeviceList()
		If _deviceManager IsNot Nothing Then
			AddHandler _deviceManager.Devices.CollectionChanged, AddressOf deviceManager_devices_CollectionChanged
		End If
		closeToolStripMenuItem.Enabled = _deviceManager IsNot Nothing
		connectToolStripMenuItem.Enabled = _deviceManager IsNot Nothing
		If _deviceManager Is Nothing Then
			Text = AppName
		Else
			Text = String.Format("{0} (Device types: {1})", AppName, _deviceManager.DeviceTypes)
		End If
	End Sub

	Private Function GetDeviceNode(ByVal device As NDevice, ByVal nodes As TreeNodeCollection) As TreeNode
		For Each node As TreeNode In nodes
			If node.Tag.Equals(device) Then
				Return node
			End If
			Dim subNode As TreeNode = GetDeviceNode(device, node.Nodes)
			If subNode IsNot Nothing Then
				Return subNode
			End If
		Next node
		Return Nothing
	End Function

	Private Function GetDeviceNode(ByVal device As NDevice) As TreeNode
		Return GetDeviceNode(device, deviceTreeView.Nodes)
	End Function

	Private Function CreateDeviceNode(ByVal device As NDevice) As TreeNode
		Dim deviceTreeNode As New TreeNode(device.DisplayName)
		deviceTreeNode.Tag = device
		Return deviceTreeNode
	End Function

	Private Sub FoundDevice(ByVal nodes As TreeNodeCollection, ByVal device As NDevice)
		Log("Found device: {0}", device.Id)
		Dim deviceTreeNode As TreeNode = CreateDeviceNode(device)
		nodes.Add(deviceTreeNode)
		For Each child As NDevice In device.Children
			FoundDevice(deviceTreeNode.Nodes, child)
		Next child
	End Sub

	Private Sub AddDevice(ByVal device As NDevice)
		Log("Added device: {0}", device.Id)
		If GetDeviceNode(device) IsNot Nothing Then	' Device is already added
			Return
		End If
		Dim deviceTreeNode As TreeNode = CreateDeviceNode(device)
		If device.Parent Is Nothing Then
			deviceTreeView.Nodes.Add(deviceTreeNode)
		Else
			Dim parentTreeNode As TreeNode = GetDeviceNode(device.Parent)
			If parentTreeNode IsNot Nothing Then
				parentTreeNode.Nodes.Add(deviceTreeNode)
			End If
		End If
		For Each child As NDevice In device.Children
			Dim childTreeNode As TreeNode = GetDeviceNode(child)
			If childTreeNode IsNot Nothing Then
				If (childTreeNode.Parent Is Nothing) Then
					deviceTreeView.Nodes.Remove(childTreeNode)
				Else
					childTreeNode.Parent.Nodes.Remove(childTreeNode)
				End If
				deviceTreeNode.Nodes.Add(childTreeNode)
			End If
		Next child
	End Sub

	Private Sub RemoveDevice(ByVal device As NDevice)
		For Each cf As CaptureForm In _captureForms
			If cf.Device Is device Then
				cf.WaitForCaptureToFinish()
			End If
		Next cf
		Log("Removed device: {0}", device.Id)
		Dim deviceTreeNode As TreeNode = GetDeviceNode(device)
		Dim isSelected As Boolean = deviceTreeView.SelectedNode Is deviceTreeNode
		Dim childTreeNodes(deviceTreeNode.Nodes.Count - 1) As TreeNode
		For i As Integer = deviceTreeNode.Nodes.Count - 1 To 0 Step -1
			childTreeNodes(i) = deviceTreeNode.Nodes(i)
		Next i
		deviceTreeNode.Nodes.Clear()
		deviceTreeView.Nodes.AddRange(childTreeNodes)
		If deviceTreeNode.Parent Is Nothing Then
			deviceTreeView.Nodes.Remove(deviceTreeNode)
		Else
			deviceTreeNode.Parent.Nodes.Remove(deviceTreeNode)
		End If
		If isSelected Then
			Try
				OnSelectedDeviceChanged()
			Catch ex As Exception
				MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try
		End If
	End Sub

	Private Function GetSelectedDevice() As NDevice
		If deviceTreeView.SelectedNode Is Nothing Then
			Return Nothing
		Else
			Return CType(deviceTreeView.SelectedNode.Tag, NDevice)
		End If
	End Function

	Private Sub SetSelectedDevice(ByVal device As NDevice)
		If device Is Nothing Then
			deviceTreeView.SelectedNode = Nothing
		Else
			For Each node As TreeNode In deviceTreeView.Nodes
				If device.Equals(node.Tag) Then
					deviceTreeView.SelectedNode = node
					Exit For
				End If
			Next node
		End If
	End Sub

	Private Sub OnSelectedDeviceChanged()
		Dim device As NDevice = GetSelectedDevice()
		Dim captureDevice As NCaptureDevice = TryCast(device, NCaptureDevice)
		Dim camera As NCamera = TryCast(device, NCamera)
		Dim microphone As NMicrophone = TryCast(device, NMicrophone)
		Dim biometricDevice As NBiometricDevice = TryCast(device, NBiometricDevice)
		Dim fScanner As NFScanner = TryCast(device, NFScanner)
		Dim irisScanner As NIrisScanner = TryCast(device, NIrisScanner)
		Dim isCaptureDevice As Boolean = camera IsNot Nothing OrElse microphone IsNot Nothing OrElse fScanner IsNot Nothing OrElse irisScanner IsNot Nothing

		disconnectToolStripMenuItem.Enabled = device IsNot Nothing AndAlso device.IsDisconnectable
		showPluginToolStripMenuItem.Enabled = device IsNot Nothing
		If device IsNot Nothing Then
			typeLabel.Text = String.Format("Type: {0}", device.GetType())
		Else
			typeLabel.Text = Nothing
		End If
		devicePropertyGrid.SelectedObject = If(device IsNot Nothing AndAlso device.IsAvailable, device, Nothing)
		biometricDeviceImpressionTypeComboBox.Visible = fScanner IsNot Nothing
		biometricDevicePositionComboBox.Visible = fScanner IsNot Nothing OrElse irisScanner IsNot Nothing
		rlCheckBox.Visible = fScanner IsNot Nothing
		rrCheckBox.Visible = rlCheckBox.Visible
		rmCheckBox.Visible = rrCheckBox.Visible
		riCheckBox.Visible = rmCheckBox.Visible
		rtCheckBox.Visible = riCheckBox.Visible
		ltCheckBox.Visible = rtCheckBox.Visible
		liCheckBox.Visible = ltCheckBox.Visible
		lmCheckBox.Visible = liCheckBox.Visible
		lrCheckBox.Visible = lmCheckBox.Visible
		llCheckBox.Visible = lrCheckBox.Visible
		lblMiliseconds.Visible = isCaptureDevice AndAlso biometricDevice IsNot Nothing
		cbAutomatic.Visible = lblMiliseconds.Visible
		cbUseTimeout.Visible = cbAutomatic.Visible
		tbMiliseconds.Visible = cbUseTimeout.Visible
		deviceCaptureButton.Visible = isCaptureDevice
		cbGatherImages.Visible = deviceCaptureButton.Visible
		customizeFormatButton.Visible = False
		formatsComboBox.Visible = customizeFormatButton.Visible
		endSequenceButton.Visible = biometricDevice IsNot Nothing
		startSequenceButton.Visible = endSequenceButton.Visible

		If fScanner IsNot Nothing Then
			biometricDeviceImpressionTypeComboBox.BeginUpdate()
			biometricDevicePositionComboBox.BeginUpdate()
			biometricDeviceImpressionTypeComboBox.Items.Clear()
			biometricDevicePositionComboBox.Items.Clear()
			biometricDevicePositionList.Clear()
			Try
				If fScanner.IsAvailable Then
					For Each impressionType As NFImpressionType In fScanner.GetSupportedImpressionTypes()
						biometricDeviceImpressionTypeComboBox.Items.Add(impressionType)
					Next impressionType
					If biometricDeviceImpressionTypeComboBox.Items.Count <> 0 Then
						biometricDeviceImpressionTypeComboBox.SelectedIndex = 0
					End If
					For Each position As NFPosition In fScanner.GetSupportedPositions()
						biometricDevicePositionList.Add(position)
					Next position
					ReformDevicePositionComboBox()
				End If
			Finally	' because it may become unavailable in process
				biometricDeviceImpressionTypeComboBox.EndUpdate()
				biometricDevicePositionComboBox.EndUpdate()
			End Try
		ElseIf irisScanner IsNot Nothing Then
			biometricDevicePositionComboBox.BeginUpdate()
			biometricDevicePositionComboBox.Items.Clear()
			Try
				If irisScanner.IsAvailable Then
					For Each position As NEPosition In irisScanner.GetSupportedPositions()
						biometricDevicePositionComboBox.Items.Add(position)
					Next position
					If biometricDevicePositionComboBox.Items.Count <> 0 Then
						biometricDevicePositionComboBox.SelectedIndex = 0
					End If
				End If
			Finally	' because it may become unavailable in process
				biometricDevicePositionComboBox.EndUpdate()
			End Try
		End If

		If captureDevice IsNot Nothing Then
			formatsComboBox.BeginUpdate()
			formatsComboBox.Items.Clear()
			Try
				If captureDevice.IsAvailable Then
					For Each format As NMediaFormat In captureDevice.GetFormats()
						formatsComboBox.Items.Add(format)
					Next format
					Dim currentFormat As NMediaFormat = captureDevice.GetCurrentFormat()
					If currentFormat IsNot Nothing Then
						Dim formatIndex As Integer = formatsComboBox.Items.IndexOf(currentFormat)
						If formatIndex = -1 Then
							formatsComboBox.Items.Add(currentFormat)
							formatsComboBox.SelectedIndex = formatsComboBox.Items.Count - 1
						Else
							formatsComboBox.SelectedIndex = formatIndex
						End If
					End If
				End If
			Finally
				formatsComboBox.EndUpdate()
			End Try
			customizeFormatButton.Visible = True
			formatsComboBox.Visible = customizeFormatButton.Visible
		End If
	End Sub

	Private Sub SetDeviceManager(ByVal value As NDeviceManager)
		If value Is _deviceManager Then
			Return
		End If
		If _deviceManager IsNot Nothing Then
			RemoveHandler _deviceManager.Devices.CollectionChanged, AddressOf deviceManager_devices_CollectionChanged
			_deviceManager.Dispose()
			_deviceManager = Nothing
		End If
		_deviceManager = value
		OnDeviceManagerChanged()
	End Sub

	Private Sub CloseDeviceManager()
		SetDeviceManager(Nothing)
	End Sub

	Private Sub NewDeviceManager()
		Dim settings As Settings = settings.Default
		Dim form As New DeviceManagerForm()
		form.Text = "New Device Manager"
		form.DeviceTypes = settings.DeviceTypes
		form.AutoPlug = settings.AutoPlug
		If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Dim deviceManager = New NDeviceManager()
			deviceManager.DeviceTypes = form.DeviceTypes
			deviceManager.AutoPlug = form.AutoPlug
			deviceManager.Initialize()
			SetDeviceManager(deviceManager)
			settings.DeviceTypes = form.DeviceTypes
			settings.AutoPlug = form.AutoPlug
			settings.Save()
		End If
	End Sub

	Private Sub ReformDevicePositionComboBox()
		Dim selectedImpression As NFImpressionType = CType(biometricDeviceImpressionTypeComboBox.SelectedItem, NFImpressionType)
		biometricDevicePositionComboBox.BeginUpdate()
		biometricDevicePositionComboBox.Items.Clear()
		For Each position As NFPosition In biometricDevicePositionList
			If NBiometricTypes.IsPositionCompatibleWith(position, selectedImpression) Then
				biometricDevicePositionComboBox.Items.Add(position)
			End If
		Next position
		biometricDevicePositionComboBox.EndUpdate()
		If biometricDevicePositionComboBox.Items.Count <> 0 Then
			biometricDevicePositionComboBox.SelectedIndex = 0
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub MainForm_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		AddHandler NCore.ErrorSuppressed, AddressOf NCore_ErrorSuppressed
		NewDeviceManager()
	End Sub

	Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
		For Each form As CaptureForm In _captureForms.ToArray()
			form.Close()
		Next form
		CloseDeviceManager()
		RemoveHandler NCore.ErrorSuppressed, AddressOf NCore_ErrorSuppressed
	End Sub

	Private Sub NCore_ErrorSuppressed(ByVal sender As Object, ByVal ea As ErrorSuppressedEventArgs)
		BeginInvoke(New LogDelegate(AddressOf Log), String.Format("Error suppressed: {0}", ea.Error))
	End Sub

	Private Sub deviceManager_devices_CollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
		BeginInvoke(New Action(Of NotifyCollectionChangedEventArgs)(AddressOf AnonymousMethod1), e)
	End Sub
	Private Sub AnonymousMethod1(ByVal ea As Object)
		Select Case ea.Action
			Case NotifyCollectionChangedAction.Add
				AddDevice(CType(ea.NewItems(0), NDevice))
				If deviceTreeView.SelectedNode Is Nothing Then
					deviceTreeView.SelectedNode = deviceTreeView.Nodes(0)
				End If
			Case NotifyCollectionChangedAction.Remove
				RemoveDevice(CType(ea.OldItems(0), NDevice))
			Case NotifyCollectionChangedAction.Reset
				Log("Refreshing device list...")
				UpdateDeviceList()
		End Select
	End Sub

	Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
		AboutBox.Show()
	End Sub

	Private Sub deviceTreeView_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles deviceTreeView.AfterSelect
		Try
			OnSelectedDeviceChanged()
		Catch ex As Exception
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub showPluginToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles showPluginToolStripMenuItem.Click
		PluginManagerBox.Show(NDeviceManager.PluginManager, GetSelectedDevice().Plugin)
	End Sub

	Private Sub deviceCaptureButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles deviceCaptureButton.Click
		Dim device As NDevice = GetSelectedDevice()
		Dim captureDevice As NCaptureDevice = TryCast(device, NCaptureDevice)
		Dim camera As NCamera = TryCast(device, NCamera)
		Dim microphone As NMicrophone = TryCast(device, NMicrophone)
		Dim biometricDevice As NBiometricDevice = TryCast(device, NBiometricDevice)
		Dim form As CaptureForm
		If captureDevice IsNot Nothing Then
			If formatsComboBox.SelectedItem IsNot Nothing Then
				captureDevice.SetCurrentFormat(CType(formatsComboBox.SelectedItem, NMediaFormat))
			End If
		End If
		If camera IsNot Nothing Then
			Dim cameraForm As New CameraForm()
			form = cameraForm
		ElseIf microphone IsNot Nothing Then
			Dim microphoneForm As New MicrophoneForm()
			form = microphoneForm
		ElseIf biometricDevice IsNot Nothing Then
			Dim fScanner As NFScanner = TryCast(biometricDevice, NFScanner)
			Dim irisScanner As NIrisScanner = TryCast(biometricDevice, NIrisScanner)
			Dim biometricDeviceForm As BiometricDeviceForm
			If fScanner IsNot Nothing Then
				Dim fScannerForm As New FScannerForm()
				fScannerForm.ImpressionType = CType(biometricDeviceImpressionTypeComboBox.SelectedItem, NFImpressionType)
				fScannerForm.Position = CType(biometricDevicePositionComboBox.SelectedItem, NFPosition)
				Dim missingPositions As New List(Of NFPosition)()
				If llCheckBox.Checked Then
					missingPositions.Add(NFPosition.LeftLittleFinger)
				End If
				If lrCheckBox.Checked Then
					missingPositions.Add(NFPosition.LeftRingFinger)
				End If
				If lmCheckBox.Checked Then
					missingPositions.Add(NFPosition.LeftMiddleFinger)
				End If
				If liCheckBox.Checked Then
					missingPositions.Add(NFPosition.LeftIndexFinger)
				End If
				If ltCheckBox.Checked Then
					missingPositions.Add(NFPosition.LeftThumb)
				End If
				If rtCheckBox.Checked Then
					missingPositions.Add(NFPosition.RightThumb)
				End If
				If riCheckBox.Checked Then
					missingPositions.Add(NFPosition.RightIndexFinger)
				End If
				If rmCheckBox.Checked Then
					missingPositions.Add(NFPosition.RightMiddleFinger)
				End If
				If rrCheckBox.Checked Then
					missingPositions.Add(NFPosition.RightRingFinger)
				End If
				If rlCheckBox.Checked Then
					missingPositions.Add(NFPosition.RightLittleFinger)
				End If
				fScannerForm.MissingPositions = missingPositions.ToArray()
				biometricDeviceForm = fScannerForm
			ElseIf irisScanner IsNot Nothing Then
				Dim irisScannerForm As New IrisScannerForm()
				irisScannerForm.Position = CType(biometricDevicePositionComboBox.SelectedItem, NEPosition)
				biometricDeviceForm = irisScannerForm
			Else
				Throw New NotImplementedException()
			End If
			biometricDeviceForm.Automatic = cbAutomatic.Checked
			biometricDeviceForm.Timeout = (If((cbUseTimeout.Checked), CInt(Fix(UInteger.Parse(tbMiliseconds.Text))), -1))
			form = biometricDeviceForm
		Else
			Throw New NotImplementedException()
		End If
		form.GatherImages = cbGatherImages.Checked
		form.Owner = Me
		form.Device = device
		_captureForms.Add(form)
		AddHandler form.FormClosed, AddressOf AnonymousMethod2
		form.Show()
	End Sub
	Private Sub AnonymousMethod2(ByVal fcSender As Object, ByVal fcE As FormClosedEventArgs)
		_captureForms.Remove(CType(fcSender, CaptureForm))
	End Sub

	Private Sub exitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem.Click
		Close()
	End Sub

	Private Sub closeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles closeToolStripMenuItem.Click
		CloseDeviceManager()
	End Sub

	Private Sub newToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles newToolStripMenuItem.Click
		NewDeviceManager()
	End Sub

	Private Sub cbUseTimeout_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbUseTimeout.CheckedChanged
		tbMiliseconds.Enabled = cbUseTimeout.Checked
	End Sub

	Private Sub customizeFormatButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles customizeFormatButton.Click
		Dim selectedFormat As NMediaFormat = TryCast(formatsComboBox.SelectedItem, NMediaFormat)
		If selectedFormat Is Nothing Then
			Dim device As NDevice = GetSelectedDevice()
			If (device.DeviceType And NDeviceType.Camera) = NDeviceType.Camera Then
				selectedFormat = New NVideoFormat()
			ElseIf (device.DeviceType And NDeviceType.Microphone) = NDeviceType.Microphone Then
				selectedFormat = New NAudioFormat()
			Else
				Throw New NotImplementedException()
			End If
		End If
		Dim customFormat As NMediaFormat = CustomizeFormatForm.CustomizeFormat(selectedFormat)
		If customFormat IsNot Nothing Then
			Dim index As Integer = formatsComboBox.Items.IndexOf(customFormat)
			If index = -1 Then
				formatsComboBox.Items.Add(customFormat)
			End If
			formatsComboBox.SelectedItem = customFormat
		End If
	End Sub

	Private Sub startSequenceButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles startSequenceButton.Click
		Dim biometricDevice As NBiometricDevice = CType(GetSelectedDevice(), NBiometricDevice)
		If biometricDevice IsNot Nothing Then
			Try
				biometricDevice.StartSequence()
			Catch ex As Exception
				MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try
		End If
	End Sub

	Private Sub endSequenceButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles endSequenceButton.Click
		Dim biometricDevice As NBiometricDevice = CType(GetSelectedDevice(), NBiometricDevice)
		If biometricDevice IsNot Nothing Then
			Try
				biometricDevice.EndSequence()
			Catch ex As Exception
				MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try
		End If
	End Sub

#End Region

	Private Sub connectToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles connectToolStripMenuItem.Click
		Dim device As NDevice = GetSelectedDevice()
		Try
			Dim form As New ConnectToDeviceForm()
			If device IsNot Nothing Then
				form.SelectedPlugin = device.Plugin
			End If
			If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				Dim newDevice As NDevice = _deviceManager.ConnectToDevice(form.SelectedPlugin, form.Parameters)
				BeginInvoke(New Action(Of NDevice)(AddressOf SetSelectedDevice), newDevice)
			End If
		Catch ex As Exception
			MessageBox.Show(ex.ToString(), "Connect to Device Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub disconnectToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles disconnectToolStripMenuItem.Click
		Dim device As NDevice = GetSelectedDevice()
		If device IsNot Nothing Then
			Try
				_deviceManager.DisconnectFromDevice(device)
			Catch ex As Exception
				MessageBox.Show(ex.ToString(), "Disconnect from Device Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try
		End If
	End Sub

	Private Sub biometricDeviceImpressionTypeComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles biometricDeviceImpressionTypeComboBox.SelectionChangeCommitted
		ReformDevicePositionComboBox()
	End Sub
End Class
