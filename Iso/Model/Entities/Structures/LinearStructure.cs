using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public abstract class LinearStructure : Structure
    {
        //FIELDS
        private LinearEquation _equation;
        private List<Load> _pointLoads = new List<Load>();
        private List<Load> _virtualPointLoads = new List<Load>();
        private List<DistributedLoad> _distributedLoads = new List<DistributedLoad>();
        private List<DistributedLoad> _virtualDistributedLoads = new List<DistributedLoad>();
        private List<SupportPredicate> _supportPredicates = new List<SupportPredicate>();

        //PROPERTIES
        public IsoPosition P1 { get; set; }
        public IsoPosition P2 { get; set; }

        public double I { get; set; }
        public double E { get; set; }

        public LinearEquation Equation { get { return _equation; } }
        public double Length
        {
            get
            {
                double x = P1.X - P2.X;
                double y = P1.Y - P2.Y;
                double z = P1.Z - P2.Z;

                return Math.Sqrt(x * x + y * y + z * z);
            }
        }
        public IsoPosition MiddlePoint { get { return Equation.MiddlePoint; } }

        public List<Load> PointLoads
        {
            get { return _pointLoads; }
        }
        public List<Load> VirtualPointLoads
        {
            get { return _virtualPointLoads; }
        }
        public List<DistributedLoad> DistributedLoads
        {
            get { return _distributedLoads; }
        }
        public List<DistributedLoad> VirtualDistributedLoads
        {
            get { return _virtualDistributedLoads; }
        }

        public List<SupportPredicate> SupportPredicates
        {
            get { return _supportPredicates; }
        }
        public Predicate<Support> SupportSelectionPredicate { get; set; }

        //CONSTRUCTORS
        public LinearStructure(IsoPosition p1, IsoPosition p2)
        {
            P1 = p1;
            P2 = p2;

            AddPoint(p1);
            AddPoint(p2);

            _equation = new LinearEquation(p1, p2);
        }

        //METHODS
        public abstract override void Draw(Bitmap backbuffer);
        public abstract override void DrawSideView(Bitmap backbuffer);

        public void AddSupportPredicate(SupportPredicate predicate)
        {
            _supportPredicates.Add(predicate);
        }
        public void RemoveSupportPredicate(SupportPredicate predicate)
        {
            _supportPredicates.Remove(predicate);
        }
        public void AddSupportTesting(Support s)
        {
            bool added = false;
            foreach(SupportPredicate pred in _supportPredicates)
            {
                if (pred.Invoke(s))
                {
                    if(pred.SupportType == SupportType.ApoioMovel)
                    {
                        s = new ApoioMovel(s.Position, s.Structure);
                        AddSupport(s);
                        added = true;
                        break;
                    }
                    else if(pred.SupportType == SupportType.Engaste)
                    {
                        s = new Engaste(s.Position, s.Structure);
                        AddSupport(s);
                        added = true;
                        break;
                    }
                    else
                    {
                        s = new ApoioFixo(s.Position, s.Structure);
                        AddSupport(s);
                        added = true;
                        break;
                    }
                }
            }

            if (!added)
            {
                if(BeamProperties.SupportType == SupportType.ApoioMovel)
                {
                    s = new ApoioMovel(s.Position, s.Structure);
                    AddSupport(s);
                }
                else if(BeamProperties.SupportType == SupportType.ApoioFixo)
                {
                    s = new ApoioFixo(s.Position, s.Structure);
                    AddSupport(s);
                }
                else
                {
                    s = new Engaste(s.Position, s.Structure);
                    AddSupport(s);
                }
            }

            if(SupportSelectionPredicate != null)
            {
                if (SupportSelectionPredicate.Invoke(s))
                    s.Selected = true;
            }
        }

        public override bool RemoveInnerObject(InnerObject obj)
        {
            if (obj is Support)
            {
                FormMessage fm = new FormMessage("Não é possível usar este comando para remover um suporte.");
                fm.ShowDialog();
                return false;
            }
            else if (obj is Load)
                return RemovePointLoad(obj as Load);
            else if (obj is DistributedLoad)
                return RemoveDistributedLoad(obj as DistributedLoad);
            else
                return false;
        }

        public bool HasPoint(IsoPosition p)
        {
            bool pIsUnderLimits =
                Math.Round(p.X, 2) >= Math.Round(Util.GetLower(P1.X, P2.X), 2) && Math.Round(p.X, 2) <= Math.Round(Util.GetHigher(P1.X, P2.X), 2) &&
                Math.Round(p.Y, 2) >= Math.Round(Util.GetLower(P1.Y, P2.Y), 2) && Math.Round(p.Y, 2) <= Math.Round(Util.GetHigher(P1.Y, P2.Y), 2) &&
                Math.Round(p.Z, 2) >= Math.Round(Util.GetLower(P1.Z, P2.Z), 2) && Math.Round(p.Z, 2) <= Math.Round(Util.GetHigher(P1.Z, P2.Z), 2);

            return (Equation.HasPoint(p) && pIsUnderLimits);
        }
        public IsoPosition GetIntersectionPoint(LinearStructure structure)
        {
            return Equation.GetIntersectionPoint(structure.Equation);
        }
        public bool Intersects(LinearStructure s)
        {
            IsoPosition intersection = GetIntersectionPoint(s);

            if (intersection != null && HasPoint(intersection) && s.HasPoint(intersection))
                return true;

            return false;
        }

        protected bool AddPointLoad(Load load)
        {
            if (!HasPoint(load.Position)) return false;

            _pointLoads.Add(load);
            AddInnerObject(load);

            return true;
        }
        protected bool RemovePointLoad(Load load)
        {
            return (_pointLoads.Remove(load) && RemoveInnerObject(load));
        }

        public bool AddVirtualPointLoad(Load load)
        {
            if (!HasPoint(load.Position)) return false;

            _virtualPointLoads.Add(load);
            AddInnerObject(load);

            return true;
        }
        public bool RemoveVirtualPointLoad(Load load)
        {
            return (_virtualPointLoads.Remove(load) && RemoveInnerObject(load));
        }

        protected bool AddDistributedLoad(DistributedLoad load)
        {
            if (_distributedLoads.Contains(load)) return false;

            _distributedLoads.Add(load);
            AddInnerObject(load);
            return true;
        }
        protected bool RemoveDistributedLoad(DistributedLoad load)
        {
            return (_distributedLoads.Remove(load) && RemoveInnerObject(load));
        }

        public bool AddVirtualDistLoad(DistributedLoad load)
        {
            if (_virtualDistributedLoads.Contains(load)) return false;

            _virtualDistributedLoads.Add(load);
            AddInnerObject(load);
            return true;
        }
        public bool RemoveVirtualDistLoad(DistributedLoad load)
        {
            return (_virtualDistributedLoads.Remove(load) && RemoveInnerObject(load));
        }

        public override int GetHashCode()
        {
            return "LinearStructure".GetHashCode() +
                   P1.GetHashCode() +
                   P2.GetHashCode() +
                   Length.GetHashCode() +
                   MiddlePoint.GetHashCode();
        }
        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;
            if (this.GetType() != o.GetType()) return false;
            LinearStructure other = (LinearStructure)o;

            return P1.Equals(other.P1) &&
                   P2.Equals(other.P2) &&
                   Length.Equals(other.Length) &&
                   MiddlePoint.Equals(other.Length);
        }
    }
}
