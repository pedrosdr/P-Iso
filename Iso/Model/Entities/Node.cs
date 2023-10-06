using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Node : IDrawable
    {
        //FIELDS
        private bool _hovered;

        //PROPERTIES
        public bool Selected { get; set; }
        public IsoPosition Position { get; set; }
        public NodeType Type { get; set; }

        public bool Hovered
        {
            get { return _hovered; }
            set { _hovered = value; }
        }

        //CONSTRUCTORS
        public Node(IsoPosition position)
        {
            Position = position;

            Type = NodeType.Endpoint;
        }

        //METHODS
        public  void Draw(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            //Body
            float x;
            float y;

            if (Hovered)
            {
                x = (float)(Position.ToPointF().X - 4);
                y = (float)(Position.ToPointF().Y - 4);

                Pen pen = new Pen(Color.Black, 3);
                graphs.DrawRectangle(pen, x, y, 8f, 8f);
            }
            else if (Type == NodeType.Origin)
            {
                x = (float)(Position.ToPointF().X - 3);
                y = (float)(Position.ToPointF().Y - 3);

                graphs = Graphics.FromImage(backbuffer);
                Pen pen = new Pen(Color.Black, 2);
                graphs.DrawEllipse(pen, x, y, 6f, 6f);
            }

            ////Coordinate
            //string text = Position.ToString();
            //Font font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            //Brush brush = new SolidBrush(Color.Black);
            //IsoPosition point = new IsoPosition(Position.X + 0.1, Position.Y + 0.1, Position.Z + 0.1);
            //graphs.DrawString(text, font, brush, point.ToPointF());
        }

        public  void DrawSideView(Bitmap backbuffer)
        {
            //Not Implemented
        }

        public override int GetHashCode()
        {
            return "Node".GetHashCode() + Position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            Node other = (Node)obj;
            return Position.Equals(other.Position);
        }
    }
}
