using RealEstate.Core.Enums;
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
        public Villa(string id, Address address, LegalForm legalForm, int numberOfRooms, int numberOfFloors, bool hasGarage)
            : base(id, address, legalForm, numberOfRooms)
        {
            this.NumberOfFloors = numberOfFloors;
            this.HasGarage = hasGarage;
        }
        public Villa() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {
            
        }
        public override Estate AutoFill()
        {
            return new Villa(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0,
                    true// Number of programs
                );
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Number of Floors: {NumberOfFloors}, Has Garage: {HasGarage}";
        }
    }
}
