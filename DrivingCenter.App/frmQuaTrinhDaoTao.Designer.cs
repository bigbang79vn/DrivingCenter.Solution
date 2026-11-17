namespace DrivingCenter.App
{
    partial class frmQuaTrinhDaoTao
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
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.btn_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ChiTietPhien = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_Desc = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            this.pnl_Profile = new DevExpress.XtraEditors.PanelControl();
            this.lbl_GhiChu = new DevExpress.XtraEditors.LabelControl();
            this.val_GuiTC = new DevExpress.XtraEditors.LabelControl();
            this.cap_GuiTC = new DevExpress.XtraEditors.LabelControl();
            this.val_SoPhien = new DevExpress.XtraEditors.LabelControl();
            this.cap_SoPhien = new DevExpress.XtraEditors.LabelControl();
            this.val_HangDT = new DevExpress.XtraEditors.LabelControl();
            this.cap_HangDT = new DevExpress.XtraEditors.LabelControl();
            this.val_KhoaHoc = new DevExpress.XtraEditors.LabelControl();
            this.cap_KhoaHoc = new DevExpress.XtraEditors.LabelControl();
            this.val_NgaySinh = new DevExpress.XtraEditors.LabelControl();
            this.cap_NgaySinh = new DevExpress.XtraEditors.LabelControl();
            this.lb_MaDK = new DevExpress.XtraEditors.LabelControl();
            this.lb_HoTen = new DevExpress.XtraEditors.LabelControl();
            this.pic_Avatar = new DevExpress.XtraEditors.PictureEdit();
            this.pnl_Kpi = new DevExpress.XtraEditors.PanelControl();
            this.gridControl_KetQua = new DevExpress.XtraGrid.GridControl();
            this.grdView_KetQuaDaoTao = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Img_Login = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_Picture = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.Img_Logout = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaPhien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BienSoXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HangXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Login = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Logout = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Distance_Phien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Time_Phien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Result_Sent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnl_Body = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Profile)).BeginInit();
            this.pnl_Profile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Avatar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Kpi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_KetQua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdView_KetQuaDaoTao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Body)).BeginInit();
            this.pnl_Body.SuspendLayout();
            this.SuspendLayout();
            //
            // pnl_Header
            //
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.btn_Refresh);
            this.pnl_Header.Controls.Add(this.btn_Export);
            this.pnl_Header.Controls.Add(this.btn_ChiTietPhien);
            this.pnl_Header.Controls.Add(this.lbl_Desc);
            this.pnl_Header.Controls.Add(this.lbl_Title);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(12, 12);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1336, 62);
            this.pnl_Header.TabIndex = 0;
            //
            // btn_Refresh
            //
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Refresh.Appearance.Options.UseFont = true;
            this.btn_Refresh.Location = new System.Drawing.Point(894, 14);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(110, 34);
            this.btn_Refresh.TabIndex = 2;
            this.btn_Refresh.Text = "Làm mới";
            this.btn_Refresh.ToolTip = "Tải lại thông tin và các phiên học (F5)";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            //
            // btn_Export
            //
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Export.Appearance.Options.UseFont = true;
            this.btn_Export.Location = new System.Drawing.Point(1010, 14);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(120, 34);
            this.btn_Export.TabIndex = 3;
            this.btn_Export.Text = "Xuất Excel";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            //
            // btn_ChiTietPhien
            //
            this.btn_ChiTietPhien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ChiTietPhien.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_ChiTietPhien.Appearance.Options.UseFont = true;
            this.btn_ChiTietPhien.Location = new System.Drawing.Point(1136, 14);
            this.btn_ChiTietPhien.Name = "btn_ChiTietPhien";
            this.btn_ChiTietPhien.Size = new System.Drawing.Size(200, 34);
            this.btn_ChiTietPhien.TabIndex = 4;
            this.btn_ChiTietPhien.Text = "Xem chi tiết phiên";
            this.btn_ChiTietPhien.ToolTip = "Xem lộ trình, ảnh chụp của phiên đang chọn (bấm đúp / Enter)";
            this.btn_ChiTietPhien.Click += new System.EventHandler(this.btn_ChiTietPhien_Click);
            //
            // lbl_Desc
            //
            this.lbl_Desc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Desc.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Desc.Appearance.Options.UseFont = true;
            this.lbl_Desc.Appearance.Options.UseForeColor = true;
            this.lbl_Desc.Location = new System.Drawing.Point(16, 36);
            this.lbl_Desc.Name = "lbl_Desc";
            this.lbl_Desc.Size = new System.Drawing.Size(520, 16);
            this.lbl_Desc.TabIndex = 1;
            this.lbl_Desc.Text = "Các phiên học DAT hợp lệ (từ 5 phút trở lên) của học viên, mới nhất ở trên.";
            //
            // lbl_Title
            //
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Appearance.Options.UseForeColor = true;
            this.lbl_Title.Location = new System.Drawing.Point(16, 6);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(190, 25);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "Quá trình đào tạo";
            //
            // pnl_Profile
            //
            this.pnl_Profile.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Profile.Controls.Add(this.lbl_GhiChu);
            this.pnl_Profile.Controls.Add(this.val_GuiTC);
            this.pnl_Profile.Controls.Add(this.cap_GuiTC);
            this.pnl_Profile.Controls.Add(this.val_SoPhien);
            this.pnl_Profile.Controls.Add(this.cap_SoPhien);
            this.pnl_Profile.Controls.Add(this.val_HangDT);
            this.pnl_Profile.Controls.Add(this.cap_HangDT);
            this.pnl_Profile.Controls.Add(this.val_KhoaHoc);
            this.pnl_Profile.Controls.Add(this.cap_KhoaHoc);
            this.pnl_Profile.Controls.Add(this.val_NgaySinh);
            this.pnl_Profile.Controls.Add(this.cap_NgaySinh);
            this.pnl_Profile.Controls.Add(this.lb_MaDK);
            this.pnl_Profile.Controls.Add(this.lb_HoTen);
            this.pnl_Profile.Controls.Add(this.pic_Avatar);
            this.pnl_Profile.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Profile.Location = new System.Drawing.Point(12, 74);
            this.pnl_Profile.Name = "pnl_Profile";
            this.pnl_Profile.Size = new System.Drawing.Size(300, 674);
            this.pnl_Profile.TabIndex = 1;
            this.pnl_Profile.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Profile_Paint);
            //
            // lbl_GhiChu
            //
            this.lbl_GhiChu.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F);
            this.lbl_GhiChu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_GhiChu.Appearance.Options.UseFont = true;
            this.lbl_GhiChu.Appearance.Options.UseForeColor = true;
            this.lbl_GhiChu.Appearance.Options.UseTextOptions = true;
            this.lbl_GhiChu.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbl_GhiChu.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lbl_GhiChu.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_GhiChu.Location = new System.Drawing.Point(20, 532);
            this.lbl_GhiChu.Name = "lbl_GhiChu";
            this.lbl_GhiChu.Size = new System.Drawing.Size(262, 96);
            this.lbl_GhiChu.TabIndex = 13;
            this.lbl_GhiChu.Text = "• Chỉ tính phiên từ 5 phút trở lên.\r\n• Ban đêm: phiên đăng nhập từ 18:00.\r\n• Số tự động: phiên chạy trên xe hạng B.01.";
            //
            // val_GuiTC
            //
            this.val_GuiTC.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.val_GuiTC.Appearance.Options.UseFont = true;
            this.val_GuiTC.Location = new System.Drawing.Point(130, 470);
            this.val_GuiTC.Name = "val_GuiTC";
            this.val_GuiTC.Size = new System.Drawing.Size(8, 16);
            this.val_GuiTC.TabIndex = 12;
            this.val_GuiTC.Text = "—";
            //
            // cap_GuiTC
            //
            this.cap_GuiTC.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.cap_GuiTC.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.cap_GuiTC.Appearance.Options.UseFont = true;
            this.cap_GuiTC.Appearance.Options.UseForeColor = true;
            this.cap_GuiTC.Location = new System.Drawing.Point(20, 470);
            this.cap_GuiTC.Name = "cap_GuiTC";
            this.cap_GuiTC.Size = new System.Drawing.Size(90, 16);
            this.cap_GuiTC.TabIndex = 11;
            this.cap_GuiTC.Text = "Đã gửi Tổng cục";
            //
            // val_SoPhien
            //
            this.val_SoPhien.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.val_SoPhien.Appearance.Options.UseFont = true;
            this.val_SoPhien.Location = new System.Drawing.Point(130, 442);
            this.val_SoPhien.Name = "val_SoPhien";
            this.val_SoPhien.Size = new System.Drawing.Size(8, 16);
            this.val_SoPhien.TabIndex = 10;
            this.val_SoPhien.Text = "—";
            //
            // cap_SoPhien
            //
            this.cap_SoPhien.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.cap_SoPhien.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.cap_SoPhien.Appearance.Options.UseFont = true;
            this.cap_SoPhien.Appearance.Options.UseForeColor = true;
            this.cap_SoPhien.Location = new System.Drawing.Point(20, 442);
            this.cap_SoPhien.Name = "cap_SoPhien";
            this.cap_SoPhien.Size = new System.Drawing.Size(48, 16);
            this.cap_SoPhien.TabIndex = 9;
            this.cap_SoPhien.Text = "Số phiên";
            //
            // val_HangDT
            //
            this.val_HangDT.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.val_HangDT.Appearance.Options.UseFont = true;
            this.val_HangDT.Location = new System.Drawing.Point(130, 414);
            this.val_HangDT.Name = "val_HangDT";
            this.val_HangDT.Size = new System.Drawing.Size(8, 16);
            this.val_HangDT.TabIndex = 8;
            this.val_HangDT.Text = "—";
            //
            // cap_HangDT
            //
            this.cap_HangDT.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.cap_HangDT.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.cap_HangDT.Appearance.Options.UseFont = true;
            this.cap_HangDT.Appearance.Options.UseForeColor = true;
            this.cap_HangDT.Location = new System.Drawing.Point(20, 414);
            this.cap_HangDT.Name = "cap_HangDT";
            this.cap_HangDT.Size = new System.Drawing.Size(80, 16);
            this.cap_HangDT.TabIndex = 7;
            this.cap_HangDT.Text = "Hạng đào tạo";
            //
            // val_KhoaHoc
            //
            this.val_KhoaHoc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.val_KhoaHoc.Appearance.Options.UseFont = true;
            this.val_KhoaHoc.Location = new System.Drawing.Point(130, 386);
            this.val_KhoaHoc.Name = "val_KhoaHoc";
            this.val_KhoaHoc.Size = new System.Drawing.Size(8, 16);
            this.val_KhoaHoc.TabIndex = 6;
            this.val_KhoaHoc.Text = "—";
            //
            // cap_KhoaHoc
            //
            this.cap_KhoaHoc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.cap_KhoaHoc.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.cap_KhoaHoc.Appearance.Options.UseFont = true;
            this.cap_KhoaHoc.Appearance.Options.UseForeColor = true;
            this.cap_KhoaHoc.Location = new System.Drawing.Point(20, 386);
            this.cap_KhoaHoc.Name = "cap_KhoaHoc";
            this.cap_KhoaHoc.Size = new System.Drawing.Size(52, 16);
            this.cap_KhoaHoc.TabIndex = 5;
            this.cap_KhoaHoc.Text = "Khóa học";
            //
            // val_NgaySinh
            //
            this.val_NgaySinh.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.val_NgaySinh.Appearance.Options.UseFont = true;
            this.val_NgaySinh.Location = new System.Drawing.Point(130, 358);
            this.val_NgaySinh.Name = "val_NgaySinh";
            this.val_NgaySinh.Size = new System.Drawing.Size(8, 16);
            this.val_NgaySinh.TabIndex = 4;
            this.val_NgaySinh.Text = "—";
            //
            // cap_NgaySinh
            //
            this.cap_NgaySinh.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.cap_NgaySinh.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.cap_NgaySinh.Appearance.Options.UseFont = true;
            this.cap_NgaySinh.Appearance.Options.UseForeColor = true;
            this.cap_NgaySinh.Location = new System.Drawing.Point(20, 358);
            this.cap_NgaySinh.Name = "cap_NgaySinh";
            this.cap_NgaySinh.Size = new System.Drawing.Size(56, 16);
            this.cap_NgaySinh.TabIndex = 3;
            this.cap_NgaySinh.Text = "Ngày sinh";
            //
            // lb_MaDK
            //
            this.lb_MaDK.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lb_MaDK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lb_MaDK.Appearance.Options.UseFont = true;
            this.lb_MaDK.Appearance.Options.UseForeColor = true;
            this.lb_MaDK.Appearance.Options.UseTextOptions = true;
            this.lb_MaDK.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lb_MaDK.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_MaDK.Location = new System.Drawing.Point(10, 276);
            this.lb_MaDK.Name = "lb_MaDK";
            this.lb_MaDK.Size = new System.Drawing.Size(280, 18);
            this.lb_MaDK.TabIndex = 2;
            this.lb_MaDK.Text = "Mã ĐK: —";
            //
            // lb_HoTen
            //
            this.lb_HoTen.Appearance.Font = new System.Drawing.Font("Roboto", 13F, System.Drawing.FontStyle.Bold);
            this.lb_HoTen.Appearance.Options.UseFont = true;
            this.lb_HoTen.Appearance.Options.UseTextOptions = true;
            this.lb_HoTen.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lb_HoTen.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.lb_HoTen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_HoTen.Location = new System.Drawing.Point(10, 248);
            this.lb_HoTen.Name = "lb_HoTen";
            this.lb_HoTen.Size = new System.Drawing.Size(280, 26);
            this.lb_HoTen.TabIndex = 1;
            this.lb_HoTen.Text = "Đang tải...";
            //
            // pic_Avatar
            //
            this.pic_Avatar.Location = new System.Drawing.Point(75, 28);
            this.pic_Avatar.Name = "pic_Avatar";
            this.pic_Avatar.Properties.NullText = "Chưa có ảnh";
            this.pic_Avatar.Properties.ReadOnly = true;
            this.pic_Avatar.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_Avatar.Properties.ShowMenu = false;
            this.pic_Avatar.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_Avatar.Size = new System.Drawing.Size(150, 200);
            this.pic_Avatar.TabIndex = 0;
            //
            // pnl_Kpi
            //
            this.pnl_Kpi.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Kpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Kpi.Location = new System.Drawing.Point(16, 10);
            this.pnl_Kpi.Name = "pnl_Kpi";
            this.pnl_Kpi.Size = new System.Drawing.Size(1008, 90);
            this.pnl_Kpi.TabIndex = 0;
            this.pnl_Kpi.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Kpi_Paint);
            this.pnl_Kpi.Resize += new System.EventHandler(this.pnl_Kpi_Resize);
            //
            // gridControl_KetQua
            //
            this.gridControl_KetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_KetQua.Location = new System.Drawing.Point(16, 100);
            this.gridControl_KetQua.MainView = this.grdView_KetQuaDaoTao;
            this.gridControl_KetQua.Name = "gridControl_KetQua";
            this.gridControl_KetQua.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_Picture});
            this.gridControl_KetQua.Size = new System.Drawing.Size(1008, 574);
            this.gridControl_KetQua.TabIndex = 1;
            this.gridControl_KetQua.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdView_KetQuaDaoTao});
            //
            // grdView_KetQuaDaoTao
            //
            this.grdView_KetQuaDaoTao.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdView_KetQuaDaoTao.Appearance.GroupRow.Options.UseFont = true;
            this.grdView_KetQuaDaoTao.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdView_KetQuaDaoTao.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdView_KetQuaDaoTao.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdView_KetQuaDaoTao.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdView_KetQuaDaoTao.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.grdView_KetQuaDaoTao.Appearance.Row.Options.UseFont = true;
            this.grdView_KetQuaDaoTao.Appearance.Row.Options.UseTextOptions = true;
            this.grdView_KetQuaDaoTao.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdView_KetQuaDaoTao.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Img_Login,
            this.Img_Logout,
            this.MaPhien,
            this.BienSoXe,
            this.HangXe,
            this.Login,
            this.Logout,
            this.Distance_Phien,
            this.Time_Phien,
            this.Result_Sent});
            this.grdView_KetQuaDaoTao.GridControl = this.gridControl_KetQua;
            this.grdView_KetQuaDaoTao.GroupCount = 1;
            this.grdView_KetQuaDaoTao.GroupFormat = "Xe hạng {1}  {2}";
            this.grdView_KetQuaDaoTao.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "HangXe", null, "·  {0} phiên"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TongQuangDuongLD_Phien", null, "{0:n1} km")});
            this.grdView_KetQuaDaoTao.Name = "grdView_KetQuaDaoTao";
            this.grdView_KetQuaDaoTao.OptionsBehavior.AutoExpandAllGroups = true;
            this.grdView_KetQuaDaoTao.OptionsBehavior.Editable = false;
            this.grdView_KetQuaDaoTao.OptionsBehavior.ReadOnly = true;
            this.grdView_KetQuaDaoTao.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.HangXe, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.Login, DevExpress.Data.ColumnSortOrder.Descending)});
            this.grdView_KetQuaDaoTao.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdView_KetQuaDaoTao_RowCellStyle);
            this.grdView_KetQuaDaoTao.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.grdView_KetQuaDaoTao_CustomDrawEmptyForeground);
            this.grdView_KetQuaDaoTao.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grdView_KetQuaDaoTao_FocusedRowChanged);
            this.grdView_KetQuaDaoTao.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grdView_KetQuaDaoTao_CustomColumnDisplayText);
            this.grdView_KetQuaDaoTao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdView_KetQuaDaoTao_KeyDown);
            this.grdView_KetQuaDaoTao.DoubleClick += new System.EventHandler(this.grdView_KetQuaDaoTao_DoubleClick);
            //
            // Img_Login
            //
            this.Img_Login.Caption = "Ảnh đăng nhập";
            this.Img_Login.ColumnEdit = this.repo_Picture;
            this.Img_Login.FieldName = "AnhChup";
            this.Img_Login.Name = "Img_Login";
            this.Img_Login.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.Img_Login.OptionsFilter.AllowFilter = false;
            this.Img_Login.Visible = true;
            this.Img_Login.VisibleIndex = 0;
            this.Img_Login.Width = 120;
            //
            // repo_Picture
            //
            this.repo_Picture.Name = "repo_Picture";
            this.repo_Picture.NullText = "Không có ảnh";
            this.repo_Picture.ShowMenu = false;
            this.repo_Picture.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //
            // Img_Logout
            //
            this.Img_Logout.Caption = "Ảnh đăng xuất";
            this.Img_Logout.ColumnEdit = this.repo_Picture;
            this.Img_Logout.FieldName = "AnhChup_Logout";
            this.Img_Logout.Name = "Img_Logout";
            this.Img_Logout.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.Img_Logout.OptionsFilter.AllowFilter = false;
            this.Img_Logout.Visible = true;
            this.Img_Logout.VisibleIndex = 1;
            this.Img_Logout.Width = 120;
            //
            // MaPhien
            //
            this.MaPhien.Caption = "Mã phiên";
            this.MaPhien.FieldName = "DiemDanhNguoiLxID";
            this.MaPhien.Name = "MaPhien";
            this.MaPhien.Width = 260;
            //
            // BienSoXe
            //
            this.BienSoXe.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.BienSoXe.AppearanceCell.Options.UseFont = true;
            this.BienSoXe.Caption = "Biển số";
            this.BienSoXe.FieldName = "BienSo";
            this.BienSoXe.Name = "BienSoXe";
            this.BienSoXe.Visible = true;
            this.BienSoXe.VisibleIndex = 2;
            this.BienSoXe.Width = 100;
            //
            // HangXe
            //
            this.HangXe.Caption = "Hạng xe";
            this.HangXe.FieldName = "HangXe";
            this.HangXe.Name = "HangXe";
            this.HangXe.Visible = true;
            this.HangXe.VisibleIndex = 0;
            this.HangXe.Width = 80;
            //
            // Login
            //
            this.Login.Caption = "Đăng nhập";
            this.Login.DisplayFormat.FormatString = "dd/MM/yyyy  HH:mm";
            this.Login.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Login.FieldName = "ThoiDiem";
            this.Login.Name = "Login";
            this.Login.Visible = true;
            this.Login.VisibleIndex = 3;
            this.Login.Width = 140;
            //
            // Logout
            //
            this.Logout.Caption = "Đăng xuất";
            this.Logout.DisplayFormat.FormatString = "dd/MM/yyyy  HH:mm";
            this.Logout.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Logout.FieldName = "ThoiDiem_Logout";
            this.Logout.Name = "Logout";
            this.Logout.Visible = true;
            this.Logout.VisibleIndex = 4;
            this.Logout.Width = 140;
            //
            // Distance_Phien
            //
            this.Distance_Phien.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.Distance_Phien.AppearanceCell.Options.UseFont = true;
            this.Distance_Phien.Caption = "Quãng đường (km)";
            this.Distance_Phien.DisplayFormat.FormatString = "n2";
            this.Distance_Phien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Distance_Phien.FieldName = "TongQuangDuongLD_Phien";
            this.Distance_Phien.Name = "Distance_Phien";
            this.Distance_Phien.Visible = true;
            this.Distance_Phien.VisibleIndex = 5;
            this.Distance_Phien.Width = 120;
            //
            // Time_Phien
            //
            this.Time_Phien.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.Time_Phien.AppearanceCell.Options.UseFont = true;
            this.Time_Phien.Caption = "Thời gian";
            this.Time_Phien.FieldName = "Time";
            this.Time_Phien.Name = "Time_Phien";
            this.Time_Phien.Visible = true;
            this.Time_Phien.VisibleIndex = 6;
            this.Time_Phien.Width = 100;
            //
            // Result_Sent
            //
            this.Result_Sent.Caption = "Gửi Tổng cục";
            this.Result_Sent.FieldName = "SendTongCuc";
            this.Result_Sent.Name = "Result_Sent";
            this.Result_Sent.Visible = true;
            this.Result_Sent.VisibleIndex = 7;
            this.Result_Sent.Width = 110;
            //
            // pnl_Body
            //
            this.pnl_Body.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Body.Controls.Add(this.gridControl_KetQua);
            this.pnl_Body.Controls.Add(this.pnl_Kpi);
            this.pnl_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Body.Location = new System.Drawing.Point(312, 74);
            this.pnl_Body.Name = "pnl_Body";
            this.pnl_Body.Padding = new System.Windows.Forms.Padding(16, 10, 0, 0);
            this.pnl_Body.Size = new System.Drawing.Size(1024, 674);
            this.pnl_Body.TabIndex = 2;
            //
            // frmQuaTrinhDaoTao
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 760);
            this.Controls.Add(this.pnl_Body);
            this.Controls.Add(this.pnl_Profile);
            this.Controls.Add(this.pnl_Header);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1100, 640);
            this.Name = "frmQuaTrinhDaoTao";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quá trình đào tạo học viên";
            this.Load += new System.EventHandler(this.frmQuaTrinhDaoTao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmQuaTrinhDaoTao_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Profile)).EndInit();
            this.pnl_Profile.ResumeLayout(false);
            this.pnl_Profile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Avatar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Kpi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_KetQua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdView_KetQuaDaoTao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Body)).EndInit();
            this.pnl_Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.SimpleButton btn_Refresh;
        private DevExpress.XtraEditors.SimpleButton btn_Export;
        private DevExpress.XtraEditors.SimpleButton btn_ChiTietPhien;
        private DevExpress.XtraEditors.LabelControl lbl_Desc;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.PanelControl pnl_Profile;
        private DevExpress.XtraEditors.PictureEdit pic_Avatar;
        private DevExpress.XtraEditors.LabelControl lb_HoTen;
        private DevExpress.XtraEditors.LabelControl lb_MaDK;
        private DevExpress.XtraEditors.LabelControl cap_NgaySinh;
        private DevExpress.XtraEditors.LabelControl val_NgaySinh;
        private DevExpress.XtraEditors.LabelControl cap_KhoaHoc;
        private DevExpress.XtraEditors.LabelControl val_KhoaHoc;
        private DevExpress.XtraEditors.LabelControl cap_HangDT;
        private DevExpress.XtraEditors.LabelControl val_HangDT;
        private DevExpress.XtraEditors.LabelControl cap_SoPhien;
        private DevExpress.XtraEditors.LabelControl val_SoPhien;
        private DevExpress.XtraEditors.LabelControl cap_GuiTC;
        private DevExpress.XtraEditors.LabelControl val_GuiTC;
        private DevExpress.XtraEditors.LabelControl lbl_GhiChu;
        private DevExpress.XtraEditors.PanelControl pnl_Kpi;
        private DevExpress.XtraEditors.PanelControl pnl_Body;
        private DevExpress.XtraGrid.GridControl gridControl_KetQua;
        private DevExpress.XtraGrid.Views.Grid.GridView grdView_KetQuaDaoTao;
        private DevExpress.XtraGrid.Columns.GridColumn Img_Login;
        private DevExpress.XtraGrid.Columns.GridColumn Img_Logout;
        private DevExpress.XtraGrid.Columns.GridColumn MaPhien;
        private DevExpress.XtraGrid.Columns.GridColumn BienSoXe;
        private DevExpress.XtraGrid.Columns.GridColumn HangXe;
        private DevExpress.XtraGrid.Columns.GridColumn Login;
        private DevExpress.XtraGrid.Columns.GridColumn Logout;
        private DevExpress.XtraGrid.Columns.GridColumn Distance_Phien;
        private DevExpress.XtraGrid.Columns.GridColumn Time_Phien;
        private DevExpress.XtraGrid.Columns.GridColumn Result_Sent;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repo_Picture;
    }
}
