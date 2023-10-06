using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Iso
{
    public class IsoSplitContainer : SplitContainer
    {
        //FIELDS
        private DoubleTextBox tbMeasure;
        private Label labelTbMeasure;

        private PictureBox btnDeleteLoad;
        private PictureBox btnEditSupport;
        private PictureBox btnShowMomentumDiagram;
        private PictureBox btnShowShearDiagram;
        private PictureBox btnShowPointLoads;
        private PictureBox btnShowDistLoads;
        private PictureBox btnShowDef;
        private PictureBox btnShowRot;

        //PROPERTIES
        public FormMain FormMain {get; set;}
        public PictureBoxScreen PicScreen { get; set; }
        public PictureBoxSideView PicSideView { get; set; }
        
        public DoubleTextBox TextBoxMeasure
        {
            get { return tbMeasure; }
        }
        public Label LabelMeasure
        {
            get { return labelTbMeasure; }
        }

        //CONSTRUCTORS
        public IsoSplitContainer
        (PictureBoxSideView picSideView, PictureBoxScreen picScreen, FormMain formMain) : base()
        {
            FormMain = formMain;
            PicScreen = picScreen;
            PicSideView = picSideView;

            Panel1.Controls.Add(picScreen);
            Panel2.Controls.Add(PicSideView);

            Initialize();
            Design();

            SplitterMoved += IsoSplitContainer_SplitterMoved;
        }

        //METHODS
        public void Initialize()
        {
            //picSideView
            PicSideView.BackgroundImageChanged += PicSideView_BackgroundImageChanged;

            //picScreen
            PicScreen.GotFocus += PicScreen_GotFocus;

            //TextBox Measure
            tbMeasure = new DoubleTextBox();
            tbMeasure.Text = null;
            tbMeasure.TextAlign = HorizontalAlignment.Left;
            Panel2.Controls.Add(tbMeasure);
            tbMeasure.LostFocus += TbMeasure_LostFocus;
            tbMeasure.KeyDown += TbMeasure_KeyDown;

            //Label tbMeasure
            labelTbMeasure = new Label();
            labelTbMeasure.Text = "comandos:";
            Panel2.Controls.Add(labelTbMeasure);

            //btnDeleteLoad
            btnDeleteLoad = new PictureBox();
            btnDeleteLoad.BackgroundImageLayout = ImageLayout.Stretch;
            btnDeleteLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnDeleteLoad.png");
            Panel2.Controls.Add(btnDeleteLoad);
            btnDeleteLoad.MouseEnter += BtnDeleteLoad_MouseEnter;
            btnDeleteLoad.MouseLeave += BtnDeleteLoad_MouseLeave;
            btnDeleteLoad.Click += BtnDeleteLoad_Click;
            btnDeleteLoad.EnabledChanged += BtnDeleteLoad_EnabledChanged;

            //btnEditSupport
            btnEditSupport = new PictureBox();
            btnEditSupport.BackgroundImageLayout = ImageLayout.Stretch;
            btnEditSupport.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnEditSupport.png");
            Panel2.Controls.Add(btnEditSupport);
            btnEditSupport.MouseEnter += BtnEditSupport_MouseEnter;
            btnEditSupport.MouseLeave += BtnEditSupport_MouseLeave;
            btnEditSupport.Click += BtnEditSupport_Click;
            btnEditSupport.EnabledChanged += BtnEditSupport_EnabledChanged;

            //btnShowMomentumDiagram
            btnShowMomentumDiagram = new PictureBox();
            btnShowMomentumDiagram.BackgroundImageLayout = ImageLayout.Stretch;
            if (SideViewScreenProperties.MomentumDiagramOn)
                btnShowMomentumDiagram.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowMomentumDiagram_high.png");
            else
                btnShowMomentumDiagram.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowMomentumDiagram.png");
            Panel2.Controls.Add(btnShowMomentumDiagram);
            btnShowMomentumDiagram.Click += BtnShowMomentumDiagram_Click;

            //btnShowShearDiagram
            btnShowShearDiagram = new PictureBox();
            btnShowShearDiagram.BackgroundImageLayout = ImageLayout.Stretch;
            if (SideViewScreenProperties.ShearDiagramOn)
                btnShowShearDiagram.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowShearDiagram_high.png");
            else
                btnShowShearDiagram.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowShearDiagram.png");
            Panel2.Controls.Add(btnShowShearDiagram);
            btnShowShearDiagram.Click += BtnShowShearDiagram_Click;

            //btnShowPointLoads
            btnShowPointLoads = new PictureBox();
            btnShowPointLoads.BackgroundImageLayout = ImageLayout.Stretch;
            if (SideViewScreenProperties.PointLoadsOn)
                btnShowPointLoads.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowPointLoads_high.png");
            else
                btnShowPointLoads.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowPointLoads.png");
            Panel2.Controls.Add(btnShowPointLoads);
            btnShowPointLoads.Click += BtnShowPointLoads_Click;

            //btnShowDistLoads
            btnShowDistLoads = new PictureBox();
            btnShowDistLoads.BackgroundImageLayout = ImageLayout.Stretch;
            if (SideViewScreenProperties.DistributedLoadsOn)
                btnShowDistLoads.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowDistLoads_high.png");
            else
                btnShowDistLoads.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowDistLoads.png");
            Panel2.Controls.Add(btnShowDistLoads);
            btnShowDistLoads.Click += BtnShowDistLoads_Click;

            //btnShowRot
            btnShowRot = new PictureBox();
            btnShowRot.BackgroundImageLayout = ImageLayout.Stretch;
            if (SideViewScreenProperties.RotationDiagramOn)
                btnShowRot.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowRot_high.png");
            else
                btnShowRot.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowRot.png");
            Panel2.Controls.Add(btnShowRot);
            btnShowRot.Click += BtnShowRot_Click;

            //btnShowDef
            btnShowDef = new PictureBox();
            btnShowDef.BackgroundImageLayout = ImageLayout.Stretch;
            if (SideViewScreenProperties.DeformationDiagramOn)
                btnShowDef.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowDef_high.png");
            else
                btnShowDef.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowDef.png");
            Panel2.Controls.Add(btnShowDef);
            btnShowDef.Click += BtnShowDef_Click;
        }

        private void Design()
        {
            int btnSize = 23;

            //this
            SplitterWidth = 3;

            //PicSideView
            PicSideView.Location = new Point(6 + btnSize + 5, 20);
            PicSideView.Width = Panel2.Width - 2 * btnSize - 25;
            PicSideView.Height = (int)(PicSideView.Width / 1.62);
            if (PicSideView.Height >= Panel2.Height - 90)
            {
                PicSideView.Height = Panel2.Height - 90;
                PicSideView.Width = (int)(PicSideView.Height * 1.62);
            }

            //Panel2
            Bitmap backbuffer = new Bitmap(Panel2.Width, Panel2.Height);
            Graphics graphs = Graphics.FromImage(backbuffer);
            graphs.Clear(Color.FromArgb(250, 250, 250));

            graphs.DrawLine
                (
                    new Pen(Color.FromArgb(190, 190, 190)),
                    new Point(0, 0),
                    new Point(0, Panel2.Height)
                );

            graphs.DrawRectangle
                (
                    new Pen(Color.FromArgb(190, 190, 190)),
                    new Rectangle(PicSideView.Location.X - 1, PicSideView.Location.Y - 1, PicSideView.Width + 1, PicSideView.Height + 1)
                );

            Panel2.BackgroundImage = backbuffer;

            //PicScreen
            PicScreen.Location = new Point(0, 7);
            PicScreen.Size = new Size(Panel1.Width, Panel1.Height);

            //TextBox Measure
            tbMeasure.Location = new Point(5, Panel2.Height - 30);
            tbMeasure.Width = Panel2.Width - 10;

            //LabelTbMeasure
            labelTbMeasure.Location = new Point(5, Panel2.Height - 50);
            labelTbMeasure.BackColor = Color.Transparent;
            labelTbMeasure.AutoSize = true;

            //btnDeleteLoad
            btnDeleteLoad.Size = new Size(btnSize, btnSize);
            btnDeleteLoad.Location = new Point(PicSideView.Location.X + PicSideView.Width + 7, PicSideView.Location.Y + PicSideView.Height - btnDeleteLoad.Height);

            //btnEditSupport
            btnEditSupport.Location = new Point(PicSideView.Location.X + PicSideView.Width + 5, PicSideView.Location.Y);
            btnEditSupport.Size = new Size(btnSize, btnSize);

            //btnShowMomentumDiagram
            btnShowMomentumDiagram.Size = new Size(btnSize, btnSize);
            btnShowMomentumDiagram.Location = new Point(5, PicSideView.Location.Y);

            //btnShowShearDiagram
            btnShowShearDiagram.Size = new Size(btnSize, btnSize);
            btnShowShearDiagram.Location = new Point(btnShowMomentumDiagram.Location.X, btnShowMomentumDiagram.Location.Y + btnShowMomentumDiagram.Height + 7);

            //btnShowPointLoads
            btnShowPointLoads.Size = new Size(btnSize, btnSize);
            btnShowPointLoads.Location = new Point(btnShowShearDiagram.Location.X, btnShowShearDiagram.Location.Y + btnShowShearDiagram.Height + 7);

            //btnShowDistLoads
            btnShowDistLoads.Size = new Size(btnSize, btnSize);
            btnShowDistLoads.Location = new Point(btnShowPointLoads.Location.X, btnShowPointLoads.Location.Y + btnShowPointLoads.Height + 7);

            //btnShowRot
            btnShowRot.Size = new Size(btnSize, btnSize);
            btnShowRot.Location = new Point(btnShowDistLoads.Location.X, btnShowDistLoads.Location.Y + btnShowDistLoads.Height + 7);

            //btnShowDef
            btnShowDef.Size = new Size(btnSize, btnSize);
            btnShowDef.Location = new Point(btnShowRot.Location.X, btnShowRot.Location.Y + btnShowRot.Height + 7);
        }

        //EVENTS (this)
        private void IsoSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Design();
            if(FormMain.Manager != null)
                FormMain.Manager.DrawSideView();
        }
        private void IsoSplitContainer_MouseMove(object sender, EventArgs e)
        {
            if (FormMain.Manager != null)
            {
                if (FormMain.Manager.SelectedInnerObject != null)
                    btnDeleteLoad.Enabled = true;
                else
                    btnDeleteLoad.Enabled = false;
            }
        }

        //EVENTS (others)
        private void PicScreen_GotFocus(object sender, EventArgs e)
        {
            if (FormMain.Manager != null)
            {
                FormMain.Manager.UpdateSelectedInnerObjects(new PointF(-100, -100));
                FormMain.Manager.Update();
            }

            PicSideView_BackgroundImageChanged(PicSideView, EventArgs.Empty);
        }

        private void PicSideView_BackgroundImageChanged(object sender, EventArgs e)
        {
            if (FormMain.Manager != null)
            {
                if (FormMain.Manager.SelectedItems.Count != 1)
                {
                    btnDeleteLoad.Enabled = false;
                    btnEditSupport.Enabled = false;
                }
                else
                {
                    if(FormMain.Manager.SelectedLoad != null || FormMain.Manager.SelectedDistLoad != null)
                    {
                        btnDeleteLoad.Enabled = true;
                        btnEditSupport.Enabled = false;
                    }
                    else if(FormMain.Manager.SelectedSupport != null)
                    {
                        btnDeleteLoad.Enabled = false;
                        btnEditSupport.Enabled = true;
                    }
                    else
                    {
                        btnDeleteLoad.Enabled = false;
                        btnEditSupport.Enabled = false;
                    }
                }
            }
        }

        private void TbMeasure_LostFocus(object sender, EventArgs e)
        {
            tbMeasure.Text = null;
            labelTbMeasure.Text = null;
        }
        private void TbMeasure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    double d = Math.Round(double.Parse(TextBoxMeasure.Text), 2);
                    PicScreen.MeasureData = d;
                    PicScreen.AddByMeasure();
                }
                catch
                {
                    PicScreen.MeasureData = double.NaN;
                }

                PicScreen.Focus();
                tbMeasure.Text = null;
                labelTbMeasure.Text = "comandos:";
                FormMain.Manager.Update();
            }
            
            else if(e.KeyCode == Keys.Escape)
            {
                tbMeasure.Text = null;
                labelTbMeasure.Text = "comandos:";
                PicScreen.MeasureTypeOn = false;
                PicScreen.Focus();
            }
        }

        private void BtnDeleteLoad_Click(object sender, EventArgs e)
        {
            if (FormMain.Manager.SelectedItem == null)
                return;
            if (!(FormMain.Manager.SelectedItem is LinearStructure))
                return;

            LinearStructure s = FormMain.Manager.SelectedItem as LinearStructure;

            if(FormMain.Manager.SelectedLoad != null)
                s.RemoveVirtualPointLoad(FormMain.Manager.SelectedLoad);

            else if (FormMain.Manager.SelectedDistLoad != null)
                s.RemoveVirtualDistLoad(FormMain.Manager.SelectedDistLoad);

            FormMain.Manager.Update();
        }
        private void BtnDeleteLoad_MouseLeave(object sender, EventArgs e)
        {
            btnDeleteLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnDeleteLoad.png");
        }
        private void BtnDeleteLoad_MouseEnter(object sender, EventArgs e)
        {
            btnDeleteLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnDeleteLoad_high.png");
        }
        private void BtnDeleteLoad_EnabledChanged(object sender, EventArgs e)
        {
            if(btnDeleteLoad.Enabled)
                btnDeleteLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnDeleteLoad.png");
            else
                btnDeleteLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnDeleteLoad_disabled.png");
        }

        private void BtnEditSupport_EnabledChanged(object sender, EventArgs e)
        {
            if (btnEditSupport.Enabled)
                btnEditSupport.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnEditSupport.png");
            else
                btnEditSupport.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnEditSupport_disabled.png");
        }
        private void BtnEditSupport_Click(object sender, EventArgs e)
        {
            FormEditSupport form = new FormEditSupport(FormMain.Manager);
            form.ShowDialog();
            FormMain.Manager.Update();
        }
        private void BtnEditSupport_MouseLeave(object sender, EventArgs e)
        {
            btnEditSupport.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnEditSupport.png");
        }
        private void BtnEditSupport_MouseEnter(object sender, EventArgs e)
        {
            btnEditSupport.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnEditSupport_high.png");
        }

        private void BtnShowMomentumDiagram_Click(object sender, EventArgs e)
        {
            if (SideViewScreenProperties.MomentumDiagramOn)
            {
                SideViewScreenProperties.MomentumDiagramOn = false;
                btnShowMomentumDiagram.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowMomentumDiagram.png");
            }
            else
            {
                SideViewScreenProperties.MomentumDiagramOn = true;
                btnShowMomentumDiagram.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowMomentumDiagram_high.png");
            }

            FormMain.Manager.DrawSideView();
        }

        private void BtnShowDistLoads_Click(object sender, EventArgs e)
        {
            if(SideViewScreenProperties.DistributedLoadsOn)
            {
                SideViewScreenProperties.DistributedLoadsOn = false;
                btnShowDistLoads.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowDistLoads.png");
            }
            else
            {
                SideViewScreenProperties.DistributedLoadsOn = true;
                btnShowDistLoads.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowDistLoads_high.png");
            }

            FormMain.Manager.DrawSideView();
        }

        private void BtnShowPointLoads_Click(object sender, EventArgs e)
        {
            if (SideViewScreenProperties.PointLoadsOn)
            {
                SideViewScreenProperties.PointLoadsOn = false;
                btnShowPointLoads.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowPointLoads.png");
            }
            else
            {
                SideViewScreenProperties.PointLoadsOn = true;
                btnShowPointLoads.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowPointLoads_high.png");
            }

            FormMain.Manager.DrawSideView();
        }

        private void BtnShowDef_Click(object sender, EventArgs e)
        {
            if (SideViewScreenProperties.DeformationDiagramOn)
            {
                SideViewScreenProperties.DeformationDiagramOn = false;
                btnShowDef.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowDef.png");
            }
            else
            {
                SideViewScreenProperties.DeformationDiagramOn = true;
                btnShowDef.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowDef_high.png");
            }

            FormMain.Manager.Update();
        }

        private void BtnShowRot_Click(object sender, EventArgs e)
        {
            if (SideViewScreenProperties.RotationDiagramOn)
            {
                SideViewScreenProperties.RotationDiagramOn = false;
                btnShowRot.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowRot.png");
            }
            else
            {
                SideViewScreenProperties.RotationDiagramOn = true;
                btnShowRot.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowRot_high.png");
            }

            FormMain.Manager.Update();
        }

        private void BtnShowShearDiagram_Click(object sender, EventArgs e)
        {
            if (SideViewScreenProperties.ShearDiagramOn)
            {
                SideViewScreenProperties.ShearDiagramOn = false;
                btnShowShearDiagram.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowShearDiagram.png");
            }
            else
            {
                SideViewScreenProperties.ShearDiagramOn = true;
                btnShowShearDiagram.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnShowShearDiagram_high.png");
            }

            FormMain.Manager.Update();
        }
    }
}
