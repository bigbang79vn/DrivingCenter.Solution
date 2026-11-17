
namespace DrivingCenter.App.UserControl
{
    partial class UcChild_GiaoVien
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.btn_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Sync = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_Desc = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            this.spacerTop = new System.Windows.Forms.Panel();
            this.grp_Detail = new DevExpress.XtraEditors.GroupControl();
            this.lbl_Hint = new DevExpress.XtraEditors.LabelControl();
            this.val_HocVan = new DevExpress.XtraEditors.LabelControl();
            this.cap_HocVan = new DevExpress.XtraEditors.LabelControl();
            this.val_MaCNDT = new DevExpress.XtraEditors.LabelControl();
            this.cap_MaCNDT = new DevExpress.XtraEditors.LabelControl();
            this.val_HangGPLX = new DevExpress.XtraEditors.LabelControl();
            this.cap_HangGPLX = new DevExpress.XtraEditors.LabelControl();
            this.val_CCCD = new DevExpress.XtraEditors.LabelControl();
            this.cap_CCCD = new DevExpress.XtraEditors.LabelControl();
            this.val_NgaySinh = new DevExpress.XtraEditors.LabelControl();
            this.cap_NgaySinh = new DevExpress.XtraEditors.LabelControl();
            this.lbl_MaGV = new DevExpress.XtraEditors.LabelControl();
            this.lbl_HoTen = new DevExpress.XtraEditors.LabelControl();
            this.pic_AnhChup = new DevExpress.XtraEditors.PictureEdit();
            this.spacerRight = new System.Windows.Forms.Panel();
            this.grp_List = new DevExpress.XtraEditors.GroupControl();
            this.gridControl_GiaoVien = new DevExpress.XtraGrid.GridControl();
            this.gridView_GiaoVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AnhChup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_Avatar = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.MaGV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HoTenGV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoCMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HangGPLX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaCNDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TrinhDoHocVan = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_Detail)).BeginInit();
            this.grp_Detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_AnhChup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_List)).BeginInit();
            this.grp_List.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_GiaoVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_GiaoVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_Avatar)).BeginInit();
            this.SuspendLayout();
            //
            // pnl_Header
            //
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.btn_Refresh);
            this.pnl_Header.Controls.Add(this.btn_Export);
            this.pnl_Header.Controls.Add(this.btn_Sync);
            this.pnl_Header.Controls.Add(this.lbl_Desc);
            this.pnl_Header.Controls.Add(this.lbl_Title);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(12, 12);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1476, 62);
            this.pnl_Header.TabIndex = 0;
            //
            // btn_Refresh
            //
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Refresh.Appearance.Options.UseFont = true;
            this.btn_Refresh.Location = new System.Drawing.Point(1064, 14);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(110, 34);
            this.btn_Refresh.TabIndex = 2;
            this.btn_Refresh.Text = "Làm mới";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            //
            // btn_Export
            //
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Export.Appearance.Options.UseFont = true;
            this.btn_Export.Location = new System.Drawing.Point(1180, 14);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(120, 34);
            this.btn_Export.TabIndex = 3;
            this.btn_Export.Text = "Xuất Excel";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            //
            // btn_Sync
            //
            this.btn_Sync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Sync.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Sync.Appearance.Options.UseFont = true;
            this.btn_Sync.Location = new System.Drawing.Point(1306, 14);
            this.btn_Sync.Name = "btn_Sync";
            this.btn_Sync.Size = new System.Drawing.Size(170, 34);
            this.btn_Sync.TabIndex = 4;
            this.btn_Sync.Text = "Đồng bộ từ GPLX";
            this.btn_Sync.ToolTip = "Lấy lại danh sách giáo viên từ hệ thống GPLX";
            this.btn_Sync.Click += new System.EventHandler(this.btn_Sync_Click);
            //
            // lbl_Desc
            //
            this.lbl_Desc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Desc.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Desc.Appearance.Options.UseFont = true;
            this.lbl_Desc.Appearance.Options.UseForeColor = true;
            this.lbl_Desc.Location = new System.Drawing.Point(18, 36);
            this.lbl_Desc.Name = "lbl_Desc";
            this.lbl_Desc.Size = new System.Drawing.Size(420, 16);
            this.lbl_Desc.TabIndex = 1;
            this.lbl_Desc.Text = "Danh sách giáo viên dạy thực hành, đồng bộ từ hệ thống quản lý GPLX.";
            //
            // lbl_Title
            //
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Appearance.Options.UseForeColor = true;
            this.lbl_Title.Location = new System.Drawing.Point(16, 6);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(110, 25);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "Giáo viên";
            //
            // spacerTop
            //
            this.spacerTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.spacerTop.Location = new System.Drawing.Point(12, 74);
            this.spacerTop.Name = "spacerTop";
            this.spacerTop.Size = new System.Drawing.Size(1476, 10);
            this.spacerTop.TabIndex = 1;
            //
            // grp_Detail
            //
            this.grp_Detail.AppearanceCaption.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold);
            this.grp_Detail.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.grp_Detail.AppearanceCaption.Options.UseFont = true;
            this.grp_Detail.AppearanceCaption.Options.UseForeColor = true;
            this.grp_Detail.Controls.Add(this.lbl_Hint);
            this.grp_Detail.Controls.Add(this.val_HocVan);
            this.grp_Detail.Controls.Add(this.cap_HocVan);
            this.grp_Detail.Controls.Add(this.val_MaCNDT);
            this.grp_Detail.Controls.Add(this.cap_MaCNDT);
            this.grp_Detail.Controls.Add(this.val_HangGPLX);
            this.grp_Detail.Controls.Add(this.cap_HangGPLX);
            this.grp_Detail.Controls.Add(this.val_CCCD);
            this.grp_Detail.Controls.Add(this.cap_CCCD);
            this.grp_Detail.Controls.Add(this.val_NgaySinh);
            this.grp_Detail.Controls.Add(this.cap_NgaySinh);
            this.grp_Detail.Controls.Add(this.lbl_MaGV);
            this.grp_Detail.Controls.Add(this.lbl_HoTen);
            this.grp_Detail.Controls.Add(this.pic_AnhChup);
            this.grp_Detail.Dock = System.Windows.Forms.DockStyle.Right;
            this.grp_Detail.Location = new System.Drawing.Point(1108, 84);
            this.grp_Detail.Name = "grp_Detail";
            this.grp_Detail.Size = new System.Drawing.Size(380, 704);
            this.grp_Detail.TabIndex = 4;
            this.grp_Detail.Text = "HỒ SƠ GIÁO VIÊN";
            this.grp_Detail.Paint += new System.Windows.Forms.PaintEventHandler(this.grp_Detail_Paint);
            //
            // lbl_Hint
            //
            this.lbl_Hint.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Italic);
            this.lbl_Hint.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Hint.Appearance.Options.UseFont = true;
            this.lbl_Hint.Appearance.Options.UseForeColor = true;
            this.lbl_Hint.Appearance.Options.UseTextOptions = true;
            this.lbl_Hint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbl_Hint.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lbl_Hint.Location = new System.Drawing.Point(20, 420);
            this.lbl_Hint.Name = "lbl_Hint";
            this.lbl_Hint.Size = new System.Drawing.Size(340, 26);
            this.lbl_Hint.TabIndex = 13;
            this.lbl_Hint.Text = "Thông tin giáo viên được quản lý trên hệ thống GPLX. Bấm \"Đồng bộ từ GPLX\" để cập nhật dữ liệu mới nhất.";
            //
            // val_HocVan
            //
            this.val_HocVan.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_HocVan.Appearance.Options.UseFont = true;
            this.val_HocVan.AutoEllipsis = true;
            this.val_HocVan.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_HocVan.Location = new System.Drawing.Point(150, 362);
            this.val_HocVan.Name = "val_HocVan";
            this.val_HocVan.Size = new System.Drawing.Size(210, 18);
            this.val_HocVan.TabIndex = 12;
            this.val_HocVan.Text = "—";
            //
            // cap_HocVan
            //
            this.cap_HocVan.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_HocVan.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_HocVan.Appearance.Options.UseFont = true;
            this.cap_HocVan.Appearance.Options.UseForeColor = true;
            this.cap_HocVan.Location = new System.Drawing.Point(20, 363);
            this.cap_HocVan.Name = "cap_HocVan";
            this.cap_HocVan.Size = new System.Drawing.Size(95, 15);
            this.cap_HocVan.TabIndex = 11;
            this.cap_HocVan.Text = "Trình độ học vấn";
            //
            // val_MaCNDT
            //
            this.val_MaCNDT.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_MaCNDT.Appearance.Options.UseFont = true;
            this.val_MaCNDT.AutoEllipsis = true;
            this.val_MaCNDT.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_MaCNDT.Location = new System.Drawing.Point(150, 326);
            this.val_MaCNDT.Name = "val_MaCNDT";
            this.val_MaCNDT.Size = new System.Drawing.Size(210, 18);
            this.val_MaCNDT.TabIndex = 10;
            this.val_MaCNDT.Text = "—";
            //
            // cap_MaCNDT
            //
            this.cap_MaCNDT.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_MaCNDT.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_MaCNDT.Appearance.Options.UseFont = true;
            this.cap_MaCNDT.Appearance.Options.UseForeColor = true;
            this.cap_MaCNDT.Location = new System.Drawing.Point(20, 327);
            this.cap_MaCNDT.Name = "cap_MaCNDT";
            this.cap_MaCNDT.Size = new System.Drawing.Size(52, 15);
            this.cap_MaCNDT.TabIndex = 9;
            this.cap_MaCNDT.Text = "Mã CNĐT";
            //
            // val_HangGPLX
            //
            this.val_HangGPLX.Appearance.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.val_HangGPLX.Appearance.Options.UseFont = true;
            this.val_HangGPLX.AutoEllipsis = true;
            this.val_HangGPLX.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_HangGPLX.Location = new System.Drawing.Point(150, 290);
            this.val_HangGPLX.Name = "val_HangGPLX";
            this.val_HangGPLX.Size = new System.Drawing.Size(210, 18);
            this.val_HangGPLX.TabIndex = 8;
            this.val_HangGPLX.Text = "—";
            //
            // cap_HangGPLX
            //
            this.cap_HangGPLX.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_HangGPLX.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_HangGPLX.Appearance.Options.UseFont = true;
            this.cap_HangGPLX.Appearance.Options.UseForeColor = true;
            this.cap_HangGPLX.Location = new System.Drawing.Point(20, 291);
            this.cap_HangGPLX.Name = "cap_HangGPLX";
            this.cap_HangGPLX.Size = new System.Drawing.Size(59, 15);
            this.cap_HangGPLX.TabIndex = 7;
            this.cap_HangGPLX.Text = "Hạng GPLX";
            //
            // val_CCCD
            //
            this.val_CCCD.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_CCCD.Appearance.Options.UseFont = true;
            this.val_CCCD.AutoEllipsis = true;
            this.val_CCCD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_CCCD.Location = new System.Drawing.Point(150, 254);
            this.val_CCCD.Name = "val_CCCD";
            this.val_CCCD.Size = new System.Drawing.Size(210, 18);
            this.val_CCCD.TabIndex = 6;
            this.val_CCCD.Text = "—";
            //
            // cap_CCCD
            //
            this.cap_CCCD.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_CCCD.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_CCCD.Appearance.Options.UseFont = true;
            this.cap_CCCD.Appearance.Options.UseForeColor = true;
            this.cap_CCCD.Location = new System.Drawing.Point(20, 255);
            this.cap_CCCD.Name = "cap_CCCD";
            this.cap_CCCD.Size = new System.Drawing.Size(70, 15);
            this.cap_CCCD.TabIndex = 5;
            this.cap_CCCD.Text = "Số CMT/CCCD";
            //
            // val_NgaySinh
            //
            this.val_NgaySinh.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_NgaySinh.Appearance.Options.UseFont = true;
            this.val_NgaySinh.AutoEllipsis = true;
            this.val_NgaySinh.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_NgaySinh.Location = new System.Drawing.Point(150, 218);
            this.val_NgaySinh.Name = "val_NgaySinh";
            this.val_NgaySinh.Size = new System.Drawing.Size(210, 18);
            this.val_NgaySinh.TabIndex = 4;
            this.val_NgaySinh.Text = "—";
            //
            // cap_NgaySinh
            //
            this.cap_NgaySinh.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_NgaySinh.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_NgaySinh.Appearance.Options.UseFont = true;
            this.cap_NgaySinh.Appearance.Options.UseForeColor = true;
            this.cap_NgaySinh.Location = new System.Drawing.Point(20, 219);
            this.cap_NgaySinh.Name = "cap_NgaySinh";
            this.cap_NgaySinh.Size = new System.Drawing.Size(54, 15);
            this.cap_NgaySinh.TabIndex = 3;
            this.cap_NgaySinh.Text = "Ngày sinh";
            //
            // lbl_MaGV
            //
            this.lbl_MaGV.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_MaGV.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_MaGV.Appearance.Options.UseFont = true;
            this.lbl_MaGV.Appearance.Options.UseForeColor = true;
            this.lbl_MaGV.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_MaGV.Location = new System.Drawing.Point(152, 120);
            this.lbl_MaGV.Name = "lbl_MaGV";
            this.lbl_MaGV.Size = new System.Drawing.Size(208, 16);
            this.lbl_MaGV.TabIndex = 2;
            this.lbl_MaGV.Text = "Mã GV: —";
            //
            // lbl_HoTen
            //
            this.lbl_HoTen.Appearance.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
            this.lbl_HoTen.Appearance.Options.UseFont = true;
            this.lbl_HoTen.Appearance.Options.UseTextOptions = true;
            this.lbl_HoTen.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.lbl_HoTen.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbl_HoTen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_HoTen.Location = new System.Drawing.Point(152, 48);
            this.lbl_HoTen.Name = "lbl_HoTen";
            this.lbl_HoTen.Size = new System.Drawing.Size(208, 66);
            this.lbl_HoTen.TabIndex = 1;
            this.lbl_HoTen.Text = "Chọn một giáo viên";
            //
            // pic_AnhChup
            //
            this.pic_AnhChup.Location = new System.Drawing.Point(20, 42);
            this.pic_AnhChup.Name = "pic_AnhChup";
            this.pic_AnhChup.Properties.AllowFocused = false;
            this.pic_AnhChup.Properties.NullText = "Chưa có ảnh";
            this.pic_AnhChup.Properties.ReadOnly = true;
            this.pic_AnhChup.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pic_AnhChup.Properties.ShowMenu = false;
            this.pic_AnhChup.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_AnhChup.Size = new System.Drawing.Size(116, 145);
            this.pic_AnhChup.TabIndex = 0;
            //
            // spacerRight
            //
            this.spacerRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.spacerRight.Location = new System.Drawing.Point(1098, 84);
            this.spacerRight.Name = "spacerRight";
            this.spacerRight.Size = new System.Drawing.Size(10, 704);
            this.spacerRight.TabIndex = 3;
            //
            // grp_List
            //
            this.grp_List.AppearanceCaption.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold);
            this.grp_List.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.grp_List.AppearanceCaption.Options.UseFont = true;
            this.grp_List.AppearanceCaption.Options.UseForeColor = true;
            this.grp_List.Controls.Add(this.gridControl_GiaoVien);
            this.grp_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_List.Location = new System.Drawing.Point(12, 84);
            this.grp_List.Name = "grp_List";
            this.grp_List.Size = new System.Drawing.Size(1086, 704);
            this.grp_List.TabIndex = 2;
            this.grp_List.Text = "DANH SÁCH GIÁO VIÊN";
            //
            // gridControl_GiaoVien
            //
            this.gridControl_GiaoVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_GiaoVien.Location = new System.Drawing.Point(2, 27);
            this.gridControl_GiaoVien.MainView = this.gridView_GiaoVien;
            this.gridControl_GiaoVien.Name = "gridControl_GiaoVien";
            this.gridControl_GiaoVien.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_Avatar});
            this.gridControl_GiaoVien.Size = new System.Drawing.Size(1082, 675);
            this.gridControl_GiaoVien.TabIndex = 0;
            this.gridControl_GiaoVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_GiaoVien});
            //
            // gridView_GiaoVien
            //
            this.gridView_GiaoVien.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_GiaoVien.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_GiaoVien.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_GiaoVien.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_GiaoVien.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_GiaoVien.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_GiaoVien.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_GiaoVien.Appearance.Row.Options.UseFont = true;
            this.gridView_GiaoVien.Appearance.Row.Options.UseTextOptions = true;
            this.gridView_GiaoVien.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_GiaoVien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.AnhChup,
            this.MaGV,
            this.HoTenGV,
            this.NgaySinh,
            this.SoCMT,
            this.HangGPLX,
            this.MaCNDT,
            this.TrinhDoHocVan});
            this.gridView_GiaoVien.GridControl = this.gridControl_GiaoVien;
            this.gridView_GiaoVien.GroupCount = 1;
            this.gridView_GiaoVien.GroupFormat = "Hạng {1} {2}";
            this.gridView_GiaoVien.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "MaGV", null, "— {0} giáo viên")});
            this.gridView_GiaoVien.Name = "gridView_GiaoVien";
            this.gridView_GiaoVien.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView_GiaoVien.OptionsBehavior.Editable = false;
            this.gridView_GiaoVien.OptionsBehavior.ReadOnly = true;
            this.gridView_GiaoVien.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.HangGPLX, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.MaGV, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView_GiaoVien.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_GiaoVien_FocusedRowChanged);
            this.gridView_GiaoVien.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView_GiaoVien_CustomDrawEmptyForeground);
            //
            // ID
            //
            this.ID.Caption = "GiaoVienID";
            this.ID.FieldName = "GiaoVienID";
            this.ID.Name = "ID";
            //
            // AnhChup
            //
            this.AnhChup.Caption = "Ảnh";
            this.AnhChup.ColumnEdit = this.repo_Avatar;
            this.AnhChup.FieldName = "AnhChup";
            this.AnhChup.Name = "AnhChup";
            this.AnhChup.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.AnhChup.OptionsColumn.FixedWidth = true;
            this.AnhChup.OptionsFilter.AllowFilter = false;
            this.AnhChup.Visible = true;
            this.AnhChup.VisibleIndex = 0;
            this.AnhChup.Width = 50;
            //
            // repo_Avatar
            //
            this.repo_Avatar.Name = "repo_Avatar";
            this.repo_Avatar.NullText = " ";
            this.repo_Avatar.ShowMenu = false;
            this.repo_Avatar.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            //
            // MaGV
            //
            this.MaGV.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.MaGV.AppearanceCell.Options.UseFont = true;
            this.MaGV.Caption = "Mã GV";
            this.MaGV.FieldName = "MaGV";
            this.MaGV.Name = "MaGV";
            this.MaGV.Visible = true;
            this.MaGV.VisibleIndex = 1;
            this.MaGV.Width = 90;
            //
            // HoTenGV
            //
            this.HoTenGV.AppearanceCell.Options.UseTextOptions = true;
            this.HoTenGV.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.HoTenGV.Caption = "Họ và tên";
            this.HoTenGV.FieldName = "HoTenGV";
            this.HoTenGV.Name = "HoTenGV";
            this.HoTenGV.Visible = true;
            this.HoTenGV.VisibleIndex = 2;
            this.HoTenGV.Width = 200;
            //
            // NgaySinh
            //
            this.NgaySinh.Caption = "Ngày sinh";
            this.NgaySinh.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.NgaySinh.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.NgaySinh.FieldName = "NgaySinh";
            this.NgaySinh.Name = "NgaySinh";
            this.NgaySinh.Visible = true;
            this.NgaySinh.VisibleIndex = 3;
            this.NgaySinh.Width = 100;
            //
            // SoCMT
            //
            this.SoCMT.Caption = "Số CMT/CCCD";
            this.SoCMT.FieldName = "SoCMT";
            this.SoCMT.Name = "SoCMT";
            this.SoCMT.Visible = true;
            this.SoCMT.VisibleIndex = 4;
            this.SoCMT.Width = 130;
            //
            // HangGPLX
            //
            this.HangGPLX.Caption = "Hạng GPLX";
            this.HangGPLX.FieldName = "HangGPLX";
            this.HangGPLX.Name = "HangGPLX";
            this.HangGPLX.Visible = true;
            this.HangGPLX.VisibleIndex = 5;
            this.HangGPLX.Width = 90;
            //
            // MaCNDT
            //
            this.MaCNDT.Caption = "Mã CNĐT";
            this.MaCNDT.FieldName = "MaCNDT";
            this.MaCNDT.Name = "MaCNDT";
            this.MaCNDT.Visible = true;
            this.MaCNDT.VisibleIndex = 5;
            this.MaCNDT.Width = 110;
            //
            // TrinhDoHocVan
            //
            this.TrinhDoHocVan.Caption = "Trình độ học vấn";
            this.TrinhDoHocVan.FieldName = "TrinhDoHocVan";
            this.TrinhDoHocVan.Name = "TrinhDoHocVan";
            this.TrinhDoHocVan.Visible = true;
            this.TrinhDoHocVan.VisibleIndex = 6;
            this.TrinhDoHocVan.Width = 130;
            //
            // UcChild_GiaoVien
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_List);
            this.Controls.Add(this.spacerRight);
            this.Controls.Add(this.grp_Detail);
            this.Controls.Add(this.spacerTop);
            this.Controls.Add(this.pnl_Header);
            this.Name = "UcChild_GiaoVien";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(1500, 800);
            this.Load += new System.EventHandler(this.UcChild_GiaoVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_Detail)).EndInit();
            this.grp_Detail.ResumeLayout(false);
            this.grp_Detail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_AnhChup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_List)).EndInit();
            this.grp_List.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_GiaoVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_GiaoVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_Avatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.LabelControl lbl_Desc;
        private DevExpress.XtraEditors.SimpleButton btn_Sync;
        private DevExpress.XtraEditors.SimpleButton btn_Export;
        private DevExpress.XtraEditors.SimpleButton btn_Refresh;
        private System.Windows.Forms.Panel spacerTop;
        private DevExpress.XtraEditors.GroupControl grp_List;
        private DevExpress.XtraGrid.GridControl gridControl_GiaoVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_GiaoVien;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn AnhChup;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repo_Avatar;
        private DevExpress.XtraGrid.Columns.GridColumn MaGV;
        private DevExpress.XtraGrid.Columns.GridColumn HoTenGV;
        private DevExpress.XtraGrid.Columns.GridColumn NgaySinh;
        private DevExpress.XtraGrid.Columns.GridColumn SoCMT;
        private DevExpress.XtraGrid.Columns.GridColumn HangGPLX;
        private DevExpress.XtraGrid.Columns.GridColumn MaCNDT;
        private DevExpress.XtraGrid.Columns.GridColumn TrinhDoHocVan;
        private System.Windows.Forms.Panel spacerRight;
        private DevExpress.XtraEditors.GroupControl grp_Detail;
        private DevExpress.XtraEditors.PictureEdit pic_AnhChup;
        private DevExpress.XtraEditors.LabelControl lbl_HoTen;
        private DevExpress.XtraEditors.LabelControl lbl_MaGV;
        private DevExpress.XtraEditors.LabelControl cap_NgaySinh;
        private DevExpress.XtraEditors.LabelControl val_NgaySinh;
        private DevExpress.XtraEditors.LabelControl cap_CCCD;
        private DevExpress.XtraEditors.LabelControl val_CCCD;
        private DevExpress.XtraEditors.LabelControl cap_HangGPLX;
        private DevExpress.XtraEditors.LabelControl val_HangGPLX;
        private DevExpress.XtraEditors.LabelControl cap_MaCNDT;
        private DevExpress.XtraEditors.LabelControl val_MaCNDT;
        private DevExpress.XtraEditors.LabelControl cap_HocVan;
        private DevExpress.XtraEditors.LabelControl val_HocVan;
        private DevExpress.XtraEditors.LabelControl lbl_Hint;
    }
}
