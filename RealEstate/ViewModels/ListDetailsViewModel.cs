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
                MessageBox.Show($"Wish to change:\n{selected}");

                var viewModel = _serviceProvider.GetRequiredService<ApartmentFormViewModel>();

                var buldingType = selected.Type;
                switch (buldingType)
                {
                    case "Villa":
                        var dlg = new ApartmentFormWindow(viewModel);
                        dlg.ShowDialog();

                        break;
                    case "Apartment":
                        //var dlg = new ApartmentFormWindow(viewModel);
                        //dlg.ShowDialog();

                        break;
                    default:
                        MessageBox.Show($"Unknown buldingType: {buldingType}", "Error",MessageBoxButton.OK,MessageBoxImage.Error);
                        break;
                }

            }
            else
                MessageBox.Show("Please select an estate");

        }

        [RelayCommand]
        private void DeleteEstate(Estate selected) {
            if (selected != null)
            {
                _estateDataService.RemoveEstateAsync(selected.ID);
                MessageBox.Show($"Deleted estate:\n{selected.DisplayDetails()}");
            }
            else
                MessageBox.Show("Please select an estate");
        }

        [RelayCommand]
        private void NewApartment()
        {
            var viewModel = _serviceProvider.GetRequiredService<ApartmentFormViewModel>();
            var dlg = new ApartmentFormWindow(viewModel);
            dlg.ShowDialog();
        }

        [RelayCommand]
        private void NewVilla()
        {
            // Resolve VillaFormViewModel from the DI container
            var viewModel = _serviceProvider.GetRequiredService<VillaFormViewModel>();
            var dlg = new VillaFormWindow(viewModel);
            dlg.ShowDialog();
        }
        // This method will be called when the view is navigated to
        public async void OnNavigatedTo(object parameter)
        {
            Estates.Clear();

            // Fetch estate data from the data service
            var data = await _estateDataService.GetEstatesAsync();

            // Add each estate to the observable collection
            foreach (var estate in data)
            {
                Estates.Add(estate);
            }

            // Set the first estate as the selected estate by default
            SelectedEstate = Estates.FirstOrDefault();
        }

        // Empty method to handle navigation away from the view (optional)
        public void OnNavigatedFrom()
        {
        }
    }
}
