using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RealEstate.Contracts.ViewModels;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using Microsoft.Extensions.DependencyInjection;

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

        // Method to refresh the list of persons
        private async Task RefreshPersonsAsync()
        {
            Persons.Clear();

            var data = await _personDataService.GetAsync();

            foreach (var person in data)
            {
                Persons.Add(person);
            }

            SelectedPerson = Persons.FirstOrDefault();
        }
    }
}
