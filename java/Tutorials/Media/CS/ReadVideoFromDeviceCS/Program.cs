using System;
using Neurotec.Images;
using Neurotec.Licensing;
using Neurotec.Media;

namespace Neurotec.Tutorials
{
	class Program
	{
		static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [frameCount]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\tframeCount - number of frames to capture from each device to current directory");
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

				int frameCount = int.Parse(args[0]);
				if (frameCount == 0)
				{
					Console.WriteLine("no frames will be captured as frame count is not specified");
				}

				Console.WriteLine("quering connected video devices ...");
				NMediaSource[] devices = NMediaSource.EnumDevices(NMediaType.Video);
				Console.WriteLine("devices found: {0}", devices.Length);

				for (int i = 0; i < devices.Length; i++)
				{
					NMediaSource mediaSource = devices[i];
					Console.WriteLine("found device: {0}", mediaSource.DisplayName);
					using (var mediaReader = new NMediaReader(mediaSource, NMediaType.Video, true))
					{
						ReadFrames(mediaReader, frameCount, i);
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

		static void ReadFrames(NMediaReader mediaReader, int frameCount, int deviceNumber)
		{
			NMediaSource mediaSource = mediaReader.Source;

			Console.WriteLine("media length: {0}", mediaReader.Length);

			NMediaFormat[] mediaFormats = mediaSource.GetFormats(NMediaType.Video);
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

			NMediaFormat currentMediaFormat = mediaSource.GetCurrentFormat(NMediaType.Video);
			if (currentMediaFormat != null)
			{
				Console.WriteLine("current media format:");
				DumpMediaFormat(currentMediaFormat);

				if (mediaFormats != null)
				{
					Console.WriteLine("set the last supported format (optional) ... ");
					mediaSource.SetCurrentFormat(NMediaType.Video, mediaFormats[mediaFormats.Length - 1]);
				}
			}
			else Console.WriteLine("current media format is not yet available (will be availble after media reader start)");

			Console.Write("starting capture ... ");
			mediaReader.Start();
			Console.WriteLine("capture started");

			try
			{
				currentMediaFormat = mediaSource.GetCurrentFormat(NMediaType.Video);
				if (currentMediaFormat == null)
					throw new Exception("current media format is not set even after media reader start!");
				Console.WriteLine("capturing with format: ");
				DumpMediaFormat(currentMediaFormat);

				for (int i = 0; i < frameCount; i++)
				{
					TimeSpan timeSpan, duration;

					using (NImage image = mediaReader.ReadVideoSample(out timeSpan, out duration))
					{
						if (image == null) return; // end of stream

						string filename = String.Format("{0}_{1:d4}.jpg", deviceNumber, i);
						image.Save(filename);

						Console.WriteLine("[{0} {1}] {2}", timeSpan, duration, filename);
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
