namespace Neurotec.Samples
{
	partial class NewFingerViewForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewFingerViewForm));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tbSizeY = new System.Windows.Forms.TextBox();
			this.tbSizeX = new System.Windows.Forms.TextBox();
			this.tbVertRes = new System.Windows.Forms.TextBox();
			this.tbHorRes = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(198, 95);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(59, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(263, 94);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(60, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Vertical size";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(160, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Horizontal size";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 57);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Vertical resolution";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(160, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(102, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Horizontal resolution";
			// 
			// tbSizeY
			// 
			this.tbSizeY.Location = new System.Drawing.Point(113, 21);
			this.tbSizeY.Name = "tbSizeY";
			this.tbSizeY.Size = new System.Drawing.Size(41, 20);
			this.tbSizeY.TabIndex = 6;
			this.tbSizeY.Text = "400";
			this.tbSizeY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberBox_KeyPress);
			// 
			// tbSizeX
			// 
			this.tbSizeX.Location = new System.Drawing.Point(266, 20);
			this.tbSizeX.Name = "tbSizeX";
			this.tbSizeX.Size = new System.Drawing.Size(41, 20);
			this.tbSizeX.TabIndex = 7;
			this.tbSizeX.Text = "400";
			this.tbSizeX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberBox_KeyPress);
			// 
			// tbVertRes
			// 
			this.tbVertRes.Location = new System.Drawing.Point(115, 57);
			this.tbVertRes.Name = "tbVertRes";
			this.tbVertRes.Size = new System.Drawing.Size(39, 20);
			this.tbVertRes.TabIndex = 8;
			this.tbVertRes.Text = "500";
			this.tbVertRes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberBox_KeyPress);
			// 
			// tbHorRes
			// 
			this.tbHorRes.Location = new System.Drawing.Point(266, 54);
			this.tbHorRes.Name = "tbHorRes";
			this.tbHorRes.Size = new System.Drawing.Size(41, 20);
			this.tbHorRes.TabIndex = 9;
			this.tbHorRes.Text = "500";
			this.tbHorRes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberBox_KeyPress);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.tbHorRes);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tbVertRes);
			this.groupBox1.Controls.Add(this.tbSizeY);
			this.groupBox1.Controls.Add(this.tbSizeX);
			this.groupBox1.Location = new System.Drawing.Point(6, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(316, 88);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			// 
			// NewFingerViewForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(326, 120);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "NewFingerViewForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New finger view";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		internal System.Windows.Forms.TextBox tbSizeY;
		internal System.Windows.Forms.TextBox tbSizeX;
		internal System.Windows.Forms.TextBox tbVertRes;
		internal System.Windows.Forms.TextBox tbHorRes;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
