namespace zhouchen.chart
{
    partial class ChartView
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ChartView";
            this.Size = new System.Drawing.Size(710, 559);
            this.Load += new System.EventHandler(this.ChartView_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChartView_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChartView_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChartView_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChartView_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ChartView_MouseWheel);
            this.Resize += new System.EventHandler(this.ChartView_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
