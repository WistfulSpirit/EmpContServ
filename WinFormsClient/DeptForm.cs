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
    public partial class DeptForm : Form
    {
        public DeptForm()
        {
            InitializeComponent();
            if (Data.dept != null)
            {
                textBoxName.Text = Data.dept.Name;
                buttonDelete.Visible = true;
            }
            else
                buttonDelete.Visible = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxName.Text) || textBoxName.Text.Length > 50)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBoxName, "Поле не должно быть пустым или превышать 50 символов");
                return;
            }
            int id = Data.dept?.Id ?? 0;
            Data.dept = new Dept()
            {
                Id = id,
                Name = textBoxName.Text
            };
            this.DialogResult = DialogResult.OK;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Data.dept.EpmployeesCount > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Будут удалены и все сотрудники принадлежащие этому отделу!\nПродолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult != DialogResult.Yes)
                    return;
            }
            Data.dept = new Dept() { Id = -1 };
            this.DialogResult = DialogResult.OK;
    
        }
    }
}
