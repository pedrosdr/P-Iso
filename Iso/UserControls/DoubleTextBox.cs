using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Iso
{
    public class DoubleTextBox : TextBox
    {
        //CONSTRUCTORS
        public DoubleTextBox() : base()
        {
            LostFocus += DoubleTextBox_LostFocus;
            KeyPress += DoubleTextBox_KeyPress;
            GotFocus += DoubleTextBox_GotFocus;

            Design();
        }


        //METHODS
        private void Design()
        {
            //this
            Text = "0,00";
            BackColor = Color.FromArgb(240, 240, 240);
            BorderStyle = BorderStyle.FixedSingle;
            TextAlign = HorizontalAlignment.Center;
        }

        //EVENTS
        private void DoubleTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int[] accepted = new int[] { 8, 13, 44, 45, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };

            if (!accepted.Contains(e.KeyChar))
                e.Handled = true;
            if (e.KeyChar == (char)13)
                e.Handled = true;
        }
        private void DoubleTextBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                double d = double.Parse(Text);
                Text = d.ToString("0.00");

            }
            catch
            {
                Text = "0,00";
            }
        }
        private void DoubleTextBox_GotFocus(object sender, EventArgs e)
        {
            SelectionStart = 0;
            SelectionLength = Text.Length;
        }
    }
}
