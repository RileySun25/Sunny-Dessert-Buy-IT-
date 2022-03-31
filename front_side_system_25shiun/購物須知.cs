using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace front_side_system_25shiun
{
    public partial class 購物須知 : Form
    {
        public 購物須知()
        {
            InitializeComponent();
        }

        private void lbl回上頁_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 購物須知_Load(object sender, EventArgs e)
        {
            lbl最下面著作權標示.Text = "以上圖片、商品全部為Sunny Dessert點心坊團隊擁有©";
            lbl問題1.Text = "Ｑ：請問線上訂購，有注意事項嗎？";
            lbl回答1.Visible = false;
            lbl回答1.Text = "線上訂購需要加入Sunny的會員才能訂購喔^^";
            lbl回答2.Visible = false;
            lbl回答3.Visible = false;
            lbl回答4.Visible = false;
            lbl問題2.Text = "請問為甚麼訂購有數量限制呢？";
            lbl問題3.Text = "請問在哪裡可以買到Sunny手工點心呢？";
            lbl問題4.Text = "請問有宅配嗎??";
            lbl回答2.Text = "是因為Sunny團隊所有的甜點皆是純手工的喔！";
            lbl回答3.Text = "目前在第一科大的B1女宿餐廳或網路線上訂購再約時間面交喔^^";
            lbl回答4.Text = "不好意思、目前Sunny是學生團隊，還沒有提供宅配服務喔><";
            pictureBox1.Image = Image.FromFile(@"image\open.png");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            lbl回答1.Visible = true;
            pictureBox1.Image = Image.FromFile(@"image\close.png");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            lbl回答2.Visible = true;
            pictureBox2.Image = Image.FromFile(@"image\close.png");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            lbl回答3.Visible = true;
            pictureBox4.Image = Image.FromFile(@"image\close.png");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            lbl回答4.Visible = true;
            pictureBox5.Image = Image.FromFile(@"image\close.png");
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            lbl回答1.Visible = false;
            pictureBox1.Image = Image.FromFile(@"image\open.png");
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            lbl回答2.Visible = false;
            pictureBox2.Image = Image.FromFile(@"image\open.png");
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            lbl回答3.Visible = false;
            pictureBox4.Image = Image.FromFile(@"image\open.png");
        }

        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {
            lbl回答4.Visible = false;
            pictureBox5.Image = Image.FromFile(@"image\open.png");
        }

        private void btn購物車_Click(object sender, EventArgs e)
        {
            購物車 news = new 購物車();
            news.ShowDialog();
        }

        private void 購物須知_Activated(object sender, EventArgs e)
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

        private void 購物須知_Deactivate(object sender, EventArgs e)
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
