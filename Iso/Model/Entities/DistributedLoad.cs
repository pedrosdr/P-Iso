using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class DistributedLoad : InnerObject
    {
        //FIELDS
        private Structure _structure;
        //PROPERTIES
        public override Structure Structure
        {
            get
            {
                return _structure;
            }
            set
            {
                _structure = value;

                if (P1 != null && P2 != null)
                    ComputeInnerPoints();
                else
                    ComputePositions();
            }
        }
        public double Value { get; set; }
        public double InnerP1 { get; set; }
        public double InnerP2 { get; set; }
        public IsoPosition P1 { get; set; }
        public IsoPosition P2 { get; set; }
        public SideViewPosition SideViewP1
        {
            get
            {
                LinearStructure s = (LinearStructure)Structure;
                if (s is Beam)
                {
                    return new SideViewPosition(s, InnerP1, 0);
                }
                else
                    return new SideViewPosition(s, 0, InnerP1);
            }
        }
        public SideViewPosition SideViewP2
        {
            get
            {
                LinearStructure s = (LinearStructure)Structure;
                if (s is Beam)
                {
                    return new SideViewPosition(s, InnerP2, 0);
                }
                else
                    return new SideViewPosition(s, 0, InnerP2);
            }
        }
        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow(InnerP2 - InnerP1, 2));
            }
        }
        public Load ResultingForce
        {
            get
            {
                double vectY = Value * Length;
                double innerPosition = (InnerP1 + InnerP2) / 2;
                return new Load(innerPosition, Structure, 0, vectY, 0);
            }
        }

        public double Far
        {
            get
            {
                return Util.GetHigher(InnerP1, InnerP2);
            }
        }
        public double Near
        {
            get
            {
                return Util.GetLower(InnerP1, InnerP2);
            }
        }

        public override RectangleF SelectionBox
        {
            get
            {
                float x = (float)Util.GetLower(SideViewP1.ToPointF().X, SideViewP2.ToPointF().X);
                float y = (float)SideViewP2.ToPointF().Y - 45;
                float width = (float)(Util.GetHigher(SideViewP1.ToPointF().X, SideViewP2.ToPointF().X) -
                                      Util.GetLower(SideViewP1.ToPointF().X, SideViewP2.ToPointF().X));
                float height = (float)10;

                return new RectangleF(x, y, width, height);
            }
        }

        //CONSTRUCTORS
        public DistributedLoad()
        {
        }

        public DistributedLoad
        (LinearStructure structure, double value, double innerP1, double innerP2) : base(structure)
        {
            Value = value;
            InnerP1 = innerP1;
            InnerP2 = innerP2;
            ComputePositions();
        }

        public DistributedLoad
        (LinearStructure structure, double value, IsoPosition p1, IsoPosition p2) : base(structure)
        {
            Value = value;
            P1 = p1;
            P2 = p2;
            ComputeInnerPoints();
        }

        //METHODS
        private void ComputeInnerPoints()
        {
            LinearStructure s = Structure as LinearStructure;
            double vectX;
            double vectY;
            double vectZ;

            if (s is Beam)
            {
                vectX = P1.X - s.P1.X;
                vectY = P1.Y - s.P1.Y;
                vectZ = P1.Z - s.P1.Z;

                vectX = P2.X - s.P2.X;
                vectY = P2.Y - s.P2.Y;
                vectZ = P2.Z - s.P2.Z;
            }
            else
            {
                vectX = P1.X - s.LowerPoint.X;
                vectY = P1.Y - s.LowerPoint.Y;
                vectZ = P1.Z - s.LowerPoint.Z;

                vectX = P2.X - s.LowerPoint.X;
                vectY = P2.Y - s.LowerPoint.Y;
                vectZ = P2.Z - s.LowerPoint.Z;
            }

            InnerP1 = Math.Sqrt(vectX * vectX + vectY * vectY + vectZ * vectZ);
            InnerP2 = Math.Sqrt(vectX * vectX + vectY * vectY + vectZ * vectZ);
        }

        private void ComputePositions()
        {
            LinearStructure s = Structure as LinearStructure;

            double lambda = InnerP1 / s.Length;

            double x = s.Equation.xo + s.Equation.a * lambda;
            double y = s.Equation.yo + s.Equation.b * lambda;
            double z = s.Equation.zo + s.Equation.c * lambda;

            P1 = new IsoPosition(x, y, z);

            lambda = InnerP2 / s.Length;

            x = s.Equation.xo + s.Equation.a * lambda;
            y = s.Equation.yo + s.Equation.b * lambda;
            z = s.Equation.zo + s.Equation.c * lambda;

            P2 = new IsoPosition(x, y, z);
        }

        public override void Draw(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            PointF p1 = new PointF(P1.ToPointF().X, P1.ToPointF().Y - 40);
            PointF p2 = new PointF(P2.ToPointF().X, P2.ToPointF().Y - 40);
            graphs.DrawLine(new Pen(Color.Green, 2), p1, p2);

            p1 = P1.ToPointF();
            p2 = new PointF(P1.ToPointF().X, P1.ToPointF().Y - 40);
            graphs.DrawLine(new Pen(Color.Green, 2), p1, p2);

            p1 = P2.ToPointF();
            p2 = new PointF(P2.ToPointF().X, P2.ToPointF().Y - 40);
            graphs.DrawLine(new Pen(Color.Green, 2), p1, p2);
        }

        public override void DrawSideView(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            Pen pen;
            if (Selected)
                pen = new Pen(Color.CadetBlue, 2);
            else
                pen = new Pen(Color.MediumPurple, 1);

            //Body
            PointF p1 = new PointF(SideViewP1.ToPointF().X, SideViewP1.ToPointF().Y - 40);
            PointF p2 = new PointF(SideViewP2.ToPointF().X, SideViewP2.ToPointF().Y - 40);
            graphs.DrawLine(pen, p1, p2);

            p1 = new PointF(SideViewP1.ToPointF().X, SideViewP1.ToPointF().Y - 15);
            p2 = new PointF(SideViewP1.ToPointF().X, SideViewP1.ToPointF().Y - 40);
            graphs.DrawLine(pen, p1, p2);

            graphs.DrawLines(pen,
                             new PointF[]
                             {
                                 new PointF(SideViewP1.ToPointF().X - 3, SideViewP1.ToPointF().Y - 20),
                                 new PointF(SideViewP1.ToPointF().X, SideViewP1.ToPointF().Y - 15),
                                 new PointF(SideViewP1.ToPointF().X + 3, SideViewP1.ToPointF().Y - 20)
                             });


            p1 = new PointF(SideViewP2.ToPointF().X, SideViewP2.ToPointF().Y - 15);
            p2 = new PointF(SideViewP2.ToPointF().X, SideViewP2.ToPointF().Y - 40);
            graphs.DrawLine(pen, p1, p2);

            graphs.DrawLines(pen,
                             new PointF[]
                             {
                                 new PointF(SideViewP2.ToPointF().X - 3, SideViewP1.ToPointF().Y - 20),
                                 new PointF(SideViewP2.ToPointF().X, SideViewP1.ToPointF().Y - 15),
                                 new PointF(SideViewP2.ToPointF().X + 3, SideViewP1.ToPointF().Y - 20)
                             });

            for (float i = SideViewP1.ToPointF().X; i < SideViewP2.ToPointF().X; i += 25f)
            {
                p1 = new PointF(i, SideViewP1.ToPointF().Y - 15);
                p2 = new PointF(i, SideViewP1.ToPointF().Y - 40);
                graphs.DrawLine(pen, p1, p2);

                graphs.DrawLines(pen,
                             new PointF[]
                             {
                                 new PointF(i - 3, SideViewP1.ToPointF().Y - 20),
                                 new PointF(i, SideViewP1.ToPointF().Y - 15),
                                 new PointF(i + 3, SideViewP1.ToPointF().Y - 20)
                             });
            }

            //Value
            string text = Value.ToString("0.00") + " kN/m";
            Font font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            float x = (float)Util.GetHigher(SideViewP1.ToPointF().X, SideViewP2.ToPointF().X);
            PointF point = new PointF(x - 60, SideViewP1.ToPointF().Y - 55);
            graphs.DrawString(text, font, brush, point);
        }

        public string GetFileString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<d>");
            sb.Append(InnerP1.ToString() + "<d>");
            sb.Append(InnerP2.ToString() + "<d>");
            sb.Append(Value.ToString() + "<d>");

            return sb.ToString();
        }

        public static DistributedLoad LoadComponent(string fileString)
        {
            if (!fileString.StartsWith("<d>"))
                throw new ArgumentException("file string is not DistributedLoad.");

            string s = fileString.Trim();

            string[] props = s.Split(new string[] { "<d>" }, StringSplitOptions.RemoveEmptyEntries);

            double p1;
            double p2;
            double value;
            try
            {
                p1 = double.Parse(props[0]);
                p2 = double.Parse(props[1]);
                value = double.Parse(props[2]);
            }
            catch
            {
                throw new ArgumentException("File string in the wrong format.");
            }

            DistributedLoad distLoad = new DistributedLoad();
            distLoad.InnerP1 = p1;
            distLoad.InnerP2 = p2;
            distLoad.Value = value;

            return distLoad;
        }
    }
}
