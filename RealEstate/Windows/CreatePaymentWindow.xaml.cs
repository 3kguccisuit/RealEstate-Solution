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

            this.Closing += (sender, e) =>
            {
                var vm = DataContext as CreatePaymentViewModel;
                if (vm != null && vm.OnWindowClosingCommand.CanExecute(e))
                {
                    vm.OnWindowClosingCommand.Execute(e);
                }
            };
        }
    }
}
