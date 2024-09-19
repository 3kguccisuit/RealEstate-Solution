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
            MessageBox.Show($"Wish to change:\n{selected}");
        }

        [RelayCommand]
        private void DeleteEstate(Estate selected) {
            MessageBox.Show($"Wish to delete:\n{selected.DisplayDetails()}");
        }

        [RelayCommand]
        private void NewApartment()
        {
            //ApartmentFormViewModel viewModel = new();
            var viewModel = _serviceProvider.GetRequiredService<ApartmentFormViewModel>();
            var dlg = new ApartmentFormWindow(viewModel);
            //ApartmentFormWindow dlg = new(viewModel);
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
