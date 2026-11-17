namespace DrivingCenter.App.UserControl
{
    partial class UcCabinClientCard
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
            this.grp_Cabin = new DevExpress.XtraEditors.GroupControl();
            this.gridControl_Log = new DevExpress.XtraGrid.GridControl();
            this.gridView_Log = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_ThoiGian = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_HocVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_SuKien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnl_Info = new DevExpress.XtraEditors.PanelControl();
            this.btn_Unassign = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_LastSeen = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Cccd = new DevExpress.XtraEditors.LabelControl();
            this.lbl_HocVien = new DevExpress.XtraEditors.LabelControl();
            this.lbl_HocVienCaption = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Ip = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Status = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grp_Cabin)).BeginInit();
            this.grp_Cabin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Log)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Log)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Info)).BeginInit();
            this.pnl_Info.SuspendLayout();
            this.SuspendLayout();
            //
            // grp_Cabin
            //
            this.grp_Cabin.AppearanceCaption.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold);
            this.grp_Cabin.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(109)))), ((int)(((byte)(156)))));
            this.grp_Cabin.AppearanceCaption.Options.UseFont = true;
            this.grp_Cabin.AppearanceCaption.Options.UseForeColor = true;
            this.grp_Cabin.Controls.Add(this.gridControl_Log);
            this.grp_Cabin.Controls.Add(this.pnl_Info);
            this.grp_Cabin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Cabin.Location = new System.Drawing.Point(0, 0);
            this.grp_Cabin.Name = "grp_Cabin";
            this.grp_Cabin.Size = new System.Drawing.Size(560, 420);
            this.grp_Cabin.TabIndex = 0;
            this.grp_Cabin.Text = "CABIN 1";
            //
            // gridControl_Log
            //
            this.gridControl_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_Log.Location = new System.Drawing.Point(2, 173);
            this.gridControl_Log.MainView = this.gridView_Log;
            this.gridControl_Log.Name = "gridControl_Log";
            this.gridControl_Log.Size = new System.Drawing.Size(556, 245);
            this.gridControl_Log.TabIndex = 1;
            this.gridControl_Log.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Log});
            //
            // gridView_Log
            //
            this.gridView_Log.Appearance.Row.Font = new System.Drawing.Font("Roboto", 9F);
            this.gridView_Log.Appearance.Row.Options.UseFont = true;
            this.gridView_Log.Appearance.ViewCaption.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridView_Log.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.gridView_Log.Appearance.ViewCaption.Options.UseFont = true;
            this.gridView_Log.Appearance.ViewCaption.Options.UseForeColor = true;
            this.gridView_Log.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gridView_Log.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView_Log.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_ThoiGian,
            this.col_HocVien,
            this.col_SuKien});
            this.gridView_Log.GridControl = this.gridControl_Log;
            this.gridView_Log.Name = "gridView_Log";
            this.gridView_Log.OptionsBehavior.Editable = false;
            this.gridView_Log.OptionsView.ShowGroupPanel = false;
            this.gridView_Log.OptionsView.ShowIndicator = false;
            this.gridView_Log.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView_Log.OptionsView.ShowViewCaption = true;
            this.gridView_Log.RowHeight = 26;
            this.gridView_Log.ViewCaption = "LỊCH SỬ KẾT NỐI";
            //
            // col_ThoiGian
            //
            this.col_ThoiGian.Caption = "Thời gian";
            this.col_ThoiGian.DisplayFormat.FormatString = "HH:mm:ss dd/MM";
            this.col_ThoiGian.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.col_ThoiGian.FieldName = "ThoiGian";
            this.col_ThoiGian.Name = "col_ThoiGian";
            this.col_ThoiGian.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.col_ThoiGian.Visible = true;
            this.col_ThoiGian.VisibleIndex = 0;
            this.col_ThoiGian.Width = 110;
            //
            // col_HocVien
            //
            this.col_HocVien.Caption = "Học viên";
            this.col_HocVien.FieldName = "HocVien";
            this.col_HocVien.Name = "col_HocVien";
            this.col_HocVien.Visible = true;
            this.col_HocVien.VisibleIndex = 1;
            this.col_HocVien.Width = 150;
            //
            // col_SuKien
            //
            this.col_SuKien.Caption = "Sự kiện";
            this.col_SuKien.FieldName = "SuKien";
            this.col_SuKien.Name = "col_SuKien";
            this.col_SuKien.Visible = true;
            this.col_SuKien.VisibleIndex = 2;
            this.col_SuKien.Width = 280;
            //
            // pnl_Info
            //
            this.pnl_Info.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Info.Controls.Add(this.btn_Unassign);
            this.pnl_Info.Controls.Add(this.lbl_LastSeen);
            this.pnl_Info.Controls.Add(this.lbl_Cccd);
            this.pnl_Info.Controls.Add(this.lbl_HocVien);
            this.pnl_Info.Controls.Add(this.lbl_HocVienCaption);
            this.pnl_Info.Controls.Add(this.lbl_Ip);
            this.pnl_Info.Controls.Add(this.lbl_Status);
            this.pnl_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Info.Location = new System.Drawing.Point(2, 27);
            this.pnl_Info.Name = "pnl_Info";
            this.pnl_Info.Size = new System.Drawing.Size(556, 146);
            this.pnl_Info.TabIndex = 0;
            //
            // btn_Unassign
            //
            this.btn_Unassign.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.btn_Unassign.Appearance.Options.UseFont = true;
            this.btn_Unassign.Location = new System.Drawing.Point(12, 104);
            this.btn_Unassign.Name = "btn_Unassign";
            this.btn_Unassign.Size = new System.Drawing.Size(104, 32);
            this.btn_Unassign.TabIndex = 0;
            this.btn_Unassign.Text = "Bỏ gán";
            this.btn_Unassign.ToolTip = "Gỡ học viên khỏi cabin này";
            this.btn_Unassign.Click += new System.EventHandler(this.btn_Unassign_Click);
            //
            // lbl_LastSeen
            //
            this.lbl_LastSeen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_LastSeen.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_LastSeen.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_LastSeen.Appearance.Options.UseFont = true;
            this.lbl_LastSeen.Appearance.Options.UseForeColor = true;
            this.lbl_LastSeen.Appearance.Options.UseTextOptions = true;
            this.lbl_LastSeen.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_LastSeen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_LastSeen.Location = new System.Drawing.Point(284, 30);
            this.lbl_LastSeen.Name = "lbl_LastSeen";
            this.lbl_LastSeen.Size = new System.Drawing.Size(260, 15);
            this.lbl_LastSeen.TabIndex = 7;
            this.lbl_LastSeen.Text = "Chưa nhận tín hiệu";
            //
            // lbl_Cccd
            //
            this.lbl_Cccd.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lbl_Cccd.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Cccd.Appearance.Options.UseFont = true;
            this.lbl_Cccd.Appearance.Options.UseForeColor = true;
            this.lbl_Cccd.Location = new System.Drawing.Point(12, 74);
            this.lbl_Cccd.Name = "lbl_Cccd";
            this.lbl_Cccd.Size = new System.Drawing.Size(51, 16);
            this.lbl_Cccd.TabIndex = 6;
            this.lbl_Cccd.Text = "CCCD: —";
            //
            // lbl_HocVien
            //
            this.lbl_HocVien.Appearance.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_HocVien.Appearance.Options.UseFont = true;
            this.lbl_HocVien.Location = new System.Drawing.Point(12, 50);
            this.lbl_HocVien.Name = "lbl_HocVien";
            this.lbl_HocVien.Size = new System.Drawing.Size(150, 19);
            this.lbl_HocVien.TabIndex = 5;
            this.lbl_HocVien.Text = "Chưa gán học viên";
            //
            // lbl_HocVienCaption
            //
            this.lbl_HocVienCaption.Appearance.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_HocVienCaption.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_HocVienCaption.Appearance.Options.UseFont = true;
            this.lbl_HocVienCaption.Appearance.Options.UseForeColor = true;
            this.lbl_HocVienCaption.Location = new System.Drawing.Point(12, 32);
            this.lbl_HocVienCaption.Name = "lbl_HocVienCaption";
            this.lbl_HocVienCaption.Size = new System.Drawing.Size(55, 13);
            this.lbl_HocVienCaption.TabIndex = 4;
            this.lbl_HocVienCaption.Text = "HỌC VIÊN";
            //
            // lbl_Ip
            //
            this.lbl_Ip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Ip.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_Ip.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lbl_Ip.Appearance.Options.UseFont = true;
            this.lbl_Ip.Appearance.Options.UseForeColor = true;
            this.lbl_Ip.Appearance.Options.UseTextOptions = true;
            this.lbl_Ip.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_Ip.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Ip.Location = new System.Drawing.Point(284, 8);
            this.lbl_Ip.Name = "lbl_Ip";
            this.lbl_Ip.Size = new System.Drawing.Size(260, 15);
            this.lbl_Ip.TabIndex = 3;
            this.lbl_Ip.Text = "—";
            //
            // lbl_Status
            //
            this.lbl_Status.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Status.Appearance.Options.UseFont = true;
            this.lbl_Status.Location = new System.Drawing.Point(12, 8);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(105, 16);
            this.lbl_Status.TabIndex = 2;
            this.lbl_Status.Text = "●  Chưa kết nối";
            //
            // UcCabinClientCard
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_Cabin);
            this.Name = "UcCabinClientCard";
            this.Size = new System.Drawing.Size(560, 420);
            ((System.ComponentModel.ISupportInitialize)(this.grp_Cabin)).EndInit();
            this.grp_Cabin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Log)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Log)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Info)).EndInit();
            this.pnl_Info.ResumeLayout(false);
            this.pnl_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grp_Cabin;
        private DevExpress.XtraEditors.PanelControl pnl_Info;
        private DevExpress.XtraEditors.LabelControl lbl_Status;
        private DevExpress.XtraEditors.LabelControl lbl_Ip;
        private DevExpress.XtraEditors.LabelControl lbl_LastSeen;
        private DevExpress.XtraEditors.LabelControl lbl_HocVienCaption;
        private DevExpress.XtraEditors.LabelControl lbl_HocVien;
        private DevExpress.XtraEditors.LabelControl lbl_Cccd;
        private DevExpress.XtraEditors.SimpleButton btn_Unassign;
        private DevExpress.XtraGrid.GridControl gridControl_Log;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Log;
        private DevExpress.XtraGrid.Columns.GridColumn col_ThoiGian;
        private DevExpress.XtraGrid.Columns.GridColumn col_HocVien;
        private DevExpress.XtraGrid.Columns.GridColumn col_SuKien;
    }
}
