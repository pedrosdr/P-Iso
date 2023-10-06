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
    public partial class FormAddDistLoad : Form
    {
        //PROPERTIES
        public Manager Manager { get; set; }

        //CONTRUCTORS
        public FormAddDistLoad(Manager manager)
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
                double d1 = double.Parse(tbValue.Text);
                double d2 = double.Parse(tbP1.Text);
                double d3 = double.Parse(tbP2.Text);

                if (d1 == 0)
                {
                    FormMessage fm = new FormMessage("O valor da carga distribuída não deve ser igual a zero.");
                    fm.ShowDialog();
                    return false;
                }
                if(d2 == d3)
                {
                    FormMessage fm = new FormMessage("Os pontos inicial e final da carga distribuída não devem ser iguais.");
                    fm.ShowDialog();
                    return false;
                }

                LinearStructure s = Manager.SelectedItem as LinearStructure;

                if (d2 < 0 || d2 > s.Length || d3 < 0 || d3 > s.Length)
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

            double p1 = double.Parse(tbP1.Text);
            double p2 = double.Parse(tbP2.Text);
            double value = double.Parse(tbValue.Text);

            DistributedLoad load = new DistributedLoad(s, value, p1, p2);

            s.AddVirtualDistLoad(load);

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
