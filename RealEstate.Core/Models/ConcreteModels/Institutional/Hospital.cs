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

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Number of Beds: {NumberOfBeds}";
        }
    }
}
