using RealEstate.ViewModels;
using System.Windows.Controls;

namespace RealEstate.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
