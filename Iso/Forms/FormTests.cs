using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Iso
{
    public partial class FormTests : Form
    {
        //FIELDS
        private PictureBox picScreen;
        private PictureBox picSideView;
        private Manager manager;
        public FormTests()
        {
            InitializeComponent();
            picScreen = new PictureBox();
            picSideView = new PictureBox();
            ScreenProperties.Screen = picScreen;
            SideViewScreenProperties.Screen = picSideView;
            manager = new Manager(picScreen, picSideView);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Beam beam = new Beam(new IsoPosition(0, 0, 3), new IsoPosition(8, 0, 3));
            //beam.AddSupport(new Engaste(new IsoPosition(0, 0, 3), beam));
            //beam.AddSupport(new ApoioFixo(new IsoPosition(4, 0, 3), beam));
            //beam.AddSupport(new ApoioMovel(new IsoPosition(8, 0, 3), beam));
            //beam.AddVirtualDistLoad(new DistributedLoad(beam, -10, 0, 8));

            //Beam beam = new Beam(new IsoPosition(0, 0, 3), new IsoPosition(8, 0, 3));
            //beam.AddSupport(new Engaste(new IsoPosition(0, 0, 3), beam));
            //beam.AddSupport(new ApoioFixo(new IsoPosition(4, 0, 3), beam));
            //beam.AddSupport(new Engaste(new IsoPosition(8, 0, 3), beam));
            //beam.AddVirtualDistLoad(new DistributedLoad(beam, -10, 0, 8));

            //beam.ComputeReactions();

            //Beam beam = new Beam(new IsoPosition(0, 0, 3), new IsoPosition(8, 0, 3));
            //beam.AddSupport(new ApoioFixo(new IsoPosition(0, 0, 3), beam));
            //beam.AddSupport(new ApoioMovel(new IsoPosition(4, 0, 3), beam));
            //beam.AddVirtualDistLoad(new DistributedLoad(beam, -10, 0, 8));

            //MessageBox.Show(Beams.GetRotationFuncEI(beam, 0).ToString());

            Beam beam = new Beam(new IsoPosition(0, 0, 3), new IsoPosition(8, 0, 3));
            beam.AddSupport(new Engaste(new IsoPosition(0, 0, 3), beam));
            beam.AddSupport(new ApoioFixo(new IsoPosition(4, 0, 3), beam));
            beam.AddSupport(new Engaste(new IsoPosition(8, 0, 3), beam));
            beam.AddVirtualDistLoad(new DistributedLoad(beam, -10, 0, 8));

            beam.ComputeReactions();
        }
    }
}
