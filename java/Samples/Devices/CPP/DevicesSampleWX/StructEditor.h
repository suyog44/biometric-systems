#ifndef STRUCT_EDITOR_H_INCLUDED
#define STRUCT_EDITOR_H_INCLUDED

namespace Neurotec
{
	namespace Samples
	{
		namespace CommonUIHelpers
		{
			class StructEditor : public wxDialog
			{
			public:
				StructEditor(wxWindow *parent, const NType & type, const NValue &value = NULL, bool isNullable = true);
				NValue GetValue();

			private:
				void CreateGUIControls();
				void LoadData();
				void OnCheckBoxUseValueClick(wxCommandEvent &event);

				wxPropertyGridManager *m_propertyGridManager;
				wxCheckBox *m_checkBoxUse;

				NType m_type;
				NValue m_object;
				bool m_isNullable;

				DECLARE_EVENT_TABLE();
			};
		}
	}
}

#endif
