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
    public partial class 商品專區 : Form
    {
        public 商品專區()
        {
            InitializeComponent();
        }
        Thread th;
        Thread th2;
        string mySunnyConnectionString = "";
        List<string> listName = new List<string>();
        List<int> listPrice = new List<int>();
        List<string> listID = new List<string>();
        
        List<string> listpic = new List<string>();
        private void 商品專區_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "Sunny";
            scsb.IntegratedSecurity = true;
            mySunnyConnectionString = scsb.ToString();
            讀取資料庫的方法();
            listview產生商品展示的大圖片模式();

            if (Global.訂購總數 == 0)
            {
                lbl總數量.Visible = false;
            }
            else
            {
                lbl總數量.Visible = true;
                lbl總數量.BackColor = Color.Transparent;
                lbl總數量.Parent = 購物車;
                lbl總數量.Location = new Point(0, 0);
                lbl總數量.Text = Global.訂購總數.ToString();

            }
            lbl最下面著作權標示.Text = "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";
            th = new Thread
                    (
                        delegate ()
                        {
                            try {
                                for (int i = 0; i < 100; i++)//3就是要迴圈輪數了
                                {
                                    //呼叫方法
                                    //設定圖片的位置和顯示時間（1000 為1秒）
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\三角褲.jpg"), 1000);
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\885337_497425430293742_236143849_o.jpg"), 1000);
                                    ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\389778_435307736505512_939566377_n.jpg"), 1000);
                                }
                            }
                            catch (Exception) { 
                            
                            }
                            
                        }
                    );
            th.IsBackground = true;
            th.Start();
            th2 = new Thread
                    (
                        delegate ()
                        {
                            //3就是要迴圈輪數了
                            try
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    //呼叫方法
                                    //設定圖片的位置和顯示時間（1000 為1秒）
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_7918.JPG"), 1000);
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_8237.JPG"), 1000);
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_8238.JPG"), 1000);
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_7921.JPG"), 1000);
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_7922.JPG"), 1000);
                                }
                            }
                            catch (Exception) { 
                            
                            }
                            
                        }
                    );
            th2.IsBackground = true;
            th2.Start();
           
        }
        void 讀取資料庫的方法()
        {
            SqlConnection con = new SqlConnection(mySunnyConnectionString);
            con.Open();
            string str = "Select * from product where level=1;";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader reader = cmd.ExecuteReader();

            string image_dir = @"image\";  //圖檔目錄
            string image_name = "";  //圖檔名稱
            int i = 0;

            while (reader.Read())
            {
                try {
                    listID.Add((string)reader["產品編號"]);
                    listName.Add((string)reader["產品名稱"]);
                    listPrice.Add((int)reader["價格"]);
                    image_name = reader["圖片路徑"].ToString();
                    imageList1.Images.Add(Image.FromFile(image_dir + image_name));
                    listpic.Add(image_dir + image_name);
                    i++;
                } catch (Exception)
                { 
                
                }
                
            }
            Console.WriteLine("讀取{0}筆資料");
            reader.Close();
            con.Close();

        }
        void listview產生商品展示的大圖片模式()
        {
            listView單片.Clear();
            listView單片.View = View.LargeIcon;  //大圖模式圖上字下
            //列表有一種，圖片有四種(llargeicon/smallicon圖字平行/list/Tile)
            imageList1.ImageSize = new Size(200, 180);  //改變照片讀出大小
            listView單片.LargeImageList = imageList1;//就可顯示圖片


            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Font = new Font("微軟正黑體", 10, FontStyle.Regular);
                item.Text = listName[i]+"\n"+listPrice[i]+"元";
                //item.Tag = listID[i];                               
                listView單片.Items.Add(item);
                

            }
        }        
        private void ChangeImage(Image img, int millisecondsTimeOut)
        {
            try
            {
                    this.Invoke(new Action(() =>
            {
                pictureBox奶酪專區.Image = img;
            })
                );
                    Thread.Sleep(millisecondsTimeOut);
            }
            catch (Exception) { 
            
            }
        }
        private void ChangeImage2(Image img, int millisecondsTimeOut)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    pictureBox主打2.Image = img;
                })
                    );
                Thread.Sleep(millisecondsTimeOut);

            }
            catch (Exception) { 
            
            }
            
        }
        private void lbl重乳酪系列_Click(object sender, EventArgs e)
        {
            重乳酪專區 news = new 重乳酪專區();
            news.ShowDialog();
        }

        private void lbl奶酪系列_Click(object sender, EventArgs e)
        {
            奶酪系列 news = new 奶酪系列();
            news.ShowDialog();
        }

        private void 購物車_Click(object sender, EventArgs e)
        {
            購物車 news = new 購物車();
            news.ShowDialog();
        }

        private void pictureBox奶酪專區_MouseMove(object sender, MouseEventArgs e)
        {
            try {
                th.Suspend();
            }
            catch (Exception) { 
            
            }
        }

        private void pictureBox奶酪專區_MouseLeave(object sender, EventArgs e)
        {
            try {
                th = new Thread
                         (
                             delegate ()
                             {
                             //3就是要迴圈輪數了
                             for (int i = 0; i < 20; i++)
                                 {
                                 //呼叫方法
                                 //設定圖片的位置和顯示時間（1000 為1秒）
                                     ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\三角褲.jpg"), 1000);
                                     ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\885337_497425430293742_236143849_o.jpg"), 1000);
                                     ChangeImage(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\389778_435307736505512_939566377_n.jpg"), 1000);
                                 }
                             }
                         );
                th.IsBackground = true;
                th.Start();
            }
            catch (Exception) { 
            
            }
            
        }

        private void pictureBox主打2_MouseMove(object sender, MouseEventArgs e)
        {
            th2.Suspend();
        }

        private void pictureBox主打2_MouseLeave(object sender, EventArgs e)
        {
            th2 = new Thread
                    (
                        delegate ()
                        {
                            //3就是要迴圈輪數了
                            try
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    //呼叫方法
                                    //設定圖片的位置和顯示時間（1000 為1秒）
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_7918.JPG"), 1000);
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_8237.JPG"), 1000);
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_8238.JPG"), 1000);
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_7921.JPG"), 1000);
                                    ChangeImage2(Image.FromFile(@"D:\資策會全端養成班\期中專題-冷飲店訂購單\期中專題(完整)\前台系統\front_side_system_25shiun\front_side_system_25shiun\bin\Debug\image\IMG_7922.JPG"), 1000);
                                }
                            }
                            catch (Exception) { 
                            
                            }
                            
                        }
                    );
            th2.IsBackground = true;
            th2.Start();
        }

        private void lbl回上頁_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView單片_ItemActivate(object sender, EventArgs e)
        {
            int selID = (int)listView單片.SelectedIndices[0];   //取得所選項目的ID
            string 點選產品 = "";
            點選產品 = listName[selID];
            string 點選圖片 = "";
            點選圖片 = listpic[selID];
            int price = 0;
            price = listPrice[selID];
            Global.訂購產品名稱.Add(點選產品);
            Global.訂購產品圖片路徑.Add(點選圖片);
            Global.訂購產品價格.Add(price);
            Global.訂購總數++;
            MessageBox.Show("已替您加入購物車!");

        }

        private void 商品專區_Activated(object sender, EventArgs e)
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

        private void 商品專區_Deactivate(object sender, EventArgs e)
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
