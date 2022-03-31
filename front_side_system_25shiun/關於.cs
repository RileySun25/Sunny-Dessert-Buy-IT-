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
using System.Diagnostics;

namespace front_side_system_25shiun
{
    public partial class 關於 : Form
    {
        public 關於()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lbl品牌故事文案.Text = "    Sunny奶酪起源於民國100年5月，由一群第一科大熱愛甜點的大學生所創立。\n\n" +
                "    起初只是想將這種美味分享給家人與身邊的朋友，意外大受好評。為了能讓更多人品嘗到Sunny奶酪的美味，我們參與了學校的創業競賽，獲得創業資金以及學校提供的實體店面空間。\n\n" +
                "    於是我們開始在Face Book用線上表單接訂單的方式，販售給第一科大的師生，我們秉持「分享給摯愛」的初衷，堅持以手工方式製作Sunny奶酪，並在發源地打出良好口碑。從第一科大的女宿餐廳開始，我們一起讓更多人品嘗Sunny奶酪。\n";
            lbl新聞報導文案標題.Text = "1.財經要聞 微型創業-Sunny奶酪要征服學子味蕾";
            超連結一文案.Text = "https://www.chinatimes.com/newspapers/20121010000203-260202?chdtv";
            lbl標題2.Text = "2.中國時報 愛上做蛋糕 跨領域行銷 Sunny Dessert開點心坊";
            超連結二文案.Text = "https://tw.news.yahoo.com/愛上做蛋糕跨領域行銷.html";
            lbl標題3.Text = "3.中時新聞網 微型創業－Sunny奶酪香";
            超連結3.Text = "https://tw.news.yahoo.com/微型創業－Sunny奶酪香.html";
        }

        private void 超連結一文案_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            超連結一文案.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.chinatimes.com/newspapers/20121010000203-260202?chdtv");
        }

        private void 超連結二文案_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            超連結二文案.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tw.news.yahoo.com/%25E6%2584%259B%25E4%25B8%258A%25E5%2581%259A%25E8%259B%258B%25E7%25B3%2595-%25E8%25B7%25A8%25E9%25A0%2598%25E5%259F%259F%25E8%25A1%258C%25E9%258A%25B7-%25E6%25A9%259F%25E6%25A2%25B0%25E7%25B3%25BB%25E6%2594%25B9%25E6%2594%25BB%25E8%25A8%25AD%25E8%25A8%2588-%25E9%2596%258B%25E9%25BB%259E%25E5%25BF%2583%25E5%259D%258A-213000782.html");
        }

        private void 超連結3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            超連結3.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tw.news.yahoo.com/%E5%BE%AE%E5%9E%8B%E5%89%B5%E6%A5%AD-sunny%E5%A5%B6%E9%85%AA%E9%A6%99-%E8%A6%81%E5%BE%81%E6%9C%8D%E5%AD%B8%E5%AD%90%E5%91%B3%E8%95%BE-213000651.html");
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            lbl品牌故事文案.Text = "    Sunny奶酪起源於民國100年5月，由一群第一科大熱愛甜點的大學生所創立。\n\n" +
                "    起初只是想將這種美味分享給家人與身邊的朋友，意外大受好評。為了能讓更多人品嘗到Sunny奶酪的美味，我們參與了學校的創業競賽，獲得創業資金以及學校提供的實體店面空間。\n\n" +
                "    於是我們開始在Face Book用線上表單接訂單的方式，販售給第一科大的師生，我們秉持「分享給摯愛」的初衷，堅持以手工方式製作Sunny奶酪，並在發源地打出良好口碑。從第一科大的女宿餐廳開始，我們一起讓更多人品嘗Sunny奶酪。\n";
            lbl新聞報導文案標題.Text = "1.財經要聞 微型創業-Sunny奶酪要征服學子味蕾";
            超連結一文案.Text = "https://www.chinatimes.com/newspapers/20121010000203-260202?chdtv";
            lbl標題2.Text = "2.中國時報 愛上做蛋糕 跨領域行銷 Sunny Dessert開點心坊";
            超連結二文案.Text = "https://tw.news.yahoo.com/愛上做蛋糕跨領域行銷.html";
            lbl標題3.Text = "3.中時新聞網 微型創業－Sunny奶酪香";
            超連結3.Text = "https://tw.news.yahoo.com/微型創業－Sunny奶酪香.html";
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

        private void lbl回上頁_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn購物車_Click(object sender, EventArgs e)
        {
            購物車 news = new 購物車();
            news.ShowDialog();
        }

        private void Form2_Activated(object sender, EventArgs e)
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

        private void Form2_Deactivate(object sender, EventArgs e)
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
