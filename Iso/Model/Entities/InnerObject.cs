using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public abstract class InnerObject : IDrawable
    {
        //PROPERTIES
        public Structure SourceStructure { get; set; }
        public virtual Structure Structure { get; set; } 
        public bool Selected { get; set; }

        public abstract RectangleF SelectionBox { get;}

        //CONSTRUCTORS
        public InnerObject()
        {

        }
        public InnerObject(Structure structure)
        {
            Structure = structure;
        }

        //METHODS
        public abstract void Draw(Bitmap backbuffer);
        public abstract void DrawSideView(Bitmap backbuffer);

        public bool MouseInsideBox(PointF mousePosition)
        {
            return Util.PointIsInsideRectangle(mousePosition, SelectionBox);
        }
    }
}
