using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Iso
{
    public class FileManager
    {
        //FIELDS
        int filenumber = 0;

        //PROPERTIES
        public Manager Manager { get; set; }

        //CONSTRUCTORS
        public FileManager(Manager manager)
        {
            Manager = manager;
        }

        //METHODS
        private int GetFileNumber(string path)
        {
            string file = Path.GetFileNameWithoutExtension(path);
            int num = int.Parse(file.Split(new string[] { "autosave" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            return num;
        }

        public void AutoSave()
        {
            filenumber++;
            string path = Application.StartupPath + @"\resources\temp\autosave" + filenumber + ".piso";

            using(StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                List<Structure> structures = Manager.Structures;
                structures.ForEach(s => writer.WriteLine(s.GetFileString()));
            }


            string[] paths = Directory.GetFiles(Application.StartupPath + @"\resources\temp");

            if (paths.Length > 10)
            {
                foreach (string p in paths)
                {
                    if (GetFileNumber(p) < filenumber - 10)
                        File.Delete(p);
                }
            }
        }

        public void Undo()
        {
            string file = Directory.GetFiles(Application.StartupPath + @"\resources\temp")
            .ToList()
            .Where(f => GetFileNumber(f) == filenumber)
            .FirstOrDefault();

            if (file != null)
            {
                File.Delete(file);
                filenumber--;
            }

            file = Directory.GetFiles(Application.StartupPath + @"\resources\temp")
                .ToList()
                .Where(f => GetFileNumber(f) == filenumber)
                .FirstOrDefault();

            Manager.Clear();

            List<Beam> beams = new List<Beam>();
            List<Column> columns = new List<Column>();
            List<Slab> slabs = new List<Slab>();

            using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
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

            beams.ForEach(b => Manager.AddStructure(b));
            columns.ForEach(c => Manager.AddStructure(c));
            slabs.ForEach(s => Manager.AddStructure(s));
            Manager.Update();
        }

        public void Clear()
        {
            string[] files = Directory.GetFiles(Application.StartupPath + @"\resources\temp");
            foreach(string file in files)
                File.Delete(file);
        }
    }
}
