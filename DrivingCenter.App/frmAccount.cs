using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DrivingCenter.App.Helper;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmAccount : DevExpress.XtraEditors.XtraForm
    {
        private const int MinPasswordLength = 6;

        private readonly string _username;
        private SvgImage _eyeShow;
        private SvgImage _eyeHide;

        /// <summary>Tên hiển thị sau khi lưu (frmMain dùng để cập nhật thanh tiêu đề).</summary>
        public string DisplayName { get; private set; }

        /// <summary>Khóa lưu tên hiển thị của từng tài khoản trong AppPreferences.</summary>
        public static string DisplayNameKey(string username) => "DisplayName." + (username ?? string.Empty).Trim().ToLowerInvariant();

        public frmAccount() : this(null, null)
        {
        }

        public frmAccount(string username, string displayName)
        {
            InitializeComponent();
            _username = string.IsNullOrWhiteSpace(username) ? "—" : username.Trim();
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? _username : displayName.Trim();

            // Giao diện (màu, icon, tiêu đề mục...) nằm trong Designer. Ở đây chỉ lấy 2 icon con mắt
            // để đổi qua lại khi bấm hiện/ẩn mật khẩu (dùng chung với màn hình đăng nhập).
            var loginResources = new ComponentResourceManager(typeof(frmLogin));
            _eyeShow = loginResources.GetObject("btnShow.ImageOptions.SvgImage") as SvgImage;
            _eyeHide = loginResources.GetObject("btnHide.ImageOptions.SvgImage") as SvgImage;
        }

        private ButtonEdit[] PasswordEdits => new[] { txt_CurrentPass, txt_NewPass, txt_RepeatPass };

        private void frmAccount_Load(object sender, EventArgs e)
        {
            txt_Username.Text = _username;
            txt_DisplayName.Text = DisplayName;
            UpdateHeader();
            ActiveControl = txt_DisplayName;
            txt_DisplayName.SelectAll();
        }

        #region Vẽ giao diện

        /// <summary>
        /// Ảnh đại diện tròn với chữ cái đầu của tên hiển thị (nền gradient của header đặt trong Designer).
        /// Chữ đổi theo tên đang gõ nên phải vẽ bằng code.
        /// </summary>
        private void pnl_Header_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var avatar = new Rectangle(28, 28, 64, 64);
            using (var fill = new SolidBrush(Color.White))
                g.FillEllipse(fill, avatar);
            using (var font = new Font("Roboto", 20F, FontStyle.Bold))
                TextRenderer.DrawText(g, Initials(DisplayName), font, avatar, UiTheme.Brand,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }

        /// <summary>"Nguyễn Trung Hiếu" → "NH"; "admin" → "A".</summary>
        private static string Initials(string name)
        {
            var words = (name ?? string.Empty).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0) return "?";
            string first = words[0].Substring(0, 1);
            string last = words.Length > 1 ? words[words.Length - 1].Substring(0, 1) : string.Empty;
            return (first + last).ToUpperInvariant();
        }

        private void UpdateHeader()
        {
            string name = string.IsNullOrWhiteSpace(txt_DisplayName.Text) ? _username : txt_DisplayName.Text.Trim();
            lbl_HeaderName.Text = name;
            lbl_HeaderUser.Text = "Tên đăng nhập: " + _username;
            DisplayName = name;
            pnl_Header.Invalidate();
        }

        #endregion

        #region Mật khẩu

        private void Password_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var edit = (ButtonEdit)sender;
            edit.Properties.UseSystemPasswordChar = !edit.Properties.UseSystemPasswordChar;
            UpdatePasswordButton(edit);
            edit.Focus();
            edit.SelectionStart = edit.Text.Length;
        }

        private void UpdatePasswordButton(ButtonEdit edit)
        {
            bool hidden = edit.Properties.UseSystemPasswordChar;
            var button = edit.Properties.Buttons[0];
            button.ImageOptions.SvgImage = hidden ? _eyeShow : _eyeHide;
            button.ToolTip = hidden ? "Hiện mật khẩu" : "Ẩn mật khẩu";
        }

        private void Input_Changed(object sender, EventArgs e)
        {
            if (sender == txt_DisplayName) UpdateHeader();
            if (sender == txt_NewPass) UpdateStrength();
            ShowError(null);
        }

        /// <summary>Gợi ý độ mạnh mật khẩu mới: yếu / trung bình / mạnh.</summary>
        private void UpdateStrength()
        {
            string pass = txt_NewPass.Text;
            if (pass.Length == 0)
            {
                lbl_Strength.Text = $"Tối thiểu {MinPasswordLength} ký tự, nên có cả chữ hoa, chữ số và ký tự đặc biệt.";
                lbl_Strength.Appearance.ForeColor = UiTheme.TextMuted;
                return;
            }

            int score = 0;
            if (pass.Length >= MinPasswordLength) score++;
            if (pass.Length >= 10) score++;
            if (pass.Any(char.IsUpper) && pass.Any(char.IsLower)) score++;
            if (pass.Any(char.IsDigit)) score++;
            if (pass.Any(c => !char.IsLetterOrDigit(c))) score++;

            if (pass.Length < MinPasswordLength)
                SetStrength($"Quá ngắn — cần ít nhất {MinPasswordLength} ký tự", UiTheme.Danger);
            else if (score <= 2)
                SetStrength("Độ mạnh: Yếu", UiTheme.Danger);
            else if (score <= 3)
                SetStrength("Độ mạnh: Trung bình", UiTheme.Warning);
            else
                SetStrength("Độ mạnh: Mạnh", UiTheme.Success);
        }

        private void SetStrength(string text, Color color)
        {
            lbl_Strength.Text = "●  " + text;
            lbl_Strength.Appearance.ForeColor = color;
        }

        #endregion

        #region Lưu

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string displayName = txt_DisplayName.Text.Trim();
            if (displayName.Length == 0)
            {
                ShowError("Vui lòng nhập tên hiển thị.", txt_DisplayName);
                return;
            }

            bool doiMatKhau = PasswordEdits.Any(x => x.Text.Length > 0);
            if (doiMatKhau && !ValidatePassword()) return;

            AppPreferences.SetString(DisplayNameKey(_username), displayName);
            DisplayName = displayName;

            if (doiMatKhau)
            {
                // Ứng dụng chưa có bảng tài khoản trên máy chủ (đăng nhập chưa xác thực mật khẩu),
                // nên không có nơi lưu mật khẩu mới: báo rõ thay vì giả vờ "đổi thành công".
                XtraMessageBox.Show(this,
                    "Đã lưu tên hiển thị.\n\nMật khẩu CHƯA được đổi: hệ thống chưa kết nối cơ sở dữ liệu tài khoản.",
                    "Tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidatePassword()
        {
            if (txt_CurrentPass.Text.Length == 0)
                return ShowError("Vui lòng nhập mật khẩu hiện tại.", txt_CurrentPass);
            if (txt_NewPass.Text.Length < MinPasswordLength)
                return ShowError($"Mật khẩu mới cần ít nhất {MinPasswordLength} ký tự.", txt_NewPass);
            if (txt_NewPass.Text == txt_CurrentPass.Text)
                return ShowError("Mật khẩu mới phải khác mật khẩu hiện tại.", txt_NewPass);
            if (txt_RepeatPass.Text != txt_NewPass.Text)
                return ShowError("Mật khẩu nhập lại không khớp.", txt_RepeatPass);
            return true;
        }

        /// <summary>Hiện lỗi dưới form và đưa con trỏ về ô bị lỗi; luôn trả false để dùng trong ValidatePassword.</summary>
        private bool ShowError(string message, Control focus = null)
        {
            lbl_Error.Text = message ?? string.Empty;
            lbl_Error.Visible = !string.IsNullOrEmpty(message);
            focus?.Focus();
            return false;
        }

        #endregion
    }
}
