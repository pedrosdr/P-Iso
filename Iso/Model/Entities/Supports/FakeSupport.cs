using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class FakeSupport : IDrawable
    {
        public LinearStructure Structure { get; set; }
        public SideViewPosition SideViewPosition { get; set; }
        public bool Selected { get; set; }
       
        public FakeSupport(double innerPoint, LinearStructure structure)
        {
            Structure = structure;
            SideViewPosition = new SideViewPosition(structure, 0, innerPoint);
        }

        public void Draw(Bitmap backbuffer)
        {
            //ignored
        }

        public void DrawSideView(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            Pen pen;
            if (Selected)
                pen = new Pen(Color.CadetBlue, 2);
            else
                pen = new Pen(Color.OrangeRed, 1);

            //Body
            //triangle
            PointF[] lines = new PointF[]
            {
                SideViewPosition.ToPointF(),
                new PointF(SideViewPosition.ToPointF().X - 3, SideViewPosition.ToPointF().Y - 10),
                new PointF(SideViewPosition.ToPointF().X + 3, SideViewPosition.ToPointF().Y - 10),
                SideViewPosition.ToPointF()
            };
            graphs.DrawLines(pen, lines);

            //base line
            PointF p1 = new PointF(SideViewPosition.ToPointF().X - 10, SideViewPosition.ToPointF().Y - 10);
            PointF p2 = new PointF(SideViewPosition.ToPointF().X + 10, SideViewPosition.ToPointF().Y - 10);
            graphs.DrawLine(pen, p1, p2);

            //base hatch
            int x = (int)(SideViewPosition.ToPointF().X - 10);
            while (x < SideViewPosition.ToPointF().X + 13)
            {
                p1 = new Point(x, (int)(SideViewPosition.ToPointF().Y - 10));
                p2 = new Point(x - 3, (int)(SideViewPosition.ToPointF().Y) - 15);
                graphs.DrawLine(pen, p1, p2);
                x += 3;
            }
        }
    }
}
