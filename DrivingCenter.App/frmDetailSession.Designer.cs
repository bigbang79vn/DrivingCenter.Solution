namespace DrivingCenter.App
{
    partial class frmDetailSession
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetailSession));
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.lb_TrangThai = new DevExpress.XtraEditors.LabelControl();
            this.btn_CopyMa = new DevExpress.XtraEditors.SimpleButton();
            this.txt_MaPhien = new DevExpress.XtraEditors.TextEdit();
            this.lb_Sub = new DevExpress.XtraEditors.LabelControl();
            this.lb_HocVien = new DevExpress.XtraEditors.LabelControl();
            this.pnl_Kpi = new DevExpress.XtraEditors.PanelControl();
            this.split_Main = new DevExpress.XtraEditors.SplitContainerControl();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tab_XacThuc = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl_HistoryData = new DevExpress.XtraGrid.GridControl();
            this.Menu_DetailSession = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.update_ImageLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.update_ImageLoout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mn_InsertHinhDem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView_HistoryData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colH_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colH_ThoiDiem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colH_VanToc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colH_QuangDuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colH_ThoiGian = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colH_XacThuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_TextH = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colH_Anh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_Anh = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.tab_HanhTrinh = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl_VehicleHistory = new DevExpress.XtraGrid.GridControl();
            this.gridView_VehicleHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colV_ThoiDiem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colV_VanToc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colV_QuangDuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colV_ThoiGian = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colV_XacThuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_TextV = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colV_Lat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colV_Lng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnl_Anh = new DevExpress.XtraEditors.PanelControl();
            this.lbl_AnhHint = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Logout = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Login = new DevExpress.XtraEditors.LabelControl();
            this.Img_Logout = new DevExpress.XtraEditors.PictureEdit();
            this.Img_Login = new DevExpress.XtraEditors.PictureEdit();
            this.gmapControl = new GMap.NET.WindowsForms.GMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_MaPhien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Kpi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.split_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.split_Main.Panel1)).BeginInit();
            this.split_Main.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split_Main.Panel2)).BeginInit();
            this.split_Main.Panel2.SuspendLayout();
            this.split_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.tab_XacThuc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_HistoryData)).BeginInit();
            this.Menu_DetailSession.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_HistoryData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_TextH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_Anh)).BeginInit();
            this.tab_HanhTrinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_VehicleHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_VehicleHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_TextV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Anh)).BeginInit();
            this.pnl_Anh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Img_Logout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Img_Login.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // pnl_Header
            //
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.btn_CopyMa);
            this.pnl_Header.Controls.Add(this.txt_MaPhien);
            this.pnl_Header.Controls.Add(this.lb_TrangThai);
            this.pnl_Header.Controls.Add(this.lb_Sub);
            this.pnl_Header.Controls.Add(this.lb_HocVien);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(12, 12);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1474, 64);
            this.pnl_Header.TabIndex = 0;
            //
            // btn_CopyMa
            //
            this.btn_CopyMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CopyMa.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_CopyMa.ImageOptions.SvgImage")));
            this.btn_CopyMa.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btn_CopyMa.Location = new System.Drawing.Point(1438, 32);
            this.btn_CopyMa.Name = "btn_CopyMa";
            this.btn_CopyMa.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_CopyMa.Size = new System.Drawing.Size(26, 26);
            this.btn_CopyMa.TabIndex = 4;
            this.btn_CopyMa.ToolTip = "Sao chép mã phiên học";
            this.btn_CopyMa.Click += new System.EventHandler(this.btn_CopyMa_Click);
            //
            // txt_MaPhien
            //
            this.txt_MaPhien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_MaPhien.Location = new System.Drawing.Point(1134, 33);
            this.txt_MaPhien.Name = "txt_MaPhien";
            this.txt_MaPhien.Properties.Appearance.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txt_MaPhien.Properties.Appearance.Options.UseFont = true;
            this.txt_MaPhien.Properties.Appearance.Options.UseTextOptions = true;
            this.txt_MaPhien.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txt_MaPhien.Properties.AutoHeight = false;
            this.txt_MaPhien.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txt_MaPhien.Properties.ReadOnly = true;
            this.txt_MaPhien.Size = new System.Drawing.Size(300, 24);
            this.txt_MaPhien.TabIndex = 3;
            this.txt_MaPhien.ToolTip = "Mã phiên học (bôi đen để chọn, hoặc bấm nút bên cạnh để sao chép)";
            //
            // lb_TrangThai
            //
            this.lb_TrangThai.AllowHtmlString = true;
            this.lb_TrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_TrangThai.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.lb_TrangThai.Appearance.Options.UseFont = true;
            this.lb_TrangThai.Appearance.Options.UseTextOptions = true;
            this.lb_TrangThai.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lb_TrangThai.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb_TrangThai.Location = new System.Drawing.Point(1074, 8);
            this.lb_TrangThai.Name = "lb_TrangThai";
            this.lb_TrangThai.Size = new System.Drawing.Size(390, 20);
            this.lb_TrangThai.TabIndex = 2;
            //
            // lb_Sub
            //
            this.lb_Sub.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lb_Sub.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lb_Sub.Appearance.Options.UseFont = true;
            this.lb_Sub.Appearance.Options.UseForeColor = true;
            this.lb_Sub.Location = new System.Drawing.Point(16, 38);
            this.lb_Sub.Name = "lb_Sub";
            this.lb_Sub.Size = new System.Drawing.Size(140, 16);
            this.lb_Sub.TabIndex = 1;
            this.lb_Sub.Text = "Đang tải thông tin phiên...";
            //
            // lb_HocVien
            //
            this.lb_HocVien.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lb_HocVien.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lb_HocVien.Appearance.Options.UseFont = true;
            this.lb_HocVien.Appearance.Options.UseForeColor = true;
            this.lb_HocVien.Location = new System.Drawing.Point(16, 8);
            this.lb_HocVien.Name = "lb_HocVien";
            this.lb_HocVien.Size = new System.Drawing.Size(180, 25);
            this.lb_HocVien.TabIndex = 0;
            this.lb_HocVien.Text = "Chi tiết phiên học";
            //
            // pnl_Kpi
            //
            this.pnl_Kpi.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Kpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Kpi.Location = new System.Drawing.Point(12, 76);
            this.pnl_Kpi.Name = "pnl_Kpi";
            this.pnl_Kpi.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnl_Kpi.Size = new System.Drawing.Size(1474, 100);
            this.pnl_Kpi.TabIndex = 1;
            this.pnl_Kpi.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Kpi_Paint);
            this.pnl_Kpi.Resize += new System.EventHandler(this.pnl_Kpi_Resize);
            //
            // split_Main
            //
            this.split_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split_Main.Location = new System.Drawing.Point(12, 176);
            this.split_Main.Name = "split_Main";
            //
            // split_Main.Panel1
            //
            this.split_Main.Panel1.Controls.Add(this.xtraTabControl);
            this.split_Main.Panel1.Controls.Add(this.pnl_Anh);
            this.split_Main.Panel1.Text = "Panel1";
            //
            // split_Main.Panel2
            //
            this.split_Main.Panel2.Controls.Add(this.gmapControl);
            this.split_Main.Panel2.Text = "Panel2";
            this.split_Main.Size = new System.Drawing.Size(1474, 580);
            this.split_Main.SplitterPosition = 700;
            this.split_Main.TabIndex = 2;
            //
            // xtraTabControl
            //
            this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.xtraTabControl.AppearancePage.HeaderActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderActive.Options.UseForeColor = true;
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 214);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.tab_XacThuc;
            this.xtraTabControl.Size = new System.Drawing.Size(700, 366);
            this.xtraTabControl.TabIndex = 1;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tab_XacThuc,
            this.tab_HanhTrinh});
            //
            // tab_XacThuc
            //
            this.tab_XacThuc.Controls.Add(this.gridControl_HistoryData);
            this.tab_XacThuc.Name = "tab_XacThuc";
            this.tab_XacThuc.Size = new System.Drawing.Size(698, 337);
            this.tab_XacThuc.Text = "Ảnh xác thực";
            this.tab_XacThuc.Tooltip = "Ảnh chụp nhận diện khuôn mặt trong phiên (chuột phải để bổ sung ảnh đăng nhập / đăng xuất)";
            //
            // gridControl_HistoryData
            //
            this.gridControl_HistoryData.ContextMenuStrip = this.Menu_DetailSession;
            this.gridControl_HistoryData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_HistoryData.Location = new System.Drawing.Point(0, 0);
            this.gridControl_HistoryData.MainView = this.gridView_HistoryData;
            this.gridControl_HistoryData.Name = "gridControl_HistoryData";
            this.gridControl_HistoryData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_TextH,
            this.repo_Anh});
            this.gridControl_HistoryData.Size = new System.Drawing.Size(698, 337);
            this.gridControl_HistoryData.TabIndex = 0;
            this.gridControl_HistoryData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_HistoryData});
            //
            // Menu_DetailSession
            //
            this.Menu_DetailSession.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.Menu_DetailSession.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.update_ImageLogin,
            this.update_ImageLoout,
            this.toolStripSeparator1,
            this.mn_InsertHinhDem});
            this.Menu_DetailSession.Name = "Menu_DetailSession";
            this.Menu_DetailSession.Size = new System.Drawing.Size(330, 82);
            this.Menu_DetailSession.Opening += new System.ComponentModel.CancelEventHandler(this.Menu_DetailSession_Opening);
            //
            // update_ImageLogin
            //
            this.update_ImageLogin.Name = "update_ImageLogin";
            this.update_ImageLogin.Size = new System.Drawing.Size(329, 24);
            this.update_ImageLogin.Text = "Dùng ảnh này làm ảnh đăng nhập";
            this.update_ImageLogin.Click += new System.EventHandler(this.update_ImageLogin_Click);
            //
            // update_ImageLoout
            //
            this.update_ImageLoout.Name = "update_ImageLoout";
            this.update_ImageLoout.Size = new System.Drawing.Size(329, 24);
            this.update_ImageLoout.Text = "Dùng ảnh này làm ảnh đăng xuất";
            this.update_ImageLoout.Click += new System.EventHandler(this.update_ImageLoout_Click);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(326, 6);
            //
            // mn_InsertHinhDem
            //
            this.mn_InsertHinhDem.Name = "mn_InsertHinhDem";
            this.mn_InsertHinhDem.Size = new System.Drawing.Size(329, 24);
            this.mn_InsertHinhDem.Text = "Thêm ảnh này làm mẫu khuôn mặt ban đêm (VT_11)";
            this.mn_InsertHinhDem.Click += new System.EventHandler(this.mn_InsertHinhDem_Click);
            //
            // gridView_HistoryData
            //
            this.gridView_HistoryData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_HistoryData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_HistoryData.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_HistoryData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView_HistoryData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_HistoryData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_HistoryData.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_HistoryData.Appearance.Row.Options.UseFont = true;
            this.gridView_HistoryData.Appearance.Row.Options.UseTextOptions = true;
            this.gridView_HistoryData.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_HistoryData.ColumnPanelRowHeight = 34;
            this.gridView_HistoryData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colH_Id,
            this.colH_ThoiDiem,
            this.colH_VanToc,
            this.colH_QuangDuong,
            this.colH_ThoiGian,
            this.colH_XacThuc,
            this.colH_Anh});
            this.gridView_HistoryData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView_HistoryData.GridControl = this.gridControl_HistoryData;
            this.gridView_HistoryData.Name = "gridView_HistoryData";
            this.gridView_HistoryData.OptionsBehavior.Editable = false;
            this.gridView_HistoryData.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView_HistoryData.OptionsView.EnableAppearanceEvenRow = false;
            this.gridView_HistoryData.OptionsView.ShowGroupPanel = false;
            this.gridView_HistoryData.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView_HistoryData.RowHeight = 96;
            this.gridView_HistoryData.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView_CustomDrawEmptyForeground);
            this.gridView_HistoryData.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView_HistoryData.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView_CustomColumnDisplayText);
            this.gridView_HistoryData.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
            //
            // colH_Id
            //
            this.colH_Id.Caption = "ID";
            this.colH_Id.FieldName = "Id";
            this.colH_Id.Name = "colH_Id";
            //
            // colH_ThoiDiem
            //
            this.colH_ThoiDiem.Caption = "Thời điểm";
            this.colH_ThoiDiem.DisplayFormat.FormatString = "HH:mm:ss";
            this.colH_ThoiDiem.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colH_ThoiDiem.FieldName = "ThoiDiem";
            this.colH_ThoiDiem.Name = "colH_ThoiDiem";
            this.colH_ThoiDiem.Visible = true;
            this.colH_ThoiDiem.VisibleIndex = 0;
            this.colH_ThoiDiem.Width = 80;
            //
            // colH_VanToc
            //
            this.colH_VanToc.Caption = "Vận tốc (km/h)";
            this.colH_VanToc.DisplayFormat.FormatString = "n1";
            this.colH_VanToc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colH_VanToc.FieldName = "VanToc";
            this.colH_VanToc.Name = "colH_VanToc";
            this.colH_VanToc.Visible = true;
            this.colH_VanToc.VisibleIndex = 1;
            this.colH_VanToc.Width = 90;
            //
            // colH_QuangDuong
            //
            this.colH_QuangDuong.Caption = "Quãng đường (km)";
            this.colH_QuangDuong.DisplayFormat.FormatString = "n2";
            this.colH_QuangDuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colH_QuangDuong.FieldName = "QuangDuong";
            this.colH_QuangDuong.Name = "colH_QuangDuong";
            this.colH_QuangDuong.Visible = true;
            this.colH_QuangDuong.VisibleIndex = 2;
            this.colH_QuangDuong.Width = 100;
            //
            // colH_ThoiGian
            //
            this.colH_ThoiGian.Caption = "Đã học";
            this.colH_ThoiGian.FieldName = "ThoiGianText";
            this.colH_ThoiGian.Name = "colH_ThoiGian";
            this.colH_ThoiGian.Visible = true;
            this.colH_ThoiGian.VisibleIndex = 3;
            this.colH_ThoiGian.Width = 75;
            //
            // colH_XacThuc
            //
            this.colH_XacThuc.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.colH_XacThuc.AppearanceCell.Options.UseFont = true;
            this.colH_XacThuc.Caption = "Nhận diện";
            this.colH_XacThuc.ColumnEdit = this.repo_TextH;
            this.colH_XacThuc.FieldName = "XacThuc";
            this.colH_XacThuc.Name = "colH_XacThuc";
            this.colH_XacThuc.Visible = true;
            this.colH_XacThuc.VisibleIndex = 4;
            this.colH_XacThuc.Width = 85;
            //
            // repo_TextH
            //
            this.repo_TextH.AutoHeight = false;
            this.repo_TextH.Name = "repo_TextH";
            //
            // colH_Anh
            //
            this.colH_Anh.Caption = "Ảnh xác thực";
            this.colH_Anh.ColumnEdit = this.repo_Anh;
            this.colH_Anh.FieldName = "AnhXacThuc";
            this.colH_Anh.Name = "colH_Anh";
            this.colH_Anh.Visible = true;
            this.colH_Anh.VisibleIndex = 5;
            this.colH_Anh.Width = 150;
            //
            // repo_Anh
            //
            this.repo_Anh.Name = "repo_Anh";
            this.repo_Anh.NullText = "Không có ảnh";
            this.repo_Anh.ShowMenu = false;
            this.repo_Anh.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //
            // tab_HanhTrinh
            //
            this.tab_HanhTrinh.Controls.Add(this.gridControl_VehicleHistory);
            this.tab_HanhTrinh.Name = "tab_HanhTrinh";
            this.tab_HanhTrinh.Size = new System.Drawing.Size(698, 337);
            this.tab_HanhTrinh.Text = "Hành trình GPS";
            this.tab_HanhTrinh.Tooltip = "Dữ liệu GPS gửi từ thiết bị trên xe; chọn một dòng để xem vị trí trên bản đồ";
            //
            // gridControl_VehicleHistory
            //
            this.gridControl_VehicleHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_VehicleHistory.Location = new System.Drawing.Point(0, 0);
            this.gridControl_VehicleHistory.MainView = this.gridView_VehicleHistory;
            this.gridControl_VehicleHistory.Name = "gridControl_VehicleHistory";
            this.gridControl_VehicleHistory.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_TextV});
            this.gridControl_VehicleHistory.Size = new System.Drawing.Size(698, 337);
            this.gridControl_VehicleHistory.TabIndex = 0;
            this.gridControl_VehicleHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_VehicleHistory});
            //
            // gridView_VehicleHistory
            //
            this.gridView_VehicleHistory.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_VehicleHistory.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_VehicleHistory.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_VehicleHistory.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView_VehicleHistory.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_VehicleHistory.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_VehicleHistory.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_VehicleHistory.Appearance.Row.Options.UseFont = true;
            this.gridView_VehicleHistory.Appearance.Row.Options.UseTextOptions = true;
            this.gridView_VehicleHistory.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_VehicleHistory.ColumnPanelRowHeight = 34;
            this.gridView_VehicleHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colV_ThoiDiem,
            this.colV_VanToc,
            this.colV_QuangDuong,
            this.colV_ThoiGian,
            this.colV_XacThuc,
            this.colV_Lat,
            this.colV_Lng});
            this.gridView_VehicleHistory.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView_VehicleHistory.GridControl = this.gridControl_VehicleHistory;
            this.gridView_VehicleHistory.Name = "gridView_VehicleHistory";
            this.gridView_VehicleHistory.OptionsBehavior.Editable = false;
            this.gridView_VehicleHistory.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView_VehicleHistory.OptionsView.EnableAppearanceEvenRow = false;
            this.gridView_VehicleHistory.OptionsView.ShowGroupPanel = false;
            this.gridView_VehicleHistory.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView_VehicleHistory.RowHeight = 28;
            this.gridView_VehicleHistory.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView_CustomDrawEmptyForeground);
            this.gridView_VehicleHistory.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView_VehicleHistory.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView_CustomColumnDisplayText);
            this.gridView_VehicleHistory.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
            //
            // colV_ThoiDiem
            //
            this.colV_ThoiDiem.Caption = "Thời điểm";
            this.colV_ThoiDiem.DisplayFormat.FormatString = "HH:mm:ss";
            this.colV_ThoiDiem.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colV_ThoiDiem.FieldName = "ThoiDiem";
            this.colV_ThoiDiem.Name = "colV_ThoiDiem";
            this.colV_ThoiDiem.Visible = true;
            this.colV_ThoiDiem.VisibleIndex = 0;
            this.colV_ThoiDiem.Width = 80;
            //
            // colV_VanToc
            //
            this.colV_VanToc.Caption = "Vận tốc (km/h)";
            this.colV_VanToc.DisplayFormat.FormatString = "n1";
            this.colV_VanToc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colV_VanToc.FieldName = "VanToc";
            this.colV_VanToc.Name = "colV_VanToc";
            this.colV_VanToc.Visible = true;
            this.colV_VanToc.VisibleIndex = 1;
            this.colV_VanToc.Width = 90;
            //
            // colV_QuangDuong
            //
            this.colV_QuangDuong.Caption = "Quãng đường (km)";
            this.colV_QuangDuong.DisplayFormat.FormatString = "n2";
            this.colV_QuangDuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colV_QuangDuong.FieldName = "QuangDuong";
            this.colV_QuangDuong.Name = "colV_QuangDuong";
            this.colV_QuangDuong.Visible = true;
            this.colV_QuangDuong.VisibleIndex = 2;
            this.colV_QuangDuong.Width = 100;
            //
            // colV_ThoiGian
            //
            this.colV_ThoiGian.Caption = "Đã học";
            this.colV_ThoiGian.FieldName = "ThoiGianText";
            this.colV_ThoiGian.Name = "colV_ThoiGian";
            this.colV_ThoiGian.Visible = true;
            this.colV_ThoiGian.VisibleIndex = 3;
            this.colV_ThoiGian.Width = 75;
            //
            // colV_XacThuc
            //
            this.colV_XacThuc.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.colV_XacThuc.AppearanceCell.Options.UseFont = true;
            this.colV_XacThuc.Caption = "Nhận diện";
            this.colV_XacThuc.ColumnEdit = this.repo_TextV;
            this.colV_XacThuc.FieldName = "XacThuc";
            this.colV_XacThuc.Name = "colV_XacThuc";
            this.colV_XacThuc.Visible = true;
            this.colV_XacThuc.VisibleIndex = 4;
            this.colV_XacThuc.Width = 85;
            //
            // repo_TextV
            //
            this.repo_TextV.AutoHeight = false;
            this.repo_TextV.Name = "repo_TextV";
            //
            // colV_Lat
            //
            this.colV_Lat.Caption = "Vĩ độ";
            this.colV_Lat.DisplayFormat.FormatString = "n6";
            this.colV_Lat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colV_Lat.FieldName = "Lat";
            this.colV_Lat.Name = "colV_Lat";
            this.colV_Lat.Visible = true;
            this.colV_Lat.VisibleIndex = 5;
            this.colV_Lat.Width = 90;
            //
            // colV_Lng
            //
            this.colV_Lng.Caption = "Kinh độ";
            this.colV_Lng.DisplayFormat.FormatString = "n6";
            this.colV_Lng.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colV_Lng.FieldName = "Lng";
            this.colV_Lng.Name = "colV_Lng";
            this.colV_Lng.Visible = true;
            this.colV_Lng.VisibleIndex = 6;
            this.colV_Lng.Width = 95;
            //
            // pnl_Anh
            //
            this.pnl_Anh.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Anh.Controls.Add(this.lbl_AnhHint);
            this.pnl_Anh.Controls.Add(this.lbl_Logout);
            this.pnl_Anh.Controls.Add(this.lbl_Login);
            this.pnl_Anh.Controls.Add(this.Img_Logout);
            this.pnl_Anh.Controls.Add(this.Img_Login);
            this.pnl_Anh.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Anh.Location = new System.Drawing.Point(0, 0);
            this.pnl_Anh.Name = "pnl_Anh";
            this.pnl_Anh.Size = new System.Drawing.Size(700, 214);
            this.pnl_Anh.TabIndex = 0;
            //
            // lbl_AnhHint
            //
            this.lbl_AnhHint.AllowHtmlString = true;
            this.lbl_AnhHint.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_AnhHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_AnhHint.Appearance.Options.UseFont = true;
            this.lbl_AnhHint.Appearance.Options.UseForeColor = true;
            this.lbl_AnhHint.Appearance.Options.UseTextOptions = true;
            this.lbl_AnhHint.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lbl_AnhHint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbl_AnhHint.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_AnhHint.Location = new System.Drawing.Point(344, 28);
            this.lbl_AnhHint.Name = "lbl_AnhHint";
            this.lbl_AnhHint.Size = new System.Drawing.Size(340, 176);
            this.lbl_AnhHint.TabIndex = 4;
            this.lbl_AnhHint.Text = "Ảnh do thiết bị chụp lúc học viên đăng nhập và đăng xuất phiên.<br><br>Nếu thiếu ảnh, chọn một ảnh ở tab <b>Ảnh xác thực</b>, chuột phải → <b>Dùng ảnh này làm ảnh đăng nhập / đăng xuất</b>.";
            //
            // lbl_Logout
            //
            this.lbl_Logout.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Logout.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Logout.Appearance.Options.UseFont = true;
            this.lbl_Logout.Appearance.Options.UseForeColor = true;
            this.lbl_Logout.Location = new System.Drawing.Point(172, 8);
            this.lbl_Logout.Name = "lbl_Logout";
            this.lbl_Logout.Size = new System.Drawing.Size(84, 13);
            this.lbl_Logout.TabIndex = 3;
            this.lbl_Logout.Text = "ẢNH ĐĂNG XUẤT";
            //
            // lbl_Login
            //
            this.lbl_Login.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Login.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Login.Appearance.Options.UseFont = true;
            this.lbl_Login.Appearance.Options.UseForeColor = true;
            this.lbl_Login.Location = new System.Drawing.Point(4, 8);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(84, 13);
            this.lbl_Login.TabIndex = 2;
            this.lbl_Login.Text = "ẢNH ĐĂNG NHẬP";
            //
            // Img_Logout
            //
            this.Img_Logout.Location = new System.Drawing.Point(172, 28);
            this.Img_Logout.Name = "Img_Logout";
            this.Img_Logout.Properties.NullText = "Chưa có ảnh";
            this.Img_Logout.Properties.ReadOnly = true;
            this.Img_Logout.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.Img_Logout.Properties.ShowMenu = false;
            this.Img_Logout.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.Img_Logout.Size = new System.Drawing.Size(156, 176);
            this.Img_Logout.TabIndex = 1;
            //
            // Img_Login
            //
            this.Img_Login.Location = new System.Drawing.Point(4, 28);
            this.Img_Login.Name = "Img_Login";
            this.Img_Login.Properties.NullText = "Chưa có ảnh";
            this.Img_Login.Properties.ReadOnly = true;
            this.Img_Login.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.Img_Login.Properties.ShowMenu = false;
            this.Img_Login.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.Img_Login.Size = new System.Drawing.Size(156, 176);
            this.Img_Login.TabIndex = 0;
            //
            // gmapControl
            //
            this.gmapControl.Bearing = 0F;
            this.gmapControl.CanDragMap = true;
            this.gmapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmapControl.EmptyTileColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.gmapControl.GrayScaleMode = false;
            this.gmapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmapControl.LevelsKeepInMemory = 5;
            this.gmapControl.Location = new System.Drawing.Point(0, 0);
            this.gmapControl.MarkersEnabled = true;
            this.gmapControl.MaxZoom = 20;
            this.gmapControl.MinZoom = 1;
            this.gmapControl.MouseWheelZoomEnabled = true;
            this.gmapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmapControl.Name = "gmapControl";
            this.gmapControl.NegativeMode = false;
            this.gmapControl.PolygonsEnabled = true;
            this.gmapControl.RetryLoadTile = 0;
            this.gmapControl.RoutesEnabled = true;
            this.gmapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmapControl.ShowTileGridLines = false;
            this.gmapControl.Size = new System.Drawing.Size(764, 580);
            this.gmapControl.TabIndex = 0;
            this.gmapControl.Zoom = 15D;
            //
            // frmDetailSession
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1498, 768);
            this.Controls.Add(this.split_Main);
            this.Controls.Add(this.pnl_Kpi);
            this.Controls.Add(this.pnl_Header);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("frmDetailSession.IconOptions.SvgImage")));
            this.KeyPreview = true;
            this.Name = "frmDetailSession";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết phiên học";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDetailSession_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_MaPhien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Kpi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.split_Main.Panel1)).EndInit();
            this.split_Main.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split_Main.Panel2)).EndInit();
            this.split_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split_Main)).EndInit();
            this.split_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.tab_XacThuc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_HistoryData)).EndInit();
            this.Menu_DetailSession.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView_HistoryData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_TextH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_Anh)).EndInit();
            this.tab_HanhTrinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_VehicleHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_VehicleHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_TextV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Anh)).EndInit();
            this.pnl_Anh.ResumeLayout(false);
            this.pnl_Anh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Img_Logout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Img_Login.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.LabelControl lb_HocVien;
        private DevExpress.XtraEditors.LabelControl lb_Sub;
        private DevExpress.XtraEditors.LabelControl lb_TrangThai;
        private DevExpress.XtraEditors.TextEdit txt_MaPhien;
        private DevExpress.XtraEditors.SimpleButton btn_CopyMa;
        private DevExpress.XtraEditors.PanelControl pnl_Kpi;
        private DevExpress.XtraEditors.SplitContainerControl split_Main;
        private DevExpress.XtraEditors.PanelControl pnl_Anh;
        private DevExpress.XtraEditors.LabelControl lbl_Login;
        private DevExpress.XtraEditors.LabelControl lbl_Logout;
        private DevExpress.XtraEditors.LabelControl lbl_AnhHint;
        private DevExpress.XtraEditors.PictureEdit Img_Login;
        private DevExpress.XtraEditors.PictureEdit Img_Logout;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage tab_XacThuc;
        private DevExpress.XtraTab.XtraTabPage tab_HanhTrinh;
        private DevExpress.XtraGrid.GridControl gridControl_HistoryData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_HistoryData;
        private DevExpress.XtraGrid.Columns.GridColumn colH_Id;
        private DevExpress.XtraGrid.Columns.GridColumn colH_ThoiDiem;
        private DevExpress.XtraGrid.Columns.GridColumn colH_VanToc;
        private DevExpress.XtraGrid.Columns.GridColumn colH_QuangDuong;
        private DevExpress.XtraGrid.Columns.GridColumn colH_ThoiGian;
        private DevExpress.XtraGrid.Columns.GridColumn colH_XacThuc;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repo_TextH;
        private DevExpress.XtraGrid.Columns.GridColumn colH_Anh;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repo_Anh;
        private DevExpress.XtraGrid.GridControl gridControl_VehicleHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_VehicleHistory;
        private DevExpress.XtraGrid.Columns.GridColumn colV_ThoiDiem;
        private DevExpress.XtraGrid.Columns.GridColumn colV_VanToc;
        private DevExpress.XtraGrid.Columns.GridColumn colV_QuangDuong;
        private DevExpress.XtraGrid.Columns.GridColumn colV_ThoiGian;
        private DevExpress.XtraGrid.Columns.GridColumn colV_XacThuc;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repo_TextV;
        private DevExpress.XtraGrid.Columns.GridColumn colV_Lat;
        private DevExpress.XtraGrid.Columns.GridColumn colV_Lng;
        private System.Windows.Forms.ContextMenuStrip Menu_DetailSession;
        private System.Windows.Forms.ToolStripMenuItem update_ImageLogin;
        private System.Windows.Forms.ToolStripMenuItem update_ImageLoout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mn_InsertHinhDem;
        private GMap.NET.WindowsForms.GMapControl gmapControl;
    }
}
