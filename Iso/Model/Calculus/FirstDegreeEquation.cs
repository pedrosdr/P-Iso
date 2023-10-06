using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class FirstDegreeEquation
    {
        //PROPERTIES
        public double a { get; set; }
        public double b { get; set; }

        //CONSTRUCTORS
        public FirstDegreeEquation()
        {

        }
        public FirstDegreeEquation(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        //METHODS
        public double GetY(double x)
        {
            return a * x + b;
        }
        public double GetX(double y)
        {
            return (y - b) / a;
        }

        public static FirstDegreeEquation FromValues(double x1, double y1, double x2, double y2)
        {
            double[,] mat = new double[,]
            {
                {x1, 1, y1 },
                {x2, 1, y2 }
            };

            double[] result = Calculus.TwoUnknownVariables(mat);
            return new FirstDegreeEquation(result[0], result[1]);
        }
    }
}
