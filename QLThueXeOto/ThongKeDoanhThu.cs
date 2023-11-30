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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLThueXeOto
{
    public partial class ThongKeDoanhThu : Form
    {
        private NguoiDung user = AuthDAO.Instance.User;
        public string[] dsThongke = { "Hãng xe", "Mẫu xe" };
        public ThongKeDoanhThu()
        {
            InitializeComponent();
            LoadChartByMauXe();
            // Đặt giá trị cho mỗi mục (ví dụ)
            cbbThongKe.DataSource = dsThongke; // Đặt nguồn dữ liệu cho ComboBox
                                               // Đặt tên cho biểu đồ và trục
            chart1.Titles.Add("Biếu đồ doanh thu");

        }

        private void LoadChartByHangXe()
        {


            // Lấy dữ liệu từ database
            DataTable dataTable = ThongKeDAO.Instance.GetDoanhThuTheoHangXe();

            // Thiết lập biểu đồ
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Title = "Hãng xe";
            chart1.ChartAreas[0].AxisY.Title = "Doanh thu";

            // Tạo loạt dữ liệu từ DataTable
            Series series = new Series("Doanh thu theo hãng xe");
            series.ChartType = SeriesChartType.Column;

            // Chỉ định thuộc tính Custom để hiển thị đầy đủ cột và tên cột
            series.CustomProperties = "DrawingStyle = Cylinder, PointWidth = 0.5";

            foreach (DataRow row in dataTable.Rows)
            {
                // Thêm các điểm dữ liệu từ DataTable vào Series
                series.Points.AddXY(row["HangXe"].ToString(), Convert.ToDouble(row["DoanhThu"]));
            }

            // Thêm Series vào biểu đồ
            chart1.Series.Add(series);
        }

        private void LoadChartByMauXe()
        {

            // Lấy dữ liệu từ database
            DataTable dataTable = ThongKeDAO.Instance.GetDoanhThuTheoMauXe();

            // Thiết lập biểu đồ
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Title = "Mẫu xe";
            chart1.ChartAreas[0].AxisY.Title = "Doanh thu";

            // Tạo loạt dữ liệu từ DataTable
            Series series = chart1.Series.Add("Doanh thu theo mẫu xe");
            series.ChartType = SeriesChartType.Column;

            // Chỉ định thuộc tính Custom để hiển thị đầy đủ cột và tên cột
            series.CustomProperties = "DrawingStyle = Cylinder, PointWidth = 0.5";

            foreach (DataRow row in dataTable.Rows)
            {
                // Thêm các điểm dữ liệu từ DataTable vào Series
                series.Points.AddXY(row["MauXe"].ToString(), Convert.ToDouble(row["DoanhThu"]));
            }
        }



        private void ThongKeDoanhThu_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void ThongKeDoanhThu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Homefrm frm = (Homefrm)Application.OpenForms["Homefrm"];
            if (frm != null)
            {
                frm.Show();
            }
        }

        private void cbbThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cbbThongKe.SelectedItem.ToString().Trim();

            // Sử dụng giá trị đã lấy
            if(selectedValue == "Hãng xe")
            {
                LoadChartByHangXe();
            }
            else
            {
                LoadChartByMauXe();
            }
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
