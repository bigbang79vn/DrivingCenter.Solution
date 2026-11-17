namespace DrivingCenter.App.Enums
{
    /// <summary>Kết quả so sánh một bản ghi trong file import với CSDL.</summary>
    public enum ImportAction
    {
        /// <summary>Chưa có trong CSDL → thêm mới.</summary>
        ThemMoi,
        /// <summary>Đã có và có thay đổi → cập nhật.</summary>
        CapNhat,
        /// <summary>Đã có và không thay đổi → bỏ qua.</summary>
        GiuNguyen
    }
}
