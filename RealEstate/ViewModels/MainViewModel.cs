using CommunityToolkit.Mvvm.ComponentModel;
using RealEstate.Contracts.ViewModels;
using RealEstate.Core.Models;
using System.ComponentModel;
using System.Net.NetworkInformation;

namespace RealEstate.ViewModels;

public partial class MainViewModel : ObservableObject, INotifyPropertyChanged, INavigationAware
{
    private AppState _appState;

    [ObservableProperty]
    public string fileName;
    public MainViewModel(AppState appState)
    {
        _appState = appState;
        fileName = appState.FileName;
    }

    public void OnNavigatedTo(object parameter)
    {
        FileName = _appState.FileName;
    }

    public void OnNavigatedFrom()
    {
        
    }
}
