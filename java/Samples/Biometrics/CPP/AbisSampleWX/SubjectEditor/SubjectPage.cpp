#include "Precompiled.h"
#include <SubjectEditor/SubjectPage.h>
#include <SubjectEditor/SubjectPagePanel.h>

#include <Resources/SaveIcon.xpm>
#include <Resources/OpenFolderIcon.xpm>

#include <Common/DatabaseOperationPage.h>
#include <Common/EnrollDataSerializer.h>
#include <Common/LicensingTools.h>
#include <Common/LongActionDialog.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;
using namespace Neurotec::IO;
using namespace Neurotec::Images;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_SUBJECT_PAGE_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_SUBJECT_PAGE_THREAD, wxCommandEvent);

BEGIN_EVENT_TABLE(SubjectPage, wxPanel)
	EVT_BUTTON(ID_BUTTON_ENROLL, SubjectPage::OnEnrollClick)
	EVT_BUTTON(ID_BUTTON_ENROLL_WITH_DUPLICATE_CHECK, SubjectPage::OnEnrollWithDuplicateCheckClick)
	EVT_BUTTON(ID_BUTTON_IDENTIFY, SubjectPage::OnIdentifyClick)
	EVT_BUTTON(ID_BUTTON_VERIFY, SubjectPage::OnVerifyClick)
	EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, SubjectPage::OnOpenImageClick)
	EVT_BUTTON(ID_BUTTON_UPDATE, SubjectPage::OnUpdateClick)
	EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, SubjectPage::OnSaveTemplateClick)
END_EVENT_TABLE()

SubjectPage::SubjectPage(NBiometricClient& biometricClient, NSubject& subject, SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid) :
	ModalityPage(biometricClient, subject, subjectEditorPageInterface, parent, winid),
	m_thumbnail(NULL)
{
	CreateGUIControls();

	this->Bind(wxEVT_SUBJECT_PAGE_THREAD, &SubjectPage::OnThread, this);
}

SubjectPage::~SubjectPage()
{
}

void SubjectPage::OnNavigatedTo()
{
	m_txtSubjectId->SetValue(m_subject.GetId());

	Update();
	SetCallbacks();
	GetThumbnail();
	TryFillGenderField();
	UpdateSchemaControls();

	m_txtQuery->AutoComplete(SettingsManager::GetQueryAutoComplete());

	ModalityPage::OnNavigatedTo();
}

void SubjectPage::OnNavigatingFrom()
{
	UnsetCallbacks();

	ModalityPage::OnNavigatingFrom();
}

void SubjectPage::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();
	switch(id)
	{
	case ID_UPDATE_STATUS:
		{
			this->Update();
			break;
		}
	default:
		break;
	};
}

void SubjectPage::Update()
{
	wxColourDatabase colours;
	if (IsSubjectEmpty())
	{
		if (!LicensingTools::CanCreateFingerTemplate(m_biometricClient.GetLocalOperations()) &&
			!LicensingTools::CanCreateFaceTemplate(m_biometricClient.GetLocalOperations()) &&
			!LicensingTools::CanCreateIrisTemplate(m_biometricClient.GetLocalOperations()) &&
			!LicensingTools::CanCreatePalmTemplate(m_biometricClient.GetLocalOperations()) &&
			!LicensingTools::CanCreateVoiceTemplate(m_biometricClient.GetLocalOperations()))
		{
			m_statusPanel->SetMessage(wxT("None of required licenses were obtained. For more information open ActivationWizard"), StatusPanel::ERROR_MESSAGE);
		}
		else
		{
			m_statusPanel->SetMessage(wxT("Subject is empty. Click on wanted modality in tree to create new template"), StatusPanel::INFO_MESSAGE);
		}
		DisableActions();
	}
	else
	{
		m_statusPanel->SetMessage(wxT("Subject is ready for action. Click on buttons above to perform action"), StatusPanel::SUCCESS_MESSAGE);
		EnableActions();
	}
	this->Layout();
}

void SubjectPage::OnEnrollClick(wxCommandEvent &)
{
	wxString id = m_txtSubjectId->GetValue();
	if (id == wxEmptyString)
	{
		wxMessageDialog dialog(this, wxT("Subject id can't be empty"), wxT("Invalid id"), wxOK | wxICON_ERROR);
		m_txtSubjectId->SetFocus();
		dialog.ShowModal();
	}
	else
	{
		m_subject.SetId(id);
		m_subject.SetQueryString(wxEmptyString);
		SetSubjectProperties();
		(static_cast<SubjectPagePanel&>(m_subjectEditorPage)).PerformOperation(nboEnroll);
	}
}

void SubjectPage::OnEnrollWithDuplicateCheckClick(wxCommandEvent &)
{
	wxString id = m_txtSubjectId->GetValue();
	if (id == wxEmptyString)
	{
		wxMessageDialog dialog(this, wxT("Subject id can't be empty"), wxT("Invalid id"), wxOK | wxICON_ERROR);
		m_txtSubjectId->SetFocus();
		dialog.ShowModal();
	}
	else
	{
		m_subject.SetId(id);
		m_subject.SetQueryString(wxEmptyString);
		SetSubjectProperties();
		(static_cast<SubjectPagePanel&>(m_subjectEditorPage)).PerformOperation(nboEnrollWithDuplicateCheck);
	}
}

void SubjectPage::OnIdentifyClick(wxCommandEvent&)
{
	wxString query = m_txtQuery->GetValue();
	m_subject.SetQueryString(query);
	if (query != wxEmptyString)
	{
		wxArrayString suggestions = SettingsManager::GetQueryAutoComplete();
		wxArrayString::iterator f = std::find(suggestions.begin(), suggestions.end(), query);
		if (f == suggestions.end())
		{
			suggestions.Add(query);
			SettingsManager::SetQueryAutoComplete(suggestions);
			m_txtQuery->AutoComplete(suggestions);
		}
	}

	(static_cast<SubjectPagePanel&>(m_subjectEditorPage)).PerformOperation(nboIdentify);
}

void SubjectPage::OnVerifyClick(wxCommandEvent&)
{
	wxString id = m_txtSubjectId->GetValue();
	if (id == wxEmptyString)
	{
		wxMessageDialog dialog(this, wxT("Subject id can't be empty"), wxT("Invalid id"), wxOK | wxICON_ERROR);
		m_txtSubjectId->SetFocus();
		dialog.ShowModal();
	}
	else
	{
		m_subject.SetId(id);
		m_subject.SetQueryString(wxEmptyString);
		(static_cast<SubjectPagePanel&>(m_subjectEditorPage)).PerformOperation(nboVerify);
	}
}

void SubjectPage::OnSaveTemplateClick(wxCommandEvent&)
{
	if (!IsSubjectEmpty())
	{
		wxFileDialog saveFileDialog(this, wxT("Save template as"), wxEmptyString, wxEmptyString,
			wxEmptyString, wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
		if (saveFileDialog.ShowModal() == wxID_OK)
		{
			try
			{
				NBuffer buffer = m_subject.GetTemplateBuffer();
				NFile::WriteAllBytes(saveFileDialog.GetPath(), buffer);
			}
			catch (NError& exception)
			{
				wxExceptionDlg::Show(exception);
			}
		}
	}
}

void SubjectPage::OnOpenImageClick(wxCommandEvent&)
{
	wxFileDialog openFileDialog(this, wxT("Select thumbnail image"), wxEmptyString, wxEmptyString,
		Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST);

	if (openFileDialog.ShowModal() == wxID_OK)
	{
		try
		{
			m_thumbnail = NImage::FromFile(openFileDialog.GetPath());
			m_pictureBox->SetImage(m_thumbnail);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}
	}
}

void SubjectPage::OnUpdateClick(wxCommandEvent&)
{
	wxString subjectId = m_txtSubjectId->GetValue();
	if (subjectId.IsNull() || subjectId.IsEmpty())
	{
		wxMessageDialog dialog(this, wxT("Subject id can't be empty"), wxT("Invalid id"), wxOK | wxICON_ERROR);
		m_txtSubjectId->SetFocus();
		dialog.ShowModal();
	}
	else
	{
		try
		{
			m_subject.SetId(subjectId);
			m_subject.SetQueryString(wxEmptyString);
			SetSubjectProperties();
			(static_cast<SubjectPagePanel&>(m_subjectEditorPage)).PerformOperation(nboUpdate);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
		}
	}
}

void SubjectPage::TryFillGenderField()
{
	SampleDbSchema current = SettingsManager::GetCurrentSchema();
	if (!current.IsEmpty() && current.genderDataName != wxEmptyString)
	{
		NGender gender = m_propertyGrid->GetGender();
		if (gender == ngUnspecified)
		{
			NSubject::FaceCollection faces = m_subject.GetFaces();
			for (NSubject::FaceCollection::iterator it = faces.begin(); it != faces.end(); it++)
			{
				NGender g = it->GetObjects()[0].GetGender();
				if (g == ngMale || g == ngFemale)
				{
					m_propertyGrid->SetGender(g);
				}
			}
		}
	}
}

void SubjectPage::UpdateSchemaControls()
{
	SampleDbSchema schema = SettingsManager::GetCurrentSchema();
	if (schema.IsEmpty())
	{
		m_enrollDataSizer->ShowItems(false);
	}
	else
	{
		NPropertyBag properties = NObjectDynamicCast<NPropertyBag>(m_subject.GetProperties().Clone());
		properties.Remove(schema.thumbnailDataName);
		properties.Remove(schema.enrollDataName);
		m_thumbnailSizer->ShowItems(schema.thumbnailDataName != wxEmptyString);
		m_propertyGrid->SetSchema(schema);
		m_propertyGrid->SetValues(properties);
		m_propertyGrid->SetIsReadOnly(false);
		m_propertyGrid->SetShowBlobs(true);
		m_subject.GetProperties().Clear();
	}
}

void SubjectPage::GetThumbnail()
{
	SampleDbSchema schema = SettingsManager::GetCurrentSchema();
	if (!schema.IsEmpty())
	{
		bool hasThumbnail = schema.thumbnailDataName != wxEmptyString;
		if (hasThumbnail && m_thumbnail.IsNull())
		{
			if (m_subject.GetProperties().Contains(schema.thumbnailDataName))
			{
				m_thumbnail = NImage::FromMemory(m_subject.GetProperty<NBuffer>(schema.thumbnailDataName));
			}
			else
			{
				NSubject::FaceCollection faces = m_subject.GetFaces();
				for (NSubject::FaceCollection::iterator it = faces.begin(); it != faces.end(); it++)
				{
					NLAttributes attributes = it->GetObjects()[0];
					m_thumbnail = attributes.GetThumbnail();
					if (!m_thumbnail.IsNull()) break;
				}
			}
			if (!m_thumbnail.IsNull())
			{
				m_pictureBox->SetImage(m_thumbnail);
			}
		}
	}
}

void SubjectPage::SerializeEnrollData(void * param)
{
	SubjectPage * page = static_cast<SubjectPage*>(param);
	if (page)
	{
		SampleDbSchema schema = SettingsManager::GetCurrentSchema();
		NBuffer buffer = EnrollDataSerializer::Serialize(page->m_subject, LicensingTools::IsComponentActivated(wxT("Images.WSQ")));
		page->m_subject.SetProperty(schema.enrollDataName, buffer);
	}
}

void SubjectPage::SetSubjectProperties()
{
	SampleDbSchema schema = SettingsManager::GetCurrentSchema();
	if (!schema.IsEmpty())
	{
		m_subject.GetProperties().Clear();
		if (!m_thumbnail.IsNull())
		{
			m_subject.SetProperty(schema.thumbnailDataName, m_thumbnail.Save(NImageFormat::GetPng()));
		}
		if (schema.enrollDataName != wxEmptyString)
		{
			LongActionDialog dialog(this, wxID_ANY, wxT("Serializing subject data"));
			dialog.SetActionCallback(&SubjectPage::SerializeEnrollData, this);
			dialog.ShowModal();
		}
		m_propertyGrid->ApplyTo(m_subject);
	}
}

bool SubjectPage::IsSubjectEmpty()
{
	return (m_subject.GetFingers().GetCount() +
		m_subject.GetFaces().GetCount() +
		m_subject.GetIrises().GetCount() +
		m_subject.GetVoices().GetCount() +
		m_subject.GetPalms().GetCount()) == 0;
}

void SubjectPage::Reset()
{
	GetThumbnail();
}

void SubjectPage::DisableActions()
{
	m_btnEnroll->Enable(false);
	m_btnEnrollWithDuplicateCheck->Enable(false);
	m_btnIdentify->Enable(false);
	m_btnVerify->Enable(false);
	m_btnSaveTemplate->Enable(false);
	m_btnUpdate->Enable(false);
}

void SubjectPage::EnableActions()
{
	m_btnEnroll->Enable(true);
	m_btnEnrollWithDuplicateCheck->Enable(true);
	m_btnIdentify->Enable(true);
	m_btnVerify->Enable(true);
	m_btnSaveTemplate->Enable(true);
	m_btnUpdate->Enable(true);
}

void SubjectPage::SetCallbacks()
{
	m_subject.GetFingers().AddCollectionChangedCallback(&SubjectPage::FingerCollectionChangedCallback, this);
	m_subject.GetFaces().AddCollectionChangedCallback(&SubjectPage::FaceCollectionChangedCallback, this);
	m_subject.GetIrises().AddCollectionChangedCallback(&SubjectPage::IrisCollectionChangedCallback, this);
	m_subject.GetPalms().AddCollectionChangedCallback(&SubjectPage::PalmCollectionChangedCallback, this);
	m_subject.GetVoices().AddCollectionChangedCallback(&SubjectPage::VoiceCollectionChangedCallback, this);
}

void SubjectPage::UnsetCallbacks()
{
	m_subject.GetFingers().RemoveCollectionChangedCallback(&SubjectPage::FingerCollectionChangedCallback, this);
	m_subject.GetFaces().RemoveCollectionChangedCallback(&SubjectPage::FaceCollectionChangedCallback, this);
	m_subject.GetIrises().RemoveCollectionChangedCallback(&SubjectPage::IrisCollectionChangedCallback, this);
	m_subject.GetPalms().RemoveCollectionChangedCallback(&SubjectPage::PalmCollectionChangedCallback, this);
	m_subject.GetVoices().RemoveCollectionChangedCallback(&SubjectPage::VoiceCollectionChangedCallback, this);
}

void SubjectPage::BiometricCollectionChangedCallback(void *pParam)
{
	SubjectPage *subjectPage = reinterpret_cast<SubjectPage *>(pParam);
	wxCommandEvent event(wxEVT_SUBJECT_PAGE_THREAD, ID_UPDATE_STATUS);
	wxPostEvent(subjectPage, event);
}

void SubjectPage::FingerCollectionChangedCallback(Collections::CollectionChangedEventArgs<NFinger> args)
{
	BiometricCollectionChangedCallback(args.GetParam());
}

void SubjectPage::FaceCollectionChangedCallback(Collections::CollectionChangedEventArgs<NFace> args)
{
	BiometricCollectionChangedCallback(args.GetParam());
}

void SubjectPage::IrisCollectionChangedCallback(Collections::CollectionChangedEventArgs<NIris> args)
{
	BiometricCollectionChangedCallback(args.GetParam());
}

void SubjectPage::PalmCollectionChangedCallback(Collections::CollectionChangedEventArgs<NPalm> args)
{
	BiometricCollectionChangedCallback(args.GetParam());
}

void SubjectPage::VoiceCollectionChangedCallback(Collections::CollectionChangedEventArgs<NVoice> args)
{
	BiometricCollectionChangedCallback(args.GetParam());
}

void SubjectPage::CreateGUIControls()
{
	wxGridBagSizer * gbSizer = new wxGridBagSizer();
	gbSizer->SetFlexibleDirection(wxBOTH);
	gbSizer->SetNonFlexibleGrowMode(wxFLEX_GROWMODE_SPECIFIED);

	wxStaticText * staticText = new wxStaticText(this, wxID_ANY, wxT("Subject id:"));
	staticText->Wrap(-1);
	gbSizer->Add(staticText, wxGBPosition(0, 0), wxGBSpan(1, 1), wxALL | wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL, 5);

	m_txtSubjectId = new wxTextCtrl(this, wxID_ANY);
	gbSizer->Add(m_txtSubjectId, wxGBPosition(0, 1), wxGBSpan(1, 2), wxALL|wxEXPAND, 5);

	m_btnIdentify = new wxButton(this, ID_BUTTON_IDENTIFY, wxT("Identify"));
	gbSizer->Add(m_btnIdentify, wxGBPosition(1, 0), wxGBSpan(1, 1), wxALL|wxEXPAND, 5);

	staticText = new wxStaticText(this, wxID_ANY, wxT("Query:"));
	staticText->Wrap(-1);
	gbSizer->Add(staticText, wxGBPosition(1, 1), wxGBSpan(1, 1), wxALL|wxEXPAND|wxALIGN_CENTER, 5);

	m_txtQuery = new wxTextCtrl(this, wxID_ANY);
	m_txtQuery->SetToolTip( wxT("Identification operation can use query so that only subjects with matching biographic data would be used for biometric identification.")
							wxT("Example: Country='Germany' AND NOT (City='Berlin' OR City='Hamburg')"));
	gbSizer->Add(m_txtQuery, wxGBPosition(1, 2), wxGBSpan(1, 1), wxALL|wxEXPAND, 5);

	m_btnVerify = new wxButton(this, ID_BUTTON_VERIFY, wxT("Verify"));
	gbSizer->Add(m_btnVerify, wxGBPosition(2, 0), wxGBSpan(1, 1), wxALL|wxEXPAND, 5);

	m_btnEnroll= new wxButton( this, ID_BUTTON_ENROLL, wxT("Enroll"));
	gbSizer->Add(m_btnEnroll, wxGBPosition(3, 0), wxGBSpan(1, 1), wxALL|wxEXPAND, 5);

	m_btnEnrollWithDuplicateCheck = new wxButton(this, ID_BUTTON_ENROLL_WITH_DUPLICATE_CHECK, wxT("Enroll with \nduplicate check"), wxDefaultPosition, wxSize( 118,36 ));
	gbSizer->Add(m_btnEnrollWithDuplicateCheck, wxGBPosition(4, 0), wxGBSpan(1, 1), wxALL|wxEXPAND, 5);

	m_btnUpdate = new wxButton(this, ID_BUTTON_UPDATE, wxT("Update"));
	gbSizer->Add(m_btnUpdate, wxGBPosition(5, 0), wxGBSpan(1, 1), wxALL|wxEXPAND, 5);

	m_btnSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, wxT("Save template"));
	m_btnSaveTemplate->SetBitmap(wxImage(saveIcon_xpm));
	gbSizer->Add(m_btnSaveTemplate, wxGBPosition(6, 0), wxGBSpan(1, 1), wxALL, 5);

	m_statusPanel = new StatusPanel(this, wxID_ANY);
	m_statusPanel->SetMessage(wxT("Hint"));
	gbSizer->Add(m_statusPanel, wxGBPosition(8, 0), wxGBSpan(1, 3), wxALL|wxEXPAND, 5);

	m_enrollDataSizer = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Enroll data")), wxHORIZONTAL );
	m_thumbnailSizer = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Thumnail")), wxVERTICAL );

	m_btnOpenImage = new wxButton( this, ID_BUTTON_OPEN_IMAGE, wxT("Open image"));
	m_btnOpenImage->SetBitmap(wxImage(openFolderIcon_xpm));
	m_thumbnailSizer->Add(m_btnOpenImage, 0, wxALL, 5);

	m_pictureBox = new ImageView(this, wxID_ANY);
	m_pictureBox->SetMinSize(wxSize(250, 250));
	m_thumbnailSizer->Add(m_pictureBox, 1, wxALL|wxEXPAND, 5);

	m_enrollDataSizer->Add(m_thumbnailSizer, 0, wxEXPAND, 5);

	m_propertyGrid = new SchemaPropertyGrid(this, wxID_ANY);
	m_enrollDataSizer->Add(m_propertyGrid, 1, wxALL|wxEXPAND, 5);

	gbSizer->Add(m_enrollDataSizer, wxGBPosition(2, 1), wxGBSpan(5, 2), wxEXPAND, 5);

	gbSizer->AddGrowableCol(2, 1);
	gbSizer->AddGrowableRow(7, 1);

	this->SetSizer(gbSizer);
	this->Layout();
}

}}

