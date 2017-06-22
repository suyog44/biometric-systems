#include "Precompiled.h"
#include "VerifyIris.h"
#include "Resources/OpenFolderIcon.xpm"

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;

#define	VERIFICATION_REQUIRED_LICENSE_COMPONENTS "Biometrics.IrisExtraction,Biometrics.IrisMatching"
namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_VERIFYPAGE_TEMPLATE_CREATED_COMPLETE)
			DEFINE_EVENT_TYPE(wxEVT_VERIFYPAGE_VERIFICATION_COMPLETE)
			BEGIN_EVENT_TABLE(VerifyIris, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE1, VerifyIris::OnButtonOpenIrisLeftClick)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE2, VerifyIris::OnButtonOpenIrisRightClick)
			EVT_BUTTON(ID_BUTTON_CLEAR, VerifyIris::OnButtonClearImageClick)
			EVT_BUTTON(ID_BUTTON_DEFAULT_FAR, VerifyIris::OnButtonDefaultFARClick)
			EVT_BUTTON(ID_BUTTON_VERIFY, VerifyIris::OnButtonVerifyClick)
			EVT_COMBOBOX(ID_COMBOBOX_FAR, VerifyIris::OnComboBoxMatchingFARChange)
			EVT_TEXT(ID_COMBOBOX_FAR, VerifyIris::OnComboBoxMatchingFARChange)
			EVT_COMMAND(wxID_ANY, wxEVT_VERIFYPAGE_TEMPLATE_CREATED_COMPLETE, VerifyIris::OnCreateTemplateCompleted)
			EVT_COMMAND(wxID_ANY, wxEVT_VERIFYPAGE_VERIFICATION_COMPLETE, VerifyIris::OnVerifyCompleted)
		END_EVENT_TABLE()

		VerifyIris::VerifyIris(wxWindow *parent, const NBiometricClient & biometricClient)
			: wxPanel(parent), m_biometricClient(biometricClient), m_subject1(NULL), m_subject2(NULL)
		{
			m_defaultFar = m_biometricClient.GetMatchingThreshold();
			CreateGUIControls();
			InitializeBiometricParams();
		}

		void VerifyIris::CreateGUIControls()
		{
			wxImage imageOpen(openFolderIcon_xpm);
			wxBoxSizer *boxSizerMain = new wxBoxSizer(wxVERTICAL);

			LicensePanel *licencePanel = new LicensePanel(this);
			licencePanel->RefreshComponentsStatus(VERIFICATION_REQUIRED_LICENSE_COMPONENTS, wxEmptyString);
			boxSizerMain->Add(licencePanel, 0, wxEXPAND | wxALL, 2);

			wxBoxSizer *boxSizerOpen = new wxBoxSizer(wxHORIZONTAL);
			m_buttonOpenIris1 = new wxBitmapButton(this, ID_BUTTON_OPEN_IMAGE1, imageOpen);
			m_buttonOpenIris1->SetToolTip("Open first iris image to verify");
			boxSizerOpen->Add(m_buttonOpenIris1, 0, wxALIGN_CENTER | wxALL, 2);
			wxStaticBoxSizer *staticBoxSizerMatchingFAR = new wxStaticBoxSizer(wxHORIZONTAL, this, "Matching FAR");
			m_comboFAR = new wxComboBox(this, ID_COMBOBOX_FAR);
			m_comboFAR->Append(Utils::MatchingThresholdToString(36));
			m_comboFAR->Append(Utils::MatchingThresholdToString(48));
			m_comboFAR->Append(Utils::MatchingThresholdToString(60));
			m_comboFAR->Append(Utils::MatchingThresholdToString(72));
			staticBoxSizerMatchingFAR->Add(m_comboFAR, 1, wxALL, 2);
			m_buttonDefaultFAR = new wxButton(this, ID_BUTTON_DEFAULT_FAR, "Default", wxDefaultPosition, wxSize(-1, 25));
			staticBoxSizerMatchingFAR->Add(m_buttonDefaultFAR, 0, wxALL, 2);
			boxSizerOpen->Add(staticBoxSizerMatchingFAR, 0, wxALIGN_CENTER | wxALL, 2);
			m_buttonOpenIris2 = new wxBitmapButton(this, ID_BUTTON_OPEN_IMAGE2, imageOpen);
			m_buttonOpenIris2->SetToolTip("Open second iris image to verify");
			boxSizerOpen->Add(m_buttonOpenIris2, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerMain->Add(boxSizerOpen, 0, wxALIGN_CENTER | wxALL, 2);

			wxFlexGridSizer *flexGridSizerView = new wxFlexGridSizer(2, 2, 2, 2);
			wxNViewZoomSlider *zoomslider1 = new wxNViewZoomSlider(this);
			wxNViewZoomSlider *zoomslider2 = new wxNViewZoomSlider(this);
			m_irisView1 = new wxNIrisView(this);
			m_irisView2 = new wxNIrisView(this);
			m_irisView1->SetBackgroundColour(GetBackgroundColour());
			m_irisView2->SetBackgroundColour(GetBackgroundColour());

			zoomslider1->SetView(m_irisView1);
			zoomslider2->SetView(m_irisView2);

			flexGridSizerView->Add(m_irisView1, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(m_irisView2, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(zoomslider1, 0, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(zoomslider2, 0, wxEXPAND | wxALL, 2);

			flexGridSizerView->AddGrowableCol(0);
			flexGridSizerView->AddGrowableCol(1);
			flexGridSizerView->AddGrowableRow(0);
			boxSizerMain->Add(flexGridSizerView, 1, wxEXPAND | wxALL, 2);

			m_buttonClearImages = new wxButton(this, ID_BUTTON_CLEAR, "Clear Images", wxDefaultPosition, wxSize(-1, 25));
			boxSizerMain->Add(m_buttonClearImages, 0, wxALIGN_CENTER | wxALL, 2);

			wxBoxSizer *boxSizerLeftInfo = new wxBoxSizer(wxHORIZONTAL);
			wxStaticText *statictxtLeftInfoTitle = new wxStaticText(this, wxID_ANY, "Image or template left: ");
			m_staticTxtLeftInfo = new wxStaticText(this, wxID_ANY, wxEmptyString);
			boxSizerLeftInfo->Add(statictxtLeftInfoTitle, 0, wxALL, 2);
			boxSizerLeftInfo->Add(m_staticTxtLeftInfo, 0, wxALL, 2);
			boxSizerMain->Add(boxSizerLeftInfo, 0, wxEXPAND | wxALL, 2);

			wxBoxSizer *boxSizerRightImageInfo = new wxBoxSizer(wxHORIZONTAL);
			wxStaticText *staticTextRightInfoTitle = new wxStaticText(this, wxID_ANY, "Image or template right :");
			m_staticTextRightInfo = new wxStaticText(this, wxID_ANY, wxEmptyString);
			boxSizerRightImageInfo->Add(staticTextRightInfoTitle, 0, wxALL, 2);
			boxSizerRightImageInfo->Add(m_staticTextRightInfo, 0, wxALL, 2);
			boxSizerMain->Add(boxSizerRightImageInfo, 0, wxEXPAND | wxALL, 2);

			m_buttonVerify = new wxButton(this, ID_BUTTON_VERIFY, "Verify", wxDefaultPosition, wxSize(-1, 25));
			m_buttonVerify->Enable(false);
			boxSizerMain->Add(m_buttonVerify, 0, wxALL, 2);

			m_staticTextScore = new wxStaticText(this, wxID_ANY, wxEmptyString);
			boxSizerMain->Add(m_staticTextScore, 0, wxALL, 2);

			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizerAndFit(boxSizerMain);
		}

		VerifyIris::~VerifyIris()
		{
		}

		void VerifyIris::InitializeBiometricParams()
		{
			m_defaultFar = m_biometricClient.GetMatchingThreshold();
			m_comboFAR->SetStringSelection(Utils::MatchingThresholdToString(m_defaultFar));
		}

		wxString VerifyIris::OpenTemplateOrImage(wxNIrisView *irisView, NSubject* subject)
		{
			wxFileDialog openDialog(this, "Open Image or Template File", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openDialog.ShowModal() == wxID_OK)
			{
				irisView->SetIris(NULL);
				m_staticTextScore->SetLabelText(wxEmptyString);
				*subject = NULL;
				try
				{
					//Try to load as a template.
					*subject = NSubject::FromFile(openDialog.GetPath());
					EnableVerifyButton();
				}
				catch (NError & /*ex*/)
				{
				}
				if (subject->IsNull())
				{
					//If fails try to load as an image
					*subject = NSubject();
					NIris iris = NIris();
					iris.SetFileName(openDialog.GetPath());
					subject->GetIrises().Add(iris);
					irisView->SetIris(iris);
					NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(*subject);
					operation.AddCompletedCallback(&VerifyIris::OnCreateTemplateCompletedCallback, this);
				}
				return openDialog.GetPath();
			}
			return wxEmptyString;
		}

		void VerifyIris::OnCreateTemplateCompletedCallback(const EventArgs & args)
		{
			VerifyIris *verifyPanel = reinterpret_cast<VerifyIris *>(args.GetParam());
			wxCommandEvent ev(wxEVT_VERIFYPAGE_TEMPLATE_CREATED_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(verifyPanel, ev);
		}

		void VerifyIris::OnCreateTemplateCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			NValue result = operation.GetResult();
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = (NBiometricStatus)operation.GetResult().ToInt32();
				if (status != nbsOk)
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox("The template was not extracted: " + statusString, "Error", wxOK | wxICON_ERROR);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
			EnableVerifyButton();
		}

		void VerifyIris::OnButtonOpenIrisLeftClick(wxCommandEvent& WXUNUSED(event))
		{
			wxString imagePositionLeft = OpenTemplateOrImage(m_irisView1, &m_subject1);
			if (imagePositionLeft != wxEmptyString)
			{
				m_staticTxtLeftInfo->SetLabelText(imagePositionLeft);
			}
		}

		void VerifyIris::OnButtonOpenIrisRightClick(wxCommandEvent& WXUNUSED(event))
		{
			wxString imagePositionRight = OpenTemplateOrImage(m_irisView2, &m_subject2);
			if (imagePositionRight != wxEmptyString)
			{
				m_staticTextRightInfo->SetLabelText(imagePositionRight);
			}
		}

		void VerifyIris::OnButtonClearImageClick(wxCommandEvent& WXUNUSED(event))
		{
			m_irisView1->SetIris(NULL);
			m_irisView2->SetIris(NULL);
			m_subject1 = NULL;
			m_subject2 = NULL;
			m_staticTxtLeftInfo->SetLabelText(wxEmptyString);
			m_staticTextRightInfo->SetLabelText(wxEmptyString);
			m_staticTextScore->SetLabelText(wxEmptyString);
			m_buttonVerify->Enable(false);
		}

		void VerifyIris::OnButtonDefaultFARClick(wxCommandEvent& WXUNUSED(event))
		{
			m_comboFAR->SetValue(Utils::MatchingThresholdToString(m_defaultFar));
			m_buttonDefaultFAR->Enable(false);
			EnableVerifyButton();
		}

		void VerifyIris::OnComboBoxMatchingFARChange(wxCommandEvent& WXUNUSED(event))
		{
			EnableVerifyButton();
			m_buttonDefaultFAR->Enable(true);
		}

		void VerifyIris::OnButtonVerifyClick(wxCommandEvent& WXUNUSED(event))
		{
			double threshold;
			try
			{
				threshold = Utils::MatchingThresholdFromString(m_comboFAR->GetValue());
			}
			catch (NError & /*ex*/)
			{
				wxMessageBox("FAR is not valid", "Error", wxICON_ERROR);
				return;
			}
			m_biometricClient.SetMatchingThreshold(threshold);
			m_comboFAR->SetValue(Utils::MatchingThresholdToString(m_biometricClient.GetMatchingThreshold()));
			NAsyncOperation operation = m_biometricClient.VerifyAsync(m_subject1, m_subject2);
			operation.AddCompletedCallback(&VerifyIris::OnVerifyCompletedCallback, this);
			m_buttonVerify->Enable(false);
		}

		void VerifyIris::OnVerifyCompletedCallback(const EventArgs & args)
		{
			VerifyIris *verifyPanel = reinterpret_cast<VerifyIris *>(args.GetParam());
			wxCommandEvent ev(wxEVT_VERIFYPAGE_VERIFICATION_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(verifyPanel, ev);
		}

		void VerifyIris::OnVerifyCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				int score = 0;
				wxString resultString;
				NBiometricStatus status = m_subject1.GetStatus();
				if (status == nbsOk)
				{
					score = m_subject1.GetMatchingResults()[0].GetScore();
					resultString = wxString::Format("Verification status: Ok\nScore of matched templates %i" , score);
				}
				else
				{
					resultString = "Verification status: " + NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(),status);
				}
				wxMessageDialog message(this,resultString , wxEmptyString, wxOK | wxCENTRE | wxICON_NONE, wxDefaultPosition);
				message.ShowModal();
				m_staticTextScore->SetLabelText(wxString::Format("Score of matched templates : %i", score));
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void VerifyIris::EnableVerifyButton()
		{
			m_buttonVerify->Enable(IsSubjectValid(m_subject1) && IsSubjectValid(m_subject2));
		}

		bool VerifyIris::IsSubjectValid(const NSubject & subject)
		{
			return !subject.IsNull() && (subject.GetStatus() == nbsOk
				|| subject.GetStatus() == nbsMatchNotFound
				|| (subject.GetStatus() == nbsNone && !subject.GetTemplateBuffer().IsNull()));
		}
	}
}
