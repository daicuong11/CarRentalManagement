using QLThueXeOto.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DAO
{
    public class LoaiXeDAO
    {
        private static LoaiXeDAO instance;

        public static LoaiXeDAO Instance
        {
            get { if (instance == null) instance = new LoaiXeDAO(); return LoaiXeDAO.instance; }
            private set { LoaiXeDAO.instance = value; }
        }

        public DataTable getAll()
        {
            return DataProvider.Instance.ExecuteQuery("select * from loaixe");
        }
    }
}
