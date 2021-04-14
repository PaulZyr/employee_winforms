using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Home0330_8_7
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        List<Employee> _employees = new List<Employee>();
        List<Employee> _copied = new List<Employee>();
        List<Employee> _temp = new List<Employee>();
        private string _path = "employee.json";

        #region Load Show
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(_path))
            {
                try
                {
                    string str = File.ReadAllText(_path);
                    List<Employee> tmpStrips = new List<Employee>();
                    tmpStrips = JsonConvert.DeserializeObject<List<Employee>>(str);
                    if (tmpStrips.Count > 0)
                    {
                        _employees = tmpStrips;
                        ShowItems();
                        return;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                try
                {
                    File.Create(_path);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            
        }

        private void showAllToolStripButton_Click(object sender, EventArgs e)
        {
            ShowItems();
        }
        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowItems();
        }
        public void ShowItems()
        {
            mainListView.Items.Clear();
            foreach (var item in _employees)
            {
                AddItemToListView(item);
            }

        }
        public void ShowTemp()
        {
            mainListView.Items.Clear();
            foreach (var item in _temp)
            {
                AddItemToListView(item);
            }
        }
        private void AddItemToListView(Employee employee)
        {
            ListViewItem item = new ListViewItem(new string[]
            {
                employee.SurName, employee.Name, employee.MiddleName, 
                employee.BirthDate.ToShortDateString(), employee.BirthCity
            });
            item.Tag = employee.UniqueCode;
            mainListView.Items.Add(item);
        }
        private Employee GetItemByUniqueCode(string str)
        {
            foreach(var item in _employees)
            {
                if (str == item.UniqueCode.ToString())
                {
                    return item;
                }
            }
            return null;
        }
        #endregion

        #region New Edit Delete

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            CreateNewItem();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewItem();
        }
        private void newContext_Click(object sender, EventArgs e)
        {
            CreateNewItem();
        }

        private void CreateNewItem()
        {
            EmployeeForm form = new EmployeeForm(new Employee());
            var res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                _employees.Add(form.Employee);
            }
            ShowItems();
        }

        private void editTripToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            EditItem();
        }
        private void editContext_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void EditItem()
        {
            if (mainListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Choose line to edit");
            }
            else if (mainListView.SelectedItems.Count > 1)
            {
                MessageBox.Show("Choose ONE line only to edit");
            }
            else
            {
                Employee item = GetItemByUniqueCode(mainListView.SelectedItems[0].Tag.ToString());
                EmployeeForm form = new EmployeeForm(item);
                var res = form.ShowDialog();
                if(res == DialogResult.OK)
                {
                    item = form.Employee;
                    ShowItems();
                }
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteItems();
        }
        private void deleteContext_Click(object sender, EventArgs e)
        {
            DeleteItems();
        }
        private void DeleteItems()
        {
            CopyItems();
            var result = MessageBox.Show($"Delete {_copied.Count} employees?", "Deleting", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                RemoveItems();
            }
        }
        private void RemoveItems()
        {
            foreach (var item in _copied)
            {
                _employees.Remove(item);
            }
        }
        #endregion

        #region Copy Cut Paste
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyItems();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            CopyItems();
        }

        private void copyContext_Click(object sender, EventArgs e)
        {
            CopyItems();
        }

        private void CopyItems()
        {
            _copied.Clear();
            foreach (ListViewItem item in mainListView.SelectedItems)
            {
                _copied.Add(GetItemByUniqueCode(item.Tag.ToString()));
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutItems();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            CutItems();
        }

        private void cutContext_Click(object sender, EventArgs e)
        {
            CutItems();
        }
        private void CutItems()
        {
            CopyItems();
            RemoveItems();
            ShowItems();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            PasteItems();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteItems();
        }

        private void pasteContext_Click(object sender, EventArgs e)
        {
            PasteItems();
        }
        private void PasteItems()
        {
            foreach (var item in _copied)
            {
                _employees.Add(Employee.DeepCopy(item));
            }
            ShowItems();
        }
        #endregion

        #region Open Save
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void loadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void OpenFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Json Files(*.json)|*.json|All files(*.*)|*.*";
            string str;
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    str = File.ReadAllText(open.FileName);
                    List<Employee> tmpEmployee = new List<Employee>();
                    tmpEmployee = JsonConvert.DeserializeObject<List<Employee>>(str);
                    if (tmpEmployee.Count > 0)
                    {
                        _employees = tmpEmployee;
                        ShowItems();
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        private void addToolStripButton_Click(object sender, EventArgs e)
        {
            AddFromFile();
        }

        private void loadContext_Click(object sender, EventArgs e)
        {
            AddFromFile();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFromFile();
        }
        private void AddFromFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Json Files(*.json)|*.json|All files(*.*)|*.*";
            string str;
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    str = File.ReadAllText(open.FileName);
                    List<Employee> tmpEmployee = new List<Employee>();
                    tmpEmployee = JsonConvert.DeserializeObject<List<Employee>>(str);
                    if (tmpEmployee.Count > 0)
                    {
                        _employees.AddRange(tmpEmployee);
                        ShowItems();
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveContext_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            string str = JsonConvert.SerializeObject(_employees); 
            File.WriteAllText(_path, str);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsAll();
        }
        private void saveAsContext_Click(object sender, EventArgs e)
        {
            SaveAsAll();
        }

        private void SaveAsAll()
        {
            string str = JsonConvert.SerializeObject(_employees);
            SaveAs(str);
        }
        private void saveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsSelected();
        }

        private void saveSelectedContext_Click(object sender, EventArgs e)
        {
            SaveAsSelected();
        }

        private void SaveAsSelected()
        {
            CopyItems();
            string str = JsonConvert.SerializeObject(_copied);
            SaveAs(str);
        }

        private void SaveAs(string str)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Json Files(*.json)|*.json|All files(*.*)|*.*";
            save.FileName = "employees.json";
            if (save.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save.FileName, str);
            }
        }

        #endregion

        #region Hot Keys
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyValue == 'S')
            {
                SaveAsAll();
            }
            else if (e.Control && e.KeyValue == 'S')
            {
                Save();
            }
            else if (e.Control && e.KeyValue == 'N')
            {
                CreateNewItem();
            }
            else if (e.Control && e.KeyValue == 'P')
            {
                PrintItems();
            }
            else if (e.Control && e.KeyValue == 'C')
            {
                CopyItems();
            }
            else if (e.Control && e.KeyValue == 'V')
            {
                PasteItems();
            }
            else if (e.Control && e.KeyValue == 'X')
            {
                CutItems();
            }
            else if (e.Control && e.KeyValue == 'F')
            {
                //FindTrips();
            }
            else if (e.Control && e.KeyValue == 'W')
            {
                ShowItems();
            }
            else if (e.Control && e.KeyValue == 'A')
            {
                SelectAll();
            }
        }
        #endregion

        #region Search
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }
        private void SelectAll()
        {
            var n = mainListView.Items.Count;
            for (var i = 0; i < n; ++i)
            {
                mainListView.Items[i].Selected = true;
                mainListView.Items[i].Focused = true;
            }
        }
        private void searchByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchBy();
        }

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            SearchBy();
        }
        private void SearchBy()
        {
            _temp.Clear();
            SearchByForm form = new SearchByForm();
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                string sur = form.SurName;
                string name = form.FirstName;
                string middle = form.MiddleName;
                string city = form.BirthCity;
                string year = form.Year;

                bool s = true;
                bool n = true;
                bool m = true;
                bool c = true;
                bool y = true;

                foreach (var item in _employees)
                {
                    if (sur != "any") s = item.SurName.ToLower().Contains(sur.ToLower());
                    if (name != "any") n = item.Name.ToLower().Contains(name.ToLower());
                    if (middle != "any") m = item.MiddleName.ToLower().Contains(middle.ToLower());
                    if (city != "any") c = item.BirthCity.ToLower().Contains(city.ToLower());
                    if (year != "any")
                    {
                        int seekYear = 0;
                        if (Int32.TryParse(year, out seekYear)) y = item.BirthDate.Year == seekYear;
                    }

                    if (s && n && m && c && y) _temp.Add(item);
                }
                ShowTemp();
            }
        }
        private void birthdayInBullYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _temp.Clear();
            int year = 1901;
            foreach (var item in _employees)
            {
                if ((item.BirthDate.Year - year) % 12 == 0)
                {
                    _temp.Add(item);
                }
            }
            ShowTemp();
        }
        private void springToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SearchSeasonBirthday(2);
        }
        private void springToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSeasonBirthday(2);
        }
        private void summerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSeasonBirthday(3);
        }

        private void summerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SearchSeasonBirthday(3);
        }

        private void autumnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SearchSeasonBirthday(4);
        }

        private void autumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSeasonBirthday(4);
        }

        private void winterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchSeasonBirthday(1);
        }

        private void winterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SearchSeasonBirthday(1);
        }
        private void SearchSeasonBirthday(int n)
        {
            _temp.Clear();
            foreach (var item in _employees)
            {
                int t = (item.BirthDate.Month + 1) % 12;
                if ((t - 1) / 3 == n - 1) _temp.Add(item);
            }
            ShowTemp();
        }
        #endregion

        #region About Print Exit

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bus Station Info App, v.1.0\nHomework in 'STEP' University\n" +
                "Group: PE911\nStudent: Zyrianov Pavlo");
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PrintItems();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintItems();
        }
        private void PrintItems()
        {
            PrintDocument print = new PrintDocument();
            print.PrintPage += new PrintPageEventHandler(print_PrintPage);
            print.Print();
        }

        private void print_PrintPage(System.Object sender,
           System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, mainListView.Width, mainListView.Height);
            Bitmap bitmap = new Bitmap(mainListView.Width, mainListView.Height);
            mainListView.DrawToBitmap(bitmap, rect);
            e.Graphics.DrawImage(bitmap, new Point(0, 0));
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApp();
        }
        private void exitContext_Click(object sender, EventArgs e)
        {
            ExitApp();
        }
        private void ExitApp()
        {
            var result = MessageBox.Show("Save All to File before closing?", "Checking Saving Dialog", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Save();
            }
            Close();
        }





        #endregion

        #region Sort

        private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();

        private void mainTripsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(e.Column);
        }

        private void SortColumn(int n)
        {
            mainListView.ListViewItemSorter = lvwColumnSorter;

            if (n == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = n;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            mainListView.Sort();
        }





        #endregion

        
    }
}
