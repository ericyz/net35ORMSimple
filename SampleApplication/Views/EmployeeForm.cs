using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class EmployeeForm : Form {
        #region model

        private IList<Employee> _employees;
        private EmployeeService _employeeService;
        private readonly string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private readonly int BTN_X = 70;
        private readonly int BTN_Y = 25;
        private readonly int LABEL_X = 150;
        private readonly int LABEL_Y = 25;
        private readonly int GAP_X = 25;
        private readonly int GAP_Y = 25;

        #endregion
        public EmployeeForm() {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;     // Disable resize
            this.MaximizeBox = false;
            _connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        }
        #region formLifeCycle
        private void EmployeeForm_Load(object sender, EventArgs e) {
            LoadModel();
            LoadView();
        }
        #endregion
        #region callback
        private void LoadView() {

            var countX = 0;
            var countY = 0;
            // Add Label
            // Header
            if (_employees.Count > 0) {
                foreach (var employee in _employees) {
                    foreach (var prop in typeof(Employee).GetProperties()) {
                        if (countY == 0) {
                            string labelName = string.Format("lblColumn{0}", prop.Name);
                            Label label = new Label();
                            label.Name = labelName;
                            label.Width = LABEL_X;
                            label.Height = LABEL_Y;
                            label.Text = prop.Name;
                            label.Location = new Point(GAP_X + LABEL_X, GAP_Y);
                            this.Controls.Add(label);
                        } 
                            string txtName = string.Format("txtField{0}{1}", prop.Name, countY);
                            if (prop.GetCustomAttributes(typeof(DataBaseGenerated), true).Length == 0) {
                                // Not Database Generated Key
                                Control control = null;
                                if (prop.PropertyType == typeof(DateTime)) {
                                    control = new DateTimePicker();
                                    ((DateTimePicker)control).Value = employee.DateOfBirth;
                                } else if (prop.PropertyType == typeof(int)) {
                                    control = new TextBox();
                                    ((TextBox)control).Text = ((int)(prop.GetValue(employee, null) ?? "")).ToString();
                                } else if (prop.PropertyType == typeof(string)) {
                                    control = new TextBox();
                                ((TextBox)control).Text = (string)(prop.GetValue(employee, null) ?? "");
                                }
                                control.Name = txtName;
                                control.Location = new Point(GAP_X + (countX-1)*LABEL_X, GAP_Y + (countY+1) * LABEL_Y);
                                this.Controls.Add(control);
                        }
                        countX++;
                    }
                    countY++;
                }
            }

            // Add button 
            var btnGenerate = new Button { Text = "Generate Class", Width = BTN_X, Height = BTN_Y };
            btnGenerate.Name = "BtnGenerate";

            if (_employees.Count == 0) {
                this.Width = GAP_X * 2 + 2 * BTN_X;
                this.Height = GAP_Y * 2 + 2 * BTN_Y;
            } else {
                this.Width = GAP_X * (countX + 1) + LABEL_X * countX;
                this.Height = GAP_Y * (countY + 2) + LABEL_X * countY + BTN_Y;
            }

            var btnAdd = new Button { Text = "Add Employee", Width = BTN_X, Height = BTN_Y };
            btnAdd.Name = "BtnAdd";
            btnAdd.Location = new Point(this.Width-(GAP_X * 3)- BTN_X, (countY + 2) * GAP_Y + countY * BTN_Y);
            btnAdd.Click += Btn_OnClick;
            this.Controls.Add(btnAdd);

            btnGenerate.Location = new Point(GAP_X*3, (countY+2)*GAP_Y +countY* BTN_Y);
            btnGenerate.Click += Btn_OnClick;

            this.Width += 30;
            this.Height += 30;
            this.Controls.Add(btnGenerate);


        }

        private void LoadModel() {
            _employees = new EmployeeService(_connectionString).ReadAll();
            Debug.WriteLine(string.Format("Employee count = {0}", _employees.Count));
        }
        #endregion

        #region callback
        public void Btn_OnClick(object sender, EventArgs e) {
            Debug.WriteLine("Btn Click CallBack");
            var btn = (Button)sender;
            switch (btn.Name) {
                case "BtnGenerate":
                    Debug.WriteLine("BtnGenerate Click CallBack");
                    new ClassGeneratorForm().Show();
                    break;
                case "BtnAdd":
                    Debug.WriteLine("BtnAdd Click CallBack");
                    new EmployeeUpdateForm().Show();
                    break;
            }
        }
        #endregion
    }
}
