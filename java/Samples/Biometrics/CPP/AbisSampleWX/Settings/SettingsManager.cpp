#include "Precompiled.h"

#include <Settings/SettingsManager.h>
#include <Common/LicensingTools.h>

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Devices;

namespace Neurotec { namespace Samples
{

SampleDbSchema::SampleDbSchema(const wxString & name)
{
	schemaName = name;
}

SampleDbSchema::SampleDbSchema(const SampleDbSchema & schema)
{
	this->schemaName = schema.schemaName;
	this->enrollDataName = schema.enrollDataName;
	this->genderDataName = schema.genderDataName;
	this->thumbnailDataName = schema.thumbnailDataName;
	this->biographicData = NBiographicDataSchema::Parse(schema.biographicData.ToString());
	this->customData = NBiographicDataSchema::Parse(schema.customData.ToString());
}

SampleDbSchema SampleDbSchema::Parse(const wxString & value)
{
	SampleDbSchema result(wxT("N/A"));
	wxStringTokenizer tokenizer(value, wxT("#"));
	if (tokenizer.CountTokens() != 6) NThrowArgumentException(wxT("Value"));
	result.schemaName = tokenizer.GetNextToken();

	wxString biographicDataString = tokenizer.GetNextToken();
	wxString customDataString = tokenizer.GetNextToken();

	result.biographicData = biographicDataString != wxEmptyString ? NBiographicDataSchema::Parse(biographicDataString) : NBiographicDataSchema();
	result.customData = customDataString != wxEmptyString ? NBiographicDataSchema::Parse(customDataString) : NBiographicDataSchema();

	wxString genderString = tokenizer.GetNextToken();
	wxString thumbnailString = tokenizer.GetNextToken();
	wxString enrollDataString = tokenizer.GetNextToken();

	wxStringTokenizer genderTokenizer(genderString, wxT("="));
	if (genderTokenizer.CountTokens() == 2)
	{
		genderTokenizer.GetNextToken();
		result.genderDataName = genderTokenizer.GetNextToken();
	}

	wxStringTokenizer thumbnailTokenizer(thumbnailString, wxT("="));
	if (thumbnailTokenizer.CountTokens() == 2)
	{
		thumbnailTokenizer.GetNextToken();
		result.thumbnailDataName = thumbnailTokenizer.GetNextToken();
	}

	wxStringTokenizer enrollDataTokenizer(enrollDataString, wxT("="));
	if (enrollDataTokenizer.CountTokens() == 2)
	{
		enrollDataTokenizer.GetNextToken();
		result.enrollDataName = enrollDataTokenizer.GetNextToken();
	}

	return result;
}

wxString SampleDbSchema::Save() const
{
	if (IsEmpty()) NThrowException(N_E_INVALID_OPERATION);

	wxString format = wxT("%s#%s#%s#Gender=%s#Thumbnail=%s#EnrollData=%s");
	return wxString::Format(format, schemaName, (wxString)biographicData.ToString(), (wxString)customData.ToString(), genderDataName, thumbnailDataName, enrollDataName);
}

#define CLIENT_PROPERTIES_KEY        wxT("BiometricClient/Properties")
#define CONNECTION_TYPE_KEY          wxT("ConnectionType")
#define ODBC_CONNECTION_STRING_KEY   wxT("OdbcConnectionString")
#define TABLE_NAME_KEY               wxT("TableName")
#define HOST_NAME_KEY                wxT("HostName")
#define CLIENT_PORT_KEY              wxT("ClientPort")
#define ADMIN_PORT_KEY               wxT("AdminPort")
#define PHRASES_KEY                  wxT("Phrases")
#define FACES_GEN_RECORD_COUNT_KEY   wxT("FacesGeneralizationRecordCount")
#define FINGERS_GEN_RECORD_COUNT_KEY wxT("FingersGeneralizationRecordCount")
#define PALMS_GEN_RECORD_COUNT_KEY   wxT("PalmsGeneralizationRecordCount")
#define QUERY_AUTOCOMPLETE_KEY       wxT("QueryQutocomplete")
#define WARN_HAS_SCHEMA_KEY          wxT("WarnHasSchema")
#define SCHEMAS_KEY                  wxT("Schema/Values")
#define CURRENT_SCHEMA_KEY           wxT("CurrentSchema")
#define LOCAL_OPERATIONS_KEY         wxT("LocalOperations")
#define MIRROR_HORIZONTALLY_KEY      wxT("MirrorHorizontally")

#define DEFAULT_PHRASES wxT("1=What is your grandmother's name?;2=Where and when have you been born?;3=What is your favourite meal and drink?")
#define DEFAULT_GEN_RECORD_COUNT 3
#define DEFAULT_LOCAL_OPERATIONS 5
#define DEFAULT_MIRROR_HORIZONTALLY true

#define DEFAULT_CLIENT_PROPERTIES \
	wxT("Fingers.DeterminePatternClass=True;")\
	wxT("Fingers.CalculateNfiq=True;")\
	wxT("Fingers.ReturnBinarizedImage=True;")\
	wxT("Faces.CreateThumbnailImage=True;")\
	wxT("Faces.DetectAllFeaturePoints=True;")\
	wxT("Faces.DetermineGender=True;")\
	wxT("Faces.DetermineAge=True;")\
	wxT("Faces.DetectProperties=True;")\
	wxT("Faces.RecognizeExpression=True;")\
	wxT("Faces.RecognizeEmotion=True;")\
	wxT("Palms.ReturnBinarizedImage=True;")\
	wxT("Matching.WithDetails=True;")\
	wxT("Fingers.CheckForDuplicatesWhenCapturing=True")

#define DEFAULT_QUERY_AUTO_COMPLETE \
	wxT("Id IN (\n")\
	wxT("Id=\n")\
	wxT("Country=\n")\
	wxT("City=\n")\
	wxT("FirstName=\n")\
	wxT("LastName=\n")\
	wxT("YearOfBirth > \n")\
	wxT("GenderString='Male'\n")\
	wxT("GenderString='Female'")

#define DEFAULT_SCHEMAS \
	wxT("Sample db schema#(FirstName string, LastName string, YearOfBirth int, GenderString string, Country string, City string)#(Thumbnail blob, EnrollData blob)#Gender=GenderString#Thumbnail=Thumbnail#EnrollData=EnrollData\n")\
	wxT("Remote server schema#(FirstName string, LastName string, YearOfBirth int, GenderString string, Country string, City string)##Gender=GenderString#Thumbnail=#EnrollData=")

void SettingsManager::LoadSettings(NBiometricClient & biometricClient)
{
	if (!biometricClient.IsNull())
	{
		wxConfigBase * config = wxConfigBase::Get();
		wxString propertiesString = wxEmptyString;
		config->Read(CLIENT_PROPERTIES_KEY, &propertiesString, DEFAULT_CLIENT_PROPERTIES);
		if (propertiesString != wxEmptyString)
		{
			NPropertyBag::Parse(propertiesString).ApplyTo(biometricClient);
		}
	}

	if (biometricClient.GetFingersDeterminePatternClass() && !LicensingTools::CanDetectFingerSegments(biometricClient.GetLocalOperations()))
		biometricClient.SetFingersDeterminePatternClass(false);
	if (biometricClient.GetFingersCalculateNfiq() && !LicensingTools::CanAssessFingerQuality(biometricClient.GetLocalOperations()))
		biometricClient.SetFingersCalculateNfiq(false);
	if (biometricClient.GetFingersCheckForDuplicatesWhenCapturing())
	{
		NRemoteBiometricConnection remoteConnection = biometricClient.GetRemoteConnections().GetCount() > 0 ? biometricClient.GetRemoteConnections()[0] : NULL;
		NBiometricOperations operations = remoteConnection.IsNull() ? nboNone : remoteConnection.GetOperations();
		if (!LicensingTools::CanFingerBeMatched(operations))
			biometricClient.SetFingersCheckForDuplicatesWhenCapturing(false);
	}
	if (!LicensingTools::CanDetectFaceSegments(biometricClient.GetLocalOperations()))
	{
		biometricClient.SetFacesDetectAllFeaturePoints(false);
		biometricClient.SetFacesDetectBaseFeaturePoints(false);
		biometricClient.SetFacesDetermineGender(false);
		biometricClient.SetFacesRecognizeEmotion(false);
		biometricClient.SetFacesDetectProperties(false);
		biometricClient.SetFacesRecognizeExpression(false);
		biometricClient.SetFacesDetermineAge(false);
	}
}

void SettingsManager::SaveSettings(const NBiometricClient & biometricClient)
{
	if (!biometricClient.IsNull())
	{
		NPropertyBag properties;
		biometricClient.CaptureProperties(properties);
		wxConfigBase::Get()->Write(CLIENT_PROPERTIES_KEY, (wxString)properties.ToString());
	}
}

void SettingsManager::SetDatabaseConnection(int value)
{
	wxConfigBase::Get()->Write(CONNECTION_TYPE_KEY, value);
}

int SettingsManager::GetDatabaseConnection()
{
	int value = SQLiteDatabase;
	wxConfigBase::Get()->Read(CONNECTION_TYPE_KEY, &value);
	return value;
}

void SettingsManager::SetOdbcConnectionString(const wxString & value)
{
	wxConfigBase::Get()->Write(ODBC_CONNECTION_STRING_KEY, value);
}

wxString SettingsManager::GetOdbcConnectionString()
{
	wxString value = wxEmptyString;
	wxConfigBase::Get()->Read(ODBC_CONNECTION_STRING_KEY, &value);
	return value;
}

void SettingsManager::SetTableName(const wxString & value)
{
	wxConfigBase::Get()->Write(TABLE_NAME_KEY, value);
}

wxString SettingsManager::GetTableName()
{
	wxString value = wxEmptyString;
	wxConfigBase::Get()->Read(TABLE_NAME_KEY, &value);
	return value;
}

void SettingsManager::SetRemoteServerAddress(const wxString & value)
{
	wxConfigBase::Get()->Write(HOST_NAME_KEY, value);
}

wxString SettingsManager::GetRemoteServerAddress()
{
	wxString value = wxT("localhost");
	wxConfigBase::Get()->Read(HOST_NAME_KEY, &value);
	return value;
}

void SettingsManager::SetRemoteServerPort(int value)
{
	wxConfigBase::Get()->Write(CLIENT_PORT_KEY, value);
}

int SettingsManager::GetRemoteServerPort()
{
	int value = 25452;
	wxConfigBase::Get()->Read(CLIENT_PORT_KEY, &value);
	return value;
}

void SettingsManager::SetRemoteServerAdminPort(int value)
{
	wxConfigBase::Get()->Write(ADMIN_PORT_KEY, value);
}

int SettingsManager::GetRemoteServerAdminPort()
{
	int value = 24932;
	wxConfigBase::Get()->Read(ADMIN_PORT_KEY, &value);
	return value;
}

void SettingsManager::SetPhrases(SettingsManager::Phrase *phrases, int count)
{
	wxString buffer = wxEmptyString;
	if (phrases != NULL && count > 0)
	{
		for (int i = 0; i < count; i++)
		{
			buffer += wxString::Format(wxT("%d=%s;"), phrases[i].GetId(), phrases[i].GetPhrase());
		}
	}
	wxConfigBase::Get()->Write(PHRASES_KEY, buffer);
}

SettingsManager::Phrase * SettingsManager::GetPhrases(int *count)
{
	SettingsManager::Phrase *phrases = NULL;
	wxString strBuffer = wxEmptyString;
	int phrasesCount = 0;

	wxConfigBase::Get()->Read(PHRASES_KEY, &strBuffer, DEFAULT_PHRASES);
	wxArrayString phrasesArray = wxStringTokenize(strBuffer, wxT(";"));
	phrasesCount = phrasesArray.GetCount();
	if (phrasesCount > 0)
	{
		phrases = new Phrase[phrasesCount];
	}

	for (int i = 0; i < phrasesCount; i++)
	{
		wxArrayString args = wxStringTokenize(phrasesArray[i], wxT("="));
		if (args.GetCount() < 2)
			continue;

		long id = -1;
		args[0].ToLong(&id, 10);

		phrases[i].SetId(id);
		phrases[i].SetPhrase(args[1]);
	}

	if (count) *count = phrasesCount;
	return phrases;
}

SettingsManager::Phrase::Phrase(int id, wxString phrase)
	: m_id(id), m_phrase(phrase)
{
}

SettingsManager::Phrase::Phrase()
{
	m_id = -1;
	m_phrase = wxEmptyString;
}

int SettingsManager::Phrase::GetId()
{
	return m_id;
}

wxString SettingsManager::Phrase::GetPhrase()
{
	return m_phrase;
}

void SettingsManager::Phrase::SetId(int id)
{
	m_id = id;
}

void SettingsManager::Phrase::SetPhrase(wxString phrase)
{
	m_phrase = phrase;
}

void SettingsManager::SetFingersGeneralizationRecordCount(int value)
{
	wxConfigBase::Get()->Write(FINGERS_GEN_RECORD_COUNT_KEY, value);
}

int SettingsManager::GetFingersGeneralizationRecordCount()
{
	int value;
	wxConfigBase::Get()->Read(FINGERS_GEN_RECORD_COUNT_KEY, &value, DEFAULT_GEN_RECORD_COUNT);
	return value;
}

void SettingsManager::SetPalmsGeneralizationRecordCount(int value)
{
	wxConfigBase::Get()->Write(PALMS_GEN_RECORD_COUNT_KEY, value);
}

int SettingsManager::GetPalmsGeneralizationRecordCount()
{
	int value;
	wxConfigBase::Get()->Read(PALMS_GEN_RECORD_COUNT_KEY, &value, DEFAULT_GEN_RECORD_COUNT);
	return value;
}

void SettingsManager::SetFacesGeneralizationRecordCount(int value)
{
	wxConfigBase::Get()->Write(FACES_GEN_RECORD_COUNT_KEY, value);
}

int SettingsManager::GetFacesGeneralizationRecordCount()
{
	int value;
	wxConfigBase::Get()->Read(FACES_GEN_RECORD_COUNT_KEY, &value, DEFAULT_GEN_RECORD_COUNT);
	return value;
}

wxArrayString SettingsManager::SplitString(const wxString & value, const wxString & token)
{
	wxArrayString result;
	wxStringTokenizer tokenizer(value, token);
	while(tokenizer.HasMoreTokens())
	{
		result.push_back(tokenizer.GetNextToken());
	}
	return result;
}

void SettingsManager::SetQueryAutoComplete(const wxArrayString & values)
{
	wxString value = wxEmptyString;
	for (int i = 0; i < (int)values.size(); i++)
	{
		if (i != 0) value.Append(wxT("\n"));
		value.Append(values[i]);
	}

	wxConfigBase::Get()->Write(QUERY_AUTOCOMPLETE_KEY, value);
}

wxArrayString SettingsManager::GetQueryAutoComplete()
{
	wxString value;
	wxConfigBase::Get()->Read(QUERY_AUTOCOMPLETE_KEY, &value, DEFAULT_QUERY_AUTO_COMPLETE);
	return SettingsManager::SplitString(value, wxT("\n"));
}

void SettingsManager::SetWarnHasSchema(bool value)
{
	wxConfigBase::Get()->Write(WARN_HAS_SCHEMA_KEY, value);
}

bool SettingsManager::GetWarnHasSchema()
{
	bool value;
	wxConfigBase::Get()->Read(WARN_HAS_SCHEMA_KEY, &value, true);
	return value;
}

void SettingsManager::SetSchemas(const SchemaVector & values)
{
	wxString value = wxEmptyString;
	for (int i = 0; i < (int)values.size(); i++)
	{
		if (i != 0) value.Append(wxT("\n"));
		value.Append(values[i].Save());
	}

	wxConfigBase::Get()->Write(SCHEMAS_KEY, value);
}

SchemaVector SettingsManager::GetSchemas()
{
	SchemaVector result;
	wxString value;
	wxConfigBase::Get()->Read(SCHEMAS_KEY, &value, DEFAULT_SCHEMAS);
	wxArrayString split = SettingsManager::SplitString(value, wxT("\n"));
	for (wxArrayString::iterator it = split.begin(); it != split.end(); it++)
	{
		result.push_back(SampleDbSchema::Parse(*it));
	}
	return result;
}

void SettingsManager::SetCurrentSchemaIndex(int index)
{
	wxConfigBase::Get()->Write(CURRENT_SCHEMA_KEY, index);
}

int SettingsManager::GetCurrentSchemaIndex()
{
	int result;
	wxConfigBase::Get()->Read(CURRENT_SCHEMA_KEY, &result, 0);
	return result;
}

void SettingsManager::SetLocalOperationsIndex(int index)
{
	wxConfigBase::Get()->Write(LOCAL_OPERATIONS_KEY, index);
}

int SettingsManager::GetLocalOperationsIndex()
{
	int result;
	wxConfigBase::Get()->Read(LOCAL_OPERATIONS_KEY, &result, DEFAULT_LOCAL_OPERATIONS);
	return result;
}

void SettingsManager::SetFacesMirrorHorizontally(bool value)
{
	wxConfigBase::Get()->Write(MIRROR_HORIZONTALLY_KEY, value);
}

bool SettingsManager::GetFacesMirrorHorizontally()
{
	bool result;
	wxConfigBase::Get()->Read(MIRROR_HORIZONTALLY_KEY, &result, DEFAULT_MIRROR_HORIZONTALLY);
	return result;
}

SampleDbSchema SettingsManager::GetCurrentSchema()
{
	int index = GetCurrentSchemaIndex();
	if (index == -1)
		return SampleDbSchema::GetEmpty();
	else
	{
		SchemaVector v = GetSchemas();
		return v[index];
	}
}

}}

