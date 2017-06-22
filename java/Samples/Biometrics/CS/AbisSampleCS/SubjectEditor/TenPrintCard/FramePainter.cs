using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Neurotec.Images;

namespace Neurotec.Samples.SubjectEditor.TenPrintCard
{
	sealed class FramePainter : Control
	{
		#region Nested types

		private enum MouseCapturing
		{
			NotCapturing,
			Moving,
			Resizing
		};

		private enum Position
		{
			Left = 1,
			Right = 2,
			Top = 4,
			Bottom = 8,
			Center = 16
		}

		private class FrameDefinition
		{
			#region Public types

			public class Text
			{
				#region Public Constructor

				public Text(string text, int size, Position alignment, bool drawCheckBox, bool isBold)
				{
					Value = text;
					Size = size;
					Alignment = alignment;
					DrawCheckBox = drawCheckBox;
					IsBold = isBold;
				}

				#endregion

				#region Public Properties

				public string Value { get; private set; }
				public int Size { get; private set; }
				public Position Alignment { get; private set; }
				public bool DrawCheckBox { get; private set; }
				public bool IsBold { get; private set; }

				#endregion
			}

			public class Block
			{
				#region Public Consnstructor

				public Block(int x, int y, int width, int height, bool isFingerBlock)
				{
					X = x;
					Y = y;
					Width = width;
					Height = height;
					Cells = new List<Cell>();
					Frames = new List<FrameInfo>();
					IsFingerBlock = isFingerBlock;
				}

				#endregion

				#region Public properties

				public int X { get; private set; }
				public int Y { get; private set; }
				public int Width { get; private set; }
				public int Height { get; private set; }
				public bool IsFingerBlock { get; private set; }
				public List<Cell> Cells { get; private set; }
				public List<FrameInfo> Frames { get; private set; }

				#endregion
			}

			public class Cell
			{
				#region Public Contructor

				public Cell(int width, int height, bool drawRect, int fingerNumber)
				{
					Width = width;
					Height = height;
					DrawRect = drawRect;
					FingerNumber = fingerNumber;
					TextLines = new List<Text>();
				}

				#endregion

				#region Public properties

				public int Width { get; private set; }
				public int Height { get; private set; }
				public bool DrawRect { get; private set; }
				public int FingerNumber { get; private set; }
				public List<Text> TextLines { get; private set; }

				#endregion
			}

			public class FrameInfo
			{
				#region Public Constructor

				public FrameInfo(int size, Position positions)
				{
					Size = size;
					Positions = positions;
				}

				#endregion

				#region Public properties

				public int Size { get; private set; }
				public Position Positions { get; private set; }

				#endregion

			}

			#endregion

			#region Public constructor

			public FrameDefinition()
			{
				Blocks = new List<Block>();
				Color = Color.Red;
			}

			#endregion

			#region Public properties

			public List<Block> Blocks { get; private set; }
			public Color Color { get; set; }

			#endregion

		}

		#endregion

		#region Private fields

		private readonly FrameDefinition _frames;

		private int _x, _y;
		private int _width;
		private int _height;
		private double _aspect;

		private NImage _image;
		private Image _imageSmall;

		private Rectangle _imageRect;
		private double _currScale = 1;

		private int _mouseDownX, _mouseDownY;
		private MouseCapturing _mouseCapturing;

		#endregion

		#region Public constructor

		public FramePainter(string frameDefinition)
		{
			_frames = new FrameDefinition();

			try
			{
				ParseFrameDefinition(frameDefinition);
			}
			catch (Exception ex)
			{
				throw new Exception("Error parsing frame settings.\nError message: " + ex.Message);
			}

			var block = _frames.Blocks.Where(x => x.IsFingerBlock).FirstOrDefault();
			if (block != null)
			{
				_aspect = (double)block.Width / (double)block.Height;
			}

			SetStyle(ControlStyles.DoubleBuffer, true);
			BackColor = Color.FromKnownColor(KnownColor.White);

			MouseUp += new MouseEventHandler(OnMouseUp);
			MouseMove += new MouseEventHandler(OnMouseMove);
			MouseDown += new MouseEventHandler(OnMouseDown);
			SizeChanged += new EventHandler(OnSizeChanged);
		}

		#endregion

		#region Protected methods

		protected override void Dispose(bool disposing)
		{
			if (_image != null)
			{
				_image.Dispose();
				_image = null;
			}

			if (_imageSmall != null)
			{
				_imageSmall.Dispose();
				_imageSmall = null;
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			if (_image == null)
				return;

			DrawFrame(pe.Graphics);
		}

		protected override void OnPaintBackground(PaintEventArgs pe)
		{
			using (SolidBrush brush = new SolidBrush(BackColor))
			{
				pe.Graphics.FillRectangle(brush, 0, 0, Width, Height);
				DrawImage(pe.Graphics);
			}
		}

		#endregion

		#region Public methods

		public void SetImage(NImage img)
		{
			if (_image != null)
			{
				_image.Dispose();
				_image = null;
			}

			if (_imageSmall != null)
			{
				_imageSmall.Dispose();
				_imageSmall = null;
			}

			_image = img;
			OnSizeChanged(null, null);
			SetDefaultFramePosition();

			Refresh();
		}

		public Dictionary<int, NImage> GetFramedFingerprints()
		{
			Dictionary<int, Rectangle> splitting = GetFrameSplitting();
			Dictionary<int, NImage> images = new Dictionary<int, NImage>();

			foreach (int key in splitting.Keys)
			{
				Rectangle rect = splitting[key];

				NImage img = _image.Crop((uint)rect.Left, (uint)rect.Top, (uint)rect.Width, (uint)rect.Height);
				images.Add(key, img);
			}

			return images;
		}

		public Dictionary<int, Rectangle> GetFrameSplitting()
		{
			double scale = 1 / _currScale;
			int x = (int)Math.Round((_x - _imageRect.X) * scale);
			int y = (int)Math.Round((_y - _imageRect.Y) * scale);
			int width = (int)Math.Round(_width * scale);
			int height = (int)Math.Round(_height * scale);

			var areaList = GetFingerRectangles(new Rectangle(x, y, width, height));

			return areaList;
		}

		public void DrawForm(Graphics g)
		{
			var pen = new Pen(_frames.Color);
			foreach (var block in _frames.Blocks)
			{
				int currentWidth = 0, currentHeight = 0;
				foreach (var cell in block.Cells)
				{
					var cellRect = new Rectangle(block.X + currentWidth, block.Y + currentHeight, cell.Width, cell.Height);
					if (cell.DrawRect) g.DrawRectangle(pen, cellRect);

					var brush = new SolidBrush(_frames.Color);
					var endPoint = new Point();
					Position tmpAlignment = 0;
					foreach (var t in cell.TextLines.OrderBy(x => x.Alignment))
					{
						if (tmpAlignment != t.Alignment) endPoint = new Point();
						var textStyle = t.IsBold ? FontStyle.Bold : FontStyle.Regular;
						var testFont = new Font("Arial", t.Size, textStyle);

						var point = GetAlignmentPoint(g, cellRect, t, testFont, ref endPoint);
						g.DrawString(t.Value, testFont, brush, point);
						tmpAlignment = t.Alignment;

						if (t.DrawCheckBox) DrawCheckBox(g, pen, cellRect, point, endPoint);
					}
					// do not increment height if line contains more than one cell
					var height = cell.Width + currentWidth == block.Width ? cell.Height : 0;
					currentHeight = cell.Height + currentHeight <= block.Height ? currentHeight + height : 0;
					currentWidth = cell.Width + currentWidth < block.Width ? currentWidth + cell.Width : 0;
				}
				DrawBlockFrames(g, block);
			}
		}

		#endregion

		#region Private methods

		private void DrawCheckBox(Graphics g, Pen pen, Rectangle rect, Point startPoint, Point endPoint)
		{
			const int squareWidth = 16;
			int textCenter = startPoint.X - rect.X + (endPoint.X - startPoint.X) / 2;
			int x = rect.X + textCenter - squareWidth / 2;
			int y = rect.Y + 6;
			g.DrawRectangle(pen, x, y, squareWidth, squareWidth);
		}

		private void DrawBlockFrames(Graphics g, FrameDefinition.Block block)
		{
			foreach (var frame in block.Frames)
			{
				var pen = new Pen(_frames.Color, frame.Size);
				if ((frame.Positions & Position.Bottom) != 0)
				{
					g.DrawLine(pen, block.X, block.Y + block.Height, block.X + block.Width, block.Y + block.Height);
				}
				if ((frame.Positions & Position.Top) != 0)
				{
					g.DrawLine(pen, block.X, block.Y, block.X + block.Width, block.Y);
				}
				if ((frame.Positions & Position.Left) != 0)
				{
					g.DrawLine(pen, block.X, block.Y, block.X, block.Y + block.Height);
				}
				if ((frame.Positions & Position.Right) != 0)
				{
					g.DrawLine(pen, block.X + block.Width, block.Y, block.X + block.Width, block.Y + block.Height);
				}
			}
		}

		private Point GetAlignmentPoint(Graphics g, Rectangle rect, FrameDefinition.Text text, Font font, ref Point endPoint)
		{
			var size = g.MeasureString(text.Value, font);
			var stringWidth = (int)size.Width;
			var stringHeight = (int)size.Height;
			var point = new Point();

			bool centerIncluded = (text.Alignment & Position.Center) != 0;
			bool topBottomIncluded = (text.Alignment & (Position.Bottom | Position.Top)) != 0;
			bool leftRightIncluded = (text.Alignment & (Position.Left | Position.Right)) != 0;

			// vertical
			if ((text.Alignment & Position.Top) != 0)
			{
				point.Y = endPoint.Y == 0 || !centerIncluded ? rect.Y + 2 : endPoint.Y;
				endPoint.Y = point.Y + stringHeight;
			}
			else if ((text.Alignment & Position.Bottom) != 0)
			{
				endPoint.Y = endPoint.Y == 0 ? rect.Y + rect.Height - 4 : endPoint.Y - 1;
				point.Y = endPoint.Y - stringHeight;
			}
			else if (centerIncluded)
			{
				point.Y = endPoint.Y == 0 || leftRightIncluded ? rect.Y + (rect.Height - stringHeight) / 2 : endPoint.Y + 1;
				endPoint.Y = point.Y + stringHeight;
			}
			// horizontal
			if ((text.Alignment & Position.Left) != 0)
			{
				point.X = endPoint.X == 0 ? rect.X + 4 : endPoint.X + 1;
				endPoint.X = point.X + stringWidth;
			}
			else if ((text.Alignment & Position.Right) != 0)
			{
				endPoint.X = endPoint.X == 0 ? rect.X + Width - 2 : endPoint.X - 1;
				point.X = endPoint.X - stringWidth;
			}
			else if (centerIncluded)
			{
				point.X = endPoint.X == 0 || topBottomIncluded ? rect.X + (rect.Width - stringWidth) / 2 : endPoint.X + 1;
				endPoint.X = point.X + stringWidth;
			}

			return point;
		}

		private void SetDefaultFramePosition()
		{
			if (_image == null)
			{
				return;
			}

			_x = _imageRect.X;
			_y = _imageRect.Y;
			_width = _imageRect.Width;
			_height = _imageRect.Height;
			if ((double)_width / _height > _aspect)
			{
				_width = (int)Math.Round(_height * _aspect);
				_x += (_imageRect.Width - _width) / 2;
			}
			else
			{
				_height = (int)Math.Round(_width / _aspect);
				_y += (_imageRect.Height - _height) / 2;
			}
		}

		private bool InFrame(Point p)
		{
			return (p.X < _width + _x) && (p.Y < _height + _y) && (p.X > _x) && (p.Y > _y);
		}

		private bool InSizeFrame(Point p)
		{
			return (p.X < _width + _x + 5) && (p.Y < _height + _y + 5) && (p.X > _width + _x - 5) && (p.Y > _height + _y - 5);
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (InSizeFrame(e.Location) || _mouseCapturing == MouseCapturing.Resizing)
			{
				Cursor = Cursors.SizeNWSE;
			}
			else
				if (InFrame(e.Location) || _mouseCapturing == MouseCapturing.Moving)
					Cursor = Cursors.SizeAll;
				else
					Cursor = Cursors.Default;

			switch (_mouseCapturing)
			{
				case MouseCapturing.Resizing:
					if (e.X - _mouseDownX > 250)
					{
						_width = e.X - _mouseDownX;
						_height = (int)Math.Round(_width / _aspect);
					}
					Refresh();
					break;
				case MouseCapturing.Moving:
					_x = e.X - _mouseDownX;
					_y = e.Y - _mouseDownY;
					Refresh();
					break;
			}
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (InSizeFrame(e.Location))
			{
				_mouseDownX = e.X - _width;
				_mouseDownY = e.Y - _height;
				Capture = true;
				_mouseCapturing = MouseCapturing.Resizing;
			}
			else
				if (InFrame(e.Location))
				{
					_mouseDownX = e.X - _x;
					_mouseDownY = e.Y - _y;
					Capture = true;
					_mouseCapturing = MouseCapturing.Moving;
				}
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (_mouseCapturing == MouseCapturing.NotCapturing) return;
			Capture = false;
			_mouseCapturing = MouseCapturing.NotCapturing;
		}

		private void ChangeScale(double newScale)
		{
			_x = (int)Math.Round(_x * newScale);
			_y = (int)Math.Round(_y * newScale);
			_width = (int)Math.Round(_width * newScale);
			_height = (int)Math.Round(_height * newScale);
		}

		private void OnSizeChanged(object sender, EventArgs e)
		{
			if ((Width == 0) || (Height == 0) || (_image == null))
			{
				if (_imageSmall != null)
				{
					_imageSmall.Dispose();
				}
				_imageSmall = null;
				Refresh();
				return;
			}

			int w, h;

			if (_image.Height > _image.Width)
			{
				h = Height;
				w = (int)((h * _image.Width) / _image.Height);
			}
			else
			{
				w = Width;
				h = (int)((w * _image.Height) / _image.Width);
			}

			if (_imageSmall != null)
			{
				_imageSmall.Dispose();
			}
			_imageSmall = null;

			if (w <= 0 || h <= 0)
			{
				Refresh();
				return;
			}

			_x -= _imageRect.X;
			_y -= _imageRect.Y;

			_imageRect = new Rectangle(Width / 2 - w / 2, Height / 2 - h / 2, w, h);
			_imageSmall = new Bitmap(w, h);

			using (Graphics g = Graphics.FromImage(_imageSmall))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				using (Bitmap bmp = _image.ToBitmap())
				{
					g.DrawImage(bmp, 0, 0, w, h);
				}
			}

			ChangeScale(1 / _currScale);
			_currScale = (double)w / _image.Width;
			ChangeScale(_currScale);

			_x += _imageRect.X;
			_y += _imageRect.Y;

			Refresh();
		}

		private void DrawImage(Graphics g)
		{
			if (_imageSmall != null)
				g.DrawImage(_imageSmall, _imageRect);
		}

		private void ParseFrameDefinition(string frameDefinition)
		{
			StringReader stringReader = new StringReader(frameDefinition);
			XmlReader reader = XmlReader.Create(stringReader);

			reader.Read();

			var c = reader.GetAttribute("color");
			if (c != null)
			{
				_frames.Color = Color.FromName(c);
			}

			while (reader.Read())
			{
				if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "block"))
				{
					int x = reader.GetAttribute("x") != null ? int.Parse(reader.GetAttribute("x")) : 0;
					int y = reader.GetAttribute("y") != null ? int.Parse(reader.GetAttribute("y")) : 0;
					int height = reader.GetAttribute("height") != null ? int.Parse(reader.GetAttribute("height")) : 0;
					int width = reader.GetAttribute("width") != null ? int.Parse(reader.GetAttribute("width")) : 0;
					bool isFingerBlock = reader.GetAttribute("isFingerBlock") != null ? bool.Parse(reader.GetAttribute("isFingerBlock")) : false;

					var block = new FrameDefinition.Block(x, y, width, height, isFingerBlock);
					_frames.Blocks.Add(block);
					ReadCellInformation(reader, block);
				}
			}

			reader.Close();
		}

		private Position ParseAlignment(string alignment)
		{
			var values = alignment.Split(',');
			Position align = 0;
			foreach (var value in values)
			{
				align |= (Position)Enum.Parse(typeof(Position), value, true);
			}
			return align;
		}

		private void ReadTextInformation(XmlReader reader, FrameDefinition.Cell cell)
		{
			if (reader.ReadToDescendant("text"))
			{
				do
				{
					if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "text"))
					{
						int size = int.Parse(reader.GetAttribute("size"));
						string value = reader.GetAttribute("value").Replace("\\n", Environment.NewLine);
						Position alignment = ParseAlignment(reader.GetAttribute("alignment"));
						bool drawSquare = reader.GetAttribute("square") != null ? bool.Parse(reader.GetAttribute("square")) : false;
						bool isBold = reader.GetAttribute("bold") != null ? bool.Parse(reader.GetAttribute("bold")) : false;
						cell.TextLines.Add(new FrameDefinition.Text(value, size, alignment, drawSquare, isBold));
					}
				} while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement));
			}
		}

		private void ReadCellInformation(XmlReader reader, FrameDefinition.Block block)
		{
			if (reader.ReadToDescendant("cell") || reader.ReadToDescendant("frame"))
			{
				do
				{
					if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "cell"))
					{
						int h = reader.GetAttribute("height") != null ? int.Parse(reader.GetAttribute("height")) : block.Height;
						int w = reader.GetAttribute("width") != null ? int.Parse(reader.GetAttribute("width")) : block.Width;
						bool drawRect = reader.GetAttribute("drawRect") != null ? bool.Parse(reader.GetAttribute("drawRect")) : true;
						int fingerNo = reader.GetAttribute("fingerNo") != null ? int.Parse(reader.GetAttribute("fingerNo")) : -1;
						var cell = new FrameDefinition.Cell(w, h, drawRect, fingerNo);
						block.Cells.Add(cell);
						ReadTextInformation(reader, cell);
					}
					if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "frame"))
					{
						int size = int.Parse(reader.GetAttribute("size"));
						Position alignment = ParseAlignment(reader.GetAttribute("positions"));
						block.Frames.Add(new FrameDefinition.FrameInfo(size, alignment));
					}
				} while (reader.Read() && (reader.NodeType != XmlNodeType.EndElement));
			}
		}

		private void DrawFrame(Graphics g)
		{
			if (_width <= 0 || _height <= 0) return;

			var p = new Pen(_frames.Color, 2);

			g.TranslateTransform(_x, _y);
			g.SmoothingMode = SmoothingMode.AntiAlias;

			g.DrawRectangle(p, 0, 0, _width, _height);
			g.FillRectangle(Brushes.White, _width - 3, _height - 3, 5, 5);
			g.DrawRectangle(p, _width - 3, _height - 3, 5, 5);

			var rect = GetFingerRectangles(new Rectangle(0, 0, _width, _height));
			for(var i = 1; i <= 14; i++)
			{
				g.DrawRectangle(p, rect[i]);
			}
		}

		private Dictionary<int, Rectangle> GetFingerRectangles(Rectangle fingerFramePos)
		{
			var areaList = new Dictionary<int, Rectangle>();

			var block = _frames.Blocks.First(b => b.IsFingerBlock);
			var widthRatio = fingerFramePos.Width != 0 ? fingerFramePos.Width / (double)block.Width : 1;
			var heightRatio = fingerFramePos.Height != 0 ? fingerFramePos.Height / (double)block.Height : 1;

			int currentWidth = 0, currentHeight = 0;
			foreach (var cell in block.Cells)
			{
				int cellX = (int)((block.X + currentWidth) * widthRatio) + fingerFramePos.X;
				int cellY = (int)((block.Y + currentHeight) * heightRatio) + fingerFramePos.Y;
				int cellW = (int)(widthRatio * cell.Width);
				int cellH = (int)(heightRatio * cell.Height);
				if (cellX < 0) cellX = 0;
				if (cellY < 0) cellY = 0;
				if (_image != null)
				{
					if (cellX + cellW > _image.Width) cellW = (int)_image.Width - cellX;
					if (cellY + cellH > _image.Height) cellH = (int)_image.Height - cellY;
				}
				areaList.Add(cell.FingerNumber, new Rectangle(cellX, cellY, cellW, cellH));

				// do not increment height if line contains more than one cell
				var tmpHeight = cell.Width + currentWidth == block.Width ? cell.Height : 0;
				currentHeight = cell.Height + currentHeight <= block.Height ? currentHeight + tmpHeight : 0;
				currentWidth = cell.Width + currentWidth < block.Width ? currentWidth + cell.Width : 0;
			}

			return areaList;
		}

		#endregion

	}

}
