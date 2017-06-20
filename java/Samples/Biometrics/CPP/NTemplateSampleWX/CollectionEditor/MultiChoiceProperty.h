#ifndef MULTI_CHOICE_PROPERTY_H_INCLUDED
#define MULTI_CHOICE_PROPERTY_H_INCLUDED

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	class WXDLLIMPEXP_PROPGRID MultiChoiceProperty : public wxPGProperty
	{
		WX_PG_DECLARE_PROPERTY_CLASS(MultiChoiceProperty)
	public:

		MultiChoiceProperty(const wxString& label,
			const wxString& name,
			const wxArrayString& strings,
			const wxArrayString& value);
		MultiChoiceProperty(const wxString& label,
			const wxString& name,
			const wxPGChoices& choices,
			const wxArrayString& value = wxArrayString());

		MultiChoiceProperty(const wxString& label = wxPG_LABEL,
			const wxString& name = wxPG_LABEL,
			const wxArrayString& value = wxArrayString());

		virtual ~MultiChoiceProperty();

		virtual void OnSetValue();
		virtual wxString ValueToString(wxVariant& value, int argFlags = 0) const;
		virtual bool StringToValue(wxVariant& variant,
			const wxString& text,
			int argFlags = 0) const;
		virtual bool OnEvent(wxPropertyGrid* propgrid,
			wxWindow* primary, wxEvent& event);

		wxArrayInt GetValueAsArrayInt() const
		{
			return m_choices.GetValuesForStrings(m_value.GetArrayString());
		}

	protected:

		void GenerateValueAsString(wxVariant& value, wxString* target) const;

		wxArrayInt GetValueAsIndices() const;
		wxArrayString m_valueAsStrings;
		wxString m_display;
	};
}}}

#endif
