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
    public partial class ModelUpdateDonDatXefrm : Form
    {
        private Xe xeHienHanh = null;
        private DonDatXe donDatXeHienHanh = null;
        private CTDD chiTietDonDatHienHanh = null;
        string[] trangThaiXe = { "Trống", "Đang cho thuê", "Hỏng" };
        public ModelUpdateDonDatXefrm()
        {
            InitializeComponent();
        }

        public ModelUpdateDonDatXefrm(CTDD chiTietDonDatHienHanh)
        {
            InitializeComponent();
            this.chiTietDonDatHienHanh = chiTietDonDatHienHanh;
            this.xeHienHanh = XeDAO.Instance.getXeByXeId(chiTietDonDatHienHanh.XeId);
            this.donDatXeHienHanh = DonDatXeDAO.Instance.getDonDatXeById(chiTietDonDatHienHanh.DonDatXeId);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string trangThaiXe = cbbTrangThai.SelectedItem.ToString().Trim();
            DateTime ngayTra = dtNgayTra.Value;
            int soLuong = soNgayThue(ngayTra);
            decimal thanhTien = soLuong * chiTietDonDatHienHanh.DonGia;
            //MessageBox.Show(ngayTra + " " + soLuong + " " + thanhTien);
            XeDAO.Instance.Update_TrangThaiXe(trangThaiXe, chiTietDonDatHienHanh.XeId);
            bool result = CTDDDAO.Instance.Update_NgayTra(ngayTra, soLuong, thanhTien, chiTietDonDatHienHanh.XeId, chiTietDonDatHienHanh.DonDatXeId);
            if (result)
            {
                MessageBox.Show("Thành công");
                this.Close();
            }
            else
            {
                MessageBox.Show("Thất bại");
                this.Close();
            }
        }

        public int soNgayThue (DateTime ngayTra)
        {
            TimeSpan timeSpan = ngayTra - chiTietDonDatHienHanh.NgayThue;
            return timeSpan.Days;
        }

        private void ModelUpdateDonDatXefrm_Load(object sender, EventArgs e)
        {
            lbTenXe.Text = "Xe " + xeHienHanh.TenXe;
            dtNgayTra.Value = chiTietDonDatHienHanh.NgayTra;
            LoadTrangThai();
        }
        private void LoadTrangThai()
        {
            // Đặt mảng trạng thái xe vào ComboBox cho trạng thái
            cbbTrangThai.DataSource = trangThaiXe;
            if (cbbTrangThai.Items.Contains(xeHienHanh.TrangThaiXe))
            {
                cbbTrangThai.SelectedItem = xeHienHanh.TrangThaiXe;
            }
        }
    }
}
