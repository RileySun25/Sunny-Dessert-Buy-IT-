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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
       
        SqlConnectionStringBuilder scsb; //資料庫物件連線字串產生器，不用精靈，自己來。簡寫一個名字
        string mySunnyConnectionString = ""; //資料庫產生的字串存在這裡
        List<int> serchIDs = new List<int>(); //進階搜尋的結果
        Thread th;
        string image_dir = @"image\";  //圖檔目錄
        string image_name = "";  //圖檔名稱
        private void Form1_Load(object sender, EventArgs e)
        {
            {             
                th = new Thread
                    (
                        delegate ()
                        {
                            for (int a = 0; a < 5; a++)
                            {
                                try
                                {
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\Sunny\0001.jpg"), 1000);
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\Sunny\0002.jpg"), 1000);
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\Sunny\0003.jpg"), 1000);
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\Sunny\0004.jpg"), 1000);
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_8234.JPG"), 1000);
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_8235.JPG"), 1000);
                                }
                                catch (Exception) { 
                                    
                                }
                            }
                        }
                    );
                th.IsBackground = true;
                th.Start();

                lbl最下面著作權標示.Text = "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";
                lbl親愛的.Visible = false;
                lbl會員名稱登入後.Visible = false;
                lbl您好.Visible = false;
                btn會員專區.Visible = false;
                if (Global.訂購總數==0)
                {
                    lbl總數量.Visible = false;
                }
                else
                {
                    lbl總數量.Visible = true;
                    lbl總數量.Text = Global.訂購總數.ToString();      

                }
                btn登出.Visible = false;
                         
            }
        }
        private void ChangeImage(Image img, int millisecondsTimeOut)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    pictureBox1.Image = img;
                })
                    );
                Thread.Sleep(millisecondsTimeOut);
            }
            catch (Exception) { 
            
            }
            
        }

        private void 關於SunnyDessert(object sender, EventArgs e)
        {
            關於 Aboat = new 關於();
            Aboat.ShowDialog();
        }

        private void 最新消息(object sender, EventArgs e)
        {
            最新消息 news = new 最新消息();
            news.ShowDialog();
        }

        private void 商品專區(object sender, EventArgs e)
        {
            商品專區 news = new 商品專區();
            news.ShowDialog();
        }

        private void 購物須知(object sender, EventArgs e)
        {
            購物須知 news = new 購物須知();
             news.ShowDialog();
        }

        private void 聯絡我們(object sender, EventArgs e)
        {
            會員專區 news = new 會員專區();
            news.ShowDialog();
        }

        private void 購物車_Click(object sender, EventArgs e)
        {
            購物車 news = new 購物車();
            news.ShowDialog();
        }

        private void 會員註冊_Click(object sender, EventArgs e)
        {
            註冊新會員 news = new 註冊新會員();
             news.ShowDialog();
        }

        private void lbl加入會員_Click(object sender, EventArgs e)
        {
            註冊新會員 news = new 註冊新會員();
            news.ShowDialog();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (Global.登入的會員名字 != "")
            {
                lbl會員名稱登入後.Text = Global.登入的會員名字;
                lbl親愛的.Visible = true;
                lbl會員名稱登入後.Visible = true;
                lbl您好.Visible = true;
                btn會員專區.Visible = true;
            }
            else {
                btn會員專區.Visible = false;
                lbl親愛的.Visible = false;
                lbl會員名稱登入後.Visible = false;
                lbl您好.Visible = false;
            }
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
            if (Global.登入的會員名字 != "")
            {
                btn登出.Visible = true;
            }
            else {
                btn登出.Visible = false;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (Global.登入的會員名字 != "")
            {
                lbl會員名稱登入後.Text = Global.登入的會員名字;
                lbl親愛的.Visible = true;
                lbl會員名稱登入後.Visible = true;
                lbl您好.Visible = true;
                btn會員專區.Visible = true;
            }
            else
            {
                btn會員專區.Visible = false;
                lbl親愛的.Visible = false;
                lbl會員名稱登入後.Visible = false;
                lbl您好.Visible = false;                ;
            }
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
            if (Global.登入的會員名字 != "")
            {
                btn登出.Visible = true;
            }
            else
            {
                btn登出.Visible = false;
            }
        }

        private void lbl會員登入_Click(object sender, EventArgs e)
        {
            會員登入 news = new 會員登入();
            news.ShowDialog();
        }

        private void btn登出_Click(object sender, EventArgs e)
        {
            Global.登入的會員名字 = "";
            MessageBox.Show("您登出成功!");
            lbl您好.Visible = false;
            lbl親愛的.Visible = false;
            lbl會員名稱登入後.Visible = false;
            btn會員專區.Visible = false;
        }

        private void btn會員專區_Click(object sender, EventArgs e)
        {
            會員專區 news = new 會員專區();
            news.ShowDialog();
        }
    }
}
