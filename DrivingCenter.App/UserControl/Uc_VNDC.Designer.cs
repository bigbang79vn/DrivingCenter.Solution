namespace DrivingCenter.App.UserControl
{
    partial class Uc_VNDC
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_VNDC));
            this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.skinRibbonGalleryBarItem = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.employeesBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.customersBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.btnKhoaHoc = new DevExpress.XtraBars.BarButtonItem();
            this.btnGiaoVien = new DevExpress.XtraBars.BarButtonItem();
            this.btnXeTapLai = new DevExpress.XtraBars.BarButtonItem();
            this.btnDevice = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportData = new DevExpress.XtraBars.BarButtonItem();
            this.btnXepLop = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuuLuongDT = new DevExpress.XtraBars.BarButtonItem();
            this.btn_DangDaoTao = new DevExpress.XtraBars.BarButtonItem();
            this.btn_PhanCong = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.barSubItemNavigation = new DevExpress.XtraBars.BarSubItem();
            this.MainContainer = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.ribbonControl.SearchEditItem,
            this.skinRibbonGalleryBarItem,
            this.employeesBarButtonItem,
            this.customersBarButtonItem,
            this.btnKhoaHoc,
            this.btnGiaoVien,
            this.btnXeTapLai,
            this.btnDevice,
            this.btnImportData,
            this.btnXepLop,
            this.btnLuuLuongDT,
            this.btn_DangDaoTao,
            this.btn_PhanCong});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 55;
            this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.Size = new System.Drawing.Size(1500, 126);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // skinRibbonGalleryBarItem
            // 
            this.skinRibbonGalleryBarItem.Id = 14;
            this.skinRibbonGalleryBarItem.Name = "skinRibbonGalleryBarItem";
            // 
            // employeesBarButtonItem
            // 
            this.employeesBarButtonItem.Caption = "Employees";
            this.employeesBarButtonItem.Id = 44;
            this.employeesBarButtonItem.Name = "employeesBarButtonItem";
            this.employeesBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonNavigation_ItemClick);
            // 
            // customersBarButtonItem
            // 
            this.customersBarButtonItem.Caption = "Customers";
            this.customersBarButtonItem.Id = 45;
            this.customersBarButtonItem.Name = "customersBarButtonItem";
            this.customersBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonNavigation_ItemClick);
            // 
            // btnKhoaHoc
            // 
            this.btnKhoaHoc.Caption = "Khóa học";
            this.btnKhoaHoc.Id = 46;
            this.btnKhoaHoc.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKhoaHoc.ImageOptions.Image")));
            this.btnKhoaHoc.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnKhoaHoc.ImageOptions.LargeImage")));
            this.btnKhoaHoc.Name = "btnKhoaHoc";
            this.btnKhoaHoc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnKhoaHoc_ItemClick);
            // 
            // btnGiaoVien
            // 
            this.btnGiaoVien.Caption = "Giáo viên";
            this.btnGiaoVien.Id = 47;
            this.btnGiaoVien.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnGiaoVien.ImageOptions.SvgImage")));
            this.btnGiaoVien.Name = "btnGiaoVien";
            this.btnGiaoVien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGiaoVien_ItemClick);
            // 
            // btnXeTapLai
            // 
            this.btnXeTapLai.Caption = "Car";
            this.btnXeTapLai.Id = 48;
            this.btnXeTapLai.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXeTapLai.ImageOptions.Image")));
            this.btnXeTapLai.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnXeTapLai.ImageOptions.LargeImage")));
            this.btnXeTapLai.Name = "btnXeTapLai";
            this.btnXeTapLai.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXeTapLai_ItemClick);
            // 
            // btnDevice
            // 
            this.btnDevice.Caption = "Device";
            this.btnDevice.Id = 49;
            this.btnDevice.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDevice.ImageOptions.SvgImage")));
            this.btnDevice.Name = "btnDevice";
            this.btnDevice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDevice_ItemClick);
            // 
            // btnImportData
            // 
            this.btnImportData.Caption = "Nhập dữ liệu";
            this.btnImportData.Id = 50;
            this.btnImportData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportData.ImageOptions.Image")));
            this.btnImportData.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnImportData.ImageOptions.LargeImage")));
            this.btnImportData.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnImportData.ImageOptions.SvgImage")));
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportData_ItemClick);
            // 
            // btnXepLop
            // 
            this.btnXepLop.Caption = "Xếp lớp";
            this.btnXepLop.Id = 51;
            this.btnXepLop.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnXepLop.ImageOptions.SvgImage")));
            this.btnXepLop.Name = "btnXepLop";
            this.btnXepLop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXepLop_ItemClick);
            // 
            // btnLuuLuongDT
            // 
            this.btnLuuLuongDT.Caption = "Sắp lưu lượng";
            this.btnLuuLuongDT.Id = 52;
            this.btnLuuLuongDT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLuuLuongDT.ImageOptions.Image")));
            this.btnLuuLuongDT.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnLuuLuongDT.ImageOptions.LargeImage")));
            this.btnLuuLuongDT.Name = "btnLuuLuongDT";
            this.btnLuuLuongDT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuuLuongDT_ItemClick);
            // 
            // btn_DangDaoTao
            // 
            this.btn_DangDaoTao.Caption = "Đang đào tạo";
            this.btn_DangDaoTao.Id = 53;
            this.btn_DangDaoTao.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_DangDaoTao.ImageOptions.SvgImage")));
            this.btn_DangDaoTao.Name = "btn_DangDaoTao";
            // 
            // btn_PhanCong
            // 
            this.btn_PhanCong.Caption = "Phân công";
            this.btn_PhanCong.Id = 54;
            this.btn_PhanCong.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_PhanCong.ImageOptions.SvgImage")));
            this.btn_PhanCong.Name = "btn_PhanCong";
            this.btn_PhanCong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_PhanCong_ItemClick);
            // 
            // ribbonPage
            // 
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup,
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "Trung Tâm GDNN Việt Nhật";
            // 
            // ribbonPageGroup
            // 
            this.ribbonPageGroup.AllowTextClipping = false;
            this.ribbonPageGroup.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonPageGroup.ItemLinks.Add(this.btnKhoaHoc);
            this.ribbonPageGroup.ItemLinks.Add(this.btnGiaoVien);
            this.ribbonPageGroup.ItemLinks.Add(this.btnXeTapLai);
            this.ribbonPageGroup.ItemLinks.Add(this.btnDevice);
            this.ribbonPageGroup.Name = "ribbonPageGroup";
            this.ribbonPageGroup.Text = "Quản Lý";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnImportData);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnXepLop);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnLuuLuongDT);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Helper";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btn_DangDaoTao);
            this.ribbonPageGroup2.ItemLinks.Add(this.btn_PhanCong);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Đào Tạo";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 773);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1500, 27);
            // 
            // barSubItemNavigation
            // 
            this.barSubItemNavigation.Caption = "Navigation";
            this.barSubItemNavigation.Id = 15;
            this.barSubItemNavigation.ImageOptions.ImageUri.Uri = "NavigationBar";
            this.barSubItemNavigation.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)});
            this.barSubItemNavigation.Name = "barSubItemNavigation";
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 126);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.Size = new System.Drawing.Size(1500, 647);
            this.MainContainer.TabIndex = 4;
            // 
            // Uc_VNDC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainContainer);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "Uc_VNDC";
            this.Size = new System.Drawing.Size(1500, 800);
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem;
        private DevExpress.XtraBars.BarButtonItem employeesBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem customersBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem btnKhoaHoc;
        private DevExpress.XtraBars.BarButtonItem btnGiaoVien;
        private DevExpress.XtraBars.BarButtonItem btnXeTapLai;
        private DevExpress.XtraBars.BarButtonItem btnDevice;
        private DevExpress.XtraBars.BarSubItem barSubItemNavigation;
        private System.Windows.Forms.Panel MainContainer;
        private DevExpress.XtraBars.BarButtonItem btnImportData;
        private DevExpress.XtraBars.BarButtonItem btnXepLop;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnLuuLuongDT;
        private DevExpress.XtraBars.BarButtonItem btn_DangDaoTao;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem btn_PhanCong;
    }
}
