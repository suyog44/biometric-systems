#include "Precompiled.h"
#include "TemplateSample.h"
#include "TemplateSampleForm.h"
#include "NTemplateSampleWXVersionInfo.h"
#include "RecordAddDialogs/NFingerRecordDlg.h"
#include "RecordAddDialogs/NIrisRecordDlg.h"
#include "CollectionEditor/NCollectionProperty.h"
#include "CollectionEditor/MultiChoiceProperty.h"

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NGui/Gui/Neurotechnology.xpm>
#else
	#include <Gui/Neurotechnology.xpm>
#endif

using namespace Neurotec;
using namespace Neurotec::IO;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Licensing;
using namespace Neurotec::Samples::CollectionEditor;
using namespace Neurotec::Samples::Controllers;
using namespace Neurotec::Samples::MainUIComponentsHelpers;

namespace Neurotec { namespace Samples
{
	BEGIN_EVENT_TABLE(TemplateSampleForm, wxFrame)
		EVT_MENU(ID_MNU_NEW, TemplateSampleForm::NewTemplate)
		EVT_MENU(ID_MNU_SAVE, TemplateSampleForm::SaveTemplate)
		EVT_MENU(ID_MNU_OPEN, TemplateSampleForm::OpenTemplate)
		EVT_PG_CHANGED(ID_PROPERY_GRID, TemplateSampleForm::OnPropertyGridValueChange)
		EVT_PG_CHANGING(ID_PROPERY_GRID, TemplateSampleForm::OnPropertyGridValueChanging)
		EVT_MENU(ID_MNU_ADD_FINGERS, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_FINGER, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_FACES, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_FACE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_IRISES, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_IRISE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_VOICES, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_VOICE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_PALMS, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_PALM, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_FINGERS_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_FINGER_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_FACES_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_FACE_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_IRISES_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_IRISE_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_VOICES_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_VOICE_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_PALMS_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_ADD_PALM_FROM_FILE, TemplateSampleForm::ChangeNTemplate)
		EVT_MENU(ID_MNU_REMOVE, TemplateSampleForm::Remove)
		EVT_MENU(ID_MNU_SAVE_ITEM, TemplateSampleForm::SaveItem)
		EVT_MENU(ID_MNU_ABOUT, TemplateSampleForm::About)
		EVT_TREE_SEL_CHANGED(ID_TREE_VIEW, TemplateSampleForm::TreeViewSelectionChanged)
		EVT_TREE_SEL_CHANGING(ID_TREE_VIEW, TemplateSampleForm::TreeViewSelectionChanging)
		EVT_CLOSE(TemplateSampleForm::OnClose)
		EVT_MENU(ID_MNU_EXIT, TemplateSampleForm::OnExitClick)
	END_EVENT_TABLE()

	TemplateSampleForm::TemplateSampleForm(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize& size, long style) : wxFrame(parent, id, title, position, size, style)
	{
		InitMembers();
		CreateGUIControls();
		RefreshTemplateView();
	}

	void TemplateSampleForm::InitMembers()
	{
		m_templateSampleController = new TemplateSampleController();
	}

	TemplateSampleForm::~TemplateSampleForm()
	{
		if (m_zoomSlider) m_zoomSlider->SetView(NULL);
		delete m_templateSampleController;
	}

	void TemplateSampleForm::CreateGUIControls()
	{
		menuBar = new wxMenuBar();
		menu = new wxMenu();
		menu->Append(ID_MNU_NEW, N_TMPL_NEW);
		menu->Append(ID_MNU_OPEN, N_TMPL_OPEN);
		menu->Append(ID_MNU_SAVE, N_TMPL_SAVE);
		menu->AppendSeparator();
#ifdef __WXMAC__
		wxApp::s_macExitMenuItemId = wxID_EXIT;
		menu->Append(wxID_EXIT);
#else
		menu->Append(ID_MNU_EXIT, N_TMPL_EXIT);
#endif
		menuBar->Append(menu, N_TMPL_FILE);
		menu = new wxMenu();
		menu->Append(ID_MNU_ADD_FINGERS, N_TMPL_ADD_FINGERS);
		menu->Append(ID_MNU_ADD_FACES, N_TMPL_ADD_FACES);
		menu->Append(ID_MNU_ADD_IRISES, N_TMPL_ADD_IRISES);
		menu->Append(ID_MNU_ADD_PALMS, N_TMPL_ADD_PALMS);
		menu->Append(ID_MNU_ADD_VOICES, N_TMPL_ADD_VOICES);
		menu->AppendSeparator();
		menu->Append(ID_MNU_ADD_FINGERS_FROM_FILE, N_TMPL_ADD_FINGERS_FRM_FILE);
		menu->Append(ID_MNU_ADD_FACES_FROM_FILE, N_TMPL_ADD_FACES_FRM_FILE);
		menu->Append(ID_MNU_ADD_IRISES_FROM_FILE, N_TMPL_ADD_IRISES_FRM_FILE);
		menu->Append(ID_MNU_ADD_PALMS_FROM_FILE, N_TMPL_ADD_PALMS_FRM_FILE);
		menu->Append(ID_MNU_ADD_VOICES_FROM_FILE, N_TMPL_ADD_VOICES_FRM_FILE);
		menu->AppendSeparator();
		menu->Append(ID_MNU_ADD_FINGER, N_TMPL_ADD_FINGER);
		menu->Append(ID_MNU_ADD_FACE, N_TMPL_ADD_FACE);
		menu->Append(ID_MNU_ADD_IRISE, N_TMPL_ADD_IRIS);
		menu->Append(ID_MNU_ADD_PALM, N_TMPL_ADD_PALM);
		menu->Append(ID_MNU_ADD_VOICE, N_TMPL_ADD_VOICE);
		menu->AppendSeparator();
		menu->Append(ID_MNU_ADD_FINGER_FROM_FILE, N_TMPL_ADD_FINGER_FRM_FILE);
		menu->Append(ID_MNU_ADD_FACE_FROM_FILE, N_TMPL_ADD_FACE_FRM_FILE);
		menu->Append(ID_MNU_ADD_IRISE_FROM_FILE, N_TMPL_ADD_IRIS_FRM_FILE);
		menu->Append(ID_MNU_ADD_PALM_FROM_FILE, N_TMPL_ADD_PALM_FRM_FILE);
		menu->Append(ID_MNU_ADD_VOICE_FROM_FILE, N_TMPL_ADD_VOICE_FRM_FILE);
		menu->AppendSeparator();
		menu->Append(ID_MNU_REMOVE, N_TMPL_REMOVE);
		menu->Append(ID_MNU_SAVE_ITEM, N_TMPL_SAVE_ITEM);
		menuBar->Append(menu, N_TMPL_EDIT);

		menuBar->Enable(ID_MNU_ADD_FINGER, false);
		menuBar->Enable(ID_MNU_ADD_FACE, false);
		menuBar->Enable(ID_MNU_ADD_IRISE, false);
		menuBar->Enable(ID_MNU_ADD_PALM, false);
		menuBar->Enable(ID_MNU_ADD_VOICE, false);

		menuBar->Enable(ID_MNU_ADD_FINGER_FROM_FILE, false);
		menuBar->Enable(ID_MNU_ADD_FACE_FROM_FILE, false);
		menuBar->Enable(ID_MNU_ADD_IRISE_FROM_FILE, false);
		menuBar->Enable(ID_MNU_ADD_PALM_FROM_FILE, false);
		menuBar->Enable(ID_MNU_ADD_VOICE_FROM_FILE, false);

		menuBar->Enable(ID_MNU_REMOVE, false);
		menuBar->Enable(ID_MNU_SAVE_ITEM, false);

		menu = new wxMenu();
#ifdef __WXMAC__
		wxApp::s_macAboutMenuItemId = wxID_ABOUT;
		menu->Append(wxID_ABOUT);
#else
		menu->Append(ID_MNU_ABOUT, N_TMPL_ABOUT);
#endif
		menuBar->Append(menu, N_TMPL_HELP);
		SetMenuBar(menuBar);

		splitterLeft = new wxSplitterWindow(this, ID_SPLITTER_LEFT, wxDefaultPosition, wxDefaultSize, wxSP_3D);
		splitterRight = new wxSplitterWindow(this, ID_SPLITTER_RIGHT, wxDefaultPosition, wxDefaultSize, wxSP_3D);

		m_templateSampleTreeCtrl = new TemplateSampleTreeCtrl(splitterLeft, ID_TREE_VIEW, wxPoint(0, 0), wxSize(243, 184), wxTR_HAS_BUTTONS | wxTR_LINES_AT_ROOT);
		m_templateSamplePropertyGrid = new TemplateSamplePropertyGrid(splitterLeft, ID_PROPERY_GRID, wxDefaultPosition, wxSize(243, 301), wxPG_BOLD_MODIFIED | wxPG_AUTO_SORT | wxPG_SPLITTER_AUTO_CENTER | wxPG_DEFAULT_STYLE);
		m_fingerView = new wxNFingerView(splitterRight, ID_FINGER_VIEW);
		m_palmView = new wxNPalmView(splitterRight, ID_FINGER_VIEW);
		m_fingerView->SetMinSize(wxSize(482, 20));
		m_palmView->SetMinSize(wxSize(482, 20));
		m_fingerView->SetZoomToFit(false);
		m_palmView->SetZoomToFit(false);
		m_zoomSlider = new wxNViewZoomSlider(this);

		m_palmView->Hide();

		m_fingerView->Connect(m_fingerView->GetId(), wxEVT_LEFT_DOWN, wxMouseEventHandler(TemplateSampleForm::OnFingerViewClick), NULL, this);
		m_palmView->Connect(m_palmView->GetId(), wxEVT_LEFT_DOWN, wxMouseEventHandler(TemplateSampleForm::OnFingerViewClick), NULL, this);

		wxBoxSizer * bSizer = new wxBoxSizer(wxVERTICAL);
		bSizer->Add(splitterRight, 1, wxALL | wxEXPAND);
		bSizer->Add(m_zoomSlider, 0, wxALL);

		fgSizer = new wxFlexGridSizer(1, 2, 0, 0);
		fgSizer->Add(splitterLeft, 1, wxEXPAND);
		fgSizer->Add(bSizer, 1, wxEXPAND | wxRIGHT);
		fgSizer->AddGrowableRow(0, 0);
		fgSizer->AddGrowableCol(0, 0);
		fgSizer->AddGrowableCol(1, 0);

		splitterLeft->SplitHorizontally(m_templateSampleTreeCtrl, m_templateSamplePropertyGrid, 0);
		splitterRight->SplitVertically(m_fingerView, m_palmView);
		splitterRight->Unsplit(m_palmView);

		this->SetSizer(fgSizer);
		this->Layout();

		UpdateMenu();

		SetTitle(TEMPLATE_SAMPLE_WX_TITLE);
		SetIcon(Neurotechnology_XPM);
		SetSize(wxSize(745, 552));
		Center();
	}

	void TemplateSampleForm::OnClose(wxCloseEvent& /*event*/)
	{
		Destroy();
	}

	void TemplateSampleForm::OnSelectedImageChanged(wxTreeItemId itemId)
	{
		UpdateMenu();
		UpdatePropertyGridAndFingerView(itemId);
	}

	void TemplateSampleForm::RefreshTemplateView()
	{
		m_templateSampleTreeCtrl->SetTemplate(m_templateSampleController->GetTemplate());
		m_templateSampleTreeCtrl->SetFilename(m_templateSampleController->GetFileName());
		m_templateSamplePropertyGrid->SetTemplate(m_templateSampleController->GetTemplate());

		UpdateMenu();
		m_templateSampleTreeCtrl->UpdateTreeView();
	}

	void TemplateSampleForm::UpdatePropertyGridAndFingerView(wxTreeItemId itemId)
	{
		NObject obj = m_templateSampleTreeCtrl->GetCurrentSelectionData(itemId);
		int currentItemId = m_templateSampleTreeCtrl->GetCurrentSelectionId(itemId);

		m_templateSamplePropertyGrid->UpdatePropertyGrid(obj, currentItemId);

		NFRecord nfRecord = NULL;
		wxString type = obj.GetNativeType().GetName();

		if (type == N_TMPL_NFREC) nfRecord = (NFRecord&)obj;

		if(type == N_TMPL_NFTMPL || type == N_TMPL_NLTMPL || type == N_TMPL_NSTMPL || type == N_TMPL_NETMPL ||
			type == N_TMPL_NFREC || type == N_TMPL_NLREC || type == N_TMPL_NSREC || type == N_TMPL_NEREC)
		{
			menuBar->Enable(ID_MNU_REMOVE, true);
			menuBar->Enable(ID_MNU_SAVE_ITEM, true);
		}
		else 
		{
			menuBar->Enable(ID_MNU_REMOVE, false);
			menuBar->Enable(ID_MNU_SAVE_ITEM, false);
		}

		if (!nfRecord.IsNull())
		{
			nfRecord = (NFRecord&)obj;
			NFrictionRidge frictionRidge = m_templateSampleController->GetDataForFingerView(nfRecord);

			if (frictionRidge.GetBiometricType() == nbtFinger)
			{
				m_fingerView->SetFinger((NFinger&)frictionRidge);
				m_fingerView->Show();
				m_zoomSlider->SetView(m_fingerView);

				splitterRight->SplitVertically(m_fingerView, m_palmView);
				splitterRight->Unsplit(m_palmView);
			}
			else
			{
				m_palmView->SetPalm((NPalm&)frictionRidge);
				m_palmView->Show();
				m_zoomSlider->SetView(m_palmView);

				splitterRight->SplitVertically(m_fingerView, m_palmView);
				splitterRight->Unsplit(m_fingerView);
			}
			m_zoomSlider->Show();
		}
		else
		{
			m_fingerView->Clear();
			m_palmView->Clear();
			m_zoomSlider->Hide();
		}
		PostSizeEvent();
	}

	void TemplateSampleForm::OnFingerViewClick(wxMouseEvent& /*event*/)
	{
		m_fingerView->Refresh();
		m_palmView->Refresh();
	}

	void TemplateSampleForm::OnPropertyGridValueChanging(wxPropertyGridEvent& event)
	{
		m_templateSamplePropertyGrid->PropertyGridValueChanging(event);
	}

	void TemplateSampleForm::OnPropertyGridValueChange(wxPropertyGridEvent& event)
	{
		wxVariant value = m_templateSamplePropertyGrid->GetSelectedProperty()->GetValue();
		wxString name = m_templateSamplePropertyGrid->GetSelectedProperty()->GetName();

		NObject obj = m_templateSampleTreeCtrl->GetCurrentSelectionData(m_currentTreeItemId);
		int currentItemId = m_templateSampleTreeCtrl->GetCurrentSelectionId(m_currentTreeItemId);

		if (event.GetPropertyValue().GetString() == wxT("CollectionChanged"))
		{
			m_templateSampleTreeCtrl->UpdateTreeView();
		}

		m_fingerView->Refresh();
		m_palmView->Refresh();

		NObject parentObject = m_templateSampleTreeCtrl->GetCurrentSelectionParentData(m_currentTreeItemId);
		bool isPalm = false;
		if(!parentObject.IsNull())
		{
			wxString type = parentObject.GetNativeType().GetName();
			if(type == N_TMPL_NFTMPL)
			{
				NFTemplate nfTemplate = (NFTemplate&)parentObject;
				isPalm = nfTemplate.IsPalm();
			}
		}

		m_templateSamplePropertyGrid->PropertyGridValueChange(obj, currentItemId, name, value, isPalm);
	}

	void TemplateSampleForm::ChangeNTemplate(wxCommandEvent &event)
	{
		NFingerRecordDlg addFingerRecDlg(this, false);
		NFingerRecordDlg addPalmRecDlg(this, true);
		NIrisRecordDlg addIrisRecDlg(this);

		NTemplate nTemplate = m_templateSampleController->GetTemplate();

		switch (event.GetId())
		{
		case ID_MNU_ADD_FINGER:
			if (addFingerRecDlg.ShowModal() == wxID_OK)
			{
				nTemplate.GetFingers().GetRecords().Add(
					NFRecord(false, addFingerRecDlg.GetWidth(), addFingerRecDlg.GetHeight(), addFingerRecDlg.GetHorizontalResolution(), addFingerRecDlg.GetVirticalResolution()));
			}
			break;
		case ID_MNU_ADD_FINGERS:
			nTemplate.SetFingers(NFTemplate(false));
			break;
		case ID_MNU_ADD_FINGER_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_FTEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_FRECORD_DLG_MASK, N_TMPL_NFREC);
			break;
		case ID_MNU_ADD_FINGERS_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_FTEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_FRECORD_DLG_MASK, N_TMPL_NFTMPL);
			break;
		case ID_MNU_ADD_PALM:
			if (addPalmRecDlg.ShowModal() == wxID_OK)
			{
				nTemplate.GetPalms().GetRecords().Add(
					NFRecord(true, addPalmRecDlg.GetWidth(), addPalmRecDlg.GetHeight(), addPalmRecDlg.GetHorizontalResolution(), addPalmRecDlg.GetVirticalResolution()));
			}
			break;
		case ID_MNU_ADD_PALMS:
			nTemplate.SetPalms(NFTemplate(true));
			break;
		case ID_MNU_ADD_PALM_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_FTEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_FRECORD_DLG_MASK, wxT("NFRecord_Palm"));
			break;
		case ID_MNU_ADD_PALMS_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_FTEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_FRECORD_DLG_MASK, wxT("NFTemplate_Palm"));
			break;
		case ID_MNU_ADD_FACE:
			nTemplate.GetFaces().GetRecords().Add(NLRecord());
			break;
		case ID_MNU_ADD_FACES:
			nTemplate.SetFaces(NLTemplate());
			break;
		case ID_MNU_ADD_FACE_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_LTEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_LRECORD_DLG_MASK, N_TMPL_NLREC);
			break;
		case ID_MNU_ADD_FACES_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_LTEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_LRECORD_DLG_MASK, N_TMPL_NLTMPL);
			break;
		case ID_MNU_ADD_IRISE:
			if (addIrisRecDlg.ShowModal() == wxID_OK)
			{
				nTemplate.GetIrises().GetRecords().Add(
					NERecord(addIrisRecDlg.GetWidth(), addIrisRecDlg.GetHeight()));
			}
			break;
		case ID_MNU_ADD_IRISES:
			nTemplate.SetIrises(NETemplate());
			break;
		case ID_MNU_ADD_IRISE_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_ETEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_ERECORD_DLG_MASK, N_TMPL_NEREC);
			break;
		case ID_MNU_ADD_IRISES_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_ETEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_ERECORD_DLG_MASK, N_TMPL_NETMPL);
			break;
		case ID_MNU_ADD_VOICE:
			nTemplate.GetVoices().GetRecords().Add(NSRecord());
			break;
		case ID_MNU_ADD_VOICES:
			nTemplate.SetVoices(NSTemplate());
			break;
		case ID_MNU_ADD_VOICE_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_STEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_SRECORD_DLG_MASK, N_TMPL_NSREC);
			break;
		case ID_MNU_ADD_VOICES_FROM_FILE:
			m_templateSampleController->OpenBuffer(this, N_TMPL_STEMPLATE_DLG_MASK N_TMPL_SEP  N_TMPL_SRECORD_DLG_MASK, N_TMPL_NSTMPL);
			break;
		}

		m_templateSampleController->SetTemplate(nTemplate);

		RefreshTemplateView();
	}

	void TemplateSampleForm::Remove(wxCommandEvent &/*event*/)
	{
		NObject obj = m_templateSampleTreeCtrl->GetCurrentSelectionData(m_currentTreeItemId);
		int currentItemId = m_templateSampleTreeCtrl->GetCurrentSelectionId(m_currentTreeItemId);

		m_templateSampleController->RemoveItem(obj, currentItemId);

		RefreshTemplateView();
	}

	void TemplateSampleForm::SaveItem(wxCommandEvent &/*event*/)
	{
		NObject obj = m_templateSampleTreeCtrl->GetCurrentSelectionData(m_currentTreeItemId);
		int currentItemId = m_templateSampleTreeCtrl->GetCurrentSelectionId(m_currentTreeItemId);

		m_templateSampleController->SaveItem(this, obj, currentItemId);
	}

	void TemplateSampleForm::About(wxCommandEvent &/*event*/)
	{
		wxAboutBox aboutBox(this, wxID_ANY, TEMPLATE_SAMPLE_WX_PRODUCT_NAME, TEMPLATE_SAMPLE_WX_VERSION_STRING, TEMPLATE_SAMPLE_WX_COPYRIGHT);

		aboutBox.ShowModal();
	}

	void TemplateSampleForm::TreeViewSelectionChanged(wxTreeEvent &event)
	{
		OnSelectedImageChanged(event.GetItem());
		m_currentTreeItemId = event.GetItem();
	}

	void TemplateSampleForm::TreeViewSelectionChanging(wxTreeEvent &/*event*/)
	{
		m_templateSamplePropertyGrid->CommitChangesFromEditor();
		m_templateSamplePropertyGrid->UnfocusEditor();
	}

	void TemplateSampleForm::NewTemplate(wxCommandEvent &/*event*/)
	{
		ClearData();
		RefreshTemplateView();
	}

	void TemplateSampleForm::SaveTemplate(wxCommandEvent &/*event*/)
	{
		try
		{
			m_templateSampleController->SaveBuffer(this, N_TMPL_TEMPLATE_DLG_MASK, m_templateSampleController->GetTemplate().Save());
		}
		catch(NError& er)
		{
			wxExceptionDlg::Show(er);
		}
	}

	void TemplateSampleForm::OpenTemplate(wxCommandEvent &/*event*/)
	{
		m_templateSampleController->OpenBuffer(this, N_TMPL_OPEN_DIALOG_FILTER, N_TMPL_NTMPL);
		RefreshTemplateView();
	}

	void TemplateSampleForm::OnExitClick(wxCommandEvent &/*event*/)
	{
		Close(false);
	}

	void TemplateSampleForm::ClearData()
	{
		m_templateSampleController->SetFileName(N_TMPL_NO_WCHARS);
		m_templateSampleController->SetTemplate(NTemplate());
	}

	void TemplateSampleForm::UpdateMenu()
	{
		m_templateSampleController->UpdateMenu(menuBar, m_templateSampleController->GetTemplate());
	}
}}
