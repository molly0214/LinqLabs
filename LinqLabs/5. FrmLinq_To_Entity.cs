using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqLabs;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            dbContext.Database.Log = Console.WriteLine;
        }
        NorthwindEntities1 dbContext = new NorthwindEntities1();

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products
                    where p.UnitPrice > 30
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //1.找出第一筆 2.並秀出第一筆是甚麼
            this.dataGridView1.DataSource = this.dbContext.Categories.First().Products.ToList();
            MessageBox.Show(dbContext.Products.First().Category.CategoryName);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //使用orderby
            var q = from n in dbContext.Products
                    orderby n.UnitsInStock ascending, n.ProductID
                    select n;
            this.dataGridView1.DataSource = q.ToList();
            //---------------------------------------------------------------------------------------------------------------
            //結果一樣但方法不同  使用OrderByDescending  ThenBy
            var q2 = from n in dbContext.Products.OrderByDescending(p => p.UnitsInStock).ThenBy(n => n.ProductID)
                     select n;
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products.AsEnumerable()
                    group p by p.Category.CategoryName into g
                    select new { 產品單價 = g.Key, 平均單價 = $"{g.Average(p => p.UnitPrice):c2}" };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var q = from o in dbContext.Orders
                    group o by o.OrderDate.Value into g
                    select new { g.Key, 筆數 = g.Count() };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Product pro = new Product { ProductName = "123", Discontinued = false };
            this.dbContext.Products.Add(pro);
            dbContext.SaveChanges();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
