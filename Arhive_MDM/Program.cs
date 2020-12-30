using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Arhive_MDM.Data;
using Arhive_MDM.Data.Repositories;

namespace BarbershopMDM
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }

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
            Application.Run(/*new SmthForm()*/);
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
