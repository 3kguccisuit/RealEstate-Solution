﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DTO.Enums;
using DTO.Models;
using DTO.Models.BaseModels;
using DTO.Models.ConcreteModels.Persons;
using System.ComponentModel;
using System.Windows;
using RealEstateDLL.Helpers;
using UtilitiesLib.Helpers;

namespace RealEstate.ViewModels
{
    public partial class CreatePersonViewModel : ObservableObject
    {
       // private readonly IDataService<Person> _personDataService;
        public IRelayCommand<CancelEventArgs> OnWindowClosingCommand { get; }
        private bool _isCancelConfirmed = false;
        private bool _isSaved = false;
        [ObservableProperty]
        private Person selected;

        public CreatePersonViewModel()
        {
           // _personDataService = personDataService;
            OnWindowClosingCommand = new RelayCommand<CancelEventArgs>(OnWindowClosing);
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
                _isCancelConfirmed = true;
                window.DialogResult = false;
                //Selected.ID = "Cancel";
                window.Close(); // Close the window if the user confirms
            }
        }

        // Save command to save the person
        [RelayCommand]
        private void Save(Window window)
        {
            if (UIHelper.HasValidationError(window))
            {
                // If validation errors exist, show a message and stop the save
                MessageBox.Show("Please fix the input errors before saving.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
                //e.Cancel = false;
                window.DialogResult= true;
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
