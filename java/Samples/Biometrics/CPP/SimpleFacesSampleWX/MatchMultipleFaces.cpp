#include "Precompiled.h"
#include "MatchMultipleFaces.h"
#include "Resources/OpenFolderIcon.xpm"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(EVT_MULTIFACEPAGE_EXTRACT_REFERENCE_COMPLETED);
		DEFINE_EVENT_TYPE(EVT_MULTIFACEPAGE_EXTRACT_MULTIFACE_COMPLETED);
		DEFINE_EVENT_TYPE(EVT_MULTIFACEPAGE_ENROLL_COMPLETED);
		DEFINE_EVENT_TYPE(EVT_MULTIFACEPAGE_IDENTIFY_COMPLETED);
		BEGIN_EVENT_TABLE(MatchMultipleFaces, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_REFERENCE_IMAGE, MatchMultipleFaces::OnButtonOpenReferenceImageClick)
			EVT_BUTTON(ID_BUTTON_OPEN_MULTIFACE_IMAGE, MatchMultipleFaces::OnButtonOpenMultifaceImageClick)
			EVT_COMMAND(wxID_ANY, EVT_MULTIFACEPAGE_EXTRACT_REFERENCE_COMPLETED, MatchMultipleFaces::OnExtractReferenceCompleted)
			EVT_COMMAND(wxID_ANY, EVT_MULTIFACEPAGE_EXTRACT_MULTIFACE_COMPLETED, MatchMultipleFaces::OnExtractMultifaceCompleted)
			EVT_COMMAND(wxID_ANY, EVT_MULTIFACEPAGE_ENROLL_COMPLETED, MatchMultipleFaces::OnEnrollCompleted)
			EVT_COMMAND(wxID_ANY, EVT_MULTIFACEPAGE_IDENTIFY_COMPLETED, MatchMultipleFaces::OnIdentifyCompleted)
		END_EVENT_TABLE()

		MatchMultipleFaces::MatchMultipleFaces(wxWindow* parent, NBiometricClient &biometricClient)
		: wxPanel(parent), m_biometricClient(biometricClient), m_referenceSubject(NULL), m_multipleFacesSubject(NULL), m_enrollmentTask(NULL)
		{
			CreateGUIControls();
		}

		void MatchMultipleFaces::CreateGUIControls()
		{
			wxBoxSizer* mainBox = new wxBoxSizer(wxVERTICAL);

			LicensePanel *licensePanel = new LicensePanel(this);
			licensePanel->RefreshComponentsStatus("Biometrics.FaceExtraction,Biometrics.FaceMatching",wxEmptyString);
			mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

			wxBoxSizer *toolBarSizer = new wxBoxSizer(wxHORIZONTAL);
			m_buttonOpenReferenceImage = new wxButton(this, ID_BUTTON_OPEN_REFERENCE_IMAGE, "Open Reference Image");
			m_buttonOpenReferenceImage->SetBitmap(openFolderIcon_xpm);
			m_buttonOpenReferenceImage->SetToolTip("Open Reference Image.");
			toolBarSizer->Add(m_buttonOpenReferenceImage, 0, wxALL, 2);
			m_buttonOpenMultifaceImage = new wxButton(this, ID_BUTTON_OPEN_MULTIFACE_IMAGE, "Open Multiface Image");
			m_buttonOpenMultifaceImage->SetBitmap(openFolderIcon_xpm);
			m_buttonOpenMultifaceImage->SetToolTip("Open Multiface Image.");
			toolBarSizer->Add(m_buttonOpenMultifaceImage, 0, wxALL, 2);
			mainBox->Add(toolBarSizer, 0, wxALL | wxEXPAND, 2);

			wxFlexGridSizer *flexGridSizerView = new wxFlexGridSizer(3, 2, 5, 5);
			flexGridSizerView->AddGrowableCol(0);
			flexGridSizerView->AddGrowableCol(1);
			flexGridSizerView->AddGrowableRow(1);
			wxStaticText* staticTextReferanceImage = new wxStaticText(this, wxID_ANY, "Reference Image", wxDefaultPosition, wxDefaultSize);
			wxStaticText* staticTextMultiple = new wxStaticText(this, wxID_ANY, "Multiple Face Image", wxDefaultPosition, wxDefaultSize);
			staticTextReferanceImage->SetBackgroundColour(wxSystemSettings::GetColour(wxSYS_COLOUR_ACTIVECAPTION));
			staticTextMultiple->SetBackgroundColour(wxSystemSettings::GetColour(wxSYS_COLOUR_ACTIVECAPTION));
			wxNViewZoomSlider *zoomSliderReferance = new wxNViewZoomSlider(this);
			wxNViewZoomSlider *zoomSliderMultiple = new wxNViewZoomSlider(this);
			m_faceViewReference = new wxNFaceView(this);
			m_faceViewMultiFace = new wxNFaceView(this);
			m_faceViewReference->SetBackgroundColour(GetBackgroundColour());
			m_faceViewMultiFace->SetBackgroundColour(GetBackgroundColour());
			m_faceViewMultiFace->SetShowFaceConfidence(false);
			zoomSliderReferance->SetView(m_faceViewReference);
			zoomSliderMultiple->SetView(m_faceViewMultiFace);
			flexGridSizerView->Add(staticTextReferanceImage, 0, wxEXPAND);
			flexGridSizerView->Add(staticTextMultiple, 0, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(m_faceViewReference, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(m_faceViewMultiFace, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(zoomSliderReferance, 1, wxEXPAND | wxALL, 2);
			flexGridSizerView->Add(zoomSliderMultiple, 1, wxEXPAND | wxALL, 2);
			mainBox->Add(flexGridSizerView, 1, wxEXPAND | wxALL, 2);

			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainBox);
		}

		void MatchMultipleFaces::OnButtonOpenReferenceImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog wxfdOpenFile(this, "Open Reference Image", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (wxfdOpenFile.ShowModal() == wxID_OK)
			{
				m_faceViewMultiFace->SetFaceIds(wxArrayString());
				// Set template size (for matching medium is recommended) (optional)
				m_biometricClient.SetFacesTemplateSize(ntsMedium);
				NFace face;
				face.SetFileName(wxfdOpenFile.GetPath());
				m_referenceSubject = NSubject();
				m_referenceSubject.GetFaces().Add(face);
				m_faceViewReference->SetFace(face);
				NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_referenceSubject);
				operation.AddCompletedCallback(&MatchMultipleFaces::OnExtractReferenceCompletedCallback, this);
			}
		}

		void MatchMultipleFaces::OnButtonOpenMultifaceImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog wxfdOpenFile(this, "Open Multiface Image", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (wxfdOpenFile.ShowModal() == wxID_OK)
			{
				// Set template size (for enrolling large is recommended) (optional)
				m_biometricClient.SetFacesTemplateSize(ntsLarge);
				NFace face;
				face.SetFileName(wxfdOpenFile.GetPath());
				m_multipleFacesSubject = NSubject();
				m_multipleFacesSubject.GetFaces().Add(face);
				m_faceViewMultiFace->SetFace(face);
				m_multipleFacesSubject.SetMultipleSubjects(true);
				NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_multipleFacesSubject);
				operation.AddCompletedCallback(&MatchMultipleFaces::OnExtractMultifaceCompletedCallback, this);
			}
		}

		void MatchMultipleFaces::EnrollMultipleFaceSubject()
		{
			m_biometricClient.Clear();
			m_enrollmentTask = NBiometricTask(nboEnroll);
			m_multipleFacesSubject.SetId("firstSubject");
			m_enrollmentTask.GetSubjects().Add(m_multipleFacesSubject);
			// Enroll all faces
			for (int i = 0; i < m_multipleFacesSubject.GetRelatedSubjects().GetCount(); i++)
			{
				NSubject tmpSubject = m_multipleFacesSubject.GetRelatedSubjects()[i];
				tmpSubject.SetId(wxString::Format("relatedSubject%i", i));
				m_enrollmentTask.GetSubjects().Add(tmpSubject);
			}
			// Enroll subjects
			NAsyncOperation operation = m_biometricClient.PerformTaskAsync(m_enrollmentTask);
			operation.AddCompletedCallback(&MatchMultipleFaces::OnEnrollCompletedCallback, this);
		}

		void MatchMultipleFaces::MatchFaces()
		{
			NAsyncOperation operation = m_biometricClient.IdentifyAsync(m_referenceSubject);
			operation.AddCompletedCallback(&MatchMultipleFaces::OnIdentifyCompletedCallback, this);
		}

		void MatchMultipleFaces::OnExtractReferenceCompletedCallback(EventArgs args)
		{
			MatchMultipleFaces* panel = reinterpret_cast<MatchMultipleFaces*>(args.GetParam());
			wxCommandEvent ev(EVT_MULTIFACEPAGE_EXTRACT_REFERENCE_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void MatchMultipleFaces::OnExtractMultifaceCompletedCallback(EventArgs args)
		{
			MatchMultipleFaces* panel = reinterpret_cast<MatchMultipleFaces*>(args.GetParam());
			wxCommandEvent ev(EVT_MULTIFACEPAGE_EXTRACT_MULTIFACE_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void MatchMultipleFaces::OnEnrollCompletedCallback(EventArgs args)
		{
			MatchMultipleFaces* panel = reinterpret_cast<MatchMultipleFaces*>(args.GetParam());
			wxCommandEvent ev(EVT_MULTIFACEPAGE_ENROLL_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void MatchMultipleFaces::OnIdentifyCompletedCallback(EventArgs args)
		{
			MatchMultipleFaces* panel = reinterpret_cast<MatchMultipleFaces*>(args.GetParam());
			wxCommandEvent ev(EVT_MULTIFACEPAGE_IDENTIFY_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void MatchMultipleFaces::OnExtractReferenceCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_referenceSubject.GetStatus();
				if (status != nbsOk)
				{
					wxMessageBox("Could not extract template from reference image.", wxMessageBoxCaptionStr, wxOK | wxICON_ERROR);
					m_referenceSubject = NULL;
				}
				else if (!m_multipleFacesSubject.IsNull())
				{
					EnrollMultipleFaceSubject();
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
				m_referenceSubject = NULL;
			}
		}

		void MatchMultipleFaces::OnExtractMultifaceCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_multipleFacesSubject.GetStatus();
				if (status != nbsOk )
				{
					wxMessageBox("Could not extract template from multiple face image.", wxMessageBoxCaptionStr, wxOK | wxICON_ERROR);
					m_multipleFacesSubject = NULL;
				}
				else if (!m_referenceSubject.IsNull())
				{
					EnrollMultipleFaceSubject();
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
				m_multipleFacesSubject = NULL;
			}
		}

		void MatchMultipleFaces::OnEnrollCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_enrollmentTask.GetStatus();
				if (status != nbsOk)
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("Enroll failed: %s", statusString), wxMessageBoxCaptionStr, wxOK | wxICON_ERROR);
				}
				else
				{
					MatchFaces();
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void MatchMultipleFaces::OnIdentifyCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_referenceSubject.GetStatus();
				if (status == nbsOk || status == nbsMatchNotFound)
				{
					int multipleFacesCount = m_enrollmentTask.GetSubjects().GetCount();
					wxArrayString results;
					results.SetCount(multipleFacesCount);
					NSubject::MatchingResultCollection resultCollection = m_referenceSubject.GetMatchingResults();
					for (int i = 0; i < resultCollection.GetCount(); i++)
					{
						NMatchingResult result = resultCollection.Get(i);
						int score = result.GetScore();
						for (int j = 0; j < multipleFacesCount; j++)
						{
							if (result.GetId() == m_enrollmentTask.GetSubjects().Get(j).GetId())
							{
								results.Item(j) = wxString::Format("score: %i (match)", score);
							}
						}
					}
					for (int i = 0; i < multipleFacesCount; i++)
					{
						if (results[i] == wxEmptyString)
							results[i] = "score: 0";
					}
					m_faceViewMultiFace->SetFaceIds(results);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}
	}
}
