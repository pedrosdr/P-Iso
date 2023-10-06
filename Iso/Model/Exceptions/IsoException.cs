using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class IsoException : Exception
    {
        //CONSTRUCTORS
        public IsoException(string message) : base(message)
        {
        }
    }
}
