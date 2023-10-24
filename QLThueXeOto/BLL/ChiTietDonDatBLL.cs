﻿using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.BLL
{
    public class ChiTietDonDatBLL
    {
        private static ChiTietDonDatBLL instance;
        public static ChiTietDonDatBLL Instance
        {
            get { if (instance == null) instance = new ChiTietDonDatBLL(); return ChiTietDonDatBLL.instance; }
            private set { ChiTietDonDatBLL.instance = value; }
        }

        public DataTable TableCTDD( int donDatXeId)
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("STT", typeof(int));
            tb.Columns.Add("Tên xe", typeof(string));
            tb.Columns.Add("Ngày thuê", typeof(DateTime));
            tb.Columns.Add("Ngày trả", typeof(DateTime));
            tb.Columns.Add("Tổng ngày", typeof(int));
            tb.Columns.Add("Giá thuê", typeof(decimal));
            tb.Columns.Add("Thành tiền", typeof(decimal));

            DataTable dsCTDD = CTDDDAO.Instance.getCTDDByDonDatXeId(donDatXeId);
            int i = 0;
            foreach (DataRow dr in dsCTDD.Rows)
            {
                i++;
                CTDD ctdd = new CTDD(dr);
                Xe x = XeDAO.Instance.getXeByXeId(ctdd.XeId);
                DataRow newRow = tb.NewRow();
                newRow["STT"] = i;
                newRow["Tên xe"] = x.TenXe;
                newRow["Ngày thuê"] = ctdd.NgayThue;
                newRow["Ngày trả"] = ctdd.NgayTra;
                newRow["Tổng ngày"] = ctdd.SoLuong;
                newRow["Giá thuê"] = ctdd.DonGia;
                newRow["Thành tiền"] = ctdd.ThanhTien;

                tb.Rows.Add(newRow);
            }
            return tb;
        }
    }
}
