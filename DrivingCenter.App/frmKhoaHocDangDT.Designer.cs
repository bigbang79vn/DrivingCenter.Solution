namespace DrivingCenter.App
{
    partial class frmKhoaHocDangDT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKhoaHocDangDT));
            this.panelControl_Header = new DevExpress.XtraEditors.PanelControl();
            this.lblSubTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelControl_Toolbar = new DevExpress.XtraEditors.PanelControl();
            this.btnCollapse = new DevExpress.XtraEditors.SimpleButton();
            this.btnExpand = new DevExpress.XtraEditors.SimpleButton();
            this.btnBoChon = new DevExpress.XtraEditors.SimpleButton();
            this.btnGoiY = new DevExpress.XtraEditors.SimpleButton();
            this.searchControl_KhoaHoc = new DevExpress.XtraEditors.SearchControl();
            this.treeList_KhoaHoc = new DevExpress.XtraTreeList.TreeList();
            this.colTenKH = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMaKH = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colHangDT = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNgayKG = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNgayBG = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNgayBatDauDT = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTrangThai = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl_Bottom = new DevExpress.XtraEditors.PanelControl();
            this.sep_Bottom = new DevExpress.XtraEditors.SeparatorControl();
            this.lblDaChon = new DevExpress.XtraEditors.LabelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnCapNhat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Header)).BeginInit();
            this.panelControl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Toolbar)).BeginInit();
            this.panelControl_Toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl_KhoaHoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList_KhoaHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Bottom)).BeginInit();
            this.panelControl_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sep_Bottom)).BeginInit();
            this.SuspendLayout();
            //
            // panelControl_Header
            //
            this.panelControl_Header.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.panelControl_Header.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(132)))), ((int)(((byte)(186)))));
            this.panelControl_Header.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.panelControl_Header.Appearance.Options.UseBackColor = true;
            this.panelControl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl_Header.Controls.Add(this.lblSubTitle);
            this.panelControl_Header.Controls.Add(this.lblTitle);
            this.panelControl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl_Header.Location = new System.Drawing.Point(0, 0);
            this.panelControl_Header.Name = "panelControl_Header";
            this.panelControl_Header.Size = new System.Drawing.Size(980, 76);
            this.panelControl_Header.TabIndex = 0;
            //
            // lblSubTitle
            //
            this.lblSubTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTitle.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblSubTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(245)))));
            this.lblSubTitle.Appearance.Options.UseBackColor = true;
            this.lblSubTitle.Appearance.Options.UseFont = true;
            this.lblSubTitle.Appearance.Options.UseForeColor = true;
            this.lblSubTitle.Location = new System.Drawing.Point(24, 46);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(574, 16);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Tick các khóa đang dạy. Danh sách này dùng cho tab Kết quả, Phiên học bỏ và các báo cáo DAT.";
            //
            // lblTitle
            //
            this.lblTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Appearance.Options.UseBackColor = true;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(24, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(247, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Khóa học đang đào tạo";
            //
            // panelControl_Toolbar
            //
            this.panelControl_Toolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl_Toolbar.Controls.Add(this.btnCollapse);
            this.panelControl_Toolbar.Controls.Add(this.btnExpand);
            this.panelControl_Toolbar.Controls.Add(this.btnBoChon);
            this.panelControl_Toolbar.Controls.Add(this.btnGoiY);
            this.panelControl_Toolbar.Controls.Add(this.searchControl_KhoaHoc);
            this.panelControl_Toolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl_Toolbar.Location = new System.Drawing.Point(0, 76);
            this.panelControl_Toolbar.Name = "panelControl_Toolbar";
            this.panelControl_Toolbar.Size = new System.Drawing.Size(980, 56);
            this.panelControl_Toolbar.TabIndex = 1;
            //
            // btnCollapse
            //
            this.btnCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCollapse.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btnCollapse.Appearance.Options.UseFont = true;
            this.btnCollapse.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCollapse.ImageOptions.SvgImage")));
            this.btnCollapse.ImageOptions.SvgImageSize = new System.Drawing.Size(14, 14);
            this.btnCollapse.Location = new System.Drawing.Point(870, 12);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnCollapse.Size = new System.Drawing.Size(94, 32);
            this.btnCollapse.TabIndex = 4;
            this.btnCollapse.Text = "Thu gọn";
            this.btnCollapse.ToolTip = "Thu gọn về danh sách năm";
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            //
            // btnExpand
            //
            this.btnExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpand.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btnExpand.Appearance.Options.UseFont = true;
            this.btnExpand.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnExpand.ImageOptions.SvgImage")));
            this.btnExpand.ImageOptions.SvgImageSize = new System.Drawing.Size(14, 14);
            this.btnExpand.Location = new System.Drawing.Point(770, 12);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnExpand.Size = new System.Drawing.Size(94, 32);
            this.btnExpand.TabIndex = 3;
            this.btnExpand.Text = "Mở rộng";
            this.btnExpand.ToolTip = "Mở toàn bộ năm / tháng";
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            //
            // btnBoChon
            //
            this.btnBoChon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoChon.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btnBoChon.Appearance.Options.UseFont = true;
            this.btnBoChon.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnBoChon.ImageOptions.SvgImage")));
            this.btnBoChon.ImageOptions.SvgImageSize = new System.Drawing.Size(14, 14);
            this.btnBoChon.Location = new System.Drawing.Point(652, 12);
            this.btnBoChon.Name = "btnBoChon";
            this.btnBoChon.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnBoChon.Size = new System.Drawing.Size(112, 32);
            this.btnBoChon.TabIndex = 2;
            this.btnBoChon.Text = "Bỏ chọn hết";
            this.btnBoChon.Click += new System.EventHandler(this.btnBoChon_Click);
            //
            // btnGoiY
            //
            this.btnGoiY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoiY.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btnGoiY.Appearance.Options.UseFont = true;
            this.btnGoiY.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnGoiY.ImageOptions.SvgImage")));
            this.btnGoiY.ImageOptions.SvgImageSize = new System.Drawing.Size(14, 14);
            this.btnGoiY.Location = new System.Drawing.Point(452, 12);
            this.btnGoiY.Name = "btnGoiY";
            this.btnGoiY.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnGoiY.Size = new System.Drawing.Size(194, 32);
            this.btnGoiY.TabIndex = 1;
            this.btnGoiY.Text = "Chọn khóa đang học";
            this.btnGoiY.ToolTip = "Tick thêm các khóa đã khai giảng và chưa bế giảng";
            this.btnGoiY.Click += new System.EventHandler(this.btnGoiY_Click);
            //
            // searchControl_KhoaHoc
            //
            this.searchControl_KhoaHoc.Client = this.treeList_KhoaHoc;
            this.searchControl_KhoaHoc.Location = new System.Drawing.Point(16, 12);
            this.searchControl_KhoaHoc.Name = "searchControl_KhoaHoc";
            this.searchControl_KhoaHoc.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.searchControl_KhoaHoc.Properties.Appearance.Options.UseFont = true;
            this.searchControl_KhoaHoc.Properties.AutoHeight = false;
            this.searchControl_KhoaHoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl_KhoaHoc.Properties.Client = this.treeList_KhoaHoc;
            this.searchControl_KhoaHoc.Properties.NullValuePrompt = "Tìm theo tên, mã khóa học, hạng...";
            this.searchControl_KhoaHoc.Size = new System.Drawing.Size(320, 32);
            this.searchControl_KhoaHoc.TabIndex = 0;
            //
            // treeList_KhoaHoc
            //
            this.treeList_KhoaHoc.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(232)))), ((int)(((byte)(244)))));
            this.treeList_KhoaHoc.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.treeList_KhoaHoc.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeList_KhoaHoc.Appearance.FocusedRow.Options.UseForeColor = true;
            this.treeList_KhoaHoc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.treeList_KhoaHoc.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.treeList_KhoaHoc.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeList_KhoaHoc.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.treeList_KhoaHoc.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeList_KhoaHoc.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeList_KhoaHoc.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(232)))), ((int)(((byte)(244)))));
            this.treeList_KhoaHoc.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.treeList_KhoaHoc.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.treeList_KhoaHoc.Appearance.Row.Options.UseFont = true;
            this.treeList_KhoaHoc.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colTenKH,
            this.colMaKH,
            this.colHangDT,
            this.colNgayKG,
            this.colNgayBG,
            this.colNgayBatDauDT,
            this.colTrangThai});
            this.treeList_KhoaHoc.ColumnPanelRowHeight = 34;
            this.treeList_KhoaHoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList_KhoaHoc.KeyFieldName = "Id";
            this.treeList_KhoaHoc.Location = new System.Drawing.Point(0, 132);
            this.treeList_KhoaHoc.Name = "treeList_KhoaHoc";
            this.treeList_KhoaHoc.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeList_KhoaHoc.OptionsBehavior.Editable = false;
            this.treeList_KhoaHoc.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.ParentBranch;
            this.treeList_KhoaHoc.OptionsFind.AllowFindPanel = false;
            this.treeList_KhoaHoc.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeList_KhoaHoc.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.treeList_KhoaHoc.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            this.treeList_KhoaHoc.OptionsView.ShowHorzLines = false;
            this.treeList_KhoaHoc.OptionsView.ShowIndicator = false;
            this.treeList_KhoaHoc.OptionsView.ShowVertLines = false;
            this.treeList_KhoaHoc.ParentFieldName = "ParentId";
            this.treeList_KhoaHoc.RowHeight = 30;
            this.treeList_KhoaHoc.Size = new System.Drawing.Size(980, 488);
            this.treeList_KhoaHoc.TabIndex = 2;
            this.treeList_KhoaHoc.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList_KhoaHoc_NodeCellStyle);
            this.treeList_KhoaHoc.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList_KhoaHoc_AfterCheckNode);
            this.treeList_KhoaHoc.CustomColumnDisplayText += new DevExpress.XtraTreeList.CustomColumnDisplayTextEventHandler(this.treeList_KhoaHoc_CustomColumnDisplayText);
            //
            // colTenKH
            //
            this.colTenKH.Caption = "Khóa học";
            this.colTenKH.FieldName = "TenKH";
            this.colTenKH.MinWidth = 200;
            this.colTenKH.Name = "colTenKH";
            this.colTenKH.Visible = true;
            this.colTenKH.VisibleIndex = 0;
            this.colTenKH.Width = 260;
            //
            // colMaKH
            //
            this.colMaKH.AppearanceCell.Options.UseTextOptions = true;
            this.colMaKH.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaKH.Caption = "Mã khóa học";
            this.colMaKH.FieldName = "MaKH";
            this.colMaKH.Name = "colMaKH";
            this.colMaKH.Visible = true;
            this.colMaKH.VisibleIndex = 1;
            this.colMaKH.Width = 130;
            //
            // colHangDT
            //
            this.colHangDT.AppearanceCell.Options.UseTextOptions = true;
            this.colHangDT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHangDT.Caption = "Hạng";
            this.colHangDT.FieldName = "HangDT";
            this.colHangDT.Name = "colHangDT";
            this.colHangDT.Visible = true;
            this.colHangDT.VisibleIndex = 2;
            this.colHangDT.Width = 60;
            //
            // colNgayKG
            //
            this.colNgayKG.AppearanceCell.Options.UseTextOptions = true;
            this.colNgayKG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayKG.Caption = "Khai giảng";
            this.colNgayKG.FieldName = "NgayKG";
            this.colNgayKG.Format.FormatString = "dd/MM/yyyy";
            this.colNgayKG.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayKG.Name = "colNgayKG";
            this.colNgayKG.Visible = true;
            this.colNgayKG.VisibleIndex = 3;
            this.colNgayKG.Width = 100;
            //
            // colNgayBG
            //
            this.colNgayBG.AppearanceCell.Options.UseTextOptions = true;
            this.colNgayBG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayBG.Caption = "Bế giảng";
            this.colNgayBG.FieldName = "NgayBG";
            this.colNgayBG.Format.FormatString = "dd/MM/yyyy";
            this.colNgayBG.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayBG.Name = "colNgayBG";
            this.colNgayBG.Visible = true;
            this.colNgayBG.VisibleIndex = 4;
            this.colNgayBG.Width = 100;
            //
            // colNgayBatDauDT
            //
            this.colNgayBatDauDT.AppearanceCell.Options.UseTextOptions = true;
            this.colNgayBatDauDT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayBatDauDT.Caption = "Hạn đào tạo";
            this.colNgayBatDauDT.FieldName = "NgayBatDauDT";
            this.colNgayBatDauDT.Format.FormatString = "dd/MM/yyyy";
            this.colNgayBatDauDT.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayBatDauDT.Name = "colNgayBatDauDT";
            this.colNgayBatDauDT.Visible = true;
            this.colNgayBatDauDT.VisibleIndex = 5;
            this.colNgayBatDauDT.Width = 100;
            //
            // colTrangThai
            //
            this.colTrangThai.AppearanceCell.Options.UseTextOptions = true;
            this.colTrangThai.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTrangThai.Caption = "Trạng thái";
            this.colTrangThai.FieldName = "TrangThai";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 6;
            this.colTrangThai.Width = 120;
            //
            // panelControl_Bottom
            //
            this.panelControl_Bottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl_Bottom.Controls.Add(this.sep_Bottom);
            this.panelControl_Bottom.Controls.Add(this.lblDaChon);
            this.panelControl_Bottom.Controls.Add(this.btnThoat);
            this.panelControl_Bottom.Controls.Add(this.btnCapNhat);
            this.panelControl_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl_Bottom.Location = new System.Drawing.Point(0, 620);
            this.panelControl_Bottom.Name = "panelControl_Bottom";
            this.panelControl_Bottom.Size = new System.Drawing.Size(980, 60);
            this.panelControl_Bottom.TabIndex = 3;
            //
            // sep_Bottom
            //
            this.sep_Bottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.sep_Bottom.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.sep_Bottom.Location = new System.Drawing.Point(0, 0);
            this.sep_Bottom.Name = "sep_Bottom";
            this.sep_Bottom.Padding = new System.Windows.Forms.Padding(0);
            this.sep_Bottom.Size = new System.Drawing.Size(980, 1);
            this.sep_Bottom.TabIndex = 3;
            //
            // lblDaChon
            //
            this.lblDaChon.AllowHtmlString = true;
            this.lblDaChon.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblDaChon.Appearance.Options.UseFont = true;
            this.lblDaChon.Location = new System.Drawing.Point(18, 22);
            this.lblDaChon.Name = "lblDaChon";
            this.lblDaChon.Size = new System.Drawing.Size(103, 16);
            this.lblDaChon.TabIndex = 2;
            this.lblDaChon.Text = "Đang tải khóa học...";
            //
            // btnThoat
            //
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Location = new System.Drawing.Point(854, 13);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(110, 34);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "Hủy";
            //
            // btnCapNhat
            //
            this.btnCapNhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapNhat.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.btnCapNhat.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCapNhat.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Appearance.Options.UseBackColor = true;
            this.btnCapNhat.Appearance.Options.UseFont = true;
            this.btnCapNhat.Appearance.Options.UseForeColor = true;
            this.btnCapNhat.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btnCapNhat.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.AppearanceHovered.Options.UseBackColor = true;
            this.btnCapNhat.AppearanceHovered.Options.UseForeColor = true;
            this.btnCapNhat.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btnCapNhat.AppearancePressed.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.AppearancePressed.Options.UseBackColor = true;
            this.btnCapNhat.AppearancePressed.Options.UseForeColor = true;
            this.btnCapNhat.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCapNhat.ImageOptions.SvgImage")));
            this.btnCapNhat.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.btnCapNhat.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnCapNhat.Location = new System.Drawing.Point(696, 13);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(150, 34);
            this.btnCapNhat.TabIndex = 0;
            this.btnCapNhat.Text = "Lưu thay đổi";
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            //
            // frmKhoaHocDangDT
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(980, 680);
            this.Controls.Add(this.treeList_KhoaHoc);
            this.Controls.Add(this.panelControl_Toolbar);
            this.Controls.Add(this.panelControl_Bottom);
            this.Controls.Add(this.panelControl_Header);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 520);
            this.Name = "frmKhoaHocDangDT";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Khóa học đang đào tạo";
            this.Load += new System.EventHandler(this.frmKhoaHocDangDT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Header)).EndInit();
            this.panelControl_Header.ResumeLayout(false);
            this.panelControl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Toolbar)).EndInit();
            this.panelControl_Toolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl_KhoaHoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList_KhoaHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Bottom)).EndInit();
            this.panelControl_Bottom.ResumeLayout(false);
            this.panelControl_Bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sep_Bottom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl_Header;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblSubTitle;
        private DevExpress.XtraEditors.PanelControl panelControl_Toolbar;
        private DevExpress.XtraEditors.SearchControl searchControl_KhoaHoc;
        private DevExpress.XtraEditors.SimpleButton btnGoiY;
        private DevExpress.XtraEditors.SimpleButton btnBoChon;
        private DevExpress.XtraEditors.SimpleButton btnExpand;
        private DevExpress.XtraEditors.SimpleButton btnCollapse;
        private DevExpress.XtraTreeList.TreeList treeList_KhoaHoc;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenKH;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMaKH;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colHangDT;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayKG;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayBG;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNgayBatDauDT;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTrangThai;
        private DevExpress.XtraEditors.PanelControl panelControl_Bottom;
        private DevExpress.XtraEditors.SeparatorControl sep_Bottom;
        private DevExpress.XtraEditors.LabelControl lblDaChon;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnCapNhat;
    }
}
