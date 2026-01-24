using System.Configuration;
using System.Data;
using System.Windows;
using BudgetPlanner2._0.Data;
using BudgetPlanner2._0.Services;
using BudgetPlanner2._0.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetPlanner2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<TransactionService>();
            services.AddSingleton<CategoryService>();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

}
