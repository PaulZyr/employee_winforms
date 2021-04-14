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
    public partial class SearchByForm : Form
    {
        public SearchByForm()
        {
            InitializeComponent();
        }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Year { get; set; }
        public string BirthCity { get; set; }
        private void SearchByForm_Load(object sender, EventArgs e)
        {
            surNameTextBox.Text = "any";
            surNameTextBox.Enabled = false;
            nameTextBox.Text = "any";
            nameTextBox.Enabled = false;
            middleTextBox.Text = "any";
            middleTextBox.Enabled = false;
            cityTextBox.Text = "any";
            cityTextBox.Enabled = false;
            yearTextBox.Text = "any";
            yearTextBox.Enabled = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            CheckInputs();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CheckInputs()
        {
            if (surNameTextBox.Text == "") SurName = "any";
            else SurName = surNameTextBox.Text;

            if (nameTextBox.Text == "") FirstName = "any";
            else FirstName = nameTextBox.Text;

            if (middleTextBox.Text == "") MiddleName = "any";
            else MiddleName = middleTextBox.Text;

            if (cityTextBox.Text == "") BirthCity = "any";
            else BirthCity = cityTextBox.Text;

            if (yearTextBox.Text == "") Year = "any";
            else Year = yearTextBox.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void surCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (surNameTextBox.Enabled == true)
            {
                surNameTextBox.Enabled = false;
                surNameTextBox.Text = "any";
            }
            else
            {
                surNameTextBox.Enabled = true;
                surNameTextBox.Text = "";
            }
        }
        private void nameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (nameTextBox.Enabled == true)
            {
                nameTextBox.Enabled = false;
                nameTextBox.Text = "any";
            }
            else
            {
                nameTextBox.Enabled = true;
                nameTextBox.Text = "";
            }
                
        }
        private void middleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (middleTextBox.Enabled == true)
            {
                middleTextBox.Enabled = false;
                middleTextBox.Text = "any";
            }   
            else
            {
                middleTextBox.Enabled = true;
                middleTextBox.Text = "";
            } 
        }
        private void cityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cityTextBox.Enabled == true)
            {
                cityTextBox.Enabled = false;
                cityTextBox.Text = "any";
            }
            else
            {
                cityTextBox.Enabled = true;
                cityTextBox.Text = "";
            }

        }
        private void yearCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (yearTextBox.Enabled == true)
            {
                yearTextBox.Enabled = false;
                yearTextBox.Text = "any";
            }
            else
            {
                yearTextBox.Enabled = true;
                yearTextBox.Text = "";
            }
        }
    }
}
