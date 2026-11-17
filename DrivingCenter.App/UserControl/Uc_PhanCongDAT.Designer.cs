namespace DrivingCenter.App.UserControl
{
    partial class Uc_PhanCongDAT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_PhanCongDAT));
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.btn_Assign = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Open = new DevExpress.XtraEditors.SimpleButton();
            this.cbb_KhoaHoc = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbl_Sheet = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Desc = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            this.pnl_Info = new DevExpress.XtraEditors.PanelControl();
            this.chk_ChiLoi = new DevExpress.XtraEditors.CheckEdit();
            this.lbl_Summary = new DevExpress.XtraEditors.LabelControl();
            this.grdCtrl_Assign = new DevExpress.XtraGrid.GridControl();
            this.gridView_Assign = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHocVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhoaHoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGiaoVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBienSo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThayDoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbb_KhoaHoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Info)).BeginInit();
            this.pnl_Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_ChiLoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrl_Assign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Assign)).BeginInit();
            this.SuspendLayout();
            //
            // pnl_Header
            //
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.btn_Assign);
            this.pnl_Header.Controls.Add(this.btn_Open);
            this.pnl_Header.Controls.Add(this.cbb_KhoaHoc);
            this.pnl_Header.Controls.Add(this.lbl_Sheet);
            this.pnl_Header.Controls.Add(this.lbl_Desc);
            this.pnl_Header.Controls.Add(this.lbl_Title);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(12, 12);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1476, 64);
            this.pnl_Header.TabIndex = 0;
            //
            // btn_Assign
            //
            this.btn_Assign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Assign.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.btn_Assign.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Assign.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Assign.Appearance.Options.UseBackColor = true;
            this.btn_Assign.Appearance.Options.UseFont = true;
            this.btn_Assign.Appearance.Options.UseForeColor = true;
            this.btn_Assign.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btn_Assign.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btn_Assign.AppearanceHovered.Options.UseBackColor = true;
            this.btn_Assign.AppearanceHovered.Options.UseForeColor = true;
            this.btn_Assign.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btn_Assign.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btn_Assign.AppearancePressed.Options.UseBackColor = true;
            this.btn_Assign.AppearancePressed.Options.UseForeColor = true;
            this.btn_Assign.Enabled = false;
            this.btn_Assign.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Assign.ImageOptions.SvgImage")));
            this.btn_Assign.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btn_Assign.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btn_Assign.Location = new System.Drawing.Point(1300, 16);
            this.btn_Assign.Name = "btn_Assign";
            this.btn_Assign.Size = new System.Drawing.Size(170, 32);
            this.btn_Assign.TabIndex = 5;
            this.btn_Assign.Text = "Áp dụng phân công";
            this.btn_Assign.ToolTip = "Gán giáo viên và xe cho các dòng hợp lệ của sheet đang chọn";
            this.btn_Assign.Click += new System.EventHandler(this.btn_Assign_Click);
            //
            // btn_Open
            //
            this.btn_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Open.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Open.Appearance.Options.UseFont = true;
            this.btn_Open.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Open.ImageOptions.SvgImage")));
            this.btn_Open.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btn_Open.Location = new System.Drawing.Point(1160, 16);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(134, 32);
            this.btn_Open.TabIndex = 4;
            this.btn_Open.Text = "Mở file Excel...";
            this.btn_Open.ToolTip = "Mỗi sheet là một khóa; dòng đầu là tiêu đề cột GiaoVien, MaDK, BienSoXe (Ctrl+O)";
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            //
            // cbb_KhoaHoc
            //
            this.cbb_KhoaHoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbb_KhoaHoc.Enabled = false;
            this.cbb_KhoaHoc.Location = new System.Drawing.Point(934, 16);
            this.cbb_KhoaHoc.Name = "cbb_KhoaHoc";
            this.cbb_KhoaHoc.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.cbb_KhoaHoc.Properties.Appearance.Options.UseFont = true;
            this.cbb_KhoaHoc.Properties.AutoHeight = false;
            this.cbb_KhoaHoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbb_KhoaHoc.Properties.NullText = "Chưa mở file";
            this.cbb_KhoaHoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbb_KhoaHoc.Size = new System.Drawing.Size(220, 32);
            this.cbb_KhoaHoc.TabIndex = 3;
            this.cbb_KhoaHoc.SelectedIndexChanged += new System.EventHandler(this.cbb_KhoaHoc_SelectedIndexChanged);
            //
            // lbl_Sheet
            //
            this.lbl_Sheet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Sheet.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Sheet.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Sheet.Appearance.Options.UseFont = true;
            this.lbl_Sheet.Appearance.Options.UseForeColor = true;
            this.lbl_Sheet.Location = new System.Drawing.Point(868, 24);
            this.lbl_Sheet.Name = "lbl_Sheet";
            this.lbl_Sheet.Size = new System.Drawing.Size(60, 16);
            this.lbl_Sheet.TabIndex = 2;
            this.lbl_Sheet.Text = "Sheet khóa";
            //
            // lbl_Desc
            //
            this.lbl_Desc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Desc.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Desc.Appearance.Options.UseFont = true;
            this.lbl_Desc.Appearance.Options.UseForeColor = true;
            this.lbl_Desc.Location = new System.Drawing.Point(16, 38);
            this.lbl_Desc.Name = "lbl_Desc";
            this.lbl_Desc.Size = new System.Drawing.Size(640, 16);
            this.lbl_Desc.TabIndex = 1;
            this.lbl_Desc.Text = "Mở file Excel phân công, kiểm tra từng dòng (học viên, giáo viên, xe) rồi áp dụng vào hệ thống DAT.";
            //
            // lbl_Title
            //
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Appearance.Options.UseForeColor = true;
            this.lbl_Title.Location = new System.Drawing.Point(16, 8);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(360, 25);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "Phân công giáo viên và xe (DAT)";
            //
            // pnl_Info
            //
            this.pnl_Info.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Info.Controls.Add(this.chk_ChiLoi);
            this.pnl_Info.Controls.Add(this.lbl_Summary);
            this.pnl_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Info.Location = new System.Drawing.Point(12, 76);
            this.pnl_Info.Name = "pnl_Info";
            this.pnl_Info.Size = new System.Drawing.Size(1476, 44);
            this.pnl_Info.TabIndex = 1;
            //
            // chk_ChiLoi
            //
            this.chk_ChiLoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_ChiLoi.Location = new System.Drawing.Point(1250, 12);
            this.chk_ChiLoi.Name = "chk_ChiLoi";
            this.chk_ChiLoi.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.chk_ChiLoi.Properties.Appearance.Options.UseFont = true;
            this.chk_ChiLoi.Properties.Caption = "Chỉ hiện dòng cần xem lại";
            this.chk_ChiLoi.Size = new System.Drawing.Size(220, 22);
            this.chk_ChiLoi.TabIndex = 1;
            this.chk_ChiLoi.CheckedChanged += new System.EventHandler(this.chk_ChiLoi_CheckedChanged);
            //
            // lbl_Summary
            //
            this.lbl_Summary.AllowHtmlString = true;
            this.lbl_Summary.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Summary.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Summary.Appearance.Options.UseFont = true;
            this.lbl_Summary.Appearance.Options.UseForeColor = true;
            this.lbl_Summary.Location = new System.Drawing.Point(16, 14);
            this.lbl_Summary.Name = "lbl_Summary";
            this.lbl_Summary.Size = new System.Drawing.Size(500, 16);
            this.lbl_Summary.TabIndex = 0;
            this.lbl_Summary.Text = "Mở file Excel để bắt đầu. Mỗi sheet là một khóa với các cột <b>GiaoVien</b>, <b>MaDK</b>, <b>BienSoXe</b>.";
            //
            // grdCtrl_Assign
            //
            this.grdCtrl_Assign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCtrl_Assign.Location = new System.Drawing.Point(12, 120);
            this.grdCtrl_Assign.MainView = this.gridView_Assign;
            this.grdCtrl_Assign.Name = "grdCtrl_Assign";
            this.grdCtrl_Assign.Size = new System.Drawing.Size(1476, 668);
            this.grdCtrl_Assign.TabIndex = 2;
            this.grdCtrl_Assign.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Assign});
            //
            // gridView_Assign
            //
            this.gridView_Assign.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_Assign.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_Assign.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_Assign.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView_Assign.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_Assign.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_Assign.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_Assign.Appearance.Row.Options.UseFont = true;
            this.gridView_Assign.ColumnPanelRowHeight = 36;
            this.gridView_Assign.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDK,
            this.colHocVien,
            this.colKhoaHoc,
            this.colGiaoVien,
            this.colBienSo,
            this.colThayDoi,
            this.colTrangThai});
            this.gridView_Assign.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView_Assign.GridControl = this.grdCtrl_Assign;
            this.gridView_Assign.IndicatorWidth = 45;
            this.gridView_Assign.Name = "gridView_Assign";
            this.gridView_Assign.OptionsBehavior.Editable = false;
            this.gridView_Assign.OptionsFind.AlwaysVisible = true;
            this.gridView_Assign.OptionsFind.FindNullPrompt = "Tìm mã ĐK, học viên, giáo viên, biển số...";
            this.gridView_Assign.OptionsFind.ShowClearButton = false;
            this.gridView_Assign.OptionsFind.ShowFindButton = false;
            this.gridView_Assign.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView_Assign.OptionsView.EnableAppearanceEvenRow = false;
            this.gridView_Assign.OptionsView.ShowGroupPanel = false;
            this.gridView_Assign.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView_Assign.RowHeight = 30;
            this.gridView_Assign.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_Assign_RowCellStyle);
            this.gridView_Assign.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView_Assign_CustomDrawEmptyForeground);
            this.gridView_Assign.CustomRowFilter += new DevExpress.XtraGrid.Views.Base.RowFilterEventHandler(this.gridView_Assign_CustomRowFilter);
            //
            // colMaDK
            //
            this.colMaDK.Caption = "Mã đăng ký";
            this.colMaDK.FieldName = "MaDK";
            this.colMaDK.Name = "colMaDK";
            this.colMaDK.Visible = true;
            this.colMaDK.VisibleIndex = 0;
            this.colMaDK.Width = 160;
            //
            // colHocVien
            //
            this.colHocVien.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.colHocVien.AppearanceCell.Options.UseFont = true;
            this.colHocVien.Caption = "Học viên (DAT)";
            this.colHocVien.FieldName = "HocVien";
            this.colHocVien.Name = "colHocVien";
            this.colHocVien.Visible = true;
            this.colHocVien.VisibleIndex = 1;
            this.colHocVien.Width = 170;
            //
            // colKhoaHoc
            //
            this.colKhoaHoc.AppearanceCell.Options.UseTextOptions = true;
            this.colKhoaHoc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKhoaHoc.Caption = "Khóa";
            this.colKhoaHoc.FieldName = "KhoaHoc";
            this.colKhoaHoc.Name = "colKhoaHoc";
            this.colKhoaHoc.Visible = true;
            this.colKhoaHoc.VisibleIndex = 2;
            this.colKhoaHoc.Width = 100;
            //
            // colGiaoVien
            //
            this.colGiaoVien.Caption = "Giáo viên (file)";
            this.colGiaoVien.FieldName = "GiaoVien";
            this.colGiaoVien.Name = "colGiaoVien";
            this.colGiaoVien.Visible = true;
            this.colGiaoVien.VisibleIndex = 3;
            this.colGiaoVien.Width = 160;
            //
            // colBienSo
            //
            this.colBienSo.AppearanceCell.Options.UseTextOptions = true;
            this.colBienSo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBienSo.Caption = "Biển số (file)";
            this.colBienSo.FieldName = "BienSoXe";
            this.colBienSo.Name = "colBienSo";
            this.colBienSo.Visible = true;
            this.colBienSo.VisibleIndex = 4;
            this.colBienSo.Width = 100;
            //
            // colThayDoi
            //
            this.colThayDoi.Caption = "Thay đổi so với hiện tại";
            this.colThayDoi.FieldName = "ThayDoi";
            this.colThayDoi.Name = "colThayDoi";
            this.colThayDoi.Visible = true;
            this.colThayDoi.VisibleIndex = 5;
            this.colThayDoi.Width = 300;
            //
            // colTrangThai
            //
            this.colTrangThai.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.colTrangThai.AppearanceCell.Options.UseFont = true;
            this.colTrangThai.Caption = "Kiểm tra";
            this.colTrangThai.FieldName = "TrangThai";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 6;
            this.colTrangThai.Width = 220;
            //
            // Uc_PhanCongDAT
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdCtrl_Assign);
            this.Controls.Add(this.pnl_Info);
            this.Controls.Add(this.pnl_Header);
            this.Name = "Uc_PhanCongDAT";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(1500, 800);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbb_KhoaHoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Info)).EndInit();
            this.pnl_Info.ResumeLayout(false);
            this.pnl_Info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_ChiLoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrl_Assign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Assign)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.LabelControl lbl_Desc;
        private DevExpress.XtraEditors.LabelControl lbl_Sheet;
        private DevExpress.XtraEditors.ComboBoxEdit cbb_KhoaHoc;
        private DevExpress.XtraEditors.SimpleButton btn_Open;
        private DevExpress.XtraEditors.SimpleButton btn_Assign;
        private DevExpress.XtraEditors.PanelControl pnl_Info;
        private DevExpress.XtraEditors.LabelControl lbl_Summary;
        private DevExpress.XtraEditors.CheckEdit chk_ChiLoi;
        private DevExpress.XtraGrid.GridControl grdCtrl_Assign;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Assign;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDK;
        private DevExpress.XtraGrid.Columns.GridColumn colHocVien;
        private DevExpress.XtraGrid.Columns.GridColumn colKhoaHoc;
        private DevExpress.XtraGrid.Columns.GridColumn colGiaoVien;
        private DevExpress.XtraGrid.Columns.GridColumn colBienSo;
        private DevExpress.XtraGrid.Columns.GridColumn colThayDoi;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
    }
}
