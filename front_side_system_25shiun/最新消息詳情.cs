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
    public partial class 最新消息詳情 : Form
    {
        public 最新消息詳情()
        {
            InitializeComponent();
        }
        string mySunnyConnectionString = "";       
        public int newsid=0 ;
        List<string> listtitle = new List<string>();
        List<string> listDate = new List<string>();
        List<string> listdetail = new List<string>();
        List<int> listid = new List<int>();

        private void 最新消息詳情_Load(object sender, EventArgs e)
        {
            lbl最下面著作權標示.Text = "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "Sunny";
            scsb.IntegratedSecurity = true;
            mySunnyConnectionString = scsb.ToString();

            SqlConnection con = new SqlConnection(mySunnyConnectionString);
            con.Open();
            string str = "Select 更新者,最新消息標題,最新消息內容,更新日期 from news where 最新消息id=@Serchid;";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@Serchid",newsid);
            SqlDataReader reader = cmd.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                lbl標題內容.Text = reader["最新消息標題"].ToString();
                lbl詳情內容.Text= reader["最新消息內容"].ToString();
                lbl日期內容.Text = reader["更新日期"].ToString();
                lbl單位內容.Text = reader["更新者"].ToString();
                i++;
            }
            reader.Close();
            con.Close();
            Console.WriteLine(newsid);
        }

        private void lbl回上頁_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
