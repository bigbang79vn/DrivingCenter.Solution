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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_VNDC));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnKhoaHoc = new DevExpress.XtraBars.BarButtonItem();
            this.btnGiaoVien = new DevExpress.XtraBars.BarButtonItem();
            this.btnXeTapLai = new DevExpress.XtraBars.BarButtonItem();
            this.btnDevice = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportData = new DevExpress.XtraBars.BarButtonItem();
            this.btnXepLop = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuuLuongDT = new DevExpress.XtraBars.BarButtonItem();
            this.btn_PhanCong = new DevExpress.XtraBars.BarButtonItem();
            this.bsi_Breadcrumb = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpg_DanhMuc = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpg_DaoTao = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpg_DuLieu = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.MainContainer = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
            this.SuspendLayout();
            //
            // ribbonControl
            //
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.ribbonControl.SearchEditItem,
            this.btnKhoaHoc,
            this.btnGiaoVien,
            this.btnXeTapLai,
            this.btnDevice,
            this.btnImportData,
            this.btnXepLop,
            this.btnLuuLuongDT,
            this.btn_PhanCong,
            this.bsi_Breadcrumb});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 56;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl.ShowToolbarCustomizeItem = false;
            this.ribbonControl.Size = new System.Drawing.Size(1500, 101);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            //
            // btnKhoaHoc
            //
            this.btnKhoaHoc.Caption = "Khóa học\r\nđang đào tạo";
            this.btnKhoaHoc.Hint = "Chọn các khóa học đang đào tạo để theo dõi trên Dashboard";
            this.btnKhoaHoc.Id = 46;
            this.btnKhoaHoc.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKhoaHoc.ImageOptions.Image")));
            this.btnKhoaHoc.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnKhoaHoc.ImageOptions.LargeImage")));
            this.btnKhoaHoc.Name = "btnKhoaHoc";
            this.btnKhoaHoc.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnKhoaHoc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnKhoaHoc_ItemClick);
            //
            // btnGiaoVien
            //
            this.btnGiaoVien.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.btnGiaoVien.Caption = "Giáo viên";
            this.btnGiaoVien.GroupIndex = 1;
            this.btnGiaoVien.Hint = "Danh sách giáo viên dạy thực hành";
            this.btnGiaoVien.Id = 47;
            this.btnGiaoVien.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnGiaoVien.ImageOptions.SvgImage")));
            this.btnGiaoVien.Name = "btnGiaoVien";
            this.btnGiaoVien.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnGiaoVien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGiaoVien_ItemClick);
            //
            // btnXeTapLai
            //
            this.btnXeTapLai.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.btnXeTapLai.Caption = "Xe tập lái";
            this.btnXeTapLai.GroupIndex = 1;
            this.btnXeTapLai.Hint = "Danh sách xe tập lái của trung tâm";
            this.btnXeTapLai.Id = 48;
            this.btnXeTapLai.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXeTapLai.ImageOptions.Image")));
            this.btnXeTapLai.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnXeTapLai.ImageOptions.LargeImage")));
            this.btnXeTapLai.Name = "btnXeTapLai";
            this.btnXeTapLai.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnXeTapLai.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXeTapLai_ItemClick);
            //
            // btnDevice
            //
            this.btnDevice.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.btnDevice.Caption = "Thiết bị DAT";
            this.btnDevice.GroupIndex = 1;
            this.btnDevice.Hint = "Thiết bị giám sát DAT gắn trên xe (IMEI, Serial, phiên bản)";
            this.btnDevice.Id = 49;
            this.btnDevice.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDevice.ImageOptions.SvgImage")));
            this.btnDevice.Name = "btnDevice";
            this.btnDevice.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnDevice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDevice_ItemClick);
            //
            // btnImportData
            //
            this.btnImportData.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.btnImportData.Caption = "Nhập dữ liệu";
            this.btnImportData.GroupIndex = 1;
            this.btnImportData.Hint = "Nhập dữ liệu GPLX / học viên từ file";
            this.btnImportData.Id = 50;
            this.btnImportData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportData.ImageOptions.Image")));
            this.btnImportData.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnImportData.ImageOptions.LargeImage")));
            this.btnImportData.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnImportData.ImageOptions.SvgImage")));
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImportData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportData_ItemClick);
            //
            // btnXepLop
            //
            this.btnXepLop.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.btnXepLop.Caption = "Xếp lớp";
            this.btnXepLop.GroupIndex = 1;
            this.btnXepLop.Hint = "Xếp học viên vào lớp";
            this.btnXepLop.Id = 51;
            this.btnXepLop.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnXepLop.ImageOptions.SvgImage")));
            this.btnXepLop.Name = "btnXepLop";
            this.btnXepLop.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnXepLop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXepLop_ItemClick);
            //
            // btnLuuLuongDT
            //
            this.btnLuuLuongDT.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.btnLuuLuongDT.Caption = "Sắp lưu lượng";
            this.btnLuuLuongDT.GroupIndex = 1;
            this.btnLuuLuongDT.Hint = "Sắp xếp lưu lượng đào tạo theo khóa học";
            this.btnLuuLuongDT.Id = 52;
            this.btnLuuLuongDT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLuuLuongDT.ImageOptions.Image")));
            this.btnLuuLuongDT.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnLuuLuongDT.ImageOptions.LargeImage")));
            this.btnLuuLuongDT.Name = "btnLuuLuongDT";
            this.btnLuuLuongDT.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnLuuLuongDT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuuLuongDT_ItemClick);
            //
            // btn_PhanCong
            //
            this.btn_PhanCong.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.btn_PhanCong.Caption = "Phân công";
            this.btn_PhanCong.GroupIndex = 1;
            this.btn_PhanCong.Hint = "Phân công giáo viên, xe cho học viên DAT";
            this.btn_PhanCong.Id = 54;
            this.btn_PhanCong.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_PhanCong.ImageOptions.SvgImage")));
            this.btn_PhanCong.Name = "btn_PhanCong";
            this.btn_PhanCong.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btn_PhanCong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_PhanCong_ItemClick);
            //
            // bsi_Breadcrumb
            //
            this.bsi_Breadcrumb.Caption = "Trung Tâm GDNN Việt Nhật";
            this.bsi_Breadcrumb.Id = 55;
            this.bsi_Breadcrumb.ImageOptions.ImageUri.Uri = "Home;Size16x16";
            this.bsi_Breadcrumb.Name = "bsi_Breadcrumb";
            this.bsi_Breadcrumb.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //
            // ribbonPage
            //
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpg_DanhMuc,
            this.rpg_DaoTao,
            this.rpg_DuLieu});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "Trung Tâm GDNN Việt Nhật";
            //
            // rpg_DanhMuc
            //
            this.rpg_DanhMuc.AllowTextClipping = false;
            this.rpg_DanhMuc.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.rpg_DanhMuc.ItemLinks.Add(this.btnGiaoVien);
            this.rpg_DanhMuc.ItemLinks.Add(this.btnXeTapLai);
            this.rpg_DanhMuc.ItemLinks.Add(this.btnDevice);
            this.rpg_DanhMuc.Name = "rpg_DanhMuc";
            this.rpg_DanhMuc.Text = "Danh Mục";
            //
            // rpg_DaoTao
            //
            this.rpg_DaoTao.AllowTextClipping = false;
            this.rpg_DaoTao.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.rpg_DaoTao.ItemLinks.Add(this.btnKhoaHoc);
            this.rpg_DaoTao.ItemLinks.Add(this.btnXepLop);
            this.rpg_DaoTao.ItemLinks.Add(this.btn_PhanCong);
            this.rpg_DaoTao.ItemLinks.Add(this.btnLuuLuongDT);
            this.rpg_DaoTao.Name = "rpg_DaoTao";
            this.rpg_DaoTao.Text = "Đào Tạo";
            //
            // rpg_DuLieu
            //
            this.rpg_DuLieu.AllowTextClipping = false;
            this.rpg_DuLieu.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.rpg_DuLieu.ItemLinks.Add(this.btnImportData);
            this.rpg_DuLieu.Name = "rpg_DuLieu";
            this.rpg_DuLieu.Text = "Dữ Liệu";
            //
            // ribbonStatusBar
            //
            this.ribbonStatusBar.ItemLinks.Add(this.bsi_Breadcrumb);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 776);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1500, 24);
            //
            // MainContainer
            //
            this.MainContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 101);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.Size = new System.Drawing.Size(1500, 675);
            this.MainContainer.TabIndex = 4;
            this.MainContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.MainContainer_Paint);
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
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpg_DanhMuc;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpg_DaoTao;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpg_DuLieu;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnKhoaHoc;
        private DevExpress.XtraBars.BarButtonItem btnGiaoVien;
        private DevExpress.XtraBars.BarButtonItem btnXeTapLai;
        private DevExpress.XtraBars.BarButtonItem btnDevice;
        private DevExpress.XtraBars.BarButtonItem btnImportData;
        private DevExpress.XtraBars.BarButtonItem btnXepLop;
        private DevExpress.XtraBars.BarButtonItem btnLuuLuongDT;
        private DevExpress.XtraBars.BarButtonItem btn_PhanCong;
        private DevExpress.XtraBars.BarStaticItem bsi_Breadcrumb;
        private DevExpress.XtraEditors.PanelControl MainContainer;
    }
}
