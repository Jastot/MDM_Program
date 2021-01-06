using System;
using System.Windows.Forms;
using Arhive_MDM.Data.Repositories;
using Arhive_MDM;

namespace Arhive_MDM.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IWorkerRepository _workersRepo;

        public LoginForm()
        {
            _workersRepo = (IWorkerRepository)Program.ServiceProvider.GetService(typeof(IWorkerRepository));
            InitializeComponent();
        }

        private async void ButtonEnter_Click(object sender, EventArgs e)
        {
            var login = textBoxLogin.Text;
            var password = textBoxPassword.Text;

            if (login == "admin")
            {
                if (password == "admin")
                {
                    Hide();
                    new AdminForm().ShowDialog();
                    return;
                }
                MessageBox.Show("Неверный логин или пароль.");
                return;
            }

            var user = await _workersRepo.GetWorker(login);
            if (user == null || user.Password != password)
            {
                MessageBox.Show("Неверный логин или пароль.");
                return;
            }

            switch (user.Role)
            {
                case "accountant":
                    Hide();
                    new AccountantForm(user.Id).ShowDialog();
                    break;
                case "manager":
                    Hide();
                    new ManagerForm(user.Id).ShowDialog();
                    break;
                case "archivatius":
                    Hide();

                    break;
                default:
                    MessageBox.Show("Ошибка 1: Неопознанная роль пользователя.");
                    break;
            }
        }

        private void LoginForm_VisibleChanged(object sender, EventArgs e)
        {
            textBoxPassword.Text = "";
        }
    }
}
