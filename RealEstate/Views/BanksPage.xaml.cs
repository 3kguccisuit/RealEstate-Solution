using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class BanksPage : Page
{
    public BanksPage(BanksViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
