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
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace front_side_system_25shiun
{
    public partial class 購買紀錄 : Form
    {
        public 購買紀錄()
        {
            InitializeComponent();
        }
        SqlConnectionStringBuilder scsb; //資料庫物件連線字串產生器，不用精靈，自己來。簡寫一個名字
        string mySunnyConnectionString = ""; //資料庫產生的字串存在這裡
        List<string> serchIDs = new List<string>(); //進階搜尋的結果
        List<string> 明細id = new List<string>(); //進階搜尋的結果
        List<int> 合計 = new List<int>(); //進階搜尋的結果

        private void 購買紀錄_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "Sunny";
            scsb.IntegratedSecurity = true;
            mySunnyConnectionString = scsb.ToString();

            listBox歷史紀錄.Items.Clear();  //先清空
            serchIDs.Clear(); //集合也內容清掉，可能之前有東西           

            SqlConnection con = new SqlConnection(mySunnyConnectionString);
            string str = "SELECT  * FROM master where 會員id=@Serchid ;";
            SqlCommand cmd = new SqlCommand(str, con);
            int  a= 0;
            a = Convert.ToInt32(Global.會員id);
            cmd.Parameters.AddWithValue("@Serchid", a);
            //查詢是變動的，SQL指令會不同
            con.Open();           
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;

            while (reader.Read())   //把東西讀出來
            {
                listBox歷史紀錄.Items.Add("訂單編號：" + reader["訂單編號"] );
                lbl訂購日期.Text = reader["訂購日期"].ToString();
                serchIDs.Add(reader["訂單編號"].ToString());
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("查無相關資訊!");
            }
            reader.Close();
            con.Close();
        }

        private void listBox歷史紀錄_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox歷史紀錄.SelectedIndex > -1)
            {
                string intId = serchIDs[listBox歷史紀錄.SelectedIndex];
                SqlConnection con = new SqlConnection(mySunnyConnectionString);
                con.Open();
                string str = "SELECT *FROM master as m INNER JOIN detail as d ON d.訂單編號=m.訂單編號 where d.訂單編號=@SerchID;";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@SerchID", intId);
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 0;
                listBox歷史紀錄明細.Items.Clear();
                合計.Clear();
                while (reader.Read())   //把東西讀出來
                {
                    int 數量 = Convert.ToInt32(reader["數量"]);
                    int 價格 = Convert.ToInt32(reader["價格"]);
                    listBox歷史紀錄明細.Items.Add("產品名稱：" + reader["產品名稱"]+" "+"數量："+ reader["數量"] + " " + reader["價格"]+"元，小計"+數量*價格+"元");                   
                    明細id.Add(reader["訂單id"].ToString());
                    serchIDs.Add(reader["訂單編號"].ToString());
                    合計.Add(價格);
                    i++;
                }
                int total = 0;
                foreach (int sum in 合計)
                {
                    total += sum;
                }
                lbl合計.Text = total.ToString();
                if (i <= 0)
                {
                    MessageBox.Show("查無相關資訊!");
                }
                reader.Close();                
            }
            else
            {
                MessageBox.Show("您尚未點選欲察看詳情之訂單項目!");

            }
        }

        private void listBox歷史紀錄明細_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
