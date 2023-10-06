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
    public partial class FormBeamComunication : Form
    {
        //PROPERTIES
        public Manager Manager { get; set; }
        public Beam ChildBeam { get; set; }
        public Beam ParentBeam { get; set; }

        //CONSTRUCTORS
        public FormBeamComunication(Manager manager, Beam child, Beam parent)
        {
            Manager = manager;
            ChildBeam = child;
            ParentBeam = parent;

            InitializeComponent();
            Design();
        }

        //METHODS
        public void Design()
        {
            //btnOk
            btnOk.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnOk.png");

            //btnCancel
            btnCancel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnCancel.png");

            //rbParentSupChild
            rbParentSupChild.Text = ParentBeam.ToString() + " apoiando " + ChildBeam.ToString();

            //rbChildSupParent
            rbChildSupParent.Text = ChildBeam.ToString() + " apoiando " + ParentBeam.ToString();
        }

        //EVENTS
        private void btnOk_MouseEnter(object sender, EventArgs e)
        {
            btnOk.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnOk_high.png");
        }
        private void btnOk_MouseLeave(object sender, EventArgs e)
        {
            btnOk.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnOk.png");
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentBeam.SupportBeams.Remove(ChildBeam);
            ParentBeam.LoadBeams.Remove(ChildBeam);
            ChildBeam.LoadBeams.Remove(ParentBeam);
            ChildBeam.SupportBeams.Remove(ParentBeam);

            if (rbParentSupChild.Checked)
            {
                ParentBeam.SupportBeams.Remove(ChildBeam);
                ParentBeam.LoadBeams.Add(ChildBeam);
                ChildBeam.LoadBeams.Remove(ParentBeam);
                ChildBeam.SupportBeams.Add(ParentBeam);
            }
            else if (rbChildSupParent.Checked)
            {
                ParentBeam.SupportBeams.Add(ChildBeam);
                ParentBeam.LoadBeams.Remove(ChildBeam);
                ChildBeam.LoadBeams.Add(ParentBeam);
                ChildBeam.SupportBeams.Remove(ParentBeam);
            }

            Manager.Update();

            this.Close();
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnCancel_high.png");
        }
        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnCancel.png");
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
