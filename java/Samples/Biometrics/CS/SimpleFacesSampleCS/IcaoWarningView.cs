using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class IcaoWarningView : UserControl
	{
		#region Private fields

		private NFace _face;
		private NLAttributes _attributes;
		private Color _noWarning = Color.Green;
		private Color _warningColor = Color.Red;
		private Color _indeterminateColor = Color.Orange;

		#endregion

		#region Public constructor

		public IcaoWarningView()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		[DefaultValue(null)]
		public NFace Face
		{
			get { return _face; }
			set
			{
				if (_face != value)
				{
					UnsubscribeFromFaceEvents();
					_face = value;
					SubscribeToFaceEvents();
					UpdateUI();
					Invalidate();
				}
			}
		}

		public Color NoWarningColor
		{
			get { return _noWarning; }
			set { _noWarning = value; }
		}

		public Color WarningColor
		{
			get { return _warningColor; }
			set { _warningColor = value; }
		}

		public Color IndeterminateColor
		{
			get { return _indeterminateColor; }
			set { _indeterminateColor = value; }
		}

		#endregion

		#region Private methods

		private IEnumerable<Label> GetLabels()
		{
			yield return lblFaceDetected;
			yield return lblExpression;
			yield return lblDarkGlasses;
			yield return lblBlink;
			yield return lblMouthOpen;
			yield return lblRoll;
			yield return lblYaw;
			yield return lblPitch;
			yield return lblTooClose;
			yield return lblTooFar;
			yield return lblTooNorth;
			yield return lblTooSouth;
			yield return lblTooEast;
			yield return lblTooWest;
			yield return lblSharpness;
			yield return lblGrayscaleDensity;
			yield return lblSaturation;
			yield return lblBackgroundUniformity;
		}

		private Color GetColorForConfidence(NIcaoWarnings warnings, NIcaoWarnings flag, byte confidence)
		{
			if ((warnings & flag) == flag)
			{
				return confidence <= 100 ? WarningColor : IndeterminateColor;
			}
			return NoWarningColor;
		}

		private Color GetColorForFlags(NIcaoWarnings warnings, params NIcaoWarnings[] flags)
		{
			return flags.Any(f => (f & warnings) == f) ? WarningColor : NoWarningColor;
		}

		private string GetConfidenceString(string name, byte value)
		{
			return string.Format("{0}: {1}", name, value <= 100 ? value.ToString() : "N/A");
		}

		private void UpdateUI()
		{
			if (_attributes != null)
			{
				var warnings = _attributes.IcaoWarnings;
				if ((warnings & NIcaoWarnings.FaceNotDetected) == NIcaoWarnings.FaceNotDetected)
				{
					foreach (var lbl in GetLabels())
					{
						lbl.ForeColor = IndeterminateColor;
					}
					lblFaceDetected.ForeColor = WarningColor;
				}
				else
				{
					lblFaceDetected.ForeColor = NoWarningColor;
					lblExpression.ForeColor = GetColorForConfidence(warnings, NIcaoWarnings.Expression, _attributes.ExpressionConfidence);
					lblDarkGlasses.ForeColor = GetColorForConfidence(warnings, NIcaoWarnings.DarkGlasses, _attributes.DarkGlassesConfidence);
					lblBlink.ForeColor = GetColorForConfidence(warnings, NIcaoWarnings.Blink, _attributes.BlinkConfidence);
					lblMouthOpen.ForeColor = GetColorForConfidence(warnings, NIcaoWarnings.MouthOpen, _attributes.MouthOpenConfidence);
					lblRoll.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.RollLeft, NIcaoWarnings.RollRight);
					lblYaw.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.YawLeft, NIcaoWarnings.YawRight);
					lblPitch.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.PitchDown, NIcaoWarnings.PitchUp);
					lblTooClose.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooNear);
					lblTooFar.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooFar);
					lblTooNorth.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooNorth);
					lblTooSouth.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooSouth);
					lblTooWest.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooWest);
					lblTooEast.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.TooEast);

					lblSharpness.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.Sharpness);
					lblSharpness.Text = GetConfidenceString("Sharpness", _attributes.Sharpness);
					lblSaturation.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.Saturation);
					lblSaturation.Text = GetConfidenceString("Saturation", _attributes.Saturation);
					lblGrayscaleDensity.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.GrayscaleDensity);
					lblGrayscaleDensity.Text = GetConfidenceString("Grayscale Density", _attributes.GrayscaleDensity);
					lblBackgroundUniformity.ForeColor = GetColorForFlags(warnings, NIcaoWarnings.BackgroundUniformity);
					lblBackgroundUniformity.Text = GetConfidenceString("Background Uniformity", _attributes.BackgroundUniformity);
				}
			}
			else
			{
				foreach (var lbl in GetLabels())
				{
					lbl.ForeColor = IndeterminateColor;
				}
			}
		}

		private void UnsubscribeFromFaceEvents()
		{
			if (_face != null)
			{
				_face.Objects.CollectionChanged -= OnObjectsCollectionChanged;
			}
			if (_attributes != null)
			{
				_attributes.PropertyChanged -= OnAttributesPropertyChanged;
			}
		}

		private void SubscribeToFaceEvents()
		{
			if (_face != null)
			{
				_face.Objects.CollectionChanged += OnObjectsCollectionChanged;
				_attributes = _face.Objects.ToArray().FirstOrDefault();
				if (_attributes != null)
				{
					_attributes.PropertyChanged += OnAttributesPropertyChanged;
				}
			}
		}

		private void OnAttributesPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!IsDisposed && IsHandleCreated)
			{
				if (e.PropertyName == "IcaoWarnings")
					BeginInvoke(new MethodInvoker(UpdateUI));
			}
		}

		private void OnObjectsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (!IsDisposed && IsHandleCreated)
			{
				if (e.Action == NotifyCollectionChangedAction.Add)
				{
					BeginInvoke(new Action<object, NLAttributes>((s, a) =>
					{
						if (object.Equals(((NFace.ObjectCollection)s).Owner, _face))
						{
							if (_attributes != null) _attributes.PropertyChanged -= OnAttributesPropertyChanged;
							_attributes = a;
							_attributes.PropertyChanged += OnAttributesPropertyChanged;
						}
					}), sender, (NLAttributes)e.NewItems[0]);
				}
				else if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset)
				{
					BeginInvoke(new Action<object>(s =>
					{
						if (object.Equals(s, _face))
						{
							if (_attributes != null)
								_attributes.PropertyChanged -= OnAttributesPropertyChanged;
							_attributes = null;
						}
					}), sender);
				}
			}
		}

		#endregion

		#region Protected methods

		protected override void Dispose(bool disposing)
		{
			UnsubscribeFromFaceEvents();
			_face = null;
			_attributes = null;

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion
	}
}
