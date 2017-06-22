#include "Precompiled.h"
#include "Resources/OpenFolderIcon.xpm"
#include "VerifyFinger.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_VERIFYPAGE_OPEN_IMAGE_COMPLETE)
		DEFINE_EVENT_TYPE(wxEVT_VERIFYPAGE_VERIFY_COMPLETE)
		BEGIN_EVENT_TABLE(VerifyFinger, wxPanel)
			EVT_BUTTON(ID_BITMAPBUTTON_OPEN_IMAGE_LEFT, VerifyFinger::OnButtonOpenLeftClick)
			EVT_BUTTON(ID_BITMAPBUTTON_OPEN_IMAGE_RIGHT, VerifyFinger::OnButtonOpenRightClick)
			EVT_CHECKBOX(ID_CHECKBOX_SHOW_BINARIZED_LEFT, VerifyFinger::OnCheckBoxShowProsessedImageLeftClick)
			EVT_CHECKBOX(ID_CHECKBOX_SHOW_BINARIZED_RIGHT, VerifyFinger::OnCheckBoxShowProsessedImageRightClick)
			EVT_BUTTON(ID_BUTTON_VERIFY, VerifyFinger::OnButtonVerifyClick)
			EVT_BUTTON(ID_BUTTON_CLEAR_IMG, VerifyFinger::OnButtonClearImagesClick)
			EVT_BUTTON(ID_BUTTON_DEFAULT_FAR, VerifyFinger::OnButtonDefaultFARClick)
			EVT_COMBOBOX(ID_COMBOBOX_FAR, VerifyFinger::OnComboBoxFARChange)
			EVT_TEXT(ID_COMBOBOX_FAR, VerifyFinger::OnComboBoxFARChange)
			EVT_COMMAND(wxID_ANY, wxEVT_VERIFYPAGE_OPEN_IMAGE_COMPLETE, VerifyFinger::OnCreateTemplateCompleted)
			EVT_COMMAND(wxID_ANY, wxEVT_VERIFYPAGE_VERIFY_COMPLETE, VerifyFinger::OnVerifyCompleted)
		END_EVENT_TABLE()

		VerifyFinger::VerifyFinger(wxWindow *parent, const NBiometricClient & biometricClient, wxWindowID id, const wxPoint & pos, const wxSize & size, long style, const wxString & name)
			: wxPanel(parent, id, pos, size, style, name), m_biometricClient(biometricClient), m_subjectLeft(NULL), m_subjectRight(NULL), m_subjectCurrent(NULL)
		{
			CreateGUIControls();
			InitializeBiometricParams();
		}

		VerifyFinger::~VerifyFinger()
		{
		}

		void VerifyFinger::CreateGUIControls()
		{
			wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
			wxString licences = "Biometrics.FingerExtraction,Biometrics.FingerMatching";
			wxString licencesOptional = "Images.WSQ";
			LicensePanel *licencePanel;
			licencePanel = new LicensePanel(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxBORDER_SIMPLE);
			licencePanel->RefreshComponentsStatus(licences, licencesOptional);
			mainSizer->Add(licencePanel, 0, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer *staticBoxSizerMatchingFAR = new wxStaticBoxSizer(wxHORIZONTAL, this, "Matching FAR");
			wxImage openImage(openFolderIcon_xpm);
			m_buttonImageLeft = new wxBitmapButton(this, ID_BITMAPBUTTON_OPEN_IMAGE_LEFT, openImage);
			m_buttonImageLeft->SetToolTip("Open first fingerprint image or template (*.dat) file");
			m_buttonImageRight = new wxBitmapButton(this, ID_BITMAPBUTTON_OPEN_IMAGE_RIGHT, openImage);
			m_buttonImageRight->SetToolTip("Open second fingerprint image or template (*.dat) file");
			m_buttonDefaultFAR = new wxButton(this, ID_BUTTON_DEFAULT_FAR, "Default", wxDefaultPosition, wxSize(-1, 25));
			m_comboBoxFAR = new wxComboBox(this, ID_COMBOBOX_FAR, wxEmptyString, wxDefaultPosition);
			m_comboBoxFAR->AppendString("0.1%");
			m_comboBoxFAR->AppendString("0.01%");
			m_comboBoxFAR->AppendString("0.001%");
			m_comboBoxFAR->SetSelection(1);
			staticBoxSizerMatchingFAR->Add(m_comboBoxFAR, 0, wxALIGN_CENTER | wxALL, 2);
			staticBoxSizerMatchingFAR->Add(m_buttonDefaultFAR, 0, wxALIGN_CENTER | wxALL, 2);

			wxBoxSizer *boxSizerImageLoading = new wxBoxSizer(wxHORIZONTAL);
			boxSizerImageLoading->Add(m_buttonImageLeft, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerImageLoading->Add(staticBoxSizerMatchingFAR, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerImageLoading->Add(m_buttonImageRight, 0, wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(boxSizerImageLoading, 0, wxALIGN_CENTER | wxALL, 2);

			wxBoxSizer *boxSizerFingerviews = new wxBoxSizer(wxHORIZONTAL);
			wxNViewZoomSlider *zoomSliderLeft = new wxNViewZoomSlider(this);
			wxNViewZoomSlider *zoomSliderRight = new wxNViewZoomSlider(this);
			m_fingerViewLeft = new wxNFingerView(this, wxID_ANY);
			m_fingerViewRight = new wxNFingerView(this, wxID_ANY);
			boxSizerFingerviews->Add(m_fingerViewLeft, 1, wxEXPAND | wxALL, 2);
			boxSizerFingerviews->Add(m_fingerViewRight, 1, wxEXPAND | wxALL, 2);
			mainSizer->Add(boxSizerFingerviews, 1, wxEXPAND | wxALL, 2);

			wxBoxSizer *boxSizerFingerViewControllers = new wxBoxSizer(wxHORIZONTAL);
			m_checkBoxShowBinarizedImageLeft = new wxCheckBox(this, ID_CHECKBOX_SHOW_BINARIZED_LEFT, "Show binarized image", wxDefaultPosition, wxDefaultSize, wxALIGN_RIGHT);
			m_checkBoxShowBinarizedImageRight = new wxCheckBox(this, ID_CHECKBOX_SHOW_BINARIZED_RIGHT, "Show binarized image");
			m_checkBoxShowBinarizedImageLeft->Enable(false);
			m_checkBoxShowBinarizedImageRight->Enable(false);
			m_checkBoxShowBinarizedImageLeft->SetValue(true);
			m_checkBoxShowBinarizedImageRight->SetValue(true);
			m_fingerViewRight->SetShownImage(wxNFrictionRidgeView::PROCESSED_IMAGE);
			m_fingerViewLeft->SetShownImage(wxNFrictionRidgeView::PROCESSED_IMAGE);

			zoomSliderLeft->SetView(m_fingerViewLeft);
			zoomSliderRight->SetView(m_fingerViewRight);
			zoomSliderLeft->SetSizeHints(100, -1);
			zoomSliderRight->SetSizeHints(100, -1);
			boxSizerFingerViewControllers->Add(zoomSliderLeft, 30, wxEXPAND | wxALIGN_CENTER | wxALL, 2);
			boxSizerFingerViewControllers->AddStretchSpacer(1);
			boxSizerFingerViewControllers->Add(m_checkBoxShowBinarizedImageLeft, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerFingerViewControllers->Add(m_checkBoxShowBinarizedImageRight, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerFingerViewControllers->AddStretchSpacer(1);
			boxSizerFingerViewControllers->Add(zoomSliderRight, 30, wxEXPAND | wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(boxSizerFingerViewControllers, 0, wxALIGN_CENTER | wxALL | wxEXPAND, 2);

			m_buttonClearImg = new wxButton(this, ID_BUTTON_CLEAR_IMG, "Clear Images", wxDefaultPosition, wxSize(-1, 25));
			mainSizer->Add(m_buttonClearImg, 0, wxALIGN_CENTER | wxALL, 2);

			wxFlexGridSizer *flexGridSizerFilePath = new wxFlexGridSizer(2, 5, 5);
			wxStaticText *staticTextLabelLeft = new wxStaticText(this, wxID_ANY, "Image or template left:", wxDefaultPosition, wxDefaultSize);
			wxStaticText *staticTextLabelRight = new wxStaticText(this, wxID_ANY, "Image or template right:", wxDefaultPosition, wxDefaultSize);
			m_staticTextLeftImagePath = new wxStaticText(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize);
			m_staticTextRightImagePath = new wxStaticText(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize);
			flexGridSizerFilePath->Add(staticTextLabelLeft, 0, wxALL, 2);
			flexGridSizerFilePath->Add(m_staticTextLeftImagePath, 0, wxALL, 2);
			flexGridSizerFilePath->Add(staticTextLabelRight, 0, wxALL, 2);
			flexGridSizerFilePath->Add(m_staticTextRightImagePath, 0, wxALL, 2);
			mainSizer->Add(flexGridSizerFilePath, 0, wxEXPAND);

			m_staticTextMatchedTemplateScore = new wxStaticText(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize);
			m_buttonVerify = new wxButton(this, ID_BUTTON_VERIFY, "Verify", wxDefaultPosition, wxSize(-1, 25));
			m_buttonVerify->Enable(false);
			mainSizer->Add(m_buttonVerify, 0, wxALL, 2);
			mainSizer->Add(m_staticTextMatchedTemplateScore, 0, wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainSizer);
		}

		void VerifyFinger::EnableCheckBoxes()
		{
			m_checkBoxShowBinarizedImageLeft->Enable(!m_fingerViewLeft->GetFinger().IsNull() && !m_fingerViewLeft->GetFinger().GetBinarizedImage().IsNull());
			m_checkBoxShowBinarizedImageRight->Enable(!m_fingerViewRight->GetFinger().IsNull() && !m_fingerViewRight->GetFinger().GetBinarizedImage().IsNull());
		}

		void VerifyFinger::EnableVerifyButton()
		{
			m_buttonVerify->Enable(IsSubjectValid(m_subjectLeft) && IsSubjectValid(m_subjectRight));
		}

		bool VerifyFinger::IsSubjectValid(const NSubject & subject)
		{
			return !subject.IsNull() && (subject.GetStatus() == nbsOk
				|| subject.GetStatus() == nbsMatchNotFound
				|| (subject.GetStatus() == nbsNone && !subject.GetTemplateBuffer().IsNull()));
		}

		void VerifyFinger::InitializeBiometricParams()
		{
			m_defaultFar = m_biometricClient.GetMatchingThreshold();
			m_comboBoxFAR->SetStringSelection(Utils::MatchingThresholdToString(m_defaultFar));
		}

		void VerifyFinger::OpenTemplateOrImage(wxNFingerView *fingerView, NSubject *subject, wxStaticText *label)
		{
			wxFileDialog openFileDialog(this, "Open Template or Image", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openFileDialog.ShowModal() == wxID_OK)
			{
				m_fingerViewLeft->SetSpanningTree(SpanningTree());
				m_fingerViewRight->SetSpanningTree(SpanningTree());

				*subject = NULL;
				fingerView->SetFinger(NULL);
				m_staticTextMatchedTemplateScore->SetLabelText(wxEmptyString);
				label->SetLabelText(openFileDialog.GetPath());
				try
				{
					//Try to load the file as a template.
					*subject = NSubject::FromFile((openFileDialog.GetPath()));
				}
				catch (NError& /*ex*/)
				{
				}
				if (!subject->IsNull() && subject->GetFingers().GetCount() > 0)
				{
					fingerView->SetFinger(subject->GetFingers().Get(0));
					EnableVerifyButton();
				}
				else
				{
					// If fails try to load the file as an image.
					wxArrayString filename;
					NFinger finger;
					*subject = NSubject();
					finger.SetFileName(openFileDialog.GetPath());
					subject->GetFingers().Add(finger);
					subject->SetId(openFileDialog.GetFilename());
					fingerView->SetFinger(finger);
					m_subjectCurrent = *subject;
					NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(*subject);
					operation.AddCompletedCallback(&VerifyFinger::OnCreateTemplateCompletedCallback, this);
				}
			}
		}

		void VerifyFinger::OnCreateTemplateCompletedCallback(const EventArgs & args)
		{
			VerifyFinger *panel = reinterpret_cast<VerifyFinger*>(args.GetParam());
			wxCommandEvent ev(wxEVT_VERIFYPAGE_OPEN_IMAGE_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void VerifyFinger::OnCreateTemplateCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subjectCurrent.GetStatus();
				if (status != nbsOk)
				{
					wxString sStatus = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("Template was not extracted: %s", sStatus), "Error", wxICON_ERROR);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
			EnableCheckBoxes();
			EnableVerifyButton();
		}

		void VerifyFinger::OnButtonOpenLeftClick(wxCommandEvent& WXUNUSED(event))
		{
			m_checkBoxShowBinarizedImageLeft->Enable(false);
			OpenTemplateOrImage(m_fingerViewLeft, &m_subjectLeft, m_staticTextLeftImagePath);
		}

		void VerifyFinger::OnButtonOpenRightClick(wxCommandEvent& WXUNUSED(event))
		{
			m_checkBoxShowBinarizedImageRight->Enable(false);
			OpenTemplateOrImage(m_fingerViewRight, &m_subjectRight, m_staticTextRightImagePath);
		}

		void VerifyFinger::OnButtonVerifyClick(wxCommandEvent& WXUNUSED(event))
		{
			m_fingerViewLeft->SetSpanningTree(SpanningTree());
			m_fingerViewRight->SetSpanningTree(SpanningTree());
			m_staticTextMatchedTemplateScore->SetLabelText(wxEmptyString);

			double threshold;
			try
			{
				threshold = Utils::MatchingThresholdFromString(m_comboBoxFAR->GetValue());
			}
			catch (NError ex)
			{
				wxMessageBox("FAR is not valid", "Error", wxICON_ERROR);
				return;
			}
			m_biometricClient.SetMatchingThreshold(threshold);
			m_comboBoxFAR->SetValue(Utils::MatchingThresholdToString(m_biometricClient.GetMatchingThreshold()));
			m_biometricClient.SetMatchingWithDetails(true);
			NAsyncOperation operation = m_biometricClient.VerifyAsync(m_subjectLeft, m_subjectRight);
			operation.AddCompletedCallback(&VerifyFinger::OnVerifyCompletedCallback, this);
			m_buttonVerify->Enable(false);
		}

		void VerifyFinger::OnVerifyCompletedCallback(const EventArgs & args)
		{
			VerifyFinger *panel = reinterpret_cast<VerifyFinger*>(args.GetParam());
			wxCommandEvent ev(wxEVT_VERIFYPAGE_VERIFY_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void VerifyFinger::OnVerifyCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subjectLeft.GetStatus();
				wxString verificationStatus = wxString::Format("Verification status: %s", wxString(NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status)));
				if (status != nbsOk)
				{
					wxMessageBox(verificationStatus, "Error", wxICON_ERROR);
				}
				int score = 0;
				wxString msg = wxEmptyString;
				if (status == nbsOk && m_subjectLeft.GetMatchingResults().GetCount() > 0)
				{
					score = m_subjectLeft.GetMatchingResults().Get(0).GetScore();
					msg = wxString::Format("Score of matched templates: %d", score);
					wxMessageBox(wxString::Format("%s\n%s", msg, verificationStatus));
					NMatchingDetails matchingDetails = NMatchingDetails(m_subjectLeft.GetMatchingResults().Get(0).GetMatchingDetails());
					std::list<NIndexPair> matedMinutiae = wxNFingerView::GetMatedMinutiae(matchingDetails);
					SpanningTree spanningTree = wxNFingerView::CalculateSpanningTree(m_subjectLeft.GetFingers().Get(0), matedMinutiae);
					m_fingerViewLeft->SetSpanningTree(spanningTree);
					m_fingerViewRight->SetSpanningTree(wxNFingerView::InverseSpanningTree(m_subjectLeft.GetFingers().Get(0), matchingDetails, spanningTree));
				}
				m_staticTextMatchedTemplateScore->SetLabelText(msg);
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void VerifyFinger::OnCheckBoxShowProsessedImageLeftClick(wxCommandEvent& WXUNUSED(event))
		{
			m_fingerViewLeft->SetShownImage(m_checkBoxShowBinarizedImageLeft->IsChecked() ? wxNFrictionRidgeView::PROCESSED_IMAGE : wxNFrictionRidgeView::ORIGINAL_IMAGE);
		}

		void VerifyFinger::OnCheckBoxShowProsessedImageRightClick(wxCommandEvent& WXUNUSED(event))
		{
			m_fingerViewRight->SetShownImage(m_checkBoxShowBinarizedImageRight->IsChecked() ? wxNFrictionRidgeView::PROCESSED_IMAGE : wxNFrictionRidgeView::ORIGINAL_IMAGE);
		}

		void VerifyFinger::OnButtonDefaultFARClick(wxCommandEvent& WXUNUSED(event))
		{
			m_comboBoxFAR->SetValue(Utils::MatchingThresholdToString(m_defaultFar));
			m_buttonDefaultFAR->Enable(false);
		}

		void VerifyFinger::OnComboBoxFARChange(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonDefaultFAR->Enable(true);
			EnableVerifyButton();
		}

		void VerifyFinger::OnButtonClearImagesClick(wxCommandEvent& WXUNUSED(event))
		{
			m_subjectLeft = NULL;
			m_subjectRight = NULL;
			m_fingerViewLeft->SetFinger(NULL);
			m_fingerViewRight->SetFinger(NULL);
			m_staticTextLeftImagePath->SetLabelText(wxEmptyString);
			m_staticTextRightImagePath->SetLabelText(wxEmptyString);

			m_buttonVerify->Enable(false);
			m_checkBoxShowBinarizedImageLeft->Enable(false);
			m_checkBoxShowBinarizedImageRight->Enable(false);
		}
	}
}
