#include "Precompiled.h"

#include <Common/LicensingTools.h>
#include <Settings/SettingsManager.h>

using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{

std::map<wxString, bool> LicensingTools::m_map;

bool LicensingTools::IsComponentActivated(wxString component)
{
	if (m_map.count(component) > 0)
	{
		return m_map.find(component)->second;
	}

	bool result = NLicense::IsComponentActivated(component);
	m_map[component] = result;

	return result;
}

bool LicensingTools::CanCreateFaceTemplate(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.FaceExtraction")) || (localOperations & nboCreateTemplate) == 0;
}

bool LicensingTools::CanDetectFaceSegments(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.FaceSegmentation")) || (localOperations & nboDetectSegments) == 0;
}

bool LicensingTools::CanCreatePalmTemplate(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.PalmExtraction")) || (localOperations & nboCreateTemplate) == 0;
}

bool LicensingTools::CanCreateIrisTemplate(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.IrisExtraction")) || (localOperations & nboCreateTemplate) == 0;
}

bool LicensingTools::CanSegmentIris(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.IrisSegmentation")) || (localOperations & nboSegment) == 0;
}

bool LicensingTools::CanCreateVoiceTemplate(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.VoiceExtraction")) || (localOperations & nboCreateTemplate) == 0;
}

bool LicensingTools::CanSegmentVoice(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.VoiceSegmentation")) || (localOperations & nboSegment) == 0;
}

bool LicensingTools::CanCreateFingerTemplate(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.FingerExtraction")) || (localOperations & nboCreateTemplate) == 0;
}

bool LicensingTools::CanSegmentFinger(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.FingerSegmentation")) || (localOperations & nboSegment) == 0;
}

bool LicensingTools::CanDetectFingerSegments(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.FingerSegmentsDetection")) || (localOperations & nboDetectSegments) == 0;
}

bool LicensingTools::CanAssessFingerQuality(NBiometricOperations localOperations)
{
	return IsComponentActivated(wxT("Biometrics.FingerQualityAssessmentBase")) || (localOperations & nboAssessQuality) == 0;
}

bool LicensingTools::CanFingerBeMatched(NBiometricOperations remoteOperations)
{
	return IsComponentActivated(wxT("Biometrics.FingerMatching")) || (remoteOperations & nboVerifyOffline) == nboVerifyOffline;
}

}}

