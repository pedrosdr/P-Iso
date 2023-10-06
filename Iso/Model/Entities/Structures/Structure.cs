using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public abstract class Structure : IDrawable, ISavable
    {
        //FIELDS
        private List<InnerObject> _innerObjects = new List<InnerObject>();
        private List<Support> _supports = new List<Support>();
        private List<IsoPosition> _points = new List<IsoPosition>();
        private bool _selected = false;
        private List<Structure> _structures = new List<Structure>();

        //PROPERTIES
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public List<Support> Supports
        {
            get { return _supports; }
        }

        public List<IsoPosition> Points
        {
            get { return _points; }
        }

        public List<InnerObject> InnerObjects
        {
            get { return _innerObjects; }
        }

        public IsoPosition HigherPoint
        {
            get
            {
                IsoPosition higher = Points[0];
                foreach (IsoPosition p in Points)
                {
                    if (p.Z > higher.Z)
                        higher = p;
                }
                return higher;
            }
        }

        public IsoPosition LowerPoint
        {
            get
            {
                IsoPosition lower = Points[0];
                foreach (IsoPosition p in Points)
                {
                    if (p.Z < lower.Z)
                        lower = p;
                }
                return lower;
            }
        }

        public List<Structure> Structures
        {
            get { return _structures; }
        }

        //METHODS
        public virtual bool AddInnerObject(InnerObject obj)
        {
            if (_innerObjects.Contains(obj))
                return false;

            _innerObjects.Add(obj);
            return true;
        }
        public virtual bool RemoveInnerObject(InnerObject obj)
        {
            return _innerObjects.Remove(obj);
        }

        public virtual void RecognizeColumnSupports(List<Structure> structures) { }
        public virtual void RecognizeBeamSupports(List<Structure> structures) { }
        public abstract void Draw(Bitmap backbuffer);
        public abstract void DrawSideView(Bitmap backbuffer);

        public bool AddSupport(Support support)
        {
            if (_supports.Contains(support))
                return false;

            _supports.Add(support);
            AddInnerObject(support);
            return true;
        }

        public bool RemoveSupport(Support support)
        {
            return _supports.Remove(support);
        }

        public void ClearSupports()
        {
            _supports.Clear();
        }

        public void AddPoint(IsoPosition p)
        {
            if (!_points.Contains(p))
                _points.Add(p);
        }

        public abstract string GetFileString();
    }
}
