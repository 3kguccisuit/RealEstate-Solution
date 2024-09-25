using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Townhouse : Residential
    {
        public bool HasGarden { get; set; }

        public override string Type => "Townhouse";

        [JsonConstructor]
        public Townhouse(string id, Address address, LegalForm legalForm, int numberOfRooms, bool hasGarden)
            : base(id, address, legalForm, numberOfRooms)  // Pass LegalForm and other common properties to base class
        {
            HasGarden = hasGarden;
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has garden: {HasGarden}";
        }
    }
}
