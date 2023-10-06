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
    public partial class FormAddLoadBeam : Form
    {
        //PROPERTIES
        public Manager Manager { get; set; }

        //CONSTRUCTORS
        public FormAddLoadBeam(Manager manager)
        {
            Manager = manager;
            InitializeComponent();
            Design();
        }

        //METHODS
        private void Design()
        {
            //btnAdd
            btnAdd.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAdd.png");

            //btnCancel
            btnCancel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnCancel.png");
        }

        private bool ControlsOk()
        {
            try
            {
                double d1 = double.Parse(tbVectX.Text);
                double d2 = double.Parse(tbVectY.Text);
                double d3 = double.Parse(tbMomentum.Text);
                double d4 = double.Parse(tbPosition.Text);

                if (d1 == 0 && d2 == 0 && d3 == 0)
                {
                    FormMessage fm = new FormMessage("Pelo menos um dos esforços não deve ser igual a zero.");
                    fm.ShowDialog();
                    return false;
                }

                if (!(Manager.SelectedItem is LinearStructure) || Manager.SelectedItem == null)
                    return false;

                LinearStructure s = Manager.SelectedItem as LinearStructure;

                if (d4 < 0 || d4 > s.Length)
                {
                    FormMessage fm = new FormMessage("O ponto especificado não está dentro dos limites da estrutura.");
                    fm.ShowDialog();
                    return false;
                }

                return true;
            }
            catch
            {
                FormMessage fm = new FormMessage("Os campos não estão preenchidos corretamente.");
                fm.ShowDialog();
                return false;
            }
        }

        //EVENTS
        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAdd_high.png");
        }
        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAdd.png");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ControlsOk()) return;

            LinearStructure s = Manager.SelectedItem as LinearStructure;

            double innerPoint = double.Parse(tbPosition.Text);
            double vectX = double.Parse(tbVectX.Text);
            double vectY = double.Parse(tbVectY.Text);
            double momentum = double.Parse(tbMomentum.Text);

            Iso.Load load = new Load(innerPoint, s, vectX, vectY, momentum);
            s.AddVirtualPointLoad(load);

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
