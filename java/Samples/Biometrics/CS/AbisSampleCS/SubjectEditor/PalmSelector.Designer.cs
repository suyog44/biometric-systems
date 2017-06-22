namespace Neurotec.Samples
{
	partial class PalmSelector
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// PalmSelector
			// 
			this.AllowedPositions = new Neurotec.Biometrics.NFPosition[] {
        Neurotec.Biometrics.NFPosition.RightThumb,
        Neurotec.Biometrics.NFPosition.RightIndexFinger,
        Neurotec.Biometrics.NFPosition.RightMiddleFinger,
        Neurotec.Biometrics.NFPosition.RightRingFinger,
        Neurotec.Biometrics.NFPosition.RightLittleFinger,
        Neurotec.Biometrics.NFPosition.LeftThumb,
        Neurotec.Biometrics.NFPosition.LeftIndexFinger,
        Neurotec.Biometrics.NFPosition.LeftMiddleFinger,
        Neurotec.Biometrics.NFPosition.LeftRingFinger,
        Neurotec.Biometrics.NFPosition.LeftLittle};
			this.MissingPositions = new Neurotec.Biometrics.NFPosition[0];
			this.Text = "fc";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip;
	}
}
