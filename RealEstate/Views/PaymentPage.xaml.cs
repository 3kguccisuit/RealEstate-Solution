using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class PaymentPage : Page
{
    public PaymentPage(PaymentViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
