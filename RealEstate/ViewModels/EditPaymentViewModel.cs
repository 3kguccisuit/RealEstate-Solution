using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class EditPaymentViewModel : ObservableObject
    {
        //private readonly IEstateDataService _estateDataService;
        private readonly IDataService<Payment> _paymentDataService;

        [ObservableProperty]
        private Payment selected;
        public EditPaymentViewModel(IDataService<Payment> paymenmtDataService)
        {
            _paymentDataService = paymenmtDataService;

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

            var result = MessageBox.Show("Do you really want to cancel?", appName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                window.Close();
            }
        }

        // Create command to save (update) the payment
        [RelayCommand]
        private void Save(Window window)
        {
            MessageBox.Show($"Updated {Selected.Type} with the props: {Selected}");
            //await _paymentDataService.UpdateAsync(Selected);

            window.Close();
        }
    }
}
