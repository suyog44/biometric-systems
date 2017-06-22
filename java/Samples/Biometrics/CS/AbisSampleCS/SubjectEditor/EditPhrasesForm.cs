using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class EditPhrasesForm : Form
	{
		#region Private fields

		private List<Phrase> _phrases;

		#endregion

		#region Public constructor

		public EditPhrasesForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		public List<Phrase> Phrases
		{
			get { return _phrases; }
			set
			{
				_phrases = value;
				ListAllPhrases();
			}
		}

		#endregion

		#region Private form events

		private void BtnAddClick(object sender, EventArgs e)
		{
			int phraseId;

			if (string.IsNullOrEmpty(tbPhraseId.Text) || string.IsNullOrEmpty(tbPhrase.Text))
			{
				Utilities.ShowInformation("One or more fields is empty. Please fill all of the fields.");
				return;
			}

			if (!int.TryParse(tbPhraseId.Text, out  phraseId))
			{
				Utilities.ShowError("Phrase Id should be integer (above 0)!");
				return;
			}

			if (phraseId <= 0)
			{
				Utilities.ShowError("Phrase Id should be above 0!");
				return;
			}

			if (tbPhrase.Text.Contains("=") || tbPhrase.Text.Contains(";"))
			{
				Utilities.ShowError("Phrase must not countain symbols: '=', ';'!");
				return;
			}

			Phrase phraseToAdd = new Phrase(phraseId, tbPhrase.Text);
			foreach (Phrase phrase in _phrases)
			{
				if (phrase.Id == phraseToAdd.Id)
				{
					Utilities.ShowError("Another phrase with the same phrase id already exist in the list!");
					return;
				}
			}

			_phrases.Add(phraseToAdd);
			ListAllPhrases();
		}

		private void BtnRemoveClick(object sender, EventArgs e)
		{
			if (lvPhrases.SelectedItems.Count == 1)
			{
				ListViewItem toRemove = lvPhrases.SelectedItems[0];
				Phrase phrase = toRemove.Tag as Phrase;
				if (phrase != null)
					_phrases.Remove(phrase);
				lvPhrases.Items.Remove(toRemove);
			}
			else
			{
				Utilities.ShowInformation("No or more than one phrases are selected!");
			}
		}

		private void BtnCloseClick(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		#region Private methods

		private void ListAllPhrases()
		{
			lvPhrases.Items.Clear();
			foreach (Phrase phrase in _phrases)
			{
				ListViewItem item = new ListViewItem(new string[] { phrase.Id.ToString(), phrase.String });
				item.Tag = phrase;
				lvPhrases.Items.Add(item);
			}
			if (lvPhrases.Items.Count > 0)
				lvPhrases.SelectedIndices.Add(0);
		}

		#endregion
	}
}
