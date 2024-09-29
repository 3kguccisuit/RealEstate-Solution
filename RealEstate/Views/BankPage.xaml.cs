using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class BankPage : Page
{
    public BankPage(BankViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
