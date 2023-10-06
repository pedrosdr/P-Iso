using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iso
{
    public class Column : LinearStructure
    {
        //CONSTRUCTORS
        public Column(IsoPosition p1, IsoPosition p2) : base(p1, p2)
        {
            Supports.Add(new Engaste(LowerPoint, this));
        }

        public override void Draw(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            //Body
            if (Selected)
                graphs.DrawLine(new Pen(Color.FromArgb(150, 150, 0), 8), P1.ToPointF(), P2.ToPointF());
            else
                graphs.DrawLine(new Pen(Color.FromArgb(200, 200, 0), 6), P1.ToPointF(), P2.ToPointF());

            //Name
            string text = "P" + Id.ToString();
            Font font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            IsoPosition point = new IsoPosition(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z + 0.5);
            //graphs.DrawString(text, font, brush, point.ToPointF());
        }
        public override void DrawSideView(Bitmap backbuffer)
        {
            Graphics graphs = Graphics.FromImage(backbuffer);

            //Body
            SideViewPosition p1 = SideViewPosition.FromIsoPosition(LowerPoint, this);
            SideViewPosition p2 = SideViewPosition.FromIsoPosition(HigherPoint, this);
            graphs.DrawLine(new Pen(Color.RoyalBlue, 1), p1.ToPointF(), p2.ToPointF());

            //Supports
            Supports.ForEach(s => s.DrawSideView(backbuffer));

            //FakeSupport
            FakeSupport fakesup = new FakeSupport(Length, this);
            fakesup.DrawSideView(backbuffer);

            if (SideViewScreenProperties.PointLoadsOn)
            {
                //PointLoads
                PointLoads.ForEach(l => l.DrawSideView(backbuffer));
                VirtualPointLoads.ForEach(l => l.DrawSideView(backbuffer));

                //Reactions
                Supports.ForEach(s => s.Reaction.DrawSideView(backbuffer));
            }

            if (SideViewScreenProperties.DistributedLoadsOn)
            {
                //DistributedLoads
                DistributedLoads.ForEach(l => l.DrawSideView(backbuffer));
                VirtualDistributedLoads.ForEach(l => l.DrawSideView(backbuffer));
            }

            //Name
            string text = this.ToString();
            Font font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            graphs.DrawString(text, font, brush, new Point(5, 5));
        }

        public bool SupportsColumn(Column column)
        {
            if (column.Supports.Count == 0) return false;
            return (HasPoint(column.Supports[0].Position));
        }

        public void RecognizeLoads(List<Structure> structures)
        {

            //beams
            PointLoads.Clear();

            List<Beam> beams = structures
                .Where(s => s is Beam)
                .Select(s => s as Beam)
                .Where(b => b.IsSupportedByColumn(this))
                .ToList();

            List<Support> supports = new List<Support>();
            beams.ForEach(b => b.Supports.ForEach(s => supports.Add(s)));
            supports = supports.Where(s => HasPoint(s.Position)).ToList();

            supports.ForEach(s => PointLoads.Add(new Load(s.Position, this, 0, -s.Reaction.VectorY, 0)));

            //columns
            List<Column> columns = structures
                .Where(s => s is Column)
                .Select(s => s as Column)
                .Where(c => c != this)
                .Where(c => SupportsColumn(c))
                .ToList();

            foreach(Column column in columns)
            {
                Load load = new Load();
                load.Structure = this;
                load.Position = column.Supports[0].Position;
                load.VectorY = -column.Supports[0].Reaction.VectorY;

                PointLoads.Add(load);
            }
        }
        public void ComputeReactions()
        {
            if (Supports.Count == 0)
                return;

            Support sup = Supports.First();

            List<Load> loads = new List<Load>();
            VirtualPointLoads.ForEach(l => loads.Add(l));
            PointLoads.ForEach(l => loads.Add(l));

            double reactionY = -loads.Select(l => l.VectorY).Sum();
            sup.Reaction.VectorY = reactionY;
        }

        public override string ToString()
        {
            return "P" + Id.ToString();
        }

        public override string GetFileString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<c>" + Id + "<c>");

            sb.Append(P1.GetFileString() + "<c>");
            sb.Append(P2.GetFileString() + "<c>");

            sb.Append("<lf>");
            VirtualPointLoads.ForEach(l => sb.Append(l.GetFileString() + "<lf>"));
            if (VirtualPointLoads.Count == 0) sb.Append("<lf>");
            sb.Append("<c>");

            sb.Append("<ld>");
            VirtualDistributedLoads.ForEach(l => sb.Append(l.GetFileString() + "<ld>"));
            if (VirtualDistributedLoads.Count == 0) sb.Append("<ld>");
            sb.Append("<c>");

            sb.Append("<lsp>");
            SupportPredicates.ForEach(sp => sb.Append(sp.GetFileString() + "<lsp>"));
            if (SupportPredicates.Count == 0) sb.Append("<lsp>");
            sb.Append("<c>");

            return sb.ToString();
        }
        public static Column LoadComponent(string fileString)
        {
            if (!fileString.StartsWith("<c>"))
                throw new ArgumentException("File string is not Column.");

            string s = fileString.Trim();

            string[] props = s.Split(new string[] { "<c>" }, StringSplitOptions.RemoveEmptyEntries);

            int id;
            IsoPosition p1;
            IsoPosition p2;
            List<Iso.Load> virtualLoads;
            List<DistributedLoad> virtualDistLoads;
            List<SupportPredicate> supportPredicates;

            try
            {
                id = int.Parse(props[0]);
                p1 = IsoPosition.LoadComponent(props[1]);
                p2 = IsoPosition.LoadComponent(props[2]);
                virtualLoads = ListUnpacker.UnpackLoadList(props[3]);
                virtualDistLoads = ListUnpacker.UnpackDistributedLoadList(props[4]);
                supportPredicates = ListUnpacker.UnpackSupportPredicateList(props[5]);
            }
            catch
            {
                throw new ArgumentException("File string in the wrong format.");
            }

            Column column = new Column(p1, p2);
            column.Id = id;

            virtualLoads.ForEach(l => l.Structure = column);
            virtualLoads.ForEach(l => column.VirtualPointLoads.Add(l));
            virtualDistLoads.ForEach(l => l.Structure = column);
            virtualDistLoads.ForEach(l => column.VirtualDistributedLoads.Add(l));
            supportPredicates.ForEach(sp => column.SupportPredicates.Add(sp));

            return column;
        }
    }
}
