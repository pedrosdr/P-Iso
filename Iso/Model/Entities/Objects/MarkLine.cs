using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class MarkLine : IDrawable
    {
        public bool Selected { get; set; }
        
        public IsoPosition P1 { get; set; }
        public PointF P2 { get; set; }
        public Color Color { get; set; }
        public float LineWidth { get; set; }

        //CONSTRUCTORS
        public MarkLine(IsoPosition p1, PointF p2, Color color) : base()
        {
            P1 = p1;
            P2 = p2;
            Color = color;
            LineWidth = 6;
        }

        public MarkLine(IsoPosition p1, PointF p2, Color color, float lineWidth) : base()
        {
            P1 = p1;
            P2 = p2;
            Color = color;
            LineWidth = lineWidth;
        }

        public void Draw(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);
            graphs.DrawLine(new Pen(Color, LineWidth), P1.ToPointF(), P2);
        }

        public void DrawSideView(Bitmap backbuffer)
        {
        }
    }
}
