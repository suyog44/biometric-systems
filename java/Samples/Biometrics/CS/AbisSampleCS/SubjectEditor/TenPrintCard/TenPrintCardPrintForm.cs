using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;

namespace Neurotec.Samples.SubjectEditor.TenPrintCard
{
	public partial class TenPrintCardPrintForm : PrintPreviewDialog
	{
		#region Private fields

		private readonly NFPosition[] _rolledPositions =
				{
					NFPosition.RightThumb, NFPosition.RightIndex, NFPosition.RightMiddle, NFPosition.RightRing, NFPosition.RightLittle,
					NFPosition.LeftThumb, NFPosition.LeftIndex, NFPosition.LeftMiddle, NFPosition.LeftRing, NFPosition.LeftLittle
				};

		private FramePainter _painter;
		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private Dictionary<int, NImage> _images;

		#endregion

		#region Public constructor

		public TenPrintCardPrintForm(string frameDefinition)
		{
			InitializeComponent();
			_painter = new FramePainter(frameDefinition);
			_images = new Dictionary<int, NImage>();
		}

		#endregion

		#region Public properties

		public NSubject Subject
		{
			get { return _subject; }
			set { _subject = value; }
		}

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set { _biometricClient = value; }
		}

		#endregion

		#region Events

		private void TenPrintCardPrintFormShown(object sender, EventArgs e)
		{
			var doc = new PrintDocument();
			doc.PrintPage += PrintPreviewEvent;
			doc.BeginPrint += BeginPrintEvent;
			Document = doc;
		}

		private void PrintPreviewEvent(object o, PrintPageEventArgs e)
		{
			var graphics = e.Graphics;
			var rect = _painter.GetFrameSplitting();

			graphics.TranslateTransform(13, 25);
			if (_subject.Fingers.Count > 0)
			{
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

				var subject = MakeSubject();

				SegmentFingers(subject);

				for (int i = 1; i <= 14; i++)
				{
					if (_images.ContainsKey(i))
					{
						var currrentRect = rect[i];
						var image = _images[i].ToBitmap();
						var tmpRect = new Rectangle(currrentRect.X + 2, currrentRect.Y + 2, currrentRect.Width - 4, currrentRect.Height - 4);

						var imagePosition = ImagePosition(tmpRect, image);
						graphics.DrawImage(image, imagePosition);
					}
				}
			}

			_painter.DrawForm(graphics);

		}

		private void BeginPrintEvent(object sender, PrintEventArgs e)
		{
			Document.DefaultPageSettings.PrinterResolution = new PrinterResolution { Kind = PrinterResolutionKind.Custom, X = 100, Y = 100 };
			Document.DefaultPageSettings.PaperSize = Document.PrinterSettings.PaperSizes.Cast<PaperSize>().Where(x => x.Kind == PaperKind.A4).First();
			if (!Document.PrintController.IsPreview)
			{
				printDialog1.Document = Document;
				printDialog1.AllowSelection = false;
				printDialog1.AllowSomePages = false;
				if (printDialog1.ShowDialog() != DialogResult.OK)
				{
					e.Cancel = true;
				}
			}
		}

		#endregion

		#region Private methods

		private NSubject MakeSubject()
		{
			var subject = new NSubject();
			var fingers = _subject.Fingers.Where(x => x.Image != null);
			var rolled = fingers.Where(x => NBiometricTypes.IsImpressionTypeRolled(x.ImpressionType))
				.Where(x => NBiometricTypes.IsPositionOneOf(x.Position, _rolledPositions))
				.Select(x => new NFinger { Image = x.Image, Position = x.Position, ImpressionType = x.ImpressionType })
				.GroupBy(x => x.Position)
				.Select(x => x.First());

			foreach (var finger in rolled)
			{
				subject.Fingers.Add(finger);
			}

			var thumbPositions = new NFPosition[] { NFPosition.LeftThumb, NFPosition.RightThumb, NFPosition.PlainRightThumb, NFPosition.PlainLeftThumb };
			var plain = fingers.Where(x => NBiometricTypes.IsImpressionTypePlain(x.ImpressionType))
				.Where(x => NBiometricTypes.IsPositionFourFingers(x.Position) || NBiometricTypes.IsPositionOneOf(x.Position, thumbPositions))
				.Select(x => new NFinger { Image = x.Image, Position = x.Position, ImpressionType = x.ImpressionType })
				.GroupBy(x => x.Position)
				.Select(x => x.First());

			foreach (var finger in plain)
			{
				subject.Fingers.Add(finger);
			}

			return subject;
		}

		private void SegmentFingers(NSubject subject)
		{
			_images.Clear();
			using (var task = _biometricClient.CreateTask(NBiometricOperations.Segment, subject))
			{
				_biometricClient.PerformTask(task);
				if (task.Status != NBiometricStatus.Ok)
				{
					throw new Exception("Segmentation failed");
				}

				var segmentedFingers = subject.Fingers.Where(x => x.ParentObject != null);
				var rolled = segmentedFingers.Where(x => NBiometricTypes.IsImpressionTypeRolled(x.ImpressionType));

				var rolledPosition = _rolledPositions.ToList();
				foreach (var finger in rolled)
				{
					if (rolledPosition.Contains(finger.Position))
					{
						int index = rolledPosition.IndexOf(finger.Position) + 1;
						_images.Add(index, finger.Image);
					}
				}

				var thumbs = segmentedFingers.Where(x => NBiometricTypes.IsImpressionTypePlain(x.ImpressionType))
					.Where(x => NBiometricTypes.IsPositionSingleFinger(x.Position) && x.Position.ToString().Contains("Thumb"));
				var leftThumb = thumbs.Where(x => NBiometricTypes.IsPositionLeft(x.Position)).FirstOrDefault();
				if (leftThumb != null)
				{
					_images.Add(12, leftThumb.Image);
				}

				var rightThumb = thumbs.Where(x => NBiometricTypes.IsPositionRight(x.Position)).FirstOrDefault();
				if (rightThumb != null)
				{
					_images.Add(13, rightThumb.Image);
				}

				var fourFingers = subject.Fingers.Where(x => x.ParentObject == null && NBiometricTypes.IsPositionFourFingers(x.Position));
				var leftFour = fourFingers.Where(x => NBiometricTypes.IsPositionLeft(x.Position)).FirstOrDefault();
				if (leftFour != null)
				{
					_images.Add(11, GetFingerImage(leftFour));
				}

				var rightFour = fourFingers.Where(x => NBiometricTypes.IsPositionRight(x.Position)).FirstOrDefault();
				if (rightFour != null)
				{
					_images.Add(14, GetFingerImage(rightFour));
				}
			}
		}

		private NImage GetFingerImage(NFinger finger)
		{
			Rectangle rect = finger.Objects.First().BoundingRect;
			for (int i = 1; i < finger.Objects.Count; i++)
			{
				rect = Rectangle.Union(rect, finger.Objects[i].BoundingRect);
			}

			var fingerImage = finger.Image;
			if (rect.X < 0) rect.X = 0;
			if (rect.Y < 0) rect.Y = 0;
			if (rect.Width + rect.X > fingerImage.Width)
				rect.Width = (int)fingerImage.Width - rect.X;
			if (rect.Height + rect.Y > fingerImage.Height)
				rect.Height = (int)fingerImage.Height - rect.Y;

			return finger.Image.Crop((uint)rect.X, (uint)rect.Y, (uint)rect.Width, (uint)rect.Height);
		}

		private Rectangle ImagePosition(Rectangle rect, Image image)
		{
			double widthRatio = 1.0 * rect.Width / image.Width;
			double heightRatio = 1.0 * rect.Height / image.Height;

			double ratio = widthRatio < heightRatio ? widthRatio : heightRatio;
			int width = (int)Math.Truncate(image.Width * ratio);
			int height = (int)Math.Truncate(image.Height * ratio);

			var rectangle = new Rectangle(rect.X, rect.Y, width, height);
			int centerWidth = rect.Width - width;
			if (centerWidth > 0)
			{
				rectangle.X += centerWidth / 2;
			}

			int centerHeight = rect.Height - height;
			if (centerHeight > 0)
			{
				rectangle.Y += centerHeight / 2;
			}

			return rectangle;
		}

		#endregion

	}
}
