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

            this.Closing += (sender, e) =>
            {
                var vm = DataContext as EditEstateViewModel;
                if (vm != null && vm.OnWindowClosingCommand.CanExecute(e))
                {
                    vm.OnWindowClosingCommand.Execute(e);
                }
            };

        }
    }
}
