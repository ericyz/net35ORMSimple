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

namespace SampleApplication.Views
{
    public partial class EmployeeForm : Form
    {
        #region model

        private IList<Employee> _employees;
        private EmployeeService _employeeService;
        private readonly string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private readonly int BTN_X = 120;
        private readonly int BTN_Y = 25;
        private readonly int LABEL_X = 150;
        private readonly int LABEL_Y = 25;
        private readonly int GAP_X = 25;
        private readonly int GAP_Y = 25;

        #endregion

        public EmployeeForm()
        {
            try
            {
                InitializeComponent();
                this.FormBorderStyle = FormBorderStyle.FixedSingle; // Disable resize
                this.MaximizeBox = false;
                _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                _employeeService = new EmployeeService(_connectionString);
            } catch (Exception)
            {
                MessageBox.Show(this, "Fail in EmployeeForm.");
            }


        }

        #region formLifeCycle

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadForm();

            } catch (Exception ex)
            {
                MessageBox.Show(this, "Fail during loading");
            }
        }

        private void LoadForm()
        {
            try
            {
                LoadModel();
                LoadView();
            } catch (Exception)
            {
                MessageBox.Show(this, "Fail during LoadForm");
            }

        }

        #endregion

        #region callback

        private void LoadView()
        {

            var countY = 0;
            var countX = 0;

            // Add Label
            // Header
            if (_employees.Count > 0)
            {

                foreach (var employee in _employees)
                {
                    countX = 0;
                    foreach (var prop in typeof(Employee).GetProperties())
                    {
                        if (countY == 0)
                        {
                            string labelName = string.Format("lblColumn{0}", prop.Name);
                            Label label = new Label();
                            label.Name = labelName;
                            label.Width = LABEL_X;
                            label.Height = LABEL_Y;
                            label.Text = prop.Name;
                            label.Location = new Point(countX*LABEL_X + GAP_X, GAP_Y);
                            this.Controls.Add(label);
                        }
                        string txtName = string.Format("txtField{0}{1}", prop.Name, countY);

                        // Not Database Generated Key
                        Control control = null;
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            control = new DateTimePicker();
                            ((DateTimePicker) control).Value = employee.DateOfBirth;
                        } else if (prop.PropertyType == typeof(int))
                        {
                            control = new TextBox();
                            ((TextBox) control).Text = ((int) (prop.GetValue(employee, null) ?? "")).ToString();
                        } else if (prop.PropertyType == typeof(string))
                        {
                            control = new TextBox();
                            ((TextBox) control).Text = (string) (prop.GetValue(employee, null) ?? "");
                        }
                        control.Name = txtName;
                        control.Location = new Point(countX*LABEL_X + GAP_X, GAP_Y + (countY + 1)*LABEL_Y);
                        if (prop.GetCustomAttributes(typeof(DataBaseGenerated), true).Length > 0)
                        {
                            control.Enabled = false;
                        }
                        Debug.WriteLine("Update Button Location: " + control.Location);
                        this.Controls.Add(control);
                        countX++;
                    }
                    // Edit buttn
                    Button btnUpdate = new Button();
                    btnUpdate.Name = string.Format("BtnUpdate_{0}", employee.Id);
                    btnUpdate.Text = "Update";
                    btnUpdate.Click += Btn_OnClick;
                    btnUpdate.Location = new Point((countX - 1)*LABEL_X + GAP_X*countX + BTN_X,
                                                   GAP_Y + (countY + 1)*LABEL_Y);
                    Debug.WriteLine("Update Button Location: " + btnUpdate.Location);
                    this.Controls.Add(btnUpdate);


                    Button btnDelete = new Button();
                    btnDelete.Name = string.Format("BtnDelete_{0}", employee.Id);
                    btnDelete.Text = "Delete";
                    btnDelete.Click += Btn_OnClick;
                    btnDelete.Location = new Point((countX)*LABEL_X + GAP_X*(countX - 2) + BTN_X,
                                                   GAP_Y + (countY + 1)*LABEL_Y);
                    Debug.WriteLine("Update Button Location: " + btnDelete.Location);
                    this.Controls.Add(btnDelete);
                    countY++;
                }
            }

            // Add button 
            var btnGenerate = new Button {Text = "Generate Class", Width = BTN_X, Height = BTN_Y};
            btnGenerate.Name = "BtnGenerate";

            if (_employees.Count == 0)
            {
                this.Width = GAP_X*3 + 2*BTN_X;
                this.Height = 2*GAP_Y + BTN_Y;
            } else
            {
                this.Width = LABEL_X*countX + (countX + 1)*GAP_X + (GAP_X + BTN_X);
                this.Height = GAP_Y*(countY + 3) + LABEL_Y*(countY + 1) + BTN_Y;
            }

            var btnAdd = new Button {Text = "Add Employee", Width = BTN_X, Height = BTN_Y};
            btnAdd.Name = "BtnAdd";
            btnAdd.Location = new Point(this.Width*2/3, (countY + 2)*GAP_Y + countY*BTN_Y);
            btnAdd.Click += Btn_OnClick;
            this.Controls.Add(btnAdd);

            btnGenerate.Location = new Point(this.Width/3 - BTN_X/2, (countY + 2)*GAP_Y + countY*BTN_Y);
            btnGenerate.Click += Btn_OnClick;
            this.Controls.Add(btnGenerate);


            this.Width += 60; // Offset X
            this.Height += 60; // Offset Y
            this.Controls.Add(btnGenerate);


        }

        private void LoadModel()
        {
            _employees = new EmployeeService(_connectionString).ReadAll();
            Debug.WriteLine(string.Format("Employee count = {0}", _employees.Count));
        }

        private void ReloadForm()
        {
            this.Controls.Clear();
            this.LoadForm();

        }

        #endregion

        #region callback

        public void Btn_OnClick(object sender, EventArgs e)
        {
            try
            {

                Debug.WriteLine("Btn Click CallBack");
                var btn = (Button)sender;
                if (btn.Name.Contains("BtnUpdate")) {
                    string id = btn.Name.Replace("BtnUpdate_", "");
                    Debug.WriteLine("btn update id = " + id);
                    var form = new EmployeeUpdateForm(id);
                    form.ShowDialog();
                    this.ReloadForm();

                } else if (btn.Name.Contains("BtnDelete")) {
                    string id = btn.Name.Replace("BtnDelete_", "");
                    Debug.WriteLine("btn delete id = " + id);
                    var result = MessageBox.Show(this, string.Format("Are you sure to delete the employee {0}", id),
                                                 "Delete", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) {
                        _employeeService.DeleteById(id);
                        ReloadForm();
                    }
                } else {
                    switch (btn.Name) {
                        case "BtnGenerate":
                            Debug.WriteLine("BtnGenerate Click CallBack");
                            new ClassGeneratorForm().ShowDialog();
                            break;
                        case "BtnAdd":
                            Debug.WriteLine("BtnAdd Click CallBack");
                            new EmployeeUpdateForm().ShowDialog();
                            this.ReloadForm();
                            break;
                    }
                }
            } catch (Exception)
            {
                MessageBox.Show(this, "Fail during Btn_OnClick");
            }

            #endregion
        }
    }
}
