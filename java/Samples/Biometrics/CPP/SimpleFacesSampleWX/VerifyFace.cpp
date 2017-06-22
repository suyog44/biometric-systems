#include "Precompiled.h"
#include "VerifyFace.h"
#include "LicensePanel.h"
#include "Resources/OpenFolderIcon.xpm"

using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(EVT_VERIFYPAGE_CREATETEMPLATE_COMPLETED);
		DEFINE_EVENT_TYPE(EVT_VERIFYPAGE_VERIFY_COMPLETED);
		BEGIN_EVENT_TABLE(VerifyFace, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE_1, VerifyFace::OnButtonOpenImage1Click)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE_2, VerifyFace::OnButtonOpenImage2Click)
			EVT_BUTTON(ID_BUTTON_CLEAR_IMAGES, VerifyFace::OnButtonClearImagesClick)
			EVT_BUTTON(ID_BUTTON_DEFAULT_FAR, VerifyFace::OnButtonDefaultFarClick)
			EVT_BUTTON(ID_BUTTON_VERIFY, VerifyFace::OnButtonVerifyClick)
			EVT_COMBOBOX(ID_COMBOBOX_FAR, VerifyFace::OnComboBoxMatchingFarChange)
			EVT_TEXT(ID_COMBOBOX_FAR, VerifyFace::OnComboBoxMatchingFarChange)
			EVT_COMMAND(wxID_ANY, EVT_VERIFYPAGE_CREATETEMPLATE_COMPLETED, VerifyFace::OnCreateTemplateCompleted)
			EVT_COMMAND(wxID_ANY, EVT_VERIFYPAGE_VERIFY_COMPLETED, VerifyFace::OnVerifyCompleted)
		END_EVENT_TABLE()

		VerifyFace::VerifyFace(wxWindow *parent, NBiometricClient &biometricClient)
		: wxPanel(parent), m_biometricClient(biometricClient), m_subject1(NULL), m_subject2(NULL)
		{
			CreateGUIControls();
			InitializeBiometricParams();
		}

		void VerifyFace::CreateGUIControls()
		{
			wxBoxSizer *mainBox = new wxBoxSizer(wxVERTICAL);

			LicensePanel *licensePanel = new LicensePanel(this);
			licensePanel->RefreshComponentsStatus("Biometrics.FaceExtraction,Biometrics.FaceMatching", wxEmptyString);
			mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

			wxBoxSizer *boxSizerTopControlls = new wxBoxSizer(wxHORIZONTAL);
			m_buttonOpenImage1 = new wxBitmapButton(this, ID_BUTTON_OPEN_IMAGE_1, openFolderIcon_xpm);
			m_buttonOpenImage1->SetToolTip("Open first face image or template file (*.dat)");
			m_buttonOpenImage2 = new wxBitmapButton(this, ID_BUTTON_OPEN_IMAGE_2, openFolderIcon_xpm);
			m_buttonOpenImage2->SetToolTip("Open second face image or template file (*.dat)");

			wxStaticBoxSizer* staticBoxSizerFAR = new wxStaticBoxSizer(wxHORIZONTAL, this, "Matching FAR");
			m_comboBoxMatchingFar = new wxComboBox(this, ID_COMBOBOX_FAR, wxEmptyString);
			m_comboBoxMatchingFar->Append(Utils::MatchingThresholdToString(36));
			m_comboBoxMatchingFar->Append(Utils::MatchingThresholdToString(48));
			m_comboBoxMatchingFar->Append(Utils::MatchingThresholdToString(60));
			m_buttonDefaultFar = new wxButton(this, ID_BUTTON_DEFAULT_FAR, "Default");
			m_buttonDefaultFar->Enable(false);

			staticBoxSizerFAR->Add(m_comboBoxMatchingFar, 0, wxALIGN_CENTER | wxALL, 2);
			staticBoxSizerFAR->Add(m_buttonDefaultFar, 0, wxALIGN_CENTER | wxALL, 2);

			boxSizerTopControlls->Add(m_buttonOpenImage1, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerTopControlls->Add(staticBoxSizerFAR, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerTopControlls->Add(m_buttonOpenImage2, 0, wxALIGN_CENTER | wxALL, 2);

			mainBox->Add(boxSizerTopControlls, 0, wxALIGN_CENTER | wxALL, 2);

			wxFlexGridSizer *flexGridSizerView = new wxFlexGridSizer(2, 2, 5, 5);
			wxNViewZoomSlider *zoomslider1 = new wxNViewZoomSlider(this);
			wxNViewZoomSlider *zoomslider2 = new wxNViewZoomSlider(this);
			m_faceView1 = new wxNFaceView(this);
			m_faceView2 = new wxNFaceView(this);
			m_faceView1->SetBackgroundColour(GetBackgroundColour());
			m_faceView2->SetBackgroundColour(GetBackgroundColour());
			zoomslider1->SetView(m_faceView1);
			zoomslider2->SetView(m_faceView2);
			flexGridSizerView->Add(m_faceView1, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(m_faceView2, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(zoomslider1, 0, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(zoomslider2, 0, wxEXPAND | wxALL, 2);

			flexGridSizerView->AddGrowableCol(0);
			flexGridSizerView->AddGrowableCol(1);
			flexGridSizerView->AddGrowableRow(0);

			mainBox->Add(flexGridSizerView, 1, wxEXPAND | wxALL, 2);
			m_buttonClearImages = new wxButton(this, ID_BUTTON_CLEAR_IMAGES, "Clear Images", wxDefaultPosition, wxDefaultSize);
			mainBox->Add(m_buttonClearImages, 0, wxALIGN_CENTER | wxALL, 2);

			wxStaticText *staticTextTemplateLeft = new wxStaticText(this, wxID_ANY, "Image or template left: ");
			wxStaticText *staticTextTemplateRight = new wxStaticText(this, wxID_ANY, "Image or template right: ");
			staticTextTemplateNameLeft = new wxStaticText(this, wxID_ANY, wxEmptyString);
			staticTextTemplateNameRight = new wxStaticText(this, wxID_ANY, wxEmptyString);
			wxBoxSizer* boxSizerLeftImageDetails = new wxBoxSizer(wxHORIZONTAL);
			wxBoxSizer* boxSizerRightImageDetails = new wxBoxSizer(wxHORIZONTAL);

			boxSizerLeftImageDetails->Add(staticTextTemplateLeft, 0, wxALL, 2);
			boxSizerLeftImageDetails->Add(staticTextTemplateNameLeft, 0, wxALL, 2);
			boxSizerRightImageDetails->Add(staticTextTemplateRight, 0, wxALL, 2);
			boxSizerRightImageDetails->Add(staticTextTemplateNameRight, 0, wxALL, 2);
			mainBox->Add(boxSizerLeftImageDetails, 0, wxALL, 2);
			mainBox->Add(boxSizerRightImageDetails, 0, wxALL, 2);

			m_buttonVerify = new wxButton(this, ID_BUTTON_VERIFY, "Verify");
			m_buttonVerify->Enable(false);
			mainBox->Add(m_buttonVerify, 0, wxALL, 2);

			m_staticTextScore = new wxStaticText(this, wxID_ANY, "Score");
			m_staticTextScore->SetLabel(wxEmptyString);
			staticTextTemplateNameLeft->SetLabel(wxEmptyString);
			staticTextTemplateNameRight->SetLabel(wxEmptyString);
			mainBox->Add(m_staticTextScore, 0, wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainBox);
		}

		void VerifyFace::InitializeBiometricParams()
		{
			m_defaultFar = m_biometricClient.GetMatchingThreshold();
			m_comboBoxMatchingFar->SetStringSelection(Utils::MatchingThresholdToString(m_defaultFar));
			m_buttonDefaultFar->Disable();
		}

		void VerifyFace::EnableVerifyButton(){
			m_buttonVerify->Enable(IsSubjectValid(m_subject1) && IsSubjectValid(m_subject2));
		}
		
		bool VerifyFace::IsSubjectValid(NSubject &subject){
			return (!subject.IsNull()) && (subject.GetStatus() == nbsOk
				|| subject.GetStatus() == nbsMatchNotFound
				|| subject.GetStatus() == nbsNone) && (!subject.GetTemplateBuffer().IsNull());
		}

		void VerifyFace::OnButtonOpenImage1Click(wxCommandEvent& WXUNUSED(event))
		{
			wxString label = OpenImageTemplate(m_faceView1, m_subject1);
			if (label != wxEmptyString)
				staticTextTemplateNameLeft->SetLabel(label);
		}

		void VerifyFace::OnButtonOpenImage2Click(wxCommandEvent& WXUNUSED(event))
		{
			wxString label = OpenImageTemplate(m_faceView2, m_subject2);
			if (label != wxEmptyString)
				staticTextTemplateNameRight->SetLabel(label);
		}

		wxString VerifyFace::OpenImageTemplate(wxNFaceView* faceView, NSubject& subject)
		{
			wxString fileLocation = wxEmptyString;
			wxFileDialog wxfdOpenFile(this, "Open Template", wxEmptyString, wxEmptyString, wxFileSelectorDefaultWildcardStr, wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (wxfdOpenFile.ShowModal() == wxID_OK)
			{
				subject = NULL;
				faceView->SetFace(NULL);
				m_staticTextScore->SetLabel(wxEmptyString);
				fileLocation = wxfdOpenFile.GetPath();
				// Check if given file is a template
				try
				{
					subject = NSubject::FromFile(fileLocation);
					EnableVerifyButton();
				}
				catch (NError&)
				{
				}

				if (subject.IsNull())
				{
					// If file is not a template, try to load it as image
					subject = NSubject();
					NFace face;
					face.SetFileName(fileLocation);
					subject.GetFaces().Add(face);
					NBiometricTask task = m_biometricClient.CreateTask(nboCreateTemplate, subject);
					NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
					operation.AddCompletedCallback(&VerifyFace::OnCreateTemplateCompletedCallback, this);
					faceView->SetFace(face);
				}
			}
			return fileLocation;
		}

		void VerifyFace::OnCreateTemplateCompletedCallback(EventArgs args)
		{
			VerifyFace* panel = reinterpret_cast<VerifyFace*>(args.GetParam());
			wxCommandEvent ev(EVT_VERIFYPAGE_CREATETEMPLATE_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void VerifyFace::OnCreateTemplateCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NValue result = operation.GetResult(); 
				NBiometricTask task = result.ToObject<NBiometricTask>();
				NBiometricStatus status = task.GetStatus();
				if (status != nbsOk)
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("The template was not extracted: %s.", statusString), wxMessageBoxCaptionStr, wxOK | wxICON_ERROR);
				}
				EnableVerifyButton();
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void VerifyFace::OnButtonVerifyClick(wxCommandEvent& WXUNUSED(event))
		{
			if (!m_subject1.IsNull() && !m_subject2.IsNull())
			{
				int threshold;
				try
				{
					threshold = Utils::MatchingThresholdFromString(m_comboBoxMatchingFar->GetValue());
				}
				catch (NError&)
				{
					wxMessageBox("FAR is not valid", "Error", wxICON_ERROR);
					return;
				}
				m_biometricClient.SetMatchingThreshold(threshold);
				m_comboBoxMatchingFar->SetValue(Utils::MatchingThresholdToString(m_biometricClient.GetMatchingThreshold()));
				m_biometricClient.SetMatchingWithDetails(true);
				NAsyncOperation operation = m_biometricClient.VerifyAsync(m_subject1, m_subject2);
				operation.AddCompletedCallback(&VerifyFace::OnVerifyCompletedCallback, this);
				m_buttonVerify->Enable(false);
			}
		}

		void VerifyFace::OnVerifyCompletedCallback(EventArgs args)
		{
			VerifyFace* panel = reinterpret_cast<VerifyFace*>(args.GetParam());
			wxCommandEvent ev(EVT_VERIFYPAGE_VERIFY_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void VerifyFace::OnVerifyCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subject1.GetStatus();
				wxString verificationStatus = wxString::Format("Verification status: %s", wxString(NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status)));
				if (status == nbsOk && m_subject1.GetMatchingResults().GetCount() != 0)
				{
					//Get matching score
					int score = ( m_subject1.GetMatchingResults()[0].GetScore());
					wxString msg = wxString::Format("Score of matched templates: %i", score);
					m_staticTextScore->SetLabel(msg);
					wxMessageBox(wxString::Format("%s\n%s", verificationStatus, msg), wxMessageBoxCaptionStr, wxOK | wxICON_INFORMATION);
				}
				else
				{
					wxMessageBox(verificationStatus, wxMessageBoxCaptionStr, wxOK | wxICON_ERROR);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void VerifyFace::OnButtonDefaultFarClick(wxCommandEvent& WXUNUSED(event))
		{
			m_comboBoxMatchingFar->SetValue(Utils::MatchingThresholdToString(m_defaultFar));
			m_buttonDefaultFar->Enable(false);
		}

		void VerifyFace::OnComboBoxMatchingFarChange(wxCommandEvent &WXUNUSED(event))
		{
			m_buttonDefaultFar->Enable(true);
			EnableVerifyButton();
		}

		void VerifyFace::OnButtonClearImagesClick(wxCommandEvent& WXUNUSED(event))
		{
			m_subject1 = NULL;
			m_subject2 = NULL;
			m_faceView1->SetFace(NULL);
			m_faceView2->SetFace(NULL);
			m_buttonVerify->Enable(false);
			m_staticTextScore->SetLabel(wxEmptyString);
			staticTextTemplateNameLeft->SetLabel(wxEmptyString);
			staticTextTemplateNameRight->SetLabel(wxEmptyString);
		}
	}
}
