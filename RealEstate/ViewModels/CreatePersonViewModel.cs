using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels.Persons;
using RealEstate.Helpers;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class CreatePersonViewModel : ObservableObject
    {
        private readonly IDataService<Person> _personDataService;

        [ObservableProperty]
        private Person selected;

        public CreatePersonViewModel(IDataService<Person> personDataService)
        {
            _personDataService = personDataService;
        }

        // Initialize the person based on the provided type
        public void Initialize(string type)
        {
            var id = IDGenerator.GetUniqueId();
            if (type == "Seller")
            {
                Selected = new Seller(id, "", new Address("", "", "", Country.Sverige), 0);
            }
            else if (type == "Buyer")
            {
                Selected = new Buyer(id, "", new Address("", "", "", Country.Sverige), 0, false);
            }
            else
            {
                MessageBox.Show("Invalid person type.");
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

        // Save command to save the person
        [RelayCommand]
        private async Task Save(Window window)
        {
            MessageBox.Show($"Created {Selected.GetType().Name} with the details: {Selected}");
            await _personDataService.AddAsync(Selected);

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
