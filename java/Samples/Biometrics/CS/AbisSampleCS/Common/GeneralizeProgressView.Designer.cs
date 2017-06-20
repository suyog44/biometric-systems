namespace Neurotec.Samples.Common
{
	partial class GeneralizeProgressView
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
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.lblStatus = new System.Windows.Forms.Label();
			this.tlpBiometrics = new System.Windows.Forms.TableLayoutPanel();
			this.panelPaint = new Neurotec.Samples.Common.DoubleBufferedPanel();
			this.tableLayoutPanel.SuspendLayout();
			this.tlpBiometrics.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.lblStatus, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.tlpBiometrics, 0, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(362, 47);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblStatus.Location = new System.Drawing.Point(3, 34);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(356, 13);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Text = "Generalization status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tlpBiometrics
			// 
			this.tlpBiometrics.AutoSize = true;
			this.tlpBiometrics.ColumnCount = 1;
			this.tlpBiometrics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpBiometrics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpBiometrics.Controls.Add(this.panelPaint, 0, 0);
			this.tlpBiometrics.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpBiometrics.Location = new System.Drawing.Point(3, 3);
			this.tlpBiometrics.MinimumSize = new System.Drawing.Size(0, 20);
			this.tlpBiometrics.Name = "tlpBiometrics";
			this.tlpBiometrics.RowCount = 1;
			this.tlpBiometrics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpBiometrics.Size = new System.Drawing.Size(356, 28);
			this.tlpBiometrics.TabIndex = 1;
			// 
			// panelPaint
			// 
			this.panelPaint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelPaint.Location = new System.Drawing.Point(3, 3);
			this.panelPaint.Name = "panelPaint";
			this.panelPaint.Size = new System.Drawing.Size(350, 22);
			this.panelPaint.TabIndex = 0;
			this.panelPaint.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelPaintPaint);
			this.panelPaint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelPaintMouseMove);
			this.panelPaint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PanelPaintMouseClick);
			// 
			// GeneralizationView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "GeneralizationView";
			this.Size = new System.Drawing.Size(362, 47);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.tlpBiometrics.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.TableLayoutPanel tlpBiometrics;
		private Neurotec.Samples.Common.DoubleBufferedPanel panelPaint;
	}
}
