using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //            public interface IEnumerable<T>
            //    System.Collections.Generic 的成員

            //摘要:
            //公開支援指定類型集合上簡單反覆運算的列舉值。

            //類型參數:
            //T: 要列舉之物件的類型。

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

            foreach (int n in nums)
            {
                this.listBox1.Items.Add(n);
            }

            this.listBox1.Items.Add("------------------------");

            //c#compiler翻譯
            System.Collections.IEnumerator en = nums.GetEnumerator();

            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (int n in list)
            {
                this.listBox1.Items.Add(n);
            }

            this.listBox1.Items.Add("------------------------");

            //c#compiler翻譯
            int w = 100;
            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //step1: define 來源物件 source object
            //step2: define query-from where select
            //step3: execute query-投射出來


            //step1: define 來源物件 source object
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //step2: define query-from where select
            IEnumerable<int> q = from n in nums
                                              //where n % 2 == 0 /*找偶數*/
                                              //where n >= 5 , n =< 10;
                                              where n < 3 || n > 10
                                               select n;

            //step3: execute query --投射出來
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //step1: define 來源物件 source object
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //step2: define query-from where select
            IEnumerable<int> q = from n in nums
                                               where isEven(n)
                                               select n;
            //step3: execute query --投射出來
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        bool isEven(int n)
        {
            //if (n % 2 == 0)
            //    return true;
            //else
            //    return false;
            return n%2 ==0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //step1: define 來源物件 source object
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //step2: define query-from where select
            IEnumerable<Point> q = from n in nums
                                 where isEven(n)
                                 select  new Point (n, n*n);
            //step3: execute query -foreach-投射出來 
            foreach (Point n in q)
            {
                this.listBox1.Items.Add(n);
            }

            //-----------------------
            //step3: execute query-Toxxx()

            List<Point> list =q.ToList();
            this.dataGridView1.DataSource = list;

            //-----------------------

            this.chart1.DataSource = list;

            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].Color = Color.LightCoral;
        }

        //找出只有APPLE字體並且字大於五
        private void button1_Click(object sender, EventArgs e)
        {
            //step1
            string[] word = { "aaa", "aaaApple", "bbbApple", "cccApple", "dddApple" };

            //step2
            IEnumerable<string> q = from w in word
                                                   where w.Length > 5 && w.ToLower().Contains("apple")  /*w.ToLower().Contains("apple")  apple不分大小寫*/
                                                   orderby w descending  /*descending=desc排序由大到小*/
                                                   select w;
            //step3
            foreach (string w in q)
            {
                this.listBox1.Items.Add(w);
            }

            //
            //var q1 = word.Where(delegate).select(...)          
        }

        private void button49_Click(object sender, EventArgs e)
        {
            // #region 組件 System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            // C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.2\System.Core.dll
            // #endregion

            // using System.Collections;
            // using System.Collections.Generic;
            //namespace System.Linq
            //    {
            //
            // 摘要:
            ////     提供一組 static (Shared 在 Visual Basic 中) 方法來查詢物件實作 System.Collections.Generic.IEnumerable`1。
            //public static class Enumerable
            //{
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] word = { "aaa", "aaaApple", "bbbApple", "cccApple", "dddApple" };

        }

        //找出產品名字只有C開頭且價錢高於30
        private void button8_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
            var q = from p in this.nwDataSet11.Products
                        where  p.ProductName.StartsWith("C")  && p.UnitPrice > 30
                        select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        //找出1997年訂單
        private void button9_Click(object sender, EventArgs e)
        {
            //step1: define 來源物件 source object
            //step2: define query-from where select
            //step3: execute query-投射出來


            this.ordersTableAdapter1.Fill(this.nwDataSet11.Orders);
            var q = from p in this.nwDataSet11.Orders
                    where p.OrderDate.Year == 1997 
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
