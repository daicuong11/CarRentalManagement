using QLThueXeOto.BLL;
using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
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
    public partial class HopDongThueXefrm : Form
    {
        private int minWithSightBar = 54;
        private int maxWithSightBar = 300;
        private int heightItem = 64;
        public HopDongThueXefrm()
        {
            InitializeComponent();
        }

        private void HopDongThueXefrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Homefrm frm = (Homefrm)Application.OpenForms["Homefrm"];
            if (frm != null)
            {
                frm.Show();
            }
        }

        private void Homefrm_SizeChanged(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = minWithSightBar;
            btnChoThue.Text = "";

        }

        public void changeSightBar()
        {
            if (scLayer.SplitterDistance < maxWithSightBar)
            {
                scLayer.SplitterDistance = maxWithSightBar;
                btnHidenBar.Text = "<<";
                lbTitle.Text = "Công Ty";
                btnChoThue.Text = "HỢP ĐỒNG CHO THUÊ";
                btnQLOto.Text = "QUẢN LÝ OTO";
                btnQLLichTrinh.Text = "QUẢN LÝ LỊCH TRÌNH";
                btnQLKhachHang.Text = "QUẢN LÝ KHÁCH HÀNG";
                btnThongKe.Text = "THỐNG KÊ             >>";
                btnQLDonDatXe.Text = "QUẢN LÝ ĐƠN ĐẶT XE";
            }
            else
            {
                scLayer.SplitterDistance = minWithSightBar;
                btnHidenBar.Text = ">>";
                lbTitle.Text = "";
                btnChoThue.Text = "";
                btnQLOto.Text = "";
                btnQLLichTrinh.Text = "";
                btnQLKhachHang.Text = "";
                btnQLDonDatXe.Text = "";
                btnThongKe.Text = "";
                scItemThongKe.Height = heightItem;
                scItemThongKe.Panel2Collapsed = true;
            }
        }

        private void HopDongThueXefrm_Load(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = maxWithSightBar;
            this.SizeChanged += Homefrm_SizeChanged;
            lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;
            loadLoaiXe();
        }

        private void loadLoaiXe()
        {
            List<LoaiXe> loaiXes = HopDongThueXeBLL.Instance.loaiXes();
            foreach(LoaiXe x in loaiXes)
            {
                Button btn = new Button();
                btn.Width = HopDongThueXeBLL.Instance.WidthBtn;
                btn.Height = HopDongThueXeBLL.Instance.HeightBtn;
                btn.Text = x.TenLoaiXe;
                btn.Image = Properties.Resources.Car2;
                btn.Padding = new Padding(5, 5, 5, 5);
                btn.Margin = new Padding(10, 10, 10, 10);
                btn.ImageAlign = ContentAlignment.TopCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                btn.ForeColor = Color.DimGray; 
                btn.Font = new Font("Arial", 12, FontStyle.Bold); 
                btn.UseVisualStyleBackColor = false;
                btn.Cursor = Cursors.Hand;
                flTable.Controls.Add(btn);
            }
        }

        private void btnHidenBar_Click(object sender, EventArgs e)
        {
            changeSightBar();

        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(btnThongKe.Text))
            {
                changeSightBar();
                scItemThongKe.Height = (heightItem * 3);
                scItemThongKe.Panel2Collapsed = !scItemThongKe.Panel2Collapsed;
            }
            else
            {
                if (scItemThongKe.Height < 66)
                {
                    scItemThongKe.Height = (heightItem * 3);
                    btnThongKe.Text = "THỐNG KÊ             VV";
                }
                else
                {
                    scItemThongKe.Height = heightItem;
                    btnThongKe.Text = "THỐNG KÊ             >>";
                }
                scItemThongKe.Panel2Collapsed = !scItemThongKe.Panel2Collapsed;
            }
        }
    }
}
