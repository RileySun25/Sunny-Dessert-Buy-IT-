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
    public partial class 最新消息 : Form
    {
        public 最新消息()
        {
            InitializeComponent();
        }
        string mySunnyConnectionString = "";
        List<string> listtitle = new List<string>();
        List<string> listDate = new List<string>();
        List<int> listid = new List<int>();
        

        private void 最新消息_Load(object sender, EventArgs e)
        {
            if (Global.訂購總數 == 0)
            {
                lbl總數量.Visible = false;
                lbl總數量標籤.Visible = false;
            }
            else
            {
                lbl總數量.Visible = true;
                lbl總數量標籤.Visible = true;
                lbl總數量.Text = Global.訂購總數.ToString();

            }
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "Sunny";
            scsb.IntegratedSecurity = true;
            mySunnyConnectionString = scsb.ToString();

            SqlConnection con = new SqlConnection(mySunnyConnectionString);
            con.Open();
            string str = "Select 最新消息id,最新消息標題,更新日期 from news;";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader reader = cmd.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                listid.Add((int)reader["最新消息id"]);
                listtitle.Add((string)reader["最新消息標題"]);
                listDate.Add(reader["更新日期"].ToString());              
                i++;
            }
            Console.WriteLine("讀取{0}筆資料");
            reader.Close();
            con.Close();
            listBox最新消息.Items.Clear();
            string 最新消息資訊 = "";
            string 日期 = "";
            
            foreach (string title in listtitle)
            {
                最新消息資訊 = title;
                listBox最新消息.Items.Add(最新消息資訊);
            }
            foreach (string date in listDate)
            {
                日期 = date;
                listBox日期.Items.Add(日期);
            }    
            lbl特別內容.Text = "特別感謝：" +
                "\n國立高雄第一科技大學 金融系、應英系、資管系、機械系、會資系\n" +
                "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";
        }

        private void listBox最新消息_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selid = listid[ listBox最新消息.SelectedIndex];
            Console.WriteLine(selid);
            最新消息詳情 news = new 最新消息詳情 ();
            news.newsid = selid;
            Console.WriteLine(news.newsid);
            news.ShowDialog();              
        }

        private void lbl回上頁_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox最新消息_MouseMove(object sender, MouseEventArgs e)
        {
            lbl點兩下看詳情.Visible = true;

        }

        private void listBox最新消息_MouseLeave(object sender, EventArgs e)
        {
            lbl點兩下看詳情.Visible = false;
        }

        private void btn購物車_Click(object sender, EventArgs e)
        {
            購物車 news = new 購物車();
            news.ShowDialog();
        }

        private void 最新消息_Activated(object sender, EventArgs e)
        {
            if (Global.訂購總數 == 0)
            {
                lbl總數量.Visible = false;
                lbl總數量標籤.Visible = false;
            }
            else
            {
                lbl總數量.Visible = true;
                lbl總數量標籤.Visible = true;
                lbl總數量.Text = Global.訂購總數.ToString();

            }
        }

        private void 最新消息_Deactivate(object sender, EventArgs e)
        {
            if (Global.訂購總數 == 0)
            {
                lbl總數量.Visible = false;
                lbl總數量標籤.Visible = false;
            }
            else
            {
                lbl總數量.Visible = true;
                lbl總數量標籤.Visible = true;
                lbl總數量.Text = Global.訂購總數.ToString();

            }
        }
    }
}
