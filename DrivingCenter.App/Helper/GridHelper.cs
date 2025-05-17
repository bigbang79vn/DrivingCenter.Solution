using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;

public static class GridHelper
{
    public static void EnableRowNumbering(GridView gridView, int width = 40)
    {
        gridView.IndicatorWidth = width;
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
