using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public static class Calculus
    {
        //Returns the values of two unknown variables of a system of two equations (Ex: 2x + 3y = 9)
        public static double[] TwoUnknownVariables(double[,] mat)
        {
            double a1 = mat[0, 0];
            double b1 = mat[0, 1];
            double c1 = mat[0, 2];

            double a2 = mat[1, 0];
            double b2 = mat[1, 1];
            double c2 = mat[1, 2];


            if (mat.GetLength(0) != 2) return null;
            if (mat.GetLength(1) != 3) return null;
            if (a1 == 0 && b1 == 0 && a2 == 0 && b2 == 0)
                return null;
            if (a1 == 0 && b1 == 0) return null;
            if (a2 == 0 && b2 == 0) return null;
            if (a1 == a2 && b1 == b2) return null;

            double x = 0;
            double y = 0;

            if (a1 == 0)
            {
                y = c1 / b1;
                x = (c2 - b2 * y) / a2;
            }
            else if (b1 == 0)
            {
                x = c1 / a1;
                y = (c2 - a2 * x) / b2;
            }
            else if (a2 == 0)
            {
                y = c2 / b2;
                x = (c1 - b1 * y) / a1;
            }
            else if (b2 == 0)
            {
                x = c2 / a2;
                y = (c1 - a1 * x) / b1;
            }
            else
            {
                x = ((c2 / a2) - ((b2 * c1) / (b1 * a2))) / (1 - ((b2 * a1) / (b1 * a2)));
                y = (c1 - a1 * x) / b1;
            }

            return new double[] { x, y };
        }

        /*Returns the values of n unknown variables of a system of n equations
        The calculus is based on escalation, therefore it can return NaN, +inf or -inf if
        any of the terms is equals to zero. If the number of unknown variables is equals to 2
        it's recomended using the method TwoUnknownVariables instead*/
        public static double[] MultiEquation(double[,] mat)
        {
            double acres = 0;
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Random random = new Random(acres.GetHashCode());
                    if (j > 0)
                        acres += mat[i, j - 1].GetHashCode() * random.NextDouble() * 0.0000000000001;
                    if (i > 0)
                        acres += mat[i - 1, j].GetHashCode() * random.NextDouble() * 0.0000000000001;

                    mat[i, j] += acres;
                }
            }

            int rows = mat.GetLength(0);
            int columns = mat.GetLength(1);

            if (rows < 2)
                throw new CalculusException("Matrix must have more than 1 row");
            if (columns - rows != 1)
                throw new CalculusException("Matrix in wrong format");

            int iLimit = 0;
            for (int j = 0; j < columns - 2; j++)
            {
                for (int i = rows - 1; i > iLimit; i--)
                {
                    double factor = mat[i - 1, j] / mat[i, j];

                    for (int k = 0; k < columns; k++)
                    {
                        double b = mat[i, k];
                        double c = mat[i - 1, k];

                        mat[i, k] = factor * b - c;
                    }
                }
                iLimit++;
            }

            double[] results = new double[rows];
            results[rows - 1] = mat[rows - 1, columns - 1] / mat[rows - 1, columns - 2];

            int varJPosition = columns - 3;
            int varPosition = rows - 1;
            for (int i = rows - 2; i > -1; i--)
            {
                double sum = 0.0;
                for (int j = 0; j < columns - 1; j++)
                {
                    if (j == varJPosition) continue;
                    if (mat[i, j] == 0) continue;
                    sum += results[j] * mat[i, j];
                }

                varPosition--;
                results[varPosition] = (mat[i, columns - 1] - sum) / mat[i, varJPosition];
                varJPosition--;
            }

            return results;
        }
        public static List<Effort> MultiEquation(Effort[,] mat)
        {
            double acres = 0;
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    Random random = new Random(acres.GetHashCode());
                    if (j > 0)
                        acres += mat[i, j - 1].GetHashCode() * random.NextDouble() * 0.0000000000001;
                    if (i > 0)
                        acres += mat[i - 1, j].GetHashCode() * random.NextDouble() * 0.0000000000001;

                    mat[i, j].Value += acres;
                }
            }

            int rows = mat.GetLength(0);
            int columns = mat.GetLength(1);

            if (rows < 2)
                throw new CalculusException("Matrix must have more than 1 row");
            if (columns - rows != 1)
                throw new CalculusException("Matrix in wrong format");

            int iLimit = 0;
            for (int j = 0; j < columns - 2; j++)
            {
                for (int i = rows - 1; i > iLimit; i--)
                {
                    double factor = mat[i - 1, j].Value / mat[i, j].Value;

                    for (int k = 0; k < columns; k++)
                    {
                        double b = mat[i, k].Value;
                        double c = mat[i - 1, k].Value;

                        mat[i, k].Value = factor * b - c;
                    }
                }
                iLimit++;
            }

            List<Effort> results = new List<Effort>();

            for(int i = 0; i < rows; i++)
                results.Add(new Effort(mat[0, i].EffortType, mat[0, i].EffortPoint));

            results[rows - 1].Value = mat[rows - 1, columns - 1].Value / mat[rows - 1, columns - 2].Value;

            int varJPosition = columns - 3;
            int varPosition = rows - 1;
            for (int i = rows - 2; i > -1; i--)
            {
                double sum = 0.0;
                for (int j = 0; j < columns - 1; j++)
                {
                    if (j == varJPosition) continue;
                    if (mat[i, j].Value == 0) continue;
                    sum += results[j].Value * mat[i, j].Value;
                }

                varPosition--;
                results[varPosition].Value = (mat[i, columns - 1].Value - sum) / mat[i, varJPosition].Value;
                varJPosition--;
            }

            return results;
        }

        //Returns the value of the integral of a simple function. (ex.: ax³ + bx² + cx + d)
        public static double SimpleIntegral(double[,] multiplierExpoentPair, double upperLimit, double LowerLimit)
        {
            if (multiplierExpoentPair == null) return double.NaN;
            if (multiplierExpoentPair.GetLength(1) != 2) return double.NaN;

            double[,] raw = multiplierExpoentPair;
            double[,] treated = new double[raw.GetLength(0),raw.GetLength(1)];

            for(int i = 0; i < raw.GetLength(0); i++)
            {
                double expoent = raw[i, 1] + 1;
                double multiplier = raw[i, 0] / expoent;
                treated[i, 0] = multiplier;
                treated[i, 1] = expoent;
            }

            double upperResult = 0.0;
            for(int i = 0; i < treated.GetLength(0); i++)
            {
                upperResult += treated[i, 0] * Math.Pow(upperLimit, treated[i, 1]);
            }

            double lowerResult = 0.0;
            for(int i = 0; i < treated.GetLength(0); i++)
            {
                lowerResult += treated[i, 0] * Math.Pow(LowerLimit, treated[i, 1]);
            }

            return upperResult - lowerResult;
        }

        public static double[,] SimpleIntegralEquation(double[,] multiplierExpoentPair)
        {
            if (multiplierExpoentPair == null) return null;
            if (multiplierExpoentPair.GetLength(1) != 2) return null;

            double[,] raw = multiplierExpoentPair;
            double[,] treated = new double[raw.GetLength(0), raw.GetLength(1)];

            for (int i = 0; i < raw.GetLength(0); i++)
            {
                double expoent = raw[i, 1] + 1;
                double multiplier = raw[i, 0] / expoent;
                treated[i, 0] = multiplier;
                treated[i, 1] = expoent;
            }

            return treated;
        }

        //Returns the derivative of a simple function. (ex.: ax³ + bx² + cx + d)
        public static double[,] SimpleDerivativeEquation(double[,] multiplierExpoentPair)
        {
            double[,] raw = multiplierExpoentPair;
            if (raw == null)
                throw new CalculusException("Matrix was null");
            if (raw.GetLength(1) != 2)
                throw new CalculusException("Wrong matrix format");

            double[,] treated = new double[raw.GetLength(0), 2];
            for(int i = 0; i < raw.GetLength(0); i++)
            {
                treated[i, 0] = raw[i, 0] * raw[i, 1];
                treated[i, 1] = raw[i, 1] - 1;
            }

            return treated;
        }

        //Returns the intersection point of the given linear equations
        public static IsoPosition GetIntersectionPoint(LinearEquation eq1, LinearEquation eq2)
        {
            double lambda1;
            double lambda2;

            double x = double.NaN;
            double y = double.NaN;
            double z = double.NaN;

            if (eq1.a == 0 && eq2.a == 0 && eq1.b == 0 && eq2.b == 0 && eq1.c == 0 && eq2.c == 0) //All lines = 0
            {
                return null;
            } //All lines = 0
            else if (eq1.a == 0 && eq2.a == 0 && eq1.b == 0 && eq2.b == 0) //L1, L2 = 0
            {
                if (eq1.xo != eq2.xo) return null;
                else if (eq1.yo != eq2.yo) return null;
                else if (eq1.c == 0)
                {
                    lambda2 = (eq1.zo - eq2.zo) / eq2.c;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.c == 0)
                {
                    lambda1 = (eq2.zo - eq1.zo) / eq1.c;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
            } //L1, L2 = 0
            else if (eq1.a == 0 && eq2.a == 0 && eq1.c == 0 && eq2.c == 0) //L1, L3 = 0
            {
                if (eq1.xo != eq2.xo) return null;
                else if (eq1.zo != eq2.zo) return null;
                else if (eq1.b == 0)
                {
                    lambda2 = (eq1.yo - eq2.yo) / eq2.b;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.b == 0)
                {
                    lambda1 = (eq2.yo - eq1.yo) / eq1.b;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
            } //L1, L3 = 0
            else if (eq1.b == 0 && eq2.b == 0 && eq1.c == 0 && eq2.c == 0) //L2, L3 = 0
            {
                if (eq1.yo != eq2.yo) return null;
                else if (eq1.zo != eq2.zo) return null;
                else if (eq1.a == 0)
                {
                    lambda2 = (eq1.xo - eq2.xo) / eq2.a;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.a == 0)
                {
                    lambda1 = (eq2.xo - eq1.xo) / eq1.a;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else return null;
            } //L2, L3 = 0
            else if (eq1.a == 0 && eq2.a == 0) //L1 = 0
            {
                if (eq1.xo != eq2.xo) return null;
                else if (eq1.b == 0)
                {
                    lambda2 = (eq1.yo - eq2.yo) / eq2.b;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.b == 0)
                {
                    lambda1 = (eq2.yo - eq1.yo) / eq1.b;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else if (eq1.c == 0)
                {
                    lambda2 = (eq1.zo - eq2.zo) / eq2.c;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.c == 0)
                {
                    lambda1 = (eq2.zo - eq1.zo) / eq1.c;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else
                {
                    double[,] mat1 = new double[,]
                    {
                        {1, -eq2.b / eq1.b, (eq2.yo - eq1.yo) / eq1.b },
                        {1, -eq2.c / eq1.c, (eq2.zo - eq1.zo) / eq1.c },
                    };

                    lambda1 = Calculus.MultiEquation(mat1)[0];
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
            } //L1 = 0
            else if (eq1.b == 0 && eq2.b == 0) //L2
            {
                if (eq1.yo != eq2.yo) return null;
                else if (eq1.a == 0)
                {
                    lambda2 = (eq1.xo - eq2.xo) / eq2.a;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.a == 0)
                {
                    lambda1 = (eq2.xo - eq1.xo) / eq1.a;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else if (eq1.c == 0)
                {
                    lambda2 = (eq1.zo - eq2.zo) / eq2.c;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.c == 0)
                {
                    lambda1 = (eq2.zo - eq1.zo) / eq1.c;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else
                {
                    double[,] mat1 = new double[,]
                    {
                        {1, -eq2.a / eq1.a, (eq2.xo - eq1.xo) / eq1.a },
                        {1, -eq2.c / eq1.c, (eq2.zo - eq1.zo) / eq1.c },
                    };

                    lambda1 = Calculus.MultiEquation(mat1)[0];
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
            } //L2 = 0
            else if (eq1.c == 0 && eq2.c == 0) //L3 = 0
            {
                if (eq1.zo != eq2.zo) return null;
                else if (eq1.a == 0)
                {
                    lambda2 = (eq1.xo - eq2.xo) / eq2.a;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.a == 0)
                {
                    lambda1 = (eq2.xo - eq1.xo) / eq1.a;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else if (eq1.b == 0)
                {
                    lambda2 = (eq1.yo - eq2.yo) / eq2.b;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.b == 0)
                {
                    lambda1 = (eq2.yo - eq1.yo) / eq1.b;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else
                {
                    double[,] mat1 = new double[,]
                    {
                        {1, -eq2.a / eq1.a, (eq2.xo - eq1.xo) / eq1.a },
                        {1, -eq2.b / eq1.b, (eq2.yo - eq1.yo) / eq1.b }
                    };

                    lambda1 = Calculus.MultiEquation(mat1)[0];
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
            } //L3 = 0
            else
            {
                if (eq1.a == 0)
                {
                    lambda2 = (eq1.xo - eq2.xo) / eq2.a;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.a == 0)
                {
                    lambda1 = (eq2.xo - eq1.xo) / eq1.a;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else if (eq1.b == 0)
                {
                    lambda2 = (eq1.yo - eq2.yo) / eq2.b;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.b == 0)
                {
                    lambda1 = (eq2.yo - eq1.yo) / eq1.b;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else if (eq1.c == 0)
                {
                    lambda2 = (eq1.zo - eq2.zo) / eq2.c;
                    x = eq2.xo + eq2.a * lambda2;
                    y = eq2.yo + eq2.b * lambda2;
                    z = eq2.zo + eq2.c * lambda2;
                }
                else if (eq2.c == 0)
                {
                    lambda1 = (eq2.zo - eq1.zo) / eq1.c;
                    x = eq1.xo + eq1.a * lambda1;
                    y = eq1.yo + eq1.b * lambda1;
                    z = eq1.zo + eq1.c * lambda1;
                }
                else
                {

                    double[,] mat1 = new double[,]
                    {
                    {1, -(eq2.a / eq1.a), (eq2.xo - eq1.xo) / eq1.a},
                    {1, -(eq2.b / eq1.b), (eq2.yo - eq1.yo) / eq1.b}
                    };

                    double[,] mat2 = new double[,]
                    {
                    {1, -(eq2.a / eq1.a), (eq2.xo - eq1.xo) / eq1.a},
                    {1, -(eq2.c / eq1.c), (eq2.zo - eq1.zo) / eq1.c}
                    };

                    double upperLambda1 = Calculus.MultiEquation(mat1)[0];
                    double lowerLambda1 = Calculus.MultiEquation(mat2)[0];

                    if (upperLambda1 != lowerLambda1) return null;

                    x = eq1.xo + eq1.a * upperLambda1;
                    y = eq1.yo + eq1.b * upperLambda1;
                    z = eq1.zo + eq1.c * upperLambda1;
                }
            } //No line = 0

            return new IsoPosition(x, y, z);
        }
    }
}

