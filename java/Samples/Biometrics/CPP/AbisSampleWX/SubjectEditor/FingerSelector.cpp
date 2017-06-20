#include "Precompiled.h"

#include <Common/SvgShape.h>

#include <SubjectEditor/FingerSelector.h>

#include <algorithm>

using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{

wxDEFINE_EVENT(wxEVT_FINGER_SELECTOR_FINGER_SELECTED, wxCommandEvent);

FingerSelector::FingerSelector(wxWindow *parent, wxWindowID id) : FingersSelectorBase(parent, id)
{
	m_menu = NULL;
	m_allowOnlyAmputation = false;

	CreateGuiElements();
}

FingerSelector::~FingerSelector()
{
	if (m_menu != NULL)
	{
		m_menu->Disconnect(wxEVT_COMMAND_MENU_SELECTED, (wxObjectEventFunction)&FingerSelector::OnContextMenu, NULL, this);
		delete m_menu;
		m_menu = NULL;
	}
}

void FingerSelector::SetMissingPositions(std::vector<Neurotec::Biometrics::NFPosition> positions)
{
	for (std::vector<NFPosition>::iterator it = positions.begin(); it != positions.end(); it++)
	{
		MarkAsMissing(*it, true);
	}
}

std::vector<NFPosition> FingerSelector::GetMissingPositions()
{
	std::vector<NFPosition> missingPositions;

	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		NFObjectUi *object = *it;

		if (!object->IsAmputated())
		{
			continue;
		}

		bool exists = false;

		for (unsigned int i = 0; i < missingPositions.size(); i++)
		{
			if (missingPositions[i] == object->GetPosition())
			{
				exists = true;
				break;
			}
		}

		if (!exists)
		{
			missingPositions.push_back(object->GetPosition());
		}
	}

	return missingPositions;
}

std::vector<NFPosition> FingerSelector::GetValidPositions()
{
	std::vector<NFPosition> validPositions;

	for (std::vector<NFPosition>::iterator it = m_allowedPositions.begin(); it != m_allowedPositions.end(); it++)
	{
		if (!IsPositionMissing(*it))
		{
			validPositions.push_back(*it);
		}
	}

	return validPositions;
}

void FingerSelector::SetAllowedPositions(std::vector<NFPosition> positions)
{
	FingersSelectorBase::SetAllowedPositions(positions);

	for (std::vector<NFPosition>::iterator it = m_allowedPositions.begin(); it != m_allowedPositions.end(); it++)
	{
		UpdateItemWithPosition(*it);
	}

	UpdateZIndices();

	Refresh(true, NULL);
}

void FingerSelector::UpdateItemWithPosition(NFPosition position, NFObjectUi::Type type)
{
	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		NFObjectUi *object = *it;

		if (object->GetPosition() != position || object->GetType() != type)
		{
			continue;
		}

		object->SetSelectable(true);

		if (!NBiometricTypes::IsPositionSingleFinger(position))
		{
			NArrayWrapper<NFPosition> positions = NBiometricTypes::GetPositionAvailableParts(position, NULL, 0);
			for (int i = 0; i < positions.GetCount(); i++)
			{
				UpdateItemWithPosition(positions[i], NFObjectUi::ItemPart);
			}
		}
	}
}

void FingerSelector::SetAllowOnlyAmputateAction(bool value)
{
	m_allowOnlyAmputation = value;
}

void FingerSelector::MarkAsMissing(::Neurotec::Biometrics::NFPosition position, bool markAsMissing)
{
	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		NFObjectUi *object = *it;

		if (object->IsVisible() && object->GetPosition() == position && object->GetType() != NFObjectUi::Item)
		{
			object->SetAmputated(markAsMissing);
		}
	}
}

bool FingerSelector::IsPositionMissing(NFPosition position)
{
	bool result = false;

	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		if ((*it)->GetPosition() == position && (*it)->IsAmputated())
		{
			result = true;
			break;
		}
	}

	return result;
}

void FingerSelector::OnMouseAction(wxMouseEvent& event)
{
	long cursorX = 0;
	long cursorY = 0;

	event.GetPosition(&cursorX, &cursorY);

	wxDouble ratioX = 0;
	wxDouble ratioY = 0;

	GetOriginalAndCurrentSizeRatio(ratioX, ratioY);

	std::vector<NFObjectUi *> activeObjects;
	std::vector<NFObjectUi *> activeSingleItemObjects;
	std::vector<NFObjectUi *> activeMultipleItemsObjects;

	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		NFObjectUi *object = *it;

		object->SetActivated(false);

		if (!object->IsSelectable())
		{
			continue;
		}

		if (object->Contains((wxDouble)cursorX / ratioX, (wxDouble)cursorY / ratioX))
		{
			activeObjects.push_back(object);

			if (object->GetType() != NFObjectUi::Item)
			{
				continue;
			}

			if (NBiometricTypes::IsPositionSingleFinger(object->GetPosition()))
			{
				activeSingleItemObjects.push_back(object);
			}
			else
			{
				activeMultipleItemsObjects.push_back(object);
			}
		}
	}

	if (activeMultipleItemsObjects.size() + activeSingleItemObjects.size() > 1)
	{
		if (activeSingleItemObjects.size() > 0)
		{
			activeSingleItemObjects.front()->SetActivated(true);
		}
		else
		{
			activeMultipleItemsObjects.front()->SetActivated(true);
		}
	}
	else
	{
		for (std::vector<NFObjectUi *>::iterator it = activeObjects.begin(); it != activeObjects.end(); it++)
		{
			(*it)->SetActivated(true);
		}
	}

	if (activeObjects.size() > 0 && event.LeftUp())
	{
		if (m_menu != NULL)
		{
			m_menu->Disconnect(wxEVT_COMMAND_MENU_SELECTED, (wxObjectEventFunction)&FingerSelector::OnContextMenu, NULL, this);
			delete m_menu;
		}

		m_menu = new wxMenu();
		m_menu->Connect(wxEVT_COMMAND_MENU_SELECTED, (wxObjectEventFunction)&FingerSelector::OnContextMenu, NULL, this);

		m_menuItems.clear();

		for (std::vector<NFObjectUi *>::iterator it = activeObjects.begin(); it != activeObjects.end(); it++)
		{
			NFObjectUi *activeObject = *it;

			if (!activeObject->IsActivated())
			{
				continue;
			}

			wxString strPosition = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), activeObject->GetPosition());
			bool isMissing = IsPositionMissing(activeObject->GetPosition());

			if (!m_allowOnlyAmputation)
			{
				if (activeObject->GetType() == NFObjectUi::ItemPart)
				{
					ContextMenuItem menuItem;

					menuItem.action = isMissing? ACTION_MARK_AS_NOT_MISSING : ACTION_MARK_AS_MISSING;
					menuItem.item = activeObject;

					m_menuItems.push_back(menuItem);
					m_menu->Append(m_menuItems.size() - 1, wxString::Format(wxT("Mark %s as %smissing"), strPosition, isMissing? wxT("not ") : wxEmptyString));
				}
				else
				{
					ContextMenuItem menuItem;

					menuItem.action = ACTION_SELECT;
					menuItem.item = activeObject;

					m_menuItems.push_back(menuItem);
					m_menu->Append(m_menuItems.size() - 1, wxString::Format(wxT("Select %s"), strPosition));

					if (NBiometricTypes::IsPositionSingleFinger(activeObject->GetPosition()))
					{
						ContextMenuItem missingItem;
						missingItem.action = isMissing? ACTION_MARK_AS_NOT_MISSING : ACTION_MARK_AS_MISSING;
						missingItem.item = activeObject;

						m_menuItems.push_back(missingItem);
						m_menu->Append(m_menuItems.size() - 1, wxString::Format(wxT("Mark %s as %smissing"), strPosition,
							isMissing? wxT("not ") : wxEmptyString));
					}
				}
			}
			else
			{
				if (NBiometricTypes::IsPositionSingleFinger(activeObject->GetPosition()))
				{
					MarkAsMissing(activeObject->GetPosition(), isMissing? false : true);
					break;
				}
			}
		}

		if (m_menu->GetMenuItemCount() > 0)
			PopupMenu(m_menu);
	}

	Refresh(true, NULL);
}

void FingerSelector::OnContextMenu(wxCommandEvent &event)
{
	int selection = event.GetId();

	if (selection < 0)
	{
		return;
	}

	ContextMenuItem menuItem = m_menuItems.at(selection);

	switch (menuItem.action) {
	case ACTION_MARK_AS_MISSING:
	case ACTION_MARK_AS_NOT_MISSING:
		menuItem.item->SetSelected(false);
		MarkAsMissing(menuItem.item->GetPosition(), (menuItem.action == ACTION_MARK_AS_MISSING)? true : false);
		break;
	case ACTION_SELECT:
		SetSelection(menuItem.item->GetPosition());
		if (IsEnabled())
		{
			NFPosition position = GetSelection();
			wxCommandEvent evt(wxEVT_FINGER_SELECTOR_FINGER_SELECTED, GetId());
			evt.SetInt((int)position);
			wxPostEvent(this, evt);
		}
		break;
	default:
		break;
	};
}

void FingerSelector::OnPaint(wxPaintEvent&)
{
	wxPaintDC dc(this);
	wxGraphicsContext *gc = wxGraphicsContext::Create(dc);

	wxDouble wRatio, hRatio;
	GetOriginalAndCurrentSizeRatio(wRatio, hRatio);

	gc->Scale(wRatio, wRatio);

	std::vector<NFObjectUi *> sorted = m_nfObjects;
	std::sort(sorted.begin(), sorted.end(), SvgShape::IsLessThen);

	for (std::vector<NFObjectUi *>::iterator it = sorted.begin(); it != sorted.end(); it++)
	{
		bool drawBorder = false;

		if ((*it)->GetPosition() == nfpLeftFullPalm || (*it)->GetPosition() == nfpRightFullPalm || (*it)->GetType() == NFObjectUi::Fingernails)
		{
			drawBorder = true;
		}

		(*it)->Draw(gc, drawBorder);
	}

	delete gc;
}

void FingerSelector::UpdateZIndices()
{
	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		NFObjectUi *object = *it;

		if (NBiometricTypes::IsPositionSingleFinger(object->GetPosition()))
		{
			object->SetZIndex((object->GetType() == NFObjectUi::Item)? 2 : 1);
		}

		if (object->GetType() == NFObjectUi::Fingernails)
		{
			object->SetZIndex(3);
		}

		if (object->GetPosition() == nfpLeftFullPalm || object->GetPosition() == nfpRightFullPalm)
		{
			object->SetZIndex(4);
		}
	}
}

void FingerSelector::CreateGuiElements()
{
	for (std::vector<NFObjectUi *>::iterator it = m_objects.begin(); it != m_objects.end(); it++)
	{
		switch((*it)->GetPosition())
		{
		case nfpLeftFullPalm:
		case nfpRightFullPalm:
		case nfpLeftMiddle:
		case nfpLeftLittle:
		case nfpLeftIndex:
		case nfpLeftRing:
		case nfpLeftThumb:
		case nfpRightThumb:
		case nfpRightRing:
		case nfpRightIndex:
		case nfpRightMiddle:
		case nfpRightLittle:
		case nfpPlainLeftFourFingers:
		case nfpPlainRightFourFingers:
		case nfpPlainThumbs:
			{
				if ((*it)->GetType() != NFObjectUi::Rotation)
				{
					(*it)->SetVisible(true);
				}

				break;
			}
		default:
			if ((*it)->GetType() == NFObjectUi::Fingernails)
			{
				(*it)->SetVisible(true);
			}

			break;
		};

		if ((*it)->IsVisible())
		{
			m_nfObjects.push_back(*it);
		}
	}
}

}}

