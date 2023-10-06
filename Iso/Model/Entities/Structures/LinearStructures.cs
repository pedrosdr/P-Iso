using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public static class LinearStructures
    {
        public static IsoPosition GetIntersectionPoint(LinearStructure s1, LinearStructure s2)
        {
            return Calculus.GetIntersectionPoint(s1.Equation, s2.Equation);
        }

        public static bool Intersect(LinearStructure s1, LinearStructure s2)
        {
            IsoPosition intersection = GetIntersectionPoint(s1, s2);

            if (intersection != null && s1.HasPoint(intersection) && s2.HasPoint(intersection)) 
                return true;

            return false;
        } 
    }
}
