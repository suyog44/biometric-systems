#include "Precompiled.h"
#include "ExtractionSource.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{
	ExtractionSource::ExtractionSource(int sourceType)
	{
		m_sourceType = sourceType;
		m_selectedMode = nepUnknown;
	}

	int ExtractionSource::GetSourceType()
	{
		return m_sourceType;
	}

	void ExtractionSource::SetSelectedMode(int mode)
	{
		switch(mode)
		{
		case 0:
			m_selectedMode = nepUnknown;
			break;
		case 1:
			m_selectedMode = nepRight;
			break;
		case 2:
			m_selectedMode = nepLeft;
			break;
		case 3:
			m_selectedMode = nepBoth;
			break;
		default:
			m_selectedMode = nepUnknown;
			break;
		}
	}

	int ExtractionSource::GetSelectedMode()
	{
		return m_selectedMode;
	}

	wxString ExtractionSource::GetModeAsString(int mode)
	{
		switch(mode)
		{
		case nepUnknown:
			return wxT("Unknown Iris");
		case 1:
			return wxT("Right Iris");
		case 2:
			return wxT("Left Iris");
		case 3:
			return wxT("Both Irises");
		default:
			return wxEmptyString;
		}
	}

	int ExtractionSource::GetModesCount()
	{
		return 4;
	}

}}
