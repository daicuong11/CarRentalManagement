using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.BLL
{
    public class HomeBLL
    {
        private static HomeBLL instance;

        public static HomeBLL Instance {
            get { if (instance == null) instance = new HomeBLL(); return HomeBLL.instance; }
            private set { HomeBLL.instance = value; }
        }

    }
}
