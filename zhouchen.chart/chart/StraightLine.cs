using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zhouchen.chart.chart
{
    // 直线类
    public class StraightLine : LinerChart
    {
        private Point _ptStart = new Point(0,0);
        private Point ptStart
        {
            get { return _ptStart; }
            set { 
                _ptStart = value;
                NotifyPropertyChanged("ptStart");
            }
        }

        private Point _ptEnd = new Point(0,0);
        public Point ptEnd
        {
            get { return _ptEnd; }
            set
            {
                _ptEnd = value;
                NotifyPropertyChanged("ptEnd");
            }
        }

        public StraightLine()
        {
            _chartType = ChartType.Chart_Straight;
        }

        public StraightLine(Point ptStart, Point ptEnd) : this()
        {
            _ptStart = ptStart;
            _ptEnd = ptEnd;
        }


        public override void DrawChart(Graphics graph, Matrix matrix)
        {
            Point[] pts = new Point[] { _ptStart, _ptEnd };
            matrix.TransformPoints(pts);
            Pen pen = new Pen(this._ColorLine, this._nLineWidth);

            graph.DrawLine(pen, pts[0], pts[1]);

            if (!IsSelect)
            {
                return;
            }

            Pen pen_select_line = new Pen(COLOR_SELECT_LINE, 1);
            Brush brush_select_fill = new SolidBrush(COLOR_SELECT_FILL);

            Rectangle[] rcpt = new Rectangle[pts.Length];
            int nIndex = 0;
            foreach (Point pt in pts)
            {
                rcpt[nIndex++] = new Rectangle(pt.X - SPACE_HALF, pt.Y - SPACE_HALF, SPACE_FULL, SPACE_FULL);
            }

            graph.DrawLine(pen_select_line, pts[0], pts[1]);
            foreach (Rectangle rc in rcpt)
            {
                graph.FillEllipse(brush_select_fill, rc);
                graph.DrawEllipse(pen_select_line, rc);
            }
        }
    }
}
