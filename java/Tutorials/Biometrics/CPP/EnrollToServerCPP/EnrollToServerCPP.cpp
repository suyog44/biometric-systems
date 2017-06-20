#include <TutorialUtils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
	#include <NBiometricClient/NBiometricClient.hpp>
	#include <NBiometrics/NBiometrics.hpp>
	#include <NMedia/NMedia.hpp>
	#include <NLicensing/NLicensing.hpp>
#else
	#include <NCore.hpp>
	#include <NBiometricClient.hpp>
	#include <NBiometrics.hpp>
	#include <NMedia.hpp>
	#include <NLicensing.hpp>
#endif

using namespace std;
using namespace Neurotec;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::IO;

const NChar title[] = N_T("EnrollToServer");
const NChar description[] = N_T("Demonstrates template enrollment to server.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

const NChar * defaultServerIp = N_T("127.0.0.1");
const NInt defaultAdminPort = 24932;
const NInt defaultPort = 25452;

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [template] [server] [port]" << endl << endl;
	cout << "\t[template] - template to be sent for enrollment (required)" << endl;
	cout << "\t[server]   - matching server address (optional parameter, default address is " << defaultServerIp << ")" << endl;
	cout << "\t[port]     - matching server port (optional parameter, default port is " << defaultAdminPort << ")" << endl;
	return 1;
}

static NSubject CreateSubject(const NStringWrapper& subjectId, const NStringWrapper& fileName)
{
	NSubject subject;
	subject.SetTemplateBuffer(NFile::ReadAllBytes(fileName));
	subject.SetId(subjectId);
	return subject;
}

int main(int argc, NChar * * argv)
{
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 2)
	{
		OnExit();
		return usage();
	}
	try
	{
		NString serverIp = defaultServerIp;
		NInt adminPort = defaultAdminPort;
		if (argc == 3)
			serverIp = argv[2];
		if (argc == 4)
		{
			serverIp = argv[2];
			adminPort = atoi(argv[3]);
		}
		NClusterBiometricConnection clusterBiometricConnection(serverIp, defaultPort, adminPort);
		NBiometricClient biometricClient;
		biometricClient.GetRemoteConnections().Add(clusterBiometricConnection);
		NSubject subject = CreateSubject(argv[1], argv[1]);
		NBiometricTask enrollTask = biometricClient.CreateTask(nboEnroll, subject);
		biometricClient.PerformTask(enrollTask);
		NBiometricStatus status = enrollTask.GetStatus();
		if (status != nbsOk)
		{
			cout << "Enrollment was unsuccessful. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			if (!enrollTask.GetError().IsNull())
				throw enrollTask.GetError();
			return -1;
		}
		cout << "Enrollment was successful" << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	OnExit();
	return 0;
}
