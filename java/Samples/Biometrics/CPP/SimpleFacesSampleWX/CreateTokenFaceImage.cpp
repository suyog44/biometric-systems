#include "Precompiled.h"
#include "Resources/OpenFolderIcon.xpm"
#include "Resources/SaveIcon.xpm"
#include "CreateTokenFaceImage.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::Images;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(EVT_CREATETOKENIMAGEPAGE_CREATETOKENIMAGE_COMPLETED);
		BEGIN_EVENT_TABLE(CreateTokenFaceImage, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, CreateTokenFaceImage::OnButtonOpenImageClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TOKEN_IMAGE, CreateTokenFaceImage::OnButtonSaveTokenImageClick)
			EVT_COMMAND(wxID_ANY, EVT_CREATETOKENIMAGEPAGE_CREATETOKENIMAGE_COMPLETED, CreateTokenFaceImage::OnCreateTokenImageCompleted)
		END_EVENT_TABLE()

		CreateTokenFaceImage::CreateTokenFaceImage(wxWindow* parent, NBiometricClient &biometricClient) : wxPanel(parent), m_biometricClient(biometricClient), m_subject(NULL)
		{
			CreateGUIControls();
		}

		void CreateTokenFaceImage::CreateGUIControls()
		{
			wxBoxSizer *mainBox = new wxBoxSizer(wxVERTICAL);
			LicensePanel *licensePanel = new LicensePanel(this);
			licensePanel->RefreshComponentsStatus("Biometrics.FaceDetection,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment", wxEmptyString);
			mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

			m_buttonOpenImage = new wxButton(this, ID_BUTTON_OPEN_IMAGE, "Open Image");
			m_buttonOpenImage->SetBitmap(openFolderIcon_xpm);
			m_buttonOpenImage->SetToolTip("Open Image.");
			mainBox->Add(m_buttonOpenImage, 0, wxALL , 2);

			wxFlexGridSizer *flexGridSizerView = new wxFlexGridSizer(3, 2, 5, 5);
			wxStaticText *staticTextOriginalFaceImage = new wxStaticText(this, wxID_ANY, "Original face image");
			wxStaticText *staticTextTokenFaceImage = new wxStaticText(this, wxID_ANY, "Token face image");
			staticTextOriginalFaceImage->SetBackgroundColour(wxSystemSettings::GetColour(wxSYS_COLOUR_ACTIVECAPTION));
			staticTextTokenFaceImage->SetBackgroundColour(wxSystemSettings::GetColour(wxSYS_COLOUR_ACTIVECAPTION));
			wxNViewZoomSlider *zoomSliderOriginal = new wxNViewZoomSlider(this);
			wxNViewZoomSlider *zoomSliderToken = new wxNViewZoomSlider(this);
			m_faceViewOriginal = new wxNFaceView(this);
			m_faceViewOriginal->SetFaceRectangleColor(wxNullColour);
			m_faceViewOriginal->SetShowBaseFeaturePoints(false);
			m_faceViewOriginal->SetShowEmotions(false);
			m_faceViewOriginal->SetShowEmotionsConfidence(false);
			m_faceViewOriginal->SetShowExpression(false);
			m_faceViewOriginal->SetShowExpressionConfidence(false);
			m_faceViewOriginal->SetShowEyes(false);
			m_faceViewOriginal->SetShowEyesConfidence(false);
			m_faceViewOriginal->SetShowFaceConfidence(false);
			m_faceViewOriginal->SetShowGender(false);
			m_faceViewOriginal->SetShowGenderConfidence(false);
			m_faceViewOriginal->SetShowMouth(false);
			m_faceViewOriginal->SetShowMouthConfidence(false);
			m_faceViewToken = new wxNFaceView(this);
			m_faceViewToken->SetFaceRectangleColor(wxNullColour);
			m_faceViewToken->SetShowBaseFeaturePoints(false);
			m_faceViewToken->SetShowEmotions(false);
			m_faceViewToken->SetShowEmotionsConfidence(false);
			m_faceViewToken->SetShowExpression(false);
			m_faceViewToken->SetShowExpressionConfidence(false);
			m_faceViewToken->SetShowEyes(false);
			m_faceViewToken->SetShowEyesConfidence(false);
			m_faceViewToken->SetShowFaceConfidence(false);
			m_faceViewToken->SetShowGender(false);
			m_faceViewToken->SetShowGenderConfidence(false);
			m_faceViewToken->SetShowMouth(false);
			m_faceViewToken->SetShowMouthConfidence(false);
			zoomSliderOriginal->SetView(m_faceViewOriginal);
			zoomSliderToken->SetView(m_faceViewToken);
			m_faceViewToken->SetBackgroundColour(GetBackgroundColour());
			m_faceViewOriginal->SetBackgroundColour(GetBackgroundColour());
			m_staticTextQuality = new wxStaticText(m_faceViewToken, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize);
			m_staticTextSharpness = new wxStaticText(m_faceViewToken, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize);
			m_staticTextUniformity = new wxStaticText(m_faceViewToken, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize);
			m_staticTextDensity = new wxStaticText(m_faceViewToken, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize);
			wxBoxSizer *boxSizerNview = new wxBoxSizer(wxHORIZONTAL);
			wxBoxSizer *boxSizerTokenProperties = new wxBoxSizer(wxVERTICAL);
			boxSizerTokenProperties->Add(m_staticTextQuality);
			boxSizerTokenProperties->Add(m_staticTextSharpness);
			boxSizerTokenProperties->Add(m_staticTextUniformity);
			boxSizerTokenProperties->Add(m_staticTextDensity);
			boxSizerNview->AddStretchSpacer(1);
			boxSizerNview->Add(boxSizerTokenProperties, 0, wxALIGN_BOTTOM | wxALIGN_CENTER_HORIZONTAL);
			boxSizerNview->AddStretchSpacer(1);
			m_faceViewToken->SetSizer(boxSizerNview);
			flexGridSizerView->Add(staticTextOriginalFaceImage, 0, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(staticTextTokenFaceImage, 0, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(m_faceViewOriginal, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(m_faceViewToken, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(zoomSliderOriginal, 0, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(zoomSliderToken, 0, wxEXPAND | wxALL, 2);
			flexGridSizerView->AddGrowableCol(0);
			flexGridSizerView->AddGrowableCol(1);
			flexGridSizerView->AddGrowableRow(1);
			mainBox->Add(flexGridSizerView, 1, wxEXPAND | wxALL, 2);

			m_buttonSaveTokenImage = new wxButton(this, ID_BUTTON_SAVE_TOKEN_IMAGE, "Save Token image");
			m_buttonSaveTokenImage->SetBitmap(saveIcon_xpm);
			m_buttonSaveTokenImage->SetToolTip("Save token face image to a file");
			m_buttonSaveTokenImage->Enable(false);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			mainBox->Add(m_buttonSaveTokenImage, 0, wxALL | wxALIGN_RIGHT, 2);
			SetSizer(mainBox);
		}

		void CreateTokenFaceImage::OnButtonOpenImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog fileDialog(this, "Open Face Image", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (fileDialog.ShowModal() == wxID_OK)
			{
				m_faceViewToken->SetFace(NULL);
				m_faceViewOriginal->SetFace(NULL);
				m_buttonSaveTokenImage->Enable(false);
				ShowAttributeLabels(false);

				NFace originalFace;
				originalFace.SetFileName(fileDialog.GetPath());
				m_faceViewOriginal->SetFace(originalFace);
				m_subject = NSubject();
				m_subject.GetFaces().Add(originalFace);

				NBiometricTask task = m_biometricClient.CreateTask((NBiometricOperations)(nboSegment | nboAssessQuality), m_subject);
				NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
				operation.AddCompletedCallback(&CreateTokenFaceImage::OnCreateTokenImageCompletedCallback, this);
			}
		}

		void CreateTokenFaceImage::OnCreateTokenImageCompletedCallback(EventArgs args)
		{
			CreateTokenFaceImage* panel = reinterpret_cast<CreateTokenFaceImage *>(args.GetParam());
			wxCommandEvent ev(EVT_CREATETOKENIMAGEPAGE_CREATETOKENIMAGE_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void CreateTokenFaceImage::OnCreateTokenImageCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subject.GetStatus();
				if (status == nbsOk)
				{
					m_faceViewToken->SetFace(m_subject.GetFaces().Get(1));
					m_buttonSaveTokenImage->Enable();
					ShowTokenAttributes();
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("Could not create token face image! Status:  %s", statusString), wxMessageBoxCaptionStr, wxOK | wxICON_ERROR);
					m_subject = NULL;
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void CreateTokenFaceImage::ShowTokenAttributes()
		{
			NLAttributes attributes = m_subject.GetFaces().Get(1).GetObjects().Get(0);
			m_staticTextQuality->SetLabel(wxString::Format("Quality:  %i", attributes.GetQuality()));
			m_staticTextSharpness->SetLabel(wxString::Format("Sharpness score:  %i", attributes.GetSharpness()));
			m_staticTextUniformity->SetLabel(wxString::Format("Background uniformity score:  %i", attributes.GetBackgroundUniformity()));
			m_staticTextDensity->SetLabel(wxString::Format("Grayscale density score:  %i", attributes.GetGrayscaleDensity()));
			ShowAttributeLabels(true);
			m_faceViewToken->Layout();
		}

		void CreateTokenFaceImage::ShowAttributeLabels(bool show)
		{
			m_staticTextQuality->Show(show);
			m_staticTextSharpness->Show(show);
			m_staticTextUniformity->Show(show);
			m_staticTextDensity->Show(show);
		}

		void CreateTokenFaceImage::OnButtonSaveTokenImageClick(wxCommandEvent& WXUNUSED(event))
		{
			NFace tokenFace = m_faceViewToken->GetFace();
			if (tokenFace.IsNull())
			{
				return;
			}
			wxFileDialog wxfdSaveFile(this, "Save Image", wxEmptyString, wxEmptyString, Common::GetSaveFileFilterString(), wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			if (wxfdSaveFile.ShowModal() == wxID_OK)
			{
				try
				{
					tokenFace.GetImage().Save(wxfdSaveFile.GetPath());
				}
				catch (NError& e)
				{
					wxExceptionDlg::Show(e);
				}
			}
		}
	}
}
