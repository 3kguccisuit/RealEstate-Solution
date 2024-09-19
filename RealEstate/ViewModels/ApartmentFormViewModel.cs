using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class ApartmentFormViewModel : ObservableObject
    {
        private readonly IEstateDataService _estateDataService;
        [ObservableProperty]
        private Apartment apartment;  

        public List<Country> Countries { get; }
        public List<LegalFormType> LegalForms { get; }  

        public ApartmentFormViewModel(IEstateDataService estateDataService)
        {
            _estateDataService = estateDataService;
            int id = IDGenerator.GetNextID();
            // Initialize Apartment object with default values
            Apartment = new Apartment(id,
                new Address("Street name", "Zip code", "City", Country.Sverige),
                                      new LegalForm(LegalFormType.Ownership),
                                      0,
                                      0);

            // Populate the list of countries and legal forms
            Countries = Enum.GetValues(typeof(Country)).Cast<Country>().ToList();
            LegalForms = Enum.GetValues(typeof(LegalFormType)).Cast<LegalFormType>().ToList();
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            var app = (App)Application.Current;
            var appName = app.AppName;

            var ret = MessageBox.Show("Do you really want to cancel?", appName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ret == MessageBoxResult.Yes)
                window.Close();
        }

        [RelayCommand]
        private async Task Create(Window window)
        {
            var app = (App)Application.Current;
            var appName = app.AppName;
            await _estateDataService.AddEstateAsync(Apartment);
            // Perform validation or save the Apartment instance here
            MessageBox.Show($"Apartment created: {Apartment}", appName, MessageBoxButton.OK);

            window.Close();
        }
    }
}
