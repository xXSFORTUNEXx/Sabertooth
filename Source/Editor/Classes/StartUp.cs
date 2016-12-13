using System;
using static System.Windows.Forms.Application;

namespace Editor
{
    static class StartUp
    {
        [STAThread]
        static void Main()
        {
            EnableVisualStyles();
            SetCompatibleTextRenderingDefault(false);
            Run(new Editor());
        }
    }
}
