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
    public partial class FormInit : Form
    {
        //FIELDS
        private int _tick = 0;

        //CONSTRUCTORS
        public FormInit()
        {
            InitializeComponent();
            Design();
        }

        //METHODS
        private void Design()
        {
            BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\img\init.png");
            labelInit.Text = "Versão: " + ApplicationProperties.VERSION; 
        }

        private void InitializeProgram()
        {
            Show();
            Hide();
            FormMain main = new FormMain();
            main.Show();
        }

        //EVENTS
        private void timerInit_Tick(object sender, EventArgs e)
        {
            if(_tick == 1)
            {
                labelInit.Text = "Bem-vinda(o)!";
            }
            if (_tick == 2)
            {
                _tick = 0;
                InitializeProgram();
                timerInit.Stop();
            }
            _tick++;
        }
    }
}
