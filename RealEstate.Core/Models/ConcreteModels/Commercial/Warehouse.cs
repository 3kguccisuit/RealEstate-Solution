using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Warehouse : Commercial
    {
        public int LoadingDocks { get; set; }
        public override string Type => "Warehouse";
        [JsonConstructor]
        public Warehouse(string id, Address address, LegalForm legalForm, double squareMeters, int loadingDocks)
            : base(id, address, legalForm, squareMeters)
        {
            LoadingDocks = loadingDocks;
        }

        public override Estate AutoFill()
        {
            return new Warehouse(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    0 // LoadingDocks
                );
        }
        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Loading Docks: {LoadingDocks}";
        }
    }
}
