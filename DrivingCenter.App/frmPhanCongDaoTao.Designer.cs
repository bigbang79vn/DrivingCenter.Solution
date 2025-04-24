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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhanCongDaoTao));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridCtrl_LichHoc = new DevExpress.XtraGrid.GridControl();
            this.gridView_LichHoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KhoaHocId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKHGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GiaoVienID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GiaoVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ThoiDiemChayDAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ThoiDiemKetThucDAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaXeTap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoGrid_XeTapLai = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaXeTapB11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoGrid_XeSTD = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.TimeInB11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.TimeOutB11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HangDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Cabin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.StartCabin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EndCabin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoGrid_Cabin = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Apdunglich = new System.Windows.Forms.Button();
            this.dateEdit_End = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.dateEdit_Start = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.windowsUIButtonPanel = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.gridControl_XeSTD = new DevExpress.XtraGrid.GridControl();
            this.gridView_XeSTD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IdXeTap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BienSoXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HangXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SuDungDen = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_LichHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LichHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_XeTapLai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_XeSTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_Cabin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_XeSTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_XeSTD)).BeginInit();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.Caption = "Check";
            this.Status.FieldName = "Status";
            this.Status.Name = "Status";
            // 
            // gridCtrl_LichHoc
            // 
            this.gridCtrl_LichHoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrl_LichHoc.Location = new System.Drawing.Point(3, 3);
            this.gridCtrl_LichHoc.MainView = this.gridView_LichHoc;
            this.gridCtrl_LichHoc.Name = "gridCtrl_LichHoc";
            this.gridCtrl_LichHoc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoGrid_XeTapLai,
            this.repoDate,
            this.repoGrid_Cabin,
            this.repoGrid_XeSTD});
            this.gridCtrl_LichHoc.Size = new System.Drawing.Size(1024, 714);
            this.gridCtrl_LichHoc.TabIndex = 0;
            this.gridCtrl_LichHoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_LichHoc});
            // 
            // gridView_LichHoc
            // 
            this.gridView_LichHoc.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_LichHoc.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_LichHoc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_LichHoc.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_LichHoc.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_LichHoc.Appearance.Row.Options.UseFont = true;
            this.gridView_LichHoc.ColumnPanelRowHeight = 32;
            this.gridView_LichHoc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Id,
            this.KhoaHocId,
            this.MaKH,
            this.TenKH,
            this.TenKHGroup,
            this.GiaoVienID,
            this.GiaoVien,
            this.ThoiDiemChayDAT,
            this.ThoiDiemKetThucDAT,
            this.MaXeTap,
            this.MaXeTapB11,
            this.TimeInB11,
            this.TimeOutB11,
            this.HangDT,
            this.Status,
            this.Cabin,
            this.StartCabin,
            this.EndCabin});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.Status;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            formatConditionRuleValue1.Appearance.Options.UseFont = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = "True";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gridView_LichHoc.FormatRules.Add(gridFormatRule1);
            this.gridView_LichHoc.GridControl = this.gridCtrl_LichHoc;
            this.gridView_LichHoc.GroupCount = 2;
            this.gridView_LichHoc.GroupRowHeight = 32;
            this.gridView_LichHoc.Name = "gridView_LichHoc";
            this.gridView_LichHoc.OptionsView.ShowGroupPanelColumnsAsSingleRow = true;
            this.gridView_LichHoc.OptionsView.ShowIndicator = false;
            this.gridView_LichHoc.RowHeight = 27;
            this.gridView_LichHoc.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.HangDT, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.TenKH, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView_LichHoc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_LichHoc_CellValueChanged);
            // 
            // Id
            // 
            this.Id.Caption = "Id";
            this.Id.FieldName = "Id";
            this.Id.Name = "Id";
            // 
            // KhoaHocId
            // 
            this.KhoaHocId.Caption = "KhoaHocId";
            this.KhoaHocId.FieldName = "KhoaHocId";
            this.KhoaHocId.Name = "KhoaHocId";
            // 
            // MaKH
            // 
            this.MaKH.AppearanceCell.Options.UseTextOptions = true;
            this.MaKH.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaKH.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.MaKH.AppearanceHeader.Options.UseBackColor = true;
            this.MaKH.Caption = "Mã Khóa Học";
            this.MaKH.FieldName = "MaKH";
            this.MaKH.Name = "MaKH";
            this.MaKH.Visible = true;
            this.MaKH.VisibleIndex = 0;
            this.MaKH.Width = 80;
            // 
            // TenKH
            // 
            this.TenKH.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.TenKH.AppearanceHeader.Options.UseBackColor = true;
            this.TenKH.Caption = "Khoá Học";
            this.TenKH.FieldName = "TenKH";
            this.TenKH.Name = "TenKH";
            this.TenKH.Visible = true;
            this.TenKH.VisibleIndex = 2;
            this.TenKH.Width = 68;
            // 
            // TenKHGroup
            // 
            this.TenKHGroup.AppearanceCell.Options.UseTextOptions = true;
            this.TenKHGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TenKHGroup.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.TenKHGroup.AppearanceHeader.Options.UseBackColor = true;
            this.TenKHGroup.Caption = "Khóa Học";
            this.TenKHGroup.FieldName = "TenKH";
            this.TenKHGroup.Name = "TenKHGroup";
            this.TenKHGroup.Visible = true;
            this.TenKHGroup.VisibleIndex = 1;
            this.TenKHGroup.Width = 43;
            // 
            // GiaoVienID
            // 
            this.GiaoVienID.Caption = "GiaoVienID";
            this.GiaoVienID.FieldName = "GiaoVienID";
            this.GiaoVienID.Name = "GiaoVienID";
            // 
            // GiaoVien
            // 
            this.GiaoVien.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.GiaoVien.AppearanceHeader.Options.UseBackColor = true;
            this.GiaoVien.Caption = "Giáo Viên";
            this.GiaoVien.FieldName = "GiaoVien";
            this.GiaoVien.Name = "GiaoVien";
            this.GiaoVien.Visible = true;
            this.GiaoVien.VisibleIndex = 2;
            this.GiaoVien.Width = 119;
            // 
            // ThoiDiemChayDAT
            // 
            this.ThoiDiemChayDAT.AppearanceCell.Options.UseTextOptions = true;
            this.ThoiDiemChayDAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ThoiDiemChayDAT.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.ThoiDiemChayDAT.AppearanceHeader.Options.UseBackColor = true;
            this.ThoiDiemChayDAT.Caption = "Start DAT";
            this.ThoiDiemChayDAT.FieldName = "ThoiDiemChayDAT";
            this.ThoiDiemChayDAT.Name = "ThoiDiemChayDAT";
            this.ThoiDiemChayDAT.Visible = true;
            this.ThoiDiemChayDAT.VisibleIndex = 5;
            this.ThoiDiemChayDAT.Width = 76;
            // 
            // ThoiDiemKetThucDAT
            // 
            this.ThoiDiemKetThucDAT.AppearanceCell.Options.UseTextOptions = true;
            this.ThoiDiemKetThucDAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ThoiDiemKetThucDAT.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.ThoiDiemKetThucDAT.AppearanceHeader.Options.UseBackColor = true;
            this.ThoiDiemKetThucDAT.Caption = "End DAT";
            this.ThoiDiemKetThucDAT.FieldName = "ThoiDiemKetThucDAT";
            this.ThoiDiemKetThucDAT.Name = "ThoiDiemKetThucDAT";
            this.ThoiDiemKetThucDAT.Visible = true;
            this.ThoiDiemKetThucDAT.VisibleIndex = 6;
            this.ThoiDiemKetThucDAT.Width = 81;
            // 
            // MaXeTap
            // 
            this.MaXeTap.AppearanceCell.Options.UseTextOptions = true;
            this.MaXeTap.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaXeTap.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.MaXeTap.AppearanceHeader.Options.UseBackColor = true;
            this.MaXeTap.Caption = "Xe Tập Lái";
            this.MaXeTap.ColumnEdit = this.repoGrid_XeTapLai;
            this.MaXeTap.FieldName = "MaXeTap";
            this.MaXeTap.Name = "MaXeTap";
            this.MaXeTap.Visible = true;
            this.MaXeTap.VisibleIndex = 3;
            this.MaXeTap.Width = 58;
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
            // MaXeTapB11
            // 
            this.MaXeTapB11.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.MaXeTapB11.AppearanceCell.Font = new System.Drawing.Font("San Francisco Text Bold G3", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaXeTapB11.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MaXeTapB11.AppearanceCell.Options.UseBackColor = true;
            this.MaXeTapB11.AppearanceCell.Options.UseFont = true;
            this.MaXeTapB11.AppearanceCell.Options.UseForeColor = true;
            this.MaXeTapB11.AppearanceCell.Options.UseTextOptions = true;
            this.MaXeTapB11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaXeTapB11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.MaXeTapB11.AppearanceHeader.Options.UseBackColor = true;
            this.MaXeTapB11.Caption = "Xe STĐ";
            this.MaXeTapB11.ColumnEdit = this.repoGrid_XeSTD;
            this.MaXeTapB11.FieldName = "MaXeTapB11";
            this.MaXeTapB11.Name = "MaXeTapB11";
            this.MaXeTapB11.Visible = true;
            this.MaXeTapB11.VisibleIndex = 4;
            this.MaXeTapB11.Width = 55;
            // 
            // repoGrid_XeSTD
            // 
            this.repoGrid_XeSTD.AutoHeight = false;
            this.repoGrid_XeSTD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoGrid_XeSTD.Name = "repoGrid_XeSTD";
            this.repoGrid_XeSTD.NullText = "Chưa phân công";
            this.repoGrid_XeSTD.PopupView = this.gridView2;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // TimeInB11
            // 
            this.TimeInB11.AppearanceCell.Options.UseTextOptions = true;
            this.TimeInB11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TimeInB11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.TimeInB11.AppearanceHeader.Options.UseBackColor = true;
            this.TimeInB11.Caption = "Start STĐ";
            this.TimeInB11.ColumnEdit = this.repoDate;
            this.TimeInB11.FieldName = "TimeInB11";
            this.TimeInB11.Name = "TimeInB11";
            this.TimeInB11.Visible = true;
            this.TimeInB11.VisibleIndex = 7;
            this.TimeInB11.Width = 83;
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
            // TimeOutB11
            // 
            this.TimeOutB11.AppearanceCell.Options.UseTextOptions = true;
            this.TimeOutB11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TimeOutB11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.TimeOutB11.AppearanceHeader.Options.UseBackColor = true;
            this.TimeOutB11.Caption = "End STĐ";
            this.TimeOutB11.ColumnEdit = this.repoDate;
            this.TimeOutB11.FieldName = "TimeOutB11";
            this.TimeOutB11.Name = "TimeOutB11";
            this.TimeOutB11.Visible = true;
            this.TimeOutB11.VisibleIndex = 8;
            this.TimeOutB11.Width = 89;
            // 
            // HangDT
            // 
            this.HangDT.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.HangDT.AppearanceHeader.Options.UseBackColor = true;
            this.HangDT.Caption = "Hạng Đào Tạo";
            this.HangDT.FieldName = "HangDT";
            this.HangDT.Name = "HangDT";
            this.HangDT.Visible = true;
            this.HangDT.VisibleIndex = 3;
            // 
            // Cabin
            // 
            this.Cabin.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.Cabin.AppearanceHeader.Options.UseBackColor = true;
            this.Cabin.Caption = "Cabin";
            this.Cabin.Name = "Cabin";
            this.Cabin.Visible = true;
            this.Cabin.VisibleIndex = 9;
            this.Cabin.Width = 57;
            // 
            // StartCabin
            // 
            this.StartCabin.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.StartCabin.AppearanceHeader.Options.UseBackColor = true;
            this.StartCabin.Caption = "Start Cabin";
            this.StartCabin.Name = "StartCabin";
            this.StartCabin.Visible = true;
            this.StartCabin.VisibleIndex = 10;
            this.StartCabin.Width = 102;
            // 
            // EndCabin
            // 
            this.EndCabin.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.EndCabin.AppearanceHeader.Options.UseBackColor = true;
            this.EndCabin.Caption = "End Cabin";
            this.EndCabin.Name = "EndCabin";
            this.EndCabin.Visible = true;
            this.EndCabin.VisibleIndex = 11;
            this.EndCabin.Width = 82;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.panel2.Controls.Add(this.btn_Apdunglich);
            this.panel2.Controls.Add(this.dateEdit_End);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dateEdit_Start);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 723);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 63);
            this.panel2.TabIndex = 2;
            // 
            // btn_Apdunglich
            // 
            this.btn_Apdunglich.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Apdunglich.BackColor = System.Drawing.Color.White;
            this.btn_Apdunglich.FlatAppearance.BorderSize = 0;
            this.btn_Apdunglich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Apdunglich.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Apdunglich.Location = new System.Drawing.Point(819, 22);
            this.btn_Apdunglich.Name = "btn_Apdunglich";
            this.btn_Apdunglich.Size = new System.Drawing.Size(177, 23);
            this.btn_Apdunglich.TabIndex = 2;
            this.btn_Apdunglich.Text = "Áp dụng cho khóa học";
            this.btn_Apdunglich.UseVisualStyleBackColor = false;
            this.btn_Apdunglich.Click += new System.EventHandler(this.btn_Apdunglich_Click);
            // 
            // dateEdit_End
            // 
            this.dateEdit_End.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEdit_End.EditValue = null;
            this.dateEdit_End.Location = new System.Drawing.Point(624, 22);
            this.dateEdit_End.Name = "dateEdit_End";
            this.dateEdit_End.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEdit_End.Properties.Appearance.Options.UseFont = true;
            this.dateEdit_End.Properties.Appearance.Options.UseTextOptions = true;
            this.dateEdit_End.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateEdit_End.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dateEdit_End.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_End.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_End.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit_End.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit_End.Size = new System.Drawing.Size(156, 22);
            this.dateEdit_End.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(502, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ngày Kết Thúc DAT";
            // 
            // dateEdit_Start
            // 
            this.dateEdit_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEdit_Start.EditValue = null;
            this.dateEdit_Start.Location = new System.Drawing.Point(329, 22);
            this.dateEdit_Start.Name = "dateEdit_Start";
            this.dateEdit_Start.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEdit_Start.Properties.Appearance.Options.UseFont = true;
            this.dateEdit_Start.Properties.Appearance.Options.UseTextOptions = true;
            this.dateEdit_Start.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateEdit_Start.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dateEdit_Start.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_Start.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_Start.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit_Start.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit_Start.Properties.MaskSettings.Set("mask", "d");
            this.dateEdit_Start.Size = new System.Drawing.Size(156, 22);
            this.dateEdit_Start.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(210, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày Bắt Đầu DAT";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tableLayoutPanel.Controls.Add(this.windowsUIButtonPanel, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.gridCtrl_LichHoc, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.gridControl_XeSTD, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1480, 789);
            this.tableLayoutPanel.TabIndex = 3;
            // 
            // windowsUIButtonPanel
            // 
            this.windowsUIButtonPanel.AppearanceButton.Hovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.windowsUIButtonPanel.AppearanceButton.Hovered.FontSizeDelta = -1;
            this.windowsUIButtonPanel.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.windowsUIButtonPanel.AppearanceButton.Hovered.Options.UseBackColor = true;
            this.windowsUIButtonPanel.AppearanceButton.Hovered.Options.UseFont = true;
            this.windowsUIButtonPanel.AppearanceButton.Hovered.Options.UseForeColor = true;
            this.windowsUIButtonPanel.AppearanceButton.Normal.FontSizeDelta = -1;
            this.windowsUIButtonPanel.AppearanceButton.Normal.Options.UseFont = true;
            this.windowsUIButtonPanel.AppearanceButton.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(159)))));
            this.windowsUIButtonPanel.AppearanceButton.Pressed.FontSizeDelta = -1;
            this.windowsUIButtonPanel.AppearanceButton.Pressed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(159)))));
            this.windowsUIButtonPanel.AppearanceButton.Pressed.Options.UseBackColor = true;
            this.windowsUIButtonPanel.AppearanceButton.Pressed.Options.UseFont = true;
            this.windowsUIButtonPanel.AppearanceButton.Pressed.Options.UseForeColor = true;
            this.windowsUIButtonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            windowsUIButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("windowsUIButtonImageOptions1.Image")));
            windowsUIButtonImageOptions1.ImageUri.Uri = "Save;Size32x32;GrayScaled";
            windowsUIButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("windowsUIButtonImageOptions2.Image")));
            this.windowsUIButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Cập Nhật", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Sắp STD", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1, false)});
            this.windowsUIButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButtonPanel.EnableImageTransparency = true;
            this.windowsUIButtonPanel.ForeColor = System.Drawing.Color.White;
            this.windowsUIButtonPanel.Location = new System.Drawing.Point(1034, 725);
            this.windowsUIButtonPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButtonPanel.MaximumSize = new System.Drawing.Size(0, 60);
            this.windowsUIButtonPanel.MinimumSize = new System.Drawing.Size(60, 60);
            this.windowsUIButtonPanel.Name = "windowsUIButtonPanel";
            this.windowsUIButtonPanel.Size = new System.Drawing.Size(442, 60);
            this.windowsUIButtonPanel.TabIndex = 37;
            this.windowsUIButtonPanel.Text = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel.UseButtonBackgroundImages = false;
            // 
            // gridControl_XeSTD
            // 
            this.gridControl_XeSTD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_XeSTD.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl_XeSTD.Location = new System.Drawing.Point(1033, 3);
            this.gridControl_XeSTD.MainView = this.gridView_XeSTD;
            this.gridControl_XeSTD.Name = "gridControl_XeSTD";
            this.gridControl_XeSTD.Size = new System.Drawing.Size(444, 714);
            this.gridControl_XeSTD.TabIndex = 3;
            this.gridControl_XeSTD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_XeSTD});
            // 
            // gridView_XeSTD
            // 
            this.gridView_XeSTD.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_XeSTD.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_XeSTD.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_XeSTD.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_XeSTD.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_XeSTD.Appearance.Row.Options.UseFont = true;
            this.gridView_XeSTD.Appearance.Row.Options.UseTextOptions = true;
            this.gridView_XeSTD.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_XeSTD.ColumnPanelRowHeight = 32;
            this.gridView_XeSTD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IdXeTap,
            this.BienSoXe,
            this.MaXe,
            this.HangXe,
            this.SuDungDen});
            this.gridView_XeSTD.GridControl = this.gridControl_XeSTD;
            this.gridView_XeSTD.Name = "gridView_XeSTD";
            this.gridView_XeSTD.OptionsBehavior.Editable = false;
            this.gridView_XeSTD.OptionsSelection.CheckBoxSelectorColumnWidth = 45;
            this.gridView_XeSTD.OptionsSelection.MultiSelect = true;
            this.gridView_XeSTD.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView_XeSTD.OptionsView.ShowIndicator = false;
            this.gridView_XeSTD.RowHeight = 27;
            // 
            // IdXeTap
            // 
            this.IdXeTap.Caption = "IdXeTap";
            this.IdXeTap.Name = "IdXeTap";
            // 
            // BienSoXe
            // 
            this.BienSoXe.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.BienSoXe.AppearanceHeader.Options.UseBackColor = true;
            this.BienSoXe.Caption = "Biển Số Xe";
            this.BienSoXe.FieldName = "BienSoXe";
            this.BienSoXe.Name = "BienSoXe";
            this.BienSoXe.Visible = true;
            this.BienSoXe.VisibleIndex = 1;
            this.BienSoXe.Width = 101;
            // 
            // MaXe
            // 
            this.MaXe.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.MaXe.AppearanceHeader.Options.UseBackColor = true;
            this.MaXe.Caption = "Mã Xe";
            this.MaXe.FieldName = "MaXe";
            this.MaXe.Name = "MaXe";
            this.MaXe.Width = 93;
            // 
            // HangXe
            // 
            this.HangXe.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.HangXe.AppearanceHeader.Options.UseBackColor = true;
            this.HangXe.Caption = "Hạng Xe";
            this.HangXe.FieldName = "HangGPLX";
            this.HangXe.Name = "HangXe";
            this.HangXe.Visible = true;
            this.HangXe.VisibleIndex = 2;
            this.HangXe.Width = 74;
            // 
            // SuDungDen
            // 
            this.SuDungDen.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SuDungDen.AppearanceCell.Options.UseForeColor = true;
            this.SuDungDen.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.SuDungDen.AppearanceHeader.Options.UseBackColor = true;
            this.SuDungDen.Caption = "Sử Dụng Đến";
            this.SuDungDen.DisplayFormat.FormatString = "HH:mm - dd/MM/yyyy";
            this.SuDungDen.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.SuDungDen.FieldName = "SuDungDen";
            this.SuDungDen.Name = "SuDungDen";
            this.SuDungDen.Visible = true;
            this.SuDungDen.VisibleIndex = 3;
            this.SuDungDen.Width = 199;
            // 
            // frmPhanCongDaoTao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 789);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "frmPhanCongDaoTao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bố Trí Xe Tập Lái";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPhanCongDaoTao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_LichHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LichHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_XeTapLai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_XeSTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_Cabin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_XeSTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_XeSTD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Apdunglich;
        private DevExpress.XtraEditors.DateEdit dateEdit_End;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dateEdit_Start;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridCtrl_LichHoc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_LichHoc;
        private DevExpress.XtraGrid.Columns.GridColumn Id;
        private DevExpress.XtraGrid.Columns.GridColumn KhoaHocId;
        private DevExpress.XtraGrid.Columns.GridColumn MaKH;
        private DevExpress.XtraGrid.Columns.GridColumn TenKH;
        private DevExpress.XtraGrid.Columns.GridColumn TenKHGroup;
        private DevExpress.XtraGrid.Columns.GridColumn GiaoVienID;
        private DevExpress.XtraGrid.Columns.GridColumn GiaoVien;
        private DevExpress.XtraGrid.Columns.GridColumn ThoiDiemChayDAT;
        private DevExpress.XtraGrid.Columns.GridColumn ThoiDiemKetThucDAT;
        private DevExpress.XtraGrid.Columns.GridColumn MaXeTap;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repoGrid_XeTapLai;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn MaXeTapB11;
        private DevExpress.XtraGrid.Columns.GridColumn TimeInB11;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repoDate;
        private DevExpress.XtraGrid.Columns.GridColumn TimeOutB11;
        private DevExpress.XtraGrid.Columns.GridColumn HangDT;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repoGrid_Cabin;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn Status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private DevExpress.XtraGrid.Columns.GridColumn Cabin;
        private DevExpress.XtraGrid.Columns.GridColumn StartCabin;
        private DevExpress.XtraGrid.Columns.GridColumn EndCabin;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repoGrid_XeSTD;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControl_XeSTD;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_XeSTD;
        private DevExpress.XtraGrid.Columns.GridColumn BienSoXe;
        private DevExpress.XtraGrid.Columns.GridColumn MaXe;
        private DevExpress.XtraGrid.Columns.GridColumn HangXe;
        private DevExpress.XtraGrid.Columns.GridColumn SuDungDen;
        private DevExpress.XtraGrid.Columns.GridColumn IdXeTap;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButtonPanel;
    }
}