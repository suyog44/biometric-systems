#include "Precompiled.h"
#include "Resources/OpenFolderIcon.xpm"
#include "DetectFaces.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Licensing;
using namespace Neurotec::Images;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(EVT_DETECFACESPAGE_DETECTFACES_COMPLETED);
		BEGIN_EVENT_TABLE(DetectFaces, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPENIMAGE, DetectFaces::OnButtonOpenImageClick)
			EVT_BUTTON(ID_BUTTON_DETECTFEATURES, DetectFaces::OnButtonDetectFacialFeaturesClick)
			EVT_COMMAND(wxID_ANY, EVT_DETECFACESPAGE_DETECTFACES_COMPLETED, DetectFaces::OnDetectFacesCompleted)
		END_EVENT_TABLE()

		DetectFaces::DetectFaces(wxWindow *parent, NBiometricClient &biometricClient) : wxPanel(parent), m_biometricClient(biometricClient), m_faceImage(NULL)
		{
			CreateGUIControls();
			m_isSegmentationActivated = NLicense::IsComponentActivated("Biometrics.FaceSegmentsDetection");
			InitializeBiometricParams();
		}

		void DetectFaces::CreateGUIControls()
		{
			wxBoxSizer* mainBox = new wxBoxSizer(wxVERTICAL);

			LicensePanel *licensePanel = new LicensePanel(this);
			licensePanel->RefreshComponentsStatus("Biometrics.FaceDetection", "Biometrics.FaceSegmentsDetection");
			mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

			wxBoxSizer *toolbarSizer = new wxBoxSizer(wxHORIZONTAL);
			m_buttonOpenImage = new wxButton(this, ID_BUTTON_OPENIMAGE, "&Open Image");
			m_buttonOpenImage->SetToolTip("open image...");
			m_buttonOpenImage->SetBitmap(openFolderIcon_xpm);
			toolbarSizer->Add(m_buttonOpenImage, 0, wxALIGN_CENTER | wxALL, 2);
			wxStaticText *staticTextRollAngle = new wxStaticText(this, wxID_ANY, "Max roll angle deviation: ");
			toolbarSizer->Add(staticTextRollAngle, 0, wxALIGN_CENTER | wxALL, 2);
			m_comboBoxRollAngle = new wxComboBox(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, 0, wxCB_READONLY);
			toolbarSizer->Add(m_comboBoxRollAngle, 0, wxALIGN_CENTER | wxALL, 2);
			wxStaticText *staticTextYawAngle = new wxStaticText(this, wxID_ANY, "Max yaw angle deviation: ");
			toolbarSizer->Add(staticTextYawAngle, 0, wxALIGN_CENTER | wxALL, 2);
			m_comboBoxYawAngle = new wxComboBox(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0, 0, wxCB_READONLY);
			toolbarSizer->Add(m_comboBoxYawAngle, 0, wxALIGN_CENTER | wxALL, 2);
			m_buttonDetectFacialFeatures = new wxButton(this, ID_BUTTON_DETECTFEATURES, "Detect Facial Features");
			m_buttonDetectFacialFeatures->SetFont(wxFont(8, wxFONTFAMILY_DEFAULT, wxFONTSTYLE_NORMAL, wxFONTWEIGHT_BOLD));
			m_buttonDetectFacialFeatures->SetToolTip("Detect Facial Features");
			m_buttonDetectFacialFeatures->Enable(false);
			toolbarSizer->Add(m_buttonDetectFacialFeatures, 0, wxALIGN_CENTER | wxALL, 2);
			mainBox->Add(toolbarSizer, 0, wxALL | wxEXPAND, 2);

			wxNViewZoomSlider* zoomslider = new wxNViewZoomSlider(this);
			m_faceView = new wxNFaceView(this);
			zoomslider->SetView(m_faceView);
			m_faceView->SetBackgroundColour(GetBackgroundColour());
			m_faceView->SetShowBaseFeaturePoints(true);
			m_faceView->SetShowEmotions(true);
			m_faceView->SetShowGender(true);
			m_faceView->SetShowExpression(true);
			mainBox->Add(m_faceView, 1, wxEXPAND | wxALL, 2);
			mainBox->Add(zoomslider, 0, wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainBox);
		}

		void DetectFaces::OnButtonOpenImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog wxfdOpenFaceImage(this, "Open Face Image", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (wxfdOpenFaceImage.ShowModal() == wxID_OK)
			{
				m_faceView->SetFace(NULL);
				m_faceImage = NULL;
				try
				{
					m_faceImage = NImage::FromFile(wxfdOpenFaceImage.GetPath());
					DetectFace();
				}
				catch (NError& e)
				{
					wxExceptionDlg::Show(e);
					m_buttonDetectFacialFeatures->Enable(false);
				}
			}
		}

		void DetectFaces::DetectFace()
		{
			SetBiometricClientParams();
			NAsyncOperation operation = m_biometricClient.DetectFacesAsync(m_faceImage);
			operation.AddCompletedCallback(&DetectFaces::OnDetectFacesCompletedCallback, this);
		}

		void DetectFaces::OnDetectFacesCompletedCallback(EventArgs args)
		{
			DetectFaces * page = reinterpret_cast<DetectFaces *>(args.GetParam());
			wxCommandEvent event(EVT_DETECFACESPAGE_DETECTFACES_COMPLETED);
			event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(page, event);
		}

		void DetectFaces::OnDetectFacesCompleted(wxCommandEvent& event)
		{
			try
			{
				NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
				NValue result = operation.GetResult();
				NFace face = result.ToObject<NFace>();
				m_faceView->SetFace(face);
			}
			catch (NError & error)
			{
				wxExceptionDlg::Show(error);
			}
			m_buttonDetectFacialFeatures->Enable(true);
		}

		void DetectFaces::OnButtonDetectFacialFeaturesClick(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonDetectFacialFeatures->Enable(false);
			DetectFace();
		}

		void DetectFaces::InitializeBiometricParams()
		{
			try
			{
				LoadAngleCmb(180, m_biometricClient.GetFacesMaximalRoll(), m_comboBoxRollAngle);
				LoadAngleCmb(90, m_biometricClient.GetFacesMaximalYaw(), m_comboBoxYawAngle);
			}
			catch (NError& e)
			{
				wxExceptionDlg::Show(e);
			}
		}

		void DetectFaces::LoadAngleCmb(int maxValue, float selectedItem, wxComboBox *comboBox)
		{
			comboBox->Clear();
			float previousValue = -180;
			int selectedIndex = -1;
			for (float i = 15; i <= maxValue; i += 15)
			{
				if (selectedItem <= i && selectedItem > previousValue)
				{
					comboBox->Append(wxString::Format("%g", selectedItem));
					selectedIndex = i / 15 - 1;
				}
				if (selectedItem != i)
					comboBox->Append(wxString::Format("%g", i));
				previousValue = i;
			}
			comboBox->SetSelection(selectedIndex);
		}

		void DetectFaces::SetBiometricClientParams()
		{
			m_biometricClient.SetFacesMaximalRoll(wxAtof(m_comboBoxRollAngle->GetStringSelection()));
			m_biometricClient.SetFacesMaximalYaw(wxAtof(m_comboBoxYawAngle->GetStringSelection()));
			m_biometricClient.SetFacesDetectAllFeaturePoints(m_isSegmentationActivated);
			m_biometricClient.SetFacesDetectBaseFeaturePoints(m_isSegmentationActivated);
		}
	}
}
