using System.Windows;
using RealEstate.ViewModels;

namespace RealEstate.Windows
{
    /// <summary>
    /// Interaction logic for ApartmentFormWindow.xaml
    /// </summary>
    public partial class ApartmentFormWindow : Window
    {
        public ApartmentFormWindow(ApartmentFormViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
