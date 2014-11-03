namespace WiFoUI.UI.Dialogs
{
	partial class NumberDialog
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
			this.lblError = new System.Windows.Forms.Label();
			this.lblUnit = new System.Windows.Forms.Label();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblError
			// 
			this.lblError.AutoSize = true;
			this.lblError.ForeColor = System.Drawing.Color.Red;
			this.lblError.Location = new System.Drawing.Point(12, 32);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(11, 13);
			this.lblError.TabIndex = 9;
			this.lblError.Text = "*";
			this.lblError.Visible = false;
			// 
			// lblUnit
			// 
			this.lblUnit.AutoSize = true;
			this.lblUnit.Location = new System.Drawing.Point(128, 32);
			this.lblUnit.Name = "lblUnit";
			this.lblUnit.Size = new System.Drawing.Size(12, 13);
			this.lblUnit.TabIndex = 8;
			this.lblUnit.Text = "s";
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(31, 29);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(91, 20);
			this.txtValue.TabIndex = 7;
			this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(179, 41);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(179, 12);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// NumberDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(266, 77);
			this.Controls.Add(this.lblError);
			this.Controls.Add(this.lblUnit);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NumberDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "NumberForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.Label lblUnit;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;

	}
}