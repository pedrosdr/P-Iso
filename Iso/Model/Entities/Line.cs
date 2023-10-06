using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Line
    {
        //FIELDS
        private LinearEquation _linearEquation;
        private IsoPosition _p1;
        private IsoPosition _p2;

        //PROPERTIES
        public LinearEquation LinearEquation { get { return _linearEquation; } }
        public IsoPosition P1
        {
            get { return _p1; }
        }
        public IsoPosition P2
        {
            get { return _p2; }
        }

        //CONSTRUCTORS
        public Line(IsoPosition p1, IsoPosition p2)
        {
            _p1 = p1;
            _p2 = p2;
            _linearEquation = new LinearEquation(_p1, _p2);
        }

        //METHODS

        public bool TotallyOverlapped(Line line)
        {
            if(!LinearEquation.Overlaps(line.LinearEquation)) return false;

            double lowerX = Util.GetLower(line.P1.X, line.P2.X);
            double lowerY = Util.GetLower(line.P1.Y, line.P2.Y);
            double lowerZ = Util.GetLower(line.P1.Z, line.P2.Z);

            double higherX = Util.GetHigher(line.P1.X, line.P2.X);
            double higherY = Util.GetHigher(line.P1.Y, line.P2.Y);
            double higherZ = Util.GetHigher(line.P1.Z, line.P2.Z);

            return
                P1.X >= lowerX && P1.X <= higherX &&
                P1.Y >= lowerY && P1.Y <= higherY &&
                P1.Z >= lowerZ && P1.Z <= higherZ
                &&
                P2.X >= lowerX && P2.X <= higherX &&
                P2.Y >= lowerY && P2.Y <= higherY &&
                P2.Z >= lowerZ && P2.Z <= higherZ;
        }
    }
}
