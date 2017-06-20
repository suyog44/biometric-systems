using System;
using System.IO;

using Neurotec.Biometrics;

namespace Neurotec.Tutorials
{
	public class Program
	{
		private static int RotationToDegrees(int rotation)
		{
			return (2 * rotation * 360 + 256) / (2 * 256);
		}

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [NTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[NTemplate] - file containing NTemplate.");
			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 1)
			{
				return Usage();
			}

			try
			{
				byte[] templateBuffer = File.ReadAllBytes(args[0]);

				Console.WriteLine();
				Console.WriteLine("template {0} contains:", args[0]);
				using (var template = new NTemplate(templateBuffer))
				{
					if (template.Fingers != null)
					{
						Console.WriteLine("{0} fingers", template.Fingers.Records.Count);
						foreach (NFRecord nfRec in template.Fingers.Records)
						{
							PrintNFRecord(nfRec);
						}
					}
					else
					{
						Console.WriteLine("0 fingers");
					}
					if (template.Faces != null)
					{
						Console.WriteLine("{0} faces", template.Faces.Records.Count);
						foreach (NLRecord nlRec in template.Faces.Records)
						{
							PrintNLRecord(nlRec);
						}
					}
					else
					{
						Console.WriteLine("0 faces");
					}
					if (template.Irises != null)
					{
						Console.WriteLine("{0} irises", template.Irises.Records.Count);
						foreach (NERecord neRec in template.Irises.Records)
						{
							PrintNERecord(neRec);
						}
					}
					else
					{
						Console.WriteLine("0 irises");
					}
					if (template.Voices != null)
					{
						Console.WriteLine("{0} voices", template.Voices.Records.Count);
						foreach (NSRecord nsRec in template.Voices.Records)
						{
							PrintNSRecord(nsRec);
						}
					}
					else
					{
						Console.WriteLine("0 voices");
					}
					if (template.Palms != null)
					{
						Console.WriteLine("{0} palms", template.Palms.Records.Count);
						foreach (NFRecord nfRec in template.Palms.Records)
						{
							PrintNFRecord(nfRec);
						}
					}
					else
					{
						Console.WriteLine("0 palms");
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}

		private static void PrintNFRecord(NFRecord nfRec)
		{
			Console.WriteLine("\tg: {0}", nfRec.G);
			Console.WriteLine("\timpression type: {0}", nfRec.ImpressionType);
			Console.WriteLine("\tpattern class: {0}", nfRec.PatternClass);
			Console.WriteLine("\tcbeff product type: {0}", nfRec.CbeffProductType);
			Console.WriteLine("\tposition: {0}", nfRec.Position);
			Console.WriteLine("\tridge counts type: {0}", nfRec.RidgeCountsType);
			Console.WriteLine("\twidth: {0}", nfRec.Width);
			Console.WriteLine("\theight: {0}", nfRec.Height);
			Console.WriteLine("\thorizontal resolution: {0}", nfRec.HorzResolution);
			Console.WriteLine("\tvertical resolution: {0}", nfRec.VertResolution);
			Console.WriteLine("\tquality: {0}", nfRec.Quality);
			Console.WriteLine("\tsize: {0}", nfRec.GetSize());

			Console.WriteLine("\tminutia count: {0}", nfRec.Minutiae.Count);

			NFMinutiaFormat minutiaFormat = nfRec.MinutiaFormat;

			int index = 1;
			foreach (NFMinutia minutia in nfRec.Minutiae)
			{
				Console.WriteLine("\t\tminutia {0} of {1}", index, nfRec.Minutiae.Count);
				Console.WriteLine("\t\tx: {0}", minutia.X);
				Console.WriteLine("\t\ty: {0}", minutia.Y);
				Console.WriteLine("\t\tangle: {0}", RotationToDegrees(minutia.RawAngle));
				if ((minutiaFormat & NFMinutiaFormat.HasQuality) == NFMinutiaFormat.HasQuality)
				{
					Console.WriteLine("\t\tquality: {0}", minutia.Quality);
				}
				if ((minutiaFormat & NFMinutiaFormat.HasG) == NFMinutiaFormat.HasG)
				{
					Console.WriteLine("\t\tg: {0}", minutia.G);
				}
				if ((minutiaFormat & NFMinutiaFormat.HasCurvature) == NFMinutiaFormat.HasCurvature)
				{
					Console.WriteLine("\t\tcurvature: {0}", minutia.Curvature);
				}

				Console.WriteLine();
				index++;
			}

			index = 1;
			foreach (NFDelta delta in nfRec.Deltas)
			{
				Console.WriteLine("\t\tdelta {0} of {1}", index, nfRec.Deltas.Count);
				Console.WriteLine("\t\tx: {0}", delta.X);
				Console.WriteLine("\t\ty: {0}", delta.Y);
				Console.WriteLine("\t\tangle1: {0}", RotationToDegrees(delta.RawAngle1));
				Console.WriteLine("\t\tangle2: {0}", RotationToDegrees(delta.RawAngle2));
				Console.WriteLine("\t\tangle3: {0}", RotationToDegrees(delta.RawAngle3));

				Console.WriteLine();
				index++;
			}

			index = 1;
			foreach (NFCore core in nfRec.Cores)
			{
				
				Console.WriteLine("\t\tcore {0} of {1}", index, nfRec.Cores.Count);
				Console.WriteLine("\t\tx: {0}", core.X);
				Console.WriteLine("\t\ty: {0}", core.Y);
				Console.WriteLine("\t\tangle: {0}", RotationToDegrees(core.RawAngle));

				Console.WriteLine();
				index++;
			}

			index = 1;
			foreach (NFDoubleCore doubleCore in nfRec.DoubleCores)
			{

				Console.WriteLine("\t\tdouble core {0} of {1}", index, nfRec.DoubleCores.Count);
				Console.WriteLine("\t\tx: {0}", doubleCore.X);
				Console.WriteLine("\t\ty: {0}", doubleCore.Y);

				Console.WriteLine();
				index++;
			}
		}

		private static void PrintNLRecord(NLRecord nlRec)
		{
			Console.WriteLine("\tquality: {0}", nlRec.Quality);
			Console.WriteLine("\tsize: {0}", nlRec.GetSize());
		}

		private static void PrintNERecord(NERecord neRec)
		{
			Console.WriteLine("\tposition: {0}", neRec.Position);
			Console.WriteLine("\tsize: {0}", neRec.GetSize());
		}

		private static void PrintNSRecord(NSRecord nsRec)
		{
			Console.WriteLine("\tphrase id: {0}", nsRec.PhraseId);
			Console.WriteLine("\tsize: {0}", nsRec.GetSize());
		}
	}
}
