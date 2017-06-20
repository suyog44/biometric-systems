#include "Precompiled.h"
#include "Resources/OpenFolderIcon.xpm"
#include "Resources/SaveIcon.xpm"
#include "GeneralizeFinger.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::IO;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_GENARALIZEPAGE_GENERALIZE_COMPLETE)
		BEGIN_EVENT_TABLE(GeneralizeFinger, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGES, GeneralizeFinger::OnButtonOpenImagesClick)
			EVT_CHECKBOX(ID_CHECKBOX_SHOW_BINARIZED_IMAGE, GeneralizeFinger::OnCheckBoxShowBinarizedImageClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, GeneralizeFinger::OnButtonSaveTemplateClick)
			EVT_COMMAND(wxID_ANY, wxEVT_GENARALIZEPAGE_GENERALIZE_COMPLETE, GeneralizeFinger::OnGeneralizeCompleted)
		END_EVENT_TABLE()

		GeneralizeFinger::GeneralizeFinger(wxWindow *parent, const NBiometricClient & biometricClient, wxWindowID id, const wxPoint & pos, const wxSize & size, long style, const wxString & name)
			: wxPanel(parent, id, pos, size, style, name), m_biometricClient(biometricClient), m_subject(NULL)
		{
			CreateGUIControls();
		}

		GeneralizeFinger::~GeneralizeFinger()
		{
		}

		void GeneralizeFinger::CreateGUIControls()
		{
			wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
			wxString licences = "Biometrics.FingerExtraction";
			wxString licencesOptional = "Images.WSQ";
			LicensePanel *licencePanel;
			licencePanel = new LicensePanel(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxBORDER_SIMPLE, wxEmptyString);
			licencePanel->RefreshComponentsStatus(licences, licencesOptional);
			mainSizer->Add(licencePanel, 0, wxALL | wxEXPAND, 2);

			wxStaticBoxSizer *staticBoxSizerLoadFingers = new wxStaticBoxSizer(wxHORIZONTAL, this, "Load finger images(Min 3,Max 10)");
			m_buttonOpenImages = new wxBitmapButton(this, ID_BUTTON_OPEN_IMAGES, wxImage(openFolderIcon_xpm));
			m_buttonOpenImages->SetToolTip("Open 3-10 fingerprint images to perform generalizing and extraction");
			m_staticTextNoOfImages = new wxStaticText(this, wxID_ANY, " Images Loaded: 0");
			staticBoxSizerLoadFingers->Add(m_buttonOpenImages, 0, wxALL, 2);
			staticBoxSizerLoadFingers->Add(m_staticTextNoOfImages, 0, wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(staticBoxSizerLoadFingers, 0, wxALL | wxEXPAND, 2);

			wxNViewZoomSlider *fingerViewZoomSlider = new wxNViewZoomSlider(this);
			m_fingerView = new wxNFingerView(this, wxID_ANY);
			fingerViewZoomSlider->SetView(m_fingerView);
			mainSizer->Add(m_fingerView, 1, wxEXPAND | wxALL, 2);

			wxBoxSizer *boxSizerActions = new wxBoxSizer(wxHORIZONTAL);
			m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "Save Template", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveTemplate->SetBitmap(wxImage(saveIcon_xpm));
			m_buttonSaveTemplate->SetToolTip("Save extracted template to a file");
			m_buttonSaveTemplate->Enable(false);
			boxSizerActions->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);

			m_checkBoxShowBinarizedImage = new wxCheckBox(this, ID_CHECKBOX_SHOW_BINARIZED_IMAGE, "Show binarized image");
			m_checkBoxShowBinarizedImage->Enable(false);
			m_checkBoxShowBinarizedImage->SetValue(true);
			m_fingerView->SetShownImage(wxNFrictionRidgeView::PROCESSED_IMAGE);

			boxSizerActions->Add(m_checkBoxShowBinarizedImage, 0, wxALIGN_CENTER | wxALL, 2);
			m_staticTextStatus = new wxStaticText(this, wxID_ANY, wxEmptyString);
			boxSizerActions->Add(m_staticTextStatus, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerActions->AddStretchSpacer();
			boxSizerActions->Add(fingerViewZoomSlider, 0, wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(boxSizerActions, 0, wxEXPAND | wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainSizer);
		}

		void GeneralizeFinger::OnButtonOpenImagesClick(wxCommandEvent& WXUNUSED(event))
		{
			m_staticTextNoOfImages->SetLabel("Images loaded: 0");
			m_subject = NULL;
			m_fingerView->SetFinger(NULL);
			m_staticTextStatus->SetLabel(wxEmptyString);
			m_checkBoxShowBinarizedImage->Enable(false);
			wxFileDialog openFileDialog(this, "Choose fingers images", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST | wxFD_MULTIPLE);
			if (openFileDialog.ShowModal() == wxID_OK)
			{
				wxArrayString filePaths;
				openFileDialog.GetPaths(filePaths);
				int fingerCount = filePaths.GetCount();
				m_buttonSaveTemplate->Enable(false);
				if (fingerCount >= 3 && fingerCount <= 10)
				{
					m_subject = NSubject();
					for (int i = 0; i < fingerCount; i++)
					{
						NFinger finger;
						finger.SetFileName(filePaths[i]);
						finger.SetSessionId(1); // all fingers with same session will be generalized
						m_subject.GetFingers().Add(finger);
					}
					NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_subject);
					operation.AddCompletedCallback(&GeneralizeFinger::OnGeneralizeCompletedCallback, this);
					m_staticTextStatus->SetForegroundColour(wxSystemSettings::GetColour(wxSYS_COLOUR_MENUTEXT));
					m_staticTextStatus->SetLabel("Status: performing extraction and generalization");
					m_staticTextNoOfImages->SetLabel(wxString::Format("Images loaded: %d", fingerCount));
					m_buttonOpenImages->Enable(false);
				}
				else
				{
					if (fingerCount < 3)
					{
						wxMessageBox(wxString::Format("Too few images selected.Please select at least 3 and no more than 10 images"));
					}
					else
					{
						wxMessageBox(wxString::Format("Too many images selected.Please select at least 3 and no more than 10 images"));
					}
				}
			}
		}

		void GeneralizeFinger::OnGeneralizeCompletedCallback(const EventArgs & args)
		{
			GeneralizeFinger *panel = reinterpret_cast<GeneralizeFinger*>(args.GetParam());
			wxCommandEvent ev(wxEVT_GENARALIZEPAGE_GENERALIZE_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void GeneralizeFinger::OnGeneralizeCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subject.GetStatus();
				if (status == nbsOk)
				{
					m_staticTextStatus->SetForegroundColour(wxColour(0, 155, 0));
					m_staticTextStatus->SetLabel(" Status: Ok");
					m_fingerView->SetFinger(m_subject.GetFingers().Get(m_subject.GetFingers().GetCount() - 1));
					m_buttonSaveTemplate->Enable(true);
					m_checkBoxShowBinarizedImage->Enable(true);
				}
				else
				{
					m_staticTextStatus->SetForegroundColour(wxColour(155, 0, 0));
					m_staticTextStatus->SetLabel("Status: " + NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status));
				}
			}
			else
			{
				m_staticTextStatus->SetForegroundColour(wxColour(155, 0, 0));
				m_staticTextStatus->SetLabel("Status: error occurred");
				wxExceptionDlg::Show(operation.GetError());
			}
			m_buttonOpenImages->Enable(true);
		}

		void GeneralizeFinger::OnCheckBoxShowBinarizedImageClick(wxCommandEvent& WXUNUSED(event))
		{
			m_fingerView->SetShownImage(m_checkBoxShowBinarizedImage->IsChecked() ? wxNFrictionRidgeView::PROCESSED_IMAGE : wxNFrictionRidgeView::ORIGINAL_IMAGE);
		}

		void GeneralizeFinger::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog saveDialog(this, "Save File", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			try
			{
				if (saveDialog.ShowModal() == wxID_OK)
				{
					NFile::WriteAllBytes(saveDialog.GetPath(), m_subject.GetTemplateBuffer());
				}
			}
			catch (NError& ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}
	}
}
