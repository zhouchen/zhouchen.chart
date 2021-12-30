using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zhouchen.chart.chart
{
    public class Rectangular : PlanarChart
    {
        public Rectangular() : base()
        {
            _chartType = ChartType.Chart_Rectangular;
        }

        public Rectangular(Point ptStart, Point ptEnd) : base(ptStart, ptEnd)
        {
            _chartType = ChartType.Chart_Rectangular;
        }
        public override void DrawChart(Graphics graph, Matrix matrix)
        {
            Point[] pts = (Point[])ptCorrs.Clone();
            matrix.TransformPoints(pts);
            Pen pen = new Pen(this._ColorLine, this._nLineWidth);

            Point[] ptTemp = new Point[]
            {
                pts[(int)CORR_PT.PT_LT],
                pts[(int)CORR_PT.PT_RT],
                pts[(int)CORR_PT.PT_RB],
                pts[(int)CORR_PT.PT_LB]
            };

            if(_IsFillColor)
            {
                graph.FillPolygon(new SolidBrush(this._FillColor), ptTemp);
            }

            graph.DrawPolygon(pen, ptTemp);

            base.DrawChart(graph, matrix);
        }
    }
}
