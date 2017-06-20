#include "Precompiled.h"
#include "Resources/SaveIcon.xpm"
#include "Resources/OpenFolderIcon.xpm"
#include "SegmentFingerprints.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Images;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_SEGMENTPAGE_SEGMENT_COMPLETE)
		BEGIN_EVENT_TABLE(SegmentFingerprints, wxPanel)
			EVT_LISTBOX(ID_LISTBOX_POSITION, SegmentFingerprints::OnListboxPositionChange)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, SegmentFingerprints::OnButtonOpenImageClick)
			EVT_BUTTON(ID_BUTTON_SEGMENT, SegmentFingerprints::OnButtonSegmentClick)
			EVT_BUTTON(ID_BUTTON_SAVE_SEGMENTS, SegmentFingerprints::OnButtonSaveSegmentsClick)
			EVT_COMMAND(wxID_ANY, wxEVT_SEGMENTPAGE_SEGMENT_COMPLETE, SegmentFingerprints::OnSegmentCompleted)
		END_EVENT_TABLE()

		SegmentFingerprints::SegmentFingerprints(wxWindow *parent, const NBiometricClient & biometricClient, wxWindowID id, const wxPoint & pos, const wxSize & size, long style, const wxString & name)
		: wxPanel(parent, id, pos, size, style, name), m_biometricClient(biometricClient), m_subject(NULL), m_image(NULL)
		{
			CreateGUIControls();
			ClearSegments();
		}

		void SegmentFingerprints::CreateGUIControls()
		{
			wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
			wxString licences = "Biometrics.FingerSegmentation";
			wxString licencesOptional = "Biometrics.FingerQualityAssessmentBase,Images.WSQ";
			LicensePanel *licencePanel;
			licencePanel = new LicensePanel(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxBORDER_SIMPLE);
			licencePanel->RefreshComponentsStatus(licences, licencesOptional);
			mainSizer->Add(licencePanel, 0, wxALL | wxEXPAND, 2);

			wxBoxSizer *boxSizerPositionSettings = new wxBoxSizer(wxHORIZONTAL);
			wxBoxSizer *boxSizerPositionSelection = new wxBoxSizer(wxVERTICAL);
			wxStaticText *textPosition = new wxStaticText(this, wxID_ANY, "Position");
			NFPosition plainFingerPositions[] = 
			{ 
				nfpPlainLeftFourFingers,
				nfpPlainRightFourFingers,
				nfpPlainThumbs,
				nfpLeftLittle,
				nfpLeftRing,
				nfpLeftMiddle,
				nfpLeftIndex,
				nfpLeftThumb,
				nfpRightThumb,
				nfpRightIndex,
				nfpRightMiddle,
				nfpRightRing,
				nfpRightLittle
			};

			m_listBoxPosition = new wxListBox(this, ID_LISTBOX_POSITION);
			for (size_t i = 0; i < WXSIZEOF(plainFingerPositions); i++)
			{
				m_listBoxPosition->Insert(NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), plainFingerPositions[i]), i);
			}

			m_listBoxPosition->SetMaxSize(wxSize(m_listBoxPosition->GetMaxWidth(), 75));
			boxSizerPositionSelection->Add(textPosition, 0, wxALL, 2);
			boxSizerPositionSelection->Add(m_listBoxPosition, 1, wxEXPAND | wxALL, 2);
			boxSizerPositionSettings->Add(boxSizerPositionSelection, 1, wxEXPAND | wxALL, 2);
			wxBoxSizer *boxSizerMissingPositions = new wxBoxSizer(wxVERTICAL);
			wxStaticText *staticTextMissingPositions = new wxStaticText(this, wxID_ANY, "Missing positions");
			m_checkListBoxMissingPositions = new wxCheckListBox(this, wxID_ANY);
			m_checkListBoxMissingPositions->SetMaxSize(wxSize(m_checkListBoxMissingPositions->GetMaxWidth(), 75));
			boxSizerMissingPositions->Add(staticTextMissingPositions, 0, wxALL, 2);
			boxSizerMissingPositions->Add(m_checkListBoxMissingPositions, 1, wxEXPAND | wxALL, 2);
			boxSizerPositionSettings->Add(boxSizerMissingPositions, 1, wxEXPAND | wxALL, 2);
			wxBoxSizer *boxSizerPositionControls = new wxBoxSizer(wxVERTICAL);
			wxButton *buttonOpenImage = new wxButton(this, ID_BUTTON_OPEN_IMAGE, "Open Image", wxDefaultPosition, wxSize(100, 25));
			buttonOpenImage->SetBitmap(wxImage(openFolderIcon_xpm));
			buttonOpenImage->SetToolTip("Open fingerprint image to segment");
			m_buttonSegment = new wxButton(this, ID_BUTTON_SEGMENT, "Segment", wxDefaultPosition, wxSize(100, 25));
			m_buttonSegment->SetToolTip("Segment fingerprint image");
			boxSizerPositionControls->Add(buttonOpenImage, 0, wxALL | wxALIGN_BOTTOM, 2);
			boxSizerPositionControls->Add(m_buttonSegment, 0, wxALL | wxALIGN_BOTTOM, 2);
			boxSizerPositionSettings->Add(boxSizerPositionControls, 0, wxALL | wxALIGN_BOTTOM, 2);
			mainSizer->Add(boxSizerPositionSettings, 0, wxALL | wxEXPAND, 2);

			m_zoomSlider = new wxNViewZoomSlider(this);
			m_fingerView = new wxNFingerView(this, wxID_ANY);
			mainSizer->Add(m_fingerView, 1, wxALL | wxEXPAND, 2);

			wxBoxSizer *boxSizerSubviewPanel = new wxBoxSizer(wxHORIZONTAL);
			for (int i = 0; i < 4; i++)
			{
				m_subFingerView[i] = new wxNFingerView(this);
				wxSizer *sizer = new wxBoxSizer(wxVERTICAL);
				sizer->AddStretchSpacer();
				for (int j = 0; j < 3; j++)
				{
					m_subFingerViewInfo[i][j] = new wxStaticText(m_subFingerView[i], wxID_ANY, wxEmptyString);
					sizer->Add(m_subFingerViewInfo[i][j], 0, wxALL, 0);
				}
				m_subFingerView[i]->SetSizer(sizer);
				boxSizerSubviewPanel->Add(m_subFingerView[i], 1, wxEXPAND | wxALL, 2);
			}
			mainSizer->Add(boxSizerSubviewPanel, 1, wxALL | wxEXPAND, 2);

			wxBoxSizer *boxSizerImageControls = new wxBoxSizer(wxHORIZONTAL);
			m_buttonSaveImage = new wxButton(this, ID_BUTTON_SAVE_SEGMENTS, "Save Images", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveImage->SetBitmap(wxImage(saveIcon_xpm));
			m_buttonSaveImage->SetToolTip("Save segmented fingerprint images to files");
			m_buttonSaveImage->Disable();
			m_staticTextSegmntStatus = new wxStaticText(this, wxID_ANY, wxEmptyString);
			m_zoomSlider->SetView(m_fingerView);
			m_fingerView->SetZoomToFit(false);

			boxSizerImageControls->Add(m_buttonSaveImage, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerImageControls->Add(m_staticTextSegmntStatus, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerImageControls->AddStretchSpacer();
			boxSizerImageControls->Add(m_zoomSlider, 0, wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(boxSizerImageControls, 0, wxALL | wxEXPAND, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizerAndFit(mainSizer);

			m_listBoxPosition->SetSelection(0);
			wxCommandEvent ev = wxEVT_NULL;
			OnListboxPositionChange(ev);
		}

		void SegmentFingerprints::OnListboxPositionChange(wxCommandEvent& WXUNUSED(event))
		{
			NFPosition position = (NFPosition) NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), m_listBoxPosition->GetStringSelection());
			m_checkListBoxMissingPositions->Clear();
			if (!NBiometricTypes::IsPositionSingleFinger(position))
			{
				NArrayWrapper<NFPosition> array = NBiometricTypes::GetPositionAvailableParts(position, NULL, 0);
				for (NArrayWrapper<NFPosition>::iterator iterator = array.begin(); iterator < array.end(); iterator++)
				{
					wxString position = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), *iterator);
					m_checkListBoxMissingPositions->Insert(position, 0);
				}
			}
		}

		void SegmentFingerprints::OnButtonOpenImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog openFileDialog(this, "Choose fingers image", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST | wxFD_MULTIPLE);
			if (openFileDialog.ShowModal() == wxID_OK)
			{
				m_buttonSaveImage->Enable(false);
				ClearSegments();
				m_fingerView->SetFinger(NULL);
				m_image = NImage::FromFile(openFileDialog.GetPath());
				Segment();
			}
		}

		void SegmentFingerprints::Segment()
		{
			if (!m_image.IsNull())
			{
				m_subject = NSubject();
				NFinger finger = NFinger();
				finger.SetImage(m_image);
				m_subject.GetFingers().Add(finger);
				m_fingerView->SetFinger(finger);

				int index = m_listBoxPosition->GetSelection();
				wxString stringPosition = m_listBoxPosition->GetString(index);
				NFPosition pos = (NFPosition)NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), stringPosition);
				finger.SetPosition(pos);
				// Add Missing Positions
				wxArrayInt intArrayMissing;
				m_checkListBoxMissingPositions->GetCheckedItems(intArrayMissing);
				for (size_t i = 0; i < intArrayMissing.Count(); i++)
				{
					wxString stringMissingPosition = m_checkListBoxMissingPositions->GetString(intArrayMissing[i]);
					NFPosition missingPos = (NFPosition) NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), stringMissingPosition);
					m_subject.GetMissingFingers().Add(missingPos);
				}
				m_biometricClient.SetFingersDeterminePatternClass(true);
				m_biometricClient.SetFingersCalculateNfiq(true);
				NBiometricTask task = m_biometricClient.CreateTask((NBiometricOperations)(nboCreateTemplate | nboSegment | nboAssessQuality), m_subject);
				NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
				operation.AddCompletedCallback(&SegmentFingerprints::OnSegmentationCompletedCallback, this);
			}
		}

		void SegmentFingerprints::OnSegmentationCompletedCallback(const EventArgs & args)
		{
			SegmentFingerprints *Segment_panel = reinterpret_cast<SegmentFingerprints*>(args.GetParam());
			wxCommandEvent ev(wxEVT_SEGMENTPAGE_SEGMENT_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(Segment_panel, ev);
		}

		void SegmentFingerprints::OnSegmentCompleted(wxCommandEvent& event)
		{
			NBiometricStatus status = m_subject.GetStatus();
			wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
			SetLabel("Segmentation status: " + statusString);
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				if (status == nbsOk)
				{
					m_staticTextSegmntStatus->SetForegroundColour(wxColor(0, 155, 0));
					ShowSegments();
				}
				else
				{
					m_staticTextSegmntStatus->SetForegroundColour(wxColor(155, 0, 0));
				}
				m_staticTextSegmntStatus->SetLabel("Segmentation status: " + statusString);
			}
			else
			{
				m_staticTextSegmntStatus->SetLabel("Segmentation status: " + operation.GetError().ToString());
			}
			m_buttonSaveImage->Enable(status == nbsOk);
			m_fingerView->SetFinger(m_subject.GetFingers().Get(0));
		}

		void SegmentFingerprints::OnButtonSegmentClick(wxCommandEvent& WXUNUSED(event))
		{
			if (!m_image.IsNull())
			{
				Segment();
			}
			else
			{
				wxMessageBox("No image selected!", "Error", wxICON_ERROR);
			}
		}

		void SegmentFingerprints::ClearSegments()
		{
			for (int i = 0; i < 4; i++)
			{
				m_subFingerView[i]->SetFinger(NULL);
				m_subFingerViewInfo[i][0]->SetLabelText("Position:");
				m_subFingerViewInfo[i][1]->SetLabelText("Quality:");
				m_subFingerViewInfo[i][2]->SetLabelText("Class:");
			}
		}
		void SegmentFingerprints::ShowSegments()
		{
			ClearSegments();
			if (!m_subject.IsNull() && m_subject.GetFingers().GetCount() > 1)
			{
				for (int i = 1; i < m_subject.GetFingers().GetCount() && i < 5; i++)
				{
					NFinger fingerSegment = m_subject.GetFingers().Get(i);
					if (fingerSegment.GetStatus() == nbsOk)
					{
						NFAttributes attributes = fingerSegment.GetObjects().Get(0);
						wxString position = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), attributes.GetPosition());
						wxString patternClass = NEnum::ToString(NBiometricTypes::NfiqQualityNativeTypeOf(), attributes.GetNfiqQuality());
						wxString quality = NEnum::ToString(NBiometricTypes::NFPatternClassNativeTypeOf(), attributes.GetPatternClass());
						m_subFingerViewInfo[i - 1][0]->SetLabelText(wxString::Format("Position: %s", position));
						m_subFingerViewInfo[i - 1][1]->SetLabelText(wxString::Format("Quality: %s", patternClass));
						m_subFingerViewInfo[i - 1][2]->SetLabelText(wxString::Format("Class: %s", quality));
						m_subFingerView[i - 1]->SetFinger(fingerSegment);
						m_subFingerView[i - 1]->SetQualityVisualStyle(wxNFrictionRidgeView::NONE);
					}
				}
			}
		}

		void SegmentFingerprints::OnButtonSaveSegmentsClick(wxCommandEvent& WXUNUSED(event))
		{
			wxDirDialog dialog(this, "Save Images");
			if (dialog.ShowModal() == wxID_OK)
			{
				try
				{
					for (int i = 1; i < m_subject.GetFingers().GetCount(); i++)
					{
						NFinger finger = m_subject.GetFingers().Get(i);
						if (finger.GetStatus() == nbsOk)
						{
							wxString strPosition = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), finger.GetPosition());
							wxFileName fileName(dialog.GetPath(), wxString::Format("finger%d %s.%s", i, strPosition, ".png"));
							finger.GetImage().Save(fileName.GetFullPath());
						}
					}
				}
				catch (NError& ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}
	}
}
