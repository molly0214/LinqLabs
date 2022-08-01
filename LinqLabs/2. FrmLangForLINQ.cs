using LinqLabs;
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
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //100.200
            int n1, n2;
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + "," + n2);
            ClsMyUtility.Swap(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);

            //aaa.bbb
            string s1, s2;
            s1 = "aaa";
            s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);
            ClsMyUtility.Swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);

            MessageBox.Show(SystemInformation.ComputerName);
        }

        //泛用型別
        private void button7_Click(object sender, EventArgs e)
        {
            //100.200
            int n1, n2;
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + "," + n2);
            ClsMyUtility.SwapAnyType(ref n1, ref n2);
            //ClsMyUtility.SwapAnyType<int>(ref n1, ref n2); int可給可不給
            MessageBox.Show(n1 + "," + n2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.buttonX.Click += ButtonX_Click;

            //this.buttonX.Click += aaa;
            this.buttonX.Click += new EventHandler(aaa);

            //2.0匿名方法
            this.buttonX.Click += delegate (object sender1, EventArgs e1) { MessageBox.Show("匿名方法"); };
            //---------------------------------------------------------------------------------------------------------------
            //c#3.0匿名方法 簡潔版 Lamgba

            this.buttonX.Click += (object sender1, EventArgs e1) => { MessageBox.Show("c#3.0匿名方法 簡潔版 Lamgba"); };

        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonX click");
        }
        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }

        bool test(int n)
        {
            return n > 5;
        }


        //step1 create delegate class
        delegate bool Mydelegate(int n);


        //step1 create delegate class
        //step2 create delegate object
        //step3 call method
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = test(10);
            MessageBox.Show("result = " + result);

            //step1 create delegate class
            Mydelegate delegateObj = new Mydelegate(test);

            //step2 create delegate object
            result = delegateObj(2);

            //step3 call method
            MessageBox.Show("result = " + result);


            //2.0
            delegateObj = delegate (int a) { return a % 2 == 0; };

            result = delegateObj(10);

            MessageBox.Show("result = " + result);

            //3.0
            delegateObj = n => n % 2 == 0;
            result = delegateObj(100);
            MessageBox.Show("result = " + result);

        }

        List<int> MyWhere (int[] nums, Mydelegate delegateObj)
        {
            List<int>  list = new List<int>();
            
            foreach (int n in  nums)
            {
                if (delegateObj(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30};
            List<int> result =MyWhere(nums, test);

            List<int> odd = MyWhere(nums, n => n%2 == 1) ;
            List<int> even = MyWhere(nums, n => n % 2 == 0);

            foreach(int n in odd)
            {
                this.listBox1.Items.Add(n);
            }


            foreach (int n in even)
            {
                this.listBox2.Items.Add(n);
            }

        }

        IEnumerable<int>  Mylterator(int[] nums, Mydelegate delegateObj)
        {
            List<int> list = new List<int>();

            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    yield return(n);
                }
            }
        }


        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            IEnumerable<int> q = Mylterator(nums, n => n % 2 == 0);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

            IEnumerable<int> q = nums.Where<int>(n => n > 5);

            this.listBox1.Items.Clear();
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }

            //---------------------------------------------------------------------------------------------------------------
            string[] words = { "aaa", "bbb", "cccccccc", "ddddd" };
            IEnumerable<string> q2 = words.Where<string>(w => w.Length > 3);
            this.listBox2.Items.Clear();
            foreach (string s in q2)
            {
                this.listBox2.Items.Add(s);
            }
            //---------------------------------------------------------------------------------------------------------------
            //價錢大於30
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
            var q3 = nwDataSet11.Products.Where(p => p.UnitPrice > 30);
            this.dataGridView1.DataSource = q3.ToList();

        }

        private void button45_Click(object sender, EventArgs e)
        {
            //var 懶得寫(x)
            //---------------------------------------------------------------------------------------------------------------
            //var 型別難寫
            //var for 匿名型別

            int n = 100;
            var n1 = 200;
            var s = "abcde";

            MessageBox.Show(s.ToUpper());

            var p = new Point(1, 10);
            MessageBox.Show(p.X + "," + p.Y);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            MyPoint pt1 = new MyPoint();
            pt1.P1 = 100;   //set;  int w =  pt1.P1;  //get
            pt1.P2 = 200;   //set
            {
                List<MyPoint> list = new List<MyPoint>();
                list.Add(pt1);

                MyPoint pt2 = new MyPoint(99);
                MyPoint pt3 = new MyPoint(88, 88);
                list.Add(pt2);
                list.Add(pt3);

                list.Add(new MyPoint { P1 = 1, P2 = 2, Field1 = "aaaa", Field2 = "bbbb" });
                list.Add(new MyPoint { P1 = 1000 });
                list.Add(new MyPoint { P2 = 2000 });

                this.dataGridView1.DataSource = list;
                //---------------------------------------------------------------------------------------------------------------

                List<MyPoint> list2 = new List<MyPoint>()
                {
                    
                    new MyPoint { P1 = 1010 },
                    new MyPoint { P2 = 1011 },

                };
                this.dataGridView2.DataSource = list2;
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var pt1 = new { P1 = 3, P2 = 44 };
            var pt2 = new { P1 = 3, P2 = 44, P3 = 88 };
            var pt3 = new { P1 = 33, P2 = 434, P3 = 88 };

            this.listBox1.Items.Add(pt1.GetType());
            this.listBox1.Items.Add(pt2.GetType());
            this.listBox1.Items.Add(pt3.GetType());

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

            //var  q = from n in nums
            //                where n > 5
            //                select new { N = n, Square = n * n, Cube = n * n * n };

            var q = nums.Where(n => n > 5).Select(n => new { N = n, Square = n * n, Cube = n * n * n });

            this.dataGridView1.DataSource = q.ToList();

            //---------------------------------------------------------------------------------------------------------------

            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
            //var q2 = from p in this.nwDataSet1.Products
            //         where p.UnitPrice > 30
            //         select new 
            //         {
            //             ID = p.ProductID,
            //             產品名稱 = p.ProductName,
            //             p.UnitPrice,
            //             p.UnitsInStock,
            //             TotalPrice = p.UnitPrice * p.UnitsInStock
            //         };

            var q2 = this.nwDataSet11.Products.Where(p => p.UnitPrice > 30).Select(p => new { p.ProductID, p.ProductName, p.UnitPrice, p.UnitsInStock, TotalPrice = p.UnitPrice * p.UnitsInStock });
            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string s = "aqazwsxedcrfvtgbyhnujmik,ol";
            int count = s.WordCount();
            MessageBox.Show("字數為" + count);
            //---------------------------------------------------------------------------------------------------------------
            string s1 = "aqazwsxedcrfvtgbyhnujmik,ol";
            count = s1.WordCount();
            char ch = s1.Chars(3);
            MessageBox.Show("ch=" + ch);
        }
    }
}
public static class MyStringExtend
{
    public static int WordCount(this string s)
    {
        return s.Length;
    }
    public static char Chars(this string s, int index)
    {
        return s[index];
    }
}

class MyPoint
{
    public MyPoint() {    }
    public MyPoint(int p1)
    {
        this.P1 = p1;
    }
    public MyPoint(int p1, int p2)
    {
        this.P1 = p1;
        this.P2 = p2;
    }
    public MyPoint(int p1, string field1) {  }

    public string Field1 = "xxxx", Field2 = "yyyyy";

    private int m_p1;
    public int P1
    {
        get
        {
            //logic .....
            return m_p1;
        }
        set
        {
            //logic .....
            m_p1 = value;
        }
    }
    public int P2 { get; set; }

}

