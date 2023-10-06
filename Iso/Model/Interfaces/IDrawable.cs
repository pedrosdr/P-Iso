using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public interface IDrawable
    {
        bool Selected { get; set; }
        void Draw(Bitmap backbuffer);
        void DrawSideView(Bitmap backbuffer);
    }
}
