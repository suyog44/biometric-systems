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

const NChar title[] = N_T("EnrollToSQLiteDatabase");
const NChar description[] = N_T("Demonstrates template enrollment to SQLite database.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [template] [path to database fiel]" << endl << endl;
	cout << "\t[template]              - template for enrollment" << endl;
	cout << "\t[path to database file] - path to SQLite database file" << endl;
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

	if (argc < 3)
	{
		OnExit();
		return usage();
	}
	try
	{
		NBiometricClient biometricClient;
		biometricClient.SetDatabaseConnectionToSQLite(argv[2]);

		NSubject subject = CreateSubject(argv[1], argv[1]);
		NBiometricTask enrollTask = biometricClient.CreateTask(nboEnroll, subject);
		biometricClient.PerformTask(enrollTask);
		NBiometricStatus status = enrollTask.GetStatus();
		if (status != nbsOk)
		{
			cout << "Enrollment was unsuccessful. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			if (enrollTask.GetError() != NULL)
				throw enrollTask.GetError();
			return -1;
		}

		cout << "Enrollment was successful" << endl;

		NArrayWrapper<NSubject> list = biometricClient.List();
		cout << "The SQLite database contains these IDs: " << endl;
		for (NArrayWrapper<NSubject>::iterator it = list.begin(); it != list.end(); it++)
		{
			cout << "\t" << it->GetId() << endl;
		}
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	OnExit();
	return 0;
}
