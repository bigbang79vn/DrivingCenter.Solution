namespace DrivingCenter.App
{
    partial class Uc_HocVien
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.grid_Result = new DevExpress.XtraGrid.GridControl();
            this.gridView_Result = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.STT_V = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_MaDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_HoTenGV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_HoVaTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_HangDaoTao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_TongQuangDuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_QuangDuongPhienGuiTC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_TongThoiGian = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_ThoiGianPhienGuiTC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_TongQuangDuong_B11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_TongThoiGian_B11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_TongQuangDuong_PhienDem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_TongThoiGian_PhienDem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_SoNgayHoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.View_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Result)).BeginInit();
            this.SuspendLayout();
            // 
            // grid_Result
            // 
            this.grid_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_Result.Location = new System.Drawing.Point(0, 0);
            this.grid_Result.MainView = this.gridView_Result;
            this.grid_Result.Name = "grid_Result";
            this.grid_Result.Size = new System.Drawing.Size(1500, 800);
            this.grid_Result.TabIndex = 3;
            this.grid_Result.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Result});
            // 
            // gridView_Result
            // 
            this.gridView_Result.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_Result.Appearance.GroupRow.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.gridView_Result.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_Result.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView_Result.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_Result.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_Result.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_Result.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_Result.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_Result.Appearance.Row.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.gridView_Result.Appearance.Row.Options.UseFont = true;
            this.gridView_Result.Appearance.Row.Options.UseForeColor = true;
            this.gridView_Result.Appearance.Row.Options.UseTextOptions = true;
            this.gridView_Result.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_Result.ColumnPanelRowHeight = 28;
            this.gridView_Result.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.STT_V,
            this.View_MaDK,
            this.View_HoTenGV,
            this.View_HoVaTen,
            this.View_HangDaoTao,
            this.View_TongQuangDuong,
            this.View_QuangDuongPhienGuiTC,
            this.View_TongThoiGian,
            this.View_ThoiGianPhienGuiTC,
            this.View_TongQuangDuong_B11,
            this.View_TongThoiGian_B11,
            this.View_TongQuangDuong_PhienDem,
            this.View_TongThoiGian_PhienDem,
            this.View_SoNgayHoc,
            this.View_Status});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.View_Status;
            gridFormatRule1.Name = "KiemTra_DDK";
            formatConditionRuleValue1.Appearance.Font = new System.Drawing.Font("Roboto Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            formatConditionRuleValue1.Appearance.Options.UseFont = true;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = "Đủ điều kiện!";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gridView_Result.FormatRules.Add(gridFormatRule1);
            this.gridView_Result.GridControl = this.grid_Result;
            this.gridView_Result.GroupCount = 1;
            this.gridView_Result.GroupRowHeight = 28;
            this.gridView_Result.Name = "gridView_Result";
            this.gridView_Result.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView_Result.OptionsBehavior.Editable = false;
            this.gridView_Result.OptionsView.ShowIndicator = false;
            this.gridView_Result.RowHeight = 28;
            this.gridView_Result.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.View_HoTenGV, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // STT_V
            // 
            this.STT_V.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.STT_V.AppearanceCell.Options.UseBackColor = true;
            this.STT_V.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.STT_V.AppearanceHeader.Options.UseBackColor = true;
            this.STT_V.Caption = "STT";
            this.STT_V.FieldName = "STT";
            this.STT_V.Name = "STT_V";
            this.STT_V.Visible = true;
            this.STT_V.VisibleIndex = 0;
            this.STT_V.Width = 35;
            // 
            // View_MaDK
            // 
            this.View_MaDK.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.View_MaDK.AppearanceCell.BackColor2 = System.Drawing.Color.White;
            this.View_MaDK.AppearanceCell.BorderColor = System.Drawing.Color.White;
            this.View_MaDK.AppearanceCell.Options.UseBackColor = true;
            this.View_MaDK.AppearanceCell.Options.UseBorderColor = true;
            this.View_MaDK.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_MaDK.AppearanceHeader.Options.UseBackColor = true;
            this.View_MaDK.Caption = "Mã Đăng Ký";
            this.View_MaDK.FieldName = "View_MaDK";
            this.View_MaDK.Name = "View_MaDK";
            this.View_MaDK.Visible = true;
            this.View_MaDK.VisibleIndex = 1;
            this.View_MaDK.Width = 172;
            // 
            // View_HoTenGV
            // 
            this.View_HoTenGV.Caption = "Giáo Viên";
            this.View_HoTenGV.FieldName = "View_HoTenGV";
            this.View_HoTenGV.Name = "View_HoTenGV";
            this.View_HoTenGV.Visible = true;
            this.View_HoTenGV.VisibleIndex = 3;
            // 
            // View_HoVaTen
            // 
            this.View_HoVaTen.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_HoVaTen.AppearanceHeader.Options.UseBackColor = true;
            this.View_HoVaTen.Caption = "Học viên";
            this.View_HoVaTen.FieldName = "View_HoVaTen";
            this.View_HoVaTen.Name = "View_HoVaTen";
            this.View_HoVaTen.Visible = true;
            this.View_HoVaTen.VisibleIndex = 2;
            this.View_HoVaTen.Width = 159;
            // 
            // View_HangDaoTao
            // 
            this.View_HangDaoTao.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_HangDaoTao.AppearanceHeader.Options.UseBackColor = true;
            this.View_HangDaoTao.Caption = "Hạng";
            this.View_HangDaoTao.FieldName = "View_HangDaoTao";
            this.View_HangDaoTao.Name = "View_HangDaoTao";
            this.View_HangDaoTao.Visible = true;
            this.View_HangDaoTao.VisibleIndex = 3;
            this.View_HangDaoTao.Width = 74;
            // 
            // View_TongQuangDuong
            // 
            this.View_TongQuangDuong.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(214)))), ((int)(((byte)(228)))));
            this.View_TongQuangDuong.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.View_TongQuangDuong.AppearanceCell.Options.UseBackColor = true;
            this.View_TongQuangDuong.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_TongQuangDuong.AppearanceHeader.Options.UseBackColor = true;
            this.View_TongQuangDuong.Caption = "Quãng đường (Km)";
            this.View_TongQuangDuong.FieldName = "View_TongQuangDuong";
            this.View_TongQuangDuong.Name = "View_TongQuangDuong";
            this.View_TongQuangDuong.Visible = true;
            this.View_TongQuangDuong.VisibleIndex = 4;
            this.View_TongQuangDuong.Width = 116;
            // 
            // View_QuangDuongPhienGuiTC
            // 
            this.View_QuangDuongPhienGuiTC.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_QuangDuongPhienGuiTC.AppearanceHeader.Options.UseBackColor = true;
            this.View_QuangDuongPhienGuiTC.Caption = "Quãng đường TC";
            this.View_QuangDuongPhienGuiTC.FieldName = "View_QuangDuongPhienGuiTC";
            this.View_QuangDuongPhienGuiTC.Name = "View_QuangDuongPhienGuiTC";
            this.View_QuangDuongPhienGuiTC.Width = 94;
            // 
            // View_TongThoiGian
            // 
            this.View_TongThoiGian.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.View_TongThoiGian.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.View_TongThoiGian.AppearanceCell.Options.UseBackColor = true;
            this.View_TongThoiGian.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_TongThoiGian.AppearanceHeader.Options.UseBackColor = true;
            this.View_TongThoiGian.Caption = "Thời gian (Giờ: Phút)";
            this.View_TongThoiGian.FieldName = "View_TongThoiGian";
            this.View_TongThoiGian.Name = "View_TongThoiGian";
            this.View_TongThoiGian.Visible = true;
            this.View_TongThoiGian.VisibleIndex = 5;
            this.View_TongThoiGian.Width = 122;
            // 
            // View_ThoiGianPhienGuiTC
            // 
            this.View_ThoiGianPhienGuiTC.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_ThoiGianPhienGuiTC.AppearanceHeader.Options.UseBackColor = true;
            this.View_ThoiGianPhienGuiTC.Caption = "Thời gian TC";
            this.View_ThoiGianPhienGuiTC.FieldName = "View_ThoiGianPhienGuiTC";
            this.View_ThoiGianPhienGuiTC.Name = "View_ThoiGianPhienGuiTC";
            this.View_ThoiGianPhienGuiTC.Width = 62;
            // 
            // View_TongQuangDuong_B11
            // 
            this.View_TongQuangDuong_B11.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.View_TongQuangDuong_B11.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.View_TongQuangDuong_B11.AppearanceCell.Options.UseBackColor = true;
            this.View_TongQuangDuong_B11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_TongQuangDuong_B11.AppearanceHeader.Options.UseBackColor = true;
            this.View_TongQuangDuong_B11.Caption = "Quãng đường B11";
            this.View_TongQuangDuong_B11.FieldName = "View_TongQuangDuong_B11";
            this.View_TongQuangDuong_B11.Name = "View_TongQuangDuong_B11";
            this.View_TongQuangDuong_B11.Visible = true;
            this.View_TongQuangDuong_B11.VisibleIndex = 6;
            this.View_TongQuangDuong_B11.Width = 116;
            // 
            // View_TongThoiGian_B11
            // 
            this.View_TongThoiGian_B11.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.View_TongThoiGian_B11.AppearanceCell.Options.UseBackColor = true;
            this.View_TongThoiGian_B11.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_TongThoiGian_B11.AppearanceHeader.Options.UseBackColor = true;
            this.View_TongThoiGian_B11.Caption = "Thời gian B11";
            this.View_TongThoiGian_B11.FieldName = "View_TongThoiGian_B11";
            this.View_TongThoiGian_B11.Name = "View_TongThoiGian_B11";
            this.View_TongThoiGian_B11.Visible = true;
            this.View_TongThoiGian_B11.VisibleIndex = 7;
            this.View_TongThoiGian_B11.Width = 116;
            // 
            // View_TongQuangDuong_PhienDem
            // 
            this.View_TongQuangDuong_PhienDem.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(214)))), ((int)(((byte)(228)))));
            this.View_TongQuangDuong_PhienDem.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.View_TongQuangDuong_PhienDem.AppearanceCell.Options.UseBackColor = true;
            this.View_TongQuangDuong_PhienDem.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_TongQuangDuong_PhienDem.AppearanceHeader.Options.UseBackColor = true;
            this.View_TongQuangDuong_PhienDem.Caption = "Quãng đường BĐ";
            this.View_TongQuangDuong_PhienDem.FieldName = "View_TongQuangDuong_PhienDem";
            this.View_TongQuangDuong_PhienDem.Name = "View_TongQuangDuong_PhienDem";
            this.View_TongQuangDuong_PhienDem.Visible = true;
            this.View_TongQuangDuong_PhienDem.VisibleIndex = 8;
            this.View_TongQuangDuong_PhienDem.Width = 104;
            // 
            // View_TongThoiGian_PhienDem
            // 
            this.View_TongThoiGian_PhienDem.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.View_TongThoiGian_PhienDem.AppearanceCell.BackColor2 = System.Drawing.Color.White;
            this.View_TongThoiGian_PhienDem.AppearanceCell.Options.UseBackColor = true;
            this.View_TongThoiGian_PhienDem.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_TongThoiGian_PhienDem.AppearanceHeader.Options.UseBackColor = true;
            this.View_TongThoiGian_PhienDem.Caption = "Thời gian BĐ";
            this.View_TongThoiGian_PhienDem.FieldName = "View_TongThoiGian_PhienDem";
            this.View_TongThoiGian_PhienDem.Name = "View_TongThoiGian_PhienDem";
            this.View_TongThoiGian_PhienDem.Visible = true;
            this.View_TongThoiGian_PhienDem.VisibleIndex = 9;
            this.View_TongThoiGian_PhienDem.Width = 98;
            // 
            // View_SoNgayHoc
            // 
            this.View_SoNgayHoc.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_SoNgayHoc.AppearanceHeader.Options.UseBackColor = true;
            this.View_SoNgayHoc.Caption = "Số ngày";
            this.View_SoNgayHoc.FieldName = "View_SoNgayHoc";
            this.View_SoNgayHoc.Name = "View_SoNgayHoc";
            this.View_SoNgayHoc.Visible = true;
            this.View_SoNgayHoc.VisibleIndex = 10;
            this.View_SoNgayHoc.Width = 64;
            // 
            // View_Status
            // 
            this.View_Status.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.View_Status.AppearanceHeader.Options.UseBackColor = true;
            this.View_Status.Caption = "Kết luận";
            this.View_Status.FieldName = "View_Status";
            this.View_Status.Name = "View_Status";
            this.View_Status.Visible = true;
            this.View_Status.VisibleIndex = 11;
            this.View_Status.Width = 133;
            // 
            // Uc_HocVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid_Result);
            this.Name = "Uc_HocVien";
            this.Size = new System.Drawing.Size(1500, 800);
            ((System.ComponentModel.ISupportInitialize)(this.grid_Result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid_Result;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Result;
        private DevExpress.XtraGrid.Columns.GridColumn STT_V;
        private DevExpress.XtraGrid.Columns.GridColumn View_MaDK;
        private DevExpress.XtraGrid.Columns.GridColumn View_HoTenGV;
        private DevExpress.XtraGrid.Columns.GridColumn View_HoVaTen;
        private DevExpress.XtraGrid.Columns.GridColumn View_HangDaoTao;
        private DevExpress.XtraGrid.Columns.GridColumn View_TongQuangDuong;
        private DevExpress.XtraGrid.Columns.GridColumn View_QuangDuongPhienGuiTC;
        private DevExpress.XtraGrid.Columns.GridColumn View_TongThoiGian;
        private DevExpress.XtraGrid.Columns.GridColumn View_ThoiGianPhienGuiTC;
        private DevExpress.XtraGrid.Columns.GridColumn View_TongQuangDuong_B11;
        private DevExpress.XtraGrid.Columns.GridColumn View_TongThoiGian_B11;
        private DevExpress.XtraGrid.Columns.GridColumn View_TongQuangDuong_PhienDem;
        private DevExpress.XtraGrid.Columns.GridColumn View_TongThoiGian_PhienDem;
        private DevExpress.XtraGrid.Columns.GridColumn View_SoNgayHoc;
        private DevExpress.XtraGrid.Columns.GridColumn View_Status;
    }
}
