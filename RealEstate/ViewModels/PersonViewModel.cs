using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RealEstate.Contracts.ViewModels;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using RealEstate.Windows;
using System.Windows;

namespace RealEstate.ViewModels
{
    public partial class PersonViewModel : ObservableObject, INavigationAware
    {
        private readonly IDataService<Person> _personDataService;
        private readonly IServiceProvider _serviceProvider;

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        // Observable collection of persons (bound to the ListView)
        public ObservableCollection<Person> Persons { get; private set; } = new ObservableCollection<Person>();

        // Constructor with the person data service dependency injected
        public PersonViewModel(IDataService<Person> personDataService, IServiceProvider serviceProvider)
        {
            _personDataService = personDataService;
            _serviceProvider = serviceProvider;
        }


        [RelayCommand]
        private async Task EditPerson(Person selected)
        {
            if (selected != null)
            {
                var viewModel = _serviceProvider.GetRequiredService<EditPersonViewModel>();
                viewModel.InitializePerson(selected); // Pass the selected person to the view model

                var editWindow = new EditPersonWindow(viewModel);
                editWindow.ShowDialog();
                await RefreshPersonsAsync();
                SelectedPerson = Persons.FirstOrDefault(p => p.ID == selected.ID);
            }
            else
            {
                MessageBox.Show("Please select a person to edit.");
            }
        }


        // Command to open the form for creating a new person
        [RelayCommand]
        private async Task OpenPersonForm(string selectedType)
        {
            var temp = SelectedPerson;

            // Resolve the ViewModel from the DI container
            var viewModel = _serviceProvider.GetRequiredService<CreatePersonViewModel>();

            // Initialize the ViewModel with the person type
             viewModel.Initialize(selectedType);

            // Open the CreatePersonWindow with the ViewModel
            var window = new CreatePersonWindow(viewModel);
            window.ShowDialog();

            // Force refresh after creation
            await RefreshPersonsAsync();
            if (viewModel.Selected.ID != "Cancel")
                SelectedPerson = Persons.FirstOrDefault(p => p.ID == viewModel.Selected.ID);
            else if (temp != null)
                SelectedPerson = Persons.FirstOrDefault(p => p.ID == temp.ID);
        }

        [RelayCommand]
        private async Task DeletePerson(Person selected)
        {
            if (selected != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the selected person?\n\n{selected.ToString()}",
                                             "Confirm Deletion",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Persons.Remove(selected);
                    await _personDataService.RemoveAsync(selected.ID);
                    SelectedPerson = Persons.FirstOrDefault();
                }
            }
            else
            {
                MessageBox.Show("Please select a person to delete.", "No Person Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task RefreshPersonsAsync()
        {
            Persons.Clear();

            var data = await _personDataService.GetAsync();

            foreach (var person in data)
            {
                Persons.Add(person);
            }

        }

        // This method will be called when the view is navigated to
        public async void OnNavigatedTo(object parameter)
        {
            Persons.Clear();

            var data = await _personDataService.GetAsync();

            foreach (var person in data)
            {
                Persons.Add(person);
            }

            SelectedPerson = Persons.FirstOrDefault();
        }

        // Empty method to handle navigation away from the view
        public void OnNavigatedFrom()
        {
        }


    }
}
