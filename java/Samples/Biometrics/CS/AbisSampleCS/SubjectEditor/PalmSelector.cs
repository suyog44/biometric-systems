using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class PalmSelector : FingerSelector
	{
		#region Private fields
		/// <summary>
		/// The DesignMode property does not correctly tell you if you are in design mode
		/// see https://connect.microsoft.com/VisualStudio/feedback/details/553305
		/// </summary>
		protected bool isConstructorCompleted = false;
		#endregion

		#region Public constructor

		public PalmSelector()
		{
			InitializeComponent();

			base._allowedPositions.Clear();
			base._allowedPositions.AddRange(new[]
			{
				NFPosition.RightUpperPalm,
				NFPosition.RightLowerPalm,
				NFPosition.RightInterdigital,
				NFPosition.RightHypothenar,
				NFPosition.RightThenar,
				NFPosition.RightFullPalm,
				NFPosition.LeftUpperPalm,
				NFPosition.LeftLowerPalm,
				NFPosition.LeftInterdigital,
				NFPosition.LeftHypothenar,
				NFPosition.LeftThenar,
				NFPosition.LeftFullPalm
			});

			DoubleBuffered = true;
			isConstructorCompleted = true;
		}

		#endregion

		#region Private fields

		private NFPosition preferedPosition = NFPosition.UnknownPalm;

		#endregion

		#region Public properties

		[Browsable(false)]
		public override NFPosition[] AllowedPositions
		{
			get { return base.AllowedPositions; }
			set
			{
				if (DesignMode || !isConstructorCompleted) return;
				base._allowedPositions.Clear();
				if (value != null)
				{
					var goodValues = value.Where(x => NBiometricTypes.IsPositionKnown(x) && NBiometricTypes.IsPositionPalm(x));
					base._allowedPositions.AddRange(goodValues);
				}
				_painter.Clear();
				OnDataChanged();
				Invalidate();
			}
		}

		[Browsable(false)]
		public override bool IsRolled
		{
			get { return base.IsRolled; }
			set
			{
			}
		}

		[Browsable(false)]
		public override NFPosition[] MissingPositions
		{
			get { return base.MissingPositions; }
			set
			{
			}
		}

		#endregion

		#region Protected methods

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (base._allowHighlight && e.Location != _mousePosition)
			{
				ShowPositionTooltip(e);
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var hit = GetHitElements(e.Location).ToList();
				if (hit.Count >= 2)
				{
					int index = hit.IndexOf(preferedPosition);
					if (index == -1)
						index = 1;
					else if (index + 1 == hit.Count)
						index = 0;
					else
						index++;

					preferedPosition = hit[index];
					ShowPositionTooltip(e);
				}
			}
			base.OnMouseDown(e);
		}

		protected override void OnDataChanged()
		{
			base.OnDataChanged();
		}

		protected override SvgPath SelectHighlightedElement(IEnumerable<SvgPath> elements)
		{
			if (_allowHighlight)
			{
				return elements
					.Where(x => x.ItemType == ItemType.Item && x.HitTest(_mousePosition))
					.OrderByDescending(x => x.Position == preferedPosition)
					.ThenBy(x => x.Position)
					.FirstOrDefault();
			}
			return null;
		}

		#endregion

		#region Private methods

		private IEnumerable<NFPosition> GetHitElements(Point location)
		{
			return GetAvailableElements(false).Where(x => x.HitTest(location)).Select(x => x.Position);
		}

		private IEnumerable<NFPosition> SortHitElements(IEnumerable<NFPosition> items)
		{
			if (items.Count() == 0) return items;
			else
			{
				var list = items.ToList();
				if (preferedPosition != NFPosition.UnknownPalm && list.Exists(x => x == preferedPosition))
				{
					return list.OrderByDescending(x => x == preferedPosition);
				}
				return list;
			}
		}

		private void ShowPositionTooltip(MouseEventArgs e)
		{
			var hit = SortHitElements(GetHitElements(e.Location));
			if (hit.Count() == 0)
			{
				toolTip.Hide(this);
			}
			else
			{
				NFPosition bestFit = hit.First();
				string msg = GenerateTooltipMessage(hit, bestFit);
				toolTip.ToolTipTitle = string.Format("Position: {0}", bestFit);
				toolTip.Show(msg, this, e.Location.X + 15, e.Location.Y + 15);
			}
		}

		private string GenerateTooltipMessage(IEnumerable<NFPosition> hit, NFPosition bestFit)
		{
			if (hit.Count() <= 1)
				return " ";

			string result = "This is also: ";
			foreach (NFPosition item in hit)
			{
				if (item != bestFit)
					result += item.ToString() + ", ";
			}
			result += "\nClick right mouse button to show other position";
			return result;
		}

		#endregion
	}
}
