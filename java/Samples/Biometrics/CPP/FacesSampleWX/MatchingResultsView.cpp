#include "Precompiled.h"
#include "MatchingResultsView.h"

namespace Neurotec { namespace Samples
{

const int MinMatchingResultWidth = 200;
const int MatchingResultHeight = 96 + 10;

BEGIN_EVENT_TABLE(MatchingResultsView, wxScrolledWindow)
EVT_PAINT(MatchingResultsView::OnPaint)
END_EVENT_TABLE()

MatchingResultsView::MatchingResultsView(wxWindow* parent)
	: wxScrolledWindow(parent, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxHSCROLL | wxFULL_REPAINT_ON_RESIZE | wxBORDER_SIMPLE)
{
	SetScrollRate(1, MatchingResultHeight);
	SetBackgroundStyle(wxBG_STYLE_CUSTOM);
}

MatchingResultsView::~MatchingResultsView()
{
}

void MatchingResultsView::Add(int faceNumber, const wxChar *id, int score, wxBitmap & bitmap)
{
	m_matchingResults.push_back(MatchingResult(faceNumber, id, score, bitmap));
	std::sort(m_matchingResults.begin(), m_matchingResults.end());

	SetVirtualSize(MinMatchingResultWidth, (int)(MatchingResultHeight * m_matchingResults.size()));
	Refresh();
}

void MatchingResultsView::Clear()
{
	m_matchingResults.clear();
	SetVirtualSize(0, 0);
	Refresh();
}

void MatchingResultsView::OnPaint(wxPaintEvent& /*event*/)
{
	wxBufferedPaintDC dc(this);
	PrepareDC(dc);

	dc.SetBackground(wxBrush(this->GetBackgroundColour()));
	dc.Clear();
	dc.SetBackground(wxNullBrush);
	dc.SetFont(GetFont());
	if (m_matchingResults.size() > 0)
	{
		for (size_t i = 0; i < m_matchingResults.size(); i++)
		{
			DrawMatchingResult(dc, (int)i);
		}
	}
	else
	{
		dc.DrawText(wxT("No matching results."), 10, 10);
	}
}

void MatchingResultsView::DrawMatchingResult(wxDC &dc, int index)
{
	MatchingResult *mr = &m_matchingResults[index];
	int offsetY = MatchingResultHeight * index;

	wxBitmap bmp = mr->GetBitmap();
	if (bmp.IsOk())
	{
		dc.DrawBitmap(bmp, 5, 5 + offsetY);
	}
	wxString faceNumberString = wxString::Format(wxT("Face #%d"), mr->GetFaceNumber());
	wxString idString = wxString::Format(wxT("Matched with: %s"), mr->GetId().c_str());
	wxCoord textWidth, textHeight;
	dc.GetTextExtent(idString, &textWidth, &textHeight);
	int textX = MatchingResultHeight + 10;
	int textY = 5 + offsetY;
	dc.DrawText(faceNumberString, textX, textY);
	textY += textHeight + 5;
	dc.DrawText(idString, textX, textY);
	textY += textHeight + 5;
	dc.DrawText(wxString::Format(wxT("Score: %d"), mr->GetScore()), textX, textY);
}

}}
