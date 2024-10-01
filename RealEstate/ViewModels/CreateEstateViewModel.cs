using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models.ConcreteModels.Persons;
using RealEstate.Helpers;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class CreateEstateViewModel : ObservableObject
    {
        //private readonly IEstateDataService _estateDataService;
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

        public CreateEstateViewModel(IDataService<Estate> estateDataService, IDataService<Person> personDataService)
        {
            _estateDataService = estateDataService;
            _personDataService = personDataService;

            // Populate the list of countries and legal forms
            Countries = Enum.GetValues(typeof(Country)).Cast<Country>().ToList();
            LegalForms = Enum.GetValues(typeof(LegalFormType)).Cast<LegalFormType>().ToList();
        }

        // Initialize the estate based on the provided type
        public async Task InitializeEstate(string type)
        {
            var id = IDGenerator.GetUniqueId();
            if (type == "Apartment")
            {
                SelectedEstate = new Apartment(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Number of rooms
                    0  // Floor level
                );
            }
            else if (type == "Villa")
            {
                SelectedEstate = new Villa(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Number of rooms
                    0, // Number of floors
                    false // HasGarage
                );
            }
            else if (type == "Townhouse")
            {
                SelectedEstate = new Townhouse(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Number of rooms
                    false // HasGarden
                );
            }
            else if (type == "Hospital")
            {
                SelectedEstate = new Hospital(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of beds
                );
            }
            else if (type == "School")
            {
                SelectedEstate = new School(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of classrooms
                );
            }
            else if (type == "University")
            {
                SelectedEstate = new University(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of programs
                );
            }
            else if (type == "Hotel")
            {
                SelectedEstate = new Hotel(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    false // hasSpa
                );
            }
            else if (type == "Shop")
            {
                SelectedEstate = new Shop(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    false // HasOnlineStore
                );
            }
            else if (type == "Warehouse")
            {
                SelectedEstate = new Warehouse(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    0 // LoadingDocks
                );
            }
            else if (type == "Factory")
            {
                SelectedEstate = new Factory(id,
                    new Address("Street name", "Zip code", "City", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    false // hasWarehouse
                );
            }
            else
            {
                MessageBox.Show("Invalid estate type.");
            }
            await LoadBuyersAndSellers();
        }

        private async Task LoadBuyersAndSellers()
        {
            var persons = await _personDataService.GetAsync();

            Buyers = persons.OfType<Buyer>().ToList();
            Sellers = persons.OfType<Seller>().ToList();


            SelectedBuyer = Buyers.FirstOrDefault();
            SelectedSeller = Sellers.FirstOrDefault();
        }

        // Cancel command
        [RelayCommand]
        private void Cancel(Window window)
        {
            var app = (App)Application.Current;
            var appName = app.AppName;

            var result = MessageBox.Show("Do you really want to cancel?", appName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SelectedEstate.ID = "Cancel";
                window.Close();
            }
        }

        // Create command to save the estate
        [RelayCommand]
        private async Task Save(Window window)
        {
            SelectedEstate.LinkedBuyer = SelectedBuyer;
            SelectedEstate.LinkedSeller = SelectedSeller;
            MessageBox.Show($"Created {SelectedEstate.Type} with the props: {SelectedEstate}");
            await _estateDataService.AddAsync(SelectedEstate);

            window.Close();
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
