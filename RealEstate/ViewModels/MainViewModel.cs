using CommunityToolkit.Mvvm.ComponentModel;
using RealEstate.Contracts.ViewModels;
using System.ComponentModel;
using RealEstateBLL.Models;
namespace RealEstate.ViewModels;

public partial class MainViewModel : ObservableObject, INotifyPropertyChanged, INavigationAware
{
    [ObservableProperty]
    private AppState _appState;

    public MainViewModel(AppState appState)
    {
        AppState = appState;
    }

    public void OnNavigatedTo(object parameter)
    {
    }

    public void OnNavigatedFrom()
    {
    }
}
