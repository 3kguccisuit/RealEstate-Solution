using RealEstate.ViewModels;
using System.Windows.Controls;

namespace RealEstate.Views;

public partial class MainPage : Page
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
