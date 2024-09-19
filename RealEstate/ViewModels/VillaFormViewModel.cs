using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models;
using RealEstate.Core.Enums;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Services;

namespace RealEstate.ViewModels
{
    public partial class VillaFormViewModel : ObservableObject
    {
        private readonly IEstateDataService _estateDataService;
        [ObservableProperty]
        private Villa villa;
        public List<Country> Countries { get; }
        public List<LegalFormType> LegalForms { get; }
        public VillaFormViewModel(IEstateDataService estateDataService)
        {
            _estateDataService = estateDataService;
            // Initialize Villa object with default values
            Villa = new Villa(0,
                        new Address("Street name","Zip code","City",
                        Country.Sverige), new LegalForm(LegalFormType.Ownership),
                        0,
                        0,
                        false);

            // Populate the list of countries and legal form types
            Countries = Enum.GetValues(typeof(Country)).Cast<Country>().ToList();
            LegalForms = Enum.GetValues(typeof(LegalFormType)).Cast<LegalFormType>().ToList();
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            var app = (App)Application.Current;
            var appName = app.AppName;

            var ret = MessageBox.Show("Do you really want to cancel?", appName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ret == MessageBoxResult.Yes)
                window.Close();
        }

        [RelayCommand]
        private async Task Create(Window window)
        {
            var app = (App)Application.Current;
            var appName = app.AppName;
            await _estateDataService.AddEstateAsync(Villa);
            var ret = MessageBox.Show($"Villa created with propeties {Villa}", appName, MessageBoxButton.OK);
            window.Close();
        }

    }
}
