namespace Neurotec.Samples
{
	partial class AddFeatureForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFeatureForm));
			this.cbFeature = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.labelType = new System.Windows.Forms.Label();
			this.cbType = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// cbFeature
			// 
			this.cbFeature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFeature.FormattingEnabled = true;
			this.cbFeature.Items.AddRange(new object[] {
            "Minutia",
            "Core",
            "Delta"});
			this.cbFeature.Location = new System.Drawing.Point(61, 12);
			this.cbFeature.Name = "cbFeature";
			this.cbFeature.Size = new System.Drawing.Size(110, 21);
			this.cbFeature.TabIndex = 0;
			this.cbFeature.SelectedIndexChanged += new System.EventHandler(this.cbFeature_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Feature";
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(15, 64);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(96, 64);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// labelType
			// 
			this.labelType.AutoSize = true;
			this.labelType.Location = new System.Drawing.Point(12, 40);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(34, 13);
			this.labelType.TabIndex = 4;
			this.labelType.Text = "Type:";
			// 
			// cbType
			// 
			this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbType.FormattingEnabled = true;
			this.cbType.Location = new System.Drawing.Point(61, 37);
			this.cbType.Name = "cbType";
			this.cbType.Size = new System.Drawing.Size(110, 21);
			this.cbType.TabIndex = 5;
			// 
			// AddFeatureForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(179, 91);
			this.Controls.Add(this.cbType);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbFeature);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddFeatureForm";
			this.Text = "Add Feature";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label labelType;
		public System.Windows.Forms.ComboBox cbFeature;
		internal System.Windows.Forms.ComboBox cbType;
	}
}
