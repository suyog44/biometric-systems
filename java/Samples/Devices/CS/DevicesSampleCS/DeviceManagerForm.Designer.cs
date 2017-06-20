namespace Neurotec.Samples
{
	partial class DeviceManagerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceManagerForm));
			this.cbAutoplug = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnAccept = new System.Windows.Forms.Button();
			this.deviceTypesGroupBox = new System.Windows.Forms.GroupBox();
			this.captureDeviceCheckBox = new System.Windows.Forms.CheckBox();
			this.microphoneCheckBox = new System.Windows.Forms.CheckBox();
			this.anyCheckBox = new System.Windows.Forms.CheckBox();
			this.fScannerCheckBox = new System.Windows.Forms.CheckBox();
			this.cameraCheckBox = new System.Windows.Forms.CheckBox();
			this.irisScannerCheckBox = new System.Windows.Forms.CheckBox();
			this.palmScannerCheckBox = new System.Windows.Forms.CheckBox();
			this.fingerScannerCheckBox = new System.Windows.Forms.CheckBox();
			this.biometricDeviceCheckBox = new System.Windows.Forms.CheckBox();
			this.deviceTypesGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbAutoplug
			// 
			this.cbAutoplug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbAutoplug.AutoSize = true;
			this.cbAutoplug.Checked = true;
			this.cbAutoplug.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAutoplug.Location = new System.Drawing.Point(9, 246);
			this.cbAutoplug.Name = "cbAutoplug";
			this.cbAutoplug.Size = new System.Drawing.Size(71, 17);
			this.cbAutoplug.TabIndex = 1;
			this.cbAutoplug.Text = "&Auto plug";
			this.cbAutoplug.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(184, 271);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnAccept
			// 
			this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnAccept.Location = new System.Drawing.Point(103, 271);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 3;
			this.btnAccept.Text = "&OK";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// deviceTypesGroupBox
			// 
			this.deviceTypesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.deviceTypesGroupBox.Controls.Add(this.captureDeviceCheckBox);
			this.deviceTypesGroupBox.Controls.Add(this.microphoneCheckBox);
			this.deviceTypesGroupBox.Controls.Add(this.anyCheckBox);
			this.deviceTypesGroupBox.Controls.Add(this.fScannerCheckBox);
			this.deviceTypesGroupBox.Controls.Add(this.cameraCheckBox);
			this.deviceTypesGroupBox.Controls.Add(this.irisScannerCheckBox);
			this.deviceTypesGroupBox.Controls.Add(this.palmScannerCheckBox);
			this.deviceTypesGroupBox.Controls.Add(this.fingerScannerCheckBox);
			this.deviceTypesGroupBox.Controls.Add(this.biometricDeviceCheckBox);
			this.deviceTypesGroupBox.Location = new System.Drawing.Point(12, 12);
			this.deviceTypesGroupBox.Name = "deviceTypesGroupBox";
			this.deviceTypesGroupBox.Size = new System.Drawing.Size(250, 228);
			this.deviceTypesGroupBox.TabIndex = 0;
			this.deviceTypesGroupBox.TabStop = false;
			this.deviceTypesGroupBox.Text = "Device types";
			// 
			// captureDeviceCheckBox
			// 
			this.captureDeviceCheckBox.AutoCheck = false;
			this.captureDeviceCheckBox.AutoSize = true;
			this.captureDeviceCheckBox.Location = new System.Drawing.Point(22, 42);
			this.captureDeviceCheckBox.Name = "captureDeviceCheckBox";
			this.captureDeviceCheckBox.Size = new System.Drawing.Size(100, 17);
			this.captureDeviceCheckBox.TabIndex = 8;
			this.captureDeviceCheckBox.Text = "Capture Device";
			this.captureDeviceCheckBox.UseVisualStyleBackColor = true;
			this.captureDeviceCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// microphoneCheckBox
			// 
			this.microphoneCheckBox.AutoCheck = false;
			this.microphoneCheckBox.AutoSize = true;
			this.microphoneCheckBox.Location = new System.Drawing.Point(40, 89);
			this.microphoneCheckBox.Name = "microphoneCheckBox";
			this.microphoneCheckBox.Size = new System.Drawing.Size(82, 17);
			this.microphoneCheckBox.TabIndex = 7;
			this.microphoneCheckBox.Text = "Microphone";
			this.microphoneCheckBox.UseVisualStyleBackColor = true;
			this.microphoneCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// anyCheckBox
			// 
			this.anyCheckBox.AutoCheck = false;
			this.anyCheckBox.AutoSize = true;
			this.anyCheckBox.Location = new System.Drawing.Point(6, 19);
			this.anyCheckBox.Name = "anyCheckBox";
			this.anyCheckBox.Size = new System.Drawing.Size(44, 17);
			this.anyCheckBox.TabIndex = 0;
			this.anyCheckBox.Text = "Any";
			this.anyCheckBox.ThreeState = true;
			this.anyCheckBox.UseVisualStyleBackColor = true;
			this.anyCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// fScannerCheckBox
			// 
			this.fScannerCheckBox.AutoCheck = false;
			this.fScannerCheckBox.AutoSize = true;
			this.fScannerCheckBox.Location = new System.Drawing.Point(40, 135);
			this.fScannerCheckBox.Name = "fScannerCheckBox";
			this.fScannerCheckBox.Size = new System.Drawing.Size(73, 17);
			this.fScannerCheckBox.TabIndex = 3;
			this.fScannerCheckBox.Text = "F scanner";
			this.fScannerCheckBox.UseVisualStyleBackColor = true;
			this.fScannerCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// cameraCheckBox
			// 
			this.cameraCheckBox.AutoCheck = false;
			this.cameraCheckBox.AutoSize = true;
			this.cameraCheckBox.Location = new System.Drawing.Point(40, 65);
			this.cameraCheckBox.Name = "cameraCheckBox";
			this.cameraCheckBox.Size = new System.Drawing.Size(62, 17);
			this.cameraCheckBox.TabIndex = 1;
			this.cameraCheckBox.Text = "Camera";
			this.cameraCheckBox.UseVisualStyleBackColor = true;
			this.cameraCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// irisScannerCheckBox
			// 
			this.irisScannerCheckBox.AutoCheck = false;
			this.irisScannerCheckBox.AutoSize = true;
			this.irisScannerCheckBox.Location = new System.Drawing.Point(40, 204);
			this.irisScannerCheckBox.Name = "irisScannerCheckBox";
			this.irisScannerCheckBox.Size = new System.Drawing.Size(80, 17);
			this.irisScannerCheckBox.TabIndex = 6;
			this.irisScannerCheckBox.Text = "Iris scanner";
			this.irisScannerCheckBox.UseVisualStyleBackColor = true;
			this.irisScannerCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// palmScannerCheckBox
			// 
			this.palmScannerCheckBox.AutoCheck = false;
			this.palmScannerCheckBox.AutoSize = true;
			this.palmScannerCheckBox.Location = new System.Drawing.Point(56, 181);
			this.palmScannerCheckBox.Name = "palmScannerCheckBox";
			this.palmScannerCheckBox.Size = new System.Drawing.Size(90, 17);
			this.palmScannerCheckBox.TabIndex = 5;
			this.palmScannerCheckBox.Text = "Palm scanner";
			this.palmScannerCheckBox.UseVisualStyleBackColor = true;
			this.palmScannerCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// fingerScannerCheckBox
			// 
			this.fingerScannerCheckBox.AutoCheck = false;
			this.fingerScannerCheckBox.AutoSize = true;
			this.fingerScannerCheckBox.Location = new System.Drawing.Point(56, 158);
			this.fingerScannerCheckBox.Name = "fingerScannerCheckBox";
			this.fingerScannerCheckBox.Size = new System.Drawing.Size(96, 17);
			this.fingerScannerCheckBox.TabIndex = 4;
			this.fingerScannerCheckBox.Text = "Finger scanner";
			this.fingerScannerCheckBox.UseVisualStyleBackColor = true;
			this.fingerScannerCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// biometricDeviceCheckBox
			// 
			this.biometricDeviceCheckBox.AutoCheck = false;
			this.biometricDeviceCheckBox.AutoSize = true;
			this.biometricDeviceCheckBox.Location = new System.Drawing.Point(22, 112);
			this.biometricDeviceCheckBox.Name = "biometricDeviceCheckBox";
			this.biometricDeviceCheckBox.Size = new System.Drawing.Size(104, 17);
			this.biometricDeviceCheckBox.TabIndex = 2;
			this.biometricDeviceCheckBox.Text = "Biometric device";
			this.biometricDeviceCheckBox.UseVisualStyleBackColor = true;
			this.biometricDeviceCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
			// 
			// DeviceManagerForm
			// 
			this.AcceptButton = this.btnAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(274, 306);
			this.Controls.Add(this.deviceTypesGroupBox);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.cbAutoplug);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DeviceManagerForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "DeviceManager";
			this.deviceTypesGroupBox.ResumeLayout(false);
			this.deviceTypesGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.CheckBox cbAutoplug;
		private System.Windows.Forms.GroupBox deviceTypesGroupBox;
		private System.Windows.Forms.CheckBox fScannerCheckBox;
		private System.Windows.Forms.CheckBox cameraCheckBox;
		private System.Windows.Forms.CheckBox irisScannerCheckBox;
		private System.Windows.Forms.CheckBox palmScannerCheckBox;
		private System.Windows.Forms.CheckBox fingerScannerCheckBox;
		private System.Windows.Forms.CheckBox biometricDeviceCheckBox;
		private System.Windows.Forms.CheckBox anyCheckBox;
		private System.Windows.Forms.CheckBox microphoneCheckBox;
		private System.Windows.Forms.CheckBox captureDeviceCheckBox;
	}
}
