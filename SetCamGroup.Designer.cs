namespace iSpyApplication
{
    partial class SetCamGroup
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
            this.trv_Data = new System.Windows.Forms.TreeView();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.tbx_NewNode = new System.Windows.Forms.TextBox();
            this.lbl_NewNode = new System.Windows.Forms.Label();
            this.btn_RemoveNode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trv_Data
            // 
            this.trv_Data.Location = new System.Drawing.Point(12, 12);
            this.trv_Data.Name = "trv_Data";
            this.trv_Data.Size = new System.Drawing.Size(776, 288);
            this.trv_Data.TabIndex = 0;
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(349, 378);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(108, 42);
            this.btn_confirm.TabIndex = 1;
            this.btn_confirm.Text = "Add";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // tbx_NewNode
            // 
            this.tbx_NewNode.Location = new System.Drawing.Point(349, 322);
            this.tbx_NewNode.Name = "tbx_NewNode";
            this.tbx_NewNode.Size = new System.Drawing.Size(177, 22);
            this.tbx_NewNode.TabIndex = 2;
            // 
            // lbl_NewNode
            // 
            this.lbl_NewNode.AutoSize = true;
            this.lbl_NewNode.Location = new System.Drawing.Point(243, 322);
            this.lbl_NewNode.Name = "lbl_NewNode";
            this.lbl_NewNode.Size = new System.Drawing.Size(99, 16);
            this.lbl_NewNode.TabIndex = 3;
            this.lbl_NewNode.Text = "Add New Node";
            // 
            // btn_RemoveNode
            // 
            this.btn_RemoveNode.Location = new System.Drawing.Point(479, 378);
            this.btn_RemoveNode.Name = "btn_RemoveNode";
            this.btn_RemoveNode.Size = new System.Drawing.Size(108, 42);
            this.btn_RemoveNode.TabIndex = 4;
            this.btn_RemoveNode.Text = "Remove Node";
            this.btn_RemoveNode.UseVisualStyleBackColor = true;
            this.btn_RemoveNode.Click += new System.EventHandler(this.btn_RemoveNode_Click);
            // 
            // SetCamGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_RemoveNode);
            this.Controls.Add(this.lbl_NewNode);
            this.Controls.Add(this.tbx_NewNode);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.trv_Data);
            this.Name = "SetCamGroup";
            this.Text = "SetCamGroup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetCamGroup_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trv_Data;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.TextBox tbx_NewNode;
        private System.Windows.Forms.Label lbl_NewNode;
        private System.Windows.Forms.Button btn_RemoveNode;
    }
}