using RealEstateBLL.Enums;
using RealEstateBLL.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstateBLL.Models.ConcreteModels
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
        public Shop() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {
            
        }
        public override Estate AutoFill()
        {
            return new Shop(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    false // HasOnlineStore
                );
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has Online Store: {HasOnlineStore}";
        }
    }
}
