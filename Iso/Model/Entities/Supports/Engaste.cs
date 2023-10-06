using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Engaste : Support
    {
        //CONSTRUCTORS
        public Engaste(IsoPosition position, Structure structure) : base(position, structure)
        {
        }

        public Engaste(IsoPosition position, Structure structure, double verticalReaction, double horizontalReaction, double momentumReaction) : base(position, structure)
        {
            Reaction = new Load(position, Structure, horizontalReaction, verticalReaction, 0);
        }

        public override void Draw(Bitmap backbuffer)
        {
            float x = (float)(Position.ToPointF().X - 5);
            float y = (float)(Position.ToPointF().Y - 5);

            Graphics graphs = Graphics.FromImage(backbuffer);
            Pen pen = new Pen(Color.Red, 3);
            RectangleF rect = new RectangleF(x, y, 10f, 10f);
            graphs.DrawEllipse(pen, rect);
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
                new PointF(SideViewPosition.ToPointF().X - 3, SideViewPosition.ToPointF().Y + 10),
                new PointF(SideViewPosition.ToPointF().X + 3, SideViewPosition.ToPointF().Y + 10),
                SideViewPosition.ToPointF()
            };
            graphs.DrawLines(pen, lines);

            PointF p1 = new PointF(SideViewPosition.ToPointF().X - 10, SideViewPosition.ToPointF().Y + 10);
            PointF p2 = new PointF(SideViewPosition.ToPointF().X + 10, SideViewPosition.ToPointF().Y + 10);
            graphs.DrawLine(pen, p1, p2);

            int x = (int)(SideViewPosition.ToPointF().X - 10);
            while (x < SideViewPosition.ToPointF().X + 13)
            {
                p1 = new Point(x, (int)(SideViewPosition.ToPointF().Y + 10));
                p2 = new Point(x - 3, (int)(SideViewPosition.ToPointF().Y) + 15);
                graphs.DrawLine(pen, p1, p2);
                x += 3;
            }
        }
    }
}
