namespace DrivingCenter.App.UserControl
{
    partial class Uc_LichHoc
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
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_LichHoc));
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            this.tileBar = new DevExpress.XtraBars.Navigation.TileBar();
            this.tileBarGroupTables = new DevExpress.XtraBars.Navigation.TileBarGroup();
            this.employeesTileBarItem = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.customersTileBarItem = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.navigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.customersNavigationPage = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPage1 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.tblLayout_LichHoc = new System.Windows.Forms.TableLayoutPanel();
            this.gridCtrl_LichHoc = new DevExpress.XtraGrid.GridControl();
            this.gridView_LichHoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.treeList_Category = new DevExpress.XtraTreeList.TreeList();
            this.navigationPage2 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.tblLayout_ImportData = new System.Windows.Forms.TableLayoutPanel();
            this.grdCtrl_ImportData = new DevExpress.XtraGrid.GridControl();
            this.gridView_ImportData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cb_SheetData = new System.Windows.Forms.ComboBox();
            this.btn_Import = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_ReadData = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).BeginInit();
            this.navigationFrame.SuspendLayout();
            this.navigationPage1.SuspendLayout();
            this.tblLayout_LichHoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_LichHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LichHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList_Category)).BeginInit();
            this.navigationPage2.SuspendLayout();
            this.tblLayout_ImportData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrl_ImportData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_ImportData)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // tileBar
            // 
            this.tileBar.AllowGlyphSkinning = true;
            this.tileBar.AllowSelectedItem = true;
            this.tileBar.AppearanceGroupText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.tileBar.AppearanceGroupText.Options.UseForeColor = true;
            this.tileBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tileBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileBar.DropDownButtonWidth = 30;
            this.tileBar.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileBar.Groups.Add(this.tileBarGroupTables);
            this.tileBar.IndentBetweenGroups = 10;
            this.tileBar.IndentBetweenItems = 10;
            this.tileBar.ItemPadding = new System.Windows.Forms.Padding(8, 6, 12, 6);
            this.tileBar.Location = new System.Drawing.Point(0, 0);
            this.tileBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tileBar.MaxId = 3;
            this.tileBar.MaximumSize = new System.Drawing.Size(0, 110);
            this.tileBar.MinimumSize = new System.Drawing.Size(100, 110);
            this.tileBar.Name = "tileBar";
            this.tileBar.Padding = new System.Windows.Forms.Padding(29, 11, 29, 11);
            this.tileBar.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.None;
            this.tileBar.SelectedItem = this.employeesTileBarItem;
            this.tileBar.SelectionBorderWidth = 2;
            this.tileBar.SelectionColorMode = DevExpress.XtraBars.Navigation.SelectionColorMode.UseItemBackColor;
            this.tileBar.ShowGroupText = false;
            this.tileBar.Size = new System.Drawing.Size(1500, 110);
            this.tileBar.TabIndex = 1;
            this.tileBar.Text = "tileBar";
            this.tileBar.WideTileWidth = 150;
            this.tileBar.SelectedItemChanged += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileBar_SelectedItemChanged);
            // 
            // tileBarGroupTables
            // 
            this.tileBarGroupTables.Items.Add(this.employeesTileBarItem);
            this.tileBarGroupTables.Items.Add(this.customersTileBarItem);
            this.tileBarGroupTables.Name = "tileBarGroupTables";
            this.tileBarGroupTables.Text = "TABLES";
            // 
            // employeesTileBarItem
            // 
            this.employeesTileBarItem.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(196)))));
            this.employeesTileBarItem.AppearanceItem.Normal.Options.UseBackColor = true;
            this.employeesTileBarItem.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement3.ImageOptions.ImageUri.Uri = "Cube;Size32x32;GrayScaled";
            tileItemElement3.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("resource.SvgImage")));
            tileItemElement3.Text = "Lịch Học";
            this.employeesTileBarItem.Elements.Add(tileItemElement3);
            this.employeesTileBarItem.Name = "employeesTileBarItem";
            // 
            // customersTileBarItem
            // 
            this.customersTileBarItem.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement4.ImageOptions.ImageUri.Uri = "Cube;Size32x32;GrayScaled";
            tileItemElement4.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("resource.SvgImage1")));
            tileItemElement4.Text = "Phân Công";
            this.customersTileBarItem.Elements.Add(tileItemElement4);
            this.customersTileBarItem.Id = 2;
            this.customersTileBarItem.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Wide;
            this.customersTileBarItem.Name = "customersTileBarItem";
            // 
            // navigationFrame
            // 
            this.navigationFrame.Controls.Add(this.customersNavigationPage);
            this.navigationFrame.Controls.Add(this.navigationPage1);
            this.navigationFrame.Controls.Add(this.navigationPage2);
            this.navigationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrame.Location = new System.Drawing.Point(0, 110);
            this.navigationFrame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.navigationFrame.Name = "navigationFrame";
            this.navigationFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navigationPage1,
            this.navigationPage2});
            this.navigationFrame.SelectedPage = this.navigationPage1;
            this.navigationFrame.Size = new System.Drawing.Size(1500, 690);
            this.navigationFrame.TabIndex = 0;
            this.navigationFrame.Text = "navigationFrame";
            // 
            // customersNavigationPage
            // 
            this.customersNavigationPage.Caption = "customersNavigationPage";
            this.customersNavigationPage.Name = "customersNavigationPage";
            this.customersNavigationPage.Size = new System.Drawing.Size(800, 490);
            // 
            // navigationPage1
            // 
            this.navigationPage1.AutoScroll = true;
            this.navigationPage1.Caption = "navigationPage1";
            this.navigationPage1.Controls.Add(this.tblLayout_LichHoc);
            this.navigationPage1.Name = "navigationPage1";
            this.navigationPage1.Size = new System.Drawing.Size(1500, 690);
            // 
            // tblLayout_LichHoc
            // 
            this.tblLayout_LichHoc.ColumnCount = 2;
            this.tblLayout_LichHoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tblLayout_LichHoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout_LichHoc.Controls.Add(this.gridCtrl_LichHoc, 1, 1);
            this.tblLayout_LichHoc.Controls.Add(this.treeList_Category, 0, 1);
            this.tblLayout_LichHoc.Location = new System.Drawing.Point(3, 8);
            this.tblLayout_LichHoc.Name = "tblLayout_LichHoc";
            this.tblLayout_LichHoc.RowCount = 2;
            this.tblLayout_LichHoc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLayout_LichHoc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout_LichHoc.Size = new System.Drawing.Size(1462, 679);
            this.tblLayout_LichHoc.TabIndex = 1;
            // 
            // gridCtrl_LichHoc
            // 
            this.gridCtrl_LichHoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCtrl_LichHoc.Location = new System.Drawing.Point(283, 53);
            this.gridCtrl_LichHoc.MainView = this.gridView_LichHoc;
            this.gridCtrl_LichHoc.Name = "gridCtrl_LichHoc";
            this.gridCtrl_LichHoc.Size = new System.Drawing.Size(1176, 623);
            this.gridCtrl_LichHoc.TabIndex = 0;
            this.gridCtrl_LichHoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_LichHoc});
            // 
            // gridView_LichHoc
            // 
            this.gridView_LichHoc.GridControl = this.gridCtrl_LichHoc;
            this.gridView_LichHoc.Name = "gridView_LichHoc";
            // 
            // treeList_Category
            // 
            this.treeList_Category.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList_Category.Appearance.Row.Options.UseFont = true;
            this.treeList_Category.Appearance.SelectedRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList_Category.Appearance.SelectedRow.Options.UseFont = true;
            this.treeList_Category.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList_Category.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList_Category.KeyFieldName = "MonthID";
            this.treeList_Category.Location = new System.Drawing.Point(3, 53);
            this.treeList_Category.Name = "treeList_Category";
            this.treeList_Category.OptionsBehavior.ReadOnly = true;
            this.treeList_Category.OptionsFilter.ExpandNodesOnFiltering = true;
            this.treeList_Category.OptionsFind.ExpandNodesOnIncrementalSearch = true;
            this.treeList_Category.OptionsView.ShowIndicator = false;
            this.treeList_Category.ParentFieldName = "ParentMonthID";
            this.treeList_Category.Size = new System.Drawing.Size(274, 623);
            this.treeList_Category.TabIndex = 1;
            this.treeList_Category.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList_Category_FocusedNodeChanged);
            // 
            // navigationPage2
            // 
            this.navigationPage2.Caption = "navigationPage2";
            this.navigationPage2.Controls.Add(this.tblLayout_ImportData);
            this.navigationPage2.Name = "navigationPage2";
            this.navigationPage2.Size = new System.Drawing.Size(1500, 690);
            // 
            // tblLayout_ImportData
            // 
            this.tblLayout_ImportData.ColumnCount = 1;
            this.tblLayout_ImportData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout_ImportData.Controls.Add(this.grdCtrl_ImportData, 0, 0);
            this.tblLayout_ImportData.Controls.Add(this.panel1, 0, 1);
            this.tblLayout_ImportData.Location = new System.Drawing.Point(3, 0);
            this.tblLayout_ImportData.Name = "tblLayout_ImportData";
            this.tblLayout_ImportData.RowCount = 2;
            this.tblLayout_ImportData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout_ImportData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblLayout_ImportData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLayout_ImportData.Size = new System.Drawing.Size(1478, 690);
            this.tblLayout_ImportData.TabIndex = 2;
            // 
            // grdCtrl_ImportData
            // 
            this.grdCtrl_ImportData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCtrl_ImportData.Location = new System.Drawing.Point(3, 3);
            this.grdCtrl_ImportData.MainView = this.gridView_ImportData;
            this.grdCtrl_ImportData.Name = "grdCtrl_ImportData";
            this.grdCtrl_ImportData.Size = new System.Drawing.Size(1472, 614);
            this.grdCtrl_ImportData.TabIndex = 0;
            this.grdCtrl_ImportData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_ImportData});
            // 
            // gridView_ImportData
            // 
            this.gridView_ImportData.GridControl = this.grdCtrl_ImportData;
            this.gridView_ImportData.Name = "gridView_ImportData";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.btn_Import);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btn_ReadData);
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 623);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1472, 64);
            this.panel1.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.cb_SheetData);
            this.panel5.Location = new System.Drawing.Point(950, 17);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(140, 30);
            this.panel5.TabIndex = 22;
            // 
            // cb_SheetData
            // 
            this.cb_SheetData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_SheetData.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_SheetData.FormattingEnabled = true;
            this.cb_SheetData.Location = new System.Drawing.Point(3, 2);
            this.cb_SheetData.Name = "cb_SheetData";
            this.cb_SheetData.Size = new System.Drawing.Size(134, 25);
            this.cb_SheetData.TabIndex = 5;
            this.cb_SheetData.SelectedIndexChanged += new System.EventHandler(this.cb_SheetData_SelectedIndexChanged);
            // 
            // btn_Import
            // 
            this.btn_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Import.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(60)))), ((int)(((byte)(94)))));
            this.btn_Import.FlatAppearance.BorderSize = 0;
            this.btn_Import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Import.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Import.ForeColor = System.Drawing.Color.White;
            this.btn_Import.Location = new System.Drawing.Point(1295, 17);
            this.btn_Import.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(140, 30);
            this.btn_Import.TabIndex = 20;
            this.btn_Import.Text = "Assignment";
            this.btn_Import.UseVisualStyleBackColor = false;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(22, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "File name";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(863, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Sheet data";
            // 
            // btn_ReadData
            // 
            this.btn_ReadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ReadData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(60)))), ((int)(((byte)(94)))));
            this.btn_ReadData.FlatAppearance.BorderSize = 0;
            this.btn_ReadData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ReadData.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ReadData.ForeColor = System.Drawing.Color.White;
            this.btn_ReadData.Location = new System.Drawing.Point(1132, 17);
            this.btn_ReadData.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ReadData.Name = "btn_ReadData";
            this.btn_ReadData.Size = new System.Drawing.Size(140, 30);
            this.btn_ReadData.TabIndex = 21;
            this.btn_ReadData.Text = "Read Data";
            this.btn_ReadData.UseVisualStyleBackColor = false;
            this.btn_ReadData.Click += new System.EventHandler(this.btn_ReadData_Click);
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel11.BackColor = System.Drawing.Color.White;
            this.panel11.Controls.Add(this.txtFilePath);
            this.panel11.Location = new System.Drawing.Point(110, 17);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(722, 30);
            this.panel11.TabIndex = 11;
            // 
            // txtFilePath
            // 
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFilePath.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(8, 7);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(704, 16);
            this.txtFilePath.TabIndex = 0;
            // 
            // Uc_LichHoc
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navigationFrame);
            this.Controls.Add(this.tileBar);
            this.Name = "Uc_LichHoc";
            this.Size = new System.Drawing.Size(1500, 800);
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).EndInit();
            this.navigationFrame.ResumeLayout(false);
            this.navigationPage1.ResumeLayout(false);
            this.tblLayout_LichHoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCtrl_LichHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LichHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList_Category)).EndInit();
            this.navigationPage2.ResumeLayout(false);
            this.tblLayout_ImportData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrl_ImportData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_ImportData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TileBar tileBar;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame;
        private DevExpress.XtraBars.Navigation.TileBarGroup tileBarGroupTables;
        private DevExpress.XtraBars.Navigation.TileBarItem employeesTileBarItem;
        private DevExpress.XtraBars.Navigation.TileBarItem customersTileBarItem;
        private DevExpress.XtraBars.Navigation.NavigationPage customersNavigationPage;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage1;
        private DevExpress.XtraGrid.GridControl gridCtrl_LichHoc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_LichHoc;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage2;
        private System.Windows.Forms.TableLayoutPanel tblLayout_ImportData;
        private DevExpress.XtraGrid.GridControl grdCtrl_ImportData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_ImportData;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cb_SheetData;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Button btn_ReadData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tblLayout_LichHoc;
        private DevExpress.XtraTreeList.TreeList treeList_Category;
    }
}
