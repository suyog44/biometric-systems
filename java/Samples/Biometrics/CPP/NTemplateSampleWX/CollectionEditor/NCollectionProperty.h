#ifndef NCOLLECTION_PROPERTY_H_INCLUDED
#define NCOLLECTION_PROPERTY_H_INCLUDED

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	class NCollectionProperty : public wxPGProperty
	{
		WX_PG_DECLARE_PROPERTY_CLASS(NCollectionProperty)
	public:

		NCollectionProperty(const wxString& label = wxPG_LABEL, const wxString& name = wxPG_LABEL, const wxArrayString& value = wxArrayString(), const Neurotec::Biometrics::NTemplate & templ = NULL, int collName = 0, int selection = 0);

		virtual ~NCollectionProperty();

		virtual void OnSetValue();
		virtual wxString ValueToString(wxVariant& value, int argFlags = 0) const;
		virtual bool StringToValue(wxVariant& variant, const wxString& text, int argFlags = 0) const;
		virtual bool OnEvent(wxPropertyGrid* propgrid, wxWindow* primary, wxEvent& event);
		virtual bool DoSetAttribute(const wxString& name, wxVariant& value);
		virtual void ConvertArrayToString(const wxArrayString& arr, wxString* pString, const wxUniChar& delimiter) const;
		virtual bool OnCustomStringEdit(wxWindow* parent, wxString& value);

		enum ConversionFlags
		{
			Escape = 0x01,
			QuoteStrings = 0x02
		};
		static void ArrayStringToString(wxString& dst, const wxArrayString& src, wxUniChar delimiter, int flags);

	protected:
		virtual void GenerateValueAsString();

		int m_precision;
		wxString m_display;
		wxUniChar m_delimiter;
		Neurotec::Biometrics::NTemplate m_template;
		int m_collectionName;
		int m_recordNum;
	};
}}}
#endif
