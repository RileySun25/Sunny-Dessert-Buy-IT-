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
    public partial class 重乳酪專區 : Form
    {
        public 重乳酪專區()
        {
            InitializeComponent();
        }
        string mySunnyConnectionString = "";
        List<string> listName = new List<string>();
        List<int> listPrice = new List<int>();
        List<string> listID = new List<string>();
        List<string> listpic = new List<string>();
        private void 重乳酪專區_Load(object sender, EventArgs e)
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
                lbl總數量標籤.Visible = false;
            }
            else
            {
                lbl總數量.Visible = true;
                lbl總數量標籤.Visible = true;
                lbl總數量.Text = Global.訂購總數.ToString();

            }
            lbl最下面著作權標示.Text = "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";
        }
        void 讀取資料庫的方法()
        {
            SqlConnection con = new SqlConnection(mySunnyConnectionString);
            con.Open();
            string str = "Select * from product where level=2;";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader reader = cmd.ExecuteReader();

            string image_dir = @"image\";  //圖檔目錄
            string image_name = "";  //圖檔名稱
            int i = 0;

            while (reader.Read())
            {
                try
                {
                    listID.Add((string)reader["產品編號"]);
                    listName.Add((string)reader["產品名稱"]);
                    listPrice.Add((int)reader["價格"]);
                    image_name = reader["圖片路徑"].ToString();
                    imageList1.Images.Add(Image.FromFile(image_dir + image_name));
                    listpic.Add(image_dir + image_name);
                    i++;
                }
                catch (Exception) {
                
                }

            }
            Console.WriteLine("讀取{0}筆資料");
            reader.Close();
            con.Close();

        }
        void listview產生商品展示的大圖片模式()
        {
            listView重乳酪系列.Clear();
            listView重乳酪系列.View = View.LargeIcon;  //大圖模式圖上字下
            //列表有一種，圖片有四種(llargeicon/smallicon圖字平行/list/Tile)
            imageList1.ImageSize = new Size(180,130);  //改變照片讀出大小
            listView重乳酪系列.LargeImageList = imageList1;//就可顯示圖片


            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Font = new Font("微軟正黑體", 10, FontStyle.Regular);
                item.Text = listName[i] + "\n" + listPrice[i] + "元";             
                listView重乳酪系列.Items.Add(item);               

            }
        }
        private void 購物車_Click(object sender, EventArgs e)
        {
            購物車 news = new 購物車();
            news.ShowDialog();
        }

        private void lbl回上頁_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView重乳酪系列_ItemActivate(object sender, EventArgs e)
        {
            int selID = (int)listView重乳酪系列.SelectedIndices[0];   //取得所選項目的ID
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

        private void 重乳酪專區_Activated(object sender, EventArgs e)
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

        private void 重乳酪專區_Deactivate(object sender, EventArgs e)
        {
            if (Global.訂購總數 == 0)
            {
                lbl總數量.Visible = false;
                lbl總數量標籤.Visible = true;

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
