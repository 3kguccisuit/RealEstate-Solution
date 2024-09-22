using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Apartment : Residential
    {
        public int FloorLevel { get; set; }
        public override string Type => "Apartment";
        [JsonConstructor]
        public Apartment(string id, Address address, LegalForm legalForm, int numberOfRooms, int floorLevel)
            : base(id, address, legalForm, numberOfRooms) // Pass id, address, legalForm, numberOfRooms to base constructor
        {
            FloorLevel = floorLevel;
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Floor Level: {FloorLevel}";
        }
    }
}