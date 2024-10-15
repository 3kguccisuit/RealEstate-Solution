
using DTO.Enums;
using DTO.Models.BaseModels;
using System.Text.Json.Serialization;

namespace DTO.Models.ConcreteModels
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
        public University() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {
            
        }

        public override Estate AutoFill()
        {
            return new University(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of programs
                );
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Number of Programs: {NumberOfPrograms}";
        }
    }
}
