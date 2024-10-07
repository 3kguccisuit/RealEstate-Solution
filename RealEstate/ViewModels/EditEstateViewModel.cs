using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels.Persons;
using RealEstate.Core.Services;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class EditEstateViewModel : ObservableObject
    {
        //private readonly IDataService<Estate> _estateDataService;
        private readonly IDataService<Person> _personDataService;
        private readonly PersonManager _personManager;
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
        public EditEstateViewModel(IDataService<Person> personDataService, PersonManager personManager)
        {
            //_estateDataService = estateDataService;
            _personDataService = personDataService;
            _personManager = personManager;

            // Populate the list of countries and legal forms
            Countries = Enum.GetValues(typeof(Country)).Cast<Country>().ToList();
            LegalForms = Enum.GetValues(typeof(LegalFormType)).Cast<LegalFormType>().ToList();

        }

        private void LoadBuyersAndSellers()
        {
            // Retrieve all persons from PersonManager (Buyers and Sellers)
            var persons = _personManager.GetAll();  // Replacing the async call with in-memory access

            Buyers = persons.OfType<Buyer>().ToList();
            Sellers = persons.OfType<Seller>().ToList();

            // Link the selected buyer and seller to the estate
            if (SelectedEstate.LinkedBuyer != null)
                SelectedBuyer = Buyers.FirstOrDefault(b => b.ID == SelectedEstate.LinkedBuyer.ID);
            if (SelectedEstate.LinkedSeller != null)
                SelectedSeller = Sellers.FirstOrDefault(s => s.ID == SelectedEstate.LinkedSeller.ID);
        }

        // Method to initialize the estate with an existing estate
        public void InitializeEstate(Estate estate)
        {
            SelectedEstate = estate;
            LoadBuyersAndSellers();
        }
        [RelayCommand]
        private void Save(Window window)
        {
            SelectedEstate.LinkedBuyer = SelectedBuyer;
            SelectedEstate.LinkedSeller = SelectedSeller;
            MessageBox.Show($"Updated: {SelectedEstate}");
           // await _estateDataService.UpdateAsync(SelectedEstate);
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
