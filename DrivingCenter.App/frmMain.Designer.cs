namespace DrivingCenter.App
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.MainContainer = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.accMain = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.grp_TongQuan = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_Home = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.grp_DaoTao = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_LichHoc = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_DAT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_VNDC = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.grp_Cabin = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_CabinEco = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_CabinEMC = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_CabinManager = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.grp_BaoCao = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_TongHopXe = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_TotNghiep = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_Print = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.grp_HeThong = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_TaiKhoan = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_DangXuat = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.itemNav = new DevExpress.XtraBars.BarStaticItem();
            this.itemUser = new DevExpress.XtraBars.BarStaticItem();
            this.barToggleSwitchItem = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.accMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(213, 31);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.Size = new System.Drawing.Size(1373, 733);
            this.MainContainer.TabIndex = 0;
            // 
            // accMain
            // 
            this.accMain.AllowItemSelection = true;
            this.accMain.Appearance.AccordionControl.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.accMain.Appearance.AccordionControl.Options.UseFont = true;
            this.accMain.Appearance.Group.Hovered.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Bold);
            this.accMain.Appearance.Group.Hovered.Options.UseFont = true;
            this.accMain.Appearance.Group.Normal.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Bold);
            this.accMain.Appearance.Group.Normal.Options.UseFont = true;
            this.accMain.Appearance.Group.Pressed.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Bold);
            this.accMain.Appearance.Group.Pressed.Options.UseFont = true;
            this.accMain.Appearance.Item.Hovered.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.accMain.Appearance.Item.Hovered.Options.UseFont = true;
            this.accMain.Appearance.Item.Normal.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.accMain.Appearance.Item.Normal.Options.UseFont = true;
            this.accMain.Appearance.Item.Pressed.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.accMain.Appearance.Item.Pressed.Options.UseFont = true;
            this.accMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.accMain.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.grp_TongQuan,
            this.grp_DaoTao,
            this.grp_Cabin,
            this.grp_BaoCao,
            this.grp_HeThong});
            this.accMain.ItemHeight = 40;
            this.accMain.Location = new System.Drawing.Point(0, 31);
            this.accMain.Name = "accMain";
            this.accMain.OptionsMinimizing.NormalWidth = 213;
            this.accMain.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accMain.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Auto;
            this.accMain.Size = new System.Drawing.Size(213, 733);
            this.accMain.TabIndex = 1;
            this.accMain.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // grp_TongQuan
            // 
            this.grp_TongQuan.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acc_Home});
            this.grp_TongQuan.Expanded = true;
            this.grp_TongQuan.Name = "grp_TongQuan";
            this.grp_TongQuan.Text = "TỔNG QUAN";
            // 
            // acc_Home
            // 
            this.acc_Home.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_Home.ImageOptions.SvgImage")));
            this.acc_Home.Name = "acc_Home";
            this.acc_Home.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_Home.Text = "Trang chủ";
            this.acc_Home.Click += new System.EventHandler(this.acc_Home_Click);
            // 
            // grp_DaoTao
            // 
            this.grp_DaoTao.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acc_LichHoc,
            this.acc_DAT,
            this.acc_VNDC});
            this.grp_DaoTao.Expanded = true;
            this.grp_DaoTao.Name = "grp_DaoTao";
            this.grp_DaoTao.Text = "ĐÀO TẠO";
            // 
            // acc_LichHoc
            // 
            this.acc_LichHoc.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_LichHoc.ImageOptions.SvgImage")));
            this.acc_LichHoc.Name = "acc_LichHoc";
            this.acc_LichHoc.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_LichHoc.Text = "Lịch học số tự động";
            this.acc_LichHoc.Click += new System.EventHandler(this.acc_LichHoc_Click);
            // 
            // acc_DAT
            // 
            this.acc_DAT.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_DAT.ImageOptions.SvgImage")));
            this.acc_DAT.Name = "acc_DAT";
            this.acc_DAT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_DAT.Text = "Giám sát DAT";
            this.acc_DAT.Click += new System.EventHandler(this.acc_DAT_Click);
            // 
            // acc_VNDC
            // 
            this.acc_VNDC.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_VNDC.ImageOptions.SvgImage")));
            this.acc_VNDC.Name = "acc_VNDC";
            this.acc_VNDC.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_VNDC.Text = "Quản lý đào tạo";
            this.acc_VNDC.Click += new System.EventHandler(this.acc_VNDC_Click);
            // 
            // grp_Cabin
            // 
            this.grp_Cabin.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acc_CabinEco,
            this.acc_CabinEMC,
            this.acc_CabinManager});
            this.grp_Cabin.Expanded = true;
            this.grp_Cabin.Name = "grp_Cabin";
            this.grp_Cabin.Text = "CABIN ĐIỆN TỬ";
            // 
            // acc_CabinEco
            // 
            this.acc_CabinEco.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_CabinEco.ImageOptions.SvgImage")));
            this.acc_CabinEco.Name = "acc_CabinEco";
            this.acc_CabinEco.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_CabinEco.Text = "Cabin Ecotek";
            this.acc_CabinEco.Click += new System.EventHandler(this.acc_CabinEco_Click);
            // 
            // acc_CabinEMC
            // 
            this.acc_CabinEMC.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_CabinEMC.ImageOptions.SvgImage")));
            this.acc_CabinEMC.Name = "acc_CabinEMC";
            this.acc_CabinEMC.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_CabinEMC.Text = "Cabin EMC";
            this.acc_CabinEMC.Click += new System.EventHandler(this.acc_CabinEMC_Click);
            //
            // acc_CabinManager
            //
            this.acc_CabinManager.Name = "acc_CabinManager";
            this.acc_CabinManager.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_CabinManager.Text = "Điều khiển Cabin";
            this.acc_CabinManager.Click += new System.EventHandler(this.acc_CabinManager_Click);
            // 
            // grp_BaoCao
            // 
            this.grp_BaoCao.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acc_TongHopXe,
            this.acc_TotNghiep,
            this.acc_Print});
            this.grp_BaoCao.Expanded = true;
            this.grp_BaoCao.Name = "grp_BaoCao";
            this.grp_BaoCao.Text = "BÁO CÁO";
            // 
            // acc_TongHopXe
            // 
            this.acc_TongHopXe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_TongHopXe.ImageOptions.SvgImage")));
            this.acc_TongHopXe.Name = "acc_TongHopXe";
            this.acc_TongHopXe.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_TongHopXe.Text = "Tổng hợp theo xe";
            this.acc_TongHopXe.Click += new System.EventHandler(this.acc_TongHopXe_Click);
            // 
            // acc_TotNghiep
            // 
            this.acc_TotNghiep.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_TotNghiep.ImageOptions.SvgImage")));
            this.acc_TotNghiep.Name = "acc_TotNghiep";
            this.acc_TotNghiep.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_TotNghiep.Text = "Kết quả tốt nghiệp";
            this.acc_TotNghiep.Click += new System.EventHandler(this.acc_TotNghiep_Click);
            // 
            // acc_Print
            // 
            this.acc_Print.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_Print.ImageOptions.SvgImage")));
            this.acc_Print.Name = "acc_Print";
            this.acc_Print.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_Print.Text = "In ấn";
            this.acc_Print.Click += new System.EventHandler(this.acc_Print_Click);
            // 
            // grp_HeThong
            // 
            this.grp_HeThong.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acc_TaiKhoan,
            this.acc_DangXuat});
            this.grp_HeThong.Expanded = true;
            this.grp_HeThong.Name = "grp_HeThong";
            this.grp_HeThong.Text = "HỆ THỐNG";
            // 
            // acc_TaiKhoan
            // 
            this.acc_TaiKhoan.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_TaiKhoan.ImageOptions.SvgImage")));
            this.acc_TaiKhoan.Name = "acc_TaiKhoan";
            this.acc_TaiKhoan.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_TaiKhoan.Text = "Tài khoản";
            this.acc_TaiKhoan.Click += new System.EventHandler(this.acc_TaiKhoan_Click);
            // 
            // acc_DangXuat
            // 
            this.acc_DangXuat.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_DangXuat.ImageOptions.SvgImage")));
            this.acc_DangXuat.Name = "acc_DangXuat";
            this.acc_DangXuat.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_DangXuat.Text = "Đăng xuất";
            this.acc_DangXuat.Click += new System.EventHandler(this.acc_DangXuat_Click);
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemNav,
            this.itemUser,
            this.barToggleSwitchItem});
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1586, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.itemNav);
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.itemUser);
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.barToggleSwitchItem);
            // 
            // itemNav
            // 
            this.itemNav.Caption = "Trang chủ";
            this.itemNav.Id = 0;
            this.itemNav.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("itemNav.ImageOptions.SvgImage")));
            this.itemNav.ItemAppearance.Normal.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.itemNav.ItemAppearance.Normal.Options.UseFont = true;
            this.itemNav.Name = "itemNav";
            this.itemNav.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // itemUser
            // 
            this.itemUser.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.itemUser.Caption = "admin";
            this.itemUser.Id = 13;
            this.itemUser.Name = "itemUser";
            this.itemUser.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barToggleSwitchItem
            // 
            this.barToggleSwitchItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barToggleSwitchItem.Caption = "Chế độ tối";
            this.barToggleSwitchItem.Id = 12;
            this.barToggleSwitchItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barToggleSwitchItem.ImageOptions.SvgImage")));
            this.barToggleSwitchItem.Name = "barToggleSwitchItem";
            this.barToggleSwitchItem.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barToggleSwitchItem_CheckedChanged);
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.Form = this;
            this.fluentFormDefaultManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemNav,
            this.itemUser,
            this.barToggleSwitchItem});
            this.fluentFormDefaultManager1.MaxItemId = 14;
            // 
            // frmMain
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1586, 764);
            this.ControlContainer = this.MainContainer;
            this.Controls.Add(this.MainContainer);
            this.Controls.Add(this.accMain);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("frmMain.IconOptions.SvgImage")));
            this.Name = "frmMain";
            this.NavigationControl = this.accMain;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trung Tâm GDNN Việt Nhật";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer MainContainer;
        private DevExpress.XtraBars.Navigation.AccordionControl accMain;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement grp_TongQuan;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_Home;
        private DevExpress.XtraBars.Navigation.AccordionControlElement grp_DaoTao;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_LichHoc;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_DAT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_VNDC;
        private DevExpress.XtraBars.Navigation.AccordionControlElement grp_Cabin;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_CabinEco;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_CabinEMC;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_CabinManager;
        private DevExpress.XtraBars.Navigation.AccordionControlElement grp_BaoCao;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_TongHopXe;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_TotNghiep;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_Print;
        private DevExpress.XtraBars.Navigation.AccordionControlElement grp_HeThong;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_TaiKhoan;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_DangXuat;
        private DevExpress.XtraBars.BarStaticItem itemNav;
        private DevExpress.XtraBars.BarStaticItem itemUser;
        private DevExpress.XtraBars.BarToggleSwitchItem barToggleSwitchItem;
    }
}
