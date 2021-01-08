using System;
using System.Windows.Forms;
using Arhive_MDM.Data.Repositories;
using Arhive_MDM;
using System.Threading.Tasks;

namespace Arhive_MDM.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IWorkerRepository _workersRepo;
        private bool IsAdminCreated = false;
        private FileManager LogFileManager;
        public LoginForm()
        {
            _workersRepo = (IWorkerRepository)Program.ServiceProvider.GetService(typeof(IWorkerRepository));
            InitializeComponent();
            LogFileManager = new FileManager();
        }

        private async Task FindAdmin()
        {
            var allusers = await _workersRepo.GetWorkers();
            foreach (var user in allusers)
            {
                if (user.Role == "admin")
                    IsAdminCreated = true;
            }
            
        }

        private async void ButtonEnter_Click(object sender, EventArgs e)
        {
            var login = textBoxLogin.Text;
            var password = textBoxPassword.Text;
            FindAdmin();
            if (IsAdminCreated == false) 
            {
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
                    new MainAccounatantForm(user.Id, LogFileManager).ShowDialog();
                    break;
                case "manager":
                    Hide();
                    new ManagerForm(user.Id).ShowDialog();
                    break;
                case "archivarius":
                    Hide();
                    new ArchivariusForm(user.Id, LogFileManager).ShowDialog();
                    break;
                case "admin":
                    Hide();
                    new AdminForm().ShowDialog();
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
