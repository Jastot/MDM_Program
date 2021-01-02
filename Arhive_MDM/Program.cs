using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Arhive_MDM.Data;
using Arhive_MDM.Data.Repositories;
using Arhive_MDM.Forms;

namespace Arhive_MDM
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static Form LoginForm { get; set; }

        [STAThread]
        static void Main()
        {

            using (var db = new DataContext())
            {
                db.Database.Migrate();
            }

            ConfigureServices();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm = new LoginForm();
            Application.Run(LoginForm);
        }

        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<DataContext>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<ICaseRepository, CaseRepository>();
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
