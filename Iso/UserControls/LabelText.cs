using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Iso
{
    public class LabelText : Label
    {
        //CONSTRUCTORS
        public LabelText() : base()
        {
            Design();
        }

        //METHODS
        private void Design()
        {
            //this
            Font = new Font("Verdana", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        }
    }
}
