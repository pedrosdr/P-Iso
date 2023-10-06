using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Effort
    {
        //PROPERTIES
        public double Value { get; set; }
        public EffortType EffortType { get; set; }
        public EffectType EffectType { get; set; }
        public double EffortPoint { get; set; }
        public double EffectPoint { get; set; }
        public Beam Beam { get; set; }

        //CONSTRUCTORS
        public Effort()
        {
        }

        public Effort(EffortType type, double loadPoint)
        {
            EffortType = type;
            EffortPoint = loadPoint;
            Value = 0;
        }

        public Effort(double value, EffortType type, double loadPoint)
        {
            EffortType = type;
            EffortPoint = loadPoint;
            Value = value;
        }

        public Effort(Beam beam, EffortType type, double loadPoint)
        {
            Beam = beam;
            EffortType = type;
            Value = 0;
        }

        public Effort(Beam beam, double value, EffortType type, double loadPoint)
        {
            Beam = beam;
            EffortType = type;
            EffortPoint = loadPoint;
            Value = value;
        }

        //METHODS
        public override int GetHashCode()
        {
            return "Effort".GetHashCode() +
                    EffortType.ToString().GetHashCode() +
                    EffortPoint.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;
            Effort other = (Effort)obj;

            return EffortType == other.EffortType &&
                   EffortPoint == other.EffortPoint;
        }
    }
}
