using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public static class ListUnpacker
    {
        public static List<Load> UnpackLoadList(string fileString)
        {
            if (string.IsNullOrEmpty(fileString)) return null;

            if (!fileString.StartsWith("<lf>"))
                throw new ArgumentException("fileString is not List<Load>.");

            List<Load> list = new List<Load>();

            string[] loads = fileString.Split(new string[] { "<lf>" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string load in loads)
                list.Add(Load.LoadComponent(load));

            return list;
        }

        public static List<DistributedLoad> UnpackDistributedLoadList(string fileString)
        {
            if (string.IsNullOrEmpty(fileString)) return null;

            if (!fileString.StartsWith("<ld>"))
                throw new ArgumentException("fileString is not List<DistributedLoad>.");

            List<DistributedLoad> list = new List<DistributedLoad>();

            string[] dists = fileString.Split(new string[] { "<ld>" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string dist in dists)
                list.Add(DistributedLoad.LoadComponent(dist));

            return list;
        }

        public static List<SupportPredicate> UnpackSupportPredicateList(string fileString)
        {
            if (string.IsNullOrEmpty(fileString)) return null;

            if (!fileString.StartsWith("<lsp>"))
                throw new ArgumentException("fileString is not List<SupportPredicate>.");

            List<SupportPredicate> list = new List<SupportPredicate>();

            string[] sps = fileString.Split(new string[] { "<lsp>" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sp in sps)
                list.Add(SupportPredicate.LoadComponent(sp));

            return list;
        }

        public static List<IsoPosition> UnpackIsoPositionList(string fileString)
        {
            if (string.IsNullOrEmpty(fileString)) return null;

            if (!fileString.StartsWith("<lp>"))
                throw new ArgumentException("fileString is not List<IsoPosition>.");

            List<IsoPosition> list = new List<IsoPosition>();

            string[] ps = fileString.Split(new string[] { "<lp>" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string p in ps)
                list.Add(IsoPosition.LoadComponent(p));

            return list;
        }
    }
}
