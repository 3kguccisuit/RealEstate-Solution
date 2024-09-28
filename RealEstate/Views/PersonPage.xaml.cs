using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class PersonPage : Page
{
    public PersonPage(PersonViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
