using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient.Controllers;
using WinFormsClient.Models;

namespace WinFormsClient
{
    public partial class Form1 : Form
    {
        HttpClient client = new HttpClient();
        EmpController empController;
        DeptController deptController;
        List<Dept> depts;

        public Form1()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:53834/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            empController = new EmpController(client);
            deptController = new DeptController(client);
            this.Load += Form1_LoadAsync;
        }

        private void Form1_LoadAsync(object sender, EventArgs e)
        {
            FillDepts();
            listBoxDept.SelectedIndexChanged += listBoxDept_SelectedIndexChanged;
        }

        private void listBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDept.SelectedItem != null)
                FillEmps(((Dept)listBoxDept.SelectedItem).Id);
        }

        #region Dept

        public async void FillDepts()
        {
            try
            {
                depts = await deptController.GetAsync();
                foreach (var d in depts)
                {
                    listBoxDept.DataSource = depts;
                    listBoxDept.ValueMember = "Id";
                    listBoxDept.DisplayMember = "Name";
                    listBoxDept.ContextMenuStrip = contextMenuDept;
                }
                Data.deptList = depts;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу" + ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
        }

        public async Task CreateDept(Dept dept)
        {
            try
            {
                await deptController.CreateAsync(dept);
                MessageBox.Show("Отдел создан успешно", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу. " + ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
        }

        public async Task DeleteDept(int id)
        {
            try
            {
                await deptController.DeleteAsync(id);
                MessageBox.Show("Отдел удален. ", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу. " + ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
        }

        public async Task EditDept(Dept dept)
        {
            try
            {
                await deptController.EditAsync(dept);
                MessageBox.Show("Отдел отредактирован успешно", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу. " + ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }

        }

        private async void AddDept_Click(object sender, EventArgs e)
        {
            Data.dept = null;
            DeptForm deptForm = new DeptForm();
            if (deptForm.ShowDialog() == DialogResult.OK)
            {
                if (Data.dept != null)
                {
                    if (Data.dept.Id == 0)
                        await CreateDept(Data.dept);
                }
            }
            FillDepts();
            Data.dept = null;
        }

        private async void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxDept.SelectedItem != null)
            {
                var dept = (Dept)listBoxDept.SelectedItem;
                Data.dept = await deptController.GetAsync(dept.Id);
                DeptForm deptForm = new DeptForm();
                if (deptForm.ShowDialog() == DialogResult.OK)
                {
                    if (Data.dept != null)
                    {
                        if (Data.dept.Id == -1)
                            await DeleteDept(dept.Id);
                        else
                            await EditDept(Data.dept);
                    }
                }
                FillDepts();
                Data.dept = null;
            }
        }

        #endregion



        #region Emp

        public async void FillEmps(int id)
        {
            try
            {
                List<Employee> res = await empController.GetAsync(id);
                dataGridViewEmp.DataSource = res;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу" + ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
        }

        public async Task CreateEmp(Employee emp)
        {
            try
            {
                await empController.CreateAsync(emp);
                MessageBox.Show("Сотрудник добавлен успешно", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу. " + ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
        }

        public async Task EditEmp(Employee emp)
        {
            try
            {
                await empController.EditAsync(emp);
                MessageBox.Show("Сотрудник редактирован успешно", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу. " + ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
        }

        public async Task DeleteEmp(int id)
        {
            try
            {
                await empController.DeleteAsync(id);
                MessageBox.Show("Сотрудник удален. ", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу. " + ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
        }

        private async void dataGridViewEmp_DoubleClick(object sender, EventArgs e)
        {
            var emp = (Employee)dataGridViewEmp.CurrentRow.DataBoundItem;
            Data.employee = emp;
            Data.deptList = depts;
            EmpForm empForm = new EmpForm();
            if (empForm.ShowDialog() == DialogResult.OK)
            {
                if (Data.employee != null)
                {
                    if (Data.employee.Id == -1)
                        await DeleteEmp(emp.Id);
                    else
                        await EditEmp(Data.employee);
                }
                FillEmps(emp.Dept_Id);

            }
            depts = await deptController.GetAsync();
            Data.employee = null;
        }



        private async void AddEmp_Click(object sender, EventArgs e)
        {
            Data.employee = new Employee() { Id = 0, Dept_Id = ((Dept)listBoxDept.SelectedItem).Id };
            Data.deptList = depts;
            EmpForm empForm = new EmpForm();
            if (empForm.ShowDialog() == DialogResult.OK)
            {
                if (Data.employee != null)
                {
                    await CreateEmp(Data.employee);
                }
                FillEmps(Data.employee.Dept_Id);
            }
            Data.employee = null;
        }

        #endregion

    }
}
