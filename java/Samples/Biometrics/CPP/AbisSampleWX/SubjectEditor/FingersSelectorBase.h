#ifndef FINGERS_SELECTOR_BASE_H_INCLUDED
#define FINGERS_SELECTOR_BASE_H_INCLUDED

#include <SubjectEditor/NFObjectUi.h>

namespace Neurotec { namespace Samples
{

class FingersSelectorBase : public wxControl
{

public:
	FingersSelectorBase(wxWindow *parent, wxWindowID id);

	virtual ~FingersSelectorBase();

	virtual ::Neurotec::Biometrics::NFPosition GetSelection();
	virtual void SetSelection(::Neurotec::Biometrics::NFPosition position);
	virtual void SetAllowedPositions(std::vector<Neurotec::Biometrics::NFPosition> positions);

protected:
	void GetOriginalAndCurrentSizeRatio(wxDouble& widthRatio, wxDouble& heightRatio);
	virtual void RegisterGuiEvents();
	virtual void UnregisterGuiEvents();
	virtual void OnMouseAction(wxMouseEvent& event);
	virtual void OnPaint(wxPaintEvent& event) = 0;

protected:
	::std::vector<NFObjectUi *> m_objects;
	::std::vector<NFObjectUi *> m_nfObjects;
	::std::vector<Neurotec::Biometrics::NFPosition> m_allowedPositions;

private:
	void CreateShapes();
	void DeselectAll();
	void MarkAllAsNotSelectable();
	void ExtractOriginalSize(long& width, long& height);

private:
	long m_originalWidth;
	long m_originalHeight;
};

}}

#endif
