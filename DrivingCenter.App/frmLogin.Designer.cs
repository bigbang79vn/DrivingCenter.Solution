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
            this.pnl_Brand = new DevExpress.XtraEditors.PanelControl();
            this.lbl_Version = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Features = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Tagline = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Center = new DevExpress.XtraEditors.LabelControl();
            this.lbl_AppName = new DevExpress.XtraEditors.LabelControl();
            this.pic_Logo = new DevExpress.XtraEditors.PictureEdit();
            this.pnl_Form = new DevExpress.XtraEditors.PanelControl();
            this.lbl_Copyright = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.chkRemember = new DevExpress.XtraEditors.CheckEdit();
            this.lbl_CapsLock = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.ButtonEdit();
            this.lbl_Pass = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.lbl_User = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Subtitle = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Brand)).BeginInit();
            this.pnl_Brand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Form)).BeginInit();
            this.pnl_Form.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemember.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // pnl_Brand
            //
            this.pnl_Brand.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Brand.Controls.Add(this.lbl_Version);
            this.pnl_Brand.Controls.Add(this.lbl_Features);
            this.pnl_Brand.Controls.Add(this.lbl_Tagline);
            this.pnl_Brand.Controls.Add(this.lbl_Center);
            this.pnl_Brand.Controls.Add(this.lbl_AppName);
            this.pnl_Brand.Controls.Add(this.pic_Logo);
            this.pnl_Brand.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Brand.Location = new System.Drawing.Point(0, 0);
            this.pnl_Brand.Name = "pnl_Brand";
            this.pnl_Brand.Size = new System.Drawing.Size(340, 480);
            this.pnl_Brand.TabIndex = 1;
            this.pnl_Brand.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Brand_Paint);
            //
            // lbl_Version
            //
            this.lbl_Version.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F);
            this.lbl_Version.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lbl_Version.Appearance.Options.UseFont = true;
            this.lbl_Version.Appearance.Options.UseForeColor = true;
            this.lbl_Version.Location = new System.Drawing.Point(42, 444);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(60, 13);
            this.lbl_Version.TabIndex = 5;
            this.lbl_Version.Text = "Phiên bản —";
            //
            // lbl_Features
            //
            this.lbl_Features.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Features.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbl_Features.Appearance.Options.UseFont = true;
            this.lbl_Features.Appearance.Options.UseForeColor = true;
            this.lbl_Features.Appearance.Options.UseTextOptions = true;
            this.lbl_Features.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbl_Features.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Features.LineVisible = false;
            this.lbl_Features.Location = new System.Drawing.Point(42, 262);
            this.lbl_Features.Name = "lbl_Features";
            this.lbl_Features.Size = new System.Drawing.Size(270, 96);
            this.lbl_Features.TabIndex = 4;
            this.lbl_Features.Text = "✓   Giám sát DAT và phiên học trực tuyến\r\n\r\n✓   Quản lý khóa học, giáo viên, xe tập lái\r\n\r\n✓   Nhập hồ sơ và in chứng chỉ tốt nghiệp";
            //
            // lbl_Tagline
            //
            this.lbl_Tagline.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Tagline.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lbl_Tagline.Appearance.Options.UseFont = true;
            this.lbl_Tagline.Appearance.Options.UseForeColor = true;
            this.lbl_Tagline.Location = new System.Drawing.Point(42, 206);
            this.lbl_Tagline.Name = "lbl_Tagline";
            this.lbl_Tagline.Size = new System.Drawing.Size(220, 16);
            this.lbl_Tagline.TabIndex = 3;
            this.lbl_Tagline.Text = "Hệ thống quản lý đào tạo lái xe";
            //
            // lbl_Center
            //
            this.lbl_Center.Appearance.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Center.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbl_Center.Appearance.Options.UseFont = true;
            this.lbl_Center.Appearance.Options.UseForeColor = true;
            this.lbl_Center.Location = new System.Drawing.Point(42, 182);
            this.lbl_Center.Name = "lbl_Center";
            this.lbl_Center.Size = new System.Drawing.Size(220, 18);
            this.lbl_Center.TabIndex = 2;
            this.lbl_Center.Text = "TRUNG TÂM GDNN VIỆT NHẬT";
            //
            // lbl_AppName
            //
            this.lbl_AppName.Appearance.Font = new System.Drawing.Font("Roboto", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lbl_AppName.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbl_AppName.Appearance.Options.UseFont = true;
            this.lbl_AppName.Appearance.Options.UseForeColor = true;
            this.lbl_AppName.Location = new System.Drawing.Point(38, 118);
            this.lbl_AppName.Name = "lbl_AppName";
            this.lbl_AppName.Size = new System.Drawing.Size(84, 58);
            this.lbl_AppName.TabIndex = 1;
            this.lbl_AppName.Text = "FLY";
            //
            // pic_Logo
            //
            this.pic_Logo.Location = new System.Drawing.Point(42, 52);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Properties.AllowFocused = false;
            this.pic_Logo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.Properties.Appearance.Options.UseBackColor = true;
            this.pic_Logo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic_Logo.Properties.ReadOnly = true;
            this.pic_Logo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_Logo.Properties.ShowMenu = false;
            this.pic_Logo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_Logo.Size = new System.Drawing.Size(56, 56);
            this.pic_Logo.TabIndex = 0;
            this.pic_Logo.TabStop = false;
            //
            // pnl_Form
            //
            this.pnl_Form.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Form.Controls.Add(this.lbl_Copyright);
            this.pnl_Form.Controls.Add(this.btnCancel);
            this.pnl_Form.Controls.Add(this.btnLogin);
            this.pnl_Form.Controls.Add(this.chkRemember);
            this.pnl_Form.Controls.Add(this.lbl_CapsLock);
            this.pnl_Form.Controls.Add(this.txtPassword);
            this.pnl_Form.Controls.Add(this.lbl_Pass);
            this.pnl_Form.Controls.Add(this.txtUserName);
            this.pnl_Form.Controls.Add(this.lbl_User);
            this.pnl_Form.Controls.Add(this.lbl_Subtitle);
            this.pnl_Form.Controls.Add(this.lbl_Title);
            this.pnl_Form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Form.Location = new System.Drawing.Point(340, 0);
            this.pnl_Form.Name = "pnl_Form";
            this.pnl_Form.Size = new System.Drawing.Size(480, 480);
            this.pnl_Form.TabIndex = 0;
            //
            // lbl_Copyright
            //
            this.lbl_Copyright.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F);
            this.lbl_Copyright.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Copyright.Appearance.Options.UseFont = true;
            this.lbl_Copyright.Appearance.Options.UseForeColor = true;
            this.lbl_Copyright.Appearance.Options.UseTextOptions = true;
            this.lbl_Copyright.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_Copyright.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Copyright.Location = new System.Drawing.Point(60, 444);
            this.lbl_Copyright.Name = "lbl_Copyright";
            this.lbl_Copyright.Size = new System.Drawing.Size(360, 13);
            this.lbl_Copyright.TabIndex = 10;
            this.lbl_Copyright.Text = "© Trung tâm GDNN Việt Nhật";
            //
            // btnCancel
            //
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Roboto", 10.5F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(60, 382);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(360, 36);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // btnLogin
            //
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Location = new System.Drawing.Point(60, 330);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(360, 42);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            //
            // chkRemember
            //
            this.chkRemember.Location = new System.Drawing.Point(58, 294);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.chkRemember.Properties.Appearance.Options.UseFont = true;
            this.chkRemember.Properties.Caption = "Ghi nhớ tên đăng nhập";
            this.chkRemember.Size = new System.Drawing.Size(200, 20);
            this.chkRemember.TabIndex = 2;
            //
            // lbl_CapsLock
            //
            this.lbl_CapsLock.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_CapsLock.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lbl_CapsLock.Appearance.Options.UseFont = true;
            this.lbl_CapsLock.Appearance.Options.UseForeColor = true;
            this.lbl_CapsLock.Location = new System.Drawing.Point(300, 297);
            this.lbl_CapsLock.Name = "lbl_CapsLock";
            this.lbl_CapsLock.Size = new System.Drawing.Size(112, 15);
            this.lbl_CapsLock.TabIndex = 9;
            this.lbl_CapsLock.Text = "⚠ Caps Lock đang bật";
            this.lbl_CapsLock.Visible = false;
            //
            // txtPassword
            //
            this.txtPassword.EditValue = "admin";
            this.txtPassword.Location = new System.Drawing.Point(60, 244);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 11.25F);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.AutoHeight = false;
            this.txtPassword.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.txtPassword.Properties.ContextImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("txtPassword.Properties.ContextImageOptions.SvgImage")));
            this.txtPassword.Properties.ContextImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.txtPassword.Properties.NullValuePrompt = "Nhập mật khẩu";
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(360, 38);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtPassword_ButtonClick);
            this.txtPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Input_KeyUp);
            this.txtPassword.Enter += new System.EventHandler(this.Input_FocusChanged);
            this.txtPassword.Leave += new System.EventHandler(this.Input_FocusChanged);
            //
            // lbl_Pass
            //
            this.lbl_Pass.Appearance.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_Pass.Appearance.Options.UseFont = true;
            this.lbl_Pass.Location = new System.Drawing.Point(60, 224);
            this.lbl_Pass.Name = "lbl_Pass";
            this.lbl_Pass.Size = new System.Drawing.Size(52, 15);
            this.lbl_Pass.TabIndex = 8;
            this.lbl_Pass.Text = "Mật khẩu";
            //
            // txtUserName
            //
            this.txtUserName.EditValue = "admin";
            this.txtUserName.Location = new System.Drawing.Point(60, 172);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 11.25F);
            this.txtUserName.Properties.Appearance.Options.UseFont = true;
            this.txtUserName.Properties.AutoHeight = false;
            this.txtUserName.Properties.ContextImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("txtUserName.Properties.ContextImageOptions.SvgImage")));
            this.txtUserName.Properties.ContextImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.txtUserName.Properties.NullValuePrompt = "Nhập tên đăng nhập";
            this.txtUserName.Size = new System.Drawing.Size(360, 38);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Input_KeyUp);
            this.txtUserName.Enter += new System.EventHandler(this.Input_FocusChanged);
            this.txtUserName.Leave += new System.EventHandler(this.Input_FocusChanged);
            //
            // lbl_User
            //
            this.lbl_User.Appearance.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_User.Appearance.Options.UseFont = true;
            this.lbl_User.Location = new System.Drawing.Point(60, 152);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(84, 15);
            this.lbl_User.TabIndex = 7;
            this.lbl_User.Text = "Tên đăng nhập";
            //
            // lbl_Subtitle
            //
            this.lbl_Subtitle.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Subtitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Subtitle.Appearance.Options.UseFont = true;
            this.lbl_Subtitle.Appearance.Options.UseForeColor = true;
            this.lbl_Subtitle.Location = new System.Drawing.Point(62, 100);
            this.lbl_Subtitle.Name = "lbl_Subtitle";
            this.lbl_Subtitle.Size = new System.Drawing.Size(290, 16);
            this.lbl_Subtitle.TabIndex = 6;
            this.lbl_Subtitle.Text = "Chào mừng trở lại! Đăng nhập để tiếp tục làm việc.";
            //
            // lbl_Title
            //
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 21.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Appearance.Options.UseForeColor = true;
            this.lbl_Title.Location = new System.Drawing.Point(58, 56);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(150, 35);
            this.lbl_Title.TabIndex = 5;
            this.lbl_Title.Text = "Đăng nhập";
            //
            // frmLogin
            //
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(820, 480);
            this.Controls.Add(this.pnl_Form);
            this.Controls.Add(this.pnl_Brand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("frmLogin.IconOptions.SvgImage")));
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Trung tâm GDNN Việt Nhật";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Brand)).EndInit();
            this.pnl_Brand.ResumeLayout(false);
            this.pnl_Brand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Form)).EndInit();
            this.pnl_Form.ResumeLayout(false);
            this.pnl_Form.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemember.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Brand;
        private DevExpress.XtraEditors.PictureEdit pic_Logo;
        private DevExpress.XtraEditors.LabelControl lbl_AppName;
        private DevExpress.XtraEditors.LabelControl lbl_Center;
        private DevExpress.XtraEditors.LabelControl lbl_Tagline;
        private DevExpress.XtraEditors.LabelControl lbl_Features;
        private DevExpress.XtraEditors.LabelControl lbl_Version;
        private DevExpress.XtraEditors.PanelControl pnl_Form;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.LabelControl lbl_Subtitle;
        private DevExpress.XtraEditors.LabelControl lbl_User;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.LabelControl lbl_Pass;
        private DevExpress.XtraEditors.ButtonEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lbl_CapsLock;
        private DevExpress.XtraEditors.CheckEdit chkRemember;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lbl_Copyright;
    }
}
