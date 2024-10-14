using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using RealEstate.Windows;
using RealEstateBLL.Enums;
using RealEstateBLL.Models.BaseModels;
using RealEstateBLL.Models.ConcreteModels.Persons;
using RealEstateDLL.Managers;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace RealEstate.ViewModels
{
    public partial class EditEstateViewModel : ObservableObject
    {
        //private readonly IDataService<Estate> _estateDataService;
        //private readonly IDataService<Person> _personDataService;
        private readonly EstateManager _estateManager;
        private readonly PersonManager _personManager;
        private readonly PaymentManager _paymentManager;
        private readonly IServiceProvider _serviceProvider;
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

        public List<Buyer> Buyers { get; private set; } = new List<Buyer>();
        public List<Seller> Sellers { get; private set; } = new List<Seller>();
        public List<Payment> Payments { get; private set; } = new List<Payment>();
        public List<Country> Countries { get; }
        public List<LegalFormType> LegalForms { get; }

        private bool _isCancelConfirmed = false;
        private bool _isSaved = false;

        // Constructor for dependency injection and initializing data
        public EditEstateViewModel(EstateManager EstateManager, PersonManager personManager, PaymentManager paymentManager, IServiceProvider serviceProvider)
        {
            //_estateDataService = estateDataService;
            //_personDataService = personDataService;
            _estateManager = EstateManager;
            _personManager = personManager;
            _paymentManager = paymentManager;
            _serviceProvider = serviceProvider;
            OnWindowClosingCommand = new RelayCommand<CancelEventArgs>(OnWindowClosing);

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

        private void LoadPayments()
        {
            // Retrieve all payments from PaymentManager
            Payments = _paymentManager.GetAll();

            // Link the selected payment to the estate
            if (SelectedEstate.LinkedPayment != null)
                SelectedPayment = Payments.FirstOrDefault(p => p.ID == SelectedEstate.LinkedPayment.ID);
        }

        // Method to initialize the estate with an existing estate
        public void InitializeEstate(Estate estate)
        {
            SelectedEstate = estate;
            LoadBuyersAndSellers();
            LoadPayments();
        }
        [RelayCommand]
        private void Save(Window window)
        {
            SelectedEstate.LinkedBuyer = SelectedBuyer;
            SelectedEstate.LinkedSeller = SelectedSeller;
            SelectedEstate.LinkedPayment = SelectedPayment;
            _isSaved = true;
            window.DialogResult = true;
         //   MessageBox.Show($"Updated: {SelectedEstate}");
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
                _isCancelConfirmed = true;
                window.DialogResult = false;
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

        [RelayCommand]
        private void CreateNewPayment(string selectedType)
        {
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

                // Optionally, clear the selected payment type to mimic dropdown behavior
                SelectedPaymentType = null;
            }
        }

        public void OnWindowClosing(CancelEventArgs e)
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (_isSaved)
            {
                window.DialogResult = true;
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
