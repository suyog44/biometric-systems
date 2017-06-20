#include "Precompiled.h"
#include "Resources/OpenFolderIcon.xpm"
#include "Resources/SaveIcon.xpm"
#include "GeneralizeFaces.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::IO;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(EVT_GENERALIZEFACESPAGE_GENERALIZEFACES_COMPLETED);
		BEGIN_EVENT_TABLE(GeneralizeFaces, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGES, GeneralizeFaces::OnButtonOpenImagesClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, GeneralizeFaces::OnButtonSaveTemplateClick)
			EVT_COMMAND(wxID_ANY, EVT_GENERALIZEFACESPAGE_GENERALIZEFACES_COMPLETED, GeneralizeFaces::OnGeneralizeFacesCompleted)
		END_EVENT_TABLE()

		GeneralizeFaces::GeneralizeFaces(wxWindow *parent, NBiometricClient &biometricClient) : wxPanel(parent), m_biometricClient(biometricClient), m_subject(NULL)
		{
			CreateGUIControls();
		}

		void GeneralizeFaces::CreateGUIControls()
		{
			wxBoxSizer *mainBox = new wxBoxSizer(wxVERTICAL);
			LicensePanel *licensePanel = new LicensePanel(this);
			licensePanel->RefreshComponentsStatus("Biometrics.FaceExtraction",wxEmptyString);
			mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer* staticBoxSizerLoadFaceImages = new wxStaticBoxSizer(wxHORIZONTAL, this, "Load face images");
			m_buttonOpenImages = new wxBitmapButton(this, ID_BUTTON_OPEN_IMAGES, openFolderIcon_xpm);
			m_buttonOpenImages->SetToolTip("Open face files for generalization");
			staticBoxSizerLoadFaceImages->Add(m_buttonOpenImages, 0, wxALIGN_CENTER | wxALL, 2);
			wxStaticText *staticTextImagesLoaded = new wxStaticText(this, wxID_ANY, "Images loaded:  ");
			staticBoxSizerLoadFaceImages->Add(staticTextImagesLoaded, 0, wxALIGN_CENTER | wxALL, 2);
			m_staticTextImageCount = new wxStaticText(this, wxID_ANY, "0");
			staticBoxSizerLoadFaceImages->Add(m_staticTextImageCount, 0, wxALIGN_CENTER | wxALL, 2);
			mainBox->Add(staticBoxSizerLoadFaceImages, 0, wxEXPAND | wxALL, 2);

			wxNViewZoomSlider *zoomSlider = new wxNViewZoomSlider(this);
			m_faceView = new wxNFaceView(this);
			zoomSlider->SetView(m_faceView);
			m_faceView->SetBackgroundColour(GetBackgroundColour());
			mainBox->Add(m_faceView, 1, wxEXPAND | wxALL, 2);

			wxBoxSizer *bottomSizer = new wxBoxSizer(wxHORIZONTAL);
			m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "Save Template");
			m_buttonSaveTemplate->SetBitmap(saveIcon_xpm);
			m_buttonSaveTemplate->SetToolTip("Save extracted template to a file");
			m_buttonSaveTemplate->Enable(false);
			m_staticTextStatus = new wxStaticText(this, wxID_ANY, wxEmptyString);
			bottomSizer->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			bottomSizer->Add(m_staticTextStatus, 0, wxALIGN_CENTER | wxALL, 2);
			bottomSizer->AddStretchSpacer();
			bottomSizer->Add(zoomSlider, 0, wxALIGN_CENTER | wxALL, 2);

			mainBox->Add(bottomSizer, 0, wxEXPAND | wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainBox);
		}

		void GeneralizeFaces::OnButtonOpenImagesClick(wxCommandEvent& WXUNUSED(event))
		{
			m_biometricClient.Clear();
			wxFileDialog wxfdOpenFile(this, "Select multiple images for generalization", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_MULTIPLE | wxFD_FILE_MUST_EXIST);
			m_buttonSaveTemplate->Enable(false);
			if (wxfdOpenFile.ShowModal() == wxID_OK)
			{
				m_subject = NSubject();
				wxArrayString imageFiles;
				wxfdOpenFile.GetPaths(imageFiles);
				for (size_t i = 0; i < imageFiles.GetCount(); i++)
				{
					NFace face;
					face.SetFileName(imageFiles[i]);
					face.SetSessionId(1); // all faces with same session will be generalized
					m_subject.GetFaces().Add(face);
				}
				m_faceView->SetFace(NULL);
				m_staticTextImageCount->SetLabel(wxString::Format("%i", imageFiles.GetCount()));
				m_staticTextStatus->SetLabel("Status: performing extraction and generalization");
				NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_subject);
				operation.AddCompletedCallback(&GeneralizeFaces::OnGeneralizeFacesCompletedCallback, this);
				m_buttonOpenImages->Enable(false);
			}
		}

		void GeneralizeFaces::OnGeneralizeFacesCompletedCallback(EventArgs args)
		{
			GeneralizeFaces* panel = reinterpret_cast<GeneralizeFaces*>(args.GetParam());
			wxCommandEvent event(EVT_GENERALIZEFACESPAGE_GENERALIZEFACES_COMPLETED);
			event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, event);
		}

		void GeneralizeFaces::OnGeneralizeFacesCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subject.GetStatus();
				if (status == nbsOk)
				{
					m_staticTextStatus->SetLabel("Status: OK");
					m_staticTextStatus->SetForegroundColour(wxColour(0, 155, 0));
					m_faceView->SetFace(m_subject.GetFaces().Get(m_subject.GetFaces().GetCount() - 1));
					m_buttonSaveTemplate->Enable(true);
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					m_staticTextStatus->SetLabel(wxString::Format("Status: %s", statusString));
					m_staticTextStatus->SetForegroundColour(wxColour(155, 0, 0));
				}
			}
			else 
			{
				m_staticTextStatus->SetLabel("Status: error occurred");
				m_staticTextStatus->SetForegroundColour(wxColour(155, 0, 0));
				wxExceptionDlg::Show(operation.GetError());
			}
			m_buttonOpenImages->Enable(true);
		}

		void GeneralizeFaces::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
		{
			if (!m_subject.IsNull())
			{
				wxFileDialog saveFileDialog(this, "Save Template", wxEmptyString, wxEmptyString, wxFileSelectorDefaultWildcardStr, wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
				if (saveFileDialog.ShowModal() == wxID_OK)
				{
					try
					{
						NFile::WriteAllBytes(saveFileDialog.GetPath(), m_subject.GetTemplateBuffer());
					}
					catch (NError& e)
					{
						wxExceptionDlg::Show(e);
					}
				}
			}
		}
	}
}
