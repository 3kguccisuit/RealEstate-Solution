using System.Windows.Controls;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using RealEstate.Contracts.Services;
using RealEstate.ViewModels;
using RealEstate.Views;

namespace RealEstate.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();
    private readonly IServiceProvider _serviceProvider;

    public PageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        Configure<MainViewModel, MainPage>();
        Configure<EstateViewModel, EstatePage>();
        Configure<PersonViewModel, PersonPage>();
        Configure<BankViewModel, BankPage>();
    }

    public Type GetPageType(string key)
    {
        Type pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    public Page GetPage(string key)
    {
        //var pageType = GetPageType(key);
        //return _serviceProvider.GetService(pageType) as Page;
        var pageType = GetPageType(key);
        var page = _serviceProvider.GetRequiredService(pageType) as Page;

        if (page == null)
        {
            throw new InvalidOperationException($"Failed to resolve page for key: {key}");
        }

        return page;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.Any(p => p.Value == type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
