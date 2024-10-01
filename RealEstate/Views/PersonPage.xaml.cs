using RealEstate.ViewModels;
using System.Windows.Controls;

namespace RealEstate.Views;

public partial class PersonPage : Page
{
    public PersonPage(PersonViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
