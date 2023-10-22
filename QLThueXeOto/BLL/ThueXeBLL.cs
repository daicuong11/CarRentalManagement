using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.BLL
{
    public class ThueXeBLL
    {
        private static ThueXeBLL instance;

        public static ThueXeBLL Instance 
        { 
            get { if(instance == null) instance = new ThueXeBLL(); return ThueXeBLL.instance; }
            private set => instance = value; 
        }

        public List<Xe> ListXe()
        {
            List<Xe> list = new List<Xe>();

            DataTable dataTable = XeDAO.Instance.getAll();
            foreach (DataRow dr in dataTable.Rows)
            {
                list.Add(new Xe(dr));
            }    
            return list;
        }

        public List<Xe> ListXeByLoaiXeId( int loaiXeId)
        {
            List<Xe> list = new List<Xe>();

            DataTable dataTable = XeDAO.Instance.getXeByLoaiXeId(loaiXeId);
            foreach (DataRow dr in dataTable.Rows)
            {
                list.Add(new Xe(dr));
            }
            return list;
        }

        public List<Xe> ListXeByTinhNangXeAndLoaiXeId (List<int> lstTinhNangId, int loaiXeId)
        {
            List <Xe> list = new List<Xe>();

            DataTable tableXe = XeDAO.Instance.getXeByTinhNangXeAndLoaiXeId(lstTinhNangId, loaiXeId);
            foreach (DataRow dr in tableXe.Rows)
            { 
                list.Add(new Xe( dr)); 
            }

            return list;
        }

        public List<Xe> ListXeByTinhNangXeAndLoaiXeIdAndLoaiNhienLieu(List<int> lstTinhNangId, int loaiXeId, string loaiNhienLieu)
        {
            List<Xe> list = new List<Xe>();

            DataTable tableXe = XeDAO.Instance.getXeByTinhNangXeAndLoaiXeIdAndLoaiNhienLieu(lstTinhNangId, loaiXeId, loaiNhienLieu);
            foreach (DataRow dr in tableXe.Rows)
            {
                list.Add(new Xe(dr));
            }

            return list;
        }

        public List<Xe> ListXeByTuKhoaTimKiem(int loaiXeId, string tuKhoaTimKiem)
        {
            List<Xe> list = new List<Xe>();

            DataTable xes = XeDAO.Instance.getXeByTuKhoaTimKiem(loaiXeId, tuKhoaTimKiem);
            foreach (DataRow dr in xes.Rows)
            { list.Add(new Xe(dr)); }
            return list;
        }
    }
}
