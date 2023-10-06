using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class SupportPredicate
    {
        //PROPERTIES
        public IsoPosition Position { get; set; }
        public SupportType SupportType { get; set; }

        //CONSTRUCTORS
        public SupportPredicate(SupportType type, IsoPosition position)
        {
            SupportType = type;
            Position = position;
        }

        //METHODS
        public bool Invoke(Support s)
        {
            return Position.Equals(s.Position);
        }

        public string GetFileString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<sp>");
            sb.Append(Position.GetFileString());
            sb.Append("<sp>");
            sb.Append(((int)SupportType).ToString());
            sb.Append("<sp>");

            return sb.ToString();
        }
        public static SupportPredicate LoadComponent(string fileString)
        {
            if (!fileString.StartsWith("<sp>"))
                throw new ArgumentException("file string is not SupportPredicate.");

            string s = fileString.Trim();

            string[] props = s.Split(new string[] { "<sp>" }, StringSplitOptions.RemoveEmptyEntries);

            IsoPosition position;
            SupportType type;
            try
            {
                position = IsoPosition.LoadComponent(props[0]);
                type = (SupportType)int.Parse(props[1]);
            }
            catch
            {
                throw new ArgumentException("File string in the wrong format.");
            }

            return new SupportPredicate(type, position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            SupportPredicate other = (SupportPredicate)obj;
            return Position.Equals(other.Position);
        }
    }
}
