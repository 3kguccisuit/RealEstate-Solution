using DTO.Enums;
using DTO.Models.BaseModels;
using System.Text.Json.Serialization;

namespace DTO.Models.ConcreteModels
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
        public Factory() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {

        }

        public override Estate AutoFill()
        {
            return new Factory(Guid.NewGuid().ToString("D"),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    false);
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has Warehouse: {HasWarehouse}";
        }
    }
}
