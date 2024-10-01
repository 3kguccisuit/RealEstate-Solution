using RealEstate.ViewModels;
using System.Windows;

namespace RealEstate.Windows
{
    /// <summary>
    /// Interaction logic for CreateEstateWindow.xaml
    /// </summary>
    public partial class CreateEstateWindow : Window
    {
        public CreateEstateWindow(CreateEstateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
