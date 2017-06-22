using System;
using System.Text;
using Neurotec.Biometrics;
using Neurotec.Devices;

namespace Neurotec.Samples
{
	public sealed partial class IrisScannerForm : BiometricDeviceForm
	{
		#region Private fields

		private NEPosition _position;
		private NEPosition[] _missingPositions;

		#endregion

		#region Public constructor

		public IrisScannerForm()
		{
			InitializeComponent();
			OnDeviceChanged();
		}

		#endregion

		#region Private methods

		private bool OnImage(NIris biometric, bool isFinal)
		{
			var sb = new StringBuilder();
			sb.Append(biometric.Status);
			foreach (var obj in biometric.Objects)
			{
				sb.AppendLine();
				sb.AppendFormat("\t{0}: {1} (Position: {2})", obj.Position, obj.Status, obj.BoundingRect);
			}
			return OnImage(biometric.Image, sb.ToString(), (biometric.Status != NBiometricStatus.None ? biometric.Status : NBiometricStatus.Ok).ToString(), isFinal);
		}

		#endregion

		#region Protected methods

		protected override bool IsValidDeviceType(Type value)
		{
			return base.IsValidDeviceType(value) && typeof(NIrisScanner).IsAssignableFrom(value);
		}

		protected override void OnCapture()
		{
			var irisScanner = (NIrisScanner)Device;
			irisScanner.CapturePreview += DeviceCapturePreview;
			try
			{
				using (var subject = new NSubject())
				{
					if (_missingPositions != null)
					{
						foreach (var missingPosition in _missingPositions)
							subject.MissingEyes.Add(missingPosition);
					}
					using (var iris = new NIris())
					{
						iris.Position = _position;
						if (!Automatic) iris.CaptureOptions = NBiometricCaptureOptions.Manual;
						irisScanner.Capture(iris, Timeout);
						OnImage(iris, true);
					}
				}
			}
			finally
			{
				irisScanner.CapturePreview -= DeviceCapturePreview;
			}
		}

		protected override void OnWriteScanParameters(System.Xml.XmlWriter writer)
		{
			base.OnWriteScanParameters(writer);
			WriteParameter(writer, "Position", Position);
			if (MissingPositions != null)
			{
				foreach (NEPosition position in MissingPositions)
				{
					WriteParameter(writer, "Missing", position);
				}
			}
		}

		#endregion

		#region Public properties

		public NEPosition Position
		{
			get
			{
				return _position;
			}
			set
			{
				if (_position != value)
				{
					CheckIsBusy();
					_position = value;
				}
			}
		}

		public NEPosition[] MissingPositions
		{
			get
			{
				return _missingPositions;
			}
			set
			{
				if (_missingPositions != value)
				{
					CheckIsBusy();
					_missingPositions = value;
				}
			}
		}

		#endregion

		#region Private form events

		private void DeviceCapturePreview(object sender, NBiometricDeviceCapturePreviewEventArgs e)
		{
			bool force = OnImage((NIris)e.Biometric, false);
			NBiometricStatus status = e.Biometric.Status;
			if (!Automatic && (status == NBiometricStatus.Ok || !NBiometricTypes.IsBiometricStatusFinal(status)))
			{
				e.Biometric.Status = force ? NBiometricStatus.Ok : NBiometricStatus.BadObject;
			}
		}

		#endregion
	}
}
