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
			Console.WriteLine("\t{0} [source] [bufferCount] <optional: is url>", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\tsource - filename or url sound buffers should be captured from");
			Console.WriteLine("\tbufferCount - number of sound buffers to capture from specified filename or url");
			Console.WriteLine("\tis url - specifies that passed source parameter is url (value: 1) or filename (value: 0)");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Media";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				string uri = args[0];
				bool isUrl = false;
				int frameCount = int.Parse(args[1]);
				if (frameCount == 0)
				{
					Console.WriteLine("no frames will be captured as frame count is not specified");
				}

				if (args.Length > 2)
					isUrl = args[2] == "1";

				// create media source
				using (NMediaSource mediaSource = (isUrl) ? NMediaSource.FromUrl(uri) : NMediaSource.FromFile(uri))
				{
					Console.WriteLine("display name: {0}", mediaSource.DisplayName);

					using (var mediaReader = new NMediaReader(mediaSource, NMediaType.Audio, true))
					{
						ReadSoundBuffers(mediaReader, frameCount);
					}
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

		static void ReadSoundBuffers(NMediaReader mediaReader, int bufferCount)
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
