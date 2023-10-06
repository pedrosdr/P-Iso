using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Effect
    {
        //PROPERTIES
        public double VerticalDeformation { get; set; }
        public double HorizontalDeformation { get; set; }
        public double Rotation { get; set; }
        public double X { get; set; }

        //CONSTRUCTORS
        public Effect()
        {
        }

        //METHODS
        public string PrintRotation()
        {
            return "θ = " + Rotation.ToString("0.00") + "/EI";
        }
        public string PrintVerticalDeformation()
        {
            return "Δv = " + VerticalDeformation.ToString("0.00") + "/EI";
        }
        public string PrintHorizontalDeformation()
        {
            return "Δh = " + HorizontalDeformation.ToString("0.00") + "/EI";
        }

        public override int GetHashCode()
        {
            return "Effect".GetHashCode() + X.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            Effect other = (Effect)obj;
            return other.X + 0.01 >= X && other.X - 0.01 <= X;
        }
    }
}
