using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public class Util
    {
        public static double GetHigher(double a, double b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static double GetLower(double a, double b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static bool IsBetween(double number, double limit1, double limit2)
        {
            return (number > limit1 && number < limit2) ||
                   (number < limit1 && number > limit2);
        }

        public static bool PointIsInsideRectangle(PointF point, RectangleF rect)
        {
            return rect.X <= point.X && point.X <= rect.X + rect.Width &&
                   rect.Y <= point.Y && point.Y <= rect.Y + rect.Height;
        }
    }
}
