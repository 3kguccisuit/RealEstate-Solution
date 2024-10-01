using RealEstate.ViewModels;
using System.Windows.Controls;

namespace RealEstate.Views;

public partial class EstatePage : Page
{
    public EstatePage(EstateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
