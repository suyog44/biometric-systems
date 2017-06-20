#ifndef SETTINGS_MANAGER_H_INCLUDED
#define SETTINGS_MANAGER_H_INCLUDED

namespace Neurotec { namespace Samples
{

class SampleDbSchema
{
public:
	SampleDbSchema(const wxString & name);
	SampleDbSchema(const SampleDbSchema & schema);

	::Neurotec::Biometrics::NBiographicDataSchema biographicData;
	::Neurotec::Biometrics::NBiographicDataSchema customData;
	wxString genderDataName;
	wxString enrollDataName;
	wxString thumbnailDataName;
	wxString schemaName;
	bool HasCustomData() const { return !customData.IsNull() && customData.GetElements().GetCount() > 0; }
	bool IsEmpty() const { return schemaName == wxT("None"); }
	static SampleDbSchema GetEmpty() { return SampleDbSchema(wxT("None")); };

	static SampleDbSchema Parse(const wxString & value);
	wxString Save() const;
};

typedef std::vector<SampleDbSchema> SchemaVector;

class SettingsManager
{
private:
	static wxArrayString SplitString(const wxString & value, const wxString & token);
public:
	class Phrase
	{
	private:
		int m_id;
		wxString m_phrase;

	public:
		Phrase(int id, wxString phrase);
		Phrase();

	public:
		int GetId();
		wxString GetPhrase();

	public:
		void SetId(int id);
		void SetPhrase(wxString phrase);
	};

	enum
	{
		SQLiteDatabase,
		OdbcDatabase,
		RemoteMatchingServer
	};

	static void LoadSettings(::Neurotec::Biometrics::Client::NBiometricClient & biometricClient);
	static void SaveSettings(const ::Neurotec::Biometrics::Client::NBiometricClient & biometricClient);

	static void SetTableName(const wxString & value);
	static wxString GetTableName();
	
	static void SetRemoteServerAddress(const wxString & value);
	static wxString GetRemoteServerAddress();

	static void SetRemoteServerPort(int value);
	static int GetRemoteServerPort();

	static void SetRemoteServerAdminPort(int value);
	static int GetRemoteServerAdminPort();

	static void SetDatabaseConnection(int value);
	static int GetDatabaseConnection();

	static void SetOdbcConnectionString(const wxString & value);
	static wxString GetOdbcConnectionString();

	static void SetPhrases(Phrase * phrases, int count);
	static Phrase * GetPhrases(int * count);

	static void SetFingersGeneralizationRecordCount(int value);
	static int GetFingersGeneralizationRecordCount();

	static void SetPalmsGeneralizationRecordCount(int value);
	static int GetPalmsGeneralizationRecordCount();

	static void SetFacesGeneralizationRecordCount(int value);
	static int GetFacesGeneralizationRecordCount();

	static void SetQueryAutoComplete(const wxArrayString & values);
	static wxArrayString GetQueryAutoComplete();

	static void SetWarnHasSchema(bool value);
	static bool GetWarnHasSchema();

	static void SetSchemas(const SchemaVector & values);
	static SchemaVector GetSchemas();

	static void SetCurrentSchemaIndex(int index);
	static int GetCurrentSchemaIndex();

	static void SetLocalOperationsIndex(int index);
	static int GetLocalOperationsIndex();

	static void SetFacesMirrorHorizontally(bool value);
	static bool GetFacesMirrorHorizontally();

	static SampleDbSchema GetCurrentSchema();
};

}}

#endif

