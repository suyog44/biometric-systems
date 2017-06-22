#ifndef DEVICES_SAMPLE_H_INCLUDED
#define DEVICES_SAMPLE_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		class DevicesSample : public wxApp
		{
		public:
			bool OnInit();
			int OnExit();
		};
	}
}
#endif
