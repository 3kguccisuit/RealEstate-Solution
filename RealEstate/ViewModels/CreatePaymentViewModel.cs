using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels.Payments;
using RealEstate.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace RealEstate.ViewModels
{
    public partial class CreatePaymentViewModel : ObservableObject
    {
        //private readonly IEstateDataService _estateDataService;
        private readonly IDataService<Payment> _paymentDataService;

        [ObservableProperty]
        private Payment selected;
        public CreatePaymentViewModel(IDataService<Payment> paymenmtDataService)
        {
            _paymentDataService = paymenmtDataService;

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
                Selected.ID = "Cancel";
                window.Close();
            }
        }

        // Create command to save the estate
        [RelayCommand]
        private async Task Save(Window window)
        {

            MessageBox.Show($"Created {Selected.Type} with the props: {Selected}");
            await _paymentDataService.AddAsync(Selected);

            window.Close();
        }

        // AutofillCommand to populate the TextBoxes with default values
        [RelayCommand]
        private void Autofill()
        {
            // MessageBox.Show($"Autofill");
            Selected = Selected.AutoFill();
        }
    }
}
