namespace DrivingCenter.App
{
    partial class frmGhiChu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGhiChu));
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.lbl_MaPhien = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Subtitle = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            this.pic_Icon = new DevExpress.XtraEditors.PictureEdit();
            this.lbl_Reason = new DevExpress.XtraEditors.LabelControl();
            this.btn_Reason1 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Reason2 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Reason3 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Reason4 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Reason5 = new DevExpress.XtraEditors.SimpleButton();
            this.memo_GhiChu = new DevExpress.XtraEditors.MemoEdit();
            this.lbl_Error = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Counter = new DevExpress.XtraEditors.LabelControl();
            this.pnl_Footer = new DevExpress.XtraEditors.PanelControl();
            this.lbl_Shortcut = new DevExpress.XtraEditors.LabelControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_AcceptGhiChu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Icon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_GhiChu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Footer)).BeginInit();
            this.pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            //
            // pnl_Header
            //
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.lbl_MaPhien);
            this.pnl_Header.Controls.Add(this.lbl_Subtitle);
            this.pnl_Header.Controls.Add(this.lbl_Title);
            this.pnl_Header.Controls.Add(this.pic_Icon);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(520, 84);
            this.pnl_Header.TabIndex = 0;
            this.pnl_Header.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Header_Paint);
            //
            // lbl_MaPhien
            //
            this.lbl_MaPhien.Appearance.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_MaPhien.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_MaPhien.Appearance.Options.UseFont = true;
            this.lbl_MaPhien.Appearance.Options.UseForeColor = true;
            this.lbl_MaPhien.Location = new System.Drawing.Point(78, 58);
            this.lbl_MaPhien.Name = "lbl_MaPhien";
            this.lbl_MaPhien.Size = new System.Drawing.Size(60, 15);
            this.lbl_MaPhien.TabIndex = 3;
            this.lbl_MaPhien.Text = "Mã phiên: —";
            //
            // lbl_Subtitle
            //
            this.lbl_Subtitle.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_Subtitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Subtitle.Appearance.Options.UseFont = true;
            this.lbl_Subtitle.Appearance.Options.UseForeColor = true;
            this.lbl_Subtitle.Location = new System.Drawing.Point(78, 39);
            this.lbl_Subtitle.Name = "lbl_Subtitle";
            this.lbl_Subtitle.Size = new System.Drawing.Size(300, 15);
            this.lbl_Subtitle.TabIndex = 2;
            this.lbl_Subtitle.Text = "Phiên học bị khóa sẽ không được tính vào kết quả đào tạo.";
            //
            // lbl_Title
            //
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Location = new System.Drawing.Point(78, 12);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(140, 23);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Khóa phiên học";
            //
            // pic_Icon
            //
            this.pic_Icon.Location = new System.Drawing.Point(22, 18);
            this.pic_Icon.Name = "pic_Icon";
            this.pic_Icon.Properties.AllowFocused = false;
            this.pic_Icon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pic_Icon.Properties.Appearance.Options.UseBackColor = true;
            this.pic_Icon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic_Icon.Properties.ReadOnly = true;
            this.pic_Icon.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_Icon.Properties.ShowMenu = false;
            this.pic_Icon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_Icon.Size = new System.Drawing.Size(44, 44);
            this.pic_Icon.TabIndex = 0;
            this.pic_Icon.TabStop = false;
            //
            // lbl_Reason
            //
            this.lbl_Reason.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Reason.Appearance.Options.UseFont = true;
            this.lbl_Reason.Location = new System.Drawing.Point(22, 98);
            this.lbl_Reason.Name = "lbl_Reason";
            this.lbl_Reason.Size = new System.Drawing.Size(110, 16);
            this.lbl_Reason.TabIndex = 1;
            this.lbl_Reason.Text = "Lý do khóa phiên *";
            //
            // btn_Reason1
            //
            this.btn_Reason1.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btn_Reason1.Appearance.Options.UseFont = true;
            this.btn_Reason1.Location = new System.Drawing.Point(22, 122);
            this.btn_Reason1.Name = "btn_Reason1";
            this.btn_Reason1.Size = new System.Drawing.Size(150, 26);
            this.btn_Reason1.TabIndex = 2;
            this.btn_Reason1.TabStop = false;
            this.btn_Reason1.Text = "Không đúng khuôn mặt";
            this.btn_Reason1.Click += new System.EventHandler(this.btn_Reason_Click);
            //
            // btn_Reason2
            //
            this.btn_Reason2.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btn_Reason2.Appearance.Options.UseFont = true;
            this.btn_Reason2.Location = new System.Drawing.Point(178, 122);
            this.btn_Reason2.Name = "btn_Reason2";
            this.btn_Reason2.Size = new System.Drawing.Size(130, 26);
            this.btn_Reason2.TabIndex = 3;
            this.btn_Reason2.TabStop = false;
            this.btn_Reason2.Text = "Thiết bị / GPS lỗi";
            this.btn_Reason2.Click += new System.EventHandler(this.btn_Reason_Click);
            //
            // btn_Reason3
            //
            this.btn_Reason3.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btn_Reason3.Appearance.Options.UseFont = true;
            this.btn_Reason3.Location = new System.Drawing.Point(314, 122);
            this.btn_Reason3.Name = "btn_Reason3";
            this.btn_Reason3.Size = new System.Drawing.Size(184, 26);
            this.btn_Reason3.TabIndex = 4;
            this.btn_Reason3.TabStop = false;
            this.btn_Reason3.Text = "Quãng đường bất thường";
            this.btn_Reason3.Click += new System.EventHandler(this.btn_Reason_Click);
            //
            // btn_Reason4
            //
            this.btn_Reason4.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btn_Reason4.Appearance.Options.UseFont = true;
            this.btn_Reason4.Location = new System.Drawing.Point(22, 154);
            this.btn_Reason4.Name = "btn_Reason4";
            this.btn_Reason4.Size = new System.Drawing.Size(196, 26);
            this.btn_Reason4.TabIndex = 5;
            this.btn_Reason4.TabStop = false;
            this.btn_Reason4.Text = "Học viên nhờ người học hộ";
            this.btn_Reason4.Click += new System.EventHandler(this.btn_Reason_Click);
            //
            // btn_Reason5
            //
            this.btn_Reason5.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btn_Reason5.Appearance.Options.UseFont = true;
            this.btn_Reason5.Location = new System.Drawing.Point(224, 154);
            this.btn_Reason5.Name = "btn_Reason5";
            this.btn_Reason5.Size = new System.Drawing.Size(150, 26);
            this.btn_Reason5.TabIndex = 6;
            this.btn_Reason5.TabStop = false;
            this.btn_Reason5.Text = "Phiên học trùng lặp";
            this.btn_Reason5.Click += new System.EventHandler(this.btn_Reason_Click);
            //
            // memo_GhiChu
            //
            this.memo_GhiChu.Location = new System.Drawing.Point(22, 190);
            this.memo_GhiChu.Name = "memo_GhiChu";
            this.memo_GhiChu.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 10.5F);
            this.memo_GhiChu.Properties.Appearance.Options.UseFont = true;
            this.memo_GhiChu.Properties.MaxLength = 150;
            this.memo_GhiChu.Properties.NullValuePrompt = "Nhập lý do, hoặc bấm chọn nhanh ở trên rồi bổ sung chi tiết...";
            this.memo_GhiChu.Properties.NullValuePromptShowForEmptyValue = true;
            this.memo_GhiChu.Size = new System.Drawing.Size(476, 84);
            this.memo_GhiChu.TabIndex = 0;
            this.memo_GhiChu.EditValueChanged += new System.EventHandler(this.memo_GhiChu_EditValueChanged);
            this.memo_GhiChu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.memo_GhiChu_KeyDown);
            //
            // lbl_Error
            //
            this.lbl_Error.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_Error.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lbl_Error.Appearance.Options.UseFont = true;
            this.lbl_Error.Appearance.Options.UseForeColor = true;
            this.lbl_Error.Location = new System.Drawing.Point(22, 280);
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.Size = new System.Drawing.Size(200, 15);
            this.lbl_Error.TabIndex = 7;
            this.lbl_Error.Text = "Vui lòng nhập lý do khóa phiên.";
            this.lbl_Error.Visible = false;
            //
            // lbl_Counter
            //
            this.lbl_Counter.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_Counter.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Counter.Appearance.Options.UseFont = true;
            this.lbl_Counter.Appearance.Options.UseForeColor = true;
            this.lbl_Counter.Appearance.Options.UseTextOptions = true;
            this.lbl_Counter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_Counter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Counter.Location = new System.Drawing.Point(398, 280);
            this.lbl_Counter.Name = "lbl_Counter";
            this.lbl_Counter.Size = new System.Drawing.Size(100, 15);
            this.lbl_Counter.TabIndex = 8;
            this.lbl_Counter.Text = "0 / 150";
            //
            // pnl_Footer
            //
            this.pnl_Footer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Footer.Controls.Add(this.lbl_Shortcut);
            this.pnl_Footer.Controls.Add(this.btn_Cancel);
            this.pnl_Footer.Controls.Add(this.btn_AcceptGhiChu);
            this.pnl_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Footer.Location = new System.Drawing.Point(0, 308);
            this.pnl_Footer.Name = "pnl_Footer";
            this.pnl_Footer.Size = new System.Drawing.Size(520, 60);
            this.pnl_Footer.TabIndex = 9;
            this.pnl_Footer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Footer_Paint);
            //
            // lbl_Shortcut
            //
            this.lbl_Shortcut.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Italic);
            this.lbl_Shortcut.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Shortcut.Appearance.Options.UseFont = true;
            this.lbl_Shortcut.Appearance.Options.UseForeColor = true;
            this.lbl_Shortcut.Location = new System.Drawing.Point(22, 23);
            this.lbl_Shortcut.Name = "lbl_Shortcut";
            this.lbl_Shortcut.Size = new System.Drawing.Size(150, 13);
            this.lbl_Shortcut.TabIndex = 2;
            this.lbl_Shortcut.Text = "Ctrl + Enter để khóa · Esc để hủy";
            //
            // btn_Cancel
            //
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(270, 13);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 34);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Hủy";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            //
            // btn_AcceptGhiChu
            //
            this.btn_AcceptGhiChu.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_AcceptGhiChu.Appearance.Options.UseFont = true;
            this.btn_AcceptGhiChu.Location = new System.Drawing.Point(378, 13);
            this.btn_AcceptGhiChu.Name = "btn_AcceptGhiChu";
            this.btn_AcceptGhiChu.Size = new System.Drawing.Size(120, 34);
            this.btn_AcceptGhiChu.TabIndex = 0;
            this.btn_AcceptGhiChu.Text = "Khóa phiên";
            this.btn_AcceptGhiChu.Click += new System.EventHandler(this.btn_AcceptGhiChu_Click);
            //
            // fGhiChu
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(520, 368);
            this.Controls.Add(this.lbl_Counter);
            this.Controls.Add(this.lbl_Error);
            this.Controls.Add(this.memo_GhiChu);
            this.Controls.Add(this.btn_Reason5);
            this.Controls.Add(this.btn_Reason4);
            this.Controls.Add(this.btn_Reason3);
            this.Controls.Add(this.btn_Reason2);
            this.Controls.Add(this.btn_Reason1);
            this.Controls.Add(this.lbl_Reason);
            this.Controls.Add(this.pnl_Footer);
            this.Controls.Add(this.pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("fGhiChu.IconOptions.SvgImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fGhiChu";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Khóa phiên học";
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Icon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo_GhiChu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Footer)).EndInit();
            this.pnl_Footer.ResumeLayout(false);
            this.pnl_Footer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.PictureEdit pic_Icon;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.LabelControl lbl_Subtitle;
        private DevExpress.XtraEditors.LabelControl lbl_MaPhien;
        private DevExpress.XtraEditors.LabelControl lbl_Reason;
        private DevExpress.XtraEditors.SimpleButton btn_Reason1;
        private DevExpress.XtraEditors.SimpleButton btn_Reason2;
        private DevExpress.XtraEditors.SimpleButton btn_Reason3;
        private DevExpress.XtraEditors.SimpleButton btn_Reason4;
        private DevExpress.XtraEditors.SimpleButton btn_Reason5;
        private DevExpress.XtraEditors.MemoEdit memo_GhiChu;
        private DevExpress.XtraEditors.LabelControl lbl_Error;
        private DevExpress.XtraEditors.LabelControl lbl_Counter;
        private DevExpress.XtraEditors.PanelControl pnl_Footer;
        private DevExpress.XtraEditors.LabelControl lbl_Shortcut;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_AcceptGhiChu;
    }
}
