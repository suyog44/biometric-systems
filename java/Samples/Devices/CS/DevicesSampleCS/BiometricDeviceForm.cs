using System;
using Neurotec.Devices;
using Neurotec.Gui;

namespace Neurotec.Samples
{
	public partial class BiometricDeviceForm : CaptureForm
	{
		#region Private fields

		private bool _automatic = true;
		private int _timeout = -1;

		#endregion

		#region Public constructor

		public BiometricDeviceForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Protected methods

		protected override bool IsValidDeviceType(Type value)
		{
			return base.IsValidDeviceType(value) && typeof(NBiometricDevice).IsAssignableFrom(value);
		}

		protected override void OnCancelCapture()
		{
			base.OnCancelCapture();
			NGui.InvokeAsync(((NBiometricDevice)Device).Cancel);
		}

		protected override void OnWriteScanParameters(System.Xml.XmlWriter writer)
		{
			base.OnWriteScanParameters(writer);
			WriteParameter(writer, "Modality", ((NBiometricDevice)Device).BiometricType);
		}

		#endregion

		#region Public properties

		public bool Automatic
		{
			get
			{
				return _automatic;
			}
			set
			{
				if (_automatic != value)
				{
					CheckIsBusy();
					_automatic = value;
				}
			}
		}

		public int Timeout
		{
			get
			{
				return _timeout;
			}
			set
			{
				if (_timeout != value)
				{
					CheckIsBusy();
					_timeout = value;
				}
			}
		}

		#endregion
	}
}
