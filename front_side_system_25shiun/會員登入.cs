using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data.SqlClient;

namespace front_side_system_25shiun
{
    public partial class 會員登入 : Form
    {
        public 會員登入()
        {
            InitializeComponent();
        }
        SqlConnectionStringBuilder scsb; //資料庫物件連線字串產生器，不用精靈，自己來。簡寫一個名字
        string mySunnyConnectionString = ""; //資料庫產生的字串存在這裡
        List<int> serchIDs = new List<int>(); //進階搜尋的結果

        private void 會員登入_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "Sunny";
            scsb.IntegratedSecurity = true;
            mySunnyConnectionString = scsb.ToString();
            lbl最下面著作權標示.Text = "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";


            Random myrand = new Random();  //隨機物件
            string aa = CreateCheckCode();
            string 驗證碼 =  aa + myrand.Next(1000, 9999).ToString();
            lbl驗證碼的內容.Text = 驗證碼;
        }
        public string CreateCheckCode()
        {
            char[] CharArray = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string sCode = "";
            Random random = new Random();
            for (int i = 0; i < 2; i++)
            {
                sCode += CharArray[random.Next(CharArray.Length)];
            }
            return sCode;
        }
        private void btn登入_Click(object sender, EventArgs e)
        {
            if (txt信箱.Text != "" && txt密碼.Text != "")
            {
                SqlConnection con = new SqlConnection(mySunnyConnectionString);
                con.Open();
                string str = "select * from client where 電子信箱=@NewEmail;";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@NewEmail", txt信箱.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                string Code = "";
                int i = 0;

                while (reader.Read())
                {
                    Code = reader["密碼"].ToString();
                    //lbl會員名稱登入後.Text = reader["姓名"].ToString();
                    Global.登入的會員名字 = reader["姓名"].ToString();
                    Global.會員id = reader["會員id"].ToString();
                    Global.密碼 = reader["密碼"].ToString();
                    i++;
                }
                reader.Close();
                con.Close();
                if (Code == txt密碼.Text && lbl驗證碼的內容.Text == txt驗證碼.Text)
                {
                    MessageBox.Show("親愛的會員\n恭喜您！您已登入成功！");
                    Close();
                }
                else
                {
                    MessageBox.Show("您的密碼有誤");
                    txt信箱.Text = "";
                    txt密碼.Text = "";
                }
            }
            else
            {
                MessageBox.Show("請輸入帳號、密碼");
            }
        }
    }
}
