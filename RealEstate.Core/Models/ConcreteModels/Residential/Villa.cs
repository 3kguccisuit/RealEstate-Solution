using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Villa : Residential
    {
        public int NumberOfFloors { get; set; }
        public bool HasGarage { get; set; }
        public override string Type => "Villa";

        [JsonConstructor]
        public Villa(int id, Address address, LegalForm legalForm, int numberOfRooms, int numberOfFloors, bool hasGarage)
            : base(id, address, legalForm, numberOfRooms) // Pass id, address, legalForm, numberOfRooms to base constructor
        {
            this.NumberOfFloors = numberOfFloors;
            this.HasGarage = hasGarage;
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Number of Floors: {NumberOfFloors}, Has Garage: {HasGarage}";
        }
    }
}
