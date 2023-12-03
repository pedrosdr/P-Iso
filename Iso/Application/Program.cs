using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Iso
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Creating temp directory for autosaves
            if (!Directory.Exists(Application.ExecutablePath + @"\resources\temp"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\resources\temp");
                }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormInit());
        }
    }
}
