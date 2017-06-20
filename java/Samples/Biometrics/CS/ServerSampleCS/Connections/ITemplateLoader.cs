using System;
using Neurotec.Biometrics;

namespace Neurotec.Samples.Connections
{
	public interface ITemplateLoader : IDisposable
	{
		void BeginLoad();
		void EndLoad();
		bool LoadNext(out NSubject[] subjects, int n);
		int TemplateCount { get; }
	}
}
