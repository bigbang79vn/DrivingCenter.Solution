namespace DrivingCenter.App.UserControl
{
    partial class Uc_Print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_Print));
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.lbl_Desc = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            this.pnl_Filter = new DevExpress.XtraEditors.PanelControl();
            this.lbl_Summary = new DevExpress.XtraEditors.LabelControl();
            this.btn_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.sle_KhoaHoc = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.view_KhoaHoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_KH_MaKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_KH_TenKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_KH_Hang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_KH_NgayKG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_KH_NgayBG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lbl_KhoaHoc = new DevExpress.XtraEditors.LabelControl();
            this.pnl_Actions = new DevExpress.XtraEditors.PanelControl();
            this.lbl_Hint = new DevExpress.XtraEditors.LabelControl();
            this.btn_BoChon = new DevExpress.XtraEditors.SimpleButton();
            this.btn_InTem = new DevExpress.XtraEditors.SimpleButton();
            this.btn_InPhieu = new DevExpress.XtraEditors.SimpleButton();
            this.btn_InThe = new DevExpress.XtraEditors.SimpleButton();
            this.grd_lstHocVien = new DevExpress.XtraGrid.GridControl();
            this.gridView_lstHocVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_NguoiLxID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_MaDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_HoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_NgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_GioiTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_SoCMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_NgayCapCMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_DiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_GiaoVien = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Filter)).BeginInit();
            this.pnl_Filter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sle_KhoaHoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view_KhoaHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Actions)).BeginInit();
            this.pnl_Actions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_lstHocVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_lstHocVien)).BeginInit();
            this.SuspendLayout();
            //
            // pnl_Header
            //
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.lbl_Desc);
            this.pnl_Header.Controls.Add(this.lbl_Title);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(12, 12);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1476, 62);
            this.pnl_Header.TabIndex = 0;
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
            this.lbl_Desc.Text = "In thẻ học viên, phiếu theo dõi và tem thẻ DAT cho các học viên được chọn trong khóa học.";
            //
            // lbl_Title
            //
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Appearance.Options.UseForeColor = true;
            this.lbl_Title.Location = new System.Drawing.Point(16, 6);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(150, 25);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "In ấn học viên";
            //
            // pnl_Filter
            //
            this.pnl_Filter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Filter.Controls.Add(this.lbl_Summary);
            this.pnl_Filter.Controls.Add(this.btn_Refresh);
            this.pnl_Filter.Controls.Add(this.sle_KhoaHoc);
            this.pnl_Filter.Controls.Add(this.lbl_KhoaHoc);
            this.pnl_Filter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Filter.Location = new System.Drawing.Point(12, 74);
            this.pnl_Filter.Name = "pnl_Filter";
            this.pnl_Filter.Size = new System.Drawing.Size(1476, 56);
            this.pnl_Filter.TabIndex = 1;
            //
            // lbl_Summary
            //
            this.lbl_Summary.AllowHtmlString = true;
            this.lbl_Summary.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Summary.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Summary.Appearance.Options.UseFont = true;
            this.lbl_Summary.Appearance.Options.UseForeColor = true;
            this.lbl_Summary.Location = new System.Drawing.Point(576, 20);
            this.lbl_Summary.Name = "lbl_Summary";
            this.lbl_Summary.Size = new System.Drawing.Size(105, 16);
            this.lbl_Summary.TabIndex = 3;
            this.lbl_Summary.Text = "Đang tải khóa học...";
            //
            // btn_Refresh
            //
            this.btn_Refresh.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Refresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Refresh.ImageOptions.SvgImage")));
            this.btn_Refresh.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btn_Refresh.Location = new System.Drawing.Point(520, 12);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Refresh.Size = new System.Drawing.Size(36, 32);
            this.btn_Refresh.TabIndex = 2;
            this.btn_Refresh.ToolTip = "Tải lại danh sách khóa học và học viên (F5)";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            //
            // sle_KhoaHoc
            //
            this.sle_KhoaHoc.Location = new System.Drawing.Point(84, 12);
            this.sle_KhoaHoc.Name = "sle_KhoaHoc";
            this.sle_KhoaHoc.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.sle_KhoaHoc.Properties.Appearance.Options.UseFont = true;
            this.sle_KhoaHoc.Properties.AutoHeight = false;
            this.sle_KhoaHoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sle_KhoaHoc.Properties.DisplayMember = "TenKH";
            this.sle_KhoaHoc.Properties.NullText = "Chọn khóa học...";
            this.sle_KhoaHoc.Properties.PopupFormSize = new System.Drawing.Size(620, 420);
            this.sle_KhoaHoc.Properties.PopupView = this.view_KhoaHoc;
            this.sle_KhoaHoc.Properties.ValueMember = "KhoaHocID";
            this.sle_KhoaHoc.Size = new System.Drawing.Size(428, 32);
            this.sle_KhoaHoc.TabIndex = 1;
            this.sle_KhoaHoc.EditValueChanged += new System.EventHandler(this.sle_KhoaHoc_EditValueChanged);
            //
            // view_KhoaHoc
            //
            this.view_KhoaHoc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.view_KhoaHoc.Appearance.HeaderPanel.Options.UseFont = true;
            this.view_KhoaHoc.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9F);
            this.view_KhoaHoc.Appearance.Row.Options.UseFont = true;
            this.view_KhoaHoc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_KH_MaKH,
            this.col_KH_TenKH,
            this.col_KH_Hang,
            this.col_KH_NgayKG,
            this.col_KH_NgayBG});
            this.view_KhoaHoc.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.view_KhoaHoc.Name = "view_KhoaHoc";
            this.view_KhoaHoc.OptionsFind.AlwaysVisible = true;
            this.view_KhoaHoc.OptionsFind.FindNullPrompt = "Tìm theo tên khóa, mã khóa, hạng...";
            this.view_KhoaHoc.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.view_KhoaHoc.OptionsView.ShowGroupPanel = false;
            this.view_KhoaHoc.OptionsView.ShowIndicator = false;
            this.view_KhoaHoc.RowHeight = 24;
            this.view_KhoaHoc.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.col_KH_NgayKG, DevExpress.Data.ColumnSortOrder.Descending)});
            //
            // col_KH_MaKH
            //
            this.col_KH_MaKH.Caption = "Mã khóa";
            this.col_KH_MaKH.FieldName = "MaKH";
            this.col_KH_MaKH.Name = "col_KH_MaKH";
            this.col_KH_MaKH.Visible = true;
            this.col_KH_MaKH.VisibleIndex = 1;
            this.col_KH_MaKH.Width = 130;
            //
            // col_KH_TenKH
            //
            this.col_KH_TenKH.Caption = "Khóa học";
            this.col_KH_TenKH.FieldName = "TenKH";
            this.col_KH_TenKH.Name = "col_KH_TenKH";
            this.col_KH_TenKH.Visible = true;
            this.col_KH_TenKH.VisibleIndex = 0;
            this.col_KH_TenKH.Width = 140;
            //
            // col_KH_Hang
            //
            this.col_KH_Hang.AppearanceCell.Options.UseTextOptions = true;
            this.col_KH_Hang.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_KH_Hang.Caption = "Hạng";
            this.col_KH_Hang.FieldName = "HangDaoTao";
            this.col_KH_Hang.Name = "col_KH_Hang";
            this.col_KH_Hang.Visible = true;
            this.col_KH_Hang.VisibleIndex = 2;
            this.col_KH_Hang.Width = 60;
            //
            // col_KH_NgayKG
            //
            this.col_KH_NgayKG.AppearanceCell.Options.UseTextOptions = true;
            this.col_KH_NgayKG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_KH_NgayKG.Caption = "Khai giảng";
            this.col_KH_NgayKG.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.col_KH_NgayKG.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.col_KH_NgayKG.FieldName = "NgayKG";
            this.col_KH_NgayKG.Name = "col_KH_NgayKG";
            this.col_KH_NgayKG.Visible = true;
            this.col_KH_NgayKG.VisibleIndex = 3;
            this.col_KH_NgayKG.Width = 90;
            //
            // col_KH_NgayBG
            //
            this.col_KH_NgayBG.AppearanceCell.Options.UseTextOptions = true;
            this.col_KH_NgayBG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_KH_NgayBG.Caption = "Bế giảng";
            this.col_KH_NgayBG.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.col_KH_NgayBG.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.col_KH_NgayBG.FieldName = "NgayBG";
            this.col_KH_NgayBG.Name = "col_KH_NgayBG";
            this.col_KH_NgayBG.Visible = true;
            this.col_KH_NgayBG.VisibleIndex = 4;
            this.col_KH_NgayBG.Width = 90;
            //
            // lbl_KhoaHoc
            //
            this.lbl_KhoaHoc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_KhoaHoc.Appearance.Options.UseFont = true;
            this.lbl_KhoaHoc.Location = new System.Drawing.Point(16, 20);
            this.lbl_KhoaHoc.Name = "lbl_KhoaHoc";
            this.lbl_KhoaHoc.Size = new System.Drawing.Size(54, 16);
            this.lbl_KhoaHoc.TabIndex = 0;
            this.lbl_KhoaHoc.Text = "Khóa học";
            //
            // pnl_Actions
            //
            this.pnl_Actions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Actions.Controls.Add(this.lbl_Hint);
            this.pnl_Actions.Controls.Add(this.btn_BoChon);
            this.pnl_Actions.Controls.Add(this.btn_InTem);
            this.pnl_Actions.Controls.Add(this.btn_InPhieu);
            this.pnl_Actions.Controls.Add(this.btn_InThe);
            this.pnl_Actions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Actions.Location = new System.Drawing.Point(12, 728);
            this.pnl_Actions.Name = "pnl_Actions";
            this.pnl_Actions.Size = new System.Drawing.Size(1476, 60);
            this.pnl_Actions.TabIndex = 3;
            //
            // lbl_Hint
            //
            this.lbl_Hint.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_Hint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Hint.Appearance.Options.UseFont = true;
            this.lbl_Hint.Appearance.Options.UseForeColor = true;
            this.lbl_Hint.Location = new System.Drawing.Point(4, 22);
            this.lbl_Hint.Name = "lbl_Hint";
            this.lbl_Hint.Size = new System.Drawing.Size(560, 14);
            this.lbl_Hint.TabIndex = 0;
            this.lbl_Hint.Text = "Tick ô vuông để chọn học viên cần in  ·  tick ở tiêu đề cột để chọn tất cả  ·  tick ở dòng giáo viên để chọn cả nhóm";
            //
            // btn_BoChon
            //
            this.btn_BoChon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_BoChon.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_BoChon.Appearance.Options.UseFont = true;
            this.btn_BoChon.Enabled = false;
            this.btn_BoChon.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_BoChon.ImageOptions.SvgImage")));
            this.btn_BoChon.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btn_BoChon.Location = new System.Drawing.Point(838, 12);
            this.btn_BoChon.Name = "btn_BoChon";
            this.btn_BoChon.Size = new System.Drawing.Size(110, 36);
            this.btn_BoChon.TabIndex = 1;
            this.btn_BoChon.Text = "Bỏ chọn";
            this.btn_BoChon.Click += new System.EventHandler(this.btn_BoChon_Click);
            //
            // btn_InTem
            //
            this.btn_InTem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_InTem.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_InTem.Appearance.Options.UseFont = true;
            this.btn_InTem.Enabled = false;
            this.btn_InTem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_InTem.ImageOptions.SvgImage")));
            this.btn_InTem.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.btn_InTem.Location = new System.Drawing.Point(958, 12);
            this.btn_InTem.Name = "btn_InTem";
            this.btn_InTem.Size = new System.Drawing.Size(160, 36);
            this.btn_InTem.TabIndex = 2;
            this.btn_InTem.Text = "In tem thẻ DAT";
            this.btn_InTem.ToolTip = "In tem dán thẻ DAT cho các học viên đã chọn";
            this.btn_InTem.Click += new System.EventHandler(this.btn_InTem_Click);
            //
            // btn_InPhieu
            //
            this.btn_InPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_InPhieu.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_InPhieu.Appearance.Options.UseFont = true;
            this.btn_InPhieu.Enabled = false;
            this.btn_InPhieu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_InPhieu.ImageOptions.SvgImage")));
            this.btn_InPhieu.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.btn_InPhieu.Location = new System.Drawing.Point(1128, 12);
            this.btn_InPhieu.Name = "btn_InPhieu";
            this.btn_InPhieu.Size = new System.Drawing.Size(160, 36);
            this.btn_InPhieu.TabIndex = 3;
            this.btn_InPhieu.Text = "In phiếu theo dõi";
            this.btn_InPhieu.ToolTip = "In phiếu theo dõi thực hành lái xe cho các học viên đã chọn";
            this.btn_InPhieu.Click += new System.EventHandler(this.btn_InPhieu_Click);
            //
            // btn_InThe
            //
            this.btn_InThe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_InThe.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.btn_InThe.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_InThe.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_InThe.Appearance.Options.UseBackColor = true;
            this.btn_InThe.Appearance.Options.UseFont = true;
            this.btn_InThe.Appearance.Options.UseForeColor = true;
            this.btn_InThe.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btn_InThe.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btn_InThe.AppearanceHovered.Options.UseBackColor = true;
            this.btn_InThe.AppearanceHovered.Options.UseForeColor = true;
            this.btn_InThe.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btn_InThe.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btn_InThe.AppearancePressed.Options.UseBackColor = true;
            this.btn_InThe.AppearancePressed.Options.UseForeColor = true;
            this.btn_InThe.Enabled = false;
            this.btn_InThe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_InThe.ImageOptions.SvgImage")));
            this.btn_InThe.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btn_InThe.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.btn_InThe.Location = new System.Drawing.Point(1298, 12);
            this.btn_InThe.Name = "btn_InThe";
            this.btn_InThe.Size = new System.Drawing.Size(178, 36);
            this.btn_InThe.TabIndex = 4;
            this.btn_InThe.Text = "In thẻ học viên";
            this.btn_InThe.ToolTip = "In thẻ học viên cho các học viên đã chọn";
            this.btn_InThe.Click += new System.EventHandler(this.btn_InThe_Click);
            //
            // grd_lstHocVien
            //
            this.grd_lstHocVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_lstHocVien.Location = new System.Drawing.Point(12, 130);
            this.grd_lstHocVien.MainView = this.gridView_lstHocVien;
            this.grd_lstHocVien.Name = "grd_lstHocVien";
            this.grd_lstHocVien.Size = new System.Drawing.Size(1476, 598);
            this.grd_lstHocVien.TabIndex = 2;
            this.grd_lstHocVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_lstHocVien});
            //
            // gridView_lstHocVien
            //
            this.gridView_lstHocVien.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_lstHocVien.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_lstHocVien.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_lstHocVien.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView_lstHocVien.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_lstHocVien.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.gridView_lstHocVien.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_lstHocVien.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView_lstHocVien.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_lstHocVien.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_lstHocVien.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_lstHocVien.Appearance.Row.Options.UseFont = true;
            this.gridView_lstHocVien.ColumnPanelRowHeight = 36;
            this.gridView_lstHocVien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_NguoiLxID,
            this.col_MaDK,
            this.col_HoTen,
            this.col_NgaySinh,
            this.col_GioiTinh,
            this.col_SoCMT,
            this.col_NgayCapCMT,
            this.col_DiaChi,
            this.col_GiaoVien});
            this.gridView_lstHocVien.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView_lstHocVien.GridControl = this.grd_lstHocVien;
            this.gridView_lstHocVien.GroupCount = 1;
            this.gridView_lstHocVien.GroupFormat = "{1}  {2}";
            this.gridView_lstHocVien.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "MaDK", null, "·  {0} học viên")});
            this.gridView_lstHocVien.GroupRowHeight = 32;
            this.gridView_lstHocVien.Name = "gridView_lstHocVien";
            this.gridView_lstHocVien.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView_lstHocVien.OptionsBehavior.Editable = false;
            this.gridView_lstHocVien.OptionsFind.AlwaysVisible = true;
            this.gridView_lstHocVien.OptionsFind.FindNullPrompt = "Tìm học viên, mã ĐK, CCCD, giáo viên...";
            this.gridView_lstHocVien.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView_lstHocVien.OptionsSelection.MultiSelect = true;
            this.gridView_lstHocVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView_lstHocVien.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = false;
            this.gridView_lstHocVien.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.gridView_lstHocVien.OptionsView.ShowGroupPanel = false;
            this.gridView_lstHocVien.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView_lstHocVien.RowHeight = 32;
            this.gridView_lstHocVien.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.col_GiaoVien, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView_lstHocVien.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView_lstHocVien_SelectionChanged);
            this.gridView_lstHocVien.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView_lstHocVien_CustomDrawEmptyForeground);
            //
            // col_NguoiLxID
            //
            this.col_NguoiLxID.Caption = "NguoiLxID";
            this.col_NguoiLxID.FieldName = "NguoiLxID";
            this.col_NguoiLxID.Name = "col_NguoiLxID";
            //
            // col_MaDK
            //
            this.col_MaDK.AppearanceCell.Options.UseTextOptions = true;
            this.col_MaDK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_MaDK.Caption = "Mã ĐK";
            this.col_MaDK.FieldName = "MaDK";
            this.col_MaDK.Name = "col_MaDK";
            this.col_MaDK.Visible = true;
            this.col_MaDK.VisibleIndex = 1;
            this.col_MaDK.Width = 190;
            //
            // col_HoTen
            //
            this.col_HoTen.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.col_HoTen.AppearanceCell.Options.UseFont = true;
            this.col_HoTen.Caption = "Học viên";
            this.col_HoTen.FieldName = "HoTenHV";
            this.col_HoTen.Name = "col_HoTen";
            this.col_HoTen.Visible = true;
            this.col_HoTen.VisibleIndex = 2;
            this.col_HoTen.Width = 230;
            //
            // col_NgaySinh
            //
            this.col_NgaySinh.AppearanceCell.Options.UseTextOptions = true;
            this.col_NgaySinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_NgaySinh.Caption = "Ngày sinh";
            this.col_NgaySinh.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.col_NgaySinh.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.col_NgaySinh.FieldName = "NgaySinh";
            this.col_NgaySinh.Name = "col_NgaySinh";
            this.col_NgaySinh.Visible = true;
            this.col_NgaySinh.VisibleIndex = 3;
            this.col_NgaySinh.Width = 110;
            //
            // col_GioiTinh
            //
            this.col_GioiTinh.AppearanceCell.Options.UseTextOptions = true;
            this.col_GioiTinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_GioiTinh.Caption = "Giới tính";
            this.col_GioiTinh.FieldName = "GioiTinh";
            this.col_GioiTinh.Name = "col_GioiTinh";
            this.col_GioiTinh.Visible = true;
            this.col_GioiTinh.VisibleIndex = 4;
            this.col_GioiTinh.Width = 80;
            //
            // col_SoCMT
            //
            this.col_SoCMT.AppearanceCell.Options.UseTextOptions = true;
            this.col_SoCMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_SoCMT.Caption = "Số CCCD";
            this.col_SoCMT.FieldName = "SoCMT";
            this.col_SoCMT.Name = "col_SoCMT";
            this.col_SoCMT.Visible = true;
            this.col_SoCMT.VisibleIndex = 5;
            this.col_SoCMT.Width = 130;
            //
            // col_NgayCapCMT
            //
            this.col_NgayCapCMT.AppearanceCell.Options.UseTextOptions = true;
            this.col_NgayCapCMT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_NgayCapCMT.Caption = "Ngày cấp";
            this.col_NgayCapCMT.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.col_NgayCapCMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.col_NgayCapCMT.FieldName = "NgayCapCMT";
            this.col_NgayCapCMT.Name = "col_NgayCapCMT";
            this.col_NgayCapCMT.Visible = true;
            this.col_NgayCapCMT.VisibleIndex = 6;
            this.col_NgayCapCMT.Width = 110;
            //
            // col_DiaChi
            //
            this.col_DiaChi.Caption = "Địa chỉ";
            this.col_DiaChi.FieldName = "Address";
            this.col_DiaChi.Name = "col_DiaChi";
            this.col_DiaChi.Visible = true;
            this.col_DiaChi.VisibleIndex = 7;
            this.col_DiaChi.Width = 400;
            //
            // col_GiaoVien
            //
            this.col_GiaoVien.Caption = "Giáo viên";
            this.col_GiaoVien.FieldName = "TenGV";
            this.col_GiaoVien.GroupIndex = 0;
            this.col_GiaoVien.Name = "col_GiaoVien";
            this.col_GiaoVien.Visible = true;
            //
            // Uc_Print
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grd_lstHocVien);
            this.Controls.Add(this.pnl_Actions);
            this.Controls.Add(this.pnl_Filter);
            this.Controls.Add(this.pnl_Header);
            this.Name = "Uc_Print";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(1500, 800);
            this.Load += new System.EventHandler(this.Uc_Print_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Filter)).EndInit();
            this.pnl_Filter.ResumeLayout(false);
            this.pnl_Filter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sle_KhoaHoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view_KhoaHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Actions)).EndInit();
            this.pnl_Actions.ResumeLayout(false);
            this.pnl_Actions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_lstHocVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_lstHocVien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.LabelControl lbl_Desc;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.PanelControl pnl_Filter;
        private DevExpress.XtraEditors.LabelControl lbl_Summary;
        private DevExpress.XtraEditors.SimpleButton btn_Refresh;
        private DevExpress.XtraEditors.SearchLookUpEdit sle_KhoaHoc;
        private DevExpress.XtraGrid.Views.Grid.GridView view_KhoaHoc;
        private DevExpress.XtraGrid.Columns.GridColumn col_KH_MaKH;
        private DevExpress.XtraGrid.Columns.GridColumn col_KH_TenKH;
        private DevExpress.XtraGrid.Columns.GridColumn col_KH_Hang;
        private DevExpress.XtraGrid.Columns.GridColumn col_KH_NgayKG;
        private DevExpress.XtraGrid.Columns.GridColumn col_KH_NgayBG;
        private DevExpress.XtraEditors.LabelControl lbl_KhoaHoc;
        private DevExpress.XtraEditors.PanelControl pnl_Actions;
        private DevExpress.XtraEditors.LabelControl lbl_Hint;
        private DevExpress.XtraEditors.SimpleButton btn_BoChon;
        private DevExpress.XtraEditors.SimpleButton btn_InTem;
        private DevExpress.XtraEditors.SimpleButton btn_InPhieu;
        private DevExpress.XtraEditors.SimpleButton btn_InThe;
        private DevExpress.XtraGrid.GridControl grd_lstHocVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_lstHocVien;
        private DevExpress.XtraGrid.Columns.GridColumn col_NguoiLxID;
        private DevExpress.XtraGrid.Columns.GridColumn col_MaDK;
        private DevExpress.XtraGrid.Columns.GridColumn col_HoTen;
        private DevExpress.XtraGrid.Columns.GridColumn col_NgaySinh;
        private DevExpress.XtraGrid.Columns.GridColumn col_GioiTinh;
        private DevExpress.XtraGrid.Columns.GridColumn col_SoCMT;
        private DevExpress.XtraGrid.Columns.GridColumn col_NgayCapCMT;
        private DevExpress.XtraGrid.Columns.GridColumn col_DiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn col_GiaoVien;
    }
}
