using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DAO
{
    internal class AuthDAO
    {
        private static AuthDAO instance;
        private NguoiDung user = null;
        public NguoiDung User { get => user; set => user = value; }


        public static AuthDAO Instance
        {
            get { if (instance == null) instance = new AuthDAO(); return AuthDAO.instance; }
            private set { AuthDAO.instance = value; }
        }

        public NguoiDung login(string username, string password)
        {
            NguoiDung nguoiDung;
            string query = "exec usp_login @username , @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            if (result.Rows.Count > 0)
            {
                nguoiDung = new NguoiDung(result.Rows[0]);
                this.user = nguoiDung;
                return nguoiDung;
            }
            return null;
        }
    }
}
