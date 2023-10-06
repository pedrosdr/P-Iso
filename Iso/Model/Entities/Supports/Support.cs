using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Iso
{
    public abstract class Support : InnerObject
    {
        //PROPERTIES
        public virtual Load Reaction { get; set; }
        public IsoPosition Position { get; set; }

        public double InnerPoint
        {
            get
            {
                if (!(Structure is LinearStructure))
                    throw new IsoException("Support.InnerPoint.Get: Structure is not LinearStructure.");

                double vectX;
                double vectY;
                double vectZ;

                LinearStructure s = (LinearStructure)Structure;
                if (s is Beam)
                {
                    vectX = Position.X - s.P1.X;
                    vectY = Position.Y - s.P1.Y;
                    vectZ = Position.Z - s.P1.Z;
                }
                else
                {
                    vectX = Position.X - s.LowerPoint.X;
                    vectY = Position.Y - s.LowerPoint.Y;
                    vectZ = Position.Z - s.LowerPoint.Z;
                }

                return Math.Sqrt(vectX * vectX + vectY * vectY + vectZ * vectZ);
            }
        }
        public SideViewPosition SideViewPosition
        {
            get
            {
                if (!(Structure is LinearStructure))
                    throw new IsoException("Support.SideViewPosition.Get: Structure is not LinearStructure.");

                LinearStructure s = (LinearStructure)Structure;
                if (s is Beam)
                    return new SideViewPosition(s, InnerPoint, 0);

                return new SideViewPosition(s, 0, InnerPoint);
            }
        }

        public override RectangleF SelectionBox
        {
            get
            {
                return new RectangleF(SideViewPosition.ToPointF().X - 10,
                                      SideViewPosition.ToPointF().Y,
                                      20,
                                      20);
            }
        }

        //CONSTRUCTORS
        public Support(IsoPosition position, Structure structure) : base(structure)
        {
            Position = position;
            Reaction = new Load(position, Structure);
        }

        //METHODS
        public override int GetHashCode()
        {
            return "Support".GetHashCode() + Position.GetHashCode();
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;
            if (this.GetType() != o.GetType()) return false;
            Support other = (Support)o;
            return Position.Equals(other.Position);
        }
    }
}
