using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Neurotec.Samples.Code
{
	public class Utilities
	{
		private const string ErrorTitle = "Error";
		private const string InformationTitle = "Information";
		private const string QuestionTitle = "Question";

		public static string GetCurrentApplicationLocation()
		{
			return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;
		}

		public static string GetCurrentApplicationName()
		{
			return Assembly.GetEntryAssembly().GetName().Name;
		}

		public static void ShowError(string message)
		{
			MessageBox.Show(message, string.Format("{0}: {1}", GetCurrentApplicationName(), ErrorTitle), MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowError(Exception ex)
		{
			ShowError(ex.ToString());
		}

		public static void ShowError(string format, params object[] args)
		{
			string str = string.Format(format, args);
			ShowError(str);
		}

		public static void ShowInformation(string message)
		{
			MessageBox.Show(message, string.Format("{0}: {1}", GetCurrentApplicationName(), InformationTitle), MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static void ShowInformation(string format, params object[] args)
		{
			string str = string.Format(format, args);
			ShowInformation(str);
		}

		public static bool ShowQuestion(string message)
		{
			return DialogResult.Yes == MessageBox.Show(message, string.Format("{0}: {1}", GetCurrentApplicationName(), QuestionTitle), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public static bool ShowQuestion(string format, params object[] args)
		{
			string str = string.Format(format, args);
			return ShowQuestion(str);
		}

		public static string MatchingThresholdToString(int value)
		{
			double p = -value / 12.0;
			return string.Format(string.Format("{{0:P{0}}}", Math.Max(0, (int)Math.Ceiling(-p) - 2)), Math.Pow(10, p));
		}

		public static int MatchingThresholdFromString(string value)
		{
			double p = Math.Log10(Math.Max(double.Epsilon, Math.Min(1,
				double.Parse(value.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "")) / 100)));
			return Math.Max(0, (int)Math.Round(-12 * p));
		}

		public static int MaximalRotationToDegrees(byte value)
		{
			return (2 * value * 360 + 256) / (2 * 256);
		}

		public static byte MaximalRotationFromDegrees(int value)
		{
			return (byte)((2 * value * 256 + 360) / (2 * 360));
		}
	}
}
