using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                                          };
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Clear();
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            this.dataGridView1.DataSource = files;
        }

        // 共幾個 學員成績 ?	
        private void button36_Click(object sender, EventArgs e)
        {
                int c = students_scores.Count();
                MessageBox.Show("共" + c + "個學員成績\n");
         }

        //     FileInfo[]   - 2019 Created - order 
        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from p in files
                    where p.CreationTime.Year == 2019
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        // All 訂單 
        private void button6_Click(object sender, EventArgs e)
        {
            //step1: define 來源物件 source object
            //step2: define query-from where select
            //step3: execute query-投射出來
             
            
            this.ordersTableAdapter1.Fill(this.nwDataSet11.Orders);
            var q = from p in this.nwDataSet11.Orders
                    //where p.Order
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        // 共幾個 學員 ?	
        private void button3_Click(object sender, EventArgs e)
        {
            //step1: define 來源物件 source object
            //step2: define query-from where select
            //step3: execute query-投射出來

            // 找出 後面兩個 的學員所有科目成績		
            var q2 = from s in students_scores
                     orderby s.Chi + s.Eng + s.Math
                     select s;
            this.dataGridView1.DataSource = q2.Take(2).ToList();
        }

        //     FileInfo[]   - 大檔案
        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from p in files
                    where p.Length >0
                    orderby p.Length descending
                    select p;

            this.dataGridView1.DataSource = q.ToList();
        }

        //     某年訂單 / 訂單明細
        private void button1_Click(object sender, EventArgs e)
        {
           
                
         }

        //找出前面三個的學員所有科目成績
        private void button5_Click(object sender, EventArgs e)
        {
            //step1: define 來源物件 source object
            //step2: define query-from where select
            //step3: execute query-投射出來
            var q1 = from s in students_scores
                     orderby s.Chi + s.Eng + s.Math descending
                     select s;
            this.dataGridView1.DataSource = q1.Take(3).ToList();
        }

        //找出 Name 'aaa','bbb','ccc' 的學成績
        private void button7_Click(object sender, EventArgs e)
        {
            var q3 = from s in students_scores
                     where s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc"
                     select s;
            this.dataGridView1.DataSource = q3.ToList();
        }

        //找出學員 'bbb' 的成績
        private void button8_Click(object sender, EventArgs e)
        {
            var q4 = from s in students_scores
                     where s.Name == "bbb"
                     select s;
            this.dataGridView1.DataSource = q4.ToList();
        }

        //找出除了 'bbb' 學員的學員的所有成績('bbb' 退學)
        private void button9_Click(object sender, EventArgs e)
        {
            var q5 = from s in students_scores
                     where s.Name != "bbb"
                     select s;
            this.dataGridView1.DataSource = q5.ToList();
        }

        //數學不及格... 是誰
        private void button10_Click(object sender, EventArgs e)
        {
            var q6 = from s in students_scores
                     where s.Math < 60
                     select s;
            this.dataGridView1.DataSource = q6.ToList();
        }
    }
}
