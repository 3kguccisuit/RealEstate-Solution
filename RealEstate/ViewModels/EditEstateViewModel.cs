using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class EditEstateViewModel : ObservableObject
    {
        //private readonly IEstateDataService _estateDataService;
        private readonly IDataService<Estate> _estateDataService;

        [ObservableProperty]
        private Estate selectedEstate;

        public List<Country> Countries { get; }
        public List<LegalFormType> LegalForms { get; }

        // Constructor for dependency injection and initializing data
        public EditEstateViewModel(IDataService<Estate> estateDataService)
        {
            _estateDataService = estateDataService;

            // Populate the list of countries and legal forms
            Countries = Enum.GetValues(typeof(Country)).Cast<Country>().ToList();
            LegalForms = Enum.GetValues(typeof(LegalFormType)).Cast<LegalFormType>().ToList();
        }

        // Method to initialize the estate with an existing estate
        public void InitializeEstate(Estate estate)
        {
            SelectedEstate = estate;
        }
        [RelayCommand]
        private async Task Save(Window window)
        {
            MessageBox.Show($"Updated: {SelectedEstate}");
            await _estateDataService.UpdateAsync( SelectedEstate );
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
                window.Close();
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
    }
}
