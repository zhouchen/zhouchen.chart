using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zhouchen.chart.chart;

namespace zhouchen.chart.test
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            chartView.ImgPath = @"test.jpg";

            ChartBase chartTemp = null;

            chartTemp = ChartFactory.CreateChart(ChartType.Chart_Straight, new Point[] { new Point(0, 0), new Point(300, 300) });
            chartView.LstChart.Add(chartTemp);
            chartTemp = ChartFactory.CreateChart(ChartType.Chart_Straight, new Point[] { new Point(300, 0), new Point(0, 300) });
            chartView.LstChart.Add(chartTemp);

            chartTemp = ChartFactory.CreateChart(ChartType.Chart_Rectangular, new Point[] { new Point(300, 300), new Point(600, 450) });
            chartView.LstChart.Add(chartTemp);
        }
    }
}
