using RealEstate.Core.Models.ConcreteModels.Payments;
using System.Windows;
using System.Windows.Controls;

namespace RealEstate.TemplateSelectors
{
    public class CreatePaymentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BankCreateTemplate { get; set; }
        public DataTemplate PayPalCreateTemplate { get; set; }
        public DataTemplate VippsCreateTemplate { get; set; }
        public DataTemplate WesternUnionCreateTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Bank)
            {
                return BankCreateTemplate;
            }
            else if (item is PayPal)
            {
                return PayPalCreateTemplate;
            }
            else if (item is Vipps)
            {
                return VippsCreateTemplate;
            }
            else if (item is WesternUnion)
            {
                return WesternUnionCreateTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
