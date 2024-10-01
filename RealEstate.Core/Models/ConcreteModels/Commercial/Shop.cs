using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Shop : Commercial
    {
        public bool HasOnlineStore { get; set; }
        public override string Type => "Shop";
        [JsonConstructor]
        public Shop(string id, Address address, LegalForm legalForm, double squareMeters, bool hasOnlineStore)
            : base(id, address, legalForm, squareMeters)
        {
            HasOnlineStore = hasOnlineStore;
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has Online Store: {HasOnlineStore}";
        }
    }
}
