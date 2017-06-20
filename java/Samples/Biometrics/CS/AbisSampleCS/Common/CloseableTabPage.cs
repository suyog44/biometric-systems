using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class CloseableTabPage : TabPage
	{
		#region Public constructor

		public CloseableTabPage()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private TabPageContentBase _content;

		#endregion

		#region Public properties

		public TabPageContentBase Content
		{
			get { return _content; }
			set
			{
				Controls.Clear();
				_content = value;
				if (value != null) Controls.Add(value);
			}
		}

		[DefaultValue(true)]
		public bool CanClose { get; set; }

		#endregion

		#region Public methods

		public bool Close()
		{
			if (!CanClose) return false;
			else if (_content != null)
			{
				_content.OnTabClose();
			}
			return true;
		}

		#endregion

		#region Protected methods

		protected override void OnEnter(EventArgs e)
		{
			if (_content != null) _content.OnTabEnter();
			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e)
		{
			if (_content != null) _content.OnTabLeave();
			base.OnLeave(e);
		}

		#endregion
	}
}
