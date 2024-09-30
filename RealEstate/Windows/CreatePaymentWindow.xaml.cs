using RealEstate.ViewModels;
using System.Windows;

namespace RealEstate.Windows
{
    /// <summary>
    /// Interaction logic for CreateEstateWindow.xaml
    /// </summary>
    public partial class CreatePaymentWindow : Window
    {
        public CreatePaymentWindow(CreatePaymentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; 
        }
    }
}
