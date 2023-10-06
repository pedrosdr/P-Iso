using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Iso
{
    public partial class FormMain : Form
    {
        //FIELDS
        private string _file;

        private IsoSplitContainer splitContainer;
        private PictureBoxScreen picScreen;
        private PictureBoxSideView picSideView;

        private Manager _manager;

        private bool _beamInUse = false;
        private bool _columnInUse = false;
        private bool _slabInUse = false;

        //PROPERTIES
        public Manager Manager
        {
            get { return _manager; }
        }
        public IsoSplitContainer SplitContainer
        {
            get { return splitContainer; }
        }

        public bool BeamInUse
        {
            get { return _beamInUse; }
            set { _beamInUse = value; }
        }
        public bool ColumnInUse
        {
            get { return _columnInUse; }
            set { _columnInUse = value; }
        }
        public bool SlabInUse
        {
            get { return _slabInUse; }
            set { _slabInUse = value; }
        }

        //CONSTRUCTORS
        public FormMain()
        {
            InitializeComponent();
            Initialize();
            Design();

            ScreenProperties.Screen = picScreen;
            SideViewScreenProperties.Screen = picSideView;

            //Beam b1 = new Beam(new IsoPosition(0, 0, 3), new IsoPosition(8, 0, 3));
            //b1.Id = 0;
            //Beam b2 = new Beam(new IsoPosition(4, 0, 3), new IsoPosition(4, 4, 3));
            //b2.Id = 1;
            //Column c1 = new Column(new IsoPosition(0, 0, 0), new IsoPosition(0, 0, 3));
            //c1.Id = 0;
            //Column c2 = new Column(new IsoPosition(8, 0, 0), new IsoPosition(8, 0, 3));
            //c2.Id = 1;
            //Column c3 = new Column(new IsoPosition(4, 4, 0), new IsoPosition(4, 4, 3));
            //c3.Id = 2;

            //DistributedLoad load = new DistributedLoad(b1, -10, 0, 8);
            //b1.AddVirtualDistLoad(load);

            //_manager.AddStructure(b1);
            //_manager.AddStructure(b2);
            //_manager.AddStructure(c1);
            //_manager.AddStructure(c2);
            //_manager.AddStructure(c3);
            _manager.Update();
        }

        //METHODS
        private void Initialize()
        {
            //this
            if (_file == null)
                this.Text = "Sem Título";
            else
                this.Text = Path.GetFileName(_file);

            //PicScreen
            picScreen = new PictureBoxScreen(this);

            //PicSideView
            picSideView = new PictureBoxSideView(this);
            picSideView.BackgroundImageChanged += PicSideView_BackgroundImageChanged;

            //SplitContainer
            splitContainer = new IsoSplitContainer(picSideView, picScreen, this);
            splitContainer.SplitterDistance = Width - 400;
            Controls.Add(splitContainer);

            //Managers
            _manager = new Manager(picScreen, picSideView);

            //btnAddLoad
            btnAddLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddLoad.png");
            btnAddLoad.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddLoad.MouseEnter += BtnAddLoad_MouseEnter;
            btnAddLoad.MouseLeave += BtnAddLoad_MouseLeave;
            btnAddLoad.Click += BtnAddLoad_Click;
            btnAddLoad.EnabledChanged += BtnAddLoad_EnabledChanged;
            btnAddLoad.Enabled = false;

            //btnAddDistLoad
            btnAddDistLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddDistLoad.png");
            btnAddDistLoad.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddDistLoad.MouseEnter += BtnAddDistLoad_MouseEnter;
            btnAddDistLoad.MouseLeave += BtnAddDistLoad_MouseLeave;
            btnAddDistLoad.Click += BtnAddDistLoad_Click;
            btnAddDistLoad.EnabledChanged += ButtonAddDistLoad_EnabledChanged;
            btnAddDistLoad.Enabled = false;

            //btnOpenFile
            btnOpenFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnOpenFile.png");

            //btnSaveFile
            btnSaveFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnSaveFile.png");

            //btnNewFile
            btnNewFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnNewFile.png");
        }
        private void Design()
        {
            //SplitContainer
            splitContainer.Location = new Point(0, 94);
            splitContainer.Size = new Size(Width - 12, Height - SplitContainer.Location.Y - 35);

            //logo
            btnLogo.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\logo.png");
            
            //button beam
            btnBeam.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\beam.png");

            //button column
            btnColumn.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\column.png");

            //button slab
            btnSlab.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\slab.png");

            //btnGetPoint
            picGetValue.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnGetPoint.png");
            
            //this
            Bitmap backbuffer = new Bitmap(this.Width, this.Height);
            Graphics graphs = Graphics.FromImage(backbuffer);
            graphs.Clear(Color.FromKnownColor(KnownColor.Control));
            Rectangle rect = new Rectangle(0, 0, Width, 94);
            Brush brush = new SolidBrush(Color.FromArgb(253, 253, 253));
            graphs.FillRectangle(brush, rect);

            graphs.DrawLine
                (
                    new Pen(Color.FromArgb(210, 210, 210), 2),
                    new Point(0, 94),
                    new Point(Width, 94)
                );

            graphs.DrawLine
                (
                    new Pen(Color.FromArgb(210, 210, 210), 2),
                    new Point(0, 24),
                    new Point(Width, 24)
                );

            graphs.DrawLine
                (
                    new Pen(Color.FromArgb(190, 190, 190)),
                    new Point(btnBeam.Location.X - 10, 29),
                    new Point(btnBeam.Location.X - 10, 89)
                );

            graphs.DrawLine
                (
                    new Pen(Color.FromArgb(190, 190, 190)),
                    new Point(btnColumn.Location.X + btnColumn.Width + 10, 29),
                    new Point(btnColumn.Location.X + btnColumn.Width + 10, 89)
                );
            BackgroundImage = backbuffer;
        }

        private void SetAllUsesFalse()
        {
            _beamInUse = false;
            _columnInUse = false;
            _slabInUse = false;
            picScreen.BeamInUse = false;
            picScreen.ColumnInUse = false;
            picScreen.SlabInUse = false;
        }

        //EVENTS
        private void PicSideView_BackgroundImageChanged(object sender, EventArgs e)
        {
            if (_manager != null)
            {
                if (_manager.SelectedItems.Count != 1)
                {
                    btnAddLoad.Enabled = false;
                    btnAddDistLoad.Enabled = false;
                }
                else
                {
                    btnAddLoad.Enabled = true;
                    btnAddDistLoad.Enabled = true;
                }
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            Design();
            try
            {
                splitContainer.SplitterDistance = Width - 400;
            } catch { }
            _manager.Update();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormSaveMessage savemsg = new FormSaveMessage("Deseja salvar o arquivo atual?");
            DialogResult dresult = savemsg.ShowDialog();
            if (dresult == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            else if (dresult == DialogResult.Yes)
                btnSave_Click(btnSaveFile, EventArgs.Empty);

            _manager.FileManager.Clear();
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBeam_MouseEnter(object sender, EventArgs e)
        {
            btnBeam.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\beam_high.png");
        }
        private void btnBeam_MouseLeave(object sender, EventArgs e)
        {
            btnBeam.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\beam.png");
        }
        private void btnBeam_Click(object sender, EventArgs e)
        {
            SetAllUsesFalse();
            _beamInUse = true;
            picScreen.BeamInUse = true;

            picScreen.PickSideIcon();
        }

        private void btnColumn_MouseEnter(object sender, EventArgs e)
        {
            btnColumn.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\column_high.png");
        }
        private void btnColumn_MouseLeave(object sender, EventArgs e)
        {
            btnColumn.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\column.png");
        }
        private void btnColumn_Click(object sender, EventArgs e)
        {
            SetAllUsesFalse();
            _columnInUse = true;
            picScreen.ColumnInUse = true;

            picScreen.PickSideIcon();
        }

        private void btnSlab_MouseEnter(object sender, EventArgs e)
        {
            btnSlab.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\slab_high.png");
        }
        private void btnSlab_MouseLeave(object sender, EventArgs e)
        {
            btnSlab.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\slab.png");
        }
        private void btnSlab_Click(object sender, EventArgs e)
        {
            SetAllUsesFalse();
            _slabInUse = true;
            picScreen.SlabInUse = true;

            picScreen.PickSideIcon();  
        }

        private void btnLogo_MouseEnter(object sender, EventArgs e)
        {
            btnLogo.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\logo_high.png");
        }
        private void btnLogo_MouseLeave(object sender, EventArgs e)
        {
            btnLogo.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\logo.png");
        }

        private void BtnAddLoad_Click(object sender, EventArgs e)
        {
            if (_manager.SelectedItem is Beam)
            {
                FormAddLoadBeam formAddLoad = new FormAddLoadBeam(_manager);
                formAddLoad.ShowDialog();
            }
            else
            {
                FormAddLoadColumn formAddLoad = new FormAddLoadColumn(_manager);
                formAddLoad.ShowDialog();
            }
        }
        private void BtnAddLoad_EnabledChanged(object sender, EventArgs e)
        {
            if (btnAddLoad.Enabled)
                btnAddLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddLoad.png");
            else
                btnAddLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddLoad_disabled.png");
        }
        private void BtnAddLoad_MouseLeave(object sender, EventArgs e)
        {
            if (btnAddLoad.Enabled)
                btnAddLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddLoad.png");
        }
        private void BtnAddLoad_MouseEnter(object sender, EventArgs e)
        {
            if (btnAddLoad.Enabled)
                btnAddLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddLoad_high.png");
        }

        private void BtnAddDistLoad_Click(object sender, EventArgs e)
        {
            FormAddDistLoad formAddDistLoad = new FormAddDistLoad(_manager);
            formAddDistLoad.ShowDialog();
        }
        private void ButtonAddDistLoad_EnabledChanged(object sender, EventArgs e)
        {
            if (btnAddDistLoad.Enabled)
                btnAddDistLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddDistLoad.png");
            else
                btnAddDistLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddDistLoad_disabled.png");
        }
        private void BtnAddDistLoad_MouseLeave(object sender, EventArgs e)
        {
            if (btnAddDistLoad.Enabled)
                btnAddDistLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddDistLoad.png");
        }
        private void BtnAddDistLoad_MouseEnter(object sender, EventArgs e)
        {
            if (btnAddDistLoad.Enabled)
                btnAddDistLoad.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnAddDistLoad_high.png");
        }

        private void btnOpen_MouseEnter(object sender, EventArgs e)
        {
            btnOpenFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnOpenFile_high.png");
        }
        private void btnOpen_MouseLeave(object sender, EventArgs e)
        {
            btnOpenFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnOpenFile.png");
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openf = new OpenFileDialog();
                openf.Filter = "Arquivo P-Iso|*.piso";
                if (openf.ShowDialog() == DialogResult.Cancel)
                    return;

                FormSaveMessage savemsg = new FormSaveMessage("Deseja salvar o arquivo atual?");
                DialogResult dresult = savemsg.ShowDialog();
                if (dresult == DialogResult.Cancel)
                    return;
                else if (dresult == DialogResult.Yes)
                    btnSave_Click(btnSaveFile, EventArgs.Empty);

                _manager.Clear();
                _file = openf.FileName;
                this.Text = Path.GetFileName(_file);

                List<Beam> beams = new List<Beam>();
                List<Column> columns = new List<Column>();
                List<Slab> slabs = new List<Slab>();

                using (StreamReader reader = new StreamReader(_file, Encoding.UTF8))
                {
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.StartsWith("<b>"))
                            beams.Add(Beam.LoadComponent(line));
                        else if (line.StartsWith("<c>"))
                            columns.Add(Column.LoadComponent(line));
                        else if (line.StartsWith("<s>"))
                            slabs.Add(Slab.LoadComponent(line));
                    }
                }

                beams.ForEach(b => _manager.AddStructure(b));
                columns.ForEach(c => _manager.AddStructure(c));
                slabs.ForEach(s => _manager.AddStructure(s));
                _manager.Update();
            }
            catch
            {
                FormMessage msg = new FormMessage("Não foi possível carregar o arquivo.");
                msg.ShowDialog();
            }
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            btnSaveFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnSaveFile_high.png");
        }
        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSaveFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnSaveFile.png");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_file == null)
            {
                SaveFileDialog savef = new SaveFileDialog();
                savef.Filter = "Arquivo P-Iso|*.piso";
                if (savef.ShowDialog() == DialogResult.Cancel)
                    return;

                _file = savef.FileName;
            }

            using (StreamWriter writer = new StreamWriter(_file, false, Encoding.UTF8))
            {
                _manager.Structures.ForEach(s => writer.WriteLine(s.GetFileString()));
            }
        }

        private void btnNew_MouseEnter(object sender, EventArgs e)
        {
            btnNewFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnNewFile_high.png");
        }
        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            btnNewFile.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnNewFile.png");
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            FormSaveMessage savemsg = new FormSaveMessage("Deseja salvar o arquivo atual?");
            DialogResult dresult = savemsg.ShowDialog();
            if (dresult == DialogResult.Cancel)
                return;
            else if (dresult == DialogResult.Yes)
                btnSave_Click(btnSaveFile, EventArgs.Empty);

            _manager.Clear();
            _file = null;
            this.Text = "Sem Título";
        }

        private void picGetValue_Click(object sender, EventArgs e)
        {
            if (!(Manager.SelectedItem is Beam))
            {
                MessageBox.Show("Uma das vigas deve estar selecionada");
                return;
            }

            FormGetValue form = new FormGetValue(Manager);
            form.ShowDialog();
        }
        private void picGetValue_MouseEnter(object sender, EventArgs e)
        {
            picGetValue.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnGetPoint_high.png");
        }
        private void picGetValue_MouseLeave(object sender, EventArgs e)
        {
            picGetValue.BackgroundImage = Image.FromFile(Application.StartupPath + @"\resources\icons\btnGetPoint.png");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Slab slab = new Slab(new IsoPosition(2, 0, 3), new IsoPosition(3, 3, 0));
            //_manager.AddStructure(slab);
            //_manager.Update();

            //Line line1 = new Line(new IsoPosition(0, 0, 0), new IsoPosition(3, 3, 0));
            //Line line2 = new Line(new IsoPosition(2, 2, 0), new IsoPosition(3, 3, 0));

            //FormMessage.ShowMessage(line2.TotallyOverlapped(line1).ToString());

            //FirstDegreeEquation eq = FirstDegreeEquation.FromValues(1, 1, 3, 5);
            //FormMessage.ShowMessage(eq.GetX(5).ToString());
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnNew_Click(btnNewFile, EventArgs.Empty);
        }
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnOpen_Click(btnOpenFile, EventArgs.Empty);
        }
        private void salvarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnSave_Click(btnSaveFile, EventArgs.Empty);
        }
        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savef = new SaveFileDialog();
            savef.Filter = "Arquivo P-Iso|*.piso";
            if (savef.ShowDialog() == DialogResult.Cancel)
                return;

            _file = savef.FileName;

            using (StreamWriter writer = new StreamWriter(_file, false, Encoding.UTF8))
            {
                _manager.Structures.ForEach(s => writer.WriteLine(s.GetFileString()));
            }
        }
        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Manager.FileManager.Undo();
        }
    }
}