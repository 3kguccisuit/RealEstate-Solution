using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealEstate.Contracts.Services;
using RealEstate.Contracts.Views;
using RealEstateDLL.Helpers;
using RealEstate.Models; 
using RealEstate.Services;
using RealEstate.ViewModels;
using RealEstate.Views;
using RealEstateBLL.Interfaces;
using DTO.Models.BaseModels;
using RealEstateDAL.Files;
using RealEstateDAL.Interfaces;
using RealEstateDLL.Managers;
using Serilog;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using RealEstateBLL.Service;

namespace RealEstate;

// For more information about application lifecycle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview
// create branch
// WPF UI elements use language en-US by default.
// If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
// Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
public partial class App : Application
{
    private IHost _host;

    public T GetService<T>()
        where T : class
        => _host.Services.GetService(typeof(T)) as T;

    public string AppName { get; set; }
    // public string AppLocation { get; set; }
    public App()
    {
        ConfigureLogging();
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        Log.Information("RealEstate has started");
        var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
        _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(appLocation);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

        await _host.StartAsync();
    }

    private void ConfigureLogging()
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() // Set the minimum log level (Debug, Information, Warning, Error, etc.)
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Write logs to a file, one per day
            .CreateLogger();

        // Optional: Log an initial message to confirm setup
        Log.Information("Serilog is configured and ready to log!");
    }
    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // TODO: Register your services, viewmodels and pages here

        // App Host
        services.AddHostedService<ApplicationHostService>();

        // Activation Handlers

        // Core Services
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<DataService>();

        // Services
        services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
        services.AddSingleton<ISystemService, SystemService>();
        services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();

        services.AddSingleton<IPageService, PageService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Register EstateManager as a singleton, so it's available application-wide
        services.AddSingleton<IDictionaryManager<string, Estate>, EstateManager>();
        services.AddSingleton<EstateManager>();

        services.AddSingleton<IDictionaryManager<string, Person>, PersonManager>();
        services.AddSingleton<PersonManager>();

        services.AddSingleton<IDictionaryManager<string, Payment>, PaymentManager>();
        services.AddSingleton<PaymentManager>();

        // Views and ViewModels
        //LH
        services.AddTransient<CreateEstateViewModel>();
        services.AddTransient<EditEstateViewModel>();

        services.AddTransient<CreatePaymentViewModel>();
        services.AddTransient<EditPaymentViewModel>();

        services.AddTransient<IShellWindow, ShellWindow>();
        services.AddTransient<ShellViewModel>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<MainPage>();

        services.AddTransient<EstatePage>();
        services.AddTransient<EstateViewModel>();


        services.AddTransient<PersonViewModel>();
        services.AddTransient<PersonPage>();
        services.AddTransient<CreatePersonViewModel>();
        services.AddTransient<EditPersonViewModel>();

        services.AddTransient<PaymentViewModel>();
        services.AddTransient<PaymentPage>();

        services.AddTransient<SettingsViewModel>();
        services.AddTransient<SettingsPage>();

        services.AddSingleton<AppState>();

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        Log.CloseAndFlush();
        await _host.StopAsync();
        _host.Dispose();
        _host = null;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // TODO: Please log and handle the exception as appropriate to your scenario
        // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}
