using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Iso
{
    public class PictureBoxSideView : PictureBox
    {
        //PROPERTIES
        public FormMain FormMain { get; set; }

        //CONSTRUCTORS
        public PictureBoxSideView(FormMain formMain) : base()
        {
            FormMain = formMain;

            MouseClick += PictureBoxSideView_MouseClick;
            MouseWheel += PictureBoxSideView_MouseWheel;
            DoubleClick += PictureBoxSideView_DoubleClick;
        }

        private void PictureBoxSideView_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta > 0)
                SideViewScreenProperties.MomentumDiagramScale += 0.005;
            else if(e.Delta < 0)
                SideViewScreenProperties.MomentumDiagramScale -= 0.005;

            FormMain.Manager.DrawSideView();
        }


        //EVENTS
        private void PictureBoxSideView_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            FormMain.Manager.UpdateSelectedInnerObjects(e.Location);

            FormMain.Manager.Update();
            FormMain.Manager.DrawSideView();
        }

        private void PictureBoxSideView_DoubleClick(object sender, EventArgs e)
        {
            Beam picked = FormMain.Manager.PickedBeam;
            if (picked == null) return;

            IDrawable selectedDrawable = FormMain.Manager.SelectedItem;
            if (selectedDrawable == null) return;
            if (!(selectedDrawable is Beam)) return;

            Beam selected = (Beam)selectedDrawable;

            FormBeamComunication formCom = new FormBeamComunication(FormMain.Manager, picked, selected);
            formCom.ShowDialog();
            
        }
    }
}
