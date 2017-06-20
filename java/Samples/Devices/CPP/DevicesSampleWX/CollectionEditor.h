#ifndef COLLECTION_EDITOR_H_INCLUDED
#define COLLECTION_EDITOR_H_INCLUDED

class WXDLLIMPEXP_FWD_CORE wxListEvent;
#define wxAEDIALOG_STYLE (wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER | wxOK | wxCANCEL | wxCENTRE)

namespace Neurotec
{
	namespace Samples
	{
		namespace CommonUIHelpers
		{
			class WXDLLIMPEXP_PROPGRID CollectionEditor : public wxDialog
			{
			public:
				CollectionEditor(wxWindow *parent,
					const std::vector<NValue> &collection,
					const NType &type,
					const wxString &caption,
					long style = wxAEDIALOG_STYLE,
					const wxPoint &pos = wxDefaultPosition,
					const wxSize &sz = wxDefaultSize);

				virtual ~CollectionEditor();
				std::vector<NValue> GetValue();

			private:
				void CreateGUIControls();
				void LoadData();
				void OnAddClick(wxCommandEvent &event);
				void OnDeleteClick(wxCommandEvent &event);
				void OnUpClick(wxCommandEvent &event);
				void OnDownClick(wxCommandEvent &event);
				void OnPropertyGridChange(wxPropertyGridEvent &event);
				void OnListItemSelect(wxListEvent &event);

				std::vector<NValue> m_collection;
				NType m_type;
				wxListBox *m_listBox;
				wxPropertyGridManager *m_pgCollection;

				DECLARE_DYNAMIC_CLASS_NO_COPY(CollectionEditor)
			};
		}
	}
}

#endif
