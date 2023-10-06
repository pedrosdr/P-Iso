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
    public partial class FormSaveMessage : Form
    {
        public FormSaveMessage(string message)
        {
            InitializeComponent();
            Design();
            labelMsg.Text = message;
        }

        //METHODS
        private void Design()
        {
            //btnSave
            btnSave.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnSave.png");

            //btnDontSave
            btnDontSave.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnDontSave.png");

            //btnCancel
            btnCancel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnCancel.png");
        }

        //EVENTS
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            btnSave.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnSave_high.png");
        }
        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnSave.png");
        }

        private void btnDontSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
        private void btnDontSave_MouseEnter(object sender, EventArgs e)
        {
            btnDontSave.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnDontSave_high.png");
        }
        private void btnDontSave_MouseLeave(object sender, EventArgs e)
        {
            btnDontSave.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnDontSave.png");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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
    }
}
