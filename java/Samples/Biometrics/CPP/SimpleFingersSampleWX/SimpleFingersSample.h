#ifndef SIMPLE_FINGERS_SAMPLE_H_INCLUDED
#define SIMPLE_FINGERS_SAMPLE_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class SimpleFingerSampleApp : public wxApp
		{
		public:
			virtual bool OnInit();
			int OnExit();
		};
	}
}
#endif
