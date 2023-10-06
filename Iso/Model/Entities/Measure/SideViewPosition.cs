using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public class SideViewPosition
    {
        //PROPERTIES
        public double X { get; set; }
        public double Y { get; set; }
        public LinearStructure Structure { get; set; }

        //CONSTRUCTORS
        public SideViewPosition(LinearStructure s, double x, double y)
        {
            X = x;
            Y = y;
            Structure = s;
        }

        //METHODS
        public PointF ToPointF()
        {

            double w = SideViewScreenProperties.Screen.Width;
            double h = SideViewScreenProperties.Screen.Height;
            double len = Structure.Length;
            double x;
            double y;

            if (Structure is Beam)
            {
                x = ((0.8 * w) / len) * X + 0.1 * w;
                y = -((0.8 * w) / len) * Y + (h / 2);
            }
            else
            {
                x = ((0.8 * h) / len) * X + (w / 2);
                y = -((0.4 * h) / len) * Y + 0.7 * h;

            }

            return new PointF((float)x, (float)y);
        }

        public static SideViewPosition FromIsoPosition(IsoPosition p, LinearStructure s)
        {
            if (!s.HasPoint(p))
                throw new IsoException("LinearStructure does not have the given IsoPosition");

            double innerPoint;
            SideViewPosition position;
            if (s is Beam)
            {
                innerPoint = Math.Sqrt(Math.Pow(s.P1.X - p.X, 2) + Math.Pow(s.P1.Y - p.Y, 2) + Math.Pow(s.P1.Z - p.Z, 2));
                position = new SideViewPosition(s, innerPoint, 0);
            }
            else
            {
                innerPoint = Math.Sqrt(Math.Pow(s.LowerPoint.X - p.X, 2) + Math.Pow(s.LowerPoint.Y - p.Y, 2) + Math.Pow(s.LowerPoint.Z - p.Z, 2));
                position = new SideViewPosition(s, 0, innerPoint);
            }

            return position;
        }
    }
}
