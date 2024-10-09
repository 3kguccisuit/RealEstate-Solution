﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models.ConcreteModels.Persons;
using RealEstate.Core.Services;
using RealEstate.Helpers;
using RealEstate.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace RealEstate.ViewModels
{
    public partial class CreateEstateViewModel : ObservableObject
    {
        //private readonly IEstateDataService _estateDataService;
        //private readonly IDataService<Estate> _estateDataService;
        //private readonly IDataService<Person> _personDataService;
        private readonly IServiceProvider _serviceProvider;
        private readonly PersonManager _personManager;
        private readonly PaymentManager _paymentManager;
        public IRelayCommand<CancelEventArgs> OnWindowClosingCommand { get; }

        [ObservableProperty]
        private Estate selectedEstate;
        [ObservableProperty]
        private Buyer selectedBuyer;
        [ObservableProperty]
        private Seller selectedSeller;
        [ObservableProperty]
        private Payment selectedPayment;
        [ObservableProperty]
        private string selectedPaymentType;

        private bool _isCancelConfirmed = false;
        private bool _isSaved = false;

        public List<Buyer> Buyers { get; private set; } = new List<Buyer>();
        public List<Seller> Sellers { get; private set; } = new List<Seller>();
        public List<Payment> Payments { get; private set; } = new List<Payment>();
        public List<Country> Countries { get; }
        public List<LegalFormType> LegalForms { get; }

        public CreateEstateViewModel(PersonManager personManager, PaymentManager paymentManager, IServiceProvider serviceProvider)
        {
            //_estateDataService = estateDataService;
            //_personDataService = personDataService;
            _personManager = personManager;
            _paymentManager = paymentManager;
            _serviceProvider = serviceProvider;

            OnWindowClosingCommand = new RelayCommand<CancelEventArgs>(OnWindowClosing);

            // Populate the list of countries and legal forms
            Countries = Enum.GetValues(typeof(Country)).Cast<Country>().ToList();
            LegalForms = Enum.GetValues(typeof(LegalFormType)).Cast<LegalFormType>().ToList();

        }

        // Initialize the estate based on the provided type
        public void InitializeEstate(string type)
        {
            var id = IDGenerator.GetUniqueId();
            if (type == "Apartment")
            {
                SelectedEstate = new Apartment(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0, // Number of rooms
                    0  // Floor level
                );
            }
            else if (type == "Villa")
            {
                SelectedEstate = new Villa(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Number of rooms
                    0, // Number of floors
                    false // HasGarage
                );
            }
            else if (type == "Townhouse")
            {
                SelectedEstate = new Townhouse(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Tenement),
                    0, // Number of rooms
                    false // HasGarden
                );
            }
            else if (type == "Hospital")
            {
                SelectedEstate = new Hospital(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of beds
                );
            }
            else if (type == "School")
            {
                SelectedEstate = new School(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of classrooms
                );
            }
            else if (type == "University")
            {
                SelectedEstate = new University(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of programs
                );
            }
            else if (type == "Hotel")
            {
                SelectedEstate = new Hotel(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    false // hasSpa
                );
            }
            else if (type == "Shop")
            {
                SelectedEstate = new Shop(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    false // HasOnlineStore
                );
            }
            else if (type == "Warehouse")
            {
                SelectedEstate = new Warehouse(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    0 // LoadingDocks
                );
            }
            else if (type == "Factory")
            {
                SelectedEstate = new Factory(id,
                    new Address("", "", "", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    false // hasWarehouse
                );
            }
            else
            {
                MessageBox.Show("Invalid estate type.");
            }
            LoadBuyersAndSellers();
            LoadPayments();
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

        private void LoadPayments()
        {
            // Retrieve all payments from PaymentManager
            Payments = _paymentManager.GetAll();

            // Link the selected payment to the estate
            if (SelectedEstate.LinkedPayment != null)
                SelectedPayment = Payments.FirstOrDefault(p => p.ID == SelectedEstate.LinkedPayment.ID);
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
                _isCancelConfirmed = true;
                window.DialogResult = false;
                SelectedEstate.ID = "Cancel";
                window.Close(); // Close the window if the user confirms
            }
        }

        // Create command to save the estate
        [RelayCommand]
        private void Save(Window window)
        {
            SelectedEstate.LinkedBuyer = SelectedBuyer;
            SelectedEstate.LinkedSeller = SelectedSeller;
            SelectedEstate.LinkedPayment = SelectedPayment;
            _isSaved = true;
            window.DialogResult = true;
            //MessageBox.Show($"Created {SelectedEstate.Type} with the props: {SelectedEstate}");
           // await _estateDataService.AddAsync(SelectedEstate);

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
        [RelayCommand]
        private void CreateNewPayment(string selectedType) {
            // Resolve the ViewModel from the DI container
            var viewModel = _serviceProvider.GetRequiredService<CreatePaymentViewModel>();

            // Initialize the ViewModel with the choosen estate type
            viewModel.Initialize(selectedType);

            // Open the CreateEstateWindow with the ViewModel
            var window = new CreatePaymentWindow(viewModel);
           var isOK = window.ShowDialog();

            if (isOK == true)
            {
                Payments.Add(viewModel.Selected);
                _paymentManager.Add(viewModel.Selected.ID, viewModel.Selected);
                SelectedPayment = viewModel.Selected;
            }
        }

        partial void OnSelectedPaymentTypeChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                // Trigger the CreateNewPaymentCommand with the selected type
                CreateNewPaymentCommand.Execute(value);

                SelectedPaymentType = null;
            }
        }

        // AutofillCommand to populate the TextBoxes with default values
        [RelayCommand]
        private void Autofill()
        {
            // MessageBox.Show($"Autofill");
            SelectedEstate = SelectedEstate.AutoFill();
        }

        public void OnWindowClosing(CancelEventArgs e)
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (_isSaved)
            {
                window.DialogResult = true;
                //e.Cancel = false;
            }
            // Get the current window
            else if (!_isCancelConfirmed)
            {
                var result = MessageBox.Show("Do you really want to cancel?", "Cancel Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;  // Prevent the window from closing
                }
                else
                {

                    window.DialogResult = false;
                }
            }
        }

    }
}
