namespace Neurotec.Samples
{
	partial class EditSubjectTab
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.subjectTree = new Neurotec.Samples.SubjectTreeControl();
			this.pagePanel = new System.Windows.Forms.Panel();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.subjectTree);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pagePanel);
			this.splitContainer1.Size = new System.Drawing.Size(582, 402);
			this.splitContainer1.SplitterDistance = 227;
			this.splitContainer1.TabIndex = 0;
			// 
			// subjectTree
			// 
			this.subjectTree.AllowNew = ((Neurotec.Biometrics.NBiometricType)(((((Neurotec.Biometrics.NBiometricType.Face | Neurotec.Biometrics.NBiometricType.Voice)
						| Neurotec.Biometrics.NBiometricType.Fingerprint)
						| Neurotec.Biometrics.NBiometricType.Iris)
						| Neurotec.Biometrics.NBiometricType.PalmPrint)));
			this.subjectTree.AllowRemove = true;
			this.subjectTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.subjectTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.subjectTree.Location = new System.Drawing.Point(0, 0);
			this.subjectTree.Name = "subjectTree";
			this.subjectTree.SelectedItem = null;
			this.subjectTree.ShowBiometricsOnly = false;
			this.subjectTree.ShownTypes = ((Neurotec.Biometrics.NBiometricType)(((((Neurotec.Biometrics.NBiometricType.Face | Neurotec.Biometrics.NBiometricType.Voice)
						| Neurotec.Biometrics.NBiometricType.Fingerprint)
						| Neurotec.Biometrics.NBiometricType.Iris)
						| Neurotec.Biometrics.NBiometricType.PalmPrint)));
			this.subjectTree.Size = new System.Drawing.Size(227, 402);
			this.subjectTree.Subject = null;
			this.subjectTree.TabIndex = 0;
			// 
			// pagePanel
			// 
			this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pagePanel.Location = new System.Drawing.Point(0, 0);
			this.pagePanel.Name = "pagePanel";
			this.pagePanel.Size = new System.Drawing.Size(351, 402);
			this.pagePanel.TabIndex = 0;
			// 
			// EditSubjectTab
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.splitContainer1);
			this.Name = "EditSubjectTab";
			this.Size = new System.Drawing.Size(582, 402);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private SubjectTreeControl subjectTree;
		private System.Windows.Forms.Panel pagePanel;
	}
}
