using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace front_side_system_25shiun
{
    class Global
    {
        public static string 登入的會員名字 = "";
        public static List<string> 訂購產品名稱 = new List<string>();
        public static List<string> 訂購產品圖片路徑 = new List<string>();
        public static List<int> 訂購產品價格 = new List<int>();
        public static int 訂購總數 = 0;
        public static string 會員id = "";
        public static string 密碼 = "";
    }
}
