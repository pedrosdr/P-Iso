using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class RotationEquation
    {
        //PROPERTIES
        public double From { get; set; }
        public double To { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
        public double d { get; set; }

        //CONSTRUCTORS
        public RotationEquation(double a, double b, double c, double d, double from, double to)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            From = from;
            To = to;
        }

        //METHODS
        public double GetRotation(double x)
        {
            return a * x * x * x + b * x * x + c * x + d;
        }
    }
}
