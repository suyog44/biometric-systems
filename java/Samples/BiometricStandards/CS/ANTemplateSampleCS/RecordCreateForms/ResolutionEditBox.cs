using System;
using System.Windows.Forms;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ResolutionEditBox : UserControl
	{
		#region Private fields

		ResolutionUnits.Unit _currentUnit;

		#endregion

		#region Public constructor

		public ResolutionEditBox()
		{
			InitializeComponent();

			cbScaleUnits.Items.AddRange(ResolutionUnits.Units.ToArray());

			RawValue = 0;
			cbScaleUnits.SelectedItem = _currentUnit = ResolutionUnits.PpmUnit;
		}

		#endregion

		#region Public properties

		public double RawValue
		{
			get
			{
				return double.Parse(tbValue.Text);
			}
			internal set
			{
				tbValue.Text = value.ToString();
			}
		}

		public double PpmValue
		{
			get
			{
				return ResolutionUnits.Unit.Convert(_currentUnit, ResolutionUnits.PpmUnit, RawValue);
			}
			set
			{
				cbScaleUnits.SelectedItem = ResolutionUnits.PpmUnit;
				RawValue = value;
			}
		}

		public double PpcmValue
		{
			get
			{
				return ResolutionUnits.Unit.Convert(_currentUnit, ResolutionUnits.PpcmUnit, RawValue);
			}
			set
			{
				cbScaleUnits.SelectedItem = ResolutionUnits.PpcmUnit;
				RawValue = value;
			}
		}

		public double PpmmValue
		{
			get
			{
				return ResolutionUnits.Unit.Convert(_currentUnit, ResolutionUnits.PpmmUnit, RawValue);
			}
			set
			{
				cbScaleUnits.SelectedItem = ResolutionUnits.PpmmUnit;
				RawValue = value;
			}
		}

		public double PpiValue
		{
			get
			{
				return ResolutionUnits.Unit.Convert(_currentUnit, ResolutionUnits.PpiUnit, RawValue);
			}
			set
			{
				cbScaleUnits.SelectedItem = ResolutionUnits.PpiUnit;
				RawValue = value;
			}
		}

		#endregion

		#region Private form events

		private void CbScaleUnitsSelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbScaleUnits.SelectedItem != _currentUnit)
			{
				double newValue = ResolutionUnits.Unit.Convert(_currentUnit, (ResolutionUnits.Unit)cbScaleUnits.SelectedItem, RawValue);
				RawValue = newValue;
				_currentUnit = (ResolutionUnits.Unit)cbScaleUnits.SelectedItem;
			}
		}

		#endregion
	}
}
