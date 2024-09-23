namespace iSpyApplication
{
    partial class AlertForm
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
            this.btn_close = new System.Windows.Forms.Button();
            this.dgv_CamAlertInfor = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CamAlertInfor)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_close.Location = new System.Drawing.Point(281, 381);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(130, 38);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // dgv_CamAlertInfor
            // 
            this.dgv_CamAlertInfor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgv_CamAlertInfor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_CamAlertInfor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CamAlertInfor.Location = new System.Drawing.Point(12, 28);
            this.dgv_CamAlertInfor.MultiSelect = false;
            this.dgv_CamAlertInfor.Name = "dgv_CamAlertInfor";
            this.dgv_CamAlertInfor.ReadOnly = true;
            this.dgv_CamAlertInfor.RowHeadersWidth = 51;
            this.dgv_CamAlertInfor.RowTemplate.Height = 24;
            this.dgv_CamAlertInfor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_CamAlertInfor.Size = new System.Drawing.Size(651, 347);
            this.dgv_CamAlertInfor.TabIndex = 2;
            this.dgv_CamAlertInfor.DoubleClick += new System.EventHandler(this.dgv_CamAlertInfor_DoubleClick);
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 431);
            this.Controls.Add(this.dgv_CamAlertInfor);
            this.Controls.Add(this.btn_close);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlertForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlertForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AlertForm_FormClosed);
            this.Load += new System.EventHandler(this.AlertForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CamAlertInfor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DataGridView dgv_CamAlertInfor;
    }
}