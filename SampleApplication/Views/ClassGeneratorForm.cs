using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleORM.Util;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SampleApplication.Views {
    public partial class ClassGeneratorForm : Form {
        public ClassGeneratorForm() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = this.txtTable.Text.Trim();
                string sql = $"SELECT * FROM {tableName}";
                ORMUtil.GenerateClass(ConfigurationManager.AppSettings["ConnectionString"], sql);
                string directoryPath =
                    $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}";
                MessageBox.Show(this, "Both model and data access service classes have been generated.");
                Process.Start(directoryPath);
                
                this.Close();
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show(this,"Table name does not exist.");
            }

        }
    }
}
