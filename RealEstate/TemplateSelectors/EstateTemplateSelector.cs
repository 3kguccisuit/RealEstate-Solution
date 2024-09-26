using System.Windows;
using System.Windows.Controls;
using RealEstate.Core.Models.ConcreteModels;


namespace RealEstate.TemplateSelectors
{
    public class EstateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VillaTemplate { get; set; }
        public DataTemplate ApartmentTemplate { get; set; }
        public DataTemplate TownhouseTemplate { get; set; }
        public DataTemplate HospitalTemplate { get; set; }
        public DataTemplate SchoolTemplate { get; set; }
        public DataTemplate UniversityTemplate { get; set; }

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
            else if (item is Townhouse)
            {
                return TownhouseTemplate;
            }
            else if (item is Hospital)
            {
                return HospitalTemplate;
            }
            else if (item is School)
            {
                return SchoolTemplate;
            }
            else if (item is University) { return UniversityTemplate; }

            return base.SelectTemplate(item, container);
        }
    }
}
