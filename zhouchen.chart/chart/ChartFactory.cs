using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zhouchen.chart.chart
{
    public class ChartFactory
    {
        public static ChartBase CreateChart(ChartType type, Point[] PtArr)
        {
            ChartBase graph = null;
            switch(type)
            {
                case ChartType.Chart_Straight:
                    {
                        if(PtArr.Length>=2)
                        {
                            graph = new StraightLine(PtArr[0], PtArr[1]);
                        }
                        else
                        {
                            throw new ArgumentException("创建直线,参数点低于2个坐标点");
                        }                       
                    }
                    break;
                case ChartType.Chart_Rectangular:
                    {
                        if(PtArr.Length >= 2)
                        {
                            graph = new Rectangular(PtArr[0], PtArr[1]);
                        }
                        else
                        {
                            throw new ArgumentException("创建矩形,参数点低于2个坐标点");
                        }  
                    }
                    break;
                default:
                    break;
            }
            return graph;
        }
    }
}
