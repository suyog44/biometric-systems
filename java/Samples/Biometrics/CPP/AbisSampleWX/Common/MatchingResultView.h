#ifndef MATCHING_RESULT_VIEW_H_INCLUDED
#define MATCHING_RESULT_VIEW_H_INCLUDED

namespace Neurotec { namespace Samples
{

class MatchingResultView : public wxPanel
{
public:
	typedef void (*LinkPressedCallback)(::Neurotec::Biometrics::NMatchingResult result, void *param);

public:
	MatchingResultView(wxWindow *parent, wxWindowID winid);

	~MatchingResultView();

	void SetMatchingResult(::Neurotec::Biometrics::NMatchingResult matchingResult);
	void SetLinkPressedCallback(LinkPressedCallback callback, void *param);
	void SetIsLinkActive(bool value);
	bool IsLinkActive();
	void SetMatchingThreshold(int value);

private:
	wxString MatchingResultToString(const ::Neurotec::Biometrics::NMatchingResult matchingResult);

	void OnHyperlinkClick(wxHyperlinkEvent& event);

	void RegisterGuiEvents();

	void UnregisterGuiEvents();

	void CreateGuiElements();

private:
	::Neurotec::Biometrics::NMatchingResult m_matchingResult;
	int m_matchingThreshold;
	LinkPressedCallback m_linkPressedCallback;
	void *m_callbackParam;
	bool m_isLinkActive;
	bool m_linkEnabled;
	wxHyperlinkCtrl *m_lblLink;
	wxStaticText *m_lblDetails;
};

}}

#endif

