using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models.ConcreteModels.Payments;
using RealEstate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
                Selected = new Bank(id,"name",123,"IBAN");
            }
            //else if (type == "PayPal")
            //{
            //    Selected = new PayPal(id,
            //        new Address("Street name", "Zip code", "City", Country.Sverige),
            //        new LegalForm(LegalFormType.Ownership),
            //        0, // Number of rooms
            //        0, // Number of floors
            //        false // HasGarage
            //    );
            //}
            //else if (type == "Vipps")
            //{
            //    Selected = new Vipps(id,
            //        new Address("Street name", "Zip code", "City", Country.Sverige),
            //        new LegalForm(LegalFormType.Ownership),
            //        0, // Number of rooms
            //        false // HasGarden
            //    );
            //}
            //else if (type == "WesternUnion")
            //{
            //    Selected = new WesternUnion(id,
            //        new Address("Street name", "Zip code", "City", Country.Sverige),
            //        new LegalForm(LegalFormType.Ownership),
            //        0, // Parking spaces
            //        0 // Number of beds
            //    );
            //}
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
    }
}
