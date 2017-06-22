using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Samples
{
	public partial class GetSubjectDialog : Form
	{
		#region Public constructor
		public GetSubjectDialog()
		{
			InitializeComponent();
		}
		#endregion

		#region Public properties

		public NSubject Subject { get; set; }
		public NBiometricClient Client { get; set; }

		#endregion

		#region Private form events

		private void BtnOkClick(object sender, EventArgs e)
		{
			try
			{
				NSubject subj = new NSubject { Id = tbId.Text };

				NBiometricStatus status = Client.Get(subj);
				if (status != NBiometricStatus.Ok)
				{
					Utilities.ShowInformation("Failed to retrieve subject. Status: {0}", status);
				}
				else
				{
					Subject = subj;
					DialogResult = DialogResult.OK;
				}
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
		}

		private void GetSubjectDialogLoad(object sender, EventArgs e)
		{
			if (Client == null) throw new ArgumentNullException();

			try
			{
				NBiometricOperations operations = Client.LocalOperations;
				if (Client.RemoteConnections.Count > 0)
					operations |= Client.RemoteConnections[0].Operations;

				if ((operations & NBiometricOperations.ListIds) == NBiometricOperations.ListIds)
				{
					tbId.AutoCompleteCustomSource.AddRange(Client.ListIds());
				}
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
		}

		private void TbIdKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnOk.PerformClick();
			}
		}

		#endregion
	}
}
