using RealEstate.Core.Models.BaseModels;
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

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has Spa: {HasSpa}";
        }
    }
}
