using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zhouchen.chart.chart
{
    // 平面图类
    public class PlanarChart : ChartBase
    {
        #region 外接矩形参数
        protected int _x;
        public int x
        {
            get { return _x; }
            set
            {
                _x = value;
                NotifyPropertyChanged(nameof(x));
            }
        }

        private int _y;
        public int y
        {
            get { return _y; }
            set
            {
                _y = value;
                NotifyPropertyChanged(nameof(y));
            }
        }

        private int _width;
        public int width
        {
            get { return _width; }
            set
            {
                _width = value;
                NotifyPropertyChanged(nameof(width));
            }
        }

        private int _height;
        public int height
        {
            get { return _height; }
            set
            {
                _height = value;
                NotifyPropertyChanged(nameof(height));
            }
        }
        public int left => _x;
        public int top => _y;
        public int right => _x + _width;
        public int bottom => _y + _height;

        // 外接矩形
        public Rectangle rcCircumscribed => new Rectangle(_x, _y, _width, _height);

        public enum CORR_PT
        {
            PT_UNKNOW = -1,
            PT_BEGIN = 0,
            PT_LT = PT_BEGIN, PT_MT, PT_RT,
            PT_LM, PT_MM, PT_RM,
            PT_LB, PT_MB, PT_RB,
            PT_END, PT_CNT = PT_END
        }
        public Point[] ptCorrs = new Point[(int)CORR_PT.PT_CNT];

        #endregion

        // 是否填充颜色
        protected bool _IsFillColor = false;
        public bool IsFillColor
        {
            get { return _IsFillColor; }
            set
            {
                _IsFillColor = value;
                NotifyPropertyChanged("IsFillColor");
            }
        }

        // 填充颜色
        protected Color _FillColor = Color.FromArgb(255, 255, 0, 255);
        public Color FillColor
        {
            get { return _FillColor; }
            set
            {
                _FillColor = value;
                NotifyPropertyChanged("FillColor");
            }
        }

        public PlanarChart()
        {
            _x = 0;
            _y = 0;
            _width = 0;
            _height = 0;
            this.ReCountPt();
        }

        public PlanarChart(Point ptStart, Point ptEnd)
        {
            _x = Math.Min(ptStart.X, ptEnd.X);
            _y = Math.Min(ptStart.Y, ptEnd.Y);
            _width = Math.Abs(ptStart.X - ptEnd.X);
            _height = Math.Abs(ptStart.Y - ptEnd.Y);
            this.ReCountPt();
        }

        public PlanarChart(Point ptlocation, Size size)
        {
            _x = ptlocation.X;
            _y = ptlocation.Y;
            _width = size.Width;
            _height = size.Height;
            this.ReCountPt();
        }

        public PlanarChart(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            this.ReCountPt();
        }

        // 计算每个点的坐标
        public void ReCountPt()
        {
            // 左上
            ptCorrs[(int)CORR_PT.PT_LT].X = left;
            ptCorrs[(int)CORR_PT.PT_LT].Y = top;

            // 右下
            ptCorrs[(int)CORR_PT.PT_RB].X = right;
            ptCorrs[(int)CORR_PT.PT_RB].Y = bottom;

            // 右上
            ptCorrs[(int)CORR_PT.PT_RT].X = right;
            ptCorrs[(int)CORR_PT.PT_RT].Y = top;

            // 左下
            ptCorrs[(int)CORR_PT.PT_LB].X = left;
            ptCorrs[(int)CORR_PT.PT_LB].Y = bottom;

            // 中上
            ptCorrs[(int)CORR_PT.PT_MT].X = left + _width / 2;
            ptCorrs[(int)CORR_PT.PT_MT].Y = top;

            // 左中
            ptCorrs[(int)CORR_PT.PT_LM].X = left;
            ptCorrs[(int)CORR_PT.PT_LM].Y = top + _height / 2;

            // 中中
            ptCorrs[(int)CORR_PT.PT_MM].X = left + _width / 2;
            ptCorrs[(int)CORR_PT.PT_MM].Y = top + _height / 2;

            // 右中
            ptCorrs[(int)CORR_PT.PT_RM].X = right;
            ptCorrs[(int)CORR_PT.PT_RM].Y = top + _height / 2;

            // 中下
            ptCorrs[(int)CORR_PT.PT_MB].X = left + _width / 2;
            ptCorrs[(int)CORR_PT.PT_MB].Y = bottom;

        }

        public override void DrawChart(Graphics graph, Matrix matrix)
        {
            if(!IsSelect)
            {
                return;
            }

            Point[] pts = (Point[])ptCorrs.Clone();
            matrix.TransformPoints(pts);

            Pen pen_select_line = new Pen(COLOR_SELECT_LINE, 1);
            Brush brush_select_fill = new SolidBrush(COLOR_SELECT_FILL);

            graph.DrawPolygon(pen_select_line, new Point[] 
            {
                pts[(int)CORR_PT.PT_LT],
                pts[(int)CORR_PT.PT_RT],
                pts[(int)CORR_PT.PT_RB],
                pts[(int)CORR_PT.PT_LB]
            });

            Rectangle[] rcpt = new Rectangle[pts.Length];
            int nIndex = 0;
            foreach (Point pt in pts)
            {
                rcpt[nIndex++] = new Rectangle(pt.X - SPACE_HALF, pt.Y - SPACE_HALF, SPACE_FULL, SPACE_FULL);
            }

            nIndex = -1;
            foreach (Rectangle rc in rcpt)
            {
                nIndex++;
                if (4 == nIndex)
                {
                    continue;
                }
                graph.FillEllipse(brush_select_fill, rc);
                graph.DrawEllipse(pen_select_line, rc);
                
            }





        }

    }
}
