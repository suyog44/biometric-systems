#include "Precompiled.h"
#include "PossiblePositionCollectionAdapter.h"

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	PossiblePositionAdapter::PossiblePositionAdapter(const NFRecord& record) : m_record(record)
	{
	}

	PossiblePositionAdapter::~PossiblePositionAdapter()
	{
	}

	void PossiblePositionAdapter::AddItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		NFPosition possiblePosition = NFPosition();
		NArrayWrapper<int> possiblePositionValues = NEnum::GetValues(NBiometricTypes::NFPositionNativeTypeOf());

		try
		{
			m_listViewString = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), possiblePositionValues[0]);
			possiblePosition = (NFPosition)possiblePositionValues[0];

			m_record.GetPossiblePositions().Add(possiblePosition);

			ShowCollectionInList(ctrl, index, m_listViewString);
			ShowItemCollectionInGrid(grid, index);
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);

			for (int i = 0; i < possiblePositionValues.GetCount(); i++)
			{
				if (NBiometricTypes::IsPositionPalm((NFPosition)possiblePositionValues[i]))
				{
					m_listViewString = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), possiblePositionValues[i]);
					possiblePosition = (NFPosition)possiblePositionValues[i];
					break;
				}
			}

			m_record.GetPossiblePositions().Add(possiblePosition);

			ShowCollectionInList(ctrl, index, m_listViewString);
			ShowItemCollectionInGrid(grid, index);
		}
	}

	void PossiblePositionAdapter::SwapItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index1, wxString s1, wxString s2)
	{
		NFPosition posibleposition1 = m_record.GetPossiblePositions().Get(index1 - 1);
		NFPosition posibleposition2 = m_record.GetPossiblePositions().Get(index1);

		m_record.GetPossiblePositions().RemoveAt(index1 - 1);
		m_record.GetPossiblePositions().Insert(index1 - 1, posibleposition2);

		m_record.GetPossiblePositions().RemoveAt(index1);
		m_record.GetPossiblePositions().Insert(index1, posibleposition1);

		ShowCollectionInList(ctrl, index1 - 1, s1);
		ShowCollectionInList(ctrl, index1, s2);

		ShowItemCollectionInGrid(grid, index1 - 1);
	}

	void PossiblePositionAdapter::UpdateItemCollection(wxListCtrl* ctrl, wxPropertyGridManager* grid, int index)
	{
		NFPosition possiblePosition = m_record.GetPossiblePositions().Get(index);
		NArrayWrapper<int> possiblePositionValues = NEnum::GetValues(NBiometricTypes::NFPositionNativeTypeOf());

		for (int i = 0; i < possiblePositionValues.GetCount(); i++)
		{
			if (possiblePositionValues[i] == possiblePosition)
			{
				m_listViewString = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), possiblePositionValues[i]);
			}
		}

		ShowCollectionInList(ctrl, index, m_listViewString);
		ShowItemCollectionInGrid(grid, index);
	}
	void PossiblePositionAdapter::ShowItemCollectionInGrid(wxPropertyGridManager* grid, int index)
	{
		wxPropertyGrid *pgrid = grid->GetGrid();
		pgrid->Clear();

		pgrid->Append(new wxPropertyCategory(wxT("PossiblePositions"), wxPG_LABEL));

		wxPGChoices positionChoice;
		NArrayWrapper<int> possiblePositionValues = NEnum::GetValues(NBiometricTypes::NFPositionNativeTypeOf());
		NFPosition possiblePosition = m_record.GetPossiblePositions().Get(index);

		for (int i = 0; i < possiblePositionValues.GetCount(); i++)
		{
			positionChoice.Add(NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), possiblePositionValues[i]), possiblePositionValues[i]);
		}

		pgrid->Append(new wxEnumProperty(N_TMPL_VALUE, wxPG_LABEL, positionChoice))->SetChoiceSelection(possiblePosition);
	}

	void  PossiblePositionAdapter::DeleteItem(int index)
	{
		m_record.GetPossiblePositions().RemoveAt(index);
	}

	int PossiblePositionAdapter::GetItemsCount()
	{
		return m_record.GetPossiblePositions().GetCount();
	}

	wxString PossiblePositionAdapter::PropertyValueChanged(wxString propertyName, int index, wxVariant value)
	{
		NFPosition possiblePosition;

		if (propertyName == N_TMPL_VALUE)
		{
			possiblePosition = (NFPosition)value.GetInteger();

			NArrayWrapper<int> possiblePositionValues = NEnum::GetValues(NBiometricTypes::NFPositionNativeTypeOf());

			for (int i = 0; i < possiblePositionValues.GetCount(); i++)
			{
				if (possiblePositionValues[i] == possiblePosition)
				{
					m_listViewString = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), possiblePositionValues[i]);
				}
			}

			try
			{
				m_record.GetPossiblePositions().Set(index, possiblePosition);
			}
			catch (NError& ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		return m_listViewString;
	}

	void PossiblePositionAdapter::ShowCollectionInList(wxListCtrl* ctrl, int index, wxString listString)
	{
		wxListItem item;
		item.SetId(index);
		item.SetText(wxString::Format(wxT("%i"), index));

		ctrl->InsertItem(item);
		ctrl->SetItem(item, 1, listString);
	}
}}}
