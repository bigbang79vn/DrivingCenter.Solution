namespace DrivingCenter.App
{
    partial class frmKiemTraLyThuyet
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKiemTraLyThuyet));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridCtrl_DanhSachHocVien = new DevExpress.XtraGrid.GridControl();
            this.gridView_DanhSachHocVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HangDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GiaoVienID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoGrid_GiaoVien = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NguoiLXID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HocVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GioiTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CCCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DaoDuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KyThuat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CauTao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PL1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PL2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PL3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MoPhong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KetLuan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LastLogin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TimeToDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoGrid_XeTapLai = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repoDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repoGrid_Cabin = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_KhoaHoc = new System.Windows.Forms.Label();
            this.contextMenuStrip_KiemTra = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_DanhSachHocVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_DanhSachHocVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_GiaoVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_XeTapLai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_Cabin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip_KiemTra.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.gridCtrl_DanhSachHocVien, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1498, 768);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // gridCtrl_DanhSachHocVien
            // 
            this.gridCtrl_DanhSachHocVien.ContextMenuStrip = this.contextMenuStrip_KiemTra;
            this.gridCtrl_DanhSachHocVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrl_DanhSachHocVien.Location = new System.Drawing.Point(3, 53);
            this.gridCtrl_DanhSachHocVien.MainView = this.gridView_DanhSachHocVien;
            this.gridCtrl_DanhSachHocVien.Name = "gridCtrl_DanhSachHocVien";
            this.gridCtrl_DanhSachHocVien.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoGrid_XeTapLai,
            this.repoDate,
            this.repoGrid_Cabin,
            this.repoGrid_GiaoVien});
            this.gridCtrl_DanhSachHocVien.Size = new System.Drawing.Size(1492, 712);
            this.gridCtrl_DanhSachHocVien.TabIndex = 2;
            this.gridCtrl_DanhSachHocVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_DanhSachHocVien});
            // 
            // gridView_DanhSachHocVien
            // 
            this.gridView_DanhSachHocVien.Appearance.GroupRow.BackColor = System.Drawing.Color.SteelBlue;
            this.gridView_DanhSachHocVien.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_DanhSachHocVien.Appearance.GroupRow.ForeColor = System.Drawing.Color.White;
            this.gridView_DanhSachHocVien.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView_DanhSachHocVien.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_DanhSachHocVien.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView_DanhSachHocVien.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_DanhSachHocVien.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_DanhSachHocVien.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_DanhSachHocVien.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_DanhSachHocVien.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_DanhSachHocVien.Appearance.Row.Options.UseFont = true;
            this.gridView_DanhSachHocVien.ColumnPanelRowHeight = 27;
            this.gridView_DanhSachHocVien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaDK,
            this.TenKH,
            this.HangDT,
            this.GiaoVienID,
            this.NguoiLXID,
            this.HocVien,
            this.NgaySinh,
            this.GioiTinh,
            this.CCCD,
            this.DaoDuc,
            this.KyThuat,
            this.CauTao,
            this.PL1,
            this.PL2,
            this.PL3,
            this.MoPhong,
            this.KetLuan,
            this.LastLogin,
            this.TimeToDay});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gridView_DanhSachHocVien.FormatRules.Add(gridFormatRule1);
            this.gridView_DanhSachHocVien.GridControl = this.gridCtrl_DanhSachHocVien;
            this.gridView_DanhSachHocVien.GroupCount = 1;
            this.gridView_DanhSachHocVien.GroupRowHeight = 32;
            this.gridView_DanhSachHocVien.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "GiaoVienID", null, "(Tổng:  {0} học viên)")});
            this.gridView_DanhSachHocVien.Name = "gridView_DanhSachHocVien";
            this.gridView_DanhSachHocVien.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView_DanhSachHocVien.OptionsBehavior.Editable = false;
            this.gridView_DanhSachHocVien.OptionsPrint.ExpandAllDetails = true;
            this.gridView_DanhSachHocVien.OptionsView.ShowGroupPanelColumnsAsSingleRow = true;
            this.gridView_DanhSachHocVien.RowHeight = 27;
            this.gridView_DanhSachHocVien.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.GiaoVienID, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // MaDK
            // 
            this.MaDK.Caption = "Mã Đăng Ký";
            this.MaDK.FieldName = "MaDK";
            this.MaDK.Name = "MaDK";
            this.MaDK.Visible = true;
            this.MaDK.VisibleIndex = 0;
            this.MaDK.Width = 149;
            // 
            // TenKH
            // 
            this.TenKH.Caption = "Khoá Học";
            this.TenKH.FieldName = "TenKH";
            this.TenKH.Name = "TenKH";
            this.TenKH.Width = 79;
            // 
            // HangDT
            // 
            this.HangDT.Caption = "Hạng ĐT";
            this.HangDT.FieldName = "HangDT";
            this.HangDT.Name = "HangDT";
            // 
            // GiaoVienID
            // 
            this.GiaoVienID.Caption = "Giáo Viên";
            this.GiaoVienID.ColumnEdit = this.repoGrid_GiaoVien;
            this.GiaoVienID.FieldName = "GiaoVienID";
            this.GiaoVienID.Name = "GiaoVienID";
            this.GiaoVienID.Visible = true;
            this.GiaoVienID.VisibleIndex = 5;
            this.GiaoVienID.Width = 82;
            // 
            // repoGrid_GiaoVien
            // 
            this.repoGrid_GiaoVien.AutoHeight = false;
            this.repoGrid_GiaoVien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoGrid_GiaoVien.Name = "repoGrid_GiaoVien";
            this.repoGrid_GiaoVien.NullText = "Chưa phân công giáo viên";
            this.repoGrid_GiaoVien.PopupView = this.gridView2;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // NguoiLXID
            // 
            this.NguoiLXID.Caption = "NguoiLxID";
            this.NguoiLXID.FieldName = "NguoiLXID";
            this.NguoiLXID.Name = "NguoiLXID";
            // 
            // HocVien
            // 
            this.HocVien.Caption = "Học Viên";
            this.HocVien.FieldName = "HoVaTen";
            this.HocVien.Name = "HocVien";
            this.HocVien.Visible = true;
            this.HocVien.VisibleIndex = 1;
            this.HocVien.Width = 150;
            // 
            // NgaySinh
            // 
            this.NgaySinh.AppearanceCell.Options.UseTextOptions = true;
            this.NgaySinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NgaySinh.Caption = "Ngày Sinh";
            this.NgaySinh.FieldName = "NgaySinh";
            this.NgaySinh.Name = "NgaySinh";
            this.NgaySinh.Visible = true;
            this.NgaySinh.VisibleIndex = 2;
            this.NgaySinh.Width = 94;
            // 
            // GioiTinh
            // 
            this.GioiTinh.AppearanceCell.Options.UseTextOptions = true;
            this.GioiTinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GioiTinh.Caption = "Giới Tính";
            this.GioiTinh.FieldName = "GioiTinh";
            this.GioiTinh.Name = "GioiTinh";
            this.GioiTinh.Visible = true;
            this.GioiTinh.VisibleIndex = 3;
            this.GioiTinh.Width = 80;
            // 
            // CCCD
            // 
            this.CCCD.AppearanceCell.Options.UseTextOptions = true;
            this.CCCD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CCCD.Caption = "Số CMT/CCCD";
            this.CCCD.FieldName = "CCCD";
            this.CCCD.Name = "CCCD";
            this.CCCD.Visible = true;
            this.CCCD.VisibleIndex = 4;
            this.CCCD.Width = 113;
            // 
            // DaoDuc
            // 
            this.DaoDuc.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.DaoDuc.AppearanceCell.Options.UseFont = true;
            this.DaoDuc.AppearanceCell.Options.UseTextOptions = true;
            this.DaoDuc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DaoDuc.Caption = "Đạo Đức";
            this.DaoDuc.FieldName = "DaoDuc";
            this.DaoDuc.Name = "DaoDuc";
            this.DaoDuc.Visible = true;
            this.DaoDuc.VisibleIndex = 5;
            this.DaoDuc.Width = 82;
            // 
            // KyThuat
            // 
            this.KyThuat.AppearanceCell.Options.UseTextOptions = true;
            this.KyThuat.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KyThuat.Caption = "Kỹ Thuật";
            this.KyThuat.FieldName = "KyThuat";
            this.KyThuat.Name = "KyThuat";
            this.KyThuat.Visible = true;
            this.KyThuat.VisibleIndex = 6;
            this.KyThuat.Width = 77;
            // 
            // CauTao
            // 
            this.CauTao.AppearanceCell.Options.UseTextOptions = true;
            this.CauTao.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CauTao.Caption = "Cấu Tạo";
            this.CauTao.FieldName = "CauTao";
            this.CauTao.Name = "CauTao";
            this.CauTao.Visible = true;
            this.CauTao.VisibleIndex = 7;
            this.CauTao.Width = 76;
            // 
            // PL1
            // 
            this.PL1.AppearanceCell.Options.UseTextOptions = true;
            this.PL1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PL1.Caption = "Pháp Luật 1";
            this.PL1.FieldName = "PL1";
            this.PL1.Name = "PL1";
            this.PL1.Visible = true;
            this.PL1.VisibleIndex = 8;
            this.PL1.Width = 82;
            // 
            // PL2
            // 
            this.PL2.AppearanceCell.Options.UseTextOptions = true;
            this.PL2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PL2.Caption = "Pháp Luật 2";
            this.PL2.FieldName = "PL2";
            this.PL2.Name = "PL2";
            this.PL2.Visible = true;
            this.PL2.VisibleIndex = 9;
            this.PL2.Width = 83;
            // 
            // PL3
            // 
            this.PL3.AppearanceCell.Options.UseTextOptions = true;
            this.PL3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PL3.Caption = "Pháp Luật 3";
            this.PL3.FieldName = "PL3";
            this.PL3.Name = "PL3";
            this.PL3.Visible = true;
            this.PL3.VisibleIndex = 10;
            this.PL3.Width = 82;
            // 
            // MoPhong
            // 
            this.MoPhong.AppearanceCell.Options.UseTextOptions = true;
            this.MoPhong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MoPhong.Caption = "Mô Phỏng";
            this.MoPhong.FieldName = "MoPhong";
            this.MoPhong.Name = "MoPhong";
            this.MoPhong.Visible = true;
            this.MoPhong.VisibleIndex = 11;
            this.MoPhong.Width = 76;
            // 
            // KetLuan
            // 
            this.KetLuan.AppearanceCell.Options.UseTextOptions = true;
            this.KetLuan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KetLuan.Caption = "Kết Luận";
            this.KetLuan.FieldName = "KetLuan";
            this.KetLuan.Name = "KetLuan";
            this.KetLuan.Visible = true;
            this.KetLuan.VisibleIndex = 12;
            this.KetLuan.Width = 74;
            // 
            // LastLogin
            // 
            this.LastLogin.AppearanceCell.Options.UseTextOptions = true;
            this.LastLogin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LastLogin.Caption = "Lần Cuối Đăng Nhập";
            this.LastLogin.FieldName = "LastLogin";
            this.LastLogin.Name = "LastLogin";
            this.LastLogin.Visible = true;
            this.LastLogin.VisibleIndex = 13;
            this.LastLogin.Width = 138;
            // 
            // TimeToDay
            // 
            this.TimeToDay.AppearanceCell.Options.UseTextOptions = true;
            this.TimeToDay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TimeToDay.Caption = "Giờ Học Hôm Nay";
            this.TimeToDay.FieldName = "TimeToDay";
            this.TimeToDay.Name = "TimeToDay";
            this.TimeToDay.Visible = true;
            this.TimeToDay.VisibleIndex = 14;
            this.TimeToDay.Width = 111;
            // 
            // repoGrid_XeTapLai
            // 
            this.repoGrid_XeTapLai.AutoHeight = false;
            this.repoGrid_XeTapLai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoGrid_XeTapLai.Name = "repoGrid_XeTapLai";
            this.repoGrid_XeTapLai.NullText = "Chưa phân công";
            this.repoGrid_XeTapLai.PopupView = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // repoDate
            // 
            this.repoDate.AutoHeight = false;
            this.repoDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoDate.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.repoDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoDate.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            this.repoDate.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy";
            this.repoDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repoDate.MaskSettings.Set("mask", "HH:mm:ss | dd/MM/yyyy");
            this.repoDate.Name = "repoDate";
            this.repoDate.NullDate = "...";
            this.repoDate.UseMaskAsDisplayFormat = true;
            this.repoDate.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // repoGrid_Cabin
            // 
            this.repoGrid_Cabin.AutoHeight = false;
            this.repoGrid_Cabin.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoGrid_Cabin.Name = "repoGrid_Cabin";
            this.repoGrid_Cabin.NullText = "Chưa phân công";
            this.repoGrid_Cabin.PopupView = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_KhoaHoc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1492, 44);
            this.panel1.TabIndex = 1;
            // 
            // lbl_KhoaHoc
            // 
            this.lbl_KhoaHoc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_KhoaHoc.AutoSize = true;
            this.lbl_KhoaHoc.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KhoaHoc.Location = new System.Drawing.Point(533, 10);
            this.lbl_KhoaHoc.Name = "lbl_KhoaHoc";
            this.lbl_KhoaHoc.Size = new System.Drawing.Size(62, 24);
            this.lbl_KhoaHoc.TabIndex = 0;
            this.lbl_KhoaHoc.Text = "label1";
            // 
            // contextMenuStrip_KiemTra
            // 
            this.contextMenuStrip_KiemTra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.contextMenuStrip_KiemTra.Name = "contextMenuStrip_KiemTra";
            this.contextMenuStrip_KiemTra.Size = new System.Drawing.Size(108, 26);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // frmKiemTraLyThuyet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1498, 768);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("frmKiemTraLyThuyet.IconOptions.SvgImage")));
            this.Name = "frmKiemTraLyThuyet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiểm tra kết quả học lý thuyết Online";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmKiemTraLyThuyet_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_DanhSachHocVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_DanhSachHocVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_GiaoVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_XeTapLai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_Cabin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip_KiemTra.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_KhoaHoc;
        private DevExpress.XtraGrid.GridControl gridCtrl_DanhSachHocVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_DanhSachHocVien;
        private DevExpress.XtraGrid.Columns.GridColumn MaDK;
        private DevExpress.XtraGrid.Columns.GridColumn TenKH;
        private DevExpress.XtraGrid.Columns.GridColumn HangDT;
        private DevExpress.XtraGrid.Columns.GridColumn GiaoVienID;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repoGrid_GiaoVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn NguoiLXID;
        private DevExpress.XtraGrid.Columns.GridColumn HocVien;
        private DevExpress.XtraGrid.Columns.GridColumn NgaySinh;
        private DevExpress.XtraGrid.Columns.GridColumn GioiTinh;
        private DevExpress.XtraGrid.Columns.GridColumn CCCD;
        private DevExpress.XtraGrid.Columns.GridColumn DaoDuc;
        private DevExpress.XtraGrid.Columns.GridColumn KyThuat;
        private DevExpress.XtraGrid.Columns.GridColumn CauTao;
        private DevExpress.XtraGrid.Columns.GridColumn PL1;
        private DevExpress.XtraGrid.Columns.GridColumn PL2;
        private DevExpress.XtraGrid.Columns.GridColumn PL3;
        private DevExpress.XtraGrid.Columns.GridColumn MoPhong;
        private DevExpress.XtraGrid.Columns.GridColumn KetLuan;
        private DevExpress.XtraGrid.Columns.GridColumn LastLogin;
        private DevExpress.XtraGrid.Columns.GridColumn TimeToDay;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repoGrid_XeTapLai;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repoDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repoGrid_Cabin;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_KiemTra;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}