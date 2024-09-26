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
        public DataTemplate HospitalCreateTemplate { get; set; }
        public DataTemplate SchoolCreateTemplate { get; set; }
        public DataTemplate UniversityCreateTemplate { get; set; }

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
            else if (item is Townhouse)
            {
                return TownhouseCreateTemplate;
            }
            else if (item is Hospital)
            {
                return HospitalCreateTemplate;
            }
            else if (item is School)
            {
                return SchoolCreateTemplate;
            }
            else if (item is University)
            {
                return UniversityCreateTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
