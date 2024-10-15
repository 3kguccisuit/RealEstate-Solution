using DTO.Enums;
using DTO.Models.BaseModels;
using System.Text.Json.Serialization;

namespace DTO.Models.ConcreteModels
{
    public class Apartment : Residential
    {
        public int FloorLevel { get; set; }
        public override string Type => "Apartment";
        [JsonConstructor]
        public Apartment(string id, Address address, LegalForm legalForm, int numberOfRooms, int floorLevel)
            : base(id, address, legalForm, numberOfRooms)
        {
            FloorLevel = floorLevel;
        }

        public Apartment() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {

        }
        public override Estate AutoFill()
        {
            return new Apartment(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of programs
                );
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Floor Level: {FloorLevel}";
        }
    }
}