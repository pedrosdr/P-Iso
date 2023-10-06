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
    public partial class FormMessage : Form
    {
        //PROPERTIES
        public string Message { get; set; }

        //CONSTRUCTORS
        public FormMessage(string message)
        {
            InitializeComponent();
            Message = message;

            Initialize();
            Design();
        }

        //METHODS
        public void Initialize()
        {
            //labelMessage
            labelMessage.Text = Message;
        }
        public void Design()
        {
            //btnOk
            btnOk.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnOk.png");
        }

        public static DialogResult ShowMessage(string message)
        {
            FormMessage form = new FormMessage(message);
            return form.ShowDialog();
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
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
