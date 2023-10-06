using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public class SideIcon : IDrawable
    {
        //FIELDS
        private Color _backColor = Color.FromArgb(20, 20, 255);
        //PROPERTIES
        public bool Selected { get; set; }
        public string Text { get; set; }
        public Point Position { get; set; }
        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        //CONSTRUCTORS
        public SideIcon(string text, Point position)
        {
            Text = text;
            Position = position;
        }

        //METHODS
        public void Draw(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            //Background
            int rectWidth = (int) (Text.ToCharArray().Length * 9);
            Rectangle rect = new Rectangle(Position.X, Position.Y, rectWidth, 30);
            Brush brush = new SolidBrush(_backColor);
            graphs.FillRectangle(brush, rect);

            //Text
            Font font = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
            brush = new SolidBrush(Color.White);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            graphs.DrawString(Text, font, brush, rect, format);
        }

        public void DrawSideView(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            //Background
            int rectWidth = (int)(Text.ToCharArray().Length * 12);
            Rectangle rect = new Rectangle(Position.X, Position.Y, rectWidth, 11);
            Brush brush = new SolidBrush(_backColor);
            graphs.FillRectangle(brush, rect);

            //Text
            Font font = new Font("Arial", 11, FontStyle.Bold, GraphicsUnit.Pixel);
            brush = new SolidBrush(Color.White);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            graphs.DrawString(Text, font, brush, rect, format);
        }
    }
}
