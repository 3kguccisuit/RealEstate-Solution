using System.Windows;
using System.Windows.Controls;
using RealEstate.Core.Models.ConcreteModels;


namespace RealEstate.TemplateSelectors
{
    public class EstateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VillaTemplate { get; set; }
        public DataTemplate ApartmentTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Villa)
            {
                return VillaTemplate;
            }
            else if (item is Apartment)
            {
                return ApartmentTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
