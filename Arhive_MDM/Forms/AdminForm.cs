using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arhive_MDM.Data.Repositories;

namespace Arhive_MDM.Forms
{
    public partial class AdminForm : Form
    {
        private readonly IWorkerRepository _workersRepository;
        private readonly ICaseRepository _casesRepository;
        private int _selectedUserId;

        public AdminForm()
        {
            _workersRepository = (IWorkerRepository)Program.ServiceProvider.GetService(typeof(IWorkerRepository));
            _casesRepository = (ICaseRepository)Program.ServiceProvider.GetService(typeof(ICaseRepository));

            InitializeComponent();

            dataGridViewUsers.RowHeadersVisible = false;
            dataGridViewUsers.ColumnCount = 5;
            dataGridViewUsers.Columns[0].HeaderText = "ФИО";
            dataGridViewUsers.Columns[1].HeaderText = "Логин";
            dataGridViewUsers.Columns[2].HeaderText = "Должность";
            dataGridViewUsers.Columns[1].Width = 110;
            dataGridViewUsers.Columns[2].Width = 90;
            dataGridViewUsers.Columns[3].Visible = false;
            dataGridViewUsers.Columns[4].Visible = false;
            
            comboBoxRole.Items.AddRange(new string[] { "Бухгалтер",
                "Менеджер","Архивариус","Администратор"});
            comboBoxRole.SelectedIndex = 0;
        }

        private async Task UpdateDataGridView()
        {
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
                dataGridViewUsers.Rows.Add(new[] { user.FIO, user.Login, role, user.Id.ToString(), user.Password });
            }
            ClearDataGridViewSelection();
        }

        private void ClearDataGridViewSelection()
        {
            dataGridViewUsers.ClearSelection();
            dataGridViewUsers.CurrentCell = null;
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            textBoxName.Text = "";
            textBoxLogin.Text = "";
            textBoxPassword.Text = "";
        }

        private bool VerifyTextBoxes(out string name, out string login, out string password)
        {
            name = textBoxName.Text;
            login = textBoxLogin.Text;
            password = textBoxPassword.Text;

            var message = "";
            message += name.Length < 3 ? "Поле \"ФИО\" должно содержать минимум 3 символа.\n" : "";
            message += login.Length < 3
                ? "Поле \"Логин\" должно содержать минимум 3 символа.\n"
                : login.Equals("administrator")? "Логин \"administrator\" недоступен.\n" : "";
            message += password.Length < 8 ? "Поле \"Пароль\" должно содержать минимум 8 символов.\n" : "";

            if (!message.Equals(""))
            {
                MessageBox.Show(message, "Валидация данных пользователя");
                return false;
            }
            return true;
        }

        private async void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (!VerifyTextBoxes(out var fio, out var login, out var password))
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
                 "admin"
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
            if (!VerifyTextBoxes(out var fio, out var login, out var password))
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

            await _workersRepository.UpdateWorker(user);
            await UpdateDataGridView();
        }

        private async void ButtonRemove_Click(object sender, EventArgs e)
        {
            var userOrders = await _casesRepository.GetWorkerCases(_selectedUserId);
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
            ClearDataGridViewSelection();
        }

        private void DataGridViewUsers_SelectionChanged(object sender, EventArgs e)
        {
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
                // нужно еще глянуть 181
                var selectedRow = selectedRows[0];
                textBoxName.Text = selectedRow.Cells[0].Value.ToString();
                textBoxLogin.Text = selectedRow.Cells[1].Value.ToString();
                comboBoxRole.SelectedIndex = selectedRow.Cells[2].Value.ToString().Equals("Бухгалтер") ? 0 : 1;
                _selectedUserId = Convert.ToInt32(selectedRow.Cells[3].Value.ToString());
                textBoxPassword.Text = selectedRow.Cells[4].Value.ToString();
            }
        }

        private async void AdminForm_Shown(object sender, EventArgs e)
        {
            await UpdateDataGridView();
            DataGridViewUsers_SelectionChanged(sender, e);
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.LoginForm.Show();
        }
    }
}
