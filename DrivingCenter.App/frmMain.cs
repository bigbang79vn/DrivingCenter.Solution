using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DrivingCenter.App.Helper;
using DrivingCenter.App.UserControl;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private const string SkinLight = "Basic";
        private const string SkinDark = "Office 2019 Black";
        private const string PrefDarkMode = "DarkMode";

        private bool _applyingTheme;

        /// <summary>
        /// Tên đăng nhập hiển thị trên thanh tiêu đề (frmLogin truyền vào).
        /// </summary>
        public string CurrentUser { get; set; }

        public frmMain()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            acc_DangXuat.ImageOptions.SvgImage = UiTheme.Svg("svgimages/xaf/action_exit.svg");
            itemUser.ImageOptions.SvgImage = UiTheme.Svg("svgimages/icon%20builder/actions_user.svg");
            acc_CabinManager.ImageOptions.SvgImage = UiTheme.Svg("svgimages/business%20objects/bo_scheduler.svg");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Tên hiển thị người dùng tự đặt trong "Tài khoản" (lưu trên máy), mặc định là tên đăng nhập
            itemUser.Caption = string.IsNullOrWhiteSpace(CurrentUser)
                ? "Người dùng"
                : AppPreferences.GetString(frmAccount.DisplayNameKey(CurrentUser), CurrentUser);

            // Khôi phục chế độ sáng/tối lần trước
            _applyingTheme = true;
            barToggleSwitchItem.Checked = AppPreferences.GetBool(PrefDarkMode);
            _applyingTheme = false;
            SetSkin(barToggleSwitchItem.Checked);

            ShowPage<Uc_Home>(acc_Home);
        }

        #region Điều hướng

        /// <summary>
        /// Hiển thị trang vào MainContainer: giải phóng trang cũ (dừng timer, giải phóng bộ nhớ),
        /// đánh dấu mục menu đang chọn, cập nhật tiêu đề "Nhóm › Trang".
        /// </summary>
        private void ShowPage<T>(AccordionControlElement menuItem) where T : Control, new()
        {
            // Bấm lại mục đang mở: giữ nguyên, không tải lại
            if (MainContainer.Controls.Count == 1 && MainContainer.Controls[0] is T)
            {
                accMain.SelectElement(menuItem);
                return;
            }

            IOverlaySplashScreenHandle overlay = null;
            Cursor = Cursors.WaitCursor;
            MainContainer.SuspendLayout();
            try
            {
                if (MainContainer.IsHandleCreated && MainContainer.Visible)
                    overlay = SplashScreenManager.ShowOverlayForm(MainContainer);

                foreach (var old in MainContainer.Controls.Cast<Control>().ToList())
                    old.Dispose();

                var page = new T { Dock = DockStyle.Fill };
                MainContainer.Controls.Add(page);

                accMain.SelectElement(menuItem);
                string group = (menuItem.OwnerElement as AccordionControlElement)?.Text;
                itemNav.Caption = string.IsNullOrEmpty(group) || menuItem == acc_Home
                    ? menuItem.Text
                    : $"{ToTitle(group)}  ›  {menuItem.Text}";
            }
            catch (Exception ex)
            {
                // Màn hình lỗi khi tải: báo cho người dùng thay vì để văng exception
                foreach (var broken in MainContainer.Controls.Cast<Control>().ToList())
                    broken.Dispose();

                // new T() bọc lỗi constructor trong TargetInvocationException: lấy lỗi thật và ghi log đầy đủ
                Exception real = ErrorLog.Unwrap(ex);
                string logFile = ErrorLog.Write($"ShowPage<{typeof(T).Name}>", ex);

                // Đóng overlay trước khi báo lỗi: trước đây hộp thoại hiện dưới vòng loading nên trông như bị treo
                CloseOverlay(ref overlay);
                MainContainer.ResumeLayout();
                Cursor = Cursors.Default;

                XtraMessageBox.Show(this,
                    $"Không mở được màn hình \"{menuItem.Text}\".\n\nChi tiết: {real.GetType().Name}: {real.Message}"
                    + (logFile == null ? "" : $"\n\nĐã ghi log: {logFile}"),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                CloseOverlay(ref overlay);
                Cursor = Cursors.Default;
            }
            MainContainer.ResumeLayout();
        }

        private static void CloseOverlay(ref IOverlaySplashScreenHandle overlay)
        {
            if (overlay == null) return;
            try { SplashScreenManager.CloseOverlayForm(overlay); }
            catch { /* overlay đã đóng hoặc control nền đã bị hủy */ }
            overlay = null;
        }

        // "CABIN ĐIỆN TỬ" -> "Cabin điện tử"
        private static string ToTitle(string upper)
        {
            string lower = upper.ToLower(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
            return lower.Length == 0 ? lower : char.ToUpper(lower[0]) + lower.Substring(1);
        }

        private void acc_Home_Click(object sender, EventArgs e) => ShowPage<Uc_Home>(acc_Home);

        private void acc_LichHoc_Click(object sender, EventArgs e) => ShowPage<Uc_DaoTao>(acc_LichHoc);

        private void acc_DAT_Click(object sender, EventArgs e) => ShowPage<Uc_DAT>(acc_DAT);

        private void acc_VNDC_Click(object sender, EventArgs e) => ShowPage<Uc_VNDC>(acc_VNDC);

        private void acc_CabinEco_Click(object sender, EventArgs e) => ShowPage<Uc_CabinEco>(acc_CabinEco);

        private void acc_CabinEMC_Click(object sender, EventArgs e) => ShowPage<Uc_CabinEMC>(acc_CabinEMC);

        private void acc_CabinManager_Click(object sender, EventArgs e) => ShowPage<Uc_CabinManager>(acc_CabinManager);

        private void acc_TongHopXe_Click(object sender, EventArgs e) => ShowPage<Uc_BaoCaoTheoXe>(acc_TongHopXe);

        private void acc_TotNghiep_Click(object sender, EventArgs e) => ShowPage<Uc_Gradute>(acc_TotNghiep);

        private void acc_Print_Click(object sender, EventArgs e) => ShowPage<Uc_Print>(acc_Print);

        #endregion

        #region Hệ thống

        private void acc_TaiKhoan_Click(object sender, EventArgs e)
        {
            using (frmAccount f = new frmAccount(CurrentUser, itemUser.Caption))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                    itemUser.Caption = f.DisplayName;
            }
        }

        private void acc_DangXuat_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(this, "Đăng xuất khỏi hệ thống?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // frmLogin mở frmMain bằng ShowDialog: đóng form là quay về màn hình đăng nhập
                Close();
            }
        }

        private void barToggleSwitchItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (_applyingTheme) return;

            SetSkin(barToggleSwitchItem.Checked);
            AppPreferences.SetBool(PrefDarkMode, barToggleSwitchItem.Checked);
        }

        private static void SetSkin(bool dark)
        {
            UserLookAndFeel.Default.SetSkinStyle(dark ? SkinDark : SkinLight);
        }

        #endregion
    }
}
