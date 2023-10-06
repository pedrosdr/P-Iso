using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class MomentumEquation
    {
        //PROPERTIES
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }

        public double From { get; set; }
        public double To { get; set; }

        public Momentum Max
        {
            get
            {
                double maxM = GetValue(From);
                double maxX = From;
                for(double f = From; f <= To; f += 0.01)
                {
                    double M = GetValue(f);
                    if(M > maxM)
                    {
                        maxM = M;
                        maxX = f;
                    }
                }

                return new Momentum(maxM, maxX);
            }
        }
        public Momentum Min
        {
            get
            {
                double minM = GetValue(From);
                double minX = From;
                for (double f = From; f <= To; f += 0.01)
                {
                    double M = GetValue(f);
                    if (M < minM)
                    {
                        minM = M;
                        minX = f;
                    }
                }

                return new Momentum(minM, minX);
            }
        }
        public Momentum MaxModular
        {
            get
            {
                Momentum max = new Momentum(Math.Sqrt(Max.Value * Max.Value), Max.X);
                Momentum min = new Momentum(Math.Sqrt(Min.Value * Min.Value), Min.X);

                if (max.Value > min.Value)
                    return max;
                else
                    return min;
            }
        }

        //CONSTRUCTORS
        public MomentumEquation(double a, double b, double c, double from, double to)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            From = from;
            To = to;
        }

        //METHODS
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("M = ");
            sb.Append(a.ToString("0.00") + "x² ");
            if (b >= 0) sb.Append("+");
            sb.Append(b.ToString("0.00" + "x "));
            if (c >= 0) sb.Append("+");
            sb.Append(c.ToString("0.00"));

            sb.Append(" [from ")
              .Append(From.ToString("0.00"))
              .Append("m to ")
              .Append(To.ToString("0.00"))
              .Append("m]");

            return sb.ToString();
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("M = ");

            if(a != 0)
                sb.Append(a.ToString("0.00") + "x² ");

            if (b > 0)
            {
                if(a != 0)
                    sb.Append("+");
                sb.Append(b.ToString("0.00" + "x "));
            }
            else if(b < 0)
            {
                sb.Append(b.ToString("0.00" + "x "));
            }

            if ((a != 0 || b != 0))
            {
                if (c != 0)
                {
                    if (c > 0)
                        sb.Append("+");

                    sb.Append(c.ToString("0.00"));
                }
            }
            else
            {
                sb.Append(c.ToString("0.00"));
            }

            return sb.ToString();
        }

        public double GetValue(double x)
        {
            return a * x * x + b * x + c;
        }
    }
}
