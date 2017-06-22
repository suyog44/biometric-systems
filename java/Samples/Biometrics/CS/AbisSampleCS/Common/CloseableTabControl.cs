using System.Drawing;
using System.Windows.Forms;
using Neurotec.Samples.Properties;

namespace Neurotec.Samples
{
	public partial class CloseableTabControl : TabControl
	{
		#region Public constructor

		public CloseableTabControl()
		{
			InitializeComponent();
			DrawMode = TabDrawMode.OwnerDrawFixed;
			DoubleBuffered = true;
			Point p = Padding;
			p.X += 10;
			Padding = p;
			LastPageIndex = -1;
		}

		#endregion

		#region Private fields

		private Rectangle mouseOn;

		#endregion

		#region Public properties

		public int LastPageIndex { get; set; }

		#endregion

		#region Protected methods

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			Graphics g = e.Graphics;

			Rectangle r = e.Bounds;
			using (Brush b = new SolidBrush(SelectedIndex == e.Index ? SystemColors.Window : BackColor))
			{
				g.FillRectangle(b, r);
			}

			r = GetTabRect(e.Index);
			TabPage page = TabPages[e.Index];
			string title = page.Text;
			SizeF textSize = g.MeasureString(title, e.Font);
			g.DrawString(title, e.Font, Brushes.Black, r);

			CloseableTabPage closeable = page as CloseableTabPage;
			if (closeable != null && closeable.CanClose)
			{
				r = GetCloseButtonBounds(e.Index);
				if (r.Contains(this.PointToClient(MousePosition)))
				{
					g.DrawImage(Resources.closeMouseOn, r);
				}
				else
				{
					g.DrawImage(Resources.close, r);
				}
			}

			base.OnDrawItem(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (!mouseOn.IsEmpty && !mouseOn.Contains(e.Location))
			{
				mouseOn = Rectangle.Empty;
				Invalidate();
				return;
			}
			else if (!mouseOn.IsEmpty && mouseOn.Contains(e.Location))
			{
				return;
			}

			for (int i = 0; i < TabPages.Count; i++)
			{
				CloseableTabPage page = TabPages[i] as CloseableTabPage;
				if (page != null && page.CanClose)
				{
					Rectangle r = GetCloseButtonBounds(i);
					if (r.Contains(e.Location))
					{
						mouseOn = r;
						Invalidate();
						return;
					}
				}
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			for (int i = 0; i < TabPages.Count; i++)
			{
				CloseableTabPage page = TabPages[i] as CloseableTabPage;
				if (page != null && page.CanClose)
				{
					Rectangle r = GetCloseButtonBounds(i);
					if (r.Contains(e.Location))
					{
						int currentPage = LastPageIndex;
						int removedPage = i;
						TabPages.Remove(page);
						if (TabPages.Count > 0)
						{
							if (currentPage == removedPage)
							{
								SelectedIndex = 0;
							}
							else
							{
								SelectedIndex = removedPage < currentPage ? currentPage - 1 : currentPage;
							}
						}
					}
				}
			}

			base.OnMouseClick(e);
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			CloseableTabPage control = e.Control as CloseableTabPage;
			if (control != null && control.Content != null)
			{
				control.Content.OnTabAdded();
			}

			base.OnControlAdded(e);
		}

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			CloseableTabPage control = e.Control as CloseableTabPage;
			if (control != null && control.Content != null)
			{
				control.Content.OnTabClose();
			}

			if (control != null)
				control.Dispose();
		}

		protected override void OnDeselecting(TabControlCancelEventArgs e)
		{
			if (e.TabPageIndex != -1)
			{
				LastPageIndex = e.TabPageIndex;
			}

			base.OnDeselecting(e);
		}

		#endregion

		#region Private methods

		private Rectangle GetCloseButtonBounds(int tabIndex)
		{
			Rectangle r = GetTabRect(tabIndex);
			const int offset = 5;
			int size = r.Height - offset * 2;
			Rectangle imageLocation = new Rectangle(0, offset, size, size);
			imageLocation.X = r.X + r.Width - size - offset;
			imageLocation.Y += r.Y;

			return imageLocation;
		}

		#endregion
	}
}
