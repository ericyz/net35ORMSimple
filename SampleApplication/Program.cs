using System;

using System.Windows.Forms;
using SampleApplication.Util;
using SampleApplication.Views;

namespace SampleApplication {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            DBInitializer.IntialDB();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EmployeeForm());
            
        }
    }
}
