using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Load : InnerObject
    {
        //FIELDS
        private double _innerPoint;
        private Structure _structure;
        private IsoPosition _position;

        //PROPERTIES
        public double VectorX { get; set; }
        public double VectorY { get; set; }
        public double Momentum { get; set; }
        public double InnerPoint
        {
            get { return _innerPoint; }
            set
            {
                _innerPoint = value;

                if (Structure != null)
                    ComputePosition();
            }
        }
        public override Structure Structure
        {
            get
            {
                return _structure;
            }
            set
            {
                _structure = value;

                if (Position != null)
                    ComputeInnerPoint();

                if (InnerPoint != 0)
                    ComputePosition();
            }
        }

        public SideViewPosition SideViewPosition
        {
            get
            {
                if (!(Structure is LinearStructure))
                    throw new IsoException("Support.SideViewPosition.Get: Structure is not LinearStructure.");

                LinearStructure s = (LinearStructure)Structure;
                if (s is Beam)
                {
                    return new SideViewPosition(s, InnerPoint, 0);
                }
                else
                    return new SideViewPosition(s, 0, InnerPoint);
            }
        }

        public IsoPosition Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (Structure != null)
                    ComputeInnerPoint();
            }
        }

        public override RectangleF SelectionBox
        {
            get
            {
                return new RectangleF(SideViewPosition.ToPointF().X - 10,
                                      SideViewPosition.ToPointF().Y - 10,
                                      20,
                                      20);
            }
        }


        //CONSTRUCTORS
        public Load()
        {

        }
        public Load(IsoPosition position, Structure structure) : base(structure)
        {
            Position = position;
            VectorX = 0.0;
            VectorY = 0.0;
            Momentum = 0.0;

            ComputeInnerPoint();
        }
        public Load(double innerPoint, Structure structure) : base(structure)
        {
            _innerPoint = innerPoint;
            VectorX = 0.0;
            VectorY = 0.0;
            Momentum = 0.0;

            ComputePosition();
        }
        public Load
        (IsoPosition position, Structure structure, double vectorX, double vectorY, double momentum) : base(structure)
        {
            Position = position;
            VectorX = vectorX;
            VectorY = vectorY;
            Momentum = momentum;

            ComputeInnerPoint();
        }
        public Load
        (double innerPoint, Structure structure, double vectorX, double vectorY, double momentum) : base(structure)
        {
            _innerPoint = innerPoint;
            VectorX = vectorX;
            VectorY = vectorY;
            Momentum = momentum;

            ComputePosition();
        }

        //METHODS
        private void ComputeInnerPoint()
        {
            double vectX;
            double vectY;
            double vectZ;

            LinearStructure s = (LinearStructure)Structure;
            if (s is Beam)
            {
                vectX = Position.X - s.P1.X;
                vectY = Position.Y - s.P1.Y;
                vectZ = Position.Z - s.P1.Z;
            }
            else
            {
                vectX = Position.X - s.LowerPoint.X;
                vectY = Position.Y - s.LowerPoint.Y;
                vectZ = Position.Z - s.LowerPoint.Z;
            }

            _innerPoint = Math.Sqrt(vectX * vectX + vectY * vectY + vectZ * vectZ);
        }
        private void ComputePosition()
        {
            if (!(Structure is LinearStructure))
                throw new IsoException("Support.InnerPoint.Get: Structure is not LinearStructure.");

            LinearStructure s = (LinearStructure)Structure;

            double lambda = InnerPoint / s.Length;

            double x = s.Equation.xo + s.Equation.a * lambda;
            double y = s.Equation.yo + s.Equation.b * lambda;
            double z = s.Equation.zo + s.Equation.c * lambda;

            Position = new IsoPosition(x, y, z);
        }

        public override void Draw(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);
            Color color = Color.Green;

            float shift = 5f;
            float lenght = 50f + (float)VectorY;

            if (VectorY <= 0)
            {
                Pen pen = new Pen(color, 2);
                PointF[] points = new PointF[]
                {
                new PointF(Position.ToPointF().X -shift , Position.ToPointF().Y - shift),
                Position.ToPointF(),
                new PointF(Position.ToPointF().X + shift, Position.ToPointF().Y - shift)
                };
                graphs.DrawLines(pen, points);
                graphs.DrawLine(pen, Position.ToPointF(), new PointF(Position.ToPointF().X, Position.ToPointF().Y - lenght));

                //Name
                string text = VectorY.ToString("0.0") + " kN";
                Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
                Brush brush = new SolidBrush(Color.Black);
                PointF point = new PointF(Position.ToPointF().X - 30, Position.ToPointF().Y - lenght - 20);
                graphs.DrawString(text, font, brush, point);
            }
            else if (VectorY > 0)
            {
                Pen pen = new Pen(color, 2);
                PointF[] points = new PointF[]
                {
                new PointF(Position.ToPointF().X - shift , Position.ToPointF().Y + shift),
                Position.ToPointF(),
                new PointF(Position.ToPointF().X + shift, Position.ToPointF().Y + shift)
                };
                graphs.DrawLines(pen, points);
                graphs.DrawLine(pen, Position.ToPointF(), new PointF(Position.ToPointF().X, Position.ToPointF().Y + lenght));

                //Name
                string text = VectorY.ToString("0.0") + " kN";
                Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
                Brush brush = new SolidBrush(Color.Black);
                PointF point = new PointF(Position.ToPointF().X - 30, Position.ToPointF().Y + lenght + 5);
                graphs.DrawString(text, font, brush, point);
            }
        }
        public override void DrawSideView(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            float shift = 5f;
            float lenght = 30f;

            Pen pen;
            if (Selected)
                pen = new Pen(Color.CadetBlue, 3);
            else
                pen = new Pen(Color.Green, 2);

            string text;
            Font font;
            Brush brush;
            PointF point;

            //point
            graphs.FillEllipse(new SolidBrush(Color.Black), new RectangleF(SideViewPosition.ToPointF().X - 2f, SideViewPosition.ToPointF().Y - 2f, 4f, 4f));

            //InnerPoint
            string value = InnerPoint.ToString("0.0") + " m";
            font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            brush = new SolidBrush(Color.Black);
            point = new PointF(SideViewPosition.ToPointF().X + 2, SideViewPosition.ToPointF().Y - 16);
            graphs.DrawString(value, font, brush, point);

            //VectorY
            if (VectorY < 0)
            {
                PointF[] points = new PointF[]
                {
                new PointF(SideViewPosition.ToPointF().X -shift , SideViewPosition.ToPointF().Y - shift),
                SideViewPosition.ToPointF(),
                new PointF(SideViewPosition.ToPointF().X + shift, SideViewPosition.ToPointF().Y - shift)
                };
                graphs.DrawLines(pen, points);
                graphs.DrawLine(pen, SideViewPosition.ToPointF(), new PointF(SideViewPosition.ToPointF().X, SideViewPosition.ToPointF().Y - lenght));

                //Name
                text = VectorY.ToString("0.00") + " kN";
                font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                brush = new SolidBrush(Color.Black);
                point = new PointF(SideViewPosition.ToPointF().X - 24, SideViewPosition.ToPointF().Y - lenght - 20);
                graphs.DrawString(text, font, brush, point);
            }
            else if (VectorY > 0)
            {
                PointF[] points = new PointF[]
                {
                    new PointF(SideViewPosition.ToPointF().X - shift , SideViewPosition.ToPointF().Y + shift),
                    SideViewPosition.ToPointF(),
                    new PointF(SideViewPosition.ToPointF().X + shift, SideViewPosition.ToPointF().Y + shift)
                };
                graphs.DrawLines(pen, points);
                graphs.DrawLine(pen, SideViewPosition.ToPointF(), new PointF(SideViewPosition.ToPointF().X, SideViewPosition.ToPointF().Y + lenght));

                text = VectorY.ToString("0.00") + " kN";
                font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                brush = new SolidBrush(Color.Black);
                point = new PointF(SideViewPosition.ToPointF().X - 20, SideViewPosition.ToPointF().Y + lenght + 5);
                graphs.DrawString(text, font, brush, point);
            }

            //VectorX
            if (VectorX > 0.05)
            {
                PointF[] points = new PointF[]
                {
                new PointF(SideViewPosition.ToPointF().X - shift , SideViewPosition.ToPointF().Y - shift),
                SideViewPosition.ToPointF(),
                new PointF(SideViewPosition.ToPointF().X - shift, SideViewPosition.ToPointF().Y + shift)
                };
                graphs.DrawLines(pen, points);
                graphs.DrawLine(pen, SideViewPosition.ToPointF(), new PointF(SideViewPosition.ToPointF().X - lenght, SideViewPosition.ToPointF().Y));

                //Name
                text = VectorX.ToString("0.00") + " kN";
                font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                brush = new SolidBrush(Color.Black);
                point = new PointF(SideViewPosition.ToPointF().X - lenght, SideViewPosition.ToPointF().Y);
                graphs.DrawString(text, font, brush, point);
            }
            else if (VectorX < -0.05)
            {
                PointF[] points = new PointF[]
{
                new PointF(SideViewPosition.ToPointF().X + shift , SideViewPosition.ToPointF().Y - shift),
                SideViewPosition.ToPointF(),
                new PointF(SideViewPosition.ToPointF().X + shift, SideViewPosition.ToPointF().Y + shift)
};
                graphs.DrawLines(pen, points);
                graphs.DrawLine(pen, SideViewPosition.ToPointF(), new PointF(SideViewPosition.ToPointF().X + lenght, SideViewPosition.ToPointF().Y));

                //Name
                text = VectorX.ToString("0.00") + " kN";
                font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                brush = new SolidBrush(Color.Black);
                point = new PointF(SideViewPosition.ToPointF().X + lenght - 50, SideViewPosition.ToPointF().Y);
                graphs.DrawString(text, font, brush, point);
            }

            //Momentum
            if (Momentum != 0)
            {
                //arc
                RectangleF rect = new RectangleF(SideViewPosition.ToPointF().X - 20, SideViewPosition.ToPointF().Y - 20, 40, 40);
                float startAngle = 210f;
                float sweepAngle = 120f;
                graphs.DrawArc(pen, rect, startAngle, sweepAngle);

                //text
                text = Momentum.ToString("0.00") + " kN.m";
                font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                brush = new SolidBrush(Color.Black);
                point = new PointF(SideViewPosition.ToPointF().X - 30, SideViewPosition.ToPointF().Y - 40);
                graphs.DrawString(text, font, brush, point);
            }

            if (Momentum > 0)
            {
                //arrow
                PointF[] points = new PointF[]
                {
                    new PointF(SideViewPosition.ToPointF().X - 16.8394f , SideViewPosition.ToPointF().Y - 17.0547f),
                    new PointF(SideViewPosition.ToPointF().X - 17.3205f , SideViewPosition.ToPointF().Y - 10f),
                    new PointF(SideViewPosition.ToPointF().X - 10.5267f , SideViewPosition.ToPointF().Y - 11.9607f)
                };

                graphs.DrawLines(pen, points);
            }
            else if (Momentum < 0)
            {
                //arrow
                PointF[] points = new PointF[]
                {
                    new PointF(SideViewPosition.ToPointF().X + 10.5267f , SideViewPosition.ToPointF().Y - 11.9607f),
                    new PointF(SideViewPosition.ToPointF().X + 17.3205f , SideViewPosition.ToPointF().Y - 10f),
                    new PointF(SideViewPosition.ToPointF().X + 16.8394f , SideViewPosition.ToPointF().Y - 17.0547f)
                };

                graphs.DrawLines(pen, points);
            }
        }

        public override int GetHashCode()
        {
            return "Load".GetHashCode() +
                   Position.GetHashCode() +
                   Structure.GetHashCode() +
                   (SourceStructure == null ? 0 : SourceStructure.GetHashCode());
        }
        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;
            if (this.GetType() != o.GetType()) return false;
            Load other = (Load)o;

            bool result;
            if (SourceStructure != null)
            {
                result = Position.Equals(other.Position) &&
                          Structure.Equals(other.Structure) &&
                          SourceStructure.Equals(other.SourceStructure);
            }
            else
            {
                result = Position.Equals(other.Position) &&
                         Structure.Equals(other.Structure);
            }

            return result;
        }
        public override string ToString()
        {
            return string.Format(
                    "Load [Position: {0}, VectorX: {1}, VectorY: {2}, Momentum: {3}]",
                    InnerPoint.ToString("0.00"),
                    VectorX.ToString("0.00"),
                    VectorY.ToString("0.00"),
                    Momentum.ToString("0.00")
                );
        }

        public string GetFileString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<f>");
            sb.Append(Position.GetFileString() + "<f>");
            sb.Append(VectorX.ToString() + "<f>");
            sb.Append(VectorY.ToString() + "<f>");
            sb.Append(Momentum.ToString() + "<f>");

            return sb.ToString();
        }
        public static Load LoadComponent(string fileString)
        {
            if (!fileString.StartsWith("<f>"))
                throw new ArgumentException("file string is not Load.");

            string s = fileString.Trim();

            string[] props = s.Split(new string[] { "<f>" }, StringSplitOptions.RemoveEmptyEntries);

            IsoPosition position = null;
            double vectx = 0;
            double vecty = 0;
            double mom = 0;
            try
            {
                position = IsoPosition.LoadComponent(props[0]);
                vectx = double.Parse(props[1]);
                vecty = double.Parse(props[2]);
                mom = double.Parse(props[3]);
            }
            catch
            {
                throw new ArgumentException("File string in the wrong format.");
            }

            Load load = new Load();
            load.Position = position;
            load.VectorX = vectx;
            load.VectorY = vecty;
            load.Momentum = mom;

            return load;
        }
    }
}
