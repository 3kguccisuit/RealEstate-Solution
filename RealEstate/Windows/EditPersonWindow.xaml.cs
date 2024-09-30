using RealEstate.ViewModels;
using System.Windows;


namespace RealEstate.Windows
{
    /// <summary>
    /// Interaction logic for EditPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : Window
    {
        public EditPersonWindow(EditPersonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
