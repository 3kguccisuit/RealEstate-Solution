using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class MainPage : Page
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
