using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            chartView.ImgPath = @"D:\spc_photos\正式数据\CH7\CH7_E95197115_00_0_100_2.0_0.5_50_2852.1944_2112151140.jpg";
        }
    }
}
