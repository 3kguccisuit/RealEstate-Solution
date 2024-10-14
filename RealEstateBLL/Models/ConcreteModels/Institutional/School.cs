using RealEstateBLL.Enums;
using RealEstateBLL.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstateBLL.Models.ConcreteModels
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
        public School() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {
            
        }
        public override Estate AutoFill()
        {
            return new School(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // Parking spaces
                    0 // Number of classrooms
                );
        }
        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Number of Classrooms: {NumberOfClassrooms}";
        }
    }
}
