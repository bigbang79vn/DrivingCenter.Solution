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
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions2 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.grp_PhanCongDAT = new DevExpress.XtraEditors.GroupControl();
            this.cbb_KhoaHoc = new DevExpress.XtraEditors.ComboBoxEdit();
            this.grdCtrl_Assign = new DevExpress.XtraGrid.GridControl();
            this.gridView_Assign = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grp_PhanCongDAT)).BeginInit();
            this.grp_PhanCongDAT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbb_KhoaHoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrl_Assign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Assign)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_PhanCongDAT
            // 
            this.grp_PhanCongDAT.CaptionImageOptions.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.grp_PhanCongDAT.CaptionImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("grp_PhanCongDAT.CaptionImageOptions.SvgImage")));
            this.grp_PhanCongDAT.Controls.Add(this.grdCtrl_Assign);
            this.grp_PhanCongDAT.Controls.Add(this.cbb_KhoaHoc);
            buttonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonImageOptions1.SvgImage")));
            buttonImageOptions1.SvgImageSize = new System.Drawing.Size(25, 25);
            buttonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonImageOptions2.SvgImage")));
            buttonImageOptions2.SvgImageSize = new System.Drawing.Size(25, 25);
            this.grp_PhanCongDAT.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Open", false, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1),
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Assign", false, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1)});
            this.grp_PhanCongDAT.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grp_PhanCongDAT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_PhanCongDAT.Location = new System.Drawing.Point(0, 0);
            this.grp_PhanCongDAT.Name = "grp_PhanCongDAT";
            this.grp_PhanCongDAT.Padding = new System.Windows.Forms.Padding(5);
            this.grp_PhanCongDAT.Size = new System.Drawing.Size(1500, 800);
            this.grp_PhanCongDAT.TabIndex = 0;
            this.grp_PhanCongDAT.Text = "Phân công giáo viên khóa";
            this.grp_PhanCongDAT.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.grp_PhanCongDAT_CustomButtonClick);
            // 
            // cbb_KhoaHoc
            // 
            this.cbb_KhoaHoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbb_KhoaHoc.Location = new System.Drawing.Point(1287, 10);
            this.cbb_KhoaHoc.Name = "cbb_KhoaHoc";
            this.cbb_KhoaHoc.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_KhoaHoc.Properties.Appearance.Options.UseFont = true;
            this.cbb_KhoaHoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbb_KhoaHoc.Size = new System.Drawing.Size(111, 24);
            this.cbb_KhoaHoc.TabIndex = 0;
            this.cbb_KhoaHoc.SelectedIndexChanged += new System.EventHandler(this.cbb_KhoaHoc_SelectedIndexChanged);
            // 
            // grdCtrl_Assign
            // 
            this.grdCtrl_Assign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCtrl_Assign.Location = new System.Drawing.Point(7, 48);
            this.grdCtrl_Assign.MainView = this.gridView_Assign;
            this.grdCtrl_Assign.Name = "grdCtrl_Assign";
            this.grdCtrl_Assign.Size = new System.Drawing.Size(1486, 745);
            this.grdCtrl_Assign.TabIndex = 1;
            this.grdCtrl_Assign.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Assign});
            // 
            // gridView_Assign
            // 
            this.gridView_Assign.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_Assign.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_Assign.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView_Assign.Appearance.Row.Options.UseFont = true;
            this.gridView_Assign.GridControl = this.grdCtrl_Assign;
            this.gridView_Assign.Name = "gridView_Assign";
            this.gridView_Assign.OptionsView.ShowGroupPanel = false;
            // 
            // Uc_PhanCongDAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_PhanCongDAT);
            this.Name = "Uc_PhanCongDAT";
            this.Size = new System.Drawing.Size(1500, 800);
            this.Load += new System.EventHandler(this.Uc_PhanCongDAT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grp_PhanCongDAT)).EndInit();
            this.grp_PhanCongDAT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbb_KhoaHoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCtrl_Assign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Assign)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grp_PhanCongDAT;
        private DevExpress.XtraEditors.ComboBoxEdit cbb_KhoaHoc;
        private DevExpress.XtraGrid.GridControl grdCtrl_Assign;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Assign;
    }
}
