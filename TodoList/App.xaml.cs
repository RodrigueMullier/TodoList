using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TodoList.Services;
using TodoList.Services.Interfaces;
using TodoList.ViewModels;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _servicesProvider;
        public App()
        {
            _servicesProvider = ConfigureServices().BuildServiceProvider();

            InitializeComponent();
        }

        /// <summary>
        /// On startup, show the main window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _servicesProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Dependency injection
        /// </summary>
        /// <returns></returns>
        private static ServiceCollection ConfigureServices()
        {
            ServiceCollection services = new();

            InjectViewModels(ref services);
            InjectServices(ref services);

            services.AddSingleton<Func<Type, BaseViewModel>>(s => viewModelType => (BaseViewModel)s.GetRequiredService(viewModelType));
            services.AddSingleton(s => new MainWindow
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            return services;
        }

        private static void InjectViewModels(ref ServiceCollection services)
        {
            services.AddScoped<MainViewModel>();
            services.AddScoped<HomeViewModel>();
            services.AddScoped<TasksViewModel>();
            services.AddScoped<AddTaskViewModel>();
        }

        private static void InjectServices(ref ServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
        }
    }
}