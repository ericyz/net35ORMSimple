using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleORM.Annotation;
using SimpleORM.DataAccess;
using SimpleORM.Model;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace SampleApplication.Views {
    public partial class EmployeeUpdateForm : Form {
        #region property

        private readonly int GAP_Y = 30;
        private readonly int GAP_X = 30;
        private readonly int LABEL_X = 80;
        private readonly int LABEL_Y = 30;
        private readonly int BUTTON_X = 90;
        private readonly int BUTTON_Y = 30;
        private readonly int TXT_X = 200;
        private readonly int TXT_Y = 30;
        private string _connectionString;
        private Employee _employeeToUpdate;

        private EmployeeService _employeeService;
        #endregion

        #region model



        #endregion
        public EmployeeUpdateForm() {
            try {
                InitializeComponent();

                this.FormBorderStyle = FormBorderStyle.FixedSingle;     // Disable resize
                _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                _employeeService = new EmployeeService(_connectionString);
            } catch (Exception) {
                MessageBox.Show(this, "Employee Update Form.");
            }

        }

        public EmployeeUpdateForm(string id) : this() {
            try {
                _employeeToUpdate = _employeeService.ReadById(id);

            } catch (Exception) {
                MessageBox.Show(this, "Employee Update Form.");
            }
        }
        private void EmployeeUpdateForm_Load(object sender, EventArgs e) {
            try
            {
                LoadView();
                _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                _employeeService = new EmployeeService(_connectionString);
            } catch (Exception)
            {

                MessageBox.Show(this, "Employee Update Form.");
            }
           
        }



        private void LoadView() {
            var type = typeof(Employee);
            int i = 0;
            foreach (var propertyInfo in type.GetProperties()) {
                var labelName = string.Format("label{0}", propertyInfo.Name);
                var label = new Label();
                label.Name = labelName;
                label.Width = LABEL_X;
                label.Height = LABEL_Y;
                label.Text = propertyInfo.Name;
                label.Location = new Point(GAP_X, (i) * BUTTON_Y + GAP_Y * i + 30);
                this.Controls.Add(label);

                var txtName = string.Format("txt{0}", propertyInfo.Name);
                Control control = null;

                if (propertyInfo.PropertyType == typeof(DateTime)) {
                    control = new DateTimePicker();
                    if (_employeeToUpdate != null) {
                        ((DateTimePicker)control).Value = (DateTime)propertyInfo.GetValue(_employeeToUpdate, null);
                    }
                } else if (propertyInfo.PropertyType == typeof(string)) {
                    control = new TextBox();
                    if (_employeeToUpdate != null) {
                        control.Text = (string)propertyInfo.GetValue(_employeeToUpdate, null);
                    }
                } else if (propertyInfo.PropertyType == typeof(int)) {
                    control = new TextBox();
                    if (_employeeToUpdate != null) {
                        control.Text = (int)propertyInfo.GetValue(_employeeToUpdate, null) + "";
                    }
                }
                control.Name = txtName;
                control.Location = new Point(2 * GAP_X + BUTTON_X, (i) * BUTTON_Y + GAP_Y * i + 30);
                control.Width = TXT_X;
                control.Height = TXT_Y;
                if (propertyInfo.GetCustomAttributes(typeof(DataBaseGenerated), true).Length > 0) {
                    control.Enabled = false;
                }

                this.Controls.Add(control);
                i++;
            }

            this.Width = 30 + 3 * GAP_X + BUTTON_X + TXT_X;
            this.Height = 60 + (i + 2) * GAP_Y + BUTTON_Y * i;

            var btnSave = new Button();
            btnSave.Name = "BtnSave";
            btnSave.Text = "Save";
            btnSave.Width = BUTTON_X;
            btnSave.Height = BUTTON_Y;
            btnSave.Location = new Point((this.Width - BUTTON_X) / 2, (i) * BUTTON_Y + GAP_Y * (i + 1));
            btnSave.Click += Btn_OnClick;
            this.Controls.Add(btnSave);
        }

        public void Btn_OnClick(object sender, EventArgs args) {
            
            Debug.WriteLine("Button Click");
            try {
                var btn = (Button)sender;
                switch (btn.Name) {
                    case "BtnSave":

                        var type = typeof(Employee);
                        Employee employee = _employeeToUpdate ?? new Employee();

                        foreach (var propertyInfo in type.GetProperties()) {
                            var txtName = string.Format("txt{0}", propertyInfo.Name);
                            var control = this.Controls[txtName];
                            if (propertyInfo.GetCustomAttributes(typeof(Key), true).Length == 0) {
                                if (control is DateTimePicker) {
                                    propertyInfo.SetValue(employee, ((DateTimePicker)control).Value, null);
                                } else {
                                    if (propertyInfo.PropertyType == typeof(int)) {
                                        propertyInfo.SetValue(employee, int.Parse(control.Text), null);

                                    } else {
                                        propertyInfo.SetValue(employee, control.Text, null);

                                    }
                                }
                            }
                        }
                        _employeeService.AddOrUpdate(employee);
                        this.Close();
                        break;
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show(this, "Fail to update.");
            }

        }
    }
}
