namespace DrivingCenter.App
{
    partial class frmPhanCongDaoTao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhanCongDaoTao));
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.btn_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_Desc = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            this.pnl_Right = new DevExpress.XtraEditors.PanelControl();
            this.gridControl_XeSTD = new DevExpress.XtraGrid.GridControl();
            this.gridView_XeSTD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colS_BienSo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colS_SoKhoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colS_SoHV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colS_SuDungDen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lbl_XeHint = new DevExpress.XtraEditors.LabelControl();
            this.lbl_XeSTD = new DevExpress.XtraEditors.LabelControl();
            this.sep_Right = new DevExpress.XtraEditors.SeparatorControl();
            this.pnl_Main = new DevExpress.XtraEditors.PanelControl();
            this.gridCtrl_LichHoc = new DevExpress.XtraGrid.GridControl();
            this.gridView_LichHoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGiaoVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXeTap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_XeTap = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colXeSTD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_XeSTD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colChayDAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKetThucDAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnl_ApDung = new DevExpress.XtraEditors.PanelControl();
            this.btn_Apdunglich = new DevExpress.XtraEditors.SimpleButton();
            this.dateEdit_End = new DevExpress.XtraEditors.DateEdit();
            this.lbl_Den = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit_Start = new DevExpress.XtraEditors.DateEdit();
            this.lbl_Tu = new DevExpress.XtraEditors.LabelControl();
            this.lbl_ApDung = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Right)).BeginInit();
            this.pnl_Right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_XeSTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_XeSTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sep_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Main)).BeginInit();
            this.pnl_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_LichHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LichHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_XeTap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_XeSTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_ApDung)).BeginInit();
            this.pnl_ApDung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // pnl_Header
            //
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.btn_Refresh);
            this.pnl_Header.Controls.Add(this.lbl_Desc);
            this.pnl_Header.Controls.Add(this.lbl_Title);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(12, 12);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1456, 64);
            this.pnl_Header.TabIndex = 0;
            //
            // btn_Refresh
            //
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Refresh.Appearance.Options.UseFont = true;
            this.btn_Refresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Refresh.ImageOptions.SvgImage")));
            this.btn_Refresh.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btn_Refresh.Location = new System.Drawing.Point(1350, 16);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(100, 32);
            this.btn_Refresh.TabIndex = 2;
            this.btn_Refresh.Text = "Làm mới";
            this.btn_Refresh.ToolTip = "Tải lại bố trí và mức sử dụng xe (F5)";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            //
            // lbl_Desc
            //
            this.lbl_Desc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Desc.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Desc.Appearance.Options.UseFont = true;
            this.lbl_Desc.Appearance.Options.UseForeColor = true;
            this.lbl_Desc.Location = new System.Drawing.Point(16, 38);
            this.lbl_Desc.Name = "lbl_Desc";
            this.lbl_Desc.Size = new System.Drawing.Size(560, 16);
            this.lbl_Desc.TabIndex = 1;
            this.lbl_Desc.Text = "Chọn xe số tự động cho giáo viên của từng khóa (sửa trực tiếp cột Xe STĐ) và đặt thời gian chạy DAT của khóa.";
            //
            // lbl_Title
            //
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Appearance.Options.UseForeColor = true;
            this.lbl_Title.Location = new System.Drawing.Point(16, 8);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(380, 25);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "Bố trí xe tập lái và xe số tự động";
            //
            // pnl_Right
            //
            this.pnl_Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Right.Controls.Add(this.gridControl_XeSTD);
            this.pnl_Right.Controls.Add(this.lbl_XeHint);
            this.pnl_Right.Controls.Add(this.lbl_XeSTD);
            this.pnl_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_Right.Location = new System.Drawing.Point(1048, 76);
            this.pnl_Right.Name = "pnl_Right";
            this.pnl_Right.Padding = new System.Windows.Forms.Padding(12, 8, 0, 0);
            this.pnl_Right.Size = new System.Drawing.Size(420, 701);
            this.pnl_Right.TabIndex = 3;
            //
            // gridControl_XeSTD
            //
            this.gridControl_XeSTD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_XeSTD.Location = new System.Drawing.Point(12, 92);
            this.gridControl_XeSTD.MainView = this.gridView_XeSTD;
            this.gridControl_XeSTD.Name = "gridControl_XeSTD";
            this.gridControl_XeSTD.Size = new System.Drawing.Size(408, 609);
            this.gridControl_XeSTD.TabIndex = 2;
            this.gridControl_XeSTD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_XeSTD});
            //
            // gridView_XeSTD
            //
            this.gridView_XeSTD.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_XeSTD.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_XeSTD.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_XeSTD.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView_XeSTD.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_XeSTD.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_XeSTD.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView_XeSTD.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_XeSTD.Appearance.Row.Options.UseFont = true;
            this.gridView_XeSTD.Appearance.Row.Options.UseTextOptions = true;
            this.gridView_XeSTD.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_XeSTD.ColumnPanelRowHeight = 40;
            this.gridView_XeSTD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colS_BienSo,
            this.colS_SoKhoa,
            this.colS_SoHV,
            this.colS_SuDungDen});
            this.gridView_XeSTD.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView_XeSTD.GridControl = this.gridControl_XeSTD;
            this.gridView_XeSTD.Name = "gridView_XeSTD";
            this.gridView_XeSTD.OptionsBehavior.Editable = false;
            this.gridView_XeSTD.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView_XeSTD.OptionsView.EnableAppearanceEvenRow = false;
            this.gridView_XeSTD.OptionsView.ShowGroupPanel = false;
            this.gridView_XeSTD.OptionsView.ShowIndicator = false;
            this.gridView_XeSTD.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView_XeSTD.RowHeight = 30;
            this.gridView_XeSTD.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colS_BienSo, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView_XeSTD.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_XeSTD_RowCellStyle);
            this.gridView_XeSTD.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView_CustomDrawEmptyForeground);
            this.gridView_XeSTD.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_XeSTD_FocusedRowChanged);
            this.gridView_XeSTD.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView_XeSTD_CustomColumnDisplayText);
            //
            // colS_BienSo
            //
            this.colS_BienSo.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.colS_BienSo.AppearanceCell.Options.UseFont = true;
            this.colS_BienSo.Caption = "Biển số";
            this.colS_BienSo.FieldName = "BienSoXe";
            this.colS_BienSo.Name = "colS_BienSo";
            this.colS_BienSo.Visible = true;
            this.colS_BienSo.VisibleIndex = 0;
            this.colS_BienSo.Width = 100;
            //
            // colS_SoKhoa
            //
            this.colS_SoKhoa.Caption = "Khóa đang dùng";
            this.colS_SoKhoa.FieldName = "SoKhoaDangDung";
            this.colS_SoKhoa.Name = "colS_SoKhoa";
            this.colS_SoKhoa.Visible = true;
            this.colS_SoKhoa.VisibleIndex = 1;
            this.colS_SoKhoa.Width = 80;
            //
            // colS_SoHV
            //
            this.colS_SoHV.Caption = "Học viên đang dùng";
            this.colS_SoHV.FieldName = "SoHVDangDung";
            this.colS_SoHV.Name = "colS_SoHV";
            this.colS_SoHV.Visible = true;
            this.colS_SoHV.VisibleIndex = 2;
            this.colS_SoHV.Width = 90;
            //
            // colS_SuDungDen
            //
            this.colS_SuDungDen.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.colS_SuDungDen.AppearanceCell.Options.UseFont = true;
            this.colS_SuDungDen.Caption = "Sử dụng đến";
            this.colS_SuDungDen.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colS_SuDungDen.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colS_SuDungDen.FieldName = "SuDungDen";
            this.colS_SuDungDen.Name = "colS_SuDungDen";
            this.colS_SuDungDen.Visible = true;
            this.colS_SuDungDen.VisibleIndex = 3;
            this.colS_SuDungDen.Width = 100;
            //
            // lbl_XeHint
            //
            this.lbl_XeHint.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_XeHint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_XeHint.Appearance.Options.UseFont = true;
            this.lbl_XeHint.Appearance.Options.UseForeColor = true;
            this.lbl_XeHint.Appearance.Options.UseTextOptions = true;
            this.lbl_XeHint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbl_XeHint.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_XeHint.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_XeHint.Location = new System.Drawing.Point(12, 44);
            this.lbl_XeHint.Name = "lbl_XeHint";
            this.lbl_XeHint.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lbl_XeHint.Size = new System.Drawing.Size(408, 48);
            this.lbl_XeHint.TabIndex = 1;
            this.lbl_XeHint.Text = "Tính trên mọi khóa (không riêng tháng này): khóa / học viên có xe STĐ này mà thời gian chạy DAT chưa kết thúc. Chọn một xe để tô các dòng đang dùng xe đó.";
            //
            // lbl_XeSTD
            //
            this.lbl_XeSTD.AllowHtmlString = true;
            this.lbl_XeSTD.Appearance.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_XeSTD.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_XeSTD.Appearance.Options.UseFont = true;
            this.lbl_XeSTD.Appearance.Options.UseForeColor = true;
            this.lbl_XeSTD.Appearance.Options.UseTextOptions = true;
            this.lbl_XeSTD.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lbl_XeSTD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_XeSTD.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_XeSTD.Location = new System.Drawing.Point(12, 8);
            this.lbl_XeSTD.Name = "lbl_XeSTD";
            this.lbl_XeSTD.Size = new System.Drawing.Size(408, 36);
            this.lbl_XeSTD.TabIndex = 0;
            this.lbl_XeSTD.Text = "Xe số tự động (B.01)";
            //
            // sep_Right
            //
            this.sep_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.sep_Right.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.sep_Right.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.sep_Right.Location = new System.Drawing.Point(1047, 76);
            this.sep_Right.Name = "sep_Right";
            this.sep_Right.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.sep_Right.Size = new System.Drawing.Size(1, 701);
            this.sep_Right.TabIndex = 2;
            //
            // pnl_Main
            //
            this.pnl_Main.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Main.Controls.Add(this.gridCtrl_LichHoc);
            this.pnl_Main.Controls.Add(this.pnl_ApDung);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(12, 76);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Padding = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.pnl_Main.Size = new System.Drawing.Size(1035, 701);
            this.pnl_Main.TabIndex = 1;
            //
            // gridCtrl_LichHoc
            //
            this.gridCtrl_LichHoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrl_LichHoc.Location = new System.Drawing.Point(0, 8);
            this.gridCtrl_LichHoc.MainView = this.gridView_LichHoc;
            this.gridCtrl_LichHoc.Name = "gridCtrl_LichHoc";
            this.gridCtrl_LichHoc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repo_XeTap,
            this.repo_XeSTD});
            this.gridCtrl_LichHoc.Size = new System.Drawing.Size(1023, 625);
            this.gridCtrl_LichHoc.TabIndex = 0;
            this.gridCtrl_LichHoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_LichHoc});
            //
            // gridView_LichHoc
            //
            this.gridView_LichHoc.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_LichHoc.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_LichHoc.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_LichHoc.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_LichHoc.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_LichHoc.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_LichHoc.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_LichHoc.Appearance.Row.Options.UseFont = true;
            this.gridView_LichHoc.ColumnPanelRowHeight = 36;
            this.gridView_LichHoc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHang,
            this.colKhoa,
            this.colGiaoVien,
            this.colXeTap,
            this.colXeSTD,
            this.colChayDAT,
            this.colKetThucDAT});
            this.gridView_LichHoc.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView_LichHoc.GridControl = this.gridCtrl_LichHoc;
            this.gridView_LichHoc.GroupCount = 2;
            this.gridView_LichHoc.GroupFormat = "{1}  {2}";
            this.gridView_LichHoc.GroupRowHeight = 32;
            this.gridView_LichHoc.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "GiaoVien", null, "·  {0} giáo viên")});
            this.gridView_LichHoc.Name = "gridView_LichHoc";
            this.gridView_LichHoc.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView_LichHoc.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView_LichHoc.OptionsFind.AlwaysVisible = true;
            this.gridView_LichHoc.OptionsFind.FindNullPrompt = "Tìm khóa, giáo viên, biển số...";
            this.gridView_LichHoc.OptionsFind.ShowClearButton = false;
            this.gridView_LichHoc.OptionsFind.ShowFindButton = false;
            this.gridView_LichHoc.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView_LichHoc.OptionsView.EnableAppearanceEvenRow = false;
            this.gridView_LichHoc.OptionsView.ShowGroupPanel = false;
            this.gridView_LichHoc.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView_LichHoc.RowHeight = 32;
            this.gridView_LichHoc.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colHang, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colKhoa, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colGiaoVien, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView_LichHoc.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_LichHoc_RowCellStyle);
            this.gridView_LichHoc.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView_CustomDrawEmptyForeground);
            this.gridView_LichHoc.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_LichHoc_FocusedRowChanged);
            this.gridView_LichHoc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_LichHoc_CellValueChanged);
            //
            // colHang
            //
            this.colHang.Caption = "Hạng";
            this.colHang.FieldName = "HangDT";
            this.colHang.GroupFormat.FormatString = "Hạng {0}";
            this.colHang.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colHang.Name = "colHang";
            this.colHang.OptionsColumn.AllowEdit = false;
            this.colHang.Visible = true;
            this.colHang.VisibleIndex = 0;
            //
            // colKhoa
            //
            this.colKhoa.Caption = "Khóa học";
            this.colKhoa.FieldName = "TenKH";
            this.colKhoa.Name = "colKhoa";
            this.colKhoa.OptionsColumn.AllowEdit = false;
            this.colKhoa.Visible = true;
            this.colKhoa.VisibleIndex = 1;
            //
            // colGiaoVien
            //
            this.colGiaoVien.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.colGiaoVien.AppearanceCell.Options.UseFont = true;
            this.colGiaoVien.Caption = "Giáo viên";
            this.colGiaoVien.FieldName = "GiaoVien";
            this.colGiaoVien.Name = "colGiaoVien";
            this.colGiaoVien.OptionsColumn.AllowEdit = false;
            this.colGiaoVien.Visible = true;
            this.colGiaoVien.VisibleIndex = 0;
            this.colGiaoVien.Width = 220;
            //
            // colXeTap
            //
            this.colXeTap.AppearanceCell.Options.UseTextOptions = true;
            this.colXeTap.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colXeTap.Caption = "Xe tập lái";
            this.colXeTap.ColumnEdit = this.repo_XeTap;
            this.colXeTap.FieldName = "MaXeTap";
            this.colXeTap.Name = "colXeTap";
            this.colXeTap.OptionsColumn.AllowEdit = false;
            this.colXeTap.ToolTip = "Xe tập lái chính của giáo viên (đổi ở màn hình Phân công DAT)";
            this.colXeTap.Visible = true;
            this.colXeTap.VisibleIndex = 1;
            this.colXeTap.Width = 120;
            //
            // repo_XeTap
            //
            this.repo_XeTap.AutoHeight = false;
            this.repo_XeTap.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repo_XeTap.DisplayMember = "BienSoXe";
            this.repo_XeTap.Name = "repo_XeTap";
            this.repo_XeTap.NullText = "—";
            this.repo_XeTap.ValueMember = "MaXe";
            //
            // colXeSTD
            //
            this.colXeSTD.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.colXeSTD.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.colXeSTD.AppearanceCell.Options.UseFont = true;
            this.colXeSTD.AppearanceCell.Options.UseForeColor = true;
            this.colXeSTD.AppearanceCell.Options.UseTextOptions = true;
            this.colXeSTD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colXeSTD.Caption = "Xe STĐ ✎";
            this.colXeSTD.ColumnEdit = this.repo_XeSTD;
            this.colXeSTD.FieldName = "MaXeTapB11";
            this.colXeSTD.Name = "colXeSTD";
            this.colXeSTD.ToolTip = "Xe số tự động (B.01) giáo viên dùng cho khóa; chọn là lưu ngay";
            this.colXeSTD.Visible = true;
            this.colXeSTD.VisibleIndex = 2;
            this.colXeSTD.Width = 130;
            //
            // repo_XeSTD
            //
            this.repo_XeSTD.AutoHeight = false;
            this.repo_XeSTD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.repo_XeSTD.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BienSoXe", "Biển số", 90),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SoKhoaDangDung", "Khóa đang dùng", 90),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SuDungDen", "Sử dụng đến", 90, DevExpress.Utils.FormatType.DateTime, "dd/MM/yyyy", true, DevExpress.Utils.HorzAlignment.Center)});
            this.repo_XeSTD.DisplayMember = "BienSoXe";
            this.repo_XeSTD.DropDownRows = 14;
            this.repo_XeSTD.Name = "repo_XeSTD";
            this.repo_XeSTD.NullText = "Chưa gán";
            this.repo_XeSTD.PopupWidth = 320;
            this.repo_XeSTD.ValueMember = "MaXe";
            //
            // colChayDAT
            //
            this.colChayDAT.AppearanceCell.Options.UseTextOptions = true;
            this.colChayDAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChayDAT.Caption = "Chạy DAT từ";
            this.colChayDAT.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colChayDAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colChayDAT.FieldName = "ThoiDiemChayDAT";
            this.colChayDAT.Name = "colChayDAT";
            this.colChayDAT.OptionsColumn.AllowEdit = false;
            this.colChayDAT.Visible = true;
            this.colChayDAT.VisibleIndex = 3;
            this.colChayDAT.Width = 110;
            //
            // colKetThucDAT
            //
            this.colKetThucDAT.AppearanceCell.Options.UseTextOptions = true;
            this.colKetThucDAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKetThucDAT.Caption = "đến";
            this.colKetThucDAT.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colKetThucDAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colKetThucDAT.FieldName = "ThoiDiemKetThucDAT";
            this.colKetThucDAT.Name = "colKetThucDAT";
            this.colKetThucDAT.OptionsColumn.AllowEdit = false;
            this.colKetThucDAT.Visible = true;
            this.colKetThucDAT.VisibleIndex = 4;
            this.colKetThucDAT.Width = 110;
            //
            // pnl_ApDung
            //
            this.pnl_ApDung.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_ApDung.Controls.Add(this.btn_Apdunglich);
            this.pnl_ApDung.Controls.Add(this.dateEdit_End);
            this.pnl_ApDung.Controls.Add(this.lbl_Den);
            this.pnl_ApDung.Controls.Add(this.dateEdit_Start);
            this.pnl_ApDung.Controls.Add(this.lbl_Tu);
            this.pnl_ApDung.Controls.Add(this.lbl_ApDung);
            this.pnl_ApDung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_ApDung.Location = new System.Drawing.Point(0, 633);
            this.pnl_ApDung.Name = "pnl_ApDung";
            this.pnl_ApDung.Size = new System.Drawing.Size(1023, 68);
            this.pnl_ApDung.TabIndex = 1;
            //
            // btn_Apdunglich
            //
            this.btn_Apdunglich.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.btn_Apdunglich.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Apdunglich.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Apdunglich.Appearance.Options.UseBackColor = true;
            this.btn_Apdunglich.Appearance.Options.UseFont = true;
            this.btn_Apdunglich.Appearance.Options.UseForeColor = true;
            this.btn_Apdunglich.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btn_Apdunglich.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btn_Apdunglich.AppearanceHovered.Options.UseBackColor = true;
            this.btn_Apdunglich.AppearanceHovered.Options.UseForeColor = true;
            this.btn_Apdunglich.Enabled = false;
            this.btn_Apdunglich.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Apdunglich.ImageOptions.SvgImage")));
            this.btn_Apdunglich.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btn_Apdunglich.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btn_Apdunglich.Location = new System.Drawing.Point(452, 28);
            this.btn_Apdunglich.Name = "btn_Apdunglich";
            this.btn_Apdunglich.Size = new System.Drawing.Size(190, 32);
            this.btn_Apdunglich.TabIndex = 5;
            this.btn_Apdunglich.Text = "Áp dụng cho khóa";
            this.btn_Apdunglich.Click += new System.EventHandler(this.btn_Apdunglich_Click);
            //
            // dateEdit_End
            //
            this.dateEdit_End.EditValue = null;
            this.dateEdit_End.Location = new System.Drawing.Point(296, 28);
            this.dateEdit_End.Name = "dateEdit_End";
            this.dateEdit_End.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.dateEdit_End.Properties.Appearance.Options.UseFont = true;
            this.dateEdit_End.Properties.AutoHeight = false;
            this.dateEdit_End.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_End.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_End.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit_End.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit_End.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit_End.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit_End.Properties.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.dateEdit_End.Size = new System.Drawing.Size(140, 32);
            this.dateEdit_End.TabIndex = 4;
            //
            // lbl_Den
            //
            this.lbl_Den.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Den.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Den.Appearance.Options.UseFont = true;
            this.lbl_Den.Appearance.Options.UseForeColor = true;
            this.lbl_Den.Location = new System.Drawing.Point(266, 36);
            this.lbl_Den.Name = "lbl_Den";
            this.lbl_Den.Size = new System.Drawing.Size(22, 16);
            this.lbl_Den.TabIndex = 3;
            this.lbl_Den.Text = "đến";
            //
            // dateEdit_Start
            //
            this.dateEdit_Start.EditValue = null;
            this.dateEdit_Start.Location = new System.Drawing.Point(112, 28);
            this.dateEdit_Start.Name = "dateEdit_Start";
            this.dateEdit_Start.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.dateEdit_Start.Properties.Appearance.Options.UseFont = true;
            this.dateEdit_Start.Properties.AutoHeight = false;
            this.dateEdit_Start.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_Start.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_Start.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit_Start.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit_Start.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit_Start.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit_Start.Properties.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.dateEdit_Start.Size = new System.Drawing.Size(140, 32);
            this.dateEdit_Start.TabIndex = 2;
            //
            // lbl_Tu
            //
            this.lbl_Tu.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Tu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Tu.Appearance.Options.UseFont = true;
            this.lbl_Tu.Appearance.Options.UseForeColor = true;
            this.lbl_Tu.Location = new System.Drawing.Point(4, 36);
            this.lbl_Tu.Name = "lbl_Tu";
            this.lbl_Tu.Size = new System.Drawing.Size(98, 16);
            this.lbl_Tu.TabIndex = 1;
            this.lbl_Tu.Text = "Chạy DAT từ ngày";
            //
            // lbl_ApDung
            //
            this.lbl_ApDung.AllowHtmlString = true;
            this.lbl_ApDung.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_ApDung.Appearance.Options.UseFont = true;
            this.lbl_ApDung.Location = new System.Drawing.Point(4, 6);
            this.lbl_ApDung.Name = "lbl_ApDung";
            this.lbl_ApDung.Size = new System.Drawing.Size(330, 16);
            this.lbl_ApDung.TabIndex = 0;
            this.lbl_ApDung.Text = "Thời gian chạy DAT – chọn một khóa (hoặc giáo viên của khóa) ở lưới trên";
            //
            // frmPhanCongDaoTao
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 789);
            this.Controls.Add(this.pnl_Main);
            this.Controls.Add(this.sep_Right);
            this.Controls.Add(this.pnl_Right);
            this.Controls.Add(this.pnl_Header);
            this.KeyPreview = true;
            this.Name = "frmPhanCongDaoTao";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bố trí xe tập lái";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPhanCongDaoTao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Right)).EndInit();
            this.pnl_Right.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_XeSTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_XeSTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sep_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Main)).EndInit();
            this.pnl_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_LichHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LichHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_XeTap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_XeSTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_ApDung)).EndInit();
            this.pnl_ApDung.ResumeLayout(false);
            this.pnl_ApDung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.LabelControl lbl_Desc;
        private DevExpress.XtraEditors.SimpleButton btn_Refresh;
        private DevExpress.XtraEditors.PanelControl pnl_Right;
        private DevExpress.XtraEditors.LabelControl lbl_XeSTD;
        private DevExpress.XtraEditors.LabelControl lbl_XeHint;
        private DevExpress.XtraGrid.GridControl gridControl_XeSTD;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_XeSTD;
        private DevExpress.XtraGrid.Columns.GridColumn colS_BienSo;
        private DevExpress.XtraGrid.Columns.GridColumn colS_SoKhoa;
        private DevExpress.XtraGrid.Columns.GridColumn colS_SoHV;
        private DevExpress.XtraGrid.Columns.GridColumn colS_SuDungDen;
        private DevExpress.XtraEditors.SeparatorControl sep_Right;
        private DevExpress.XtraEditors.PanelControl pnl_Main;
        private DevExpress.XtraGrid.GridControl gridCtrl_LichHoc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_LichHoc;
        private DevExpress.XtraGrid.Columns.GridColumn colHang;
        private DevExpress.XtraGrid.Columns.GridColumn colKhoa;
        private DevExpress.XtraGrid.Columns.GridColumn colGiaoVien;
        private DevExpress.XtraGrid.Columns.GridColumn colXeTap;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repo_XeTap;
        private DevExpress.XtraGrid.Columns.GridColumn colXeSTD;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repo_XeSTD;
        private DevExpress.XtraGrid.Columns.GridColumn colChayDAT;
        private DevExpress.XtraGrid.Columns.GridColumn colKetThucDAT;
        private DevExpress.XtraEditors.PanelControl pnl_ApDung;
        private DevExpress.XtraEditors.LabelControl lbl_ApDung;
        private DevExpress.XtraEditors.LabelControl lbl_Tu;
        private DevExpress.XtraEditors.DateEdit dateEdit_Start;
        private DevExpress.XtraEditors.LabelControl lbl_Den;
        private DevExpress.XtraEditors.DateEdit dateEdit_End;
        private DevExpress.XtraEditors.SimpleButton btn_Apdunglich;
    }
}
