
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
            this.acc_Home = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_CabinEco = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_CabinEMC = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_GPLX = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_QLLT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_LichHoc = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_DAT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_TongHopXe = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_XeTapLai = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_TotNghiep = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_Print = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc_TaiKhoan = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.itemNav = new DevExpress.XtraBars.BarStaticItem();
            this.btn_DarkLight = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Refresh = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.accMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(193, 31);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.Size = new System.Drawing.Size(1393, 733);
            this.MainContainer.TabIndex = 0;
            // 
            // accMain
            // 
            this.accMain.Appearance.AccordionControl.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accMain.Appearance.AccordionControl.Options.UseFont = true;
            this.accMain.Appearance.AccordionControl.Options.UseTextOptions = true;
            this.accMain.Appearance.AccordionControl.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.accMain.Appearance.Item.Default.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.accMain.Appearance.Item.Default.Options.UseFont = true;
            this.accMain.Appearance.Item.Disabled.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.accMain.Appearance.Item.Disabled.Options.UseFont = true;
            this.accMain.Appearance.Item.Hovered.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.accMain.Appearance.Item.Hovered.Options.UseFont = true;
            this.accMain.Appearance.Item.Normal.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accMain.Appearance.Item.Normal.Options.UseFont = true;
            this.accMain.Appearance.Item.Normal.Options.UseTextOptions = true;
            this.accMain.Appearance.Item.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.accMain.Appearance.Item.Pressed.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.accMain.Appearance.Item.Pressed.Options.UseFont = true;
            this.accMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.accMain.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acc_Home,
            this.acc_CabinEco,
            this.acc_CabinEMC,
            this.acc_GPLX,
            this.acc_QLLT,
            this.acc_LichHoc,
            this.acc_DAT,
            this.acc_TongHopXe,
            this.acc_XeTapLai,
            this.acc_TotNghiep,
            this.acc_Print,
            this.acc_TaiKhoan});
            this.accMain.ItemHeight = 42;
            this.accMain.Location = new System.Drawing.Point(0, 31);
            this.accMain.Name = "accMain";
            this.accMain.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accMain.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Auto;
            this.accMain.Size = new System.Drawing.Size(193, 733);
            this.accMain.TabIndex = 1;
            this.accMain.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // acc_Home
            // 
            this.acc_Home.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_Home.ImageOptions.SvgImage")));
            this.acc_Home.Name = "acc_Home";
            this.acc_Home.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_Home.Text = "Home";
            this.acc_Home.Click += new System.EventHandler(this.acc_Home_Click);
            // 
            // acc_CabinEco
            // 
            this.acc_CabinEco.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_CabinEco.ImageOptions.SvgImage")));
            this.acc_CabinEco.Name = "acc_CabinEco";
            this.acc_CabinEco.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_CabinEco.Text = "Cabin Eco";
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
            // acc_GPLX
            // 
            this.acc_GPLX.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_GPLX.ImageOptions.SvgImage")));
            this.acc_GPLX.Name = "acc_GPLX";
            this.acc_GPLX.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_GPLX.Text = "G.P.L.X";
            this.acc_GPLX.Click += new System.EventHandler(this.acc_GPLX_Click);
            // 
            // acc_QLLT
            // 
            this.acc_QLLT.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_QLLT.ImageOptions.SvgImage")));
            this.acc_QLLT.Name = "acc_QLLT";
            this.acc_QLLT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_QLLT.Text = "Lý Thuyết";
            this.acc_QLLT.Click += new System.EventHandler(this.acc_QLLT_Click);
            // 
            // acc_LichHoc
            // 
            this.acc_LichHoc.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_LichHoc.ImageOptions.SvgImage")));
            this.acc_LichHoc.Name = "acc_LichHoc";
            this.acc_LichHoc.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_LichHoc.Text = "Lịch Học STĐ";
            this.acc_LichHoc.Click += new System.EventHandler(this.acc_LichHoc_Click);
            // 
            // acc_DAT
            // 
            this.acc_DAT.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_DAT.ImageOptions.SvgImage")));
            this.acc_DAT.Name = "acc_DAT";
            this.acc_DAT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_DAT.Text = "D.A.T";
            this.acc_DAT.Click += new System.EventHandler(this.acc_DAT_Click);
            // 
            // acc_TongHopXe
            // 
            this.acc_TongHopXe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_TongHopXe.ImageOptions.SvgImage")));
            this.acc_TongHopXe.Name = "acc_TongHopXe";
            this.acc_TongHopXe.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_TongHopXe.Text = "Tổng Hợp Xe";
            this.acc_TongHopXe.Click += new System.EventHandler(this.acc_TongHopXe_Click);
            // 
            // acc_XeTapLai
            // 
            this.acc_XeTapLai.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_XeTapLai.ImageOptions.SvgImage")));
            this.acc_XeTapLai.Name = "acc_XeTapLai";
            this.acc_XeTapLai.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_XeTapLai.Text = "Xe Tập Lái";
            this.acc_XeTapLai.Click += new System.EventHandler(this.acc_XeTapLai_Click);
            // 
            // acc_TotNghiep
            // 
            this.acc_TotNghiep.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_TotNghiep.ImageOptions.SvgImage")));
            this.acc_TotNghiep.Name = "acc_TotNghiep";
            this.acc_TotNghiep.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_TotNghiep.Text = "Tốt Nghiệp";
            this.acc_TotNghiep.Click += new System.EventHandler(this.acc_TotNghiep_Click);
            // 
            // acc_Print
            // 
            this.acc_Print.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_Print.ImageOptions.SvgImage")));
            this.acc_Print.Name = "acc_Print";
            this.acc_Print.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_Print.Text = "Print";
            this.acc_Print.Click += new System.EventHandler(this.acc_Print_Click);
            // 
            // acc_TaiKhoan
            // 
            this.acc_TaiKhoan.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("acc_TaiKhoan.ImageOptions.SvgImage")));
            this.acc_TaiKhoan.Name = "acc_TaiKhoan";
            this.acc_TaiKhoan.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acc_TaiKhoan.Text = "Tài Khoản";
            this.acc_TaiKhoan.Click += new System.EventHandler(this.acc_TaiKhoan_Click);
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemNav,
            this.btn_DarkLight,
            this.barButtonItem_Refresh,
            this.barSubItem1,
            this.barButtonItem1,
            this.barButtonItem2});
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1586, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.itemNav);
            // 
            // itemNav
            // 
            this.itemNav.Caption = "itemNav";
            this.itemNav.Id = 0;
            this.itemNav.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("itemNav.ImageOptions.SvgImage")));
            this.itemNav.Name = "itemNav";
            this.itemNav.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btn_DarkLight
            // 
            this.btn_DarkLight.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btn_DarkLight.Caption = "ChangeTheme";
            this.btn_DarkLight.Id = 1;
            this.btn_DarkLight.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_DarkLight.ImageOptions.SvgImage")));
            this.btn_DarkLight.Name = "btn_DarkLight";
            // 
            // barButtonItem_Refresh
            // 
            this.barButtonItem_Refresh.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem_Refresh.Caption = "Refresh";
            this.barButtonItem_Refresh.Id = 5;
            this.barButtonItem_Refresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem_Refresh.ImageOptions.SvgImage")));
            this.barButtonItem_Refresh.Name = "barButtonItem_Refresh";
            this.barButtonItem_Refresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Refresh_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 7;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 9;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.Form = this;
            this.fluentFormDefaultManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemNav,
            this.btn_DarkLight,
            this.barButtonItem_Refresh,
            this.barSubItem1,
            this.barButtonItem1,
            this.barButtonItem2});
            this.fluentFormDefaultManager1.MaxItemId = 10;
            // 
            // frmMain
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
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
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_Home;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_CabinEco;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_CabinEMC;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_GPLX;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_QLLT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_DAT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_TongHopXe;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_TotNghiep;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_TaiKhoan;
        private DevExpress.XtraBars.BarStaticItem itemNav;
        private DevExpress.XtraBars.BarButtonItem btn_DarkLight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Refresh;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_Print;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_XeTapLai;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acc_LichHoc;
    }
}

