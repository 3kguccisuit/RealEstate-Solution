using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class University : Institutional
    {
        public int NumberOfPrograms { get; set; }
        public override string Type => "University";

        [JsonConstructor]
        public University(string id, Address address, LegalForm legalForm, int parkingSpaces, int numberOfPrograms)
            : base(id, address, legalForm, parkingSpaces)
        {
            NumberOfPrograms = numberOfPrograms;
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Number of Programs: {NumberOfPrograms}";
        }
    }
}
