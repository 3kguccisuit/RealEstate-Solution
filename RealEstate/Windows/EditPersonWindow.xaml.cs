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

            this.Closing += (sender, e) =>
            {
                var vm = DataContext as EditPersonViewModel;
                if (vm != null && vm.OnWindowClosingCommand.CanExecute(e))
                {
                    vm.OnWindowClosingCommand.Execute(e);
                }
            };

        }
    }
}
