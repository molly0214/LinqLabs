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
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(this.awDataSet11.ProductPhoto);
        }

        //All腳踏車伊
        private void button11_Click(object sender, EventArgs e)
        {
            //step1: define 來源物件 source object
            //step2: define query-from where select
            //step3: execute query-投射出來
            this.dataGridView1.Rows.Clear();
            var q = from n in this.awDataSet11.ProductPhoto
                    select n;
            this.dataGridView1.DataSource = q.ToList();
        }
         
        //區間腳踏車
        private void button3_Click(object sender, EventArgs e)
        {
            var q = from n in this.awDataSet11.ProductPhoto
                    where n.ModifiedDate >dateTimePicker1.Value && n.ModifiedDate<dateTimePicker2.Value
                    select n;
            this.dataGridView1.DataSource = q.ToList();
        }

        //某年腳踏車
        private void button5_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.Rows.Clear();
            if ("".Equals(comboBox3.Text))
                return;
            int i = int.Parse(comboBox3.Text);
            productPhotoTableAdapter1.Fill(awDataSet11.ProductPhoto);
            var q = from p in awDataSet11.ProductPhoto
                    where p.ModifiedDate.Year == i 
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        //某季腳踏車...有幾筆?
        private void button10_Click(object sender, EventArgs e)
        {
            //第一季 一月二月三月
            //第二季 四月五月六月
            //第三季 七月八月九月
            //第四季 十月十一月十二月
            //使用if else判斷

            if ("".Equals(comboBox3.Text) | "".Equals(comboBox2.Text))
                return;
            int i = int.Parse(comboBox3.Text);
            string s = "";
            int j = 0, l = 0;
            if (comboBox2.Text == "第一季")
            { s = "一"; j = 1; l = 3; }
            else if (comboBox2.Text == "第二季")
            { s = "二"; j = 4; l = 6; }
            else if (comboBox2.Text == "第三季")
            { s = "三"; j = 7; l = 9;}
            else if (comboBox2.Text == "第四季")
            { s = "四"; j = 10; l = 12;}

            var q = from p in awDataSet11.ProductPhoto
                    where p.ModifiedDate.Year ==i && p.ModifiedDate.Month >= j && p.ModifiedDate.Month <= l
                    orderby p.ModifiedDate
                    select p;
            dataGridView1.DataSource = q.ToList();
        }
    }
}
