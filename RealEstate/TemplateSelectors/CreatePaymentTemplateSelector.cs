using System.Windows;
using System.Windows.Controls;

using RealEstate.Core.Models.ConcreteModels.Payments;

namespace RealEstate.TemplateSelectors
{
    public class CreatePaymentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BankCreateTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Bank)
            {
                return BankCreateTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
