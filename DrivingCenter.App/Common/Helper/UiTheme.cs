using DevExpress.Images;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DrivingCenter.App.Helper
{
    /// <summary>
    /// Bộ màu và các hàm tạo kiểu dùng chung cho các màn hình danh mục,
    /// giúp các UserControl có cùng phong cách.
    /// </summary>
    public static class UiTheme
    {
        public static readonly Color Brand = Color.FromArgb(78, 109, 156);
        public static readonly Color BrandDark = Color.FromArgb(58, 85, 128);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Warning = Color.FromArgb(217, 119, 6);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color DangerDark = Color.FromArgb(185, 28, 28);
        public static readonly Color Muted = Color.FromArgb(203, 213, 225);
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);

        public static SvgImage Svg(string key)
        {
            try { return ImageResourceCache.Default.GetSvgImage(key); }
            catch { return null; }
        }

        /// <summary>
        /// Gán icon SVG cho nút. Nút nền màu (StylePrimary/StyleDanger) sẽ nhận icon trắng
        /// vì icon SVG gốc nhiều màu bị chìm trên nền đậm.
        /// </summary>
        public static void SetIcon(SimpleButton button, string svgKey, int size = 18)
        {
            var svg = Svg(svgKey);
            if (svg == null) return;
            button.ImageOptions.ImageUri.Uri = string.Empty;
            button.ImageOptions.SvgImage = svg;
            button.ImageOptions.SvgImageSize = new Size(size, size);
            if (button.Appearance.Options.UseBackColor)
                ApplyWhiteIcon(button);
        }

        private static void ApplyWhiteIcon(SimpleButton button)
        {
            var svg = button.ImageOptions.SvgImage;
            if (svg == null) return;

            int size = button.ImageOptions.SvgImageSize.IsEmpty ? 18 : button.ImageOptions.SvgImageSize.Width;
            Image white = RenderSvg(svg, size, Color.White);
            if (white == null) return;

            button.ImageOptions.SvgImage = null;
            button.ImageOptions.Image = white;
        }

        /// <summary>
        /// Render icon SVG thành ảnh một màu (giữ độ trong suốt), dùng cho nút nền màu hoặc icon minh họa.
        /// </summary>
        public static Image RenderSvg(SvgImage svg, int size, Color color)
        {
            if (svg == null) return null;
            try
            {
                double scale = svg.Width > 0 ? size / svg.Width : 1d;
                // KHÔNG Dispose ảnh Render trả về: DevExpress cache ảnh đã render của SvgImage và trả lại chính bản
                // trong cache. Trước đây "using" hủy bản đó → mọi control khác dùng icon ấy (vd. nút Làm mới
                // "Refresh;Size16x16" ở Uc_Home) lỗi "Parameter is not valid" khi tạo lại → không mở được Trang chủ.
                Image source = SvgBitmap.Create(svg).Render(null, scale,
                    DevExpress.Utils.DefaultBoolean.Default, DevExpress.Utils.DefaultBoolean.Default);
                var result = new Bitmap(source.Width, source.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (var src = new Bitmap(source))
                {
                    for (int x = 0; x < src.Width; x++)
                        for (int y = 0; y < src.Height; y++)
                            result.SetPixel(x, y, Color.FromArgb(src.GetPixel(x, y).A, color));
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static void StylePrimary(SimpleButton button) => StyleFilled(button, Brand, BrandDark);

        public static void StyleDanger(SimpleButton button) => StyleFilled(button, Danger, DangerDark);

        private static void StyleFilled(SimpleButton button, Color normal, Color hover)
        {
            ApplyWhiteIconAfterStyle(button);
            StyleFilledColors(button, normal, hover);
        }

        private static void ApplyWhiteIconAfterStyle(SimpleButton button)
        {
            // Icon đã gán trước khi tô màu nút cũng được chuyển sang trắng
            if (button.ImageOptions.SvgImage != null)
                ApplyWhiteIcon(button);
        }

        private static void StyleFilledColors(SimpleButton button, Color normal, Color hover)
        {
            button.Appearance.BackColor = normal;
            button.Appearance.ForeColor = Color.White;
            button.Appearance.Options.UseBackColor = true;
            button.Appearance.Options.UseForeColor = true;
            button.AppearanceHovered.BackColor = hover;
            button.AppearanceHovered.ForeColor = Color.White;
            button.AppearanceHovered.Options.UseBackColor = true;
            button.AppearanceHovered.Options.UseForeColor = true;
            button.AppearancePressed.BackColor = hover;
            button.AppearancePressed.ForeColor = Color.White;
            button.AppearancePressed.Options.UseBackColor = true;
            button.AppearancePressed.Options.UseForeColor = true;
            button.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
        }

        /// <summary>
        /// Lưới chỉ đọc: dòng cao, xen màu, có ô tìm kiếm luôn hiển thị.
        /// </summary>
        public static void StyleGrid(GridView view, string findPrompt)
        {
            view.RowHeight = 32;
            view.ColumnPanelRowHeight = 36;
            view.GroupRowHeight = 32;
            view.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            view.OptionsView.EnableAppearanceEvenRow = false;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsFind.AlwaysVisible = true;
            view.OptionsFind.FindNullPrompt = findPrompt;
            view.Appearance.HeaderPanel.ForeColor = Brand;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;
            view.Appearance.GroupRow.ForeColor = Brand;
            view.Appearance.GroupRow.Options.UseForeColor = true;
        }

        /// <summary>
        /// Handler Paint cho panel header: vạch nhấn bên trái và đường kẻ mảnh phía dưới.
        /// </summary>
        public static void PaintHeader(object sender, PaintEventArgs e)
        {
            var panel = (Control)sender;
            using (var accent = new SolidBrush(Brand))
                e.Graphics.FillRectangle(accent, 0, 8, 4, panel.Height - 16);
            using (var line = new Pen(Muted))
                e.Graphics.DrawLine(line, 0, panel.Height - 1, panel.Width, panel.Height - 1);
        }

        /// <summary>
        /// Vẽ đường phân cách mảnh có tiêu đề nhóm (VD: "THÔNG TIN QUẢN LÝ").
        /// </summary>
        public static void DrawSection(Graphics g, string title, int x, int y, int width)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var font = new Font("Roboto", 8.25F, FontStyle.Bold))
            using (var brush = new SolidBrush(TextMuted))
            using (var pen = new Pen(Muted))
            {
                var size = g.MeasureString(title, font);
                g.DrawString(title, font, brush, x, y - size.Height / 2);
                g.DrawLine(pen, x + size.Width + 6, y, x + width, y);
            }
        }

        private static readonly Font KpiTitleFont = new Font("Roboto", 8.25F, FontStyle.Bold);
        private static readonly Font KpiValueFont = new Font("Roboto", 17F, FontStyle.Bold);
        private static readonly Font KpiHintFont = new Font("Roboto", 8.25F);

        /// <summary>
        /// Thẻ chỉ số: viền bo góc, vạch màu bên trái, tiêu đề nhỏ, giá trị lớn, dòng gợi ý.
        /// Warn = true tô vạch và gợi ý màu cam.
        /// </summary>
        public struct KpiCard
        {
            public string Title;
            public string Value;
            public string Hint;
            public Color Accent;
            public bool Warn;

            public KpiCard(string title, string value, string hint, Color accent, bool warn = false)
            {
                Title = title;
                Value = value;
                Hint = hint;
                Accent = accent;
                Warn = warn;
            }
        }

        public static bool IsDarkSkin(DevExpress.LookAndFeel.UserLookAndFeel lookAndFeel)
            => DevExpress.Utils.Frames.FrameHelper.IsDarkSkin(lookAndFeel);

        /// <summary>Màu chữ chính của skin hiện tại (đọc được trên cả nền sáng và tối).</summary>
        public static Color SkinText(DevExpress.LookAndFeel.UserLookAndFeel lookAndFeel)
            => DevExpress.Skins.CommonSkins.GetSkin(lookAndFeel).GetSystemColor(SystemColors.ControlText);

        /// <summary>
        /// Vẽ các thẻ chỉ số dàn đều theo chiều ngang trong <paramref name="bounds"/> (dùng trong Paint của panel).
        /// </summary>
        public static void DrawKpiCards(Graphics g, Rectangle bounds, KpiCard[] cards, Color valueColor, int gap = 12)
        {
            if (cards.Length == 0) return;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int width = (bounds.Width - gap * (cards.Length - 1)) / cards.Length;
            int height = bounds.Height - 14;

            using (var border = new Pen(Muted))
            using (var muted = new SolidBrush(TextMuted))
            using (var valueBrush = new SolidBrush(valueColor))
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    var c = cards[i];
                    var rect = new Rectangle(bounds.X + i * (width + gap), bounds.Y + 2, width - 1, height);
                    using (var path = RoundRect(rect, 8))
                        g.DrawPath(border, path);
                    using (var accent = new SolidBrush(c.Warn ? Warning : c.Accent))
                        g.FillRectangle(accent, rect.X + 1, rect.Y + 14, 4, rect.Height - 28);

                    g.DrawString(c.Title, KpiTitleFont, muted, rect.X + 18, rect.Y + 10);
                    g.DrawString(c.Value, KpiValueFont, valueBrush, rect.X + 16, rect.Y + 26);
                    if (!string.IsNullOrEmpty(c.Hint))
                    {
                        using (var hintBrush = new SolidBrush(c.Warn ? Warning : TextMuted))
                            g.DrawString(c.Hint, KpiHintFont, hintBrush, rect.X + 18, rect.Y + rect.Height - 22);
                    }
                }
            }
        }

        private static GraphicsPath RoundRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void DrawEmptyState(CustomDrawEventArgs e, string title, string hint)
        {
            var b = e.Bounds;
            int cy = b.Y + b.Height / 2;
            using (var titleFont = new Font("Roboto", 13F, FontStyle.Bold))
            using (var hintFont = new Font("Roboto", 9.75F))
            using (var titleBrush = new SolidBrush(Brand))
            using (var hintBrush = new SolidBrush(TextMuted))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Cache.DrawString(title, titleFont, titleBrush, new Rectangle(b.X, cy - 30, b.Width, 28), format);
                e.Cache.DrawString(hint, hintFont, hintBrush, new Rectangle(b.X, cy + 2, b.Width, 22), format);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Hiển thị trạng thái dạng "● Đang sử dụng" có màu.
        /// </summary>
        public static void SetStatusText(LabelControl label, string text, Color color)
        {
            label.Text = "●  " + text;
            label.Appearance.ForeColor = color;
            label.Appearance.Options.UseForeColor = true;
        }
    }
}
