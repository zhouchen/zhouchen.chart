using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using zhouchen.chart.chart;

namespace zhouchen.chart
{
    public partial class ChartView: UserControl, INotifyPropertyChanged
    {
        #region 变量通知    
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region 变量定义
        // 缩放 该因子必须大于0
        private double _dScale = 1.0;
        public double DScale
        {
            get { return _dScale; }
            set
            {
                if (value <= 0.000001)
                {
                    return;
                }
                _dScale = value;
                NotifyPropertyChanged("DScale");
            }
        }

        // 旋转角度
        private double _dRoute = 0.0;
        public double DRoute
        {
            get { return _dRoute; }
            set
            {
                _dRoute = value; 
                NotifyPropertyChanged("DRoute");
            }

        }

        // 矩阵变换
        private Matrix _matrix = new Matrix();

        private Rectangle _rcImg = new Rectangle(0, 0, 0, 0);
        private Bitmap _bitmap = null;
        public Bitmap ImgData
        {
            set
            {
                _bitmap = value;
                if(_bitmap == null)
                {
                    _rcImg.Width = 0;
                    _rcImg.Height= 0;
                }
                else
                {
                    _rcImg.Width = _bitmap.Width;
                    _rcImg.Height = _bitmap.Height;
                    AdaptView();
                    TranslationCenter();

                }
                Refresh();
            }
            get { return _bitmap; }
        }

        private string strImgPath = string.Empty;
        public string ImgPath
        {
            set
            {
                try
                {
                    strImgPath = value;
                    if (_bitmap != null)
                    {
                        _bitmap.Dispose();
                    }
                    ImgData = (Bitmap)GetImage(strImgPath);
                }
                catch
                {
                    _bitmap = null;
                    strImgPath = string.Empty;
                    Refresh();
                };
            }
            get { return strImgPath; }
        }

        private Rectangle rcBgArea = new Rectangle();    // 图片背景区域
        private Brush _ImgBg = new SolidBrush(Color.FromArgb(0xA8, 0xA8, 0xA8));     // 图片颜色背景

        private bool _IsLeftDown = false;
        private Point _LastPt = new Point();

        public List<ChartBase> LstChart = new List<ChartBase>();

        #endregion


        public ChartView()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            ChartBase chartTemp = null;

            chartTemp = ChartFactory.CreateChart(ChartType.Chart_Straight, new Point[] { new Point(0,0),new Point(300,300) });
            LstChart.Add(chartTemp);
            chartTemp = ChartFactory.CreateChart(ChartType.Chart_Straight, new Point[] { new Point(300, 0), new Point(0, 300) });
            LstChart.Add(chartTemp);

            chartTemp = ChartFactory.CreateChart(ChartType.Chart_Rectangular, new Point[] { new Point(300, 300), new Point(600, 450) });
            LstChart.Add(chartTemp);
        }

        private Image GetImage(string filename)
        {
            Image img = null;
            using (Stream s = File.Open(filename, FileMode.Open))
            {
                img = Image.FromStream(s);
            }

            return img;
        }

        private void ChartView_Load(object sender, EventArgs e)
        {
            this.CalculationArea();
        }

        private void ChartView_Resize(object sender, EventArgs e)
        {
            this.CalculationArea();
        }

        private void CalculationArea()
        {
            // 控件大小
            int nW = this.Width;
            int nH = this.Height;


            // 图片区域
            rcBgArea.X = 0;
            rcBgArea.Y = 0;
            rcBgArea.Width = nW;
            rcBgArea.Height = nH;

        }

        #region 绘制控件内容
        private void ChartView_Paint(object sender, PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            // 双缓冲绘图
            Bitmap bmpChartView = new Bitmap(this.Width, this.Height);
            Graphics bmpChartView_g = Graphics.FromImage(bmpChartView);

            DrawView(bmpChartView_g);
            graph.DrawImage(bmpChartView, 0, 0);

            bmpChartView_g.Dispose();
            bmpChartView.Dispose();
        }

        // 绘图
        private void DrawView(Graphics graph)
        {
            DrawMainView(graph);
            DrawScrollV(graph);
            DrawScrollH(graph);
        }

        // 绘制图片展示区
        private void DrawMainView(Graphics graph)
        {
            // 填充背景
            graph.FillRectangle(_ImgBg, rcBgArea);
            if (_bitmap == null)
            {
                return;
            }

            Bitmap bitImg = new Bitmap(_rcImg.Width, _rcImg.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics bitImg_g = Graphics.FromImage(bitImg);
            bitImg_g.Transform = _matrix;
            bitImg_g.DrawImage(_bitmap, 0, 0);

            graph.DrawImage(bitImg, rcBgArea.Left, rcBgArea.Top);

            foreach(var chart in LstChart)
            {
                chart.DrawChart(graph, _matrix);
            }

            bitImg_g.Dispose();
            bitImg.Dispose();
        }

        // 绘制垂直滚动条
        private void DrawScrollV(Graphics graph)
        {
        }

        // 绘制水平滚动条
        private void DrawScrollH(Graphics graph)
        {
        }

        #endregion

        #region 鼠标操作

        private void ChartView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _IsLeftDown = true;

                Point[] points = new Point[] { e.Location };
                Matrix matrix_Invert = _matrix.Clone();
                matrix_Invert.Invert();
                matrix_Invert.TransformPoints(points);
                Console.WriteLine(points[0]);
                if (_rcImg.Contains(points[0]))
                {
                    _LastPt = points[0];
                }
            }
        }

        private void ChartView_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsLeftDown)
            {
                Point[] points = new Point[] { e.Location };
                Matrix matrix_Invert = _matrix.Clone();
                matrix_Invert.Invert();
                matrix_Invert.TransformPoints(points);
                _matrix.Translate((points[0].X - _LastPt.X), (points[0].Y - _LastPt.Y));
                Console.WriteLine(_matrix.OffsetX + "," + _matrix.OffsetY);
                Refresh();
            }
        }

        private void ChartView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _IsLeftDown = false;
            }
        }

        private void ChartView_MouseWheel(object sender, MouseEventArgs e)
        {
            Point[] points = new Point[] { e.Location };
            Matrix matrix_Invert = _matrix.Clone();
            matrix_Invert.Invert();
            matrix_Invert.TransformPoints(points);
            Console.WriteLine(points[0]);
            if (_rcImg.Contains(points[0]))
            {
                double step = 1.2;
                if (e.Delta < 0)
                {
                    step = 1.0 / 1.2;
                }
                DScale *= step;
                _matrix.Scale((float)step, (float)step);

                Point[] pointse = new Point[] { e.Location };
                matrix_Invert = _matrix.Clone();
                matrix_Invert.Invert();
                matrix_Invert.TransformPoints(pointse);
                _matrix.Translate((pointse[0].X - points[0].X), (pointse[0].Y - points[0].Y));

                Refresh();
            }
        }


        #endregion

        // 缩放图片到窗口能刚好展示完全
        public void AdaptView()
        {
            if (_bitmap == null)
            {
                return;
            }

            using (var graphPath = new GraphicsPath())
            {
                graphPath.AddRectangle(new Rectangle(0, 0, _rcImg.Width, _rcImg.Height));
                graphPath.Transform(_matrix);
                PointF[] pointFs = graphPath.PathPoints;
                float fxmin = pointFs[0].X;
                float fymin = pointFs[0].Y;
                float fxmax = pointFs[0].X;
                float fymax = pointFs[0].Y;

                foreach (var pt in pointFs)
                {
                    if (pt.X < fxmin)
                    {
                        fxmin = pt.X;
                    }
                    else if (pt.X > fxmax)
                    {
                        fxmax = pt.X;
                    }
                    if (pt.Y < fymin)
                    {
                        fymin = pt.Y;
                    }
                    else if (pt.Y > fymax)
                    {
                        fymax = pt.Y;
                    }
                }

                float fWidth = fxmax - fxmin;
                float fHeight = fymax - fymin;

                if (fWidth * rcBgArea.Height < fHeight * rcBgArea.Width)
                {
                    DScale = rcBgArea.Height / fHeight;
                }
                else
                {
                    DScale = rcBgArea.Width / fWidth;
                }
                _matrix.Scale((float)DScale, (float)DScale, MatrixOrder.Append);
            }
        }

        // 平移图片到窗口的中间
        public void TranslationCenter()
        {
            if(_bitmap == null)
            {
                return;
            }
            Matrix matrixinv = _matrix.Clone();
            matrixinv.Invert();

            Point[] ptViewCenter = new Point[] { new Point(rcBgArea.Left + rcBgArea.Width / 2, rcBgArea.Top + rcBgArea.Height / 2) };
            matrixinv.TransformPoints(ptViewCenter);

            _matrix.Translate(ptViewCenter[0].X - _rcImg.Width / 2, ptViewCenter[0].Y - _rcImg.Height / 2);
            this.Refresh();

        }


    }
}
