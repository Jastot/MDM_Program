using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arhive_MDM.Data.Repositories;

namespace Arhive_MDM.Forms
{
    public partial class AdminForm : Form
    {
        private readonly IWorkerRepository _workersRepository;
        private readonly IOrdersRepository _ordersRepository;
        private int _selectedUserId;

        public AdminForm()
        {
            _workersRepository = (IWorkerRepository)Program.ServiceProvider.GetService(typeof(IWorkerRepository));
            _ordersRepository = (IOrdersRepository)Program.ServiceProvider.GetService(typeof(IOrdersRepository));

            InitializeComponent();

            dataGridViewUsers.RowHeadersVisible = false;
            dataGridViewUsers.ColumnCount = 6;
            dataGridViewUsers.Columns[0].HeaderText = "ФИО";
            dataGridViewUsers.Columns[1].HeaderText = "Логин";
            dataGridViewUsers.Columns[2].HeaderText = "Должность";
            dataGridViewUsers.Columns[3].HeaderText = "Зарплата";
            UsersGridWidthChange();
            
            
            dataGridViewUsers.Columns[4].Visible = false;
            dataGridViewUsers.Columns[5].Visible = false;
            
            comboBoxRole.Items.AddRange(new string[] { "Бухгалтер",
                "Менеджер","Архивариус","Администратор"});
            comboBoxRole.SelectedIndex = 0;
        }

        private async Task UpdateDataGridView()
        {
            UsersGridWidthChange();
            var users = await _workersRepository.GetWorkers();
            dataGridViewUsers.Rows.Clear();
            dataGridViewUsers.Columns[0].Width = users.Count > 3 ? 313 : 330;
            foreach (var user in users)
            {
                var role = user.Role.Equals("accountant") ? "Бухгалтер" 
                    : user.Role.Equals("manager") ? "Менеджер" 
                    : user.Role.Equals("archivarius") ? "Архивариус"
                    : user.Role.Equals("admin") ? "Администратор"
                    : "Неизвестно";
                dataGridViewUsers.Rows.Add(new[] { user.FIO, user.Login, role, user.Salary.ToString() ,user.Id.ToString(), user.Password });
            }
            ClearDataGridViewSelection();
        }

        private void ClearDataGridViewSelection()
        {
            UsersGridWidthChange();
            dataGridViewUsers.ClearSelection();
            dataGridViewUsers.CurrentCell = null;
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            UsersGridWidthChange();
            textBoxName.Text = "";
            textBoxLogin.Text = "";
            textBoxPassword.Text = "";
            textBoxSalary.Text = "";
        }

        private bool VerifyTextBoxes(out string name, out string login, out string password, out string salary)
        {
            UsersGridWidthChange();
            name = textBoxName.Text;
            login = textBoxLogin.Text;
            password = textBoxPassword.Text;
            salary = textBoxSalary.Text;

            var message = "";
            message += name.Length < 3 ? "Поле \"ФИО\" должно содержать минимум 3 символа.\n" : "";
            message += login.Length < 3
                ? "Поле \"Логин\" должно содержать минимум 3 символа.\n"
                : login.Equals("administrator")? "Логин \"administrator\" недоступен.\n" : "";
            message += password.Length < 8 ? "Поле \"Пароль\" должно содержать минимум 8 символов.\n" : "";

            message += Convert.ToSingle(salary) < 25000 ? "Поле \"Зарплата\" должно содержать значение больше 25000\n" : "";

            if (!message.Equals(""))
            {
                MessageBox.Show(message, "Валидация данных пользователя");
                return false;
            }
            return true;
        }

        private async void ButtonAdd_Click(object sender, EventArgs e)
        {
            UsersGridWidthChange();
            if (!VerifyTextBoxes(out var fio, out var login, out var password, out var salary))
            {
                return;
            }

            var user = new Models.Worker()
            {
                FIO = fio,
                Login = login,
                Password = password,
                Role = comboBoxRole.SelectedIndex == 0 ? "accountant" : 
                comboBoxRole.SelectedIndex == 1 ? "manager" : 
                comboBoxRole.SelectedIndex == 2 ? "archivarius" : 
                 "admin",
                Salary = Convert.ToSingle(salary)
            };

            if (await _workersRepository.GetWorker(login) != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует.");
                return;
            }

            await _workersRepository.CreateWorker(user);
            await UpdateDataGridView();
        }

        private async void ButtonEdit_Click(object sender, EventArgs e)
        {
            UsersGridWidthChange();
            if (!VerifyTextBoxes(out var fio, out var login, out var password, out var salary))
            {
                return;
            }

            var user = await _workersRepository.GetWorker(_selectedUserId);
            if (!user.Login.Equals(login))
            {
                var userByLogin = await _workersRepository.GetWorker(login);
                if (userByLogin != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.");
                    return;
                }
                user.Login = login;
            }
            user.FIO = fio;
            user.Password = password;
            user.Role = comboBoxRole.SelectedIndex == 0 ? "accountant" :
                comboBoxRole.SelectedIndex == 1 ? "manager" :
                comboBoxRole.SelectedIndex == 2 ? "archivarius" :
                 "admin";
            user.Salary = Convert.ToSingle(salary);

            await _workersRepository.UpdateWorker(user);
            await UpdateDataGridView();
        }

        private async void ButtonRemove_Click(object sender, EventArgs e)
        {
            UsersGridWidthChange();
            var userOrders = await _ordersRepository.GetWorkerOrders(_selectedUserId);
            if (userOrders.Count > 0)
            {
                MessageBox.Show("Нельзя удалить пользователя, за которым закреплены заказы.");
                return;
            }
            var user = await _workersRepository.GetWorker(_selectedUserId);
            await _workersRepository.RemoveWorker(user);
            await UpdateDataGridView();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            dataGridViewUsers.Columns[0].Width = 90;
            ClearDataGridViewSelection();
        }

        private void DataGridViewUsers_SelectionChanged(object sender, EventArgs e)
        {
            UsersGridWidthChange();
            var selectedRows = dataGridViewUsers.SelectedRows;
           
            var userIsSelected = selectedRows.Count > 0;
            groupBoxUserValues.Text = userIsSelected
                ? "Редактирование пользователя"
                : "Добавление пользователя";
            buttonAdd.Enabled = !userIsSelected;
            buttonEdit.Enabled = userIsSelected;
            buttonRemove.Enabled = userIsSelected;

            if (userIsSelected)
            {
                var selectedRow = selectedRows[0];
                textBoxName.Text = selectedRow.Cells[0].Value.ToString();
                textBoxLogin.Text = selectedRow.Cells[1].Value.ToString();
                comboBoxRole.SelectedIndex = 
                    selectedRow.Cells[2].Value.ToString().Equals("Бухгалтер") ? 0 
                    : selectedRow.Cells[2].Value.ToString().Equals("Менеджер") ? 1
                    : selectedRow.Cells[2].Value.ToString().Equals("Архивариус") ? 2
                    : 3;
                textBoxSalary.Text = selectedRow.Cells[3].Value.ToString();
                _selectedUserId = Convert.ToInt32(selectedRow.Cells[4].Value.ToString());
                textBoxPassword.Text = selectedRow.Cells[5].Value.ToString();
                
            }
        }

        private async void AdminForm_Shown(object sender, EventArgs e)
        {
            await UpdateDataGridView();
            UsersGridWidthChange();
            DataGridViewUsers_SelectionChanged(sender, e);
        }

        private void UsersGridWidthChange()
        {
            dataGridViewUsers.Columns[0].Width = 220;
            dataGridViewUsers.Columns[1].Width = 110;
            dataGridViewUsers.Columns[2].Width = 90;
            dataGridViewUsers.Columns[3].Width = 90;
        }
        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.LoginForm.Show();
        }
    }
}
