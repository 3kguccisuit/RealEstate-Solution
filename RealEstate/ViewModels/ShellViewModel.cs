using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using RealEstate.Contracts.Services;
using RealEstate.Helpers;
using RealEstate.Properties;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using RealEstate.Models;
using RealEstateDLL.Managers;
using RealEstateDAL.Enums;

namespace RealEstate.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private HamburgerMenuItem _selectedMenuItem;
    private HamburgerMenuItem _selectedOptionsMenuItem;
    private RelayCommand _goBackCommand;
    private ICommand _menuItemInvokedCommand;
    private ICommand _optionsMenuItemInvokedCommand;
    private ICommand _loadedCommand;
    private ICommand _unloadedCommand;
    private readonly EstateManager _estateManager;
    private readonly PersonManager _personManager;
    private readonly PaymentManager _paymentManager;
    private readonly FileDataHandler _fileDataHandler;

    private AppState _appState;

    //[ObservableProperty]
    //private string fileName;

    // Define the commands for the menu items
    public ICommand NewCommand { get; }

    // JSON
    public ICommand OpenJsonFileCommand { get; }
    //public ICommand SaveJsonCommand { get; }
    public ICommand SaveAsJsonFileCommand { get; }

    // XML
    public ICommand OpenXmlFileCommand { get; }
    //public ICommand SaveXmlFileCommand { get; }
    public ICommand SaveAsXmlFileCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand ExitCommand { get; }

    public HamburgerMenuItem SelectedMenuItem
    {
        get { return _selectedMenuItem; }
        set { SetProperty(ref _selectedMenuItem, value); }
    }

    public HamburgerMenuItem SelectedOptionsMenuItem
    {
        get { return _selectedOptionsMenuItem; }
        set { SetProperty(ref _selectedOptionsMenuItem, value); }
    }

    // TODO: Change the icons and titles for all HamburgerMenuItems here.
    public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
    {
        new HamburgerMenuGlyphItem() { Label = Resources.ShellMainPage, Glyph = "\uE80F", TargetPageType = typeof(MainViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellListDetailsPage, Glyph = "\uEC06", TargetPageType = typeof(EstateViewModel) },

        
        //new HamburgerMenuGlyphItem() { Label = Resources.ShellAddEstatePage, Glyph = "\uE8A5", TargetPageType = typeof(AddEstateViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellPersonPage, Glyph = "\uE716", TargetPageType = typeof(PersonViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellBankPage, Glyph = "\uE825", TargetPageType = typeof(PaymentViewModel) },
    };

    public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
    {
        new HamburgerMenuGlyphItem() { Label = Resources.ShellSettingsPage, Glyph = "\uE713", TargetPageType = typeof(SettingsViewModel) }
    };

    public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

    public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new RelayCommand(OnMenuItemInvoked));

    public ICommand OptionsMenuItemInvokedCommand => _optionsMenuItemInvokedCommand ?? (_optionsMenuItemInvokedCommand = new RelayCommand(OnOptionsMenuItemInvoked));

    public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

    public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

    public ShellViewModel(INavigationService navigationService, FileDataHandler fileDataHandler, EstateManager estateManager, PersonManager personManager, PaymentManager paymentManager, AppState appState)
    {
        _navigationService = navigationService;

        _estateManager = estateManager;
        _personManager = personManager;
        _paymentManager = paymentManager;

        _appState = appState;
        _fileDataHandler = fileDataHandler;

        OpenJsonFileCommand = new RelayCommand(_fileDataHandler.OpenJsonFile);
        SaveAsJsonFileCommand = new RelayCommand(_fileDataHandler.SaveAsJsonFile);
        OpenXmlFileCommand = new RelayCommand(_fileDataHandler.OpenXmlFile);
        SaveAsXmlFileCommand = new RelayCommand(_fileDataHandler.SaveAsXmlFile);
        SaveCommand = new RelayCommand(_fileDataHandler.Save);

        NewCommand = new RelayCommand(OnNew);
        ExitCommand = new RelayCommand(OnExit);
    }

    private void OnNew()
    {
        if (_appState.IsDirty)
        {
            var app = (App)Application.Current;
            var appName = app.AppName;

            var result = MessageBox.Show("You have unsaved changes! Do you want to save them before creating a new RealEstate?", appName, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _fileDataHandler.Save();  // Delegate file saving to FileDataHandler
            }
            else if (result == MessageBoxResult.Cancel)
            {
                return;  // User canceled the operation
            }
        }
        // Töm befintliga estates, persons och payments
        _estateManager.Clear();
        _personManager.Clear();
        _paymentManager.Clear();

        // Set AppState
        _appState.IsDirty = false;
        _appState.Format = FileFormats.Unknown;
        _appState.FileName = "";

        // Navigate to Home Page
        _navigationService.NavigateTo(typeof(MainViewModel).FullName);
    }

    private void OnExit()
    {
        System.Windows.Application.Current.Shutdown();
    }

    private void OnLoaded()
    {
        _navigationService.Navigated += OnNavigated;
    }

    private void OnUnloaded()
    {
        _navigationService.Navigated -= OnNavigated;
    }

    private bool CanGoBack()
        => _navigationService.CanGoBack;

    private void OnGoBack()
        => _navigationService.GoBack();

    private void OnMenuItemInvoked()
        => NavigateTo(SelectedMenuItem.TargetPageType);

    private void OnOptionsMenuItemInvoked()
        => NavigateTo(SelectedOptionsMenuItem.TargetPageType);

    private void NavigateTo(Type targetViewModel)
    {
        if (targetViewModel != null)
        {
            _navigationService.NavigateTo(targetViewModel.FullName);
        }
    }

    private void OnNavigated(object sender, string viewModelName)
    {
        var item = MenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
        if (item != null)
        {
            SelectedMenuItem = item;
        }
        else
        {
            SelectedOptionsMenuItem = OptionMenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
        }

        GoBackCommand.NotifyCanExecuteChanged();
    }
}


