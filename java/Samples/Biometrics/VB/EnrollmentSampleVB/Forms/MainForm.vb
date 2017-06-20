Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Devices
Imports Neurotec.Images
Imports Neurotec.IO
Imports Neurotec.Licensing
Imports Neurotec.Samples.Controls

Namespace Forms
	Partial Public Class MainForm
		Inherits Form
		#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
		End Sub

		#End Region

		#Region "Private fields"

		Private ReadOnly _slaps() As NFPosition = { NFPosition.PlainLeftFourFingers, NFPosition.PlainRightFourFingers, NFPosition.PlainThumbs }

		Private ReadOnly _fingers() As NFPosition = { NFPosition.LeftLittle, NFPosition.LeftRing, NFPosition.LeftMiddle, NFPosition.LeftIndex, NFPosition.LeftThumb, NFPosition.RightThumb, NFPosition.RightIndex, NFPosition.RightMiddle, NFPosition.RightRing, NFPosition.RightLittle }

		Private _canCaptureSlaps As Boolean
		Private _canCaptureRolled As Boolean
		Private _captureStarted As Boolean
		Private _newSubject As Boolean = True
		Private _captureForm As FingerCaptureForm
		Private _exit As Boolean

		Private _model As DataModel
		Private _biometricClient As NBiometricClient

		#End Region

		#Region "Private form events"

		Private Sub MainFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			If DesignMode Then
				Return
			End If
			BeginInvoke(New MethodInvoker(AddressOf OnMainFormLoaded))
		End Sub

		Private Sub FingerSelectorFingerClick(ByVal sender As Object, ByVal e As FingerSelector.FingerClickArgs) Handles fSelector.FingerClick
			Dim missing = New List(Of NFPosition)(fSelector.MissingPositions)
			Dim position As NFPosition = e.Position
			If missing.Contains(position) Then
				missing.Remove(position)
			Else
				missing.Add(position)
			End If

			fSelector.MissingPositions = missing.ToArray()
		End Sub

		Private Sub BtnStartCapturingClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStartCapturing.Click
			If _model.Subject Is Nothing Then
				_model.Subject = New NSubject()
				AddHandler _model.Subject.Fingers.CollectionChanged, AddressOf OnFingersCollectionChanged
				CreateFingers(_model.Subject)
				If _model.Subject.Fingers.Count = 0 Then
					Utilities.ShowWarning(Me, "No fingers selected for capturing")
					Return
				End If
			End If
			_captureStarted = True
			_newSubject = False
			EnableControls(False)

			_captureForm = New FingerCaptureForm()
			_captureForm.BiometricClient = _biometricClient
			_captureForm.Subject = _model.Subject
			AddHandler _captureForm.FormClosed, AddressOf CaptureFormFormClosed
			_captureForm.Show(Me)
		End Sub

		Private Sub OnFingersCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
			Select Case e.Action
				Case NotifyCollectionChangedAction.Add
					BeginInvoke(New Action(Of NFinger())(AddressOf OnFingersAdded), CObj(e.NewItems.Cast(Of NFinger)().ToArray()))
					Exit Select
				Case NotifyCollectionChangedAction.Remove
					BeginInvoke(New Action(Of NFinger())(AddressOf OnFingersRemoved), CObj(e.OldItems.Cast(Of NFinger)().ToArray()))
					Exit Select
				Case NotifyCollectionChangedAction.Reset
					BeginInvoke(New Action(AddressOf OnFingersCleared))
					Exit Select
				Case Else
			End Select
		End Sub
		Private Sub OnFingersAdded(ByVal newItems As Object)
			Dim view As NFingerView = Nothing
			For Each f As NFinger In newItems
				view = GetView(f)
				If view IsNot Nothing Then
					view.Finger = f
				End If
			Next f
		End Sub
		Private Sub OnFingersRemoved(ByVal oldItems As Object)
			Dim view As NFingerView = Nothing
			For Each f As NFinger In oldItems
				view = GetView(f)
				If view IsNot Nothing Then
					view.Finger = Nothing
				End If
			Next f
		End Sub
		Private Sub OnFingersCleared()
			Dim view As NFingerView = Nothing
			For Each position As NFPosition In _slaps
				view = GetView(position, False)
				view.Finger = Nothing
			Next position
			For Each position As NFPosition In _fingers
				view = GetView(position, False)
				view.Finger = Nothing
				view = GetView(position, True)
				view.Finger = Nothing
			Next position
		End Sub

		Private Sub MainFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			SaveSettings()

			If _captureForm IsNot Nothing Then
				Text = "Enrollment Sample: Closing ..."
				e.Cancel = True
				_exit = True
				_captureForm.Close()
			End If
		End Sub

		Private Sub CaptureFormFormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
			_captureForm.Dispose()
			_captureForm = Nothing

			_captureStarted = False
			EnableControls(True)

			If _exit Then
				Close()
			End If
		End Sub

		Private Sub CaptureComboBoxCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbCapturePlainFingers.CheckedChanged, chbCaptureSlaps.CheckedChanged, chbCaptureRolled.CheckedChanged
			If sender Is chbCapturePlainFingers AndAlso (Not chbCapturePlainFingers.Checked) Then
				chbCaptureSlaps.Checked = False
			End If
			If sender Is chbCaptureSlaps AndAlso chbCaptureSlaps.Checked Then
				chbCapturePlainFingers.Checked = True
			End If

			btnStartCapturing.Enabled = chbCaptureRolled.Checked OrElse chbCapturePlainFingers.Checked
		End Sub

		Private Sub ChbShowOriginalCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowOriginal.CheckedChanged, chbShowOriginalRolled.CheckedChanged
			Dim target As CheckBox = CType(sender, CheckBox)
			Dim other As CheckBox = If(target Is chbShowOriginal, chbShowOriginalRolled, chbShowOriginal)
			If other.Checked = target.Checked Then
				Return
			End If
			other.Checked = target.Checked

			Dim view As NFingerView
			Dim shown As ShownImage = If(target.Checked, ShownImage.Original, ShownImage.Result)
			For Each position As NFPosition In _fingers
				view = CType(GetView(position, False), NFingerView)
				view.ShownImage = shown

				view = CType(GetView(position, True), NFingerView)
				view.ShownImage = shown
			Next position
		End Sub

		Private Sub ViewMouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles nfvLeftThumb.MouseEnter, nfvLeftIndex.MouseEnter, nfvLeftMiddle.MouseEnter, nfvLeftRing.MouseEnter, nfvLeftLittle.MouseEnter, nfvRightThumb.MouseEnter, nfvRightIndex.MouseEnter, nfvRightMiddle.MouseEnter, nfvRightRing.MouseEnter, nfvRightLittle.MouseEnter, nfvLeftThumbRolled.MouseEnter, nfvLeftIndexRolled.MouseEnter, nfvLeftMiddleRolled.MouseEnter, nfvLeftRingRolled.MouseEnter, nfvLeftLittleRolled.MouseEnter, nfvRightThumbRolled.MouseEnter, nfvRightIndexRolled.MouseEnter, nfvRightMiddleRolled.MouseEnter, nfvRightRingRolled.MouseEnter, nfvRightLittleRolled.MouseEnter
			Dim view As NFingerView = TryCast(sender, NFingerView)
			If view Is Nothing OrElse _captureStarted Then
				Return
			End If
			If toolStripViewControls.Parent IsNot Nothing Then
				toolStripViewControls.Parent.Controls.Remove(toolStripViewControls)
			End If
			If view.Finger IsNot Nothing Then
				Dim finger As NFrictionRidge = view.Finger
				If finger IsNot _biometricClient.CurrentBiometric Then
					If finger.Image IsNot Nothing Then
						Dim canSaveRecord As Boolean = False
						If NBiometricTypes.IsPositionSingleFinger(finger.Position) Then
							canSaveRecord = finger.Objects.ToArray().Any(Function(x) x.Template IsNot Nothing)
						End If
						tsbSaveRecord.Visible = canSaveRecord
						toolStripViewControls.Tag = finger
						view.Parent.Controls.Add(toolStripViewControls)
						toolStripViewControls.Visible = True
						toolStripViewControls.BringToFront()
					End If
				End If
			End If
		End Sub

		Private Sub ViewMouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripViewControls.MouseLeave, nfvLeftThumb.MouseLeave, nfvLeftIndex.MouseLeave, nfvLeftMiddle.MouseLeave, nfvLeftRing.MouseLeave, nfvLeftLittle.MouseLeave, nfvRightThumb.MouseLeave, nfvRightIndex.MouseLeave, nfvRightMiddle.MouseLeave, nfvRightRing.MouseLeave, nfvRightLittle.MouseLeave, nfvLeftThumbRolled.MouseLeave, nfvLeftIndexRolled.MouseLeave, nfvLeftMiddleRolled.MouseLeave, nfvLeftRingRolled.MouseLeave, nfvLeftLittleRolled.MouseLeave, nfvRightThumbRolled.MouseLeave, nfvRightIndexRolled.MouseLeave, nfvRightMiddleRolled.MouseLeave, nfvRightRingRolled.MouseLeave, nfvRightLittleRolled.MouseLeave
			If (Not toolStripViewControls.Bounds.Contains(toolStripViewControls.PointToClient(MousePosition))) Then
				toolStripViewControls.Visible = False
				toolStripViewControls.Tag = Nothing
			End If
		End Sub

		Private Sub TsbSaveImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbSaveImage.Click
			Dim fileName As String = String.Empty
			Dim finger As NFinger = CType(toolStripViewControls.Tag, NFinger)
			saveFileDialog.Filter = NImages.GetSaveFileFilterString()
			saveFileDialog.FileName = fileName
			If finger IsNot Nothing Then
				Dim originalImage As Boolean = chbShowOriginal.Checked AndAlso NBiometricTypes.IsPositionSingleFinger(finger.Position)
				Dim isRolled As Boolean = NBiometricTypes.IsImpressionTypeRolled(finger.ImpressionType)
				Using image As NImage = If(originalImage, finger.GetImage(False), finger.GetBinarizedImage(False))
					fileName = String.Format("{0}{1}{2}", finger.Position, If(isRolled, "_Rolled", String.Empty), If(originalImage, String.Empty, "_Binarized"))
					If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
						image.Save(saveFileDialog.FileName)
					End If
				End Using
			End If
		End Sub

		Private Sub TsbSaveRecordClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbSaveRecord.Click
			Dim finger As NFinger = CType(toolStripViewControls.Tag, NFinger)
			saveFileDialog.Filter = String.Empty
			If finger IsNot Nothing Then
				Dim isRolled As Boolean = NBiometricTypes.IsImpressionTypeRolled(finger.ImpressionType)
				saveFileDialog.FileName = String.Format("{0}{1}", finger.Position,If(isRolled, "_Rolled", String.Empty))
				If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					Dim record As NFRecord = finger.Objects.ToArray().First().Template
					Using buffer As NBuffer = record.Save()
						File.WriteAllBytes(saveFileDialog.FileName, buffer.ToArray())
					End Using
				End If
			End If
		End Sub

		Private Sub NewToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles newToolStripMenuItem.Click
			If (Not Utilities.ShowQuestion(Me, "This will erase all images and records. Are you sure you want to continue?")) Then
				Return
			End If

			If _model.Subject IsNot Nothing Then
				_model.Subject.Fingers.Clear()
				_model.Subject = Nothing
			End If
			fSelector.MissingPositions = Nothing

			_model = New DataModel()
			_model.Info.AddRange(LoadInfoFields())
			infoPanel.Model = _model
			_newSubject = True
			EnableControls(True)
		End Sub

		Private Sub SaveTemplateToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveTemplateToolStripMenuItem.Click
			saveFileDialog.FileName = String.Empty
			saveFileDialog.Filter = String.Empty

			If _model.Subject Is Nothing Then
				Utilities.ShowWarning("Nothing to save")
			Else
				Using template As NTemplate = _model.Subject.GetTemplate()
					If template.Fingers Is Nothing Then
						Utilities.ShowWarning("Nothing to save")
					ElseIf saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
						Using buffer As NBuffer = template.Save()
							File.WriteAllBytes(saveFileDialog.FileName, buffer.ToArray())
						End Using
					End If
				End Using
			End If
		End Sub

		Private Sub SaveImagesToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveImagesToolStripMenuItem.Click
			If _model.Subject IsNot Nothing Then
				Dim subject As NSubject = _model.Subject
				Dim fingers = subject.Fingers.Where(Function(x) x.Status = NBiometricStatus.Ok)
				If fingers.Count() > 0 Then
					If folderBrowserDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
						Return
					End If
					Try
						Dim dir As String = folderBrowserDialog.SelectedPath
						For Each item As NFinger In fingers
							Dim isRolled As Boolean = NBiometricTypes.IsImpressionTypeRolled(item.ImpressionType)
							Dim name As String = String.Format("{0}{1}.png", item.Position,If(isRolled, "_rolled", String.Empty))
							item.Image.Save(Path.Combine(dir, name))
						Next item
					Catch ex As Exception
						Utilities.ShowError(ex)
					End Try
				Else
					Utilities.ShowWarning("Nothing to save")
				End If
			Else
				Utilities.ShowWarning("Nothing to save")
			End If
		End Sub

		Private Sub ExitToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem.Click
			Close()
		End Sub

		Private Sub ChangeScannerToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles changeScannerToolStripMenuItem.Click
			Using form As New DeviceSelectForm()
				form.SelectedDevice = _biometricClient.FingerScanner
				form.DeviceManager = _biometricClient.DeviceManager
				If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					If _biometricClient.FingerScanner IsNot form.SelectedDevice Then
						OnSelectedDeviceChanging(form.SelectedDevice)
					End If
				End If
			End Using
		End Sub

		Private Sub OptionsToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles optionsToolStripMenuItem1.Click
			Using form As New ExtractionOptionsForm()
				form.BiometricClient = _biometricClient
				form.ShowDialog()
			End Using
		End Sub

		Private Sub AboutToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
			Gui.AboutBox.Show(Me)
		End Sub

		Private Sub ExportToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles exportToolStripMenuItem.Click
			If folderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				_model.Save(folderBrowserDialog.SelectedPath)
			End If
		End Sub

		Private Sub EditRequiredInfoToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles editRequiredInfoToolStripMenuItem.Click
			Using form As New EditInfoForm()
				form.Information = LoadInfoFields()
				If form.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If
				_model.Info.Clear()
				_model.Info.AddRange(form.Information)
				infoPanel.Model = _model
			End Using
		End Sub
#End Region

#Region "Private methods"

		Private Sub OnMainFormLoaded()
			Try
				_biometricClient = CreateBiometricClient()
			Catch ex As Exception
				Utilities.ShowError(ex)
				BeginInvoke(New MethodInvoker(AddressOf Close))
				Return
			End Try

			_model = New DataModel()
			_model.Info.AddRange(LoadInfoFields())
			infoPanel.Model = _model
			infoPanel.DeviceManager = _biometricClient.DeviceManager

			toolStripViewControls.Visible = False
			Try
				chbCaptureRolled.Checked = My.Settings.ScanRolled
				chbCapturePlainFingers.Checked = My.Settings.ScanPlain
				chbCaptureSlaps.Checked = My.Settings.ScanSlaps
				chbShowOriginal.Checked = My.Settings.ShowOriginal
			Catch
			End Try

			If Not chbShowOriginal.Checked Then
				For Each position As NFPosition In _fingers
					Dim view As NFingerView = GetView(position, False)
					view.ShownImage = ShownImage.Result

					view = GetView(position, True)
					view.ShownImage = ShownImage.Result
				Next position
			End If

			If _biometricClient.FingerScanner Is Nothing Then
				Using form As New DeviceSelectForm()
					form.DeviceManager = _biometricClient.DeviceManager
					If form.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
						_biometricClient.FingerScanner = form.SelectedDevice
					Else
						Close()
					End If
				End Using
			End If

			OnSelectedDeviceChanging(_biometricClient.FingerScanner)
		End Sub

		Private Function CreateBiometricClient() As NBiometricClient
			Dim client As New NBiometricClient()

			Dim propertiesString As String = String.Empty
			Try
				propertiesString = My.Settings.ClientProperties
			Catch
			End Try

			Dim propertyBag As NPropertyBag = NPropertyBag.Parse(propertiesString)
			propertyBag.ApplyTo(client)

			client.BiometricTypes = NBiometricType.Finger Or NBiometricType.Face
			client.FingersReturnBinarizedImage = True
			client.UseDeviceManager = True
			client.FingersCalculateNfiq = NLicense.IsComponentActivated("Biometrics.FingerQualityAssessmentBase")

			LongTaskForm.RunLongTask("Initializing biometric client ...", AddressOf InitializeClient, client)

			Dim preferedDeviceId As String = String.Empty
			Try
				preferedDeviceId = My.Settings.SelectedFScannerId
			Catch
			End Try

			Dim device As NDevice = Nothing
			If (Not String.IsNullOrEmpty(preferedDeviceId)) Then
				If client.DeviceManager.Devices.Contains(preferedDeviceId) Then
					device = client.DeviceManager.Devices(preferedDeviceId)
				End If
			End If
			client.FingerScanner = CType(device, NFScanner)
			Return client
		End Function
		Private Sub InitializeClient(ByVal sender As Object, ByVal args As Object)
			CType(args.Argument, NBiometricClient).Initialize()
		End Sub

		Private Function GetView(ByVal position As NFPosition, ByVal isRolled As Boolean) As NFingerView
			Select Case position
				Case NFPosition.LeftIndex
					Return If(isRolled, nfvLeftIndexRolled, nfvLeftIndex)
				Case NFPosition.LeftLittle
					Return If(isRolled, nfvLeftLittleRolled, nfvLeftLittle)
				Case NFPosition.LeftMiddle
					Return If(isRolled, nfvLeftMiddleRolled, nfvLeftMiddle)
				Case NFPosition.LeftRing
					Return If(isRolled, nfvLeftRingRolled, nfvLeftRing)
				Case NFPosition.LeftThumb
					Return If(isRolled, nfvLeftThumbRolled, nfvLeftThumb)
				Case NFPosition.RightIndex
					Return If(isRolled, nfvRightIndexRolled, nfvRightIndex)
				Case NFPosition.RightLittle
					Return If(isRolled, nfvRightLittleRolled, nfvRightLittle)
				Case NFPosition.RightMiddle
					Return If(isRolled, nfvRightMiddleRolled, nfvRightMiddle)
				Case NFPosition.RightRing
					Return If(isRolled, nfvRightRingRolled, nfvRightRing)
				Case NFPosition.RightThumb
					Return If(isRolled, nfvRightThumbRolled, nfvRightThumb)
				Case NFPosition.PlainLeftFourFingers
					Return nfvLeftFour
				Case NFPosition.PlainRightFourFingers
					Return nfvRightFour
				Case NFPosition.PlainThumbs
					Return nfvThumbs
				Case Else
					Return Nothing
			End Select
		End Function

		Private Function GetView(ByVal finger As NFinger) As NFingerView
			Dim position As NFPosition = finger.Position
			Dim isRolled As Boolean = NBiometricTypes.IsImpressionTypeRolled(finger.ImpressionType)
			Return GetView(position, isRolled)
		End Function

		Private Function LoadInfoFields() As InfoField()
			Dim info As New List(Of InfoField)()
			Dim split() As String = My.Settings.Information.Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries)
			For Each item As String In split
				Dim inf As New InfoField(item)
				If inf.ShowAsThumbnail Then
					inf.IsEditable = False
				End If
				info.Add(inf)
			Next item
			Return info.ToArray()
		End Function

		Private Sub CreateFingers(ByVal subject As NSubject)
			Dim missing = fSelector.MissingPositions
			For Each item In missing
				subject.MissingFingers.Add(item)
			Next item

			Dim fingers = _fingers.Where(Function(x) (Not missing.Contains(x)))
			If chbCaptureSlaps.Checked Then
				Dim slaps = _slaps.Where(Function(x) NBiometricTypes.GetPositionAvailableParts(x, missing).Length <> 0)
				For Each pos In slaps
					subject.Fingers.Add(New NFinger With {.Position = pos})
				Next pos
			ElseIf chbCapturePlainFingers.Checked Then
				For Each pos In fingers
					subject.Fingers.Add(New NFinger With {.Position = pos})
				Next pos
			End If

			If chbCaptureRolled.Checked Then
				For Each pos In fingers
					subject.Fingers.Add(New NFinger With {.Position = pos, .ImpressionType = NFImpressionType.LiveScanRolled})
				Next pos
			End If
		End Sub

		Private Sub OnSelectedDeviceChanging(ByVal newDevice As NFScanner)
			Dim canCaptureRolled As Boolean = False
			Dim canCaptureSlaps As Boolean = False
			If newDevice IsNot Nothing Then
				For Each item As NFPosition In newDevice.GetSupportedPositions()
					If NBiometricTypes.IsPositionFourFingers(item) Then
						canCaptureSlaps = True
						Exit For
					End If
				Next item

				For Each item As NFImpressionType In newDevice.GetSupportedImpressionTypes()
					If NBiometricTypes.IsImpressionTypeRolled(item) Then
						canCaptureRolled = True
						Exit For
					End If
				Next item
			End If

			If _biometricClient.FingerScanner IsNot Nothing AndAlso _biometricClient.FingerScanner IsNot newDevice Then
				If Utilities.ShowQuestion("Changing scanner will clear all currently captured data. Proceed?") Then
					If _model.Subject IsNot Nothing Then
						_model.Subject.Fingers.Clear()
						_model.Subject = Nothing
					End If
					fSelector.MissingPositions = Nothing

					_model = New DataModel()
					_model.Info.AddRange(LoadInfoFields())
					infoPanel.Model = _model
					_newSubject = True
				Else
					Return
				End If
			End If

			_canCaptureSlaps = canCaptureSlaps
			_canCaptureRolled = canCaptureRolled

			_biometricClient.FingerScanner = newDevice
			If (Not _canCaptureSlaps) Then
				chbCaptureSlaps.Checked = False
			End If
			If (Not _canCaptureRolled) Then
				chbCaptureRolled.Checked = False
			End If
			EnableControls(True)
		End Sub

		Private Sub SaveSettings()
			If _biometricClient IsNot Nothing Then
				My.Settings.SelectedFScannerId = If(_biometricClient.FingerScanner IsNot Nothing, _biometricClient.FingerScanner.Id, Nothing)
				My.Settings.ScanRolled = chbCaptureRolled.Checked
				My.Settings.ScanSlaps = chbCaptureSlaps.Checked
				My.Settings.ScanPlain = chbCapturePlainFingers.Checked
				My.Settings.ShowOriginal = chbShowOriginal.Checked

				Dim propertyBag As New NPropertyBag()
				_biometricClient.CaptureProperties(propertyBag)
				My.Settings.ClientProperties = propertyBag.ToString()
				My.Settings.Save()
			End If
		End Sub

		Private Sub EnableControls(ByVal enable As Boolean)
			btnStartCapturing.Enabled = enable AndAlso (chbCaptureRolled.Checked OrElse chbCapturePlainFingers.Checked)
			chbCapturePlainFingers.Enabled = enable AndAlso _newSubject
			chbCaptureRolled.Enabled = _canCaptureRolled AndAlso enable AndAlso _newSubject
			chbCaptureSlaps.Enabled = _canCaptureSlaps AndAlso enable AndAlso _newSubject
			gbFingerSelector.Enabled = enable AndAlso _newSubject
			fSelector.Enabled = enable AndAlso _newSubject
			changeScannerToolStripMenuItem.Enabled = enable
			optionsToolStripMenuItem.Enabled = enable
			saveImagesToolStripMenuItem.Enabled = enable
			saveTemplateToolStripMenuItem.Enabled = enable
			newToolStripMenuItem.Enabled = enable
			exportToolStripMenuItem.Enabled = enable
		End Sub

#End Region
	End Class
End Namespace
