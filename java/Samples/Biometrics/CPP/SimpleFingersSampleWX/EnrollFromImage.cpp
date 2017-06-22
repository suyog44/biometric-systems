#include "Precompiled.h"
#include "Resources/SaveIcon.xpm"
#include "Resources/OpenFolderIcon.xpm"
#include "EnrollFromImage.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Images;
using namespace Neurotec::Gui;
using namespace Neurotec::IO;
using namespace Neurotec::Biometrics::Gui;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_ENROLLFROMIMAGEPAGE_EXTRACTION_COMPLETE)
		BEGIN_EVENT_TABLE(EnrollFromImage, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, EnrollFromImage::OnButtonOpenImageClick)
			EVT_BUTTON(ID_BUTTON_SAVE_IMAGE, EnrollFromImage::OnButtonSaveImageClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, EnrollFromImage::OnButtonSaveTemplateClick)
			EVT_BUTTON(ID_BUTTON_EXRACT_FEATURES, EnrollFromImage::OnButtonExractFeaturesClick)
			EVT_BUTTON(ID_BUTTON_DEFAULT_FINGER_QUALITY_THRESHOLD, EnrollFromImage::OnButtonDefaultClick)
			EVT_CHECKBOX(ID_CHECKBOX_SHOW_BINARIZED_IMAGE, EnrollFromImage::OnCheckBoxShowBinarizedImageClick)
			EVT_COMMAND(ID_SPINCTRL_FINGER_QUALITY_THRESHOLD, wxEVT_SPINCTRL, EnrollFromImage::OnFingerQualityThresholdChanged)
			EVT_TEXT(ID_SPINCTRL_FINGER_QUALITY_THRESHOLD, EnrollFromImage::OnFingerQualityThresholdChanged)
			EVT_COMMAND(wxID_ANY, wxEVT_ENROLLFROMIMAGEPAGE_EXTRACTION_COMPLETE, EnrollFromImage::OnFeatureExtractionCompleted)
		END_EVENT_TABLE()

		EnrollFromImage::EnrollFromImage(wxWindow *parent, const NBiometricClient & biometricClient, wxWindowID id, const wxPoint & pos, const wxSize & size, long style, const wxString & name)
			: wxPanel(parent, id, pos, size, style, name), m_biometricClient(biometricClient), m_subject(NULL), m_image(NULL)
		{
			m_defaultQualityThreshold = m_biometricClient.GetFingersQualityThreshold();
			CreateGUIControls();
		}

		void EnrollFromImage::CreateGUIControls()
		{
			wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
			LicensePanel *licencePanel = new LicensePanel(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxBORDER_SIMPLE, wxEmptyString);
			wxString licences = "Biometrics.FingerExtraction";
			wxString licencesOptional = "Images.WSQ";
			licencePanel->RefreshComponentsStatus(licences, licencesOptional);
			mainSizer->Add(licencePanel, 0, wxALL | wxEXPAND, 2);

			wxBoxSizer *boxSizerUpperButtons = new wxBoxSizer(wxHORIZONTAL);
			wxButton* btnOpenImage = new wxButton(this, ID_BUTTON_OPEN_IMAGE, "Open image", wxDefaultPosition, wxSize(-1, 25));
			btnOpenImage->SetBitmap(wxImage(openFolderIcon_xpm));
			btnOpenImage->SetToolTip("Open fingerprint image");
			wxStaticText *staticTextFeatureExtractionThreshold = new wxStaticText(this, wxID_ANY, "Threshold:");
			m_spinCtrlFingerQualityThreshold = new wxSpinCtrl(this, ID_SPINCTRL_FINGER_QUALITY_THRESHOLD, wxEmptyString, wxDefaultPosition, wxSize(50, 25), wxSP_ARROW_KEYS, 0, 100, 0);
			m_spinCtrlFingerQualityThreshold->SetValue(m_defaultQualityThreshold);
			m_buttonDefaultFinngerQualityThreshold = new wxButton(this, ID_BUTTON_DEFAULT_FINGER_QUALITY_THRESHOLD, "Default", wxDefaultPosition, wxSize(-1, 25));
			m_buttonDefaultFinngerQualityThreshold->Disable();
			m_buttonExtractFeatures = new wxButton(this, ID_BUTTON_EXRACT_FEATURES, "Extract Features", wxDefaultPosition, wxSize(-1, 25));
			m_buttonExtractFeatures->Enable(false);
			boxSizerUpperButtons->Add(btnOpenImage, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerUpperButtons->AddStretchSpacer();
			boxSizerUpperButtons->Add(staticTextFeatureExtractionThreshold, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerUpperButtons->Add(m_spinCtrlFingerQualityThreshold, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerUpperButtons->Add(m_buttonDefaultFinngerQualityThreshold, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerUpperButtons->Add(m_buttonExtractFeatures, 0, wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(boxSizerUpperButtons, 0, wxALL | wxEXPAND, 2);

			wxNViewZoomSlider *zoomSliderOriginalImage = new wxNViewZoomSlider(this);
			wxNViewZoomSlider *zoomSliderBinarizedImage = new wxNViewZoomSlider(this);
			m_fingerViewOriginalImage = new wxNFingerView(this, wxID_ANY);
			m_fingerViewBinarizedImage = new wxNFingerView(this, wxID_ANY);
			m_fingerViewBinarizedImage->SetShownImage(wxNFrictionRidgeView::PROCESSED_IMAGE);
			zoomSliderOriginalImage->SetView(m_fingerViewOriginalImage);
			zoomSliderBinarizedImage->SetView(m_fingerViewBinarizedImage);
			zoomSliderOriginalImage->SetSizeHints(0, -1);
			zoomSliderBinarizedImage->SetSizeHints(0, -1);
			wxFlexGridSizer* flexGridSizerViews = new wxFlexGridSizer(2, 2, 4, 4);
			flexGridSizerViews->AddGrowableCol(0,1);
			flexGridSizerViews->AddGrowableCol(1,1);
			flexGridSizerViews->AddGrowableRow(0,1);
			flexGridSizerViews->Add(m_fingerViewOriginalImage, 1, wxEXPAND | wxALL, 2);
			flexGridSizerViews->Add(m_fingerViewBinarizedImage, 1, wxEXPAND | wxALL, 2);
			flexGridSizerViews->Add(zoomSliderOriginalImage, 0, wxALL | wxEXPAND, 2);
			flexGridSizerViews->Add(zoomSliderBinarizedImage, 0, wxALL | wxEXPAND, 2);
			mainSizer->Add(flexGridSizerViews, 1, wxEXPAND | wxALL, 2);

			wxBoxSizer *binarizedImageButtonLineAdjustPanelSizer = new wxBoxSizer(wxHORIZONTAL);
			wxImage saveImage(saveIcon_xpm);
			m_buttonSaveImage = new wxButton(this, ID_BUTTON_SAVE_IMAGE, "Save image", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveImage->SetBitmap(saveImage);
			m_buttonSaveImage->SetToolTip("Save fingerprint image to file");
			m_buttonSaveImage->Enable(false);
			m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "Save Template", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveTemplate->SetBitmap(saveImage);
			m_buttonSaveTemplate->SetToolTip("Save extracted template to file");
			m_buttonSaveTemplate->Enable(false);
			m_checkBoxShowBinarizedImage = new wxCheckBox(this, ID_CHECKBOX_SHOW_BINARIZED_IMAGE, "Show binarized image");
			m_checkBoxShowBinarizedImage->SetValue(true);
			m_checkBoxShowBinarizedImage->Enable(false);
			m_staticTextQuality = new wxStaticText(this, wxID_ANY, wxEmptyString);
			binarizedImageButtonLineAdjustPanelSizer->Add(m_staticTextQuality, 0, wxALL | wxALIGN_CENTER, 2);
			binarizedImageButtonLineAdjustPanelSizer->AddStretchSpacer();
			binarizedImageButtonLineAdjustPanelSizer->Add(m_checkBoxShowBinarizedImage, 0, wxALL | wxALIGN_CENTER, 2);
			binarizedImageButtonLineAdjustPanelSizer->Add(m_buttonSaveImage, 0, wxALIGN_CENTER | wxALL, 2);
			binarizedImageButtonLineAdjustPanelSizer->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(binarizedImageButtonLineAdjustPanelSizer, 0, wxEXPAND | wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainSizer);
		}

		void EnrollFromImage::OnButtonOpenImageClick(wxCommandEvent & WXUNUSED(event))
		{
			try
			{
				wxFileDialog openFileDialog(this, "Choose finger image", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN);
				if (openFileDialog.ShowModal() == wxID_OK)
				{
					m_fingerViewOriginalImage->SetFinger(NULL);
					m_subject = NULL;
					m_image = NULL;

					NImage image = NImage::FromFile(openFileDialog.GetPath());
					NFinger finger;
					finger.SetImage(image);
					m_fingerViewOriginalImage->SetFinger(finger);
					m_image = image;
					ExtractFeatures();
				}
			}
			catch (NError & ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		void EnrollFromImage::ExtractFeatures()
		{
			if (!m_image.IsNull())
			{
				m_buttonSaveImage->Enable(false);
				m_buttonSaveTemplate->Enable(false);
				m_checkBoxShowBinarizedImage->Enable(false);
				m_buttonExtractFeatures->Enable(false);
				m_fingerViewBinarizedImage->SetFinger(NULL);
				m_staticTextQuality->SetLabel(wxEmptyString);

				m_subject = NSubject();
				NFinger finger;
				finger.SetImage(m_image);
				m_subject.GetFingers().Add(finger);
				m_biometricClient.SetFingersQualityThreshold(m_spinCtrlFingerQualityThreshold->GetValue());
				NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_subject);
				operation.AddCompletedCallback(&EnrollFromImage::OnFeaturesExtractionCompletedCallback, this);
			}
		}

		void EnrollFromImage::OnFeaturesExtractionCompletedCallback(const EventArgs & args)
		{
			EnrollFromImage *panel = reinterpret_cast<EnrollFromImage*>(args.GetParam());
			wxCommandEvent ev(wxEVT_ENROLLFROMIMAGEPAGE_EXTRACTION_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void EnrollFromImage::OnFeatureExtractionCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			NBiometricStatus status = m_subject.GetStatus();
			if (operation.GetError().IsNull())
			{
				if (status == nbsOk)
				{
					m_staticTextQuality->SetLabel(wxString::Format("Quality: %d", m_subject.GetFingers()[0].GetObjects()[0].GetQuality()));
					m_fingerViewBinarizedImage->SetFinger(m_subject.GetFingers().Get(0));
					m_buttonSaveImage->Enable(true);
					m_buttonSaveTemplate->Enable(true);
					m_checkBoxShowBinarizedImage->Enable(true);
				}
				else
				{
					wxMessageBox(wxString("Extraction failed. Status: ").Append(NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), m_subject.GetStatus())), "Error", wxICON_ERROR);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
			m_buttonExtractFeatures->Enable(true);
		}

		void EnrollFromImage::OnButtonSaveImageClick(wxCommandEvent& WXUNUSED(event))
		{
			try
			{
				wxFileDialog dialog(this, "Save image", wxEmptyString, wxEmptyString, Common::GetSaveFileFilterString(), wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
				if (dialog.ShowModal() == wxID_OK)
				{
					NImage image = m_checkBoxShowBinarizedImage->GetValue() ? m_subject.GetFingers().Get(0).GetBinarizedImage () : m_subject.GetFingers().Get(0).GetImage();
					image.Save(dialog.GetPath());
				}
			}
			catch (NError& ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		void EnrollFromImage::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
		{
			try
			{
				wxFileDialog saveDialog(this, "Save File", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
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

		void EnrollFromImage::OnButtonExractFeaturesClick(wxCommandEvent& WXUNUSED(event))
		{
			ExtractFeatures();
		}

		void EnrollFromImage::OnCheckBoxShowBinarizedImageClick(wxCommandEvent& WXUNUSED(event))
		{
			m_fingerViewBinarizedImage->SetShownImage(m_checkBoxShowBinarizedImage->IsChecked() ? wxNFrictionRidgeView::PROCESSED_IMAGE : wxNFrictionRidgeView::ORIGINAL_IMAGE);
		}

		void EnrollFromImage::OnFingerQualityThresholdChanged(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonDefaultFinngerQualityThreshold->Enable(m_defaultQualityThreshold != m_spinCtrlFingerQualityThreshold->GetValue());
		}

		void EnrollFromImage::OnButtonDefaultClick(wxCommandEvent& WXUNUSED(event))
		{
			m_biometricClient.SetFingersQualityThreshold(m_defaultQualityThreshold);
			m_spinCtrlFingerQualityThreshold->SetValue(m_defaultQualityThreshold);
		}
	}
}
