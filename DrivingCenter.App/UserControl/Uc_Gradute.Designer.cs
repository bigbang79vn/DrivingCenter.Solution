namespace DrivingCenter.App.UserControl
{
    partial class Uc_Gradute
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_Gradute));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.KetQua_LT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Tab_Control = new System.Windows.Forms.TabControl();
            this.KhoaThiTotNghiep = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dtgv_khoahocdangthi = new System.Windows.Forms.DataGridView();
            this.TenKhoaHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDKhoaHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mn_Gradute = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mn_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mn_DanhSachHocVienKetThucMon = new System.Windows.Forms.ToolStripMenuItem();
            this.mn_DanhSachHocVienThiTotNghiep = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mn_XuatDanhSach = new System.Windows.Forms.ToolStripMenuItem();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl_ThiSinh = new DevExpress.XtraGrid.GridControl();
            this.gridView_ThiSinh = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.STT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IDThiSinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoBaoDanh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HocVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CCCD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HangGPLX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DiemLyThuyet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DiemMoPhong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KetQua_MP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Insert = new System.Windows.Forms.Button();
            this.btn_ReadData = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Tab_Control.SuspendLayout();
            this.KhoaThiTotNghiep.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_khoahocdangthi)).BeginInit();
            this.mn_Gradute.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_ThiSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_ThiSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // KetQua_LT
            // 
            this.KetQua_LT.AppearanceCell.Options.UseTextOptions = true;
            this.KetQua_LT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KetQua_LT.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.KetQua_LT.AppearanceHeader.Options.UseBackColor = true;
            this.KetQua_LT.Caption = "Kết Quả LT";
            this.KetQua_LT.FieldName = "Result_LT";
            this.KetQua_LT.Name = "KetQua_LT";
            this.KetQua_LT.Visible = true;
            this.KetQua_LT.VisibleIndex = 7;
            this.KetQua_LT.Width = 121;
            // 
            // Tab_Control
            // 
            this.Tab_Control.Controls.Add(this.KhoaThiTotNghiep);
            this.Tab_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Control.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab_Control.ItemSize = new System.Drawing.Size(110, 35);
            this.Tab_Control.Location = new System.Drawing.Point(0, 0);
            this.Tab_Control.Name = "Tab_Control";
            this.Tab_Control.Padding = new System.Drawing.Point(8, 5);
            this.Tab_Control.SelectedIndex = 0;
            this.Tab_Control.Size = new System.Drawing.Size(1500, 800);
            this.Tab_Control.TabIndex = 3;
            // 
            // KhoaThiTotNghiep
            // 
            this.KhoaThiTotNghiep.Controls.Add(this.tableLayoutPanel1);
            this.KhoaThiTotNghiep.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Underline);
            this.KhoaThiTotNghiep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(60)))), ((int)(((byte)(94)))));
            this.KhoaThiTotNghiep.ImageIndex = 0;
            this.KhoaThiTotNghiep.Location = new System.Drawing.Point(4, 39);
            this.KhoaThiTotNghiep.Margin = new System.Windows.Forms.Padding(0);
            this.KhoaThiTotNghiep.Name = "KhoaThiTotNghiep";
            this.KhoaThiTotNghiep.Size = new System.Drawing.Size(1492, 757);
            this.KhoaThiTotNghiep.TabIndex = 1;
            this.KhoaThiTotNghiep.Text = "Khóa Thi Tốt Nghiệp";
            this.KhoaThiTotNghiep.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 757F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1492, 757);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1486, 751);
            this.panel3.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 252F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dtgv_khoahocdangthi, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel7, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1486, 751);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // dtgv_khoahocdangthi
            // 
            this.dtgv_khoahocdangthi.AllowUserToAddRows = false;
            this.dtgv_khoahocdangthi.AllowUserToDeleteRows = false;
            this.dtgv_khoahocdangthi.AllowUserToResizeColumns = false;
            this.dtgv_khoahocdangthi.AllowUserToResizeRows = false;
            this.dtgv_khoahocdangthi.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dtgv_khoahocdangthi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgv_khoahocdangthi.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgv_khoahocdangthi.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgv_khoahocdangthi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgv_khoahocdangthi.ColumnHeadersHeight = 35;
            this.dtgv_khoahocdangthi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgv_khoahocdangthi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenKhoaHoc,
            this.IDKhoaHoc});
            this.dtgv_khoahocdangthi.ContextMenuStrip = this.mn_Gradute;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(60)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgv_khoahocdangthi.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgv_khoahocdangthi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgv_khoahocdangthi.EnableHeadersVisualStyles = false;
            this.dtgv_khoahocdangthi.GridColor = System.Drawing.Color.DarkGray;
            this.dtgv_khoahocdangthi.Location = new System.Drawing.Point(3, 3);
            this.dtgv_khoahocdangthi.MultiSelect = false;
            this.dtgv_khoahocdangthi.Name = "dtgv_khoahocdangthi";
            this.dtgv_khoahocdangthi.ReadOnly = true;
            this.dtgv_khoahocdangthi.RowHeadersVisible = false;
            this.dtgv_khoahocdangthi.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dtgv_khoahocdangthi.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgv_khoahocdangthi.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dtgv_khoahocdangthi.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Crimson;
            this.dtgv_khoahocdangthi.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dtgv_khoahocdangthi.RowTemplate.Height = 35;
            this.dtgv_khoahocdangthi.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgv_khoahocdangthi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgv_khoahocdangthi.Size = new System.Drawing.Size(246, 745);
            this.dtgv_khoahocdangthi.TabIndex = 7;
            this.dtgv_khoahocdangthi.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgv_khoahocdangthi_CellDoubleClick);
            // 
            // TenKhoaHoc
            // 
            this.TenKhoaHoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenKhoaHoc.DataPropertyName = "TenKhoaHoc";
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.TenKhoaHoc.DefaultCellStyle = dataGridViewCellStyle2;
            this.TenKhoaHoc.HeaderText = "Kỳ Thi Tốt Nghiệp";
            this.TenKhoaHoc.Name = "TenKhoaHoc";
            this.TenKhoaHoc.ReadOnly = true;
            // 
            // IDKhoaHoc
            // 
            this.IDKhoaHoc.DataPropertyName = "IDKhoaHoc";
            this.IDKhoaHoc.HeaderText = "IDKhoaHoc";
            this.IDKhoaHoc.Name = "IDKhoaHoc";
            this.IDKhoaHoc.ReadOnly = true;
            this.IDKhoaHoc.Visible = false;
            this.IDKhoaHoc.Width = 300;
            // 
            // mn_Gradute
            // 
            this.mn_Gradute.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mn_Refresh,
            this.toolStripSeparator2,
            this.mn_DanhSachHocVienKetThucMon,
            this.mn_DanhSachHocVienThiTotNghiep,
            this.toolStripSeparator1,
            this.mn_XuatDanhSach});
            this.mn_Gradute.Name = "mn_Gradute";
            this.mn_Gradute.Size = new System.Drawing.Size(253, 104);
            // 
            // mn_Refresh
            // 
            this.mn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("mn_Refresh.Image")));
            this.mn_Refresh.Name = "mn_Refresh";
            this.mn_Refresh.Size = new System.Drawing.Size(252, 22);
            this.mn_Refresh.Text = "Refresh";
            this.mn_Refresh.Click += new System.EventHandler(this.mn_Refresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(249, 6);
            // 
            // mn_DanhSachHocVienKetThucMon
            // 
            this.mn_DanhSachHocVienKetThucMon.Image = ((System.Drawing.Image)(resources.GetObject("mn_DanhSachHocVienKetThucMon.Image")));
            this.mn_DanhSachHocVienKetThucMon.Name = "mn_DanhSachHocVienKetThucMon";
            this.mn_DanhSachHocVienKetThucMon.Size = new System.Drawing.Size(252, 22);
            this.mn_DanhSachHocVienKetThucMon.Text = "Danh sách học viên kết thúc môn";
            this.mn_DanhSachHocVienKetThucMon.Click += new System.EventHandler(this.mn_DanhSachHocVienKetThucMon_Click);
            // 
            // mn_DanhSachHocVienThiTotNghiep
            // 
            this.mn_DanhSachHocVienThiTotNghiep.Image = ((System.Drawing.Image)(resources.GetObject("mn_DanhSachHocVienThiTotNghiep.Image")));
            this.mn_DanhSachHocVienThiTotNghiep.Name = "mn_DanhSachHocVienThiTotNghiep";
            this.mn_DanhSachHocVienThiTotNghiep.Size = new System.Drawing.Size(252, 22);
            this.mn_DanhSachHocVienThiTotNghiep.Text = "Danh sách học viên thi tốt nghiệp";
            this.mn_DanhSachHocVienThiTotNghiep.Click += new System.EventHandler(this.mn_DanhSachHocVienThiTotNghiep_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
            // 
            // mn_XuatDanhSach
            // 
            this.mn_XuatDanhSach.Image = ((System.Drawing.Image)(resources.GetObject("mn_XuatDanhSach.Image")));
            this.mn_XuatDanhSach.Name = "mn_XuatDanhSach";
            this.mn_XuatDanhSach.Size = new System.Drawing.Size(252, 22);
            this.mn_XuatDanhSach.Text = "Export kết quả";
            this.mn_XuatDanhSach.Click += new System.EventHandler(this.mn_XuatDanhSach_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tableLayoutPanel5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(252, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1234, 751);
            this.panel7.TabIndex = 8;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.gridControl_ThiSinh, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1234, 751);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // gridControl_ThiSinh
            // 
            this.gridControl_ThiSinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_ThiSinh.Location = new System.Drawing.Point(0, 3);
            this.gridControl_ThiSinh.MainView = this.gridView_ThiSinh;
            this.gridControl_ThiSinh.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.gridControl_ThiSinh.Name = "gridControl_ThiSinh";
            this.gridControl_ThiSinh.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControl_ThiSinh.Size = new System.Drawing.Size(1234, 673);
            this.gridControl_ThiSinh.TabIndex = 2;
            this.gridControl_ThiSinh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_ThiSinh});
            // 
            // gridView_ThiSinh
            // 
            this.gridView_ThiSinh.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_ThiSinh.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_ThiSinh.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_ThiSinh.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_ThiSinh.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_ThiSinh.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_ThiSinh.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_ThiSinh.Appearance.Row.Options.UseFont = true;
            this.gridView_ThiSinh.ColumnPanelRowHeight = 35;
            this.gridView_ThiSinh.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.STT,
            this.IDThiSinh,
            this.MaDK,
            this.SoBaoDanh,
            this.HocVien,
            this.NgaySinh,
            this.CCCD,
            this.HangGPLX,
            this.DiemLyThuyet,
            this.KetQua_LT,
            this.DiemMoPhong,
            this.KetQua_MP});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.KetQua_LT;
            gridFormatRule1.Name = "FormatUnLogout";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleValue1.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Appearance.Options.UseFont = true;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = "False";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gridView_ThiSinh.FormatRules.Add(gridFormatRule1);
            this.gridView_ThiSinh.GridControl = this.gridControl_ThiSinh;
            this.gridView_ThiSinh.IndicatorWidth = 45;
            this.gridView_ThiSinh.Name = "gridView_ThiSinh";
            this.gridView_ThiSinh.OptionsBehavior.ReadOnly = true;
            this.gridView_ThiSinh.OptionsCustomization.CustomizationFormSearchBoxVisible = true;
            this.gridView_ThiSinh.OptionsFind.FindNullPrompt = "Nhập từ khóa để tìm kiếm...";
            this.gridView_ThiSinh.OptionsFind.SearchInPreview = true;
            this.gridView_ThiSinh.RowHeight = 28;
            this.gridView_ThiSinh.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_ThiSinh_CustomDrawRowIndicator);
            // 
            // STT
            // 
            this.STT.AppearanceCell.Options.UseTextOptions = true;
            this.STT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.STT.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.STT.AppearanceHeader.ForeColor = System.Drawing.Color.White;
            this.STT.AppearanceHeader.Options.UseBackColor = true;
            this.STT.AppearanceHeader.Options.UseForeColor = true;
            this.STT.Caption = "STT";
            this.STT.FieldName = "STT";
            this.STT.Name = "STT";
            this.STT.Width = 40;
            // 
            // IDThiSinh
            // 
            this.IDThiSinh.Caption = "ID Thí Sinh";
            this.IDThiSinh.FieldName = "IDThiSinh";
            this.IDThiSinh.Name = "IDThiSinh";
            // 
            // MaDK
            // 
            this.MaDK.AppearanceCell.Options.UseTextOptions = true;
            this.MaDK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MaDK.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.MaDK.AppearanceHeader.Options.UseBackColor = true;
            this.MaDK.Caption = "Mã Đăng Ký";
            this.MaDK.FieldName = "MaDK";
            this.MaDK.Name = "MaDK";
            this.MaDK.Visible = true;
            this.MaDK.VisibleIndex = 0;
            this.MaDK.Width = 182;
            // 
            // SoBaoDanh
            // 
            this.SoBaoDanh.AppearanceCell.Options.UseTextOptions = true;
            this.SoBaoDanh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SoBaoDanh.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.SoBaoDanh.AppearanceHeader.Options.UseBackColor = true;
            this.SoBaoDanh.Caption = "SBD";
            this.SoBaoDanh.FieldName = "SoBaoDanh";
            this.SoBaoDanh.Name = "SoBaoDanh";
            this.SoBaoDanh.Visible = true;
            this.SoBaoDanh.VisibleIndex = 1;
            this.SoBaoDanh.Width = 59;
            // 
            // HocVien
            // 
            this.HocVien.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.HocVien.AppearanceHeader.Options.UseBackColor = true;
            this.HocVien.Caption = "Học Viên";
            this.HocVien.FieldName = "HoVaTen";
            this.HocVien.Name = "HocVien";
            this.HocVien.Visible = true;
            this.HocVien.VisibleIndex = 2;
            this.HocVien.Width = 214;
            // 
            // NgaySinh
            // 
            this.NgaySinh.AppearanceCell.Options.UseTextOptions = true;
            this.NgaySinh.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NgaySinh.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.NgaySinh.AppearanceHeader.Options.UseBackColor = true;
            this.NgaySinh.Caption = "Ngày Sinh";
            this.NgaySinh.FieldName = "NgaySinh";
            this.NgaySinh.Name = "NgaySinh";
            this.NgaySinh.Visible = true;
            this.NgaySinh.VisibleIndex = 3;
            this.NgaySinh.Width = 99;
            // 
            // CCCD
            // 
            this.CCCD.AppearanceCell.Options.UseTextOptions = true;
            this.CCCD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CCCD.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.CCCD.AppearanceHeader.Options.UseBackColor = true;
            this.CCCD.Caption = "CMT/CCCD";
            this.CCCD.FieldName = "SoCMT";
            this.CCCD.Name = "CCCD";
            this.CCCD.Visible = true;
            this.CCCD.VisibleIndex = 4;
            this.CCCD.Width = 99;
            // 
            // HangGPLX
            // 
            this.HangGPLX.AppearanceCell.Options.UseTextOptions = true;
            this.HangGPLX.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.HangGPLX.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.HangGPLX.AppearanceHeader.Options.UseBackColor = true;
            this.HangGPLX.Caption = "Hạng GPLX";
            this.HangGPLX.FieldName = "HangGPLX";
            this.HangGPLX.Name = "HangGPLX";
            this.HangGPLX.Visible = true;
            this.HangGPLX.VisibleIndex = 5;
            this.HangGPLX.Width = 77;
            // 
            // DiemLyThuyet
            // 
            this.DiemLyThuyet.AppearanceCell.Options.UseTextOptions = true;
            this.DiemLyThuyet.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DiemLyThuyet.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.DiemLyThuyet.AppearanceHeader.Options.UseBackColor = true;
            this.DiemLyThuyet.Caption = "Điểm Lý Thuyết";
            this.DiemLyThuyet.FieldName = "DiemLT";
            this.DiemLyThuyet.Name = "DiemLyThuyet";
            this.DiemLyThuyet.Visible = true;
            this.DiemLyThuyet.VisibleIndex = 6;
            this.DiemLyThuyet.Width = 111;
            // 
            // DiemMoPhong
            // 
            this.DiemMoPhong.AppearanceCell.Options.UseTextOptions = true;
            this.DiemMoPhong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DiemMoPhong.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.DiemMoPhong.AppearanceHeader.Options.UseBackColor = true;
            this.DiemMoPhong.Caption = "Điểm Mô Phỏng";
            this.DiemMoPhong.FieldName = "DiemMP";
            this.DiemMoPhong.Name = "DiemMoPhong";
            this.DiemMoPhong.Visible = true;
            this.DiemMoPhong.VisibleIndex = 8;
            this.DiemMoPhong.Width = 115;
            // 
            // KetQua_MP
            // 
            this.KetQua_MP.AppearanceCell.Options.UseTextOptions = true;
            this.KetQua_MP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.KetQua_MP.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.KetQua_MP.AppearanceHeader.Options.UseBackColor = true;
            this.KetQua_MP.Caption = "Kết Quả MP";
            this.KetQua_MP.FieldName = "Result_MP";
            this.KetQua_MP.Name = "KetQua_MP";
            this.KetQua_MP.Visible = true;
            this.KetQua_MP.VisibleIndex = 9;
            this.KetQua_MP.Width = 115;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.btn_Insert);
            this.panel1.Controls.Add(this.btn_ReadData);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 679);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1228, 69);
            this.panel1.TabIndex = 1;
            // 
            // btn_Insert
            // 
            this.btn_Insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Insert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.btn_Insert.FlatAppearance.BorderSize = 0;
            this.btn_Insert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Insert.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Insert.ForeColor = System.Drawing.Color.White;
            this.btn_Insert.Location = new System.Drawing.Point(1099, 19);
            this.btn_Insert.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Insert.Name = "btn_Insert";
            this.btn_Insert.Size = new System.Drawing.Size(114, 30);
            this.btn_Insert.TabIndex = 11;
            this.btn_Insert.Text = "Insert";
            this.btn_Insert.UseVisualStyleBackColor = false;
            this.btn_Insert.Click += new System.EventHandler(this.btn_Insert_Click);
            // 
            // btn_ReadData
            // 
            this.btn_ReadData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ReadData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.btn_ReadData.FlatAppearance.BorderSize = 0;
            this.btn_ReadData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ReadData.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ReadData.ForeColor = System.Drawing.Color.White;
            this.btn_ReadData.Location = new System.Drawing.Point(960, 20);
            this.btn_ReadData.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ReadData.Name = "btn_ReadData";
            this.btn_ReadData.Size = new System.Drawing.Size(114, 30);
            this.btn_ReadData.TabIndex = 11;
            this.btn_ReadData.Text = "Open";
            this.btn_ReadData.UseVisualStyleBackColor = false;
            this.btn_ReadData.Click += new System.EventHandler(this.btn_ReadData_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.txtFilePath);
            this.panel2.Location = new System.Drawing.Point(159, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(765, 30);
            this.panel2.TabIndex = 9;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.BackColor = System.Drawing.Color.White;
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFilePath.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(13, 6);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(733, 19);
            this.txtFilePath.TabIndex = 0;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "File Path";
            // 
            // Uc_Gradute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Tab_Control);
            this.Name = "Uc_Gradute";
            this.Size = new System.Drawing.Size(1500, 800);
            this.Load += new System.EventHandler(this.Uc_Gradute_Load);
            this.Tab_Control.ResumeLayout(false);
            this.KhoaThiTotNghiep.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_khoahocdangthi)).EndInit();
            this.mn_Gradute.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_ThiSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_ThiSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tab_Control;
        private System.Windows.Forms.TabPage KhoaThiTotNghiep;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridView dtgv_khoahocdangthi;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKhoaHoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDKhoaHoc;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Insert;
        private System.Windows.Forms.Button btn_ReadData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControl_ThiSinh;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_ThiSinh;
        private DevExpress.XtraGrid.Columns.GridColumn STT;
        private DevExpress.XtraGrid.Columns.GridColumn MaDK;
        private DevExpress.XtraGrid.Columns.GridColumn SoBaoDanh;
        private DevExpress.XtraGrid.Columns.GridColumn HocVien;
        private DevExpress.XtraGrid.Columns.GridColumn NgaySinh;
        private DevExpress.XtraGrid.Columns.GridColumn CCCD;
        private DevExpress.XtraGrid.Columns.GridColumn HangGPLX;
        private DevExpress.XtraGrid.Columns.GridColumn DiemLyThuyet;
        private DevExpress.XtraGrid.Columns.GridColumn KetQua_LT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn DiemMoPhong;
        private DevExpress.XtraGrid.Columns.GridColumn KetQua_MP;
        private DevExpress.XtraGrid.Columns.GridColumn IDThiSinh;
        private System.Windows.Forms.ContextMenuStrip mn_Gradute;
        private System.Windows.Forms.ToolStripMenuItem mn_DanhSachHocVienKetThucMon;
        private System.Windows.Forms.ToolStripMenuItem mn_XuatDanhSach;
        private System.Windows.Forms.ToolStripMenuItem mn_DanhSachHocVienThiTotNghiep;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mn_Refresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
