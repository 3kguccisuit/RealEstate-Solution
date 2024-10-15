using DTO.Models.ConcreteModels;
using System.Windows;
using System.Windows.Controls;

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
        public DataTemplate HotelCreateTemplate { get; set; }
        public DataTemplate ShopCreateTemplate { get; set; }
        public DataTemplate WarehouseCreateTemplate { get; set; }
        public DataTemplate FactoryCreateTemplate { get; set; }

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
            else if (item is Hotel)
            {
                return HotelCreateTemplate;
            }
            else if (item is Shop)
            {
                return ShopCreateTemplate;
            }
            else if (item is Warehouse)
            {
                return WarehouseCreateTemplate;
            }
            else if (item is Factory)
            {
                return FactoryCreateTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
