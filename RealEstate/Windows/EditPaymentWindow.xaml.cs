using RealEstate.ViewModels;
using System.Windows;

namespace RealEstate.Windows
{
    /// <summary>
    /// Interaction logic for CreateEstateWindow.xaml
    /// </summary>
    public partial class EditPaymentWindow : Window
    {
        public EditPaymentWindow(EditPaymentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
