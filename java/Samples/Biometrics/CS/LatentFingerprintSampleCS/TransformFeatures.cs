using System;
using Neurotec.Biometrics;
using Neurotec.Images;

namespace Neurotec.Samples
{
	static class TransformFeatures
	{
		#region Private static methods

		private static double FlipFeatureAngleHorizontally(double angle)
		{
			angle = Math.PI - angle;
			while (angle > Math.PI)
			{
				angle -= Math.PI * 2;
			}
			return angle;
		}

		private static double FlipFeatureAngleVertically(double angle)
		{
			return -angle;
		}

		private static double RotateFeatureAngle(double angle, double rotateAngle)
		{
			angle -= rotateAngle;

			if (angle < -Math.PI)
			{
				angle += Math.PI * 2;
			}
			else if (angle > Math.PI)
			{
				angle -= Math.PI * 2;
			}

			return angle;
		}

		#endregion

		#region Public static methods

		public static NFRecord FlipHorizontally(NFRecord template)
		{
			var record = new NFRecord(template.Width, template.Height, template.HorzResolution, template.VertResolution)
			{
				MinutiaFormat = template.MinutiaFormat,
				CbeffProductType = template.CbeffProductType,
				RidgeCountsType = template.RidgeCountsType
			};

			var templateWidth = (int)((double)template.Width * NFRecord.Resolution / template.HorzResolution);
			foreach (NFMinutia minutia in template.Minutiae)
			{
				NFMinutia newMinutia = minutia;
				newMinutia.X = (ushort)(templateWidth - minutia.X - 1);
				newMinutia.Angle = FlipFeatureAngleHorizontally(newMinutia.Angle);
				record.Minutiae.Add(newMinutia);
			}
			foreach (NFDelta delta in template.Deltas)
			{
				NFDelta newDelta = delta;
				newDelta.X = (ushort)(templateWidth - delta.X - 1);
				newDelta.Angle1 = FlipFeatureAngleHorizontally(newDelta.Angle1);
				newDelta.Angle2 = FlipFeatureAngleHorizontally(newDelta.Angle2);
				newDelta.Angle3 = FlipFeatureAngleHorizontally(newDelta.Angle3);
				record.Deltas.Add(newDelta);
			}
			foreach (NFCore core in template.Cores)
			{
				NFCore newCore = core;
				newCore.X = (ushort)(templateWidth - core.X - 1);
				newCore.Angle = FlipFeatureAngleHorizontally(newCore.Angle);
				record.Cores.Add(newCore);
			}
			foreach (NFDoubleCore doubleCore in template.DoubleCores)
			{
				NFDoubleCore newDoubleCore = doubleCore;
				newDoubleCore.X = (ushort)(templateWidth - doubleCore.X - 1);
				record.DoubleCores.Add(newDoubleCore);
			}

			return record;
		}

		public static NFRecord FlipVertically(NFRecord template)
		{
			var record = new NFRecord(template.Width, template.Height, template.HorzResolution, template.VertResolution)
			{
				MinutiaFormat = template.MinutiaFormat,
				CbeffProductType = template.CbeffProductType,
				RidgeCountsType = template.RidgeCountsType
			};

			var templateHeight = (int)((double)template.Height * NFRecord.Resolution / template.VertResolution);
			foreach (NFMinutia minutia in template.Minutiae)
			{
				NFMinutia newMinutia = minutia;
				newMinutia.Y = (ushort)(templateHeight - minutia.Y - 1);
				newMinutia.Angle = FlipFeatureAngleVertically(newMinutia.Angle);
				record.Minutiae.Add(newMinutia);
			}
			foreach (NFDelta delta in template.Deltas)
			{
				NFDelta newDelta = delta;
				newDelta.Y = (ushort)(templateHeight - delta.Y - 1);
				newDelta.Angle1 = FlipFeatureAngleVertically(newDelta.Angle1);
				newDelta.Angle2 = FlipFeatureAngleVertically(newDelta.Angle2);
				newDelta.Angle3 = FlipFeatureAngleVertically(newDelta.Angle3);
				record.Deltas.Add(newDelta);
			}
			foreach (NFCore core in template.Cores)
			{
				NFCore newCore = core;
				newCore.Y = (ushort)(templateHeight - core.Y - 1);
				newCore.Angle = FlipFeatureAngleVertically(newCore.Angle);
				record.Cores.Add(newCore);
			}
			foreach (NFDoubleCore doubleCore in template.DoubleCores)
			{
				NFDoubleCore newDoubleCore = doubleCore;
				newDoubleCore.Y = (ushort)(templateHeight - doubleCore.Y - 1);
				record.DoubleCores.Add(newDoubleCore);
			}

			return record;
		}

		public static NFRecord Rotate90(NFRecord template)
		{
			var record = new NFRecord(template.Height, template.Width, template.VertResolution, template.HorzResolution)
			{
				MinutiaFormat = template.MinutiaFormat,
				CbeffProductType = template.CbeffProductType,
				RidgeCountsType = template.RidgeCountsType,
			};

			var templateHeight = (int)((double)template.Height * NFRecord.Resolution / template.VertResolution);
			foreach (NFMinutia minutia in template.Minutiae)
			{
				NFMinutia newMinutia = minutia;
				newMinutia.X = (ushort)(templateHeight - minutia.Y - 1);
				newMinutia.Y = (minutia.X);
				newMinutia.Angle = RotateFeatureAngle(newMinutia.Angle, -Math.PI / 2);
				record.Minutiae.Add(newMinutia);
			}
			foreach (NFDelta delta in template.Deltas)
			{
				NFDelta newDelta = delta;
				newDelta.X = (ushort)(templateHeight - delta.Y - 1);
				newDelta.Y = (delta.X);
				newDelta.Angle1 = RotateFeatureAngle(newDelta.Angle1, -Math.PI / 2);
				newDelta.Angle2 = RotateFeatureAngle(newDelta.Angle2, -Math.PI / 2);
				newDelta.Angle3 = RotateFeatureAngle(newDelta.Angle3, -Math.PI / 2);
				record.Deltas.Add(newDelta);
			}
			foreach (NFCore core in template.Cores)
			{
				NFCore newCore = core;
				newCore.X = (ushort)(templateHeight - core.Y - 1);
				newCore.Y = (core.X);
				newCore.Angle = RotateFeatureAngle(newCore.Angle, -Math.PI / 2);
				record.Cores.Add(newCore);
			}
			foreach (NFDoubleCore doubleCore in template.DoubleCores)
			{
				NFDoubleCore newDoubleCore = doubleCore;
				newDoubleCore.X = (ushort)(templateHeight - doubleCore.Y - 1);
				newDoubleCore.Y = (doubleCore.X);
				record.DoubleCores.Add(newDoubleCore);
			}

			return record;
		}

		public static NFRecord Rotate180(NFRecord template)
		{
			var record = new NFRecord(template.Width, template.Height, template.HorzResolution, template.VertResolution)
			{
				MinutiaFormat = template.MinutiaFormat,
				CbeffProductType = template.CbeffProductType,
				RidgeCountsType = template.RidgeCountsType
			};

			var templateWidth = (int)((double)template.Width * NFRecord.Resolution / template.HorzResolution);
			var templateHeight = (int)((double)template.Height * NFRecord.Resolution / template.VertResolution);
			foreach (NFMinutia minutia in template.Minutiae)
			{
				NFMinutia newMinutia = minutia;
				newMinutia.X = (ushort)(templateWidth - minutia.X - 1);
				newMinutia.Y = (ushort)(templateHeight - minutia.Y - 1);
				newMinutia.Angle = RotateFeatureAngle(newMinutia.Angle, Math.PI);
				record.Minutiae.Add(newMinutia);
			}
			foreach (NFDelta delta in template.Deltas)
			{
				NFDelta newDelta = delta;
				newDelta.X = (ushort)(templateWidth - delta.X - 1);
				newDelta.Y = (ushort)(templateHeight - delta.Y - 1);
				newDelta.Angle1 = RotateFeatureAngle(newDelta.Angle1, Math.PI);
				newDelta.Angle2 = RotateFeatureAngle(newDelta.Angle2, Math.PI);
				newDelta.Angle3 = RotateFeatureAngle(newDelta.Angle3, Math.PI);
				record.Deltas.Add(newDelta);
			}
			foreach (NFCore core in template.Cores)
			{
				NFCore newCore = core;
				newCore.X = (ushort)(templateWidth - core.X - 1);
				newCore.Y = (ushort)(templateHeight - core.Y - 1);
				newCore.Angle = RotateFeatureAngle(newCore.Angle, Math.PI);
				record.Cores.Add(newCore);
			}
			foreach (NFDoubleCore doubleCore in template.DoubleCores)
			{
				NFDoubleCore newDoubleCore = doubleCore;
				newDoubleCore.X = (ushort)(templateWidth - doubleCore.X - 1);
				newDoubleCore.Y = (ushort)(templateHeight - doubleCore.Y - 1);
				record.DoubleCores.Add(newDoubleCore);
			}

			return record;
		}

		public static NFRecord Rotate270(NFRecord template)
		{
			var record = new NFRecord(template.Height, template.Width, template.VertResolution, template.HorzResolution)
			{
				MinutiaFormat = template.MinutiaFormat,
				CbeffProductType = template.CbeffProductType,
				RidgeCountsType = template.RidgeCountsType,
			};

			var templateWidth = (int)((double)template.Width * NFRecord.Resolution / template.HorzResolution);
			foreach (NFMinutia minutia in template.Minutiae)
			{
				NFMinutia newMinutia = minutia;
				newMinutia.X = (minutia.Y);
				newMinutia.Y = (ushort)(templateWidth - minutia.X - 1);
				newMinutia.Angle = RotateFeatureAngle(newMinutia.Angle, Math.PI / 2);
				record.Minutiae.Add(newMinutia);
			}
			foreach (NFDelta delta in template.Deltas)
			{
				NFDelta newDelta = delta;
				newDelta.X = (delta.Y);
				newDelta.Y = (ushort)(templateWidth - delta.X - 1);
				newDelta.Angle1 = RotateFeatureAngle(newDelta.Angle1, Math.PI / 2);
				newDelta.Angle2 = RotateFeatureAngle(newDelta.Angle2, Math.PI / 2);
				newDelta.Angle3 = RotateFeatureAngle(newDelta.Angle3, Math.PI / 2);
				record.Deltas.Add(newDelta);
			}
			foreach (NFCore core in template.Cores)
			{
				NFCore newCore = core;
				newCore.X = (core.Y);
				newCore.Y = (ushort)(templateWidth - core.X - 1);
				newCore.Angle = RotateFeatureAngle(newCore.Angle, Math.PI / 2);
				record.Cores.Add(newCore);
			}
			foreach (NFDoubleCore doubleCore in template.DoubleCores)
			{
				NFDoubleCore newDoubleCore = doubleCore;
				newDoubleCore.X = (doubleCore.Y);
				newDoubleCore.Y = (ushort)(templateWidth - doubleCore.X - 1);
				record.DoubleCores.Add(newDoubleCore);
			}

			return record;
		}

		public static NFRecord Crop(NFRecord template, NImage image, uint x, uint y, uint width, uint height)
		{
			var record = new NFRecord((ushort)image.Width, (ushort)image.Height, template.HorzResolution, template.VertResolution)
			{
				MinutiaFormat = template.MinutiaFormat,
				CbeffProductType = template.CbeffProductType,
				RidgeCountsType = template.RidgeCountsType,
			};

			foreach (NFMinutia minutia in template.Minutiae)
			{
				if (minutia.X > x
					&& minutia.Y > y
					&& minutia.X - x < width
					&& minutia.Y - y < height)
				{
					NFMinutia newMinutia = minutia;
					newMinutia.X = (ushort)(minutia.X - x);
					newMinutia.Y = (ushort)(minutia.Y - y);
					record.Minutiae.Add(newMinutia);
				}
			}
			foreach (NFDelta delta in template.Deltas)
			{
				if (delta.X > x
					&& delta.Y > y
					&& delta.X - x < width
					&& delta.Y - y < height)
				{
					NFDelta newDelta = delta;
					newDelta.X = (ushort)(delta.X - x);
					newDelta.Y = (ushort)(delta.Y - y);
					record.Deltas.Add(newDelta);
				}
			}
			foreach (NFCore core in template.Cores)
			{
				if (core.X > x
					&& core.Y > y
					&& core.X - x < width
					&& core.Y - y < height)
				{
					NFCore newCore = core;
					newCore.X = (ushort)(core.X - x);
					newCore.Y = (ushort)(core.Y - y);
					record.Cores.Add(newCore);
				}
			}
			foreach (NFDoubleCore doubleCore in template.DoubleCores)
			{
				if (doubleCore.X > x
					&& doubleCore.Y > y
					&& doubleCore.X - x < width
					&& doubleCore.Y - y < height)
				{
					NFDoubleCore newDoubleCore = doubleCore;
					newDoubleCore.X = (ushort)(doubleCore.X - x);
					newDoubleCore.Y = (ushort)(doubleCore.Y - y);
					record.DoubleCores.Add(newDoubleCore);
				}
			}

			return record;
		}

		#endregion
	}
}
