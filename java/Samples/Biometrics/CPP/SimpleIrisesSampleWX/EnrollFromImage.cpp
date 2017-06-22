#include "Precompiled.h"
#include "EnrollFromImage.h"
#include "Resources/OpenFolderIcon.xpm"
#include "Resources/SaveIcon.xpm"

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::IO;

#define ENROLL_FROM_IMAGE_REQUIRED_LICENSE_COMPONENTS "Biometrics.IrisExtraction"

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_ENROLLFROMIMAGEPAGE_ENROLL_COMPLETE);
		BEGIN_EVENT_TABLE(EnrollFromImage, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, EnrollFromImage::OnButtonOpenImageClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, EnrollFromImage::OnButtonSaveTemplateClick)
			EVT_COMMAND(wxID_ANY, wxEVT_ENROLLFROMIMAGEPAGE_ENROLL_COMPLETE, EnrollFromImage::OnCreateTemplateCompleted)
		END_EVENT_TABLE()

		EnrollFromImage::EnrollFromImage(wxWindow *parent, const NBiometricClient & biometricClient)
			: wxPanel(parent), m_biometricClient(biometricClient), m_subject(NULL)
		{
			CreateGUIControls();
		}

		EnrollFromImage::~EnrollFromImage()
		{
		}

		void EnrollFromImage::CreateGUIControls()
		{
			wxBoxSizer *boxSizerMain = new wxBoxSizer(wxVERTICAL);

			LicensePanel* licencePanel = new LicensePanel(this);
			licencePanel->RefreshComponentsStatus(ENROLL_FROM_IMAGE_REQUIRED_LICENSE_COMPONENTS, wxEmptyString);
			boxSizerMain->Add(licencePanel, 0, wxALL | wxEXPAND, 2);

			m_buttonOpenImage = new wxButton(this, ID_BUTTON_OPEN_IMAGE, "Open Image", wxDefaultPosition, wxSize(-1, 25));
			m_buttonOpenImage->SetBitmap(wxImage(openFolderIcon_xpm));
			m_buttonOpenImage->SetToolTip("Open iris image");
			boxSizerMain->Add(m_buttonOpenImage, 0, wxALL, 2);

			wxBoxSizer *boxSizerIrisView = new wxBoxSizer(wxHORIZONTAL);
			m_zoomSlider = new wxNViewZoomSlider(this);
			m_irisView = new wxNIrisView(this);
			boxSizerIrisView->Add(m_irisView, 1, wxEXPAND | wxALL, 2);

			m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "Save Template", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveTemplate->SetBitmap(wxImage(saveIcon_xpm));
			m_buttonSaveTemplate->SetToolTip("Save extracted template to a file");
			m_buttonSaveTemplate->Enable(false);

			wxBoxSizer *boxSizerSaveButton = new wxBoxSizer(wxHORIZONTAL);
			boxSizerSaveButton->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerSaveButton->AddStretchSpacer();
			m_staticTxtQuality = new wxStaticText(this, wxID_ANY, wxEmptyString);
			boxSizerSaveButton->Add(m_staticTxtQuality, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerSaveButton->AddStretchSpacer();
			m_zoomSlider->SetView(m_irisView);
			boxSizerSaveButton->Add(m_zoomSlider, 0, wxALIGN_CENTER | wxALL, 2);

			boxSizerMain->Add(boxSizerIrisView, 1, wxEXPAND | wxALL, 2);
			boxSizerMain->Add(boxSizerSaveButton, 0, wxEXPAND | wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());

			SetSizerAndFit(boxSizerMain);
		}

		void EnrollFromImage::OnButtonOpenImageClick(wxCommandEvent& WXUNUSED(event))
		{
			m_staticTxtQuality->SetLabel(wxEmptyString);
			wxFileDialog openFileDialog(this, "Open", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openFileDialog.ShowModal() == wxID_OK)
			{
				m_buttonSaveTemplate->Enable(false);
				NIris iris;
				iris.SetFileName(openFileDialog.GetPath());
				m_subject = NSubject();
				m_subject.GetIrises().Add(iris);
				m_irisView->SetIris(iris);
				NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_subject);
				operation.AddCompletedCallback(&EnrollFromImage::OnCreateTemplateCompletedCallback, this);
			}
		}

		void EnrollFromImage::OnCreateTemplateCompletedCallback(const EventArgs & args)
		{
			EnrollFromImage *enrollFrmImgPnl = reinterpret_cast<EnrollFromImage *>(args.GetParam());
			wxCommandEvent ev(wxEVT_ENROLLFROMIMAGEPAGE_ENROLL_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(enrollFrmImgPnl, ev);
		}

		void EnrollFromImage::OnCreateTemplateCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = (NBiometricStatus)operation.GetResult().ToInt32();
				if (status == nbsOk)
				{
					m_buttonSaveTemplate->Enable(true);
					m_staticTxtQuality->SetLabel(wxString::Format("Quality : %d", m_subject.GetIrises()[0].GetObjects()[0].GetQuality()));
				}
				else
				{
					wxMessageBox("Iris image quality is too low.");
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void EnrollFromImage::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog saveDialog(this, "Save File", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			if (saveDialog.ShowModal() == wxID_OK)
			{
				try
				{
					Neurotec::IO::NFile::WriteAllBytes(saveDialog.GetPath(), m_subject.GetTemplateBuffer());
				}
				catch (NError & ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}
	}
}
