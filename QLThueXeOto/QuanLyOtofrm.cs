using OfficeOpenXml;
using QLThueXeOto.BLL;
using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLThueXeOto
{
    public partial class QuanLyOtofrm : Form
    {
        private NguoiDung user = AuthDAO.Instance.User;
        private int minWithSightBar = 54;
        private int maxWithSightBar = 300;
        private int heightItem = 64;
        //
        // Mảng nhiên liệu xe
        string[] nhienLieuXe = { "Xăng", "Dầu Diesel", "Điện" };

        // Mảng trạng thái xe
        string[] trangThaiXe = { "Trống", "Đang cho thuê", "Hỏng"};
        public QuanLyOtofrm()
        {
            InitializeComponent();
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

        private void QuanLyOtofrm_Load(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = maxWithSightBar;
            this.SizeChanged += Homefrm_SizeChanged;
            lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;

            //Load
            LoadListXe(XeDAO.Instance.getXeDetail());
            LoadLoaiXe(LoaiXeDAO.Instance.getAll());
            LoadTrangThaiAndNhienLieu();
        }

        private void LoadTrangThaiAndNhienLieu()
        {
            // Đặt mảng nhiên liệu xe vào ComboBox cho nhiên liệu
            cbbNhienLieu.DataSource = nhienLieuXe;

            // Đặt mảng trạng thái xe vào ComboBox cho trạng thái
            cbbTrangThai.DataSource = trangThaiXe;

        }

        private void LoadLoaiXe(DataTable listLoaiXe)
        {
            cbbLoaiXe.DataSource = listLoaiXe;
            cbbLoaiXe.DisplayMember = "tenLoaiXe";
            cbbLoaiXe.ValueMember = "loaiXeId";
            cbbLoaiXe.Refresh();
        }

        private void LoadListXe(DataTable listXe)
        {
            dgvDsXe.DataSource = listXe;
            dgvDsXe.Refresh();
        }

        private void BinDingFormXeDetail(string ten, string hang, string mau, string bienSo, string trangThai, string nhienLieu, decimal giaThue, string tenLoaiXe)
        {
            txtTenXe.Text = ten;
            txtHangXe.Text = hang;
            txtMauXe.Text = mau;
            txtBienSo.Text = bienSo;
            if (cbbTrangThai.Items.Contains(trangThai))
            {
                cbbTrangThai.SelectedItem = trangThai;
            }
            if (cbbNhienLieu.Items.Contains(nhienLieu))
            {
                cbbNhienLieu.SelectedItem = nhienLieu;
            }
            txtGia.Text = giaThue.ToString();
            cbbLoaiXe.SelectedIndex = cbbLoaiXe.FindString(tenLoaiXe);
        }

        private string CheckFormXeDetail()
        {
            string tenXe = txtTenXe.Text.Trim();
            string hangXe = txtHangXe.Text.Trim();
            string mauXe = txtMauXe.Text.Trim();
            string bienSoXe = txtBienSo.Text.Trim();
            string trangThaiXe = cbbTrangThai.SelectedItem.ToString().Trim();
            string nhienLieu = cbbNhienLieu.SelectedItem.ToString().Trim();

            decimal giaThue;

            // Kiểm tra và chuyển đổi giá thuê
            if (!decimal.TryParse(txtGia.Text, out giaThue))
            {
                return "Giá thuê không hợp lệ. Vui lòng nhập một số hợp lệ.";
            }

            int loaiXeId = (int)cbbLoaiXe.SelectedValue;

            if (string.IsNullOrEmpty(tenXe))
            {
                return "Vui lòng nhập tên xe";
            }
            else if (string.IsNullOrEmpty(hangXe))
            {
                return "Vui lòng nhập hãng xe";
            }
            else if (string.IsNullOrEmpty(mauXe))
            {
                return "Vui lòng nhập mẫu xe";
            }
            else if (string.IsNullOrEmpty(bienSoXe))
            {
                return "Vui lòng nhập biển số xe";
            }
            else if (string.IsNullOrEmpty(trangThaiXe))
            {
                return "Vui lòng nhập trạng thái xe";
            }
            else if (string.IsNullOrEmpty(nhienLieu))
            {
                return "Vui lòng nhập loại nhiên liệu";
            }
            else if (giaThue <= 0)
            {
                return "Giá thuê phải là một số dương.";
            }
            else if (loaiXeId <= 0)
            {
                return "Vui lòng chọn loại xe";
            }

            return null;
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

        private void btnHidenBar_Click(object sender, EventArgs e)
        {
            changeSightBar();

        }

        private void QuanLyOtofrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Homefrm frm = (Homefrm)Application.OpenForms["Homefrm"];
            if (frm != null)
            {
                frm.Show();
            }
        }

        private void dgvDsXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvDsXe.Rows[e.RowIndex];

                // Lấy dữ liệu từ các ô trong dòng được chọn
                int id = (int) selectedRow.Cells["ID"].Value;
                string tenXe = selectedRow.Cells["Tên xe"].Value.ToString();
                string hangXe = selectedRow.Cells["Hãng"].Value.ToString();
                string mauXe = selectedRow.Cells["Mẫu"].Value.ToString();
                string bienSoXe = selectedRow.Cells["Biển số"].Value.ToString();
                string trangThaiXe = selectedRow.Cells["Trạng thái"].Value.ToString();
                string nhienLieu = selectedRow.Cells["Nhiên liệu"].Value.ToString();
                decimal giaThue = (decimal) selectedRow.Cells["Giá thuê"].Value;
                string tenLoaiXe = selectedRow.Cells["Loại xe"].Value.ToString();

                // Sử dụng dữ liệu hoặc thực hiện các thao tác khác ở đây
                //MessageBox.Show(id.ToString() + tenXe + hangXe + mauXe + bienSoXe + trangThaiXe + loaiNhienLieu + tenLoaiXe);
                BinDingFormXeDetail(tenXe, hangXe, mauXe, bienSoXe, trangThaiXe, nhienLieu, giaThue, tenLoaiXe);
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            string message = CheckFormXeDetail();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }
            else
            {
                string tenXe = txtTenXe.Text.Trim();
                string hangXe = txtHangXe.Text.Trim();
                string mauXe = txtMauXe.Text.Trim();
                string bienSoXe = txtBienSo.Text.Trim();
                string trangThaiXe = cbbTrangThai.SelectedItem.ToString();
                string nhienLieu = cbbNhienLieu.SelectedItem.ToString();

                decimal giaThue = decimal.Parse(txtGia.Text);
                int loaiXeId = (int)cbbLoaiXe.SelectedValue;

                //them xe
                bool result = Insert_Xe(tenXe, hangXe, mauXe, bienSoXe, trangThaiXe, nhienLieu, giaThue, loaiXeId);
                if (result)
                {
                    MessageBox.Show("Thêm xe thành công", "Thông báo");
                    LoadListXe(XeDAO.Instance.getXeDetail());
                }
                else
                {
                    MessageBox.Show("Thêm xe thất bại", "Thông báo");
                }
            }
        }

        private bool Insert_Xe(string tenXe, string hangXe, string mauXe, string bienSoXe, string trangThaiXe, string loaiNhienLieu, decimal giaChoThue, int loaiXeId)
        {
            //check tên xe và biển số xe 
            if (XeDAO.Instance.findXebyTenXe(tenXe) != null)
            {
                MessageBox.Show("Xe đã tồn tại." + Environment.NewLine + "Thông tin: " + Environment.NewLine + XeDAO.Instance.findXebyTenXe(tenXe).ToString(), "Thông báo");
                return false;
            }
            else if (XeDAO.Instance.findXebyBienSoXe(bienSoXe) != null)
            {
                MessageBox.Show("Biển số xe đã tồn tại." + Environment.NewLine + "Thông tin: " + Environment.NewLine + XeDAO.Instance.findXebyBienSoXe(bienSoXe), "Thông báo");
                return false;
            }

            //thêm xe tại đây

            Xe newXe = new Xe(tenXe, hangXe, mauXe, bienSoXe, trangThaiXe, loaiNhienLieu, giaChoThue, loaiXeId);
            //MessageBox.Show(newXe.ToString() + " Id loại xe: " + loaiXeId.ToString());

            if (XeDAO.Instance.Insert_Xe(newXe))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTenXe.Clear();
            txtHangXe.Clear();
            txtMauXe.Clear();
            txtBienSo.Clear();
            cbbNhienLieu.SelectedIndex = 0;
            cbbTrangThai.SelectedIndex = 0;
            txtGia.Clear();
            cbbLoaiXe.SelectedValue = 1;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoaTimKiem = txtTimKiem.Text.Trim();
            if (String.IsNullOrEmpty(tuKhoaTimKiem))
            {
                LoadListXe(XeDAO.Instance.getXeDetail());
            }
            else
            {
                LoadListXe(XeDAO.Instance.getXeDetailByTuKhoaTimKiem(tuKhoaTimKiem));
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoaTimKiem = txtTimKiem.Text.Trim();
            if (String.IsNullOrEmpty(tuKhoaTimKiem))
            {
                LoadListXe(XeDAO.Instance.getXeDetail());
            }
            else
            {
                LoadListXe(XeDAO.Instance.getXeDetailByTuKhoaTimKiem(tuKhoaTimKiem));
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDsXe.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDsXe.SelectedRows[0];
                int Id = (int) selectedRow.Cells["ID"].Value;
                if (XeDAO.Instance.DelXeById(Id))
                {
                    MessageBox.Show("Xóa xe thành công", "Thông báo");
                    LoadListXe(XeDAO.Instance.getXeDetail());

                    return;
                }
                MessageBox.Show("Xóa xe thất bại", "Thông báo");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn xe để xóa", "Thông báo");
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int Id = 0;
            if (dgvDsXe.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDsXe.SelectedRows[0];
                Id = (int)selectedRow.Cells["ID"].Value;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn xe để sửa", "Thông báo");
                return;
            }

            string message = CheckFormXeDetail();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }
            else
            {
                string tenXe = txtTenXe.Text.Trim();
                string hangXe = txtHangXe.Text.Trim();
                string mauXe = txtMauXe.Text.Trim();
                string bienSoXe = txtBienSo.Text.Trim();
                string trangThaiXe = cbbTrangThai.SelectedItem.ToString();
                string nhienLieu = cbbNhienLieu.SelectedItem.ToString();

                decimal giaThue = decimal.Parse(txtGia.Text);
                int loaiXeId = (int)cbbLoaiXe.SelectedValue;

                //check tên xe và biển số xe 
                //if (XeDAO.Instance.findXebyTenXe(tenXe) != null)
                //{
                //    if (XeDAO.Instance.findXebyTenXe(tenXe).TenXe.Trim() != tenXe)
                //    {
                //        MessageBox.Show("Xe đã tồn tại." + Environment.NewLine + "Thông tin: " + Environment.NewLine + XeDAO.Instance.findXebyTenXe(tenXe).ToString(), "Thông báo");
                //        return;
                //    }
                //}
                if (XeDAO.Instance.findXebyBienSoXe(bienSoXe) != null)
                {
                    DataGridViewRow selectedRow = dgvDsXe.SelectedRows[0];
                    string biensohientai = selectedRow.Cells["Biển số"].Value.ToString();
                    if (XeDAO.Instance.findXebyBienSoXe(bienSoXe).BienSoXe.Trim() != biensohientai)
                    {
                        MessageBox.Show("Biển số xe đã tồn tại." + Environment.NewLine + "Thông tin: " + Environment.NewLine + XeDAO.Instance.findXebyBienSoXe(bienSoXe), "Thông báo");
                        return;
                    }
                }

                //sửa xe tại đây

                Xe newXe = new Xe(Id,tenXe, hangXe, mauXe, bienSoXe, trangThaiXe, nhienLieu, giaThue, loaiXeId);
                //MessageBox.Show(newXe.ToString() + " Id loại xe: " + loaiXeId.ToString());

                if (XeDAO.Instance.Update_Xe(newXe))
                {
                    MessageBox.Show("Sửa xe thành công", "Thông báo");
                    LoadListXe(XeDAO.Instance.getXeDetail());
                }
                else
                {
                    MessageBox.Show("Sửa xe thất bại", "Thông báo");
                }
            }
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            // chọn file excel và trả về path
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ImportDataFromExcel(filePath);
                }
            }
        }

        private void ImportDataFromExcel(string filePath)
        {
            // Sử dụng EPPLus đọc dữ liệu
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Lặp qua từng dòng và cột trong tệp Excel để lấy dữ liệu
                for (int row = 2; row <= rowCount; row++)
                {
                    // Xử lý dữ liệu và thêm vào ứng dụng
                    try
                    {
                        string tenXe = worksheet.Cells[row, 2].Text;
                        string hangXe = worksheet.Cells[row, 3].Text;
                        string mauXe = worksheet.Cells[row, 4].Text;
                        string bienSoXe = worksheet.Cells[row, 5].Text;
                        string trangThaiXe = worksheet.Cells[row, 6].Text;
                        string nhienLieu = worksheet.Cells[row, 7].Text;

                        decimal giaThue = decimal.Parse(worksheet.Cells[row, 8].Text);
                        int loaiXeId = int.Parse(worksheet.Cells[row, 9].Text);
                        Insert_Xe(tenXe, hangXe, mauXe, bienSoXe, trangThaiXe, nhienLieu, giaThue, loaiXeId);
                        //Xe newXe = new Xe(tenXe, hangXe, mauXe, bienSoXe, trangThaiXe, nhienLieu, giaThue, loaiXeId);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi đọc file", "Thông báo");
                    }
                }
                MessageBox.Show("Đã nhập xong dữ liệu", "Thông báo");
                LoadListXe(XeDAO.Instance.getXeDetail());
            }
        }

        private void btnChoThue_Click(object sender, EventArgs e)
        {
            HopDongThueXefrm frm = new HopDongThueXefrm();
            this.Close();
            frm.Show();
        }

        private void btnQLDonDatXe_Click(object sender, EventArgs e)
        {
            QuanLyDonDatXefrm frm = new QuanLyDonDatXefrm();
            this.Close();
            frm.Show();
        }

        private void btnQLLichTrinh_Click(object sender, EventArgs e)
        {
            QuanLyLichTrinhfrm frm = new QuanLyLichTrinhfrm();
            this.Close();
            frm.Show();
        }

        private void btnQLKhachHang_Click(object sender, EventArgs e)
        {
            QuanLyKhachHangfrm frm = new QuanLyKhachHangfrm();
            this.Close();
            frm.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (user.QuyenId != 1)
            {
                MessageBox.Show("Bạn không có quyền vào chức năng này", "Thông báo");
                return;
            }
            ThongKeDoanhThu frm = new ThongKeDoanhThu();
            this.Hide();
            frm.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (user.QuyenId != 1)
            {
                MessageBox.Show("Bạn không có quyền vào chức năng này", "Thông báo");
                return;
            }
            ThongKeDoanhThu2 frm = new ThongKeDoanhThu2();
            this.Hide();
            frm.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
