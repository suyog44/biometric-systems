using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;
using Neurotec.Samples.SubjectEditor.TenPrintCard;
using Neurotec.Samples.Properties;

namespace Neurotec.Samples.TenPrintCard
{
	public partial class TenPrintCardForm : Form
	{
		#region Private fields

		private readonly FramePainter _painter;
		private NBiometricClient _biometricClient;
		private NSubject _subject;

		#endregion

		#region Public properties

		public NSubject Result
		{
			get { return _subject; }
			set { _subject = value; }
		}

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set { _biometricClient = value; }
		}

		#endregion

		#region Public constructor

		public TenPrintCardForm()
		{
			InitializeComponent();

			_painter = new FramePainter(Resources.TenPrintCard);
			_painter.Visible = false;

			Controls.Add(_painter);
		}

		#endregion

		#region Private methods

		private void SetImage(NImage img)
		{
			_painter.SetImage(img);
			tsbOK.Enabled = img != null;
		}

		private NSubject MakeSubject()
		{
			Dictionary<int, NImage> srcimg = _painter.GetFramedFingerprints();
			NSubject subject = new NSubject();
			NFinger finger;
			NFPosition[] positions = new NFPosition[]
				{
					NFPosition.RightThumb, NFPosition.RightIndex, NFPosition.RightMiddle, NFPosition.RightRing, NFPosition.RightLittle,
					NFPosition.LeftThumb, NFPosition.LeftIndex, NFPosition.LeftMiddle, NFPosition.LeftRing, NFPosition.LeftLittle,
				};

			for (int i = 0; i < positions.Length; i++)
			{
				try
				{
					finger = new NFinger
					{
						Position = positions[i],
						ImpressionType = NFImpressionType.NonliveScanRolled,
						Image = srcimg[i + 1]
					};
					subject.Fingers.Add(finger);
				}
				catch
				{
				}
			}

			finger = new NFinger
			{
				Position = NFPosition.PlainLeftFourFingers,
				ImpressionType = NFImpressionType.NonliveScanPlain,
				Image = srcimg[11]
			};
			subject.Fingers.Add(finger);

			finger = new NFinger
			{
				Position = NFPosition.LeftThumb,
				ImpressionType = NFImpressionType.NonliveScanPlain,
				Image = srcimg[12]
			};
			subject.Fingers.Add(finger);

			finger = new NFinger
			{
				Position = NFPosition.RightThumb,
				ImpressionType = NFImpressionType.NonliveScanPlain,
				Image = srcimg[13]
			};
			subject.Fingers.Add(finger);

			finger = new NFinger
			{
				Position = NFPosition.PlainRightFourFingers,
				ImpressionType = NFImpressionType.NonliveScanPlain,
				Image = srcimg[14]
			};
			subject.Fingers.Add(finger);

			foreach (KeyValuePair<int, NImage> nImage in srcimg)
			{
				nImage.Value.Dispose();
			}

			return subject;
		}

		private void TsbOpenFileClick(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				_painter.Visible = true;
				_painter.Dock = DockStyle.Fill;
				NImage img = NImage.FromFile(openFileDialog1.FileName);
				SetImage(img);
			}
		}

		private void DoScan()
		{
			NPhotoScannerForm scanner = new NPhotoScannerForm(this);
			NImage img = scanner.Scan();
			if (img != null)
			{
				_painter.Visible = true;
				_painter.Dock = DockStyle.Fill;
				SetImage(img);
			}
		}

		private void TsbScanClick(object sender, EventArgs e)
		{
			DoScan();
		}

		private void TbsScanDefaultButtonClick(object sender, EventArgs e)
		{
			DoScan();
		}

		private void SelectDeviceToolStripMenuItemClick(object sender, EventArgs e)
		{
			NPhotoScannerForm scanner = new NPhotoScannerForm(this);
			scanner.SelectDevice();
		}

		private void TsbOKClick(object sender, EventArgs e)
		{
			try
			{
				NBiometricOperations operations = NBiometricOperations.CreateTemplate;
				if (_biometricClient.FingersCalculateNfiq) operations |= NBiometricOperations.AssessQuality;
				NBiometricTask task;

				_subject = MakeSubject();
				task = _biometricClient.CreateTask(operations, _subject);
				LongActionDialog.ShowDialog(this, "Creating template ...", new Action<NBiometricTask>(_biometricClient.PerformTask), task);

				NBiometricStatus status = task.Status;
				if (task.Error != null)
				{
					Utilities.ShowError("Error while segmenting images: {0}.", task.Error);
					return;
				}
				if (status != NBiometricStatus.Ok)
				{
					StringBuilder failedList = new StringBuilder();
					List<NFinger> failed = new List<NFinger>(_subject.Fingers.Where(x => x.Status != NBiometricStatus.Ok || x.Position == NFPosition.Unknown));
					foreach (var item in failed)
					{
						_subject.Fingers.Remove(item);
					}
					failedList.AppendLine("Failed to extract the following fingerprints:");
					foreach (NFinger item in failed.Where(x => x.Status != NBiometricStatus.Ok))
					{
						failedList.AppendFormat("- {0}\n", item.Position);
					}
					Utilities.ShowInformation(failedList.ToString());
				}
				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				Utilities.ShowError("Error while segmenting images: {0}.", ex.Message);
			}
		}

		private void TenPrintCardFormKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Escape:
					DialogResult = DialogResult.Cancel;
					break;
				case Keys.Return:
					DialogResult = DialogResult.OK;
					break;
			}
		}

		private void TenPrintCardFormLoad(object sender, EventArgs e)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32Windows)
			{
				toolStrip1.Items.Remove(tsbScan);
				toolStrip1.Items.Remove(selectDeviceToolStripMenuItem);
				toolStrip1.Items.Remove(tbsScanDefault);
			}
		}

		#endregion
	}
}
