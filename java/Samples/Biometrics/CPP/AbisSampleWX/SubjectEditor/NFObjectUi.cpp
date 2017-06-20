#include "Precompiled.h"

#include <SubjectEditor/NFObjectUi.h>

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{

NFObjectUi::NFObjectUi(wxString svgPath) : SvgShape(svgPath)
{
	m_id = wxEmptyString;

	m_type = None;
	m_position = nfpUnknown;

	m_isAmputated = false;
	m_isVisible = false;
	m_isSelected = false;
	m_isActivated = false;
	m_isSelectable = false;
}

NFObjectUi::~NFObjectUi()
{
}

void NFObjectUi::SetPosition(NFPosition position)
{
	m_position = position;
}

void NFObjectUi::SetId(wxString id)
{
	m_id = id;
}

void NFObjectUi::SetType(NFObjectUi::Type type)
{
	m_type = type;
}

void NFObjectUi::SetVisible(bool value)
{
	m_isVisible = value;
}

void NFObjectUi::SetActivated(bool value)
{
	m_isActivated = value;
}

void NFObjectUi::SetSelected(bool value)
{
	m_isSelected = value;
}

void NFObjectUi::SetSelectable(bool value)
{
	m_isSelectable = value;
}

void NFObjectUi::SetAmputated(bool value)
{
	m_isAmputated = value;
}

bool NFObjectUi::IsVisible()
{
	return m_isVisible;
}

bool NFObjectUi::IsActivated()
{
	return m_isActivated;
}

bool NFObjectUi::IsAmputated()
{
	return m_isAmputated;
}

bool NFObjectUi::IsSelected()
{
	return m_isSelected;
}

bool NFObjectUi::IsSelectable()
{
	return m_isSelectable;
}

NFPosition NFObjectUi::GetPosition()
{
	return m_position;
}

NFObjectUi::Type NFObjectUi::GetType()
{
	return m_type;
}

wxString NFObjectUi::GetId()
{
	return m_id;
}

void NFObjectUi::Draw(wxGraphicsContext *gc, bool drawBorder)
{
	if (!m_isVisible) return;

	if (drawBorder)
		gc->SetPen(*wxBLACK_PEN);
	else
		gc->SetPen(wxNullPen);

	if (m_isActivated)
	{
		wxBrush brush(wxColour(57, 188, 245, (GetZIndex() > 0)? 255 : 80));
		gc->SetBrush(brush);
	}
	else if (m_isAmputated)
	{
		gc->SetBrush(wxColour(153, 0, 0, 255));
	}
	else if (m_isSelected)
	{
		wxBrush brush(wxColour(0, 153, 0, 255));
		gc->SetBrush(brush);
	}
	else
	{
		gc->SetBrush(wxNullBrush);
	}

	SvgShape::Draw(gc);
}

bool NFObjectUi::Compare(NFObjectUi *objectA, NFObjectUi *objectB)
{
	return (objectA->GetType() > objectB->GetType());
}

NFObjectUi::Type NFObjectUi::TypeFromString(wxString value)
{
	if (value == wxT("Item")) return Item;
	if (value == wxT("ItemPart")) return ItemPart;
	if (value == wxT("Fingernails")) return Fingernails;
	if (value == wxT("PalmCreases")) return PalmCreases;
	if (value == wxT("Rotation")) return Rotation;

	return None;
}

}}

