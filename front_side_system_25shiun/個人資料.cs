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
    public partial class 個人資料 : Form
    {
        public 個人資料()
        {
            InitializeComponent();
        }
        SqlConnectionStringBuilder scsb; //資料庫物件連線字串產生器，不用精靈，自己來。簡寫一個名字
        string mySunnyConnectionString = ""; //資料庫產生的字串存在這裡
        List<int> serchIDs = new List<int>(); //進階搜尋的結果
        string image_dir = @"image\";  //將圖檔路徑寫成欄位
        string image_name = "";
        Boolean 是否修改過圖檔 = false;
        private void 個人資料_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "Sunny";
            scsb.IntegratedSecurity = true;
            mySunnyConnectionString = scsb.ToString();
           

            SqlConnection con = new SqlConnection(mySunnyConnectionString);
            con.Open();
            string str = "select* from client where 會員id=@NewNum;";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@NewNum", Global.會員id);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;

            while (reader.Read())
            {
                txt姓名.Text = reader["姓名"].ToString();              
                txt信箱.Text = reader["電子信箱"].ToString();              
                txt電話.Text = reader["手機"].ToString();
                txt住址.Text = reader["地址"].ToString();
                dtp生日.Value = Convert.ToDateTime(reader["生日"]);
                Boolean sex = Convert.ToBoolean(reader["性別"]);
                ckbox男生.Checked = Convert.ToBoolean(reader["性別"]);
                image_name = reader["會員大頭照路徑"].ToString();
                try {
                    pictureBox會員照片.Image = Image.FromFile(image_dir + image_name);
                } catch (Exception)
                {
                    pictureBox會員照片.Image = null;
                }
                
                if (Convert.ToBoolean(reader["性別"]))
                {
                    ckbox男生.Checked = Convert.ToBoolean(reader["性別"]);
                }
                else
                {
                    ckbox女生.Checked = !sex;
                }
                i++;
            }
            reader.Close();
            con.Close();
        }

        private void btn便照片_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();  //選取新的圖片跳出對話框
            f.Filter = "圖檔類型(*.jpg,*.JPG,*.png)|*.jpeg;*.jpg;*.png";
            //限制可以選取圖片的附檔名

            DialogResult R = f.ShowDialog();

            if (R == DialogResult.OK)
            {
                pictureBox會員照片.Image = Image.FromFile(f.FileName);
                string fileExt = Path.GetExtension(f.SafeFileName); //用檔案名稱來取得附檔名
                Random myrand = new Random();  //隨機物件
                image_name = DateTime.Now.ToString("yyyyMMddHH") + myrand.Next(1000, 9999).ToString() + fileExt;
                //設定隨機檔名的格式
                是否修改過圖檔 = true;
                Console.WriteLine(image_name);
            }
        }
        private void btn儲存_Click(object sender, EventArgs e)
        {
            if (txt姓名.Text != "" && (txt電話.Text != ""))
            {
                if (是否修改過圖檔 == true)
                {
                    //將照片存檔
                    pictureBox會員照片.Image.Save(image_dir + image_name);
                    是否修改過圖檔 = false;
                }
                SqlConnection con = new SqlConnection(mySunnyConnectionString);
                con.Open();
                string str = "Update client set 手機=@NewTel,性別 = @NewSex,生日 = @NewDate,電子信箱 = @NewEmail,地址 = @NewPath,會員大頭照路徑 = @Newpic,姓名 = @NewName where 會員id = @SerchID;";
                //不要用字串合成!會被入侵，避免SQL inJection!用字串插入
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@SerchID", Global.會員id);
                int a = 0;
                if (ckbox男生.Checked)
                {
                    a = 1;
                }
                else
                {
                    a = 0;
                }
                cmd.Parameters.AddWithValue("@NewSex", a);
                cmd.Parameters.AddWithValue("@NewEmail", txt信箱.Text);
                cmd.Parameters.AddWithValue("@NewPath", txt住址.Text);              
                cmd.Parameters.AddWithValue("@Newpic", image_name);
                cmd.Parameters.AddWithValue("@NewName", txt姓名.Text);
                cmd.Parameters.AddWithValue("@NewImage", image_name);
                cmd.Parameters.AddWithValue("@NewDate", dtp生日.Value);
                cmd.Parameters.AddWithValue("@NewTel", txt電話.Text);

                int rows = cmd.ExecuteNonQuery();  //只執行部查詢，顯示影響的資料筆數
                con.Close();
                MessageBox.Show($"{rows}筆資料更新成功!");

                Global.登入的會員名字 = txt姓名.Text;
            }
            else
            {
                MessageBox.Show("欲修改產品資料需填產品編號及單價!");
            }
        }
    }
}
