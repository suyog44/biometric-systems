using System;
using System.Text;
using Neurotec.Biometrics;
using Neurotec.Devices;

namespace Neurotec.Samples
{
	public sealed partial class FScannerForm : BiometricDeviceForm
	{
		#region Private fields

		private NFImpressionType _impressionType;
		private NFPosition _position;
		private NFPosition[] _missingPositions;

		#endregion

		#region Public constructor

		public FScannerForm()
		{
			InitializeComponent();
			OnDeviceChanged();
		}

		#endregion

		#region Private methods

		private bool OnImage(NFrictionRidge biometric, bool isFinal)
		{
			var sb = new StringBuilder();
			sb.Append(biometric.Status);
			foreach (var obj in biometric.Objects)
			{
				sb.AppendLine();
				sb.AppendFormat("\t{0}: {1}", obj.Position, obj.Status);
			}
			return OnImage(biometric.Image, sb.ToString(), (biometric.Status != NBiometricStatus.None ? biometric.Status : NBiometricStatus.Ok).ToString(), isFinal);
		}

		#endregion

		#region Protected methods

		protected override bool IsValidDeviceType(Type value)
		{
			return base.IsValidDeviceType(value) && typeof(NFScanner).IsAssignableFrom(value);
		}

		protected override void OnCapture()
		{
			var fScanner = (NFScanner)Device;
			fScanner.CapturePreview += DeviceCapturePreview;
			try
			{
				using (var subject = new NSubject())
				{
					if (_missingPositions != null)
					{
						foreach (var missingPosition in _missingPositions)
							subject.MissingFingers.Add(missingPosition);
					}
					var biometric = NFrictionRidge.FromPosition(_position);
					if (biometric is NFinger)
						subject.Fingers.Add(biometric as NFinger);
					else
						subject.Palms.Add(biometric as NPalm);
					biometric.ImpressionType = _impressionType;
					biometric.Position = _position;
					if (!Automatic) biometric.CaptureOptions = NBiometricCaptureOptions.Manual;

					fScanner.Capture(biometric, Timeout);
					OnImage(biometric, true);
				}
			}
			finally
			{
				fScanner.CapturePreview -= DeviceCapturePreview;
			}
		}

		protected override void OnWriteScanParameters(System.Xml.XmlWriter writer)
		{
			base.OnWriteScanParameters(writer);
			WriteParameter(writer, "ImpressionType", ImpressionType);
			WriteParameter(writer, "Position", Position);
			if (MissingPositions != null)
			{
				foreach (NFPosition position in MissingPositions)
				{
					WriteParameter(writer, "Missing", position);
				}
			}
		}

		#endregion

		#region Public properties

		public NFImpressionType ImpressionType
		{
			get
			{
				return _impressionType;
			}
			set
			{
				if (_impressionType != value)
				{
					CheckIsBusy();
					_impressionType = value;
				}
			}
		}

		public NFPosition Position
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

		public NFPosition[] MissingPositions
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
			bool force = OnImage((NFrictionRidge)e.Biometric, false);
			NBiometricStatus status = e.Biometric.Status;
			if (!Automatic && (status == NBiometricStatus.Ok || !NBiometricTypes.IsBiometricStatusFinal(status)))
			{
				e.Biometric.Status = force ? NBiometricStatus.Ok : NBiometricStatus.BadObject;
			}
		}

		#endregion
	}
}
