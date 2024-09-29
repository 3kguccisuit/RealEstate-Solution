using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using RealEstate.Contracts.ViewModels;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;

namespace RealEstate.ViewModels;

public class BankViewModel : ObservableObject, INavigationAware
{
    private readonly IDataService<Payment> _paymentDataService;
    private readonly IServiceProvider _serviceProvider;
    private Payment _selected;


    public Payment Selected
    {
        get { return _selected; }
        set { SetProperty(ref _selected, value); }
    }

    public ObservableCollection<Payment> Payments { get; private set; } = new ObservableCollection<Payment>();

    public BankViewModel(IDataService<Payment> paymentDataService, IServiceProvider serviceProvider)
    {
        _paymentDataService = paymentDataService;
        _serviceProvider = serviceProvider;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Payments.Clear();

        var data = await _paymentDataService.GetAsync();

        foreach (var item in data)
        {
            Payments.Add(item);
        }

        Selected = Payments.FirstOrDefault();
    }

    public void OnNavigatedFrom()
    {
    }
}
