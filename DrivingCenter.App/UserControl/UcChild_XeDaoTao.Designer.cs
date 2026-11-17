
namespace DrivingCenter.App.UserControl
{
    partial class UcChild_XeDaoTao
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
            this.pnl_Header = new DevExpress.XtraEditors.PanelControl();
            this.btn_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ImportXml = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_Desc = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Title = new DevExpress.XtraEditors.LabelControl();
            this.spacerTop = new System.Windows.Forms.Panel();
            this.grp_Detail = new DevExpress.XtraEditors.GroupControl();
            this.lbl_Hint = new DevExpress.XtraEditors.LabelControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Edit = new DevExpress.XtraEditors.SimpleButton();
            this.toggleSwitch_TrangThai = new DevExpress.XtraEditors.ToggleSwitch();
            this.cap_ThanhLy = new DevExpress.XtraEditors.LabelControl();
            this.txt_SoHuu = new DevExpress.XtraEditors.TextEdit();
            this.cap_SoHuu = new DevExpress.XtraEditors.LabelControl();
            this.val_SuDungDen = new DevExpress.XtraEditors.LabelControl();
            this.cap_SuDungDen = new DevExpress.XtraEditors.LabelControl();
            this.val_NgayHH = new DevExpress.XtraEditors.LabelControl();
            this.cap_NgayHH = new DevExpress.XtraEditors.LabelControl();
            this.val_NgayCap = new DevExpress.XtraEditors.LabelControl();
            this.cap_NgayCap = new DevExpress.XtraEditors.LabelControl();
            this.val_SoGPXTL = new DevExpress.XtraEditors.LabelControl();
            this.cap_SoGPXTL = new DevExpress.XtraEditors.LabelControl();
            this.val_NamSX = new DevExpress.XtraEditors.LabelControl();
            this.cap_NamSX = new DevExpress.XtraEditors.LabelControl();
            this.val_MaXe = new DevExpress.XtraEditors.LabelControl();
            this.cap_MaXe = new DevExpress.XtraEditors.LabelControl();
            this.lbl_TrangThai = new DevExpress.XtraEditors.LabelControl();
            this.lbl_HangXe = new DevExpress.XtraEditors.LabelControl();
            this.lbl_BienSo = new DevExpress.XtraEditors.LabelControl();
            this.spacerRight = new System.Windows.Forms.Panel();
            this.grp_List = new DevExpress.XtraEditors.GroupControl();
            this.gridControl_XeTapLai = new DevExpress.XtraGrid.GridControl();
            this.gridView_XeTapLai = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BienSoXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HangXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NamSX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoGPXTL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayCapGPXTL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NgayHHGPXTL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChuSoHuu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ThanhLy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DateTime_Use = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).BeginInit();
            this.pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_Detail)).BeginInit();
            this.grp_Detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch_TrangThai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SoHuu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_List)).BeginInit();
            this.grp_List.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_XeTapLai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_XeTapLai)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Header
            // 
            this.pnl_Header.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Header.Controls.Add(this.btn_Refresh);
            this.pnl_Header.Controls.Add(this.btn_Export);
            this.pnl_Header.Controls.Add(this.btn_ImportXml);
            this.pnl_Header.Controls.Add(this.lbl_Desc);
            this.pnl_Header.Controls.Add(this.lbl_Title);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.Location = new System.Drawing.Point(12, 12);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Size = new System.Drawing.Size(1476, 62);
            this.pnl_Header.TabIndex = 0;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Refresh.Appearance.Options.UseFont = true;
            this.btn_Refresh.Location = new System.Drawing.Point(1084, 14);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(110, 34);
            this.btn_Refresh.TabIndex = 2;
            this.btn_Refresh.Text = "Làm mới";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Export.Appearance.Options.UseFont = true;
            this.btn_Export.Location = new System.Drawing.Point(1200, 14);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(120, 34);
            this.btn_Export.TabIndex = 3;
            this.btn_Export.Text = "Xuất Excel";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_ImportXml
            // 
            this.btn_ImportXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ImportXml.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_ImportXml.Appearance.Options.UseFont = true;
            this.btn_ImportXml.Location = new System.Drawing.Point(1326, 14);
            this.btn_ImportXml.Name = "btn_ImportXml";
            this.btn_ImportXml.Size = new System.Drawing.Size(150, 34);
            this.btn_ImportXml.TabIndex = 4;
            this.btn_ImportXml.Text = "Import Xml";
            this.btn_ImportXml.ToolTip = "Nhập danh sách xe tập lái từ file XML (DSXTL) do Sở gửi";
            this.btn_ImportXml.Click += new System.EventHandler(this.btn_ImportXml_Click);
            // 
            // lbl_Desc
            // 
            this.lbl_Desc.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Desc.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Desc.Appearance.Options.UseFont = true;
            this.lbl_Desc.Appearance.Options.UseForeColor = true;
            this.lbl_Desc.Location = new System.Drawing.Point(18, 36);
            this.lbl_Desc.Name = "lbl_Desc";
            this.lbl_Desc.Size = new System.Drawing.Size(410, 18);
            this.lbl_Desc.TabIndex = 1;
            this.lbl_Desc.Text = "Danh sách xe tập lái, giấy phép xe tập lái (GPXTL) và tình trạng sử dụng. Cập nhật GPXTL bằng file XML do Sở gửi.";
            // 
            // lbl_Title
            // 
            this.lbl_Title.Appearance.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_Title.Appearance.Options.UseFont = true;
            this.lbl_Title.Appearance.Options.UseForeColor = true;
            this.lbl_Title.Location = new System.Drawing.Point(16, 6);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(87, 28);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "Xe tập lái";
            // 
            // spacerTop
            // 
            this.spacerTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.spacerTop.Location = new System.Drawing.Point(12, 74);
            this.spacerTop.Name = "spacerTop";
            this.spacerTop.Size = new System.Drawing.Size(1476, 10);
            this.spacerTop.TabIndex = 1;
            // 
            // grp_Detail
            // 
            this.grp_Detail.AppearanceCaption.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold);
            this.grp_Detail.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.grp_Detail.AppearanceCaption.Options.UseFont = true;
            this.grp_Detail.AppearanceCaption.Options.UseForeColor = true;
            this.grp_Detail.Controls.Add(this.lbl_Hint);
            this.grp_Detail.Controls.Add(this.btn_Cancel);
            this.grp_Detail.Controls.Add(this.btn_Save);
            this.grp_Detail.Controls.Add(this.btn_Edit);
            this.grp_Detail.Controls.Add(this.toggleSwitch_TrangThai);
            this.grp_Detail.Controls.Add(this.cap_ThanhLy);
            this.grp_Detail.Controls.Add(this.txt_SoHuu);
            this.grp_Detail.Controls.Add(this.cap_SoHuu);
            this.grp_Detail.Controls.Add(this.val_SuDungDen);
            this.grp_Detail.Controls.Add(this.cap_SuDungDen);
            this.grp_Detail.Controls.Add(this.val_NgayHH);
            this.grp_Detail.Controls.Add(this.cap_NgayHH);
            this.grp_Detail.Controls.Add(this.val_NgayCap);
            this.grp_Detail.Controls.Add(this.cap_NgayCap);
            this.grp_Detail.Controls.Add(this.val_SoGPXTL);
            this.grp_Detail.Controls.Add(this.cap_SoGPXTL);
            this.grp_Detail.Controls.Add(this.val_NamSX);
            this.grp_Detail.Controls.Add(this.cap_NamSX);
            this.grp_Detail.Controls.Add(this.val_MaXe);
            this.grp_Detail.Controls.Add(this.cap_MaXe);
            this.grp_Detail.Controls.Add(this.lbl_TrangThai);
            this.grp_Detail.Controls.Add(this.lbl_HangXe);
            this.grp_Detail.Controls.Add(this.lbl_BienSo);
            this.grp_Detail.Dock = System.Windows.Forms.DockStyle.Right;
            this.grp_Detail.Location = new System.Drawing.Point(1108, 84);
            this.grp_Detail.Name = "grp_Detail";
            this.grp_Detail.Size = new System.Drawing.Size(380, 704);
            this.grp_Detail.TabIndex = 4;
            this.grp_Detail.Text = "THÔNG TIN XE TẬP LÁI";
            this.grp_Detail.Paint += new System.Windows.Forms.PaintEventHandler(this.grp_Detail_Paint);
            // 
            // lbl_Hint
            // 
            this.lbl_Hint.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Italic);
            this.lbl_Hint.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_Hint.Appearance.Options.UseFont = true;
            this.lbl_Hint.Appearance.Options.UseForeColor = true;
            this.lbl_Hint.Appearance.Options.UseTextOptions = true;
            this.lbl_Hint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbl_Hint.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lbl_Hint.Location = new System.Drawing.Point(20, 530);
            this.lbl_Hint.Name = "lbl_Hint";
            this.lbl_Hint.Size = new System.Drawing.Size(340, 26);
            this.lbl_Hint.TabIndex = 22;
            this.lbl_Hint.Text = "Số GPXTL, ngày cấp và ngày hết hạn được cập nhật bằng Import Xml. Tại đây chỉ sửa được Chủ sở hữu và tình trạng Thanh lý.";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Location = new System.Drawing.Point(248, 480);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(112, 34);
            this.btn_Cancel.TabIndex = 21;
            this.btn_Cancel.Text = "Hủy";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Location = new System.Drawing.Point(134, 480);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(108, 34);
            this.btn_Save.TabIndex = 20;
            this.btn_Save.Text = "Lưu";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Edit.Appearance.Options.UseFont = true;
            this.btn_Edit.Location = new System.Drawing.Point(20, 480);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(108, 34);
            this.btn_Edit.TabIndex = 19;
            this.btn_Edit.Text = "Sửa";
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // toggleSwitch_TrangThai
            // 
            this.toggleSwitch_TrangThai.Location = new System.Drawing.Point(20, 436);
            this.toggleSwitch_TrangThai.Name = "toggleSwitch_TrangThai";
            this.toggleSwitch_TrangThai.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.toggleSwitch_TrangThai.Properties.Appearance.Options.UseFont = true;
            this.toggleSwitch_TrangThai.Properties.OffText = "Đang sử dụng";
            this.toggleSwitch_TrangThai.Properties.OnText = "Đã thanh lý";
            this.toggleSwitch_TrangThai.Size = new System.Drawing.Size(340, 22);
            this.toggleSwitch_TrangThai.TabIndex = 18;
            // 
            // cap_ThanhLy
            // 
            this.cap_ThanhLy.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_ThanhLy.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_ThanhLy.Appearance.Options.UseFont = true;
            this.cap_ThanhLy.Appearance.Options.UseForeColor = true;
            this.cap_ThanhLy.Location = new System.Drawing.Point(20, 416);
            this.cap_ThanhLy.Name = "cap_ThanhLy";
            this.cap_ThanhLy.Size = new System.Drawing.Size(56, 16);
            this.cap_ThanhLy.TabIndex = 17;
            this.cap_ThanhLy.Text = "Tình trạng";
            // 
            // txt_SoHuu
            // 
            this.txt_SoHuu.Location = new System.Drawing.Point(20, 380);
            this.txt_SoHuu.Name = "txt_SoHuu";
            this.txt_SoHuu.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.txt_SoHuu.Properties.Appearance.Options.UseFont = true;
            this.txt_SoHuu.Properties.NullValuePrompt = "Tên chủ sở hữu xe";
            this.txt_SoHuu.Size = new System.Drawing.Size(340, 24);
            this.txt_SoHuu.TabIndex = 16;
            // 
            // cap_SoHuu
            // 
            this.cap_SoHuu.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_SoHuu.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_SoHuu.Appearance.Options.UseFont = true;
            this.cap_SoHuu.Appearance.Options.UseForeColor = true;
            this.cap_SoHuu.Location = new System.Drawing.Point(20, 360);
            this.cap_SoHuu.Name = "cap_SoHuu";
            this.cap_SoHuu.Size = new System.Drawing.Size(62, 16);
            this.cap_SoHuu.TabIndex = 15;
            this.cap_SoHuu.Text = "Chủ sở hữu";
            // 
            // val_SuDungDen
            // 
            this.val_SuDungDen.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_SuDungDen.Appearance.Options.UseFont = true;
            this.val_SuDungDen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_SuDungDen.Location = new System.Drawing.Point(160, 300);
            this.val_SuDungDen.Name = "val_SuDungDen";
            this.val_SuDungDen.Size = new System.Drawing.Size(200, 18);
            this.val_SuDungDen.TabIndex = 14;
            this.val_SuDungDen.Text = "—";
            // 
            // cap_SuDungDen
            // 
            this.cap_SuDungDen.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_SuDungDen.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_SuDungDen.Appearance.Options.UseFont = true;
            this.cap_SuDungDen.Appearance.Options.UseForeColor = true;
            this.cap_SuDungDen.Location = new System.Drawing.Point(20, 301);
            this.cap_SuDungDen.Name = "cap_SuDungDen";
            this.cap_SuDungDen.Size = new System.Drawing.Size(68, 16);
            this.cap_SuDungDen.TabIndex = 13;
            this.cap_SuDungDen.Text = "Sử dụng đến";
            // 
            // val_NgayHH
            // 
            this.val_NgayHH.Appearance.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.val_NgayHH.Appearance.Options.UseFont = true;
            this.val_NgayHH.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_NgayHH.Location = new System.Drawing.Point(160, 266);
            this.val_NgayHH.Name = "val_NgayHH";
            this.val_NgayHH.Size = new System.Drawing.Size(200, 18);
            this.val_NgayHH.TabIndex = 12;
            this.val_NgayHH.Text = "—";
            // 
            // cap_NgayHH
            // 
            this.cap_NgayHH.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_NgayHH.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_NgayHH.Appearance.Options.UseFont = true;
            this.cap_NgayHH.Appearance.Options.UseForeColor = true;
            this.cap_NgayHH.Location = new System.Drawing.Point(20, 267);
            this.cap_NgayHH.Name = "cap_NgayHH";
            this.cap_NgayHH.Size = new System.Drawing.Size(113, 16);
            this.cap_NgayHH.TabIndex = 11;
            this.cap_NgayHH.Text = "Ngày hết hạn GPXTL";
            // 
            // val_NgayCap
            // 
            this.val_NgayCap.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_NgayCap.Appearance.Options.UseFont = true;
            this.val_NgayCap.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_NgayCap.Location = new System.Drawing.Point(160, 232);
            this.val_NgayCap.Name = "val_NgayCap";
            this.val_NgayCap.Size = new System.Drawing.Size(200, 18);
            this.val_NgayCap.TabIndex = 10;
            this.val_NgayCap.Text = "—";
            // 
            // cap_NgayCap
            // 
            this.cap_NgayCap.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_NgayCap.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_NgayCap.Appearance.Options.UseFont = true;
            this.cap_NgayCap.Appearance.Options.UseForeColor = true;
            this.cap_NgayCap.Location = new System.Drawing.Point(20, 233);
            this.cap_NgayCap.Name = "cap_NgayCap";
            this.cap_NgayCap.Size = new System.Drawing.Size(92, 16);
            this.cap_NgayCap.TabIndex = 9;
            this.cap_NgayCap.Text = "Ngày cấp GPXTL";
            // 
            // val_SoGPXTL
            // 
            this.val_SoGPXTL.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_SoGPXTL.Appearance.Options.UseFont = true;
            this.val_SoGPXTL.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_SoGPXTL.Location = new System.Drawing.Point(160, 198);
            this.val_SoGPXTL.Name = "val_SoGPXTL";
            this.val_SoGPXTL.Size = new System.Drawing.Size(200, 18);
            this.val_SoGPXTL.TabIndex = 8;
            this.val_SoGPXTL.Text = "—";
            // 
            // cap_SoGPXTL
            // 
            this.cap_SoGPXTL.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_SoGPXTL.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_SoGPXTL.Appearance.Options.UseFont = true;
            this.cap_SoGPXTL.Appearance.Options.UseForeColor = true;
            this.cap_SoGPXTL.Location = new System.Drawing.Point(20, 199);
            this.cap_SoGPXTL.Name = "cap_SoGPXTL";
            this.cap_SoGPXTL.Size = new System.Drawing.Size(54, 16);
            this.cap_SoGPXTL.TabIndex = 7;
            this.cap_SoGPXTL.Text = "Số GPXTL";
            // 
            // val_NamSX
            // 
            this.val_NamSX.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_NamSX.Appearance.Options.UseFont = true;
            this.val_NamSX.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_NamSX.Location = new System.Drawing.Point(160, 164);
            this.val_NamSX.Name = "val_NamSX";
            this.val_NamSX.Size = new System.Drawing.Size(200, 18);
            this.val_NamSX.TabIndex = 6;
            this.val_NamSX.Text = "—";
            // 
            // cap_NamSX
            // 
            this.cap_NamSX.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_NamSX.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_NamSX.Appearance.Options.UseFont = true;
            this.cap_NamSX.Appearance.Options.UseForeColor = true;
            this.cap_NamSX.Location = new System.Drawing.Point(20, 165);
            this.cap_NamSX.Name = "cap_NamSX";
            this.cap_NamSX.Size = new System.Drawing.Size(76, 16);
            this.cap_NamSX.TabIndex = 5;
            this.cap_NamSX.Text = "Năm sản xuất";
            // 
            // val_MaXe
            // 
            this.val_MaXe.Appearance.Font = new System.Drawing.Font("Roboto", 10F);
            this.val_MaXe.Appearance.Options.UseFont = true;
            this.val_MaXe.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.val_MaXe.Location = new System.Drawing.Point(160, 130);
            this.val_MaXe.Name = "val_MaXe";
            this.val_MaXe.Size = new System.Drawing.Size(200, 18);
            this.val_MaXe.TabIndex = 4;
            this.val_MaXe.Text = "—";
            // 
            // cap_MaXe
            // 
            this.cap_MaXe.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.cap_MaXe.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.cap_MaXe.Appearance.Options.UseFont = true;
            this.cap_MaXe.Appearance.Options.UseForeColor = true;
            this.cap_MaXe.Location = new System.Drawing.Point(20, 131);
            this.cap_MaXe.Name = "cap_MaXe";
            this.cap_MaXe.Size = new System.Drawing.Size(33, 16);
            this.cap_MaXe.TabIndex = 3;
            this.cap_MaXe.Text = "Mã xe";
            // 
            // lbl_TrangThai
            // 
            this.lbl_TrangThai.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_TrangThai.Appearance.Options.UseFont = true;
            this.lbl_TrangThai.Location = new System.Drawing.Point(150, 80);
            this.lbl_TrangThai.Name = "lbl_TrangThai";
            this.lbl_TrangThai.Size = new System.Drawing.Size(3, 18);
            this.lbl_TrangThai.TabIndex = 2;
            this.lbl_TrangThai.Text = " ";
            // 
            // lbl_HangXe
            // 
            this.lbl_HangXe.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_HangXe.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.lbl_HangXe.Appearance.Options.UseFont = true;
            this.lbl_HangXe.Appearance.Options.UseForeColor = true;
            this.lbl_HangXe.Location = new System.Drawing.Point(20, 80);
            this.lbl_HangXe.Name = "lbl_HangXe";
            this.lbl_HangXe.Size = new System.Drawing.Size(3, 18);
            this.lbl_HangXe.TabIndex = 1;
            this.lbl_HangXe.Text = " ";
            // 
            // lbl_BienSo
            // 
            this.lbl_BienSo.Appearance.Font = new System.Drawing.Font("Roboto", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbl_BienSo.Appearance.Options.UseFont = true;
            this.lbl_BienSo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_BienSo.Location = new System.Drawing.Point(18, 38);
            this.lbl_BienSo.Name = "lbl_BienSo";
            this.lbl_BienSo.Size = new System.Drawing.Size(342, 36);
            this.lbl_BienSo.TabIndex = 0;
            this.lbl_BienSo.Text = "Chọn một xe";
            // 
            // spacerRight
            // 
            this.spacerRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.spacerRight.Location = new System.Drawing.Point(1098, 84);
            this.spacerRight.Name = "spacerRight";
            this.spacerRight.Size = new System.Drawing.Size(10, 704);
            this.spacerRight.TabIndex = 3;
            // 
            // grp_List
            // 
            this.grp_List.AppearanceCaption.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold);
            this.grp_List.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.grp_List.AppearanceCaption.Options.UseFont = true;
            this.grp_List.AppearanceCaption.Options.UseForeColor = true;
            this.grp_List.Controls.Add(this.gridControl_XeTapLai);
            this.grp_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_List.Location = new System.Drawing.Point(12, 84);
            this.grp_List.Name = "grp_List";
            this.grp_List.Size = new System.Drawing.Size(1086, 704);
            this.grp_List.TabIndex = 2;
            this.grp_List.Text = "DANH SÁCH XE TẬP LÁI";
            // 
            // gridControl_XeTapLai
            // 
            this.gridControl_XeTapLai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_XeTapLai.Location = new System.Drawing.Point(2, 23);
            this.gridControl_XeTapLai.MainView = this.gridView_XeTapLai;
            this.gridControl_XeTapLai.Name = "gridControl_XeTapLai";
            this.gridControl_XeTapLai.Size = new System.Drawing.Size(1082, 679);
            this.gridControl_XeTapLai.TabIndex = 0;
            this.gridControl_XeTapLai.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_XeTapLai});
            // 
            // gridView_XeTapLai
            // 
            this.gridView_XeTapLai.Appearance.GroupRow.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_XeTapLai.Appearance.GroupRow.Options.UseFont = true;
            this.gridView_XeTapLai.Appearance.HeaderPanel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView_XeTapLai.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_XeTapLai.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_XeTapLai.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_XeTapLai.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.gridView_XeTapLai.Appearance.Row.Options.UseFont = true;
            this.gridView_XeTapLai.Appearance.Row.Options.UseTextOptions = true;
            this.gridView_XeTapLai.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_XeTapLai.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaXe,
            this.BienSoXe,
            this.HangXe,
            this.NamSX,
            this.SoGPXTL,
            this.NgayCapGPXTL,
            this.NgayHHGPXTL,
            this.ChuSoHuu,
            this.ThanhLy,
            this.DateTime_Use});
            this.gridView_XeTapLai.GridControl = this.gridControl_XeTapLai;
            this.gridView_XeTapLai.GroupCount = 1;
            this.gridView_XeTapLai.GroupFormat = "Hạng {1} {2}";
            this.gridView_XeTapLai.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "BienSoXe", null, "— {0} xe")});
            this.gridView_XeTapLai.Name = "gridView_XeTapLai";
            this.gridView_XeTapLai.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView_XeTapLai.OptionsBehavior.Editable = false;
            this.gridView_XeTapLai.OptionsBehavior.ReadOnly = true;
            this.gridView_XeTapLai.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.HangXe, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.BienSoXe, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView_XeTapLai.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_XeTapLai_RowCellStyle);
            this.gridView_XeTapLai.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gridView_XeTapLai_CustomDrawEmptyForeground);
            this.gridView_XeTapLai.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_XeTapLai_FocusedRowChanged);
            this.gridView_XeTapLai.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView_XeTapLai_CustomColumnDisplayText);
            // 
            // MaXe
            // 
            this.MaXe.Caption = "Mã xe";
            this.MaXe.FieldName = "MaXe";
            this.MaXe.Name = "MaXe";
            this.MaXe.Visible = true;
            this.MaXe.VisibleIndex = 0;
            this.MaXe.Width = 80;
            // 
            // BienSoXe
            // 
            this.BienSoXe.AppearanceCell.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.BienSoXe.AppearanceCell.Options.UseFont = true;
            this.BienSoXe.Caption = "Biển số";
            this.BienSoXe.FieldName = "BienSoXe";
            this.BienSoXe.Name = "BienSoXe";
            this.BienSoXe.Visible = true;
            this.BienSoXe.VisibleIndex = 1;
            this.BienSoXe.Width = 110;
            // 
            // HangXe
            // 
            this.HangXe.Caption = "Hạng xe";
            this.HangXe.FieldName = "HangGPLX";
            this.HangXe.Name = "HangXe";
            this.HangXe.Visible = true;
            this.HangXe.VisibleIndex = 2;
            this.HangXe.Width = 80;
            // 
            // NamSX
            // 
            this.NamSX.Caption = "Năm SX";
            this.NamSX.FieldName = "NamSX";
            this.NamSX.Name = "NamSX";
            this.NamSX.Visible = true;
            this.NamSX.VisibleIndex = 2;
            this.NamSX.Width = 80;
            // 
            // SoGPXTL
            // 
            this.SoGPXTL.Caption = "Số GPXTL";
            this.SoGPXTL.FieldName = "SoGPXTL";
            this.SoGPXTL.Name = "SoGPXTL";
            this.SoGPXTL.Visible = true;
            this.SoGPXTL.VisibleIndex = 3;
            this.SoGPXTL.Width = 120;
            // 
            // NgayCapGPXTL
            // 
            this.NgayCapGPXTL.Caption = "Ngày cấp GPXTL";
            this.NgayCapGPXTL.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.NgayCapGPXTL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.NgayCapGPXTL.FieldName = "NgayCapGPXTL";
            this.NgayCapGPXTL.Name = "NgayCapGPXTL";
            this.NgayCapGPXTL.Visible = true;
            this.NgayCapGPXTL.VisibleIndex = 4;
            this.NgayCapGPXTL.Width = 110;
            // 
            // NgayHHGPXTL
            // 
            this.NgayHHGPXTL.Caption = "Hết hạn GPXTL";
            this.NgayHHGPXTL.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.NgayHHGPXTL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.NgayHHGPXTL.FieldName = "NgayHHXTL";
            this.NgayHHGPXTL.Name = "NgayHHGPXTL";
            this.NgayHHGPXTL.ToolTip = "Đỏ: đã hết hạn · Cam: hết hạn trong 30 ngày";
            this.NgayHHGPXTL.Visible = true;
            this.NgayHHGPXTL.VisibleIndex = 5;
            this.NgayHHGPXTL.Width = 110;
            // 
            // ChuSoHuu
            // 
            this.ChuSoHuu.AppearanceCell.Options.UseTextOptions = true;
            this.ChuSoHuu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.ChuSoHuu.Caption = "Chủ sở hữu";
            this.ChuSoHuu.FieldName = "ChuSoHuu";
            this.ChuSoHuu.Name = "ChuSoHuu";
            this.ChuSoHuu.Visible = true;
            this.ChuSoHuu.VisibleIndex = 6;
            this.ChuSoHuu.Width = 170;
            // 
            // ThanhLy
            // 
            this.ThanhLy.Caption = "Tình trạng";
            this.ThanhLy.FieldName = "ThanhLy";
            this.ThanhLy.Name = "ThanhLy";
            this.ThanhLy.Visible = true;
            this.ThanhLy.VisibleIndex = 7;
            this.ThanhLy.Width = 110;
            // 
            // DateTime_Use
            // 
            this.DateTime_Use.Caption = "Sử dụng đến";
            this.DateTime_Use.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.DateTime_Use.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateTime_Use.FieldName = "SuDungDen";
            this.DateTime_Use.Name = "DateTime_Use";
            this.DateTime_Use.Visible = true;
            this.DateTime_Use.VisibleIndex = 8;
            this.DateTime_Use.Width = 110;
            // 
            // UcChild_XeDaoTao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_List);
            this.Controls.Add(this.spacerRight);
            this.Controls.Add(this.grp_Detail);
            this.Controls.Add(this.spacerTop);
            this.Controls.Add(this.pnl_Header);
            this.Name = "UcChild_XeDaoTao";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(1500, 800);
            this.Load += new System.EventHandler(this.UcChild_XeTap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Header)).EndInit();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp_Detail)).EndInit();
            this.grp_Detail.ResumeLayout(false);
            this.grp_Detail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch_TrangThai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SoHuu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grp_List)).EndInit();
            this.grp_List.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_XeTapLai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_XeTapLai)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnl_Header;
        private DevExpress.XtraEditors.LabelControl lbl_Title;
        private DevExpress.XtraEditors.LabelControl lbl_Desc;
        private DevExpress.XtraEditors.SimpleButton btn_ImportXml;
        private DevExpress.XtraEditors.SimpleButton btn_Export;
        private DevExpress.XtraEditors.SimpleButton btn_Refresh;
        private System.Windows.Forms.Panel spacerTop;
        private DevExpress.XtraEditors.GroupControl grp_List;
        private DevExpress.XtraGrid.GridControl gridControl_XeTapLai;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_XeTapLai;
        private DevExpress.XtraGrid.Columns.GridColumn MaXe;
        private DevExpress.XtraGrid.Columns.GridColumn BienSoXe;
        private DevExpress.XtraGrid.Columns.GridColumn HangXe;
        private DevExpress.XtraGrid.Columns.GridColumn NamSX;
        private DevExpress.XtraGrid.Columns.GridColumn SoGPXTL;
        private DevExpress.XtraGrid.Columns.GridColumn NgayCapGPXTL;
        private DevExpress.XtraGrid.Columns.GridColumn NgayHHGPXTL;
        private DevExpress.XtraGrid.Columns.GridColumn ChuSoHuu;
        private DevExpress.XtraGrid.Columns.GridColumn ThanhLy;
        private DevExpress.XtraGrid.Columns.GridColumn DateTime_Use;
        private System.Windows.Forms.Panel spacerRight;
        private DevExpress.XtraEditors.GroupControl grp_Detail;
        private DevExpress.XtraEditors.LabelControl lbl_BienSo;
        private DevExpress.XtraEditors.LabelControl lbl_HangXe;
        private DevExpress.XtraEditors.LabelControl lbl_TrangThai;
        private DevExpress.XtraEditors.LabelControl cap_MaXe;
        private DevExpress.XtraEditors.LabelControl val_MaXe;
        private DevExpress.XtraEditors.LabelControl cap_NamSX;
        private DevExpress.XtraEditors.LabelControl val_NamSX;
        private DevExpress.XtraEditors.LabelControl cap_SoGPXTL;
        private DevExpress.XtraEditors.LabelControl val_SoGPXTL;
        private DevExpress.XtraEditors.LabelControl cap_NgayCap;
        private DevExpress.XtraEditors.LabelControl val_NgayCap;
        private DevExpress.XtraEditors.LabelControl cap_NgayHH;
        private DevExpress.XtraEditors.LabelControl val_NgayHH;
        private DevExpress.XtraEditors.LabelControl cap_SuDungDen;
        private DevExpress.XtraEditors.LabelControl val_SuDungDen;
        private DevExpress.XtraEditors.LabelControl cap_SoHuu;
        private DevExpress.XtraEditors.TextEdit txt_SoHuu;
        private DevExpress.XtraEditors.LabelControl cap_ThanhLy;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch_TrangThai;
        private DevExpress.XtraEditors.SimpleButton btn_Edit;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.LabelControl lbl_Hint;
    }
}
