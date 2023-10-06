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
    public partial class FormEditSupport : Form
    {
        //FIELDS
        private Manager _manager;

        //CONSTRUCTORS
        public FormEditSupport(Manager manager)
        {
            _manager = manager;

            InitializeComponent();
            Initialize();
            Design();
        }

        //METHODS
        public void Initialize()
        {
            if(BeamProperties.SupportType == SupportType.ApoioMovel)
            {
                rbMovel.Checked = true;
            }
            else if(BeamProperties.SupportType == SupportType.ApoioFixo)
            {
                rbFixo.Checked = true;
            }
            else
            {
                rbEngaste.Checked = true;
            }
        }
        public void Design()
        {
            //btnOk
            btnOk.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnOk.png");

            //btnCancel
            btnCancel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnCancel.png");
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
            //Possible errors
            if(_manager == null)
            {
                FormMessage message = new FormMessage("Erro inesperado:" + Environment.NewLine + "O gerenciador era nulo.");
                message.ShowDialog();
                return;
            }
            else if(_manager.SelectedItem == null)
            {
                FormMessage message = new FormMessage("Erro inesperado:" + Environment.NewLine + "Nenhum item selecionado.");
                message.ShowDialog();
                return;
            }
            else if(!(_manager.SelectedItem is LinearStructure))
            {
                FormMessage message = new FormMessage("Erro inesperado:" + Environment.NewLine + "O item selecionado não era uma estrutura linear.");
                message.ShowDialog();
                return;
            }
            else if(_manager.SelectedSupport == null)
            {
                FormMessage message = new FormMessage("Erro inesperado:" + Environment.NewLine + "Nenhum suporte foi selecionado.");
                message.ShowDialog();
                return;
            }
            //
            //if no error occurred
            //
            LinearStructure structure = _manager.SelectedItem as LinearStructure;
            Support support = _manager.SelectedSupport;

            SupportType supportType = BeamProperties.SupportType;

            if (rbMovel.Checked)
                supportType = SupportType.ApoioMovel;
            else if (rbFixo.Checked)
                supportType = SupportType.ApoioFixo;
            else if (rbEngaste.Checked)
                supportType = SupportType.Engaste;

            SupportPredicate predicate = new SupportPredicate(supportType, support.Position);
            structure.SupportPredicates.RemoveAll(p => p.Equals(predicate));
            structure.SupportPredicates.Add(predicate);

            _manager.Update();
            DialogResult = DialogResult.OK;
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
