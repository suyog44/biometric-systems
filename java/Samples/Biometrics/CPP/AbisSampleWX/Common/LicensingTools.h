#ifndef LICENSING_TOOLS_H_INCLUDED
#define LICENSING_TOOLS_H_INCLUDED

#include <map>

namespace Neurotec { namespace Samples
{

class LicensingTools
{
public:
	static bool IsComponentActivated(wxString component);

	static bool CanCreateFaceTemplate(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanDetectFaceSegments(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanCreatePalmTemplate(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanCreateIrisTemplate(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanSegmentIris(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanCreateVoiceTemplate(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanSegmentVoice(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanCreateFingerTemplate(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanSegmentFinger(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanDetectFingerSegments(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanAssessFingerQuality(Neurotec::Biometrics::NBiometricOperations localOperations);

	static bool CanFingerBeMatched(Neurotec::Biometrics::NBiometricOperations remoteOperations);

private:
	static std::map<wxString, bool> m_map;
};

}}

#endif
