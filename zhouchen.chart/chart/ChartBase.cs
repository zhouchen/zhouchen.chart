using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zhouchen.chart.chart
{
    public enum ChartType
    {
        Chart_Unknow,       // 未知类型
        Chart_Straight,     // 直线
        Chart_Rectangular,  // 矩形
        Chart_Cnt,
    }

    public abstract class ChartBase: INotifyPropertyChanged
    {
        protected static int SPACE_HALF = 5;
        protected static int SPACE_FULL = 10;
        protected static Color COLOR_SELECT_LINE = Color.Gray;
        protected static Color COLOR_SELECT_FILL = Color.White;

        protected ChartType _chartType = ChartType.Chart_Unknow;
        public ChartType chartType
        {
            get { return _chartType; }
        }


        #region 变量通知
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region 变量
        protected uint _nLineWidth = 2;
        public uint LineWidth
        {
            get { return _nLineWidth; }
            set { 
                _nLineWidth = value;
                NotifyPropertyChanged("LineWidth");
            }
        }

        protected Color _ColorLine = Color.Black;
        public Color LineColor
        {
            get { return _ColorLine; }
            set { 
                _ColorLine = value;
                NotifyPropertyChanged("LineColor");
            }
        }

        // 旋转中心
        protected Point _ptCenter = new Point(0,0);
        public Point ptCenter
        {
            get { return _ptCenter; }
            set { _ptCenter = value;}
        }

        // 旋转矩阵
        protected Matrix _matrix = new Matrix();
        // 旋转角度
        protected double _dRoute = 0.0;
        public double Route
        {
            get { return _dRoute; }
            set { 
                _matrix.RotateAt((float)(value - _dRoute), _ptCenter);
                _dRoute = value;
                NotifyPropertyChanged("Route");
            }
        }

        protected bool _IsSelect = false;
        public bool IsSelect {
            get { return _IsSelect; } 
            set {
                _IsSelect = value;
                NotifyPropertyChanged("IsSelect");
            } 
        }
        #endregion

        public abstract void DrawChart(Graphics graph, Matrix matrix);

    }
}
