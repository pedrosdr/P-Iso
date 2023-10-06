using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Iso
{
    public class ScreenDraw
    {
        //FIELDS
        private List<IDrawable> _items = new List<IDrawable>();
        
        //PROPERTIES
        public List<IDrawable> Items
        {
            get { return _items; }
        }

        public Control Screen { get; set; }

        //CONSTRUCTORS
        public ScreenDraw(Control screen)
        {
            Screen = screen;
        }

        //METHODS
        public bool AddItem(IDrawable item)
        {
            if (!_items.Contains(item))
            {
                _items.Add(item);
                return true;
            }

            return false;
        }

        public bool RemoveItem(IDrawable item)
        {
            return _items.Remove(item);
        }

        public void Clear()
        {
            _items.RemoveAll(i => true);
        }

        private void DrawAxis(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            double size = ScreenProperties.ScreenSize.Height * 100 / ScreenProperties.Zoom;

            IsoPosition axisX1 = new IsoPosition(0, 0, 0);
            IsoPosition axisX2 = new IsoPosition(size, 0, 0);

            IsoPosition axisY1 = new IsoPosition(0, 0, 0);
            IsoPosition axisY2 = new IsoPosition(0, size, 0);

            IsoPosition axisZ1 = new IsoPosition(0, 0, 0);
            IsoPosition axisZ2 = new IsoPosition(0, 0, size);

            graphs.DrawLine(new Pen(Color.FromArgb(80, 255, 0, 0), 2), axisX1.ToPointF(), axisX2.ToPointF());
            graphs.DrawLine(new Pen(Color.FromArgb(80, 0, 255, 0), 2), axisY1.ToPointF(), axisY2.ToPointF());
            graphs.DrawLine(new Pen(Color.FromArgb(80, 0, 0, 255), 2), axisZ1.ToPointF(), axisZ2.ToPointF());
        }

        public void DrawAll()
        {
            Bitmap backbuffer = new Bitmap(Screen.Width, Screen.Height);
            Graphics graphs = Graphics.FromImage(backbuffer);
            graphs.Clear(ScreenProperties.BackColor);
            DrawAxis(backbuffer);

            //Drawables
            _items.ForEach(i => i.Draw(backbuffer));

            Screen.BackgroundImage = backbuffer;
        }

        public void DrawSideView(IDrawable item)
        {
            Bitmap backbuffer = new Bitmap(Screen.Width, Screen.Height);
            Graphics graphs = Graphics.FromImage(backbuffer);
            graphs.Clear(Screen.BackColor);

            item.DrawSideView(backbuffer);

            Screen.BackgroundImage = backbuffer;
        }
    }
}
