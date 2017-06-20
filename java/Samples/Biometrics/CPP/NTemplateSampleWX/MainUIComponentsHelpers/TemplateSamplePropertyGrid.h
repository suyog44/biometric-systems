#ifndef TEMPLATE_SAMPLE_PROPGRID_H_INCLUDED
#define TEMPLATE_SAMPLE_PROPGRID_H_INCLUDED

namespace Neurotec { namespace Samples { namespace MainUIComponentsHelpers
{
	class TemplateSamplePropertyGrid : public wxPropertyGrid
	{
	public:
		TemplateSamplePropertyGrid(wxWindow *parent, wxWindowID id = wxID_ANY,
			const wxPoint& pos = wxDefaultPosition,
			const wxSize& size = wxDefaultSize,
			long style = wxPG_DEFAULT_STYLE);
		virtual ~TemplateSamplePropertyGrid();

		void UpdatePropertyGrid(Neurotec::NObject dataObject, int currentItemId);
		void PropertyGridValueChange(Neurotec::NObject dataObject, int currentItemId, wxString propertyName, wxVariant propertyValue, bool isPalm);
		void PropertyGridValueChanging(wxPropertyGridEvent& event);
		void ShowFingerOrPalmPropertyValuesInGrid(Neurotec::Biometrics::NFRecord record, int itemId);
		void FingerRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value);
		void PalmRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value);
		void FaceRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value);
		void IrisRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value);
		void VoiceRecordPropertyValueChanged(wxString propertyName, int index, wxVariant value);
		void ValidatePropertyGridValues(wxVariant pendingValue, long maxValue, wxPropertyGridEvent& event);
		void ValidatePropertyGridStrings(wxVariant pendingValue, wxPropertyGridEvent& event);
		void SetTemplate(Neurotec::Biometrics::NTemplate ntemplate);

		void RefreshProperty(wxPGProperty *) { }
		void Clear() { }

	private:
		Neurotec::Biometrics::NTemplate m_template;

		wxPGProperty* palmTemplateProperty;
		wxPGProperty* fingerTemplateProperty;
		wxPGProperty* faceTemplateProperty;
		wxPGProperty* irisTemplateProperty;
		wxPGProperty* voiceTemplateProperty;
		wxArrayString m_collectionString;
	};
}}}

#endif

