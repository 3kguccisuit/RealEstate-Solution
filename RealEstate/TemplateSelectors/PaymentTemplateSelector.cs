using RealEstate.Core.Models.ConcreteModels.Payments;
using System.Windows;
using System.Windows.Controls;


namespace RealEstate.TemplateSelectors
{
    public class PaymentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BankTemplate { get; set; }
        public DataTemplate PayPalTemplate { get; set; }
        public DataTemplate VippsTemplate { get; set; }
        public DataTemplate WesternUnionTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Bank)
            {
                return BankTemplate;
            }
            else if (item is PayPal)
            {
                return PayPalTemplate;
            }
            else if (item is Vipps)
            {
                return VippsTemplate;
            }
            else if (item is WesternUnion)
            {
                return WesternUnionTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
