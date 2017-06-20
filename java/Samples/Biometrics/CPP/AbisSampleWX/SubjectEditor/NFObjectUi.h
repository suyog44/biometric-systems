#ifndef NF_OBJECT_UI_H_INCLUDED
#define NF_OBJECT_UI_H_INCLUDED

#include <Common/SvgShape.h>

namespace Neurotec { namespace Samples
{

class NFObjectUi : public SvgShape
{
public:
	typedef enum
	{
		None = 0,
		Item = 1,
		ItemPart = 2,
		Fingernails = 3,
		PalmCreases = 4,
		Rotation = 5,
	} Type;

public:
	NFObjectUi(wxString svgPath);

	virtual ~NFObjectUi();

#if defined(N_CLANG)
	#pragma clang diagnostic push
	#pragma clang diagnostic ignored "-Woverloaded-virtual"
#endif
	virtual void Draw(wxGraphicsContext *gc, bool drawBorder = false);
#if defined(N_CLANG)
	#pragma clang diagnostic pop
#endif

	void SetPosition(::Neurotec::Biometrics::NFPosition position);

	void SetId(wxString id);

	void SetType(Type type);

	void SetVisible(bool value);

	void SetActivated(bool value);

	void SetSelected(bool value);

	void SetAmputated(bool value);

	void SetSelectable(bool value);

	Type GetType();

	::Neurotec::Biometrics::NFPosition GetPosition();

	wxString GetId();

	bool IsVisible();

	bool IsSelected();

	bool IsAmputated();

	bool IsSelectable();

	bool IsActivated();

	static Type TypeFromString(wxString value);

	static bool Compare (NFObjectUi *objectA, NFObjectUi *objectB);

private:
	bool m_isAmputated;
	bool m_isVisible;
	bool m_isSelected;
	bool m_isActivated;
	bool m_isSelectable;

	Type m_type;
	wxString m_id;
	::Neurotec::Biometrics::NFPosition m_position;
};

}}

#endif
