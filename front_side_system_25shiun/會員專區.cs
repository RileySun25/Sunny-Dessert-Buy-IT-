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
    public partial class 會員專區 : Form
    {
        public 會員專區()
        {
            InitializeComponent();
        }

        private void 聯絡我們_Load(object sender, EventArgs e)
        {
            lbl登入員工姓名.Text = Global.登入的會員名字;
        }

        private void btn登出_Click(object sender, EventArgs e)
        {
            Global.登入的會員名字 = "";
            MessageBox.Show("您登出成功!");           
            lbl您好.Visible = false;
            lbl親愛的.Visible = false;
            Close();
        }

        private void btn更改密碼_Click(object sender, EventArgs e)
        {
            改密碼 news = new 改密碼();
            news.ShowDialog();
        }

        private void btn個人資訊查詢_Click(object sender, EventArgs e)
        {
            個人資料 news = new 個人資料();
            news.ShowDialog();
        }

        private void bt歷史紀錄_Click(object sender, EventArgs e)
        {
            購買紀錄 news = new 購買紀錄();
            news.ShowDialog();
        }
    }
}
