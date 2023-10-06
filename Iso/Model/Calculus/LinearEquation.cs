using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class LinearEquation
    {
        //FIELDS
        private double _xo;
        private double _yo;
        private double _zo;
        private double _a;
        private double _b;
        private double _c;

        //PROPERTIES
        public IsoPosition A { get; set; }
        public IsoPosition B { get; set; }
        public double xo { get { return _xo; } }
        public double yo { get { return _yo; } }
        public double zo { get { return _zo; } }
        public double a { get { return _a; } }
        public double b { get { return _b; } }
        public double c { get { return _c; } }


        public double[] Point
        {
            get
            {
                return new double[] { _xo, _yo, _zo };
            }
        }

        public double[] Vector
        {
            get
            {
                return new double[] { _a, _b, _c };
            }
        }

        public IsoPosition MiddlePoint
        {
            get
            {
                if (A.X != B.X)
                    return GetPoint(AxisType.X, (A.X + B.X) / 2);
                if (A.Y != B.Y)
                    return GetPoint(AxisType.Y, (A.Y + B.Y) / 2);
                if (A.Z != B.Z)
                    return GetPoint(AxisType.Z, (A.Z + B.Z) / 2);

                else return A;
            }
        }

        //CONSTRUCTORS
        public LinearEquation(IsoPosition A, IsoPosition B)
        {
            this.A = A;
            this.B = B;

            //Point P
            _xo = A.X;
            _yo = A.Y;
            _zo = A.Z;

            //Vector v
            _a = B.X - A.X;
            _b = B.Y - A.Y;
            _c = B.Z - A.Z;
        }

        //METHODS
        public IsoPosition GetPoint(AxisType axis, double value)
        {
            double x = double.NaN;
            double y = double.NaN;
            double z = double.NaN;

            if (axis == AxisType.X)
            {
                if (_a == 0)
                {
                    if (value == _xo)
                        return new IsoPosition(value, double.PositiveInfinity, double.PositiveInfinity);
                    else
                        return null;
                }
                if (_b == 0 && _c == 0)
                    return new IsoPosition(value, _yo, _zo);

                double lambda = (value - _xo) / _a;

                if (_b == 0)
                {
                    y = _yo;
                    z = _zo + lambda * _c;
                }
                else if (_c == 0)
                {
                    z = _zo;
                    y = _yo + lambda * _b;
                }
                else
                {
                    y = _yo + lambda * _b;
                    z = _zo + lambda * _c;
                }

                return new IsoPosition(value, y, z);
            }
            else if (axis == AxisType.Y)
            {
                if (_b == 0)
                {
                    if (value == _yo)
                        return new IsoPosition(double.PositiveInfinity, value, double.PositiveInfinity);
                    else
                        return null;
                }
                if (_a == 0 && _c == 0)
                    return new IsoPosition(_xo, value, _zo);

                double lambda = (value - _yo) / _b;

                if (_a == 0)
                {
                    x = _xo;
                    z = _zo + lambda * _c;
                }
                else if (_c == 0)
                {
                    z = _zo;
                    x = _xo + lambda * _a;
                }
                else
                {
                    x = _xo + lambda * _a;
                    z = _zo + lambda * _c;
                }

                return new IsoPosition(x, value, z);
            }
            else if (axis == AxisType.Z)
            {
                if (_c == 0)
                {
                    if (value == _zo)
                        return new IsoPosition(double.PositiveInfinity, double.PositiveInfinity, value);
                    else
                        return null;
                }
                if (_a == 0 && _b == 0)
                    return new IsoPosition(_xo, _yo, value);

                double lambda = (value - _zo) / _c;

                if (_a == 0)
                {
                    x = _xo;
                    y = _yo + lambda * _b;
                }
                else if (_b == 0)
                {
                    y = _yo;
                    x = _xo + lambda * _a;
                }
                else
                {
                    x = _xo + lambda * _a;
                    y = _yo + lambda * _b;
                }

                return new IsoPosition(x, y, value);
            }

            else
            {
                if (x == double.NaN || y == double.NaN || z == double.NaN)
                    throw new CalculusException("Non existing point in LinearEquation.GetPoint()");

                return null;
            }
        }
        public IsoPosition GetIntersectionPoint(LinearEquation eq)
        {
            if (eq == this) return null;

            double lambda1;
            double lambda2;

            double x = double.NaN;
            double y = double.NaN;
            double z = double.NaN;

            if (_a == 0 && eq.a == 0 && _b == 0 && eq.b == 0 && _c == 0 && eq.c == 0) //All lines = 0
            {
                return null;
            } //All lines = 0
            else if (_a == 0 && eq.a == 0 && _b == 0 && eq.b == 0) //L1, L2 = 0
            {
                if (_xo != eq.xo) return null;
                else if (_yo != eq.yo) return null;
                else if (_c == 0)
                {
                    lambda2 = (_zo - eq.zo) / eq.c;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.c == 0)
                {
                    lambda1 = (eq.zo - _zo) / _c;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
            } //L1, L2 = 0
            else if (_a == 0 && eq.a == 0 && _c == 0 && eq.c == 0) //L1, L3 = 0
            {
                if (_xo != eq.xo) return null;
                else if (_zo != eq.zo) return null;
                else if (_b == 0)
                {
                    lambda2 = (_yo - eq.yo) / eq.b;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.b == 0)
                {
                    lambda1 = (eq.yo - _yo) / _b;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
            } //L1, L3 = 0
            else if (_b == 0 && eq.b == 0 && _c == 0 && eq.c == 0) //L2, L3 = 0
            {
                if (_yo != eq.yo) return null;
                else if (_zo != eq.zo) return null;
                else if (_a == 0)
                {
                    lambda2 = (_xo - eq.xo) / eq.a;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.a == 0)
                {
                    lambda1 = (eq.xo - _xo) / _a;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else return null;
            } //L2, L3 = 0
            else if (_a == 0 && eq.a == 0) //L1 = 0
            {
                if (_xo != eq.xo) return null;
                else if (_b == 0)
                {
                    lambda2 = (_yo - eq.yo) / eq.b;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.b == 0)
                {
                    lambda1 = (eq.yo - _yo) / _b;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else if (_c == 0)
                {
                    lambda2 = (_zo - eq.zo) / eq.c;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.c == 0)
                {
                    lambda1 = (eq.zo - _zo) / _c;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else
                {
                    double[,] mat1 = new double[,]
                    {
                        {1, -eq.b / _b, (eq.yo - _yo) / _b },
                        {1, -eq.c / _c, (eq.zo - _zo) / _c },
                    };

                    lambda1 = Calculus.TwoUnknownVariables(mat1)[0];
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
            } //L1 = 0
            else if (_b == 0 && eq.b == 0) //L2
            {
                if (_yo != eq.yo) return null;
                else if (_a == 0)
                {
                    lambda2 = (_xo - eq.xo) / eq.a;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.a == 0)
                {
                    lambda1 = (eq.xo - _xo) / _a;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else if (_c == 0)
                {
                    lambda2 = (_zo - eq.zo) / eq.c;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.c == 0)
                {
                    lambda1 = (eq.zo - _zo) / _c;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else
                {
                    double[,] mat1 = new double[,]
                    {
                        {1, -eq.a / _a, (eq.xo - _xo) / _a },
                        {1, -eq.c / _c, (eq.zo - _zo) / _c },
                    };

                    lambda1 = Calculus.TwoUnknownVariables(mat1)[0];
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
            } //L2 = 0
            else if (_c == 0 && eq.c == 0) //L3 = 0
            {
                if (_zo != eq.zo) return null;
                else if (_a == 0)
                {
                    lambda2 = (_xo - eq.xo) / eq.a;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.a == 0)
                {
                    lambda1 = (eq.xo - _xo) / _a;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else if (_b == 0)
                {
                    lambda2 = (_yo - eq.yo) / eq.b;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.b == 0)
                {
                    lambda1 = (eq.yo - _yo) / _b;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else
                {
                    double[,] mat1 = new double[,]
                    {
                        {1, -eq.a / _a, (eq.xo - _xo) / _a },
                        {1, -eq.b / _b, (eq.yo - _yo) / _b }
                    };

                    lambda1 = Calculus.TwoUnknownVariables(mat1)[0];
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
            } //L3 = 0
            else
            {
                if (_a == 0)
                {
                    lambda2 = (_xo - eq.xo) / eq.a;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.a == 0)
                {
                    lambda1 = (eq.xo - _xo) / _a;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else if (_b == 0)
                {
                    lambda2 = (_yo - eq.yo) / eq.b;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.b == 0)
                {
                    lambda1 = (eq.yo - _yo) / _b;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else if (_c == 0)
                {
                    lambda2 = (_zo - eq.zo) / eq.c;
                    x = eq.xo + eq.a * lambda2;
                    y = eq.yo + eq.b * lambda2;
                    z = eq.zo + eq.c * lambda2;
                }
                else if (eq.c == 0)
                {
                    lambda1 = (eq.zo - _zo) / _c;
                    x = _xo + _a * lambda1;
                    y = _yo + _b * lambda1;
                    z = _zo + _c * lambda1;
                }
                else
                {

                    double[,] mat1 = new double[,]
                    {
                    {1, -(eq.a / _a), (eq.xo - _xo) / _a},
                    {1, -(eq.b / _b), (eq.yo - _yo) / _b}
                    };

                    double[,] mat2 = new double[,]
                    {
                    {1, -(eq.a / _a), (eq.xo - _xo) / _a},
                    {1, -(eq.c / _c), (eq.zo - _zo) / _c}
                    };

                    double upperLambda1 = Calculus.TwoUnknownVariables(mat1)[0];
                    double lowerLambda1 = Calculus.TwoUnknownVariables(mat2)[0];

                    if (upperLambda1 != lowerLambda1) return null;

                    x = _xo + _a * upperLambda1;
                    y = _yo + _b * upperLambda1;
                    z = _zo + _c * upperLambda1;
                }
            } //No line = 0

            return new IsoPosition(x, y, z);
        }
        public bool HasPoint(IsoPosition p)
        {
            IsoPosition realPoint = null;

            if (A.X != B.X)
                realPoint = GetPoint(AxisType.X, p.X);
            else if (A.Y != B.Y)
                realPoint = GetPoint(AxisType.Y, p.Y);
            else if (A.Z != B.Z)
                realPoint = GetPoint(AxisType.Z, p.Z);
            else
                realPoint = A;

            bool equals = p.Equals(realPoint);
            return equals;
        }
        public bool Overlaps(LinearEquation eq)
        {
            return HasPoint(eq.A) && HasPoint(eq.B);
        }
    }
}
