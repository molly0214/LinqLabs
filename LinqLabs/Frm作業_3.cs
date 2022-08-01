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
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
            students_scores = new List<Student1>()
                                         {
                                            new Student1{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student1{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student1{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student1{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student1{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student1{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                                          };
        }
        List<Student1> students_scores;

        public class Student1
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 數學成績 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
        }

        //搜尋班級成績 
        private string myScore(Student1 a)
        {
            string[] scoreGroup = { "不及格(0~59)", "待加強(60~69)", "佳(70~89)", "優良(90~100)" };
            if (a.Math < 60)
                return scoreGroup[0];
            else if (a.Math < 70)
                return scoreGroup[1];
            else if (a.Math < 90)
                return scoreGroup[2];
            else
                return scoreGroup[3];
        }

        private void button36_Click(object sender, EventArgs e)
        {
            string[] group = { "不及格(0~59)", "待加強(60~69)", "佳(70~89)", "優良(90~100)" };

            var q = from s in students_scores
                    group s by myScore(s) into g
                    select new { ScoreGroup = g.Key, Count = g.Count() };

            this.dataGridView1.DataSource = q.ToList();

            //chart
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "ScoreGroup";
            this.chart1.Series[0].YValueMembers = "Count";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].LegendText = students_scores[stud].Name + " 成績";
            this.chart1.Series[0].Color = Color.LightCoral;
        }

        int stud = 0;

        //每個學生個人成績
        private void button37_Click(object sender, EventArgs e)
        {
            int i = 0;
            chart1.DataSource = new List<Point> { new Point(1, students_scores[stud].Chi), new Point(2, students_scores[stud].Eng), new Point(3, students_scores[stud].Math) };
            //point  X軸 的屬性
            this.chart1.Series[0].XValueMember = "X";    
            //point  Y軸 屬性
            this.chart1.Series[0].YValueMembers = "Y";  
            
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].Color = Color.LightBlue;
            this.chart1.Series[0].BorderWidth = 3;
            this.chart1.Series[0].LegendText = students_scores[stud].Name + " 國英數";
            stud = (stud + 1)% students_scores.Count;
        }
    }
}
