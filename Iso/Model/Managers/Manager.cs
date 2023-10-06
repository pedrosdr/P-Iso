using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Iso
{
    public class Manager
    {
        //FIELDS
        private List<IDrawable> _drawables = new List<IDrawable>();
        private List<Structure> _structures = new List<Structure>();
        private List<Load> _forces = new List<Load>();

        private List<IDrawable> _selectedItems = new List<IDrawable>();

        //PROPERTIES
        public FileManager FileManager { get; set; }

        public List<IDrawable> Drawables
        {
            get { return _drawables; }
        }
        public List<Structure> Structures 
        { 
            get { return _structures; } 
        }
        public List<Load> Forces
        {
            get { return _forces; }
        }

        public InnerObject SelectedInnerObject { get; set; }
        public Load SelectedLoad { get; set; }
        public DistributedLoad SelectedDistLoad { get; set; }
        public Support SelectedSupport { get; set; }
        public Beam PickedBeam { get; set; }

        public IDrawable SelectedItem { get; set; }
        public List<IDrawable> SelectedItems
        {
            get { return _selectedItems; }
        }

        public Control Screen { get; set; }
        public Control SideViewScreen { get; set; }

        //CONSTRUCTORS
        public Manager(Control screen, Control sideViewScreen)
        {
            Screen = screen;
            SideViewScreen = sideViewScreen;

            FileManager = new FileManager(this);
        }

        //METHODS
        public void Update()
        {
            GenerateNodes();
            BuildAll();
            DrawAll();
            DrawSideView();
        }
        public void Clear()
        {
            _drawables.Clear();
            _structures.Clear();
            _forces.Clear();
            _selectedItems.Clear();
            SelectedInnerObject = null;
            SelectedLoad = null;
            SelectedDistLoad = null;
            SelectedSupport = null;
            PickedBeam = null;
            SelectedItem = null;
            SelectedItems.Clear();
        }

        public void UpdateSelectedItems(PointF mousePosition)
        {
            SelectedItem = null;

            foreach(IDrawable d in _drawables)
            {
                if(d is Structure)
                {
                    if(MouseOverStructure((d as Structure), mousePosition))
                    {
                        SelectedItem = d;
                        _selectedItems.Add(d);
                        (d as Structure).Selected = true;
                    }
                }
            }
        }
        public void UpdateSelectedInnerObjects(PointF mousePosition)
        {
            if (SelectedItem == null) return;
            if (!(SelectedItem is Structure)) return;

            Structure structure = (Structure)SelectedItem;

            if(structure is LinearStructure)
            {
                LinearStructure linearStructure = (LinearStructure)structure;
                
                //Load
                bool foundLoad = false;
                foreach(Load load in linearStructure.VirtualPointLoads)
                {
                    if (load.MouseInsideBox(mousePosition))
                    {
                        load.Selected = true;
                        SelectedLoad = load;
                        foundLoad = true;
                    }
                    else
                    {
                        load.Selected = false;
                        if (SelectedLoad == load)
                            SelectedLoad = null;
                    }
                }

                if (!foundLoad)
                {
                    SelectedLoad = null;
                    linearStructure.VirtualPointLoads.ForEach(l => l.Selected = false);
                }

                //DistributedLoad
                bool foundDistLoad = false;
                foreach (DistributedLoad load in linearStructure.VirtualDistributedLoads)
                {
                    if (load.MouseInsideBox(mousePosition))
                    {
                        load.Selected = true;
                        SelectedDistLoad = load;
                        foundDistLoad = true;
                    }
                    else
                    {
                        load.Selected = false;
                        if (SelectedDistLoad == load)
                            SelectedLoad = null;
                    }
                }

                if (!foundDistLoad)
                {
                    SelectedDistLoad = null;
                    linearStructure.VirtualDistributedLoads.ForEach(l => l.Selected = false);
                }

                //Support
                bool foundSupport = false;
                foreach(Support support in linearStructure.Supports)
                {
                    if (support.MouseInsideBox(mousePosition))
                    {
                        SelectedSupport = support;
                        Predicate<Support> predicate = s => s.Position.Equals(support.Position) && s.Structure == support.Structure;
                        linearStructure.SupportSelectionPredicate = predicate;
                        foundSupport = true;
                    }
                    else
                    {
                        if (SelectedSupport == support)
                            SelectedSupport = null;
                    }
                }

                if (!foundSupport)
                {
                    linearStructure.SupportSelectionPredicate = null;
                    SelectedSupport = null;
                }

                //InnerBeams
                if(structure is Beam)
                {
                    Beam beam = (Beam)structure;
                    bool found = false;
                    foreach(Beam b in beam.InnerBeams)
                    {
                        if(b.MouseInsideBox(beam, mousePosition))
                        {
                            found = true;
                            b.Picked = true;
                            PickedBeam = b;
                        }
                        else
                        {
                            b.Picked = false;
                        }
                    }

                    if (!found)
                        PickedBeam = null;
                }
            }
            
        }
        public void ClearSelection()
        {
            _selectedItems.Clear();
            SelectedItem = null;
            _drawables.ForEach(d => d.Selected = false);
        }
        public void DeleteSelectedItems()
        {
            _structures
                .Where(s => _selectedItems.Contains(s) && s is LinearStructure)
                .Select(s => s as LinearStructure)
                .ToList()
                .ForEach(ls => ls.PointLoads.ForEach(p => RemoveForce(p)));

            _structures.RemoveAll(s => _selectedItems.Contains(s));
            BuildAll();
            _drawables.RemoveAll(d => _selectedItems.Contains(d));
            _selectedItems.Clear();
        }

        //drawables

        public bool AddDrawable(IDrawable item)
        {
            if (!_drawables.Contains(item))
            {
                _drawables.Add(item);
                return true;
            }

            return false;
        }

        public bool RemoveDrawable(IDrawable item)
        {
            return _drawables.Remove(item);
        }

        public void ClearDrawables()
        {
            _drawables.RemoveAll(i => true);
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
            _drawables.ForEach(i => i.Draw(backbuffer));

            Screen.BackgroundImage = backbuffer;
        }

        public void DrawSideView()
        {
            Bitmap backbuffer = new Bitmap(SideViewScreen.Width, SideViewScreen.Height);
            Graphics graphs = Graphics.FromImage(backbuffer);
            graphs.Clear(Screen.BackColor);

            if(SelectedItem != null && _selectedItems.Count == 1)
                SelectedItem.DrawSideView(backbuffer);

            SideViewScreen.BackgroundImage = backbuffer;
        }

        //structures
        private bool MouseOverStructure(Structure s, PointF mousePosition)
        {
            if(s is LinearStructure)
            {
                LinearStructure ls = (LinearStructure)s;
                BiPosition p1 = ls.P1.ToBiPosition();
                BiPosition p2 = ls.P2.ToBiPosition();
                
                LinearEquation eq = new LinearEquation
                    (
                        new IsoPosition(p1.X, p1.Y, 0),
                        new IsoPosition(p2.X, p2.Y, 0)
                    );

                BiPosition mouseBP = BiPosition.FromPointF(mousePosition);
                IsoPosition mouse = new IsoPosition(mouseBP.X, mouseBP.Y, 0);

                IsoPosition point = null;
                if (p1.X != p2.X)
                    point = eq.GetPoint(AxisType.X, mouseBP.X);
                else if (p1.Y != p2.Y)
                    point = eq.GetPoint(AxisType.Y, mouseBP.Y);
                else
                    point = new IsoPosition(p1.X, p1.Y, 0);

                    bool pointIsInLine = point.ToPointF().X - 4 < mouse.ToPointF().X &&
                                          point.ToPointF().X + 4 > mouse.ToPointF().X &&
                                          point.ToPointF().Y - 4 < mouse.ToPointF().Y &&
                                          point.ToPointF().Y + 4 > mouse.ToPointF().Y;

                    bool pointIsUnderLimits = Util.GetLower(p1.ToPointF().X, p2.ToPointF().X) - 4 <= mouseBP.ToPointF().X &&
                                              Util.GetHigher(p1.ToPointF().X, p2.ToPointF().X) + 4 >= mouseBP.ToPointF().X &&
                                              Util.GetLower(p1.ToPointF().Y, p2.ToPointF().Y) - 4 <= mouseBP.ToPointF().Y &&
                                              Util.GetHigher(p1.ToPointF().Y, p2.ToPointF().Y) + 4 >= mouseBP.ToPointF().Y;


                    return pointIsInLine && pointIsUnderLimits;
            }
            else if (s is Slab)
            {
                Slab slab = s as Slab;

                return slab.MouseIn(mousePosition);
            }

            return false;
        }

        public IsoPosition GetPositionByStructure(PointF mousePosition)
        {
            foreach (Structure s in _structures)
            {
                if (s is LinearStructure)
                {
                    LinearStructure ls = (LinearStructure)s;
                    BiPosition p1 = ls.P1.ToBiPosition();
                    BiPosition p2 = ls.P2.ToBiPosition();

                    LinearEquation eq = new LinearEquation
                        (
                            new IsoPosition(p1.X, p1.Y, 0),
                            new IsoPosition(p2.X, p2.Y, 0)
                        );

                    BiPosition mouseBP = BiPosition.FromPointF(mousePosition);
                    IsoPosition mouse = new IsoPosition(mouseBP.X, mouseBP.Y, 0);

                    IsoPosition point = null;
                    if (p1.X != p2.X)
                        point = eq.GetPoint(AxisType.X, mouseBP.X);
                    else if (p1.Y != p2.Y)
                        point = eq.GetPoint(AxisType.Y, mouseBP.Y);
                    else
                        point = new IsoPosition(p1.X, p1.Y, 0);

                    bool pointIsInLine = point.ToPointF().X - 4 < mouse.ToPointF().X &&
                                          point.ToPointF().X + 4 > mouse.ToPointF().X &&
                                          point.ToPointF().Y - 4 < mouse.ToPointF().Y &&
                                          point.ToPointF().Y + 4 > mouse.ToPointF().Y;

                    bool pointIsUnderLimits = Util.GetLower(p1.ToPointF().X, p2.ToPointF().X) - 4 <= mouseBP.ToPointF().X &&
                                              Util.GetHigher(p1.ToPointF().X, p2.ToPointF().X) + 4 >= mouseBP.ToPointF().X &&
                                              Util.GetLower(p1.ToPointF().Y, p2.ToPointF().Y) - 4 <= mouseBP.ToPointF().Y &&
                                              Util.GetHigher(p1.ToPointF().Y, p2.ToPointF().Y) + 4 >= mouseBP.ToPointF().Y;

                    if (pointIsInLine && pointIsUnderLimits)
                    {
                        BiPosition biPosition = new BiPosition(point.X, point.Y);

                        IsoPosition position = null;
                        if (ls is Beam) //Beam
                        {
                            double z = ls.P1.Z;

                            position = AxisConverter.BeamPointFromBiPosition(biPosition, z);

                            if (ls.P1.X == ls.P2.X)
                            {
                                position.X = ls.P1.X;
                            }
                            else if (ls.P1.Y == ls.P2.Y)
                            {
                                position.Y = ls.P1.Y;
                            }
                            else
                            {
                                position = ls.Equation.GetPoint(AxisType.X, position.X);
                            }

                            return position;
                        }
                        else //Column
                        {
                            double x = ls.P1.X;
                            double y = ls.P1.Y;

                            return AxisConverter.ColumnPointFromBiPosition(biPosition, x, y);
                        }
                    }
                }
            }
            return null;
        }

        private void SetBaseShift()
        {
            double distX = 0;
            double distY = 0;
            int count = 0;
            foreach (Structure s in _structures)
            {
                foreach (IsoPosition p in s.Points)
                {
                    distX += p.X;
                    distY += p.Y;
                    count++;
                }
            }

            if (count != 0)
            {
                double avgX = distX / count;
                double avgY = distY / count;
                ScreenProperties.BaseShift = new IsoDistance(avgX, avgY, 0);
            }
        }

        public bool AddStructure(Structure s)
        {
            if (!_structures.Contains(s))
            {
                _structures.Add(s);
                AddDrawable(s);
                BuildAll();

                FileManager.AutoSave();
                return true;
            }
            return false;
        }

        public void RemoveStructure(Structure s)
        {
            _structures.Remove(s);
            _drawables.Remove(s);
            BuildAll();

            FileManager.AutoSave();
        }

        public void BuildAll()
        {
            _structures.ForEach(s => s.Structures.Clear());
            _structures.ForEach(s1 => _structures.ForEach(s2 => s1.Structures.Add(s2)));

            List<Beam> beams = _structures
                .Where(s => s is Beam)
                .Select(s => s as Beam)
                .ToList();

            beams.ForEach(s => s.ClearSupports());
            foreach (Structure structure in _structures)
            {
                beams.ForEach(s => s.RecognizeColumnSupports(_structures));
                beams.ForEach(s => s.RecognizeBeamSupports(_structures));
            }

            beams.ForEach(b => b.RemoveUnnecessarySupports(_structures));

            beams.ForEach(b => b.PointLoads.Clear());
            beams.ForEach(b => b.DistributedLoads.Clear());

            for (int i = 0; i < _structures.Count; i++)
                beams.ForEach(b => b.RecognizePointLoads(_structures));


            List<Column> columns = _structures.Where(s => s is Column).Select(s => s as Column).ToList();
            columns.ForEach(c => c.RecognizeLoads(_structures));
            columns.ForEach(c => c.ComputeReactions());

            SetBaseShift();
        }

        //nodes
        private void GenerateNodes()
        {
            _drawables.RemoveAll(d => d is Node);
            Node origin = new Node(new IsoPosition(0, 0, 0));
            origin.Type = NodeType.Origin;
            AddDrawable(origin);

            List<Structure> structures = _structures;

            foreach (Structure s in structures)
            {
                foreach (IsoPosition p in s.Points)
                {
                    Node node = new Node(p);
                    AddDrawable(node);
                }

                if (s is LinearStructure)
                {
                    LinearStructure ls = (LinearStructure)s;
                    Node node = new Node(ls.MiddlePoint);
                    AddDrawable(node);

                    List<Structure> linearStructures = structures
                        .Where(st => st is LinearStructure)
                        .ToList();

                    foreach (Structure linearStructure in linearStructures)
                    {
                        LinearStructure ls1 = (LinearStructure)linearStructure;
                        if (ls.Intersects(ls1))
                        {
                            node = new Node(ls.GetIntersectionPoint(ls1));
                            AddDrawable(node);
                        }
                    }
                }
            }
        }

        public void CheckHoveredNodes(PointF cursorPosition)
        {
            foreach (IDrawable d in _drawables)
            {
                if (d is Node)
                {
                    Node node = (Node)d;

                    PointF nodePosition = node.Position.ToPointF();

                    bool hovered =
                        cursorPosition.X >= nodePosition.X - 5f &&
                        cursorPosition.X <= nodePosition.X + 5f &&
                        cursorPosition.Y >= nodePosition.Y - 5f &&
                        cursorPosition.Y <= nodePosition.Y + 5f;

                    if (hovered)
                        node.Hovered = true;
                    else
                        node.Hovered = false;
                }
            }

            DrawAll();
        }

        public Node GetHoveredNode()
        {
            return _drawables
                   .Where(d => d is Node)
                   .Select(d => d as Node)
                   .Where(n => n.Hovered).FirstOrDefault();
        }

        //forces
        public bool AddForce(Load force)
        {
            if (!_forces.Contains(force))
            {
                _forces.Add(force);
                AddDrawable(force);
                return true;
            }

            return false;
        }

        public void RemoveForce(Load force)
        {
            _forces.Remove(force);
            RemoveDrawable(force);
        }
    }
}
