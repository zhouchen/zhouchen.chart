namespace zhouchen.chart.test
{
    partial class mainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chartView = new zhouchen.chart.ChartView();
            this.SuspendLayout();
            // 
            // chartView
            // 
            this.chartView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartView.DRoute = 0D;
            this.chartView.DScale = 1D;
            this.chartView.ImgData = null;
            this.chartView.ImgPath = "";
            this.chartView.Location = new System.Drawing.Point(12, 12);
            this.chartView.Name = "chartView";
            this.chartView.Size = new System.Drawing.Size(750, 647);
            this.chartView.TabIndex = 0;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 671);
            this.Controls.Add(this.chartView);
            this.Name = "mainForm";
            this.Text = "主测试窗口";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ChartView chartView;
    }
}

