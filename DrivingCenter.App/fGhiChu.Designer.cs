namespace DrivingCenter.App
{
    partial class fGhiChu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fGhiChu));
            this.label1 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_AcceptGhiChu = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 11.25F);
            this.label1.Location = new System.Drawing.Point(51, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ghi chú (*)";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGhiChu.Font = new System.Drawing.Font("Roboto", 11.25F);
            this.txtGhiChu.Location = new System.Drawing.Point(6, 4);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(249, 19);
            this.txtGhiChu.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(60)))), ((int)(((byte)(94)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(474, 54);
            this.panel2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(134, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "GHI CHÚ PHIÊN HỌC";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtGhiChu);
            this.panel1.Location = new System.Drawing.Point(150, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 27);
            this.panel1.TabIndex = 13;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(60)))), ((int)(((byte)(94)))));
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("Roboto", 11.25F);
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Location = new System.Drawing.Point(295, 123);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(116, 31);
            this.btn_Cancel.TabIndex = 11;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_AcceptGhiChu
            // 
            this.btn_AcceptGhiChu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(60)))), ((int)(((byte)(94)))));
            this.btn_AcceptGhiChu.FlatAppearance.BorderSize = 0;
            this.btn_AcceptGhiChu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AcceptGhiChu.Font = new System.Drawing.Font("Roboto", 11.25F);
            this.btn_AcceptGhiChu.ForeColor = System.Drawing.Color.White;
            this.btn_AcceptGhiChu.Location = new System.Drawing.Point(150, 123);
            this.btn_AcceptGhiChu.Name = "btn_AcceptGhiChu";
            this.btn_AcceptGhiChu.Size = new System.Drawing.Size(116, 31);
            this.btn_AcceptGhiChu.TabIndex = 12;
            this.btn_AcceptGhiChu.Text = "OK";
            this.btn_AcceptGhiChu.UseVisualStyleBackColor = false;
            this.btn_AcceptGhiChu.Click += new System.EventHandler(this.btn_AcceptGhiChu_Click);
            // 
            // fGhiChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 185);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_AcceptGhiChu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("fGhiChu.IconOptions.SvgImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fGhiChu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Note phiên học";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_AcceptGhiChu;
    }
}