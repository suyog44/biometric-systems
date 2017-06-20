using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public partial class MediaPlayerControl : UserControl
	{
		#region Public constructor

		public MediaPlayerControl()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBuffer _soundBuffer;
		private SoundPlayer _soundPlayer = new SoundPlayer();

		#endregion

		#region Public properties

		public NBuffer SoundBuffer
		{
			get { return _soundBuffer; }
			set
			{
				Stop();
				_soundBuffer = value;
				btnStop.Enabled = btnPlay.Enabled = value != null && value != NBuffer.Empty;
			}
		}

		#endregion

		#region Private methods

		protected override void Dispose(bool disposing)
		{
			_soundPlayer.Stop();

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void Stop()
		{
			_soundPlayer.Stop();
		}

		public void Start()
		{
			Stop();
			_soundPlayer.Stream = new MemoryStream(_soundBuffer.ToArray());
			_soundPlayer.Play();
		}

		private void BtnPlayClick(object sender, EventArgs e)
		{
			Start();
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			Stop();
		}

		private void MediaPlayerControlVisibleChanged(object sender, EventArgs e)
		{
			if (!Visible)
				Stop();
		}

		#endregion
	}
}
