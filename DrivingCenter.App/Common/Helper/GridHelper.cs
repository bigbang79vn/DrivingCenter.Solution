using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Runtime.CompilerServices;

public static class GridHelper
{
    // Các view đã gắn handler. Nhiều màn hình gọi EnableRowNumbering sau MỖI lần tải dữ liệu,
    // trước đây mỗi lần gọi lại cộng thêm 1 handler nên số handler vẽ STT tăng dần theo thời gian.
    // ConditionalWeakTable không giữ view sống khi màn hình đã bị giải phóng.
    private static readonly ConditionalWeakTable<GridView, object> _enabledViews = new ConditionalWeakTable<GridView, object>();

    public static void EnableRowNumbering(GridView gridView, int width = 40)
    {
        gridView.IndicatorWidth = width;

        if (_enabledViews.TryGetValue(gridView, out _)) return;
        _enabledViews.Add(gridView, null);

        gridView.CustomDrawRowIndicator += (s, e) =>
        {
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else
                {
                    e.Info.DisplayText = ""; // Không vẽ STT cho group hoặc header
                }

                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            }
        };
    }
}
