using System.Configuration;
using System.Data;
using System.Windows;
using BudgetPlanner2._0.Data;
using BudgetPlanner2._0.Repositories;
using BudgetPlanner2._0.Services;
using BudgetPlanner2._0.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BudgetPlanner2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private IServiceProvider serviceProvider;
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    var serviceCollection = new ServiceCollection();
        //    ConfigureServices(serviceCollection);

        //    serviceProvider = serviceCollection.BuildServiceProvider();

        //    var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        //    mainWindow.Show();
        //}



        //private void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddLogging();
        //    services.AddSingleton<MainViewModel>();
        //    services.AddSingleton<MainWindow>();
        //    services.AddSingleton<TransactionService>();
        //    services.AddSingleton<CategoryService>();
        //}

        //private void OnExit(object sender, ExitEventArgs e)
        //{
        //    if (serviceProvider is IDisposable disposable)
        //    {
        //        disposable.Dispose();
        //    }
        //}
        //private readonly IHost host;
        public new static App Current => (App)Application.Current;
        public IHost Host { get; }
        public App()
        {
            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BudgetPlanner2.0;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");
                    });

                    services.AddScoped<ITransactionRepository, TransactionRepository>();
                    services.AddScoped<ICategoryRepository, CategoryRepository>();
                    services.AddScoped<DataSeederService>();
                    services.AddTransient<ManageCategoryViewModel>();

                    services.AddSingleton<TransactionService>();
                    services.AddSingleton<CategoryService>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await Host.StartAsync();

            using(var scope = Host.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeederService>();
                await seeder.SeedAsync();
            }

            var mainWindow = Host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await Host.StopAsync();
            Host.Dispose();
            base.OnExit(e);
        }

    }
}
