using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Iso
{
    public class PictureBoxScreen : PictureBox
    {
        //FIELDS
        private double _measureData = double.NaN;

        private List<IsoPosition> _points = new List<IsoPosition>();
        private int _clickCount = 0;

        private int _beamId = 0;
        private int _columnId = 0;

        private bool _justAClick = true;

        private bool _leftUp = true;
        private bool _leftDown = false;
        private bool _middleUp = true;
        private bool _middleDown = false;
        private bool _shiftOn = false;
        private bool _measureTypeOn = false;
        private bool _yAxisTypeOn = false;
        private bool _xAxisTypeOn = false;

        private bool _beamInUse = false;
        private bool _columnInUse = false;
        private bool _slabInUse = false;

        private SideIcon _sideIcon;
        private SideIcon _shiftSideIcon;
        private SideIcon _measureSideIcon;

        private MarkLine _markLine;
        private IsoPosition _markLineP1;
        private bool _markLineOn = false;

        private PointF _previousMousePosition = new PointF(0, 0);
        private double _previousRightShift = ScreenProperties.RightShift;
        private double _previousDownShift = ScreenProperties.DownShift;

        //PROPERTIES
        public double MeasureData
        {
            get { return _measureData; }
            set { _measureData = value; }
        }
        public bool MeasureTypeOn
        {
            get { return _measureTypeOn; }
            set { _measureTypeOn = value; }
        }

        public FormMain FormMain { get; set; }

        public bool BeamInUse
        {
            get { return _beamInUse; }
            set { _beamInUse = value; }
        }
        public bool ColumnInUse
        {
            get { return _columnInUse; }
            set { _columnInUse = value; }
        }
        public bool SlabInUse
        {
            get { return _slabInUse; }
            set { _slabInUse = value; }
        }

        //CONSTRUCTOR
        public PictureBoxScreen(FormMain formMain) : base()
        {
            FormMain = formMain;

            MouseEnter += PictureBoxScreen_MouseEnter;
            MouseWheel += PictureBoxScreen_MouseWheel;
            MouseDown += PictureBoxScreen_MouseDown;
            MouseUp += PictureBoxScreen_MouseUp;
            MouseMove += PictureBoxScreen_MouseMove;
            PreviewKeyDown += PictureBoxScreen_PreviewKeyDown;
            MouseClick += PictureBoxScreen_MouseClick;
            GotFocus += PictureBoxScreen_GotFocus;
        }




        //METHODS
        private void SetAllUsesFalse()
        {
            _beamInUse = false;
            _columnInUse = false;
            _slabInUse = false;
            FormMain.BeamInUse = false;
            FormMain.ColumnInUse = false;
            FormMain.SlabInUse = false;
        }

        private bool ThereAreActiveTools()
        {
            return _beamInUse || _columnInUse || SlabInUse;
        }

        public void PickSideIcon()
        {
            FormMain.Manager.RemoveDrawable(_sideIcon);

            string text = null;
            Point position = new Point(20, Height - 50);

            if (_beamInUse)
            {
                text = "Desenhar viga";
            }
            else if (_columnInUse)
            {
                text = "Desenhar pilar";
            }
            else if (_slabInUse)
            {
                text = "Desenhar laje";
            }

            if (!string.IsNullOrEmpty(text))
            {
                _sideIcon = new SideIcon(text, position);
                FormMain.Manager.AddDrawable(_sideIcon);
            }

            FormMain.Manager.Update();
        }

        public void AddByMeasure()
        {
            if (_measureTypeOn && _measureData != double.NaN)
            {
                if (_beamInUse)
                {
                    if (_xAxisTypeOn)
                    {
                        Beam b = new Beam(_points[0], new IsoPosition(_points[0].X + MeasureData, _points[0].Y, _points[0].Z));
                        b.Id = _beamId;

                        _beamId++;
                        FormMain.Manager.AddStructure(b);
                    }
                    else if (_yAxisTypeOn)
                    {
                        Beam b = new Beam(_points[0], new IsoPosition(_points[0].X, _points[0].Y + MeasureData, _points[0].Z));
                        b.Id = _beamId;

                        _beamId++;
                        FormMain.Manager.AddStructure(b);
                    }
                }
                else if (_columnInUse)
                {
                    Column c = new Column(_points[0], new IsoPosition(_points[0].X, _points[0].Y, _points[0].Z + MeasureData));
                    c.Id = _columnId;
                    _columnId++;
                    FormMain.Manager.AddStructure(c);
                }

                _points.Clear();
                _clickCount = 0;
                _markLineOn = false;
                FormMain.Manager.RemoveDrawable(_markLine);
            }

            _measureTypeOn = false;
            _xAxisTypeOn = false;
            _yAxisTypeOn = false;
            FormMain.Manager.RemoveDrawable(_measureSideIcon);
        }

        //EVENTS
        private void PictureBoxScreen_MouseMove(object sender, MouseEventArgs e)
        {
            _justAClick = false;

            //Draw markline?
            try
            {
                if (_markLineOn)
                {
                    FormMain.Manager.RemoveDrawable(_markLine);
                    if (_beamInUse)
                    {
                        _markLine = new MarkLine(_markLineP1, e.Location, Color.RoyalBlue);
                        FormMain.Manager.AddDrawable(_markLine);
                    }
                    else if (_columnInUse && _markLineP1 != null)
                    {
                        _markLine = new MarkLine(_markLineP1, new PointF(_markLineP1.ToPointF().X, e.Location.Y), Color.FromArgb(200, 200, 0));
                        FormMain.Manager.AddDrawable(_markLine);
                    }
                    else if (_slabInUse && _markLineP1 != null)
                    {
                        _markLine = new MarkLine(_markLineP1, e.Location, Color.Black, 1);
                        FormMain.Manager.AddDrawable(_markLine);
                    }
                }
            }
            catch{}

            //Mouse position while mouse buttons up
            if (_leftUp && _middleUp)
            { 
                _previousMousePosition = new PointF(e.X, e.Y);

                //Check Nodes
                if (ThereAreActiveTools())
                    FormMain.Manager.CheckHoveredNodes(new PointF(e.X, e.Y));
            }
            
            //Rotation
            if (_middleDown)
            {
                if (e.X > _previousMousePosition.X)
                    ScreenProperties.Rotation += 0.07;
                else
                    ScreenProperties.Rotation -= 0.07;
            }

            //Drag
            else if (_leftDown)
            {
                ScreenProperties.RightShift = _previousRightShift + ( e.X - _previousMousePosition.X);
                ScreenProperties.DownShift = _previousDownShift + (e.Y - _previousMousePosition.Y);
            }

            //Update Drawing
            FormMain.Manager.DrawAll();
        }

        private void PictureBoxScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _middleDown = false;
                _middleUp = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                _leftDown = false;
                _leftUp = true;
            }

            _previousMousePosition = new PointF(e.X, e.Y);
        }

        private void PictureBoxScreen_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
            _justAClick = true;
            _previousDownShift = ScreenProperties.DownShift;
            _previousRightShift = ScreenProperties.RightShift;

            if (e.Button == MouseButtons.Middle)
            {
                _middleUp = false;
                _middleDown = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                _leftUp = false;
                _leftDown = true;
            }
        }

        private void PictureBoxScreen_MouseEnter(object sender, EventArgs e)
        {
            PickSideIcon();
        }

        private void PictureBoxScreen_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                ScreenProperties.Zoom += 3;
            else if (e.Delta < 0)
                ScreenProperties.Zoom -= 3;

            FormMain.Manager.DrawAll();
        }
        
        private void PictureBoxScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.V)
            {
                SetAllUsesFalse();
                _beamInUse = true;
                FormMain.BeamInUse = true;
            }
            else if (e.KeyCode == Keys.P)
            {
                SetAllUsesFalse();
                _columnInUse = true;
                FormMain.ColumnInUse = true;
            }
            else if (e.KeyCode == Keys.L)
            {
                SetAllUsesFalse();
                _slabInUse = true;
                FormMain.SlabInUse = true;
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                if (_shiftOn)
                {
                    _shiftOn = false;
                    FormMain.Manager.RemoveDrawable(_shiftSideIcon);
                }
                else
                {
                    _shiftOn = true;
                    _shiftSideIcon = new SideIcon("Shift", new Point(20, Height - 100));
                    FormMain.Manager.AddDrawable(_shiftSideIcon);
                }
            }
            else if (e.KeyCode == Keys.Y)
            {
                if (_beamInUse && _points.Count == 1)
                {
                    FormMain.SplitContainer.LabelMeasure.Text = "Distância no eixo Y:";
                    FormMain.SplitContainer.TextBoxMeasure.Focus();
                    _measureTypeOn = true;
                    _yAxisTypeOn = true;

                    FormMain.Manager.RemoveDrawable(_measureSideIcon);
                    _xAxisTypeOn = false;
                    _measureSideIcon = new SideIcon("No eixo Y", new Point(Width - 100, Height - 50));
                    _measureSideIcon.BackColor = Color.Green;
                    FormMain.Manager.AddDrawable(_measureSideIcon);
                }
            }
            else if (e.KeyCode == Keys.X)
            {
                if (_beamInUse && _points.Count == 1)
                {
                    FormMain.SplitContainer.LabelMeasure.Text = "Distância no eixo X:";
                    FormMain.SplitContainer.TextBoxMeasure.Focus();
                    _measureTypeOn = true;
                    _xAxisTypeOn = true;

                    FormMain.Manager.RemoveDrawable(_measureSideIcon);
                    _yAxisTypeOn = false;
                    _measureSideIcon = new SideIcon("No eixo X", new Point(Width - 100, Height - 50));
                    _measureSideIcon.BackColor = Color.Red;
                    FormMain.Manager.AddDrawable(_measureSideIcon);
                }
            }
            else if (e.KeyCode == Keys.Z)
            {
                if (_columnInUse && _points.Count == 1)
                {
                    FormMain.SplitContainer.LabelMeasure.Text = "Distância no eixo Z:";
                    FormMain.SplitContainer.TextBoxMeasure.Focus();
                    _measureTypeOn = true;

                    FormMain.Manager.RemoveDrawable(_measureSideIcon);
                    _measureSideIcon = new SideIcon("No eixo Z", new Point(Width - 100, Height - 50));
                    FormMain.Manager.AddDrawable(_measureSideIcon);
                }
            }
            else if(e.KeyCode == Keys.Delete)
            {
                FormMain.Manager.DeleteSelectedItems();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SetAllUsesFalse();
                FormMain.Manager.ClearSelection();
                _clickCount = 0;
                _points.Clear();

                _markLineOn = false;
                FormMain.Manager.RemoveDrawable(_markLine);
                FormMain.Manager.RemoveDrawable(_measureSideIcon);
            }

            PickSideIcon();
        }
       
        private void PictureBoxScreen_MouseClick(object sender, MouseEventArgs e)
        {
            PointF mousePosition = new PointF(e.X, e.Y);

            if (_justAClick)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (!ThereAreActiveTools())
                    {
                        if (!_shiftOn)
                        {
                            FormMain.Manager.ClearSelection();
                        }
                        FormMain.Manager.UpdateSelectedItems(mousePosition);
                        FormMain.Manager.Update();

                        _markLineOn = false;
                        FormMain.Manager.RemoveDrawable(_markLine);
                    }
                    else if (_beamInUse) //Draw beam
                    {
                        if (_clickCount == 0)
                        {
                            _points.Clear();

                            Node node = FormMain.Manager.GetHoveredNode();
                            if (node != null)
                            {
                                _points.Add(node.Position);

                                _markLineP1 = node.Position;
                                _markLineOn = true;
                            }
                            else
                            {
                                IsoPosition point = FormMain.Manager.GetPositionByStructure(mousePosition);
                                if (point != null)
                                {
                                    _points.Add(point);

                                    _markLineP1 = point;
                                    _markLineOn = true;
                                }
                            }

                            _clickCount++;
                        }
                        else
                        {
                            if (_points.Count != 0)
                            {
                                Node node = FormMain.Manager.GetHoveredNode();
                                if (node != null)
                                {
                                    IsoPosition p2 = new IsoPosition(node.Position.X, node.Position.Y, _points[0].Z);
                                    
                                    Beam b = new Beam(_points[0], p2);
                                    b.Id = _beamId;
                                    _beamId++;

                                    FormMain.Manager.AddStructure(b);
                                    FormMain.Manager.Update();

                                    _markLineOn = false;
                                    FormMain.Manager.RemoveDrawable(_markLine);
                                }
                                else
                                {
                                    IsoPosition point = FormMain.Manager.GetPositionByStructure(mousePosition);
                                    if (point != null)
                                    { 
                                        IsoPosition p2 = new IsoPosition(point.X, point.Y, _points[0].Z);

                                        Beam b = new Beam(_points[0], p2);
                                        b.Id = _beamId;
                                        _beamId++;

                                        FormMain.Manager.AddStructure(b);
                                        FormMain.Manager.Update();

                                        _markLineOn = false;
                                        FormMain.Manager.RemoveDrawable(_markLine);
                                    }
                                }
                            }

                            _clickCount = 0;
                        }
                    }
                    else if (_columnInUse) //Draw Column
                    {
                        if (_clickCount == 0)
                        {
                            _points.Clear();

                            Node node = FormMain.Manager.GetHoveredNode();
                            if (node != null)
                            {
                                _points.Add(node.Position);

                                _markLineP1 = node.Position;
                                _markLineOn = true;
                            }
                            else
                            {
                                IsoPosition point = FormMain.Manager.GetPositionByStructure(mousePosition);
                                if (point != null)
                                    _points.Add(point);

                                _markLineP1 = point;
                                _markLineOn = true;
                            }

                            _clickCount++;
                        }
                        else
                        {
                            if (_points.Count != 0)
                            {
                                Node node = FormMain.Manager.GetHoveredNode();
                                if (node != null)
                                {
                                    IsoPosition p2 = new IsoPosition(_points[0].X, _points[0].Y, node.Position.Z);

                                    Column c = new Column(_points[0], p2);
                                    c.Id = _columnId;
                                    _columnId++;
                                    FormMain.Manager.AddStructure(c);
                                    FormMain.Manager.Update();

                                    _markLineOn = false;
                                    FormMain.Manager.RemoveDrawable(_markLine);
                                }
                                else
                                {
                                    IsoPosition point = FormMain.Manager.GetPositionByStructure(mousePosition);
                                    if (point != null)
                                    {
                                        IsoPosition p2 = new IsoPosition(_points[0].X, _points[0].Y, point.Z);

                                        Column c = new Column(_points[0], p2);
                                        c.Id = _columnId;
                                        _columnId++;
                                        FormMain.Manager.AddStructure(c);
                                        FormMain.Manager.Update();

                                        _markLineOn = false;
                                        FormMain.Manager.RemoveDrawable(_markLine);
                                    }
                                }
                                _points.Clear();
                                _clickCount = 0;
                            }

                            _clickCount = 0;
                        }
                    }
                    else if (SlabInUse)
                    {
                        if (_clickCount == 0)
                        {
                            _points.Clear();

                            Node node = FormMain.Manager.GetHoveredNode();
                            if (node != null)
                            {
                                _points.Add(node.Position);

                                _markLineP1 = node.Position;
                                _markLineOn = true;
                            }
                            else
                            {
                                IsoPosition point = FormMain.Manager.GetPositionByStructure(mousePosition);
                                if (point != null)
                                {
                                    _points.Add(point);

                                    _markLineP1 = point;
                                    _markLineOn = true;
                                }
                            }

                            _clickCount++;
                        }
                        else
                        {
                            if (_points.Count != 0)
                            {
                                Node node = FormMain.Manager.GetHoveredNode();
                                if (node != null)
                                {
                                    IsoPosition p2 = new IsoPosition(node.Position.X, node.Position.Y, _points[0].Z);

                                    Slab s = new Slab(_points[0], p2);
                                    //s.Id = _slabId;
                                    //_beamId++;

                                    FormMain.Manager.AddStructure(s);
                                    FormMain.Manager.Update();

                                    _markLineOn = false;
                                    FormMain.Manager.RemoveDrawable(_markLine);
                                }
                                else
                                {
                                    IsoPosition point = FormMain.Manager.GetPositionByStructure(mousePosition);
                                    if (point != null)
                                    {
                                        IsoPosition p2 = new IsoPosition(point.X, point.Y, _points[0].Z);

                                        Beam b = new Beam(_points[0], p2);
                                        b.Id = _beamId;
                                        _beamId++;

                                        FormMain.Manager.AddStructure(b);
                                        FormMain.Manager.Update();

                                        _markLineOn = false;
                                        FormMain.Manager.RemoveDrawable(_markLine);
                                    }
                                }
                            }

                            _clickCount = 0;
                        }
                    }
                }
            }
        }
      
        private void PictureBoxScreen_GotFocus(object sender, EventArgs e)
        {
            if (!_measureTypeOn)
                FormMain.Manager.RemoveDrawable(_measureSideIcon);
        }
    }
}
