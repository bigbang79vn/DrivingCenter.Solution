namespace DrivingCenter.App
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.btnHide = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 268);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SVN-Avo", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(68, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 91);
            this.label2.TabIndex = 4;
            this.label2.Text = "FLY";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 278F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 417F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 268);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnLogin);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(278, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(417, 268);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.label1.Location = new System.Drawing.Point(162, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Login";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(218, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(36, 195);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(160, 32);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.btnShow);
            this.panel4.Controls.Add(this.btnHide);
            this.panel4.Controls.Add(this.txtPassword);
            this.panel4.Location = new System.Drawing.Point(36, 132);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(342, 32);
            this.panel4.TabIndex = 0;
            // 
            // btnShow
            // 
            this.btnShow.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnShow.Appearance.Options.UseBorderColor = true;
            this.btnShow.AppearanceDisabled.BorderColor = System.Drawing.Color.White;
            this.btnShow.AppearanceDisabled.Options.UseBorderColor = true;
            this.btnShow.AppearancePressed.BorderColor = System.Drawing.Color.White;
            this.btnShow.AppearancePressed.Options.UseBorderColor = true;
            this.btnShow.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnShow.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnShow.ImageOptions.SvgImage")));
            this.btnShow.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.btnShow.Location = new System.Drawing.Point(310, 0);
            this.btnShow.Name = "btnShow";
            this.btnShow.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnShow.Size = new System.Drawing.Size(32, 32);
            this.btnShow.TabIndex = 4;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnHide
            // 
            this.btnHide.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnHide.Appearance.Options.UseBorderColor = true;
            this.btnHide.AppearanceDisabled.BorderColor = System.Drawing.Color.White;
            this.btnHide.AppearanceDisabled.Options.UseBorderColor = true;
            this.btnHide.AppearancePressed.BorderColor = System.Drawing.Color.White;
            this.btnHide.AppearancePressed.Options.UseBorderColor = true;
            this.btnHide.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnHide.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnHide.ImageOptions.SvgImage")));
            this.btnHide.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.btnHide.Location = new System.Drawing.Point(310, 0);
            this.btnHide.Name = "btnHide";
            this.btnHide.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnHide.Size = new System.Drawing.Size(32, 32);
            this.btnHide.TabIndex = 1;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Roboto Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(13, 7);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(288, 19);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.Text = "admin";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.txtUserName);
            this.panel3.Location = new System.Drawing.Point(36, 89);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(342, 32);
            this.panel3.TabIndex = 0;
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Font = new System.Drawing.Font("Roboto Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(13, 7);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(288, 19);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Text = "admin";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 268);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("frmLogin.IconOptions.SvgImage")));
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtUserName;
        private DevExpress.XtraEditors.SimpleButton btnHide;
        private DevExpress.XtraEditors.SimpleButton btnShow;
    }
}