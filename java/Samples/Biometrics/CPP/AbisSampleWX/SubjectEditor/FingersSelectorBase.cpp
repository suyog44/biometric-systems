#include "Precompiled.h"

#include <SubjectEditor/FingersSelectorBase.h>

#include <Resources/HandsSvg.h>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;

namespace Neurotec { namespace Samples
{

FingersSelectorBase::FingersSelectorBase(wxWindow *parent, wxWindowID id) : wxControl(parent, id)
{
	CreateShapes();
	ExtractOriginalSize(m_originalWidth, m_originalHeight);
	RegisterGuiEvents();

#ifndef __WXMAC__
	this->SetDoubleBuffered(true);
#endif
}

FingersSelectorBase::~FingersSelectorBase()
{
	while(!m_objects.empty())
	{
		delete m_objects.back();
		m_objects.pop_back();
	}

	UnregisterGuiEvents();
}

NFPosition FingersSelectorBase::GetSelection()
{
	NFPosition position = nfpUnknown;

	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		if ((*it)->IsSelected())
		{
			position = (*it)->GetPosition();
			break;
		}
	}

	return position;
}

void FingersSelectorBase::SetSelection(NFPosition position)
{
	DeselectAll();

	if (position == nfpUnknown)
	{
		return;
	}

	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		if ((*it)->IsVisible() && (*it)->GetPosition() == position)
		{
			(*it)->SetSelected(true);
		}
	}

	Refresh(true, NULL);
}

void FingersSelectorBase::SetAllowedPositions(std::vector<NFPosition> positions)
{
	m_allowedPositions.clear();

	MarkAllAsNotSelectable();

	m_allowedPositions = positions;
}

void FingersSelectorBase::DeselectAll()
{
	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		(*it)->SetSelected(false);
	}
}

void FingersSelectorBase::MarkAllAsNotSelectable()
{
	DeselectAll();

	for (std::vector<NFObjectUi *>::iterator it = m_nfObjects.begin(); it != m_nfObjects.end(); it++)
	{
		(*it)->SetSelectable(false);
	}
}

void FingersSelectorBase::GetOriginalAndCurrentSizeRatio(wxDouble& widthRatio, wxDouble& heightRatio)
{
	int width, height;
	this->GetSize(&width, &height);
	widthRatio = (wxDouble)width / (wxDouble)m_originalWidth;
	heightRatio = (wxDouble)height / (wxDouble)m_originalHeight;
}

void FingersSelectorBase::ExtractOriginalSize(long& width, long& height)
{
	wxStringInputStream stream(wxString(HANDS_SVG, HANDS_SVG_SIZE));
	wxXmlDocument xml(stream);
	wxXmlNode *rootNode = xml.GetRoot();
	rootNode->GetAttribute(wxT("width")).ToLong(&width, 10);
	rootNode->GetAttribute(wxT("height")).ToLong(&height, 10);
}

void FingersSelectorBase::CreateShapes()
{
	wxStringInputStream stream(wxString(HANDS_SVG, HANDS_SVG_SIZE));
	wxXmlDocument xml(stream);

	wxXmlNode *rootNode = xml.GetRoot();
	wxXmlNode *child = rootNode->GetChildren();

	while(child)
	{
		if (child->GetName() != wxT("g"))
		{
			child = child->GetNext();
			continue;
		}

		wxXmlNode *path = child->GetChildren();
		while(path)
		{
			if (path->GetAttribute(wxT("d")) == wxEmptyString)
			{
				path = path->GetNext();
				continue;
			}

			wxXmlAttribute *attributes = path->GetAttributes();
			NFObjectUi *object = new NFObjectUi(path->GetAttribute(wxT("d")));

			while(attributes != NULL)
			{
				if (attributes->GetName() == wxT("id"))
				{
					object->SetId(attributes->GetValue());
				}
				else if (attributes->GetName() == wxT("position"))
				{
					object->SetPosition((NFPosition)NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), attributes->GetValue()));
				}
				else if (attributes->GetName() == wxT("group"))
				{
					object->SetType(NFObjectUi::TypeFromString(attributes->GetValue()));
				}

				attributes = attributes->GetNext();
			}

			m_objects.push_back(object);

			path = path->GetNext();
		}

		child = child->GetNext();
	}
}

void FingersSelectorBase::OnMouseAction(wxMouseEvent&)
{
}

void FingersSelectorBase::RegisterGuiEvents()
{
	this->Connect(wxEVT_PAINT, wxPaintEventHandler(FingersSelectorBase::OnPaint), NULL, this);
	this->Connect(wxEVT_MOTION, wxMouseEventHandler(FingersSelectorBase::OnMouseAction));
	this->Connect(wxEVT_LEFT_UP, wxMouseEventHandler(FingersSelectorBase::OnMouseAction));
	this->Connect(wxEVT_RIGHT_UP, wxMouseEventHandler(FingersSelectorBase::OnMouseAction));
}

void FingersSelectorBase::UnregisterGuiEvents()
{
	this->Disconnect(wxEVT_PAINT, wxPaintEventHandler(FingersSelectorBase::OnPaint), NULL, this);
	this->Disconnect(wxEVT_MOTION, wxMouseEventHandler(FingersSelectorBase::OnMouseAction));
	this->Disconnect(wxEVT_LEFT_UP, wxMouseEventHandler(FingersSelectorBase::OnMouseAction));
	this->Disconnect(wxEVT_RIGHT_UP, wxMouseEventHandler(FingersSelectorBase::OnMouseAction));
}

}}

