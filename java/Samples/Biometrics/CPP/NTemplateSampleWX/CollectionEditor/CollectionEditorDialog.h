#ifndef COLLECTION_EDITOR_DIALOG_H_INCLUDED
#define COLLECTION_EDITOR_DIALOG_H_INCLUDED

#include "CollectionBaseAdapter.h"

class WXDLLIMPEXP_FWD_CORE wxListEvent;
#define wxAEDIALOG_STYLE (wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER | wxOK | wxCANCEL | wxCENTRE)

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	class WXDLLIMPEXP_PROPGRID CollectionEditorDialog : public wxDialog
	{
	public:
		CollectionEditorDialog();
		CollectionEditorDialog(CollectionBaseAdapter *collectionAdaptor, wxString collectionName);
		virtual ~CollectionEditorDialog() { }

		void Init();
		void AddCollectionsToList();
		void SetAddedRecordCountBy(int val);
		int GetAddedRecordCount();

		CollectionEditorDialog(wxWindow *parent,
			const wxString& message,
			const wxString& caption,
			long style = wxAEDIALOG_STYLE,
			const wxPoint& pos = wxDefaultPosition,
			const wxSize& sz = wxDefaultSize);

		CollectionEditorDialog(wxWindow *parent,
			const wxString& message,
			const wxString& caption,
			CollectionBaseAdapter* adaptor,
			long style = wxAEDIALOG_STYLE,
			const wxPoint& pos = wxDefaultPosition,
			const wxSize& sz = wxDefaultSize);

		bool Create(wxWindow *parent,
			const wxString& message,
			const wxString& caption,
			long style = wxAEDIALOG_STYLE,
			const wxPoint& pos = wxDefaultPosition,
			const wxSize& sz = wxDefaultSize);

		virtual void SetDialogValue(const wxVariant& WXUNUSED(value))
		{
			wxFAIL_MSG(wxT("re-implement this member function in derived class"));
		}

		virtual wxVariant GetDialogValue() const
		{
			wxFAIL_MSG(wxT("re-implement this member function in derived class"));
			return wxVariant();
		}

		virtual wxValidator* GetTextCtrlValidator() const
		{
			return NULL;
		}

		bool IsModified() const { return m_modified; }
		int GetSelection() const;
		void OnAddClick(wxCommandEvent& event);
		void OnDeleteClick(wxCommandEvent& event);
		void OnUpClick(wxCommandEvent& event);
		void OnDownClick(wxCommandEvent& event);
		void OnIdle(wxIdleEvent& event);
		void OnListItemSelect(wxListEvent& event);
		void OnPropertyValueChange(wxPropertyGridEvent& event);
		void OnPropertyValueChanging(wxPropertyGridEvent& event);
		virtual wxArrayString GetItemsArray();
		void SetArrayItems(wxArrayString arrayItems);
		void OnCancelClick(wxCommandEvent& event);
		void OnOkClick(wxCommandEvent& event);

	public:
		wxButton* btnAdd;
		wxButton* btnRemove;
		wxBitmapButton* btnUp;
		wxBitmapButton* btnDown;

	protected:
		bool m_modified;
		int m_listItemId;
		int m_addCount;
		int m_newItemCount;
		CollectionBaseAdapter *m_collectionAdaptor;
		wxArrayString m_arrayItems;
		virtual wxString ArrayGet(size_t index);
		virtual size_t ArrayGetCount();
		virtual bool ArrayInsert(const wxString& str, int index);
		virtual bool ArraySet(size_t index, const wxString& str);
		virtual void ArrayRemoveAt(int index);
		virtual void ArraySwap(size_t first, size_t second);
		virtual bool OnCustomNewAction(wxString* WXUNUSED(resString))
		{
			return false;
		}

	private:
		wxPropertyGridManager *collectionPropertyGrid;
		wxListCtrl* itemList;
		wxStaticText *members;
		wxStaticText *properties;
		wxBoxSizer* mainBox;
		wxBoxSizer* hbox1;
		wxBoxSizer* hbox2;
		wxBoxSizer* hbox3;
		wxBoxSizer* vbox3;
		wxBoxSizer* vbox4;
		wxStdDialogButtonSizer* buttonSizer;
		wxWindow* elbSubPanel;
		wxWindow* lastFocused;
		wxArrayInt m_arrayDeletedItems;
		int m_itemCountOnEditorStart;

		DECLARE_DYNAMIC_CLASS_NO_COPY(CollectionEditorDialog)
		DECLARE_EVENT_TABLE()
	};
}}}

#endif
