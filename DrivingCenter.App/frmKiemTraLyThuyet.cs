using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DrivingCenter.App
{
	public partial class frmKiemTraLyThuyet: DevExpress.XtraEditors.XtraForm
	{
		private readonly DataTable _data;
        public frmKiemTraLyThuyet(DataTable data)
		{
            InitializeComponent();
			_data = data;
		}

        private void frmKiemTraLyThuyet_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = _data;
        }
    }
}