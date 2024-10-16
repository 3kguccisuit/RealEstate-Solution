﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DTO.Models.BaseModels;
using System.ComponentModel;
using System.Windows;
using UtilitiesLib.Helpers;

namespace RealEstate.ViewModels
{
    public partial class EditPersonViewModel : ObservableObject
    {
        //private readonly IDataService<Person> _personDataService;
        public IRelayCommand<CancelEventArgs> OnWindowClosingCommand { get; }
        [ObservableProperty]
        private Person selected;

        private bool _isCancelConfirmed = false;
        private bool _isSaved = false;
        public EditPersonViewModel()
        {
            //_personDataService = personDataService;
            OnWindowClosingCommand = new RelayCommand<CancelEventArgs>(OnWindowClosing);
        }

        // Initialize the person based on the provided person (either Seller or Buyer)
        public void InitializePerson(Person person)
        {
            Selected = person;
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

        // Save command to save (update) the person
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
