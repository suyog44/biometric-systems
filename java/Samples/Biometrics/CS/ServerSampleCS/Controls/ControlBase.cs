using System.ComponentModel;
using System.Windows.Forms;
using Neurotec.Biometrics.Client;
using Neurotec.Samples.Code;
using Neurotec.Samples.Connections;

namespace Neurotec.Samples.Controls
{
	public partial class ControlBase : UserControl
	{
		#region Public constructor

		public ControlBase()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		public virtual ITemplateLoader TemplateLoader { get; set; }

		public virtual AcceleratorConnection Accelerator { get; set; }

		public virtual NBiometricClient BiometricClient { get; set; }

		#endregion

		#region Public methods

		public virtual bool IsBusy
		{
			get { return false; }
		}

		public virtual void Cancel()
		{
		}

		public virtual string GetTitle()
		{
			return string.Empty;
		}

		public int GetTemplateCount()
		{
			RunWorkerCompletedEventArgs args = LongTaskForm.RunLongTask("Calculating template count", GetTemplateCountDoWork, null);
			return (int)args.Result;
		}

		#endregion

		#region Private methods

		private void GetTemplateCountDoWork(object sender, DoWorkEventArgs e)
		{
			if (TemplateLoader != null)
			{
				e.Result = TemplateLoader.TemplateCount;
			}
			else
			{
				e.Result = -1;
			}
		}

		#endregion
	}
}
