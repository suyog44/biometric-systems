#include "Precompiled.h"
#include "Resources/OpenFolderIcon.xpm"
#include "Resources/SaveIcon.xpm"
#include "EnrollFromImage.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::IO;
using namespace Neurotec::Images;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(EVT_ENROLLFROMIMAGE_CREATETEMPLATECOMPLETED);
		BEGIN_EVENT_TABLE(EnrollFromImage, wxPanel)
			EVT_COMMAND(wxID_ANY, EVT_ENROLLFROMIMAGE_CREATETEMPLATECOMPLETED, EnrollFromImage::OnCreateTemplateCompleted)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, EnrollFromImage::OnButtonOpenImageClick)
			EVT_BUTTON(ID_BUTTON_EXTRACT, EnrollFromImage::OnButtonExtractClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, EnrollFromImage::OnButtonSaveTemplateClick)
		END_EVENT_TABLE()

		EnrollFromImage::EnrollFromImage(wxWindow *parent, NBiometricClient &biometricClient)
		: wxPanel(parent,wxID_ANY), m_biometricClient(biometricClient), m_subject(NULL)
		{
			CreateGUIControls();
			m_isSegmentationActivated = NLicense::IsComponentActivated("Biometrics.FaceSegmentsDetection");
			InitializeBiometricParams();
		}

		void EnrollFromImage::CreateGUIControls()
		{
			wxBoxSizer* mainBox = new wxBoxSizer(wxVERTICAL);
			LicensePanel *licensePanel = new LicensePanel(this);
			licensePanel->RefreshComponentsStatus("Biometrics.FaceExtraction", "Biometrics.FaceSegmentsDetection");
			mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

			wxBoxSizer *toolbarSizer = new wxBoxSizer(wxHORIZONTAL);
			m_buttonOpenImage = new wxButton(this, ID_BUTTON_OPEN_IMAGE, "&Open Image");
			m_buttonOpenImage->SetToolTip("Open image...");
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
			m_buttonExtract = new wxButton(this, ID_BUTTON_EXTRACT, "Extract Template");
			m_buttonExtract->SetFont(wxFont(8, wxFONTFAMILY_DEFAULT, wxFONTSTYLE_NORMAL, wxFONTWEIGHT_BOLD));
			m_buttonExtract->SetToolTip("Extract Template");
			m_buttonExtract->Enable(false);
			toolbarSizer->Add(m_buttonExtract, 0, wxALIGN_CENTER | wxALL, 2);
			mainBox->Add(toolbarSizer, 0, wxALL | wxEXPAND, 2);

			wxNViewZoomSlider *zoomslider = new wxNViewZoomSlider(this);
			m_faceView = new wxNFaceView(this);
			m_faceView->SetBackgroundColour(GetBackgroundColour());
			mainBox->Add(m_faceView, 1, wxEXPAND | wxALL, 2);

			wxBoxSizer* bottomSizer = new wxBoxSizer(wxHORIZONTAL);
			m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "&Save Template");
			m_buttonSaveTemplate->SetBitmap(saveIcon_xpm);
			m_buttonSaveTemplate->SetToolTip("Save extracted template to a file");
			m_buttonSaveTemplate->Enable(false);
			m_staticTextStatus = new wxStaticText(this, wxID_ANY, wxEmptyString);
			zoomslider->SetView(m_faceView);
			bottomSizer->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			bottomSizer->Add(m_staticTextStatus, 0, wxALIGN_CENTER | wxALL, 2);
			bottomSizer->AddStretchSpacer();
			bottomSizer->Add(zoomslider, 0, wxALIGN_CENTER | wxALL, 2);
			mainBox->Add(bottomSizer, 0, wxEXPAND | wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainBox);
		}

		void EnrollFromImage::OnButtonOpenImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog openFileDialog(this, "Open Face Image", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openFileDialog.ShowModal() == wxID_OK)
			{
				m_stringFileName = openFileDialog.GetPath();
				ExtractTemplate(m_stringFileName);
			}
		}

		void EnrollFromImage::ExtractTemplate(const wxString &fileName)
		{
			SetBiometricClientParams();
			m_subject = NSubject();
			NFace face;
			face.SetFileName(fileName);
			m_subject.GetFaces().Add(face);
			m_faceView->SetFace(face);
			NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_subject);
			operation.AddCompletedCallback(&EnrollFromImage::OnCreateTemplateCompletedCallback, this);
			m_buttonExtract->Enable(false);
		}

		void EnrollFromImage::OnCreateTemplateCompletedCallback(EventArgs args)
		{
			EnrollFromImage *panel = reinterpret_cast<EnrollFromImage*>(args.GetParam());
			wxCommandEvent ev(EVT_ENROLLFROMIMAGE_CREATETEMPLATECOMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void EnrollFromImage::OnCreateTemplateCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NValue result = operation.GetResult();
				NBiometricStatus status = (NBiometricStatus)result.ToInt32();
				if (status == nbsOk)
				{
					m_staticTextStatus->SetLabel("Template extracted");
					m_staticTextStatus->SetForegroundColour(wxColour(0, 155, 0));
					m_buttonSaveTemplate->Enable(true);
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					m_staticTextStatus->SetLabel("Extraction failed: " + statusString);
					m_buttonSaveTemplate->Enable(false);
					m_staticTextStatus->SetForegroundColour(wxColour(155, 0, 0));
				}
			}
			else
			{
				m_staticTextStatus->SetLabel("Extraction failed!");
				m_staticTextStatus->SetForegroundColour(wxColour(155, 0, 0));
				m_buttonSaveTemplate->Enable(false);
				wxExceptionDlg::Show(operation.GetError());
			}
			m_buttonExtract->Enable(true);
		}

		void EnrollFromImage::InitializeBiometricParams()
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

		void EnrollFromImage::SetBiometricClientParams()
		{
			m_biometricClient.SetFacesMaximalRoll(wxAtof(m_comboBoxRollAngle->GetStringSelection()));
			m_biometricClient.SetFacesMaximalYaw(wxAtof(m_comboBoxYawAngle->GetStringSelection()));
			m_biometricClient.SetFacesDetectAllFeaturePoints(m_isSegmentationActivated);
			m_biometricClient.SetFacesDetectBaseFeaturePoints(m_isSegmentationActivated);
			m_biometricClient.SetFacesDetermineGender(m_isSegmentationActivated);
			m_biometricClient.SetFacesDetermineAge(m_isSegmentationActivated);
			m_biometricClient.SetFacesDetectProperties(m_isSegmentationActivated);
			m_biometricClient.SetFacesRecognizeEmotion(m_isSegmentationActivated);
			m_biometricClient.SetFacesRecognizeExpression(m_isSegmentationActivated);
		}

		void EnrollFromImage::LoadAngleCmb(int maxValue, float selectedItem, wxComboBox *comboBox)
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

		void EnrollFromImage::OnButtonExtractClick(wxCommandEvent& WXUNUSED(event))
		{
			ExtractTemplate(m_stringFileName);
		}

		void EnrollFromImage::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
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
