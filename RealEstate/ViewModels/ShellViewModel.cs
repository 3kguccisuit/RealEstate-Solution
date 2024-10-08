using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using Microsoft.Extensions.Options;
using RealEstate.Contracts.Services;
using RealEstate.Core.Libs;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Services;
using RealEstate.Properties;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

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

    [ObservableProperty]
    private string fileName;

    // Define the commands for the menu items
    public ICommand NewCommand { get; }

    // JSON
    public ICommand OpenJsonFileCommand { get; }
    public ICommand SaveJsonCommand { get; }
    public ICommand SaveAsJsonFileCommand { get; }

    // XML
    public ICommand OpenXmlFileCommand { get; }
    public ICommand SaveXmlFileCommand { get; }
    public ICommand SaveAsXmlFileCommand { get; }

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

    public ShellViewModel(INavigationService navigationService, EstateManager estateManager, PersonManager personManager, PaymentManager paymentManager)
    {
        _navigationService = navigationService;

        _estateManager = estateManager;
        _personManager = personManager;
        _paymentManager = paymentManager;

        NewCommand = new RelayCommand(OnNew);

        OpenJsonFileCommand = new RelayCommand(OpenJsonFile);
        SaveJsonCommand = new RelayCommand(OnSaveJsonFile);
        SaveAsJsonFileCommand = new RelayCommand(OnSaveAsJsonFile);

        OpenXmlFileCommand = new RelayCommand(OnOpenXmlFile);
        SaveXmlFileCommand = new RelayCommand(OnSaveXmlFile);
        SaveAsXmlFileCommand = new RelayCommand(OnSaveAsXmlFile);


        ExitCommand = new RelayCommand(OnExit);
    }

    private void OnNew()
    {
        // Töm befintliga estates, persons och payments
        _estateManager.Clear();
        _personManager.Clear();
        _paymentManager.Clear();

        // Navigate to Home Page
        _navigationService.NavigateTo(typeof(MainViewModel).FullName);

    }

    #region JSON
    private void OpenJsonFile()
    {
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.FileName = "Document"; // Default file name
        dialog.DefaultExt = ".json"; // Default file extension

        dialog.Filter = "JSON|*.json|All Files|*.*";

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            FileName = dialog.FileName;

            var json = File.ReadAllText(FileName);
            var options = new JsonSerializerOptions
            {
                Converters = {
            new EstateJsonConverter(),  // Custom converter for polymorphic deserialization
            new PersonJsonConverter(),
            new PaymentJsonConverter(),
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        },
                PropertyNameCaseInsensitive = true
            };

            var rootObject = JsonSerializer.Deserialize<RootObject>(json, options);

            // Töm befintliga estates, persons och payments
            _estateManager.Clear();
            _personManager.Clear();
            _paymentManager.Clear();

            // Lägg till nya estates, persons och payments från fil
            foreach (var item in rootObject.EstateList)
            {
                _estateManager.Add(item.ID, item);
            }

            foreach (var item in rootObject.PersonList)
            {
                _personManager.Add(item.ID, item);
            }

            foreach (var item in rootObject.PaymentList)
            {
                _paymentManager.Add(item.ID, item);
            }

            // Navigate to Home Page
            _navigationService.NavigateTo(typeof(MainViewModel).FullName);

        }
    }

    private void OnSaveJsonFile()
    {
        // Logic for "OnSaveJsonFile"


        // Estates
        // Persons
        // 
    }

    private void OnSaveAsJsonFile()
    {
        // Logic for "Save as JSON"
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.SaveFileDialog();
        dialog.FileName = "Document"; // Default file name
        dialog.DefaultExt = ".json"; // Default file extension

        dialog.Filter = "JSON|*.json|All Files|*.*";

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            FileName = dialog.FileName;
            // var mngr = ((App)Application.Current).EstateManager;


            var options = new JsonSerializerOptions
            {
                WriteIndented = true,  // Pretty-print JSON
                Converters = { new EstateJsonConverter(), new PersonJsonConverter() }  // Ensure the custom converter is used
            };

            var json = JsonSerializer.Serialize(new RootObject
            {
                EstateList = _estateManager.GetAll(),
                PersonList = _personManager.GetAll(),
                PaymentList = _paymentManager.GetAll()
            },
                options);

            File.WriteAllText(FileName, json);
        }
    }

    private void OnSave()
    {
    }
    #endregion

    #region XML

    private void OnOpenXmlFile()
    {
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.FileName = "Document"; // Default file name
        dialog.DefaultExt = ".xml"; // Default file extension

        dialog.Filter = "XML|*.xml|All Files|*.*";

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            FileName = dialog.FileName;

            var xml = File.ReadAllText(FileName);

            // Deserialize the XML back to a list
            RootObject rootObject = XmlHelper.DeserializeFromXml<RootObject>(xml);

            // Töm befintliga estates, persons och payments
            _estateManager.Clear();
            _personManager.Clear();
            _paymentManager.Clear();


            // Lägg till nya estates, persons och payments från fil
            foreach (var item in rootObject.EstateList)
            {
                _estateManager.Add(item.ID, item);
            }

            foreach (var item in rootObject.PersonList)
            {
                _personManager.Add(item.ID, item);
            }

            foreach (var item in rootObject.PaymentList)
            {
                _paymentManager.Add(item.ID, item);
            }

            // Navigate to Home Page
            _navigationService.NavigateTo(typeof(MainViewModel).FullName);
        }
    }
    private void OnSaveXmlFile()
    {

    }
    private void OnSaveAsXmlFile()
    {
        // Logic for "Save as JSON"
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.SaveFileDialog();
        dialog.FileName = "Document"; // Default file name
        dialog.DefaultExt = ".xml"; // Default file extension

        dialog.Filter = "XML|*.xml|All Files|*.*";

        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            FileName = dialog.FileName;

            // Serialize the list to XML
            string xml = XmlHelper.SerializeToXml(new RootObject { EstateList = _estateManager.GetAll(), PersonList = _personManager.GetAll(), PaymentList = _paymentManager.GetAll() });
            // string xml = XmlHelper.SerializeToXml(_paymentManager.GetAll());
            File.WriteAllText(FileName, xml);
        }
    }
    #endregion

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

public class RootObject
{
    public List<Estate> EstateList { get; set; }
    public List<Person> PersonList { get; set; }
    public List<Payment> PaymentList { get; set; }
}
