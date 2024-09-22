using System.Windows;
using System.Windows.Controls;
using RealEstate.Core.Models.ConcreteModels;

namespace RealEstate.TemplateSelectors
{
    public class EditEstateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VillaEditTemplate { get; set; }
        public DataTemplate ApartmentEditTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Villa)
            {
                return VillaEditTemplate;
            }
            else if (item is Apartment)
            {
                return ApartmentEditTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
