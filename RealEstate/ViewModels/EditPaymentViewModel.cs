using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using System.ComponentModel;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class EditPaymentViewModel : ObservableObject
    {
        //private readonly IEstateDataService _estateDataService;
        //private readonly IDataService<Payment> _paymentDataService;
        public IRelayCommand<CancelEventArgs> OnWindowClosingCommand { get; }
        [ObservableProperty]
        private Payment selected;

        private bool _isCancelConfirmed = false;
        private bool _isSaved = false;
        public EditPaymentViewModel()
        {
            //_paymentDataService = paymenmtDataService;
            OnWindowClosingCommand = new RelayCommand<CancelEventArgs>(OnWindowClosing);

        }

        // Initialize the estate based on the provided type
        public void Initialize(Payment payment)
        {
            Selected = payment;

        }

        // Cancel command
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

        // Create command to save (update) the payment
        [RelayCommand]
        private void Save(Window window)
        {
            _isSaved = true;
            window.DialogResult = true;
            //await _paymentDataService.UpdateAsync(Selected);
          //  MessageBox.Show($"Updated {Selected.Type} with the props: {Selected}");
            window.Close();
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
