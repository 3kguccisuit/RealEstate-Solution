using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class CreateEstateViewModel : ObservableObject
    {
        private readonly IEstateDataService _estateDataService;

        [ObservableProperty]
        private Estate selectedEstate;

        public List<Country> Countries { get; }
        public List<LegalFormType> LegalForms { get; }

        public CreateEstateViewModel(IEstateDataService estateDataService)
        {
            _estateDataService = estateDataService;

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
            else
            {
                MessageBox.Show("Invalid estate type.");
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
                window.Close();
            }
        }

        // Create command to save the estate
        [RelayCommand]
        private async Task Save(Window window)
        {

            MessageBox.Show($"Created {SelectedEstate.Type} with the props: {SelectedEstate}");
            await _estateDataService.AddEstateAsync(SelectedEstate);

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
