namespace iSpyApplication
{
    partial class FormSearch
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
            this.dgv_Videos = new System.Windows.Forms.DataGridView();
            this.btnPlayVideo = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUnmark = new System.Windows.Forms.Button();
            this.btnMark = new System.Windows.Forms.Button();
            this.btnDeleteVideo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Videos)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_Videos
            // 
            this.dgv_Videos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Videos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Videos.Location = new System.Drawing.Point(0, 0);
            this.dgv_Videos.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.dgv_Videos.Name = "dgv_Videos";
            this.dgv_Videos.RowHeadersWidth = 51;
            this.dgv_Videos.RowTemplate.Height = 24;
            this.dgv_Videos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Videos.Size = new System.Drawing.Size(928, 545);
            this.dgv_Videos.TabIndex = 1;
            this.dgv_Videos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Videos_CellClick);
            this.dgv_Videos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Videos_CellDoubleClick_1);
            // 
            // btnPlayVideo
            // 
            this.btnPlayVideo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPlayVideo.ForeColor = System.Drawing.Color.Green;
            this.btnPlayVideo.Location = new System.Drawing.Point(3, 22);
            this.btnPlayVideo.Name = "btnPlayVideo";
            this.btnPlayVideo.Size = new System.Drawing.Size(103, 42);
            this.btnPlayVideo.TabIndex = 2;
            this.btnPlayVideo.Text = "Phát video";
            this.btnPlayVideo.UseVisualStyleBackColor = false;
            this.btnPlayVideo.Click += new System.EventHandler(this.btnPlayVideo_Click_1);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(801, 22);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(103, 42);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUnmark);
            this.panel1.Controls.Add(this.btnMark);
            this.panel1.Controls.Add(this.btnDeleteVideo);
            this.panel1.Controls.Add(this.btnPlayVideo);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Location = new System.Drawing.Point(0, 469);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(928, 76);
            this.panel1.TabIndex = 4;
            // 
            // btnUnmark
            // 
            this.btnUnmark.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnUnmark.ForeColor = System.Drawing.Color.Navy;
            this.btnUnmark.Location = new System.Drawing.Point(405, 22);
            this.btnUnmark.Name = "btnUnmark";
            this.btnUnmark.Size = new System.Drawing.Size(103, 42);
            this.btnUnmark.TabIndex = 6;
            this.btnUnmark.Text = "Bỏ đánh dấu";
            this.btnUnmark.UseVisualStyleBackColor = false;
            this.btnUnmark.Click += new System.EventHandler(this.btnUnmark_Click);
            // 
            // btnMark
            // 
            this.btnMark.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnMark.ForeColor = System.Drawing.Color.Navy;
            this.btnMark.Location = new System.Drawing.Point(280, 22);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(103, 42);
            this.btnMark.TabIndex = 5;
            this.btnMark.Text = "Đánh dấu";
            this.btnMark.UseVisualStyleBackColor = false;
            this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
            // 
            // btnDeleteVideo
            // 
            this.btnDeleteVideo.BackColor = System.Drawing.Color.Red;
            this.btnDeleteVideo.ForeColor = System.Drawing.Color.Honeydew;
            this.btnDeleteVideo.Location = new System.Drawing.Point(146, 22);
            this.btnDeleteVideo.Name = "btnDeleteVideo";
            this.btnDeleteVideo.Size = new System.Drawing.Size(103, 42);
            this.btnDeleteVideo.TabIndex = 4;
            this.btnDeleteVideo.Text = "Xóa";
            this.btnDeleteVideo.UseVisualStyleBackColor = false;
            this.btnDeleteVideo.Click += new System.EventHandler(this.btnDeleteVideo_Click);
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 545);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_Videos);
            this.Name = "FormSearch";
            this.Text = "FormSearch";
            this.Load += new System.EventHandler(this.FormSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Videos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Videos;
        private System.Windows.Forms.Button btnPlayVideo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteVideo;
        private System.Windows.Forms.Button btnMark;
        private System.Windows.Forms.Button btnUnmark;
    }
}