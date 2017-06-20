using System;
using Neurotec.Licensing;
using Neurotec.Media;
using Neurotec.Sound;

namespace Neurotec.Tutorials
{
	class Program
	{
		static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [bufferCount]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\tbufferCount - number of sound buffers to capture from each device to current directory");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Media";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 1)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				int bufferCount = int.Parse(args[0]);
				if (bufferCount == 0)
				{
					Console.WriteLine("no sound buffers will be captured as sound buffer count is not specified");
				}

				Console.WriteLine("quering connected audio devices ...");
				NMediaSource[] devices = NMediaSource.EnumDevices(NMediaType.Audio);
				Console.WriteLine("devices found: {0}", devices.Length);

				foreach (NMediaSource source in devices)
				{
					Console.WriteLine("found device: {0}", source.DisplayName);
					using (var mediaReader = new NMediaReader(source, NMediaType.Audio, true))
					{
						ReadSoundBufers(mediaReader, bufferCount);
					}
					Console.WriteLine("done");
				}
				Console.WriteLine("done");
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				NLicense.ReleaseComponents(Components);
			}
		}

		static void DumpMediaFormat(NMediaFormat mediaFormat)
		{
			if (mediaFormat == null) throw new ArgumentNullException("mediaFormat");

			switch (mediaFormat.MediaType)
			{
				case NMediaType.Video:
					var videoFormat = (NVideoFormat)mediaFormat;
					Console.WriteLine("video format .. {0}x{1} @ {2}/{3} (interlace: {4}, aspect ratio: {5}/{6})", videoFormat.Width, videoFormat.Height,
						videoFormat.FrameRate.Numerator, videoFormat.FrameRate.Denominator, videoFormat.InterlaceMode, videoFormat.PixelAspectRatio.Numerator, videoFormat.PixelAspectRatio.Denominator);
					break;
				case NMediaType.Audio:
					var audioFormat = (NAudioFormat)mediaFormat;
					Console.WriteLine("audio format .. channels: {0}, samples/second: {1}, bits/channel: {2}", audioFormat.ChannelCount,
										audioFormat.SampleRate, audioFormat.BitsPerChannel);
					break;
				default:
					throw new ArgumentException("unknown media type specified in format!");
			}
		}

		static void ReadSoundBufers(NMediaReader mediaReader, int bufferCount)
		{
			NMediaSource mediaSource = mediaReader.Source;

			Console.WriteLine("media length: {0}", mediaReader.Length);

			NMediaFormat[] mediaFormats = mediaSource.GetFormats(NMediaType.Audio);
			if (mediaFormats == null)
			{
				Console.WriteLine("formats are not yet available (should be available after media reader is started");
			}
			else
			{
				Console.WriteLine("format count: {0}", mediaFormats.Length);
				for (int i = 0; i < mediaFormats.Length; i++)
				{
					Console.Write("[{0}] ", i);
					DumpMediaFormat(mediaFormats[i]);
				}
			}

			NMediaFormat currentMediaFormat = mediaSource.GetCurrentFormat(NMediaType.Audio);
			if (currentMediaFormat != null)
			{
				Console.WriteLine("current media format:");
				DumpMediaFormat(currentMediaFormat);

				if (mediaFormats != null)
				{
					Console.WriteLine("set the last supported format (optional) ... ");
					mediaSource.SetCurrentFormat(NMediaType.Audio, mediaFormats[mediaFormats.Length - 1]);
				}
			}
			else Console.WriteLine("current media format is not yet available (will be availble after media reader start)");

			Console.Write("starting capture ... ");
			mediaReader.Start();
			Console.WriteLine("capture started");

			try
			{
				currentMediaFormat = mediaSource.GetCurrentFormat(NMediaType.Audio);
				if (currentMediaFormat == null)
					throw new Exception("current media format is not set even after media reader start!");
				Console.WriteLine("capturing with format: ");
				DumpMediaFormat(currentMediaFormat);

				for (int i = 0; i < bufferCount; i++)
				{
					TimeSpan timeSpan, duration;

					using (NSoundBuffer buffer = mediaReader.ReadAudioSample(out timeSpan, out duration))
					{
						if (buffer == null) return; // end of stream

						Console.WriteLine("[{0} {1}] sample rate: {2}, sample length: {3}", timeSpan, duration, buffer.SampleRate, buffer.Length);
					}
				}
			}
			finally
			{
				mediaReader.Stop();
			}
		}
	}
}
