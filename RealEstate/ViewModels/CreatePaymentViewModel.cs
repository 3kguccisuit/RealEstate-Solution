using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DTO.Models.BaseModels;
using DTO.Models.ConcreteModels.Payments;
using System.ComponentModel;
using System.Windows;
using RealEstateDLL.Helpers;
using UtilitiesLib.Helpers;

namespace RealEstate.ViewModels
{
    public partial class CreatePaymentViewModel : ObservableObject
    {
        //private readonly IEstateDataService _estateDataService;
        //private readonly IDataService<Payment> _paymentDataService;
        public IRelayCommand<CancelEventArgs> OnWindowClosingCommand { get; }

        [ObservableProperty]
        private Payment selected;

        private bool _isCancelConfirmed = false;
        private bool _isSaved = false;
        public CreatePaymentViewModel()
        {
            // _paymentDataService = paymenmtDataService;
            OnWindowClosingCommand = new RelayCommand<CancelEventArgs>(OnWindowClosing);


        }

        // Initialize the estate based on the provided type
        public void Initialize(string type)
        {
            var id = IDGenerator.GetUniqueId();
            if (type == "Bank")
            {
                Selected = new Bank(id, "", 0, "");
            }
            else if (type == "PayPal")
            {
                Selected = new PayPal(id, "", 0, "");
            }
            else if (type == "Vipps")
            {
                Selected = new Vipps(id, "", 0, "");
            }
            else if (type == "WesternUnion")
            {
                Selected = new WesternUnion(id, "", 0, "");
            }
            else
            {
                MessageBox.Show("Invalid payment type.");
            }
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
                window.DialogResult=false;
                //window.Close(); // Close the window if the user confirms
            }
        }

        // Create command to save the estate
        [RelayCommand]
        private void Save(Window window)
        {
            // Use HasValidationError from UtilitiesLib to check for validation errors
            if (UIHelper.HasValidationError(window))
            {
                // If validation errors exist, show a message and stop the save
                MessageBox.Show("Please fix the input errors before saving.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // If no validation errors, proceed with the save
            _isSaved = true;
            window.DialogResult = true;
            window.Close();
        }


        // AutofillCommand to populate the TextBoxes with default values
        [RelayCommand]
        private void Autofill()
        {
            // MessageBox.Show($"Autofill");
            Selected = Selected.AutoFill();
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
