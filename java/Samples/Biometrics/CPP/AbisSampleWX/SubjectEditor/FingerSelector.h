#ifndef FINGER_SELECTOR_H_INCLUDED
#define FINGER_SELECTOR_H_INCLUDED

#include <SubjectEditor/FingersSelectorBase.h>
#include <SubjectEditor/NFObjectUi.h>

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_FINGER_SELECTOR_FINGER_SELECTED, wxCommandEvent);

class FingerSelector : public FingersSelectorBase
{
public:
	FingerSelector(wxWindow *parent, wxWindowID id);

	~FingerSelector();

	void SetAllowedPositions(std::vector<Neurotec::Biometrics::NFPosition> positions);
	void SetMissingPositions(std::vector<Neurotec::Biometrics::NFPosition> positions);
	void SetAllowOnlyAmputateAction(bool value);
	::std::vector<Neurotec::Biometrics::NFPosition> GetMissingPositions();
	::std::vector<Neurotec::Biometrics::NFPosition> GetValidPositions();

private:
	void MarkAsMissing(::Neurotec::Biometrics::NFPosition position, bool markAsMissing);
	bool IsPositionMissing(::Neurotec::Biometrics::NFPosition position);
	void CreateGuiElements();
	void UpdateZIndices();
	void UpdateItemWithPosition(::Neurotec::Biometrics::NFPosition position, NFObjectUi::Type type = NFObjectUi::Item);
	void OnMouseAction(wxMouseEvent& event);
	void OnContextMenu(wxCommandEvent &event);
	void OnPaint(wxPaintEvent& event);

private:
	typedef enum
	{
		ACTION_MARK_AS_MISSING,
		ACTION_MARK_AS_NOT_MISSING,
		ACTION_SELECT
	} ContextMenuAction;

	struct ContextMenuItem
	{
		ContextMenuAction action;
		NFObjectUi *item;
	};

	wxMenu *m_menu;
	bool m_allowOnlyAmputation;
	::std::vector<Neurotec::Biometrics::NFPosition> m_missingPositions;
	::std::vector<ContextMenuItem> m_menuItems;
};

}}

#endif
