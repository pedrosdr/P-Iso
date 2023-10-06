using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class CalculusException : Exception
    {
        //CONSTRUCTORS
        public CalculusException(string message) : base(message)
        {
        }
    }
}
