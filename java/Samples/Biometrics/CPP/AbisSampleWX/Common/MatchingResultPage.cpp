#include "Precompiled.h"

#include <Common/MatchingResultPage.h>
#include <Common/SubjectUtils.h>
#include <Common/EnrollDataSerializer.h>
#include <Settings/SettingsManager.h>

#include <math.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Images;
using namespace Neurotec::IO;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_MATCHING_RESULT_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_MATCHING_RESULT_THREAD, wxCommandEvent);

template <class T1, class T2>
MatchingResultPage::MatchedPair<T1, T2>::MatchedPair(T1 first, T1 second, T2 details) : m_first(first), m_second(second), m_details(details)
{
	m_type = wxT("MatchedPair");
}

template <class T1, class T2>
MatchingResultPage::MatchedPair<T1, T2>::~MatchedPair()
{
}

template <class T1, class T2>
wxString MatchingResultPage::MatchedPair<T1, T2>::GetType()
{
	return m_type;
}

template <class T1, class T2>
void MatchingResultPage::MatchedPair<T1, T2>::SetFirst(T1 first)
{
	m_first = first;
}

template <class T1, class T2>
void MatchingResultPage::MatchedPair<T1, T2>::SetSecond(T1 second)
{
	m_second = second;
}

template <class T1, class T2>
void MatchingResultPage::MatchedPair<T1, T2>::SetDetails(T2 details)
{
	m_details = details;
}

template <class T1, class T2>
T1 MatchingResultPage::MatchedPair<T1, T2>::GetFirst()
{
	return m_first;
}

template <class T1, class T2>
T1 MatchingResultPage::MatchedPair<T1, T2>::GetSecond()
{
	return m_second;
}

template <class T1, class T2>
T2 MatchingResultPage::MatchedPair<T1, T2>::GetDetails()
{
	return m_details;
}

MatchingResultPage::MatchedFinger::MatchedFinger(NFrictionRidge f1, NFrictionRidge f2, NFMatchingDetails dt)
	: MatchingResultPage::MatchedPair<NFrictionRidge, NFMatchingDetails>(f1, f2, dt)
{
	m_type = wxT("MatchedFinger");
}

wxString MatchingResultPage::MatchedFinger::ToString()
{
	wxString strPositionFirst = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), GetFirst().GetPosition());
	wxString strPositionSecond = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), GetSecond().GetPosition());
	return wxString::Format(wxT("Probe finger(%s) matched with gallery finger(%s). Score = %d"), strPositionFirst, strPositionSecond, GetDetails().GetScore());
}

MatchingResultPage::MatchedIris::MatchedIris(NIris f1, NIris f2, NEMatchingDetails dt)
	: MatchingResultPage::MatchedPair<NIris, NEMatchingDetails>(f1, f2, dt)
{
	m_type = wxT("MatchedIris");
}

wxString MatchingResultPage::MatchedIris::ToString()
{
	wxString strPositionFirst = NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), GetFirst().GetPosition());
	wxString strPositionSecond = NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), GetSecond().GetPosition());
	return wxString::Format(wxT("Probe iris(%s) matched with gallery iris(%s). Score = %d"), strPositionFirst, strPositionSecond, GetDetails().GetScore());
}

MatchingResultPage::MatchedFace::MatchedFace(NFace f1, NFace f2, NLMatchingDetails dt)
	: MatchingResultPage::MatchedPair<NFace, NLMatchingDetails>(f1, f2, dt)
{
	m_type = wxT("MatchedFace");
}

wxString MatchingResultPage::MatchedFace::ToString()
{
	return wxString::Format(wxT("Matched faces. Score = %d"), GetDetails().GetScore());
}

MatchingResultPage::MatchedVoice::MatchedVoice(NVoice f1, NVoice f2, NSMatchingDetails dt)
	: MatchingResultPage::MatchedPair<NVoice, NSMatchingDetails>(f1, f2, dt)
{
	m_type = wxT("MatchedVoice");
}

wxString MatchingResultPage::MatchedVoice::ToString()
{
	return wxString::Format(wxT("Matched probe voice(PhraseId = %d) with gallery voice(PhraseId = %d). Score = %d"), GetFirst().GetPhraseId(),
		GetSecond().GetPhraseId(), GetDetails().GetScore());
}

MatchingResultPage::MatchingResultPage(wxWindow *parent, wxWindowID winid, NBiometricClient& biometricClient) :
	TabPage(parent, winid),
	m_biometricClient(biometricClient),
	m_probeSubject(NULL),
	m_galerySubject(NULL),
	m_matchingResult(NULL)
{
	m_isSubjectLoaded = false;
	m_leftWindow = NULL;
	m_rightWindow = NULL;

	CreateGuiElements();
	RegisterGuiEvents();
}

MatchingResultPage::~MatchingResultPage()
{
	for (unsigned int i = 0; i < m_choiceBiometric->GetCount(); i++)
	{
		if (m_choiceBiometric->GetClientData(i))
		{
			delete static_cast<MatchedPair<NBiometric, NXMatchingDetails>* >(m_choiceBiometric->GetClientData(i));
		}
	}

	UnregisterGuiEvents();
}

int MatchingResultPage::RecordIndexToFaceIndex(int index, wxArrayInt & recordCounts)
{
	int sum = 0;
	for (int i = 0; i < (int)recordCounts.size(); i++)
	{
		int item = recordCounts[i];
		if (index >= sum && index < sum + item) return i;
		sum += item;
	}
	return 0;
}

void MatchingResultPage::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVENT_GET_COMPLETED:
		{
			try
			{
				NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
				NError error = operation.GetError();
				if (error.IsNull())
				{
					SampleDbSchema schema = SettingsManager::GetCurrentSchema();
					bool hasSchema = !schema.IsEmpty();
					wxString thumbnailName = schema.thumbnailDataName;
					bool hasThumbnail = hasSchema && thumbnailName != wxEmptyString && m_galerySubject.GetProperties().Contains(thumbnailName);
					if (hasThumbnail)
					{
						NBuffer buffer = m_galerySubject.GetProperty<NBuffer>(N_T("Thumbnail"));
						NImage image = NImage::FromMemory(buffer);
						m_thumbnail->SetImage(image);
					}
					else
					{
						m_thumbnail->Hide();
					}

					wxArrayInt galeryRecordCounts;
					if (hasSchema)
					{
						NPropertyBag bag;
						m_galerySubject.CaptureProperties(bag);
						if (schema.enrollDataName != wxEmptyString && bag.Contains(schema.enrollDataName))
						{
							NBuffer templateBuffer = m_galerySubject.GetTemplateBuffer();
							NBuffer enrollData = bag.Get(schema.enrollDataName).ToObject<NBuffer>();
							m_galerySubject = EnrollDataSerializer::Deserialize(templateBuffer, enrollData, galeryRecordCounts);
						}
						if (schema.genderDataName != wxEmptyString && bag.Contains(schema.genderDataName))
						{
							wxString genderString = bag.Get(schema.genderDataName).ToString();
							NGender g = (NGender)NEnum::Parse(NBiometricTypes::NGenderNativeTypeOf(), genderString);
							bag.Set(schema.genderDataName, NValue::FromValue(NBiometricTypes::NGenderNativeTypeOf(), &g, sizeof(g)));
						}

						m_propertyGrid->SetSchema(schema);
						m_propertyGrid->SetValues(bag);
					}
					else
					{
						m_propertyGrid->Hide();
					}

					NMatchingDetails details = m_matchingResult.GetMatchingDetails();
					if (!details.IsNull())
					{
						int threshold = m_biometricClient.GetMatchingThreshold();

						//Fingers
						std::vector<NFinger> templateFingers = SubjectUtils::GetTemplateCompositeFingers(m_probeSubject);
						NMatchingDetails::FingerCollection fingers = details.GetFingers();
						NSubject::FingerCollection galeryFingers = m_galerySubject.GetFingers();
						for (int i = 0; i < fingers.GetCount(); i++)
						{
							NFMatchingDetails mDetails = fingers[i];
							if (!mDetails.IsNull() && mDetails.GetMatchedIndex() != -1 && mDetails.GetScore() >= threshold)
							{
								MatchedFinger * mFinger = new MatchedFinger(templateFingers[i], galeryFingers[mDetails.GetMatchedIndex()], mDetails);
								m_choiceBiometric->Append(mFinger->ToString(), mFinger);
							}
						}

						//Faces
						std::vector<NFace> faces = SubjectUtils::GetTemplateCompositeFaces(m_probeSubject);
						wxArrayInt recordCounts;
						for (std::vector<NFace>::iterator it = faces.begin(); it != faces.end(); it++)
						{
							recordCounts.push_back(it->GetObjects()[0].GetTemplate().GetRecords().GetCount());
						}
						NMatchingDetails::FaceCollection matchedFaces = details.GetFaces();
						for (int i = 0; i < (int)faces.size(); i++)
						{
							NLMatchingDetails mDetails = matchedFaces[i];
							if (!mDetails.IsNull() && mDetails.GetMatchedIndex() != -1 && mDetails.GetScore() >= threshold)
							{
								MatchedFace *mFace = new MatchedFace(faces[RecordIndexToFaceIndex(i, recordCounts)], m_galerySubject.GetFaces()[RecordIndexToFaceIndex(i, galeryRecordCounts)], mDetails);
								m_choiceBiometric->Append(mFace->ToString(), mFace);
							}
						}

						//Irises
						std::vector<NIris> templateIrises = SubjectUtils::GetTemplateCompositeIrises(m_probeSubject);
						NMatchingDetails::IrisCollection irises = details.GetIrises();
						for (int i = 0; i < irises.GetCount(); i++)
						{
							NEMatchingDetails mDetails = irises[i];
							if (!mDetails.IsNull() && mDetails.GetMatchedIndex() != -1 && mDetails.GetScore() >= threshold)
							{
								MatchedIris *mIris = new MatchedIris(templateIrises[i], m_galerySubject.GetIrises().Get(mDetails.GetMatchedIndex()), mDetails);
								m_choiceBiometric->Append(mIris->ToString(), mIris);
							}
						}

						//Palms
						std::vector<NPalm> templatePalms = SubjectUtils::GetTemplateCompositePalms(m_probeSubject);
						NMatchingDetails::PalmCollection palms = details.GetPalms();
						for (int i = 0; i < palms.GetCount(); i++)
						{
							NFMatchingDetails mDetails = palms[i];
							if (!mDetails.IsNull() && mDetails.GetMatchedIndex() != -1 && mDetails.GetScore() >= threshold)
							{
								MatchedFinger *mPalm = new MatchedFinger(templatePalms[i], m_galerySubject.GetPalms().Get(mDetails.GetMatchedIndex()), mDetails);
								m_choiceBiometric->Append(mPalm->ToString(), mPalm);
							}
						}

						//Voices
						std::vector<NVoice> templateVoices = SubjectUtils::GetTemplateCompositeVoices(m_probeSubject);
						NMatchingDetails::VoiceCollection voices = details.GetVoices();
						for (int i = 0; i < voices.GetCount(); i++)
						{
							NSMatchingDetails mDetails = voices[i];
							if (!mDetails.IsNull() && mDetails.GetMatchedIndex() != -1 && mDetails.GetScore() >= threshold)
							{
								MatchedVoice *mVoice = new MatchedVoice(templateVoices[i], m_galerySubject.GetVoices().Get(mDetails.GetMatchedIndex()), mDetails);
								m_choiceBiometric->Append(mVoice->ToString(), mVoice);
							}
						}

						if (m_choiceBiometric->GetCount() > 0)
						{
							m_choiceBiometric->Select(0);
							wxPostEvent(m_choiceBiometric, wxCommandEvent(wxEVT_CHOICE, m_choiceBiometric->GetId()));
						}
					}
					else
					{
						m_lblInfo->SetLabelText(wxT("Enable 'Return matching details' in settings to see more details in this tab"));
						m_lblGalery->Hide();
						m_lblProbe->Hide();
						m_choiceBiometric->Hide();
						m_lblGaleryTitle->Hide();
						m_lblProbeTitle->Hide();
						m_lblMatchedBiometrics->Hide();
						m_thumbnail->Hide();
					}
				}
				else
				{
					wxExceptionDlg::Show(error);
				}
			}
			catch(NError& error)
			{
				wxExceptionDlg::Show(error);
				m_thumbnail->Hide();
			}

			this->Layout();

			break;
		}
	default:
		break;
	};
}

void MatchingResultPage::OnBiometricSelect(wxCommandEvent&)
{
	if (m_choiceBiometric->GetSelection() == -1 || m_choiceBiometric->GetClientData(m_choiceBiometric->GetSelection()) == NULL)
	{
		return;
	}

	m_leftSizer->Clear(true);
	m_rightSizer->Clear(true);

	m_leftWindow = NULL;
	m_rightWindow = NULL;

	MatchedPair<NBiometric, NXMatchingDetails> *genericPair = static_cast<MatchedPair<NBiometric, NXMatchingDetails>* >
		(m_choiceBiometric->GetClientData(m_choiceBiometric->GetSelection()));

	if (genericPair->GetType() == wxT("MatchedFinger"))
	{
		MatchedFinger *finger = reinterpret_cast<MatchedFinger *>(genericPair);
		m_leftWindow = ShowFinger(finger->GetFirst(), m_lblProbe);
		m_rightWindow = ShowFinger(finger->GetSecond(), m_lblGalery);
	}
	else if (genericPair->GetType() == wxT("MatchedFace"))
	{
		MatchedFace *face = reinterpret_cast<MatchedFace *>(genericPair);
		m_leftWindow = ShowFace(face->GetFirst(), m_lblProbe);
		m_rightWindow = ShowFace(face->GetSecond(), m_lblGalery);
	}
	else if (genericPair->GetType() == wxT("MatchedIris"))
	{
		MatchedIris *iris = reinterpret_cast<MatchedIris *>(genericPair);
		m_leftWindow = ShowIris(iris->GetFirst(), m_lblProbe);
		m_rightWindow = ShowIris(iris->GetSecond(), m_lblGalery);
	}
	else if (genericPair->GetType() == wxT("MatchedVoice"))
	{
		MatchedVoice *voice = reinterpret_cast<MatchedVoice *>(genericPair);
		m_leftWindow = ShowVoice(voice->GetFirst(), m_lblProbe);
		m_rightWindow = ShowVoice(voice->GetSecond(), m_lblGalery);
	}

	if (m_leftWindow != NULL)
		m_leftSizer->Add(m_leftWindow, 1, wxALL | wxEXPAND);

	if (m_rightWindow != NULL)
		m_rightSizer->Add(m_rightWindow, 1, wxALL | wxEXPAND);

	this->Layout();
}

void MatchingResultPage::OnGetCompleted(EventArgs args)
{
	MatchingResultPage *page = static_cast<MatchingResultPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_MATCHING_RESULT_THREAD, ID_EVENT_GET_COMPLETED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void MatchingResultPage::SetParameters(NSubject probeSubject, NSubject galerySubject)
{
	if (probeSubject.IsNull() || galerySubject.IsNull())
	{
		return;
	}

	m_probeSubject = probeSubject;
	m_galerySubject = galerySubject;

	NSubject::MatchingResultCollection matchingResults = m_probeSubject.GetMatchingResults();
	for (int i = 0; i < matchingResults.GetCount(); i++)
	{
		NMatchingResult result = matchingResults[i];

		if (result.GetId().Equals(m_galerySubject.GetId()))
		{
			m_matchingResult = result;
			break;
		}
	}
}

void MatchingResultPage::OnSelectPage()
{
	if (m_isSubjectLoaded)
	{
		return;
	}

	m_isSubjectLoaded = true;

	wxString id = m_galerySubject.GetId();
	if (id.Length() > 30) id = id.SubString(0, 30) << wxT("...");
	m_lblInfo->SetLabelText(wxString::Format(wxT("Subject: '%s'\nScore: %d"), (wxString)id, m_matchingResult.GetScore()));
	m_lblInfo->Wrap(400);

	NAsyncOperation operation = m_biometricClient.GetAsync(m_galerySubject);
	operation.AddCompletedCallback(&MatchingResultPage::OnGetCompleted, this);

	this->Layout();
}

wxNFingerView * MatchingResultPage::ShowFinger(NFrictionRidge target, wxStaticText *label)
{
	wxNFingerView *view = new wxNFingerView(this, wxID_ANY);
	view->EnableContextMenu(false);

	if (target.IsNull())
	{
		view->SetFinger(NULL);
		return view;
	}

	view->SetFrictionRidge(target);

	if (target.GetObjects().GetCount() > 0)
	{
		NFAttributes attributes = target.GetObjects()[0];
		if (!attributes.IsNull())
		{
			label->SetLabelText(wxString::Format(wxT("Position=%s, Quality=%d"),
				(wxString)NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), target.GetPosition()), attributes.GetQuality()));
		}
		else
		{
			label->SetLabelText(wxString::Format(wxT("Position=%s"),
				(wxString)NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), target.GetPosition())));
		}
	}

	return view;
}

wxNIrisView * MatchingResultPage::ShowIris(NIris target, wxStaticText *label)
{
	wxNIrisView *view = new wxNIrisView(this, wxID_ANY);
	view->EnableContextMenu(false);

	view->SetIris(target);
	if (target.IsNull())
	{
		return view;
	}

	if (target.GetObjects().GetCount() > 0)
	{
		NEAttributes attributes = target.GetObjects()[0];

		if (!attributes.IsNull())
		{
			label->SetLabelText(wxString::Format(wxT("Position=%s, Quality=%d"),
				(wxString)NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), target.GetPosition()), attributes.GetQuality()));
		}
		else
		{
			label->SetLabelText(wxString::Format(wxT("Position=%s"),
				(wxString)NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), target.GetPosition())));
		}
	}

	return view;
}

wxNFaceView * MatchingResultPage::ShowFace(NFace target, wxStaticText *label)
{
	wxNFaceView *view = new wxNFaceView(this, wxID_ANY);
	view->EnableContextMenu(false);
	view->SetWindowStyle(wxBORDER_NONE);
	view->SetBackgroundColour(wxNullColour);

	view->SetFace(target);

	if (target.IsNull())
	{
		return view;
	}

	if (target.GetObjects().GetCount() > 0)
	{
		NLAttributes attributes = target.GetObjects()[0];
		if (!attributes.IsNull())
		{
			label->SetLabelText(wxString::Format(wxT("Quality=%d"), attributes.GetQuality()));
		}
		else
		{
			label->SetLabelText(wxEmptyString);
		}
	}

	return view;
}

VoiceView * MatchingResultPage::ShowVoice(NVoice target, wxStaticText *label)
{
	VoiceView *view = new VoiceView(this, wxID_ANY);

	view->SetVoice(target);

	if (target.IsNull())
	{
		return view;
	}

	if (target.GetObjects().GetCount() > 0)
	{
		NSAttributes attributes = target.GetObjects()[0];
		if (!attributes.IsNull())
		{
			label->SetLabelText(wxString::Format(wxT("Quality=%d"), attributes.GetQuality()));
		}
		else
		{
			label->SetLabelText(wxEmptyString);
		}
	}

	return view;
}

void MatchingResultPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_MATCHING_RESULT_THREAD, &MatchingResultPage::OnThread, this);
	m_choiceBiometric->Connect(wxEVT_CHOICE, wxCommandEventHandler(MatchingResultPage::OnBiometricSelect), NULL, this);
}

void MatchingResultPage::UnregisterGuiEvents()
{
	this->Unbind(wxEVT_MATCHING_RESULT_THREAD, &MatchingResultPage::OnThread, this);
	m_choiceBiometric->Disconnect(wxEVT_CHOICE, wxCommandEventHandler(MatchingResultPage::OnBiometricSelect), NULL, this);
}

void MatchingResultPage::CreateGuiElements()
{
	wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(mainSizer, true);

	wxFlexGridSizer *szFlex = NULL;
	wxBoxSizer *sizer = NULL;

	sizer = new wxBoxSizer(wxHORIZONTAL);
	mainSizer->Add(sizer, 0, wxALL | wxEXPAND);

	m_thumbnail = new ImageView(this);
	m_thumbnail->SetMinSize(wxSize(120, 120));
	sizer->Add(m_thumbnail, 0, wxALL, 5);

	m_lblInfo = new wxStaticText(this, wxID_ANY, wxEmptyString);
	m_lblInfo->SetMaxSize(wxSize(400, -1));
	sizer->Add(m_lblInfo, 0, wxALL, 5);

	m_propertyGrid = new SchemaPropertyGrid(this, wxID_ANY);
	m_propertyGrid->SetIsReadOnly(true);
	m_propertyGrid->SetShowBlobs(false);
	m_propertyGrid->SetMinSize(wxSize(140, 140));
	sizer->Add(m_propertyGrid, 1, wxALL | wxEXPAND, 5);

	szFlex = new wxFlexGridSizer(3, 2, 5, 5);
	mainSizer->Add(szFlex, 1, wxALL | wxEXPAND, 5);

	szFlex->AddGrowableCol(0);
	szFlex->AddGrowableCol(1);
	szFlex->AddGrowableRow(1);
	szFlex->SetRows(3);
	szFlex->SetCols(2);

	m_lblProbeTitle = new wxStaticText(this, wxID_ANY, wxT("Probe subject:"));
	szFlex->Add(m_lblProbeTitle, 0, wxALL | wxEXPAND);

	m_lblGaleryTitle = new wxStaticText(this, wxID_ANY, wxT("Galery subject:"));
	szFlex->Add(m_lblGaleryTitle, 0, wxALL);

	m_leftSizer = new wxBoxSizer(wxVERTICAL);
	szFlex->Add(m_leftSizer, 1, wxALL | wxEXPAND);

	m_rightSizer = new wxBoxSizer(wxVERTICAL);
	szFlex->Add(m_rightSizer, 1, wxALL | wxEXPAND);

	m_lblProbe = new wxStaticText(this, wxID_ANY, wxEmptyString);
	szFlex->Add(m_lblProbe, 0, wxALL);

	m_lblGalery = new wxStaticText(this, wxID_ANY, wxEmptyString);
	szFlex->Add(m_lblGalery, 0, wxALL);

	sizer = new wxBoxSizer(wxHORIZONTAL);
	mainSizer->Add(sizer, 0, wxALL | wxEXPAND);

	m_lblMatchedBiometrics = new wxStaticText(this, wxID_ANY, wxT("Matched biometrics:"));
	sizer->Add(m_lblMatchedBiometrics, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_choiceBiometric = new wxChoice(this, wxID_ANY);
	sizer->Add(m_choiceBiometric, 1, wxALL | wxEXPAND);

	this->Layout();
}

}}

