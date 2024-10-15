using DTO.Models.ConcreteModels.Persons;
using System.Windows;
using System.Windows.Controls;

namespace RealEstate.TemplateSelectors
{
    public class CreatePersonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SellerCreateTemplate { get; set; }
        public DataTemplate BuyerCreateTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Seller)
            {
                return SellerCreateTemplate;
            }
            else if (item is Buyer)
            {
                return BuyerCreateTemplate;

            }
            return base.SelectTemplate(item, container);
        }
    }
}
