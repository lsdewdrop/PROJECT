using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 fm1 = new Form1();
            Form7 fm7 = new Form7();
            Form2 fm2 = new Form2();
            Form3 fm3 = new Form3();
            Form4 fm4 = new Form4();
            Form5 fm5 = new Form5();
            Form6 fm6 = new Form6();
            
            Application.Run(fm1);

        }
    }
}
