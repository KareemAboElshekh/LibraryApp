
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LibraryApp.ViewModels;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.BusinessLogic.Services;
using LibraryApp.ViewModels.ViewModels;
using Microsoft.Extensions.Configuration;
using LibraryApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using LibraryApp.DesktopApp.Views; 

namespace LibraryApp.DesktopApp
{
    public partial class App : Application
    {
        private readonly IHost _host;
        public static IHost Host => ((App)Current)._host;
        public App()
        {
            _host = Microsoft.Extensions.Hosting.Host   
            .CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // تحميل الإعدادات من appsettings.json
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
               .ConfigureServices((ctx, services) =>
               {
                   var connStr = ctx.Configuration.GetConnectionString("Default");

                   // 1) سجل Factory بدلاً من DbContext وحيد
                   services.AddDbContextFactory<LibraryDbContext>(opts =>
     opts.UseSqlServer(connStr));

                   services.AddScoped<IUserService, UserService>();
                   services.AddScoped<IBookService, BookService>();
                   services.AddSingleton<IUserSessionService, UserSessionService>();

                   services.AddTransient<LoginViewModel>();
                   services.AddTransient<WelcomeViewModel>();
                   services.AddTransient<ShellViewModel>();
                   services.AddTransient<ChangePasswordViewModel>();
                   services.AddTransient<UserManagementViewModel>();
                   services.AddTransient<BookReservationViewModel>();
                   services.AddTransient<BookManagementViewModel>();

                   services.AddTransient<LoginWindow>();
                   services.AddTransient<WelcomeWindow>();
                   services.AddTransient<ShellWindow>();
                   services.AddTransient<ChangePasswordView>();
                   services.AddTransient<UserManagementView>();
                   services.AddTransient<BookReservationView>();
                   services.AddTransient<BookManagementView>();

               })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var login = Host.Services.GetRequiredService<LoginWindow>();
            login.Show();

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
            _host.Dispose();
            base.OnExit(e);
        }
    }
}