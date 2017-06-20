namespace Neurotec.Samples.SubjectEditor.TenPrintCard
{
	partial class TenPrintCardPrintForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.SuspendLayout();
			// 
			// printDialog1
			// 
			this.printDialog1.ShowNetwork = false;
			this.printDialog1.UseEXDialog = true;
			// 
			// TenPrintCardPrintForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(933, 466);
			this.Name = "TenPrintCardPrintForm";
			this.Text = "TenPrintCardPrintForm";
			this.Shown += new System.EventHandler(this.TenPrintCardPrintFormShown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PrintDialog printDialog1;

	}
}
