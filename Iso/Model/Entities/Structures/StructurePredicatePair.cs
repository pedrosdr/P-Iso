using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public sealed class StructurePredicatePair<T>
    {
        //PROPERTIES
        public Structure Structure { get; set; }
        public Predicate<T> Predicate { get; set; }

        //CONSTRUCTORS
        public StructurePredicatePair(Structure structure, Predicate<T> predicate)
        {
            Structure = structure;
            Predicate = predicate;
        }
    }
}
