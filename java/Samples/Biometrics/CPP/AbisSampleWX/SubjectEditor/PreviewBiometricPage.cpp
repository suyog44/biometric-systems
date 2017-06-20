#include "Precompiled.h"

#include <SubjectEditor/PreviewBiometricPage.h>
#include <SubjectEditor/VoiceView.h>

#include <Resources/SaveIcon.xpm>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Images;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

PreviewBiometricPage::PreviewBiometricPage(NBiometricClient& biometricClient, NSubject& subject, SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid) :
	ModalityPage(biometricClient, subject, subjectEditorPageInterface, parent, winid),
	m_canSave(false),
	m_showBinarizedCheckbox(false),
	m_biometric(NULL),
	m_selection(NULL),
	m_view(NULL),
	m_icaoWarningsView(NULL),
	m_zoomSlider(NULL)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

PreviewBiometricPage::~PreviewBiometricPage()
{
	UnregisterGuiEvents();
}

void PreviewBiometricPage::SetSelection(Node * node)
{
	m_selection = node;
}

void PreviewBiometricPage::OnNavigatingFrom()
{
	m_biometric = NULL;
	m_generalizeView->Clear();
	m_chbShowBinarized->SetValue(false);
	m_zoomSlider->SetView(NULL);
	if (m_view)
	{
		m_centerSizer->Remove(1);
		delete m_view;
		m_view = NULL;
	}
	m_icaoWarningsView->SetFace(NULL);
	m_generalizeView->SetIcaoView(NULL);
}

void PreviewBiometricPage::OnNavigatedTo()
{
	m_showBinarizedCheckbox = false;
	m_canSave = false;

	if (m_selection && m_selection->IsBiometricNode())
	{
		bool withGeneralization = m_selection->IsGeneralizedNode();
		std::vector<NBiometric> items = m_selection->GetItems();
		std::vector<NBiometric> generalized = m_selection->GetAllGeneralized();
		m_generalizeView->Show(withGeneralization);
		m_icaoWarningsView->Hide();
		m_biometric = items[0];
		switch(m_selection->GetBiometricType())
		{
		case nbtFinger:
			{
				wxNFingerView * view = new wxNFingerView(this, wxID_ANY);
				view->EnableContextMenu(false);
				view->SetFinger(NObjectDynamicCast<NFinger>(m_biometric));
				m_view = view;
				m_zoomSlider->SetView(view);
				m_zoomSlider->Show();
				break;
			}
		case nbtPalm:
			{
				wxNPalmView * view = new wxNPalmView(this, wxID_ANY);
				view->EnableContextMenu(false);
				view->SetPalm(NObjectDynamicCast<NPalm>(m_biometric));
				m_view = view;
				m_zoomSlider->SetView(view);
				m_zoomSlider->Show();
				break;
			}
		case nbtFace:
			{
				wxNFaceView * view = new wxNFaceView(this, wxID_ANY);
				view->EnableContextMenu(false);
				view->SetFace(NObjectDynamicCast<NFace>(m_biometric));
				view->SetShowIcaoArrows(false);
				m_view = view;

				if ((m_selection->GetParent() && m_selection->GetParent()->IsBiometricNode()) || !m_selection->GetChildren().empty())
				{
					m_icaoWarningsView->Show();
				}

				m_zoomSlider->SetView(view);
				m_zoomSlider->Show();
				break;
			}
		case nbtIris:
			{
				wxNIrisView * view = new wxNIrisView(this, wxID_ANY);
				view->EnableContextMenu(false);
				view->SetIris(NObjectDynamicCast<NIris>(m_biometric));
				m_view = view;
				m_zoomSlider->SetView(view);
				m_zoomSlider->Show();
				break;
			}
		case nbtVoice:
			{
				VoiceView * view = new VoiceView(this, wxID_ANY);
				view->SetVoice(NObjectDynamicCast<NVoice>(m_biometric));
				m_view = view;
				m_zoomSlider->Hide();
				break;
			}
		default:
			NThrowNotImplementedException();
		};

		m_view->SetBackgroundColour(wxNullColour);
		m_centerSizer->Insert(1, m_view, 1, wxALL | wxEXPAND);

		OnBiometricChanged(items[0]);
		if (withGeneralization)
		{
			m_generalizeView->SetView(reinterpret_cast<wxNView*>(m_view));
			m_generalizeView->SetBiometrics(items);
			m_generalizeView->SetGeneralized(generalized);
			m_generalizeView->SetSelected(generalized.empty() ? items[0] : generalized[0]);
			if (m_selection->GetBiometricType() == nbtFace)
			{
				m_generalizeView->SetIcaoView(m_icaoWarningsView);
				m_icaoWarningsView->SetFace(NObjectDynamicCast<NFace>(m_biometric));
			}
		}
		else if (m_selection->GetBiometricType() == nbtFace)
		{
			m_icaoWarningsView->SetFace(NObjectDynamicCast<NFace>(m_biometric));
		}
	}

	ModalityPage::OnNavigatedTo();
}

void PreviewBiometricPage::OnBiometricChanged(NBiometric biometric)
{
	bool canSave = false;
	bool showBinarized = false;

	m_biometric = biometric;
	if (!m_biometric.IsNull())
	{
		m_btnSave->SetLabel(wxT("Save image"));

		NBiometricType type = biometric.GetBiometricType();
		if (type == nbtFinger || type == nbtPalm)
		{
			NFrictionRidge ridge = NObjectDynamicCast<NFrictionRidge>(biometric);
			canSave = !ridge.GetImage().IsNull();
			showBinarized = !ridge.GetBinarizedImage().IsNull();
		}
		else if (type == nbtFace)
		{
			NFace face = NObjectDynamicCast<NFace>(biometric);
			canSave = !face.GetImage().IsNull();
		}
		else if (type == nbtIris)
		{
			NIris iris = NObjectDynamicCast<NIris>(biometric);
			canSave = !iris.GetImage().IsNull();
		}
		else if (type == nbtVoice)
		{
			NVoice voice = NObjectDynamicCast<NVoice>(biometric);
			canSave = !voice.GetSoundBuffer().IsNull();
			m_btnSave->SetLabel(wxT("Save audio file"));
		}
	}

	m_btnSave->Show(canSave);
	m_chbShowBinarized->Show(showBinarized);
	if (!showBinarized && m_chbShowBinarized->GetValue())
	{
		wxCommandEvent empty;
		m_chbShowBinarized->SetValue(true);
		OnShowProcessedImageClick(empty);
	}
}

void PreviewBiometricPage::OnFinishClick(wxCommandEvent&)
{
	SelectFirstPage();
}

void PreviewBiometricPage::OnSaveClick(wxCommandEvent&)
{
	wxString filter = wxEmptyString;
	NBiometricType type = m_biometric.GetBiometricType();
	if (type != nbtVoice)
	{
		filter = Common::GetSaveFileFilterString();
	}

	wxFileDialog saveFileDialog(this, wxT("Save as"), wxEmptyString, wxEmptyString, filter, wxFD_SAVE);
	if (saveFileDialog.ShowModal() == wxID_OK)
	{
		try
		{
			wxString filePath = saveFileDialog.GetPath();
			if (type == nbtVoice)
			{
				NVoice voice = NObjectDynamicCast<NVoice>(m_biometric);
				voice.GetSoundBuffer().Save(filePath);
			}
			else
			{
				NImage image = NULL;
				if (type == nbtFinger || type == nbtPalm)
				{
					NFrictionRidge frictionRidge = NObjectDynamicCast<NFrictionRidge>(m_biometric);
					image = frictionRidge.GetImage();
				}
				else if (type == nbtIris)
				{
					NIris iris = NObjectDynamicCast<NIris>(m_biometric);
					image = iris.GetImage();
				}
				else if (type == nbtFace)
				{
					NFace face = NObjectDynamicCast<NFace>(m_biometric);
					image = face.GetImage();
				}

				image.Save(filePath);
			}
		}
		catch(NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}
	}
}

void PreviewBiometricPage::OnShowProcessedImageClick(wxCommandEvent&)
{
	wxNFrictionRidgeView *view = dynamic_cast<wxNFrictionRidgeView *>(m_view);
	if (view)
		view->SetShownImage(m_chbShowBinarized->GetValue() ? wxNFrictionRidgeView::PROCESSED_IMAGE : wxNFrictionRidgeView::ORIGINAL_IMAGE);
}

void PreviewBiometricPage::OnSelectedItemChanged(wxCommandEvent&)
{
	if (m_generalizeView->IsShown())
		OnBiometricChanged(m_generalizeView->GetSelected());
}

void PreviewBiometricPage::RegisterGuiEvents()
{
	m_btnSave->Connect(wxEVT_BUTTON, wxCommandEventHandler(PreviewBiometricPage::OnSaveClick), NULL, this);
	m_btnFinish->Connect(wxEVT_BUTTON, wxCommandEventHandler(PreviewBiometricPage::OnFinishClick), NULL, this);
	m_chbShowBinarized->Connect(wxEVT_COMMAND_CHECKBOX_CLICKED, wxCommandEventHandler(PreviewBiometricPage::OnShowProcessedImageClick), NULL, this);
	m_generalizeView->Connect(wxEVT_GEN_SELECTED_ITEM_CHANGED, wxCommandEventHandler(PreviewBiometricPage::OnSelectedItemChanged), NULL, this);
}

void PreviewBiometricPage::UnregisterGuiEvents()
{
	m_btnSave->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(PreviewBiometricPage::OnSaveClick), NULL, this);
	m_btnFinish->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(PreviewBiometricPage::OnFinishClick), NULL, this);
	m_chbShowBinarized->Disconnect(wxEVT_COMMAND_CHECKBOX_CLICKED, wxCommandEventHandler(PreviewBiometricPage::OnShowProcessedImageClick), NULL, this);
	m_generalizeView->Disconnect(wxEVT_GEN_SELECTED_ITEM_CHANGED, wxCommandEventHandler(PreviewBiometricPage::OnSelectedItemChanged), NULL, this);
}

void PreviewBiometricPage::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer, true);

	m_viewSizer = new wxBoxSizer(wxVERTICAL);
	sizer->Add(m_viewSizer, 1, wxALL | wxEXPAND);

	m_centerSizer = new wxFlexGridSizer(1, 2, 0, 0);
	m_centerSizer->AddGrowableCol(1);
	m_centerSizer->AddGrowableRow(0);
	m_centerSizer->SetFlexibleDirection(wxBOTH);
	m_centerSizer->SetNonFlexibleGrowMode(wxFLEX_GROWMODE_SPECIFIED);

	m_icaoWarningsView = new IcaoWarningsView(this);
	m_centerSizer->Add(m_icaoWarningsView, 0, wxALL | wxEXPAND, 5);

	m_generalizeView = new GeneralizeProgressView(this, wxID_ANY);
	m_generalizeView->SetMinSize(wxSize(20, 20));
	m_viewSizer->Add(m_centerSizer, 1, wxALL | wxEXPAND);
	m_viewSizer->Add(m_generalizeView, 0, wxALL | wxEXPAND);

	wxBoxSizer *szBox = new wxBoxSizer(wxHORIZONTAL);
	sizer->Add(szBox, 0, wxALL | wxEXPAND);

	m_controlsSizer = new wxBoxSizer(wxHORIZONTAL);
	szBox->Add(m_controlsSizer, 1, wxALL | wxEXPAND);

	m_btnSave = new wxButton(this, wxID_ANY, wxEmptyString);
	m_btnSave->SetBitmap(wxImage(saveIcon_xpm));
	m_controlsSizer->Add(m_btnSave, 0, wxALL | wxALIGN_CENTRE_VERTICAL, 5);

	m_zoomSlider = new wxNViewZoomSlider(this);
	m_controlsSizer->Add(m_zoomSlider, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_chbShowBinarized = new wxCheckBox(this, wxID_ANY, wxT("Show binarized image"));
	m_controlsSizer->Add(m_chbShowBinarized, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	wxBoxSizer *szFinish = new wxBoxSizer(wxVERTICAL);
	szBox->Add(szFinish, 0, wxALL | wxEXPAND);

	m_btnFinish = new wxButton(this, wxID_ANY, wxT("Finish"));
	szFinish->Add(m_btnFinish, 0, wxALL | wxALIGN_CENTER_VERTICAL | wxALIGN_RIGHT, 5);

	this->Layout();
}

}}

