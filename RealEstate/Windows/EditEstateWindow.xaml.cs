using RealEstate.ViewModels;
using System.Windows;

namespace RealEstate.Windows
{
    /// <summary>
    /// Interaction logic for EditEstateWindow.xaml
    /// </summary>
    public partial class EditEstateWindow : Window
    {
        public EditEstateWindow(EditEstateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; 
        }
    }
}
