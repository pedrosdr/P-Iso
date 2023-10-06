using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class ShearEquation
    {
        //PROPERTIES
        public double a { get; set; }
        public double b { get; set; }
        public double From { get; set; }
        public double To { get; set; }

        //CONSTRUCTORS
        public ShearEquation(double a, double b, double from, double to)
        {
            this.a = a;
            this.b = b;
            From = from;
            To = to;
        }

        //METHODS
        public double GetShearForce(double x)
        {
            return a * x + b;
        }
    }
}
