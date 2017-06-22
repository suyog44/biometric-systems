using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class MatchingResultView : UserControl
	{
		#region Public constructor

		public MatchingResultView()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private bool _linkEnabled = true;
		private NMatchingResult _matchingResult;

		#endregion

		#region Public properties

		public bool LinkEnabled
		{
			get { return _linkEnabled; }
			set
			{
				_linkEnabled = value;
				linkLabel.Enabled = value;
				linkLabel.ForeColor = value ? Color.Green : Color.Black;
				linkLabel.LinkBehavior = value ? LinkBehavior.SystemDefault : LinkBehavior.NeverUnderline;
			}
		}

		public NMatchingResult Result
		{
			get { return _matchingResult; }
			set
			{
				_matchingResult = value;
				linkLabel.Text = string.Empty;
				lblDetails.Text = string.Empty;
				if (_matchingResult != null)
				{
					linkLabel.Text = string.Format("Matched with '{0}', score  = {1}", _matchingResult.Id, _matchingResult.Score);
					lblDetails.Text = MatchingResultToString(_matchingResult);
				}
			}
		}

		public int MatchingThreshold { get; set; }

		#endregion

		#region Public events

		public event EventHandler LinkActivated;

		#endregion

		#region Protected methods

		protected override void Dispose(bool disposing)
		{
			_matchingResult = null;
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Private methods

		private string MatchingResultToString(NMatchingResult result)
		{
			int index;
			NMatchingDetails details = result.MatchingDetails;

			StringBuilder builder = new StringBuilder();
			if (details != null)
			{
				string belowThreshold = " (Below matching threshold)";
				if ((details.BiometricType & NBiometricType.Face) == NBiometricType.Face)
				{
					builder.AppendFormat("Face match details: score = {0}{1}", details.FacesScore, Environment.NewLine);
					index = 0;
					foreach (NLMatchingDetails faceDetails in details.Faces)
					{
						if (faceDetails.MatchedIndex != -1)
						{
							builder.AppendFormat("    face index {0}: matched with index {1}, score = {2}{3};{4}",
								index++, faceDetails.MatchedIndex, faceDetails.Score, faceDetails.Score < MatchingThreshold ? belowThreshold : string.Empty, Environment.NewLine);
						}
						else
							builder.AppendFormat("    face index {0}: doesn't match{1}", index++, Environment.NewLine);
					}
				}
				if ((details.BiometricType & NBiometricType.Finger) == NBiometricType.Finger)
				{
					builder.AppendFormat("Fingerprint match details: score = {0}{1}", details.FingersScore, Environment.NewLine);
					index = 0;
					foreach (NFMatchingDetails fngrDetails in details.Fingers)
					{
						if (fngrDetails.MatchedIndex != -1)
						{
							builder.AppendFormat("    fingerprint index {0}: matched with index {1}, score = {2}{3};{4}",
								index++, fngrDetails.MatchedIndex, fngrDetails.Score, fngrDetails.Score < MatchingThreshold ? belowThreshold : string.Empty, Environment.NewLine);
						}
						else
							builder.AppendFormat("    fingerprint index: {0}: doesn't match{1}", index++, Environment.NewLine);
					}
				}
				if ((details.BiometricType & NBiometricType.Iris) == NBiometricType.Iris)
				{
					builder.AppendFormat("Irises match details: score = {0}{1}", details.IrisesScore, Environment.NewLine);
					index = 0;
					foreach (NEMatchingDetails irisesDetails in details.Irises)
					{
						if (irisesDetails.MatchedIndex != -1)
						{
							builder.AppendFormat("    irises index: {0}: matched with index {1}, score = {2}{3};{4}",
								index++, irisesDetails.MatchedIndex, irisesDetails.Score, irisesDetails.Score < MatchingThreshold ? belowThreshold : string.Empty, Environment.NewLine);
						}
						else
							builder.AppendFormat("    irises index: {0}: doesn't match{1}", index++, Environment.NewLine);
					}
				}
				if ((details.BiometricType & NBiometricType.Palm) == NBiometricType.Palm)
				{
					builder.AppendFormat("Palmprint match details: score = {0}{1}", details.PalmsScore, Environment.NewLine);
					index = 0;
					foreach (NFMatchingDetails fngrDetails in details.Palms)
					{
						if (fngrDetails.MatchedIndex != -1)
						{
							builder.AppendFormat("    palmprint index {0}: matched with index {1}, score = {2}{3};{4}",
								index++, fngrDetails.MatchedIndex, fngrDetails.Score, fngrDetails.Score < MatchingThreshold ? belowThreshold : string.Empty, Environment.NewLine);
						}
						else
							builder.AppendFormat("    palmprint index: {0}: doesn't match{1}", index++, Environment.NewLine);
					}
				}
				if ((details.BiometricType & NBiometricType.Voice) == NBiometricType.Voice)
				{
					builder.AppendFormat("Voice match details: score = {0}{1}", details.VoicesScore, Environment.NewLine);
					index = 0;
					foreach (NSMatchingDetails voicesDetails in details.Voices)
					{
						if (voicesDetails.MatchedIndex != -1)
						{
							builder.AppendFormat("    voices index {0}: matched with index {1}, score = {2}{3};{4}",
								index++, voicesDetails.MatchedIndex, voicesDetails.Score, voicesDetails.Score < MatchingThreshold ? belowThreshold : string.Empty, Environment.NewLine);
						}
						else
							builder.AppendFormat("    voices index {0}: doesn't match{1}", index++, Environment.NewLine);
					}
				}

				if ((details.BiometricType & (NBiometricType.Finger | NBiometricType.Palm | NBiometricType.Iris | NBiometricType.Voice | NBiometricType.Face)) == NBiometricType.None)
				{
					builder.AppendFormat(" score = {0}", details.Score);
				}
			}

			return builder.ToString();
		}

		private void LinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (LinkActivated != null)
			{
				LinkActivated(this, EventArgs.Empty);
			}
		}

		#endregion
	}
}
