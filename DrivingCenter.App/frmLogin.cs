using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DrivingCenter.App.Helper;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        // Chỉ ghi nhớ tên đăng nhập, không bao giờ lưu mật khẩu
        private static readonly string RememberFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrivingCenter", "login_user.txt");

        private SvgImage _eyeShow;
        private SvgImage _eyeHide;

        public frmLogin()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            pic_Logo.Image = UiTheme.RenderSvg(UiTheme.Svg("svgimages/icon%20builder/travel_car.svg"), 56, Color.White);
            UiTheme.StylePrimary(btnLogin);

            var resources = new ComponentResourceManager(typeof(frmLogin));
            _eyeShow = resources.GetObject("btnShow.ImageOptions.SvgImage") as SvgImage;
            _eyeHide = resources.GetObject("btnHide.ImageOptions.SvgImage") as SvgImage;
            txtPassword.Properties.Buttons[0].ImageOptions.SvgImageSize = new Size(18, 18);
            UpdatePasswordButton();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            lbl_Version.Text = $"Phiên bản {version.Major}.{version.Minor}.{version.Build}";
            lbl_Copyright.Text = $"© {DateTime.Now.Year} Trung tâm GDNN Việt Nhật";
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string remembered = LoadRememberedUser();
            if (!string.IsNullOrEmpty(remembered))
            {
                txtUserName.Text = remembered;
                chkRemember.Checked = true;
                ActiveControl = txtPassword;
            }
            else
            {
                ActiveControl = txtUserName;
            }
            UpdateCapsLock();
        }

        /// <summary>
        /// Nền gradient màu thương hiệu cho panel bên trái, kèm vài vòng tròn trang trí mờ.
        /// </summary>
        private void pnl_Brand_Paint(object sender, PaintEventArgs e)
        {
            var rect = pnl_Brand.ClientRectangle;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new LinearGradientBrush(rect, Color.FromArgb(58, 85, 128), Color.FromArgb(96, 132, 186), 60f))
                g.FillRectangle(brush, rect);

            using (var circle = new SolidBrush(Color.FromArgb(22, Color.White)))
            {
                g.FillEllipse(circle, rect.Right - 170, rect.Bottom - 190, 260, 260);
                g.FillEllipse(circle, rect.Right - 90, -60, 180, 180);
            }
        }

        #region Mật khẩu & Caps Lock

        private void txtPassword_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            txtPassword.Properties.UseSystemPasswordChar = !txtPassword.Properties.UseSystemPasswordChar;
            UpdatePasswordButton();
            txtPassword.Focus();
            txtPassword.SelectionStart = txtPassword.Text.Length;
        }

        private void UpdatePasswordButton()
        {
            bool hidden = txtPassword.Properties.UseSystemPasswordChar;
            var button = txtPassword.Properties.Buttons[0];
            button.ImageOptions.SvgImage = hidden ? _eyeShow : _eyeHide;
            button.ToolTip = hidden ? "Hiện mật khẩu" : "Ẩn mật khẩu";
        }

        private void Input_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateCapsLock();
        }

        private void Input_FocusChanged(object sender, EventArgs e)
        {
            UpdateCapsLock();
        }

        private void UpdateCapsLock()
        {
            lbl_CapsLock.Visible = Control.IsKeyLocked(Keys.CapsLock);
        }

        #endregion

        #region Ghi nhớ tên đăng nhập

        private static string LoadRememberedUser()
        {
            try
            {
                return File.Exists(RememberFile) ? File.ReadAllText(RememberFile).Trim() : null;
            }
            catch
            {
                return null;
            }
        }

        private void SaveRememberedUser()
        {
            try
            {
                if (chkRemember.Checked)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(RememberFile));
                    File.WriteAllText(RememberFile, txtUserName.Text.Trim());
                }
                else if (File.Exists(RememberFile))
                {
                    File.Delete(RememberFile);
                }
            }
            catch
            {
                // Không ghi nhớ được cũng không chặn việc đăng nhập
            }
        }

        #endregion

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                txtUserName.ErrorText = "Vui lòng nhập tên đăng nhập";
                txtUserName.Focus();
                return;
            }
            txtUserName.ErrorText = string.Empty;

            SaveRememberedUser();

            this.Hide();
            using (frmMain f = new frmMain { CurrentUser = txtUserName.Text.Trim() })
            {
                f.ShowDialog();
            }

            // Sau khi MainForm đóng, quay trở lại LoginForm
            this.Show();
            txtPassword.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
