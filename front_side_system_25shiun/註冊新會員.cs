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
    public partial class 註冊新會員 : Form
    {
        public 註冊新會員()
        {
            InitializeComponent();
        }
        SqlConnectionStringBuilder scsb; //資料庫物件連線字串產生器，不用精靈，自己來。簡寫一個名字
        string mySunnyConnectionString = ""; //資料庫產生的字串存在這裡
        List<int> serchIDs = new List<int>(); //進階搜尋的結果
        string image_dir = @"image\";  //圖檔目錄
        string image_name = "";  //圖檔名稱
        private void 註冊新會員_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "Sunny";
            scsb.IntegratedSecurity = true;
            mySunnyConnectionString = scsb.ToString();
            lbl最下面著作權標示.Text = "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";
            lbl註冊內容.Text = "歡迎成為Sunny Dessert手工點心坊的會員，" +
                "\n首先要請您注意以下兩點：" +
                "\n1.姓名、Email、密碼為必填欄位，請務必填寫！" +
                "\n2.往後Email和密碼為登入會員的帳密，請註冊之後務必牢記！";
        }

        private void btn送出_Click(object sender, EventArgs e)
        {

            if ((txt名字.Text != "") && (txt電子信箱.Text != "") && (txt密碼.Text != "") && (txt確認密碼.Text != ""))
            {
                if (txt密碼.Text == txt確認密碼.Text)
                {
                    pictureBox大頭貼.Image.Save(image_dir + image_name);
                    SqlConnection con = new SqlConnection(mySunnyConnectionString);
                    con.Open();
                    string str = "insert into client values (@Newsex,@NewBirth,@NewEmail,@NewCode,@NewTel,@NewAdress,@NewPic,@NewName);";
                    SqlCommand smd = new SqlCommand(str, con);
                    //參數帶入值
                    smd.Parameters.AddWithValue("@Newsex", checkBox男生.Checked);
                    smd.Parameters.AddWithValue("@NewBirth", dpt生日.Value);
                    smd.Parameters.AddWithValue("@NewEmail", txt電子信箱.Text);
                    smd.Parameters.AddWithValue("@NewTel", txt手機.Text);
                    smd.Parameters.AddWithValue("@NewCode", txt確認密碼.Text);
                    smd.Parameters.AddWithValue("@NewAdress", txt地址.Text);
                    smd.Parameters.AddWithValue("@NewName", txt名字.Text);
                    smd.Parameters.AddWithValue("@NewPic", image_name);

                    int rows = smd.ExecuteNonQuery();  //只執行部查詢，顯示影響的資料筆數
                    con.Close();
                    MessageBox.Show($"{rows}筆資料新增成功!");
                    txt名字.Text = "";
                    txt地址.Text = "";
                    txt密碼.Text = "";
                    txt手機.Text = "";
                    txt確認密碼.Text = "";
                    txt電子信箱.Text = "";
                    dpt生日.Value = Convert.ToDateTime("1990-01-01");
                    checkBox女生.Checked = false;
                    checkBox男生.Checked = false;
                    pictureBox大頭貼.Image = null;
                }
                else {
                    MessageBox.Show("確認密碼需與密碼相同!");
                }
                
            }
            else
            {
                MessageBox.Show("記得填寫姓名、電子信箱以及密碼!");
            }
        }

        private void lbl船大頭照_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();  //選取新的圖片跳出對話框
            f.Filter = "圖檔類型(*.jpg,*.JPG,*.png)|*.jpeg;*.jpg;*.png";
            //限制可以選取圖片的附檔名

            DialogResult R = f.ShowDialog();

            if (R == DialogResult.OK)
            {
                pictureBox大頭貼.Image = Image.FromFile(f.FileName);
                string fileExt = Path.GetExtension(f.SafeFileName); //用檔案名稱來取得附檔名
                Random myrand = new Random();  //隨機物件
                image_name = DateTime.Now.ToString("yyyyMMddHHmmss") + myrand.Next(1000, 9999).ToString() + fileExt;                
                
            }
        }

        private void lbl回上頁_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
