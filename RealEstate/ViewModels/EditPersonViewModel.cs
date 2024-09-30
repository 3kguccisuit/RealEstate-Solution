using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace RealEstate.ViewModels
{
    public partial class EditPersonViewModel : ObservableObject
    {
        private readonly IDataService<Person> _personDataService;

        [ObservableProperty]
        private Person selected;

        public EditPersonViewModel(IDataService<Person> personDataService)
        {
            _personDataService = personDataService;
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

            var result = MessageBox.Show("Do you really want to cancel?", appName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
              //  Selected.ID = "Cancel";
                window.Close();
            }
        }

        // Save command to save (update) the person
        [RelayCommand]
        private async Task Save(Window window)
        {
            MessageBox.Show($"Updated {Selected.GetType().Name} with the details: {Selected}");
            await _personDataService.UpdateAsync(Selected);

            window.Close();
        }
    }
}
