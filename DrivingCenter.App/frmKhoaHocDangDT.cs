using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DrivingCenter.App.DAO;
using DrivingCenter.App.Dtos.DAT;
using DrivingCenter.App.Entities.DAT;
using DrivingCenter.App.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App
{
    public partial class frmKhoaHocDangDT : DevExpress.XtraEditors.XtraForm
    {
        private const string DangHoc = "Đang học";
        private const string DaBeGiang = "Đã bế giảng";
        private const string ChuaKhaiGiang = "Chưa khai giảng";
        private const string DaKetThuc = "Đã kết thúc";

        private readonly KhoaHocDatDAO _khoaHocDao = new KhoaHocDatDAO();

        /// <summary>Danh sách khoá học đã lưu là "đang đào tạo" (có sau khi bấm Lưu thay đổi).</summary>
        public List<KhoaHocDat> SelectedKhoaHoc { get; private set; } = new List<KhoaHocDat>();

        // Tra cứu KhoaHocDat gốc theo KhoaHocID
        private Dictionary<int, KhoaHocDat> _dictKhoaHoc = new Dictionary<int, KhoaHocDat>();
        // Khóa đang có trong bảng KhoaHocDangDT lúc mở form (để biết thêm / bỏ những gì)
        private HashSet<int> _banDau = new HashSet<int>();
        private bool _loaded;

        // Màu theo cấp node và theo skin sáng/tối; bố cục, font, màu skin sáng còn lại nằm trong Designer
        private static readonly Font GroupFont = new Font("Roboto", 9.75F, FontStyle.Bold);
        private Color _yearFore, _monthFore, _checkedBack, _checkedFore, _successFore;

        private bool IsAlive => !IsDisposed && !Disposing && IsHandleCreated;

        public frmKhoaHocDangDT()
        {
            InitializeComponent();
            ApplySkinColors();
        }

        /// <summary>
        /// Skin tối: dòng đang chọn nền xanh đậm chữ trắng (nền sáng của Designer làm chữ trắng không đọc được),
        /// khóa đã tick nền xanh lá đậm chữ sáng.
        /// </summary>
        private void ApplySkinColors()
        {
            bool dark = UiTheme.IsDarkSkin(LookAndFeel);
            _yearFore = dark ? Color.FromArgb(147, 176, 222) : UiTheme.Brand;
            _monthFore = dark ? Color.FromArgb(203, 213, 225) : Color.FromArgb(51, 65, 85);
            _checkedBack = dark ? Color.FromArgb(22, 60, 40) : Color.FromArgb(220, 252, 231);
            _checkedFore = dark ? Color.FromArgb(134, 239, 172) : Color.FromArgb(21, 128, 61);
            _successFore = dark ? Color.FromArgb(134, 239, 172) : UiTheme.Success;

            if (!dark) return;
            foreach (var appearance in new[] { treeList_KhoaHoc.Appearance.FocusedRow, treeList_KhoaHoc.Appearance.HideSelectionRow })
            {
                appearance.BackColor = UiTheme.BrandDark;
                appearance.ForeColor = Color.White;
                appearance.Options.UseBackColor = true;
                appearance.Options.UseForeColor = true;
            }
            treeList_KhoaHoc.Appearance.HeaderPanel.ForeColor = _yearFore;
        }

        private async void frmKhoaHocDangDT_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        #region Tải dữ liệu

        private async Task LoadDataAsync()
        {
            SetBusy(true);
            List<KhoaHocDat> khoaHoc;
            HashSet<int> dangDT;
            try
            {
                var khoaHocTask = Task.Run(() => _khoaHocDao.GetAllKhoaHoc());
                var dangDTTask = Task.Run(() => _khoaHocDao.GetKhoaHocDangDTIds());
                khoaHoc = await khoaHocTask;
                dangDT = await dangDTTask;
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                lblDaChon.Text = "Không tải được danh sách khóa học.";
                XtraMessageBox.Show(this, "Không tải được danh sách khóa học.\n\nChi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsAlive) return;

            // Khóa đã xóa (Del) không hiển thị, trừ khi vẫn đang nằm trong danh sách đang đào tạo
            khoaHoc = khoaHoc.Where(k => !k.Del || dangDT.Contains(k.KhoaHocID)).ToList();
            _dictKhoaHoc = khoaHoc.GroupBy(k => k.KhoaHocID).ToDictionary(g => g.Key, g => g.First());
            _banDau = new HashSet<int>(dangDT.Where(_dictKhoaHoc.ContainsKey));

            treeList_KhoaHoc.BeginUpdate();
            try
            {
                treeList_KhoaHoc.DataSource = BuildTreeData(khoaHoc);
                treeList_KhoaHoc.CollapseAll();
                // Tick sẵn các khóa đang đào tạo (trước đây form luôn mở với danh sách trống)
                treeList_KhoaHoc.NodesIterator.DoOperation(node =>
                {
                    if (!node.HasChildren && Record(node) is KhoaHocNode rec && _banDau.Contains(rec.KhoaHocID))
                        node.CheckState = CheckState.Checked;
                });
                RefreshParentCheckStates();
                ExpandDefault();
            }
            finally
            {
                treeList_KhoaHoc.EndUpdate();
            }

            _loaded = true;
            SetBusy(false);
            UpdateDaChon();
        }

        /// <summary>
        /// Dựng cây Năm → Tháng → Khoá học (nhóm theo Ngày KG, mới nhất ở trên).
        /// Node khoá học: Id = KhoaHocID; node nhóm: Id âm để không trùng.
        /// </summary>
        private static List<KhoaHocNode> BuildTreeData(List<KhoaHocDat> lstKhoaHoc)
        {
            DateTime today = DateTime.Today;
            var lst = new List<KhoaHocNode>();
            int groupId = -1;

            foreach (var nam in lstKhoaHoc.GroupBy(k => k.NgayKG.Year).OrderByDescending(g => g.Key))
            {
                int yearId = groupId--;
                lst.Add(new KhoaHocNode { Id = yearId, ParentId = 0, TenKH = "Năm " + nam.Key, Nam = nam.Key });

                foreach (var thang in nam.GroupBy(k => k.NgayKG.Month).OrderByDescending(g => g.Key))
                {
                    int monthId = groupId--;
                    lst.Add(new KhoaHocNode
                    {
                        Id = monthId,
                        ParentId = yearId,
                        TenKH = $"Tháng {thang.Key:00}/{nam.Key}",
                        Nam = nam.Key,
                        Thang = thang.Key
                    });

                    foreach (KhoaHocDat kh in thang.OrderBy(k => k.NgayKG).ThenBy(k => k.TenKH))
                    {
                        DateTime? han = kh.NgayDaoTao == DateTime.MinValue ? (DateTime?)null : kh.NgayDaoTao;
                        DateTime? beGiang = kh.NgayBG == DateTime.MinValue ? (DateTime?)null : kh.NgayBG;
                        lst.Add(new KhoaHocNode
                        {
                            Id = kh.KhoaHocID,
                            ParentId = monthId,
                            KhoaHocID = kh.KhoaHocID,
                            TenKH = kh.TenKH,
                            MaKH = kh.MaKH,
                            NgayKG = kh.NgayKG,
                            NgayBG = beGiang,
                            HangDT = kh.HangDaoTao,
                            NgayBatDauDT = han,
                            TrangThai = TrangThai(kh.NgayKG, beGiang, han, today)
                        });
                    }
                }
            }
            return lst;
        }

        private static string TrangThai(DateTime khaiGiang, DateTime? beGiang, DateTime? hanDaoTao, DateTime today)
        {
            if (today < khaiGiang.Date) return ChuaKhaiGiang;
            if (beGiang == null || today <= beGiang.Value.Date) return DangHoc;
            if (hanDaoTao != null && today <= hanDaoTao.Value.Date) return DaBeGiang;
            return DaKetThuc;
        }

        private KhoaHocNode Record(TreeListNode node) => treeList_KhoaHoc.GetDataRecordByNode(node) as KhoaHocNode;

        /// <summary>
        /// Mở sẵn năm hiện tại và các tháng có khóa đang chọn, cuộn tới khóa đầu tiên đang chọn.
        /// (Trước đây không cho thu gọn/mở rộng và ẩn nút +/- nên khó duyệt danh sách dài.)
        /// </summary>
        private void ExpandDefault()
        {
            TreeListNode first = null;
            foreach (TreeListNode year in treeList_KhoaHoc.Nodes)
            {
                if (Record(year)?.Nam == DateTime.Today.Year) year.Expanded = true;
                foreach (TreeListNode month in year.Nodes)
                {
                    if (month.CheckState == CheckState.Unchecked) continue;
                    year.Expanded = true;
                    month.Expanded = true;
                    if (first == null) first = month;
                }
            }

            if (first == null && treeList_KhoaHoc.Nodes.Count > 0) first = treeList_KhoaHoc.Nodes[0];
            if (first != null)
            {
                treeList_KhoaHoc.FocusedNode = first;
                treeList_KhoaHoc.TopVisibleNodeIndex = treeList_KhoaHoc.GetVisibleIndexByNode(first);
            }
        }

        /// <summary>Cập nhật trạng thái tick (đủ / một phần / không) của node năm, tháng theo các khóa con.</summary>
        private void RefreshParentCheckStates()
        {
            foreach (TreeListNode year in treeList_KhoaHoc.Nodes)
            {
                foreach (TreeListNode month in year.Nodes)
                    month.CheckState = Aggregate(month.Nodes.Cast<TreeListNode>().Select(n => n.CheckState));
                year.CheckState = Aggregate(year.Nodes.Cast<TreeListNode>().Select(n => n.CheckState));
            }
        }

        private static CheckState Aggregate(IEnumerable<CheckState> states)
        {
            var list = states.ToList();
            if (list.Count > 0 && list.All(s => s == CheckState.Checked)) return CheckState.Checked;
            if (list.All(s => s == CheckState.Unchecked)) return CheckState.Unchecked;
            return CheckState.Indeterminate;
        }

        private void SetBusy(bool busy)
        {
            treeList_KhoaHoc.Enabled = !busy;
            foreach (var btn in new[] { btnCapNhat, btnGoiY, btnBoChon, btnExpand, btnCollapse })
                btn.Enabled = !busy;
            if (busy) Cursor = Cursors.WaitCursor; else Cursor = Cursors.Default;
        }

        #endregion

        #region Giao diện TreeList

        private void treeList_KhoaHoc_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            bool focused = e.Node == treeList_KhoaHoc.FocusedNode;
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = GroupFont;
                if (!focused) e.Appearance.ForeColor = _yearFore;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = GroupFont;
                if (!focused) e.Appearance.ForeColor = _monthFore;
            }
            else if (e.Column == colTrangThai && !focused)
            {
                string status = Record(e.Node)?.TrangThai;
                e.Appearance.ForeColor = status == DangHoc ? _successFore
                    : status == DaBeGiang ? UiTheme.Warning
                    : UiTheme.TextMuted;
                if (e.Node.Checked) e.Appearance.BackColor = _checkedBack;
            }
            else if (e.Node.Checked && !focused)
            {
                e.Appearance.BackColor = _checkedBack;
                e.Appearance.ForeColor = _checkedFore;
            }
        }

        // Số khoá học sau tên năm / tháng, VD: "Tháng 03/2026  (2/5)" = 2 khóa đang chọn trên 5 khóa.
        // Dựng lại từ tên gốc: sự kiện được gọi nhiều lần cho cùng một ô, nối vào DisplayText sẽ bị lặp.
        private void treeList_KhoaHoc_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column != colTenKH || e.Node == null || !e.Node.HasChildren) return;
            var leaves = Leaves(e.Node).ToList();
            int chon = leaves.Count(n => n.Checked);
            string ten = Record(e.Node)?.TenKH ?? Convert.ToString(e.Value);
            e.DisplayText = chon > 0 ? $"{ten}  ({chon}/{leaves.Count})" : $"{ten}  ({leaves.Count})";
        }

        private void treeList_KhoaHoc_AfterCheckNode(object sender, NodeEventArgs e)
        {
            UpdateDaChon();
            treeList_KhoaHoc.Invalidate();   // vẽ lại màu dòng đã chọn và số đếm của nhóm
        }

        private IEnumerable<TreeListNode> Leaves(TreeListNode node)
        {
            foreach (TreeListNode child in node.Nodes)
            {
                if (child.HasChildren)
                    foreach (var leaf in Leaves(child)) yield return leaf;
                else
                    yield return child;
            }
        }

        private List<int> CheckedIds()
        {
            return treeList_KhoaHoc.GetAllCheckedNodes()
                .Where(n => !n.HasChildren)
                .Select(Record)
                .Where(x => x != null && _dictKhoaHoc.ContainsKey(x.KhoaHocID))
                .Select(x => x.KhoaHocID)
                .Distinct()
                .ToList();
        }

        /// <summary>"Đã chọn 12 khóa học · thêm 2 · bỏ 1" — cho biết bấm Lưu sẽ thay đổi gì.</summary>
        private void UpdateDaChon()
        {
            if (!_loaded) return;
            var ids = CheckedIds();
            int them = ids.Count(id => !_banDau.Contains(id));
            int bo = _banDau.Count(id => !ids.Contains(id));

            string text = $"Đã chọn <b>{ids.Count}</b> khóa học";
            if (them > 0) text += $"   <color=21,128,61>·  thêm {them}</color>";
            if (bo > 0) text += $"   <color=185,28,28>·  bỏ {bo}</color>";
            if (them == 0 && bo == 0) text += "   <color=100,116,139>·  chưa thay đổi</color>";
            lblDaChon.Text = text;
            btnCapNhat.Enabled = them > 0 || bo > 0;
        }

        #endregion

        #region Nút bấm

        private void btnGoiY_Click(object sender, EventArgs e)
        {
            // Tick thêm khóa đang học (đã khai giảng, chưa bế giảng) — giống cách danh sách hiện có được chọn; không bỏ tick khóa nào
            treeList_KhoaHoc.BeginUpdate();
            int count = 0;
            treeList_KhoaHoc.NodesIterator.DoOperation(node =>
            {
                if (node.HasChildren || node.Checked) return;
                string status = Record(node)?.TrangThai;
                if (status != DangHoc) return;
                node.CheckState = CheckState.Checked;
                node.ParentNode.Expanded = true;
                node.ParentNode.ParentNode.Expanded = true;
                count++;
            });
            RefreshParentCheckStates();
            treeList_KhoaHoc.EndUpdate();
            UpdateDaChon();

            if (count == 0)
                XtraMessageBox.Show(this, "Các khóa đang học đều đã được chọn.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBoChon_Click(object sender, EventArgs e)
        {
            treeList_KhoaHoc.UncheckAll();
            UpdateDaChon();
            treeList_KhoaHoc.Invalidate();
        }

        private void btnExpand_Click(object sender, EventArgs e) => treeList_KhoaHoc.ExpandAll();

        private void btnCollapse_Click(object sender, EventArgs e) => treeList_KhoaHoc.CollapseAll();

        private async void btnCapNhat_Click(object sender, EventArgs e)
        {
            var ids = CheckedIds();
            if (ids.Count == 0)
            {
                XtraMessageBox.Show(this, "Vui lòng chọn ít nhất một khoá học.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var them = ids.Where(id => !_banDau.Contains(id)).Select(id => _dictKhoaHoc[id].TenKH).OrderBy(x => x).ToList();
            var bo = _banDau.Where(id => !ids.Contains(id)).Select(id => _dictKhoaHoc[id].TenKH).OrderBy(x => x).ToList();
            string message = $"Lưu danh sách {ids.Count} khóa học đang đào tạo?\n";
            if (them.Count > 0) message += $"\n• Thêm ({them.Count}): {Preview(them)}";
            if (bo.Count > 0) message += $"\n• Bỏ ({bo.Count}): {Preview(bo)}";
            if (XtraMessageBox.Show(this, message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            SetBusy(true);
            try
            {
                await Task.Run(() => _khoaHocDao.SaveKhoaHocDangDT(ids));
            }
            catch (Exception ex)
            {
                if (!IsAlive) return;
                SetBusy(false);
                XtraMessageBox.Show(this, "Không lưu được danh sách khóa học.\n\nChi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsAlive) return;

            SelectedKhoaHoc = ids.Select(id => _dictKhoaHoc[id]).ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private static string Preview(List<string> names)
        {
            const int max = 8;
            return names.Count <= max ? string.Join(", ", names) : string.Join(", ", names.Take(max)) + $" và {names.Count - max} khóa khác";
        }

        #endregion
    }
}
