using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public class IsoPosition
    {
        //PROPERTIES
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        //CONSTRUCTORS
        public IsoPosition(double x, double y, double z)
        {
            X = Math.Round(x, 4);
            Y = Math.Round(y, 4);
            Z = Math.Round(z, 4);
        }

        //METHODS
        public BiPosition ToBiPosition()
        {
            double Xconvert = X - ScreenProperties.BaseShift.X;
            double Yconvert = Y - ScreenProperties.BaseShift.Y;
            double Zconvert = Z - ScreenProperties.BaseShift.Z;

            BiPosition axisX = AxisConverter.ToBiPosition(AxisType.X, Xconvert);
            BiPosition axisY = AxisConverter.ToBiPosition(AxisType.Y, Yconvert);
            BiPosition axisZ = AxisConverter.ToBiPosition(AxisType.Z, Zconvert);

            double x = axisX.X + axisY.X + axisZ.X;
            double y = axisX.Y + axisY.Y + axisZ.Y;

            return new BiPosition(x, y);
        }
        public PointF ToPointF()
        {
            return ToBiPosition().ToPointF();
        }
        public double ToInnerPoint(LinearStructure s)
        {
            return Math.Sqrt(Math.Pow(X - s.P1.X, 2) + Math.Pow(Y - s.P1.Y, 2) + Math.Pow(Z - s.P1.Z, 2));
        }

        public override int GetHashCode()
        {
            return
                "IsoPosition".GetHashCode() +
                Math.Round(X, 1).GetHashCode() +
                Math.Round(Y, 1).GetHashCode() +
                Math.Round(Z, 1).GetHashCode();
        }
        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;
            if (this.GetType() != o.GetType()) return false;
            IsoPosition other = (IsoPosition)o;

            return
                X + 0.05 >= other.X && X - 0.05 <= other.X &&
                Y + 0.05 >= other.Y && Y - 0.05 <= other.Y &&
                Z + 0.05 >= other.Z && Z - 0.05 <= other.Z;
        }
        public override string ToString()
        {
            return "(" + X.ToString() + " ; " + Y.ToString() + " ; " + Z.ToString() + ")";
        }

        public string GetFileString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(X.ToString() + "<p>");
            sb.Append(Y.ToString() + "<p>");
            sb.Append(Z.ToString() + "<p>");

            return sb.ToString();
        }
        public static IsoPosition LoadComponent(string fileString)
        {
            if (!fileString.StartsWith("<p>"))
                throw new ArgumentException("file string is not IsoPosition.");

            string s = fileString.Trim();

            string[] props = s.Split(new string[] { "<p>" }, StringSplitOptions.RemoveEmptyEntries);

            double x;
            double y;
            double z;
            try
            {
                x = double.Parse(props[0]);
                y = double.Parse(props[1]);
                z = double.Parse(props[2]);
            }
            catch
            {
                throw new ArgumentException("File string in the wrong format.");
            }

            return new IsoPosition(x, y, z);
        }
    }
}
