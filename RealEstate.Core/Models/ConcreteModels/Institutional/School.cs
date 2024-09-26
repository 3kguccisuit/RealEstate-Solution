using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class School : Institutional
    {
        public int NumberOfClassrooms { get; set; }
        public override string Type => "School";
        [JsonConstructor]
        public School(string id, Address address, LegalForm legalForm, int parkingSpaces, int numberOfClassrooms)
            : base(id, address, legalForm, parkingSpaces)
        {
            NumberOfClassrooms = numberOfClassrooms;
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Number of Classrooms: {NumberOfClassrooms}";
        }
    }
}
