using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Momentum
    {
        //PROPERTIES
        public double Value { get; set; }
        public double X { get; set; }

        //CONSTRUCTORS
        public Momentum()
        {
        }

        public Momentum(double value, double x)
        {
            Value = value;
            X = x;
        }

        //METHODS
        public override int GetHashCode()
        {
            return "Momentum".GetHashCode() + Value.GetHashCode() + X.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            Momentum other = (Momentum)obj;
            return Value.Equals(other.Value) && X.Equals(other.X);
        }
    }
}
