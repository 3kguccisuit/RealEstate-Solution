using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels.Payments;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Hotel : Commercial
    {
        public bool HasSpa { get; set; }
        public override string Type => "Hotel";
        [JsonConstructor]
        public Hotel(string id, Address address, LegalForm legalForm, double squareMeters, bool hasSpa)
            : base(id, address, legalForm, squareMeters)
        {
            HasSpa = hasSpa;
        }
        public Hotel() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm(), 0)
        {
            
        }
        public override Estate AutoFill()
        {
            return new Hotel(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    false); 
        }
        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has Spa: {HasSpa}";
        }
    }
}
