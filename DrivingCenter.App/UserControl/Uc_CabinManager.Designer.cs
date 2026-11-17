namespace DrivingCenter.App.UserControl
{
    partial class Uc_CabinManager
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
            this.components = new System.ComponentModel.Container();
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.lbl_Desc = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Server = new DevExpress.XtraEditors.LabelControl();
            this.grp_HocVien = new DevExpress.XtraEditors.GroupControl();
            this.gridControl_HocVien = new DevExpress.XtraGrid.GridControl();
            this.gridView_HocVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_HoTenHV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_SoCMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_HangGPLX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ToTalSecond = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Cabin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnl_Assign = new DevExpress.XtraEditors.PanelControl();
            this.btn_AutoAssign = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Assign = new DevExpress.XtraEditors.SimpleButton();
            this.cbo_Cabin = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pnl_KhoaHoc = new DevExpress.XtraEditors.PanelControl();
            this.btn_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.lookUp_KhoaHoc = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.view_KhoaHoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_view_KhoaHoc_MaKhoaHoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_view_KhoaHoc_TenKhoaHoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_view_KhoaHoc_HangDaoTao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lbl_KhoaHoc = new DevExpress.XtraEditors.LabelControl();
            this.tbl_Cabins = new System.Windows.Forms.TableLayoutPanel();
            this.card_Cabin1 = new DrivingCenter.App.UserControl.UcCabinClientCard();
            this.card_Cabin2 = new DrivingCenter.App.UserControl.UcCabinClientCard();
            this.card_Cabin3 = new DrivingCenter.App.UserControl.UcCabinClientCard();
            this.card_Cabin4 = new DrivingCenter.App.UserControl.UcCabinClientCard();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_HocVien)).BeginInit();
            this.grp_HocVien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_HocVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_HocVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Assign)).BeginInit();
            this.pnl_Assign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_Cabin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_KhoaHoc)).BeginInit();
            this.pnl_KhoaHoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUp_KhoaHoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view_KhoaHoc)).BeginInit();
            this.tbl_Cabins.SuspendLayout();
            this.SuspendLayout();
            //
            // pnl_Header
            //
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.lbl_Server);
            this.pnl_Header.Controls.Add(this.lbl_Desc);
            this.pnl_Header.Controls.Add(this.lbl_Title);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(12, 12);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1476, 62);
            this.pnl_Header.TabIndex = 0;
            //
            // lbl_Server
            //
            this.lbl_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Server.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Server.Appearance.Options.UseFont = true;
            this.lbl_Server.Appearance.Options.UseTextOptions = true;
            this.lbl_Server.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_Server.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Server.Location = new System.Drawing.Point(1026, 22);
            this.lbl_Server.Name = "lbl_Server";
            this.lbl_Server.Size = new System.Drawing.Size(440, 18);
            this.lbl_Server.TabIndex = 6;
            this.lbl_Server.Text = "●  Chưa mở cổng kết nối";
            //
            // lbl_Desc
            //
            this.lbl_Desc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Desc.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Desc.Appearance.Options.UseFont = true;
            this.lbl_Desc.Appearance.Options.UseForeColor = true;
            this.lbl_Desc.Location = new System.Drawing.Point(16, 38);
            this.lbl_Desc.Name = "lbl_Desc";
            this.lbl_Desc.Size = new System.Drawing.Size(480, 16);
            this.lbl_Desc.TabIndex = 5;
            this.lbl_Desc.Text = "Theo dõi kết nối của các máy cabin và xếp học viên vào từng máy.";
            //
            // lbl_Title
            //
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Appearance.Options.UseForeColor = true;
            this.lbl_Title.Location = new System.Drawing.Point(16, 8);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(240, 25);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "ĐIỀU KHIỂN CABIN";
            //
            // grp_HocVien
            //
            this.grp_HocVien.AppearanceCaption.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold);
            this.grp_HocVien.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.grp_HocVien.AppearanceCaption.Options.UseFont = true;
            this.grp_HocVien.AppearanceCaption.Options.UseForeColor = true;
            this.grp_HocVien.Controls.Add(this.gridControl_HocVien);
            this.grp_HocVien.Controls.Add(this.pnl_Assign);
            this.grp_HocVien.Controls.Add(this.pnl_KhoaHoc);
            this.grp_HocVien.Dock = System.Windows.Forms.DockStyle.Left;
            this.grp_HocVien.Location = new System.Drawing.Point(12, 74);
            this.grp_HocVien.Name = "grp_HocVien";
            this.grp_HocVien.Size = new System.Drawing.Size(460, 814);
            this.grp_HocVien.TabIndex = 1;
            this.grp_HocVien.Text = "DANH SÁCH HỌC VIÊN";
            //
            // gridControl_HocVien
            //
            this.gridControl_HocVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_HocVien.Location = new System.Drawing.Point(2, 75);
            this.gridControl_HocVien.MainView = this.gridView_HocVien;
            this.gridControl_HocVien.Name = "gridControl_HocVien";
            this.gridControl_HocVien.Size = new System.Drawing.Size(456, 681);
            this.gridControl_HocVien.TabIndex = 1;
            this.gridControl_HocVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_HocVien});
            //
            // gridView_HocVien
            //
            this.gridView_HocVien.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_HocVien.Appearance.Row.Options.UseFont = true;
            this.gridView_HocVien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_HoTenHV,
            this.col_SoCMT,
            this.col_HangGPLX,
            this.col_ToTalSecond,
            this.col_Cabin});
            this.gridView_HocVien.GridControl = this.gridControl_HocVien;
            this.gridView_HocVien.Name = "gridView_HocVien";
            this.gridView_HocVien.OptionsBehavior.Editable = false;
            this.gridView_HocVien.OptionsSelection.MultiSelect = true;
            this.gridView_HocVien.DoubleClick += new System.EventHandler(this.gridView_HocVien_DoubleClick);
            //
            // col_HoTenHV
            //
            this.col_HoTenHV.Caption = "Họ tên";
            this.col_HoTenHV.FieldName = "HoTenHV";
            this.col_HoTenHV.Name = "col_HoTenHV";
            this.col_HoTenHV.Visible = true;
            this.col_HoTenHV.VisibleIndex = 0;
            this.col_HoTenHV.Width = 150;
            //
            // col_SoCMT
            //
            this.col_SoCMT.Caption = "CCCD";
            this.col_SoCMT.FieldName = "SoCMT";
            this.col_SoCMT.Name = "col_SoCMT";
            this.col_SoCMT.Visible = true;
            this.col_SoCMT.VisibleIndex = 1;
            this.col_SoCMT.Width = 105;
            //
            // col_HangGPLX
            //
            this.col_HangGPLX.AppearanceCell.Options.UseTextOptions = true;
            this.col_HangGPLX.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_HangGPLX.Caption = "Hạng";
            this.col_HangGPLX.FieldName = "HangGPLX";
            this.col_HangGPLX.Name = "col_HangGPLX";
            this.col_HangGPLX.Visible = true;
            this.col_HangGPLX.VisibleIndex = 2;
            this.col_HangGPLX.Width = 45;
            //
            // col_ToTalSecond
            //
            this.col_ToTalSecond.AppearanceCell.Options.UseTextOptions = true;
            this.col_ToTalSecond.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_ToTalSecond.Caption = "Đã học";
            this.col_ToTalSecond.FieldName = "ToTalSecond";
            this.col_ToTalSecond.Name = "col_ToTalSecond";
            this.col_ToTalSecond.Visible = true;
            this.col_ToTalSecond.VisibleIndex = 3;
            this.col_ToTalSecond.Width = 65;
            //
            // col_Cabin
            //
            this.col_Cabin.AppearanceCell.Options.UseTextOptions = true;
            this.col_Cabin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Cabin.Caption = "Cabin";
            this.col_Cabin.FieldName = "col_Cabin";
            this.col_Cabin.Name = "col_Cabin";
            this.col_Cabin.UnboundDataType = typeof(string);
            this.col_Cabin.Visible = true;
            this.col_Cabin.VisibleIndex = 4;
            this.col_Cabin.Width = 55;
            //
            // pnl_Assign
            //
            this.pnl_Assign.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Assign.Controls.Add(this.btn_AutoAssign);
            this.pnl_Assign.Controls.Add(this.btn_Assign);
            this.pnl_Assign.Controls.Add(this.cbo_Cabin);
            this.pnl_Assign.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Assign.Location = new System.Drawing.Point(2, 756);
            this.pnl_Assign.Name = "pnl_Assign";
            this.pnl_Assign.Size = new System.Drawing.Size(456, 56);
            this.pnl_Assign.TabIndex = 2;
            //
            // btn_AutoAssign
            //
            this.btn_AutoAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AutoAssign.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_AutoAssign.Appearance.Options.UseFont = true;
            this.btn_AutoAssign.Location = new System.Drawing.Point(316, 12);
            this.btn_AutoAssign.Name = "btn_AutoAssign";
            this.btn_AutoAssign.Size = new System.Drawing.Size(130, 34);
            this.btn_AutoAssign.TabIndex = 2;
            this.btn_AutoAssign.Text = "Gán lần lượt";
            this.btn_AutoAssign.ToolTip = "Gán các học viên đang chọn vào các cabin còn trống theo thứ tự";
            this.btn_AutoAssign.Click += new System.EventHandler(this.btn_AutoAssign_Click);
            //
            // btn_Assign
            //
            this.btn_Assign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Assign.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Assign.Appearance.Options.UseFont = true;
            this.btn_Assign.Location = new System.Drawing.Point(180, 12);
            this.btn_Assign.Name = "btn_Assign";
            this.btn_Assign.Size = new System.Drawing.Size(130, 34);
            this.btn_Assign.TabIndex = 1;
            this.btn_Assign.Text = "Gán vào cabin";
            this.btn_Assign.Click += new System.EventHandler(this.btn_Assign_Click);
            //
            // cbo_Cabin
            //
            this.cbo_Cabin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbo_Cabin.Location = new System.Drawing.Point(10, 17);
            this.cbo_Cabin.Name = "cbo_Cabin";
            this.cbo_Cabin.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.cbo_Cabin.Properties.Appearance.Options.UseFont = true;
            this.cbo_Cabin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_Cabin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbo_Cabin.Size = new System.Drawing.Size(164, 24);
            this.cbo_Cabin.TabIndex = 0;
            //
            // pnl_KhoaHoc
            //
            this.pnl_KhoaHoc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_KhoaHoc.Controls.Add(this.btn_Refresh);
            this.pnl_KhoaHoc.Controls.Add(this.lookUp_KhoaHoc);
            this.pnl_KhoaHoc.Controls.Add(this.lbl_KhoaHoc);
            this.pnl_KhoaHoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_KhoaHoc.Location = new System.Drawing.Point(2, 27);
            this.pnl_KhoaHoc.Name = "pnl_KhoaHoc";
            this.pnl_KhoaHoc.Size = new System.Drawing.Size(456, 48);
            this.pnl_KhoaHoc.TabIndex = 0;
            //
            // btn_Refresh
            //
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.Location = new System.Drawing.Point(412, 10);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Refresh.Size = new System.Drawing.Size(34, 28);
            this.btn_Refresh.TabIndex = 1;
            this.btn_Refresh.ToolTip = "Tải lại khóa học";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // view_KhoaHoc
            // 
            this.view_KhoaHoc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.view_KhoaHoc.Appearance.HeaderPanel.Options.UseFont = true;
            this.view_KhoaHoc.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9F);
            this.view_KhoaHoc.Appearance.Row.Options.UseFont = true;
            this.view_KhoaHoc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_view_KhoaHoc_MaKhoaHoc,
            this.col_view_KhoaHoc_TenKhoaHoc,
            this.col_view_KhoaHoc_HangDaoTao});
            this.view_KhoaHoc.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.view_KhoaHoc.Name = "view_KhoaHoc";
            this.view_KhoaHoc.OptionsFind.AlwaysVisible = true;
            this.view_KhoaHoc.OptionsFind.FindNullPrompt = "Tìm theo tên khóa, mã khóa, hạng...";
            this.view_KhoaHoc.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.view_KhoaHoc.OptionsView.ShowGroupPanel = false;
            this.view_KhoaHoc.OptionsView.ShowIndicator = false;
            this.view_KhoaHoc.RowHeight = 24;
            // 
            // col_view_KhoaHoc_MaKhoaHoc
            // 
            this.col_view_KhoaHoc_MaKhoaHoc.Caption = "Mã khóa";
            this.col_view_KhoaHoc_MaKhoaHoc.FieldName = "MaKhoaHoc";
            this.col_view_KhoaHoc_MaKhoaHoc.Name = "col_view_KhoaHoc_MaKhoaHoc";
            this.col_view_KhoaHoc_MaKhoaHoc.Visible = true;
            this.col_view_KhoaHoc_MaKhoaHoc.VisibleIndex = 0;
            this.col_view_KhoaHoc_MaKhoaHoc.Width = 130;
            // 
            // col_view_KhoaHoc_TenKhoaHoc
            // 
            this.col_view_KhoaHoc_TenKhoaHoc.Caption = "Tên khóa học";
            this.col_view_KhoaHoc_TenKhoaHoc.FieldName = "TenKhoaHoc";
            this.col_view_KhoaHoc_TenKhoaHoc.Name = "col_view_KhoaHoc_TenKhoaHoc";
            this.col_view_KhoaHoc_TenKhoaHoc.Visible = true;
            this.col_view_KhoaHoc_TenKhoaHoc.VisibleIndex = 1;
            this.col_view_KhoaHoc_TenKhoaHoc.Width = 170;
            // 
            // col_view_KhoaHoc_HangDaoTao
            // 
            this.col_view_KhoaHoc_HangDaoTao.Caption = "Hạng";
            this.col_view_KhoaHoc_HangDaoTao.FieldName = "HangDaoTao";
            this.col_view_KhoaHoc_HangDaoTao.Name = "col_view_KhoaHoc_HangDaoTao";
            this.col_view_KhoaHoc_HangDaoTao.Visible = true;
            this.col_view_KhoaHoc_HangDaoTao.VisibleIndex = 2;
            this.col_view_KhoaHoc_HangDaoTao.Width = 60;
            //
            // lookUp_KhoaHoc
            //
            this.lookUp_KhoaHoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lookUp_KhoaHoc.Location = new System.Drawing.Point(80, 12);
            this.lookUp_KhoaHoc.Name = "lookUp_KhoaHoc";
            this.lookUp_KhoaHoc.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lookUp_KhoaHoc.Properties.Appearance.Options.UseFont = true;
            this.lookUp_KhoaHoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUp_KhoaHoc.Properties.PopupFormSize = new System.Drawing.Size(460, 400);
            this.lookUp_KhoaHoc.Properties.PopupView = this.view_KhoaHoc;
            this.lookUp_KhoaHoc.Properties.DisplayMember = "TenKhoaHoc";
            this.lookUp_KhoaHoc.Properties.NullText = "Chọn khóa học...";
            this.lookUp_KhoaHoc.Properties.ValueMember = "KhoaHocID";
            this.lookUp_KhoaHoc.Size = new System.Drawing.Size(326, 24);
            this.lookUp_KhoaHoc.TabIndex = 0;
            this.lookUp_KhoaHoc.EditValueChanged += new System.EventHandler(this.lookUp_KhoaHoc_EditValueChanged);
            //
            // lbl_KhoaHoc
            //
            this.lbl_KhoaHoc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_KhoaHoc.Appearance.Options.UseFont = true;
            this.lbl_KhoaHoc.Location = new System.Drawing.Point(10, 16);
            this.lbl_KhoaHoc.Name = "lbl_KhoaHoc";
            this.lbl_KhoaHoc.Size = new System.Drawing.Size(54, 16);
            this.lbl_KhoaHoc.TabIndex = 2;
            this.lbl_KhoaHoc.Text = "Khóa học";
            //
            // tbl_Cabins
            //
            this.tbl_Cabins.ColumnCount = 2;
            this.tbl_Cabins.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_Cabins.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_Cabins.Controls.Add(this.card_Cabin1, 0, 0);
            this.tbl_Cabins.Controls.Add(this.card_Cabin2, 1, 0);
            this.tbl_Cabins.Controls.Add(this.card_Cabin3, 0, 1);
            this.tbl_Cabins.Controls.Add(this.card_Cabin4, 1, 1);
            this.tbl_Cabins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_Cabins.Location = new System.Drawing.Point(472, 74);
            this.tbl_Cabins.Name = "tbl_Cabins";
            this.tbl_Cabins.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.tbl_Cabins.RowCount = 2;
            this.tbl_Cabins.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_Cabins.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_Cabins.Size = new System.Drawing.Size(1016, 814);
            this.tbl_Cabins.TabIndex = 2;
            //
            // card_Cabin1
            //
            this.card_Cabin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card_Cabin1.Location = new System.Drawing.Point(12, 3);
            this.card_Cabin1.Margin = new System.Windows.Forms.Padding(6, 3, 6, 6);
            this.card_Cabin1.Name = "card_Cabin1";
            this.card_Cabin1.Size = new System.Drawing.Size(493, 398);
            this.card_Cabin1.TabIndex = 0;
            //
            // card_Cabin2
            //
            this.card_Cabin2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card_Cabin2.Location = new System.Drawing.Point(517, 3);
            this.card_Cabin2.Margin = new System.Windows.Forms.Padding(6, 3, 0, 6);
            this.card_Cabin2.Name = "card_Cabin2";
            this.card_Cabin2.Size = new System.Drawing.Size(499, 398);
            this.card_Cabin2.TabIndex = 1;
            //
            // card_Cabin3
            //
            this.card_Cabin3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card_Cabin3.Location = new System.Drawing.Point(12, 410);
            this.card_Cabin3.Margin = new System.Windows.Forms.Padding(6, 3, 6, 0);
            this.card_Cabin3.Name = "card_Cabin3";
            this.card_Cabin3.Size = new System.Drawing.Size(493, 404);
            this.card_Cabin3.TabIndex = 2;
            //
            // card_Cabin4
            //
            this.card_Cabin4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card_Cabin4.Location = new System.Drawing.Point(517, 410);
            this.card_Cabin4.Margin = new System.Windows.Forms.Padding(6, 3, 0, 0);
            this.card_Cabin4.Name = "card_Cabin4";
            this.card_Cabin4.Size = new System.Drawing.Size(499, 404);
            this.card_Cabin4.TabIndex = 3;
            //
            // Uc_CabinManager
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(1500, 900);
            this.Controls.Add(this.tbl_Cabins);
            this.Controls.Add(this.grp_HocVien);
            this.Controls.Add(this.pnl_Header);
            this.Name = "Uc_CabinManager";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Load += new System.EventHandler(this.Uc_CabinManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_HocVien)).EndInit();
            this.grp_HocVien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_HocVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_HocVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Assign)).EndInit();
            this.pnl_Assign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbo_Cabin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_KhoaHoc)).EndInit();
            this.pnl_KhoaHoc.ResumeLayout(false);
            this.pnl_KhoaHoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUp_KhoaHoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view_KhoaHoc)).EndInit();
            this.tbl_Cabins.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.LabelControl lbl_Desc;
        private DevExpress.XtraEditors.LabelControl lbl_Server;
        private DevExpress.XtraEditors.GroupControl grp_HocVien;
        private DevExpress.XtraEditors.PanelControl pnl_KhoaHoc;
        private DevExpress.XtraEditors.LabelControl lbl_KhoaHoc;
        private DevExpress.XtraEditors.SearchLookUpEdit lookUp_KhoaHoc;
        private DevExpress.XtraGrid.Views.Grid.GridView view_KhoaHoc;
        private DevExpress.XtraGrid.Columns.GridColumn col_view_KhoaHoc_MaKhoaHoc;
        private DevExpress.XtraGrid.Columns.GridColumn col_view_KhoaHoc_TenKhoaHoc;
        private DevExpress.XtraGrid.Columns.GridColumn col_view_KhoaHoc_HangDaoTao;
        private DevExpress.XtraEditors.SimpleButton btn_Refresh;
        private DevExpress.XtraGrid.GridControl gridControl_HocVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_HocVien;
        private DevExpress.XtraGrid.Columns.GridColumn col_HoTenHV;
        private DevExpress.XtraGrid.Columns.GridColumn col_SoCMT;
        private DevExpress.XtraGrid.Columns.GridColumn col_HangGPLX;
        private DevExpress.XtraGrid.Columns.GridColumn col_ToTalSecond;
        private DevExpress.XtraGrid.Columns.GridColumn col_Cabin;
        private DevExpress.XtraEditors.PanelControl pnl_Assign;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_Cabin;
        private DevExpress.XtraEditors.SimpleButton btn_Assign;
        private DevExpress.XtraEditors.SimpleButton btn_AutoAssign;
        private System.Windows.Forms.TableLayoutPanel tbl_Cabins;
        private DrivingCenter.App.UserControl.UcCabinClientCard card_Cabin1;
        private DrivingCenter.App.UserControl.UcCabinClientCard card_Cabin2;
        private DrivingCenter.App.UserControl.UcCabinClientCard card_Cabin3;
        private DrivingCenter.App.UserControl.UcCabinClientCard card_Cabin4;
    }
}
