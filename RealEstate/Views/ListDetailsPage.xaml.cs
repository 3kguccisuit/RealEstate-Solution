using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class ListDetailsPage : Page
{
    public ListDetailsPage(ListDetailsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
