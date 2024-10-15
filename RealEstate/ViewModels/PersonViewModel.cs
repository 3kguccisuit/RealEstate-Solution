using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Contracts.ViewModels;
using RealEstate.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using RealEstate.Models;
using RealEstateDLL.Managers;
using DTO.Models.BaseModels;
namespace RealEstate.ViewModels
{
    public partial class PersonViewModel : ObservableObject, INavigationAware
    {
        //private readonly IDataService<Person> _personDataService;
        private readonly IServiceProvider _serviceProvider;
        private readonly PersonManager _personManager;
        private AppState _appState;

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        // Observable collection of persons (bound to the ListView)
        public ObservableCollection<Person> Persons { get; private set; } = new ObservableCollection<Person>();

        // Constructor with the person data service dependency injected
        public PersonViewModel(IServiceProvider serviceProvider, PersonManager personManager, AppState appState)
        {
           // _personDataService = personDataService;
            _serviceProvider = serviceProvider;
            _personManager = personManager;
            _appState = appState;
        }


        [RelayCommand]
        private void EditPerson(Person selected)
        {
            if (selected != null)
            {
                // Create a deep clone of the selected person
                Person temporaryPerson = (Person)Activator.CreateInstance(selected.GetType(), selected);

                var viewModel = _serviceProvider.GetRequiredService<EditPersonViewModel>();
                viewModel.InitializePerson(temporaryPerson); // Pass the temporary person to the view model

                var editWindow = new EditPersonWindow(viewModel);
                var isOK = editWindow.ShowDialog();

                if (isOK == true)
                {
                    // Apply the changes to the original person if the user confirms
                    _personManager.Update(selected.ID, temporaryPerson);
                    RefreshPersonsAsync();
                    SelectedPerson = Persons.FirstOrDefault(p => p.ID == temporaryPerson.ID);
                    _appState.IsDirty = true;
                }
                // If the user cancels, the original person is left unchanged
            }
            else
            {
                MessageBox.Show("Please select a person to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        // Command to open the form for creating a new person
        [RelayCommand]
        private void OpenPersonForm(string selectedType)
        {
            var temp = SelectedPerson;

            // Resolve the ViewModel from the DI container
            var viewModel = _serviceProvider.GetRequiredService<CreatePersonViewModel>();

            // Initialize the ViewModel with the person type
            viewModel.Initialize(selectedType);

            // Open the CreatePersonWindow with the ViewModel
            var window = new CreatePersonWindow(viewModel);
            var isOK = window.ShowDialog();

            // Force refresh after creation
            //RefreshPersonsAsync();
            if (isOK == true)
            {
                _personManager.Add(viewModel.Selected.ID, viewModel.Selected);
                RefreshPersonsAsync();
                SelectedPerson = Persons.FirstOrDefault(p => p.ID == viewModel.Selected.ID);
                _appState.IsDirty = true;
            }   
            else
                SelectedPerson = Persons.FirstOrDefault(p => p.ID == temp.ID);
        }

        [RelayCommand]
        private void DeletePerson(Person selected)
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
                    _personManager.Remove(selected.ID);
                    // await _personDataService.RemoveAsync(selected.ID);
                    SelectedPerson = Persons.FirstOrDefault();
                    _appState.IsDirty = true;
                }
            }
            else
            {
                MessageBox.Show("Please select a person to delete.", "No Person Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void RefreshPersonsAsync()
        {
            Persons.Clear();

            // Retrieve the list of persons from the PersonManager
            var persons = _personManager.GetAll();

            foreach (var person in persons)
            {
                Persons.Add(person);
            }
        }

        // This method will be called when the view is navigated to
        public void OnNavigatedTo(object parameter)
        {
            RefreshPersonsAsync();
            SelectedPerson = Persons.FirstOrDefault();
        }

        // Empty method to handle navigation away from the view
        public void OnNavigatedFrom()
        {
        }


    }
}
