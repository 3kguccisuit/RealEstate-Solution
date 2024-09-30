using System.Windows.Controls;

using RealEstate.ViewModels;

namespace RealEstate.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
