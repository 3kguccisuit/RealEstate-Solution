using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Contracts.ViewModels;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Windows;
using System.Collections.ObjectModel;
using System.Windows;

namespace RealEstate.ViewModels;

public partial class PaymentViewModel : ObservableObject, INavigationAware
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

    public PaymentViewModel(IDataService<Payment> paymentDataService, IServiceProvider serviceProvider)
    {
        _paymentDataService = paymentDataService;
        _serviceProvider = serviceProvider;
    }

    [RelayCommand]
    private async Task EditPayment(Payment selected)
    {
        MessageBox.Show($"EditPayment {selected}", "EditPayment", MessageBoxButton.OK, MessageBoxImage.Information);
        if (selected != null)
        {
            var viewModel = _serviceProvider.GetRequiredService<EditPaymentViewModel>();
            viewModel.Initialize(selected); // Pass the selected payment to the view model

            var editWindow = new EditPaymentWindow(viewModel);
            editWindow.ShowDialog();
            await RefreshEstatesAsync();
            Selected = Payments.FirstOrDefault(e => e.ID == selected.ID);

        }
        else
            MessageBox.Show("Please select a payment");
    }

    [RelayCommand]
    private async Task DeletePayment(Payment selected)
    {
        if (selected != null)
        {
            var result = MessageBox.Show($"Are you sure you want to delete the selected payment?\n\n{selected.ToString()}",
                                         "Confirm Deletion",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                //force update
                Payments.Remove(selected);
                //remove from json
                await _paymentDataService.RemoveAsync(selected.ID);
                Selected = Payments.FirstOrDefault();
            }
        }
        else
        {
            MessageBox.Show("Please select an payment", "No Payment Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }


    [RelayCommand]
    private async Task OpenPaymentForm(string selectedType)
    {
        //MessageBox.Show($"OpenPaymentForm {selectedType}", "OpenPaymentForm", MessageBoxButton.OK, MessageBoxImage.Information);

        var temp = Selected;
        // Resolve the ViewModel from the DI container
        var viewModel = _serviceProvider.GetRequiredService<CreatePaymentViewModel>();

        // Initialize the ViewModel with the estate
        viewModel.Initialize(selectedType);

        // Open the CreateEstateWindow with the ViewModel
        var window = new CreatePaymentWindow(viewModel);
        window.ShowDialog();

        //force refresh
        await RefreshEstatesAsync();
        if (viewModel.Selected.ID != "Cancel")
            Selected = Payments.FirstOrDefault(e => e.ID == viewModel.Selected.ID);
        else if (temp != null)
            Selected = Payments.FirstOrDefault(e => e.ID == temp.ID);
    }

    private async Task RefreshEstatesAsync()
    {
        Payments.Clear();

        var data = await _paymentDataService.GetAsync();

        foreach (var estate in data)
        {
            Payments.Add(estate);
        }
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
