using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Iso
{
    public static class ScreenProperties
    {
        //FIELDS
        private static PictureBox _screen;
        private static Color _backColor = Color.FromKnownColor(KnownColor.Control);
        private static double _zoom = 50;
        private static double _rightShift = 0;
        private static double _downShift = 0;
        private static double _rotation = 0;
        private static IsoDistance _baseShift = new IsoDistance(0, 0, 0);

        //PROPERTIES
        public static PictureBox Screen
        {
            get { return _screen; }
            set { _screen = value; }
        }

        public static Size ScreenSize 
        { 
            get { return _screen.Size; } 
        }

        public static Image BackgroundImage
        {
            get { return Screen.BackgroundImage; }
            set { Screen.BackgroundImage = value; }
        }

        public static double Zoom
        {
            get { return _zoom; }
            set 
            {
                if (value < 1)
                    _zoom = 1;
                else
                    _zoom = value; 
            }
        }

        public static Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        public static double RightShift
        {
            get { return _rightShift; }
            set { _rightShift = value; }
        }

        public static double DownShift
        {
            get { return _downShift; }
            set { _downShift = value; }
        }

        public static double Rotation
        {
            get { return _rotation; }
            set 
            {
                if (value > 2 * Math.PI)
                    _rotation = 0;
                else if (value < 0)
                    _rotation = 2 * Math.PI;
                else
                    _rotation = value; 
            }
        }

        public static IsoDistance BaseShift
        {
            get { return _baseShift; }
            set { _baseShift = value; }
        }
    }
}
