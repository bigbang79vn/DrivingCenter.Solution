using DrivingCenter.App.DTO.Account;
using System;

namespace DrivingCenter.App
{
    public partial class fAccount : DevExpress.XtraEditors.XtraForm
    {
        private Account _account;
        public fAccount(Account account) : this()
        {
            _account = account;
        }
        public fAccount()
        {
            InitializeComponent();
        }

        private void fAccount_Load(object sender, EventArgs e)
        {
            LoadInfo(_account);
        }

        private void LoadInfo(Account account)
        {
            if (_account != null)
            {
                txbUsername.Text = account.Username;
                txbDisplayName.Text = account.DisplayName;
                txbPassword.Focus();
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            //if (txbPassword.Text != loginAcc.Password)
            //{
            //    MessageBox.Show("Sai mật khẩu!", "Thông báo");
            //}
            //else
            //{
            //    if (txbNewPassword.Text == txbPasswordAgain.Text)
            //    {
            //        Account acc = LoginAcc;
            //        acc.Username = loginAcc.Username;
            //        acc.Password = txbNewPassword.Text;
            //        acc.DisplayName = txbDisplayName.Text;
            //        ReadWritetoTxt rw = new ReadWritetoTxt();
            //        rw.WriteTotxt(acc);
            //    }
            //    MessageBox.Show("Thay đổi thông tin thành công!", "Thông báo");
            //    this.Close();
            //}
        }
    }
}