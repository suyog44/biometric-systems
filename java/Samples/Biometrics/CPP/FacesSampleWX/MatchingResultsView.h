#ifndef MATCHING_RESULTS_VIEW_H_INCLUDED
#define MATCHING_RESULTS_VIEW_H_INCLUDED

namespace Neurotec { namespace Samples
{

class MatchingResultsView: public wxScrolledWindow
{
private:
	class MatchingResult
	{
	private:
		int m_faceNumber;
		wxString m_id;
		int m_score;
		wxBitmap m_bitmap;
	
	public:
		MatchingResult(int faceNumber, const wxChar *matchedId, int score, wxBitmap & bitmap)
			: m_faceNumber(faceNumber), m_id(matchedId), m_score(score), m_bitmap(bitmap)
		{
		}

		~MatchingResult()
		{
		}

		int GetFaceNumber() const
		{
			return m_faceNumber;
		}

		const wxBitmap GetBitmap() const
		{
			return m_bitmap;
		}

		const wxString& GetId() const
		{
			return m_id;
		}

		int GetScore() const
		{
			return m_score;
		}

		bool operator< (const MatchingResult& other) const
		{
			return GetScore() > other.GetScore();
		}
	};

	typedef std::vector<MatchingResult> MatchingResultList;

public:
	MatchingResultsView(wxWindow *parent);
	virtual ~MatchingResultsView();

	void Add(int faceNumber, const wxChar *id, int score, wxBitmap & bitmap);
	void Clear();

private:
	void OnPaint(wxPaintEvent& event);
	void DrawMatchingResult(wxDC &dc, int index);
	wxRect GetImageRect();

private:
	MatchingResultList m_matchingResults;

private:
	DECLARE_EVENT_TABLE()
};

}}

#endif // MATCHING_RESULTS_VIEW_H_INCLUDED
