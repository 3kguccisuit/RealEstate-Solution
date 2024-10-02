using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels.Payments;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Factory : Commercial
    {

        public bool HasWarehouse { get; set; }
        public override string Type => "Factory";

        [JsonConstructor]
        public Factory(string id, Address address, LegalForm legalForm, double squareMeters, bool hasWarehouse)
            : base(id, address, legalForm, squareMeters)
        {
            HasWarehouse = hasWarehouse;
        }

        public override Estate AutoFill()
        {
            return new Factory(Guid.NewGuid().ToString("D"),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, 
                    false );
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has Warehouse: {HasWarehouse}";
        }
    }
}
