namespace Neurotec.Samples
{
	partial class EditPhrasesForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPhrasesForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lvPhrases = new System.Windows.Forms.ListView();
			this.chPhraseId = new System.Windows.Forms.ColumnHeader();
			this.chPhrase = new System.Windows.Forms.ColumnHeader();
			this.btnRemove = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.tbPhrase = new System.Windows.Forms.TextBox();
			this.tbPhraseId = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lvPhrases);
			this.groupBox1.Controls.Add(this.btnRemove);
			this.groupBox1.Location = new System.Drawing.Point(8, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(268, 316);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// lvPhrases
			// 
			this.lvPhrases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPhraseId,
            this.chPhrase});
			this.lvPhrases.FullRowSelect = true;
			this.lvPhrases.Location = new System.Drawing.Point(6, 19);
			this.lvPhrases.MultiSelect = false;
			this.lvPhrases.Name = "lvPhrases";
			this.lvPhrases.Size = new System.Drawing.Size(256, 247);
			this.lvPhrases.TabIndex = 0;
			this.lvPhrases.UseCompatibleStateImageBehavior = false;
			this.lvPhrases.View = System.Windows.Forms.View.Details;
			// 
			// chPhraseId
			// 
			this.chPhraseId.Text = "Phrase Id";
			// 
			// chPhrase
			// 
			this.chPhrase.Text = "Phrase";
			this.chPhrase.Width = 189;
			// 
			// btnRemove
			// 
			this.btnRemove.Image = global::Neurotec.Samples.Properties.Resources.remove;
			this.btnRemove.Location = new System.Drawing.Point(221, 272);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(41, 33);
			this.btnRemove.TabIndex = 1;
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.BtnRemoveClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnAdd);
			this.groupBox2.Controls.Add(this.tbPhrase);
			this.groupBox2.Controls.Add(this.tbPhraseId);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(8, 325);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(268, 117);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Add new";
			// 
			// btnAdd
			// 
			this.btnAdd.Image = global::Neurotec.Samples.Properties.Resources.add;
			this.btnAdd.Location = new System.Drawing.Point(221, 75);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(41, 34);
			this.btnAdd.TabIndex = 4;
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
			// 
			// tbPhrase
			// 
			this.tbPhrase.Location = new System.Drawing.Point(79, 49);
			this.tbPhrase.Name = "tbPhrase";
			this.tbPhrase.Size = new System.Drawing.Size(183, 20);
			this.tbPhrase.TabIndex = 3;
			// 
			// tbPhraseId
			// 
			this.tbPhraseId.Location = new System.Drawing.Point(79, 23);
			this.tbPhraseId.Name = "tbPhraseId";
			this.tbPhraseId.Size = new System.Drawing.Size(183, 20);
			this.tbPhraseId.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Phrase:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Phrase id:";
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(201, 448);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.BtnCloseClick);
			// 
			// EditPhrasesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(288, 484);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditPhrasesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add phrase";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lvPhrases;
		private System.Windows.Forms.ColumnHeader chPhraseId;
		private System.Windows.Forms.ColumnHeader chPhrase;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TextBox tbPhrase;
		private System.Windows.Forms.TextBox tbPhraseId;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnClose;

	}
}
