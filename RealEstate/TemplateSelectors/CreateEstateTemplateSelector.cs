using System.Windows;
using System.Windows.Controls;
using RealEstate.Core.Models.ConcreteModels;

namespace RealEstate.TemplateSelectors
{
    public class CreateEstateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VillaCreateTemplate { get; set; }
        public DataTemplate ApartmentCreateTemplate { get; set; }
        public DataTemplate TownhouseCreateTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Villa)
            {
                return VillaCreateTemplate;
            }
            else if (item is Apartment)
            {
                return ApartmentCreateTemplate;
            }
            else if (item is Townhouse) {
                return TownhouseCreateTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
