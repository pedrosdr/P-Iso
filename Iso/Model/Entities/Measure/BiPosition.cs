using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public class BiPosition
    {
        //PROPERTIES
        public double X { get; set; }
        public double Y { get; set; }

        //CONSTRUCTORS
        public BiPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        //METHODS
        public PointF ToPointF()
        {
            double x = (X * ScreenProperties.Zoom + (ScreenProperties.ScreenSize.Width / 2)) + ScreenProperties.RightShift;
            double y = (-Y * ScreenProperties.Zoom + (ScreenProperties.ScreenSize.Height / 2)) + ScreenProperties.DownShift;

            return new PointF((float)x, (float)y);
        }

        public static BiPosition FromPointF(PointF point)
        {
            double x = (point.X - (ScreenProperties.Screen.Width / 2) - ScreenProperties.RightShift) / ScreenProperties.Zoom;
            double y = -(point.Y - (ScreenProperties.Screen.Height / 2) - ScreenProperties.DownShift) / ScreenProperties.Zoom;

            return new BiPosition(x, y);
        }
    }
}
