using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            ArrayList array = new ArrayList();
            array.Add(2);
            array.Add(4);
            array.Add(6);

            var q =  from n in array.Cast<int>()
                select new { N = n };
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
            var q = (from p in this.nwDataSet11.Products
                     orderby p.UnitPrice descending
                     select p).Take ( 5 );
            this.dataGridView1.DataSource = q.ToList();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

            listBox1.Items.Add("Sum = " + nums. Sum());
            listBox1.Items.Add("Max = " + nums.Max());
            listBox1.Items.Add("Min = " + nums.Min());
            listBox1.Items.Add("Average = " + nums.Average());
            //---------------------------------------------------------------------------------------------------------------
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);

             

            //listBox1.Items.Add()


        }
    }
}