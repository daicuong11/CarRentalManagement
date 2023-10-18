using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DTO
{
    public class User
    {
        int userId;
        string name;
        string username;
        string email;
        string password;
        int roleId;

        public User()
        {
        }

        public User(int userId, string name, string username, string email, string password, int roleId)
        {
            this.userId = userId;
            this.name = name;
            this.username = username;
            this.email = email;
            this.password = password;
            this.roleId = roleId;
        }

        public User(DataRow row)
        {
            this.userId = (int)row["userId"];
            this.name = row["name"].ToString();
            this.username = row["username"].ToString();
            this.email = row["email"].ToString();
            this.password = row["pwd"].ToString();
            this.roleId = (int)row["roleId"];
        }

        public int UserId { get => userId; set => userId = value; }
        public string Username { get => username; set => username = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int RoleId { get => roleId; set => roleId = value; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
