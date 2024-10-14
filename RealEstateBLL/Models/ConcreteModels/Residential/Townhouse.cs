
using RealEstateBLL.Enums;
using RealEstateBLL.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstateBLL.Models.ConcreteModels
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
        public Townhouse():base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {
            
        }
        public override Estate AutoFill()
        {
            return new Townhouse(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    true // Number of programs
                );
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has garden: {HasGarden}";
        }
    }
}
