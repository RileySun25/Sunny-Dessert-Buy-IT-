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
using System.IO;

namespace front_side_system_25shiun
{
    public partial class 購物車 : Form
    {
        public 購物車()
        {
            InitializeComponent();
        }
        SqlConnectionStringBuilder scsb; //資料庫物件連線字串產生器，不用精靈，自己來。簡寫一個名字
        string mySunnyConnectionString = ""; //資料庫產生的字串存在這裡
        List<int> serchIDs = new List<int>(); //進階搜尋的結果
        int total = 0;
        int 刪除後總額 = 0;
        string 訂購明細 = "";
        private void 購物車_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "Sunny";
            scsb.IntegratedSecurity = true;
            mySunnyConnectionString = scsb.ToString();
            lbl最下面著作權標示.Text = "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";
            groupBox登入框.Visible = true;
            groupBox登入之後.Visible = false;

            string str = "";
            foreach (string item in Global.訂購產品名稱)
            {
                str += item;
            }            
            string pic = "";
            foreach (string item in Global.訂購產品圖片路徑)
            {
                pic = item;
                imageList1.Images.Add(Image.FromFile(pic));
            }
            listview訂購產品.Clear();
            listview訂購產品.View = View.SmallIcon;  //大圖模式圖上字下
            //列表有一種，圖片有四種(llargeicon/smallicon圖字平行/list/Tile)
            imageList1.ImageSize = new Size(90,60);  //改變照片讀出大小
            //listView產品展示.LargeImageList = imageList1;//就可顯示圖片
            listview訂購產品.SmallImageList = imageList1;            
            
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                item.Text = Global.訂購產品名稱[i]+ "，每人限購的數量為1，單價為" + Global.訂購產品價格[i]+"元，小計"+1* Global.訂購產品價格[i]+"元";
                訂購明細= Global.訂購產品名稱[i] + "，每人限購的數量為1，單價為" + Global.訂購產品價格[i] + "元，小計" + 1 * Global.訂購產品價格[i] + "元";
                //item.Tag = listID[i];
                listview訂購產品.Items.Add(item);
            }           
            foreach (int item in Global.訂購產品價格)
            {
                total += item;
            }
            lbl合計內容.Text =total.ToString();

            if (Global.登入的會員名字 != "")
            {
                MessageBox.Show("親愛的會員您好!Sunny團隊目前活動優惠中，若您有訂購會自動將總金額優惠50元喔!");
                int aa = 0;
                aa = Convert.ToInt32(lbl合計內容.Text) - 50;
                lbl合計內容.Text = aa.ToString();
            }
                Random myrand = new Random();  //隨機物件
                string 亂數 = DateTime.Now.ToString("yyyyMMdd") + myrand.Next(1000, 9999).ToString();
                lbl訂單編號.Text = 亂數;
        }
        
        private void btn註冊新會員_Click(object sender, EventArgs e)
        {
            註冊新會員 news = new 註冊新會員();
            news.ShowDialog();
        }

        private void btn登入會員_Click(object sender, EventArgs e)
        {
            if (txt電子信箱.Text != "" && txt密碼.Text != "")
            {
                SqlConnection con = new SqlConnection(mySunnyConnectionString);
                con.Open();
                string str = "select * from client where 電子信箱=@NewEmail;";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@NewEmail", txt電子信箱.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                string Code = "";
                int i = 0;

                while (reader.Read())
                {
                    Code= reader["密碼"].ToString();
                    lbl會員名稱登入後.Text = reader["姓名"].ToString();
                    Global.登入的會員名字 = reader["姓名"].ToString();
                    Global.會員id = reader["會員id"].ToString();
                    i++;
                }
                reader.Close();
                con.Close();
                if (Code == txt密碼.Text)
                {
                    MessageBox.Show("親愛的會員\n恭喜您！您已登入成功\n若您的訂單總額超過50元，\n" +
                        "系統會自動替您優惠50元！");
                    groupBox登入框.Visible = false;
                    gro2內文.Text = "感謝您的訂購！\nSunny團隊一直秉持初衷，\n" +
                        "雖限量訂購，但堅持純手工，\n願能一直獲得您的喜愛與支持。";
                    groupBox登入之後.Visible = true;
                    int aa = 0;
                    aa = Convert.ToInt32(lbl合計內容.Text);
                    int bb = 0;

                    if (aa >50)
                    {
                        bb = aa - 50;
                        lbl合計內容.Text = bb.ToString();
                    }
                    else { 
                    lbl合計內容.Text = aa.ToString();
                    }
                    
                }
                else {
                    MessageBox.Show("您的密碼有誤");
                }
            }
            else {
                MessageBox.Show("請輸入帳號、密碼");
            }
           
        }

        private void lbl回上頁_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pic看到密碼_Click(object sender, EventArgs e)
        {
            this.txt密碼.PasswordChar = default(char);
        }

        private void lbl確認訂購建_Click(object sender, EventArgs e)
        {
            if (Global.登入的會員名字 != "")
            {
                DateTime 訂購日期 = DateTime.Now;
                string 訂購日 = 訂購日期.ToString();
                SqlConnection con = new SqlConnection(mySunnyConnectionString);
                con.Open();

                for(int i=0;i<Global.訂購產品名稱.Count ;i++)
                {
                string str = "Insert into  detail values (@NewProduct,@NewCount,@NewPrice,@NewDate,@NewName,@NewCode);";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@NewPrice", Global.訂購產品價格[i].ToString());
                cmd.Parameters.AddWithValue("@NewProduct", Global.訂購產品名稱[i].ToString()) ;
                int count = 1;
                cmd.Parameters.AddWithValue("@NewCount", count);
                cmd.Parameters.AddWithValue("@NewName", Global.登入的會員名字);
                cmd.Parameters.AddWithValue("@NewDate", 訂購日);
                cmd.Parameters.AddWithValue("@NewCode", lbl訂單編號.Text);
                int rows = cmd.ExecuteNonQuery();
                }

                string str02 = "Insert into  master values (@NewCode,@NewName,@NewDate,@Newid);";
                SqlCommand cmd02 = new SqlCommand(str02, con);
                cmd02.Parameters.AddWithValue("@NewCode", lbl訂單編號.Text);
                cmd02.Parameters.AddWithValue("@NewName", Global.登入的會員名字);     
                cmd02.Parameters.AddWithValue("@NewDate", 訂購日期);
                cmd02.Parameters.AddWithValue("@Newid", Global.會員id);
                int rows02 = cmd02.ExecuteNonQuery();

                con.Close();
               
                
                MessageBox.Show("已訂購成功！\nSunny團隊純手工製作，約3天可以面交取貨，\n感謝您的訂購~");
                listview訂購產品.Items.Clear();
                Global.訂購總數 = 0;
                Global.訂購產品價格.Clear();
                Global.訂購產品名稱.Clear();
                Global.訂購產品圖片路徑.Clear();
                lbl合計內容.Text = "";
                Close();
            }
            else
            {
                MessageBox.Show("請登入會員!");
            }


        }

        private void pic刪除_Click(object sender, EventArgs e)
        {
            int 刪除的id = 0;
            刪除的id = listview訂購產品.SelectedItems[0].Index;
            Global.訂購產品價格.RemoveAt(刪除的id);
            Global.訂購產品名稱.RemoveAt(刪除的id);
            Global.訂購產品圖片路徑.RemoveAt(刪除的id);
            listview訂購產品.Items.RemoveAt(刪除的id);
            
            foreach (int item in Global.訂購產品價格)
            {
                刪除後總額 += item;
            }
            lbl合計內容.Text = 刪除後總額.ToString();
            MessageBox.Show("已替您刪除此商品!");
        }

        private void 購物車_Activated(object sender, EventArgs e)
        {
            if (Global.登入的會員名字 != "")
            {
                groupBox登入框.Visible = false;
                groupBox登入之後.Visible = true;
                gro2內文.Text = "感謝您的訂購！\nSunny團隊一直秉持初衷，\n" +
                        "雖限量訂購，但堅持純手工，\n願能一直獲得您的喜愛與支持。";
                lbl會員名稱登入後.Text = Global.登入的會員名字;
            }
            else {
                groupBox登入框.Visible = true;
                groupBox登入之後.Visible = false;
            }
        }
    }
}
