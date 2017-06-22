using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Samples.Code;

namespace Neurotec.Samples.Controls
{
	public partial class SettingsPanel : ControlBase
	{
		#region Public constructor

		public SettingsPanel()
		{
			InitializeComponent();

			object[] thresholds =
			{
				Utilities.MatchingThresholdToString(12),
				Utilities.MatchingThresholdToString(24),
				Utilities.MatchingThresholdToString(36),
				Utilities.MatchingThresholdToString(48),
				Utilities.MatchingThresholdToString(60),
				Utilities.MatchingThresholdToString(72)
			};

			cbMatchingThreshold.Items.AddRange(thresholds);

			object[] speeds = { NMatchingSpeed.High, NMatchingSpeed.Medium, NMatchingSpeed.Low };
			cbFingersMatchingSpeed.Items.AddRange(speeds);
			cbFacesMatchingSpeed.Items.AddRange(speeds);
			cbIrisesMatchingSpeed.Items.AddRange(speeds);
			cbPalmsMatchingSpeed.Items.AddRange(speeds);
		}

		#endregion

		#region Private nested types

		private class Wrapper<T>
		{
			public T Value { get; private set; }
			private readonly string _displayName;

			public Wrapper(T value, string displayName)
			{
				Value = value;
				_displayName = displayName;
			}

			public override string ToString()
			{
				return _displayName;
			}

			public override bool Equals(object obj)
			{
				if (obj is Wrapper<T>)
				{
					var objCast = obj as Wrapper<T>;
					return objCast.Value.Equals(Value);
				}
				if (obj != null)
				{
					return obj.Equals(Value);
				}
				return Value == null || Value.Equals(obj);
			}

			public override int GetHashCode()
			{
				return Value.GetHashCode();
			}
		}

		#endregion

		#region Private form events

		private void MatchingThresholdValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				var target = sender as ComboBox;
				if (target == null) return;
				int value = Utilities.MatchingThresholdFromString(target.Text);
				target.Text = Utilities.MatchingThresholdToString(value);
			}
			catch
			{
				e.Cancel = true;
				MessageBox.Show(@"Matching threshold is invalid");
			}
		}

		private void BtnResetClick(object sender, EventArgs e)
		{
			SettingsAccesor.ResetMatchingSettings();
			LoadSettings();
		}

		private void BtnApplyClick(object sender, EventArgs e)
		{
			SettingsAccesor.MatchingThreshold = Utilities.MatchingThresholdFromString(cbMatchingThreshold.Text);
			SettingsAccesor.FingersMatchingSpeed = (NMatchingSpeed)cbFingersMatchingSpeed.SelectedItem;
			SettingsAccesor.FingersMaximalRotation = Utilities.MaximalRotationFromDegrees(Convert.ToInt32(nudFingersMaxRotation.Value));
			SettingsAccesor.FacesMatchingSpeed = (NMatchingSpeed)cbFacesMatchingSpeed.SelectedItem;
			SettingsAccesor.IrisesMatchingSpeed = (NMatchingSpeed)cbIrisesMatchingSpeed.SelectedItem;
			SettingsAccesor.IrisesMaximalRotation = Utilities.MaximalRotationFromDegrees(Convert.ToInt32(nudIrisesMaxRotation.Value));
			SettingsAccesor.PalmsMatchingSpeed = (NMatchingSpeed)cbPalmsMatchingSpeed.SelectedItem;
			SettingsAccesor.PalmsMaximalRotation = Utilities.MaximalRotationFromDegrees(Convert.ToInt32(nudPalmsMaxRotation.Value));
			SettingsAccesor.SaveSettings();
		}

		private void SettingsPanelLoad(object sender, EventArgs e)
		{
			LoadSettings();
		}

		#endregion

		#region Public methods

		public override string GetTitle()
		{
			return "Matching parameters";
		}

		#endregion

		#region Private methods

		private void LoadSettings()
		{
			SelectThreshold(cbMatchingThreshold, SettingsAccesor.MatchingThreshold);
			cbFingersMatchingSpeed.SelectedItem = SettingsAccesor.FingersMatchingSpeed;

			nudFingersMaxRotation.Value = Utilities.MaximalRotationToDegrees(SettingsAccesor.FingersMaximalRotation);
			nudIrisesMaxRotation.Value = Utilities.MaximalRotationToDegrees(SettingsAccesor.IrisesMaximalRotation);
			nudPalmsMaxRotation.Value = Utilities.MaximalRotationToDegrees(SettingsAccesor.PalmsMaximalRotation);
			cbFacesMatchingSpeed.SelectedItem = SettingsAccesor.FacesMatchingSpeed;
			cbIrisesMatchingSpeed.SelectedItem = SettingsAccesor.IrisesMatchingSpeed;
			cbPalmsMatchingSpeed.SelectedItem = SettingsAccesor.PalmsMatchingSpeed;
		}

		private void SelectThreshold(ComboBox target, int value)
		{
			string str = Utilities.MatchingThresholdToString(value);
			int index = target.Items.IndexOf(str);
			if (index != -1)
			{
				target.SelectedIndex = index;
			}
			else
			{
				target.Items.Insert(0, str);
				target.SelectedIndex = 0;
			}
		}

		#endregion
	}

}
