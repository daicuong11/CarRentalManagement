using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DAO
{
    public class NguoiDungDAO
    {
        private static NguoiDungDAO instance;

        public static NguoiDungDAO Instance
        {
            get { if (instance == null) instance = new NguoiDungDAO(); return NguoiDungDAO.instance; }
            private set { NguoiDungDAO.instance = value; }
        }


        public DataTable getAll()
        {
            return DataProvider.Instance.ExecuteQuery("Select * from nguoidung");
        }

        public NguoiDung getNguoiDungById(int id)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from nguoidung where nguoiDungId = @nguoiDungId ", new object[] { id });
            if(tb.Rows.Count > 0)
            {
                return new NguoiDung(tb.Rows[0]);
            }
            return null;
        }

    }
}
