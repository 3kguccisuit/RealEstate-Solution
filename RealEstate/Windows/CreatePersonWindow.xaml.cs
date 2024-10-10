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

            this.Closing += (sender, e) =>
            {
                var vm = DataContext as CreatePersonViewModel;
                if (vm != null && vm.OnWindowClosingCommand.CanExecute(e))
                {
                    vm.OnWindowClosingCommand.Execute(e);
                }
            };

        }
    }
}
