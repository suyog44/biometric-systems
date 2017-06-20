#include <TutorialUtils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
	#include <NMedia/NMedia.hpp>
	#include <NDevices/NDevices.hpp>
	#include <NBiometrics/NBiometrics.hpp>
	#include <NLicensing/NLicensing.hpp>
#else
	#include <NCore.hpp>
	#include <NMedia.hpp>
	#include <NDevices.hpp>
	#include <NBiometrics.hpp>
	#include <NLicensing.hpp>
#endif

using namespace std;
using namespace Neurotec;
using namespace Neurotec::Images;
using namespace Neurotec::Licensing;
using namespace Neurotec::Devices;
using namespace Neurotec::Biometrics;

const NChar title[] = N_T("IrisScan");
const NChar description[] = N_T("Demonstrates capturing iris image from iris scanner");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int main()
{
	const NChar * components = N_T("Devices.IrisScanners");
	OnStart(title, description, version, copyright, 0, NULL);

	try
	{
		if (!NLicense::ObtainComponents(N_T("/local"), N_T("5000"), components))
		{
			NThrowException(NString::Format(N_T("Could not obtain licenses for components: {S}"), components));
		}

		NDeviceManager deviceManager;
		deviceManager.SetDeviceTypes(ndtIrisScanner);
		deviceManager.SetAutoPlug(true);
		deviceManager.Initialize();

		cout << "Device manager created. Found scanners: " << deviceManager.GetDevices().GetCount() << endl;
		for (int i = 0; i < deviceManager.GetDevices().GetCount(); i++)
		{
			NDevice device = deviceManager.GetDevices().Get(i);
			NIrisScanner scanner = NObjectDynamicCast<NIrisScanner>(device);
			cout << "Found scanner " << scanner.GetDisplayName() << endl;
			cout << "\tcapturing right iris: ";
			NIris rightIris;
			rightIris.SetPosition(nepRight);
			NBiometricStatus status = scanner.Capture(rightIris, -1);
			if (status != nbsOk)
			{
				cout << "failed to capture from scanner, status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
				continue;
			}

			NString fileName = NString::Format("{S}_iris_right.jpg", scanner.GetDisplayName().GetBuffer());
			rightIris.GetImage().Save(fileName);
			cout << "done" << endl << "\tcapturing left eye: ";
			NIris leftIris;
			leftIris.SetPosition(nepLeft);
			status = scanner.Capture(leftIris, -1);
			if (status != nbsOk)
			{
				cout << "failed to capture from scanner, status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
				continue;
			}
			fileName = NString::Format("{S}_iris_left.jpg", scanner.GetDisplayName().GetBuffer());
			leftIris.GetImage().Save(fileName);
			cout << "done" << endl;
		}
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
