using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Gui;
using Neurotec.Gui;

namespace Neurotec.Samples.Common
{
	public partial class GeneralizeProgressView : UserControl, INotifyPropertyChanged
	{
		#region Private types

		protected class ItemStatus
		{
			public string Text { get; set; }
			public bool Fill { get; set; }
			public Color Color { get; set; }
			public NBiometric Biometric { get; set; }
			public RectangleF HitBox { get; set; }
			public bool Selected { get; set; }

			public bool HitTest(Point p)
			{
				if (HitBox != RectangleF.Empty)
				{
					return HitBox.X <= p.X && p.X <= HitBox.X + HitBox.Width
						&& HitBox.Y <= p.Y && p.Y <= HitBox.Y + HitBox.Height;
				}
				return false;
			}
		};

		#endregion

		#region Public constructor

		public GeneralizeProgressView()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		protected NView _view = null;
		protected IcaoWarningView _icaoView = null;
		protected NBiometric[] _biometrics = null;
		protected NBiometric _selected = null;
		protected NBiometric [] _generalized = null;
		protected bool _enableMouseSelection = true;
		protected List<ItemStatus> _drawings = new List<ItemStatus>();
		private bool _redraw = false;

		#endregion

		#region Public properties

		public NView View
		{
			get { return _view; }
			set { SetProperty(ref _view, value, "View"); }
		}

		public IcaoWarningView IcaoView
		{
			get { return _icaoView; }
			set { SetProperty(ref _icaoView, value, "IcaoView"); }
		}

		public NBiometric Selected
		{
			get { return _selected; }
			set
			{
				bool newValue = _selected != value;
				_selected = value;
				SetBiometricToView(_view, value);
				_drawings.ForEach(x => x.Selected = x.Biometric == value);
				panelPaint.Invalidate();
				if (newValue)
					OnPropertyChanged("Selected");
			}
		}

		public NBiometric[] Biometrics
		{
			get { return _biometrics; }
			set
			{
				if (_biometrics != value)
				{
					if (_biometrics != null)
					{
						foreach (var item in _biometrics)
						{
							item.PropertyChanged -= BiometricPropertyChanged;
						}
					}
					_biometrics = value;
					if (value != null)
					{
						foreach (var item in _biometrics)
						{
							item.PropertyChanged += BiometricPropertyChanged;
						}
					}
					OnDataChanged();
					OnPropertyChanged("Biometrics");
				}
			}
		}

		public NBiometric[] Generalized
		{
			get { return _generalized; }
			set
			{
				if (_generalized != value)
				{
					if (_generalized != null)
					{
						foreach (var item in _generalized)
						{
							item.PropertyChanged -= BiometricPropertyChanged;
						}
					}
					_generalized = value;
					if (_generalized != null)
					{
						foreach (var item in _generalized)
						{
							item.PropertyChanged += BiometricPropertyChanged;
						}
					}
					OnDataChanged();
					OnPropertyChanged("Generalized");
				}
			}
		}

		public string StatusText
		{
			get { return lblStatus.Text; }
			set
			{
				if (lblStatus.Text != value)
				{
					lblStatus.Text = value;
					OnPropertyChanged("StatusText");
				}
			}
		}

		public bool EnableMouseSelection
		{
			get { return _enableMouseSelection; }
			set { _enableMouseSelection = value; }
		}

		#endregion

		#region Private methods

		private void OnDataChanged()
		{
			int i;

			_drawings.Clear();
			if (_biometrics != null)
			{
				i = 1;
				foreach (var item in _biometrics)
				{
					_drawings.Add(new ItemStatus { Text = i++.ToString(), Biometric = item, Color = Color.Orange, Fill = false });
				}
			}
			if (_generalized != null)
			{
				i = 0;
				foreach (var item in _generalized)
				{
					_drawings.Add(new ItemStatus { Text = i++ == 0 ? "Generalized:" : string.Empty, Biometric = item, Color = Color.Orange, Fill = false });
				}
			}

			UpdateBiometricsStatus();
			_redraw = true;
		}

		private void BiometricPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Status")
				BeginInvoke(new MethodInvoker(UpdateBiometricsStatus));
		}

		private void UpdateBiometricsStatus()
		{
			foreach (var item in _drawings)
			{
				var biometric = item.Biometric;
				item.Color = Color.Orange;
				item.Fill = false;
				if (biometric != null)
				{
					switch (biometric.Status)
					{
					case NBiometricStatus.Ok:
						item.Color = Color.DarkGreen;
						item.Fill = true;
						break;
					case NBiometricStatus.None:
						item.Fill = false;
						item.Color = Color.Orange;
						break;
					default:
						item.Color = Color.Red;
						item.Fill = true;
						break;
					};
				}
			}
			panelPaint.Invalidate();
		}

		private void PanelPaintPaint(object sender, PaintEventArgs e)
		{
			const int Margin = 2;
			Size sz = panelPaint.Size;
			sz.Width = sz.Width - Margin * 2;
			sz.Height = sz.Height - Margin * 2;

			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			if (_drawings.Count > 0)
			{
				string text = "Az";
				SizeF defaultTextSize = g.MeasureString(text, Font);
				SizeF textSize = defaultTextSize;
				float bubleDiameter = textSize.Height - Margin * 2;
				float totalWidth = 0;
				foreach (var item in _drawings)
				{
					totalWidth += 2 * Margin + g.MeasureString(item.Text, Font).Width + Margin + bubleDiameter + 2 * Margin;
				}
				float offsetX = (sz.Width - totalWidth) / 2;
				float offsetY = (sz.Height - textSize.Height) / 2;

				var m = g.Transform;
				m.Translate(offsetX, offsetY);
				g.Transform = m;

				float offset = 2 * Margin;
				foreach (var item in _drawings)
				{
					RectangleF hitBox = new RectangleF(offsetX + offset, offsetY, 0, 0);
					if (item.Text != string.Empty)
					{
						textSize = g.MeasureString(item.Text, Font);
						g.DrawString(item.Text, Font, Brushes.Black, offset, 0);
					}
					else
					{
						textSize = new SizeF(0, defaultTextSize.Height);
					}
					hitBox.Width = textSize.Width + Margin;
					offset += hitBox.Width;
					if (item.Fill)
					{
						using (var b = new SolidBrush(item.Color))
							g.FillEllipse(b, offset, Margin, bubleDiameter, bubleDiameter);
					}
					else
					{
						using (var p = new Pen(item.Color))
							g.DrawEllipse(p, offset, Margin, bubleDiameter, bubleDiameter);
					}
					if (item.Selected)
					{
						using (var p = new Pen(Color.CadetBlue, 2))
							g.DrawEllipse(p, offset, Margin, bubleDiameter, bubleDiameter);
					}
					offset += bubleDiameter + 2 * Margin;
					hitBox.Width += bubleDiameter;
					hitBox.Height = textSize.Height;
					item.HitBox = hitBox;
				}
			}

			base.OnPaint(e);

			if (_redraw)
			{
				_redraw = false;
				ResizeForAutoSize();
			}
		}

		private void PanelPaintMouseMove(object sender, MouseEventArgs e)
		{
			bool hit = false;
			if (_enableMouseSelection)
			{
				foreach (var item in _drawings)
				{
					if (item.HitTest(e.Location))
					{
						hit = true;
					}
				}
				if (hit)
				{
					Cursor = Cursors.Hand;
				}
			}
			if (!hit) Cursor = Cursors.Default;
		}

		private void PanelPaintMouseClick(object sender, MouseEventArgs e)
		{
			if (_enableMouseSelection)
			{
				foreach (var item in _drawings)
				{
					if (item.HitTest(e.Location))
					{
						Selected = item.Biometric;
						break;
					}
				}
			}
		}

		private void ResizeForAutoSize()
		{
			if (AutoSize)
			{
				this.SetBoundsCore(Left, Top, Width, Height, BoundsSpecified.Size);
			}
		}

		#endregion

		#region Protected methods

		protected virtual void SetBiometricToView(NView view, NBiometric biometric)
		{
			if (view != null)
			{
				if (view is NFaceView)
				{
					((NFaceView)view).Face = biometric as NFace;
					if (_icaoView != null) _icaoView.Face = biometric as NFace;
				}
				else if (view is NFingerView)
				{
					((NFingerView)view).Finger = biometric as NFrictionRidge;
				}
			}
		}

		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.AutoSize && (specified & BoundsSpecified.Size) != 0)
			{
				Size size = GetPreferredSize(new Size(width, height));

				width = size.Width;
				height = size.Height;
			}

			base.SetBoundsCore(x, y, width, height, specified);
		}

		protected bool SetProperty<T>(ref T value, T newValue, string propertyName)
		{
			if (!object.Equals(value, newValue))
			{
				value = newValue;
				OnPropertyChanged(propertyName);
				return true;
			}
			return false;
		}

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		#region Public methods

		public void Clear()
		{
			Biometrics = null;
			Generalized = null;
			Selected = null;
			StatusText = string.Empty;
			_drawings.Clear();
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			Size sz = lblStatus.Size;
			if (_drawings.Count > 0)
			{
				sz.Height += (int)_drawings.Max(x => x.HitBox.Height) + 8;
				sz.Width = Math.Max((int)_drawings.Last().HitBox.Right, sz.Width);
			}
			else
			{
				sz.Width = Math.Max(50, sz.Width);
				sz.Height = 35;
			}
			return sz;
		}

		#endregion

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}

	public class DoubleBufferedPanel : Panel
	{
		public DoubleBufferedPanel()
		{
			DoubleBuffered = true;
		}
	};
}
