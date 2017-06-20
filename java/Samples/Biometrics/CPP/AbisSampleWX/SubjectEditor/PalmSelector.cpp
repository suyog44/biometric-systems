#include "Precompiled.h"

#include <SubjectEditor/PalmSelector.h>

#include <algorithm>

using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples
{

PalmSelector::PalmSelector(wxWindow *parent, wxWindowID id) : FingersSelectorBase(parent, id)
{
	m_toolTip = NULL;
	m_callbackParam = NULL;
	m_selectionChangedCallback = NULL;
	m_preferedPosition = nfpUnknownPalm;

	CreateGuiElements();
}

PalmSelector::~PalmSelector()
{
}

void PalmSelector::SetAllowedPositions(std::vector<NFPosition> positions)
{
	FingersSelectorBase::SetAllowedPositions(positions);

	for (std::vector<NFPosition>::iterator it = m_allowedPositions.begin(); it != m_allowedPositions.end(); it++)
	{
		for (std::vector<NFObjectUi *>::iterator objIterator = m_nfObjects.begin(); objIterator != m_nfObjects.end(); objIterator++)
		{
			if ((*objIterator)->GetPosition() == (*it))
			{
				(*objIterator)->SetSelectable(true);
			}
		}
	}

	UpdateZIndices();

	Refresh(true, NULL);
}

void PalmSelector::SetSelectionChangedCallback(PalmSelector::SelectionChangedCallback callback, void *param)
{
	m_selectionChangedCallback = callback;
	m_callbackParam = param;
}

void PalmSelector::OnMouseAction(wxMouseEvent& event)
{
	long cursorX = 0;
	long cursorY = 0;

	event.GetPosition(&cursorX, &cursorY);

	wxDouble ratioX = 0;
	wxDouble ratioY = 0;

	GetOriginalAndCurrentSizeRatio(ratioX, ratioY);

	std::vector<NFObjectUi *> activeObjects;

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
		}
	}

	unsigned int preferedPositionIndex = 0;
	for (unsigned int i = 0; i < activeObjects.size(); i++)
	{
		if (activeObjects[i]->GetPosition() == m_preferedPosition)
		{
			preferedPositionIndex = i;
			break;
		}
	}

	if (activeObjects.size() > 0 && event.RightUp())
	{
		preferedPositionIndex++;

		if (preferedPositionIndex >= activeObjects.size())
		{
			preferedPositionIndex = 0;
		}

		m_preferedPosition = activeObjects[preferedPositionIndex]->GetPosition();
	}
	else if (activeObjects.size() > 0 && event.LeftUp())
	{
		SetSelection(activeObjects[preferedPositionIndex]->GetPosition());

		if (m_selectionChangedCallback != NULL)
		{
			(*m_selectionChangedCallback)(m_callbackParam, activeObjects[preferedPositionIndex]->GetPosition());
		}
	}

	wxString toolTipMessage = wxEmptyString;

	if (activeObjects.size() > 0)
	{
		NFPosition activePosition = activeObjects[preferedPositionIndex]->GetPosition();

		activeObjects[preferedPositionIndex]->SetActivated(true);

		toolTipMessage = wxT("Position: ") + (wxString)NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), activePosition);

		if (activeObjects.size() > 1)
		{
			toolTipMessage += wxT("\n\nThis is also:");

			for (unsigned int i = 0; i < activeObjects.size(); i++)
			{
				if (activeObjects[i]->GetPosition() == activePosition)
				{
					continue;
				}

				toolTipMessage += wxT(" ") + (wxString)NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), activeObjects[i]->GetPosition()) + ",";
			}

			toolTipMessage += wxT("\nClick right mouse button to show other position");
		}
	}

	m_toolTip->SetTip(toolTipMessage);

	Refresh(true, NULL);
}

void PalmSelector::OnPaint(wxPaintEvent&)
{
	wxPaintDC dc(this);
	wxGraphicsContext *gc = wxGraphicsContext::Create(dc);

	wxDouble wRatio, hRatio;
	GetOriginalAndCurrentSizeRatio(wRatio, hRatio);

	gc->Scale(wRatio, wRatio);

	std::vector<NFObjectUi *> sorted = m_nfObjects;
	std::vector<NFObjectUi *> activated;
	std::vector<NFObjectUi *> palmCreases;
	std::sort(sorted.begin(), sorted.end(), SvgShape::IsLessThen);

	for (std::vector<NFObjectUi *>::iterator it = sorted.begin(); it != sorted.end(); it++)
	{
		if ((*it)->IsActivated())
		{
			activated.push_back(*it);
			continue;
		}

		if ((*it)->GetType() == NFObjectUi::PalmCreases)
		{
			palmCreases.push_back(*it);
			continue;
		}

		bool drawBorder = false;

		if ((*it)->GetPosition() == nfpLeftFullPalm || (*it)->GetPosition() == nfpRightFullPalm || (*it)->GetType() == NFObjectUi::PalmCreases)
		{
			drawBorder = true;
		}

		(*it)->Draw(gc, drawBorder);
	}

	for (std::vector<NFObjectUi *>::iterator it = activated.begin(); it != activated.end(); it++)
	{
		bool drawBorder = false;

		if ((*it)->GetPosition() == nfpLeftFullPalm || (*it)->GetPosition() == nfpRightFullPalm)
		{
			drawBorder = true;
		}

		(*it)->Draw(gc, drawBorder);
	}

	for (std::vector<NFObjectUi *>::iterator it = palmCreases.begin(); it != palmCreases.end(); it++)
	{
		(*it)->Draw(gc, true);
	}

	delete gc;
}

void PalmSelector::UpdateZIndices()
{
	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		NFObjectUi *object = *it;

		if (object->GetPosition() == nfpLeftHypothenar || object->GetPosition() == nfpRightHypothenar
			|| object->GetPosition() == nfpLeftLowerPalm || object->GetPosition() == nfpRightLowerPalm
			|| object->GetPosition() == nfpLeftThenar || object->GetPosition() == nfpRightThenar
			|| object->GetPosition() == nfpLeftUpperPalm || object->GetPosition() == nfpRightUpperPalm
			|| object->GetPosition() == nfpLeftInterdigital || object->GetPosition() == nfpRightInterdigital)
		{
			object->SetZIndex(1);
		}

		if (object->GetType() == NFObjectUi::PalmCreases)
		{
			object->SetZIndex(3);
		}

		if (object->GetPosition() == nfpLeftFullPalm || object->GetPosition() == nfpRightFullPalm)
		{
			object->SetZIndex(2);
		}
	}
}

void PalmSelector::CreateGuiElements()
{
	for (std::vector<NFObjectUi *>::iterator it = m_objects.begin(); it != m_objects.end(); it++)
	{
		NFObjectUi *object = *it;

		switch(object->GetPosition())
		{
		case nfpLeftFullPalm:
		case nfpRightFullPalm:
		case nfpLeftUpperPalm:
		case nfpRightUpperPalm:
		case nfpLeftInterdigital:
		case nfpRightInterdigital:
		case nfpLeftHypothenar:
		case nfpRightHypothenar:
		case nfpLeftLowerPalm:
		case nfpRightLowerPalm:
		case nfpLeftThenar:
		case nfpRightThenar:
			object->SetVisible(true);
			break;
		default:
			if (object->GetType() == NFObjectUi::PalmCreases)
			{
				object->SetVisible(true);
			}
			break;
		};

		if (object->IsVisible())
		{
			m_nfObjects.push_back(object);
		}
	}

	m_toolTip = new wxToolTip(wxEmptyString);
	m_toolTip->SetDelay(0);
	m_toolTip->SetReshow(0);
	this->SetToolTip(m_toolTip);
}

}}
