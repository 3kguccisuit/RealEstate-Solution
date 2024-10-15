using DTO.Models.ConcreteModels.Persons;
using System.Windows;
using System.Windows.Controls;

namespace RealEstate.TemplateSelectors
{
    public class PersonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BuyerTemplate { get; set; }
        public DataTemplate SellerTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Buyer)
            {
                return BuyerTemplate;
            }
            else if (item is Seller)
            {
                return SellerTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
