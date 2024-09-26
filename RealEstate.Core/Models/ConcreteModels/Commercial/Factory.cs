using RealEstate.Core.Models.BaseModels;
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

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has Warehouse: {HasWarehouse}";
        }
    }
}
