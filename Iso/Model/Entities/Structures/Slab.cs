using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Slab : Structure
    {
        //PROPERTIES
        public Line EdgeA { get; set; }
        public Line EdgeB { get; set; }
        public Line EdgeC { get; set; }
        public Line EdgeD { get; set; }

        //CONSTRUCTORS
        public Slab(IsoPosition p1, IsoPosition p2)
        {
            p2.Z = p1.Z;
            IsoPosition p3 = new IsoPosition(p2.X, p1.Y, p1.Z);
            IsoPosition p4 = new IsoPosition(p1.X, p2.Y, p1.Z);

            Points.Clear();

            Points.Add(p1);
            Points.Add(p3);
            Points.Add(p2);
            Points.Add(p4);

            EdgeA = new Line(p1, p3);
            EdgeB = new Line(p3, p2);
            EdgeC = new Line(p2, p4);
            EdgeD = new Line(p4, p1);
        }

        public override void Draw(Bitmap backbuffer)
        {
            Color borderColor;
            Color fillColor;

            if (Selected)
            {
                borderColor = Color.Green;
                fillColor = Color.FromArgb(120, Color.Green);
            }
            else
            {
                borderColor = Color.Green;
                fillColor = Color.FromArgb(80, Color.Green);
            }

            Pen pen = new Pen(borderColor, 1);
            Brush brush = new SolidBrush(fillColor);

            Graphics graphs = Graphics.FromImage(backbuffer);

            List<IsoPosition> points = new List<IsoPosition>();
            Points.ForEach(p => points.Add(p));
            points.Add(Points[0]);
            PointF[] pointfs = points.Select(p => p.ToPointF()).ToArray();

            graphs.DrawPolygon(pen, pointfs);
            graphs.FillPolygon(brush, pointfs);
        }
        public override void DrawSideView(Bitmap backbuffer)
        {
        }

        public bool MouseIn(PointF mousePosition)
        {
            double x = BiPosition.FromPointF(mousePosition).X;
            double y = BiPosition.FromPointF(mousePosition).Y;

            FirstDegreeEquation eqA = FirstDegreeEquation.FromValues(
                EdgeA.P1.ToBiPosition().X, EdgeA.P1.ToBiPosition().Y,
                EdgeA.P2.ToBiPosition().X, EdgeA.P2.ToBiPosition().Y
                );

            FirstDegreeEquation eqB = FirstDegreeEquation.FromValues(
                EdgeB.P1.ToBiPosition().X, EdgeB.P1.ToBiPosition().Y,
                EdgeB.P2.ToBiPosition().X, EdgeB.P2.ToBiPosition().Y
                );

            FirstDegreeEquation eqC = FirstDegreeEquation.FromValues(
                EdgeC.P1.ToBiPosition().X, EdgeC.P1.ToBiPosition().Y,
                EdgeC.P2.ToBiPosition().X, EdgeC.P2.ToBiPosition().Y
                );

            FirstDegreeEquation eqD = FirstDegreeEquation.FromValues(
                EdgeD.P1.ToBiPosition().X, EdgeD.P1.ToBiPosition().Y,
                EdgeD.P2.ToBiPosition().X, EdgeD.P2.ToBiPosition().Y
                );

            double Ax = eqA.GetX(y);
            double Bx = eqB.GetX(y);
            double Cx = eqC.GetX(y);
            double Dx = eqD.GetX(y);

            double Ay = eqA.GetX(x);
            double By = eqB.GetX(x);
            double Cy = eqC.GetX(x);
            double Dy = eqD.GetX(x);

            return (Util.IsBetween(x, Ax, Cx) && Util.IsBetween(x, Bx, Dx));

            //double x = mousePosition.X;
            //double y = mousePosition.Y;

            //FirstDegreeEquation eqA = FirstDegreeEquation.FromValues(
            //    EdgeA.P1.ToPointF().X, EdgeA.P1.ToPointF().Y,
            //    EdgeA.P2.ToPointF().X, EdgeA.P2.ToPointF().Y
            //    );

            //FirstDegreeEquation eqB = FirstDegreeEquation.FromValues(
            //    EdgeB.P1.ToPointF().X, EdgeB.P1.ToPointF().Y,
            //    EdgeB.P2.ToPointF().X, EdgeB.P2.ToPointF().Y
            //    );

            //FirstDegreeEquation eqC = FirstDegreeEquation.FromValues(
            //    EdgeC.P1.ToPointF().X, EdgeC.P1.ToPointF().Y,
            //    EdgeC.P2.ToPointF().X, EdgeC.P2.ToPointF().Y
            //    );

            //FirstDegreeEquation eqD = FirstDegreeEquation.FromValues(
            //    EdgeD.P1.ToPointF().X, EdgeD.P1.ToPointF().Y,
            //    EdgeD.P2.ToPointF().X, EdgeD.P2.ToPointF().Y
            //    );

            //double Ax = eqA.GetX(y);
            //double Bx = eqB.GetX(y);
            //double Cx = eqC.GetX(y);
            //double Dx = eqD.GetX(y);

            //double Ay = eqA.GetX(x);
            //double By = eqB.GetX(x);
            //double Cy = eqC.GetX(x);
            //double Dy = eqD.GetX(x);

            //return (Util.IsBetween(x, Ax, Cx) && Util.IsBetween(x, Bx, Dx)) ||
            //       (Util.IsBetween(y, Ay, Cy) && Util.IsBetween(y, By, Dy));
        }

        public override string GetFileString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<s>" + Id + "<s>");

            sb.Append(Points[0].GetFileString() + "<s>");
            sb.Append(Points[2].GetFileString() + "<s>");

            sb.Append("<s>");

            return sb.ToString();
        }

        public static Slab LoadComponent(string fileString)
        {
            if (!fileString.StartsWith("<s>"))
                throw new ArgumentException("File string is not Slab.");

            string s = fileString.Trim();

            string[] props = s.Split(new string[] { "<s>" }, StringSplitOptions.RemoveEmptyEntries);

            int id;
            IsoPosition p1;
            IsoPosition p2;

            try
            {
                id = int.Parse(props[0]);
                p1 = IsoPosition.LoadComponent(props[1]);
                p2 = IsoPosition.LoadComponent(props[2]);
            }
            catch
            {
                throw new ArgumentException("File string in the wrong format.");
            }

            Slab slab = new Slab(p1, p2);
            slab.Id = id;

            return slab;
        }
    }
}
