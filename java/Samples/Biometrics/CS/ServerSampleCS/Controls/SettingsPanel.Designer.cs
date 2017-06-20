namespace Neurotec.Samples.Controls
{
	partial class SettingsPanel
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.cbMatchingThreshold = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabFingers = new System.Windows.Forms.TabPage();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.nudFingersMaxRotation = new System.Windows.Forms.NumericUpDown();
			this.cbFingersMatchingSpeed = new System.Windows.Forms.ComboBox();
			this.tabFaces = new System.Windows.Forms.TabPage();
			this.label10 = new System.Windows.Forms.Label();
			this.cbFacesMatchingSpeed = new System.Windows.Forms.ComboBox();
			this.tabIrises = new System.Windows.Forms.TabPage();
			this.label13 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.nudIrisesMaxRotation = new System.Windows.Forms.NumericUpDown();
			this.cbIrisesMatchingSpeed = new System.Windows.Forms.ComboBox();
			this.tabPalms = new System.Windows.Forms.TabPage();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.nudPalmsMaxRotation = new System.Windows.Forms.NumericUpDown();
			this.cbPalmsMatchingSpeed = new System.Windows.Forms.ComboBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnApply = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tabFingers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudFingersMaxRotation)).BeginInit();
			this.tabFaces.SuspendLayout();
			this.tabIrises.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudIrisesMaxRotation)).BeginInit();
			this.tabPalms.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPalmsMaxRotation)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabGeneral);
			this.tabControl.Controls.Add(this.tabFingers);
			this.tabControl.Controls.Add(this.tabFaces);
			this.tabControl.Controls.Add(this.tabIrises);
			this.tabControl.Controls.Add(this.tabPalms);
			this.tabControl.Location = new System.Drawing.Point(4, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(522, 265);
			this.tabControl.TabIndex = 3;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.cbMatchingThreshold);
			this.tabGeneral.Controls.Add(this.label1);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(514, 239);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// cbMatchingThreshold
			// 
			this.cbMatchingThreshold.FormattingEnabled = true;
			this.cbMatchingThreshold.Location = new System.Drawing.Point(109, 9);
			this.cbMatchingThreshold.Name = "cbMatchingThreshold";
			this.cbMatchingThreshold.Size = new System.Drawing.Size(146, 21);
			this.cbMatchingThreshold.TabIndex = 4;
			this.cbMatchingThreshold.Validating += new System.ComponentModel.CancelEventHandler(this.MatchingThresholdValidating);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Matching threshold:";
			// 
			// tabFingers
			// 
			this.tabFingers.Controls.Add(this.label6);
			this.tabFingers.Controls.Add(this.label8);
			this.tabFingers.Controls.Add(this.nudFingersMaxRotation);
			this.tabFingers.Controls.Add(this.cbFingersMatchingSpeed);
			this.tabFingers.Location = new System.Drawing.Point(4, 22);
			this.tabFingers.Name = "tabFingers";
			this.tabFingers.Padding = new System.Windows.Forms.Padding(3);
			this.tabFingers.Size = new System.Drawing.Size(514, 239);
			this.tabFingers.TabIndex = 1;
			this.tabFingers.Text = "Fingers";
			this.tabFingers.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 35);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(83, 13);
			this.label6.TabIndex = 22;
			this.label6.Text = "Maximal rotation";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(38, 13);
			this.label8.TabIndex = 20;
			this.label8.Text = "Speed";
			// 
			// nudFingersMaxRotation
			// 
			this.nudFingersMaxRotation.Location = new System.Drawing.Point(188, 33);
			this.nudFingersMaxRotation.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
			this.nudFingersMaxRotation.Name = "nudFingersMaxRotation";
			this.nudFingersMaxRotation.Size = new System.Drawing.Size(89, 20);
			this.nudFingersMaxRotation.TabIndex = 6;
			// 
			// cbFingersMatchingSpeed
			// 
			this.cbFingersMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFingersMatchingSpeed.FormattingEnabled = true;
			this.cbFingersMatchingSpeed.Location = new System.Drawing.Point(188, 6);
			this.cbFingersMatchingSpeed.Name = "cbFingersMatchingSpeed";
			this.cbFingersMatchingSpeed.Size = new System.Drawing.Size(154, 21);
			this.cbFingersMatchingSpeed.TabIndex = 4;
			// 
			// tabFaces
			// 
			this.tabFaces.Controls.Add(this.label10);
			this.tabFaces.Controls.Add(this.cbFacesMatchingSpeed);
			this.tabFaces.Location = new System.Drawing.Point(4, 22);
			this.tabFaces.Name = "tabFaces";
			this.tabFaces.Size = new System.Drawing.Size(514, 239);
			this.tabFaces.TabIndex = 2;
			this.tabFaces.Text = "Faces";
			this.tabFaces.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(9, 15);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(38, 13);
			this.label10.TabIndex = 8;
			this.label10.Text = "Speed";
			// 
			// cbFacesMatchingSpeed
			// 
			this.cbFacesMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFacesMatchingSpeed.FormattingEnabled = true;
			this.cbFacesMatchingSpeed.Location = new System.Drawing.Point(70, 12);
			this.cbFacesMatchingSpeed.Name = "cbFacesMatchingSpeed";
			this.cbFacesMatchingSpeed.Size = new System.Drawing.Size(173, 21);
			this.cbFacesMatchingSpeed.TabIndex = 4;
			// 
			// tabIrises
			// 
			this.tabIrises.Controls.Add(this.label13);
			this.tabIrises.Controls.Add(this.label15);
			this.tabIrises.Controls.Add(this.nudIrisesMaxRotation);
			this.tabIrises.Controls.Add(this.cbIrisesMatchingSpeed);
			this.tabIrises.Location = new System.Drawing.Point(4, 22);
			this.tabIrises.Name = "tabIrises";
			this.tabIrises.Size = new System.Drawing.Size(514, 239);
			this.tabIrises.TabIndex = 3;
			this.tabIrises.Text = "Irises";
			this.tabIrises.UseVisualStyleBackColor = true;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(3, 36);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(83, 13);
			this.label13.TabIndex = 22;
			this.label13.Text = "Maximal rotation";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(3, 10);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(38, 13);
			this.label15.TabIndex = 20;
			this.label15.Text = "Speed";
			// 
			// nudIrisesMaxRotation
			// 
			this.nudIrisesMaxRotation.Location = new System.Drawing.Point(182, 34);
			this.nudIrisesMaxRotation.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
			this.nudIrisesMaxRotation.Name = "nudIrisesMaxRotation";
			this.nudIrisesMaxRotation.Size = new System.Drawing.Size(100, 20);
			this.nudIrisesMaxRotation.TabIndex = 6;
			// 
			// cbIrisesMatchingSpeed
			// 
			this.cbIrisesMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbIrisesMatchingSpeed.FormattingEnabled = true;
			this.cbIrisesMatchingSpeed.Location = new System.Drawing.Point(182, 7);
			this.cbIrisesMatchingSpeed.Name = "cbIrisesMatchingSpeed";
			this.cbIrisesMatchingSpeed.Size = new System.Drawing.Size(170, 21);
			this.cbIrisesMatchingSpeed.TabIndex = 4;
			// 
			// tabPalms
			// 
			this.tabPalms.Controls.Add(this.label18);
			this.tabPalms.Controls.Add(this.label19);
			this.tabPalms.Controls.Add(this.nudPalmsMaxRotation);
			this.tabPalms.Controls.Add(this.cbPalmsMatchingSpeed);
			this.tabPalms.Location = new System.Drawing.Point(4, 22);
			this.tabPalms.Name = "tabPalms";
			this.tabPalms.Size = new System.Drawing.Size(514, 239);
			this.tabPalms.TabIndex = 4;
			this.tabPalms.Text = "Palms";
			this.tabPalms.UseVisualStyleBackColor = true;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(3, 37);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(83, 13);
			this.label18.TabIndex = 28;
			this.label18.Text = "Maximal rotation";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(3, 10);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(38, 13);
			this.label19.TabIndex = 27;
			this.label19.Text = "Speed";
			// 
			// nudPalmsMaxRotation
			// 
			this.nudPalmsMaxRotation.Location = new System.Drawing.Point(185, 35);
			this.nudPalmsMaxRotation.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
			this.nudPalmsMaxRotation.Name = "nudPalmsMaxRotation";
			this.nudPalmsMaxRotation.Size = new System.Drawing.Size(89, 20);
			this.nudPalmsMaxRotation.TabIndex = 5;
			// 
			// cbPalmsMatchingSpeed
			// 
			this.cbPalmsMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPalmsMatchingSpeed.FormattingEnabled = true;
			this.cbPalmsMatchingSpeed.Location = new System.Drawing.Point(185, 8);
			this.cbPalmsMatchingSpeed.Name = "cbPalmsMatchingSpeed";
			this.cbPalmsMatchingSpeed.Size = new System.Drawing.Size(188, 21);
			this.cbPalmsMatchingSpeed.TabIndex = 4;
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReset.Location = new System.Drawing.Point(4, 278);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(75, 23);
			this.btnReset.TabIndex = 1;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.BtnResetClick);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnApply.Location = new System.Drawing.Point(85, 278);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.BtnApplyClick);
			// 
			// SettingsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.tabControl);
			this.Name = "SettingsPanel";
			this.Size = new System.Drawing.Size(526, 305);
			this.Load += new System.EventHandler(this.SettingsPanelLoad);
			this.tabControl.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.tabFingers.ResumeLayout(false);
			this.tabFingers.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudFingersMaxRotation)).EndInit();
			this.tabFaces.ResumeLayout(false);
			this.tabFaces.PerformLayout();
			this.tabIrises.ResumeLayout(false);
			this.tabIrises.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudIrisesMaxRotation)).EndInit();
			this.tabPalms.ResumeLayout(false);
			this.tabPalms.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPalmsMaxRotation)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabFingers;
		private System.Windows.Forms.TabPage tabFaces;
		private System.Windows.Forms.TabPage tabIrises;
		private System.Windows.Forms.TabPage tabPalms;
		private System.Windows.Forms.ComboBox cbMatchingThreshold;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown nudFingersMaxRotation;
		private System.Windows.Forms.ComboBox cbFingersMatchingSpeed;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox cbFacesMatchingSpeed;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown nudIrisesMaxRotation;
		private System.Windows.Forms.ComboBox cbIrisesMatchingSpeed;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.NumericUpDown nudPalmsMaxRotation;
		private System.Windows.Forms.ComboBox cbPalmsMatchingSpeed;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Label label13;
	}
}
