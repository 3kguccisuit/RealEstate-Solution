using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstate.Contracts.ViewModels;
using RealEstate.Contracts.Services;
using RealEstate.Models;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using System.Windows;
using RealEstate.Windows;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Core.Enums;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models;

namespace RealEstate.ViewModels
{
    public partial class ListDetailsViewModel : ObservableObject, INavigationAware
    {
        private readonly IEstateDataService _estateDataService;
        private readonly IServiceProvider _serviceProvider;
        private Estate _selectedEstate;
        public Estate SelectedEstate
        {
            get { return _selectedEstate; }
            set { SetProperty(ref _selectedEstate, value); }
        }


        // Observable collection of estates (bound to the ListView)
        public ObservableCollection<Estate> Estates { get; private set; } = new ObservableCollection<Estate>();

        // Constructor with the estate data service dependency injected
        public ListDetailsViewModel(IEstateDataService estateDataService, IServiceProvider serviceProvider)
        {
            _estateDataService = estateDataService;
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        private void EditEstate(Estate selected)
        {
            if (selected != null)
            {
                var viewModel = _serviceProvider.GetRequiredService<EditEstateViewModel>();
                viewModel.InitializeEstate(selected); // Pass the selected estate to the view model

                var editWindow = new EditEstateWindow(viewModel);
                editWindow.ShowDialog();

            }
            else
                MessageBox.Show("Please select an estate");

        }


        [RelayCommand]
        private void DeleteEstate(Estate selected)
        {
            if (selected != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the selected estate?\n\n{selected.DisplayDetails()}",
                                             "Confirm Deletion",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    //force update
                    Estates.Remove(selected);
                    //remove from json
                    _estateDataService.RemoveEstateAsync(selected.ID);

                }
            }
            else
            {
                // Show a message if no estate is selected
                MessageBox.Show("Please select an estate", "No Estate Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        [RelayCommand]
        private void OpenEstateForm(string estateType)
        {
            // Resolve the ViewModel from the DI container
            var viewModel = _serviceProvider.GetRequiredService<CreateEstateViewModel>();

            // Initialize the ViewModel with the estate
            viewModel.InitializeEstate(estateType);

            // Open the CreateEstateWindow with the ViewModel
            var window = new CreateEstateWindow(viewModel);
            window.ShowDialog();
        }


        // This method will be called when the view is navigated to
        public async void OnNavigatedTo(object parameter)
        {
            Estates.Clear();

            var data = await _estateDataService.GetEstatesAsync();

            foreach (var estate in data)
            {
                Estates.Add(estate);
            }

            SelectedEstate = Estates.FirstOrDefault();
        }

        // Empty method to handle navigation away from the view (optional)
        public void OnNavigatedFrom()
        {
        }
    }
}
