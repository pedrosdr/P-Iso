using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Iso
{
    public class Beam : LinearStructure
    {
        //FIELDS
        private List<Column> _supportColumns = new List<Column>();

        private List<Beam> _innerBeams = new List<Beam>();
        private List<Beam> _supportBeams = new List<Beam>();
        private List<Beam> _loadBeams = new List<Beam>();

        //PROPERTIES
        public List<Beam> InnerBeams
        {
            get
            {
                return Structures
                    .Where(s => s is Beam)
                    .Select(s => s as Beam)
                    .Where(b => Intersects(b))
                    .Where(b => !Beams.AreSupportedByTheSameColumn(this, b))
                    .ToList();
            }
        }
        public bool IsStable
        {
            get
            {
                if (Supports.Count == 0) return false;

                if (Supports.Count == 1 && !(Supports[0] is Engaste)) return false;

                foreach (Support a in Supports)
                    if (!(a is ApoioMovel))
                        return true;

                return false;
            }
        }
        public int HiperestaticityDegree
        {
            get
            {
                int unknown = 0;
                foreach (Support s in Supports)
                {
                    if (s is ApoioMovel) unknown++;
                    else if (s is ApoioFixo) unknown += 2;
                    else unknown += 3;
                }

                return unknown - 3;
            }
        }

        public bool Picked { get; set; }

        public List<Beam> SupportBeams
        {
            get { return _supportBeams; }
        }
        public List<Beam> LoadBeams
        {
            get { return _loadBeams; }
        }

        public Line Line
        {
            get { return new Line(P1, P2); }
        }

        //CONSTRUCTORS
        public Beam(IsoPosition p1, IsoPosition p2) : base(p1, p2)
        {
        }

        //METHODS
        public double GetMomento(double x)
        {
            List<MomentumEquation> eqs = Beams.GetMomentumEquations(this);
            if (eqs == null)
                return 0.00;

            MomentumEquation eq = eqs.Where(e => e.From <= x && x <= e.To).FirstOrDefault();
            if (eq == null)
                return 0.00;

            return eq.GetValue(x);
        }
        public double GetCortante(double x)
        {
            List<ShearEquation> eqs = Beams.GetShearEquations(this);
            if (eqs == null)
                return 0.00;

            ShearEquation eq = eqs.Where(e => e.From <= x && x <= e.To).FirstOrDefault();
            if (eq == null)
                return 0.00;
            
            return eq.GetShearForce(x);
        }
        public double GetRotation(double x)
        {
            return Beams.GetRotationFuncEI(this, x);
        }
        public double GetVerticalDeformation(double x)
        {
            return Beams.GetDeformationFuncEI(this, x, Direction.Vertical);
        }

        public bool IsStableWithout(LinearStructure s)
        {
            if (Supports.Count == 0) return false;

            List<Engaste> engastes = Supports
                .Where(sup => sup is Engaste)
                .Select(sup => sup as Engaste)
                .ToList();

            if (!Intersects(s)) return true;

            IsoPosition intersection = GetIntersectionPoint(s);

            if (engastes.Count > 0) return true;

            List<ApoioFixo> fixedSupports = Supports
                .Where(sup => sup is ApoioFixo)
                .Where(sup => !sup.Position.Equals(intersection))
                .Select(sup => sup as ApoioFixo)
                .ToList();

            int fixedCount = fixedSupports.Count;

            if (fixedCount > 1) return true;

            List<ApoioMovel> mobileSupports = Supports
                .Where(sup => sup is ApoioMovel)
                .Where(sup => !sup.Position.Equals(intersection))
                .Select(sup => sup as ApoioMovel)
                .ToList();

            int mobileCount = mobileSupports.Count;

            if (mobileCount >= 1 && fixedCount >= 1) return true;

            return false;
        }
        public bool SupportsColumn(List<Structure> structures, Column c)
        {
            if (!Intersects(c)) return false;

            List<Beam> beams = structures
                .Where(s => (s is Beam) && s != this)
                .Select(s => s as Beam)
                .Where(b => Intersects(b))
                .ToList();

            foreach(Beam b in beams)
            {
                if (GetIntersectionPoint(c).Equals(b.GetIntersectionPoint(c)))
                {
                    if (IsSupportedByBeam(b))
                        return false;
                }
            }

            IsoPosition intersection = GetIntersectionPoint(c);
            IsoPosition point = c.Points
                .Where(p => p.Equals(intersection))
                .FirstOrDefault();

            bool supportsColumn = false;
            if (point != null)
            {
                foreach (IsoPosition p in c.Points)
                {
                    if (p != point && p.Z > point.Z)
                        supportsColumn = true;
                }
            }

            return supportsColumn;
        }
        public bool IsSupportedByBeam(Beam b)
        {
            if (!Intersects(b)) 
                return false;
            if (!b.IsStableWithout(this)) 
                return false;
            if (!IsStableWithout(b))
                return true;
            if (GetColumnSupports().Count == 1)
                if(!(GetColumnSupports()[0] is Engaste))
                    return true;
            if (b.GetColumnSupports().Count == 1)
                if (!(b.GetColumnSupports()[0] is Engaste))
                    return false;

            Beam supported = Beams.GetMostUnfavorable(this, b);
            return supported == this;
        }
        public bool IsSupportedByColumn(Column c)
        {
            if (!Intersects(c)) return false;

            IsoPosition intersection = GetIntersectionPoint(c);
            foreach(IsoPosition point in c.Points)
                if (point.Z < intersection.Z) return true;

            return false;
        }

        public void RemoveUnnecessarySupports(List<Structure> structures)
        {
            List<Support> removed = new List<Support>();

            foreach(Support support in Supports)
            {
                //removed.Add(support);
            }

            Supports.RemoveAll(s => removed.Contains(s));
        }

        public double GetFarthestDistanceFromPointToSupport(IsoPosition point)
        {
            if (!HasPoint(point)) return double.NaN;
            if (Supports.Count == 0) return double.NaN;

            double vectX = point.X - P1.X;
            double vectY = point.Y - P1.Y;
            double vectZ = point.Z - P1.Z;

            double innerPoint = Math.Sqrt(vectX * vectX + vectY * vectY + vectZ * vectZ);

            List<Support> supports = Supports.Where(s => !s.Position.Equals(point)).ToList();
            if (supports.Count == 0) return double.NaN;

            List<Support> supportsBefore = supports.Where(s => s.InnerPoint < innerPoint).ToList();
            List<Support> supportsAfter = supports.Where(s => s.InnerPoint > innerPoint).ToList();

            double closestBeforePoint;
            if (supportsBefore.Count == 0)
                closestBeforePoint = 0;
            else
            {
                closestBeforePoint = Math.Sqrt(Math.Pow(innerPoint - supportsBefore[0].InnerPoint, 2));
                foreach (Support s in supportsBefore)
                {
                    double distance = Math.Sqrt(Math.Pow(innerPoint - s.InnerPoint, 2));
                    if (distance < closestBeforePoint)
                        closestBeforePoint = distance;
                }
            }

            double closestAfterPoint;
            if (supportsAfter.Count == 0)
                closestAfterPoint = 0;
            else
            {
                closestAfterPoint = Math.Sqrt(Math.Pow(innerPoint - supportsAfter[0].InnerPoint, 2));
                foreach (Support s in supportsAfter)
                {
                    double distance = Math.Sqrt(Math.Pow(innerPoint - s.InnerPoint, 2));
                    if (distance < closestAfterPoint)
                        closestAfterPoint = distance;
                }
            }

            return Util.GetHigher(closestAfterPoint, closestBeforePoint);
        }
        public List<Support> GetColumnSupports()
        {
            List<Column> columns = Structures.Where(s => s is Column).Select(s => s as Column).ToList();

            List<Support> columnSupports = new List<Support>();
            foreach(Column c in columns)
            {
                if (IsSupportedByColumn(c))
                    columnSupports.Add(Supports.Where(s => GetIntersectionPoint(c).Equals(s.Position)).FirstOrDefault());
            }

            return columnSupports;
        }

        private void DrawEffects(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);
            Pen pen = new Pen(Color.Red, 1);
            double def;

            List<DeformationEquation> eqs = Beams.GetDeformationEquations(this);
            List<PointF> points = new List<PointF>();
            for (float f = 0; f <= Length; f += 0.3f)
            {
                if (eqs == null) return;
                DeformationEquation eq = eqs.Where(e => e.From <= f && e.To >= f).FirstOrDefault();
                def = eq.GetDeformation(f);
                SideViewPosition p = new SideViewPosition(this, f, def * 0.1 * SideViewScreenProperties.MomentumDiagramScale);
                points.Add(p.ToPointF());
            }
            def = eqs[eqs.Count - 1].GetDeformation(Length);
            SideViewPosition pn = new SideViewPosition(this, Length, def * 0.1 * SideViewScreenProperties.MomentumDiagramScale);
            points.Add(pn.ToPointF());

            graphs.DrawLines(pen, points.ToArray());

            if (SideViewScreenProperties.DeformationDiagramOn)
            {
                List<Effect> effects = Beams.GetImportantVerticalDeformations(this);
                if (effects != null)
                {
                    foreach (Effect effect in effects)
                    {
                        string s = effect.PrintVerticalDeformation();
                        Font font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                        Brush brush = new SolidBrush(Color.Black);
                        SideViewPosition pos = new SideViewPosition(this, effect.X, effect.VerticalDeformation * 0.1 * (SideViewScreenProperties.MomentumDiagramScale));
                        PointF point = new PointF(pos.ToPointF().X - 30, pos.ToPointF().Y);
                        graphs.DrawString(s, font, brush, point);

                        SideViewPosition p1 = new SideViewPosition(this, effect.X, 0);
                        graphs.DrawLine(pen, p1.ToPointF(), pos.ToPointF());
                    }
                }
            }

            if (SideViewScreenProperties.RotationDiagramOn)
            {
                List<Effect> effects = Beams.GetImportantRotations(this);
                if (effects != null)
                {
                    foreach (Effect effect in effects)
                    {
                        string s = effect.PrintRotation();
                        Font font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                        Brush brush = new SolidBrush(Color.Black);
                        SideViewPosition pos = new SideViewPosition(this, effect.X, effect.VerticalDeformation * 0.1 * (SideViewScreenProperties.MomentumDiagramScale));
                        PointF point = new PointF(pos.ToPointF().X - 30, pos.ToPointF().Y);
                        graphs.DrawString(s, font, brush, point);

                        SideViewPosition p1 = new SideViewPosition(this, effect.X, 0);
                        graphs.DrawLine(pen, p1.ToPointF(), pos.ToPointF());
                    }
                }
            }
        }
        private void DrawShearDiagram(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            List<ShearEquation> eqs = Beams.GetShearEquations(this);

            foreach(ShearEquation eq in eqs)
            {
                Console.WriteLine(eq.ToString());
            }

            List<PointF> points = new List<PointF>();
            points.Add(new SideViewPosition(this, 0, 0).ToPointF());
            foreach(ShearEquation eq in eqs)
            {
                for(double d = eq.From; d <= eq.To; d += 0.01)
                {
                    SideViewPosition p = new SideViewPosition(this, d, eq.GetShearForce(d) * SideViewScreenProperties.MomentumDiagramScale);
                    points.Add(p.ToPointF());
                }
            }
            points.Add(new SideViewPosition(this, Length, 0).ToPointF());

            graphs.DrawLines(new Pen(Color.Orange, 1), points.ToArray());

            foreach(ShearEquation eq in eqs)
            {
                string s = eq.GetShearForce(eq.From).ToString("0.00") + " kN";
                Brush brush = new SolidBrush(Color.Orange);
                SideViewPosition p = new SideViewPosition(this, eq.From, eq.GetShearForce(eq.From) * SideViewScreenProperties.MomentumDiagramScale);
                Font font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                graphs.DrawString(s, font, brush, p.ToPointF());

                s = eq.GetShearForce(eq.To).ToString("0.00") + " kN";
                brush = new SolidBrush(Color.Orange);
                p = new SideViewPosition(this, eq.To, eq.GetShearForce(eq.To) * SideViewScreenProperties.MomentumDiagramScale);
                font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                graphs.DrawString(s, font, brush, p.ToPointF());
            }

            // Shear Equations
            foreach(ShearEquation eq in eqs)
            {
                double x = (eq.From + eq.To) / 2;
                double V = eq.a * x + eq.b;

                Font font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                Brush brush = new SolidBrush(Color.Black);
                SideViewPosition p1 = new SideViewPosition(this, x, V * SideViewScreenProperties.MomentumDiagramScale);
                graphs.DrawString(eq.ToString(), font, brush, p1.ToPointF());
            }
        }
        private void DrawMomentumDiagram(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);
            Pen pen = new Pen(Color.Red, 1);

            MomentumEquation equation;
            double M;

            List<MomentumEquation> eqs = Beams.GetMomentumEquations(this);
            if (eqs == null) return;

            List<PointF> points = new List<PointF>();
            for(float x = 0f; x < (float)Length; x += 0.001f)
            {
                equation = eqs.Where(eq => eq.From <= x && x <= eq.To).First();
                M = equation.a * x * x + equation.b * x + equation.c;

                SideViewPosition p = new SideViewPosition(this, x, -M * SideViewScreenProperties.MomentumDiagramScale);
                points.Add(p.ToPointF());
            }
            graphs.DrawLines(pen, points.ToArray());

            equation = eqs.Where(eq => eq.From == 0).First();
            M = equation.c;
            SideViewPosition p1 = new SideViewPosition(this, 0, 0);
            SideViewPosition p2 = new SideViewPosition(this, 0, -M * SideViewScreenProperties.MomentumDiagramScale);
            graphs.DrawLine(pen, p1.ToPointF(), p2.ToPointF());

            equation = eqs.Where(eq => eq.To == Length).First();
            M = equation.a * Length * Length + equation.b * Length + equation.c;
            p1 = new SideViewPosition(this, Length, 0);
            p2 = new SideViewPosition(this, Length, -M * SideViewScreenProperties.MomentumDiagramScale);
            graphs.DrawLine(pen, p1.ToPointF(), p2.ToPointF());

            //draw lines
            foreach(MomentumEquation eq in eqs)
            {
                M = eq.a * eq.From * eq.From + eq.b * eq.From + eq.c;
                p1 = new SideViewPosition(this, eq.From, 0);
                p2 = new SideViewPosition(this, eq.From, -M * SideViewScreenProperties.MomentumDiagramScale);
                graphs.DrawLine(pen, p1.ToPointF(), p2.ToPointF());

                M = eq.a * eq.To * eq.To + eq.b * eq.To + eq.c;
                p1 = new SideViewPosition(this, eq.To, 0);
                p2 = new SideViewPosition(this, eq.To, -M * SideViewScreenProperties.MomentumDiagramScale);
                graphs.DrawLine(pen, p1.ToPointF(), p2.ToPointF());
            }

            //write values
            List<Momentum> momentums = Beams.GetImportantMomentums(this);
            foreach(Momentum momentum in momentums)
            {
                string s = momentum.Value.ToString("0.0") + " kN.m";
                Font font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                Brush brush = new SolidBrush(Color.Red);
                p1 = new SideViewPosition(this, momentum.X, -momentum.Value * SideViewScreenProperties.MomentumDiagramScale);
                graphs.DrawString(s, font, brush, new PointF(p1.ToPointF().X, p1.ToPointF().Y + 5));
            }

            // momentum equations
            if (SideViewScreenProperties.MomentumEquationsOn)
            {
                foreach(MomentumEquation eq in eqs)
                {
                    double x = (eq.From + eq.To) / 2;
                    M = eq.a * x * x + eq.b * x + eq.c;

                    Font font= new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                    Brush brush = new SolidBrush(Color.Black);
                    p1 = new SideViewPosition(this, x, -M * SideViewScreenProperties.MomentumDiagramScale);
                    graphs.DrawString(eq.Print(), font, brush, p1.ToPointF());
                }
            }

        }
        public override void Draw(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            //Body
            if (Selected)
                graphs.DrawLine(new Pen(Color.FromArgb(0, 0, 200), 8), P1.ToPointF(), P2.ToPointF());
            else 
                graphs.DrawLine(new Pen(Color.RoyalBlue, 6), P1.ToPointF(), P2.ToPointF());
            
            //Name
            string text = this.ToString() /*+ " - " + Length.ToString("0.00") + "m"*/;
            Font font = new Font("Verdana", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            IsoPosition point = new IsoPosition(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z + 0.5);
            graphs.DrawString(text, font, brush, point.ToPointF());
        }
        public override void DrawSideView(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            //Body
            SideViewPosition p1 = new SideViewPosition(this, 0, 0);
            SideViewPosition p2 = new SideViewPosition(this, Length, 0);
            graphs.DrawLine(new Pen(Color.RoyalBlue, 1), p1.ToPointF(), p2.ToPointF());

            //Supports
            Supports.ForEach(s => s.DrawSideView(backbuffer));

            if (SideViewScreenProperties.PointLoadsOn)
            {
                //PointLoads
                PointLoads.ForEach(l => l.DrawSideView(backbuffer));
                VirtualPointLoads.ForEach(l => l.DrawSideView(backbuffer));

                //Reactions
                Supports.ForEach(s => s.Reaction.DrawSideView(backbuffer));
            }

            if (SideViewScreenProperties.DistributedLoadsOn)
            {
                //DistributedLoads
                DistributedLoads.ForEach(l => l.DrawSideView(backbuffer));
                VirtualDistributedLoads.ForEach(l => l.DrawSideView(backbuffer));
            }

            //Name
            string text = this.ToString();
            Font font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            graphs.DrawString(text, font, brush, new Point(5, 5));

            //MomentumDiagram
            if(SideViewScreenProperties.MomentumDiagramOn)
                DrawMomentumDiagram(backbuffer);

            //DeformationDiagram
            if(SideViewScreenProperties.DeformationDiagramOn || SideViewScreenProperties.RotationDiagramOn)
                DrawEffects(backbuffer);

            //ShearDiagram
            if(SideViewScreenProperties.ShearDiagramOn)
                DrawShearDiagram(backbuffer);

            //Inner Beams
            InnerBeams.ForEach(b => b.DrawAsInnerBeam(backbuffer, this));
        }

        public void DrawAsInnerBeam(Bitmap backbuffer, Beam parent)
        {
            if (!Intersects(parent))
                throw new IsoException("Attempt to draw beam as inner of other beam that does not intersect.");

            Graphics graphs = Graphics.FromImage(backbuffer);

            SideViewPosition position = GetSideViewPosition(parent);

            float x = GetSelectionBox(parent).X;
            float y = GetSelectionBox(parent).Y;

            SideIcon icon = new SideIcon(ToString(), new Point((int)x, (int)y));
            if (!Picked)
                icon.BackColor = Color.FromArgb(50, 0, 0, 255);

            icon.DrawSideView(backbuffer);

        }
        public SideViewPosition GetSideViewPosition(Beam parent)
        {
            if (!Intersects(parent))
                throw new IsoException("Attempt get inner point of beam that does not intersect the parent.");

            return SideViewPosition.FromIsoPosition(GetIntersectionPoint(parent), parent);
        }
        public Rectangle GetSelectionBox(Beam parent)
        {
            SideViewPosition position = GetSideViewPosition(parent);

            float x;
            float y;

            if (position.X <= 0)
            {
                x = position.ToPointF().X - 40;
                y = position.ToPointF().Y;
            }
            else if (position.X >= parent.Length)
            {
                x = position.ToPointF().X + 10;
                y = position.ToPointF().Y;
            }
            else
            {
                x = position.ToPointF().X - 10;
                y = position.ToPointF().Y + 10;
            }

            return new Rectangle((int)x, (int)y, 24, 11);
        }
        public bool MouseInsideBox(Beam parent, PointF mousePosition)
        {
            return Util.PointIsInsideRectangle(mousePosition, GetSelectionBox(parent));
        }

        public override void RecognizeColumnSupports(List<Structure> structures)
        {
            List<Structure> columns = structures.Where(s => s is Column).ToList();
            foreach (Structure s in columns)
            {
                Column c = (Column)s;
                if (Intersects(c))
                {
                    IsoPosition intersection = GetIntersectionPoint(c);

                    if (!SupportsColumn(structures, c))
                    {
                        Support support = new ApoioFixo(intersection, this);
                        AddSupportTesting(support);
                    }
                }
            }
        }
        public override void RecognizeBeamSupports(List<Structure> structures)
        {
            List<Beam> beams = structures
                .Where(s => (s is Beam) && s != this)
                .Select(s => s as Beam)
                .ToList();

            foreach(Beam b in beams)
            {
                if(_supportBeams.Contains(b))
                {
                    Support support = new ApoioFixo(GetIntersectionPoint(b), this);
                    AddSupportTesting(support);
                    continue;
                }

                if (IsSupportedByBeam(b) && !_loadBeams.Contains(b))
                {
                    Support support = new ApoioFixo(GetIntersectionPoint(b), this);
                    AddSupportTesting(support);
                    continue;
                }
            }
        }
        public void RecognizePointLoads(List<Structure> structures)
        {
            List<Beam> beams = structures
                .Where(s => (s is Beam) && s != this)
                .Select(s => (s as Beam))
                .ToList();

            //Beams
            List<Support> supports = new List<Support>();
            beams.ForEach(b => b.Supports.ForEach(s => supports.Add(s)));
            foreach (Beam b in beams)
            {
                b.ComputeReactions();

                _supportColumns = structures
                .Where(s => s is Column)
                .Select(s => s as Column)
                .Where(c => Intersects(c) && b.Intersects(c))
                .Where(c => IsSupportedByColumn(c))
                .Where(c => b.IsSupportedByColumn(c))
                .ToList();

                if (_loadBeams.Contains(b))
                {
                    AddLoadTestingBeam(b);
                    continue;
                }

                if (_supportColumns.Count == 0 && !_supportBeams.Contains(b) && b.IsSupportedByBeam(this))
                {
                    AddLoadTestingBeam(b);
                    continue;
                }
            }

            //Columns
            supports.Clear();

            List<Column> supportedColumns = structures
                .Where(s => s is Column)
                .Select(s => s as Column)
                .Where(c => SupportsColumn(structures, c))
                .ToList();

            List<Column> supportColumns = structures
                .Where(s => s is Column)
                .Select(s => s as Column)
                .Where(c => IsSupportedByColumn(c))
                .ToList();

            List<Column> excludedColumns = new List<Column>();
            int count = supportedColumns.Count;
            for(int i = 0; i < count; i++)
            {
                IsoPosition intersection1 = GetIntersectionPoint(supportedColumns[i]);
                
                foreach(Column c2 in supportColumns)
                {
                    IsoPosition intersection2 = GetIntersectionPoint(c2);

                    if (intersection1.Equals(intersection2))
                        excludedColumns.Add(supportedColumns[i]);
                }
            }

            supportedColumns.RemoveAll(c => excludedColumns.Contains(c));

            supportedColumns.ForEach(c => c.Supports.ForEach(sup => supports.Add(sup)));
            supports = supports
                .Where(sup => HasPoint(sup.Position))
                .ToList();

            foreach (Support s in supports)
            {
                double reactY = s.Reaction.VectorY;
                Load pointLoad = new Load(s.Position, this, 0.0, -reactY, 0);
                pointLoad.SourceStructure = s.Structure;

                if (PointLoads.Contains(pointLoad))
                {
                    Load existing = PointLoads.Where(f => f.Equals(pointLoad)).FirstOrDefault();
                    RemovePointLoad(existing);
                }
                AddPointLoad(pointLoad);
            }

            ComputeReactions();
        }
        public void AddLoadTestingBeam(Beam loadBeam)
        {
            IsoPosition intersection = GetIntersectionPoint(loadBeam);
            Support sup = loadBeam.Supports
                .Where(s => s.Position.Equals(intersection))
                .FirstOrDefault();

            if (sup != null)
            {
                Load force = new Load(intersection, this, -sup.Reaction.VectorX, -sup.Reaction.VectorY, 0);
                force.SourceStructure = loadBeam;

                if (PointLoads.Contains(force))
                {
                    Load existing = PointLoads.Where(f => f.Equals(force)).FirstOrDefault();
                    RemovePointLoad(existing);
                }
                AddPointLoad(force);
            }
        }

        public Beam Clone()
        {
            Beam beam = new Beam(this.P1, this.P2);
            
            foreach(Support sup in Supports)
            {
                Support newSup;
                if(sup is ApoioMovel)
                    newSup = new ApoioMovel(sup.Position, beam);
                else if(sup is ApoioFixo)
                    newSup = new ApoioFixo(sup.Position, beam);
                else
                    newSup = new Engaste(sup.Position, beam);

                beam.AddSupport(newSup);
            }

            PointLoads.ForEach(l => beam.AddPointLoad(l));
            VirtualPointLoads.ForEach(l => beam.AddVirtualPointLoad(l));
            DistributedLoads.ForEach(l => beam.AddDistributedLoad(l));
            VirtualDistributedLoads.ForEach(l => beam.AddVirtualDistLoad(l));
            SupportPredicates.ForEach(p => beam.AddSupportPredicate(p));

            return beam;
        }
        private void ComputeHiperestaticReactions()
        {
            if (!IsStable) return;
            if (HiperestaticityDegree <= 0) return;

            List<Load> forces = new List<Load>();

            PointLoads.ForEach(f => forces.Add(f));
            VirtualPointLoads.ForEach(f => forces.Add(f));
            VirtualDistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            DistributedLoads.ForEach(l => forces.Add(l.ResultingForce));

            if (forces.Count == 0) return;

            //cleaning reactions
            Supports.ForEach(s => s.Reaction = new Load(s.InnerPoint, this));

            //separating supports by type
            List<Engaste> engastes = Supports.Where(s => s is Engaste).Select(s => s as Engaste).ToList();
            List<ApoioFixo> apoiosFixos = Supports.Where(s => s is ApoioFixo).Select(s => s as ApoioFixo).ToList();

            //if the beam has only two fixed supports
            if (Supports.Count == 2 && apoiosFixos.Count == 2)
            {
                Support supA = Supports
                        .OrderBy(s => s.InnerPoint)
                        .FirstOrDefault();

                Support supB = Supports
                    .OrderByDescending(s => s.InnerPoint)
                    .FirstOrDefault();

                double a1 = supA.InnerPoint;
                double b1 = supB.InnerPoint;

                double c1 = forces
                           .Select(p => p.VectorY * p.InnerPoint + p.Momentum)
                           .Sum();

                double c2 = forces.Select(p => p.VectorY).Sum();

                double[,] equation = new double[,]
                {
                {a1, b1, -c1 },
                {1, 1, -c2 }
                };

                double[] r = Calculus.TwoUnknownVariables(equation);

                supA.Reaction.VectorY = r[0];
                supB.Reaction.VectorY = r[1];

                ComputeHiperestaticHorizontalReactions();
                return;
            }

            //creating the parent beam
            Beam parent = this.Clone();

            //creating effects list

            List<Effort> parentEfforts = new List<Effort>();
            //removing engastes
            List<Engaste> parentEngastes = parent.Supports.Where(s => s is Engaste).Select(s => s as Engaste).ToList();
            foreach (Engaste e in parentEngastes)
            {
                if (parent.HiperestaticityDegree == 0) break;

                parent.RemoveSupport(e);
                parent.AddSupport(new ApoioFixo(e.Position, parent));

                Effort effort = new Effort();
                effort.Beam = parent;
                effort.EffectType = EffectType.Rotation;
                effort.EffectPoint = e.InnerPoint;
                parentEfforts.Add(effort);
            }

            //removing fixed supports
            List<ApoioFixo> parentFixos = parent.Supports.Where(s => s is ApoioFixo).Select(s => s as ApoioFixo).ToList();
            for (int i = 0; i < parentFixos.Count - 1; i++)
            {
                if (parent.HiperestaticityDegree == 0) break;

                parent.RemoveSupport(parentFixos[i]);
                parent.AddSupport(new ApoioMovel(parentFixos[i].Position, parent));
            }

            //removing mobile supports
            List<ApoioMovel> parentMoveis = parent.Supports.Where(s => s is ApoioMovel).Select(s => s as ApoioMovel).ToList();
            for (int i = 0; i < parentMoveis.Count - 1; i++)
            {
                if (parent.HiperestaticityDegree == 0) break;

                parent.RemoveSupport(parentMoveis[i]);

                Effort effort = new Effort();
                effort.Beam = parent;
                effort.EffectType = EffectType.Vdef;
                effort.EffectPoint = parentMoveis[i].InnerPoint;
                parentEfforts.Add(effort);
            }

            //computing parent reactions
            parent.ComputeIsostaticReactions();

            //computing parent deformations/rotations
            foreach (Effort e in parentEfforts)
            {
                if (e.EffectType == EffectType.Vdef)
                    e.Value = Beams.GetDeformationFuncEI(parent, e.EffectPoint, Direction.Vertical); 
                else
                    e.Value = Beams.GetRotationFuncEI(parent, e.EffectPoint);
            }

            //generating children beams
            List<Beam> children = new List<Beam>();
            foreach (Effort e in parentEfforts)
            {
                Beam child = new Beam(parent.P1, parent.P2);

                //generating child
                foreach (Support support in parent.Supports)
                {
                    IsoPosition position = support.Position;

                    if (support is ApoioMovel)
                        child.AddSupport(new ApoioMovel(position, child));
                    else if (support is ApoioFixo)
                        child.AddSupport(new ApoioFixo(position, child));
                    else
                        child.AddSupport(new Engaste(position, child));
                }

                //adding virtual load
                if (e.EffectType == EffectType.Rotation)
                    child.AddVirtualPointLoad(new Load(e.EffectPoint, child, 0, 0, 1));
                else
                    child.AddVirtualPointLoad(new Load(e.EffectPoint, child, 0, 1, 0));

                //computing reactions
                child.ComputeIsostaticReactions();

                //adding child to the list
                children.Add(child);
            }

            //computing children efforts

            List<Effort> childrenEfforts = new List<Effort>();
            foreach(Beam child in children)
            {
                foreach(Effort e in parentEfforts)
                {
                    Effort effort = new Effort();
                    effort.Beam = child;
                    effort.EffectPoint = e.EffectPoint;
                    effort.EffectType = e.EffectType;

                    if (e.EffectType == EffectType.Rotation)
                        effort.Value = Beams.GetRotationFuncEI(child, e.EffectPoint);
                    else
                        effort.Value = Beams.GetDeformationFuncEI(child, e.EffectPoint, Direction.Vertical);

                    Load load = child.VirtualPointLoads.First();
                    effort.EffortPoint = load.InnerPoint;
                    if (load.Momentum == 1)
                    {
                        effort.EffortType = EffortType.M;
                    }
                    else
                    {
                        effort.EffortType = EffortType.V;
                    }

                    childrenEfforts.Add(effort);
                }
            }

            //building equation system
            List<Effort> result = new List<Effort>();
            if (children.Count > 1)
            {
                Effort[,] eqs = new Effort[children.Count, children.Count + 1];

                for (int j = 0; j < eqs.GetLength(0); j++)
                {
                    Beam child = children[j];
                    List<Effort> childEfforts = childrenEfforts.Where(e => e.Beam == child).ToList();
                    for (int i = 0; i < childEfforts.Count; i++)
                        eqs[i, j] = childEfforts[i];
                }

                for(int i = 0; i < eqs.GetLength(0); i++)
                {
                    Effort parentEffort = new Effort();
                    parentEffort.Beam = parent;
                    parentEffort.EffectPoint = parentEfforts[i].EffectPoint;
                    parentEffort.EffortPoint = parentEfforts[i].EffortPoint;
                    parentEffort.EffectType = parentEfforts[i].EffectType;
                    parentEffort.EffortType = parentEfforts[i].EffortType;
                    parentEffort.Value = -parentEfforts[i].Value;
                    eqs[i, eqs.GetLength(1) - 1] = parentEffort;
                }

                result = Calculus.MultiEquation(eqs);
            }
            else
            {
                double value = -parentEfforts[0].Value / childrenEfforts[0].Value;
                result.Add(new Effort(value, childrenEfforts[0].EffortType, childrenEfforts[0].EffortPoint));
            }

            //inserting the result in the parent to compute the other reactions
            foreach (Effort e in result)
            {
                if (e.EffortType == EffortType.V)
                    parent.AddVirtualPointLoad(new Load(e.EffortPoint, parent, 0, e.Value, 0));
                else
                    parent.AddVirtualPointLoad(new Load(e.EffortPoint, parent, 0, 0, e.Value));
            }
            parent.ComputeIsostaticReactions();

            //inserting reactions into this beam
            foreach (Support supParent in parent.Supports)
            {
                Support sup = Supports.Where(s => s.InnerPoint == supParent.InnerPoint).FirstOrDefault();
                sup.Reaction.VectorY = supParent.Reaction.VectorY;
                sup.Reaction.Momentum = supParent.Reaction.Momentum;
            }

            //inserting result into this beam
            foreach (Effort e in result)
            {
                Support sup = Supports.Where(s => s.InnerPoint == e.EffortPoint).FirstOrDefault();

                if (e.EffortType == EffortType.V)
                    sup.Reaction.VectorY += e.Value;
                else
                    sup.Reaction.Momentum += e.Value;
            }

            ComputeHiperestaticHorizontalReactions();
        }
        private void ComputeHiperestaticHorizontalReactions()
        {
            if (!IsStable) return;
            if (HiperestaticityDegree <= 0) return;

            List<Load> forces = new List<Load>();

            PointLoads.ForEach(f => forces.Add(f));
            VirtualPointLoads.ForEach(f => forces.Add(f));
            VirtualDistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            DistributedLoads.ForEach(l => forces.Add(l.ResultingForce));

            if (forces.Count == 0) return;

            //cleaning reactions
            Supports.ForEach(s => s.Reaction.VectorX = 0);

            //separating supports by type
            List<Engaste> engastes = Supports.Where(s => s is Engaste).Select(s => s as Engaste).ToList();
            List<ApoioFixo> apoiosFixos = Supports.Where(s => s is ApoioFixo).Select(s => s as ApoioFixo).ToList();

            //creating the parent beam
            Beam parent = this.Clone();

            //creating effects list
            List<Effort> parentEfforts = new List<Effort>();

            //removing engastes
            List<Engaste> parentEngastes = parent.Supports.Where(s => s is Engaste).Select(s => s as Engaste).ToList();
            foreach (Engaste e in parentEngastes)
            {
                if (parent.HiperestaticityDegree == 0) break;

                parent.RemoveSupport(e);
                parent.AddSupport(new ApoioFixo(e.Position, parent));
            }

            //removing fixed supports
            List<ApoioFixo> parentFixos = parent.Supports.Where(s => s is ApoioFixo).Select(s => s as ApoioFixo).ToList();
            for (int i = 0; i < parentFixos.Count - 1; i++)
            {
                List<Support> engastesOrFixed = Supports.Where(s => s is Engaste || s is ApoioFixo).ToList();
                if (engastesOrFixed.Count == 1) break;

                parent.RemoveSupport(parentFixos[i]);
                parent.AddSupport(new ApoioMovel(parentFixos[i].Position, parent));

                Effort effort = new Effort();
                effort.Beam = parent;
                effort.EffectType = EffectType.Hdef;
                effort.EffectPoint = parentFixos[i].InnerPoint;
                parentEfforts.Add(effort);
            }

            //computing parent reactions
            parent.ComputeIsostaticReactions();

            //computing parent deformations
            foreach (Effort e in parentEfforts)
                e.Value = Beams.GetDeformationFuncAE(parent, e.EffectPoint);

            //generating children beams
            List<Beam> children = new List<Beam>();
            foreach (Effort e in parentEfforts)
            {
                Beam child = new Beam(parent.P1, parent.P2);

                //generating child
                foreach (Support support in parent.Supports)
                {
                    IsoPosition position = support.Position;

                    if (support is ApoioMovel)
                        child.AddSupport(new ApoioMovel(position, child));
                    else if (support is ApoioFixo)
                        child.AddSupport(new ApoioFixo(position, child));
                    else
                        child.AddSupport(new Engaste(position, child));
                }

                //adding virtual load
                child.AddVirtualPointLoad(new Load(e.EffectPoint, child, 1, 0, 0));

                //computing reactions
                child.ComputeIsostaticReactions();

                //adding child to the list
                children.Add(child);
            }

            //computing children efforts
            List<Effort> childrenEfforts = new List<Effort>();
            foreach (Beam child in children)
            {
                foreach (Effort e in parentEfforts)
                {
                    Effort effort = new Effort();
                    effort.Beam = child;
                    effort.EffectPoint = e.EffectPoint;
                    effort.EffectType = e.EffectType;
                    effort.Value = Beams.GetDeformationFuncAE(child, e.EffectPoint);

                    Load load = child.VirtualPointLoads.First();
                    effort.EffortPoint = load.InnerPoint;
                    effort.EffortType = EffortType.H;

                    childrenEfforts.Add(effort);
                }
            }

            //building equation system
            List<Effort> result = new List<Effort>();
            if (children.Count > 1)
            {
                Effort[,] eqs = new Effort[children.Count, children.Count + 1];

                for (int j = 0; j < eqs.GetLength(0); j++)
                {
                    Beam child = children[j];
                    List<Effort> childEfforts = childrenEfforts.Where(e => e.Beam == child).ToList();
                    for (int i = 0; i < childEfforts.Count; i++)
                        eqs[i, j] = childEfforts[i];
                }

                for (int i = 0; i < eqs.GetLength(0); i++)
                {
                    Effort parentEffort = new Effort();
                    parentEffort.Beam = parent;
                    parentEffort.EffectPoint = parentEfforts[i].EffectPoint;
                    parentEffort.EffortPoint = parentEfforts[i].EffortPoint;
                    parentEffort.EffectType = parentEfforts[i].EffectType;
                    parentEffort.EffortType = parentEfforts[i].EffortType;
                    parentEffort.Value = -parentEfforts[i].Value;
                    eqs[i, eqs.GetLength(1) - 1] = parentEffort;
                }

                result = Calculus.MultiEquation(eqs);
            }
            else
            {
                if (parentEfforts.Count > 0)
                {
                    double value = -parentEfforts[0].Value / childrenEfforts[0].Value;
                    result.Add(new Effort(value, childrenEfforts[0].EffortType, childrenEfforts[0].EffortPoint));
                }
            }

            //inserting the result in the parent to compute the other reactions
            foreach (Effort e in result)
                parent.AddVirtualPointLoad(new Load(e.EffortPoint, parent, e.Value, 0, 0));

            parent.ComputeIsostaticHorizontalReactions();

            //inserting reactions into this beam
            foreach (Support supParent in parent.Supports)
            {
                Support sup = Supports.Where(s => s.InnerPoint == supParent.InnerPoint).FirstOrDefault();
                sup.Reaction.VectorX = supParent.Reaction.VectorX;
            }

            //inserting result into this beam
            foreach (Effort e in result)
            {
                Support sup = Supports.Where(s => s.InnerPoint == e.EffortPoint).FirstOrDefault();

                sup.Reaction.VectorX += e.Value;
            }
        }
        private void ComputeIsostaticReactions()
        {
            if (!IsStable) return;
            if (HiperestaticityDegree != 0) return;

            List<Load> forces = new List<Load>();

            PointLoads.ForEach(f => forces.Add(f));
            VirtualPointLoads.ForEach(f => forces.Add(f));
            VirtualDistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            DistributedLoads.ForEach(l => forces.Add(l.ResultingForce));

            if (forces.Count == 0) return;

            int apoiosMoveis = Supports.Where(s => s is ApoioMovel).ToList().Count;
            int apoiosFixos = Supports.Where(s => s is ApoioFixo).ToList().Count;
            int engastes = Supports.Where(s => s is Engaste).ToList().Count;


            if (apoiosFixos == 1 & apoiosMoveis == 1)
            {
                Support supA = Supports
                        .OrderBy(s => s.InnerPoint)
                        .FirstOrDefault();

                Support supB = Supports
                    .OrderByDescending(s => s.InnerPoint)
                    .FirstOrDefault();

                double a1 = supA.InnerPoint;
                double b1 = supB.InnerPoint;

                double c1 = forces
                           .Select(p => p.VectorY * p.InnerPoint + p.Momentum)
                           .Sum();

                double c2 = forces.Select(p => p.VectorY).Sum();

                double[,] equation = new double[,]
                {
                {a1, b1, -c1 },
                {1, 1, -c2 }
                };

                double[] result = Calculus.TwoUnknownVariables(equation);

                supA.Reaction.VectorY = result[0];
                supB.Reaction.VectorY = result[1];


                //horizontal reaction
                Support fix = Supports.Where(sup => sup is ApoioFixo).FirstOrDefault();
                fix.Reaction.VectorX = forces.Select(f => f.VectorX).Sum();
            }

            else if (Supports.Count == 1 && engastes == 1)
            {
                double a1 = Supports[0].InnerPoint;
                double c1 = forces.Select(f => f.VectorY * f.InnerPoint + f.Momentum).Sum();

                double Va = -(forces.Select(f => f.VectorY).Sum());
                double Ma = -(c1 + a1 * Va);
                double Ha = -(forces.Select(f => f.VectorX).Sum());

                Supports[0].Reaction.VectorY = Va;
                Supports[0].Reaction.VectorX = Ha;
                Supports[0].Reaction.Momentum = Ma;
            }
        }
        public void ComputeIsostaticHorizontalReactions()
        {
            List<Support> sups = Supports.Where(s => s is Engaste || s is ApoioFixo).ToList();
            if (sups.Count != 1) return;

            List<Load> forces = new List<Load>();

            PointLoads.ForEach(f => forces.Add(f));
            VirtualPointLoads.ForEach(f => forces.Add(f));
            VirtualDistributedLoads.ForEach(l => forces.Add(l.ResultingForce));
            DistributedLoads.ForEach(l => forces.Add(l.ResultingForce));

            sups[0].Reaction.VectorX = -(forces.Select(f => f.VectorX).Sum());
        }
        public void ComputeReactions()
        {
            if (HiperestaticityDegree == 0)
                ComputeIsostaticReactions();
            else if (HiperestaticityDegree > 0)
                ComputeHiperestaticReactions();

            List<Support> engastesOrFixed = Supports.Where(s => s is Engaste || s is ApoioFixo).ToList();
            if (engastesOrFixed.Count == 1)
                ComputeIsostaticHorizontalReactions();
        }

        public override string ToString()
        {
            return "V" + Id.ToString();
        }
        public override int GetHashCode()
        {
            return "Beam".GetHashCode() + P1.GetHashCode() + P2.GetHashCode();
        }
        public override bool Equals(object o)
        {
            if (o == this) return true;
            if (o == null) return false;
            if (GetType() != o.GetType()) return false;
            Beam other = (Beam)o;
            return P1.Equals(other.P1) && P2.Equals(other.P2);
        }

        public override string GetFileString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<b>" + Id + "<b>");

            sb.Append(P1.GetFileString() + "<b>");
            sb.Append(P2.GetFileString() + "<b>");

            sb.Append("<lf>");
            VirtualPointLoads.ForEach(l => sb.Append(l.GetFileString() + "<lf>"));
            if (VirtualPointLoads.Count == 0) sb.Append("<lf>");
            sb.Append("<b>");

            sb.Append("<ld>");
            VirtualDistributedLoads.ForEach(l => sb.Append(l.GetFileString() + "<ld>"));
            if (VirtualDistributedLoads.Count == 0) sb.Append("<ld>");
            sb.Append("<b>");

            sb.Append("<lsp>");
            SupportPredicates.ForEach(sp => sb.Append(sp.GetFileString() + "<lsp>"));
            if (SupportPredicates.Count == 0) sb.Append("<lsp>");
            sb.Append("<b>");

            return sb.ToString();
        }
        public static Beam LoadComponent(string fileString)
        {
            if (!fileString.StartsWith("<b>"))
                throw new ArgumentException("File string is not $Beam.");

            string s = fileString.Trim();

            string[] props = s.Split(new string[] { "<b>" }, StringSplitOptions.RemoveEmptyEntries);

            int id;
            IsoPosition p1;
            IsoPosition p2;
            List<Iso.Load> virtualLoads;
            List<DistributedLoad> virtualDistLoads;
            List<SupportPredicate> supportPredicates;

            try
            {
                id = int.Parse(props[0]);
                p1 = IsoPosition.LoadComponent(props[1]);
                p2 = IsoPosition.LoadComponent(props[2]);
                virtualLoads = ListUnpacker.UnpackLoadList(props[3]);
                virtualDistLoads = ListUnpacker.UnpackDistributedLoadList(props[4]);
                supportPredicates = ListUnpacker.UnpackSupportPredicateList(props[5]);
            }
            catch
            {
                throw new ArgumentException("File string in the wrong format.");
            }

            Beam beam = new Beam(p1, p2);
            beam.Id = id;

            virtualLoads.ForEach(l => l.Structure = beam);
            virtualLoads.ForEach(l => beam.VirtualPointLoads.Add(l));
            virtualDistLoads.ForEach(l => l.Structure = beam);
            virtualDistLoads.ForEach(l => beam.VirtualDistributedLoads.Add(l));
            supportPredicates.ForEach(sp => beam.SupportPredicates.Add(sp));

            return beam;
        }
    }
}