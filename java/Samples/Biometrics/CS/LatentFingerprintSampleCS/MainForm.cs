using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Gui;
using Neurotec.Images;
using Neurotec.Images.Processing;
using Neurotec.IO;
using Neurotec.Licensing;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Public constructor

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;

		private NImage _imageLeft;
		private NImage _imageLeftOriginal;
		private NImage _binarizedImage;
		private NFRecord _record;

		private bool? _matcherLicenseAvailable = null;

		private readonly double[] _brightness = new double[3];
		private readonly double[] _contrast = new double[3];
		private bool _invert;
		private bool _convertToGrayscale;
		private bool _processingRequested;

		private readonly float[] _zoomFactors = { .25f, .33f, .50f, .66f, .80f, 1f, 1.25f, 1.5f, 2.0f, 2.5f, 3.0f };
		private const int DefaultZoomFactor = 5;
		private int _leftZoomFactor = DefaultZoomFactor;
		private int _rightZoomFactor = DefaultZoomFactor;

		private enum AddMode
		{
			None,
			EndMinutia,
			BifurcationMinutia,
			Delta,
			Core,
			DoubleCore,
		}
		private AddMode _addMode = AddMode.None;

		#endregion

		#region Image Loading

		private bool ResolutionCheck(NImage image, ref float horzResolution, ref float vertResolution)
		{
			var frmResolution = new ResolutionForm
			{
				HorzResolution = horzResolution,
				VertResolution = vertResolution,
				FingerImage = image.ToBitmap()
			};

			if (frmResolution.ShowDialog() == DialogResult.OK)
			{
				horzResolution = frmResolution.HorzResolution;
				vertResolution = frmResolution.VertResolution;
				return true;
			}
			horzResolution = image.HorzResolution;
			vertResolution = image.VertResolution;
			return false;
		}

		private void BtnLeftClick(object sender, EventArgs e)
		{
			openFileDialog.FileName = null;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (NImage image = NImage.FromFile(openFileDialog.FileName))
					{
						float horzResolution = image.HorzResolution;
						if (horzResolution < 250.0f) horzResolution = 500f;
						float vertResolution = image.VertResolution;
						if (vertResolution < 250.0f) vertResolution = 500f;
						if (ResolutionCheck(image, ref horzResolution, ref vertResolution))
						{
							nfViewLeft.Tree = null;
							nfViewRight.Tree = null;

							NFinger finger = (NFinger)nfViewLeft.Finger;
							nfViewLeft.Finger = null;
							if (finger != null) finger.Dispose();
							nfViewLeft.ClearSelectedArea();

							if (_record != null) _record.Dispose();
							_record = null;

							if (_imageLeft != null)
							{
								_imageLeft.Dispose();
								_imageLeft = null;
							}
							if (_binarizedImage != null)
							{
								_binarizedImage.Dispose();
								_binarizedImage = null;
							}

							tsbExtractLeft.Enabled = false;
							btnMatcher.Enabled = false;

							_imageLeft = NImage.FromImage(NPixelFormat.Rgb8U, 0, image);
							_imageLeft.HorzResolution = horzResolution;
							_imageLeft.VertResolution = vertResolution;
							_imageLeft.ResolutionIsAspectRatio = false;

							nfViewLeft.ShownImage = ShownImage.Original;
							tsmiViewOriginalLeft.Checked = true;
							TsmiZoomOriginalLeftClick(this, EventArgs.Empty);

							_imageLeftOriginal = (NImage)_imageLeft.Clone();

							lblLatentSize.Text = string.Format("Size: {0} x {1}", _imageLeftOriginal.Width, _imageLeftOriginal.Height);
							lblLatentResolution.Text = string.Format("Resolution: {0:F2} x {1:F2}", _imageLeftOriginal.HorzResolution, _imageLeftOriginal.VertResolution);

							lblLeftFilename.Text = openFileDialog.FileName;

							tsbExtractLeft.Enabled = true;

							BtnResetAllClick(this, EventArgs.Empty);
						}
					}
				}
				catch (Exception ex)
				{
					Utilities.ShowError("Error opening file \"{0}\": {1}", openFileDialog.FileName, ex.Message);
				}
			}
		}

		private void BtnRightClick(object sender, EventArgs e)
		{
			openFileDialog.FileName = null;

			if (openFileDialog.ShowDialog() != DialogResult.OK) return;
			try
			{
				// read image
				NImage tmp = NImage.FromFile(openFileDialog.FileName);

				float horzResolution = tmp.HorzResolution;
				if (horzResolution < 250.0f) horzResolution = 500f;
				float vertResolution = tmp.VertResolution;
				if (vertResolution < 250.0f) vertResolution = 500f;
				if (ResolutionCheck(tmp, ref horzResolution, ref vertResolution))
				{
					nfViewRight.Tree = null;
					nfViewLeft.Tree = null;

					NFinger finger = (NFinger)nfViewRight.Finger;
					nfViewRight.Finger = null;
					if (finger != null) finger.Dispose();

					nfViewRight.ClearSelectedArea();

					btnMatcher.Enabled = false;

					NImage image = NImage.FromImage(NPixelFormat.Rgb8U, 0, tmp);
					image.HorzResolution = horzResolution;
					image.VertResolution = vertResolution;
					image.ResolutionIsAspectRatio = false;

					tmp.Dispose();

					lblReferenceSize.Text = string.Format("Size: {0} x {1}", image.Width, image.Height);
					lblReferenceResolution.Text = string.Format("Resolution: {0:F2} x {1:F2}", image.HorzResolution, image.VertResolution);

					lblRightFilename.Text = openFileDialog.FileName;

					finger = new NFinger { Image = image };
					nfViewRight.Finger = finger;
					nfViewRight.ShownImage = ShownImage.Original;
					tsmiViewOriginalRight.Checked = true;
					TsmiZoomOriginalRightClick(this, EventArgs.Empty);

					if (image != null)
					{
						nfViewRight.Tree = null;
						nfViewLeft.Tree = null;

						using (NSubject subject = new NSubject())
						{
							subject.Fingers.Add(finger);

							NBiometricTask task = _biometricClient.CreateTask(NBiometricOperations.CreateTemplate, subject);
							LongActionDialog.ShowDialog(this, "Extracting ...", new Action<NBiometricTask>(_biometricClient.PerformTask), task);
							if (task.Error != null)
							{
								Utilities.ShowError(task.Error);
								return;
							}

							var status = task.Status;
							if (status == NBiometricStatus.Ok)
							{
								NFinger leftFinger = (NFinger)nfViewLeft.Finger;
								if (leftFinger != null && leftFinger.Status == NBiometricStatus.Ok)
								{
									btnMatcher.Enabled = true;
								}
							}
							else
							{
								Utilities.ShowError(@"Failed to create template: {0}", status);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Utilities.ShowError("Error opening or extracting file \"{0}\": {1}", openFileDialog.FileName, ex.Message);
			}
		}

		#endregion

		#region Image Processing

		private void NfViewLeftPaint(object sender, PaintEventArgs e)
		{
			if (_processingRequested)
			{
				ProcessImage();
			}
		}

		private void CbInvertCheckedChanged(object sender, EventArgs e)
		{
			_invert = cbInvert.Checked;
			RequestImageProcessing();
		}

		private void CbGrayscaleCheckedChanged(object sender, EventArgs e)
		{
			_convertToGrayscale = cbGrayscale.Checked;
			RequestImageProcessing();
		}

		private void BrightnessValueChanged(object sender, EventArgs e)
		{
			var colorSlider = sender as ColorSlider;
			if (colorSlider == null) return;
			var normalizedValue = -colorSlider.Value;
			if (cbGroupBrightnessSliders.Checked)
			{
				lblBrightnessR.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
				lblBrightnessG.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
				lblBrightnessB.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);

				if (sliderBrightnessRed.Value != colorSlider.Value)
				{
					sliderBrightnessRed.Value = colorSlider.Value;
				}
				if (sliderBrightnessGreen.Value != colorSlider.Value)
				{
					sliderBrightnessGreen.Value = colorSlider.Value;
				}
				if (sliderBrightnessBlue.Value != colorSlider.Value)
				{
					sliderBrightnessBlue.Value = colorSlider.Value;
				}
				for (int i = 0; i < 3; i++)
				{
					_brightness[i] = normalizedValue / 100.0;
				}
			}
			else
			{
				_brightness[Convert.ToInt32(colorSlider.Tag)] = normalizedValue / 100.0;

				switch (Convert.ToInt32(colorSlider.Tag))
				{
					case 0:
						lblBrightnessR.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
						break;

					case 1:
						lblBrightnessG.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
						break;

					case 2:
						lblBrightnessB.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
						break;
				}
			}
			RequestImageProcessing();
		}

		private void ContrastValueChanged(object sender, EventArgs e)
		{
			var colorSlider = sender as ColorSlider;
			if (colorSlider == null) return;
			var normalizedValue = -colorSlider.Value;
			if (cbGroupContrastSliders.Checked)
			{
				lblContrastRValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
				lblContrastGValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
				lblContrastBValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);

				if (sliderContrastRed.Value != colorSlider.Value)
				{
					sliderContrastRed.Value = colorSlider.Value;
				}
				if (sliderContrastGreen.Value != colorSlider.Value)
				{
					sliderContrastGreen.Value = colorSlider.Value;
				}
				if (sliderContrastBlue.Value != colorSlider.Value)
				{
					sliderContrastBlue.Value = colorSlider.Value;
				}

				for (int i = 0; i < 3; i++)
				{
					_contrast[i] = normalizedValue / 100.0;
				}
			}
			else
			{
				_contrast[Convert.ToInt32(colorSlider.Tag)] = normalizedValue / 100.0;

				switch (Convert.ToInt32(colorSlider.Tag))
				{
					case 0:
						lblContrastRValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
						break;

					case 1:
						lblContrastGValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
						break;

					case 2:
						lblContrastBValue.Text = normalizedValue.ToString(CultureInfo.InvariantCulture);
						break;
				}
			}
			RequestImageProcessing();
		}

		private void RequestImageProcessing()
		{
			_processingRequested = true;
			nfViewLeft.Invalidate();
		}

		private void ProcessImage()
		{
			if (_imageLeftOriginal == null) return;
			var img = (NImage)_imageLeftOriginal.Clone();

			if (_invert)
			{
				Nrgbip.InvertSame(img);
			}

			if (Math.Abs(_brightness[0]) > Double.Epsilon
				|| Math.Abs(_brightness[1]) > Double.Epsilon
				|| Math.Abs(_brightness[2]) > Double.Epsilon
				|| Math.Abs(_contrast[0]) > Double.Epsilon
				|| Math.Abs(_contrast[1]) > Double.Epsilon
				|| Math.Abs(_contrast[2]) > Double.Epsilon)
			{
				Nrgbip.AdjustBrightnessContrastSame(img, _brightness[0], _contrast[0], _brightness[1], _contrast[1], _brightness[2], _contrast[2]);
			}

			if (_convertToGrayscale)
			{
				NImage grayImage = NImage.FromImage(NPixelFormat.Grayscale8U, 0, img);
				img = NImages.GetGrayscaleColorWrapper(grayImage, new NRgb(), new NRgb(255, 255, 255));
			}

			var oldFinger = nfViewLeft.Finger;
			NFrictionRidge newFinger;
			if (_record != null)
				newFinger = NFinger.FromImageAndTemplate(img, _record);
			else
				newFinger = new NFinger { Image = img };
			newFinger.BinarizedImage = _binarizedImage;
			nfViewLeft.Finger = newFinger;

			if (oldFinger != null) oldFinger.Dispose();

			nfViewLeft.Invalidate();

			if (_imageLeft != null)
			{
				_imageLeft.Dispose();
			}
			_imageLeft = img;

			_processingRequested = false;
		}

		private void BtnResetBrightnessClick(object sender, EventArgs e)
		{
			sliderBrightnessRed.Value = 0;
			sliderBrightnessGreen.Value = 0;
			sliderBrightnessBlue.Value = 0;

			RequestImageProcessing();
		}

		private void BtnResetContrastClick(object sender, EventArgs e)
		{
			sliderContrastRed.Value = 0;
			sliderContrastGreen.Value = 0;
			sliderContrastBlue.Value = 0;

			RequestImageProcessing();
		}

		private void BtnResetAllClick(object sender, EventArgs e)
		{
			cbInvert.Checked = false;

			sliderBrightnessRed.Value = 0;
			sliderBrightnessGreen.Value = 0;
			sliderBrightnessBlue.Value = 0;

			sliderContrastRed.Value = 0;
			sliderContrastGreen.Value = 0;
			sliderContrastBlue.Value = 0;

			cbGrayscale.Checked = false;

			RequestImageProcessing();
		}

		#endregion

		#region Image Transformations

		private void OnRecordChanged(NFRecord value)
		{
			NFinger finger = (NFinger)nfViewLeft.Finger;
			if (finger != null)
			{
				NFAttributes attributes = finger.Objects.FirstOrDefault();
				if (attributes != null) attributes.Template = value;
			}
			_record = value;
		}

		private void TsmiRotate90cwClick(object sender, EventArgs e)
		{
			if (_imageLeftOriginal != null)
			{
				NImage transformed = _imageLeftOriginal.RotateFlip(NImageRotateFlipType.Rotate90);
				if (_record != null)
					OnRecordChanged(TransformFeatures.Rotate90(_record));

				OnImageTransformed(transformed);
			}
		}

		private void TsmiRotate90ccwClick(object sender, EventArgs e)
		{
			if (_imageLeftOriginal != null)
			{
				NImage transformed = _imageLeftOriginal.RotateFlip(NImageRotateFlipType.Rotate270);
				if (_record != null)
					OnRecordChanged(TransformFeatures.Rotate270(_record));

				OnImageTransformed(transformed);
			}
		}

		private void TsmiRotate180Click(object sender, EventArgs e)
		{
			if (_imageLeftOriginal != null)
			{
				NImage transformed = _imageLeftOriginal.RotateFlip(NImageRotateFlipType.Rotate180);
				if (_record != null)
				{
					OnRecordChanged(TransformFeatures.Rotate180(_record));
				}

				OnImageTransformed(transformed);
			}
		}

		private void TsmiFlipHorzClick(object sender, EventArgs e)
		{
			if (_imageLeftOriginal != null)
			{
				_imageLeftOriginal.FlipHorizontally();
				if (_record != null)
					OnRecordChanged(TransformFeatures.FlipHorizontally(_record));

				OnImageTransformed(null);
			}
		}

		private void TsmiFlipVertClick(object sender, EventArgs e)
		{
			if (_imageLeftOriginal != null)
			{
				_imageLeftOriginal.FlipVertically();
				if (_record != null)
					OnRecordChanged(TransformFeatures.FlipVertically(_record));
				OnImageTransformed(null);
			}
		}

		private void TsmiCropToSelClick(object sender, EventArgs e)
		{
			if (_imageLeftOriginal != null)
			{
				if (!nfViewLeft.IsPartOfImageSelected)
				{
					Utilities.ShowInformation(@"Please select part of image with Area Selection Tool first!");
					return;
				}
				Rectangle rect = nfViewLeft.SelectedImageArea;
				if (rect.X < 0 || rect.Y < 0 || rect.X + rect.Width > _imageLeftOriginal.Width || rect.Y + rect.Height > _imageLeftOriginal.Height)
				{
					Utilities.ShowInformation(@"Please select part of image with Area Selection Tool first (only image)!");
					return;
				}

				NImage transformed = _imageLeftOriginal.Crop((uint)rect.X, (uint)rect.Y, (uint)rect.Width, (uint)rect.Height);
				if (_record != null)
				{
					double coeffX = NFRecord.Resolution / (double)_imageLeftOriginal.HorzResolution;
					double coeffY = NFRecord.Resolution / (double)_imageLeftOriginal.VertResolution;
					nfViewLeft.Tree = nfViewRight.Tree = null;
					OnRecordChanged(TransformFeatures.Crop(_record, transformed, (uint)(rect.X * coeffX), (uint)(rect.Y * coeffY), (uint)Math.Ceiling(rect.Width * coeffX), (uint)Math.Ceiling(rect.Height * coeffY)));
				}

				OnImageTransformed(transformed);
			}
		}

		private void OnImageTransformed(NImage transformed)
		{
			nfViewLeft.ClearSelectedArea();

			if (transformed != null)
			{
				if (_imageLeftOriginal != null)
				{
					_imageLeftOriginal.Dispose();
				}
				_imageLeftOriginal = transformed;
			}
			ProcessImage();
		}

		private void TsmiInvertMinutiaeClick(object sender, EventArgs e)
		{
			if (_record != null)
			{
				var minutiae = _record.Minutiae.ToArray();
				_record.Minutiae.Clear();
				for (int i = 0; i < minutiae.Length; i++)
				{
					var minutia = minutiae[i];
					if (minutia.Type == NFMinutiaType.Bifurcation)
					{
						minutia.Type = NFMinutiaType.End;
					}
					else if (minutia.Type == NFMinutiaType.End)
					{
						minutia.Type = NFMinutiaType.Bifurcation;
					}
					_record.Minutiae.Add(minutia);
				}
				nfViewLeft.Invalidate();
			}
		}

		#endregion

		#region Extraction

		private NImage GetWorkingImage()
		{
			return rbUseEditedImage.Checked ? _imageLeft : _imageLeftOriginal;
		}

		private NFRecord moveTemplateCoordinates(NFRecord inData, ushort width, ushort height, ushort horzResolution, ushort vertResolution, int offsetX, int offsetY)
		{
			if (inData == null) return null;

			double coeffX = NFRecord.Resolution / (double)horzResolution;
			double coeffY = NFRecord.Resolution / (double)vertResolution;

			var record = new NFRecord(width, height, horzResolution, vertResolution)
			{
				MinutiaFormat = inData.MinutiaFormat,
				CbeffProductType = inData.CbeffProductType,
				RidgeCountsType = inData.RidgeCountsType
			};

			var templateWidth = (int)(width * coeffX);
			var templateHeight = (int)(height * coeffY);
			for (int i = 0; i < inData.Minutiae.Count; i++)
			{
				NFMinutia min = inData.Minutiae[i];
				min.X += (ushort)(offsetX * coeffX);
				min.Y += (ushort)(offsetY * coeffY);
				if (min.X >= 0 && min.X < templateWidth && min.Y >= 0 && min.Y < templateHeight)
					record.Minutiae.Add(min);
			}
			for (int i = 0; i < inData.Deltas.Count; i++)
			{
				NFDelta delta = inData.Deltas[i];
				delta.X += (ushort)(offsetX * coeffX);
				delta.Y += (ushort)(offsetY * coeffY);
				if (delta.X >= 0 && delta.X < templateWidth && delta.Y >= 0 && delta.Y < templateHeight)
					record.Deltas.Add(delta);
			}
			for (int i = 0; i < inData.Cores.Count; i++)
			{
				NFCore core = inData.Cores[i];
				core.X += (ushort)(offsetX * coeffX);
				core.Y += (ushort)(offsetY * coeffY);
				if (core.X >= 0 && core.X < templateWidth && core.Y >= 0 && core.Y < templateHeight)
					record.Cores.Add(core);
			}
			for (int i = 0; i < inData.DoubleCores.Count; i++)
			{
				NFDoubleCore doubleCore = inData.DoubleCores[i];
				doubleCore.X += (ushort)(offsetX * coeffX);
				doubleCore.Y += (ushort)(offsetY * coeffY);
				if (doubleCore.X >= 0 && doubleCore.X < templateWidth && doubleCore.Y >= 0 && doubleCore.Y < templateHeight)
					record.DoubleCores.Add(doubleCore);
			}

			return record;
		}

		private void ExtractImage(NImage image)
		{
			try
			{
				nfViewRight.Tree = null;
				nfViewLeft.Tree = null;

				bool needCropping = nfViewLeft.IsPartOfImageSelected;
				var cropBounds = new Rectangle();
				if (needCropping)
				{
					cropBounds = nfViewLeft.SelectedImageArea;
				}

				NImage workingImage = image;
				if (needCropping)
				{
					workingImage = image.Crop((uint)cropBounds.X, (uint)cropBounds.Y, (uint)cropBounds.Width, (uint)cropBounds.Height);
				}

				NSubject subject = new NSubject();
				NFinger finger = new NFinger { Image = workingImage };
				subject.Fingers.Add(finger);

				NBiometricTask task = _biometricClient.CreateTask(NBiometricOperations.CreateTemplate, subject);
				LongActionDialog.ShowDialog(this, "Extracting ...", new Action<NBiometricTask>(_biometricClient.PerformTask), task);
				if (task.Error != null)
				{
					Utilities.ShowError(task.Error);
					return;
				}

				NBiometricStatus status = task.Status;
				if (status == NBiometricStatus.Ok)
				{
					NFRecord record = finger.Objects.First().Template;
					if (needCropping)
					{
						NImage resultImage = image.Clone() as NImage;
						using (var binarizedImage = NImage.FromImage(NPixelFormat.Rgb8U, resultImage.Stride, finger.BinarizedImage))
						{
							binarizedImage.CopyTo(resultImage, (uint)cropBounds.Left, (uint)cropBounds.Top);
						}
						_binarizedImage = resultImage;
						OnRecordChanged(moveTemplateCoordinates(record, (ushort)image.Width, (ushort)image.Height, (ushort)image.HorzResolution, (ushort)image.VertResolution, (ushort)cropBounds.X, (ushort)cropBounds.Y));
					}
					else
					{
						_binarizedImage = finger.BinarizedImage;
						OnRecordChanged(record);
					}

					NFinger fingerRight = (NFinger)nfViewRight.Finger;
					if (fingerRight != null && fingerRight.Status == NBiometricStatus.Ok)
					{
						btnMatcher.Enabled = true;
					}
				}
				else
				{
					if (_record != null)
					{
						nfViewLeft.SelectedDeltaIndex = nfViewLeft.SelectedMinutiaIndex = nfViewLeft.SelectedDoubleCoreIndex = nfViewLeft.SelectedDeltaIndex = -1;
						NFRecord newRecord = new NFRecord(_record.Width, _record.Height, _record.HorzResolution, _record.VertResolution)
						{
							CbeffProductType = _record.CbeffProductType,
							MinutiaFormat = _record.MinutiaFormat,
							RidgeCountsType = _record.RidgeCountsType
						};
						OnRecordChanged(newRecord);
					}
					_binarizedImage = null;
					Utilities.ShowError(@"Fingerprint extraction failed: {0}", status);
				}
				RequestImageProcessing();
			}
			catch (Exception ex)
			{
				Utilities.ShowError(@"Extraction error: {0}", ex.Message);
			}
		}

		private void UpdateRecord(NImage workingImage)
		{
			NSubject subject = new NSubject();
			NFinger finger = (NFinger)NFinger.FromImageAndTemplate(workingImage, _record);
			subject.Fingers.Add(finger);
			_biometricClient.CreateTemplate(subject); // Update template
			nfViewLeft.Invalidate();
		}

		private void TsbExtractLeftClick(object sender, EventArgs e)
		{
			NImage workingImage = GetWorkingImage();
			if (workingImage != null)
			{
				ExtractImage(workingImage);
			}
		}

		private void TsmiExtractionSettingsClick(object sender, EventArgs e)
		{
			var frmExtractionSettings = new ExtractionSettingsForm
			{
				QualityThreshold = _biometricClient.FingersQualityThreshold
			};
			if (frmExtractionSettings.ShowDialog() == DialogResult.OK)
			{
				_biometricClient.FingersQualityThreshold = frmExtractionSettings.QualityThreshold;
			}
		}

		#endregion

		#region Matching

		private void BtnMatcherClick(object sender, EventArgs e)
		{
			if (!_matcherLicenseAvailable.HasValue)
			{
				_matcherLicenseAvailable = ObtainMatchingLicense();
				if (_matcherLicenseAvailable == false) return;
			}
			else if (_matcherLicenseAvailable == false)
			{
				Utilities.ShowInformation(@"Matching license not available");
				return;
			}

			try
			{
				NImage workingImage = GetWorkingImage();

				lblMatchingScore.Text = @"Matching...";
				lblMatchingScore.Refresh();

				NFRecord rightRec = nfViewRight.Finger.Objects.First().Template;
				if (_record.RequiresUpdate)
				{
					UpdateRecord(workingImage);
				}

				NSubject subjectRight = new NSubject();
				NSubject subjectLeft = new NSubject();

				subjectLeft.SetTemplateBuffer(_record.Save());
				subjectRight.SetTemplateBuffer(rightRec.Save());

				NBiometricStatus status = _biometricClient.Verify(subjectLeft, subjectRight);
				NIndexPair[] matedMinutiae = null;
				int score = 0;
				if (status == NBiometricStatus.Ok)
				{
					var result = subjectLeft.MatchingResults.First();
					var details = result.MatchingDetails;
					score = result.Score;
					matedMinutiae = details.Fingers.First().GetMatedMinutiae();
				}
				nfViewLeft.MatedMinutiae = matedMinutiae;
				nfViewRight.MatedMinutiae = matedMinutiae;
				if (score > 0)
				{
					nfViewLeft.PrepareTree();
					nfViewRight.Tree = nfViewLeft.Tree;
				}
				else
				{
					Utilities.ShowInformation(@"Fingerprints do not match.");
				}

				lblMatchingScore.Text = string.Format("Score: {0}", score);
			}
			catch (Exception ex)
			{
				lblMatchingScore.Text = @"Matching failed";
				Utilities.ShowError(@"Error while matching. Please check matching settings. Reason: {0}", ex.Message);
			}
		}

		private bool ObtainMatchingLicense()
		{
			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, "Biometrics.FingerMatching"))
				{
					Utilities.ShowWarning(@"Could not obtain license for fingerprint matcher.");
					return false;
				}
				return true;
			}
			catch (Exception ex)
			{
				Utilities.ShowError(@"Failed while obtaining matching license: {0}", ex.Message);
				return false;
			}
		}

		#endregion Matching

		#region Add Features

		private void NfViewLeftFeatureAddCompleted(object sender, EventArgs e)
		{
			NFingerView.AddFeaturesTool.FeatureAddCompletedEventArgs featureAddEvent = e as NFingerView.AddFeaturesTool.FeatureAddCompletedEventArgs;
			if (featureAddEvent != null)
			{
				AddFeature(featureAddEvent.Start, featureAddEvent.End);
			}
		}

		private void BitmapToTemplateCoords(ref Point pt)
		{
			var vertResolution = _imageLeftOriginal.HorzResolution;
			var horzResolution = _imageLeftOriginal.VertResolution;
			if (vertResolution < 250) vertResolution = 500;
			if (horzResolution < 250) horzResolution = 500;
			pt.X = (ushort)(pt.X * NFRecord.Resolution / horzResolution);
			pt.Y = (ushort)(pt.Y * NFRecord.Resolution / vertResolution);
		}

		private static double CalculateDraggedAngle(Point pt, Point dragPt)
		{
			return Math.Atan2(dragPt.Y - pt.Y, dragPt.X - pt.X);
		}

		private bool AddFeature(Point pt, Point dragPt)
		{
			if (_imageLeftOriginal != null)
			{
				try
				{
					if (_record == null)
					{
						// create a new template if not exists
						var templateWidth = (ushort)_imageLeftOriginal.Width;
						var templateHeight = (ushort)_imageLeftOriginal.Height;
						var vertResolution = _imageLeftOriginal.VertResolution;
						var horzResolution = _imageLeftOriginal.HorzResolution;
						if (vertResolution < 250) vertResolution = NFRecord.Resolution;
						if (horzResolution < 250) horzResolution = NFRecord.Resolution;

						_record = new NFRecord(templateWidth, templateHeight, (ushort)vertResolution, (ushort)horzResolution)
						{
							CbeffProductType = 256,
							MinutiaFormat = NFMinutiaFormat.HasQuality | NFMinutiaFormat.HasG | NFMinutiaFormat.HasCurvature,
							RidgeCountsType = NFRidgeCountsType.EightNeighborsWithIndexes
						};
						ProcessImage();
					}

					double angle = CalculateDraggedAngle(pt, dragPt);
					BitmapToTemplateCoords(ref pt);

					var addX = (ushort)pt.X;
					var addY = (ushort)pt.Y;

					int index = -1;
					switch (_addMode)
					{
						case AddMode.EndMinutia:
							index = _record.Minutiae.Add(new NFMinutia(addX, addY, NFMinutiaType.End, angle));
							break;
						case AddMode.BifurcationMinutia:
							index = _record.Minutiae.Add(new NFMinutia(addX, addY, NFMinutiaType.Bifurcation, angle));
							break;
						case AddMode.Delta:
							index = _record.Deltas.Add(new NFDelta(addX, addY));
							break;
						case AddMode.Core:
							index = _record.Cores.Add(new NFCore(addX, addY, angle));
							break;
						case AddMode.DoubleCore:
							index = _record.DoubleCores.Add(new NFDoubleCore(addX, addY));
							break;
					}

					if (index != -1)
					{
						switch (_addMode)
						{
							case AddMode.EndMinutia:
							case AddMode.BifurcationMinutia:
								nfViewLeft.SelectedMinutiaIndex = index;
								break;
							case AddMode.Delta:
								nfViewLeft.SelectedDeltaIndex = index;
								break;
							case AddMode.Core:
								nfViewLeft.SelectedCoreIndex = index;
								break;
							case AddMode.DoubleCore:
								nfViewLeft.SelectedDoubleCoreIndex = index;
								break;
						}
						nfViewLeft.Invalidate();
					}

					NFinger fingerRight = (NFinger)nfViewRight.Finger;
					if (fingerRight != null && fingerRight.Status == NBiometricStatus.Ok)
					{
						btnMatcher.Enabled = true;
					}
					return true;
				}
				catch (Exception ex)
				{
					Utilities.ShowError("Failed to add feature: {0}", ex.Message);
					return false;
				}
			}
			Utilities.ShowInformation(@"Please open an image before editing!");
			return false;
		}

		#endregion

		#region Delete Features

		private void ContextMenuLeftOpening(object sender, CancelEventArgs e)
		{
			e.Cancel = nfViewLeft.ActiveTool is NFingerView.AddFeaturesTool;
			if (!e.Cancel)
			{
				tsmiDeleteFeature.Enabled = (nfViewLeft.SelectedMinutiaIndex >= 0
					|| nfViewLeft.SelectedDeltaIndex >= 0
					|| nfViewLeft.SelectedCoreIndex >= 0
					|| nfViewLeft.SelectedDoubleCoreIndex >= 0);
			}
		}

		private void TsmiDeleteFeatureClick(object sender, EventArgs e)
		{
			NFinger finger = (NFinger)nfViewLeft.Finger;
			if (finger != null)
			{
				if (_record != null)
				{
					bool deleted = false;
					int index = nfViewLeft.SelectedMinutiaIndex;
					if (index >= 0)
					{
						nfViewLeft.SelectedMinutiaIndex = -1;
						_record.Minutiae.RemoveAt(index);
						deleted = true;
					}
					index = nfViewLeft.SelectedDeltaIndex;
					if (index >= 0)
					{
						nfViewLeft.SelectedDeltaIndex = -1;
						_record.Deltas.RemoveAt(index);
						deleted = true;
					}
					index = nfViewLeft.SelectedCoreIndex;
					if (index >= 0)
					{
						nfViewLeft.SelectedCoreIndex = -1;
						_record.Cores.RemoveAt(index);
						deleted = true;
					}
					index = nfViewLeft.SelectedDoubleCoreIndex;
					if (index >= 0)
					{
						nfViewLeft.SelectedDoubleCoreIndex = -1;
						_record.DoubleCores.RemoveAt(index);
						deleted = true;
					}

					if (deleted)
					{
						nfViewRight.Tree = null;
						nfViewLeft.Tree = null;

						nfViewLeft.Invalidate();
					}
				}
			}
		}

		#endregion

		#region Save File

		private void TsbSaveTemplateClick(object sender, EventArgs e)
		{
			NImage workingImage = GetWorkingImage();

			if (workingImage != null)
			{
				if (_record != null)
				{
					if (saveTemplateDialog.ShowDialog() != DialogResult.OK) return;
					try
					{
						if (_record.RequiresUpdate)
						{
							UpdateRecord(workingImage);
						}
						//save template to file (*.data)
						using (NBuffer buffer = _record.Save())
						{
							File.WriteAllBytes(saveTemplateDialog.FileName, buffer.ToArray());
							return;
						}
					}
					catch (Exception ex)
					{
						Utilities.ShowError(@"Failed to save template: {0}", ex.Message);
						return;
					}
				}
			}
			Utilities.ShowWarning(@"Nothing to save.");
		}

		private void SaveImageToFile(NImage image)
		{
			if (saveImageDialog.ShowDialog() != DialogResult.OK) return;
			try
			{
				image.Save(saveImageDialog.FileName);
			}
			catch (Exception ex)
			{
				Utilities.ShowError(@"Failed to save image to file: {0}", ex.Message);
			}
		}

		private void TsmiSaveLatentImageClick(object sender, EventArgs e)
		{
			NImage workingImage = GetWorkingImage();
			if (workingImage != null)
			{
				SaveImageToFile(workingImage);
			}
		}

		private void TsmiSaveReferenceImageClick(object sender, EventArgs e)
		{
			NFinger finger = (NFinger)nfViewRight.Finger;
			if (finger != null && finger.Image != null)
			{
				SaveImageToFile(finger.Image);
			}
		}

		#endregion

		#region Event Handling

		private void MainFormLoad(object sender, EventArgs e)
		{
			if (DesignMode) return;
			try
			{
				_biometricClient = new NBiometricClient()
				{
					FingersQualityThreshold = 0,
					FingersReturnBinarizedImage = true,
					MatchingWithDetails = true
				};
				_biometricClient.SetProperty("Fingers.MinimalMinutiaCount", 0);
				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
				saveImageDialog.Filter = NImages.GetSaveFileFilterString();

				matchingFarComboBox.BeginUpdate();
				matchingFarComboBox.Items.Add(0.001.ToString("P1"));
				matchingFarComboBox.Items.Add(0.0001.ToString("P2"));
				matchingFarComboBox.Items.Add(0.00001.ToString("P3"));
				matchingFarComboBox.EndUpdate();
				matchingFarComboBox.SelectedIndex = 1;

				LoadZoomCombo(tscbZoomLeft);
				LoadZoomCombo(tscbZoomRight);

				// select pointer tool by default
				nfViewLeft.ActiveTool = new NFingerView.PointerTool();
				_addMode = AddMode.None;
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex.Message);
			}
		}

		private void NfView1IndexChanged(object sender, EventArgs e)
		{
			var args = e as TreeMinutiaEventArgs;
			if (nfViewRight != null && args != null)
			{
				nfViewRight.SelectedMinutiaIndex = args.Index;
			}
		}

		private void NfView2IndexChanged(object sender, EventArgs e)
		{
			var args = e as TreeMinutiaEventArgs;
			if (nfViewLeft != null && args != null)
			{
				nfViewLeft.SelectedMinutiaIndex = args.Index;
			}
		}

		#region Zooming

		private void LoadZoomCombo(ToolStripComboBox comboBox)
		{
			foreach (float zoom in _zoomFactors)
			{
				comboBox.Items.Add(string.Format("{0:P0}", zoom));
			}
			comboBox.SelectedIndex = DefaultZoomFactor;
		}

		private int SetZoomFactor(NFingerView nfView, int currentZoomFactor, int newZoomFactor)
		{
			if (newZoomFactor >= _zoomFactors.Length) return currentZoomFactor;
			if (newZoomFactor < 0) return currentZoomFactor;

			float zoom = _zoomFactors[newZoomFactor];
			nfView.Zoom = zoom;

			return newZoomFactor;
		}

		private void TsmiZoomInLeftClick(object sender, EventArgs e)
		{
			_leftZoomFactor = SetZoomFactor(nfViewLeft, _leftZoomFactor, _leftZoomFactor + 1);
			tscbZoomLeft.SelectedIndex = _leftZoomFactor;
		}

		private void TsmiZoomOutLeftClick(object sender, EventArgs e)
		{
			_leftZoomFactor = SetZoomFactor(nfViewLeft, _leftZoomFactor, _leftZoomFactor - 1);
			tscbZoomLeft.SelectedIndex = _leftZoomFactor;
		}

		private void TsmiZoomOriginalLeftClick(object sender, EventArgs e)
		{
			_leftZoomFactor = SetZoomFactor(nfViewLeft, _leftZoomFactor, DefaultZoomFactor);
			tscbZoomLeft.SelectedIndex = _leftZoomFactor;
		}

		private void TsmiZoomInRightClick(object sender, EventArgs e)
		{
			_rightZoomFactor = SetZoomFactor(nfViewRight, _rightZoomFactor, _rightZoomFactor + 1);
			tscbZoomRight.SelectedIndex = _rightZoomFactor;
		}

		private void TsmiZoomOutRightClick(object sender, EventArgs e)
		{
			_rightZoomFactor = SetZoomFactor(nfViewRight, _rightZoomFactor, _rightZoomFactor - 1);
			tscbZoomRight.SelectedIndex = _rightZoomFactor;
		}

		private void TsmiZoomOriginalRightClick(object sender, EventArgs e)
		{
			_rightZoomFactor = SetZoomFactor(nfViewRight, _rightZoomFactor, DefaultZoomFactor);
			tscbZoomRight.SelectedIndex = _rightZoomFactor;
		}

		private void TscbZoomLeftSelectedIndexChanged(object sender, EventArgs e)
		{
			_leftZoomFactor = SetZoomFactor(nfViewLeft, _leftZoomFactor, tscbZoomLeft.SelectedIndex);
		}

		private void TscbZoomRightSelectedIndexChanged(object sender, EventArgs e)
		{
			_rightZoomFactor = SetZoomFactor(nfViewRight, _rightZoomFactor, tscbZoomRight.SelectedIndex);
		}

		#endregion

		private void TsmiViewOriginalLeftClick(object sender, EventArgs e)
		{
			tsmiViewOriginalLeft.Checked = !tsmiViewOriginalLeft.Checked;
			nfViewLeft.ShownImage = tsmiViewOriginalLeft.Checked ? ShownImage.Original : ShownImage.Result;
			nfViewLeft.Invalidate();
		}

		private void TsmiViewOriginalRightClick(object sender, EventArgs e)
		{
			tsmiViewOriginalRight.Checked = !tsmiViewOriginalRight.Checked;
			nfViewRight.ShownImage = tsmiViewOriginalRight.Checked ? ShownImage.Original : ShownImage.Result;
			nfViewRight.Invalidate();
		}

		private void RbPointerToolCheckedChanged(object sender, EventArgs e)
		{
			if (rbPointerTool.Checked)
			{
				nfViewLeft.ActiveTool = new NFingerView.PointerTool();
				_addMode = AddMode.None;
			}
		}

		private void RbSelectAreaToolCheckedChanged(object sender, EventArgs e)
		{
			if (rbSelectAreaTool.Checked)
			{
				nfViewLeft.ActiveTool = new NFingerView.RectangleSelectionTool();
				_addMode = AddMode.None;
			}
		}

		private void RbAddEndMinutiaToolCheckedChanged(object sender, EventArgs e)
		{
			if (rbAddEndMinutiaTool.Checked)
			{
				var addFeatTool = new NFingerView.AddFeaturesTool();
				addFeatTool.FeatureAddCompleted += NfViewLeftFeatureAddCompleted;
				nfViewLeft.ActiveTool = addFeatTool;
				_addMode = AddMode.EndMinutia;
			}
		}

		private void RbAddBifurcationMinutiaCheckedChanged(object sender, EventArgs e)
		{
			if (rbAddBifurcationMinutia.Checked)
			{
				var addFeatTool = new NFingerView.AddFeaturesTool();
				addFeatTool.FeatureAddCompleted += NfViewLeftFeatureAddCompleted;
				nfViewLeft.ActiveTool = addFeatTool;
				_addMode = AddMode.BifurcationMinutia;
			}
		}

		private void RbAddDeltaToolCheckedChanged(object sender, EventArgs e)
		{
			if (rbAddDeltaTool.Checked)
			{
				var addFeatTool = new NFingerView.AddFeaturesTool(false);
				addFeatTool.FeatureAddCompleted += NfViewLeftFeatureAddCompleted;
				nfViewLeft.ActiveTool = addFeatTool;
				_addMode = AddMode.Delta;
			}
		}

		private void RbAddCoreToolCheckedChanged(object sender, EventArgs e)
		{
			if (rbAddCoreTool.Checked)
			{
				var addFeatTool = new NFingerView.AddFeaturesTool();
				addFeatTool.FeatureAddCompleted += NfViewLeftFeatureAddCompleted;
				nfViewLeft.ActiveTool = addFeatTool;
				_addMode = AddMode.Core;
			}
		}

		private void RbAddDoubleCoreToolCheckedChanged(object sender, EventArgs e)
		{
			if (rbAddDoubleCoreTool.Checked)
			{
				var addFeatTool = new NFingerView.AddFeaturesTool(false);
				addFeatTool.FeatureAddCompleted += NfViewLeftFeatureAddCompleted;
				nfViewLeft.ActiveTool = addFeatTool;
				_addMode = AddMode.DoubleCore;
			}
		}

		private void TsmiFileExitClick(object sender, EventArgs e)
		{
			Close();
		}

		private void MatchingFarComboBoxValidating(object sender, CancelEventArgs e)
		{
			try
			{
				int matchingThreshold = Utilities.MatchingThresholdFromString(matchingFarComboBox.Text);
				matchingFarComboBox.Text = Utilities.MatchingThresholdToString(matchingThreshold);
				_biometricClient.MatchingThreshold = matchingThreshold;
			}
			catch
			{
				errorProvider1.SetError(matchingFarComboBox, "Matching threshold is invalid");
				e.Cancel = true;
			}
		}

		private void MatchingFarComboBoxValidated(object sender, EventArgs e)
		{
			errorProvider1.SetError(matchingFarComboBox, "");
		}

		private void TsmiHelpAboutClick(object sender, EventArgs e)
		{
			Gui.AboutBox.Show();
		}

		private void TsmiPerformBandpassFilteringClick(object sender, EventArgs e)
		{
			NImage workingImage = GetWorkingImage();
			if (workingImage != null)
			{
				BandpassFilteringForm fft;
				try
				{
					fft = new BandpassFilteringForm(workingImage);
				}
				catch (Exception)
				{
					return;
				}

				if (fft.ShowDialog() != DialogResult.OK) return;
				_imageLeftOriginal = NImage.FromImage(NPixelFormat.Rgb8U, 0, fft.ResultImage);
				ProcessImage();
				nfViewLeft.ShownImage = ShownImage.Original;
				nfViewLeft.Invalidate();
			}
		}

		#endregion
	}
}
