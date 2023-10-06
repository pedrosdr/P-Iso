using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class DeformationEquation
    {
        //PROPERTIES
        public double From { get; set; }
        public double To { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
        public double d { get; set; }
        public double e { get; set; }

        //CONSTRUCTOR
        public DeformationEquation(double a, double b, double c, double d, double e, double from, double to)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            From = from;
            To = to;
        }

        //METHODS
        public double GetDeformation(double x)
        {
            return a * Math.Pow(x, 4) + b * Math.Pow(x, 3) + c * Math.Pow(x, 2) + d * x + e;
        }
    }
}
