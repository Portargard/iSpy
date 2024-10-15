namespace iSpyApplication
{
    partial class SetTimeForm
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
            this.lbl_TimeDelay = new System.Windows.Forms.Label();
            this.nud_TimeDelay = new System.Windows.Forms.NumericUpDown();
            this.btn_Confirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_TimeDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_TimeDelay
            // 
            this.lbl_TimeDelay.AutoSize = true;
            this.lbl_TimeDelay.Location = new System.Drawing.Point(12, 42);
            this.lbl_TimeDelay.Name = "lbl_TimeDelay";
            this.lbl_TimeDelay.Size = new System.Drawing.Size(132, 16);
            this.lbl_TimeDelay.TabIndex = 0;
            this.lbl_TimeDelay.Text = "Time Delay(Second)";
            // 
            // nud_TimeDelay
            // 
            this.nud_TimeDelay.Location = new System.Drawing.Point(160, 40);
            this.nud_TimeDelay.Name = "nud_TimeDelay";
            this.nud_TimeDelay.Size = new System.Drawing.Size(111, 22);
            this.nud_TimeDelay.TabIndex = 1;
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Location = new System.Drawing.Point(91, 93);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(112, 31);
            this.btn_Confirm.TabIndex = 2;
            this.btn_Confirm.Text = "Confirm";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new System.EventHandler(this.btn_Confirm_Click);
            // 
            // SetTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 155);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.nud_TimeDelay);
            this.Controls.Add(this.lbl_TimeDelay);
            this.Name = "SetTimeForm";
            this.Text = "SetTimeForm";
            ((System.ComponentModel.ISupportInitialize)(this.nud_TimeDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_TimeDelay;
        private System.Windows.Forms.NumericUpDown nud_TimeDelay;
        private System.Windows.Forms.Button btn_Confirm;
    }
}