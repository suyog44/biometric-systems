using System.Collections.Generic;

namespace Neurotec.Samples
{
	public static class ResolutionUnits
	{
		#region Nested types

		public class Unit
		{
			#region Private fields

			private readonly string _name;
			private readonly double _relationWithMeter;

			#endregion

			#region Public constructor

			public Unit(string name, double relationWithMeter)
			{
				_name = name;
				_relationWithMeter = relationWithMeter;
			}

			#endregion

			#region Public methods

			public static double Convert(Unit sourceUnit, Unit destinationUnit, double value)
			{
				return value * destinationUnit._relationWithMeter / sourceUnit._relationWithMeter;
			}

			public override string ToString()
			{
				return _name;
			}

			#endregion
		}

		#endregion

		#region Private static fields

		private static readonly Unit _ppmUnit = new Unit("ppm", 1.0);
		private static readonly Unit _ppcmUnit = new Unit("ppcm", 0.01);
		private static readonly Unit _ppmmUnit = new Unit("ppmm", 0.001);
		private static readonly Unit _ppiUnit = new Unit("ppi", 0.0254);
		private static readonly List<Unit> _resolutionUnits = new List<Unit>();

		#endregion

		#region Static constructor

		static ResolutionUnits()
		{
			_resolutionUnits.Add(_ppmUnit);
			_resolutionUnits.Add(_ppcmUnit);
			_resolutionUnits.Add(_ppmmUnit);
			_resolutionUnits.Add(_ppiUnit);
		}

		#endregion

		#region Public static properties

		public static List<Unit> Units
		{
			get
			{
				return _resolutionUnits;
			}
		}

		public static Unit PpmUnit
		{
			get
			{
				return _ppmUnit;
			}
		}

		public static Unit PpcmUnit
		{
			get
			{
				return _ppcmUnit;
			}
		}

		public static Unit PpmmUnit
		{
			get
			{
				return _ppmmUnit;
			}
		}

		public static Unit PpiUnit
		{
			get
			{
				return _ppiUnit;
			}
		}

		#endregion
	}
}
