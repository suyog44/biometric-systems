#ifndef SAMPLE_UTILS_HPP_INCLUDED
#define SAMPLE_UTILS_HPP_INCLUDED

#include <cmath>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
#else
	#include <NCore.hpp>
#endif

#if defined(N_PRODUCT_LIB)
	#ifdef N_MAC_OSX_FRAMEWORKS
		#include <NDevices/NDevices.hpp>
	#else
		#include <NDevices.hpp>
	#endif
#endif

namespace Neurotec { namespace Samples
{
	using namespace Neurotec::Plugins;
	#include <Utils.h>
	class Utils
	{
	public:
		static wxString MatchingThresholdToString(int value)
		{
			double p = -value / 12.0;
			return wxString::Format(wxString::Format("%%0.%df%%%%", std::max(0, (int)std::ceil(-p) - 2)), std::pow(10, p)*100);
		}

		static int MatchingThresholdFromString(wxString value)
		{
			value.Replace("%", "");
			double dValue;
			if (!value.ToDouble(&dValue)) throw NError(N_E_FAILED);
			double p = std::log10(std::max<double>(std::numeric_limits<double>::epsilon(), std::min<double>(1, dValue / 100)));
			return std::max(0, (int)std::ceil(-12 * p ));
		}
	};
}}

#endif // SAMPLE_UTILS_HPP_INCLUDED
