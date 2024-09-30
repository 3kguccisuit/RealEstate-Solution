using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models.ConcreteModels.Payments;
using RealEstate.Core.Services;
using RealEstate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Selected.ID = "Cancel";
                window.Close();
            }
        }

        // Create command to save (update) the payment
        [RelayCommand]
        private async Task Save(Window window)
        {
            MessageBox.Show($"Updated {Selected.Type} with the props: {Selected}");
            await _paymentDataService.UpdateAsync(Selected);

            window.Close();
        }
    }
}
