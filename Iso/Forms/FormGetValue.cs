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
    public partial class FormGetValue : Form
    {
        // fields
        private Beam _beam;

        // constructors
        public FormGetValue(Manager manager)
        {
            InitializeComponent();

            _beam = manager.SelectedItem as Beam;

            rbMomento.Checked = true;
            ShowResult();
        }

        // methods

        private void ShowResult()
        {
            try
            {
                double point = double.Parse(tbPoint.Text);

                if (rbMomento.Checked)
                {
                    tbResult.Text = _beam.GetMomento(point).ToString("0.00") + " kN.m";
                }
                else if (rbCortante.Checked)
                {
                    tbResult.Text = _beam.GetCortante(point).ToString("0.00") + " kN";
                }
                else if (rbRotacao.Checked)
                {
                    tbResult.Text = _beam.GetRotation(point).ToString("0.00") + "/EI";
                }
                else if (rbDeformacao.Checked)
                {
                    tbResult.Text = _beam.GetVerticalDeformation(point).ToString("0.00") + "/EI";
                }
            }
            catch { }
        }

        private void rbMomento_CheckedChanged(object sender, EventArgs e)
        {
            if(rbMomento.Checked)
                ShowResult();
        }

        private void rbCortante_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCortante.Checked)
                ShowResult();
        }

        private void rbRotacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRotacao.Checked)
                ShowResult();
        }

        private void rbDeformacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeformacao.Checked)
                ShowResult();
        }

        private void tbPoint_TextChanged(object sender, EventArgs e)
        {
            ShowResult();
        }
    }
}
