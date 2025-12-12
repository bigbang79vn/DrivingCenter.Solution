namespace DrivingCenter.App
{
    partial class HelperGeoJSON
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
            this.btn_GetProvinces = new DevExpress.XtraEditors.SimpleButton();
            this.txt_NameProvince = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NameProvince.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_GetProvinces
            // 
            this.btn_GetProvinces.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GetProvinces.Appearance.Options.UseFont = true;
            this.btn_GetProvinces.Location = new System.Drawing.Point(24, 73);
            this.btn_GetProvinces.Name = "btn_GetProvinces";
            this.btn_GetProvinces.Size = new System.Drawing.Size(293, 23);
            this.btn_GetProvinces.TabIndex = 0;
            this.btn_GetProvinces.Text = "Get Province";
            this.btn_GetProvinces.Click += new System.EventHandler(this.btn_GetProvinces_Click);
            // 
            // txt_NameProvince
            // 
            this.txt_NameProvince.Location = new System.Drawing.Point(24, 33);
            this.txt_NameProvince.Name = "txt_NameProvince";
            this.txt_NameProvince.Properties.Appearance.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NameProvince.Properties.Appearance.Options.UseFont = true;
            this.txt_NameProvince.Size = new System.Drawing.Size(293, 24);
            this.txt_NameProvince.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nhập vào tên tỉnh cần lấy";
            // 
            // HelperGeoJSON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 123);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_NameProvince);
            this.Controls.Add(this.btn_GetProvinces);
            this.Name = "HelperGeoJSON";
            this.Text = "HelperGeoJSON";
            ((System.ComponentModel.ISupportInitialize)(this.txt_NameProvince.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_GetProvinces;
        private DevExpress.XtraEditors.TextEdit txt_NameProvince;
        private System.Windows.Forms.Label label1;
    }
}