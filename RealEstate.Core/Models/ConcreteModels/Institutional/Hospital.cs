using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Hospital : Institutional
    {
        public int NumberOfBeds { get; set; }
        public override string Type => "Hospital";
        [JsonConstructor]
        public Hospital(string id, Address address, LegalForm legalForm, int parkingSpaces, int numberOfBeds)
            : base(id, address, legalForm, parkingSpaces)
        {
            NumberOfBeds = numberOfBeds;
        }
        public Hospital() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {
            
        }
        public override Estate AutoFill()
        {
            return new Hospital(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of beds
                );
        }
        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Number of Beds: {NumberOfBeds}";
        }
    }
}
