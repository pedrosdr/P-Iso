using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public static class AxisConverter
    {
        public static BiPosition ToBiPosition(AxisType axis, double value)
        {
            double x = 0;
            double y = 0;

            double angle = ScreenProperties.Rotation;

            if (axis == AxisType.X)
            {
                if (angle < (Math.PI / 6))
                {
                    x = Math.Sin(-2 * angle + (Math.PI / 3));
                    y = Math.Sin(-2 * angle - (Math.PI / 6));
                }
                else if (angle < (Math.PI / 2))
                {
                    x = Math.Sin(-angle + (Math.PI / 6));
                    y = Math.Sin(angle - (Math.PI * 2 / 3));
                }
                else if (angle < (5 * Math.PI / 6))
                {
                    x = Math.Sin(-0.5 * angle - (Math.PI / 12));
                    y = Math.Sin(0.5 * angle - (5 * Math.PI / 12));
                }
                else if (angle < Math.PI)
                {
                    x = Math.Sin(angle - (4 * Math.PI / 3));
                    y = Math.Sin(angle - (5 * Math.PI / 6));
                }
                else if (angle < (7 * Math.PI / 6))
                {
                    x = Math.Sin(2 * angle - (7 * Math.PI / 3));
                    y = Math.Sin(2 * angle - (11 * Math.PI / 6));
                }
                else if (angle < (3 * Math.PI / 2))
                {
                    x = Math.Sin(angle - (7 * Math.PI / 6));
                    y = Math.Sin(-angle + (5 * Math.PI / 3));
                }
                else if (angle < (11 * Math.PI / 6))
                {
                    x = Math.Sin(0.5 * angle - (5 * Math.PI / 12));
                    y = Math.Sin(-0.5 * angle + (11 * Math.PI / 12));
                }
                else if (angle <= (2 * Math.PI))
                {
                    x = Math.Sin(-angle + (7 * Math.PI / 3));
                    y = Math.Sin(-angle + (11 * Math.PI / 6));
                }
            }
            else if (axis == AxisType.Y)
            {

                if (angle < (Math.PI / 6))
                {
                    x = Math.Sin(angle + (Math.PI / 3));
                    y = Math.Sin(-angle + (Math.PI / 6));
                }
                else if (angle < (Math.PI / 2))
                {
                    x = Math.Sin(-0.5 * angle + (Math.PI * 7 / 12));
                    y = Math.Sin(-0.5 * angle + (Math.PI / 12));
                }
                else if (angle < (5 * Math.PI / 6))
                {
                    x = Math.Sin(-angle + (5 * Math.PI / 6));
                    y = Math.Sin(-angle + (Math.PI / 3));
                }
                else if (angle < Math.PI)
                {
                    x = Math.Sin(-2 * angle + (5 * Math.PI / 3));
                    y = Math.Sin(2 * angle - (13 * Math.PI / 6));
                }
                else if (angle < (7 * Math.PI / 6))
                {
                    x = Math.Sin(-angle + (2 * Math.PI / 3));
                    y = Math.Sin(angle - (7 * Math.PI / 6));
                }
                else if (angle < (3 * Math.PI / 2))
                {
                    x = Math.Sin(0.5 * angle - (13 * Math.PI / 12));
                    y = Math.Sin(0.5 * angle - (7 * Math.PI / 12));
                }
                else if (angle < (11 * Math.PI / 6))
                {
                    x = Math.Sin(angle - (11 * Math.PI / 6));
                    y = Math.Sin(angle - (4 * Math.PI / 3));
                }
                else if (angle <= (2 * Math.PI))
                {
                    x = Math.Sin(2 * angle - (11 * Math.PI / 3));
                    y = Math.Sin(-2 * angle + (25 * Math.PI / 6));
                }
            }
            else
            {
                x = 0;
                y = 1;
            }

            return new BiPosition(value * x, value * y);
        }

        public static IsoPosition BeamPointFromBiPosition(BiPosition p, double z)
        {
            double angle = ScreenProperties.Rotation;

            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double x3 = 0;
            double y3 = 1;

            if (angle < (Math.PI / 6))
            {
                x1 = Math.Sin(-2 * angle + (Math.PI / 3));
                y1 = Math.Sin(-2 * angle - (Math.PI / 6));
            }
            else if (angle < (Math.PI / 2))
            {
                x1 = Math.Sin(-angle + (Math.PI / 6));
                y1 = Math.Sin(angle - (Math.PI * 2 / 3));
            }
            else if (angle < (5 * Math.PI / 6))
            {
                x1 = Math.Sin(-0.5 * angle - (Math.PI / 12));
                y1 = Math.Sin(0.5 * angle - (5 * Math.PI / 12));
            }
            else if (angle < Math.PI)
            {
                x1 = Math.Sin(angle - (4 * Math.PI / 3));
                y1 = Math.Sin(angle - (5 * Math.PI / 6));
            }
            else if (angle < (7 * Math.PI / 6))
            {
                x1 = Math.Sin(2 * angle - (7 * Math.PI / 3));
                y1 = Math.Sin(2 * angle - (11 * Math.PI / 6));
            }
            else if (angle < (3 * Math.PI / 2))
            {
                x1 = Math.Sin(angle - (7 * Math.PI / 6));
                y1 = Math.Sin(-angle + (5 * Math.PI / 3));
            }
            else if (angle < (11 * Math.PI / 6))
            {
                x1 = Math.Sin(0.5 * angle - (5 * Math.PI / 12));
                y1 = Math.Sin(-0.5 * angle + (11 * Math.PI / 12));
            }
            else if (angle <= (2 * Math.PI))
            {
                x1 = Math.Sin(-angle + (7 * Math.PI / 3));
                y1 = Math.Sin(-angle + (11 * Math.PI / 6));
            }

            if (angle < (Math.PI / 6))
            {
                x2 = Math.Sin(angle + (Math.PI / 3));
                y2 = Math.Sin(-angle + (Math.PI / 6));
            }
            else if (angle < (Math.PI / 2))
            {
                x2 = Math.Sin(-0.5 * angle + (Math.PI * 7 / 12));
                y2 = Math.Sin(-0.5 * angle + (Math.PI / 12));
            }
            else if (angle < (5 * Math.PI / 6))
            {
                x2 = Math.Sin(-angle + (5 * Math.PI / 6));
                y2 = Math.Sin(-angle + (Math.PI / 3));
            }
            else if (angle < Math.PI)
            {
                x2 = Math.Sin(-2 * angle + (5 * Math.PI / 3));
                y2 = Math.Sin(2 * angle - (13 * Math.PI / 6));
            }
            else if (angle < (7 * Math.PI / 6))
            {
                x2 = Math.Sin(-angle + (2 * Math.PI / 3));
                y2 = Math.Sin(angle - (7 * Math.PI / 6));
            }
            else if (angle < (3 * Math.PI / 2))
            {
                x2 = Math.Sin(0.5 * angle - (13 * Math.PI / 12));
                y2 = Math.Sin(0.5 * angle - (7 * Math.PI / 12));
            }
            else if (angle < (11 * Math.PI / 6))
            {
                x2 = Math.Sin(angle - (11 * Math.PI / 6));
                y2 = Math.Sin(angle - (4 * Math.PI / 3));
            }
            else if (angle <= (2 * Math.PI))
            {
                x2 = Math.Sin(2 * angle - (11 * Math.PI / 3));
                y2 = Math.Sin(-2 * angle + (25 * Math.PI / 6));
            }

            double[,] mat = new double[,]
            {
                {x1, x2, p.X },
                {y1, y2, (p.Y - z - ScreenProperties.BaseShift.Z)}
            };

            double[] result = Calculus.TwoUnknownVariables(mat);

            IsoPosition position = new IsoPosition
                (
                    Math.Round(result[0], 2) + ScreenProperties.BaseShift.X,
                    Math.Round(result[1], 2) + ScreenProperties.BaseShift.Y,
                    z + ScreenProperties.BaseShift.Z
                );

            return position;
        }

        public static IsoPosition ColumnPointFromBiPosition(BiPosition p, double x, double y)
        {
            double angle = ScreenProperties.Rotation;

            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double x3 = 0;
            double y3 = 1;

            if (angle < (Math.PI / 6))
            {
                x1 = Math.Sin(-2 * angle + (Math.PI / 3));
                y1 = Math.Sin(-2 * angle - (Math.PI / 6));
            }
            else if (angle < (Math.PI / 2))
            {
                x1 = Math.Sin(-angle + (Math.PI / 6));
                y1 = Math.Sin(angle - (Math.PI * 2 / 3));
            }
            else if (angle < (5 * Math.PI / 6))
            {
                x1 = Math.Sin(-0.5 * angle - (Math.PI / 12));
                y1 = Math.Sin(0.5 * angle - (5 * Math.PI / 12));
            }
            else if (angle < Math.PI)
            {
                x1 = Math.Sin(angle - (4 * Math.PI / 3));
                y1 = Math.Sin(angle - (5 * Math.PI / 6));
            }
            else if (angle < (7 * Math.PI / 6))
            {
                x1 = Math.Sin(2 * angle - (7 * Math.PI / 3));
                y1 = Math.Sin(2 * angle - (11 * Math.PI / 6));
            }
            else if (angle < (3 * Math.PI / 2))
            {
                x1 = Math.Sin(angle - (7 * Math.PI / 6));
                y1 = Math.Sin(-angle + (5 * Math.PI / 3));
            }
            else if (angle < (11 * Math.PI / 6))
            {
                x1 = Math.Sin(0.5 * angle - (5 * Math.PI / 12));
                y1 = Math.Sin(-0.5 * angle + (11 * Math.PI / 12));
            }
            else if (angle <= (2 * Math.PI))
            {
                x1 = Math.Sin(-angle + (7 * Math.PI / 3));
                y1 = Math.Sin(-angle + (11 * Math.PI / 6));
            }

            if (angle < (Math.PI / 6))
            {
                x2 = Math.Sin(angle + (Math.PI / 3));
                y2 = Math.Sin(-angle + (Math.PI / 6));
            }
            else if (angle < (Math.PI / 2))
            {
                x2 = Math.Sin(-0.5 * angle + (Math.PI * 7 / 12));
                y2 = Math.Sin(-0.5 * angle + (Math.PI / 12));
            }
            else if (angle < (5 * Math.PI / 6))
            {
                x2 = Math.Sin(-angle + (5 * Math.PI / 6));
                y2 = Math.Sin(-angle + (Math.PI / 3));
            }
            else if (angle < Math.PI)
            {
                x2 = Math.Sin(-2 * angle + (5 * Math.PI / 3));
                y2 = Math.Sin(2 * angle - (13 * Math.PI / 6));
            }
            else if (angle < (7 * Math.PI / 6))
            {
                x2 = Math.Sin(-angle + (2 * Math.PI / 3));
                y2 = Math.Sin(angle - (7 * Math.PI / 6));
            }
            else if (angle < (3 * Math.PI / 2))
            {
                x2 = Math.Sin(0.5 * angle - (13 * Math.PI / 12));
                y2 = Math.Sin(0.5 * angle - (7 * Math.PI / 12));
            }
            else if (angle < (11 * Math.PI / 6))
            {
                x2 = Math.Sin(angle - (11 * Math.PI / 6));
                y2 = Math.Sin(angle - (4 * Math.PI / 3));
            }
            else if (angle <= (2 * Math.PI))
            {
                x2 = Math.Sin(2 * angle - (11 * Math.PI / 3));
                y2 = Math.Sin(-2 * angle + (25 * Math.PI / 6));
            }

            double bsx = ScreenProperties.BaseShift.X;
            double bsy = ScreenProperties.BaseShift.Y;
            double bsz = ScreenProperties.BaseShift.Z;

            double z = p.Y - (x - bsx) * y1 - (y - bsy) * y2 + bsz;

            return new IsoPosition(x, y, z + bsz);
        }
    }
}
