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
            this.tblLayout_LichHoc = new System.Windows.Forms.TableLayoutPanel();
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
            this.TimeInB11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.TimeOutB11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HangDT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoGrid_Cabin = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Apdunglich = new System.Windows.Forms.Button();
            this.dateEdit_End = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.dateEdit_Start = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tblLayout_LichHoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_LichHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LichHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_XeTapLai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_Cabin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_Start.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tblLayout_LichHoc
            // 
            this.tblLayout_LichHoc.ColumnCount = 1;
            this.tblLayout_LichHoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout_LichHoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLayout_LichHoc.Controls.Add(this.gridCtrl_LichHoc, 0, 1);
            this.tblLayout_LichHoc.Controls.Add(this.panel2, 0, 0);
            this.tblLayout_LichHoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayout_LichHoc.Location = new System.Drawing.Point(0, 0);
            this.tblLayout_LichHoc.Name = "tblLayout_LichHoc";
            this.tblLayout_LichHoc.RowCount = 2;
            this.tblLayout_LichHoc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLayout_LichHoc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout_LichHoc.Size = new System.Drawing.Size(1383, 747);
            this.tblLayout_LichHoc.TabIndex = 2;
            // 
            // gridCtrl_LichHoc
            // 
            this.gridCtrl_LichHoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrl_LichHoc.Location = new System.Drawing.Point(3, 53);
            this.gridCtrl_LichHoc.MainView = this.gridView_LichHoc;
            this.gridCtrl_LichHoc.Name = "gridCtrl_LichHoc";
            this.gridCtrl_LichHoc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoGrid_XeTapLai,
            this.repoDate,
            this.repoGrid_Cabin});
            this.gridCtrl_LichHoc.Size = new System.Drawing.Size(1377, 691);
            this.gridCtrl_LichHoc.TabIndex = 0;
            this.gridCtrl_LichHoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_LichHoc});
            // 
            // gridView_LichHoc
            // 
            this.gridView_LichHoc.Appearance.GroupRow.BackColor = System.Drawing.Color.SteelBlue;
            this.gridView_LichHoc.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_LichHoc.Appearance.GroupRow.ForeColor = System.Drawing.Color.White;
            this.gridView_LichHoc.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView_LichHoc.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_LichHoc.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_LichHoc.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_LichHoc.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_LichHoc.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_LichHoc.Appearance.Row.Options.UseFont = true;
            this.gridView_LichHoc.ColumnPanelRowHeight = 27;
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
            this.Status});
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
            this.gridView_LichHoc.Name = "gridView_LichHoc";
            this.gridView_LichHoc.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView_LichHoc.OptionsView.ShowGroupPanelColumnsAsSingleRow = true;
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
            this.MaKH.Caption = "Mã Khóa Học";
            this.MaKH.FieldName = "MaKH";
            this.MaKH.Name = "MaKH";
            this.MaKH.Visible = true;
            this.MaKH.VisibleIndex = 0;
            this.MaKH.Width = 117;
            // 
            // TenKH
            // 
            this.TenKH.Caption = "Khoá Học";
            this.TenKH.FieldName = "TenKH";
            this.TenKH.Name = "TenKH";
            this.TenKH.Visible = true;
            this.TenKH.VisibleIndex = 2;
            // 
            // TenKHGroup
            // 
            this.TenKHGroup.AppearanceCell.Options.UseTextOptions = true;
            this.TenKHGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TenKHGroup.Caption = "Khóa Học";
            this.TenKHGroup.FieldName = "TenKH";
            this.TenKHGroup.Name = "TenKHGroup";
            this.TenKHGroup.Visible = true;
            this.TenKHGroup.VisibleIndex = 1;
            this.TenKHGroup.Width = 87;
            // 
            // GiaoVienID
            // 
            this.GiaoVienID.Caption = "GiaoVienID";
            this.GiaoVienID.FieldName = "GiaoVienID";
            this.GiaoVienID.Name = "GiaoVienID";
            // 
            // GiaoVien
            // 
            this.GiaoVien.Caption = "Giáo Viên";
            this.GiaoVien.FieldName = "GiaoVien";
            this.GiaoVien.Name = "GiaoVien";
            this.GiaoVien.Visible = true;
            this.GiaoVien.VisibleIndex = 2;
            this.GiaoVien.Width = 207;
            // 
            // ThoiDiemChayDAT
            // 
            this.ThoiDiemChayDAT.AppearanceCell.Options.UseTextOptions = true;
            this.ThoiDiemChayDAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ThoiDiemChayDAT.Caption = "Ngày Bắt Đầu DAT";
            this.ThoiDiemChayDAT.FieldName = "ThoiDiemChayDAT";
            this.ThoiDiemChayDAT.Name = "ThoiDiemChayDAT";
            this.ThoiDiemChayDAT.Visible = true;
            this.ThoiDiemChayDAT.VisibleIndex = 3;
            this.ThoiDiemChayDAT.Width = 126;
            // 
            // ThoiDiemKetThucDAT
            // 
            this.ThoiDiemKetThucDAT.AppearanceCell.Options.UseTextOptions = true;
            this.ThoiDiemKetThucDAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ThoiDiemKetThucDAT.Caption = "Ngày Kết Thúc DAT";
            this.ThoiDiemKetThucDAT.FieldName = "ThoiDiemKetThucDAT";
            this.ThoiDiemKetThucDAT.Name = "ThoiDiemKetThucDAT";
            this.ThoiDiemKetThucDAT.Visible = true;
            this.ThoiDiemKetThucDAT.VisibleIndex = 4;
            this.ThoiDiemKetThucDAT.Width = 126;
            // 
            // MaXeTap
            // 
            this.MaXeTap.AppearanceCell.Options.UseTextOptions = true;
            this.MaXeTap.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaXeTap.Caption = "Xe Tập Lái";
            this.MaXeTap.ColumnEdit = this.repoGrid_XeTapLai;
            this.MaXeTap.FieldName = "MaXeTap";
            this.MaXeTap.Name = "MaXeTap";
            this.MaXeTap.Visible = true;
            this.MaXeTap.VisibleIndex = 5;
            this.MaXeTap.Width = 93;
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
            this.MaXeTapB11.Caption = "Xe Số Tự Động";
            this.MaXeTapB11.ColumnEdit = this.repoGrid_XeTapLai;
            this.MaXeTapB11.FieldName = "MaXeTapB11";
            this.MaXeTapB11.Name = "MaXeTapB11";
            this.MaXeTapB11.Visible = true;
            this.MaXeTapB11.VisibleIndex = 6;
            this.MaXeTapB11.Width = 93;
            // 
            // TimeInB11
            // 
            this.TimeInB11.AppearanceCell.Options.UseTextOptions = true;
            this.TimeInB11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TimeInB11.Caption = "Thời Gian Bắt Đầu STĐ";
            this.TimeInB11.ColumnEdit = this.repoDate;
            this.TimeInB11.FieldName = "TimeInB11";
            this.TimeInB11.Name = "TimeInB11";
            this.TimeInB11.Visible = true;
            this.TimeInB11.VisibleIndex = 7;
            this.TimeInB11.Width = 157;
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
            this.TimeOutB11.Caption = "Thời Gian Kết Thúc STĐ";
            this.TimeOutB11.ColumnEdit = this.repoDate;
            this.TimeOutB11.FieldName = "TimeOutB11";
            this.TimeOutB11.Name = "TimeOutB11";
            this.TimeOutB11.Visible = true;
            this.TimeOutB11.VisibleIndex = 8;
            this.TimeOutB11.Width = 168;
            // 
            // HangDT
            // 
            this.HangDT.Caption = "Hạng Đào Tạo";
            this.HangDT.FieldName = "HangDT";
            this.HangDT.Name = "HangDT";
            this.HangDT.Visible = true;
            this.HangDT.VisibleIndex = 3;
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
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1377, 44);
            this.panel2.TabIndex = 2;
            // 
            // btn_Apdunglich
            // 
            this.btn_Apdunglich.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Apdunglich.BackColor = System.Drawing.Color.White;
            this.btn_Apdunglich.FlatAppearance.BorderSize = 0;
            this.btn_Apdunglich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Apdunglich.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Apdunglich.Location = new System.Drawing.Point(1184, 11);
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
            this.dateEdit_End.Location = new System.Drawing.Point(989, 11);
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
            this.label2.Location = new System.Drawing.Point(867, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ngày Kết Thúc DAT";
            // 
            // dateEdit_Start
            // 
            this.dateEdit_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEdit_Start.EditValue = null;
            this.dateEdit_Start.Location = new System.Drawing.Point(694, 11);
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
            this.label1.Location = new System.Drawing.Point(575, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày Bắt Đầu DAT";
            // 
            // Status
            // 
            this.Status.Caption = "Check";
            this.Status.FieldName = "Status";
            this.Status.Name = "Status";
            // 
            // frmPhanCongDaoTao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 747);
            this.Controls.Add(this.tblLayout_LichHoc);
            this.Name = "frmPhanCongDaoTao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bố Trí Xe Tập Lái";
            this.Load += new System.EventHandler(this.frmPhanCongDaoTao_Load);
            this.tblLayout_LichHoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_LichHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LichHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoGrid_XeTapLai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLayout_LichHoc;
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
    }
}