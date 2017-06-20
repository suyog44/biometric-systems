#ifndef PALM_SELECTOR_H_INCLUDED
#define PALM_SELECTOR_H_INCLUDED

#include <SubjectEditor/FingerSelector.h>
#include <SubjectEditor/FingersSelectorBase.h>

namespace Neurotec { namespace Samples
{

class PalmSelector : public FingersSelectorBase
{
public:
	typedef void (*SelectionChangedCallback)(void *param, ::Neurotec::Biometrics::NFPosition position);

public:
	PalmSelector(wxWindow *parent, wxWindowID id);

	~PalmSelector();

	void SetAllowedPositions(std::vector<Neurotec::Biometrics::NFPosition> positions);

	void SetSelectionChangedCallback(SelectionChangedCallback callback, void *param);

private:
	void CreateGuiElements();

	void UpdateZIndices();

	void OnPaint(wxPaintEvent& event);

	void OnMouseAction(wxMouseEvent& event);

private:
	wxToolTip *m_toolTip;
	::Neurotec::Biometrics::NFPosition m_preferedPosition;
	void *m_callbackParam;
	SelectionChangedCallback m_selectionChangedCallback;
};

}}

#endif
