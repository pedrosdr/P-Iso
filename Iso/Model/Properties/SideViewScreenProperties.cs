using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Iso
{
    public static class SideViewScreenProperties
    {
        //FIELDS
        private static PictureBox _screen;
        private static Color _backColor = Color.FromKnownColor(KnownColor.Control);
        private static bool _momentumDiagramOn = true;
        private static bool _shearDiagramOn = true;
        private static bool _shearEquationsOn = true;
        private static bool _deformationDiagramOn = false;
        private static bool _rotationDiagramOn = false;
        private static double _momentumDiagramScale = 0.01;
        private static bool _pointLoadsOn = false;
        private static bool _distributedLoadsOn = true;
        private static bool _momentumEquationsOn = true;

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
        public static Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }
        public static bool MomentumDiagramOn
        {
            get { return _momentumDiagramOn; }
            set { _momentumDiagramOn = value; }
        }
        public static bool ShearDiagramOn
        {
            get { return _shearDiagramOn; }
            set { _shearDiagramOn = value; }
        }
        public static bool ShearEquationsOn
        {
            get { return _shearEquationsOn; }
            set { _shearEquationsOn = value; }
        }
        public static bool DeformationDiagramOn
        {
            get { return _deformationDiagramOn; }
            set { _deformationDiagramOn = value; }
        }
        public static bool RotationDiagramOn
        {
            get { return _rotationDiagramOn; }
            set { _rotationDiagramOn = value; }
        }
        public static double MomentumDiagramScale
        {
            get { return _momentumDiagramScale; }
            set 
            {
                if (value <= 0)
                    _momentumDiagramScale = 0;
                else
                    _momentumDiagramScale = value; 
            }
        }
        public static bool PointLoadsOn
        {
            get { return _pointLoadsOn; }
            set { _pointLoadsOn = value; }
        }
        public static bool DistributedLoadsOn
        {
            get { return _distributedLoadsOn; }
            set { _distributedLoadsOn = value; }
        }
        public static bool MomentumEquationsOn
        {
            get { return _momentumEquationsOn; }
            set { _momentumEquationsOn = value; }
        }
    }
}
