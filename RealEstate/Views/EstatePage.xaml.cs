using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class EstatePage : Page
{
    public EstatePage(EstateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
