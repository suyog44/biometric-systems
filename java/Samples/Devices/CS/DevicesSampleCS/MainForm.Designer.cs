namespace Neurotec.Samples
{
	partial class MainForm
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
			if (disposing)
			{
				if (components != null) components.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.deviceManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deviceManagerToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deviceToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.showPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.topSplitContainer = new System.Windows.Forms.SplitContainer();
			this.deviceTreeView = new System.Windows.Forms.TreeView();
			this.endSequenceButton = new System.Windows.Forms.Button();
			this.startSequenceButton = new System.Windows.Forms.Button();
			this.customizeFormatButton = new System.Windows.Forms.Button();
			this.formatsComboBox = new System.Windows.Forms.ComboBox();
			this.cbGatherImages = new System.Windows.Forms.CheckBox();
			this.lblMiliseconds = new System.Windows.Forms.Label();
			this.tbMiliseconds = new System.Windows.Forms.TextBox();
			this.cbUseTimeout = new System.Windows.Forms.CheckBox();
			this.cbAutomatic = new System.Windows.Forms.CheckBox();
			this.rlCheckBox = new System.Windows.Forms.CheckBox();
			this.rrCheckBox = new System.Windows.Forms.CheckBox();
			this.rmCheckBox = new System.Windows.Forms.CheckBox();
			this.riCheckBox = new System.Windows.Forms.CheckBox();
			this.rtCheckBox = new System.Windows.Forms.CheckBox();
			this.ltCheckBox = new System.Windows.Forms.CheckBox();
			this.liCheckBox = new System.Windows.Forms.CheckBox();
			this.lmCheckBox = new System.Windows.Forms.CheckBox();
			this.lrCheckBox = new System.Windows.Forms.CheckBox();
			this.llCheckBox = new System.Windows.Forms.CheckBox();
			this.biometricDeviceImpressionTypeComboBox = new System.Windows.Forms.ComboBox();
			this.biometricDevicePositionComboBox = new System.Windows.Forms.ComboBox();
			this.deviceCaptureButton = new System.Windows.Forms.Button();
			this.bottomSplitContainer = new System.Windows.Forms.SplitContainer();
			this.devicePropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.typeLabel = new System.Windows.Forms.Label();
			this.logRichTextBox = new System.Windows.Forms.RichTextBox();
			this.mainMenuStrip.SuspendLayout();
			this.mainSplitContainer.Panel1.SuspendLayout();
			this.mainSplitContainer.Panel2.SuspendLayout();
			this.mainSplitContainer.SuspendLayout();
			this.topSplitContainer.Panel1.SuspendLayout();
			this.topSplitContainer.Panel2.SuspendLayout();
			this.topSplitContainer.SuspendLayout();
			this.bottomSplitContainer.Panel1.SuspendLayout();
			this.bottomSplitContainer.Panel2.SuspendLayout();
			this.bottomSplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.deviceManagerToolStripMenuItem,
			this.deviceToolStripMenuItem,
			this.helpToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(865, 24);
			this.mainMenuStrip.TabIndex = 0;
			this.mainMenuStrip.Text = "Main";
			// 
			// deviceManagerToolStripMenuItem
			// 
			this.deviceManagerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.newToolStripMenuItem,
			this.closeToolStripMenuItem,
			this.deviceManagerToolStripSeparator2,
			this.exitToolStripMenuItem});
			this.deviceManagerToolStripMenuItem.Name = "deviceManagerToolStripMenuItem";
			this.deviceManagerToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
			this.deviceManagerToolStripMenuItem.Text = "Device &manager";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// deviceManagerToolStripSeparator2
			// 
			this.deviceManagerToolStripSeparator2.Name = "deviceManagerToolStripSeparator2";
			this.deviceManagerToolStripSeparator2.Size = new System.Drawing.Size(100, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// deviceToolStripMenuItem
			// 
			this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.connectToolStripMenuItem,
			this.disconnectToolStripMenuItem,
			this.deviceToolStripSeparator1,
			this.showPluginToolStripMenuItem});
			this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
			this.deviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.deviceToolStripMenuItem.Text = "&Device";
			// 
			// connectToolStripMenuItem
			// 
			this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
			this.connectToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.connectToolStripMenuItem.Text = "&Connect...";
			this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
			// 
			// disconnectToolStripMenuItem
			// 
			this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
			this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.disconnectToolStripMenuItem.Text = "&Disconnect";
			this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
			// 
			// deviceToolStripSeparator1
			// 
			this.deviceToolStripSeparator1.Name = "deviceToolStripSeparator1";
			this.deviceToolStripSeparator1.Size = new System.Drawing.Size(137, 6);
			// 
			// showPluginToolStripMenuItem
			// 
			this.showPluginToolStripMenuItem.Name = "showPluginToolStripMenuItem";
			this.showPluginToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.showPluginToolStripMenuItem.Text = "Show &plugin";
			this.showPluginToolStripMenuItem.Click += new System.EventHandler(this.showPluginToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// mainSplitContainer
			// 
			this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
			this.mainSplitContainer.Name = "mainSplitContainer";
			this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// mainSplitContainer.Panel1
			// 
			this.mainSplitContainer.Panel1.Controls.Add(this.topSplitContainer);
			// 
			// mainSplitContainer.Panel2
			// 
			this.mainSplitContainer.Panel2.Controls.Add(this.bottomSplitContainer);
			this.mainSplitContainer.Size = new System.Drawing.Size(865, 705);
			this.mainSplitContainer.SplitterDistance = 364;
			this.mainSplitContainer.TabIndex = 1;
			// 
			// topSplitContainer
			// 
			this.topSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.topSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.topSplitContainer.Name = "topSplitContainer";
			// 
			// topSplitContainer.Panel1
			// 
			this.topSplitContainer.Panel1.Controls.Add(this.deviceTreeView);
			// 
			// topSplitContainer.Panel2
			// 
			this.topSplitContainer.Panel2.Controls.Add(this.endSequenceButton);
			this.topSplitContainer.Panel2.Controls.Add(this.startSequenceButton);
			this.topSplitContainer.Panel2.Controls.Add(this.customizeFormatButton);
			this.topSplitContainer.Panel2.Controls.Add(this.formatsComboBox);
			this.topSplitContainer.Panel2.Controls.Add(this.cbGatherImages);
			this.topSplitContainer.Panel2.Controls.Add(this.lblMiliseconds);
			this.topSplitContainer.Panel2.Controls.Add(this.tbMiliseconds);
			this.topSplitContainer.Panel2.Controls.Add(this.cbUseTimeout);
			this.topSplitContainer.Panel2.Controls.Add(this.cbAutomatic);
			this.topSplitContainer.Panel2.Controls.Add(this.rlCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.rrCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.rmCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.riCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.rtCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.ltCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.liCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.lmCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.lrCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.llCheckBox);
			this.topSplitContainer.Panel2.Controls.Add(this.biometricDeviceImpressionTypeComboBox);
			this.topSplitContainer.Panel2.Controls.Add(this.biometricDevicePositionComboBox);
			this.topSplitContainer.Panel2.Controls.Add(this.deviceCaptureButton);
			this.topSplitContainer.Size = new System.Drawing.Size(865, 364);
			this.topSplitContainer.SplitterDistance = 288;
			this.topSplitContainer.TabIndex = 0;
			// 
			// deviceTreeView
			// 
			this.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.deviceTreeView.HideSelection = false;
			this.deviceTreeView.Location = new System.Drawing.Point(0, 0);
			this.deviceTreeView.Name = "deviceTreeView";
			this.deviceTreeView.Size = new System.Drawing.Size(288, 364);
			this.deviceTreeView.TabIndex = 0;
			this.deviceTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.deviceTreeView_AfterSelect);
			// 
			// endSequenceButton
			// 
			this.endSequenceButton.Location = new System.Drawing.Point(234, 184);
			this.endSequenceButton.Name = "endSequenceButton";
			this.endSequenceButton.Size = new System.Drawing.Size(93, 23);
			this.endSequenceButton.TabIndex = 23;
			this.endSequenceButton.Text = "End sequence";
			this.endSequenceButton.UseVisualStyleBackColor = true;
			this.endSequenceButton.Click += new System.EventHandler(this.endSequenceButton_Click);
			// 
			// startSequenceButton
			// 
			this.startSequenceButton.Location = new System.Drawing.Point(135, 184);
			this.startSequenceButton.Name = "startSequenceButton";
			this.startSequenceButton.Size = new System.Drawing.Size(93, 23);
			this.startSequenceButton.TabIndex = 22;
			this.startSequenceButton.Text = "Start sequence";
			this.startSequenceButton.UseVisualStyleBackColor = true;
			this.startSequenceButton.Click += new System.EventHandler(this.startSequenceButton_Click);
			// 
			// customizeFormatButton
			// 
			this.customizeFormatButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.customizeFormatButton.Location = new System.Drawing.Point(333, 143);
			this.customizeFormatButton.Name = "customizeFormatButton";
			this.customizeFormatButton.Size = new System.Drawing.Size(75, 23);
			this.customizeFormatButton.TabIndex = 19;
			this.customizeFormatButton.Text = "Custom...";
			this.customizeFormatButton.UseVisualStyleBackColor = true;
			this.customizeFormatButton.Click += new System.EventHandler(this.customizeFormatButton_Click);
			// 
			// formatsComboBox
			// 
			this.formatsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.formatsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.formatsComboBox.FormattingEnabled = true;
			this.formatsComboBox.Location = new System.Drawing.Point(14, 145);
			this.formatsComboBox.Name = "formatsComboBox";
			this.formatsComboBox.Size = new System.Drawing.Size(313, 21);
			this.formatsComboBox.TabIndex = 18;
			// 
			// cbGatherImages
			// 
			this.cbGatherImages.AutoSize = true;
			this.cbGatherImages.Location = new System.Drawing.Point(14, 112);
			this.cbGatherImages.Name = "cbGatherImages";
			this.cbGatherImages.Size = new System.Drawing.Size(94, 17);
			this.cbGatherImages.TabIndex = 17;
			this.cbGatherImages.Text = "&Gather images";
			this.cbGatherImages.UseVisualStyleBackColor = true;
			this.cbGatherImages.Visible = false;
			// 
			// lblMiliseconds
			// 
			this.lblMiliseconds.AutoSize = true;
			this.lblMiliseconds.Location = new System.Drawing.Point(175, 90);
			this.lblMiliseconds.Name = "lblMiliseconds";
			this.lblMiliseconds.Size = new System.Drawing.Size(20, 13);
			this.lblMiliseconds.TabIndex = 16;
			this.lblMiliseconds.Text = "ms";
			// 
			// tbMiliseconds
			// 
			this.tbMiliseconds.Enabled = false;
			this.tbMiliseconds.Location = new System.Drawing.Point(102, 87);
			this.tbMiliseconds.Name = "tbMiliseconds";
			this.tbMiliseconds.Size = new System.Drawing.Size(67, 20);
			this.tbMiliseconds.TabIndex = 15;
			this.tbMiliseconds.Text = "0";
			this.tbMiliseconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cbUseTimeout
			// 
			this.cbUseTimeout.AutoSize = true;
			this.cbUseTimeout.Location = new System.Drawing.Point(14, 89);
			this.cbUseTimeout.Name = "cbUseTimeout";
			this.cbUseTimeout.Size = new System.Drawing.Size(82, 17);
			this.cbUseTimeout.TabIndex = 14;
			this.cbUseTimeout.Text = "&Use timeout";
			this.cbUseTimeout.UseVisualStyleBackColor = true;
			this.cbUseTimeout.CheckedChanged += new System.EventHandler(this.cbUseTimeout_CheckedChanged);
			// 
			// cbAutomatic
			// 
			this.cbAutomatic.AutoSize = true;
			this.cbAutomatic.Checked = true;
			this.cbAutomatic.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAutomatic.Location = new System.Drawing.Point(14, 65);
			this.cbAutomatic.Name = "cbAutomatic";
			this.cbAutomatic.Size = new System.Drawing.Size(73, 17);
			this.cbAutomatic.TabIndex = 13;
			this.cbAutomatic.Text = "&Automatic";
			this.cbAutomatic.UseVisualStyleBackColor = true;
			// 
			// rlCheckBox
			// 
			this.rlCheckBox.AutoSize = true;
			this.rlCheckBox.Location = new System.Drawing.Point(454, 46);
			this.rlCheckBox.Name = "rlCheckBox";
			this.rlCheckBox.Size = new System.Drawing.Size(40, 17);
			this.rlCheckBox.TabIndex = 12;
			this.rlCheckBox.Text = "RL";
			this.rlCheckBox.UseVisualStyleBackColor = true;
			// 
			// rrCheckBox
			// 
			this.rrCheckBox.AutoSize = true;
			this.rrCheckBox.Location = new System.Drawing.Point(439, 28);
			this.rrCheckBox.Name = "rrCheckBox";
			this.rrCheckBox.Size = new System.Drawing.Size(42, 17);
			this.rrCheckBox.TabIndex = 11;
			this.rrCheckBox.Text = "RR";
			this.rrCheckBox.UseVisualStyleBackColor = true;
			// 
			// rmCheckBox
			// 
			this.rmCheckBox.AutoSize = true;
			this.rmCheckBox.Location = new System.Drawing.Point(414, 15);
			this.rmCheckBox.Name = "rmCheckBox";
			this.rmCheckBox.Size = new System.Drawing.Size(43, 17);
			this.rmCheckBox.TabIndex = 10;
			this.rmCheckBox.Text = "RM";
			this.rmCheckBox.UseVisualStyleBackColor = true;
			// 
			// riCheckBox
			// 
			this.riCheckBox.AutoSize = true;
			this.riCheckBox.Location = new System.Drawing.Point(395, 32);
			this.riCheckBox.Name = "riCheckBox";
			this.riCheckBox.Size = new System.Drawing.Size(37, 17);
			this.riCheckBox.TabIndex = 9;
			this.riCheckBox.Text = "RI";
			this.riCheckBox.UseVisualStyleBackColor = true;
			// 
			// rtCheckBox
			// 
			this.rtCheckBox.AutoSize = true;
			this.rtCheckBox.Location = new System.Drawing.Point(385, 64);
			this.rtCheckBox.Name = "rtCheckBox";
			this.rtCheckBox.Size = new System.Drawing.Size(41, 17);
			this.rtCheckBox.TabIndex = 8;
			this.rtCheckBox.Text = "RT";
			this.rtCheckBox.UseVisualStyleBackColor = true;
			// 
			// ltCheckBox
			// 
			this.ltCheckBox.AutoSize = true;
			this.ltCheckBox.Location = new System.Drawing.Point(341, 64);
			this.ltCheckBox.Name = "ltCheckBox";
			this.ltCheckBox.Size = new System.Drawing.Size(39, 17);
			this.ltCheckBox.TabIndex = 7;
			this.ltCheckBox.Text = "LT";
			this.ltCheckBox.UseVisualStyleBackColor = true;
			// 
			// liCheckBox
			// 
			this.liCheckBox.AutoSize = true;
			this.liCheckBox.Location = new System.Drawing.Point(327, 32);
			this.liCheckBox.Name = "liCheckBox";
			this.liCheckBox.Size = new System.Drawing.Size(35, 17);
			this.liCheckBox.TabIndex = 6;
			this.liCheckBox.Text = "LI";
			this.liCheckBox.UseVisualStyleBackColor = true;
			// 
			// lmCheckBox
			// 
			this.lmCheckBox.AutoSize = true;
			this.lmCheckBox.Location = new System.Drawing.Point(300, 15);
			this.lmCheckBox.Name = "lmCheckBox";
			this.lmCheckBox.Size = new System.Drawing.Size(41, 17);
			this.lmCheckBox.TabIndex = 5;
			this.lmCheckBox.Text = "LM";
			this.lmCheckBox.UseVisualStyleBackColor = true;
			// 
			// lrCheckBox
			// 
			this.lrCheckBox.AutoSize = true;
			this.lrCheckBox.Location = new System.Drawing.Point(273, 28);
			this.lrCheckBox.Name = "lrCheckBox";
			this.lrCheckBox.Size = new System.Drawing.Size(40, 17);
			this.lrCheckBox.TabIndex = 4;
			this.lrCheckBox.Text = "LR";
			this.lrCheckBox.UseVisualStyleBackColor = true;
			// 
			// llCheckBox
			// 
			this.llCheckBox.AutoSize = true;
			this.llCheckBox.Location = new System.Drawing.Point(250, 46);
			this.llCheckBox.Name = "llCheckBox";
			this.llCheckBox.Size = new System.Drawing.Size(38, 17);
			this.llCheckBox.TabIndex = 3;
			this.llCheckBox.Text = "LL";
			this.llCheckBox.UseVisualStyleBackColor = true;
			// 
			// biometricDeviceImpressionTypeComboBox
			// 
			this.biometricDeviceImpressionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.biometricDeviceImpressionTypeComboBox.FormattingEnabled = true;
			this.biometricDeviceImpressionTypeComboBox.Location = new System.Drawing.Point(14, 11);
			this.biometricDeviceImpressionTypeComboBox.Name = "biometricDeviceImpressionTypeComboBox";
			this.biometricDeviceImpressionTypeComboBox.Size = new System.Drawing.Size(214, 21);
			this.biometricDeviceImpressionTypeComboBox.TabIndex = 1;
			this.biometricDeviceImpressionTypeComboBox.SelectionChangeCommitted += new System.EventHandler(this.biometricDeviceImpressionTypeComboBox_SelectionChangeCommitted);
			// 
			// biometricDevicePositionComboBox
			// 
			this.biometricDevicePositionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.biometricDevicePositionComboBox.FormattingEnabled = true;
			this.biometricDevicePositionComboBox.Location = new System.Drawing.Point(14, 37);
			this.biometricDevicePositionComboBox.Name = "biometricDevicePositionComboBox";
			this.biometricDevicePositionComboBox.Size = new System.Drawing.Size(214, 21);
			this.biometricDevicePositionComboBox.TabIndex = 2;
			// 
			// deviceCaptureButton
			// 
			this.deviceCaptureButton.Location = new System.Drawing.Point(14, 184);
			this.deviceCaptureButton.Name = "deviceCaptureButton";
			this.deviceCaptureButton.Size = new System.Drawing.Size(75, 23);
			this.deviceCaptureButton.TabIndex = 0;
			this.deviceCaptureButton.Text = "Capture";
			this.deviceCaptureButton.UseVisualStyleBackColor = true;
			this.deviceCaptureButton.Click += new System.EventHandler(this.deviceCaptureButton_Click);
			// 
			// bottomSplitContainer
			// 
			this.bottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bottomSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.bottomSplitContainer.Name = "bottomSplitContainer";
			// 
			// bottomSplitContainer.Panel1
			// 
			this.bottomSplitContainer.Panel1.Controls.Add(this.devicePropertyGrid);
			this.bottomSplitContainer.Panel1.Controls.Add(this.typeLabel);
			// 
			// bottomSplitContainer.Panel2
			// 
			this.bottomSplitContainer.Panel2.Controls.Add(this.logRichTextBox);
			this.bottomSplitContainer.Size = new System.Drawing.Size(865, 337);
			this.bottomSplitContainer.SplitterDistance = 288;
			this.bottomSplitContainer.TabIndex = 0;
			// 
			// devicePropertyGrid
			// 
			this.devicePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.devicePropertyGrid.Location = new System.Drawing.Point(0, 13);
			this.devicePropertyGrid.Name = "devicePropertyGrid";
			this.devicePropertyGrid.Size = new System.Drawing.Size(288, 324);
			this.devicePropertyGrid.TabIndex = 0;
			// 
			// typeLabel
			// 
			this.typeLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.typeLabel.Location = new System.Drawing.Point(0, 0);
			this.typeLabel.Name = "typeLabel";
			this.typeLabel.Size = new System.Drawing.Size(288, 13);
			this.typeLabel.TabIndex = 1;
			this.typeLabel.Text = "Type";
			// 
			// logRichTextBox
			// 
			this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logRichTextBox.Location = new System.Drawing.Point(0, 0);
			this.logRichTextBox.Name = "logRichTextBox";
			this.logRichTextBox.ReadOnly = true;
			this.logRichTextBox.Size = new System.Drawing.Size(573, 337);
			this.logRichTextBox.TabIndex = 1;
			this.logRichTextBox.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(865, 729);
			this.Controls.Add(this.mainSplitContainer);
			this.Controls.Add(this.mainMenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "MainForm";
			this.Text = "Device Manager";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.mainSplitContainer.Panel1.ResumeLayout(false);
			this.mainSplitContainer.Panel2.ResumeLayout(false);
			this.mainSplitContainer.ResumeLayout(false);
			this.topSplitContainer.Panel1.ResumeLayout(false);
			this.topSplitContainer.Panel2.ResumeLayout(false);
			this.topSplitContainer.Panel2.PerformLayout();
			this.topSplitContainer.ResumeLayout(false);
			this.bottomSplitContainer.Panel1.ResumeLayout(false);
			this.bottomSplitContainer.Panel2.ResumeLayout(false);
			this.bottomSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.SplitContainer mainSplitContainer;
		private System.Windows.Forms.SplitContainer topSplitContainer;
		private System.Windows.Forms.TreeView deviceTreeView;
		private System.Windows.Forms.SplitContainer bottomSplitContainer;
		private System.Windows.Forms.RichTextBox logRichTextBox;
		private System.Windows.Forms.PropertyGrid devicePropertyGrid;
		private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showPluginToolStripMenuItem;
		private System.Windows.Forms.Button deviceCaptureButton;
		private System.Windows.Forms.ComboBox biometricDeviceImpressionTypeComboBox;
		private System.Windows.Forms.ComboBox biometricDevicePositionComboBox;
		private System.Windows.Forms.CheckBox rlCheckBox;
		private System.Windows.Forms.CheckBox rrCheckBox;
		private System.Windows.Forms.CheckBox rmCheckBox;
		private System.Windows.Forms.CheckBox riCheckBox;
		private System.Windows.Forms.CheckBox rtCheckBox;
		private System.Windows.Forms.CheckBox ltCheckBox;
		private System.Windows.Forms.CheckBox liCheckBox;
		private System.Windows.Forms.CheckBox lmCheckBox;
		private System.Windows.Forms.CheckBox lrCheckBox;
		private System.Windows.Forms.CheckBox llCheckBox;
		private System.Windows.Forms.ToolStripMenuItem deviceManagerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.Label lblMiliseconds;
		private System.Windows.Forms.TextBox tbMiliseconds;
		private System.Windows.Forms.CheckBox cbUseTimeout;
		private System.Windows.Forms.CheckBox cbAutomatic;
		private System.Windows.Forms.Label typeLabel;
		private System.Windows.Forms.CheckBox cbGatherImages;
		private System.Windows.Forms.ComboBox formatsComboBox;
		private System.Windows.Forms.Button customizeFormatButton;
		private System.Windows.Forms.Button endSequenceButton;
		private System.Windows.Forms.Button startSequenceButton;
		private System.Windows.Forms.ToolStripSeparator deviceManagerToolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator1;
	}
}

