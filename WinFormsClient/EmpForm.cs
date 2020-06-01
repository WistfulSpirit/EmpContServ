using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient.Models;

namespace WinFormsClient
{
    public partial class EmpForm : Form
    {
        public EmpForm()
        {
            InitializeComponent();
            var departList = Data.deptList;
            var depList = new BindingList<Dept>(departList.OrderBy(x => x.Id).ToList());
            comboBoxDept.ValueMember = "Id";
            comboBoxDept.DisplayMember = "Name";
            comboBoxDept.DataSource = depList;
            if (Data.employee != null && Data.employee.Id != 0)
            {
                Fill();
                buttonDelete.Visible = true;
            }
            else
                buttonDelete.Visible = false;
            if (Data.employee != null)
            {

                var dep = depList.Where(x => x.Id == Data.employee.Dept_Id).ElementAt(0);
                comboBoxDept.SelectedIndex = comboBoxDept.FindStringExact(dep.Name);
            }
        }


        private void Fill()
        {
            textBoxName.Text = Data.employee.Name;
            textBoxSurname.Text = Data.employee.Surname;
            textBoxPatronymic.Text = Data.employee.Patronymic;
            dateTimePickerBD.Value = Data.employee.Birthday;
            textBoxAdress.Text = Data.employee.Address;
            textBoxAbout.Text = Data.employee.About;
            var dep = Data.deptList.Where(x => x.Id == Data.employee.Dept_Id).ElementAt(0);
            comboBoxDept.SelectedIndex = comboBoxDept.FindString(dep.Name);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;
            int id = Data.employee?.Id ?? 0;
            Data.employee = new Employee()
            {
                Id = id,
                Name = textBoxName.Text,
                Surname = textBoxSurname.Text,
                Patronymic = textBoxPatronymic.Text,
                Birthday = dateTimePickerBD.Value,
                Address = textBoxAdress.Text,
                About = textBoxAbout.Text,
                Dept_Id = ((Dept)comboBoxDept.SelectedItem).Id
            };
            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateForm()
        {

            bool valid = true;
            errorProvider1.Clear();

            if (String.IsNullOrWhiteSpace(textBoxName.Text) || textBoxName.Text.Length > 20)
            {
                errorProvider1.SetError(textBoxName, "Поле не должно быть пустым или превышать 50 символов");
                valid = false;
            }
            if (String.IsNullOrWhiteSpace(textBoxSurname.Text) || textBoxSurname.Text.Length > 25)
            {
                errorProvider1.SetError(textBoxSurname, "Поле не должно быть пустым или превышать 50 символов");
                valid = false;
            }
            if (textBoxPatronymic.Text.Length > 30)
            {
                errorProvider1.SetError(textBoxPatronymic, "Содержание не должно превышать 30 символов");
                valid = false;
            }
            if (textBoxAdress.Text.Length > 60)
            {
                errorProvider1.SetError(textBoxSurname, "Содержание не должно превышать 60 символов");
                valid = false;
            }
            if (textBoxAbout.Text.Length > 500)
            {
                errorProvider1.SetError(textBoxSurname, "Содержание не должно превышать 500 символов");
                valid = false;
            }
            if (dateTimePickerBD.Value == null)
            {
                errorProvider1.SetError(dateTimePickerBD, "Поле не должно быть пустым");
                valid = false;
            }

            return valid;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int id = -1;
            Data.employee = new Employee()
            {
                Id = id
            };
            this.DialogResult = DialogResult.OK;
        }
    }
}
