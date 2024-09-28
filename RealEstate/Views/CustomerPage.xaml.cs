using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class CustomerPage : Page
{
    public CustomerPage(CustomerViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
