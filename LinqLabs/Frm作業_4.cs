using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_4 : Form
    {
        public Frm作業_4()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(nwDataSet11.Orders);
            this.productsTableAdapter1.Fill(nwDataSet11.Products);
            this.categoriesTableAdapter1.Fill(nwDataSet11.Categories);
            this.order_DetailsTableAdapter1.Fill(nwDataSet11.Order_Details);
        }

        //依檔案大小分組檔案(大=>小)
        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

             var q = from f in files
                    orderby f.Length descending
                    select f;

            this.dataGridView1.DataSource = q.ToList();
        }

        //依年分組檔案(大=>小)
        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files

                    orderby f.CreationTime descending
                    select f;
            this.dataGridView2.DataSource = q.ToList();
        }

        //NW Products 低中高價的產品
        //要創立一個低中高的門檻
        private void button8_Click(object sender, EventArgs e)
        {
            var q = from p in nwDataSet11.Products.AsEnumerable()
                    group p by MyUnitPrice(p.UnitPrice) into g
                    select new { key=g.Key, 產品名稱 = g.Select(n => n.ProductName) };
            foreach(var x in q)
            {
                string s = $"{x.key}";
                TreeNode p = this.treeView1.Nodes.Add(s);
                foreach(var o in x.產品名稱)
                {
                    p.Nodes.Add(o.ToString());
                }
            }
        }

        //創立一個低中高的門檻
        private string MyUnitPrice(decimal n)
        {
            if (n < 50)
                return "$0-$50";
            else if (n <= 100)
                return "$51-$100";
            else
                return "$100~";
        }

        // Orders -  Group by 年
        private void button15_Click(object sender, EventArgs e)
        {
            var q = from o in this.nwDataSet11.Orders
                    group o by o.OrderDate.Year into g
                    select new { 年份=g.Key, };

            this.dataGridView1.DataSource = q.ToList();
        }

        // Orders -  Group by 年 / 月
        private void button10_Click(object sender, EventArgs e)
        {
            var q = from o in this.nwDataSet11.Orders
                    group o by o.OrderDate.Year into g
                    select new { 年份 = g.Key, };

            this.dataGridView1.DataSource = q.ToList();
            //---------------------------------------------------------------------------------------------------------------

            var q2 = from o in this.nwDataSet11.Orders
                     group o by o.OrderDate.Month into g
                     orderby g.Key
                     select new { 月份 = g.Key };

            this.dataGridView2.DataSource = q2.ToList();

        }

        NorthwindEntities1 dbContext = new NorthwindEntities1();
        //總銷售金額
        private void button2_Click(object sender, EventArgs e)
        {
            var q = from o in this.dbContext.Order_Details.AsEnumerable()
                    group o by o.Product.ProductName into g
                    select new { g.Key, 總銷售金額 = (g.Sum(o => o.UnitPrice * o.Quantity*1-(decimal)o.Discount)) };

            this.dataGridView1.DataSource = q.ToList();
        }

        //銷售最好的top 5業務員
        private void button1_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Order_Details.AsEnumerable()
                    group p by p.Product.SupplierID into g
                    select new { g.Key };
            this.dataGridView2.DataSource = q.ToList();
        }

        //NW 產品最高單價前 5 筆(包括類別名稱)
        private void button9_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products.AsEnumerable()
                    group p by p.Category.CategoryName into g
                    select new { 產品類別 = g.Key, 產品最高單價 = g.Max(p => p.UnitPrice) };

            this.dataGridView1.DataSource = q.Take(5).ToList();
        }

        //NW 產品有任何一筆單價大於300 ?
        private void button3_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwDataSet11.Products
                    group p by p.UnitPrice > 300 into g
                    select new { g.Key };
            this.dataGridView2.DataSource = q.ToList();

        }

        //每年 銷售分析 &&　plot劃分
        private void button34_Click(object sender, EventArgs e)
        {

        }

        //年 銷售成長率
        private void button5_Click(object sender, EventArgs e)
        {

        }

        //int[]  分三群 - No LINQ
        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            var q = from n in nums 
                    group n by MyNum(n) into g
                    select new { MyNum = g.Key, MyCount = g.Count(), MyGroup = g };
            this.dataGridView1.DataSource = q.ToList();
            //---------------------------------------------------------------------------------------------------------------
            foreach (var group in q)
            {
                string s = $"{group.MyNum} ( {group.MyCount} )";
                TreeNode x = this.treeView1.Nodes.Add(s);// group.MyNum.ToString());
                foreach (var item in group.MyGroup)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }
        private string MyNum(int n)
        {
            if (n < 5)
                return "小";
            else if (n <= 15)
                return "中";
            else
                return "大";
        }


    }
}
