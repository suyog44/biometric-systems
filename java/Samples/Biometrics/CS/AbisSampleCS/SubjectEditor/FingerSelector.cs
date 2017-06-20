using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class FingerSelector : Control
	{
		#region Public types

		public class FingerClickArgs : MouseEventArgs
		{
			#region Public properties

			public NFPosition Position { get; set; }
			public NFPosition PositionPart { get; set; }

			#endregion

			#region Public constructors

			public FingerClickArgs(NFPosition pos, MouseEventArgs e)
				: this(pos, NFPosition.Unknown, e)
			{
			}

			public FingerClickArgs(NFPosition pos, NFPosition part, MouseEventArgs e)
				: base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
			{
				Position = pos;
				PositionPart = part;
			}

			#endregion
		}

		#endregion

		#region Protected types

		protected enum ItemType
		{
			None = 0,
			Item,
			ItemPart,
			Fingernails,
			PalmCreases,
			Rotation,
		};

		protected sealed class SvgPath
		{
			#region Public constructor

			public SvgPath()
			{
				Scale = 1;
				Position = NFPosition.Unknown;
			}

			#endregion

			#region Private fields

			private GraphicsPath _path;
			private Region _region;

			#endregion

			#region Public properties

			public ItemType ItemType { get; set; }
			public string Id { get; set; }
			public bool Fill { get; set; }
			public Color FillColor { get; set; }
			public NFPosition Position { get; set; }
			public float Scale { get; set; }
			public GraphicsPath Path
			{
				get { return _path; }
				set
				{
					_path = value;
					_region = new Region(_path);
				}
			}
			public float StrokeAlpha { get; set; }

			#endregion

			#region Public methods

			public void DrawElement(Graphics g)
			{
				using (Pen p = new Pen(Color.FromArgb((int)(StrokeAlpha * 255), Color.Black)))
				{
					g.DrawPath(p, _path);
					if (!Fill) return;
					using (Brush b = new SolidBrush(FillColor))
					{
						g.FillPath(b, _path);
					}
				}
			}

			public bool HitTest(Point point)
			{
				if (_region == null)
					return false;
				return _region.IsVisible(point.X / Scale, point.Y / Scale);
			}

			public override string ToString()
			{
				return string.Format("Id={0}, Type={1}, Position={2}", Id, ItemType, Position);
			}

			#endregion
		}

		protected class SvgPainter
		{
			#region Private fields

			private int _width, _height;
			private readonly List<SvgPath> _paths;

			#endregion

			#region Public constructor

			public SvgPainter(string handsString)
			{
				_paths = new List<SvgPath>();
				Elements = new List<SvgPath>();

				ParsePaths(handsString);

				foreach (SvgPath item in _paths.Where(x => x.Position != NFPosition.Unknown))
				{
					if (item.ItemType == ItemType.Rotation)
					{
						item.Fill = true;
						item.FillColor = Color.GreenYellow;
						item.StrokeAlpha = 1;
					}
					else
					{
						item.FillColor = Color.Transparent;
					}
				}
			}

			#endregion

			#region Private methods

			private static PointF ToAbsolute(PointF absolute, PointF reliative)
			{
				return PointF.Add(absolute, new SizeF(reliative.X, reliative.Y));
			}

			private GraphicsPath ParsePath(string value)
			{
				GraphicsPath gp = new GraphicsPath();
				string[] vals = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
				PointF[] pnts = new PointF[4];
				PointF endPoint = new PointF();
				int j = 0;
				bool relative = false;
				while (j < vals.Length)
				{
					if (vals[j] == "m" || vals[j] == "M")//move
					{
						gp.StartFigure();
						endPoint = new PointF(float.Parse(vals[j + 1], System.Globalization.CultureInfo.InvariantCulture), float.Parse(vals[j + 2], System.Globalization.CultureInfo.InvariantCulture));
						j += 3;
					}
					else if (vals[j] == "l" || vals[j] == "L")//draw line
					{
						relative = char.IsLower(vals[j][0]);
						PointF point = new PointF(float.Parse(vals[j + 1], System.Globalization.CultureInfo.InvariantCulture), float.Parse(vals[j + 2], System.Globalization.CultureInfo.InvariantCulture));
						j += 3;
						if (relative)
							point = ToAbsolute(endPoint, point);
						gp.AddLine(endPoint, point);
						endPoint = point;
					}
					else if (vals[j] == "z" || vals[j] == "Z")//end
					{
						gp.CloseFigure();
						j++;
					}
					else if (vals[j] == "c" || vals[j] == "C")//curve
					{
						relative = char.IsLower(vals[j][0]);
						j++;

						PointF p1 = new PointF(float.Parse(vals[j], System.Globalization.CultureInfo.InvariantCulture), float.Parse(vals[j + 1], System.Globalization.CultureInfo.InvariantCulture)); j += 2;
						PointF p2 = new PointF(float.Parse(vals[j], System.Globalization.CultureInfo.InvariantCulture), float.Parse(vals[j + 1], System.Globalization.CultureInfo.InvariantCulture)); j += 2;
						PointF p3 = new PointF(float.Parse(vals[j], System.Globalization.CultureInfo.InvariantCulture), float.Parse(vals[j + 1], System.Globalization.CultureInfo.InvariantCulture)); j += 2;
						PointF p0 = endPoint;
						if (relative)
						{
							p1 = ToAbsolute(endPoint, p1);
							p2 = ToAbsolute(endPoint, p2);
							p3 = ToAbsolute(endPoint, p3);
						}

						gp.AddBezier(p0, p1, p2, p3);
						endPoint = p3;
					}
				}
				return gp;
			}

			private void ParsePaths(string xmlString)
			{
				XmlReader xml = new XmlTextReader(new StringReader(xmlString));
				while (xml.Read())
				{
					if (xml.NodeType == XmlNodeType.Element)
					{
						if (xml.Name == "svg")
						{
							if (!xml.MoveToAttribute("width")) throw new Exception("width attribute not found");
							_width = int.Parse(xml.Value);
							if (_width == 0) throw new Exception("width attribute is invalid");
							if (!xml.MoveToAttribute("height")) throw new Exception("height attribute not found");
							_height = int.Parse(xml.Value);
							if (_height == 0) throw new Exception("height attribute is invalid");
						}
						else if (xml.Name == "path")
						{
							int count = xml.AttributeCount;
							SvgPath shape = new SvgPath();

							_paths.Add(shape);
							for (int i = 0; i < count; i++)
							{
								xml.MoveToAttribute(i);
								switch (xml.Name)
								{
									case "d":
										{
											string id = xml.GetAttribute("id");
											if (id != null && id.EndsWith("Rotate"))
											{
												id = 123.ToString();
											}
											shape.Path = ParsePath(xml.Value);
											break;
										}
									case "position":
										{
											shape.Position = (NFPosition)Enum.Parse(typeof(NFPosition), xml.Value);
											shape.Fill = true;
											if (shape.Position == NFPosition.PlainLeftFourFingers ||
												shape.Position == NFPosition.PlainRightFourFingers ||
												shape.Position == NFPosition.PlainThumbs)
											{
												shape.FillColor = Color.Transparent;
											}
											Elements.Add(shape);
											break;
										}
									case "id":
										{
											shape.Id = xml.Value;
											break;
										}
									case "style":
										{
											string[] vals = xml.Value.Split(';');
											foreach (string t in vals)
											{
												string item = t;
												if (item.StartsWith("fill:"))
												{
													item = item.Replace("fill:", "");
													shape.FillColor = Color.Transparent;
													if (item != "none") shape.FillColor = ColorTranslator.FromHtml(item);
													continue;
												}
												if (item.StartsWith("stroke-opacity:"))
												{
													item = item.Replace("stroke-opacity:", "");
													shape.StrokeAlpha = float.Parse(item, System.Globalization.CultureInfo.InvariantCulture);
													continue;
												}
											}
											break;
										}
									case "group":
										{
											shape.ItemType = (ItemType)Enum.Parse(typeof(ItemType), xml.Value);
											break;
										}
								}
							}
						}
					}
				}
				xml.Close();
			}

			#endregion

			#region Public methods

			public void Paint(Graphics g, Size clientSize)
			{
				g.SmoothingMode = SmoothingMode.HighQuality;
				Matrix m = g.Transform;

				float scale = Math.Min((float)clientSize.Width / _width, (float)clientSize.Height / _height);
				g.ScaleTransform(scale, scale);

				foreach (SvgPath item in _paths)
				{
					item.Scale = scale;
					if (item.ItemType != ItemType.Rotation)
					{
						if (item.ItemType == ItemType.PalmCreases)
						{
							if (ShowPalmCreases) item.DrawElement(g);
						}
						else if (item.ItemType == ItemType.Fingernails)
						{
							if (ShowFingerNails) item.DrawElement(g);
						}
						else
						{
							item.DrawElement(g);
						}
					}
				}

				g.Transform = m;
			}

			public void PaintRotateForFinger(Graphics g, Size clientSize, NFPosition position, float angle)
			{
				SvgPath path = _paths.FirstOrDefault(x => x.Position == position && x.ItemType == ItemType.Rotation);
				if (path == null) return;

				g.SmoothingMode = SmoothingMode.HighQuality;
				Matrix m = g.Transform;

				float scale = Math.Min((float)clientSize.Width / _width, (float)clientSize.Height / _height);
				m.Scale(scale, scale);

				Region reg = new Region(path.Path);
				RectangleF bounds = reg.GetBounds(g);
				PointF rotateAt = new PointF(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
				m.RotateAt(angle, rotateAt);
				g.Transform = m;

				path.DrawElement(g);
			}

			public void Clear()
			{
				foreach (var item in Elements)
				{
					if (item.ItemType == ItemType.ItemPart || item.ItemType == ItemType.Item)
					{
						item.FillColor = Color.Transparent;
					}
				}
			}

			#endregion

			#region Public properties

			public List<SvgPath> Elements { get; private set; }

			public bool ShowPalmCreases { get; set; }
			public bool ShowFingerNails { get; set; }

			#endregion
		}

		#endregion

		#region Public constructor

		public FingerSelector()
		{
			InitializeComponent();
			DoubleBuffered = true;

			_painter = new SvgPainter(Encoding.UTF8.GetString(Properties.Resources.Hands));
			_missingPositions = new List<NFPosition>();
			_allowedPositions = new List<NFPosition>();
			for (int i = (int)NFPosition.RightThumb; i <= (int)NFPosition.LeftLittle; i++)
			{
				_allowedPositions.Add((NFPosition)i);
			}
		}

		#endregion

		#region Protected fields

		protected readonly SvgPainter _painter;
		protected NFPosition _selectedPosition = NFPosition.Unknown;
		protected readonly List<NFPosition> _missingPositions;
		protected readonly List<NFPosition> _allowedPositions;
		protected Point _mousePosition = new Point(0, 0);
		protected bool _allowHighlight = true;
		protected bool _isRolled = false;
		protected bool _rollStarted = false;
		protected float _rollAngle = 0;
		private Color _highlightColor = Color.LightBlue;
		private Color _highligthPartColor = Color.LightCyan;
		private Color _missingFingerColor = Color.DarkRed;
		private Color _selectedFingerColor = Color.Green;

		#endregion

		#region Public properties

		[Category("Appearance")]
		public bool ShowFingerNails
		{
			get { return _painter.ShowFingerNails; }
			set { _painter.ShowFingerNails = value; }
		}

		[Category("Appearance")]
		public bool ShowPalmCreases
		{
			get { return _painter.ShowPalmCreases; }
			set { _painter.ShowPalmCreases = value; }
		}

		[Browsable(false)]
		public virtual NFPosition SelectedPosition
		{
			get { return _selectedPosition; }
			set
			{
				if (_selectedPosition != value)
				{
					_selectedPosition = value;
					OnDataChanged();
				}
			}
		}

		[Browsable(false)]
		public virtual NFPosition[] MissingPositions
		{
			get { return _missingPositions.ToArray(); }
			set
			{
				_missingPositions.Clear();
				if (value != null)
				{
					_missingPositions.AddRange(value);
				}
				_painter.Clear();
				OnDataChanged();
				Invalidate();
			}
		}

		[Browsable(false)]
		public virtual NFPosition [] AllowedPositions
		{
			get { return _allowedPositions.ToArray(); }
			set
			{
				if (DesignMode) return;
				_allowedPositions.Clear();
				if (value != null)
				{
					_allowedPositions.AddRange(value.Where(x => NBiometricTypes.IsPositionKnown(x) && !NBiometricTypes.IsPositionPalm(x)));
				}
				_painter.Clear();
				OnDataChanged();
				Invalidate();
			}
		}

		[Category("Behavior")]
		public bool AllowHighlight
		{
			get { return _allowHighlight; }
			set
			{
				if (_allowHighlight != value)
				{
					_allowHighlight = value;
					OnDataChanged();
				}
			}
		}

		[Browsable(false)]
		public virtual bool IsRolled
		{
			get { return _isRolled; }
			set
			{
				if (_isRolled != value)
				{
					_isRolled = value;
					OnDataChanged();
				}
			}
		}

		#endregion

		#region Public events

		public event EventHandler<FingerClickArgs> FingerClick;

		#endregion

		#region Protected methods

		protected virtual IEnumerable<SvgPath> GetAvailableElements(bool getParts)
		{
			if (DesignMode) return new SvgPath[0];

			var items = _painter.Elements.Where(x => (x.ItemType == ItemType.Item || x.ItemType == ItemType.ItemPart) && x.Position != NFPosition.Unknown && _allowedPositions.Contains(x.Position));
			if (getParts)
			{
				var parts = new List<SvgPath>();
				foreach (var item in items)
				{
					parts.AddRange(GetParts(item));
				}
				return Enumerable.Union(items, parts);
			}
			return items;
		}

		protected virtual IEnumerable<SvgPath> GetParts(SvgPath item)
		{
			if (!NBiometricTypes.IsPositionSingleFinger(item.Position) && NBiometricTypes.IsPositionFinger(item.Position))
			{
				var availableParts = NBiometricTypes.GetPositionAvailableParts(item.Position, null);
				return _painter.Elements.Where(x => x.ItemType == ItemType.ItemPart && availableParts.Contains(x.Position));
			}
			return new SvgPath[0];
		}

		protected virtual SvgPath SelectHighlightedElement(IEnumerable<SvgPath> elements)
		{
			if (_allowHighlight)
			{
				return elements
					.Where(x => x.ItemType == ItemType.Item && x.HitTest(_mousePosition))
					.OrderBy(x => x.Position)
					.FirstOrDefault();
			}
			return null;
		}

		protected void TimerTick(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new EventHandler(TimerTick), sender, e);
			}
			else
			{
				_rollAngle = (_rollAngle + 15) % 360;
				Invalidate();
			}
		}

		protected virtual void OnDataChanged()
		{
			if (DesignMode) return;

			bool needsRepaint = false;
			IEnumerable<SvgPath> elements = GetAvailableElements(true);
			SvgPath first = SelectHighlightedElement(elements);
			SvgPath firstPart = null;
			if (first != null)
			{
				firstPart = GetParts(first).Where(x => x.HitTest(_mousePosition)).FirstOrDefault();
			}

			foreach (SvgPath item in elements)
			{
				Color c = Color.Transparent;
				if (item == first)
					c = _highlightColor;
				else if (item == firstPart)
					c = _highligthPartColor;
				else if (_missingPositions.Contains(item.Position))
					c = _missingFingerColor;
				else if (item.Position == _selectedPosition)
					c = _selectedFingerColor;
				if (item.FillColor != c)
				{
					item.FillColor = c;
					needsRepaint = true;
				}
			}

			if (IsRolled && NBiometricTypes.IsPositionSingleFinger(_selectedPosition))
			{
				if (!_rollStarted)
				{
					_rollStarted = true;
					timer.Start();
				}
				needsRepaint = true;
			}
			else if (_rollStarted)
			{
				timer.Stop();
				_rollStarted = false;
				needsRepaint = true;
			}

			if (needsRepaint) Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_allowHighlight)
			{
				_mousePosition = e.Location;

				OnDataChanged();
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (DesignMode) return;
			if (FingerClick != null && e.Button == MouseButtons.Left)
			{
				EventHandler<FingerClickArgs> onClick = FingerClick;

				IEnumerable<SvgPath> elements = GetAvailableElements(false);
				SvgPath first = SelectHighlightedElement(elements);
				SvgPath firstPart = null;
				if (first != null)
				{
					firstPart = GetParts(first).Where(x => x.HitTest(e.Location)).FirstOrDefault();

					FingerClickArgs args = new FingerClickArgs(first.Position, e);
					if (firstPart != null) args.PositionPart = firstPart.Position;
					onClick(this, args);
				}
			}

			base.OnMouseClick(e);
		}

		protected override void OnResize(EventArgs e)
		{
			Invalidate();

			base.OnResize(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (_painter != null)
			{
				_painter.Paint(e.Graphics, ClientSize);

				if (!DesignMode && _isRolled && NBiometricTypes.IsPositionSingleFinger(_selectedPosition))
				{
					_painter.PaintRotateForFinger(e.Graphics, ClientSize, _selectedPosition, _rollAngle);
				}
			}

			base.OnPaint(e);
		}

		#endregion
	}
}
