namespace DrivingCenter.App
{
    partial class frmCabinClient
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
            this.lbl_Running = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Detail = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            //
            // lbl_Running
            //
            this.lbl_Running.Appearance.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Running.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(222)))), ((int)(((byte)(128)))));
            this.lbl_Running.Appearance.Options.UseFont = true;
            this.lbl_Running.Appearance.Options.UseForeColor = true;
            this.lbl_Running.Location = new System.Drawing.Point(12, 8);
            this.lbl_Running.Name = "lbl_Running";
            this.lbl_Running.Size = new System.Drawing.Size(92, 19);
            this.lbl_Running.TabIndex = 0;
            this.lbl_Running.Text = "●  CABIN 1 · ONLINE";
            //
            // lbl_Detail
            //
            this.lbl_Detail.Appearance.Font = new System.Drawing.Font("Roboto", 9F);
            this.lbl_Detail.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lbl_Detail.Appearance.Options.UseFont = true;
            this.lbl_Detail.Appearance.Options.UseForeColor = true;
            this.lbl_Detail.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Detail.Location = new System.Drawing.Point(12, 32);
            this.lbl_Detail.Name = "lbl_Detail";
            this.lbl_Detail.Size = new System.Drawing.Size(236, 16);
            this.lbl_Detail.TabIndex = 1;
            this.lbl_Detail.Text = "Đang chờ lệnh từ máy chủ...";
            //
            // frmCabinClient
            //
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 56);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_Detail);
            this.Controls.Add(this.lbl_Running);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCabinClient";
            this.Opacity = 0.85D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cabin Client";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmCabinClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbl_Running;
        private DevExpress.XtraEditors.LabelControl lbl_Detail;
    }
}
