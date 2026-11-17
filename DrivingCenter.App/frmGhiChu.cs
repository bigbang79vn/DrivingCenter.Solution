using DevExpress.XtraEditors;
using DrivingCenter.App.DAO;
using DrivingCenter.App.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    /// <summary>
    /// Hộp thoại khóa phiên học: nhập lý do (lưu vào DiemDanhNguoiLx.GhiChu) rồi đặt ActivePhien = 0.
    /// Trả về DialogResult.OK khi khóa thành công.
    /// </summary>
    public partial class frmGhiChu : DevExpress.XtraEditors.XtraForm
    {
        private string _diemdanhnguoilxId;
        private readonly PhienHocDAO _phienHocDao = new PhienHocDAO();

        public frmGhiChu()
        {
            InitializeComponent();
            ApplyTheme();
        }

        public frmGhiChu(string diemdanhnguoilxId) : this()
        {
            _diemdanhnguoilxId = diemdanhnguoilxId;
            lbl_MaPhien.Text = "Mã phiên: " + diemdanhnguoilxId;
        }

        private void ApplyTheme()
        {
            pic_Icon.Image = UiTheme.RenderSvg(UiTheme.Svg("svgimages/icon%20builder/security_lock.svg"), 44, UiTheme.Danger);

            UiTheme.StyleDanger(btn_AcceptGhiChu);
            UiTheme.SetIcon(btn_AcceptGhiChu, "svgimages/icon%20builder/security_lock.svg", 16);

            UpdateCounter();
        }

        private void pnl_Header_Paint(object sender, PaintEventArgs e)
        {
            var panel = (Control)sender;
            using (var pen = new Pen(UiTheme.Muted))
                e.Graphics.DrawLine(pen, 0, panel.Height - 1, panel.Width, panel.Height - 1);
        }

        private void pnl_Footer_Paint(object sender, PaintEventArgs e)
        {
            var panel = (Control)sender;
            using (var pen = new Pen(UiTheme.Muted))
                e.Graphics.DrawLine(pen, 0, 0, panel.Width, 0);
        }

        /// <summary>
        /// Lý do chọn nhanh: ô trống thì điền, đã có nội dung thì nối thêm (không vượt quá giới hạn cột).
        /// </summary>
        private void btn_Reason_Click(object sender, EventArgs e)
        {
            string reason = ((SimpleButton)sender).Text;
            string current = memo_GhiChu.Text.Trim();

            if (current.IndexOf(reason, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                memo_GhiChu.Focus();
                return;
            }

            string next = string.IsNullOrEmpty(current) ? reason : current + "; " + reason;
            if (next.Length > memo_GhiChu.Properties.MaxLength)
            {
                lbl_Error.Text = $"Ghi chú tối đa {memo_GhiChu.Properties.MaxLength} ký tự.";
                lbl_Error.Visible = true;
                return;
            }

            memo_GhiChu.Text = next;
            memo_GhiChu.Focus();
            memo_GhiChu.SelectionStart = memo_GhiChu.Text.Length;
        }

        private void memo_GhiChu_EditValueChanged(object sender, EventArgs e)
        {
            UpdateCounter();
            if (lbl_Error.Visible && !string.IsNullOrWhiteSpace(memo_GhiChu.Text))
                lbl_Error.Visible = false;
        }

        private void UpdateCounter()
        {
            int max = memo_GhiChu.Properties.MaxLength;
            int len = memo_GhiChu.Text.Length;
            lbl_Counter.Text = $"{len} / {max}";
            lbl_Counter.Appearance.ForeColor = len >= max ? UiTheme.Warning : Color.Gray;
        }

        private void memo_GhiChu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btn_AcceptGhiChu.PerformClick();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btn_AcceptGhiChu_Click(object sender, EventArgs e)
        {
            string note = memo_GhiChu.Text.Trim();
            if (string.IsNullOrEmpty(note))
            {
                lbl_Error.Text = "Vui lòng nhập lý do khóa phiên.";
                lbl_Error.Visible = true;
                memo_GhiChu.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                _phienHocDao.LockSession(_diemdanhnguoilxId, note);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, "Không khóa được phiên học: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
