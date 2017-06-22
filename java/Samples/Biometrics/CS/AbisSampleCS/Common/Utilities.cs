using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public class Utilities
	{
		private const string ERROR_TITLE = "Error";
		private const string INFORMATION_TITLE = "Information";
		private const string QUESTION_TITLE = "Question";

		/// <summary>
		/// Gets location for current applicaiton folder.
		/// </summary>
		/// <returns></returns>
		public static string GetCurrentApplicationLocation()
		{
			return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;
		}

		/// <summary>
		/// Returns the current application name.
		/// </summary>
		/// <returns></returns>
		public static string GetCurrentApplicationName()
		{
			return Assembly.GetEntryAssembly().GetName().Name;
		}

		/// <summary>
		/// Returns the current application version.
		/// </summary>
		/// <returns></returns>
		public static Version GetCurrentApplicationVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version;
		}

		/// <summary>
		/// Shows the error message with the exclamation mark.
		/// </summary>
		/// <param name="message">Message to be displayed.</param>
		public static void ShowError(string message)
		{
			MessageBox.Show(message, GetCurrentApplicationName() + ": " + ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// Shows the error message with the exclamation mark.
		/// </summary>
		/// <param name="ex">Exception.</param>
		public static void ShowError(Exception ex)
		{
			ShowError(ex.ToString());
		}

		/// <summary>
		/// Shows the error message with the exclamation mark.
		/// </summary>
		public static void ShowError(string format, params object[] args)
		{
			string str = string.Format(format, args);
			ShowError(str);
		}

		/// <summary>
		/// Shows the information message with the exclamation mark.
		/// </summary>
		/// <param name="message">Message to be displayed.</param>
		public static void ShowInformation(string message)
		{
			MessageBox.Show(message, GetCurrentApplicationName() + ": " + INFORMATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Shows the information message with the exclamation mark.
		/// </summary>
		public static void ShowInformation(string format, params object[] args)
		{
			string str = string.Format(format, args);
			ShowInformation(str);
		}

		/// <summary>
		/// Shows the question message with the question mark.
		/// </summary>
		/// <param name="message">Message to be displayed.</param>
		/// <returns>Returns the user response.</returns>
		public static bool ShowQuestion(string message)
		{
			return ShowQuestion((IWin32Window)null, message);
		}

		/// <summary>
		/// Shows the question message with the question mark.
		/// </summary>
		/// <returns>Returns the user response.</returns>
		public static bool ShowQuestion(string format, params object[] args)
		{
			return ShowQuestion((IWin32Window)null, format, args);
		}

		public static bool ShowQuestion(IWin32Window owner, string message, params object[] args)
		{
			string str = string.Format(message, args);
			return ShowQuestion(owner, str);
		}

		public static bool ShowQuestion(IWin32Window owner, string message)
		{
			if (DialogResult.Yes == MessageBox.Show(owner, message, GetCurrentApplicationName() + ": " + QUESTION_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets all filenames for the provided path and pattern.
		/// </summary>
		/// <param name="baseDir"></param>
		/// <param name="pattern"></param>
		/// <returns></returns>
		public static string[] GetAllFileNames(string baseDir, string pattern)
		{
			List<string> fileResults = new List<string>();

			Stack<string> directoryStack = new Stack<string>();
			directoryStack.Push(baseDir);

			while (directoryStack.Count > 0)
			{
				string currentDir = directoryStack.Pop();

				foreach (string fileName in Directory.GetFiles(currentDir, pattern))
				{
					fileResults.Add(fileName);
				}
			}

			return fileResults.ToArray();
		}

		public static bool MatchingThresholdEqual(int thresholdValue, string percentString)
		{
			if (percentString == null) return false;
			int intVal = MatchingThresholdFromString(percentString);
			return (intVal == thresholdValue);
		}

		public static string MatchingThresholdToString(int value)
		{
			double p = -value / 12.0;
			return string.Format(string.Format("{{0:P{0}}}", Math.Max(0, (int)Math.Ceiling(-p) - 2)), Math.Pow(10, p));
		}

		public static int MatchingThresholdFromString(string value)
		{
			double p = Math.Log10(Math.Max(double.Epsilon, Math.Min(1, double.Parse(value.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "")) / 100)));
			return Math.Max(0, (int)Math.Round(-12 * p));
		}

		public static string GetUserLocalDataDir(string productName)
		{
			string localDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			localDataDir = Path.Combine(localDataDir, "Neurotechnology");
			if (!Directory.Exists(localDataDir))
			{
				Directory.CreateDirectory(localDataDir);
			}
			localDataDir = Path.Combine(localDataDir, productName);
			if (!Directory.Exists(localDataDir))
			{
				Directory.CreateDirectory(localDataDir);
			}

			return localDataDir;
		}
	}
}
