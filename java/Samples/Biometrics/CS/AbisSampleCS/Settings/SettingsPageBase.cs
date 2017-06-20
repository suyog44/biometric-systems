using System;
using Neurotec.Biometrics.Client;

namespace Neurotec.Samples
{
	public partial class SettingsPageBase : Neurotec.Samples.PageBase
	{
		#region Public methods

		public SettingsPageBase()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		protected NBiometricClient Client { get; set; }

		#endregion

		#region Public methods

		public override void OnNavigatedTo(params object[] args)
		{
			if (args == null || args.Length != 1) throw new ArgumentException("args");
			Client = (NBiometricClient)args[0];
			if (Client == null) throw new ArgumentException();

			LoadSettings();
			base.OnNavigatedTo(args);
		}

		public override void OnNavigatingFrom()
		{
			SaveSettings();
			Client = null;

			base.OnNavigatingFrom();
		}

		public virtual void LoadSettings()
		{
		}

		public virtual void SaveSettings()
		{
		}

		public virtual void DefaultSettings()
		{
			LoadSettings();
		}

		#endregion
	}
}
