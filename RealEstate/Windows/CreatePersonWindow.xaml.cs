using RealEstate.ViewModels;
using System.Windows;

namespace RealEstate.Windows
{
    /// <summary>
    /// Interaction logic for CreatePersonWindow.xaml
    /// </summary>
    public partial class CreatePersonWindow : Window
    {
        public CreatePersonWindow(CreatePersonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
