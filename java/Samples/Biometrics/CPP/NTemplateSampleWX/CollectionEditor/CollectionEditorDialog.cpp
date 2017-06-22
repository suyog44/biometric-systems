#include "Precompiled.h"
#include "CollectionEditorDialog.h"
#include "../Resources/Up.xpm"
#include "../Resources/Down.xpm"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	BEGIN_EVENT_TABLE(CollectionEditorDialog, wxDialog)
		EVT_IDLE(CollectionEditorDialog::OnIdle)
		END_EVENT_TABLE()

		IMPLEMENT_ABSTRACT_CLASS(CollectionEditorDialog, wxDialog)

		void CollectionEditorDialog::OnIdle(wxIdleEvent& event)
	{
			wxWindow* lastFoc = lastFocused;
			wxWindow* focus = ::wxWindow::FindFocus();

			if (lastFoc && focus != lastFoc &&	lastFoc->GetParent() == elbSubPanel && !lastFoc->IsEnabled()) itemList->SetFocus();

			lastFocused = focus;

			event.Skip();
		}

	CollectionEditorDialog::CollectionEditorDialog() : wxDialog()
	{
		Init();
	}

	CollectionEditorDialog::CollectionEditorDialog(CollectionBaseAdapter *collectionAdaptor, wxString collectionName)
	{
		m_collectionAdaptor = collectionAdaptor;
		Init();
	}

	void CollectionEditorDialog::Init()
	{
		lastFocused = NULL;
		m_listItemId = 0;
		m_addCount = 0;
		m_newItemCount = 0;
	}

	CollectionEditorDialog::CollectionEditorDialog(wxWindow *parent, const wxString& message, const wxString& caption, long style, const wxPoint& pos, const wxSize& sz) : wxDialog()
	{
		Init();
		Create(parent, message, caption, style, pos, sz);
	}

	CollectionEditorDialog::CollectionEditorDialog(wxWindow *parent, const wxString& message, const wxString& caption, CollectionBaseAdapter* adaptor, long style, const wxPoint& pos, const wxSize& sz) : wxDialog()
	{
		m_collectionAdaptor = adaptor;
		Init();
		Create(parent, message, caption, style, pos, sz);
	}

	bool CollectionEditorDialog::Create(wxWindow *parent, const wxString& message, const wxString& caption, long style, const wxPoint& pos, const wxSize& sz)
	{
#ifdef __WXMAC__
		wxUnusedVar(style);
		int useStyle = wxCAPTION;
#else
		int useStyle = style;
#endif
		bool res = wxDialog::Create(parent, wxID_ANY, caption, pos, sz, useStyle);
		SetFont(parent->GetFont());
#if !wxPG_SMALL_SCREEN
		const int spacing = 5;
#else
		const int spacing = 3;
#endif
		m_modified = false;
		wxBoxSizer* topsizer = new wxBoxSizer(wxVERTICAL);
		if (!message.empty())
			topsizer->Add(new wxStaticText(this, -1, message), 0, wxALIGN_LEFT | wxALIGN_CENTRE_VERTICAL | wxALL, spacing);
		
		delete topsizer;

		members = new wxStaticText(this, -1, wxT("Members: "));
		properties = new wxStaticText(this, -1, wxT("Properties"));
		itemList = new wxListCtrl(this, -1, wxDefaultPosition, wxSize(180, 220), wxLC_REPORT | wxLC_NO_HEADER);
		btnAdd = new wxButton(this, -1, wxT("Add"), wxDefaultPosition, wxDefaultSize);
		btnRemove = new wxButton(this, -1, wxT("Remove"), wxDefaultPosition, wxDefaultSize);
		collectionPropertyGrid = new wxPropertyGridManager(this, -1, wxPoint(0, 0), wxSize(225, 230), wxPG_BOLD_MODIFIED | wxPG_SPLITTER_AUTO_CENTER | wxPG_TOOLBAR | wxPGMAN_DEFAULT_STYLE | wxPG_VFB_STAY_IN_PROPERTY | wxPG_VFB_SHOW_MESSAGEBOX);
		collectionPropertyGrid->SetExtraStyle(wxPG_EX_MODE_BUTTONS);

		wxBitmap upBitmap(Up_XPM, wxBITMAP_TYPE_XPM);
		wxBitmap downBitmap(Down_XPM, wxBITMAP_TYPE_XPM);
		btnUp = new wxBitmapButton(this, wxID_ANY, upBitmap, wxPoint(0, 0), wxSize(25, 25), wxBU_AUTODRAW);
		btnDown = new wxBitmapButton(this, wxID_ANY, downBitmap, wxPoint(0, 0), wxSize(25, 25), wxBU_AUTODRAW);

		hbox1 = new wxBoxSizer(wxHORIZONTAL);
		hbox3 = new wxBoxSizer(wxHORIZONTAL);
		hbox2 = new wxBoxSizer(wxHORIZONTAL);
		mainBox = new wxBoxSizer(wxVERTICAL);
		vbox3 = new wxBoxSizer(wxVERTICAL);
		vbox4 = new wxBoxSizer(wxVERTICAL);
		
		hbox1->Add(members, 1, wxEXPAND | wxTOP | wxLEFT, spacing);
		hbox1->Add(properties, 1, wxEXPAND | wxTOP  | wxALIGN_RIGHT, spacing);

		hbox2->Add(itemList, 1, wxLEFT | wxEXPAND | wxALL, spacing);

		hbox3->Add(btnAdd, 0, wxBOTTOM | wxALIGN_LEFT | wxALL, spacing);
		hbox3->Add(btnRemove, 0, wxBOTTOM | wxALIGN_LEFT | wxALL, spacing);

		vbox3->Add(btnUp, 0, wxTOP | wxLEFT, spacing);
		vbox3->Add(btnDown, 0, wxTOP | wxLEFT, spacing);
		
		vbox4->Add(collectionPropertyGrid, 1, wxEXPAND | wxALL, spacing);

		hbox2->Add(vbox3, 0, wxALIGN_LEFT);
		hbox2->Add(vbox4, 1, wxEXPAND);

		mainBox->Add(hbox1, 0, wxEXPAND);
		mainBox->Add(hbox2, 1, wxEXPAND);
		mainBox->Add(hbox3, 0);

		elbSubPanel = btnAdd->GetParent();

		btnAdd->Connect(btnAdd->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditorDialog::OnAddClick), NULL, this);
		btnRemove->Connect(btnRemove->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditorDialog::OnDeleteClick), NULL, this);
		btnUp->Connect(btnUp->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditorDialog::OnUpClick), NULL, this);
		btnDown->Connect(btnDown->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditorDialog::OnDownClick), NULL, this);
		itemList->Connect(itemList->GetId(), wxEVT_LIST_ITEM_SELECTED, wxListEventHandler(CollectionEditorDialog::OnListItemSelect), NULL, this);
		collectionPropertyGrid->Connect(collectionPropertyGrid->GetId(), wxEVT_PG_CHANGED, wxPropertyGridEventHandler(CollectionEditorDialog::OnPropertyValueChange), NULL, this);
		collectionPropertyGrid->Connect(collectionPropertyGrid->GetId(), wxEVT_PG_CHANGING, wxPropertyGridEventHandler(CollectionEditorDialog::OnPropertyValueChanging), NULL, this);

		wxListItem col0;
		col0.SetId(0);
		col0.SetWidth(20);
		itemList->InsertColumn(0, col0);

		wxListItem col1;
		col1.SetId(1);
		col1.SetWidth(itemList->GetSize().GetWidth());
		itemList->InsertColumn(1, col1);

		AddCollectionsToList();

		buttonSizer = new wxStdDialogButtonSizer();
		wxButton* okButton = new wxButton(this, wxID_OK);
		wxButton* cancelButton = new wxButton(this, wxID_CANCEL);

		buttonSizer->AddButton(okButton);
		buttonSizer->AddButton(cancelButton);
		buttonSizer->Realize();
		mainBox->Add(buttonSizer, 0, wxALIGN_RIGHT | wxALIGN_CENTRE_VERTICAL | wxALL, spacing);

		okButton->Connect(okButton->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditorDialog::OnOkClick), NULL, this);
		cancelButton->Connect(cancelButton->GetId(), wxEVT_BUTTON, wxCommandEventHandler(CollectionEditorDialog::OnCancelClick), NULL, this);

		Maximize(false);
		SetSize(wxSize(500, 375));
		SetSizer(mainBox);
		mainBox->SetSizeHints(this);

#if !wxPG_SMALL_SCREEN
		if (sz.x == wxDefaultSize.x &&
			sz.y == wxDefaultSize.y)
			SetSize(wxSize(500, 375));
		else
			SetSize(sz);
#endif

		m_itemCountOnEditorStart = itemList->GetItemCount();

		return res;
	}

	int CollectionEditorDialog::GetSelection() const
	{
		int index = itemList->GetNextItem(-1, wxLIST_NEXT_ALL, wxLIST_STATE_SELECTED);
		if (index == -1)
			return wxNOT_FOUND;

		return index;
	}

	void CollectionEditorDialog::OnAddClick(wxCommandEvent& event)
	{
		collectionPropertyGrid->CommitChangesFromEditor();

		int newItemIndex = itemList->GetItemCount();

		if (newItemIndex < 0)
		{
			newItemIndex = 0;
			btnRemove->Enable(false);
		}

		m_listItemId = newItemIndex;
		m_addCount++;

		collectionPropertyGrid->Clear();

		properties->SetLabelText(m_collectionAdaptor->GetListViewString() + wxT(" properties"));

		ArrayInsert(m_collectionAdaptor->GetListViewString(), newItemIndex);
		m_collectionAdaptor->AddItemCollection(itemList, collectionPropertyGrid, newItemIndex);
		btnRemove->Enable(true);
		SetAddedRecordCountBy(1);

		event.Skip();
	}

	void CollectionEditorDialog::OnDeleteClick(wxCommandEvent& event)
	{
		if (m_listItemId < 0)	m_listItemId = 0;

		ArrayRemoveAt(m_listItemId);
		itemList->DeleteItem(m_listItemId);
		if(m_listItemId < m_itemCountOnEditorStart)
			m_arrayDeletedItems.Add(m_listItemId);
		else
			m_collectionAdaptor->DeleteItem(m_listItemId);
		m_listItemId--;

		if (m_collectionAdaptor->GetItemsCount() <= 0)
		{
			btnRemove->Enable(false);
		}

		collectionPropertyGrid->Clear();

		SetAddedRecordCountBy(-1);
	
		itemList->SetItemState(m_listItemId, wxLIST_STATE_SELECTED, wxLIST_STATE_SELECTED);
		if (m_listItemId >= 0)
			properties->SetLabelText(itemList->GetItemText(m_listItemId, 1) + wxT(" properties"));
		else
			properties->SetLabelText(wxT("Properties"));
		event.Skip();
	}

	void CollectionEditorDialog::OnUpClick(wxCommandEvent& event)
	{
		collectionPropertyGrid->CommitChangesFromEditor();

		if (m_listItemId > 0)
		{
			itemList->DeleteItem(m_listItemId);
			itemList->DeleteItem(m_listItemId - 1);
			ArraySwap(m_listItemId - 1, m_listItemId);
			m_collectionAdaptor->SwapItemCollection(itemList, collectionPropertyGrid, m_listItemId, ArrayGet(m_listItemId - 1), ArrayGet(m_listItemId));
			itemList->SetItemState(m_listItemId - 1, wxLIST_STATE_SELECTED, wxLIST_STATE_SELECTED);
			properties->SetLabelText(itemList->GetItemText(m_listItemId, 1) + wxT(" properties"));
		}
		event.Skip();
	}

	void CollectionEditorDialog::OnDownClick(wxCommandEvent& event)
	{
		collectionPropertyGrid->CommitChangesFromEditor();

		int lastStringIndex = itemList->GetItemCount() - 1;
		if (m_listItemId >= 0 && m_listItemId < lastStringIndex)
		{
			itemList->DeleteItem(m_listItemId + 1);
			itemList->DeleteItem(m_listItemId);

			ArraySwap(m_listItemId, m_listItemId + 1);
			m_collectionAdaptor->SwapItemCollection(itemList, collectionPropertyGrid, m_listItemId + 1, ArrayGet(m_listItemId), ArrayGet(m_listItemId + 1));
			itemList->SetItemState(m_listItemId + 1, wxLIST_STATE_SELECTED, wxLIST_STATE_SELECTED);
			properties->SetLabelText(itemList->GetItemText(m_listItemId, 1) + wxT(" properties"));
		}
		event.Skip();
	}

	void CollectionEditorDialog::OnOkClick(wxCommandEvent& event)
	{
		int size = m_arrayDeletedItems.GetCount();
		for(int i=0; i<size; i++)
		{
			m_collectionAdaptor->DeleteItem(i);
		}

		m_arrayDeletedItems.Clear();
		
		Destroy();
		event.Skip();
	}

	void CollectionEditorDialog::OnCancelClick(wxCommandEvent& event)
	{
		int size = ArrayGetCount() - 1;
		for(int i=size; i>=0; i--)
		{
			if(i >= m_itemCountOnEditorStart)
			{
				ArrayRemoveAt(i);
				itemList->DeleteItem(i);
				
				if(m_collectionAdaptor->GetItemsCount() > 0)
					m_collectionAdaptor->DeleteItem(i);
			}
		}

		m_arrayDeletedItems.Clear();

		Destroy();
		event.Skip();
	}

	void CollectionEditorDialog::OnListItemSelect(wxListEvent& event)
	{
		collectionPropertyGrid->CommitChangesFromEditor();

		long itemIndex = event.GetItem().GetId(); 
		m_listItemId = itemIndex;
		collectionPropertyGrid->Clear();
		m_collectionAdaptor->ShowItemCollectionInGrid(collectionPropertyGrid, m_listItemId);
		properties->SetLabelText(itemList->GetItemText(event.GetItem(),1) + wxT(" properties"));
	}

	void CollectionEditorDialog::OnPropertyValueChanging(wxPropertyGridEvent& event)
	{
		wxPGProperty* property = event.GetProperty();
		wxVariant pendingValue = event.GetValue();
		if (property->GetName() == N_TMPL_X || property->GetName() == N_TMPL_Y || property->GetName() == N_TMPL_ANGLE || property->GetName() == N_TMPL_ANGLE1
			|| property->GetName() == N_TMPL_ANGLE2 || property->GetName() == N_TMPL_ANGLE3 || property->GetName() == N_TMPL_G
			|| property->GetName() == N_TMPL_QUALITY || property->GetName() == N_TMPL_CURVATURE)
		{
			if (!pendingValue.IsNull())
			{
				wxString strPendingValue = pendingValue.GetString();

				long longPendingValue;
				if (!strPendingValue.ToLong(&longPendingValue))
				{
					event.Veto();
					event.SetValidationFailureBehavior(wxPG_VFB_STAY_IN_PROPERTY | wxPG_VFB_BEEP | wxPG_VFB_SHOW_MESSAGEBOX);
					return;
				}
				if (longPendingValue < 0 || longPendingValue > 255)
				{
					event.Veto();
					event.SetValidationFailureBehavior(wxPG_VFB_STAY_IN_PROPERTY | wxPG_VFB_BEEP | wxPG_VFB_SHOW_MESSAGEBOX);
					return;
				}
			}
		}
		else if (property->GetName() == N_TMPL_MINUTIAE_FORMAT)
		{
			wxStringTokenizer tokenizer(pendingValue.GetString(), ";");
			wxString minutiaFormatString;
			int count = 0;

			while (tokenizer.HasMoreTokens())
			{
				minutiaFormatString = tokenizer.GetNextToken();
				count += NEnum::Parse(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), minutiaFormatString);
			}

			NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), count);

			if (count == 0 && minutiaFormatString != (wxString)NEnum::ToString(NBiometricTypes::NFMinutiaFormatNativeTypeOf(), nfmfNone))
			{
				event.Veto();
				event.SetValidationFailureBehavior(wxPG_VFB_STAY_IN_PROPERTY |
					wxPG_VFB_BEEP |
					wxPG_VFB_SHOW_MESSAGEBOX);
				return;
			}
		}
	}

	void CollectionEditorDialog::OnPropertyValueChange(wxPropertyGridEvent& event)
	{
		if (m_listItemId < 0)	m_listItemId = 0;

		wxVariant selection = collectionPropertyGrid->GetSelectedProperty()->GetValue();
		wxString listViewString = m_collectionAdaptor->PropertyValueChanged(collectionPropertyGrid->GetSelectedProperty()->GetName(), m_listItemId, selection);

		ArrayRemoveAt(m_listItemId);
		itemList->DeleteItem(m_listItemId);
		ArrayInsert(listViewString, m_listItemId);

		wxListItem item;
		item.SetId(m_listItemId);
		item.SetText(wxString::Format(wxT("%i"), m_listItemId));
		itemList->InsertItem(item);
		itemList->SetItem(m_listItemId, 1, listViewString);

		event.Skip();
	}

	void CollectionEditorDialog::AddCollectionsToList()
	{
		if (m_collectionAdaptor->GetItemsCount() <= 0)
		{
			int itemCount = m_collectionAdaptor->GetItemsCount();

			if (itemCount <= 0)	btnRemove->Enable(false);

			for (int i = 0; i < itemCount; i++){
				wxString listString = ArrayGet(i);
				wxListItem item;
				item.SetId(i);
				item.SetText(wxString::Format(wxT("%i"), i));
				itemList->InsertItem(item);
				itemList->SetItem(item, 1, listString);
				m_modified = true;
			}
		}
		else
		{
			if (!m_modified)
			{
				m_listItemId = 0;
				int itemCount = m_collectionAdaptor->GetItemsCount();

				if (itemCount <= 0)	btnRemove->Enable(false);

				collectionPropertyGrid->Clear();

				for (int i = 0; i < itemCount; i++){
					m_collectionAdaptor->UpdateItemCollection(itemList, collectionPropertyGrid, i);
					ArrayInsert(m_collectionAdaptor->GetListViewString(), i);
				}
			}
		}
	}

	void CollectionEditorDialog::SetAddedRecordCountBy(int value)
	{
		m_newItemCount += value;
	}

	int CollectionEditorDialog::GetAddedRecordCount()
	{
		return m_newItemCount;
	}

	wxArrayString CollectionEditorDialog::GetItemsArray()
	{
		return m_arrayItems;
	}

	wxString CollectionEditorDialog::ArrayGet(size_t index)
	{
		return m_arrayItems[index];
	}

	size_t CollectionEditorDialog::ArrayGetCount()
	{
		return m_arrayItems.GetCount();
	}

	bool CollectionEditorDialog::ArrayInsert(const wxString& str, int index)
	{
		if (index < 0)
			m_arrayItems.Add(str);
		else
			m_arrayItems.Insert(str, index);

		return TRUE;
	}

	bool CollectionEditorDialog::ArraySet(size_t index, const wxString& str)
	{
		m_arrayItems[index] = str;
		return TRUE;
	}

	void CollectionEditorDialog::ArrayRemoveAt(int index)
	{
		if (index >= 0) m_arrayItems.RemoveAt(index);
	}

	void CollectionEditorDialog::ArraySwap(size_t first, size_t second)
	{
		wxString temp1 = m_arrayItems[first];
		wxString temp2 = m_arrayItems[second];

		m_arrayItems[first] = temp2;
		m_arrayItems[second] = temp1;
	}

	void CollectionEditorDialog::SetArrayItems(wxArrayString arrayItems)
	{
		m_arrayItems = arrayItems;
	}
}}}
