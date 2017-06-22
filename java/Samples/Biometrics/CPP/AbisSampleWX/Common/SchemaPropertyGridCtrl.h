#ifndef SCHEMA_PROPERTY_GRID_CTRL_H_INCLUDED
#define SCHEMA_PROPERTY_GRID_CTRL_H_INCLUDED

#include <Settings/SettingsManager.h>

namespace Neurotec { namespace Samples
{

class SchemaPropertyGrid : public wxPanel
{
public:
	SchemaPropertyGrid(wxWindow * parent, int id = wxID_ANY);
	virtual ~SchemaPropertyGrid();

	void SetSchema(const SampleDbSchema & value);
	SampleDbSchema GetSchema() const;
	void SetValues(const ::Neurotec::NPropertyBag & value);
	::Neurotec::NPropertyBag GetValues() const;

	void SetGender(::Neurotec::Biometrics::NGender value);
	::Neurotec::Biometrics::NGender GetGender() const;

	void SetIsReadOnly(bool value);
	bool GetIsReadOnly() const;

	void SetShowBlobs(bool value);
	bool GetShowBlobs() const;

	void ApplyTo(::Neurotec::Biometrics::NSubject & subject);

private:
	void CreateGui();
	void UpdatePropertyGrid();
	wxPGProperty * CreateProperty(::Neurotec::Biometrics::NDBType dbType, const wxString & name);
	void OnPropertyChanged(wxPropertyGridEvent & event);

	wxPropertyGrid * m_propertyGrid;

	SampleDbSchema m_schema;
	bool m_isReadOnly;
	bool m_showBlobs;
	::Neurotec::NPropertyBag m_propertyBag;
};

class NullableIntProperty : public wxPGProperty
{
public:
	NullableIntProperty(const wxString& label = wxPG_LABEL, const wxString& name = wxPG_LABEL, long value = 0, bool hasValue = false);

	virtual bool StringToValue(wxVariant &variant, const wxString &text, int argFlags=0) const;
	virtual wxString ValueToString (wxVariant &value, int argFlags=0) const;
};

}};

#endif
