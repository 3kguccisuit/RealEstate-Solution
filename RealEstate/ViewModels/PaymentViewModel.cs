using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Contracts.ViewModels;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using RealEstate.Core.Models;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Services;
using RealEstate.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using RealEstate.Models;
namespace RealEstate.ViewModels;

public partial class PaymentViewModel : ObservableObject, INavigationAware
{
    //private readonly IDataService<Payment> _paymentDataService;
    private readonly IServiceProvider _serviceProvider;
    private readonly PaymentManager _paymentManager;

    private Payment _selected;
    private AppState _appState;

    public Payment Selected
    {
        get { return _selected; }
        set { SetProperty(ref _selected, value); }
    }

    public ObservableCollection<Payment> Payments { get; private set; } = new ObservableCollection<Payment>();

    public PaymentViewModel(IServiceProvider serviceProvider, PaymentManager paymentManager, AppState appState)
    {
        //_paymentDataService = paymentDataService;
        _serviceProvider = serviceProvider;
        _paymentManager = paymentManager;
        _appState = appState;
    }

    [RelayCommand]
    private void EditPayment(Payment selected)
    {
       // MessageBox.Show($"EditPayment {selected}", "EditPayment", MessageBoxButton.OK, MessageBoxImage.Information);
        if (selected != null)
        {
            // Create a deep clone of the selected payment
            Payment temporaryPayment = (Payment)Activator.CreateInstance(selected.GetType(), selected);

            var viewModel = _serviceProvider.GetRequiredService<EditPaymentViewModel>();
            viewModel.Initialize(temporaryPayment); // Pass the temporary payment to the view model

            var editWindow = new EditPaymentWindow(viewModel);
            var isOK = editWindow.ShowDialog();

            if (isOK == true)
            {
                // Apply the changes to the original payment if the user confirms
                _paymentManager.Update(selected.ID, temporaryPayment);
                RefreshPaymentsAsync();
                Selected = Payments.FirstOrDefault(e => e.ID == temporaryPayment.ID);

                // Set AppState dirty bit
                _appState.IsDirty = true;
           }

        }
        else
            MessageBox.Show("Please select a payment");
    }

    [RelayCommand]
    private void DeletePayment(Payment selected)
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
                _paymentManager.Remove(selected.ID);
                //await _paymentDataService.RemoveAsync(selected.ID);
                Selected = Payments.FirstOrDefault();

                // Set AppState dirty bit
                _appState.IsDirty = true;
            }
        }
        else
        {
            MessageBox.Show("Please select an payment", "No Payment Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }


    [RelayCommand]
    private void OpenPaymentForm(string selectedType)
    {
        //MessageBox.Show($"OpenPaymentForm {selectedType}", "OpenPaymentForm", MessageBoxButton.OK, MessageBoxImage.Information);

        var temp = Selected;
        // Resolve the ViewModel from the DI container
        var viewModel = _serviceProvider.GetRequiredService<CreatePaymentViewModel>();

        // Initialize the ViewModel with the choosen estate type
        viewModel.Initialize(selectedType);

        // Open the CreateEstateWindow with the ViewModel
        var window = new CreatePaymentWindow(viewModel);
        var isOK = window.ShowDialog();

        //force refresh
        if (isOK == true)
        {
            _paymentManager.Add(viewModel.Selected.ID, viewModel.Selected); // Add to PaymentManager
            RefreshPaymentsAsync();
            Selected = Payments.FirstOrDefault(e => e.ID == viewModel.Selected.ID);

            // Set AppState dirty bit
            _appState.IsDirty = true;

        }
        else if (temp != null)
            Selected = Payments.FirstOrDefault(e => e.ID == temp.ID);
    }

    private void RefreshPaymentsAsync()
    {
        Payments.Clear();

        var data = _paymentManager.GetAll();  // Retrieve payments from PaymentManager

        foreach (var payment in data)
        {
            Payments.Add(payment);
        }
    }


    public void OnNavigatedTo(object parameter)
    {
        RefreshPaymentsAsync();
        Selected = Payments.FirstOrDefault();
    }

    public void OnNavigatedFrom()
    {
    }
}
