using System;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class VoiceView : UserControl
	{
		#region Public constructor

		public VoiceView()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NVoice _voice;

		#endregion

		#region Public properties

		public NVoice Voice
		{
			get { return _voice; }
			set
			{
				_voice = value;
				if (value != null)
				{
					lblPhraseId.Text = value.PhraseId.ToString();
					Phrase phrase = SettingsManager.Phrases.FirstOrDefault(x => x.Id == value.PhraseId);
					lblPhrase.Text = phrase != null ? phrase.String : "N/A";

					NSAttributes attributes = _voice.Objects.FirstOrDefault();
					if (attributes != null)
					{
						byte quality = attributes.Quality;
						switch (quality)
						{
							case NBiometricTypes.QualityUnknown:
								lblQuality.Text = "N/A";
								break;
							case NBiometricTypes.QualityFailed:
								lblQuality.Text = "Failed to determine quality";
								break;
							default:
								lblQuality.Text = attributes.Quality.ToString();
								break;
						}

						bool hasTimespan = attributes.VoiceStart != TimeSpan.Zero || attributes.VoiceDuration != TimeSpan.Zero;
						lblStart.Text = hasTimespan ? attributes.VoiceStart.ToString() : "N/A";
						lblDuration.Text = hasTimespan ? attributes.VoiceDuration.ToString() : "N/A";
					}
					else
					{
						lblStart.Text = lblDuration.Text = lblQuality.Text = "N/A";
					}

					
				}
				else
				{
					lblStart.Text = lblDuration.Text = lblQuality.Text = "N/A";
					lblPhraseId.Text = "-1";
					lblPhrase.Text = "N/A";
				}
			}
		}

		#endregion
	}
}
