Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Images
Imports Neurotec.Images.Processing
Imports Neurotec.Licensing
Imports Neurotec.Biometrics.Client
Imports System.Linq
Imports Neurotec.IO

Partial Public Class MainForm
	Inherits Form
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Private fields"

	Private _biometricClient As NBiometricClient

	Private _imageLeft As NImage
	Private _imageLeftOriginal As NImage
	Private _binarizedImage As NImage
	Private _record As NFRecord

	Private _matcherLicenseAvailable? As Boolean = Nothing

	Private ReadOnly _brightness(2) As Double
	Private ReadOnly _contrast(2) As Double
	Private _invert As Boolean
	Private _convertToGrayscale As Boolean
	Private _processingRequested As Boolean

	Private ReadOnly _zoomFactors() As Single = {0.25F, 0.33F, 0.5F, 0.66F, 0.8F, 1.0F, 1.25F, 1.5F, 2.0F, 2.5F, 3.0F}
	Private Const DefaultZoomFactor As Integer = 5
	Private _leftZoomFactor As Integer = DefaultZoomFactor
	Private _rightZoomFactor As Integer = DefaultZoomFactor

	Private Enum AddMode
		None
		EndMinutia
		BifurcationMinutia
		Delta
		Core
		DoubleCore
	End Enum
	Private _addMode As AddMode = AddMode.None

#End Region

#Region "Image Loading"

	Private Function ResolutionCheck(ByVal image As NImage, ByRef horzResolution As Single, ByRef vertResolution As Single) As Boolean
		Dim frmResolution = New ResolutionForm With {.HorzResolution = horzResolution, .VertResolution = vertResolution, .FingerImage = image.ToBitmap()}

		If frmResolution.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			horzResolution = frmResolution.HorzResolution
			vertResolution = frmResolution.VertResolution
			Return True
		End If
		horzResolution = image.HorzResolution
		vertResolution = image.VertResolution
		Return False
	End Function

	Private Sub BtnLeftClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenLeft.Click
		openFileDialog.FileName = Nothing

		If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Try
				Using image As NImage = NImage.FromFile(openFileDialog.FileName)
					Dim horzResolution As Single = image.HorzResolution
					If horzResolution < 250.0F Then
						horzResolution = 500.0F
					End If
					Dim vertResolution As Single = image.VertResolution
					If vertResolution < 250.0F Then
						vertResolution = 500.0F
					End If
					If ResolutionCheck(image, horzResolution, vertResolution) Then
						nfViewLeft.Tree = Nothing
						nfViewRight.Tree = Nothing

						Dim finger As NFinger = CType(nfViewLeft.Finger, NFinger)
						nfViewLeft.Finger = Nothing
						If finger IsNot Nothing Then
							finger.Dispose()
						End If
						nfViewLeft.ClearSelectedArea()

						If _record IsNot Nothing Then
							_record.Dispose()
						End If
						_record = Nothing

						If _imageLeft IsNot Nothing Then
							_imageLeft.Dispose()
							_imageLeft = Nothing
						End If
						If _binarizedImage IsNot Nothing Then
							_binarizedImage.Dispose()
							_binarizedImage = Nothing
						End If

						tsbExtractLeft.Enabled = False
						btnMatcher.Enabled = False

						_imageLeft = NImage.FromImage(NPixelFormat.Rgb8U, 0, image)
						_imageLeft.HorzResolution = horzResolution
						_imageLeft.VertResolution = vertResolution
						_imageLeft.ResolutionIsAspectRatio = False

						nfViewLeft.ShownImage = ShownImage.Original
						tsmiViewOriginalLeft.Checked = True
						TsmiZoomOriginalLeftClick(Me, EventArgs.Empty)

						_imageLeftOriginal = CType(_imageLeft.Clone(), NImage)

						lblLatentSize.Text = String.Format("Size: {0} x {1}", _imageLeftOriginal.Width, _imageLeftOriginal.Height)
						lblLatentResolution.Text = String.Format("Resolution: {0:F2} x {1:F2}", _imageLeftOriginal.HorzResolution, _imageLeftOriginal.VertResolution)

						lblLeftFilename.Text = openFileDialog.FileName

						tsbExtractLeft.Enabled = True

						BtnResetAllClick(Me, EventArgs.Empty)
					End If
				End Using
			Catch ex As Exception
				Utilities.ShowError("Error opening file ""{0}"": {1}", openFileDialog.FileName, ex.Message)
			End Try
		End If
	End Sub

	Private Sub BtnRightClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenRight.Click
		openFileDialog.FileName = Nothing

		If openFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
			Return
		End If
		Try
			' read image
			Dim tmp As NImage = NImage.FromFile(openFileDialog.FileName)

			Dim horzResolution As Single = tmp.HorzResolution
			If horzResolution < 250.0F Then
				horzResolution = 500.0F
			End If
			Dim vertResolution As Single = tmp.VertResolution
			If vertResolution < 250.0F Then
				vertResolution = 500.0F
			End If
			If ResolutionCheck(tmp, horzResolution, vertResolution) Then
				nfViewRight.Tree = Nothing
				nfViewLeft.Tree = Nothing

				Dim finger As NFinger = CType(nfViewRight.Finger, NFinger)
				nfViewRight.Finger = Nothing
				If finger IsNot Nothing Then
					finger.Dispose()
				End If

				nfViewRight.ClearSelectedArea()

				btnMatcher.Enabled = False

				Dim image As NImage = NImage.FromImage(NPixelFormat.Rgb8U, 0, tmp)
				image.HorzResolution = horzResolution
				image.VertResolution = vertResolution
				image.ResolutionIsAspectRatio = False

				tmp.Dispose()

				lblReferenceSize.Text = String.Format("Size: {0} x {1}", image.Width, image.Height)
				lblReferenceResolution.Text = String.Format("Resolution: {0:F2} x {1:F2}", image.HorzResolution, image.VertResolution)

				lblRightFilename.Text = openFileDialog.FileName

				finger = New NFinger With {.Image = image}
				nfViewRight.Finger = finger
				nfViewRight.ShownImage = ShownImage.Original
				tsmiViewOriginalRight.Checked = True
				TsmiZoomOriginalRightClick(Me, EventArgs.Empty)

				If image IsNot Nothing Then
					nfViewRight.Tree = Nothing
					nfViewLeft.Tree = Nothing

					Using subject As New NSubject()
						subject.Fingers.Add(finger)

						Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.CreateTemplate, subject)
						LongActionDialog.ShowDialog(Me, "Extracting ...", New Action(Of NBiometricTask)(AddressOf _biometricClient.PerformTask), task)
						If task.Error IsNot Nothing Then
							Utilities.ShowError(task.Error)
							Return
						End If

						Dim status = task.Status
						If status = NBiometricStatus.Ok Then
							Dim leftFinger As NFinger = CType(nfViewLeft.Finger, NFinger)
							If leftFinger IsNot Nothing AndAlso leftFinger.Status = NBiometricStatus.Ok Then
								btnMatcher.Enabled = True
							End If
						Else
							Utilities.ShowError("Failed to create template: {0}", status)
						End If
					End Using
				End If
			End If
		Catch ex As Exception
			Utilities.ShowError("Error opening or extracting file ""{0}"": {1}", openFileDialog.FileName, ex.Message)
		End Try
	End Sub

#End Region

#Region "Image Processing"

	Private Sub NfViewLeftPaint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles nfViewLeft.Paint
		If _processingRequested Then
			ProcessImage()
		End If
	End Sub

	Private Sub CbInvertCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbInvert.CheckedChanged
		_invert = cbInvert.Checked
		RequestImageProcessing()
	End Sub

	Private Sub CbGrayscaleCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbGrayscale.CheckedChanged
		_convertToGrayscale = cbGrayscale.Checked
		RequestImageProcessing()
	End Sub

	Private Sub BrightnessValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles sliderBrightnessGreen.ValueChanged, sliderBrightnessRed.ValueChanged, sliderBrightnessBlue.ValueChanged
		Dim colorSlider = TryCast(sender, ColorSlider)
		If colorSlider Is Nothing Then
			Return
		End If
		Dim normalizedValue = -colorSlider.Value
		If cbGroupBrightnessSliders.Checked Then
			lblBrightnessR.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)
			lblBrightnessG.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)
			lblBrightnessB.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)

			If sliderBrightnessRed.Value <> colorSlider.Value Then
				sliderBrightnessRed.Value = colorSlider.Value
			End If
			If sliderBrightnessGreen.Value <> colorSlider.Value Then
				sliderBrightnessGreen.Value = colorSlider.Value
			End If
			If sliderBrightnessBlue.Value <> colorSlider.Value Then
				sliderBrightnessBlue.Value = colorSlider.Value
			End If
			For i As Integer = 0 To 2
				_brightness(i) = normalizedValue / 100.0
			Next i
		Else
			_brightness(Convert.ToInt32(colorSlider.Tag)) = normalizedValue / 100.0

			Select Case Convert.ToInt32(colorSlider.Tag)
				Case 0
					lblBrightnessR.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)

				Case 1
					lblBrightnessG.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)

				Case 2
					lblBrightnessB.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)
			End Select
		End If
		RequestImageProcessing()
	End Sub

	Private Sub ContrastValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles sliderContrastBlue.ValueChanged, sliderContrastGreen.ValueChanged, sliderContrastRed.ValueChanged
		Dim colorSlider = TryCast(sender, ColorSlider)
		If colorSlider Is Nothing Then
			Return
		End If
		Dim normalizedValue = -colorSlider.Value
		If cbGroupContrastSliders.Checked Then
			lblContrastRValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)
			lblContrastGValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)
			lblContrastBValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)

			If sliderContrastRed.Value <> colorSlider.Value Then
				sliderContrastRed.Value = colorSlider.Value
			End If
			If sliderContrastGreen.Value <> colorSlider.Value Then
				sliderContrastGreen.Value = colorSlider.Value
			End If
			If sliderContrastBlue.Value <> colorSlider.Value Then
				sliderContrastBlue.Value = colorSlider.Value
			End If

			For i As Integer = 0 To 2
				_contrast(i) = normalizedValue / 100.0
			Next i
		Else
			_contrast(Convert.ToInt32(colorSlider.Tag)) = normalizedValue / 100.0

			Select Case Convert.ToInt32(colorSlider.Tag)
				Case 0
					lblContrastRValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)

				Case 1
					lblContrastGValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)

				Case 2
					lblContrastBValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture)
			End Select
		End If
		RequestImageProcessing()
	End Sub

	Private Sub RequestImageProcessing()
		_processingRequested = True
		nfViewLeft.Invalidate()
	End Sub

	Private Sub ProcessImage()
		If _imageLeftOriginal Is Nothing Then
			Return
		End If
		Dim img = CType(_imageLeftOriginal.Clone(), NImage)

		If _invert Then
			Nrgbip.InvertSame(img)
		End If

		If Math.Abs(_brightness(0)) > Double.Epsilon OrElse Math.Abs(_brightness(1)) > Double.Epsilon OrElse Math.Abs(_brightness(2)) > Double.Epsilon OrElse Math.Abs(_contrast(0)) > Double.Epsilon OrElse Math.Abs(_contrast(1)) > Double.Epsilon OrElse Math.Abs(_contrast(2)) > Double.Epsilon Then
			Nrgbip.AdjustBrightnessContrastSame(img, _brightness(0), _contrast(0), _brightness(1), _contrast(1), _brightness(2), _contrast(2))
		End If

		If _convertToGrayscale Then
			Dim grayImage As NImage = NImage.FromImage(NPixelFormat.Grayscale8U, 0, img)
			img = NImages.GetGrayscaleColorWrapper(grayImage, New NRgb(), New NRgb(255, 255, 255))
		End If

		Dim oldFinger = nfViewLeft.Finger
		Dim newFinger As NFrictionRidge
		If _record IsNot Nothing Then
			newFinger = NFinger.FromImageAndTemplate(img, _record)
		Else
			newFinger = New NFinger With {.Image = img}
		End If
		newFinger.BinarizedImage = _binarizedImage
		nfViewLeft.Finger = newFinger

		If oldFinger IsNot Nothing Then
			oldFinger.Dispose()
		End If

		nfViewLeft.Invalidate()

		If _imageLeft IsNot Nothing Then
			_imageLeft.Dispose()
		End If
		_imageLeft = img

		_processingRequested = False
	End Sub

	Private Sub BtnResetBrightnessClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetBrightness.Click
		sliderBrightnessRed.Value = 0
		sliderBrightnessGreen.Value = 0
		sliderBrightnessBlue.Value = 0

		RequestImageProcessing()
	End Sub

	Private Sub BtnResetContrastClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetContrast.Click
		sliderContrastRed.Value = 0
		sliderContrastGreen.Value = 0
		sliderContrastBlue.Value = 0

		RequestImageProcessing()
	End Sub

	Private Sub BtnResetAllClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetAll.Click
		cbInvert.Checked = False

		sliderBrightnessRed.Value = 0
		sliderBrightnessGreen.Value = 0
		sliderBrightnessBlue.Value = 0

		sliderContrastRed.Value = 0
		sliderContrastGreen.Value = 0
		sliderContrastBlue.Value = 0

		cbGrayscale.Checked = False

		RequestImageProcessing()
	End Sub

#End Region

#Region "Image Transformations"

	Private Sub OnRecordChanged(ByVal value As NFRecord)
		Dim finger As NFinger = CType(nfViewLeft.Finger, NFinger)
		If finger IsNot Nothing Then
			Dim attributes As NFAttributes = finger.Objects.FirstOrDefault()
			If attributes IsNot Nothing Then
				attributes.Template = value
			End If
		End If
		_record = value
	End Sub

	Private Sub TsmiRotate90cwClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiRotate90cw.Click
		If _imageLeftOriginal IsNot Nothing Then
			Dim transformed As NImage = _imageLeftOriginal.RotateFlip(NImageRotateFlipType.Rotate90)
			If _record IsNot Nothing Then
				OnRecordChanged(TransformFeatures.Rotate90(_record))
			End If

			OnImageTransformed(transformed)
		End If
	End Sub

	Private Sub TsmiRotate90ccwClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiRotate90ccw.Click
		If _imageLeftOriginal IsNot Nothing Then
			Dim transformed As NImage = _imageLeftOriginal.RotateFlip(NImageRotateFlipType.Rotate270)
			If _record IsNot Nothing Then
				OnRecordChanged(TransformFeatures.Rotate270(_record))
			End If

			OnImageTransformed(transformed)
		End If
	End Sub

	Private Sub TsmiRotate180Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiRotate180.Click
		If _imageLeftOriginal IsNot Nothing Then
			Dim transformed As NImage = _imageLeftOriginal.RotateFlip(NImageRotateFlipType.Rotate180)
			If _record IsNot Nothing Then
				OnRecordChanged(TransformFeatures.Rotate180(_record))
			End If

			OnImageTransformed(transformed)
		End If
	End Sub

	Private Sub TsmiFlipHorzClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiFlipHorz.Click
		If _imageLeftOriginal IsNot Nothing Then
			_imageLeftOriginal.FlipHorizontally()
			If _record IsNot Nothing Then
				OnRecordChanged(TransformFeatures.FlipHorizontally(_record))
			End If

			OnImageTransformed(Nothing)
		End If
	End Sub

	Private Sub TsmiFlipVertClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiFlipVert.Click
		If _imageLeftOriginal IsNot Nothing Then
			_imageLeftOriginal.FlipVertically()
			If _record IsNot Nothing Then
				OnRecordChanged(TransformFeatures.FlipVertically(_record))
			End If
			OnImageTransformed(Nothing)
		End If
	End Sub

	Private Sub TsmiCropToSelClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiCropToSel.Click
		If _imageLeftOriginal IsNot Nothing Then
			If (Not nfViewLeft.IsPartOfImageSelected) Then
				Utilities.ShowInformation("Please select part of image with Area Selection Tool first!")
				Return
			End If
			Dim rect As Rectangle = nfViewLeft.SelectedImageArea
			If rect.X < 0 OrElse rect.Y < 0 OrElse rect.X + rect.Width > _imageLeftOriginal.Width OrElse rect.Y + rect.Height > _imageLeftOriginal.Height Then
				Utilities.ShowInformation("Please select part of image with Area Selection Tool first (only image)!")
				Return
			End If

			Dim transformed As NImage = _imageLeftOriginal.Crop(CUInt(rect.X), CUInt(rect.Y), CUInt(rect.Width), CUInt(rect.Height))
			If _record IsNot Nothing Then
				Dim coeffX As Double = NFRecord.Resolution / CDbl(_imageLeftOriginal.HorzResolution)
				Dim coeffY As Double = NFRecord.Resolution / CDbl(_imageLeftOriginal.VertResolution)
				nfViewRight.Tree = Nothing
				nfViewLeft.Tree = nfViewRight.Tree
				OnRecordChanged(TransformFeatures.Crop(_record, transformed, CUInt(rect.X * coeffX), CUInt(rect.Y * coeffY), CUInt(Math.Ceiling(rect.Width * coeffX)), CUInt(Math.Ceiling(rect.Height * coeffY))))
			End If

			OnImageTransformed(transformed)
		End If
	End Sub

	Private Sub OnImageTransformed(ByVal transformed As NImage)
		nfViewLeft.ClearSelectedArea()

		If transformed IsNot Nothing Then
			If _imageLeftOriginal IsNot Nothing Then
				_imageLeftOriginal.Dispose()
			End If
			_imageLeftOriginal = transformed
		End If
		ProcessImage()
	End Sub

	Private Sub TsmiInvertMinutiaeClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiInvertMinutiae.Click
		If _record IsNot Nothing Then
			Dim minutiae = _record.Minutiae.ToArray()
			_record.Minutiae.Clear()
			For i As Integer = 0 To minutiae.Length - 1
				Dim minutia = minutiae(i)
				If minutia.Type = NFMinutiaType.Bifurcation Then
					minutia.Type = NFMinutiaType.End
				ElseIf minutia.Type = NFMinutiaType.End Then
					minutia.Type = NFMinutiaType.Bifurcation
				End If
				_record.Minutiae.Add(minutia)
			Next i
			nfViewLeft.Invalidate()
		End If
	End Sub

#End Region

#Region "Extraction"

	Private Function GetWorkingImage() As NImage
		Return If(rbUseEditedImage.Checked, _imageLeft, _imageLeftOriginal)
	End Function

	Private Function moveTemplateCoordinates(ByVal inData As NFRecord, ByVal width As UShort, ByVal height As UShort, ByVal horzResolution As UShort, ByVal vertResolution As UShort, ByVal offsetX As Integer, ByVal offsetY As Integer) As NFRecord
		If inData Is Nothing Then
			Return Nothing
		End If

		Dim coeffX As Double = NFRecord.Resolution / CDbl(horzResolution)
		Dim coeffY As Double = NFRecord.Resolution / CDbl(vertResolution)

		Dim record = New NFRecord(width, height, horzResolution, vertResolution) With {.MinutiaFormat = inData.MinutiaFormat, .CbeffProductType = inData.CbeffProductType, .RidgeCountsType = inData.RidgeCountsType}

		Dim templateWidth = CInt(Fix(width * coeffX))
		Dim templateHeight = CInt(Fix(height * coeffY))
		For i As Integer = 0 To inData.Minutiae.Count - 1
			Dim min As NFMinutia = inData.Minutiae(i)
			min.X += CUShort(offsetX * coeffX)
			min.Y += CUShort(offsetY * coeffY)
			If min.X >= 0 AndAlso min.X < templateWidth AndAlso min.Y >= 0 AndAlso min.Y < templateHeight Then
				record.Minutiae.Add(min)
			End If
		Next i
		For i As Integer = 0 To inData.Deltas.Count - 1
			Dim delta As NFDelta = inData.Deltas(i)
			delta.X += CUShort(offsetX * coeffX)
			delta.Y += CUShort(offsetY * coeffY)
			If delta.X >= 0 AndAlso delta.X < templateWidth AndAlso delta.Y >= 0 AndAlso delta.Y < templateHeight Then
				record.Deltas.Add(delta)
			End If
		Next i
		For i As Integer = 0 To inData.Cores.Count - 1
			Dim core As NFCore = inData.Cores(i)
			core.X += CUShort(offsetX * coeffX)
			core.Y += CUShort(offsetY * coeffY)
			If core.X >= 0 AndAlso core.X < templateWidth AndAlso core.Y >= 0 AndAlso core.Y < templateHeight Then
				record.Cores.Add(core)
			End If
		Next i
		For i As Integer = 0 To inData.DoubleCores.Count - 1
			Dim doubleCore As NFDoubleCore = inData.DoubleCores(i)
			doubleCore.X += CUShort(offsetX * coeffX)
			doubleCore.Y += CUShort(offsetY * coeffY)
			If doubleCore.X >= 0 AndAlso doubleCore.X < templateWidth AndAlso doubleCore.Y >= 0 AndAlso doubleCore.Y < templateHeight Then
				record.DoubleCores.Add(doubleCore)
			End If
		Next i

		Return record
	End Function

	Private Sub ExtractImage(ByVal image As NImage)
		Try
			nfViewRight.Tree = Nothing
			nfViewLeft.Tree = Nothing

			Dim needCropping As Boolean = nfViewLeft.IsPartOfImageSelected
			Dim cropBounds = New Rectangle()
			If needCropping Then
				cropBounds = nfViewLeft.SelectedImageArea
			End If

			Dim workingImage As NImage = image
			If needCropping Then
				workingImage = image.Crop(CUInt(cropBounds.X), CUInt(cropBounds.Y), CUInt(cropBounds.Width), CUInt(cropBounds.Height))
			End If

			Dim subject As New NSubject()
			Dim finger As NFinger = New NFinger With {.Image = workingImage}
			subject.Fingers.Add(finger)

			Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.CreateTemplate, subject)
			LongActionDialog.ShowDialog(Me, "Extracting ...", New Action(Of NBiometricTask)(AddressOf _biometricClient.PerformTask), task)
			If task.Error IsNot Nothing Then
				Utilities.ShowError(task.Error)
				Return
			End If

			Dim status = task.Status
			If status = NBiometricStatus.Ok Then
				Dim record As NFRecord = finger.Objects.First().Template
				If needCropping Then
					Dim resultImage As NImage = TryCast(image.Clone(), NImage)
					Using binarizedImage = NImage.FromImage(NPixelFormat.Rgb8U, resultImage.Stride, finger.BinarizedImage)
						binarizedImage.CopyTo(resultImage, CUInt(cropBounds.Left), CUInt(cropBounds.Top))
					End Using
					_binarizedImage = resultImage
					OnRecordChanged(moveTemplateCoordinates(record, CUShort(image.Width), CUShort(image.Height), CUShort(image.HorzResolution), CUShort(image.VertResolution), CUShort(cropBounds.X), CUShort(cropBounds.Y)))
				Else
					_binarizedImage = finger.BinarizedImage
					OnRecordChanged(record)
				End If

				Dim fingerRight As NFinger = CType(nfViewRight.Finger, NFinger)
				If fingerRight IsNot Nothing AndAlso fingerRight.Status = NBiometricStatus.Ok Then
					btnMatcher.Enabled = True
				End If
			Else
				If _record IsNot Nothing Then
					nfViewLeft.SelectedDeltaIndex = -1
					nfViewLeft.SelectedDoubleCoreIndex = nfViewLeft.SelectedDeltaIndex
					nfViewLeft.SelectedMinutiaIndex = nfViewLeft.SelectedDoubleCoreIndex
					nfViewLeft.SelectedDeltaIndex = nfViewLeft.SelectedMinutiaIndex
					Dim newRecord As NFRecord = New NFRecord(_record.Width, _record.Height, _record.HorzResolution, _record.VertResolution)
					newRecord.CbeffProductType = _record.CbeffProductType
					newRecord.MinutiaFormat = _record.MinutiaFormat
					newRecord.RidgeCountsType = _record.RidgeCountsType
					OnRecordChanged(newRecord)
				End If
				_binarizedImage = Nothing
				Utilities.ShowError("Fingerprint extraction failed: {0}", status)
			End If
			RequestImageProcessing()
		Catch ex As Exception
			Utilities.ShowError("Extraction error: {0}", ex.Message)
		End Try
	End Sub

	Private Sub UpdateRecord(ByVal workingImage As NImage)
		Dim subject As New NSubject()
		Dim finger As NFinger = CType(NFinger.FromImageAndTemplate(workingImage, _record), NFinger)
		subject.Fingers.Add(finger)
		_biometricClient.CreateTemplate(subject) ' Update template
		nfViewLeft.Invalidate()
	End Sub

	Private Sub TsbExtractLeftClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbExtractLeft.Click
		Dim workingImage As NImage = GetWorkingImage()
		If workingImage IsNot Nothing Then
			ExtractImage(workingImage)
		End If
	End Sub

	Private Sub TsmiExtractionSettingsClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiExtractionSettings.Click
		Dim frmExtractionSettings = New ExtractionSettingsForm With {.QualityThreshold = _biometricClient.FingersQualityThreshold}
		If frmExtractionSettings.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			_biometricClient.FingersQualityThreshold = frmExtractionSettings.QualityThreshold
		End If
	End Sub

#End Region

	#Region "Matching"

	Private Sub BtnMatcherClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnMatcher.Click
		If (Not _matcherLicenseAvailable.HasValue) Then
			_matcherLicenseAvailable = ObtainMatchingLicense()
			If _matcherLicenseAvailable.GetValueOrDefault() = False Then
				Return
			End If
		ElseIf _matcherLicenseAvailable.GetValueOrDefault() = False Then
			Utilities.ShowInformation("Matching license not available")
			Return
		End If

		Try
			Dim workingImage As NImage = GetWorkingImage()

			lblMatchingScore.Text = "Matching..."
			lblMatchingScore.Refresh()

			Dim rightRec As NFRecord = nfViewRight.Finger.Objects.First().Template
			If _record.RequiresUpdate Then
				UpdateRecord(workingImage)
			End If

			Dim subjectRight As New NSubject()
			Dim subjectLeft As New NSubject()

			subjectLeft.SetTemplateBuffer(_record.Save())
			subjectRight.SetTemplateBuffer(rightRec.Save())

			Dim status As NBiometricStatus = _biometricClient.Verify(subjectLeft, subjectRight)
			Dim matedMinutiae() As NIndexPair = Nothing
			Dim score As Integer = 0
			If status = NBiometricStatus.Ok Then
				Dim result = subjectLeft.MatchingResults.First()
				Dim details = result.MatchingDetails
				score = result.Score
				matedMinutiae = details.Fingers.First().GetMatedMinutiae()
			End If
			nfViewLeft.MatedMinutiae = matedMinutiae
			nfViewRight.MatedMinutiae = matedMinutiae
			If score > 0 Then
				nfViewLeft.PrepareTree()
				nfViewRight.Tree = nfViewLeft.Tree
			Else
				Utilities.ShowInformation("Fingerprints do not match.")
			End If

			lblMatchingScore.Text = String.Format("Score: {0}", score)
		Catch ex As Exception
			lblMatchingScore.Text = "Matching failed"
			Utilities.ShowError("Error while matching. Please check matching settings. Reason: {0}", ex.Message)
		End Try
	End Sub

	Private Function ObtainMatchingLicense() As Boolean
		Try
			If (Not NLicense.ObtainComponents("/local", 5000, "Biometrics.FingerMatching")) Then
				Utilities.ShowWarning("Could not obtain license for fingerprint matcher.")
				Return False
			End If
			Return True
		Catch ex As Exception
			Utilities.ShowError("Failed while obtaining matching license: {0}", ex.Message)
			Return False
		End Try
	End Function

	#End Region ' Matching

	#Region "Add Features"

	Private Sub NfViewLeftFeatureAddCompleted(ByVal sender As Object, ByVal e As EventArgs)
		Dim featureAddEvent As NFingerView.AddFeaturesTool.FeatureAddCompletedEventArgs = TryCast(e, NFingerView.AddFeaturesTool.FeatureAddCompletedEventArgs)
		If featureAddEvent IsNot Nothing Then
			AddFeature(featureAddEvent.Start, featureAddEvent.End)
		End If
	End Sub

	Private Sub BitmapToTemplateCoords(ByRef pt As Point)
		Dim vertResolution = _imageLeftOriginal.HorzResolution
		Dim horzResolution = _imageLeftOriginal.VertResolution
		If vertResolution < 250 Then
			vertResolution = 500
		End If
		If horzResolution < 250 Then
			horzResolution = 500
		End If
		pt.X = CUShort(pt.X * NFRecord.Resolution / horzResolution)
		pt.Y = CUShort(pt.Y * NFRecord.Resolution / vertResolution)
	End Sub

	Private Shared Function CalculateDraggedAngle(ByVal pt As Point, ByVal dragPt As Point) As Double
		Return Math.Atan2(dragPt.Y - pt.Y, dragPt.X - pt.X)
	End Function

	Private Function AddFeature(ByVal pt As Point, ByVal dragPt As Point) As Boolean
		If _imageLeftOriginal IsNot Nothing Then
			Try
				If _record Is Nothing Then
					' create a new template if not exists
					Dim templateWidth = CUShort(_imageLeftOriginal.Width)
					Dim templateHeight = CUShort(_imageLeftOriginal.Height)
					Dim vertResolution = _imageLeftOriginal.VertResolution
					Dim horzResolution = _imageLeftOriginal.HorzResolution
					If vertResolution < 250 Then
						vertResolution = NFRecord.Resolution
					End If
					If horzResolution < 250 Then
						horzResolution = NFRecord.Resolution
					End If

					_record = New NFRecord(templateWidth, templateHeight, CUShort(vertResolution), CUShort(horzResolution))
					_record.CbeffProductType = 256
					_record.MinutiaFormat = NFMinutiaFormat.HasCurvature Or NFMinutiaFormat.HasG Or NFMinutiaFormat.HasQuality
					_record.RidgeCountsType = NFRidgeCountsType.EightNeighborsWithIndexes
					ProcessImage()
				End If

				Dim angle As Double = CalculateDraggedAngle(pt, dragPt)
				BitmapToTemplateCoords(pt)

				Dim addX = CUShort(pt.X)
				Dim addY = CUShort(pt.Y)

				Dim index As Integer = -1
				Select Case _addMode
					Case AddMode.EndMinutia
						index = _record.Minutiae.Add(New NFMinutia(addX, addY, NFMinutiaType.End, angle))
					Case AddMode.BifurcationMinutia
						index = _record.Minutiae.Add(New NFMinutia(addX, addY, NFMinutiaType.Bifurcation, angle))
					Case AddMode.Delta
						index = _record.Deltas.Add(New NFDelta(addX, addY))
					Case AddMode.Core
						index = _record.Cores.Add(New NFCore(addX, addY, angle))
					Case AddMode.DoubleCore
						index = _record.DoubleCores.Add(New NFDoubleCore(addX, addY))
				End Select

				If index <> -1 Then
					Select Case _addMode
						Case AddMode.EndMinutia, AddMode.BifurcationMinutia
							nfViewLeft.SelectedMinutiaIndex = index
						Case AddMode.Delta
							nfViewLeft.SelectedDeltaIndex = index
						Case AddMode.Core
							nfViewLeft.SelectedCoreIndex = index
						Case AddMode.DoubleCore
							nfViewLeft.SelectedDoubleCoreIndex = index
					End Select
					nfViewLeft.Invalidate()
				End If

				Dim fingerRight As NFinger = CType(nfViewRight.Finger, NFinger)
				If fingerRight IsNot Nothing AndAlso fingerRight.Status = NBiometricStatus.Ok Then
					btnMatcher.Enabled = True
				End If
				Return True
			Catch ex As Exception
				Utilities.ShowError("Failed to add feature: {0}", ex.Message)
				Return False
			End Try
		End If
		Utilities.ShowInformation("Please open an image before editing!")
		Return False
	End Function

	#End Region

	#Region "Delete Features"

	Private Sub ContextMenuLeftOpening(ByVal sender As Object, ByVal e As CancelEventArgs) Handles contextMenuLeft.Opening
		e.Cancel = TypeOf nfViewLeft.ActiveTool Is NFingerView.AddFeaturesTool
		If (Not e.Cancel) Then
			tsmiDeleteFeature.Enabled = (nfViewLeft.SelectedMinutiaIndex >= 0 OrElse nfViewLeft.SelectedDeltaIndex >= 0 OrElse nfViewLeft.SelectedCoreIndex >= 0 OrElse nfViewLeft.SelectedDoubleCoreIndex >= 0)
		End If
	End Sub

	Private Sub TsmiDeleteFeatureClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiDeleteFeature.Click
		Dim finger As NFinger = CType(nfViewLeft.Finger, NFinger)
		If finger IsNot Nothing Then
			If _record IsNot Nothing Then
				Dim deleted As Boolean = False
				Dim index As Integer = nfViewLeft.SelectedMinutiaIndex
				If index >= 0 Then
					nfViewLeft.SelectedMinutiaIndex = -1
					_record.Minutiae.RemoveAt(index)
					deleted = True
				End If
				index = nfViewLeft.SelectedDeltaIndex
				If index >= 0 Then
					nfViewLeft.SelectedDeltaIndex = -1
					_record.Deltas.RemoveAt(index)
					deleted = True
				End If
				index = nfViewLeft.SelectedCoreIndex
				If index >= 0 Then
					nfViewLeft.SelectedCoreIndex = -1
					_record.Cores.RemoveAt(index)
					deleted = True
				End If
				index = nfViewLeft.SelectedDoubleCoreIndex
				If index >= 0 Then
					nfViewLeft.SelectedDoubleCoreIndex = -1
					_record.DoubleCores.RemoveAt(index)
					deleted = True
				End If

				If deleted Then
					nfViewRight.Tree = Nothing
					nfViewLeft.Tree = Nothing

					nfViewLeft.Invalidate()
				End If
			End If
		End If
	End Sub

	#End Region

	#Region "Save File"

	Private Sub TsbSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbSaveTemplate.Click, tsmiSaveTemplate.Click
		Dim workingImage As NImage = GetWorkingImage()

		If workingImage IsNot Nothing Then
			If _record IsNot Nothing Then
				If saveTemplateDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If
				Try
					If _record.RequiresUpdate Then
						UpdateRecord(workingImage)
					End If
					'save template to file (*.data)
					Using buffer As NBuffer = _record.Save()
						File.WriteAllBytes(saveTemplateDialog.FileName, buffer.ToArray())
						Return
					End Using
				Catch ex As Exception
					Utilities.ShowError("Failed to save template: {0}", ex.Message)
					Return
				End Try
			End If
		End If
		Utilities.ShowWarning("Nothing to save.")
	End Sub

	Private Sub SaveImageToFile(ByVal image As NImage)
		If saveImageDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
			Return
		End If
		Try
			image.Save(saveImageDialog.FileName)
		Catch ex As Exception
			Utilities.ShowError("Failed to save image to file: {0}", ex.Message)
		End Try
	End Sub

	Private Sub TsmiSaveLatentImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiSaveLatentImage.Click
		Dim workingImage As NImage = GetWorkingImage()
		If workingImage IsNot Nothing Then
			SaveImageToFile(workingImage)
		End If
	End Sub

	Private Sub TsmiSaveReferenceImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiSaveReferenceImage.Click
		Dim finger As NFinger = CType(nfViewRight.Finger, NFinger)
		If finger IsNot Nothing AndAlso finger.Image IsNot Nothing Then
			SaveImageToFile(finger.Image)
		End If
	End Sub

	#End Region

	#Region "Event Handling"

	Private Sub MainFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If DesignMode Then
			Return
		End If
		Try
			_biometricClient = New NBiometricClient() With {.FingersQualityThreshold = 0, .FingersReturnBinarizedImage = True, .MatchingWithDetails = True}
			_biometricClient.SetProperty("Fingers.MinimalMinutiaCount", 0)
			openFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
			saveImageDialog.Filter = NImages.GetSaveFileFilterString()

			matchingFarComboBox.BeginUpdate()
			matchingFarComboBox.Items.Add(0.001.ToString("P1"))
			matchingFarComboBox.Items.Add(0.0001.ToString("P2"))
			matchingFarComboBox.Items.Add(0.00001.ToString("P3"))
			matchingFarComboBox.EndUpdate()
			matchingFarComboBox.SelectedIndex = 1

			LoadZoomCombo(tscbZoomLeft)
			LoadZoomCombo(tscbZoomRight)

			' select pointer tool by default
			nfViewLeft.ActiveTool = New NFingerView.PointerTool()
			_addMode = AddMode.None
		Catch ex As Exception
			Utilities.ShowError(ex.Message)
		End Try
	End Sub

	Private Sub NfView1IndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nfViewLeft.SelectedTreeMinutiaIndexChanged
		Dim args = TryCast(e, TreeMinutiaEventArgs)
		If nfViewRight IsNot Nothing AndAlso args IsNot Nothing Then
			nfViewRight.SelectedMinutiaIndex = args.Index
		End If
	End Sub

	Private Sub NfView2IndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nfViewRight.SelectedTreeMinutiaIndexChanged
		Dim args = TryCast(e, TreeMinutiaEventArgs)
		If nfViewLeft IsNot Nothing AndAlso args IsNot Nothing Then
			nfViewLeft.SelectedMinutiaIndex = args.Index
		End If
	End Sub

	#Region "Zooming"

	Private Sub LoadZoomCombo(ByVal comboBox As ToolStripComboBox)
		For Each zoom As Single In _zoomFactors
			comboBox.Items.Add(String.Format("{0:P0}", zoom))
		Next zoom
		comboBox.SelectedIndex = DefaultZoomFactor
	End Sub

	Private Function SetZoomFactor(ByVal nfView As NFingerView, ByVal currentZoomFactor As Integer, ByVal newZoomFactor As Integer) As Integer
		If newZoomFactor >= _zoomFactors.Length Then
			Return currentZoomFactor
		End If
		If newZoomFactor < 0 Then
			Return currentZoomFactor
		End If

		Dim zoom As Single = _zoomFactors(newZoomFactor)
		nfView.Zoom = zoom

		Return newZoomFactor
	End Function

	Private Sub TsmiZoomInLeftClick(ByVal sender As Object, ByVal e As EventArgs) Handles zoomInToolStripMenuItem.Click, tsbLeftZoomIn.Click, toolStripMenuItem9.Click
		_leftZoomFactor = SetZoomFactor(nfViewLeft, _leftZoomFactor, _leftZoomFactor + 1)
		tscbZoomLeft.SelectedIndex = _leftZoomFactor
	End Sub

	Private Sub TsmiZoomOutLeftClick(ByVal sender As Object, ByVal e As EventArgs) Handles zoomOutToolStripMenuItem.Click, tsbLeftZoomOut.Click, toolStripMenuItem10.Click
		_leftZoomFactor = SetZoomFactor(nfViewLeft, _leftZoomFactor, _leftZoomFactor - 1)
		tscbZoomLeft.SelectedIndex = _leftZoomFactor
	End Sub

	Private Sub TsmiZoomOriginalLeftClick(ByVal sender As Object, ByVal e As EventArgs) Handles originalToolStripMenuItem.Click, toolStripMenuItem11.Click
		_leftZoomFactor = SetZoomFactor(nfViewLeft, _leftZoomFactor, DefaultZoomFactor)
		tscbZoomLeft.SelectedIndex = _leftZoomFactor
	End Sub

	Private Sub TsmiZoomInRightClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiZoomInRight.Click, tsbRightZoomIn.Click, toolStripMenuItem6.Click
		_rightZoomFactor = SetZoomFactor(nfViewRight, _rightZoomFactor, _rightZoomFactor + 1)
		tscbZoomRight.SelectedIndex = _rightZoomFactor
	End Sub

	Private Sub TsmiZoomOutRightClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiZoomOutRight.Click, tsbRightZoomOut.Click, toolStripMenuItem7.Click
		_rightZoomFactor = SetZoomFactor(nfViewRight, _rightZoomFactor, _rightZoomFactor - 1)
		tscbZoomRight.SelectedIndex = _rightZoomFactor
	End Sub

	Private Sub TsmiZoomOriginalRightClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiZoomOriginalRight.Click, toolStripMenuItem8.Click
		_rightZoomFactor = SetZoomFactor(nfViewRight, _rightZoomFactor, DefaultZoomFactor)
		tscbZoomRight.SelectedIndex = _rightZoomFactor
	End Sub

	Private Sub TscbZoomLeftSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tscbZoomLeft.SelectedIndexChanged
		_leftZoomFactor = SetZoomFactor(nfViewLeft, _leftZoomFactor, tscbZoomLeft.SelectedIndex)
	End Sub

	Private Sub TscbZoomRightSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tscbZoomRight.SelectedIndexChanged
		_rightZoomFactor = SetZoomFactor(nfViewRight, _rightZoomFactor, tscbZoomRight.SelectedIndex)
	End Sub

	#End Region

	Private Sub TsmiViewOriginalLeftClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiViewOriginalLeft.Click
		tsmiViewOriginalLeft.Checked = Not tsmiViewOriginalLeft.Checked
		nfViewLeft.ShownImage = If(tsmiViewOriginalLeft.Checked, ShownImage.Original, ShownImage.Result)
		nfViewLeft.Invalidate()
	End Sub

	Private Sub TsmiViewOriginalRightClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiViewOriginalRight.Click
		tsmiViewOriginalRight.Checked = Not tsmiViewOriginalRight.Checked
		nfViewRight.ShownImage = If(tsmiViewOriginalRight.Checked, ShownImage.Original, ShownImage.Result)
		nfViewRight.Invalidate()
	End Sub

	Private Sub RbPointerToolCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbPointerTool.CheckedChanged
		If rbPointerTool.Checked Then
			nfViewLeft.ActiveTool = New NFingerView.PointerTool()
			_addMode = AddMode.None
		End If
	End Sub

	Private Sub RbSelectAreaToolCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbSelectAreaTool.CheckedChanged
		If rbSelectAreaTool.Checked Then
			nfViewLeft.ActiveTool = New NFingerView.RectangleSelectionTool()
			_addMode = AddMode.None
		End If
	End Sub

	Private Sub RbAddEndMinutiaToolCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbAddEndMinutiaTool.CheckedChanged
		If rbAddEndMinutiaTool.Checked Then
			Dim addFeatTool = New NFingerView.AddFeaturesTool()
			AddHandler addFeatTool.FeatureAddCompleted, AddressOf NfViewLeftFeatureAddCompleted
			nfViewLeft.ActiveTool = addFeatTool
			_addMode = AddMode.EndMinutia
		End If
	End Sub

	Private Sub RbAddBifurcationMinutiaCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbAddBifurcationMinutia.CheckedChanged
		If rbAddBifurcationMinutia.Checked Then
			Dim addFeatTool = New NFingerView.AddFeaturesTool()
			AddHandler addFeatTool.FeatureAddCompleted, AddressOf NfViewLeftFeatureAddCompleted
			nfViewLeft.ActiveTool = addFeatTool
			_addMode = AddMode.BifurcationMinutia
		End If
	End Sub

	Private Sub RbAddDeltaToolCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbAddDeltaTool.CheckedChanged
		If rbAddDeltaTool.Checked Then
			Dim addFeatTool = New NFingerView.AddFeaturesTool(False)
			AddHandler addFeatTool.FeatureAddCompleted, AddressOf NfViewLeftFeatureAddCompleted
			nfViewLeft.ActiveTool = addFeatTool
			_addMode = AddMode.Delta
		End If
	End Sub

	Private Sub RbAddCoreToolCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbAddCoreTool.CheckedChanged
		If rbAddCoreTool.Checked Then
			Dim addFeatTool = New NFingerView.AddFeaturesTool()
			AddHandler addFeatTool.FeatureAddCompleted, AddressOf NfViewLeftFeatureAddCompleted
			nfViewLeft.ActiveTool = addFeatTool
			_addMode = AddMode.Core
		End If
	End Sub

	Private Sub RbAddDoubleCoreToolCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbAddDoubleCoreTool.CheckedChanged
		If rbAddDoubleCoreTool.Checked Then
			Dim addFeatTool = New NFingerView.AddFeaturesTool(False)
			AddHandler addFeatTool.FeatureAddCompleted, AddressOf NfViewLeftFeatureAddCompleted
			nfViewLeft.ActiveTool = addFeatTool
			_addMode = AddMode.DoubleCore
		End If
	End Sub

	Private Sub TsmiFileExitClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiFileExit.Click
		Close()
	End Sub

	Private Sub MatchingFarComboBoxValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles matchingFarComboBox.Validating
		Try
			Dim matchingThreshold As Integer = Utilities.MatchingThresholdFromString(matchingFarComboBox.Text)
			matchingFarComboBox.Text = Utilities.MatchingThresholdToString(matchingThreshold)
			_biometricClient.MatchingThreshold = matchingThreshold
		Catch
			errorProvider1.SetError(matchingFarComboBox, "Matching threshold is invalid")
			e.Cancel = True
		End Try
	End Sub

	Private Sub MatchingFarComboBoxValidated(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Validated
		errorProvider1.SetError(matchingFarComboBox, "")
	End Sub

	Private Sub TsmiHelpAboutClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiHelpAbout.Click
		Gui.AboutBox.Show()
	End Sub

	Private Sub TsmiPerformBandpassFilteringClick(ByVal sender As Object, ByVal e As EventArgs) Handles performBandpassFilteringToolStripMenuItem.Click
		Dim workingImage As NImage = GetWorkingImage()
		If workingImage IsNot Nothing Then
			Dim fft As BandpassFilteringForm
			Try
				fft = New BandpassFilteringForm(workingImage)
			Catch e1 As Exception
				Return
			End Try

			If fft.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
				Return
			End If
			_imageLeftOriginal = NImage.FromImage(NPixelFormat.Rgb8U, 0, fft.ResultImage)
			ProcessImage()
			nfViewLeft.ShownImage = ShownImage.Original
			nfViewLeft.Invalidate()
		End If
	End Sub

	#End Region
End Class
