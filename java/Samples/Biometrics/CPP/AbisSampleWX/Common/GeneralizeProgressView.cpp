#include "Precompiled.h"
#include <Common/GeneralizeProgressView.h>

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;

namespace Neurotec { namespace Samples
{

GeneralizeProgressView::ItemStatus::ItemStatus() :
	text(wxEmptyString),
	fill(false),
	color(wxColor(255,165,0)),
	biometric(NULL),
	hitBox(wxRect(0, 0, 0, 0)),
	selected(false)
{
}

bool GeneralizeProgressView::ItemStatus::HitTest(wxPoint p)
{
	if (!hitBox.IsEmpty())
	{
		return hitBox.Contains(p);
	}
	return false;
}

wxDECLARE_EVENT(wxEVT_BIOMETRIC_PROPERTY_CHANGED, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_BIOMETRIC_PROPERTY_CHANGED, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_GEN_SELECTED_ITEM_CHANGED, wxCommandEvent);

BEGIN_EVENT_TABLE(GeneralizeProgressView, wxWindow)
	EVT_COMMAND(wxID_ANY, wxEVT_BIOMETRIC_PROPERTY_CHANGED, GeneralizeProgressView::OnPropertyChanged)
	EVT_PAINT(GeneralizeProgressView::OnPaint)
	EVT_MOTION(GeneralizeProgressView::OnMouseMove)
	EVT_LEFT_DOWN(GeneralizeProgressView::OnMouseDown)
END_EVENT_TABLE()

GeneralizeProgressView::GeneralizeProgressView(wxWindow * parent, int winid) :
	wxWindow(parent, winid, wxDefaultPosition, wxDefaultSize, wxFULL_REPAINT_ON_RESIZE),
	m_view(NULL),
	m_icaoView(NULL),
	m_selected(NULL),
	m_enableMouseSelection(true)
{
	SetBackgroundStyle(wxBG_STYLE_PAINT);
}

GeneralizeProgressView::~GeneralizeProgressView()
{
	m_biometrics.clear();
	m_generalized.clear();
	m_selected = NULL;
	m_drawings.clear();
}

void GeneralizeProgressView::OnPaint(wxPaintEvent&)
{
	wxAutoBufferedPaintDC dc(this);
	dc.SetBackground(wxBrush(this->GetBackgroundColour()));
	dc.Clear();
	dc.SetBackground(wxNullBrush);

	::std::auto_ptr<wxGraphicsContext> gc(wxGraphicsContext::Create(dc));
	wxFont font(10, wxFONTFAMILY_SWISS, wxFONTSTYLE_NORMAL, wxFONTWEIGHT_NORMAL);
	gc->SetFont(font, wxColour(0, 0, 0));
	OnDraw(gc.get());
}

void GeneralizeProgressView::OnDraw(wxGraphicsContext *gc)
{
	const int Margin = 2;
	wxSize sz = GetSize();
	sz.SetWidth(sz.GetWidth() - Margin * 2);
	sz.SetHeight(sz.GetHeight() - Margin * 2);

	if (!m_drawings.empty())
	{
		double x, y;
		wxString text = wxT("Az");
		gc->GetTextExtent(text, &x, &y);
		wxSize defaultTextSize((int)x, (int)y);
		wxSize textSize = defaultTextSize;
		int bubleDiameter = textSize.GetHeight() - Margin * 2;
		int totalWidth = 0;
		for (std::vector<ItemStatus>::iterator it = m_drawings.begin(); it != m_drawings.end(); it++)
		{
			gc->GetTextExtent(it->text, &x, &y);
			totalWidth += 2 * (int)x + Margin + bubleDiameter + 2 * Margin;
		}
		int offsetX = (sz.GetWidth() - totalWidth) / 2;
		int offsetY = (sz.GetHeight() - textSize.GetHeight()) / 2;

		gc->Translate(offsetX, offsetY);
		float offset = 2 * Margin;
		for (std::vector<ItemStatus>::iterator it = m_drawings.begin(); it != m_drawings.end(); it++)
		{
			wxRect hitBox(offsetX + offset, offsetY, 0, 0);
			if (it->text != wxEmptyString)
			{
				wxColor black(0, 0, 0);
				gc->SetBrush(wxBrush(black));
				gc->SetPen(wxPen(black));
				gc->GetTextExtent(it->text, &x, &y);
				textSize = wxSize((int)x, (int)y);
				gc->DrawText(it->text, offset, 0);
			}
			else
			{
				textSize = wxSize(0, defaultTextSize.GetHeight());
			}
			hitBox.SetWidth(textSize.GetWidth() + Margin);
			offset += hitBox.GetWidth();

			wxPen p(it->color);
			gc->SetPen(p);
			if (it->fill)
			{
				wxBrush br(it->color);
				gc->SetBrush(br);
			}
			else
			{
				gc->SetBrush(wxNullBrush);
			}

			gc->DrawEllipse(offset, Margin, bubleDiameter, bubleDiameter);
			if (it->selected)
			{
				wxColor cadetBlue(0x5F, 0x9E, 0xA0);
				gc->SetPen(wxPen(cadetBlue, 2));
				gc->SetBrush(wxNullBrush);
				gc->DrawEllipse(offset, Margin, bubleDiameter, bubleDiameter);
			}
			offset += bubleDiameter + 2 * Margin;
			hitBox.SetWidth(hitBox.GetWidth()+ bubleDiameter);
			hitBox.SetHeight(textSize.GetHeight());
			it->hitBox = hitBox;
		}
	}
}

void GeneralizeProgressView::OnMouseMove(wxMouseEvent & event)
{
	bool hit = false;
	if (m_enableMouseSelection)
	{
		for (std::vector<ItemStatus>::iterator it = m_drawings.begin(); it != m_drawings.end(); it++)
		{
			if (it->HitTest(event.GetPosition()))
			{
				hit = true;
			}
		}
	}
	SetCursor(hit ? wxCURSOR_HAND : wxCURSOR_ARROW);
}

void GeneralizeProgressView::OnMouseDown(wxMouseEvent & event)
{
	if (m_enableMouseSelection)
	{
		for (std::vector<ItemStatus>::iterator it = m_drawings.begin(); it != m_drawings.end(); it++)
		{
			if (it->HitTest(event.GetPosition()))
			{
				SetSelected(it->biometric);
				break;
			}
		}
	}
}

void GeneralizeProgressView::Clear()
{
	std::vector<NBiometric> empty;
	NFace nullBiometric = NULL;
	SetBiometrics(empty);
	SetGeneralized(empty);
	SetSelected(nullBiometric);
	m_drawings.clear();
}

void GeneralizeProgressView::SetView(wxNView * view)
{
	m_view = view;
}

void GeneralizeProgressView::SetIcaoView(IcaoWarningsView * view)
{
	m_icaoView = view;
}

void GeneralizeProgressView::SetEnableMouseSelection(bool value)
{
	m_enableMouseSelection = value;
}

void GeneralizeProgressView::OnDataChanged()
{
	m_drawings.clear();
	for (int i = 0; i < (int)m_biometrics.size(); i++)
	{
		ItemStatus status;
		status.text = wxString::Format(wxT("%d"), i + 1);
		status.biometric = m_biometrics[i];
		status.color = wxColor(255, 165, 0);
		status.fill = false;
		m_drawings.push_back(status);
	}
	for (int i = 0; i < (int)m_generalized.size(); i++)
	{
		ItemStatus status;
		status.text = i == 0 ? wxT("Generalized: ") : wxEmptyString;
		status.biometric = m_generalized[i];
		status.color = wxColor(255, 165, 0);
		status.fill = false;
		m_drawings.push_back(status);
	}
	UpdateBiometricsStatus();
	Refresh();
}

void GeneralizeProgressView::UpdateBiometricsStatus()
{
	for (std::vector<ItemStatus>::iterator it = m_drawings.begin(); it != m_drawings.end(); it++)
	{
		NBiometric biometric = it->biometric;
		it->color = wxColor(255, 165, 0);
		it->fill = false;
		if (!biometric.IsNull())
		{
			switch(biometric.GetStatus())
			{
			case nbsOk:
				it->color = wxColor(0, 100, 0);
				it->fill = true;
				break;
			case nbsNone:
				it->fill = false;
				it->color = wxColor(255, 165, 0);
				break;
			default:
				it->color = wxColor(255, 0, 0);
				it->fill = true;
				break;
			}
		}
	}
	Refresh(true, NULL);
}

void GeneralizeProgressView::SetBiometricToView()
{
	if (m_view)
	{
		if (m_icaoView)
		{
			NFace face = NULL;
			if (!m_selected.IsNull())
				face = NObjectDynamicCast<NFace>(m_selected);
			m_icaoView->SetFace(face);
		}

		wxNFaceView * faceView = dynamic_cast<wxNFaceView*>(m_view);
		if (faceView)
		{
			NFace face = NULL;
			if (!m_selected.IsNull())
				face = NObjectDynamicCast<NFace>(m_selected);
			faceView->SetFace(face);
			return;
		}

		wxNFingerView * fingerView = dynamic_cast<wxNFingerView*>(m_view);
		if (fingerView)
		{
			NFinger finger = NULL;
			if (!m_selected.IsNull())
				finger = NObjectDynamicCast<NFinger>(m_selected);
			fingerView->SetFinger(finger);
		}

		wxNPalmView * palmView = dynamic_cast<wxNPalmView*>(m_view);
		if (palmView)
		{
			NPalm palm = NULL;
			if (!m_selected.IsNull())
				palm = NObjectDynamicCast<NPalm>(m_selected);
			palmView->SetPalm(palm);
		}
	}
}

void GeneralizeProgressView::OnPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	wxString propertyName = args.GetPropertyName();
	GeneralizeProgressView * view = reinterpret_cast<GeneralizeProgressView*>(args.GetParam());
	if (propertyName == wxT("Status"))
	{
		wxCommandEvent evt(wxEVT_BIOMETRIC_PROPERTY_CHANGED, wxID_ANY);
		wxPostEvent(view, evt);
	}
}

void GeneralizeProgressView::OnPropertyChanged(wxCommandEvent & /*event*/)
{
	UpdateBiometricsStatus();
}

}};
