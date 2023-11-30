using QLThueXeOto.BLL;
using QLThueXeOto.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThueXeOto
{
    public partial class DangNhapfrm : Form
    {
        public DangNhapfrm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DangNhapfrm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String isEmptyForm = DangNhapBLL.Instance.checkFormLogin(tbUsername.Text.ToString().Trim(), tbPassword.Text.ToString().Trim());
            if(isEmptyForm == null)
            {
                bool checkLogin = DangNhapBLL.Instance.checkLogin(tbUsername.Text.ToString().Trim(), tbPassword.Text.ToString().Trim());
                if(checkLogin) 
                {
                    Homefrm frm = new Homefrm();
                    this.Hide();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show(isEmptyForm.ToString(), "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
