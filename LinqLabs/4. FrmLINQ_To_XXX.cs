using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
            this.categoriesTableAdapter1.Fill(this.nwDataSet11.Categories);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //製作數字1-30 除2以後餘數會是1及0 呈現出來  並使用語法  "group by"
            //int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            //IEnumerable<IGrouping<int, int>> q = from n in nums
            //                                     group n by (n % 2 );
            //this.dataGridView1.DataSource = q.ToList();
            //---------------------------------------------------------------------------------------------------------------
            //製作數字1-30 除2以後餘數會是偶數及奇數呈現出來  
            //int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            //IEnumerable<IGrouping<string, int>> q = from n in nums
            //                                     group n by n % 2 ==0? "偶數":"奇數";
            //this.dataGridView1.DataSource = q.ToList();
            //---------------------------------------------------------------------------------------------------------------
            //製作數字1-30 除2以後餘數會是偶數及奇數呈現出來 並使用語法  "into"
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            var q = from n in nums
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyGroup = g };

            this.dataGridView1.DataSource = q.ToList();
            foreach (var group in q)
            {
                string s = $"{group.MyKey} ( {group.MyCount} )";
                TreeNode x = this.treeView1.Nodes.Add(s);// group.MyKey.ToString());

                foreach (var item in group.MyGroup)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            var q = from n in nums
                    group n by MyKey(n) into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyGroup = g };

            this.dataGridView1.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.MyKey} ( {group.MyCount} 個)";
                TreeNode x = this.treeView1.Nodes.Add(s);// group.MyKey.ToString());

                foreach (var item in group.MyGroup)
                {
                    x.Nodes.Add(item.ToString());
                }
                //---------------------------------------------------------------------------------------------------------------
                this.chart1.DataSource = q.ToList();
                this.chart1.Series[0].XValueMember = "MyKey";
                this.chart1.Series[0].YValueMembers = "MyCount";
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                this.chart1.Series[0].Color = Color.LightCoral;
            }
        }
        private  string MyKey(int n)
            {
                if (n < 5)
                    return "小";
                else if (n <= 15)
                    return "中";
                else
                    return "大";
            }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files

                    group f by f.Extension into g
                    orderby g.Count() descending
                    select new { g.Key, MyCount = g.Count()};
                    
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    let s = f.Extension
                    where f.Extension ==".exe"
                    select f;
            MessageBox.Show("總共exe檔有"+q.Count()+"個");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            int[] nums2 = { 1, 2, 3, 4, 5, 6, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

            //集合運算子 Distinct / Union / Intersect / Except
            var q = nums1.Intersect(nums2);
             q = nums2.Distinct();

            //數量詞作業 : Any / All / Contains
            bool result = nums1.Any(n => n > 100);
            result = nums1.All(n => n >1 );


            //切割運算子
            q = nums1.Take(2);
            //單一元素運算子 :  
            //First / Last / Single / ElementAt
            //FirstOrDefault / LastOrDefault / SingleOrDefault / ElementAtOrDefault
            int N = nums1.First();
            N = nums1.ElementAt(2);
            N = nums1.ElementAt(12);
            //產生作業 : Generation – Range / Repeat / Empty DefaultIfEmpty
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwDataSet11.Products
                    group p by p.CategoryID into g
                    orderby g.Key
                    select new { 產品名稱 = g.Key, 平均單價 = g.Average(p=>p.UnitPrice) };
            this.dataGridView1.DataSource = q.ToList();
            //---------------------------------------------------------------------------------------------------------------
            //使用join寫法
            var q2 = from c in this.nwDataSet11.Categories
                     join p in this.nwDataSet11.Products on c.CategoryID equals p.CategoryID
                     group p by c.CategoryName into g
                     orderby g.Key
                     select new { 產品名稱 = g.Key, 平均單價 = g.Average(p => p.UnitPrice) };

            this.dataGridView2.DataSource = q2.ToList();





        }
    }
    }


