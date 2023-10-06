using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class ApoioMovel : Support
    {
        //FIELDS
        private Load _reaction;

        //PROPERTIES
        public override Load Reaction
        {
            get { return _reaction; }
            set
            {
                value.VectorX = 0.0;
                _reaction = value;
            }
        }

        //CONSTRUCTORS
        public ApoioMovel(IsoPosition position, Structure structure) : base(position, structure)
        {
        }

        public ApoioMovel(IsoPosition position, Structure structure, double verticalReaction) : base(position, structure)
        {
            _reaction = new Load(position, Structure, 0.0, verticalReaction, 0);
        }

        public override void Draw(Bitmap backbuffer)
        {
            float x1 = (float)(Position.ToPointF().X);
            float y1 = (float)(Position.ToPointF().Y - 5);

            float x2 = (float)(Position.ToPointF().X);
            float y2 = (float)(Position.ToPointF().Y + 5);

            Graphics graphs = Graphics.FromImage(backbuffer);
            Pen pen = new Pen(Color.Red, 3);
            graphs.DrawLine(pen, x1, y1, x2, y2);
        }

        public override void DrawSideView(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            Pen pen;
            if (Selected)
                pen = new Pen(Color.CadetBlue, 2);
            else
                pen = new Pen(Color.OrangeRed, 1);

            //Body
            PointF[] lines = new PointF[]
            {
                SideViewPosition.ToPointF(),
                new PointF(SideViewPosition.ToPointF().X - 10, SideViewPosition.ToPointF().Y + 20),
                new PointF(SideViewPosition.ToPointF().X + 10, SideViewPosition.ToPointF().Y + 20),
                SideViewPosition.ToPointF()
            };
            graphs.DrawLines(pen, lines);

            PointF p1 = new PointF(SideViewPosition.ToPointF().X - 10, SideViewPosition.ToPointF().Y + 25);
            PointF p2 = new PointF(SideViewPosition.ToPointF().X + 10, SideViewPosition.ToPointF().Y + 25);
            graphs.DrawLine(pen, p1, p2);
        }
    }
}
