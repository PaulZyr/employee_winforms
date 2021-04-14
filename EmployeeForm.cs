using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Home0330_8_7
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm( Employee employee)
        {
            InitializeComponent();
            Employee = employee;
        }
        public Employee Employee { get; set; }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            surNameTextBox.Text = Employee.SurName;
            nameTextBox.Text = Employee.Name;
            middleTextBox.Text = Employee.MiddleName;
            birthDateTimePicker.Value = Employee.BirthDate;
            cityTextBox.Text = Employee.BirthCity;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Employee.SurName = surNameTextBox.Text;
            Employee.Name = nameTextBox.Text;
            Employee.MiddleName = middleTextBox.Text;
            Employee.BirthDate = birthDateTimePicker.Value;
            Employee.BirthCity = cityTextBox.Text;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
