using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Contracts.ViewModels;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Services;
using RealEstate.Windows;
using System.Collections.ObjectModel;
using System.Windows;


namespace RealEstate.ViewModels
{
    public partial class EstateViewModel : ObservableObject, INavigationAware
    {
        //private readonly IDataService<Estate> _estateDataService;
        private readonly IServiceProvider _serviceProvider;
        private readonly EstateManager _estateManager;

        private Estate _selectedEstate;
        public Estate SelectedEstate
        {
            get { return _selectedEstate; }
            set { SetProperty(ref _selectedEstate, value); }
        }


        // Observable collection of estates (bound to the ListView)
        public ObservableCollection<Estate> Estates { get; private set; } = new ObservableCollection<Estate>();

        // Constructor with the estate data service dependency injected
        public EstateViewModel(IServiceProvider serviceProvider, EstateManager estateManager)
        {
            //_estateDataService = estateDataService;
            _serviceProvider = serviceProvider;
            _estateManager = estateManager;
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

                _estateManager.Update(selected.ID, selected);
                RefreshEstatesAsync();
                SelectedEstate = Estates.FirstOrDefault(e => e.ID == selected.ID);

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
            window.ShowDialog();
            
            //force refresh
            RefreshEstatesAsync();
            if (viewModel.SelectedEstate.ID != "Cancel")
            {
                _estateManager.Add(viewModel.SelectedEstate.ID, viewModel.SelectedEstate);
                Estates.Add(viewModel.SelectedEstate);
                SelectedEstate = Estates.FirstOrDefault(e => e.ID == viewModel.SelectedEstate.ID);
                
            }
                
            else if (temp != null)
                SelectedEstate = Estates.FirstOrDefault(e => e.ID == temp.ID);
        }

        private void RefreshEstatesAsync()
        {
            Estates.Clear();

            // Retrieve the list of estates from the EstateManager instead of the data service
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
