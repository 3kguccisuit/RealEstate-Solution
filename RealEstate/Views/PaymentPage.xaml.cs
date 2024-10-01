using RealEstate.ViewModels;
using System.Windows.Controls;

namespace RealEstate.Views;

public partial class PaymentPage : Page
{
    public PaymentPage(PaymentViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
