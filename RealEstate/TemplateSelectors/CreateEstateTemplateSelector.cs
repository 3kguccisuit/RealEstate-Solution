using System.Windows;
using System.Windows.Controls;
using RealEstate.Core.Models.ConcreteModels;

namespace RealEstate.TemplateSelectors
{
    public class CreateEstateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VillaCreateTemplate { get; set; }
        public DataTemplate ApartmentCreateTemplate { get; set; }

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

            return base.SelectTemplate(item, container);
        }
    }
}
