using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels.Persons;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class EditEstateViewModel : ObservableObject
    {
        private readonly IDataService<Estate> _estateDataService;
        private readonly IDataService<Person> _personDataService;
        [ObservableProperty]
        private Estate selectedEstate;
        [ObservableProperty]
        private Buyer selectedBuyer;
        [ObservableProperty]
        private Seller selectedSeller;

        public List<Buyer> Buyers { get; private set; } = new List<Buyer>();
        public List<Seller> Sellers { get; private set; } = new List<Seller>();
        public List<Country> Countries { get; }
        public List<LegalFormType> LegalForms { get; }

        // Constructor for dependency injection and initializing data
        public EditEstateViewModel(IDataService<Estate> estateDataService, IDataService<Person> personDataService)
        {
            _estateDataService = estateDataService;
            _personDataService = personDataService;

            // Populate the list of countries and legal forms
            Countries = Enum.GetValues(typeof(Country)).Cast<Country>().ToList();
            LegalForms = Enum.GetValues(typeof(LegalFormType)).Cast<LegalFormType>().ToList();

        }

        private async Task LoadBuyersAndSellers()
        {
            var persons = await _personDataService.GetAsync();

            Buyers = persons.OfType<Buyer>().ToList();
            Sellers = persons.OfType<Seller>().ToList();

            if (SelectedEstate.LinkedBuyer != null)
                SelectedBuyer = Buyers.FirstOrDefault(e => e.ID == SelectedEstate.LinkedBuyer.ID);
            if (SelectedEstate.LinkedSeller != null)
                SelectedSeller = Sellers.FirstOrDefault(e => e.ID == SelectedEstate.LinkedSeller.ID);
        }

        // Method to initialize the estate with an existing estate
        public async Task InitializeEstate(Estate estate)
        {
            SelectedEstate = estate;
            await LoadBuyersAndSellers();
        }
        [RelayCommand]
        private async Task Save(Window window)
        {
            SelectedEstate.LinkedBuyer = SelectedBuyer;
            SelectedEstate.LinkedSeller = SelectedSeller;
            MessageBox.Show($"Updated: {SelectedEstate}");
            await _estateDataService.UpdateAsync(SelectedEstate);
            window.Close();
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            var app = (App)Application.Current;
            var appName = app.AppName;

            var result = MessageBox.Show("Do you really want to cancel editing?", appName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                window.Close();
            }
        }

        [RelayCommand]
        private void SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedEstate.ImagePath = openFileDialog.FileName;
            }
        }
    }
}
