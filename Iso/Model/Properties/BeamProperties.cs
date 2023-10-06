using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public static class BeamProperties
    {
        //FIELDS
        private static SupportType _supportType = SupportType.ApoioFixo;

        //PROPERTIES
        public static SupportType SupportType
        {
            get { return _supportType; }
            set { _supportType = value; }
        }
    }
}
