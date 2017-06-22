#include "Precompiled.h"
#include "SegmentIris.h"
#include "Resources/OpenFolderIcon.xpm"
#include "Resources/SaveIcon.xpm"

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Images;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;

#define	SEGMENTATION_REQUIRED_LICENSE_COMPONENTS "Biometrics.IrisExtraction,Biometrics.IrisSegmentation"

DEFINE_EVENT_TYPE(wxEVT_SEGMENTPAGE_SEGMENT_COMPLETE)

namespace Neurotec
{
	namespace Samples
	{
		BEGIN_EVENT_TABLE(SegmentIris, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, SegmentIris::OnButtonOpenImageClick)
			EVT_BUTTON(ID_BUTTON_SEGMENT, SegmentIris::OnButtonSegmentClick)
			EVT_BUTTON(ID_BUTTON_SAVE_SEGMENTED_IMAGE, SegmentIris::OnButtonSaveImageClick)
			EVT_COMMAND(wxID_ANY, wxEVT_SEGMENTPAGE_SEGMENT_COMPLETE, SegmentIris::OnSegmentCompleted)
		END_EVENT_TABLE()

		SegmentIris::SegmentIris(wxWindow *parent, const NBiometricClient & biometricClient)
			: wxPanel(parent), m_biometricClient(biometricClient), m_subject(NULL), m_iris(NULL), m_segmentedIris(NULL)
		{
			CreateGUIControls();
		}

		SegmentIris::~SegmentIris()
		{
		}

		void SegmentIris::CreateGUIControls()
		{
			wxBoxSizer *boxSizerMain = new wxBoxSizer(wxHORIZONTAL);
			wxSplitterWindow *splitterMain = new wxSplitterWindow(this);
			wxPanel *pannelLower = new wxPanel(splitterMain);
			wxPanel *pannelUpper = new wxPanel(splitterMain);

			wxBoxSizer *boxSizerMainUpper = new wxBoxSizer(wxVERTICAL);
			LicensePanel *licencePanel = new LicensePanel(pannelUpper);
			licencePanel->RefreshComponentsStatus(SEGMENTATION_REQUIRED_LICENSE_COMPONENTS, wxEmptyString);
			boxSizerMainUpper->Add(licencePanel, 0, wxEXPAND | wxALL, 2);

			wxButton *buttonOpenImage = new wxButton(pannelUpper, ID_BUTTON_OPEN_IMAGE, "Open Image", wxDefaultPosition, wxSize(-1, 25));
			buttonOpenImage->SetBitmap(wxImage(openFolderIcon_xpm));
			buttonOpenImage->SetToolTip("Open iris image");
			boxSizerMainUpper->Add(buttonOpenImage, 0, wxALL, 2);

			m_zoomSliderOriginal = new wxNViewZoomSlider(pannelLower);
			m_irisView = new wxNIrisView(pannelUpper);
			m_zoomSliderOriginal->SetView(m_irisView);
			boxSizerMainUpper->Add(m_irisView, 1, wxEXPAND | wxALL, 2);

			pannelUpper->SetSizerAndFit(boxSizerMainUpper);

			wxBoxSizer *boxSizerMainLower = new wxBoxSizer(wxVERTICAL);
			wxBoxSizer *boxSizerSegment = new wxBoxSizer(wxHORIZONTAL);
			m_buttonSegmentIris = new wxButton(pannelLower, ID_BUTTON_SEGMENT, "Segment Iris", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSegmentIris->Enable(false);
			boxSizerSegment->Add(m_buttonSegmentIris, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerSegment->AddStretchSpacer();
			boxSizerSegment->Add(m_zoomSliderOriginal, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerMainLower->Add(boxSizerSegment, 0, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer *staticBoxResults = new wxStaticBoxSizer(wxVERTICAL, pannelLower, "Result");
			wxFlexGridSizer *irisAttributesSizer = new wxFlexGridSizer(4, 6, 2, 2);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Quality:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlQuality = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlQuality);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Iris Sclera Contrast:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlIrisSclareContrast = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlIrisSclareContrast);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Sharpness:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlSharpness = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlSharpness);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Pupil To Iris Ratio:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlPupilToIrisRatio = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlPupilToIrisRatio);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Iris Pupil Contrast:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlIrisPupilContrast = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlIrisPupilContrast);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Interlance:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlInterlance = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlInterlance);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Usable Iris Area:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlUsableIrisArea = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlUsableIrisArea);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Iris Pupil Contrenticity:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlIrisPupilConcentricity = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlIrisPupilConcentricity);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Margin Adequacy:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlMargineAdequacy = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlMargineAdequacy);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Gray Scale Utilization:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlGrayScaleUtilisation = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlGrayScaleUtilisation);
			irisAttributesSizer->Add(new wxStaticText(pannelLower, wxID_ANY, "Pupil Boundary Circularity:"), 0, wxALIGN_RIGHT | wxALIGN_CENTER_VERTICAL | wxLEFT, 20);
			m_textCtrlPupilBoundaryCircularity = new wxTextCtrl(pannelLower, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_READONLY);
			irisAttributesSizer->Add(m_textCtrlPupilBoundaryCircularity);
			staticBoxResults->Add(irisAttributesSizer, 0, wxALL, 2);

			m_zoomSliderSegmented = new wxNViewZoomSlider(pannelLower);
			m_irisViewSegmented = new wxNIrisView(pannelLower);
			m_zoomSliderSegmented->SetView(m_irisViewSegmented);
			staticBoxResults->Add(m_irisViewSegmented, 1, wxEXPAND | wxALL, 2);
			boxSizerMainLower->Add(staticBoxResults, 1, wxEXPAND | wxALL, 2);

			wxBoxSizer *boxSizerSave = new wxBoxSizer(wxHORIZONTAL);
			m_buttonSaveImage = new wxButton(pannelLower, ID_BUTTON_SAVE_SEGMENTED_IMAGE, "Save Image", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveImage->SetBitmap(wxImage(saveIcon_xpm));
			m_buttonSaveImage->SetToolTip("Save segmented iris image");
			m_buttonSaveImage->Enable(false);
			boxSizerSave->Add(m_buttonSaveImage, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerSave->AddStretchSpacer();
			boxSizerSave->Add(m_zoomSliderSegmented, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerMainLower->Add(boxSizerSave, 0, wxALL | wxEXPAND, 2);

			SetBackgroundColour(GetParent()->GetBackgroundColour());

			pannelLower->SetSizerAndFit(boxSizerMainLower);
			splitterMain->SplitHorizontally(pannelUpper, pannelLower);
			boxSizerMain->Add(splitterMain,1,wxEXPAND);
			SetSizerAndFit(boxSizerMain);
		}

		void SegmentIris::ClearSegmentationData()
		{
			m_textCtrlQuality->Clear();
			m_textCtrlPupilToIrisRatio->Clear();
			m_textCtrlUsableIrisArea->Clear();
			m_textCtrlGrayScaleUtilisation->Clear();
			m_textCtrlIrisSclareContrast->Clear();
			m_textCtrlIrisPupilContrast->Clear();
			m_textCtrlIrisPupilConcentricity->Clear();
			m_textCtrlPupilBoundaryCircularity->Clear();
			m_textCtrlSharpness->Clear();
			m_textCtrlInterlance->Clear();
			m_textCtrlMargineAdequacy->Clear();
			m_irisView->SetIris(NULL);
			m_irisViewSegmented->SetIris(NULL);
		}

		void SegmentIris::OnButtonOpenImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog openFileDialog(this, "Open", wxEmptyString, wxEmptyString, Common::GetOpenFileFilterString(true, false), wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openFileDialog.ShowModal() == wxID_OK)
			{
				try
				{
					m_buttonSaveImage->Enable(false);
					ClearSegmentationData();
					m_iris = NIris();
					m_iris.SetImage(NImage::FromFile(openFileDialog.GetPath()));
					m_irisView->SetIris(m_iris);
					m_buttonSegmentIris->Enable(true);
				}
				catch (NError & ex)
				{
					m_iris = NULL;
					m_buttonSegmentIris->Enable(false);
					wxExceptionDlg::Show(ex);
				}
			}
		}

		void SegmentIris::OnButtonSaveImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxString caption = "Choose a file";
			wxFileDialog dialog(this, caption, wxEmptyString, wxEmptyString, Common::GetSaveFileFilterString(), wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			if (dialog.ShowModal() == wxID_OK)
			{
				try
				{
					m_segmentedIris.GetImage().Save(dialog.GetPath());
				}
				catch (NError & ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}

		void SegmentIris::OnButtonSegmentClick(wxCommandEvent& WXUNUSED(event))
		{
			m_subject = NSubject();
			m_subject.GetIrises().Add(m_iris);
			m_iris.SetImageType(neitCroppedAndMasked);
			NBiometricTask segmentTask = m_biometricClient.CreateTask(nboSegment, m_subject);
			NAsyncOperation operation = m_biometricClient.PerformTaskAsync(segmentTask);
			operation.AddCompletedCallback(&SegmentIris::OnSegmentCompletedCallback, this);
			m_buttonSegmentIris->Enable(false);
		}

		void SegmentIris::OnSegmentCompletedCallback(const EventArgs & args)
		{
			SegmentIris *segmentPanel = reinterpret_cast<SegmentIris *>(args.GetParam());
			wxCommandEvent ev(wxEVT_SEGMENTPAGE_SEGMENT_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(segmentPanel, ev);
		}

		void SegmentIris::OnSegmentCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			NBiometricTask task = operation.GetResult().ToObject<NBiometricTask>();
			NBiometricStatus status = task.GetStatus();
			if (operation.GetError().IsNull())
			{
				if (status == nbsOk)
				{
					NEAttributes attributes = m_iris.GetObjects()[0];
					m_textCtrlQuality->SetLabelText(wxString::Format("%d", attributes.GetQuality()));
					m_textCtrlPupilToIrisRatio->SetLabelText(wxString::Format("%d", attributes.GetPupilToIrisRatio()));
					m_textCtrlUsableIrisArea->SetLabelText(wxString::Format("%d", attributes.GetUsableIrisArea()));
					m_textCtrlGrayScaleUtilisation->SetLabelText(wxString::Format("%d", attributes.GetGrayScaleUtilisation()));
					m_textCtrlIrisSclareContrast->SetLabelText(wxString::Format("%d", attributes.GetIrisScleraContrast()));
					m_textCtrlIrisPupilContrast->SetLabelText(wxString::Format("%d", attributes.GetIrisPupilContrast()));
					m_textCtrlIrisPupilConcentricity->SetLabelText(wxString::Format("%d", attributes.GetIrisPupilConcentricity()));
					m_textCtrlPupilBoundaryCircularity->SetLabelText(wxString::Format("%d", attributes.GetPupilBoundaryCircularity()));
					m_textCtrlSharpness->SetLabelText(wxString::Format("%d", attributes.GetSharpness()));
					m_textCtrlInterlance->SetLabelText(wxString::Format("%d", attributes.GetInterlace()));
					m_textCtrlMargineAdequacy->SetLabelText(wxString::Format("%d", attributes.GetMarginAdequacy()));

					m_segmentedIris = NObjectDynamicCast<NIris>(attributes.GetChild());
					m_irisViewSegmented->SetIris(m_segmentedIris);

					m_buttonSegmentIris->Enable(false);
					m_buttonSaveImage->Enable(true);
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox("Segmentation failed: "+statusString+".", "Error", wxOK | wxICON_ERROR);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}
	}
}
