using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Contracts.ViewModels;
using RealEstate.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using RealEstateBLL.Models;
using RealEstateDLL.Managers;
using DTO.Models.BaseModels;
using DTO.Enums;

namespace RealEstate.ViewModels
{
    public partial class EstateViewModel : ObservableObject, INavigationAware
    {
        //private readonly IDataService<Estate> _estateDataService;
        private readonly IServiceProvider _serviceProvider;
        private readonly EstateManager _estateManager;
        private AppState _appState;
        private Estate _selectedEstate;
        public Estate SelectedEstate
        {
            get { return _selectedEstate; }
            set { SetProperty(ref _selectedEstate, value); }
        }


        // Observable collection of estates (bound to the ListView)
        public ObservableCollection<Estate> Estates { get; private set; } = new ObservableCollection<Estate>();

        [ObservableProperty]
        private string searchOption;

        // Constructor with the estate data service dependency injected
        public EstateViewModel(IServiceProvider serviceProvider, EstateManager estateManager, AppState appState )
        {
            //_estateDataService = estateDataService;
            _serviceProvider = serviceProvider;
            _estateManager = estateManager;
            SearchOption = "City";
            _appState = appState;
        }

        [RelayCommand]
        private void EditEstate(Estate selected)
        {
            if (selected != null)
            {
                Estate temporaryEstate = selected.Clone();
                var viewModel = _serviceProvider.GetRequiredService<EditEstateViewModel>();
                //viewModel.InitializeEstate(selected); // Pass the selected estate to the view model
                viewModel.InitializeEstate(temporaryEstate);

                var editWindow = new EditEstateWindow(viewModel);
                var isOK = editWindow.ShowDialog();
                if (isOK == true) {
                    _estateManager.Update(selected.ID, temporaryEstate);
                    RefreshEstatesAsync();
                    SelectedEstate = Estates.FirstOrDefault(e => e.ID == selected.ID);
                    _appState.IsDirty = true;
                }
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
                    _estateManager.Remove(selected.ID);
                    //force update
                    Estates.Remove(selected);
                    SelectedEstate = Estates.FirstOrDefault();
                    _appState.IsDirty = true;
                }
            }
            else
            {
                MessageBox.Show("Please select an estate", "No Estate Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        [RelayCommand]
        private void AddEstateForm(string selectedType) // addEstate
        {
            var temp = SelectedEstate;
            // Resolve the ViewModel from the DI container
            var viewModel = _serviceProvider.GetRequiredService<CreateEstateViewModel>();

            // Initialize the ViewModel with the estate
            viewModel.InitializeEstate(selectedType);

            // Open the CreateEstateWindow with the ViewModel
            var window = new CreateEstateWindow(viewModel);
            var isOK =  window.ShowDialog();
            
            //force refresh
            //RefreshEstatesAsync();
            if (isOK == true)
            {
                _estateManager.Add(viewModel.SelectedEstate.ID, viewModel.SelectedEstate);
                Estates.Add(viewModel.SelectedEstate);
                SelectedEstate = Estates.FirstOrDefault(e => e.ID == viewModel.SelectedEstate.ID);
                _appState.IsDirty = true;
            }
            else
                SelectedEstate = Estates.FirstOrDefault(e => e.ID == temp.ID);  
        }

        [RelayCommand]
        private void Search(string searchType)
        {

            IEnumerable<Estate> estatesRes =null;
            if (string.IsNullOrEmpty(searchType))
            {
                MessageBox.Show("Please enter text in the Search Box", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(SearchOption))
            {
                MessageBox.Show("Please select a type from the combo box", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SearchOption == "City")
            {
                estatesRes = _estateManager.GetEstatesByCity(searchType);
            }
            if (SearchOption == "Country")
            {
                if (Enum.TryParse<Country>(searchType, true, out var country))
                {
                    estatesRes = _estateManager.GetEstatesByCountry(country);
                }
            }

            if (estatesRes.Any())
                UpdateSearchResult(estatesRes);
        }

        private void UpdateSearchResult(IEnumerable<Estate> estates)
        {
            Estates.Clear();
            foreach (var estate in estates)
            {
                Estates.Add(estate);
            }
            if (!Estates.Any())
            {
                MessageBox.Show($"No estates found, please be precise with query", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        partial void OnSearchOptionChanged(string value)
        {
            SearchOption = value;
        }

        private void RefreshEstatesAsync()
        {
            Estates.Clear();

            // Retrieve the list of estates from the EstateManager
            var estates = _estateManager.GetAll();

            foreach (var estate in estates)
            {
                Estates.Add(estate);
            }
        }

        public void OnNavigatedTo(object parameter)
        {
            RefreshEstatesAsync();
            SelectedEstate = Estates.FirstOrDefault();
        }

        // Empty method to handle navigation away from the view
        public void OnNavigatedFrom()
        {
        }
    }
}
